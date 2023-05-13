Public Class frmGrdLocationWiseStockLevelSummary
    Public Level As Integer = 0
    Public LocationId As Integer = 0
    Private Sub frmGrdLocationWiseStockLevelSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_LocationWiseStockLevelSummary"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()

            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns(1).Visible = False
            Me.grdSaved.RootTable.Columns("Max Stock").Visible = False
            Me.grdSaved.RootTable.Columns("Normal Stock").Visible = False
            Me.grdSaved.RootTable.Columns("Min Stock").Visible = False

            Me.grdSaved.RootTable.Columns("Max Stock").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("Normal Stock").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("Min Stock").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("location_name").Caption = "Location"
            Me.grdSaved.AutoSizeColumns()

            If Level = 0 Then
                Me.grdSaved.RootTable.Columns("Min Stock").Visible = True
            ElseIf Level = 1 Then
                Me.grdSaved.RootTable.Columns("Normal Stock").Visible = True
            ElseIf Level = 2 Then
                Me.grdSaved.RootTable.Columns("Max Stock").Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "Min Stock" Then
                Level = 0
                LocationId = Me.grdSaved.CurrentRow.Cells(0).Value
            ElseIf e.Column.Key = "Normal Stock" Then
                Level = 1
                LocationId = Me.grdSaved.CurrentRow.Cells(0).Value
            ElseIf e.Column.Key = "Max Stock" Then
                Level = 2
                LocationId = Me.grdSaved.CurrentRow.Cells(0).Value
            End If
            Dim frm As New frmGrdLoctionWiseStockLevel
            ApplyStyleSheet(frm)
            frm.StartPosition = FormStartPosition.CenterParent
            frm.Level = Level
            frm.LocationId = LocationId
            frm.Text = frm.Text & " " & "[" & Me.grdSaved.CurrentRow.Cells("location_name").Value.ToString & "]"
            frm.ShowDialog()
            frm.BringToFront()
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Location Wise Stock Level Summary"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class