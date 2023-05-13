'''TASK TFS1629 Muhammad Ameen completed on 27-10-2017. Date exceeded, In progress and Not started plans should be displayed in this report.
'' TASK TFS3487 Muhammad Amin made changes on 08-06-2018 in Store Procedure to fix 'Produciton plan progress report is not working'
Imports SBModel

Public Class frmRptPlansStatus

    Private Sub frmRptPlansStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            Me.dtpFrom.Value = Now

            Me.dtpTo.Value = Now
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim query As String = ""
        Try
            query = "Exec spProductionPlanningStatus '" & dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As DataTable = GetDataTable(query)
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            Dim fCondition1 As New Janus.Windows.GridEX.GridEXFormatCondition(GridEX1.RootTable.Columns("Status"), Janus.Windows.GridEX.ConditionOperator.Equal, "Not started")
            fCondition1.FormatStyle.BackColor = Color.LightYellow
            'fCondition1.
            Dim fCondition2 As New Janus.Windows.GridEX.GridEXFormatCondition(GridEX1.RootTable.Columns("Status"), Janus.Windows.GridEX.ConditionOperator.Equal, "In progress")
            fCondition2.FormatStyle.BackColor = Color.LightGreen

            Dim fCondition3 As New Janus.Windows.GridEX.GridEXFormatCondition(GridEX1.RootTable.Columns("Status"), Janus.Windows.GridEX.ConditionOperator.Equal, "Date exceeded")
            fCondition3.FormatStyle.BackColor = Color.Red
            Me.GridEX1.RootTable.FormatConditions.Add(fCondition1)
            Me.GridEX1.RootTable.FormatConditions.Add(fCondition2)
            Me.GridEX1.RootTable.FormatConditions.Add(fCondition3)

            Me.GridEX1.RootTable.Columns("PlanDate").FormatString = str_DisplayDateFormat
            Me.GridEX1.RootTable.Columns("CompletionDate").FormatString = str_DisplayDateFormat

            Me.GridEX1.RootTable.Columns("PlanId").Visible = False
            'Me.GridEX1.RetrieveStructure()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Plans Status " & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
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
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class