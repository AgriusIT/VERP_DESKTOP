''27-Dec-2013 M-1   Imran Ali             Skip Customer Wich have no transaction
''03-Feb-2014      TSK:2412     Imran Ali           Revised Dispatch Detail Report
''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
'15-May-2014 Task:2627 Junaid dispatch detail "company name shift name report name and date" fields added.
Public Class frmGrdDispatchDetail
    Public Enum enmCustomers
        Id
        Code
        Title
        Count
    End Enum
    Dim flgShift As Boolean = False
    Dim IsOpendForm As Boolean = False
    Private Sub frmGrdDispatchDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name From tblDefCostCenter")
            Me.cmbPeriod.Text = "Current Month"
            IsOpendForm = True
            'FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select convert(varchar, coa_detail_id) as coa_detail_id, detail_code, detail_title From vwCOADetail  WHERE vwCOADetail.Account_Type='Customer' union select '0A','','Opening Stock' as [Oping Stock] union select '0B','','Production' as [Production Stock] Union Select '0C', '', 'Receiving' as [Receiving Stock]  Union Select '0CC', '', 'Total Receiving' as [Total Receiving] Union Select '0E' ,'', 'Total Dispatch' as [Total Dispatch] Union Select '0F', '', 'Current Stock' as [Current Stock] ORDER BY 1 ASC"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            '-------------------------------- Products ---------------------------
            If Me.btnItemName.Checked = True Then
                strSQL = "Select ArticleId, ArticleDescription, SalePrice From ArticleDefView WHERE SalesItem=1 AND Active=1 ORDER BY ArticleDefView.SortOrder Asc"
            Else
                strSQL = "Select ArticleDefTable.ArticleId, ArticleDefTableMaster.ArticleCode, ArticleDefTable.SalePrice From ArticleDefTable INNER JOIN ArticleDefTableMaster On ArticleDefTableMaster.ArticleId = ArticleDefTable.MasterId INNER JOIN ArticleGroupDefTable ON  ArticleGroupDefTable.ArticleGroupId = ArticleDefTableMaster.ArticleGroupId WHERE ArticleGroupDefTable.SalesItem=1 AND ArticleDefTable.Active=1 ORDER BY ArticleDefTable.SortOrder Asc"
            End If
            Dim dtArt As New DataTable
            dtArt = GetDataTable(strSQL)
            Dim i As Integer = 1
            For Each r As DataRow In dtArt.Rows
                If Not dtArt.Columns.Contains(r(1)) Then
                    dt.Columns.Add(r(0), GetType(System.String), r(0))
                    dt.Columns.Add(r(1) & "^" & i, GetType(System.Double))
                    dt.Columns.Add("Price" & "^" & i, GetType(System.Double), r(2))
                End If
                i += 1
            Next
            '--------------------------------------------------------------------- Adjustment Stock  ---------------------------------------------
            Dim drType As DataRow
            Dim dtType As New DataTable
            dtType = GetDataTable("Select AdjType_Id, AdjType_Id, AdjType From tblAdjustmentType")
            For Each r As DataRow In dtType.Rows
                drType = dt.NewRow
                drType(0) = r(0)
                drType(1) = r(1)
                drType(2) = r(2)
                dt.Rows.Add(drType)
            Next

            For Each r As DataRow In dt.Rows
                For c As Integer = enmCustomers.Count To dt.Columns.Count - 3 Step 3
                    r.BeginEdit()
                    r(c + 1) = 0
                    r.EndEdit()
                Next
            Next
            dt.AcceptChanges()
            strSQL = String.Empty
            strSQL = "Select '0A' as [Opening], vwArt.ArticleId, (ISNULL(Opening.Qty,0) " & IIf(flgShift = True, "+0)", " +ISNULL(NStock.Qty,0))") & " as OpeningQty From ArticleDefView vwArt LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) As Qty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransID LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = StockDetailTable.LocationId WHERE (Convert(Varchar, StockMasterTable.docDate, 102) < Convert(DateTime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' Group By ArticleDefID) Opening On Opening.ArticleDefId = vwArt.ArticleId LEFT OUTER JOIN (Select ArticleDefID, SUM(Isnull(InQty,0)-isnull(OutQty,0)) as Qty From StockMasterTable  INNER JOIN StockDetailTable on StockDetailTable.StockTransID = StockMasterTAble.StockTransId  LEFT OUTER JOIN tblDefCostCenter on tblDefCostCenter.CostCenterId = StockMasterTable.Project LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id = StockDetailTable.LocationId  WHERE ISNULL(tbldefCostCenter.DayShift,0)=1 AND tblDefLocation.Location_Type <> 'Damage' AND (Convert(varchar, StockMasterTable.docDate, 102) = Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' ,102)) Group By ArticleDefID) NStock On NStock.ArticleDefId = vwArt.ArticleId WHERE vwArt.SalesItem=1 "
            Dim dtOpening As New DataTable
            dtOpening = GetDataTable(strSQL)
            Dim drOpening() As DataRow
            For Each r As DataRow In dt.Rows
                drOpening = dtOpening.Select("[Opening]='" & r(0) & "'")
                If drOpening IsNot Nothing Then
                    If drOpening.Length > 0 Then
                        For Each drFound As DataRow In drOpening
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'end Task:2487
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()
            strSQL = String.Empty
            strSQL = "Select '0B' as Production, ArticleDefId, SUM(ISNULL(Qty,0)) as Opening From ProductionMasterTable INNER JOIN ProductionDetailTable On ProductionMasterTable.Production_ID = ProductionDetailTable.Production_Id WHERE (Convert(Varchar, Production_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND ProductionMasterTable.Project=" & Me.cmbCostCenter.SelectedValue & "", "") & " Group By ArticleDefId"
            Dim dtReceiving As New DataTable
            dtReceiving = GetDataTable(strSQL)
            Dim drReceiving() As DataRow
            For Each r As DataRow In dt.Rows
                drReceiving = dtReceiving.Select("[Production]='" & r(0) & "'")
                If drReceiving IsNot Nothing Then
                    If drReceiving.Length > 0 Then
                        For Each drFound As DataRow In drReceiving
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'End Task:2487
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()



            '--------------------------------- Receiving Stock --------------------------------
            strSQL = String.Empty
            strSQL = "Select '0C' as Receiving, ArticleDefId, SUM(ISNULL(Qty,0)) as Opening From ReceivingMasterTable INNER JOIN ReceivingDetailTable On ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId WHERE (Convert(Varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND ReceivingMasterTable.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " Group By ArticleDefId"
            Dim dtRec As New DataTable
            dtRec = GetDataTable(strSQL)
            Dim drRec() As DataRow
            For Each r As DataRow In dt.Rows
                drRec = dtRec.Select("[Receiving]='" & r(0) & "'")
                If drRec IsNot Nothing Then
                    If drRec.Length > 0 Then
                        For Each drFound As DataRow In drRec
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'End Task:2487
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()


            '--------------------------------- Receiving Stock --------------------------------
            strSQL = String.Empty
            strSQL = "Select '0CC' as [Total Receiving], ArticleId, ISNULL(Opening.OpeningQty,0)+isnull(RecvQty,0)+Isnull(ProdQty,0)" & IIf(flgShift = False, "+ISNULL(NStock.NStockQty,0)", "+0") & " as Qty From ArticleDefView vwArt LEFT OUTER JOIN(Select ArticleDefId, SUM(ISNULL(Qty,0)) as ProdQty From ProductionMasterTable INNER JOIN ProductionDetailTable On ProductionMasterTable.Production_ID = ProductionDetailTable.Production_Id WHERE (Convert(Varchar, Production_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND ProductionMasterTable.Project=" & Me.cmbCostCenter.SelectedValue & "", "") & " Group By ArticleDefId) Prod on Prod.ArticleDefId = vwArt.ArticleId LEFT OUTER JOIN(Select ArticleDefId, SUM(ISNULL(Qty,0)) as RecvQty From ReceivingMasterTable INNER JOIN ReceivingDetailTable On ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId WHERE (Convert(Varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND ReceivingMasterTable.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " Group By ArticleDefId) Recv on Recv.ArticleDefId = vwArt.ArticleId LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) As OpeningQty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransID LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = StockDetailTable.LocationId WHERE (Convert(Varchar, StockMasterTable.docDate, 102) < Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' Group By ArticleDefID) Opening On Opening.ArticleDefId = vwArt.ArticleId LEFT OUTER JOIN (Select ArticleDefId, SUM(Isnull(InQty,0)-isnull(OutQty,0)) as NStockQty From StockMasterTable  INNER JOIN StockDetailTable on StockDetailTable.StockTransID = StockMasterTAble.StockTransId  LEFT OUTER JOIN tblDefCostCenter on tblDefCostCenter.CostCenterId = StockMasterTable.Project LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id = StockDetailTable.LocationId  WHERE ISNULL(tbldefCostCenter.DayShift,0)=1 AND tblDefLocation.Location_Type <> 'Damage' AND (Convert(varchar, StockMasterTable.docDate, 102) = Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' ,102)) Group By ArticleDefID) NStock On NStock.ArticleDefId = vwArt.ArticleId WHERE vwArt.SalesItem=1 "
            Dim dtTotRec As New DataTable
            dtTotRec = GetDataTable(strSQL)
            Dim drTotRec() As DataRow
            For Each r As DataRow In dt.Rows
                drTotRec = dtTotRec.Select("[Total Receiving]='" & r(0) & "'")
                If drTotRec IsNot Nothing Then
                    If drTotRec.Length > 0 Then
                        For Each drFound As DataRow In drTotRec
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'End Task:2487
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()


            ''------------------------------------ Total Stock ----------------------------------
            'strSQL = String.Empty
            'strSQL = "Select '0D' as [Total Stock], ArticleId, (ISNULL(OpeningQty,0)+ISNULL(ReceivingQty,0))" & IIf(flgShift = False, "+ISNULL(NStock.NStockQty,0)", "+0") & " as Qty From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) as OpeningQty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id = StockDetailTable.LocationID WHERE (Convert(Varchar, StockMasterTable.docDate, 102) < Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' Group By ArticleDefId) Opening on Opening.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) as ReceivingQty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id = StockDetailTable.LocationId WHERE (Convert(Varchar, StockMasterTable.docDate, 102) BETWEEN Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND StockMasterTable.Project=" & Me.cmbCostCenter.SelectedValue & "", "") & "  Group By ArticleDefId) Receiving on Receiving.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) as NStockQty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefCostCenter On tbldefCostCenter.CostCenterId= StockMasterTable.Project LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = StockDetailTable.LocationID WHERE(Convert(Varchar, StockMasterTable.docDate, 102) BETWEEN Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' AND ISNULL(tbldefCostCenter.DayShift,0)=1 Group By ArticleDefId) NStock on NStock.ArticleDefId = ArticleDefView.ArticleId WHERE ArticleDefView.SalesItem=1 "
            'Dim dtStock As New DataTable
            'dtStock = GetDataTable(strSQL)
            'Dim drStock() As DataRow
            'For Each r As DataRow In dt.Rows
            '    drStock = dtStock.Select("[Total Stock]='" & r(0) & "'")
            '    If drStock IsNot Nothing Then
            '        If drStock.Length > 0 Then
            '            For Each drFound As DataRow In drStock
            '                r.BeginEdit()
            '                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
            '                r.EndEdit()
            '            Next
            '        End If
            '    End If
            'Next
            'dt.AcceptChanges()


            '----------------------------------- Total Dispatch --------------------------------------
            strSQL = String.Empty
            strSQL = "Select '0E' as [Total Dispatch], ArticleDefId, SUM(isnull(Qty,0)) as Qty From SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId LEFT OUTER JOIN CompanyDefTable On CompanyDefTable.CompanyId = SalesMasterTable.LocationId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND  CompanyDefTable.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " Group By ArticleDefId"
            Dim dtDisp As New DataTable
            dtDisp = GetDataTable(strSQL)
            Dim drDisp() As DataRow
            For Each r As DataRow In dt.Rows
                drDisp = dtDisp.Select("[Total Dispatch]='" & r(0) & "'")
                If drDisp IsNot Nothing Then
                    If drDisp.Length > 0 Then
                        For Each drFound As DataRow In drDisp
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'End Task:2487
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()

            ' ----------------------------------- Current Stock ---------------------------------------------------------
            strSQL = String.Empty
            strSQL = "Select '0F' as [Current Stock], ArticleId, ((ISNULL(OpeningQty,0)+ISNULL(ReceivingQty,0))-ISNULL(OutQty,0))" & IIf(flgShift = False, "+ISNULL(NStock.NStockQty,0)", "+0") & " as Qty From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) as OpeningQty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id = StockDetailTable.LocationId WHERE (Convert(Varchar, StockMasterTable.docDate, 102) < Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' Group By ArticleDefId) Opening on Opening.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)) as ReceivingQty, SUM(ISNULL(OutQty,0)) as OutQty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id  = StockDetailTable.LocationId WHERE (Convert(Varchar, StockMasterTable.docDate, 102) BETWEEN Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND StockMasterTable.Project=" & Me.cmbCostCenter.SelectedValue & "", "") & " Group By ArticleDefId) Receiving on Receiving.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) as NStockQty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN tblDefCostCenter On tblDefCostCenter.CostCenterId = StockMasterTable.Project LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id = StockDetailTable.LocationId WHERE(Convert(Varchar, StockMasterTable.docDate, 102) BETWEEN Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' AND ISNULL(tbldefCostCenter.DayShift,0)=1 Group By ArticleDefId) NStock on NStock.ArticleDefId = ArticleDefView.ArticleId WHERE ArticleDefView.SalesItem=1 "
            Dim dtCurrStock As New DataTable
            dtCurrStock = GetDataTable(strSQL)
            Dim drCurrStock() As DataRow
            For Each r As DataRow In dt.Rows
                drCurrStock = dtCurrStock.Select("[Current Stock]='" & r(0) & "'")
                If drCurrStock IsNot Nothing Then
                    If drCurrStock.Length > 0 Then
                        For Each drFound As DataRow In drCurrStock
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'End Task:2487
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()


            '------------------------------------------------------------- Transactions -----------------------------------------
            strSQL = "Select convert(varchar, CustomerCode) as CustomerCode, ArticleDefId, SUM(Isnull(Qty,0)) as Qty From SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId LEFT OUTER JOIN CompanyDefTable On CompanyDefTable.CompanyId = SalesMasterTable.LocationId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND  CompanyDefTable.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " Group By CustomerCode, ArticleDefId"
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dtData.Select("CustomerCode='" & r(0) & "'")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'End Task:2487
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()

            strSQL = "SELECT Convert(varchar, dbo.StockAdjustmentDetail.AdjustmentTypeId) AS AdjType_Id, dbo.StockAdjustmentDetail.Artical_id AS ArticleDefId, abs(SUM(dbo.StockAdjustmentDetail.Qty)) AS Qty  FROM dbo.StockAdjustmentDetail INNER JOIN dbo.StockAdjustmentMaster ON dbo.StockAdjustmentDetail.SA_id = dbo.StockAdjustmentMaster.SA_id WHERE (Convert(varchar, Doc_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(DateTime , '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND Project=" & Me.cmbCostCenter.SelectedValue & "", "") & " GROUP BY dbo.StockAdjustmentDetail.AdjustmentTypeId, dbo.StockAdjustmentDetail.Artical_id HAVING   (dbo.StockAdjustmentDetail.AdjustmentTypeId <> 0) "
            Dim dtAdj As New DataTable
            dtAdj = GetDataTable(strSQL)
            Dim drAdj() As DataRow
            For Each r As DataRow In dt.Rows
                drAdj = dtAdj.Select("AdjType_Id='" & r(1) & "'")
                If drAdj IsNot Nothing Then
                    If drAdj.Length > 0 Then
                        For Each drFound As DataRow In drAdj
                            ''12-Mar-2014  TASK:2487  Imran Ali Opening Stock Receiving Current Stock Record Not Show 
                            If Not dt.Columns.IndexOf(drFound(1)) = -1 Then
                                r.BeginEdit()
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r.EndEdit()
                            End If
                            'End Task:2487
                        Next
                    End If
                End If
            Next

            dt.Columns.Add("Total", GetType(System.Double))
            dt.Columns.Add("Amount", GetType(System.Double))
            Dim strTotal As String = String.Empty
            Dim strTotalAmount As String = String.Empty

            For c As Integer = enmCustomers.Count To dt.Columns.Count - 3 Step 3
                If strTotal.Length > 0 Then
                    strTotal = strTotal & "+" & "[" & dt.Columns(c + 1).ColumnName & "]"
                Else
                    strTotal = "[" & dt.Columns(c + 1).ColumnName & "]"
                End If
            Next

            For c As Integer = enmCustomers.Count To dt.Columns.Count - 3 Step 3
                If strTotalAmount.Length > 0 Then
                    strTotalAmount = strTotalAmount & "+" & "[" & dt.Columns(c + 1).ColumnName & "]" & "*" & "[" & dt.Columns(c + 2).ColumnName & "]"
                Else
                    strTotalAmount = "[" & dt.Columns(c + 1).ColumnName & "]" & "*" & "[" & dt.Columns(c + 2).ColumnName & "]"
                End If
            Next
            dt.Columns("Total").Expression = strTotal.ToString
            dt.Columns("Amount").Expression = strTotalAmount.ToString

            dt.TableName = "DispatchDetail"
            Dim dv As New DataView
            dv.Table = dt
            'R:M-1-------------- Filter Customer Which have no transaction --------
            dv.RowFilter = "Total <> 0"
            '-------------- R:M-1 End ----------------------------------------
            Me.grd.DataSource = dv.ToTable
            Me.grd.RetrieveStructure()
            ApplyGridSetting()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).Caption = "Account Code"
            Me.grd.RootTable.Columns(2).Caption = "Account Description"
            For c As Integer = enmCustomers.Count To Me.grd.RootTable.Columns.Count - 3 Step 3
                Me.grd.RootTable.Columns(c).Visible = False
                Me.grd.RootTable.Columns(c + 2).Visible = False
                Me.grd.RootTable.Columns(c + 1).Caption = Microsoft.VisualBasic.Left(Me.grd.RootTable.Columns(c + 1).Caption, Me.grd.RootTable.Columns(c + 1).Caption.LastIndexOf("^") - 1 + 1)
                Me.grd.RootTable.Columns(c + 1).AllowSort = False
                'Me.grd.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Next
            Me.grd.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.FrozenColumns = enmCustomers.Count
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub grd_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.LoadingRow
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            If e.Row.Cells(0).Value = "0A" Then
                Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowStyle.BackColor = Color.LightCyan
                e.Row.RowStyle = rowStyle
            ElseIf e.Row.Cells(0).Value = "0B" Then
                Dim rowStyle1 As New Janus.Windows.GridEX.GridEXFormatStyle
                rowStyle1.BackColor = Color.LightCyan
                e.Row.RowStyle = rowStyle1
            ElseIf e.Row.Cells(0).Value = "0C" Then
                Dim rowStyle2 As New Janus.Windows.GridEX.GridEXFormatStyle
                rowStyle2.BackColor = Color.LightCyan
                e.Row.RowStyle = rowStyle2
            ElseIf e.Row.Cells(0).Value = "0D" Then
                Dim rowStyle3 As New Janus.Windows.GridEX.GridEXFormatStyle
                rowStyle3.BackColor = Color.Ivory
                e.Row.RowStyle = rowStyle3
            ElseIf e.Row.Cells(0).Value = "0E" Then
                Dim rowStyle4 As New Janus.Windows.GridEX.GridEXFormatStyle
                rowStyle4.BackColor = Color.Honeydew
                e.Row.RowStyle = rowStyle4
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
    Private Sub frmGrdDispatchDetail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Back Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            'Task: 2627 Junaid Call CtrlGrdBarValue method
            CtrlGrdBarValue()
            'End Task 2627
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

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbCostCenter.SelectedValue
            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name From tblDefCostCenter")
            Me.cmbCostCenter.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub btnItemName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemName.CheckedChanged, btnItemCode.CheckedChanged
    '    Try
    '        Dim rd As RadioButton = CType(sender, RadioButton)

    '        Select Case rd.Name
    '            Case Me.btnItemName.Name

    '        End Select

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbCostCenter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCostCenter.SelectedIndexChanged
        Try
            If IsOpendForm = False Then Exit Sub
            Dim str As String = "Select ISNULL(DayShift,0) as DayShift From tblDefCostCenter WHERE CostCenterId=" & Me.cmbCostCenter.SelectedValue
            Dim dt As New DataTable
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) = True Then
                        flgShift = True
                    Else
                        flgShift = False
                    End If
                Else
                    flgShift = False
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    'Task:2627 Junaid
    Private Sub CtrlGrdBarValue()
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Dispatch Detail" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            CtrlGrdBarValue()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    'End task:2627
End Class