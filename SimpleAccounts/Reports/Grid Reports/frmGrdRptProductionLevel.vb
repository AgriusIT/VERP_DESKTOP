''19-Dec-2013 ReqId-900 Imran Ali            Add Amount Total in Production

Public Class frmGrdRptProductionLevel
    Dim StoreIssuanceDependonProductionPlan As Boolean = False
    Enum enmdetail
        ArticleId
        ArticleCode
        ArticleDescription
        ArticleColorName
        ArticleSizeName
        Price 'ReqId-900 Added Index
        Plan
        PlanValue 'ReqId-900 Added Index
        Count
    End Enum
    Private Sub frmGrdRptProductionLevel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("Plan")
            FillCombos("Project")
            FillCombos("Steps")
            FillCombos("Items")

            If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
                StoreIssuanceDependonProductionPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
            End If
            If StoreIssuanceDependonProductionPlan = False Then
                Me.dtpDemandDate.Enabled = True
                Me.ComboBox1.Enabled = False
            Else
                Me.dtpDemandDate.Enabled = False
                Me.ComboBox1.Enabled = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombos(Optional ByVal Condition As String = "")
        Try

            If Condition = "Plan" Then
                FillDropDown(Me.ComboBox1, "Select PlanId, PlanNo From PlanMasterTable ORDER BY PlanNo DESC")
            ElseIf Condition = "Project" Then
                FillDropDown(Me.ComboBox2, "Select CostCenterId, Name From tblDefCostCenter")
            ElseIf Condition = "Steps" Then
                FillListBox(Me.UiListControl1.ListItem, "Select ProdStep_Id, prod_step From tblproSteps")
            ElseIf Condition = "Items" Then
                FillListBox(Me.UiListControl2.ListItem, "Select ArticleId, ArticleDescription + ' ~ ' + ArticleCode as Article From ArticleDefView WHERE SalesItem=1 AND Active=1")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I
            id = Me.ComboBox1.SelectedIndex
            FillCombos("Plan")
            Me.ComboBox1.SelectedIndex = id
            id = Me.ComboBox2.SelectedIndex
            FillCombos("Project")
            Me.ComboBox2.SelectedIndex = id
            FillCombos("Steps")
            FillCombos("Items")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptProductionLevel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                If e.KeyCode = Keys.Escape Then
                    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                End If
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try

            Dim dt As New DataTable
            If StoreIssuanceDependonProductionPlan = True Then
                'Beofre against request no 900 
                'dt = GetDataTable("Select ArticleId, ArticleCode, ArticleDescription, ArticleColorName as [Color], ArticleSizeName as [Size], Isnull([Plan].Qty,0) as [Plan] From ArticleDefView LEFT OUTER JOIN (SELECT ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM dbo.PlanDetailTable WHERE PlanDetailTable.PlanId=" & Me.ComboBox1.SelectedValue & " GROUP BY ArticleDefId) [Plan] on [Plan].ArticleDefId = ArticleDefView.ArticleId WHERE  SalesItem=1 AND Active=1  " & IIf(Me.UiListControl2.SelectedIDs.Length > 0, " AND ArticleId In(" & Me.UiListControl2.SelectedIDs & ")", "") & " ORDER BY SortOrder ASC ")
                'ReqId-900 Added Column SalePrice
                dt = GetDataTable("Select ArticleId, ArticleCode, ArticleDescription, ArticleColorName as [Color], ArticleSizeName as [Size], Isnull(ArticleDefView.SalePrice,0) as Price, Isnull([Plan].Qty,0) as [Plan], 0 as PlanValue From ArticleDefView LEFT OUTER JOIN (SELECT ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM dbo.PlanDetailTable WHERE PlanDetailTable.PlanId=" & Me.ComboBox1.SelectedValue & " GROUP BY ArticleDefId) [Plan] on [Plan].ArticleDefId = ArticleDefView.ArticleId WHERE  SalesItem=1 AND Active=1  " & IIf(Me.UiListControl2.SelectedIDs.Length > 0, " AND ArticleId In(" & Me.UiListControl2.SelectedIDs & ")", "") & " ORDER BY SortOrder ASC ")
            Else
                'Beofre against request no 900 
                'dt = GetDataTable("Select ArticleId, ArticleCode, ArticleDescription, ArticleColorName as [Color], ArticleSizeName as [Size],Isnull([Plan].Qty,0) as [Plan] From ArticleDefView LEFT OUTER JOIN (SELECT ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM dbo.SalesOrderDetailTable WHERE SalesOrderId IN(Select SalesOrderId From SalesOrderMasterTable WHERE (Convert(Varchar, SalesOrderDate, 102) = Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.ComboBox2.SelectedIndex > 0, "  AND SalesOrderMasterTable.LocationId=" & Me.ComboBox2.SelectedValue & "", "") & ") GROUP BY ArticleDefId) [Plan] on [Plan].ArticleDefId = ArticleDefView.ArticleId WHERE  SalesItem=1 AND Active=1  " & IIf(Me.UiListControl2.SelectedIDs.Length > 0, " AND ArticleId In(" & Me.UiListControl2.SelectedIDs & ")", "") & " ORDER BY SortOrder ASC ")
                'ReqId-900 Added Column SalePrice
                dt = GetDataTable("Select ArticleId, ArticleCode, ArticleDescription, ArticleColorName as [Color], ArticleSizeName as [Size], Isnull(ArticleDefView.SalePrice,0) as Price, Isnull([Plan].Qty,0) as [Plan], 0 as PlanValue From ArticleDefView LEFT OUTER JOIN (SELECT ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM dbo.SalesOrderDetailTable WHERE SalesOrderId IN(Select SalesOrderId From SalesOrderMasterTable WHERE (Convert(Varchar, SalesOrderDate, 102) = Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.ComboBox2.SelectedIndex > 0, "  AND SalesOrderMasterTable.LocationId=" & Me.ComboBox2.SelectedValue & "", "") & ") GROUP BY ArticleDefId) [Plan] on [Plan].ArticleDefId = ArticleDefView.ArticleId WHERE  SalesItem=1 AND Active=1  " & IIf(Me.UiListControl2.SelectedIDs.Length > 0, " AND ArticleId In(" & IIf(Me.UiListControl2.SelectedIDs.Length > 0, Me.UiListControl2.SelectedIDs, 0) & ")", "") & " ORDER BY SortOrder ASC ")
            End If
            If dt IsNot Nothing Then
                Dim dtSteps As New DataTable
                dtSteps = GetDataTable("Select ProdStep_Id, prod_step,Isnull(Prod_Less,0) as Prod_Less From tblproSteps WHERE ProdStep_Id IN (" & IIf(Me.UiListControl1.SelectedIDs.Length > 0, Me.UiListControl1.SelectedIDs, 0) & ")")
                Dim myInt As Integer = 1I
                For Each r As DataRow In dtSteps.Rows
                    If Not dt.Columns.Contains(r("Prod_Step")) Then
                        dt.Columns.Add(r("ProdStep_Id"), GetType(System.Int32), r("ProdStep_Id"))
                        dt.Columns.Add(r("prod_step"), GetType(System.Double))
                        dt.Columns.Add(r("Prod_Less") & "^" & myInt, GetType(System.Boolean), r("Prod_Less"))
                        dt.Columns.Add("Amount" & "^" & myInt, GetType(System.Double), "Price*[" & r("prod_step") & "]") 'ReqId-900 Added Column Amount
                    End If
                    myInt += 1
                Next
                For Each r As DataRow In dt.Rows
                    'For c As Integer = enmdetail.Count To dt.Columns.Count - 3 Step 3
                    For c As Integer = enmdetail.Count To dt.Columns.Count - 4 Step 4 'ReqId-900 Chanage Step
                        r.BeginEdit()
                        r(c + 1) = 0
                        r.EndEdit()
                    Next
                Next
                dt.AcceptChanges()
                Dim dtData As New DataTable
                Dim strSQL As String = String.Empty
                strSQL = "SELECT dbo.ProductionLevelMasterTable.PStepsId, dbo.ProductionLevelDetailTable.ArticleDefId, SUM(ISNULL(dbo.ProductionLevelDetailTable.Qty, 0)) AS Qty " _
                         & " FROM dbo.ProductionLevelMasterTable INNER JOIN " _
                         & " dbo.ProductionLevelDetailTable ON dbo.ProductionLevelMasterTable.PLevelId = dbo.ProductionLevelDetailTable.PLevelId "
                strSQL += " WHERE ProductionLevelMasterTable.PStepsId <> 0 "
                If Me.ComboBox1.SelectedIndex > 0 Then
                    strSQL += " AND ProductionLevelMasterTable.PlanId=" & Me.ComboBox1.SelectedValue & ""
                End If
                If StoreIssuanceDependonProductionPlan = False Then
                    strSQL += " AND  (Convert(Varchar, ProductionLevelMasterTable.Doc_Date,102)=Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                End If
                If Me.ComboBox2.SelectedIndex > 0 Then
                    strSQL += " AND ProductionLevelMasterTable.ProjectId=" & Me.ComboBox2.SelectedValue & ""
                End If
                strSQL += " GROUP BY dbo.ProductionLevelDetailTable.ArticleDefId, dbo.ProductionLevelMasterTable.PStepsId"
                dtData = GetDataTable(strSQL)
                Dim dr() As DataRow
                For Each r As DataRow In dt.Rows
                    dr = dtData.Select("ArticleDefId=" & r("ArticleId") & "")
                    If dr IsNot Nothing Then
                        If dr.Length > 0 Then
                            For Each drFound As DataRow In dr
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(0)) + 1) = drFound(2)
                                r.EndEdit()
                            Next
                        End If
                    End If
                Next
                dt.Columns.Add("Total", GetType(System.Double))
                dt.Columns.Add("Total Amount", GetType(System.Double))
                Dim strTotal As String = String.Empty
                Dim strTotalAmount As String = String.Empty
                'For c As Integer = enmdetail.Count To dt.Columns.Count - 3 Step 3

                For c As Integer = enmdetail.Count To dt.Columns.Count - 4 Step 4 'ReqId-900 Change Step

                    If strTotalAmount.Length > 0 Then
                        strTotalAmount += "+" & "[" & dt.Columns(c + 3).ColumnName.ToString & "]"
                    Else
                        strTotalAmount = "[" & dt.Columns(c + 3).ColumnName.ToString & "]"
                    End If


                    If strTotal.Length > 0 Then
                        strTotal += IIf(dt.Columns(c + 2).ColumnName.Substring(0, dt.Columns(c + 2).ColumnName.IndexOf("^")) = "True", "-", "+") & "[" & dt.Columns(c + 1).ColumnName & "]"
                    Else
                        strTotal = IIf(dt.Columns(c + 2).ColumnName.Substring(0, dt.Columns(c + 2).ColumnName.IndexOf("^")) = "True", "-", "+") & "[" & dt.Columns(c + 1).ColumnName & "]"
                    End If
                Next

                dt.AcceptChanges()
                dt.Columns("Total").Expression = strTotal.ToString
                dt.Columns("PlanValue").Expression = "Price*Plan" 'ReqId-900 Set Expression Plan Amount
                dt.Columns("Total Amount").Expression = strTotalAmount.ToString 'ReqId-900 Set Expression Total Amount
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub ApplyGridSettings()
        Try
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            'For c As Integer = enmdetail.Count To Me.grd.RootTable.Columns.Count - 3 Step 3 'Before against request no . 900
            For c As Integer = enmdetail.Count To Me.grd.RootTable.Columns.Count - 4 Step 4 'ReqId-900 Change Step
                Me.grd.RootTable.Columns(c).Visible = False
                Me.grd.RootTable.Columns(c + 2).Visible = False
                Me.grd.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                'ReqId-900 Formating Column Alignment Caption And Aggregate Function
                Me.grd.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 3).Caption = "Amount"
                Me.grd.RootTable.Columns(c + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                'End ReqId-900
            Next
            Me.grd.RootTable.Columns("Plan").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Plan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Plan").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'ReqId-900 Formating Column Alignment And Aggregate Function
            Me.grd.RootTable.Columns("PlanValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("PlanValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PlanValue").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'ReqId-900 Formating Column Alignment 
            Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'ReqId-900 Formating Column Alignment And Aggregate Function
            Me.grd.RootTable.Columns("Total Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(str_ApplicationStartUpPath & "\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New System.IO.FileStream(str_ApplicationStartUpPath & "\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Production Level Report " & vbCrLf & "Plan No:" & IIf(Me.ComboBox1.SelectedIndex > 0, Me.ComboBox1.Text, "All") & vbCrLf & " Dispatch: " & IIf(Me.ComboBox2.SelectedIndex > 0, Me.ComboBox2.Text, "All") & "" & vbCrLf & "Demand Date: " & Me.dtpDemandDate.Value & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefrehs1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefrehs1.Click
        Try
            Me.Button1_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If Not Me.ComboBox1.SelectedIndex = -1 Then Me.ComboBox1.SelectedIndex = 0
            If Not Me.ComboBox2.SelectedIndex = -1 Then Me.ComboBox2.SelectedIndex = 0
            If Not Me.UiListControl1.ListItem.SelectedIndex = -1 Then Me.UiListControl1.ListItem.ClearSelected()
            If Not Me.UiListControl2.ListItem.SelectedIndex = -1 Then Me.UiListControl2.ListItem.ClearSelected()
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptProductionLevel_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Try
        '    ctr()
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
End Class