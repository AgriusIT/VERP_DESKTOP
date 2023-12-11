Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class rptExpenses
    Public flgCompanyRights As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim fromDate As String = Me.DateTimePicker1.Value.Year & "," & Me.DateTimePicker1.Value.Month & "," & Me.DateTimePicker1.Value.Day & ",0,0,0"
            Dim ToDate As String = Me.DateTimePicker2.Value.Year & "," & Me.DateTimePicker2.Value.Month & "," & Me.DateTimePicker2.Value.Day & ",23,59,59"
            Dim opening As Integer = 0 'GetAccountOpeningBalance(0, Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            AddRptParam("FromDate", Me.DateTimePicker1.Value)
            AddRptParam("ToDate", Me.DateTimePicker2.Value)

            Dim str As String
            Dim CostCenterId As String = ""
            If Me.cmbBranch.SelectedIndex > 0 Then
                str = "select CostCenterId from tbldefCostCenter where CostCenterGroup = '" & Me.cmbBranch.Text & "'"
                Dim dt As DataTable = GetDataTable(str)
                CostCenterId = GetCommaSeparatedValues(dt)
            End If
            'AddRptParam("CostCenter", CostCenterId)
            ShowReport("rptExpenseStatment", "{vw_Expenses.voucher_date} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & IIf(Me.cmbBranch.SelectedIndex > 0, IIf(cmbCostCenter.SelectedValue > 0, " AND {vw_Expenses.CostCenterID} = " & cmbCostCenter.SelectedValue, " and {vw_Expenses.CostCenterGroup} = '" & cmbBranch.Text & "'"), IIf(Me.cmbCostCenter.SelectedIndex > 0, " and {vw_Expenses.CostCenterID}=" & Me.cmbCostCenter.SelectedValue, "")), "Nothing", "Nothing", Print, Val(opening).ToString)
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        CallShowReport(True)
    End Sub
    Private Function GetCommaSeparatedValues(ByVal dt As DataTable) As String
        Dim ReturnValue As String = ""
        Dim Counter As Integer = 0

        For Each row As DataRow In dt.Rows
            If Counter = 0 Then
                ReturnValue = "" & row.Item("CostCenterId") & ""
            Else
                ReturnValue += "," & row.Item("CostCenterId") & ""
            End If
            Counter += 1
        Next

        Return ReturnValue

    End Function
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DateTimePicker1.Value = Date.Today
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            Me.DateTimePicker2.Value = Date.Today
        End If
    End Sub
    Private Sub rptExpenses_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.Text = "Expense Report"
            'Me.pnlCost.Visible = False
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            FillDropDown(Me.cmbBranch, "SELECT Distinct CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE active = 1", True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub cmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBranch.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbCostCenter, "SELECT * FROM tbldefCostCenter " & IIf(cmbBranch.SelectedIndex > 0, "WHERE CostCenterGroup = '" & Me.cmbBranch.Text & "'", "") & "order by sortorder , name", True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class