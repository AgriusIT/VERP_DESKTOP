Public Class frmRptProjectHistory
    Dim IsOpenedForm As Boolean = False
    Public Sub FillCombo()
        Try
            Dim strQuery As String = String.Empty
            strQuery = "Select ProjectCode, ProjectNo, ProjectName From tblProjectPortFolio Order By ProjectNo DESC"
            FillUltraDropDown(Me.cmbProject, strQuery)
            Me.cmbProject.Rows(0).Activate()
            Me.cmbProject.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmRptProjectHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub frmRptProjectHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            FillCombo()
            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateTo.Value = Date.Now

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowComment.Click
        Try
            If Me.cmbProject.IsItemInList = False Then Exit Sub
            If Me.cmbProject.ActiveRow Is Nothing Then
                ShowErrorMessage("Please select Project.")
                Me.cmbProject.Focus()
                Exit Sub
            End If
            Dim FromDate As String = Me.dtpFromDate.Value.ToString("yyyy,M,d,00,00,00")
            Dim ToDate As String = Me.dtpDateTo.Value.ToString("yyyy,M,d,23,59,59")
            AddRptParam("@ProjectId", Me.cmbProject.Value)
            ShowReport("rptProjectHistory", "{SP_ProjectHistory;1.LogFollowupDate} in DateTime (" & FromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub rbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtByName.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rbtByName.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString

            ElseIf rbtByCode.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
            End If
            ''End 'Task# E08062015
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtByCode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtByCode.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rbtByCode.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectNo").Key.ToString
            ElseIf rbtByName.Checked = True Then
                Me.cmbProject.DisplayMember = Me.cmbProject.DisplayLayout.Bands(0).Columns("ProjectName").Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRptProjectHistory_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        
        Try

            Me.KeyPreview = True
            rbtByCode.Checked = True
            FillCombo()
            IsOpenedForm = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShowRemarks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowRemarks.Click
        Try
            If Me.cmbProject.IsItemInList = False Then Exit Sub
            If Me.cmbProject.ActiveRow Is Nothing Then
                ShowErrorMessage("Please select Project.")
                Me.cmbProject.Focus()
                Exit Sub
            End If
            Dim FromDate As String = Me.dtpFromDate.Value.ToString("yyyy,M,d,00,00,00")
            Dim ToDate As String = Me.dtpDateTo.Value.ToString("yyyy,M,d,23,59,59")
            AddRptParam("@ProjectId", Me.cmbProject.Value)
            ShowReport("rptProjectHistory1", "{SP_ProjectHistory;1.LogFollowupDate} in DateTime (" & FromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class