Imports SBModel
Imports System.Data.OleDb
Public Class frmRptStockStatment

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        Dim strSql As String

        strSql = "Exec SP_Rpt_StockStatement '" & dtpFrom.Value.Date & "','" & dtpTo.Value.Date & "'"

        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        Me.grdReport.DataSource = dt
        GridFormating()
    End Sub
    Private Sub GridFormating()
        grdReport.DisplayLayout.Bands(0).Columns(0).Hidden = True
        'grdReport.DisplayLayout.Bands(0).Columns(0).Header.Caption = ""


    End Sub

    Private Sub frmRptStockStatment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmRptStockStatment)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpto.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpfrom.Value = Date.Today.AddDays(-1)
            Me.dtpto.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpfrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpto.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpfrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpto.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpfrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpto.Value = Date.Today
        End If
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdReport.LayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Statement by Pack" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class