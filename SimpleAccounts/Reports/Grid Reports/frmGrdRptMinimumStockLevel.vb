Imports SBModel
Public Class frmGrdRptMinimumStockLevel
    Public StockLevel As String = String.Empty
    Private Sub frmGrdRptMinimumStockLevel_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        GetSecurityRights()
        Me.Cursor = Cursors.WaitCursor
        Try
            StockLevel = "MinLevel"
            Me.cmbResult.SelectedIndex = 0
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim strSQL As String = String.Empty
            If StockLevel = "MinLevel" Then
                'strSQL = "Select *, a.[Minimum Stock Level] - a.[Current Stock] as [Stock Below Level] From (Select ArticleId,ArticleDescription as Item, ArticleCode as Code, ArticleSizeName as Size, ArticleColorName as Color, " _
                '& " ArticleGroupName as Department, ArticleTypeName as Type, IsNull(StockLevel,0) as [Minimum Stock Level], " _
                '& " Case When IsNull(Stock.CurrentStock,0) < IsNull(StockLevel,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Current Stock]  " _
                '& " From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable " _
                '& " Group By ArticleDefId) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Current Stock] <> 0 AND a.[Minimum Stock Level] <> 0"
                'Ali Faisal : TFS1637 & TFS1638 : Show Unit column and also show stock with zero level
                strSQL = "Select *, a.[Minimum Stock Level] - a.[Current Stock] as [Stock Below Level], a.[Current Stock] - a.[Minimum Stock Level] as [Stock Above Level] From (Select ArticleId,ArticleDescription as Item, ArticleCode as Code, ArticleSizeName as Size, ArticleColorName as Color, ArticleUnitName As Unit, " _
                            & " ArticleGroupName as Department, ArticleTypeName as Type, IsNull(StockLevel,0) as [Minimum Stock Level], " _
                            & " IsNull(Stock.CurrentStock,0) as [Current Stock]  " _
                            & " From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable " _
                            & " Group By ArticleDefId) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE  "
                'Ali Faisal : TFS1637 & TFS1638 : End
                If Me.cmbResult.SelectedIndex = 0 Then
                    strSQL = strSQL & "  (a.[Minimum Stock Level] - a.[Current Stock]) > 0 AND a.[Minimum Stock Level] > 0 "
                ElseIf Me.cmbResult.SelectedIndex = 1 Then
                    strSQL = strSQL & " a.[Current Stock] <> 0 and  (a.[Current Stock] - a.[Minimum Stock Level]) > 0 "
                Else
                    strSQL = strSQL & " a.[Current Stock] <> 0 and  (a.[Current Stock] - a.[Minimum Stock Level]) = 0 "

                End If
                'Ali Faisal : TFS1637 & TFS1638 : Show Unit column and also show stock with zero level
            ElseIf StockLevel = "MaxLevel" Then
                strSQL = "Select * From (Select ArticleId,ArticleDescription as Item, ArticleCode as Code, ArticleSizeName as Size, ArticleColorName as Color, ArticleUnitName As Unit, ArticleGroupName as Department, ArticleTypeName as Type, IsNull(StockLevelMax,0) as StockLevel, Case When IsNull(Stock.CurrentStock,0) >= IsNull(StockLevelMax,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Max Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Max Level Stock] <> 0 "
            ElseIf StockLevel = "OptLevel" Then
                strSQL = "Select * From (Select ArticleId,ArticleDescription as Item, ArticleCode as Code, ArticleSizeName as Size, ArticleColorName as Color, ArticleUnitName As Unit, ArticleGroupName as Department, ArticleTypeName as Type, IsNull(StockLevelOpt,0) as StockLevel, Case When IsNull(Stock.CurrentStock,0) >= IsNull(StockLevelOpt,0) And IsNull(Stock.CurrentStock,0) <= IsNull(StockLevelOpt,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Opt Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Opt Level Stock] <> 0 "
            Else
                strSQL = "Select * From (Select ArticleId,ArticleDescription as Item, ArticleCode as Code, ArticleSizeName as Size, ArticleColorName as Color, ArticleUnitName As Unit, ArticleGroupName as Department, ArticleTypeName as Type, IsNull(StockLevel,0) as StockLevel, Case When IsNull(Stock.CurrentStock,0) < IsNull(StockLevel,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Min Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Min Level Stock] <> 0 "
            End If
            'Ali Faisal : TFS1637 & TFS1638 : End
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()


            Me.grd.RootTable.Columns(0).Visible = False
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            If StockLevel = "MinLevel" Then

                grd.RootTable.Columns("Current Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Current Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Current Stock").FormatString = "N"
                grd.RootTable.Columns("Current Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                grd.RootTable.Columns("Current Stock").TotalFormatString = "N"


                grd.RootTable.Columns("Stock Below Level").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Stock Below Level").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Stock Below Level").FormatString = "N"
                grd.RootTable.Columns("Stock Below Level").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                grd.RootTable.Columns("Stock Below Level").TotalFormatString = "N"

                grd.RootTable.Columns("Stock Above Level").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Stock Above Level").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Stock Above Level").FormatString = "N"
                grd.RootTable.Columns("Stock Above Level").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                grd.RootTable.Columns("Stock Above Level").TotalFormatString = "N"

                grd.RootTable.Columns("Minimum Stock Level").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Minimum Stock Level").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                grd.RootTable.Columns("Minimum Stock Level").FormatString = "N"
                grd.RootTable.Columns("Minimum Stock Level").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                grd.RootTable.Columns("Minimum Stock Level").TotalFormatString = "N"

                If cmbResult.SelectedIndex = 0 Then
                    Me.grd.RootTable.Columns("Stock Below Level").Visible = True
                    Me.grd.RootTable.Columns("Stock Above Level").Visible = False

                ElseIf cmbResult.SelectedIndex = 1 Then
                    Me.grd.RootTable.Columns("Stock Below Level").Visible = False
                    Me.grd.RootTable.Columns("Stock Above Level").Visible = True

                Else
                    Me.grd.RootTable.Columns("Stock Below Level").Visible = False
                    Me.grd.RootTable.Columns("Stock Above Level").Visible = False

                End If

                Me.chkShowDetails_CheckedChanged(Me, Nothing)
            End If
            ' StockLevel = String.Empty




            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = AdminEmail.ToString
            CtrlGrdBar1.Email.Subject = "Level: " + "" & Me.cmbResult.Text & ""
            CtrlGrdBar1.Email.DocumentNo = String.Empty
            CtrlGrdBar1.Email.DocumentDate = Date.Now

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            grd.ClearStructure()
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Minimum Stock"
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkShowDetails_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowDetails.CheckedChanged
        Try
            If chkShowDetails.Checked = True Then
                grd.RootTable.Columns("Size").Visible = True
                grd.RootTable.Columns("Color").Visible = True
                grd.RootTable.Columns("Type").Visible = True
                grd.RootTable.Columns("Department").Visible = True
                grd.RootTable.Columns("Unit").Visible = True
            Else
                grd.RootTable.Columns("Size").Visible = False
                grd.RootTable.Columns("Color").Visible = False
                grd.RootTable.Columns("Type").Visible = False
                grd.RootTable.Columns("Department").Visible = False
                grd.RootTable.Columns("Unit").Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub cmbResult_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbResult.SelectedIndexChanged
        Try
            FillGrid()


        Catch ex As Exception

        End Try
    End Sub
End Class