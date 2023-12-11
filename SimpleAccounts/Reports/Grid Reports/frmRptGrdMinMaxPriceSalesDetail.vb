Imports SBDal
Public Class frmRptGrdMinMaxPriceSalesDetail
    Private _dt As DataTable
    Private Function getData() As DataTable
        Try
            Dim str As String = "Select SrNo, ArticleId, ArticleCode, ArticleDescription, ArticleColorName, ArticleSizeName, ArticleUnitName, Sum(MinPrice) as MinPrice,  Sum(MinPriceAmount) as MinPriceAmount, Sum(MaxPrice) as MaxPrice, Sum(MaxPriceAmount) as MaxPriceAmount        From (Select 0 as SrNo, a.ArticleId, a.ArticleCode, a.ArticleDescription, a.ArticleColorName, a.ArticleSizeName, a.ArticleUnitName, ISNULL(vwDetail.MinPrice,0) as MinPrice, ISNULL(vwDetail.MinPriceAmount,0) as MinPriceAmount, ISNULL(vwDetail.MaxPrice,0) as MaxPrice, ISNULL(vwDetail.MaxPriceAmount,0) as MaxPriceAmount From ArticleDefView a LEFT OUTER JOIN " _
                                   & " ( " _
                                   & " select  ArticleId, MinPrice, Sum(MinPriceAmount) as MinPriceAmount,MaxPrice, sum(MaxPriceAmount) as MaxPriceAmount from " _
                                   & " (select a.ArticleDefId as ArticleId, 'Minimum' as Type, ISNULL(A.Price,0) as MinPrice, ISNULL(A.Qty,0)*ISNULL(A.Price,0) as MinPriceAmount,0 MaxPrice, 0 as MaxPriceAmount from SalesDetailTable A INNER JOIN SalesMasterTable C ON A.SalesId = C.SalesId, " _
                                   & " (select articleDefId,Min(Price) as Price from SalesDetailTable " _
                                   & " group by ArticleDefId) B " _
                                   & " where a.ArticleDefId=b.ArticleDefId  " _
                                   & " and a.Price=b.Price " _
                                   & " union  " _
                                   & " select a.ArticleDefId as ArticleId, 'Maximum' as Type, 0,0, ISNULL(A.Price,0) as MaxPrice, ISNULL(A.Qty,0)*ISNULL(A.Price,0) as MaxPriceAmount from SalesDetailTable A INNER JOIN SalesMasterTable C ON A.SalesId = C.SalesId, " _
                                   & " (select articleDefId, max(Price) as Price from SalesDetailTable " _
                                   & " group by ArticleDefId) B " _
                                   & " where a.ArticleDefId=b.ArticleDefId  " _
                                   & " and a.Price=b.Price) details " _
                                   & " group by ArticleId, MinPrice,MaxPrice " _
                                   & " ) vwDetail on vwDetail.ArticleId = a.ArticleId " _
                                   & " WHERE vwDetail.ArticleId IN (Select distinct articledefid from salesDetailTable )) abc WHERE (abc.MinPrice <> 0 Or abc.MinPriceAmount <> 0 Or abc.MaxPrice <> 0 Or abc.MaxPriceAmount <> 0) Group By  SrNo, ArticleId, ArticleCode, ArticleDescription, ArticleColorName, ArticleSizeName, ArticleUnitName "

            _dt = GetDataTable(str)
            Dim RIndex As Integer = 0
            For Each r As DataRow In _dt.Rows
                r.BeginEdit()
                RIndex += 1
                r("SrNO") = RIndex
                r.EndEdit()
            Next
            _dt.AcceptChanges()
            Return _dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub FillGrid()


        Try
            'Dim str As String = "Select a.ArticleId, a.ArticleCode, a.ArticleDescription, a.ArticleColorName, a.ArticleSizeName, a.ArticleUnitName, ISNULL(vwDetail.MinPrice,0) as MinPrice, ISNULL(vwDetail.MinPriceAmount,0) as MinPriceAmount, ISNULL(vwDetail.MaxPrice,0) as MaxPrice, ISNULL(vwDetail.MaxPriceAmount,0) as MaxPriceAmount From ArticleDefView a LEFT OUTER JOIN " _
            '                  & " ( " _
            '                  & " select  ArticleId, MinPrice, Sum(MinPriceAmount) as MinPriceAmount,MaxPrice, sum(MaxPriceAmount) as MaxPriceAmount from " _
            '                  & " (select a.ArticleDefId as ArticleId, 'Minimum' as Type, ISNULL(A.Price,0) as MinPrice, ISNULL(A.Qty,0)*ISNULL(A.Price,0) as MinPriceAmount,0 MaxPrice, 0 as MaxPriceAmount from SalesDetailTable A INNER JOIN SalesMasterTable C ON A.SalesId = C.SalesId, " _
            '                  & " (select articleDefId,Min(Price) as Price from SalesDetailTable " _
            '                  & " group by ArticleDefId) B " _
            '                  & " where a.ArticleDefId=b.ArticleDefId AND (Convert(varchar, c.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
            '                  & " and a.Price=b.Price " _
            '                  & " union  " _
            '                  & " select a.ArticleDefId as ArticleId, 'Maximum' as Type, 0,0, ISNULL(A.Price,0) as MaxPrice, ISNULL(A.Qty,0)*ISNULL(A.Price,0) as MaxPriceAmount from SalesDetailTable A INNER JOIN SalesMasterTable C ON A.SalesId = C.SalesId, " _
            '                  & " (select articleDefId, max(Price) as Price from SalesDetailTable " _
            '                  & " group by ArticleDefId) B " _
            '                  & " where a.ArticleDefId=b.ArticleDefId AND (Convert(varchar, c.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
            '                  & " and a.Price=b.Price) details " _
            '                  & " group by ArticleId, MinPrice,MaxPrice " _
            '                  & " ) vwDetail on vwDetail.ArticleId = a.ArticleId " _
            '                  & " WHERE vwDetail.ArticleId IN (Select distinct articledefid from salesDetailTable )  ORDER BY a.SortOrder Asc "

            Me.grd.DataSource = getData()
            Me.grd.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub
    Private Sub FillCombo(Optional ByVal Condition As String = "")
        'Try
        '    Dim strsql As String = " SELECT     TOP 100 PERCENT dbo.ArticleDefTable.ArticleId, " & _
        '         " dbo.ArticleDefTable.ArticleDescription + '------' + + y.ArticleColorName + '-' + b.ArticleSizeName + '-' + a.ArticleUnitName AS ArticleDescription, " & _
        '            " y.ArticleColorName, b.ArticleSizeName, a.ArticleUnitName " & _
        '              " FROM dbo.ArticleDefTable INNER JOIN " & _
        '               "   dbo.ArticleUnitDefTable a ON dbo.ArticleDefTable.ArticleUnitId = a.ArticleUnitId INNER JOIN " & _
        '                 " dbo.ArticleSizeDefTable b ON dbo.ArticleDefTable.SizeRangeId = b.ArticleSizeId INNER JOIN " & _
        '                   " dbo.ArticleColorDefTable y ON y.ArticleColorId = dbo.ArticleDefTable.ArticleColorId WHERE ArticleDefTable.ArticleId In(Select ArticleDefId From SalesDetailTable) AND ArticleDefTable.ArticleDescription LIKE '" & Me.TextBox1.Text & "%'" & _
        '                     " ORDER BY dbo.ArticleDefTable.ArticleDescription"
        '    Me.lstItems.ListItem.ValueMember = "ArticleID"
        '    Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
        '    Me.lstItems.ListItem.DataSource = UtilityDAL.GetDataTable(strsql)
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Me.Cursor = Cursors.WaitCursor
        Dim lbl As New Label
        Try
            lbl.Visible = True
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            lbl.Visible = False
        End Try
    End Sub

    Private Sub frmRptGrdMinMaxPriceSalesDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnGenerate_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmRptGrdMinMaxPriceSalesDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            'Me.dtpFromDate.Value = Me.dtpFromDate.Value.AddYears(-1)
            'Me.dtpToDate.Value = Date.Now
            'Me.cmbPeriod.Text = "Current Month"
            'FillCombo()
            'FillGrid()
            'FillGrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        FillCombo()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Min And Max Price Sales Detail"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.cmbPeriod.Text = "Today" Then
    '        Me.dtpFromDate.Value = Date.Today
    '        Me.dtpToDate.Value = Date.Today
    '    ElseIf Me.cmbPeriod.Text = "Yesterday" Then
    '        Me.dtpFromDate.Value = Date.Today.AddDays(-1)
    '        Me.dtpToDate.Value = Date.Today.AddDays(-1)
    '    ElseIf Me.cmbPeriod.Text = "Current Week" Then
    '        Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
    '        Me.dtpToDate.Value = Date.Today
    '    ElseIf Me.cmbPeriod.Text = "Current Month" Then
    '        Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
    '        Me.dtpToDate.Value = Date.Today
    '    ElseIf Me.cmbPeriod.Text = "Current Year" Then
    '        Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
    '        Me.dtpToDate.Value = Date.Today
    '    End If
    'End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            getData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmRptGrdMinMaxPriceSalesDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lbl As New Label
        Try
            lbl.Visible = True
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()
        Catch ex As Exception
        Finally
            lbl.Visible = False
        End Try
    End Sub
End Class