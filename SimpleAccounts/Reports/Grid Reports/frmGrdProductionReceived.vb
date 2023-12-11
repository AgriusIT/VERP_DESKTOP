Public Class frmGrdProductionReceived
    Public ArticleId As Integer
    Public startDate As DateTime
    Public endDate As DateTime
    Public ProjectId As Integer = 0I
    Public PlanId As Integer = 0I
    Private Sub GridBarUserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
          
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdProductionReceived_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim str As String = String.Empty
            str = "SP_ProductionHisotry '" & ArticleId & "', '" & startDate.Date.ToString("dd/MMM/yyyy") & "', '" & endDate.Date.ToString("dd/MMM/yyyy") & "', '" & ProjectId & "'"
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            dt.TableName = "Default"
            Dim dv As New DataView
            dv.Table = dt
            If PlanId > 0 Then
                dv.RowFilter = "PlanId=" & PlanId & ""
            End If
            dv.ToTable.AcceptChanges()
            Me.grd.DataSource = dv.ToTable
            grd.RetrieveStructure()
            Me.grd.RootTable.Columns("PlanId").Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub UiCtrlGridBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.UiCtrlGridBar1.txtGridTitle.Text = "Production Received From " & startDate.Date.ToString("dd-MM-yyyy") & " To " & endDate.Date.ToString("dd-MM-yyyy") & " "
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = "Production Received From " & startDate.Date.ToString("dd-MM-yyyy") & " To " & endDate.Date.ToString("dd-MM-yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

    End Sub
End Class