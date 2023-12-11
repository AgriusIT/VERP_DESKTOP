Imports SBDal
Imports SBModel
Public Class frmUpdateCurrency
    Implements IGeneral
    Dim list As List(Of clsCurrencyRate)


    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            '    For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdCurrencyRate.RootTable.Columns
            '        Dim filter As Janus.Windows.GridEX.GridEXFilterCondition
            '        filter.
            '        Me.grdCurrencyRate.RootTable.FilterCondition()

            '    Next
            '    'Me.grdCurrencyRate.RootTable.AlternatingRowFormatStyle.
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillCombo(Optional Condition As String = "", Optional ByVal currencyId As Integer = 0)
        Try
            Dim str As String = String.Empty
            Try
                str = "Select currency_id, currency_code from tblCurrency Where currency_id =" & currencyId & ""
                FillDropDown(cmbCurrency, str, False)
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            list = New List(Of clsCurrencyRate)
            Dim obj As clsCurrencyRate
            For Each row As Janus.Windows.GridEX.GridEXRow In grdCurrencyRate.GetRows
                obj = New clsCurrencyRate
                obj.UpdateDate = Me.dtpDate.Value
                'obj.CurrencyName = row.Cells("CurrencyName").Value.ToString
                'obj.CurrencyRate = Val(row.Cells("currency_code").Value.ToString)
                obj.CurrencyRate = Val(row.Cells("CurrencyRate").Value.ToString)
                obj.OldRate = Val(row.Cells("OldRate").Value.ToString)
                obj.CurrencyId = Val(row.Cells("currency_id").Value.ToString)
                obj.BasicCurrencyId = Val(Me.cmbCurrency.SelectedValue.ToString)
                list.Add(obj)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Dim dt As New DataTable
        Dim dv As New DataView
        Dim dt1 As New DataTable
        Try

            dt = New CurrencyRateDAL().GetAll()
            dt.TableName = "Currencies"
            dv.Table = dt
            dv.RowFilter = "currency_id <>'" & Me.cmbCurrency.SelectedValue & "'"

            Me.grdCurrencyRate.DataSource = dv.ToTable
            'Me.grdCurrencyRate.RetrieveStructure()
            Me.grdCurrencyRate.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim dal As New CurrencyRateDAL
            dal.Add(list)
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

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmUpdateCurrency_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FillCombo(, Val(getConfigValueByType("Currency")))
            'Me.txtBaseCurrency.Text = GetBaseCurrency(Val(getConfigValueByType("Currency"))).ToString
            'If Me.txtBaseCurrency.Text.ToUpper = "ERROR" Then
            '    Me.txtBaseCurrency.Text = "Not set yet"
            'End If
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            FillModel()
            Save()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetBaseCurrency(ByVal currencyID As Integer) As String
        Dim dt As New DataTable
        Dim str As String = String.Empty
        Try
            str = "Select currency_id, currency_code From tblCurrency Where currency_id = " & currencyID & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Return dt.Rows(0).Item(0).ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class