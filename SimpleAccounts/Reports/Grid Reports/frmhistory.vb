Public Class frmhistory
    Public article_code As String
    Public startDate As DateTime '= Date.Now.AddMonths(-1).Date
    Public EndDate As DateTime '= Date.Now.Date
    Public LocationId As Integer = 0
    Public ArticleCode As String
    Public ArticleDescription As String
    Dim DCStockImpact As Boolean = False
    Dim GRNStockImpact As Boolean = False
    Private Sub frmhistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '' TASK TFS1378

            If Not getConfigValueByType("GRNStockImpact").ToString = "Error" Then
                GRNStockImpact = Convert.ToBoolean(getConfigValueByType("GRNStockImpact").ToString)
            End If
            If Not getConfigValueByType("DCStockImpact").ToString = "Error" Then
                DCStockImpact = Convert.ToBoolean(getConfigValueByType("DCStockImpact").ToString)
            End If
            ''END TASK TFS1378
            Dim dt As New DataTable
            Me.Text = "History of article " & ArticleCode
            Dim adp As New OleDb.OleDbDataAdapter
            Dim dtData As New DataTable()
            ''Below line is commented on 16-02-2018 by Ameen against TASK TFS2425 to incorporate all changes in one Store Procedure.
            adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocation '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
            ''Below lines are commented against TASK TFS2425 ON 16-02-2018 done by Ameen
            ''TASK TFS1378
            'If GRNStockImpact = False AndAlso DCStockImpact = False Then
            '    adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocation '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
            'ElseIf GRNStockImpact = True AndAlso DCStockImpact = True Then
            '    adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocationWithBothStockImpact '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
            'ElseIf GRNStockImpact = True AndAlso DCStockImpact = False Then
            '    adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocationGRNStockImpact '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
            'ElseIf GRNStockImpact = False AndAlso DCStockImpact = True Then
            '    adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocationDCStockImpact '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
            'End If
            ''END TASK TFS1378
            dt.Clear()
            adp.Fill(dt)
            dt.AcceptChanges()
            GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
            GridEX1.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Packs").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim dtReport As New DataTable
            ''Commented below lines against task TFS1378 on 30-10-2017
            'dtReport = GetDataTable("ArticleHistory_ByLocation '" & article_code & "','" & startDate.Date.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.Date.ToString("yyyy-M-d 23:59:59") & "'," & LocationId)

            ''TASK TFS1378
            If GRNStockImpact = False AndAlso DCStockImpact = False Then
                'adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocation '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
                dtReport = GetDataTable("ArticleHistory_ByLocation '" & article_code & "','" & startDate.Date.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.Date.ToString("yyyy-M-d 23:59:59") & "'," & LocationId)
            ElseIf GRNStockImpact = True AndAlso DCStockImpact = True Then
                'adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocationWithBothStockImpact '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
                dtReport = GetDataTable("ArticleHistory_ByLocationWithBothStockImpact '" & article_code & "','" & startDate.Date.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.Date.ToString("yyyy-M-d 23:59:59") & "'," & LocationId)
            ElseIf GRNStockImpact = True AndAlso DCStockImpact = False Then
                'adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocationGRNStockImpact '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
                dtReport = GetDataTable("ArticleHistory_ByLocationGRNStockImpact '" & article_code & "','" & startDate.Date.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.Date.ToString("yyyy-M-d 23:59:59") & "'," & LocationId)

            ElseIf GRNStockImpact = False AndAlso DCStockImpact = True Then
                'adp = New OleDb.OleDbDataAdapter("ArticleHistory_ByLocationDCStockImpact '" & article_code & "','" & startDate.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.ToString("yyyy-M-d 23:59:59") & "'," & LocationId, Con)
                dtReport = GetDataTable("ArticleHistory_ByLocationDCStockImpact '" & article_code & "','" & startDate.Date.ToString("yyyy-M-d 00:00:00") & "','" & EndDate.Date.ToString("yyyy-M-d 23:59:59") & "'," & LocationId)
            End If
            ''END TASK TFS1378
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
            Me.CtrlGrdBar1.txtGridTitle.Text = "Stock Statement" & Chr(10) & CompanyTitle & Chr(10) & "Date From: " & Me.startDate.ToString("yyyy-M-d") & " Date To: " & Me.EndDate.ToString("yyyy-M-d") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class