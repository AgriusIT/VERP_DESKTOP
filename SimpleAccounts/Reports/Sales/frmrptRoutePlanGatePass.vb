Imports SBModel
Public Class frmrptRoutePlanGatePass
    Private Sub frmrptRoutePlanGatePass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.DateTimePicker1.Value = Date.Now
            FillDropDown(Me.cmbRoutePlan, "Select RootPlanId,RootPlanName from tblDefRootPlan where Active=1")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            GetCrystalReportRights()
            If Me.cmbRoutePlan.SelectedIndex = 0 Then
                ShowErrorMessage("Please select route plan")
                Me.cmbRoutePlan.Focus()
                Exit Sub
            End If
            AddRptParam("@DocDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@RootPlanId", Me.cmbRoutePlan.SelectedValue)
            ShowReport("rptRoutePlanWiseGatePass")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.cmbRoutePlan.SelectedIndex = 0 Then
                ShowErrorMessage("Please select route plan")
                Me.cmbRoutePlan.Focus()
                Exit Sub
            End If
            AddRptParam("@DocDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@RootPlanId", Me.cmbRoutePlan.SelectedValue)
            ShowReport("rptRoutePlanWiseGatePass", , "Nothing", "Nothing", True)
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
                Me.btnShow.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Me.btnShow.Enabled = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        Me.btnShow.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class