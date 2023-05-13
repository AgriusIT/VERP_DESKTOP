Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Net
Imports SBDal
Imports SBModel

Public Class frmSMSConfig

    Public Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            If Me.GridEX1.RootTable Is Nothing Then Exit Sub
            Me.GridEX1.RootTable.Columns("Config_Key").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmSMSConfig_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            GetEnventKeyList()
            GetAllSMSTemplate()
            GetLocationList()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSMSConfig_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Me.GridEX1.UpdateData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSMSConfig_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Me.GridEX1.UpdateData()
    End Sub
    Private Sub frmSMSConfig_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select ID, Config_Key, IsNull(Enabled,0) as Enabled, IsNull(EnabledAdmin,0) as EnabledAdmin From SMSConfigurationTable")
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
            ApplyGridSetting()
            Me.GridEX1.UpdateData()
            GetEnventKeyList()
            GetAllSMSTemplate()
            GetLocationList()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub GridEX1_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Try

            Me.GridEX1.UpdateData()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Update SMSConfigurationTable Set Enabled=" & IIf(Me.GridEX1.GetRow.Cells("Enabled").Value = False, 0, 1) & ",EnabledAdmin=" & IIf(Me.GridEX1.GetRow.Cells("EnabledAdmin").Value = False, 0, 1) & " WHERE Config_Key='" & Me.GridEX1.GetRow.Cells("Config_Key").Value.ToString & "'"
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.ExecuteNonQuery()
            trans.Commit()
            GetEnventKeyList()

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub

End Class