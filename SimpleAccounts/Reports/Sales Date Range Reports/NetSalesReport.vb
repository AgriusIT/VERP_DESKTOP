Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class NetSalesReport
    Public flgCompanyRights As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim fromDate As String = Me.DateTimePicker1.Value.Year & "," & Me.DateTimePicker1.Value.Month & "," & Me.DateTimePicker1.Value.Day & ",0,0,0"
            Dim ToDate As String = Me.DateTimePicker2.Value.Year & "," & Me.DateTimePicker2.Value.Month & "," & Me.DateTimePicker2.Value.Day & ",23,59,59"
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
            ShowReport("rptNetSales", , , , , , , GetNetSalesReportData)
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        CallShowReport(True)
    End Sub
    Public Function GetNetSalesReportData() As DataTable
        Try
            ''TASK TFS2160 New columns 'Voucher No, Remarks, UOM' required 
            Dim strQuery As String = String.Empty
            strQuery = "SELECT detail_code, detail_title, ArticleCode, ArticleDescription, Color, Size, ISNULL(Price, 0) AS Price, SUM(ISNULL(SalesQty, 0)) " _
                  & "  AS SalesQty, SUM(ISNULL(ReturnQty, 0)) AS ReturnQty,  DocumentNo, Remarks, UOM " _
                  & "  FROM (SELECT dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription,  " _
                  & "  dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, dbo.SalesDetailTable.Price,  " _
                  & "  SUM(dbo.SalesDetailTable.Qty) AS SalesQty, 0 AS ReturnQty, dbo.SalesMasterTable.SalesNo AS DocumentNo, dbo.SalesMasterTable.Remarks, dbo.ArticleDefView.ArticleUnitName AS UOM " _
                  & "  FROM dbo.SalesDetailTable INNER JOIN " _
                  & "  dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId INNER JOIN " _
                  & "  dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId INNER JOIN " _
                  & "  dbo.vwCOADetail ON dbo.SalesMasterTable.CustomerCode = dbo.vwCOADetail.coa_detail_id "
            strQuery += " WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
            If Me.cmbCompany.SelectedIndex > 0 Then
                strQuery += " AND SalesMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & ""
            End If
            If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                strQuery += " AND SalesMasterTable.CustomerCode=" & Me.cmbCustomer.Value & ""
            End If
            strQuery += "  GROUP BY dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription,  " _
                  & "  dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, dbo.SalesDetailTable.Price, dbo.SalesMasterTable.SalesNo, dbo.SalesMasterTable.Remarks, dbo.ArticleDefView.ArticleUnitName " _
                  & "  UNION ALL " _
                  & "  SELECT vwCOADetail_1.detail_code, vwCOADetail_1.detail_title, ArticleDefView_1.ArticleCode, ArticleDefView_1.ArticleDescription,  " _
                  & "  ArticleDefView_1.ArticleColorName AS Color, ArticleDefView_1.ArticleSizeName AS Size, dbo.SalesReturnDetailTable.Price,  " _
                  & "  0 AS SalesQty, SUM(dbo.SalesReturnDetailTable.Qty) AS ReturnQty, dbo.SalesReturnMasterTable.SalesReturnNo AS DocumentNo, dbo.SalesReturnMasterTable.Remarks, ArticleDefView_1.ArticleUnitName AS UOM  " _
                  & "  FROM dbo.SalesReturnDetailTable INNER JOIN " _
                  & "  dbo.ArticleDefView AS ArticleDefView_1 ON dbo.SalesReturnDetailTable.ArticleDefId = ArticleDefView_1.ArticleId INNER JOIN " _
                  & "  dbo.SalesReturnMasterTable ON dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId INNER JOIN  " _
                  & "  dbo.vwCOADetail AS vwCOADetail_1 ON dbo.SalesReturnMasterTable.CustomerCode = vwCOADetail_1.coa_detail_id "
            strQuery += " WHERE (Convert(Varchar, SalesReturnMasterTable.SalesReturnDate, 102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
            If Me.cmbCompany.SelectedIndex > 0 Then
                strQuery += " AND SalesReturnMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & ""
            End If
            If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                strQuery += " AND SalesReturnMasterTable.CustomerCode=" & Me.cmbCustomer.Value & ""
            End If
            strQuery += "  GROUP BY vwCOADetail_1.detail_code, vwCOADetail_1.detail_title, ArticleDefView_1.ArticleCode, ArticleDefView_1.ArticleDescription,  " _
            & "  ArticleDefView_1.ArticleColorName, ArticleDefView_1.ArticleSizeName, dbo.SalesReturnDetailTable.Price, dbo.SalesReturnMasterTable.SalesReturnNo, dbo.SalesReturnMasterTable.Remarks, ArticleDefView_1.ArticleUnitName)  " _
            & "  AS derivedtbl_1 " _
            & "  GROUP BY detail_code, detail_title, ArticleCode, ArticleDescription, Color, Size, ISNULL(Price, 0), DocumentNo, Remarks, UOM "
            Dim dtSale As New DataTable
            dtSale = GetDataTable(strQuery)
            Return dtSale
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DateTimePicker1.Value = Date.Today
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            Me.DateTimePicker2.Value = Date.Today
        End If
    End Sub
    Private Sub NetSalesReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            Me.pnlInvType.Visible = True
            Me.pnlPeriod.Visible = True
            Me.pnlVendorCustomer.Visible = True
            Me.cmbPeriod.Text = "Current Month"
            Me.Text = "Net Sales Report"
            FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
            Dim Str As String = "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE 1=1 " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
            ''Start TFS2124
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (account_type = 'Customer')  "
            Else
                Str += " AND (account_type in('Customer','Vendor'))  "
            End If
            Str += " ORDER BY 2 ASC"
            ''End TFS2124
            FillUltraDropDown(cmbCustomer, Str)
            Me.cmbCustomer.Rows(0).Activate()
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class