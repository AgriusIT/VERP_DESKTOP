Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class PLNotesDetail
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
            If Not Me.cmbCostCenter.SelectedIndex = -1 AndAlso Me.cmbCostCenter.SelectedIndex > 0 Then
                ShowReport("cRptprofitLossNotesDetail", "{spProfitLossNotesDetail;1.CostCenterID} = " & Me.cmbCostCenter.SelectedValue & "")
            Else
                ShowReport("cRptprofitLossNotesDetail")
            End If
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
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Me.OK_Button.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.OK_Button.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Show" Then
                    Me.OK_Button.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
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
    Private Sub PLNotesDetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            Me.pnlCost.Visible = True
            Me.lblCostCenter.Visible = True
            Me.cmbCostCenter.Visible = True
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class