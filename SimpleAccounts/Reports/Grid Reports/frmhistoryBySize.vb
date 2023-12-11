Public Class frmhistoryBySize
    Public article_code As String
    Public startDate As DateTime '= Date.Now.AddMonths(-1).Date
    Public EndDate As DateTime '= Date.Now.Date
    Public LocationId As Integer = 0
    Public ArticleCode As String
    Public ArticleDescription As String

    Private Sub frmhistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dt As New DataTable
            Me.Text = "History of article " & ArticleCode
            Dim adp As New OleDb.OleDbDataAdapter
            Dim dtData As New DataTable()
            adp = New OleDb.OleDbDataAdapter("ArticleHistory_BySize'" & article_code & "','" & startDate.Date & "','" & EndDate.Date & "'," & LocationId, Con)
            dt.Clear()
            adp.Fill(dt)
            GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
            GridEX1.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Total Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'GridEX1.RootTable.Columns("Pack Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim dtReport As New DataTable
            dtReport = GetDataTable("ArticleHistory_BySize '" & article_code & "','" & startDate.Date.ToString("dd/MMM/yyyy") & "','" & EndDate.Date.ToString("dd/MMM/yyyy") & "'," & LocationId)
            AddRptParam("ArticleCode", ArticleCode)
            AddRptParam("ArticleDescription", ArticleDescription)
            AddRptParam("FromDate", startDate.Date)
            AddRptParam("ToDate", EndDate.Date)
            ShowReport("rptArticleHistoryByLocation", "Nothing", "Nothing", "Nothing", False, "Nothing", , dtReport)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
            Me.CtrlGrdBar1.txtGridTitle.Text = "History" & Chr(10) & CompanyTitle & Chr(10) & "Date From: " & Me.startDate.ToString("yyyy-M-d") & " Date To: " & Me.EndDate.ToString("yyyy-M-d") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class