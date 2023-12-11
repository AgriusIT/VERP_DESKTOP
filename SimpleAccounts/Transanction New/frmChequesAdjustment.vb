Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmChequesAdjustment

    Implements IGeneral
    Dim IsOpenForm As Boolean = False
    Dim PK_ID As Integer = 0I

    Dim objMod As ReceivedChequeAdjustmentBE

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.GridEX1.RootTable.Columns.Add("Delete")
            Me.GridEX1.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.GridEX1.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.GridEX1.RootTable.Columns("Delete").ButtonText = "Delete"

            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.GridEX1.RootTable.Columns("PK_ID").Visible = False
            Me.GridEX1.RootTable.Columns("cheque_voucher_detail_id").Visible = False
            Me.GridEX1.RootTable.Columns("cheque_voucher_id").Visible = False
            Me.GridEX1.RootTable.Columns("Adjsted_Voucher_Id").Visible = False
            Me.GridEX1.RootTable.Columns("coa_detail_id").Visible = False
            Me.GridEX1.RootTable.Columns("Adjustment_Amount").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Adjustment_Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Adjustment_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adjustment_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.GridEX1.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.GridEX1.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
            Me.GridEX1.RootTable.Columns("Cheque Voucher Date").FormatString = "dd/MMM/yyyy"
            Me.GridEX1.RootTable.Columns("Adjusted Voucher Date").FormatString = "dd/MMM/yyyy"


            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New ReceivedChequeAdjustmentDAL().Delete(objMod) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try


            If Condition = "Accounts" Then
                FillUltraDropDown(Me.cmbAccounts, "Select coa_detail_id, detail_title as [Account Title],detail_code as [Account Code] From vwCOADetail WHERE detail_title <> '' AND Active=1 ORDER BY 1 ASC")
                Me.cmbAccounts.Rows(0).Activate()
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "ChequeList" Then
                FillUltraDropDown(Me.cmbChequeList, "Select b.voucher_detail_id,b.cheque_no as [Cheque No], b.cheque_date as [Cheque Date],  b.credit_amount as Amount, IsNull(c.Adjustment,0) as Adjustment, a.voucher_no as [V No],a.voucher_date as [V Date],b.voucher_id, b.coa_detail_id From tblVoucherDetail b INNER JOIN tblVoucher a  on a.voucher_id = b.voucher_id LEFT OUTER JOIN (SELECT  coa_detail_id, cheque_voucher_detail_id, cheque_voucher_id, SUM(ISNULL(Adjustment_Amount, 0)) AS Adjustment FROM dbo.ReceivedChequeAdjustmentTable GROUP BY coa_detail_id, cheque_voucher_detail_id, cheque_voucher_id) c On b.voucher_detail_id = c.cheque_voucher_detail_id and b.voucher_id = c.cheque_voucher_id and b.coa_detail_id = c.coa_detail_id WHERE (a.voucher_type_id in(3,5)) and IsNull(b.credit_amount,0)-IsNull(c.Adjustment,0) <> 0 and IsNull(a.post,0)=0 and IsNull(a.checked,0)=0 And b.cheque_no <> '' And b.coa_detail_id =" & IIf(Me.cmbAccounts.Value > 0, "" & Val(Me.cmbAccounts.ActiveRow.Cells("coa_detail_id").Value.ToString) & "", "0") & "  ORDER BY b.voucher_detail_id DESC")
                Me.cmbChequeList.Rows(0).Activate()
                cmbChequeList.DisplayLayout.Bands(0).Columns("voucher_detail_id").Hidden = True
                cmbChequeList.DisplayLayout.Bands(0).Columns("voucher_id").Hidden = True
                cmbChequeList.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "VoucherList" Then
                FillUltraDropDown(Me.cmbVoucherList, "Select Voucher_Id, [V No], [V Date], Amount, Settled, coa_detail_id From( SELECT V.voucher_id, V.voucher_no as [V No], V.voucher_date as [V Date], SUM(ISNULL(V_D.credit_amount, 0)) AS Amount, IsNull(c.Adjustment,0) As Settled,  V_D.coa_detail_id FROM  dbo.tblVoucher AS V INNER JOIN dbo.tblVoucherDetail AS V_D ON V.voucher_id = V_D.voucher_id LEFT OUTER JOIN (SELECT coa_detail_id, Adjsted_Voucher_Id, SUM(ISNULL(Adjustment_Amount, 0)) AS Adjustment FROM dbo.ReceivedChequeAdjustmentTable AS Rec_Cheq GROUP BY coa_detail_id, Adjsted_Voucher_Id) c On c.Adjsted_Voucher_Id = V.voucher_id and c.coa_detail_id = V_D.coa_detail_id WHERE (V.voucher_type_id IN (3,5,1)) AND V_D.coa_detail_id=" & IIf(Me.cmbAccounts.Value > 0, "" & Val(Me.cmbAccounts.ActiveRow.Cells("coa_detail_id").Value.ToString) & "", "0") & " and (IsNull(post,0) <> 0 or IsNull(checked,0) <> 0) GROUP BY V.voucher_id, V.voucher_no, V.voucher_date, V_D.coa_detail_id, IsNull(c.Adjustment,0)) a where (isnull(a.amount,0)-isnull(a.settled,0)) <> 0")
                cmbVoucherList.Rows(0).Activate()
                cmbVoucherList.DisplayLayout.Bands(0).Columns("voucher_id").Hidden = True
                cmbVoucherList.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            objMod = New ReceivedChequeAdjustmentBE
            objMod.Adjusted_voucher_id = Me.cmbVoucherList.Value
            objMod.Adjustment_Amount = Val(Me.txtReceivedAmount.Text)
            If Not IsDBNull(Me.cmbChequeList.ActiveRow.Cells("Cheque Date").Value) Then
                objMod.cheque_date = Me.cmbChequeList.ActiveRow.Cells("Cheque Date").Value
            Else
                objMod.cheque_date = Date.Now
            End If
            If Not IsDBNull(Me.cmbChequeList.ActiveRow.Cells("Cheque No").Value) Then
                objMod.cheque_no = Me.cmbChequeList.ActiveRow.Cells("Cheque No").Value
            Else
                objMod.cheque_no = String.Empty
            End If
            objMod.cheque_voucher_detail_id = Me.cmbChequeList.ActiveRow.Cells("voucher_detail_id").Value
            objMod.cheque_voucher_id = Me.cmbChequeList.ActiveRow.Cells("voucher_id").Value
            objMod.cheque_voucher_no = Me.cmbChequeList.ActiveRow.Cells("V No").Value.ToString
            objMod.coa_detail_id = Me.cmbAccounts.Value
            objMod.DocDate = Me.dtpAdjVoucherDate.Value
            objMod.DocNo = Me.txtAdjVoucherNo.Text
            objMod.Prefix = "Adj-" & Me.dtpAdjVoucherDate.Value.ToString("yy") & "-"
            objMod.Length = 5
            objMod.User_Name = LoginUserName


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim dt As New DataTable
            dt = New ReceivedChequeAdjustmentDAL().GetAllRecords
            dt.AcceptChanges()

            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            ApplyGridSettings()
            CtrlGrdBar3_Load(Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbAccounts.ActiveRow Is Nothing Then
                ShowErrorMessage("Please define account")
                Me.cmbAccounts.Focus()
                Return False
            End If
            If Me.cmbAccounts.Value <= 0 Then
                ShowErrorMessage("Please select account")
                Me.cmbAccounts.Focus()
                Return False
            End If
            If Me.cmbChequeList.Value <= 0 Then
                ShowErrorMessage("Please select cheque.")
                Me.cmbChequeList.Focus()
                Return False
            End If
            If Me.cmbVoucherList.Value <= 0 Then
                ShowErrorMessage("Please select voucher.")
                Me.cmbVoucherList.Focus()
                Return False
            End If
            If Val(Me.txtReceivedAmount.Text) <= 0 Then
                ShowErrorMessage("Please enter received amount.")
                Me.txtReceivedAmount.Focus()
                Return False
            End If
            If Val(Me.txtBalance.Text) < Val(Me.txtReceivedAmount.Text) Then
                ShowErrorMessage("Received amount greater than balance amount")
                Me.txtReceivedAmount.Focus()
                Return False
            End If
            If Val(Me.txtVoucherAmount.Text) < Val(Me.txtReceivedAmount.Text) Then
                ShowErrorMessage("Received amount greater than voucher amount")
                Me.txtReceivedAmount.Focus()
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
            PK_ID = 0I
            Me.btnSave.Text = "&Save"
            Me.txtAdjVoucherNo.Text = ReceivedChequeAdjustmentDAL.GetDocNo("Adj-" & Me.dtpAdjVoucherDate.Value.ToString("yy") & "-", 5, Nothing)
            Me.dtpAdjVoucherDate.Value = Date.Now
            Me.cmbAccounts.Rows(0).Activate()
            Me.cmbChequeList.Rows(0).Activate()
            Me.cmbVoucherList.Rows(0).Activate()
            Me.txtChequeAmount.Text = String.Empty
            Me.txtChequeAdjustedAmount.Text = String.Empty
            Me.txtBalance.Text = String.Empty
            Me.txtVoucherAmount.Text = String.Empty
            Me.txtSattledAmount.Text = String.Empty
            Me.txtReceivedAmount.Text = String.Empty
            GetAllRecords()
            Me.dtpAdjVoucherDate.Focus()
            CtrlGrdBar3_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New ReceivedChequeAdjustmentDAL().Save(objMod) = True Then
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
            If New ReceivedChequeAdjustmentDAL().Update(objMod) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmChequesAdjustment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos("Accounts")
            FillCombos("ChequeList")
            FillCombos("VoucherList")
            IsOpenForm = True
            ReSetControls()

            'TFS3360_Aashir_Added select/contain filters on account feilds in all transaction screens
            UltraDropDownSearching(cmbAccounts, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAccounts_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAccounts.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbAccounts.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbAccounts_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccounts.Leave
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbAccounts.IsItemInList = False Then Exit Sub
            If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
            FillCombos("ChequeList")
            FillCombos("VoucherList")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetChequeDetail()
        Try

            If Me.cmbAccounts.IsItemInList = False Then Exit Sub
            If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbChequeList.IsItemInList = False Then Exit Sub
            If Me.cmbChequeList.ActiveRow Is Nothing Then Exit Sub

            If Me.cmbChequeList.Value > 0 Then
                Me.txtChequeAmount.Text = Val(Me.cmbChequeList.ActiveRow.Cells("Amount").Value.ToString)
                Me.txtChequeAdjustedAmount.Text = Val(Me.cmbChequeList.ActiveRow.Cells("Adjustment").Value.ToString)
                Me.txtBalance.Text = (Val(Me.txtChequeAmount.Text) - Val(Me.txtChequeAdjustedAmount.Text))
            Else
                Me.txtChequeAmount.Text = 0
                Me.txtChequeAdjustedAmount.Text = 0
                Me.txtBalance.Text = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetVoucherDetail()
        Try

            If Me.cmbAccounts.IsItemInList = False Then Exit Sub
            If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbVoucherList.IsItemInList = False Then Exit Sub
            If Me.cmbVoucherList.ActiveRow Is Nothing Then Exit Sub

            If Me.cmbVoucherList.Value > 0 Then
                Me.txtVoucherAmount.Text = Val(Me.cmbVoucherList.ActiveRow.Cells("Amount").Value.ToString)
                Me.txtSattledAmount.Text = Val(Me.cmbVoucherList.ActiveRow.Cells("Settled").Value.ToString)
            Else
                Me.txtVoucherAmount.Text = 0
                Me.txtSattledAmount.Text = 0
                Me.txtReceivedAmount.Text = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbChequeList_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbChequeList.RowSelected
        Try
            GetChequeDetail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbVoucherList_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbVoucherList.RowSelected
        Try
            GetVoucherDetail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = False Then Exit Sub
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            objMod = New ReceivedChequeAdjustmentBE
            objMod.PK_ID = Val(Me.GridEX1.GetRow.Cells("PK_ID").Value.ToString())
            objMod.User_Name = LoginUserName
            If Not IsDBNull(Me.GridEX1.GetRow.Cells("DocDate").Value) Then
                objMod.DocDate = Me.GridEX1.GetRow.Cells("DocDate").Value
            Else
                objMod.DocDate = Date.Now
            End If
            objMod.DocNo = Me.GridEX1.GetRow.Cells("DocNo").Value.ToString()
            objMod.coa_detail_id = Val(Me.GridEX1.GetRow.Cells("coa_detail_id").Value.ToString())
            objMod.cheque_voucher_detail_id = Val(Me.GridEX1.GetRow.Cells("cheque_voucher_detail_id").Value.ToString)
            objMod.cheque_voucher_id = Val(Me.GridEX1.GetRow.Cells("cheque_voucher_id").Value.ToString)
            objMod.Adjusted_voucher_id = Val(Me.GridEX1.GetRow.Cells("Adjsted_Voucher_Id").Value.ToString)

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()

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
    Private Sub GridEX1_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                btnDelete_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridEX1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            'Me.btnSave.Text = "&Update"
            'PK_ID = Val(Me.GridEX1.GetRow.Cells("PK_ID").Value.ToString)
            'Me.cmbAccounts.Value = Val(Me.GridEX1.GetRow.Cells("coa_detail_id").Value.ToString)
            'Me.cmbChequeList.Value = Val(Me.GridEX1.GetRow.Cells("cheque_voucher_detail_id").Value.ToString)
            'Me.cmbVoucherList.Value = Val(Me.GridEX1.GetRow.Cells("cheque_voucher_id").Value.ToString)
            'Me.txtReceivedAmount.Text = Val(Me.GridEX1.GetRow.Cells("Adjustment_Amount").Value.ToString)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            If Me.cmbAccounts.IsItemInList = False Then
                FillCombos("Accounts")
            Else
                id = Me.cmbAccounts.ActiveRow.Cells(0).Value
                FillCombos("Accounts")
                Me.cmbAccounts.Value = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cheques Adjustment"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class