'19-Mar-2014 TASK:2508 IMRAN ALI Add Amount Columns Of openin Amount , In Amount, Out Amount
''05-Jul-2014 TASK:2718 IMRAN ALI Add new field Item Type In StockStatement Report
''02-Oct-2014 Task:M102142 Imran Ali Department Wise Filter Report
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmRptGrdStockStatement

    Dim IsOpenedForm As Boolean = False
    Private Sub frmRptGrdStockStatement_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Escape Then
            btnNew_Click(Nothing, Nothing)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        End If
        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            btnPrint_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub frmRptGrdStockStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
            FillDropDown(Me.cmbStatus, "select ArticleStatusID as ID, ArticleStatusName as Name from ArticleStatus where active=1")
            FillListBox(Me.UiListControl1.ListItem, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable WHERE Active=1 ORDER BY 2 ASC")
            FillListBox(Me.lstType.ListItem, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable WHERE Active=1 ORDER BY 2 ASC")
            Me.SplitContainer1.Panel2Collapsed = False
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetStockRecords()
        Try
            
            Dim strSQL As String = ""


            'GetDataTable("SP_StockStatementNew '" & Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy") & "', '" & Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy") & "', " & Me.cmbLocation.SelectedValue & "")
            Dim dt As DataTable = New DataTable 'New SBDal.RptStockStatmentDal().GetStockStatment(Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"), Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"), Me.cmbLocation.SelectedValue, IIf(Me.rdLoose.Checked = True, False, True))
            'Dim Str As String = "SP_StockStatementNew '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', " & IIf(Me.cmbLocation.SelectedIndex > 0, Me.cmbLocation.SelectedValue, 0) & ", " & IIf(Me.rdLoose.Checked = True, 0, 1) & ""
            'dt = GetDataTable(Str)
            'dt.AcceptChanges()
            'dt.TableName = "Stock"
            'Dim dv As New DataView
            'dv.Table = dt
            'Dim strItems As String = Me.UiListControl1.SelectedItems
            'dv.RowFilter = " ArticleId <> 0"
            'If strItems.Length > 0 Then
            '    dv.RowFilter += " AND ArticleGroupName In(" & strItems & ")"
            'End If
            'If Me.lstType.SelectedItems.Length > 0 Then
            '    dv.RowFilter += " AND ArticleTypeName In(" & Me.lstType.SelectedItems & ")"
            'End If
            'Me.grdStock.DataSource = dv
            ''02-Oct-2014 Task:M102142 Imran Ali Department Wise Filter Report

            Dim IsPackReport As Int32 = 0I

            If Me.rdLoose.Checked = False Then
                IsPackReport = 0
            Else
                IsPackReport = 1
            End If

            'Dim strSQL As String = "SELECT    dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName,dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,IsNull(ArticleDefView.PackQty,0) as [Pack Qty], ISNULL(Price.Price,0) as Price,   " _
            '        & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OpenStock.OpenQty, 0) ", " (ISNULL(OpenStock.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OpenStock.OpenQty, 0)*ISNULL(Price.Price,0)) as OpeningAmount,  " _
            '        & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(InStock.InQty, 0) ", " (ISNULL(InStock.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, (ISNULL(InStock.InQty, 0)*ISNULL(Price.Price,0)) as InAmount,  " _
            '        & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OutStock.OutQty, 0) ", " (ISNULL(OutStock.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  (ISNULL(OutStock.OutQty, 0)*ISNULL(Price.Price,0)) as OutAmount,  " _
            '        & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) ", " ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) *ISNULL(Price.Price,0)) as Amount, ArticleDefView.SortOrder  " _
            '        & " FROM         dbo.ArticleDefView INNER JOIN ArticleGroupDefTable Grp on Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
            '        & "       (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS OpenQty,   " _
            '        & "                                SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) AS OpenAmount  " _
            '        & "        FROM          StockMasterTable INNER JOIN " _
            '        & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
            '        & "         WHERE    lc.Location_Type <> 'Damage' AND (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "  " _
            '        & "        GROUP BY StockDetailTable.ArticleDefId Having  SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) <> 0 ) OpenStock ON OpenStock.ArticleDefId = dbo.ArticleDefView.ArticleId  left outer JOIN " _
            '        & "      (SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.OutQty, 0)) AS OutQty, SUM(IsNull(StockDetailTable.OutAmount, 0))  " _
            '        & "                               AS OutAmount " _
            '        & "        FROM          StockMasterTable  JOIN " _
            '        & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
            '        & "        WHERE  lc.Location_Type <> 'Damage'  AND (Convert(varchar, StockMasterTable.docDate,102) BETWEEN CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "       GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.OutQty, 0)) <> 0 ) OutStock ON OutStock.ArticleDefId = dbo.ArticleDefView.ArticleId left outer JOIN " _
            '        & "      ( " _
            '        & "        SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.InQty, 0)) AS InQty, SUM(IsNull(StockDetailTable.InAmount, 0))  " _
            '        & "                                AS InAmount  " _
            '        & "         FROM          StockMasterTable INNER JOIN " _
            '        & "                                StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID  " _
            '        & "       WHERE  lc.Location_Type <> 'Damage' AND (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "         GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.InQty, 0)) <> 0) InStock ON InStock.ArticleDefId = dbo.ArticleDefView.ArticleId  " _
            '        & "  LEFT OUTER JOIN (Select ArticleDefId, PurchaseNewPrice as Price From  IncrementReductionTable " _
            '        & "  WHERE (Id in (Select  Max(Id) as Id From IncrementReductionTable WHERE (Convert(varchar, IncrementReductionDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))  Group By ArticleDefId))) Price on Price.ArticleDefId = ArticleDefView.ArticleId " _
            '        & " WHERE(ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
            '        & " AND ISNULL(Grp.ServiceItem,0) <> 1 ORDER BY ArticleDefView.SortOrder ASC"
            'Dim strSQL As String = "SELECT    dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName,dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,IsNull(ArticleDefView.PackQty,0) as [Pack Qty], ISNULL(Price.Price,0) as Price,   " _
            '       & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OpenStock.OpenQty, 0) ", " (ISNULL(OpenStock.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OpenStock.OpenQty, 0)*ISNULL(Price.Price,0)) as OpeningAmount,  " _
            '       & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(InStock.InQty, 0) ", " (ISNULL(InStock.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, (ISNULL(InStock.InQty, 0)*ISNULL(Price.Price,0)) as InAmount,  " _
            '       & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OutStock.OutQty, 0) ", " (ISNULL(OutStock.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  (ISNULL(OutStock.OutQty, 0)*ISNULL(Price.Price,0)) as OutAmount,  " _
            '       & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) ", " ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) *ISNULL(Price.Price,0)) as Amount, ArticleDefView.SortOrder  " _
            '       & " FROM         dbo.ArticleDefView INNER JOIN ArticleGroupDefTable Grp on Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
            '       & "       (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS OpenQty,   " _
            '       & "                                SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) AS OpenAmount  " _
            '       & "        FROM          StockMasterTable INNER JOIN " _
            '       & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
            '       & "         WHERE    lc.Location_Type <> 'Damage' AND (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "  " _
            '       & "        GROUP BY StockDetailTable.ArticleDefId) OpenStock ON OpenStock.ArticleDefId = dbo.ArticleDefView.ArticleId  left outer JOIN " _
            '       & "      (SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.OutQty, 0)) AS OutQty, SUM(IsNull(StockDetailTable.OutAmount, 0))  " _
            '       & "                               AS OutAmount " _
            '       & "        FROM          StockMasterTable  JOIN " _
            '       & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
            '       & "        WHERE  lc.Location_Type <> 'Damage'  AND (Convert(varchar, StockMasterTable.docDate,102) BETWEEN CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '       & "       GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.OutQty, 0)) <> 0 ) OutStock ON OutStock.ArticleDefId = dbo.ArticleDefView.ArticleId left outer JOIN " _
            '       & "      ( " _
            '       & "        SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.InQty, 0)) AS InQty, SUM(IsNull(StockDetailTable.InAmount, 0))  " _
            '       & "                                AS InAmount  " _
            '       & "         FROM          StockMasterTable INNER JOIN " _
            '       & "                                StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID  " _
            '       & "       WHERE  lc.Location_Type <> 'Damage' AND (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '       & "         GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.InQty, 0)) <> 0) InStock ON InStock.ArticleDefId = dbo.ArticleDefView.ArticleId  " _
            '       & "  LEFT OUTER JOIN (Select ArticleDefId, PurchaseNewPrice as Price From  IncrementReductionTable " _
            '       & "  WHERE (Id in (Select  Max(Id) as Id From IncrementReductionTable WHERE (Convert(varchar, IncrementReductionDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))  Group By ArticleDefId))) Price on Price.ArticleDefId = ArticleDefView.ArticleId " _
            '       & " WHERE(ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
            '       & " AND ISNULL(Grp.ServiceItem,0) <> 1 ORDER BY ArticleDefView.SortOrder ASC"
            'Dim strSQL As String = "SELECT    dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName,dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,IsNull(ArticleDefView.PackQty,0) as [Pack Qty], ISNULL(Price.Price,0) as Price,   " _
            '      & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OpenStock.OpenQty, 0) ", " (ISNULL(OpenStock.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OpenStock.OpenQty, 0)*ISNULL(Price.Price,0)) as OpeningAmount,  " _
            '      & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(InStock.InQty, 0) ", " (ISNULL(InStock.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, (ISNULL(InStock.InQty, 0)*ISNULL(Price.Price,0)) as InAmount,  " _
            '      & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OutStock.OutQty, 0) ", " (ISNULL(OutStock.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  (ISNULL(OutStock.OutQty, 0)*ISNULL(Price.Price,0)) as OutAmount,  " _
            '      & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) ", " ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) *ISNULL(Price.Price,0)) as Amount, ArticleDefView.SortOrder  " _
            '      & " FROM  dbo.ArticleDefView INNER JOIN ArticleGroupDefTable Grp on Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
            '      & "       (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS OpenQty,   " _
            '      & "                                SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) AS OpenAmount  " _
            '      & "        FROM          StockMasterTable INNER JOIN " _
            '      & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
            '      & "         WHERE  (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "  " _
            '      & "        GROUP BY StockDetailTable.ArticleDefId) OpenStock ON OpenStock.ArticleDefId = dbo.ArticleDefView.ArticleId  left outer JOIN " _
            '      & "      (SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.OutQty, 0)) AS OutQty, SUM(IsNull(StockDetailTable.OutAmount, 0))  " _
            '      & "                               AS OutAmount " _
            '      & "        FROM          StockMasterTable  JOIN " _
            '      & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
            '      & "        WHERE  (Convert(varchar, StockMasterTable.docDate,102) BETWEEN CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '      & "       GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.OutQty, 0)) <> 0 ) OutStock ON OutStock.ArticleDefId = dbo.ArticleDefView.ArticleId left outer JOIN " _
            '      & "      ( " _
            '      & "        SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.InQty, 0)) AS InQty, SUM(IsNull(StockDetailTable.InAmount, 0))  " _
            '      & "                                AS InAmount  " _
            '      & "         FROM          StockMasterTable INNER JOIN " _
            '      & "                                StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID  " _
            '      & "       WHERE (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '      & "         GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.InQty, 0)) <> 0) InStock ON InStock.ArticleDefId = dbo.ArticleDefView.ArticleId  " _
            '      & "  LEFT OUTER JOIN (Select ArticleDefId, " & IIf(Me.rbtnLastPurchasePrice.Checked = True, " PurchaseNewPrice", "Cost_Price_New") & "  as Price From  IncrementReductionTable " _
            '      & "  WHERE (Id in (Select  Max(Id) as Id From IncrementReductionTable WHERE (Convert(varchar, IncrementReductionDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))  Group By ArticleDefId))) Price on Price.ArticleDefId = ArticleDefView.ArticleId " _
            '      & " WHERE(ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
            '      & " AND ISNULL(Grp.ServiceItem,0) <> 1 " & IIf(Me.cmbStatus.SelectedIndex > 0, " AND ArticleDefView.ArticleStatusID=" & Me.cmbStatus.SelectedValue & "", "") & " ORDER BY ArticleDefView.SortOrder ASC"

            If (Me.rbtnByTranscation.Checked) Then

                'strSQL = "Select  dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,IsNull(ArticleDefView.PackQty,0) as [Pack Qty],ISNULL(OC.Rate , 0)  AS Price, " _
                '              & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OC.OpenQty, 0) ", " (ISNULL(OC.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OC.OpeningAmount, 0)) as OpeningAmount,  " _
                '              & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(OC.InQty, 0) ", " (ISNULL(OC.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, ISNULL(OC.InAmount, 0) as InAmount,  " _
                '              & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OC.OutQty, 0) ", " (ISNULL(OC.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  ISNULL(OC.OutAmount, 0) as OutAmount,  " _
                '              & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OC.OpenQty, 0) + ISNULL(OC.InQty, 0)) - ISNULL(OC.OutQty, 0)) ", " ((( ISNULL(OC.OpenQty, 0) + ISNULL(OC.InQty, 0)) - ISNULL(OC.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  (ISNULL(OC.OpeningAmount, 0)  + ISNULL(OC.InAmount ,0)-   ISNULL(oc.OutAmount,0)) as Amount, ArticleDefView.SortOrder, 'View Image' as lnkImage, ArticleDefView.ArticlePicture, Convert(Image, '') As ArticleImage  " _
                '              & " FROM  ArticleDefView INNER JOIN ArticleGroupDefTable Grp on  Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
                '              & "(select st.ArticleDefId, isnull(ot.openingAmount ,0) as OpeningAmount, isnull(ot.OpenQty,0) as OpenQty, isnull(st.inamount,0) as InAmount ,isnull( st.Outamount,0) as OutAmount ,isnull( st.InQty,0) as inqty  ,isnull( st.outqty,0)  as OutQty ,  (( ISNULL(Ot.OpenQty, 0)+ISNULL(st.InQty, 0)) - ISNULL(st.OutQty, 0))  AS ClosingQty , ISNULL(st.rate, 0)  AS Rate from ( select isnull( sum(InQty),0) - isnull(sum(OutQty),0) as OpenQty, isnull(sum(InAmount),0) - isnull(sum(OutAmount),0) as openingAmount  " _
                '              & ",StockDetailTable.ArticleDefId from StockDetailTable inner join StockMasterTable on StockDetailTable.StockTransId= StockMasterTable.StockTransId   LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                '              & "         WHERE  (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "  " _
                '               & " GROUP BY StockDetailTable.ArticleDefId) OT " _
                '& " 	      Left Outer Join (select sum(isnull( StockDetailTable.InQty,0)) as inqty,sum(isnull(StockDetailTable.InAmount,0))  as inamount, sum(isnull(StockDetailTable.OutAmount,0))  as Outamount, sum(isnull(StockDetailTable.OutQty,0)) as outqty ,StockDetailTable.ArticleDefId , StockDetailTable.Rate as Rate from StockDetailTable inner join StockMasterTable on StockDetailTable.StockTransId= StockMasterTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID" _
                '& "      WHERE (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " group by StockDetailTable.ArticleDefId  , StockDetailTable.Rate ) as ST " _
                '              & ") as OC on OC.ArticleDefId=ArticleDefView.ArticleId  " _
                '              & " WHERE(ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
                '  & " AND ISNULL(Grp.ServiceItem,0) <> 1 " & IIf(Me.cmbStatus.SelectedIndex > 0, " AND ArticleDefView.ArticleStatusID=" & Me.cmbStatus.SelectedValue & "", "") & " ORDER BY ArticleDefView.SortOrder ASC"

                strSQL = "Select  dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, ArticleDefView.ArticleLpoName as Model, IsNull(ArticleDefView.PackQty,0) as [Pack Qty], ISNULL(Rate.Rate , 0)  AS Price, " _
                              & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OT.OpenQty, 0) ", " (ISNULL(OT.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OT.OpeningAmount, 0)) as OpeningAmount,  " _
                              & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(ST.InQty, 0) ", " (ISNULL(ST.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, ISNULL(ST.InAmount, 0) as InAmount,  " _
                              & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(ST.OutQty, 0) ", " (ISNULL(ST.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  ISNULL(ST.OutAmount, 0) as OutAmount,  " _
                              & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OT.OpenQty, 0) + ISNULL(ST.InQty, 0)) - ISNULL(ST.OutQty, 0)) ", " ((( ISNULL(OT.OpenQty, 0) + ISNULL(ST.InQty, 0)) - ISNULL(ST.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  (ISNULL(OT.OpeningAmount, 0)  + ISNULL(ST.InAmount ,0)-   ISNULL(ST.OutAmount,0)) as Amount, ArticleDefView.SortOrder, 'View Image' as lnkImage, ArticleDefView.ArticlePicture, Convert(Image, '') As ArticleImage  " _
                              & " FROM  ArticleDefView INNER JOIN ArticleGroupDefTable Grp on  Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
                              & "(select StockDetailTable.ArticleDefId,  SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) as OpenQty,  SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) AS OpeningAmount  " _
                              & " from StockDetailTable inner join StockMasterTable on StockDetailTable.StockTransId= StockMasterTable.StockTransId   LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                              & "         WHERE  (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "AND StockDetailTable.LocationId IN (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")") & "  " _
                               & " GROUP BY StockDetailTable.ArticleDefId) OT ON OT.ArticleDefId = ArticleDefView.ArticleId " _
                & " 	      Left Outer Join (select sum(isnull( StockDetailTable.InQty,0)) as inqty,sum(isnull(StockDetailTable.InAmount,0))  as inamount, sum(isnull(StockDetailTable.OutAmount,0))  as Outamount, sum(isnull(StockDetailTable.OutQty,0)) as outqty ,StockDetailTable.ArticleDefId from StockDetailTable inner join StockMasterTable on StockDetailTable.StockTransId= StockMasterTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID" _
                & "      WHERE (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "AND StockDetailTable.LocationId IN (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")") & " group by StockDetailTable.ArticleDefId) as ST ON ST.ArticleDefId=ArticleDefView.ArticleId" _
                 & "  LEFT OUTER JOIN (Select ArticleDefId, Rate From  StockDetailTable StockDetail1 Where StockDetail1.StockTransDetailId = (Select Max(StockTransDetailId) From StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId AND StockDetailTable.ArticleDefId =  StockDetail1.ArticleDefId   " _
                  & "  WHERE StockDetailTable.Rate > 0 And  (Convert(varchar, StockMasterTable.DocDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group By ArticleDefId)) Rate on Rate.ArticleDefId = ArticleDefView.ArticleId " _
                  & " WHERE(ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
                  & " AND ISNULL(Grp.ServiceItem,0) <> 1 " & IIf(Me.cmbStatus.SelectedIndex > 0, " AND ArticleDefView.ArticleStatusID=" & Me.cmbStatus.SelectedValue & "", "") & " ORDER BY ArticleDefView.SortOrder ASC"
            Else

                'strSQL = "SELECT    dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,IsNull(ArticleDefView.PackQty,0) as [Pack Qty], ISNULL(Price.Price,0) as Price,   " _
                '                                 & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OpenStock.OpenQty, 0) ", " (ISNULL(OpenStock.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OpenStock.OpenQty, 0)*ISNULL(Price.Price,0)) as OpeningAmount,  " _
                '                                 & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(InStock.InQty, 0) ", " (ISNULL(InStock.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, (ISNULL(InStock.InQty, 0)*ISNULL(Price.Price,0)) as InAmount,  " _
                '                                 & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OutStock.OutQty, 0) ", " (ISNULL(OutStock.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  (ISNULL(OutStock.OutQty, 0)*ISNULL(Price.Price,0)) as OutAmount,  " _
                '                                 & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) ", " ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) *ISNULL(Price.Price,0)) as Amount, ArticleDefView.SortOrder, 'View Image' as lnkImage, ArticleDefView.ArticlePicture, Convert(Image, '') As ArticleImage " _
                '                                  & " FROM  ArticleDefView INNER JOIN ArticleGroupDefTable Grp on  Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
                '                                 & "       (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS OpenQty,   " _
                '                                 & "                                SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) AS OpenAmount  " _
                '                                 & "        FROM          StockMasterTable INNER JOIN " _
                '                                 & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                '                                 & "         WHERE  (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "  " _
                '                                 & "        GROUP BY StockDetailTable.ArticleDefId) OpenStock ON OpenStock.ArticleDefId = dbo.ArticleDefView.ArticleId  left outer JOIN " _
                '                                 & "      (SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.OutQty, 0)) AS OutQty, SUM(IsNull(StockDetailTable.OutAmount, 0))  " _
                '                                 & "                               AS OutAmount " _
                '                                 & "        FROM          StockMasterTable  JOIN " _
                '                                 & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                '                                & "       WHERE  (Convert(varchar, StockMasterTable.docDate,102) BETWEEN CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
                '                                 & "       GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.OutQty, 0)) <> 0 ) OutStock ON OutStock.ArticleDefId = dbo.ArticleDefView.ArticleId left outer JOIN " _
                '                                 & "      ( " _
                '                                 & "        SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.InQty, 0)) AS InQty, SUM(IsNull(StockDetailTable.InAmount, 0))  " _
                '                                 & "                                AS InAmount  " _
                '                                 & "         FROM          StockMasterTable INNER JOIN " _
                '                                 & "                                StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID  " _
                '                                 & "       WHERE (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
                '                                & "        GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.InQty, 0)) <> 0) InStock ON InStock.ArticleDefId = dbo.ArticleDefView.ArticleId  " _
                '                                 & "  LEFT OUTER JOIN (Select ArticleDefId, " & IIf(Me.rbtnLastPurchasePrice.Checked = True, " PurchaseNewPrice", "Cost_Price_New") & "  as Price From  IncrementReductionTable " _
                '                                 & "  WHERE (Id in (Select  Max(Id) as Id From IncrementReductionTable WHERE (Convert(varchar, IncrementReductionDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))  Group By ArticleDefId))) Price on Price.ArticleDefId = ArticleDefView.ArticleId " _
                '                                 & " WHERE(ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
                '                                 & " AND ISNULL(Grp.ServiceItem,0) <> 1 " & IIf(Me.cmbStatus.SelectedIndex > 0, " AND ArticleDefView.ArticleStatusID=" & Me.cmbStatus.SelectedValue & "", "") & " ORDER BY ArticleDefView.SortOrder ASC"

                'strSQL = "SELECT    dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,IsNull(ArticleDefView.PackQty,0) as [Pack Qty], ISNULL(Price.Price,0) as Price,   " _
                '                                             & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OpenStock.OpenQty, 0) ", " (ISNULL(OpenStock.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OpenStock.OpenQty, 0)*ISNULL(Price.Price,0)) as OpeningAmount,  " _
                '                                             & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(InStock.InQty, 0) ", " (ISNULL(InStock.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, (ISNULL(InStock.InQty, 0)*ISNULL(Price.Price,0)) as InAmount,  " _
                '                                             & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OutStock.OutQty, 0) ", " (ISNULL(OutStock.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  (ISNULL(OutStock.OutQty, 0)*ISNULL(Price.Price,0)) as OutAmount,  " _
                '                                             & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) ", " ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) *ISNULL(Price.Price,0)) as Amount, ArticleDefView.SortOrder, 'View Image' as lnkImage, ArticleDefView.ArticlePicture, Convert(Image, '') As ArticleImage " _
                '                                              & " FROM  ArticleDefView INNER JOIN ArticleGroupDefTable Grp on  Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
                '                                             & "       (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS OpenQty,   " _
                '                                             & "                                SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) AS OpenAmount  " _
                '                                             & "        FROM          StockMasterTable INNER JOIN " _
                '                                             & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                '                                             & "         WHERE  (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "  " _
                '                                             & "        GROUP BY StockDetailTable.ArticleDefId) OpenStock ON OpenStock.ArticleDefId = dbo.ArticleDefView.ArticleId  left outer JOIN " _
                '                                             & "      (SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.OutQty, 0)) AS OutQty, SUM(IsNull(StockDetailTable.OutAmount, 0))  " _
                '                                             & "                               AS OutAmount " _
                '                                             & "        FROM          StockMasterTable  JOIN " _
                '                                             & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                '                                            & "       WHERE  (Convert(varchar, StockMasterTable.docDate,102) BETWEEN CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
                '                                             & "       GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.OutQty, 0)) <> 0 ) OutStock ON OutStock.ArticleDefId = dbo.ArticleDefView.ArticleId left outer JOIN " _
                '                                             & "      ( " _
                '                                             & "        SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.InQty, 0)) AS InQty, SUM(IsNull(StockDetailTable.InAmount, 0))  " _
                '                                             & "                                AS InAmount  " _
                '                                             & "         FROM          StockMasterTable INNER JOIN " _
                '                                             & "                                StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID  " _
                '                                             & "       WHERE (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
                '                                            & "        GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.InQty, 0)) <> 0) InStock ON InStock.ArticleDefId = dbo.ArticleDefView.ArticleId  " _
                '                                             & "  LEFT OUTER JOIN (Select ArticleDefId, " & IIf(Me.rbtnLastPurchasePrice.Checked = True, "Rate", "Cost_Price") & "  as Price From  StockDetailTable StockDetail1 Where StockDetail1.StockTransDetailId = (Select Max(StockTransDetailId) From StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId AND StockDetailTable.ArticleDefId =  StockDetail1.ArticleDefId   " _
                '                                           & "  WHERE " & IIf(Me.rbtnLastPurchasePrice.Checked = True, "StockDetailTable.Rate > 0 And StockDetailTable.InQty > 0 ", "StockDetailTable.Cost_Price > 0") & " And  (Convert(varchar, StockMasterTable.DocDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group By ArticleDefId)) Price on Price.ArticleDefId = ArticleDefView.ArticleId " _
                '                                              & " WHERE (ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
                '                                             & " AND ISNULL(Grp.ServiceItem,0) <> 1 " & IIf(Me.cmbStatus.SelectedIndex > 0, " AND ArticleDefView.ArticleStatusID=" & Me.cmbStatus.SelectedValue & "", "") & " ORDER BY ArticleDefView.SortOrder ASC"

                'Ali Faisal : Query modified to Get Last Price from StockDetailTable rather than IncreamentReductionTable on 27-Jan-2017
                strSQL = "SELECT    dbo.ArticleDefView.ArticleId,  ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  dbo.ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, ArticleDefView.ArticleLpoName as Model, IsNull(ArticleDefView.PackQty,0) as [Pack Qty], ISNULL(Price.Price,0) as Price,   " _
                                             & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OpenStock.OpenQty, 0) ", " (ISNULL(OpenStock.OpenQty, 0))/ArticleDefView.PackQty ") & " AS OpeningQty,  (ISNULL(OpenStock.OpenQty, 0)*ISNULL(Price.Price,0)) as OpeningAmount,  " _
                                             & "   " & IIf(Me.rdLoose.Checked = True, " ISNULL(InStock.InQty, 0) ", " (ISNULL(InStock.InQty, 0))/ArticleDefView.PackQty ") & " AS InQty, (ISNULL(InStock.InQty, 0)*ISNULL(Price.Price,0)) as InAmount,  " _
                                             & "  " & IIf(Me.rdLoose.Checked = True, " ISNULL(OutStock.OutQty, 0) ", " (ISNULL(OutStock.OutQty, 0))/ArticleDefView.PackQty ") & " AS OutQty,  (ISNULL(OutStock.OutQty, 0)*ISNULL(Price.Price,0)) as OutAmount,  " _
                                             & "   " & IIf(Me.rdLoose.Checked = True, " (( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) ", " ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)))/ArticleDefView.PackQty ") & " AS ClosingQty,  ((( ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0)) - ISNULL(OutStock.OutQty, 0)) *ISNULL(Price.Price,0)) as Amount, ArticleDefView.SortOrder, 'View Image' as lnkImage, ArticleDefView.ArticlePicture, Convert(Image, '') As ArticleImage " _
                                             & " FROM  ArticleDefView INNER JOIN ArticleGroupDefTable Grp on  Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER  JOIN  " _
                                             & "       (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS OpenQty,   " _
                                             & "                                SUM(ISNULL(StockDetailTable.InAmount, 0) - ISNULL(StockDetailTable.OutAmount, 0)) AS OpenAmount  " _
                                             & "        FROM          StockMasterTable INNER JOIN " _
                                             & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                                             & "         WHERE  (Convert(varchar, StockMasterTable.DocDate,102) < CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "AND StockDetailTable.LocationId IN (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")") & "  " _
                                             & "        GROUP BY StockDetailTable.ArticleDefId) OpenStock ON OpenStock.ArticleDefId = dbo.ArticleDefView.ArticleId  left outer JOIN " _
                                             & "      (SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.OutQty, 0)) AS OutQty, SUM(IsNull(StockDetailTable.OutAmount, 0))  " _
                                             & "                               AS OutAmount " _
                                             & "        FROM          StockMasterTable  JOIN " _
                                             & "                               StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID " _
                                             & "       WHERE  (Convert(varchar, StockMasterTable.docDate,102) BETWEEN CONVERT(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "AND StockDetailTable.LocationId IN (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")") & " " _
                                             & "       GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.OutQty, 0)) <> 0 ) OutStock ON OutStock.ArticleDefId = dbo.ArticleDefView.ArticleId left outer JOIN " _
                                             & "      ( " _
                                             & "        SELECT     StockDetailTable.ArticleDefId, SUM(IsNull(StockDetailTable.InQty, 0)) AS InQty, SUM(IsNull(StockDetailTable.InAmount, 0))  " _
                                             & "                                AS InAmount  " _
                                             & "         FROM          StockMasterTable INNER JOIN " _
                                             & "                                StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId  LEFT OUTER JOIN tblDefLocation lc on lc.Location_Id = StockDetailTable.LocationID  " _
                                             & "       WHERE (Convert(varchar, StockMasterTable.DocDate,102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "AND StockDetailTable.LocationId IN (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")") & " " _
                                             & "        GROUP BY StockDetailTable.ArticleDefId Having SUM(IsNull(StockDetailTable.InQty, 0)) <> 0) InStock ON InStock.ArticleDefId = dbo.ArticleDefView.ArticleId  " _
                                             & "  LEFT OUTER JOIN (Select StockDetailTable.ArticleDefId, " & IIf(Me.rbtnLastPurchasePrice.Checked = True, "Rate", "Cost_Price") & "  as Price From  StockDetailTable  Where StockDetailTable.StockTransDetailId IN (Select Max(StockTransDetailId) FROM StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId Where INQTY > 0 AND DOCNO not like'%SRN%' AND (CONVERT(varchar, DOCDate, 102)  " _
                                             & "  <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group By StockDetailTable.ArticleDefId)) Price on Price.ArticleDefId = ArticleDefView.ArticleId " _
                                             & " WHERE (ArticleDefView.Active = 1) " & IIf(UiListControl1.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleGroupId IN(" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " AND ArticleDefView.ArticleTypeId In (" & Me.lstType.SelectedIDs & ") ", "") & " " _
                                             & " AND ISNULL(Grp.ServiceItem,0) <> 1 " & IIf(Me.cmbStatus.SelectedIndex > 0, " AND ArticleDefView.ArticleStatusID=" & Me.cmbStatus.SelectedValue & "", "") & " ORDER BY ArticleDefView.SortOrder ASC"

            End If

            dt = GetDataTable(strSQL)
            Me.grdStock.DataSource = dt
            'End Task:M102142
            Me.grdStock.RetrieveStructure()
            Me.grdStock.RootTable.Columns("ArticleId").Visible = False
            'Me.grdStock.RootTable.Columns.Add("lnkImage", Janus.Windows.GridEX.ColumnType.Link, Janus.Windows.GridEX.EditType.TextBox)
            Me.grdStock.RootTable.Columns("lnkImage").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdStock.RootTable.Columns("lnkImage").Caption = "View"
            Me.grdStock.RootTable.Columns("lnkImage").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdStock.RootTable.Columns("lnkImage").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ApplyGridSettings()
            IsOpenedForm = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnHistory.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmRptGrdStockStatement)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnHistory.Text = "History" Then
                            Me.btnHistory.Enabled = dt.Rows(0).Item("History_Rights").ToString()
                        End If
                    End If
                End If
            Else
                Me.btnHistory.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "History" Then
                        Me.btnHistory.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            'Me.dtpFromDate.Value = Me.dtpFromDate.Value.AddMonths(-1)
            'Me.dtpToDate.Value = Date.Now
            GetStockRecords()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If
            GetStockRecords()
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            Me.SplitContainer1.Panel2Collapsed = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            'Before against task:2718
            'Me.grdStock.RootTable.Columns("ArticleGroupName").Caption = "Type"
            Me.grdStock.RootTable.Columns("ArticleGroupName").Caption = "Department" 'Task:2718 Change Caption
            Me.grdStock.RootTable.Columns("ArticleTypeName").Caption = "Type" 'Task:2718 Change Caption
            Me.grdStock.RootTable.Columns("ArticleColorName").Caption = "Color"
            Me.grdStock.RootTable.Columns("ArticleSizeName").Caption = "Size"
            Me.grdStock.RootTable.Columns("ArticleUnitName").Caption = "Unit"
            Me.grdStock.RootTable.Columns("ArticleCompanyName").Caption = "Article Category" ''ArticleCompanyName
            Me.grdStock.RootTable.Columns("SortOrder").Visible = False 'Task:2508 Hidden Column
            Me.grdStock.RootTable.Columns("ArticleImage").Visible = False
            Me.grdStock.RootTable.Columns("ArticlePicture").Visible = False
            For Each grdCol As Janus.Windows.GridEX.GridEXColumn In Me.grdStock.RootTable.Columns
                'Before against task:2508
                'If grdCol.Index = 8 Or grdCol.Index = 9 Or grdCol.Index = 10 Or grdCol.Index = 11 Or grdCol.Index = 12 Then
                'Task:2508 Added Index 13,14,15
                'Before against Task:2718
                'If grdCol.Index = 8 Or grdCol.Index = 9 Or grdCol.Index = 10 Or grdCol.Index = 11 Or grdCol.Index = 12 Or grdCol.Index = 13 Or grdCol.Index = 14 Or grdCol.Index = 15 Then
                'Taks:2718 Change Index
                If grdCol.Index = 10 Or grdCol.Index = 11 Or grdCol.Index = 12 Or grdCol.Index = 13 Or grdCol.Index = 14 Or grdCol.Index = 15 Or grdCol.Index = 16 Or grdCol.Index = 17 Or grdCol.Index = 18 Or grdCol.Index = 19 Then
                    'End Task:2718
                    grdCol.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    grdCol.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    grdCol.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    grdCol.FormatString = "N"
                    grdCol.TotalFormatString = "N"
                    'End Task:2508
                End If
            Next

            Me.grdStock.RootTable.Columns("Pack Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("Pack Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("Pack Qty").FormatString = "N" & DecimalPointInValue

            Me.grdStock.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue

            'Me.grdStock.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click, btnPrint1.Click
        Try
            GetCrystalReportRights()
            Dim dt As DataTable = CType(Me.grdStock.DataSource, DataTable)
            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    r.BeginEdit()
                    If IO.File.Exists(r.Item("ArticlePicture").ToString) Then
                        LoadPicture(r, "ArticleImage", r.Item("ArticlePicture"))
                    End If
                    r.EndEdit()
                Next
            End If
            AddRptParam("@FromDate", Me.dtpFromDate.Value)
            AddRptParam("@ToDate", Me.dtpToDate.Value)
            If Me.grdStock.RowCount = 0 Then Exit Sub
            ShowReport("rptStockStatementWithImage", , , , , , , dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridBarUserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdStock.LoadLayoutFile(fs)
                fs.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdStock_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStock.LinkClicked
        Try
            If e.Column.DataMember = "lnkImage" Then
                ApplyStyleSheet(frmArticleImageDisplay)
                frmArticleImageDisplay.ArticleDefId = Val(Me.grdStock.GetRow.Cells("ArticleId").Value.ToString)
                frmArticleImageDisplay.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

    '        If Me.SplitContainer1.Panel2Collapsed = True Then
    '            Me.SplitContainer1.Panel2Collapsed = False
    '            grdStock_SelectionChanged(grdStock, Nothing)
    '        Else
    '            Me.SplitContainer1.Panel2Collapsed = True
    '            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
    '                frmhistoryBySize.Close()
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub grdStock_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdStock.SelectionChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.grdStock.RowCount = 0 Then Exit Sub

            'If Me.SplitContainer1.Panel2.Controls.Count = 0 Then
            '    Me.SplitContainer1.Panel2Collapsed = True
            'Else
            '    Me.SplitContainer1.Panel2Collapsed = False
            'End If

            If Me.SplitContainer1.Panel1Collapsed = True Then Exit Sub
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmhistory.Close()
            End If
            frmhistory.LocationId = Me.cmbLocation.SelectedValue
            frmhistory.article_code = Me.grdStock.GetRow.Cells("ArticleId").Value
            frmhistory.ArticleCode = Me.grdStock.GetRow.Cells("ArticleCode").Text
            frmhistory.ArticleDescription = Me.grdStock.GetRow.Cells("ArticleDescription").Text
            frmhistory.startDate = Me.dtpFromDate.Value
            frmhistory.EndDate = Me.dtpToDate.Value
            frmhistory.TopLevel = False
            frmhistory.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmhistory.Dock = DockStyle.Fill
            Me.SplitContainer1.Panel2.Controls.Add(frmhistory)
            frmhistory.Show()
            frmhistory.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub UiCtrlGridBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.UiCtrlGridBar1.txtGridTitle.Text = "Stock Statement - From " & Me.dtpFromDate.Value.Date.ToString("dd-MM-yyyy") & " To " & Me.dtpToDate.Value.Date.ToString("dd-MM-yyyy")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0
            id = Me.cmbLocation.SelectedValue
            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
            Me.cmbLocation.SelectedValue = id

            FillListBox(Me.UiListControl1.ListItem, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable WHERE Active=1 ORDER BY 2 ASC")
            FillListBox(Me.lstType.ListItem, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable WHERE Active=1 ORDER BY 2 ASC")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdStock.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Statement" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Try
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
            Else
                Me.grdStock_SelectionChanged(Nothing, Nothing)
                Me.SplitContainer1.Panel2Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtnByTranscation_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnByTranscation.CheckedChanged

    End Sub
End Class