Public Class frmGrdRptSectorSales
    Private _DateFrom As DateTime
    Private _DateTo As DateTime
    Private _Sector As Integer = 0I
    Private _SectorName As String = String.Empty
    Private _FlgDetailReport As Boolean = False
    Private _Dt As DataTable
    Enum enmCustomers
        CustomerCode
        SalesDate
        Detail_Title
        Count
    End Enum
    Private Sub frmGrdRptSectorSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillDropDown(Me.ComboBox1, "Select TerritoryId, TerritoryName From tblListTerritory")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                _DateFrom = Date.Today
                _DateTo = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                _DateFrom = Date.Today.AddDays(-1)
                _DateTo = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                _DateFrom = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                _DateTo = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                _DateFrom = New Date(Date.Now.Year, Date.Now.Month, 1)
                _DateTo = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                _DateFrom = New Date(Date.Now.Year, 1, 1)
                _DateTo = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Me.ToolStripProgressBar1.Visible = True
        Try
            _DateFrom = Me.dtpFromDate.Value
            _DateTo = Me.dtpToDate.Value
            _Sector = Me.ComboBox1.SelectedValue
            _SectorName = Me.ComboBox1.Text
            If rbtDetail.Checked = True Then
                _FlgDetailReport = True
            Else : _FlgDetailReport = False
            End If
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            If _Dt IsNot Nothing Then
                Me.grd.DataSource = Nothing
                Me.grd.DataSource = _Dt
                Me.grd.RetrieveStructure()

            End If
            ApplyGridSettings()
            Me.ToolStripProgressBar1.Visible = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Function FillGrid()
        Try
            _Dt = New DataTable
            Dim strQuery As String = String.Empty
            If _FlgDetailReport = True Then
                strQuery = "SELECT DISTINCT dbo.SalesMasterTable.CustomerCode, CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesMasterTable.SalesDate,11),102), 102) AS SalesDate, dbo.vwCOADetail.detail_title as [Account Description] FROM    dbo.vwCOADetail INNER JOIN    dbo.SalesMasterTable ON dbo.vwCOADetail.coa_detail_id = dbo.SalesMasterTable.CustomerCode WHERE (Convert(DateTime, Convert(varchar, LEFT(SalesDate,11), 102),102) BETWEEN Convert(DateTime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me._Sector > 0, " AND CustomerCode in(Select AccountId From tblCustomer WHERE Territory=" & Me._Sector & ")", "") & " ORDER BY CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesMasterTable.SalesDate,11),102), 102) ASC"
            Else
                strQuery = "SELECT coa_detail_id, Convert(DateTime, Convert(Varchar, LEFT(getDate(),11),102),102) as SalesDate, detail_title as [Account Description] From vwCOADetail WHERE Account_Type='Customer' " & IIf(Me._Sector > 0, " AND TerritoryName='" & _SectorName & "'", "")
            End If
            _Dt = GetDataTable(strQuery)
            strQuery = String.Empty
            strQuery = "Select ArticleGenderId, ArticleGenderName From ArticleGenderDefTable"
            Dim dtGen As New DataTable
            dtGen = GetDataTable(strQuery)
            Dim i As Integer = 0I
            For Each r As DataRow In dtGen.Rows
                If Not _Dt.Columns.Contains(r(1)) Then
                    _Dt.Columns.Add(r(0), GetType(System.Int32), r(0))
                    _Dt.Columns.Add(r(1), GetType(System.String))
                    _Dt.Columns.Add("Des" & "@" & i, GetType(System.Double))
                    _Dt.Columns.Add("Rep" & "@" & i, GetType(System.Double))
                    _Dt.Columns.Add("Mrk" & "@" & i, GetType(System.Double))
                    _Dt.Columns.Add("Sale" & "@" & i, GetType(System.Double))
                End If
                i += 1
            Next
            For Each r As DataRow In _Dt.Rows
                For j As Integer = enmCustomers.Count To _Dt.Columns.Count - 6 Step 6
                    r.BeginEdit()
                    r(j + 2) = 0
                    r(j + 3) = 0
                    r(j + 4) = 0
                    r(j + 5) = 0
                    r.EndEdit()
                Next
            Next
            strQuery = String.Empty
            If _FlgDetailReport = True Then
                strQuery = String.Empty
                strQuery = "SELECT dbo.SalesMasterTable.CustomerCode, CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesMasterTable.SalesDate,11), 102), 102) AS SalesDate,  dbo.ArticleDefView.ArticleGenderId, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.CurrentPrice, 0)) AS Amount, SUM(CASE WHEN isnull(CurrentPrice, 0) = 0 THEN 0 ELSE ((isnull(CurrentPrice, 0) - Isnull(Price, 0))* ISNULL(Qty,0)) END) AS MRK FROM   dbo.SalesDetailTable INNER JOIN  dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId INNER JOIN dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId  WHERE (Convert(Varchar, SalesDate, 102) BETWEEN Convert(DateTime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me._Sector > 0, " AND CustomerCode in (Select AccountId From tblCustomer WHERE Territory=" & Me._Sector & ")", "") & " GROUP BY dbo.SalesMasterTable.CustomerCode, CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesMasterTable.SalesDate,11), 102), 102), dbo.ArticleDefView.ArticleGenderId"
                Dim dtData As New DataTable
                dtData = GetDataTable(strQuery)
                Dim dr() As DataRow
                For Each r As DataRow In _Dt.Rows
                    dr = dtData.Select("CustomerCode=" & r(0) & " AND SalesDate='" & r(1) & "'")
                    If dr IsNot Nothing Then
                        If dr.Length > 0 Then
                            For Each drFound As DataRow In dr
                                r.BeginEdit()
                                r(_Dt.Columns.IndexOf(drFound(2)) + 2) = drFound(3)
                                r(_Dt.Columns.IndexOf(drFound(2)) + 4) = drFound(4)
                                r.EndEdit()
                            Next
                        End If
                    End If
                Next
                strQuery = String.Empty
                strQuery = "SELECT dbo.SalesReturnMasterTable.CustomerCode, CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate,11), 102), 102) AS SalesReturnDate,  dbo.ArticleDefView.ArticleGenderId, SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0) * ISNULL(dbo.SalesReturnDetailTable.CurrentPrice, 0)) AS Amount, SUM(CASE WHEN isnull(CurrentPrice, 0) = 0 THEN 0 ELSE ((isnull(CurrentPrice, 0) - Isnull(Price, 0)) * isnull(Qty,0)) END) AS MRK FROM   dbo.SalesReturnDetailTable INNER JOIN  dbo.SalesReturnMasterTable ON dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId INNER JOIN dbo.ArticleDefView ON dbo.SalesReturnDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId  WHERE (Convert(DateTime, Convert(Varchar, LEFT(SalesReturnDate,11),102),102) BETWEEN Convert(DateTime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me._Sector > 0, " AND CustomerCode in (Select AccountId From tblCustomer WHERE Territory=" & Me._Sector & ")", "") & " And SalesReturnDetailTable.LocationId in (Select Location_Id From tblDefLocation WHERE Location_Type='Damage') GROUP BY dbo.SalesReturnMasterTable.CustomerCode, CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate,11), 102), 102), dbo.ArticleDefView.ArticleGenderId"
                Dim dtDataRet As New DataTable
                dtDataRet = GetDataTable(strQuery)
                Dim dr1() As DataRow
                For Each r As DataRow In _Dt.Rows
                    dr1 = dtDataRet.Select("CustomerCode=" & r(0) & " AND SalesReturnDate='" & r(1) & "'")
                    If dr1 IsNot Nothing Then
                        If dr1.Length > 0 Then
                            For Each drFound As DataRow In dr1
                                r.BeginEdit()
                                r(_Dt.Columns.IndexOf(drFound(2)) + 3) = drFound(3)
                                r(_Dt.Columns.IndexOf(drFound(2)) + 4) = r(_Dt.Columns.IndexOf(drFound(2)) + 4) - drFound(4)
                                r.EndEdit()
                            Next
                        End If
                    End If
                Next
                _Dt.AcceptChanges()
                For Each r As DataRow In _Dt.Rows
                    For c As Integer = enmCustomers.Count To _Dt.Columns.Count - 6 Step 6
                        r.BeginEdit()
                        r(c + 5) = (r(c + 2) - (r(c + 3) + r(c + 4)))
                        r.EndEdit()
                    Next
                Next
                _Dt.AcceptChanges()
            Else
                '------------------------------------------------------------- Summary ----------------------------------------

                strQuery = String.Empty
                strQuery = "SELECT dbo.SalesMasterTable.CustomerCode, dbo.ArticleDefView.ArticleGenderId, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.CurrentPrice, 0)) AS Amount, SUM(CASE WHEN isnull(CurrentPrice, 0) = 0 THEN 0 ELSE ((isnull(CurrentPrice, 0) - Isnull(Price, 0))* ISNULL(Qty,0)) END) AS MRK FROM   dbo.SalesDetailTable INNER JOIN  dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId INNER JOIN dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId  WHERE (Convert(Varchar, SalesDate, 102) BETWEEN Convert(DateTime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me._Sector > 0, " AND CustomerCode in (Select AccountId From tblCustomer WHERE Territory=" & Me._Sector & ")", "") & " GROUP BY dbo.SalesMasterTable.CustomerCode,  dbo.ArticleDefView.ArticleGenderId"
                Dim dtData As New DataTable
                dtData = GetDataTable(strQuery)
                Dim dr() As DataRow
                For Each r As DataRow In _Dt.Rows
                    dr = dtData.Select("CustomerCode=" & r(0) & "")
                    If dr IsNot Nothing Then
                        If dr.Length > 0 Then
                            For Each drFound As DataRow In dr
                                r.BeginEdit()
                                r(_Dt.Columns.IndexOf(drFound(1)) + 2) = drFound(2)
                                r(_Dt.Columns.IndexOf(drFound(1)) + 4) = drFound(3)
                                r.EndEdit()
                            Next
                        End If
                    End If
                Next
                strQuery = String.Empty
                strQuery = "SELECT dbo.SalesReturnMasterTable.CustomerCode, dbo.ArticleDefView.ArticleGenderId, SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0) * ISNULL(dbo.SalesReturnDetailTable.CurrentPrice, 0)) AS Amount, SUM(CASE WHEN isnull(CurrentPrice, 0) = 0 THEN 0 ELSE ((isnull(CurrentPrice, 0) - Isnull(Price, 0)) * isnull(Qty,0)) END) AS MRK FROM   dbo.SalesReturnDetailTable INNER JOIN  dbo.SalesReturnMasterTable ON dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId INNER JOIN dbo.ArticleDefView ON dbo.SalesReturnDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId  WHERE (Convert(DateTime, Convert(Varchar, LEFT(SalesReturnDate,11),102),102) BETWEEN Convert(DateTime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me._Sector > 0, " AND CustomerCode in (Select AccountId From tblCustomer WHERE Territory=" & Me._Sector & ")", "") & " And SalesReturnDetailTable.LocationId in (Select Location_Id From tblDefLocation WHERE Location_Type='Damage') GROUP BY dbo.SalesReturnMasterTable.CustomerCode, dbo.ArticleDefView.ArticleGenderId"
                Dim dtDataRet As New DataTable
                dtDataRet = GetDataTable(strQuery)
                Dim dr1() As DataRow
                For Each r As DataRow In _Dt.Rows
                    dr1 = dtDataRet.Select("CustomerCode=" & r(0) & "")
                    If dr1 IsNot Nothing Then
                        If dr1.Length > 0 Then
                            For Each drFound As DataRow In dr1
                                r.BeginEdit()
                                r(_Dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                                r(_Dt.Columns.IndexOf(drFound(1)) + 4) = r(_Dt.Columns.IndexOf(drFound(1)) + 4) - drFound(3)
                                r.EndEdit()
                            Next
                        End If
                    End If
                Next
                _Dt.AcceptChanges()
                For Each r As DataRow In _Dt.Rows
                    For c As Integer = enmCustomers.Count To _Dt.Columns.Count - 6 Step 6
                        r.BeginEdit()
                        r(c + 5) = (r(c + 2) - (r(c + 3) + r(c + 4)))
                        r.EndEdit()
                    Next
                Next
                _Dt.AcceptChanges()
            End If

            Dim _Total As String = String.Empty
            Dim _TotalDes As String = String.Empty
            Dim _TotalRep As String = String.Empty
            Dim _TotalMrk As String = String.Empty




            For c As Integer = enmCustomers.Count To _Dt.Columns.Count - 6 Step 6
                If _Total.Length > 0 Then
                    _Total = _Total & "+" & "[" & _Dt.Columns(c + 5).ColumnName & "]"
                Else
                    _Total = "[" & _Dt.Columns(c + 5).ColumnName & "]"
                End If
                If _TotalDes.Length > 0 Then
                    _TotalDes = _TotalDes & "+" & "[" & _Dt.Columns(c + 2).ColumnName & "]"
                Else
                    _TotalDes = "[" & _Dt.Columns(c + 2).ColumnName & "]"
                End If
                If _TotalRep.Length > 0 Then
                    _TotalRep = _TotalRep & "+" & "[" & _Dt.Columns(c + 3).ColumnName & "]"
                Else
                    _TotalRep = "[" & _Dt.Columns(c + 3).ColumnName & "]"
                End If
                If _TotalMrk.Length > 0 Then
                    _TotalMrk = _TotalMrk & "+" & "[" & _Dt.Columns(c + 4).ColumnName & "]"
                Else
                    _TotalMrk = "[" & _Dt.Columns(c + 4).ColumnName & "]"
                End If

            Next
            _Dt.Columns.Add("Total", GetType(System.Double))
            _Dt.Columns.Add("Total Des", GetType(System.Double))
            _Dt.Columns.Add("Total Rep", GetType(System.Double))
            _Dt.Columns.Add("Total Mrk", GetType(System.Double))

            _Dt.Columns("Total").Expression = _Total.ToString
            _Dt.Columns("Total Des").Expression = _TotalDes.ToString
            _Dt.Columns("Total Rep").Expression = _TotalRep.ToString
            _Dt.Columns("Total Mrk").Expression = _TotalMrk.ToString
            Return _Dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
           FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            Me.grd.FrozenColumns = Me.grd.RootTable.Columns(enmCustomers.Detail_Title).Index
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            If _FlgDetailReport = False Then
                Me.grd.RootTable.Columns("SalesDate").Visible = False
            Else
                Me.grd.RootTable.Columns("SalesDate").Visible = True
            End If
            Me.grd.RootTable.Columns("SalesDate").Caption = "Date"
            Me.grd.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Me.grd.RootTable.ColumnSetRowCount = 1
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            ColumnSet1 = Me.grd.RootTable.ColumnSets.Add
            ColumnSet1.Caption = ""
            If _FlgDetailReport = True Then
                ColumnSet1.ColumnCount = 2
                ColumnSet1.Add(Me.grd.RootTable.Columns(enmCustomers.SalesDate), 0, 0)
                ColumnSet1.Add(Me.grd.RootTable.Columns(enmCustomers.Detail_Title), 0, 1)
            Else
                ColumnSet1.ColumnCount = 1
                ColumnSet1.Add(Me.grd.RootTable.Columns(enmCustomers.Detail_Title), 0, 0)
            End If
            Me.grd.RootTable.Columns(enmCustomers.SalesDate).FormatString = "dd/MMM/yyyy"
            For c As Integer = enmCustomers.Count To Me.grd.RootTable.Columns.Count - 6 Step 6
                Me.grd.RootTable.Columns(c).Visible = False
                Me.grd.RootTable.Columns(c + 2).Caption = "Des"
                Me.grd.RootTable.Columns(c + 3).Caption = "Rep"
                Me.grd.RootTable.Columns(c + 4).Caption = "Mrk"
                Me.grd.RootTable.Columns(c + 5).Caption = "Sales"
                ColumnSet = Me.grd.RootTable.ColumnSets.Add
                ColumnSet.ColumnCount = 4
                ColumnSet.Caption = Me.grd.RootTable.Columns(c + 1).Caption
                ColumnSet.Add(Me.grd.RootTable.Columns(c + 2), 0, 0)
                ColumnSet.Add(Me.grd.RootTable.Columns(c + 3), 0, 1)
                ColumnSet.Add(Me.grd.RootTable.Columns(c + 4), 0, 2)
                ColumnSet.Add(Me.grd.RootTable.Columns(c + 5), 0, 3)


                Me.grd.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 4).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 5).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


                Me.grd.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


                Me.grd.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 5).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far



                Me.grd.RootTable.Columns(c + 2).FormatString = "N"
                Me.grd.RootTable.Columns(c + 3).FormatString = "N"
                Me.grd.RootTable.Columns(c + 4).FormatString = "N"
                Me.grd.RootTable.Columns(c + 5).FormatString = "N"

                Me.grd.RootTable.Columns(c + 2).TotalFormatString = "N"
                Me.grd.RootTable.Columns(c + 3).TotalFormatString = "N"
                Me.grd.RootTable.Columns(c + 4).TotalFormatString = "N"
                Me.grd.RootTable.Columns(c + 5).TotalFormatString = "N"

            Next


            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            ColumnSet2 = Me.grd.RootTable.ColumnSets.Add
            ColumnSet2.Caption = "Total"
            ColumnSet2.ColumnCount = 4
            ColumnSet2.Add(Me.grd.RootTable.Columns("Total"), 0, 0)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Total Des"), 0, 1)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Total Rep"), 0, 2)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Total Mrk"), 0, 3)

            Me.grd.RootTable.Columns("Total").Caption = "Total Sales"
            Me.grd.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Des").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Rep").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Mrk").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Des").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Rep").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Mrk").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Des").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Rep").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Mrk").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptSectorSales_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If Me.UltraTabControl1.Tabs(1).TabPage.Tab.Selected = True Then
                If e.KeyCode = Keys.Escape Then
                    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class