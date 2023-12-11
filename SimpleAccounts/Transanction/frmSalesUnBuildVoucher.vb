
Imports SBDal
Imports SBModel
Public Class frmSalesUnBuildVoucher
    Implements IGeneral
    Public Shared AdjustmentAmount As Integer = 0I
    Public Shared SalesAccount As Integer = 0I
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillUltraDropDown(Me.cmbCustomer, "SELECT coa_detail_id,detail_title FROM vwCOADetail WHERE coa_detail_id = " & frmSales.UnbuildCustomerId & "")
            Me.cmbCustomer.Rows(1).Activate()
            FillUltraDropDown(Me.cmbSalesAccount, "SELECT coa_detail_id,detail_title FROM vwCOADetail WHERE Active = 1 AND main_type = 'Income'")
            Me.cmbSalesAccount.Rows(0).Activate()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strsql As String = ""
            strsql = "SELECT ISNULL(SUM(ISNULL(debit_amount, 0)) - SUM(ISNULL(credit_amount, 0)),0) AS Balance FROM tblVoucherDetail WHERE coa_detail_id = " & frmSales.UnbuildCustomerId & ""  'AND CostCenterID = " & frmSales.CostCenterId & ""
            Dim dt As DataTable = GetDataTable(strsql)
            If dt.Rows.Count > 0 Then
                Me.txtBalance.Text = dt.Rows(0).Item(0).ToString
            Else
                Me.txtBalance.Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbSalesAccount.Value = 0 AndAlso (Val(Me.txtInvoiceAmount.Text) - Val(Me.txtAdjustmentAmount.Text)) > 0 Then
                msg_Error("Please select the Sales Account")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbCustomer.Value = 0
            Me.txtBalance.Text = 0
            Me.txtInvoiceAmount.Text = 0
            Me.txtAdjustmentAmount.Text = 0
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

    Private Sub frmSalesUnBuildVoucher_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
            FillCombos()
            Me.txtInvoiceAmount.Text = frmSales.InvoiceAmount
            GetAllRecords()
            If Val(Me.txtBalance.Text) <= Val(Me.txtInvoiceAmount.Text) Then
                Me.txtAdjustmentAmount.Text = Me.txtInvoiceAmount.Text
            Else
                Me.txtAdjustmentAmount.Text = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnApplyChanges_Click(sender As Object, e As EventArgs) Handles btnApplyChanges.Click
        Try
            AdjustmentAmount = Me.txtAdjustmentAmount.Text
            If IsValidate() = True Then
                If (Val(Me.txtInvoiceAmount.Text) - Val(Me.txtAdjustmentAmount.Text)) > 0 Then
                    SalesAccount = Me.cmbSalesAccount.Value
                Else
                    SalesAccount = 0
                End If
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class