Public Class frmGrdRptDemandDetail
    Public Enum enmDemandDetail
        ArticleGenderName
        ArticleDescription
        Qty
        LocationId
        ArticleId
        CurrentPrice
        DemandAmount
        Count
    End Enum
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
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

    Public Sub FillGrid()

        Try
            Dim strQuery As String = String.Empty
            strQuery = "SP_Demand '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & IIf(Me.rbtItemName.Checked = True, 1, 0) & ""
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)

            strQuery = "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1"
            Dim dtShift As New DataTable
            dtShift = GetDataTable(strQuery)
            Dim Disp_No As Integer = 1
            For Each row As DataRow In dtShift.Rows
                If Not dtShift.Columns.Contains(row(1)) Then
                    dt.Columns.Add(row(0), GetType(System.Int16), row(0))
                    dt.Columns.Add("Dispath" & Disp_No, GetType(System.Double))
                    dt.Columns.Add("Dispath Amount" & Disp_No, GetType(System.Double))
                End If
                Disp_No += 1
            Next
            For Each row As DataRow In dt.Rows
                For c As Integer = enmDemandDetail.Count To dt.Columns.Count - 3 Step 3
                    row.BeginEdit()
                    row(c + 1) = 0
                    row(c + 2) = 0
                    row.EndEdit()
                Next
            Next
            strQuery = "SELECT dbo.SalesDetailTable.ArticleDefId, ISNULL(dbo.CompanyDefTable.CostCenterId, 0) AS ShiftGroupId, SUM(ISNULL(dbo.SalesDetailTable.Qty,0))AS Qty, SUM((ISNULL(dbo.SalesDetailTable.Qty,0))*ISNULL(CurrentPrice,0)) Amount " _
                       & " FROM dbo.SalesMasterTable INNER JOIN " _
                       & " dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId LEFT OUTER JOIN CompanyDefTable On CompanyDefTable.CompanyId = SalesMasterTable.LocationId  " _
                       & " WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " _
                       & " GROUP BY dbo.SalesDetailTable.ArticleDefId, ISNULL(dbo.CompanyDefTable.CostCenterId, 0)"
            Dim dtData As New DataTable
            dtData = GetDataTable(strQuery)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dtData.Select("ArticleDefId=" & r("ArticleId") & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            If Not dt.Columns.IndexOf(drFound(1)) + 1 = 0 Then
                                r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                                r(dt.Columns.IndexOf(drFound(1)) + 2) = drFound(3)
                            End If
                            r.EndEdit()
                        Next
                    End If
                End If
            Next


            Dim strTotal As String = String.Empty
            For c As Integer = enmDemandDetail.Count To dt.Columns.Count - 3 Step 3
                If strTotal.Length > 0 Then
                    strTotal += "+" & "[" & dt.Columns(c + 1).ColumnName & "]"
                Else
                    strTotal = "[" & dt.Columns(c + 1).ColumnName & "]"
                End If
            Next
            dt.Columns.Add("Total Dispatch", GetType(System.Double))
            dt.Columns.Add("Total Amount", GetType(System.Double))
            dt.Columns.Add("Less", GetType(System.Double))
            dt.Columns.Add("Less_Amount", GetType(System.Double))
            dt.Columns.Add("Extra", GetType(System.Double))
            dt.Columns.Add("Extra_Amount", GetType(System.Double))

            dt.AcceptChanges()
            If strTotal.Length > 0 Then
                dt.Columns("Total Dispatch").Expression = strTotal.ToString
            End If
            dt.Columns("Less").Expression = "IIF([Total Dispatch]-Qty  < 0, [Total Dispatch]-Qty,0)"
            dt.Columns("Extra").Expression = "IIF([Total Dispatch]-Qty > 0, [Total Dispatch]-Qty,0)"
            dt.Columns("Less_Amount").Expression = "Less*CurrentPrice"
            dt.Columns("Extra_Amount").Expression = "Extra*CurrentPrice"
            dt.Columns("Total Amount").Expression = "[Total Dispatch]*CurrentPrice"
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try

            For col As Integer = enmDemandDetail.Count To Me.grd.RootTable.Columns.Count - 7 Step 3
                Me.grd.RootTable.Columns(col).Visible = False
                Me.grd.RootTable.Columns(col + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(col + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.RootTable.Columns(col + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(col + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Next
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.RootTable.Columns("LocationId").Visible = False
            Me.grd.RootTable.Columns("CurrentPrice").Visible = False
            Me.grd.RootTable.Columns("ArticleGenderName").Caption = "Category"
            Dim grpCateGory As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("ArticleGenderName"))
            Me.grd.RootTable.Columns("Total Dispatch").Visible = True
            Me.grd.RootTable.Columns("Total Dispatch").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Less").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Extra").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Less").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Extra").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Dispatch").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Less_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Extra_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Demand Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Less_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Extra_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Demand Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Less_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Extra_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Demand Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("Less").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Extra").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Dispatch").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Groups.Add(grpCateGory)
            Me.grd.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Demand Report" & Chr(10) & "Date From:" & Me.dtpFrom.Value.ToString("dd-MMM-yyyy") & " To Date: " & Me.dtpTo.Value.ToString("dd-MMM-yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

  

   
    Private Sub frmGrdRptDemandDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
End Class