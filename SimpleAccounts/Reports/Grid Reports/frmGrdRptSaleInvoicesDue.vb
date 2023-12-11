Public Class frmGrdRptSaleInvoicesDue

    Private Sub FillGrid()

        Try
            Dim dt As New DataTable
            Dim strSql As String = String.Empty
            strSql = "SELECT dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.LocationId, dbo.SalesMasterTable.SalesNo,Convert(datetime,dbo.SalesMasterTable.SalesDate,102) as SalesDate,Convert(datetime,DATEADD(d, " _
            & " ISNULL(dbo.SalesMasterTable.DueDays, 0), dbo.SalesMasterTable.SalesDate),102) AS [Due Date], dbo.SalesMasterTable.SalesAmount, dbo.vwCOADetail.detail_code, " _
            & " dbo.vwCOADetail.detail_title FROM  dbo.SalesMasterTable INNER JOIN dbo.vwCOADetail ON dbo.SalesMasterTable.CustomerCode = dbo.vwCOADetail.coa_detail_id WHERE (dbo.SalesMasterTable.DueDays <> 0)"

            dt = GetDataTable(strSql)
            dt.AcceptChanges()

            Me.grdHistory.DataSource = dt
            Me.grdHistory.RetrieveStructure()

            Me.grdHistory.RootTable.Columns("detail_title").Caption = "Customer Name"
            Me.grdHistory.RootTable.Columns("detail_code").Caption = "Customer Code"

            Me.grdHistory.RootTable.Columns("SalesId").Visible = False
            Me.grdHistory.RootTable.Columns("LocationId").Visible = False

            Me.grdHistory.RootTable.Columns("SalesDate").FormatString = "dd/MMM/yyyy"
            Me.grdHistory.RootTable.Columns("Due Date").FormatString = "dd/MMM/yyyy"

            Me.grdHistory.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If

            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Frequently Sales Order Items"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmGrdRptSaleInvoicesDue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class