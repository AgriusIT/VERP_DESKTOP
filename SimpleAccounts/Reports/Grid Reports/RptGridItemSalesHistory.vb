''30-Dec-2013 R:982  Imran Ali         Add Column of Tax Exclusive And Inclusive Value in Item Detail History
''22-Aug-2014 Task:2797 Imran Ali Code Wise Search Item In Item Detail History Report
''16-6-2015 TASKM166152 Imran Ali Add Total Weight Column
'TASK TFS1575 Ayesha Rehman on 19-10-2017. Account name required in store issuance report
'TASK TFS1824 Ayesha Rehman on 23-11-2017. Changes in reports,Item detail history
Imports SBDal
Imports SBUtility.Utility
Imports Janus.Windows.GridEX
Public Class RptGridItemSalesHistory
    Dim flgCompanyRights As Boolean = False
    Sub fillCombo(Optional ByVal strCondition As String = "", Optional ByVal ItemFilter As String = "")
        Dim strsql As String = String.Empty
        If strCondition = "Items" Then
            'Before against task:2797
            'strsql = " SELECT     TOP 100 PERCENT dbo.ArticleDefTable.ArticleId, " & _
            '    " dbo.ArticleDefTable.ArticleDescription + '------' + + y.ArticleColorName + '-' + b.ArticleSizeName + '-' + a.ArticleUnitName AS ArticleDescription, " & _
            '       " y.ArticleColorName, b.ArticleSizeName, a.ArticleUnitName " & _
            '         " FROM dbo.ArticleDefTable INNER JOIN " & _
            '          "   dbo.ArticleUnitDefTable a ON dbo.ArticleDefTable.ArticleUnitId = a.ArticleUnitId INNER JOIN " & _
            '            " dbo.ArticleSizeDefTable b ON dbo.ArticleDefTable.SizeRangeId = b.ArticleSizeId INNER JOIN " & _
            '              " dbo.ArticleColorDefTable y ON y.ArticleColorId = dbo.ArticleDefTable.ArticleColorId WHERE ArticleDefTable.ArticleDescription LIKE '" & ItemFilter & "%'" & IIf(flgCompanyRights = True, " AND ArticleGroupId in (Select ArticleGroupId From ArticleGroupDefTable  WHERE SubSubID In (Select  main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))", "") & _
            '                " ORDER BY dbo.ArticleDefTable.ArticleDescription"
            'Task:2797 Set Filter ArticleCode And ArticleDescription
            strsql = " SELECT TOP 100 PERCENT dbo.ArticleDefTable.ArticleId, " & _
                " dbo.ArticleDefTable.ArticleDescription + ' ~ ' + dbo.ArticleDefTable.ArticleCode + '------' + + y.ArticleColorName + '-' + b.ArticleSizeName + '-' + a.ArticleUnitName AS ArticleDescription, " & _
                   " y.ArticleColorName, b.ArticleSizeName, a.ArticleUnitName " & _
                     " FROM dbo.ArticleDefTable INNER JOIN " & _
                      "   dbo.ArticleUnitDefTable a ON dbo.ArticleDefTable.ArticleUnitId = a.ArticleUnitId INNER JOIN " & _
                        " dbo.ArticleSizeDefTable b ON dbo.ArticleDefTable.SizeRangeId = b.ArticleSizeId INNER JOIN " & _
                          " dbo.ArticleColorDefTable y ON y.ArticleColorId = dbo.ArticleDefTable.ArticleColorId WHERE " & IIf(Me.rbtByName.Checked = True, " ArticleDefTable.ArticleDescription ", " ArticleDefTable.ArticleCode") & " LIKE '" & ItemFilter & "%'" & IIf(flgCompanyRights = True, " AND ArticleGroupId in (Select ArticleGroupId From ArticleGroupDefTable  WHERE SubSubID In (Select  main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))", "") & _
                            " ORDER BY dbo.ArticleDefTable.ArticleDescription"
            'End Task:2797
            Me.lstItems.ListItem.ValueMember = "ArticleID"
            Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
            Me.lstItems.ListItem.DataSource = UtilityDAL.GetDataTable(strsql)
            Me.lstItems.DeSelect()
        End If
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSaveLayout.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True

                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.RptGridItemSalesHistory)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSaveLayout.Text = "Save Group Layout" Then
                            Me.btnSaveLayout.Enabled = dt.Rows(0).Item("Save Group Layout_Rights").ToString()
                        End If
                    End If
                End If
            Else
                Me.Visible = False
                Me.btnSaveLayout.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save Group Layout" Then
                        If Me.btnSaveLayout.Text = "&Save Group Layout" Then btnSaveLayout.Enabled = True
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
    Private Sub RptGridItemSalesHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub RptGridItemSalesHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not GetConfigValue("CompanyRights").ToString = "Error" Then
            flgCompanyRights = GetConfigValue("CompanyRights")
        End If

        Me.cmbPeriod.Text = "Current Month"
        fillCombo("Items")
        Me.RBtnSales.Checked = True
        LoadLayout()
        GetSecurityRights()

    End Sub
    Sub Selected_Items(Optional ByVal Condition As String = "")
        Try

            Dim Dt As New DataTable
            Dim Da As OleDb.OleDbDataAdapter
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim strSQL As String = String.Empty

            'If Me.RBtnSales.Checked = True Then
            '    strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], SalesMasterTable.SalesDate, SalesMasterTable.SalesNo, tblDefLocation.Location_Name, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, " & _
            '                 " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " & _
            '                 " SalesDetailTable.Price, SalesDetailTable.Qty, ISNULL(SalesDetailTable.SampleQty, 0) AS SampleQty, SalesDetailTable.BatchNo, " & _
            '                 " SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0)  " & _
            '                 " * ISNULL(SalesDetailTable.Price, 0)) AS Amount, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM((ISNULL(SalesDetailTable.Qty, 0) " & _
            '                 " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))  " & _
            '                 " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS SalesTax, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)  " & _
            '                 " - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0) + (ISNULL(SalesDetailTable.Qty, 0)  " & _
            '                 " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
            '                 " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS NetAmount " & _
            '                 " FROM ArticleSizeDefTable RIGHT OUTER JOIN " & _
            '                 " vwCOADetail INNER JOIN " & _
            '                 " SalesMasterTable INNER JOIN " & _
            '                 " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN " & _
            '                 " ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId ON  " & _
            '                 " vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON " & _
            '                 " ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " & _
            '                 " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId " & _
            '                " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" & _
            '    " GROUP BY SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription,  " & _
            '    " SalesDetailTable.Qty, SalesDetailTable.Price, SalesDetailTable.BatchNo,   " & _
            '    " ISNULL(SalesDetailTable.TaxPercent, 0), ISNULL(SalesDetailTable.SampleQty, 0),  " & _
            '    " ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, vwCOADetail.detail_title, tblDefLocation.Location_Name ORDER BY ArticleDefTable.ArticleDescription "
            'End If
            'If Me.RBtnSalesReturn.Checked = True Then
            '    strSQL = "SELECT vwCOADetail.detail_title AS [Account Description], SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo, " _
            '           & " tblDefLocation.Location_Name, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
            '           & " SalesReturnDetailTable.Price AS Price, SUM(SalesReturnDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
            '           & " SalesReturnDetailTable.BatchNo, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0))" _
            '           & " AS Amount, 0 AS GST, 0 AS SalesTax, ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0) " _
            '           & " AS NetAmount " _
            '           & " FROM SalesReturnMasterTable INNER JOIN " _
            '           & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId INNER JOIN " _
            '           & " ArticleDefTable ON SalesReturnDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
            '           & " vwCOADetail ON SalesReturnMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            '           & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN " _
            '           & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesReturnDetailTable.LocationId " _
            '           & " WHERE  (Convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesReturnDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
            '           & " GROUP BY vwCOADetail.detail_title, SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo,  " _
            '           & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
            '           & " SalesReturnDetailTable.Price, SalesReturnDetailTable.Qty, SalesReturnDetailTable.BatchNo, tblDefLocation.Location_Name "
            'End If
            'If Me.RBtnPurchase.Checked = True Then
            '    strSQL = "SELECT     vwCOADetail.detail_title AS [Account Description], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, " _
            '          & " tblDefLocation.Location_Name, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
            '          & " ReceivingDetailTable.Price AS Price, SUM(ReceivingDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
            '          & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
            '          & " AS Amount, 0 AS GST, 0 AS SalesTax, ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0) " _
            '          & " AS NetAmount " _
            '          & " FROM ReceivingMasterTable INNER JOIN " _
            '          & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
            '          & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
            '          & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            '          & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
            '          & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
            '          & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND LEFT(ReceivingMasterTable.ReceivingNo,2) <> 'SR'  AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
            '          & " GROUP BY vwCOADetail.detail_title, ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo,  " _
            '          & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
            '          & " ReceivingDetailTable.Price, ReceivingDetailTable.Qty, ReceivingDetailTable.BatchNo, tblDefLocation.Location_Name  "
            'End If
            'If Me.rdoStockReceiving.Checked = True Then
            '    strSQL = "SELECT ReceivingMasterTable.Remarks AS [Account Description], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, " _
            '          & " tblDefLocation.Location_Name, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
            '          & " ReceivingDetailTable.Price AS Price, SUM(ReceivingDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
            '          & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
            '          & " AS Amount, 0 AS GST, 0 AS SalesTax, ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0) " _
            '          & " AS NetAmount " _
            '          & " FROM ReceivingMasterTable INNER JOIN " _
            '          & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
            '          & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
            '          & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            '          & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
            '          & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
            '          & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")  AND LEFT(ReceivingMasterTable.ReceivingNo,2)='SR' AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
            '          & " GROUP BY ReceivingMasterTable.Remarks, ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo,  " _
            '          & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
            '          & " ReceivingDetailTable.Price, ReceivingDetailTable.BatchNo, " _
            '          & " ReceivingDetailTable.Qty, tblDefLocation.Location_Name  "
            'End If
            'If Me.RadioButton1.Checked = True Then
            '    strSQL = "SELECT dbo.vwCOADetail.detail_title AS [Account Description], dbo.PurchaseReturnMasterTable.PurchaseReturnDate, dbo.PurchaseReturnMasterTable.PurchaseReturnNo, tblDefLocation.Location_Name, " _
            '          & " ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
            '          & " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.PurchaseReturnDetailTable.Price AS Price,  " _
            '          & " dbo.PurchaseReturnDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.PurchaseReturnDetailTable.BatchNo, 0 AS Amount, 0 AS GST, 0 AS SalesTax,  " _
            '          & " ISNULL(dbo.PurchaseReturnDetailTable.Price, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0) AS NetAmount " _
            '          & " FROM dbo.PurchaseReturnMasterTable INNER JOIN " _
            '          & " dbo.PurchaseReturnDetailTable ON  " _
            '          & " dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId INNER JOIN " _
            '          & " dbo.ArticleDefTable ON dbo.PurchaseReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId INNER JOIN " _
            '          & " dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
            '          & " dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN " _
            '          & " dbo.vwCOADetail ON dbo.PurchaseReturnMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = PurchaseReturnDetailTable.LocationId " _
            '          & " WHERE  (Convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND PurchaseReturnDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
            'End If
            'If Me.rdoDispatch.Checked = True Then
            '    strSQL = "SELECT dbo.DispatchMasterTable.Remarks AS [Account Description], dbo.DispatchMasterTable.DispatchDate,  dbo.DispatchMasterTable.DispatchNo, tblDefLocation.Location_Name, " _
            '     & "     ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription,ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
            '     & "     dbo.DispatchDetailTable.Price AS Price, dbo.DispatchDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, 0 AS Amount, " _
            '     & "     0 AS GST, 0 AS SalesTax, (Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount  " _
            '     & "     FROM dbo.vwCOADetail RIGHT OUTER JOIN " _
            '     & "     dbo.ArticleDefTable INNER JOIN " _
            '     & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
            '     & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN " _
            '     & "     dbo.DispatchDetailTable INNER JOIN " _
            '     & "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
            '     & "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.VendorId  INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
            '     & "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'GP' OR LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'DN') " _
            '     & "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
            'End If
            'If Me.rdoStoreIssueance.Checked = True Then
            '    strSQL = "SELECT dbo.DispatchMasterTable.Remarks AS [Account Description], dbo.DispatchMasterTable.DispatchDate, dbo.DispatchMasterTable.DispatchNo,  tblDefLocation.Location_Name, " _
            '     & "     ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
            '     & "     dbo.DispatchDetailTable.Price AS Price, dbo.DispatchDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, 0 AS Amount, " _
            '     & "     0 AS GST, 0 AS SalesTax,(Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount   " _
            '     & "     FROM dbo.vwCOADetail RIGHT OUTER JOIN " _
            '     & "     dbo.ArticleDefTable INNER JOIN " _
            '     & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId INNER JOIN " _
            '     & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN " _
            '     & "     dbo.DispatchDetailTable INNER JOIN " _
            '     & "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
            '     & "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.VendorId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
            '     & "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 1) = 'I') " _
            '     & "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
            'End If
            'If Me.rdoProduction.Checked = True Then
            '    strSQL = "SELECT dbo.ProductionMasterTable.Remarks AS [Account Description], dbo.ProductionMasterTable.Production_date, dbo.ProductionMasterTable.Production_no, " _
            '     & "     tblDefLocation.Location_Name, ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
            '     & "     ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.ProductionDetailTable.CurrentRate AS Price,  " _
            '     & "     dbo.ProductionDetailTable.Qty AS Qty, 0 AS SampleQty, '' AS BatchNo, 0 AS Amount, 0 AS GST, 0 AS SalesTax, (Isnull(ProductionDetailTable.Qty,0) * Isnull(ProductionDetailTable.CurrentRate,0)) as NetAmount  " _
            '     & "     FROM dbo.ArticleDefTable INNER JOIN " _
            '     & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
            '     & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN " _
            '     & "     dbo.ProductionDetailTable INNER JOIN " _
            '     & "     dbo.ProductionMasterTable ON dbo.ProductionDetailTable.Production_ID = dbo.ProductionMasterTable.Production_ID ON  " _
            '     & "     dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticledefID INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ProductionDetailTable.Location_Id " _
            '     & "     WHERE (Convert(varchar, ProductionMasterTable.Production_Date,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND ProductionDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefTable.ArticleGroupId in (Select ArticleGroupId From ArticleGroupDefTable  WHERE SubSubID In (Select  main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))", "") & ""
            'End If
            If Me.RBtnSales.Checked = True Then
                'TaskM166152 Before
                'strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], SalesMasterTable.SalesDate, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, " & _
                '             " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " & _
                '             " SalesDetailTable.Price, SalesDetailTable.Qty, ISNULL(SalesDetailTable.SampleQty, 0) AS SampleQty, SalesDetailTable.BatchNo, " & _
                '             " SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0)) AS Amount, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM((ISNULL(SalesDetailTable.Qty, 0) " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))  " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS SalesTax, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)  " & _
                '             " - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0) + (ISNULL(SalesDetailTable.Qty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS NetAmount " & _
                '             " FROM ArticleSizeDefTable RIGHT OUTER JOIN " & _
                '             " vwCOADetail INNER JOIN " & _
                '             " SalesMasterTable INNER JOIN " & _
                '             " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN " & _
                '             " ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId ON  " & _
                '             " vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON " & _
                '             " ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " & _
                '             " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId  " & _
                '            " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" & _
                '" GROUP BY SalesMasterTable.SalesNo, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, SalesMasterTable.SalesDate, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription,  " & _
                '" SalesDetailTable.Qty, SalesDetailTable.Price, SalesDetailTable.BatchNo,   " & _
                '" ISNULL(SalesDetailTable.TaxPercent, 0), ISNULL(SalesDetailTable.SampleQty, 0),  " & _
                '" ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, vwCOADetail.detail_title, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName ORDER BY ArticleDefTable.ArticleDescription "
                '  strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesMasterTable.SalesDate, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, " & _
                '             " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " & _
                '             " SalesDetailTable.Price, SalesDetailTable.Qty, ISNULL(SalesDetailTable.SampleQty, 0) AS SampleQty, SalesDetailTable.BatchNo, " & _
                '             " SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0)) AS Amount, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM((ISNULL(SalesDetailTable.Qty, 0) " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))  " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS SalesTax, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)  " & _
                '             " - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0) + (ISNULL(SalesDetailTable.Qty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS NetAmount " & _
                '             " FROM ArticleSizeDefTable RIGHT OUTER JOIN " & _
                '             " vwCOADetail INNER JOIN " & _
                '             " SalesMasterTable INNER JOIN " & _
                '             " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN " & _
                '             " ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId ON  " & _
                '             " vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON " & _
                '             " ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " & _
                '             " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director  " & _
                '            " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" & _
                '" GROUP BY Emp.Employee_Name, Dcr.Employee_Name,SalesMasterTable.SalesNo, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, SalesMasterTable.SalesDate, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription,  " & _
                '" SalesDetailTable.Qty, SalesDetailTable.Price, SalesDetailTable.BatchNo,   " & _
                '" ISNULL(SalesDetailTable.TaxPercent, 0), ISNULL(SalesDetailTable.SampleQty, 0),  " & _
                '" ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, vwCOADetail.detail_title, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName ORDER BY ArticleDefTable.ArticleDescription "


                'TAKSM166152 Add Total Weight Column
                '   strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesMasterTable.SalesDate, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, " & _
                '             " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " & _
                '             " SalesDetailTable.Price, SalesDetailTable.Qty, ISNULL(SalesDetailTable.SampleQty, 0) AS SampleQty, SalesDetailTable.BatchNo, " & _
                '             " SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0)) AS Amount, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM((ISNULL(SalesDetailTable.Qty, 0) " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))  " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS SalesTax, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)  " & _
                '             " - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0) + (ISNULL(SalesDetailTable.Qty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(SalesDetailTable.Qty,0)) as [Total Weight] " & _
                '             " FROM ArticleSizeDefTable RIGHT OUTER JOIN " & _
                '             " vwCOADetail INNER JOIN " & _
                '             " SalesMasterTable INNER JOIN " & _
                '             " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN " & _
                '             " ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId ON  " & _
                '             " vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON " & _
                '             " ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " & _
                '             " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director  " & _
                '            " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" & _
                '" GROUP BY Emp.Employee_Name, Dcr.Employee_Name,SalesMasterTable.SalesNo, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, SalesMasterTable.SalesDate, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription,  " & _
                '" SalesDetailTable.Qty, SalesDetailTable.Price, SalesDetailTable.BatchNo,   " & _
                '" ISNULL(SalesDetailTable.TaxPercent, 0), ISNULL(SalesDetailTable.SampleQty, 0),  " & _
                '" ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, vwCOADetail.detail_title, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName ORDER BY ArticleDefTable.ArticleDescription "
                'End TaskM166152 

                '     strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], vwCOADetail.NTN_NO, Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesMasterTable.SalesDate, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, " & _
                '             " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " & _
                '             " SalesDetailTable.Price, SalesDetailTable.Qty, ISNULL(SalesDetailTable.SampleQty, 0) AS SampleQty, SalesDetailTable.BatchNo, " & _
                '             " SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0)) AS Amount, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM((ISNULL(SalesDetailTable.Qty, 0) " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))  " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS SalesTax, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)  " & _
                '             " - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0) + (ISNULL(SalesDetailTable.Qty, 0)  " & _
                '             " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
                '             " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(SalesDetailTable.Qty,0)) as [Total Weight] " & _
                '             " FROM ArticleSizeDefTable RIGHT OUTER JOIN " & _
                '             " vwCOADetail INNER JOIN " & _
                '             " SalesMasterTable INNER JOIN " & _
                '             " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN " & _
                '             " ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId ON  " & _
                '             " vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON " & _
                '             " ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " & _
                '             " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director  " & _
                '            " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" & _
                '" GROUP BY Emp.Employee_Name, Dcr.Employee_Name,SalesMasterTable.SalesNo, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, SalesMasterTable.SalesDate, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription,  " & _
                '" SalesDetailTable.Qty, SalesDetailTable.Price, SalesDetailTable.BatchNo,   " & _
                '" ISNULL(SalesDetailTable.TaxPercent, 0), ISNULL(SalesDetailTable.SampleQty, 0),  " & _
                '" ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, vwCOADetail.NTN_NO, ArticleUnitDefTable.ArticleUnitName, vwCOADetail.detail_title, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName ORDER BY ArticleDefTable.ArticleDescription "



                'strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], vwCOADetail.NTN_NO, vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area, Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesMasterTable.SalesDate, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, " & _
                '" ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " & _
                '" SalesDetailTable.Price, SalesDetailTable.Qty, ISNULL(SalesDetailTable.SampleQty, 0) AS SampleQty, SalesDetailTable.BatchNo, " & _
                '" SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0)  " & _
                '" * ISNULL(SalesDetailTable.Price, 0)) AS Amount, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM((ISNULL(SalesDetailTable.Qty, 0) " & _
                '" * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))  " & _
                '" * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS SalesTax," & _
                '" IsNull(dbo.SalesDetailTable.SEDPercent,0) AS [Ad Tax %], SUM((ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) * ISNULL(SalesDetailTable.SEDPercent, 0) / 100)  AS [Ad Tax Amount], " & _
                '" SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)  " & _
                '" - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0) + (ISNULL(SalesDetailTable.Qty, 0)  " & _
                '" * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
                '" * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(SalesDetailTable.Qty,0)) as [Total Weight] " & _
                '" FROM ArticleSizeDefTable RIGHT OUTER JOIN " & _
                '" vwCOADetail INNER JOIN " & _
                '" SalesMasterTable INNER JOIN " & _
                '" SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN " & _
                '" ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId ON  " & _
                '" vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON " & _
                '" ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " & _
                '" ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director  " & _
                '" WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) AND SalesDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" & _
                '" GROUP BY Emp.Employee_Name, Dcr.Employee_Name,SalesMasterTable.SalesNo, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, SalesMasterTable.SalesDate, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription,  " & _
                '" SalesDetailTable.Qty, SalesDetailTable.Price,IsNull(dbo.SalesDetailTable.SEDPercent,0) , SalesDetailTable.BatchNo,   " & _
                '" ISNULL(SalesDetailTable.TaxPercent, 0), ISNULL(SalesDetailTable.SampleQty, 0),  " & _
                '" ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, vwCOADetail.NTN_NO, ArticleUnitDefTable.ArticleUnitName, vwCOADetail.detail_title, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,SalesDetailTable.SaleDetailId, vwCOADetail.CityName, vwCOADetail.TerritoryName ORDER BY ArticleDefTable.ArticleDescription "

                'Ali Faisal : TFS# 925 Start : Inserted a column of Party Mobile No from SalesMasterTable on 13-June-2017
                ''TASK TFS4542 Addtion of three new columns Pack Size, Pack Qty and Total Qty and Qty is replaced with Total Qty. Done on 17-09-2018
                strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], vwCOADetail.NTN_NO, vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area, Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesMasterTable.SalesDate, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, tblDefLocation.Location_Name,SalesMasterTable.Party_Mobile_No As SalesMobileNo, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, " & _
                " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " & _
                " SalesDetailTable.Price, SalesDetailTable.ArticleSize AS [Pack Size], ISNULL(SalesDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(SalesDetailTable.Sz1, 0) AS Qty, SalesDetailTable.Qty AS [Total Qty], ISNULL(SalesDetailTable.SampleQty, 0) AS SampleQty, SalesDetailTable.BatchNo, " & _
                " SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0)  " & _
                " * ISNULL(SalesDetailTable.Price, 0)) AS Amount, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM((ISNULL(SalesDetailTable.Qty, 0) " & _
                " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))  " & _
                " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS SalesTax," & _
                " IsNull(dbo.SalesDetailTable.SEDPercent,0) AS [Ad Tax %], SUM((ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) * ISNULL(SalesDetailTable.SEDPercent, 0) / 100)  AS [Ad Tax Amount], " & _
                " SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)  " & _
                " - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0) + (ISNULL(SalesDetailTable.Qty, 0)  " & _
                " * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
                " * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(SalesDetailTable.Qty,0)) as [Total Weight] " & _
                " FROM ArticleSizeDefTable RIGHT OUTER JOIN " & _
                " vwCOADetail INNER JOIN " & _
                " SalesMasterTable INNER JOIN " & _
                " SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN " & _
                " ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId ON  " & _
                " vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON " & _
                " ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " & _
                " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director  " & _
                " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) AND SalesDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" & _
                " GROUP BY Emp.Employee_Name, Dcr.Employee_Name,SalesMasterTable.SalesNo, SalesMasterTable.SalesNo,SalesMasterTable.DcNo, SalesMasterTable.SalesDate, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription,  " & _
                " SalesDetailTable.Qty, SalesDetailTable.Price,IsNull(dbo.SalesDetailTable.SEDPercent,0) , SalesDetailTable.BatchNo, SalesDetailTable.ArticleSize, ISNULL(SalesDetailTable.Sz7, 0), ISNULL(SalesDetailTable.Sz1, 0), " & _
                " ISNULL(SalesDetailTable.TaxPercent, 0), ISNULL(SalesDetailTable.SampleQty, 0),  " & _
                " ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, vwCOADetail.NTN_NO, ArticleUnitDefTable.ArticleUnitName, vwCOADetail.detail_title, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,SalesDetailTable.SaleDetailId, vwCOADetail.CityName, vwCOADetail.TerritoryName,SalesMasterTable.Party_Mobile_No ORDER BY ArticleDefTable.ArticleDescription "

                'Ali Faisal : TFS# 925 End
            End If
            If Me.RBtnSalesReturn.Checked = True Then
                'strSQL = "SELECT vwCOADetail.detail_title AS [Account Description], Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo, '' as DcNo,  " _
                '       & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type],ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                '       & " SalesReturnDetailTable.Price AS Price, SUM(SalesReturnDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
                '       & " SalesReturnDetailTable.BatchNo, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0))" _
                '       & " AS Amount, IsNull(Tax_Percent,0) AS GST, SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0))+SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                '       & " AS NetAmount " _
                '       & " FROM SalesReturnMasterTable INNER JOIN " _
                '       & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId INNER JOIN " _
                '       & " ArticleDefTable ON SalesReturnDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                '       & " vwCOADetail ON SalesReturnMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '       & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN " _
                '       & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesReturnDetailTable.LocationId  LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director " _
                '       & " WHERE  (Convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesReturnDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                '       & " GROUP BY Emp.Employee_Name, Dcr.Employee_Name, vwCOADetail.detail_title, SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo,  " _
                '       & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                '       & " SalesReturnDetailTable.Price, SalesReturnDetailTable.Qty, SalesReturnDetailTable.BatchNo, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName, IsNull(Tax_Percent,0)"

                ''TAKSM166152 Add Total Weight Column
                'strSQL = "SELECT vwCOADetail.detail_title AS [Account Description], Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo, '' as DcNo,  " _
                '      & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type],ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                '      & " SalesReturnDetailTable.Price AS Price, SUM(SalesReturnDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
                '      & " SalesReturnDetailTable.BatchNo, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0))" _
                '      & " AS Amount, IsNull(Tax_Percent,0) AS GST, SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0))+SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                '      & " AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0) * IsNull(SalesReturnDetailTable.Qty,0)) as [Total Weight] " _
                '      & " FROM SalesReturnMasterTable INNER JOIN " _
                '      & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId INNER JOIN " _
                '      & " ArticleDefTable ON SalesReturnDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                '      & " vwCOADetail ON SalesReturnMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '      & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN " _
                '      & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesReturnDetailTable.LocationId  LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director " _
                '      & " WHERE  (Convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesReturnDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                '      & " GROUP BY Emp.Employee_Name, Dcr.Employee_Name, vwCOADetail.detail_title, SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo,  " _
                '      & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                '      & " SalesReturnDetailTable.Price, SalesReturnDetailTable.Qty, SalesReturnDetailTable.BatchNo, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName, IsNull(Tax_Percent,0)"
                'End TaskM166152

                'TAKSM166152 Add Total Weight Column
                strSQL = "SELECT vwCOADetail.detail_title AS [Account Description], vwCOADetail.NTN_NO,  vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area,Emp.Employee_Name as [Sale Person], Dcr.Employee_Name as [Director], SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo, '' as DcNo,  " _
                      & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type],ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                      & " SalesReturnDetailTable.Price AS Price,  SalesReturnDetailTable.ArticleSize AS [Pack Size], ISNULL(SalesReturnDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(SalesReturnDetailTable.Sz1, 0) AS Qty, SalesReturnDetailTable.Qty AS [Total Qty], 0 AS SampleQty,  " _
                      & " SalesReturnDetailTable.BatchNo, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0))" _
                      & " AS Amount, IsNull(Tax_Percent,0) AS GST, SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, 0 AS [Ad Tax %], 0 AS [Ad Tax Amount], SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0))+SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                      & " AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0) * IsNull(SalesReturnDetailTable.Qty,0)) as [Total Weight] " _
                      & " FROM SalesReturnMasterTable INNER JOIN " _
                      & " SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId INNER JOIN " _
                      & " ArticleDefTable ON SalesReturnDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                      & " vwCOADetail ON SalesReturnMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                      & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN " _
                      & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = SalesReturnDetailTable.LocationId  LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN tblDefEmployee Dcr on Dcr.Employee_Id = vwCOADetail.Director " _
                      & " WHERE  (Convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND SalesReturnDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                      & " GROUP BY Emp.Employee_Name, Dcr.Employee_Name, vwCOADetail.detail_title, SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo,  " _
                      & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                      & " SalesReturnDetailTable.Price,SalesReturnDetailTable.SalesReturnDetailId, SalesReturnDetailTable.Qty, vwCOADetail.NTN_NO,SalesReturnDetailTable.BatchNo, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName, IsNull(Tax_Percent,0) , vwCOADetail.CityName, vwCOADetail.TerritoryName, SalesReturnDetailTable.ArticleSize, ISNULL(SalesReturnDetailTable.Sz7, 0), ISNULL(SalesReturnDetailTable.Sz1, 0)"

            End If
            If Me.RBtnPurchase.Checked = True Then
                'strSQL = "SELECT     vwCOADetail.detail_title AS [Account Description],'' as [Sale Person], '' as [Director], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No as [Invoice No], " _
                '      & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type],ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                '      & " ReceivingDetailTable.Price AS Price, SUM(ReceivingDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
                '      & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
                '      & " AS Amount, IsNull(TaxPercent,0) AS GST, SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))+SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                '      & " AS NetAmount " _
                '      & " FROM ReceivingMasterTable INNER JOIN " _
                '      & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
                '      & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                '      & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '      & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                '      & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
                '      & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND LEFT(ReceivingMasterTable.ReceivingNo,2) <> 'SR'  AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                '      & " GROUP BY vwCOADetail.detail_title, ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, " _
                '      & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                '      & " ReceivingDetailTable.Price, ReceivingDetailTable.Qty, ReceivingDetailTable.BatchNo, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,IsNull(TaxPercent,0) "

                'TAKSM166152 Add Total Weight Column
                'strSQL = "SELECT     vwCOADetail.detail_title AS [Account Description],'' as [Sale Person], '' as [Director], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No as [Invoice No], " _
                '    & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type],ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                '    & " ReceivingDetailTable.Price AS Price, SUM(ReceivingDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
                '    & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
                '    & " AS Amount, IsNull(TaxPercent,0) AS GST, SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))+SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                '    & " AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(ReceivingDetailTable.Qty,0)) as [Total Weight] " _
                '    & " FROM ReceivingMasterTable INNER JOIN " _
                '    & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
                '    & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                '    & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '    & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                '    & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
                '    & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND LEFT(ReceivingMasterTable.ReceivingNo,2) <> 'SR'  AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                '    & " GROUP BY vwCOADetail.detail_title, ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, " _
                '    & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                '    & " ReceivingDetailTable.Price, ReceivingDetailTable.Qty, ReceivingDetailTable.BatchNo, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,IsNull(TaxPercent,0) "
                'End TaskM166152

                strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], vwCOADetail.NTN_NO, vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area,'' as [Sale Person], '' as [Director], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No as [Invoice No], " _
                 & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type],ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                 & " ReceivingDetailTable.Price AS Price,   ReceivingDetailTable.ArticleSize AS [Pack Size], ISNULL(ReceivingDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(ReceivingDetailTable.Sz1, 0) AS Qty, ReceivingDetailTable.Qty AS [Total Qty], 0 AS SampleQty,  " _
                 & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
                 & " AS Amount, IsNull(TaxPercent,0) AS GST, SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, IsNull(AdTax_Percent,0) AS [Ad Tax %], SUM((IsNull(AdTax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS [Ad Tax Amount], SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))+SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) + SUM((IsNull(AdTax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                 & " AS NetAmount, SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(ReceivingDetailTable.Qty,0)) as [Total Weight] " _
                 & " FROM ReceivingMasterTable INNER JOIN " _
                 & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
                 & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                 & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                 & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                 & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
                 & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ") AND LEFT(ReceivingMasterTable.ReceivingNo,2) <> 'SR'  AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                 & " GROUP BY vwCOADetail.detail_title,IsNull(AdTax_Percent,0), ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, " _
                 & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                 & " ReceivingDetailTable.Price, ReceivingDetailTable.ReceivingDetailId, ReceivingDetailTable.Qty, vwCOADetail.NTN_NO,ReceivingDetailTable.BatchNo, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,IsNull(TaxPercent,0) , vwCOADetail.CityName, vwCOADetail.TerritoryName, ReceivingDetailTable.ArticleSize, ISNULL(ReceivingDetailTable.Sz7, 0), ISNULL(ReceivingDetailTable.Sz1, 0)"

            End If
            ''TFS1824 : Ayesha Rehman : Adding Record To grid For Imports
            If Me.rbtnImports.Checked = True Then
                '    strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], vwCOADetail.NTN_NO, vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area,'' as [Sale Person], '' as [Director], LCMasterTable.LCDate, LCMasterTable.LCNo, LCMasterTable.UserName as [User Name]," _
                '          & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type],ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size, ArticleUnitDefTable.ArticleUnitName As Unit, " _
                '          & " LCDetailTable.Price AS Price, SUM(LCDetailTable.Qty) AS Qty, 0 AS SampleQty, '' AS BatchNo , " _
                '          & " LCDetailTable.Comments, SUM(ISNULL(LCDetailTable.Price, 0) * ISNULL(LCDetailTable.Qty, 0)) AS Amount," _
                '          & " IsNull(LCDetailTable.SaleTaxPercent,0) AS GST, SUM((IsNull(LCDetailTable.SaleTaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, IsNull(LCDetailTable.AddSaleTaxPercent,0) AS [Ad Tax %], SUM((IsNull(LCDetailTable.AddSaleTaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS [Ad Tax Amount], SUM(ISNULL(LCDetailTable.Price, 0) * ISNULL(LCDetailTable.Qty, 0))+SUM((IsNull(LCDetailTable.SaleTaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) + SUM((IsNull(LCDetailTable.AddSaleTaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0)))  AS NetAmount," _
                '          & " SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(LCDetailTable.Qty,0)) as [Total Weight]" _
                '          & " FROM LCMasterTable INNER JOIN " _
                '          & " LCDetailTable ON LCMasterTable.LCID = LCDetailTable.LCId INNER JOIN " _
                '          & " ArticleDefTable ON LCDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                '          & " vwCOADetail ON LCMasterTable.LCAccountId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '          & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                '          & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = LCDetailTable.LocationId " _
                '          & " WHERE  (Convert(varchar, LCMasterTable.LCDate,102) BETWEEN Convert(Datetime,'" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "' , 102)) AND LCMasterTable.Financial_Impact = 1 AND LCDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                '          & " GROUP BY vwCOADetail.detail_title,LCDetailTable.AddSaleTaxPercent,LCDetailTable.SaleTaxPercent,LCMasterTable.UserName,LCDetailTable.Comments,LCMasterTable.LCDate, LCMasterTable.LCNo,ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName," _
                '          & " LCDetailTable.Price, LCDetailTable.LCDetailId, LCDetailTable.Qty, vwCOADetail.NTN_NO,tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,vwCOADetail.CityName, vwCOADetail.TerritoryName"

                strSQL = "SELECT  vwCOADetail.detail_title AS [Account Description], vwCOADetail.NTN_NO, vwCOADetail.CityName as City,vwCOADetail.TerritoryName as Area,'' as [Sale Person], '' as [Director], LCMasterTable.LCDate, LCMasterTable.LCNo,LCMasterTable.UserName as [User Name], " _
                        & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size, ArticleUnitDefTable.ArticleUnitName As Unit, " _
                        & " (LCDetailTable.Other_Charges/SUM(LCDetailTable.Qty)) AS Price, LCDetailTable.ArticleSize AS [Pack Size], ISNULL(LCDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(LCDetailTable.Sz1, 0) AS Qty, LCDetailTable.Qty AS [Total Qty], 0 AS SampleQty,'' AS BatchNo , " _
                        & "  LCDetailTable.Comments, SUM((ISNULL(LCDetailTable.Other_Charges, 0)/ISNULL(LCDetailTable.Qty, 0)) * ISNULL(LCDetailTable.Qty, 0)) AS Amount, " _
                        & "  IsNull(LCDetailTable.SaleTaxPercent,0) AS GST,SUM((IsNull(LCDetailTable.SaleTaxPercent,0)/100)*(IsNull(Qty,0)*(IsNull(Other_Charges,0)/(IsNull(Qty,0))))) AS SalesTax , IsNull(LCDetailTable.AddSaleTaxPercent,0) AS [Ad Tax %], SUM((IsNull(LCDetailTable.AddSaleTaxPercent,0)/100)*(IsNull(Qty,0)*(IsNull(Other_Charges,0)/(IsNull(Qty,0))))) AS [Ad Tax Amount], SUM((ISNULL(LCDetailTable.Other_Charges, 0)/ISNULL(LCDetailTable.Qty, 0)) * ISNULL(LCDetailTable.Qty, 0))+  SUM((IsNull(LCDetailTable.SaleTaxPercent,0)/100)*(IsNull(Qty,0)*(IsNull(Other_Charges,0)/(IsNull(Qty,0))))) + SUM((IsNull(LCDetailTable.AddSaleTaxPercent,0)/100)*(IsNull(Qty,0)*(IsNull(Other_Charges,0)/(IsNull(Qty,0)))))  AS NetAmount, " _
                        & "  SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(LCDetailTable.Qty,0)) as [Total Weight] FROM LCMasterTable INNER JOIN " _
                        & " LCDetailTable ON LCMasterTable.LCID = LCDetailTable.LCId INNER JOIN " _
                        & " ArticleDefTable ON LCDetailTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                        & " vwCOADetail ON LCMasterTable.LCAccountId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                        & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                        & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = LCDetailTable.LocationId " _
                        & " WHERE  (Convert(varchar, LCMasterTable.LCDate,102) BETWEEN Convert(Datetime,'" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "' , 102)) AND LCMasterTable.Financial_Impact = 1 AND LCDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ") AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                        & " GROUP BY vwCOADetail.detail_title,LCDetailTable.AddSaleTaxPercent,LCDetailTable.SaleTaxPercent,LCMasterTable.UserName,LCDetailTable.Comments,LCMasterTable.LCDate, LCMasterTable.LCNo,ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName," _
                        & " LCDetailTable.Price , LCDetailTable.Other_Charges, LCDetailTable.LCDetailId, LCDetailTable.Qty, vwCOADetail.NTN_NO,tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,vwCOADetail.CityName, vwCOADetail.TerritoryName , LCDetailTable.ArticleSize, ISNULL(LCDetailTable.Sz7, 0), ISNULL(LCDetailTable.Sz1, 0) "
            End If
            If Me.rdoStockReceiving.Checked = True Then
                'strSQL = "SELECT ReceivingMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No as [Invoice No], " _
                '      & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                '      & " ReceivingDetailTable.Price AS Price, SUM(ReceivingDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
                '      & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
                '      & " AS Amount, IsNull(TaxPercent,0) AS GST, SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))+SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                '      & " AS NetAmount " _
                '      & " FROM ReceivingMasterTable INNER JOIN " _
                '      & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
                '      & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
                '      & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '      & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                '      & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
                '      & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")  AND LEFT(ReceivingMasterTable.ReceivingNo,2)='SR' AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                '      & " GROUP BY ReceivingMasterTable.Remarks, ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No,  " _
                '      & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                '      & " ReceivingDetailTable.Price, ReceivingDetailTable.BatchNo, " _
                '      & " ReceivingDetailTable.Qty, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,IsNull(TaxPercent,0) "

                'TAKSM166152 Add Total Weight Column
                'strSQL = "SELECT ReceivingMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No as [Invoice No], " _
                '   & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                '   & " ReceivingDetailTable.Price AS Price, SUM(ReceivingDetailTable.Qty) AS Qty, 0 AS SampleQty,  " _
                '   & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
                '   & " AS Amount, IsNull(TaxPercent,0) AS GST, SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))+SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                '   & " AS NetAmount , SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(ReceivingDetailTable.Qty,0)) as [Total Weight] " _
                '   & " FROM ReceivingMasterTable INNER JOIN " _
                '   & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
                '   & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
                '   & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '   & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                '   & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
                '   & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")  AND LEFT(ReceivingMasterTable.ReceivingNo,2)='SR' AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                '   & " GROUP BY ReceivingMasterTable.Remarks, ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No,  " _
                '   & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                '   & " ReceivingDetailTable.Price, ReceivingDetailTable.BatchNo, " _
                '   & " ReceivingDetailTable.Qty, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,IsNull(TaxPercent,0) "
                'End TaskM166152
                'SalesDetailTable.ArticleSize AS [Pack Size], SalesDetailTable.Sz7 AS [Pack Qty], SalesDetailTable.Sz1 AS Qty, SalesDetailTable.Qty AS [Total Qty],
                strSQL = "SELECT ReceivingMasterTable.Remarks AS [Account Description], '' as NTN_NO, vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area, '' as [Sale Person], '' as [Director], ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No as [Invoice No], " _
                 & " tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit,  " _
                 & " ReceivingDetailTable.Price AS Price, ReceivingDetailTable.ArticleSize AS [Pack Size], ISNULL(ReceivingDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(ReceivingDetailTable.Sz1, 0) AS Qty, ReceivingDetailTable.Qty AS [Total Qty], 0 AS SampleQty,  " _
                 & " ReceivingDetailTable.BatchNo, SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))" _
                 & " AS Amount, IsNull(TaxPercent,0) AS GST, SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax,IsNull(AdTax_Percent,0) AS [Ad Tax %], SUM((IsNull(AdTax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS [Ad Tax Amount], SUM(ISNULL(ReceivingDetailTable.Price, 0) * ISNULL(ReceivingDetailTable.Qty, 0))+SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0)))+SUM((IsNull(AdTax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) " _
                 & " AS NetAmount , SUM(IsNull(ArticleDefTable.ItemWeight,0)*IsNull(ReceivingDetailTable.Qty,0)) as [Total Weight] " _
                 & " FROM ReceivingMasterTable INNER JOIN " _
                 & " ReceivingDetailTable ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId INNER JOIN " _
                 & " ArticleDefTable ON ReceivingDetailTable.ArticleDefId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
                 & " vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                 & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
                 & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId " _
                 & " WHERE  (Convert(varchar, ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ReceivingDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ")  AND LEFT(ReceivingMasterTable.ReceivingNo,2)='SR' AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "" _
                 & " GROUP BY ReceivingMasterTable.Remarks, IsNull(AdTax_Percent,0), ReceivingMasterTable.ReceivingDate, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No,  " _
                 & " ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleSizeDefTable.ArticleSizeName, ArticleColorDefTable.ArticleColorName, ArticleUnitDefTable.ArticleUnitName, " _
                 & " ReceivingDetailTable.Price, ReceivingDetailTable.BatchNo, " _
                 & " ReceivingDetailTable.Qty, tblDefLocation.Location_Name,ArticleTypeDefTable.ArticleTypeName,IsNull(TaxPercent,0), ReceivingDetailTable.ReceivingDetailId , vwCOADetail.CityName, vwCOADetail.TerritoryName, ReceivingDetailTable.ArticleSize, ISNULL(ReceivingDetailTable.Sz7, 0), ISNULL(ReceivingDetailTable.Sz1, 0) "
            End If
            If Me.RadioButton1.Checked = True Then

                'strSQL = "SELECT dbo.vwCOADetail.detail_title AS [Account Description],'' as [Sale Person], '' as [Director], dbo.PurchaseReturnMasterTable.PurchaseReturnDate, dbo.PurchaseReturnMasterTable.PurchaseReturnNo, '' as [Invoice No], tblDefLocation.Location_Name, " _
                '      & " ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
                '      & " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.PurchaseReturnDetailTable.Price AS Price,  " _
                '      & " dbo.PurchaseReturnDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.PurchaseReturnDetailTable.BatchNo, 0 AS Amount, IsNull(Tax_Percent,0) AS GST, ((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax,  " _
                '      & " (ISNULL(dbo.PurchaseReturnDetailTable.Price, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0))+((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS NetAmount " _
                '      & " FROM dbo.PurchaseReturnMasterTable INNER JOIN " _
                '      & " dbo.PurchaseReturnDetailTable ON  " _
                '      & " dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId INNER JOIN " _
                '      & " dbo.ArticleDefTable ON dbo.PurchaseReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId INNER JOIN " _
                '      & " dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
                '      & " dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                '      & " dbo.vwCOADetail ON dbo.PurchaseReturnMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = PurchaseReturnDetailTable.LocationId " _
                '      & " WHERE  (Convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND PurchaseReturnDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""

                'TAKSM166152 Add Total Weight Column
                'strSQL = "SELECT dbo.vwCOADetail.detail_title AS [Account Description],'' as [Sale Person], '' as [Director], dbo.PurchaseReturnMasterTable.PurchaseReturnDate, dbo.PurchaseReturnMasterTable.PurchaseReturnNo, '' as [Invoice No], tblDefLocation.Location_Name, " _
                '    & " ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
                '    & " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.PurchaseReturnDetailTable.Price AS Price,  " _
                '    & " dbo.PurchaseReturnDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.PurchaseReturnDetailTable.BatchNo, 0 AS Amount, IsNull(Tax_Percent,0) AS GST, ((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax,  " _
                '    & " (ISNULL(dbo.PurchaseReturnDetailTable.Price, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0))+((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS NetAmount, (IsNull(ArticleDefTable.ItemWeight,0)*IsNull(PurchaseReturnDetailTable.Qty,0)) as [Total Weight] " _
                '    & " FROM dbo.PurchaseReturnMasterTable INNER JOIN " _
                '    & " dbo.PurchaseReturnDetailTable ON  " _
                '    & " dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId INNER JOIN " _
                '    & " dbo.ArticleDefTable ON dbo.PurchaseReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId INNER JOIN " _
                '    & " dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
                '    & " dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                '    & " dbo.vwCOADetail ON dbo.PurchaseReturnMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = PurchaseReturnDetailTable.LocationId " _
                '    & " WHERE  (Convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND PurchaseReturnDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
                'End TaskM166152

                strSQL = "SELECT dbo.vwCOADetail.detail_title AS [Account Description],vwCOADetail.NTN_NO, vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area, '' as [Sale Person], '' as [Director], dbo.PurchaseReturnMasterTable.PurchaseReturnDate, dbo.PurchaseReturnMasterTable.PurchaseReturnNo, '' as [Invoice No], tblDefLocation.Location_Name, " _
                   & " ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
                   & " ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.PurchaseReturnDetailTable.Price AS Price,  " _
                   & " PurchaseReturnDetailTable.ArticleSize AS [Pack Size], ISNULL(PurchaseReturnDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(PurchaseReturnDetailTable.Sz1, 0) AS Qty, ISNULL(PurchaseReturnDetailTable.Qty, 0) AS [Total Qty], 0 AS SampleQty, dbo.PurchaseReturnDetailTable.BatchNo, dbo.PurchaseReturnDetailTable.Qty * dbo.PurchaseReturnDetailTable.Price AS Amount, IsNull(Tax_Percent,0) AS GST, ((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS SalesTax, IsNull(AdTax_Percent,0) AS [Ad Tax %], ((IsNull(AdTax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS [Ad Tax Amount],  " _
                   & " (ISNULL(dbo.PurchaseReturnDetailTable.Price, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0))+((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0)))+((IsNull(AdTax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS NetAmount, (IsNull(ArticleDefTable.ItemWeight,0)*IsNull(PurchaseReturnDetailTable.Qty,0)) as [Total Weight] " _
                   & " FROM dbo.PurchaseReturnMasterTable INNER JOIN " _
                   & " dbo.PurchaseReturnDetailTable ON  " _
                   & " dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId INNER JOIN " _
                   & " dbo.ArticleDefTable ON dbo.PurchaseReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId INNER JOIN " _
                   & " dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
                   & " dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                   & " dbo.vwCOADetail ON dbo.PurchaseReturnMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = PurchaseReturnDetailTable.LocationId " _
                   & " WHERE  (Convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND PurchaseReturnDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""

            End If
            If Me.rdoDispatch.Checked = True Then
                'strSQL = "SELECT dbo.DispatchMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], dbo.DispatchMasterTable.DispatchDate,  dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.PONo as [PO No],tblDefLocation.Location_Name, " _
                ' & "     ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription,ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
                ' & "     dbo.DispatchDetailTable.Price AS Price, dbo.DispatchDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, 0 AS Amount, " _
                ' & "     0 AS GST, 0 AS SalesTax, (Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount  " _
                ' & "     FROM dbo.vwCOADetail RIGHT OUTER JOIN " _
                ' & "     dbo.ArticleDefTable INNER JOIN " _
                ' & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
                ' & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                ' & "     dbo.DispatchDetailTable INNER JOIN " _
                ' & "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
                ' & "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.VendorId  INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
                ' & "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'GP' OR LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'DN') " _
                ' & "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""

                'TAKSM166152 Add Total Weight Column
                ' strSQL = "SELECT dbo.DispatchMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], dbo.DispatchMasterTable.DispatchDate,  dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.PONo as [PO No],tblDefLocation.Location_Name, " _
                '& "     ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription,ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
                '& "     dbo.DispatchDetailTable.Price AS Price, dbo.DispatchDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, 0 AS Amount, " _
                '& "     0 AS GST, 0 AS SalesTax, (Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount, (IsNull(ArticleDefTable.ItemWeight,0)*ISNull(DispatchDetailTable.Qty,0)) as [Total Weight]  " _
                '& "     FROM dbo.vwCOADetail RIGHT OUTER JOIN " _
                '& "     dbo.ArticleDefTable INNER JOIN " _
                '& "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
                '& "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                '& "     dbo.DispatchDetailTable INNER JOIN " _
                '& "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
                '& "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.VendorId  INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
                '& "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'GP' OR LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'DN') " _
                '& "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
                'End TaskM166152

                strSQL = "SELECT dbo.DispatchMasterTable.Remarks AS [Account Description], '' as NTN_NO , vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area , '' as [Sale Person], '' as [Director], dbo.DispatchMasterTable.DispatchDate,  dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.PONo as [PO No],tblDefLocation.Location_Name, " _
            & "     ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription,ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
            & "     dbo.DispatchDetailTable.Price AS Price, DispatchDetailTable.ArticleSize AS [Pack Size], ISNULL(DispatchDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(DispatchDetailTable.Sz1, 0) AS Qty, DispatchDetailTable.Qty AS [Total Qty], 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, dbo.DispatchDetailTable.Price * dbo.DispatchDetailTable.Qty AS Amount, " _
            & "     0 AS GST, 0 AS SalesTax, 0 as [Ad Tax %], 0 as [Ad Tax Amount], (Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount, (IsNull(ArticleDefTable.ItemWeight,0)*ISNull(DispatchDetailTable.Qty,0)) as [Total Weight]  " _
            & "     FROM dbo.vwCOADetail RIGHT OUTER JOIN " _
            & "     dbo.ArticleDefTable INNER JOIN " _
            & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
            & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
            & "     dbo.DispatchDetailTable INNER JOIN " _
            & "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
            & "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.VendorId  INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
            & "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'GP' OR LEFT(dbo.DispatchMasterTable.DispatchNo, 2) = 'DN') " _
            & "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""

            End If
            If Me.rdoStoreIssueance.Checked = True Then
                'strSQL = "SELECT dbo.DispatchMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], dbo.DispatchMasterTable.DispatchDate, dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.PONo as [PO No], tblDefLocation.Location_Name, " _
                ' & "     ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
                ' & "     dbo.DispatchDetailTable.Price AS Price, dbo.DispatchDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, 0 AS Amount, " _
                ' & "     0 AS GST, 0 AS SalesTax,(Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount   " _
                ' & "     FROM dbo.vwCOADetail RIGHT OUTER JOIN " _
                ' & "     dbo.ArticleDefTable INNER JOIN " _
                ' & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId INNER JOIN " _
                ' & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                ' & "     dbo.DispatchDetailTable INNER JOIN " _
                ' & "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
                ' & "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.VendorId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
                ' & "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 1) = 'I') " _
                ' & "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""

                'TAKSM166152 Add Total Weight Column
                '  strSQL = "SELECT dbo.DispatchMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], dbo.DispatchMasterTable.DispatchDate, dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.PONo as [PO No], tblDefLocation.Location_Name, " _
                '& "     ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
                '& "     dbo.DispatchDetailTable.Price AS Price, dbo.DispatchDetailTable.Qty AS Qty, 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, 0 AS Amount, " _
                '& "     0 AS GST, 0 AS SalesTax,(Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount, (IsNull(ArticleDefTable.ItemWeight,0)*ISNull(DispatchDetailTable.Qty,0)) as [Total Weight]   " _
                '& "     FROM dbo.vwCOADetail RIGHT OUTER JOIN " _
                '& "     dbo.ArticleDefTable INNER JOIN " _
                '& "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId INNER JOIN " _
                '& "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                '& "     dbo.DispatchDetailTable INNER JOIN " _
                '& "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
                '& "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.VendorId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
                '& "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 1) = 'I') " _
                '& "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
                'End TaskM166152
                'Task:1575 Account name required in store issuance report  Ayesha Rehman 
                strSQL = "SELECT dbo.vwCOADetail.detail_title AS [Account Description],'' as NTN_NO , vwCOADetail.CityName as City, vwCOADetail.TerritoryName as Area, '' as [Sale Person], '' as [Director], dbo.DispatchMasterTable.DispatchDate, dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.PONo as [PO No], tblDefLocation.Location_Name, " _
          & "     ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, " _
          & "     dbo.DispatchDetailTable.Price AS Price, DispatchDetailTable.ArticleSize AS [Pack Size], ISNULL(DispatchDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(DispatchDetailTable.Sz1, 0) AS Qty, DispatchDetailTable.Qty AS [Total Qty], 0 AS SampleQty, dbo.DispatchDetailTable.BatchNo, dbo.DispatchDetailTable.Price * dbo.DispatchDetailTable.Qty AS Amount, " _
          & "     0 AS GST, 0 AS SalesTax,0 as [Ad Tax %], 0 as [Ad Tax Amount], (Isnull(DispatchDetailTable.Qty,0) * Isnull(DispatchDetailTable.Price,0)) as NetAmount, (IsNull(ArticleDefTable.ItemWeight,0)*ISNull(DispatchDetailTable.Qty,0)) as [Total Weight]   " _
          & "     FROM dbo.vwCOADetail LEFT OUTER JOIN " _
          & "     dbo.ArticleDefTable INNER JOIN " _
          & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId INNER JOIN " _
          & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
          & "     dbo.DispatchDetailTable INNER JOIN " _
          & "     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId ON  " _
          & "     dbo.ArticleDefTable.ArticleId = dbo.DispatchDetailTable.ArticleDefId ON dbo.vwCOADetail.coa_detail_id = dbo.DispatchMasterTable.StoreIssuanceAccountId INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = DispatchDetailTable.LocationId " _
          & "     WHERE (LEFT(dbo.DispatchMasterTable.DispatchNo, 1) = 'I') " _
          & "     AND (Convert(varchar, DispatchMasterTable.DispatchDate,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND DispatchDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " "
                'End Task:1575
            End If
            If Me.rdoProduction.Checked = True Then
                'strSQL = "SELECT dbo.ProductionMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], dbo.ProductionMasterTable.Production_date, dbo.ProductionMasterTable.Production_no, '' as [PO No],  " _
                ' & "     tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
                ' & "     ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.ProductionDetailTable.CurrentRate AS Price,  " _
                ' & "     dbo.ProductionDetailTable.Qty AS Qty, 0 AS SampleQty, '' AS BatchNo, 0 AS Amount, 0 AS GST, 0 AS SalesTax, (Isnull(ProductionDetailTable.Qty,0) * Isnull(ProductionDetailTable.CurrentRate,0)) as NetAmount  " _
                ' & "     FROM dbo.ArticleDefTable INNER JOIN " _
                ' & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
                ' & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                ' & "     dbo.ProductionDetailTable INNER JOIN " _
                ' & "     dbo.ProductionMasterTable ON dbo.ProductionDetailTable.Production_ID = dbo.ProductionMasterTable.Production_ID ON  " _
                ' & "     dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticledefID INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ProductionDetailTable.Location_Id " _
                ' & "     WHERE (Convert(varchar, ProductionMasterTable.Production_Date,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND ProductionDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefTable.ArticleGroupId in (Select ArticleGroupId From ArticleGroupDefTable  WHERE SubSubID In (Select  main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))", "") & ""


                'TAKSM166152 Add Total Weight Column
                'strSQL = "SELECT dbo.ProductionMasterTable.Remarks AS [Account Description],'' as [Sale Person], '' as [Director], dbo.ProductionMasterTable.Production_date, dbo.ProductionMasterTable.Production_no, '' as [PO No],  " _
                '& "     tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
                '& "     ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.ProductionDetailTable.CurrentRate AS Price,  " _
                '& "     dbo.ProductionDetailTable.Qty AS Qty, 0 AS SampleQty, '' AS BatchNo, 0 AS Amount, 0 AS GST, 0 AS SalesTax, (Isnull(ProductionDetailTable.Qty,0) * Isnull(ProductionDetailTable.CurrentRate,0)) as NetAmount, (IsNull(ArticleDefTable.ItemWeight,0) * IsNull(ProductionDetailTable.Qty,0)) as [Total Weight]  " _
                '& "     FROM dbo.ArticleDefTable INNER JOIN " _
                '& "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
                '& "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
                '& "     dbo.ProductionDetailTable INNER JOIN " _
                '& "     dbo.ProductionMasterTable ON dbo.ProductionDetailTable.Production_ID = dbo.ProductionMasterTable.Production_ID ON  " _
                '& "     dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticledefID INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ProductionDetailTable.Location_Id " _
                '& "     WHERE (Convert(varchar, ProductionMasterTable.Production_Date,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND ProductionDetailTable.ArticleDefId In(" & Me.lstItems.SelectedIDs & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefTable.ArticleGroupId in (Select ArticleGroupId From ArticleGroupDefTable  WHERE SubSubID In (Select  main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))", "") & ""
                'End TaskM166152
                strSQL = "SELECT dbo.ProductionMasterTable.Remarks AS [Account Description],'' as NTN_NO, '' as City, '' as Area, '' as [Sale Person], '' as [Director], dbo.ProductionMasterTable.Production_date, dbo.ProductionMasterTable.Production_no, '' as [PO No],  " _
             & "     tblDefLocation.Location_Name, ArticleTypeDefTable.ArticleTypeName as [Article Type], ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, " _
             & "     ArticleColorDefTable.ArticleColorName as Color, ArticleSizeDefTable.ArticleSizeName as Size,  ArticleUnitDefTable.ArticleUnitName As Unit, dbo.ProductionDetailTable.CurrentRate AS Price,  " _
             & "     ProductionDetailTable.ArticleSize AS [Pack Size], ISNULL(ProductionDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(ProductionDetailTable.Sz1, 0) AS Qty, ProductionDetailTable.Qty AS [Total Qty], 0 AS SampleQty, '' AS BatchNo,  (Isnull(ProductionDetailTable.Qty,0) * Isnull(ProductionDetailTable.CurrentRate,0)) AS Amount, 0 AS GST, 0 AS SalesTax, 0 as [Ad Tax %], 0 as [Ad Tax Amount], (Isnull(ProductionDetailTable.Qty,0) * Isnull(ProductionDetailTable.CurrentRate,0)) as NetAmount, (IsNull(ArticleDefTable.ItemWeight,0) * IsNull(ProductionDetailTable.Qty,0)) as [Total Weight]  " _
             & "     FROM dbo.ArticleDefTable INNER JOIN " _
             & "     dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN " _
             & "     dbo.ArticleUnitDefTable ON dbo.ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleColorDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleDefTable.ArticleTypeId LEFT OUTER JOIN " _
             & "     dbo.ProductionDetailTable INNER JOIN " _
             & "     dbo.ProductionMasterTable ON dbo.ProductionDetailTable.Production_ID = dbo.ProductionMasterTable.Production_ID ON  " _
             & "     dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticledefID INNER JOIN tblDefLocation ON tblDefLocation.Location_Id = ProductionDetailTable.Location_Id " _
             & "     WHERE (Convert(varchar, ProductionMasterTable.Production_Date,102) BETWEEN Convert(Datetime, '" & Me.DtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.DtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND ProductionDetailTable.ArticleDefId In(" & IIf(Me.lstItems.SelectedIDs.Length > 0, Me.lstItems.SelectedIDs, 0) & ")) AND ArticleDefTable.Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefTable.ArticleGroupId in (Select ArticleGroupId From ArticleGroupDefTable  WHERE SubSubID In (Select  main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))", "") & ""
            End If
            'Da = New OleDb.OleDbDataAdapter(strSQL, Con)
            'Da.Fill(Dt)
            Me.GridEX1.DataSource = GetDataTable(strSQL)
            GridEX1.RetrieveStructure()
            Me.GridEX1.AutoSizeColumns()
            GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            GridEX1.RootTable.Columns("GST").Visible = True
            GridEX1.RootTable.Columns("SalesTax").Visible = True
            GridEX1.RootTable.Columns("SampleQty").Visible = False
            GridEX1.RootTable.Columns("Amount").Visible = True
            GridEX1.RootTable.Columns("BatchNo").Visible = False
            GridEX1.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("NetAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            ''R982 Set Formating And Aggregate Function
            GridEX1.RootTable.Columns("SalesTax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Total Weight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Ad Tax Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Pack Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Total Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("SalesTax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Pack Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("SalesTax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Pack Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("NetAmount").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("SalesTax").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack Qty").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Total Qty").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("NetAmount").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("SalesTax").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Ad Tax %").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Ad Tax Amount").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Ad Tax %").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Ad Tax Amount").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack Qty").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Total Qty").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("GST").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("GST").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("NetAmount").Caption = "Inc S/Tax Amount"
            GridEX1.RootTable.Columns("Amount").Caption = "Exc S/Tax Amount"
            GridEX1.RootTable.Columns("Total Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Total Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Ad Tax %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Ad Tax %").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Ad Tax Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Ad Tax Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.AutoSizeColumns()
            'End R:982
            Da = Nothing
            Dt = Nothing
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        Try
            If Me.GridEX1.RowCount > 0 Then Me.GridEX1.ClearStructure()
            Selected_Items()
            CtrlGrdBar1_Load(Nothing, Nothing)
            LoadLayout()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RBtnSales_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBtnSales.CheckedChanged, RadioButton1.CheckedChanged, RBtnPurchase.CheckedChanged, RBtnSales.CheckedChanged, RBtnSalesReturn.CheckedChanged, rdoDispatch.CheckedChanged, rdoProduction.CheckedChanged, rdoStockReceiving.CheckedChanged, rdoStoreIssueance.CheckedChanged, rbtnImports.CheckedChanged ''TFS1824
        Try
            Me.DtpFrom.Value = Me.DtpFrom.Value
            Me.DtpTo.Value = Me.DtpTo.Value
            CtrlGrdBar1_Load(CtrlGrdBar1, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiCtrlGridBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strReportName As String = String.Empty
            If Me.rdoDispatch.Checked = True Then
                strReportName = "Dispatch"
            End If
            If Me.rdoProduction.Checked = True Then
                strReportName = "Production"
            End If
            If Me.rdoStockReceiving.Checked = True Then
                strReportName = "Stock Receiving"
            End If
            If Me.RBtnSalesReturn.Checked = True Then
                strReportName = "Sales Return"
            End If
            If Me.RBtnSales.Checked = True Then
                strReportName = "Sales"
            End If
            If Me.RBtnPurchase.Checked = True Then
                strReportName = "Purchase"
            End If
            ''TFS1824 : Ayesha Rehman: Show data related to both purchases: imports and local purchases
            If Me.rbtnImports.Checked = True Then
                strReportName = "Imports"
            End If
            If Me.rdoStoreIssueance.Checked = True Then
                strReportName = "Store Issuence"
            End If
            If Me.RadioButton1.Checked = True Then
                strReportName = "Purchase Return"
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "Item Detail History On (" & strReportName & ") From  " & Me.DtpFrom.Value.Date.ToString("dd-MM-yyyy") & " To " & Me.DtpTo.Value.Date.ToString("dd-MM-yyyy") & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim strsql As String = String.Empty
            strsql = "SELECT ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription + '---' +  b.ArticleSizeName + '-' +  a.ArticleUnitName AS ArticleDescription FROM ArticleDefTable INNER JOIN ArticleUnitDefTable a ON ArticleDefTable.ArticleUnitId = a.ArticleUnitId INNER JOIN ArticleSizeDefTable b ON ArticleDefTable.SizeRangeId = b.ArticleSizeId ORDER BY  ArticleDescription asc "
            Me.lstItems.ListItem.ValueMember = "ArticleID"
            Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
            Me.lstItems.ListItem.DataSource = UtilityDAL.GetDataTable(strsql)
            Me.lstItems.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsearch.KeyUp
        Try
            fillCombo("Items", Me.txtsearch.Text.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            If Me.RBtnSales.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Sales Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            ElseIf Me.RBtnSalesReturn.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Sales Return Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            ElseIf Me.RBtnPurchase.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Purchase Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
                ''TFS1824
            ElseIf Me.rbtnImports.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Import Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            ElseIf Me.RadioButton1.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Purchase Return Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            ElseIf Me.rdoDispatch.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Dispatch Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            ElseIf Me.rdoStoreIssueance.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Issuance Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            ElseIf Me.rdoStockReceiving.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Receiving Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            ElseIf Me.rdoProduction.Checked = True Then
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Produced Detail History" & vbCrLf & "Date From: " & Me.DtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DtpTo.Value.ToString("dd/MMM/yyyy") & ""
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DtpFrom.Value = Date.Today
            Me.DtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DtpFrom.Value = Date.Today.AddDays(-1)
            Me.DtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.DtpTo.Value = Date.Today
        End If
    End Sub

    Private Sub lstItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstItems.Load

    End Sub

    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        Try
            SaveLayout()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SaveLayout()
        Try
            If Me.RBtnSales.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnSales.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Sales layout saved successfully.")
            ElseIf Me.RBtnSalesReturn.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnSalesReturn.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Sales Return layout saved successfully.")
            ElseIf Me.rdoDispatch.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoDispatch.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Stock Dispatch layout saved successfully.")
            ElseIf Me.rdoProduction.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoProduction.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Production layout saved successfully.")
            ElseIf Me.RBtnPurchase.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnPurchase.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Pruchase layout saved successfully.")
                ''TFS1824
            ElseIf Me.rbtnImports.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rbtnImports.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Import layout saved successfully.")
            ElseIf Me.RadioButton1.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RadioButton1.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Pruchase Return layout saved successfully.")
            ElseIf Me.rdoStockReceiving.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoStockReceiving.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Stock Receiving layout saved successfully.")
            ElseIf Me.rdoStoreIssueance.Checked = True Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoStoreIssueance.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEX1.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Store Issuance layout saved successfully.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LoadLayout()
        Try
            If Me.RBtnSales.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnSales.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnSales.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.RBtnSalesReturn.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnSalesReturn.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnSalesReturn.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.RBtnPurchase.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnPurchase.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RBtnPurchase.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                ''TFS1824
            ElseIf Me.rbtnImports.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rbtnImports.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rbtnImports.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.RadioButton1.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RadioButton1.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.RadioButton1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.rdoDispatch.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoDispatch.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoDispatch.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.rdoProduction.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoProduction.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoProduction.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.rdoStockReceiving.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoStockReceiving.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoStockReceiving.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.rdoStoreIssueance.Checked = True Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoStoreIssueance.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name & "_" & Me.rdoStoreIssueance.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEX1.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class