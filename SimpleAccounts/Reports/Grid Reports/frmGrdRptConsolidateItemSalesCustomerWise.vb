'Task No 2616 Mughees 8-5-2014 New Code for Grdconsolidated items customer wise
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmGrdRptConsolidateItemSalesCustomerWise
    Dim strSql As String

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If ChkConsolidated.Checked = True Then
            strSql = "SELECT   dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName as Color, " _
                      & "dbo.ArticleDefView.ArticleSizeName as Size, SUM(dbo.SalesDetailTable.Qty) AS Qty, 0 AS Price, " _
                     & "SUM(dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) AS Amount," _
                     & "SUM(dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price * dbo.SalesDetailTable.TaxPercent / 100) AS SalesTax, 0 AS [Gross Amount]  FROM dbo.SalesMasterTable INNER JOIN " _
                & "  dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId INNER JOIN  " _
                   & "  dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId "
            strSql += " WHERE dbo.SalesMasterTable.SalesId <> 0 "
            If Me.cmbCompany.SelectedIndex > 0 Then
                strSql += " AND dbo.SalesMasterTable.LocationId = " & Me.cmbCompany.SelectedValue & ""
            End If
            If Me.CmbCustomer.SelectedIndex > 0 Then
                strSql += " AND dbo.SalesMasterTable.CustomerCode = " & Me.CmbCustomer.SelectedValue & ""
            End If
            strSql += " and (Convert(varchar,dbo.SalesMasterTable.SalesDate,102) between Convert(datetime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(datetime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
            strSql += " GROUP BY dbo.ArticleDefView.ArticleId, "
            strSql += " dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,dbo.ArticleDefView.ArticleSizeName "
            Dim objdt As New DataTable
            objdt = GetDataTable(strSql)
            objdt.Columns("Gross Amount").Expression = "Amount+SalesTax"
            Me.grdsalesdata.DataSource = objdt

            Me.grdsalesdata.RetrieveStructure()
            Me.grdsalesdata.RootTable.Columns("Price").Visible = False
            Me.grdsalesdata.RootTable.Columns("ArticleId").Visible = False

            Me.grdsalesdata.AutoSizeColumns()

            Me.grdsalesdata.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("SalesTax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Gross Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdsalesdata.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("SalesTax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Gross Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdsalesdata.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdsalesdata.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdsalesdata.RootTable.Columns("SalesTax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdsalesdata.RootTable.Columns("Gross Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

        Else
            strSql = "SELECT   dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName as Color, " _
                      & "dbo.ArticleDefView.ArticleSizeName as Size, SUM(dbo.SalesDetailTable.Qty) AS Qty, dbo.SalesDetailTable.Price, " _
                     & "SUM(dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) AS Amount," _
                     & "SUM(((dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) * dbo.SalesDetailTable.TaxPercent) / 100) AS SalesTax, 0 AS [Gross Amount] , dbo.SalesMasterTable.LocationId FROM dbo.SalesMasterTable INNER JOIN " _
          & "  dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId INNER JOIN  " _
                   & "  dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId "
            strSql += " WHERE dbo.SalesMasterTable.SalesId <> 0 "
            If Me.cmbCompany.SelectedIndex > 0 Then
                strSql += " AND dbo.SalesMasterTable.LocationId = " & Me.cmbCompany.SelectedValue & ""
            End If
            If Me.CmbCustomer.SelectedIndex > 0 Then
                strSql += " AND dbo.SalesMasterTable.CustomerCode = " & Me.CmbCustomer.SelectedValue & ""
            End If
            strSql += " and (Convert(Varchar, dbo.SalesMasterTable.SalesDate,102) between Convert(datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(datetime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
            strSql += " GROUP BY dbo.ArticleDefView.ArticleId," _
             & " dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName, " _
                     & "dbo.ArticleDefView.ArticleSizeName, dbo.SalesMasterTable.LocationId,dbo.SalesDetailTable.Price"
            Dim objdt As New DataTable
            objdt = GetDataTable(strSql)
            objdt.Columns("Gross Amount").Expression = "Amount+SalesTax"
            Me.grdsalesdata.DataSource = objdt

            Me.grdsalesdata.RetrieveStructure()
            Me.grdsalesdata.RootTable.Columns("LocationId").Visible = False
            Me.grdsalesdata.RootTable.Columns("ArticleId").Visible = False
            Me.grdsalesdata.AutoSizeColumns()

            Me.grdsalesdata.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("SalesTax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Gross Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdsalesdata.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("SalesTax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdsalesdata.RootTable.Columns("Gross Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdsalesdata.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdsalesdata.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdsalesdata.RootTable.Columns("SalesTax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdsalesdata.RootTable.Columns("Gross Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

        End If

        If Not Me.grdsalesdata.RootTable Is Nothing Then CtrlGrdBar1_Load(Nothing, Nothing)

    End Sub

    Public Sub FillControls()
        Dim StrQueryCompany As String = "Select CompanyId,CompanyName from CompanyDefTable"
        Dim StrQueryCustomer As String = "Select coa_detail_id,detail_title,detail_code from vwCOADetail WHERE 1 = 1 "
        ''Start TFS2124
        If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
            StrQueryCustomer += " AND (account_type = 'Customer')  "
        Else
            StrQueryCustomer += " AND (account_type in('Customer','Vendor'))  "

        End If
        StrQueryCustomer += " Order by detail_title "
        ''End TFS2124
        FillDropDown(Me.cmbCompany, StrQueryCompany)
        FillDropDown(Me.CmbCustomer, StrQueryCustomer)
        Me.dtpFrom.Value = Date.Now.AddMonths(-1)

        Me.dtpTo.Value = Date.Now

    End Sub

    Private Sub frmGrdRptConsolidateItemSalesCustomerWise_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If

    End Sub
    Private Sub frmGrdRptConsolidateItemSalesCustomerWise_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        FillControls()
    End Sub

    'End Task 2616

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillControls()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdsalesdata.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdsalesdata.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdsalesdata.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Consolidated Item Sales Customer"
            'Me.CtrlGrdBar1.txtGridTitle.Text = "Min And Max Price Sales Detail"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class