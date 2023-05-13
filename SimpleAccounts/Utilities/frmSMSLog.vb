Public Class frmSMSLog
    Private Sub cmbLog_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLog.SelectedIndexChanged
        Try
            If Me.cmbLog.Text = "Outbox" Then
                Me.btnSave.Enabled = True
            Else
                Me.btnSave.Enabled = False
            End If
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim dt As New DataTable

            Dim str As String = String.Empty
            str = "Select SMSLogID,SMSLogDate as [Date],SMSType as [Type],PhoneNo as [Mobile], SMSBody as [Message],DeliveryStatus as [Status] From tblSMSLog WHERE SMSBody <> ''"
            If Not Me.cmbLog.SelectedIndex = -1 Then
                If Me.cmbLog.Text = "Drafts" Then
                    str += " AND DeliveryStatus Is Null"
                End If
                If Me.cmbLog.Text = "Outbox" Then
                    str += " AND DeliveryStatus='Error'"
                End If
                If Me.cmbLog.Text = "Sentbox" Then
                    str += " AND Left(DeliveryStatus,3)='Suc'"
                End If
            End If
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns.Add("Column1")
            Me.grd.RootTable.Columns("Column1").ActAsSelector = True
            Me.grd.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grd.RootTable.Columns("Column1").Width = "30"
            Me.grd.RootTable.Columns("SMSLogID").Visible = False
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).Index <> Me.grd.RootTable.Columns("Mobile").Index Then
                    grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmSMSLog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.cmbLog.Text = "Drafts"
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Me.lblProcess.Text = String.Empty
        Application.DoEvents()
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = Con
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Dim i As Integer = 0I
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                cmd.CommandText = "Delete From tblSMSLog WHERE SMSLogID=" & Val(r.Cells("SMSLogId").Value.ToString) & ""
                cmd.ExecuteNonQuery()
                i += 1
                Me.lblProcess.Text = "Deleted SMS" & i & "/" & Me.grd.GetCheckedRows.Length
                Application.DoEvents()
            Next
            trans.Commit()
            System.Threading.Thread.Sleep(5000)
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProcess.Text = String.Empty
            Application.DoEvents()
            cmd = Nothing
            Con.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.grd.RowCount = 0 Then Exit Sub
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        Try

            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE tblSMSLog Set PhoneNo='" & grd.GetRow.Cells("Mobile").Value.ToString & "' WHERE SMSLogID=" & grd.GetRow.Cells("SMSLogID").Value.ToString & ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
End Class