Imports SBModel
Public Class StockStatementByLPO
    Private Sub frmRptEnhancementNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
            Me.dtpFrom.Visible = True
            Me.dtpTo.Visible = True
            Me.Label1.Visible = True
            Me.Label2.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub CallShowReport()
        Try
            AddRptParam("@Pack", False)
            ShowReport("rptStockStatment", "Nothing", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"), Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"), False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        GetCrystalReportRights()
        Me.CallShowReport()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.UiButton2.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.UiButton2.Enabled = False
                    Exit Sub
                End If
            Else
                Me.UiButton2.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.UiButton2.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-1)
            Me.dtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpTo.Value = Date.Today
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub UiButton2_Click(sender As Object, e As EventArgs) Handles UiButton2.Click

    End Sub
End Class