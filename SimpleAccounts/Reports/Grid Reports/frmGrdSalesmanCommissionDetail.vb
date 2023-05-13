Public Class frmGrdSalesmanCommissionDetail

    Implements IGeneral

    Private Sub frmGrdSalesmanCommissionDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
            Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown

            Me.grd.RootTable.Columns("GrossSales").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("GrossSales").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DamageReturn").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Damagereturn").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("UnSold").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("UnSold").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ActualSales").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ActualSales").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("FuelExpense").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("FuelExpense").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Adjustment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Adjustment").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("NetSales").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("NetSales").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Commission").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Commission").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Commission_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Commission_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OtherExpense").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OtherExpense").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("GrossSales").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DamageReturn").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("UnSold").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("FuelExpense").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("OtherExpense").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Adjustment").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("NetSales").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Commission_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("GrossSales").FormatString = "N"
            Me.grd.RootTable.Columns("DamageReturn").FormatString = "N"
            Me.grd.RootTable.Columns("UnSold").FormatString = "N"
            Me.grd.RootTable.Columns("FuelExpense").FormatString = "N"
            Me.grd.RootTable.Columns("OtherExpense").FormatString = "N"
            Me.grd.RootTable.Columns("Adjustment").FormatString = "N"
            Me.grd.RootTable.Columns("NetSales").FormatString = "N"
            Me.grd.RootTable.Columns("Commission_Amount").FormatString = "N"

            Me.grd.RootTable.Columns("GrossSales").TotalFormatString = "N"
            Me.grd.RootTable.Columns("DamageReturn").TotalFormatString = "N"
            Me.grd.RootTable.Columns("UnSold").TotalFormatString = "N"
            Me.grd.RootTable.Columns("FuelExpense").TotalFormatString = "N"
            Me.grd.RootTable.Columns("OtherExpense").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Adjustment").TotalFormatString = "N"
            Me.grd.RootTable.Columns("NetSales").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Commission_Amount").TotalFormatString = "N"

            Me.grd.RootTable.Columns("DamageReturn").Caption = "Return"
            Me.grd.RootTable.Columns("Adjustment").Caption = "Adj"
            Me.grd.RootTable.Columns("FuelExpense").Caption = "Fuel"
            Me.grd.RootTable.Columns("Commission_Amount").Caption = "Comm Amount"
            Me.grd.RootTable.Columns("ActualSales").Caption = "Off Take"
            Me.grd.RootTable.Columns("OtherExpense").Caption = "Other Exp"
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
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
            str = "Sp_SalesmanCommission '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'  "
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.Columns("ActualSales").Expression = "GrossSales-DamageReturn-UnSold"
            dt.Columns("NetSales").Expression = "Isnull(ActualSales,0)-IsNull(FuelExpense,0) - Isnull(OtherExpense,0) - Isnull(Adjustment,0)"

            For Each r As DataRow In dt.Rows
                str = String.Empty
                str = "Select IsNull(Percentage,0) From tblDefCommissionDetail WHERE (Convert(int, " & Val(r.Item("NetSales").ToString) & ") BETWEEN Convert(int, Start_Value) AND Convert(int, End_Value)) AND SalemanId=" & r.Item("coa_detail_id") & ""
                Dim dtData As New DataTable
                dtData = GetDataTable(str)
                If dtData IsNot Nothing Then
                    If dtData.Rows.Count > 0 Then
                        r.BeginEdit()
                        r("Commission") = Val(dtData.Rows(0).Item(0).ToString)
                        r.EndEdit()
                    End If
                End If
            Next
            dt.AcceptChanges()
            dt.Columns("Commission_Amount").Expression = "((NetSales*Commission)/100)"
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("coa_detail_id").Visible = False
            Me.grd.RootTable.Columns("detail_title").Caption = "Customer Name"
            ApplyGridSettings()

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.dtpFromDate.Value = Date.Now.AddMonths(-1)
            Me.dtpToDate.Value = Date.Now
            Me.grd.DataSource = Nothing
            Me.dtpFromDate.Focus()
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

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.dtpFromDate.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Salesman Commission" & vbCrLf & "Date From:" & dtpFromDate.ToString("dd-MMM-yyyy") & " Month: " & dtpToDate.ToString("dd-MMM-yyyy") & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class