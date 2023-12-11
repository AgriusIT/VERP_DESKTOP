Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class SalesCertificateIssued
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
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
            AddRptParam("@CustomerCode", Me.cmbCustomer.Value)
            ShowReport("rptIssuedSalesCertificate")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        CallShowReport(True)
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
    Private Sub SalesCertificateIssued_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            Me.pnlInvType.Visible = True
            Me.pnlPeriod.Visible = True
            Me.pnlVendorCustomer.Visible = True
            Me.cmbPeriod.Text = "Current Month"
            Me.Text = "Issued Sales Certificate"
            FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
            Dim Str As String = "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE 1=1 " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
            ''Start TFS2124
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (account_type = 'Customer')  "
            Else
                Str += " AND (account_type in('Customer','Vendor'))  "
            End If
            Str += " ORDER BY 2 ASC"
            ''End TFS2124
            FillUltraDropDown(cmbCustomer, Str)
            Me.cmbCustomer.Rows(0).Activate()
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class