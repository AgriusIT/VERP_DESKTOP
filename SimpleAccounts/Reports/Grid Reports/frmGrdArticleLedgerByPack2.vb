''TASK TFS1183 new report transactional article ledger. on 09-10-2017
Imports SBModel
Imports System.Data
Public Class frmGrdArticleLedgerByPack2
    Dim IsOpenedForm As Boolean = False

    Private _ItemList As New List(Of SBModel.ArticleList)

    Private Sub frmGrdArticleLedgerByPack2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
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
                ElseIf RightsDt.FormControlName = "Print Report" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export Report" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdArticleLedgerByPack2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            'FillGridEx(Me.grdArticles, "Select ArticleID, ArticleCode, ArticleDescription, ArticeColorName as Color, ArticleSizeName as Size From ArticleDefView WHERE Active=1", True)
            Me.cmbPeriod.Text = "Current Month"
            '_ItemList = GetItemList()
            'Me.lstItems.ListItem.DataSource = _ItemList
            'Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
            'Me.lstItems.ListItem.ValueMember = "ArticleId"
            'FillDropDown(Me.ComboBox1, "Select ArticleId, ArticleDescription From ArticleDefView WHERE Active=1")

            ''Task# 3-11-06-2015 fill combo box of itmes with ArticleId, ArticleDescription and ArticleCode
            FillUltraDropDown(Me.cmbItems, "Select ArticleId, ArticleCode, ArticleDescription From ArticleDefView WHERE Active=1") ''Task# 3-11-06-2015 fill cmbItems combo box 
            Me.cmbItems.Rows(0).Activate()      'active first row of combo box of items

            Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True       'Hide ArticleId in combo box of items
            ''End Task# 3-11-06-2015

            FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation")

            IsOpenedForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub grdArticles_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try




        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Public Function SearchItemList(ByVal Article As SBModel.ArticleList) As Boolean
    '    Try
    '        If Article.ArticleDescription.StartsWith(Me.txtItemDesc.Text) Then
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Private Sub txtItemDesc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItemDesc.KeyDown

    'End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'Me.lstItems.ListItem.DataSource = GetItemList()
            'Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
            'Me.lstItems.ListItem.ValueMember = "ArticleId"
            '_ItemList = GetItemList()

            Dim id As Integer = 0I
            'id = Me.ComboBox1.SelectedIndex
            'FillDropDown(Me.ComboBox1, "Select ArticleId, ArticleDescription From ArticleDefView WHERE Active=1")
            'Me.ComboBox1.SelectedIndex = id

            ''Task# 3-11-06-2015 Add Fill cmbItems combox
            Dim idi As Integer = 0I
            idi = Me.cmbItems.Value
            'FillUltraDropDown(Me.cmbItems, "Select ArticleId, ArticleDescription,ArticleCode From ArticleDefView WHERE Active=1")
            FillUltraDropDown(Me.cmbItems, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination,  ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM ArticleDefView where Active=1")
            Me.cmbItems.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            Me.cmbItems.Value = idi
            ''End Task# 3-11-06-2015

            id = Me.cmbLocation.SelectedIndex
            FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation")
            Me.cmbLocation.SelectedIndex = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub txtItemDesc_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If Me.txtItemDesc.Text = String.Empty Then

    '            Me.lstItems.ListItem.DataSource = _ItemList
    '            Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
    '            Me.lstItems.ListItem.ValueMember = "ArticleId"
    '        Else
    '            Dim Article_List As List(Of SBModel.ArticleList) = _ItemList.FindAll(AddressOf SearchItemList)
    '            Me.lstItems.ListItem.DataSource = Article_List
    '            Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
    '            Me.lstItems.ListItem.ValueMember = "ArticleId"
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
        
            If Me.cmbItems.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select any item")
                Me.cmbItems.Focus()
                Exit Sub
            End If
            Dim strQuery As String = String.Empty
            ' strQuery = "Select * From (SELECT 'Opening' AS DocNo, Convert(DateTime, '" & Me.dtpFrom.Value.AddDays(-1) & "',102) AS DocDate, 'OP' AS StockDocType, 'Opening Stock' AS Remarks, '' AS ProjectName, '' AS detail_code, '' AS detail_title, " _
            '& " '' AS Location_name, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  " _
            '& " dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, '' AS Comments, '' AS Engine_No, '' AS Chassis_No, 0 AS Rate, IsNull(SUM(IsNull(dbo.StockDetailTable.In_PackQty,0)-IsNull(dbo.StockDetailTable.Out_PackQty,0)),0) as Pack_Stock, " _
            '& " Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) <0 then 0 else SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End AS In_Qty,  " _
            '& " SUM(ISNULL(dbo.StockDetailTable.Rate, 0)  * ISNULL(dbo.StockDetailTable.InQty, 0)) AS In_Amount,  " _
            '& " Abs(Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) > 0 then 0 else SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End) AS Out_Qty,  " _
            '& " SUM(ISNULL(dbo.StockDetailTable.Rate, 0) * ISNULL(dbo.StockDetailTable.OutQty, 0)) AS Out_Amount,0 AS Qty_Balance, 0 AS Amount_Balance, 0 as StockTransDetailId " _
            '& " FROM dbo.StockDetailTable INNER JOIN " _
            '& " dbo.StockMasterTable ON dbo.StockDetailTable.StockTransId = dbo.StockMasterTable.StockTransId INNER JOIN " _
            '& " dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id INNER JOIN " _
            '& " dbo.ArticleDefView ON dbo.StockDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
            '& " dbo.tblDefCostCenter ON dbo.StockMasterTable.Project = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '& " dbo.Stock_Document_Type ON dbo.StockMasterTable.DocType = dbo.Stock_Document_Type.StockDocTypeId LEFT OUTER JOIN " _
            '& " dbo.tblDefLocation AS tblDefLocation_1 ON dbo.StockMasterTable.Account_Id = tblDefLocation_1.location_id LEFT OUTER JOIN " _
            '& " dbo.vwCOADetail ON dbo.StockMasterTable.Account_Id = dbo.vwCOADetail.coa_detail_id " _
            '& "  WHERE   (Convert(varchar,dbo.StockMasterTable.DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND  (dbo.ArticleDefView.ArticleId =" & Me.cmbItems.Value & ") " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND dbo.StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '& " GROUP BY dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName,  " _
            '& " dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, dbo.ArticleDefView.ArticleId  " _
            '& " Union All " _
            '& " SELECT   dbo.StockMasterTable.DocNo, dbo.StockMasterTable.DocDate, dbo.Stock_Document_Type.StockDocType, dbo.StockMasterTable.Remarks,  " _
            '& " dbo.tblDefCostCenter.Name AS ProjectName,  " _
            '& " CASE WHEN tblDefLocation_1.location_name <> '' THEN tblDefLocation_1.location_code ELSE dbo.vwCOADetail.detail_code END AS detail_code,  " _
            '& " CASE WHEN tblDefLocation_1.location_name <> '' THEN tblDefLocation_1.location_name ELSE dbo.vwCOADetail.detail_title END AS detail_title,  " _
            '& " dbo.tblDefLocation.location_name, dbo.ArticleDefView.ArticleId,dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  " _
            '& " dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, dbo.StockDetailTable.Remarks AS Comments,  " _
            '& " dbo.StockDetailTable.Engine_No, dbo.StockDetailTable.Chassis_No, dbo.StockDetailTable.Rate, (IsNull(dbo.StockDetailTable.In_PackQty,0)-IsNull(dbo.StockDetailTable.Out_PackQty,0)) as Pack_Qty, IsNull(dbo.StockDetailTable.InQty,0) AS In_Qty,  " _
            '& " IsNull(dbo.StockDetailTable.InQty,0) * (IsNull(dbo.StockDetailTable.Rate,0)) AS In_Amount, IsNull(dbo.StockDetailTable.OutQty,0) AS Out_Qty,  " _
            '& " IsNull(dbo.StockDetailTable.OutQty,0)* (IsNull(dbo.StockDetailTable.Rate,0)) AS Out_Amount, 0 AS Qty_Balance, 0 AS Amount_Balance, dbo.StockDetailTable.StockTransDetailId " _
            '& " FROM dbo.StockDetailTable INNER JOIN " _
            '& " dbo.StockMasterTable ON dbo.StockDetailTable.StockTransId = dbo.StockMasterTable.StockTransId INNER JOIN " _
            '& " dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id INNER JOIN " _
            '& " dbo.ArticleDefView ON dbo.StockDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
            '& " dbo.tblDefCostCenter ON dbo.StockMasterTable.Project = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '& "  dbo.Stock_Document_Type ON dbo.StockMasterTable.DocType = dbo.Stock_Document_Type.StockDocTypeId LEFT OUTER JOIN " _
            '& " dbo.tblDefLocation AS tblDefLocation_1 ON dbo.StockMasterTable.Account_Id = tblDefLocation_1.location_id LEFT OUTER JOIN " _
            '& " dbo.vwCOADetail ON dbo.StockMasterTable.Account_Id = dbo.vwCOADetail.coa_detail_id " _
            '& " WHERE (Convert(varchar,dbo.StockMasterTable.DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) AND (dbo.ArticleDefView.ArticleId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND dbo.StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & ")) a Order By a.DocDate ASC "
            strQuery = "Exec TransactionArticleLedger " & Me.cmbItems.Value & ", '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & IIf(Me.cmbLocation.SelectedIndex = -1, 0, Me.cmbLocation.SelectedValue) & ""
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            dt.AcceptChanges()


            Dim dblQtyBalance As Double = 0D
            Dim dblAmtBalance As Double = 0D
            For Each r As DataRow In dt.Rows
                dblQtyBalance += Val(r.Item("In_Qty").ToString) - Val(r.Item("Out_Qty").ToString)
                dblAmtBalance += Val(r.Item("In_Amount").ToString) - Val(r.Item("Out_Amount").ToString)
                r.BeginEdit()
                r("Qty_Balance") = dblQtyBalance
                r("Amount_Balance") = dblAmtBalance
                r.EndEdit()
                'dblQtyBalance += dblQtyBalance
                'dblAmtBalance += dblAmtBalance
            Next

          
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            GridEX1.RootTable.Columns("ArticleId").Visible = False
            GridEX1.RootTable.Columns("StockTransDetailId").Visible = False
            GridEX1.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
            GridEX1.RootTable.Columns("DocDate").Caption = "Doc Date"
            GridEX1.RootTable.Columns("DocNo").Caption = "Doc No"
            GridEX1.RootTable.Columns("StockDocType").Caption = "Doc Type"
            GridEX1.RootTable.Columns("ArticleSizeName").Caption = "Size"
            GridEX1.RootTable.Columns("ArticleColorName").Caption = "Color"
            GridEX1.RootTable.Columns("ArticleUnitName").Caption = "Unit"
            GridEX1.RootTable.Columns("In_Qty").Caption = "Recv Stock"
            GridEX1.RootTable.Columns("Out_Qty").Caption = "Out Stock"
            GridEX1.RootTable.Columns("In_Amount").Caption = "Recv Stock Val"
            GridEX1.RootTable.Columns("Out_Amount").Caption = "Out Stock Val"
            GridEX1.RootTable.Columns("In_Qty").Visible = True
            GridEX1.RootTable.Columns("Out_Qty").Visible = True
            GridEX1.RootTable.Columns("Qty_Balance").Visible = True
            GridEX1.RootTable.Columns("Amount_Balance").Visible = True
            GridEX1.RootTable.Columns("In_Amount").Visible = True
            GridEX1.RootTable.Columns("Out_Amount").Visible = True

            ''R:M-2 Set Rounding figurate format
            ''Task:2359 --------------------------------------------------------------------
            GridEX1.RootTable.Columns("In_Qty").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Out_Qty").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Qty_Balance").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Amount_Balance").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("In_Amount").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Out_Amount").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            ''
            'GridEX1.RootTable.Columns("Qty_Balance").FormatString = "N" & DecimalPointInValue
            'GridEX1.RootTable.Columns("Amount_Balance").FormatString = "N" & DecimalPointInValue
            'GridEX1.RootTable.Columns("Qty_Balance").TotalFormatString = "N" & DecimalPointInValue
            'GridEX1.RootTable.Columns("Amount_Balance").TotalFormatString = "N" & DecimalPointInValue
            ''
            GridEX1.RootTable.Columns("In_Qty").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Out_Qty").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Qty_Balance").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Amount_Balance").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("In_Amount").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Out_Amount").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Rate").TotalFormatString = "N" & DecimalPointInValue
            'End R:M-2
            GridEX1.RootTable.Columns("Pack_Stock").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack_Stock").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack_Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Pack_Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_Amount").Position = GridEX1.RootTable.Columns("In_Qty").Index + 1
            GridEX1.RootTable.Columns("Out_Amount").Position = GridEX1.RootTable.Columns("Out_Qty").Index + 2
            GridEX1.RootTable.Columns("Amount_Balance").Position = GridEX1.RootTable.Columns("Qty_Balance").Index + 1
            GridEX1.RootTable.Columns("Pack_Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("In_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Out_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'GridEX1.RootTable.Columns("Amount_Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("In_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Out_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            'GridEX1.RootTable.Columns("Qty_Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'GridEX1.RootTable.Columns("Amount_Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'GridEX1.RootTable.Columns("Qty_Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("In_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Amount_Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Qty_Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Amount_Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Qty_Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            GridEX1.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            For c As Integer = 0 To Me.GridEX1.RootTable.Columns.Count - 1
                Me.GridEX1.RootTable.Columns(c).AllowSort = False
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.GridEX1.AutoSizeColumns()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Transactional Item Ledger by Pack" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Task# 3-11-06-2015 add radion button event for search by Code
    Private Sub rbtByCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnByCode.CheckedChanged
        Try
            If IsOpenedForm = False Then
                Exit Sub
            End If

            If Me.rbtnByCode.Checked = True Then
                'Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleCode").Key.ToString
                Me.cmbItems.DisplayMember = Me.cmbItems.Rows(0).Cells("ArticleCode").Column.Key.ToString()


            Else
                'Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleDescription").Key.ToString
                Me.cmbItems.DisplayMember = Me.cmbItems.Rows(0).Cells("ArticleDescription").Column.Key.ToString()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Emd Task# 3-11-06-2015

    ''Task# 3-11-06-2015 add radion button event for search by Name
    Private Sub rbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnByName.CheckedChanged
        Try
            If IsOpenedForm = False Then
                Exit Sub
            End If

            If Me.rbtnByName.Checked = True Then
                Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleDescription").Key.ToString
            Else
                Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleCode").Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task# 3-11-06-2015

    Private Sub frmGrdArticleLedgerByPack2_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'Me.BackColor = Color.White
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class