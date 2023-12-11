''2-May-2014 TASK:2599 Imran Ali Add Column Comment+UOM In Delivery Chalan Stock Report
Imports SBModel
Public Class frmGrd_Prod_DC_WiseStock
    Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Department" Then
                FillListBox(Me.cmbDepartment.ListItem, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable")
            ElseIf Condition = "Type" Then
                FillListBox(Me.cmbType.ListItem, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable")
            ElseIf Condition = "Category" Then
                FillListBox(Me.cmbCategory.ListItem, "Select ArticleCompanyId, ArticleCompanyName From ArticleCompanyDefTable")
            ElseIf Condition = "SubCategory" Then
                FillListBox(Me.cmbSubCategory.ListItem, "Select ArticleLpoId, ArticleLpoName From ArticleLpoDefTable")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmGrd_Prod_DC_WiseStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            ToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrd_Prod_DC_WiseStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
            FillCombo("Department")
            FillCombo("Type")
            FillCombo("Category")
            FillCombo("SubCategory")

        Catch ex As Exception

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
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrd_Prod_DC_WiseStock)
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

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try


            FillCombo("Department")
            FillCombo("Type")
            FillCombo("Category")
            FillCombo("SubCategory")


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim dt As New DataTable
            dt = GetDataTable("SP_StockDeliveryChalan_AND_Production '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'")
            dt.TableName = "StockTable"
            Dim dv As New DataView
            dv.Table = dt
            dv.RowFilter = "ArticleDescription <> ''"
            If Me.cmbDepartment.SelectedIDs.Length > 0 Then
                dv.RowFilter += " AND ArticleGroupId In(" & Me.cmbDepartment.SelectedIDs & ") "
            End If
            If Me.cmbType.SelectedIDs.Length > 0 Then
                dv.RowFilter += "  AND ArticleTypeId In(" & Me.cmbType.SelectedIDs & ")"
            End If
            If Me.cmbCategory.SelectedIDs.Length > 0 Then
                dv.RowFilter += "  AND ArticleCompanyId In(" & Me.cmbCategory.SelectedIDs & ")"
            End If
            If Me.cmbSubCategory.SelectedIDs.Length > 0 Then
                dv.RowFilter += "  AND ArticleLPOId In(" & Me.cmbSubCategory.SelectedIDs & ")"
            End If
            dv.RowFilter += " AND (Opening <> 0 Or ProdQty <> 0 Or DcQty <> 0)"
            dv.Table.Columns("Closing").Expression = "((Opening+ProdQty)-DcQty)"
            Me.grd.DataSource = dv
            Me.grd.RetrieveStructure()
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.Columns(0).Visible = False ''2-May-2014 TASK:2599 Imran Ali Add Column Comment+UOM In Delivery Chalan Stock Report
            Me.grd.RootTable.Columns("ArticleGenderId").Visible = False
            Me.grd.RootTable.Columns("ArticleTypeId").Visible = False
            Me.grd.RootTable.Columns("ArticleLPOId").Visible = False
            Me.grd.RootTable.Columns("ArticleGroupId").Visible = False
            Me.grd.RootTable.Columns("ArticleCompanyId").Visible = False
            Me.grd.RootTable.Columns("ProdQty").Caption = "Production"
            Me.grd.RootTable.Columns("DcQty").Caption = "Delivery"



            Me.grd.RootTable.Columns("Opening").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ProdQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DcQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Closing").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum



            Me.grd.RootTable.Columns("Opening").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProdQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DcQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Closing").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Opening").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProdQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DcQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Closing").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far



            Me.grd.AutoSizeColumns()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFromDate.Value = Date.Today
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-1)
                Me.dtpToDate.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpToDate.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Record" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class