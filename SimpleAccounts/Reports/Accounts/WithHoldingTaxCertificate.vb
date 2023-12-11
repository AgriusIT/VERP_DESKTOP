Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class WithHoldingTaxCertificate
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
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
            AddRptParam("@AccountId", Me.cmbVendor.Value)
            ShowReport("rpttaxcertificate", "Nothing", "Nothing", "Nothing")
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
                If RightsDt.FormControlName = "Print" Then
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
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub WithHoldingTaxCertificate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            Me.lblVendor.Visible = True
            Me.cmbVendor.Visible = True
            cmbVendor.DataSource = Nothing
            FillUltraDropDown(cmbVendor, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & "")
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class