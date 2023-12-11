Public Class frmGrdPlanComparison
    Dim _IsOpenForm As Boolean = False
    Dim _dt As DataTable

    Private Sub frmGrdPlanComparison_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            lnkRefresh_LinkClicked(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmGrdPlanComparison_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Dim strQuery As String = String.Empty
            strQuery = "Select PlanId, PlanNo + '~' + Convert(Varchar,PlanDate,101) as PlanDate From PlanMasterTable ORDER BY 1 DESC"
            FillDropDown(Me.ComboBox1, strQuery)
            _IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try

            If _IsOpenForm = False Then Exit Sub
            PlanDetail(Me.ComboBox1.SelectedValue)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PlanDetail(ByVal PlanId As Integer)
        Try
            Dim strQuery As String = String.Empty
            strQuery = "SELECT DISTINCT dbo.PlanMasterTable.PlanNo, dbo.PlanMasterTable.PlanDate, Isnull(dbo.tblCostSheet.MasterArticleID,0) as MasterArticleID, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode AS Code, " _
                     & " dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size,  " _
                     & " (ISNULL(dbo.PlanDetailTable.Qty, 0)) AS Qty " _
                     & " FROM  dbo.PlanDetailTable INNER JOIN " _
                     & " dbo.PlanMasterTable ON dbo.PlanDetailTable.PlanId = dbo.PlanMasterTable.PlanId LEFT OUTER JOIN " _
                     & " dbo.ArticleDefView ON dbo.PlanDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " dbo.tblCostSheet ON dbo.ArticleDefView.MasterID = dbo.tblCostSheet.MasterArticleID    WHERE PlanMasterTable.PlanId= " & PlanId & ""
            '& " GROUP BY dbo.PlanMasterTable.PlanNo, dbo.PlanMasterTable.PlanDate, dbo.tblCostSheet.MasterArticleID, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode,  " _
            '& " dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName "
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            Me.grdPlanItems.DataSource = Nothing
            Me.grdPlanItems.DataSource = dt
            Me.grdPlanItems.RetrieveStructure()
            grdPlanItems.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdPlanItems.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdPlanItems.RootTable.Columns("MasterArticleID").Visible = False
            Me.grdPlanItems.RootTable.Columns("ArticleId").Visible = False
            Me.grdPlanItems.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdPlanItems.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdPlanItems.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdPlanItems.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'WAQAR RAZA added this Rights SUB..........
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.ToolStripButton1.Enabled = True

                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmRptGrdStockStatement)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.ToolStripButton1.Text = "Plan Comparison" Then
                            Me.ToolStripButton1.Enabled = dt.Rows(0).Item("Plan Comparison_Rights").ToString()
                        End If
                    End If
                End If
            Else
                Me.Visible = False
                Me.ToolStripButton1.Enabled = False

                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Plan Comparison" Then
                        If Me.ToolStripButton1.Text = "Plan Comparison" Then ToolStripButton1.Enabled = True
                        'Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefresh.LinkClicked
        Try
            Dim id As Integer = 0
            Dim strQuery As String = String.Empty
            id = Me.ComboBox1.SelectedIndex
            strQuery = "Select PlanId, PlanNo + '~' + Convert(Varchar,PlanDate,101) as PlanDate From PlanMasterTable ORDER BY 1 DESC"
            FillDropDown(Me.ComboBox1, strQuery)
            Me.ComboBox1.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdPlanItems_LoadingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdPlanItems.LoadingRow
        Try

            Dim FormatRow As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells("MasterArticleID").Value <> 0 Then
                FormatRow.BackColor = Color.Ivory
            Else
                FormatRow.BackColor = Color.White
            End If
            e.Row.RowStyle = FormatRow

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub ComparisonDetail(ByVal PlanID As Integer, ByVal MasterID As Integer)
        Try
            Dim strQuery As String = String.Empty
            strQuery = "SELECT  ISNULL(Prod_Plan.MasterID, 0) AS MASTERID, Prod_Plan.PlanNo, Prod_Plan.PlanDate, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription, " _
                     & " dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, (ISNULL(Prod_Plan.Qty, 0) * ISNULL(dbo.tblCostSheet.Qty, 0)) AS Qty " _
                     & " FROM dbo.ArticleDefView INNER JOIN " _
                     & " dbo.tblCostSheet ON dbo.ArticleDefView.ArticleId = dbo.tblCostSheet.ArticleID LEFT OUTER JOIN " _
                     & " (SELECT dbo.PlanDetailTable.Qty, ArticleDefView_1.MasterID, PlanMasterTable.PlanNo, PlanMasterTable.PlanDate " _
                     & " FROM dbo.PlanDetailTable INNER JOIN " _
                     & " dbo.ArticleDefView AS ArticleDefView_1 ON dbo.PlanDetailTable.ArticleDefId = ArticleDefView_1.ArticleId INNER JOIN PlanMasterTable On PlanMasterTable.PlanId = PlanDetailTable.PlanId WHERE PlanDetailTable.PlanID=" & PlanID & " AND ArticleDefView_1.MASTERID=" & MasterID & " ) AS Prod_Plan ON  " _
                     & " Prod_Plan.MasterID = dbo.tblCostSheet.MasterArticleId  WHERE Prod_Plan.PlanNo <> ''" ''AND ((ISNULL(Prod_Plan.Qty, 0) * ISNULL(dbo.tblCostSheet.Qty, 0)) <> 0) 
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            _dt = New DataTable
            _dt = dt
            If dt IsNot Nothing Then
                Me.grd.DataSource = Nothing
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
                Me.grd.RootTable.Columns("MASTERID").Visible = False
                Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.AutoSizeColumns()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdPlanItems_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPlanItems.SelectionChanged
        Try
            ComparisonDetail(Me.ComboBox1.SelectedValue, Me.grdPlanItems.GetRow.Cells("MasterArticleID").Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            GetCrystalReportRights()
            If _dt Is Nothing Then Exit Sub
            ShowReport("rptPlanComparison", , , , , , , _dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class