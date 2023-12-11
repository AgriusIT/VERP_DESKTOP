Public Class frmPlanList
    Public PlanId As Integer

    Private Sub frmPlanList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid(PlanId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid(ByVal PlanId As Integer)
        Try
            Dim strSQL As String = "SELECT dbo.PlanMasterTable.PlanId, dbo.PlanMasterTable.PlanNo, dbo.PlanMasterTable.PlanDate, dbo.ArticleDefView.MasterID, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, " _
                 & " ISNULL(dbo.PlanDetailTable.Qty, 0) AS Qty, ISNULL(dbo.PlanDetailTable.PlanQty, 0) AS PlanQty, 0 as Balance, 0 as [Plan] " _
                 & " FROM  dbo.PlanDetailTable INNER JOIN " _
                 & " dbo.PlanMasterTable ON dbo.PlanDetailTable.PlanId = dbo.PlanMasterTable.PlanId INNER JOIN " _
                 & " dbo.ArticleDefView ON dbo.PlanDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE dbo.PlanMasterTable.PlanId=" & PlanId & " AND ISNULL(dbo.PlanDetailTable.Qty, 0) <> 0 ORDER BY ArticleDefView.SortOrder ASC"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    dt.Columns("Balance").Expression = "Qty-PlanQty"
                    Me.grd.DataSource = dt
                    Me.grd.AutoSizeColumns()
                Else
                    Me.grd.DataSource = Nothing
                End If
            Else
                Me.grd.DataSource = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub UpdatePlan()
        Dim cmd As New OleDb.OleDbCommand
        Try

            Dim strSQL As String = String.Empty
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
            cmd.Connection = Con
            cmd.Transaction = trans


            For Each r As DataRow In CType(Me.grd.DataSource, DataTable).GetChanges.Rows
                cmd.CommandText = ""
                strSQL = "UPDATE PlanDetailTable Set PlanQty = PlanQty + " & r.Item("Plan") & ", WHERE PlanId=" & r.Item("PlanId") & " AND ArticleDefId=" & r.Item("ArticleId") & ""
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
            Next

            trans.Commit()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try

            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            UpdatePlan()
            msg_Information(str_informUpdate)
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class