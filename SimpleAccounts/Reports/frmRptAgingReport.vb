Public Class frmRptAgingReport
    Public Enum ReportList
        Receiveables
        Payables
    End Enum
    Public ReportName As String
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        Try
            CallShowRpt()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CallShowRpt()
        Try
            'If ReportName = ReportList.Receiveables Then
            '    AddRptParam("@ToDate", Me.dtpCurrentDate.Value)
            '    AddRptParam("@1stAging", 60)
            '    AddRptParam("@1stAgingName", "30_60")
            '    AddRptParam("@1stAging", 90)
            '    AddRptParam("@1stAgingName", "60_90")
            '    AddRptParam("@1stAging", 90)
            '    AddRptParam("@1stAgingName", "90+")
            '    ShowReport("AgeingReceivable")
            If ReportName = ReportList.Receiveables Then
                AddRptParam("@ToDate", Me.dtpCurrentDate.Value)
                AddRptParam("@Aging", 30)
                'AddRptParam("@AgingName", "CurrentAmount")
                AddRptParam("@1stAging", 60)
                AddRptParam("@1stAgingName", "30_60")
                AddRptParam("@2ndAging", 90)
                AddRptParam("@2ndAgingName", "60_90")
                AddRptParam("@3rdAging", 90)
                AddRptParam("@3rdAgingName", "90+")
                AddRptParam("@IncludeUnPosted", "1")
                AddRptParam("@SubSubID", "0")
                AddRptParam("@CostCenterId", 0)
                ShowReport("AgeingReceivableSingleDate")
                'ShowReport("AgeingReceivable")
            ElseIf ReportName = ReportList.Payables Then
                AddRptParam("@ToDate", Me.dtpCurrentDate.Value)
                AddRptParam("@1stAging", 60)
                AddRptParam("@1stAgingName", "30_60")
                AddRptParam("@1stAging", 90)
                AddRptParam("@1stAgingName", "60_90")
                AddRptParam("@1stAging", 90)
                AddRptParam("@1stAgingName", "90+")
                ShowReport("AgeingPayable")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmRptAgingReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("ProjectHead")
            FillCombos("Project")
            If ReportName = ReportList.Receiveables Then
                Me.Text = "Receiveables"
            ElseIf ReportName = ReportList.Payables Then
                Me.Text = "Payables"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "ProjectHead" Then
                Dim str As String = "Select DISTINCT CostCenterGroup, CostCenterGroup from tblDefCostCenter"
                FillDropDown(Me.cmbCostCenterHead, str)
            ElseIf Condition = "Project" Then
                Dim str As String = "Select CostCenterId, Name as CostCenter From tblDefCostCenter"
                str = str + "" & IIf(Me.cmbCostCenterHead.SelectedIndex > 0, " WHERE CostCenterGroup='" & Me.cmbCostCenterHead.Text & "'", "") & " "
                FillDropDown(Me.cmbCostCenter, str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbCostCenterHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCostCenterHead.SelectedIndexChanged
        Try
            FillCombos("Project")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class