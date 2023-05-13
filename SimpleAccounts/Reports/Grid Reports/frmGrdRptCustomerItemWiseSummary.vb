Public Class frmGrdRptCustomerItemWiseSummary
    Enum enmCustomer
        coa_detail_id
        detail_code
        detail_title
        Count
    End Enum
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

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Me.cmbPeriod.Text = "Current Month"
            FillCombo("Department")
            FillCombo("Type")
            FillCombo("Category")
            FillCombo("SubCategory")


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptCustomerItemWiseSummary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillCombo("Department")
            FillCombo("Type")
            FillCombo("Category")
            FillCombo("SubCategory")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillData()
        Try
            Dim strSQL As String = String.Empty
            strSQL = String.Empty
            strSQL = "Select coa_detail_id, detail_code, detail_title From vwCOADetail WHERE Account_Type='Customer'"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            strSQL = String.Empty
            If rbtByItemCode.Checked = False Then
                strSQL = "Select ArticleId, ArticleDescription as Item From ArticleDefView WHERE ArticleId In (Select DISTINCT ArticleDefId From SalesDetailTable) "
            Else
                strSQL = "Select ArticleId, ArticleCode as Code From ArticleDefView WHERE ArticleId In (Select DISTINCT ArticleDefId From SalesDetailTable) "
            End If
            If Me.cmbDepartment.SelectedIDs.Trim.Length > 0 Then
                strSQL += " AND ArticleDefView.ArticleGroupId IN(" & Me.cmbDepartment.SelectedIDs & ")"
            End If
            If Me.cmbType.SelectedIDs.Trim.Length > 0 Then
                strSQL += " AND ArticleDefView.ArticleTypeId IN(" & Me.cmbType.SelectedIDs & ")"
            End If
            If Me.cmbCategory.SelectedIDs.Trim.Length > 0 Then
                strSQL += " AND ArticleDefView.ArticleCompanyId IN(" & Me.cmbCategory.SelectedIDs & ")"
            End If
            If Me.cmbSubCategory.SelectedIDs.Trim.Length > 0 Then
                strSQL += " AND ArticleDefView.ArticleLPOId IN(" & Me.cmbSubCategory.SelectedIDs & ")"
            End If
            Dim dtItem As New DataTable
            dtItem = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dtItem IsNot Nothing Then
                        If dtItem.Rows.Count > 0 Then
                            For Each r As DataRow In dtItem.Rows
                                If Not dt.Columns.Contains(r.Item(1).ToString) Then
                                    dt.Columns.Add(r.Item(0), GetType(System.Int32), r.Item(0))
                                    dt.Columns.Add(r.Item(1), GetType(System.Double))
                                End If
                            Next
                        End If
                    End If
                End If
            End If
            dt.AcceptChanges()
            For Each r As DataRow In dt.Rows
                For c As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2
                    r.BeginEdit()
                    r(c + 1) = 0
                    r.EndEdit()
                Next
            Next
            dt.AcceptChanges()
            strSQL = String.Empty
            strSQL = " SELECT IsNull(dbo.SalesMasterTable.CustomerCode,0) as CustomerCode, IsNull(dbo.SalesDetailTable.ArticleDefId,0) as ArticleDefId, SUM(IsNull(dbo.SalesDetailTable.Qty,0) * IsNull(dbo.SalesDetailTable.Price,0)) + SUM((Isnull(TaxPercent,0)/100)*(IsNull(dbo.SalesDetailTable.Qty,0) * IsNull(dbo.SalesDetailTable.Price,0)))  as Amount " _
                     & "  FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(varchar, SalesDate, 102) BETWEEN Convert(dateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(dateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) Group By IsNull(dbo.SalesDetailTable.ArticleDefId,0), IsNull(dbo.SalesMasterTable.CustomerCode,0) "
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)
            Dim dr() As DataRow
            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        dr = dtData.Select("CustomerCode=" & Val(r.Item(0).ToString) & "")
                        If dr IsNot Nothing Then
                            If dr.Length > 0 Then
                                For Each drFound As DataRow In dr
                                    r(dt.Columns.IndexOf(drFound(1)) + 1) = Val(drFound(2).ToString)
                                Next
                            End If
                        End If
                    Next
                End If
            End If
            dt.AcceptChanges()
            Dim strTotal As String = String.Empty
            For c As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2
                If strTotal.Length > 0 Then
                    strTotal += "+" & "[" & dt.Columns(c + 1).ColumnName.ToString & "]"
                Else
                    strTotal = "[" & dt.Columns(c + 1).ColumnName.ToString & "]"
                End If
            Next
            dt.Columns.Add("Total", GetType(System.Double))
            If strTotal.Length > 0 Then
                dt.Columns("Total").Expression = strTotal.ToString
            End If
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False
            For c As Integer = enmCustomer.Count To Me.grd.RootTable.Columns.Count - 2 Step 2
                If Me.grd.RootTable.Columns(c).DataMember <> "Total" Then
                    Me.grd.RootTable.Columns(c).Visible = False
                    Me.grd.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End If
            Next
            Me.grd.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            CtrlGrdBar2_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Customer Wise Item Sales Summary" & Chr(10) & "Date From: " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class