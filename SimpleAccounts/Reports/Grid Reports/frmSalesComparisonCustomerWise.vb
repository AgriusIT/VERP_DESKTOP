Public Class frmSalesComparisonCustomerWise
    Implements IGeneral

    Private Sub frmSalesComparisonCustomerWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim str As String = String.Empty
            str = "SP_SalesComparison '" & Me.dtpFormDate.Value & "','" & Me.dtpToDate.Value & "','" & Me.dtpFromDate1.Value & "','" & Me.dtpToDate1.Value & "'"
            Dim dt As DataTable = GetDataTable(str)
            dt.Columns("Diff").Expression = "Sales1-Sales2"
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("coa_detail_id").Visible = False
            Me.grd.RootTable.Columns("detail_title").Caption = "Customer Name"
            Me.grd.RootTable.Columns("Sales1").Caption = "Sales on " & Me.dtpFormDate.Value.Date.ToString("d-MMM-yy") & " To " & Me.dtpToDate.Value.Date.ToString("d-MMM-yy")
            Me.grd.RootTable.Columns("Sales1").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Sales1").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Sales1").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Sales1").FormatString = "N"
            Me.grd.RootTable.Columns("Sales1").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Sales2").Caption = "Sales on " & Me.dtpFromDate1.Value.Date.ToString("d-MMM-yy") & " To " & Me.dtpToDate1.Value.Date.ToString("d-MMM-yy")
            Me.grd.RootTable.Columns("Sales2").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Sales2").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Sales2").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Sales2").FormatString = "N"
            Me.grd.RootTable.Columns("Sales2").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Diff").Caption = "Difference"
            Me.grd.RootTable.Columns("Diff").Visible = True
            Me.grd.RootTable.Columns("ImageStream").ColumnType = Janus.Windows.GridEX.ColumnType.Image
            Me.grd.RootTable.Columns("ImageStream").Caption = "Status"
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
            Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges


            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.dtpFormDate.Value = Date.Now.AddDays(-1)
            Me.dtpToDate.Value = Date.Now.AddDays(-1)
            Me.dtpFromDate1.Value = Date.Now
            Me.dtpToDate1.Value = Date.Now
            Me.grd.DataSource = Nothing
            Me.dtpFormDate.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            If Me.dtpFormDate.Value > Me.dtpToDate.Value Then
                ShowErrorMessage("From date is grater than date to")
                Me.dtpFormDate.Focus()
                Exit Sub
            End If
            If Me.dtpToDate.Value < Me.dtpFormDate.Value Then
                ShowErrorMessage("To date is grater than date from")
                Me.dtpToDate.Focus()
                Exit Sub
            End If

            If Me.dtpFromDate1.Value > Me.dtpToDate1.Value Then
                ShowErrorMessage("From date is grater than date to")
                Me.dtpFromDate1.Focus()
                Exit Sub
            End If
            If Me.dtpToDate1.Value < Me.dtpFromDate1.Value Then
                ShowErrorMessage("To date is grater than date from")
                Me.dtpToDate1.Focus()
                Exit Sub
            End If



            GetAllRecords()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.dtpFormDate.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.LoadingRow
        Try
            If e.Row.Cells("Diff").Value < 0 Then
                e.Row.Cells("ImageStream").Image = Nothing 'My.Resources.arrow_down
            ElseIf e.Row.Cells("Diff").Value > 0 Then
                e.Row.Cells("ImageStream").Image = Nothing 'My.Resources.go_last
            End If
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class