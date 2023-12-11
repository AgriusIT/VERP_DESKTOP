Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class CashReceiptDetailAgainstEmployee
    Public flgCompanyRights As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
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
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptCashReceiptDetailAgainstEmployee", IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, "{SP_CashReceiptAgainstEmployee;1.Employee_Id}=" & Me.cmbEmployee.Value & "", ""))
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            CallShowReport(True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.OK_Button.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.OK_Button.Enabled = False
            Me.btnPrint.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "Show" Then
                    Me.OK_Button.Enabled = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub WithHoldingTaxCertificate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.lblEmployee.Visible = True
            Me.cmbEmployee.Visible = True
            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name as CostCenter from tblDefCostCenter")
            FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
            Me.cmbEmployee.Rows(0).Activate()
            If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class