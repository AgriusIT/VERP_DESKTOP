Imports SBModel
Public Class frmCashRecoveryReport
    Public ReportName As String = String.Empty
    Enum enmReportList
        ChequeRecovery
        ChequeDueAll
    End Enum

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            GetCrystalReportRights()
            'commented due to error in showing report 

            'If ReportName = enmReportList.ChequeRecovery Then
            '    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            '    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            '    ShowReport("rptCashRecoveryReport", IIf(Me.cmbCity.SelectedIndex > 0, "{SP_Cheque_Recovery;1.CityName}='" & Me.cmbCity.Text.Replace("'", "''") & "'", ""))
            'ElseIf ReportName = enmReportList.ChequeDueAll Then
            '    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            '    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            '    ShowReport("rptChequeDueRecovery", IIf(Me.cmbCity.SelectedIndex > 0, "{SP_Cheque_Recovery;1.CityName}='" & Me.cmbCity.Text.Replace("'", "''") & "'", ""))
            'End If

            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptChequeDueRecovery", IIf(Me.cmbCity.SelectedIndex > 0, "{SP_Cheque_Recovery;1.CityName}='" & Me.cmbCity.Text.Replace("'", "''") & "'", ""))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnShow.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.btnShow.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmCashRecoveryReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FillDropDown(Me.cmbCity, "Select CityID,CityName From tblListCity ORDER BY 2 ASC")
            Me.dtpFromDate.Value = Date.Now.AddDays(-(Me.dtpFromDate.Value.Day - 1))
            Me.dtpToDate.Value = Date.Now
            'commented due to error in showing report 

            'If ReportName = enmReportList.ChequeRecovery Then
            '    Me.Text = "Recovery Report"
            'ElseIf ReportName = enmReportList.ChequeDueAll Then
            '    Me.Text = "Customer Due All"
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class