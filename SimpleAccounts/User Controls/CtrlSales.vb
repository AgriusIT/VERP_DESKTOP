''11-Mar-2014 TASK:2485 Imran Ali    Sales and Purchase not Show On Dashboard
'2015-06-27 Task#201506030 Rectifying Sales Report Selection Ali Ansari
Public Class CtrlSales
    Dim lbl As New Label
    Dim dt As DataTable
    Dim flgCompanyRights As Boolean = False
    Dim DateFrom As DateTime
    Dim DateTo As DateTime


    Private _MyCheckBox As New CheckBox
    Public Property MyCheckBox() As CheckBox
        Get
            Return _MyCheckBox
        End Get
        Set(ByVal value As CheckBox)
            _MyCheckBox = value
        End Set
    End Property

    Private _IncludeUnPostedVoucher As Boolean = False

    Public Property IncludeUnPostedVoucher() As Boolean
        Get
            Return _IncludeUnPostedVoucher
        End Get
        Set(ByVal value As Boolean)
            _IncludeUnPostedVoucher = value
        End Set
    End Property



    Private _dtSales As DataTable
    Public Property dtSales() As DataTable
        Get
            Return _dtSales
        End Get
        Set(ByVal value As DataTable)
            _dtSales = value
        End Set
    End Property
    Private _dtSalesReturn As DataTable
    Public Property dtSalesReturn() As DataTable
        Get
            Return _dtSalesReturn
        End Get
        Set(ByVal value As DataTable)
            _dtSalesReturn = value
        End Set
    End Property

    Private _dtpDateFrom As DateTimePicker
    Public Property dtpDateFrom() As DateTimePicker
        Get
            Return _dtpDateFrom
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateFrom = value

        End Set
    End Property

    Private _dtpDateTo As DateTimePicker
    Public Property dtpDateTo() As DateTimePicker
        Get
            Return _dtpDateTo
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateTo = value
        End Set
    End Property
    Private Sub CtrlSales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If dtpDateFrom Is Nothing Then
                dtpDateFrom = New DateTimePicker
                dtpDateFrom.Value = Now
            End If
            If dtpDateTo Is Nothing Then
                dtpDateTo = New DateTimePicker
                dtpDateTo.Value = Now
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetSales()
        Try

            DateFrom = Me.dtpDateFrom.Value
            DateTo = Me.dtpDateTo.Value

            Me.Controls.Add(lbl)
            Me.lbl.BackColor = Color.White
            Me.lbl.AutoSize = False
            Me.lbl.Dock = DockStyle.Fill
            Me.lbl.TextAlign = ContentAlignment.MiddleCenter
            Me.lbl.BringToFront()


            Me.lbl.Text = "Loading..."
            Application.DoEvents()
            IncludeUnPostedVoucher = Me.MyCheckBox.Checked

            If Me.bkgSales.IsBusy Then Exit Sub
            Me.bkgSales.RunWorkerAsync()
            Do While Me.bkgSales.IsBusy
                Application.DoEvents()
            Loop

            ''11-Mar-2014 TASK:2485 Imran Ali    Sales and Purchase not Show On Dashboard
            'If Not dt Is Nothing Then
            If Not dtSales Is Nothing Then
                'End Task:2485
                Me.lblSalesAmt.Text = FormatNumber(Val(dtSales.Rows(0).ItemArray(1)), 2, TriState.True)
                Me.lblSalesReturnAmt.Text = FormatNumber(Val(dtSales.Rows(1).ItemArray(1)), 2, TriState.True)
                Me.lblTotalSalesAmt.Text = FormatNumber(Val(dtSales.Rows(0).ItemArray(1)) - Val(dtSales.Rows(1).ItemArray(1)), 2, TriState.True)
            End If

            Me.lbl.Visible = False
        Catch ex As OleDb.OleDbException
            If ex.ErrorCode = "-2146232060" Then
                Me.lbl.Text = "Could not connect to the database, please check server"
            Else
                Me.lbl.Text = ex.Message.ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub bkgSales_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bkgSales.DoWork
        Dim str As String = "SELECT 'Sales' AS Type, Round(ISNULL(SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))+SUM(((isnull(Qty,0)*ISNULL(Price,0))*isnull(TaxPercent,0))/100),0),0) AS Amount " _
                                  & "  FROM dbo.SalesMasterTable INNER JOIN " _
                                  & "  dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(varchar, SalesDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(flgCompanyRights = True, " AND SalesMasterTable.LocationId=" & MyCompanyId & " ", "") & " " _
                                  & "  UNION " _
                                  & "  SELECT 'Sales Return' AS Type, Round(ISNULL(SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0) * ISNULL(dbo.SalesReturnDetailTable.Price, 0))+SUM(((ISNULL(Qty,0)*ISNULL(Price,0))*ISNULL(Tax_Percent,0))/100),0),0) AS Amount " _
                                  & "  FROM dbo.SalesReturnMasterTable INNER JOIN " _
                                  & "  dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId WHERE (Convert(varchar, SalesReturnDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " AND SalesReturnMasterTable.LocationId=" & MyCompanyId & " ", "") & " "
        'Dim str As String = "SELECT 'Sales' AS Type, Round(ISNULL(SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)),0),0) AS Amount " _
        '                         & "  FROM dbo.SalesMasterTable INNER JOIN " _
        '                         & "  dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(varchar, SalesDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(flgCompanyRights = True, " AND SalesMasterTable.LocationId=" & MyCompanyId & " ", "") & " " _
        '                         & "  UNION " _
        '                         & "  SELECT 'Sales Return' AS Type, Round(ISNULL(SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0) * ISNULL(dbo.SalesReturnDetailTable.Price, 0)),0),0) AS Amount " _
        '                         & "  FROM dbo.SalesReturnMasterTable INNER JOIN " _
        '                         & "  dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId WHERE (Convert(varchar, SalesReturnDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " AND SalesReturnMasterTable.LocationId=" & MyCompanyId & " ", "") & " "
        dtSales = GetDataTable(str)
        'Return dtSales
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try

            Dim fromDate As String = dtpDateFrom.Value.Year & "," & dtpDateFrom.Value.Month & "," & dtpDateFrom.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpDateTo.Value.Year & "," & dtpDateTo.Value.Month & "," & dtpDateTo.Value.Day & ",23,59,59"

            AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            'Marked Against Task#201506030 Rectifying Report Selection Ali Ansari
            'ShowReport("SummaryOfInvoices", "{SalesMasterTable.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")
            'Marked Against Task#201506030 Rectifying Report Selection Ali Ansari

            'Altered Against Task#201506030 Rectifying Report Selection Ali Ansari
            ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")
            'Altered Against Task#201506030 Rectifying Report Selection Ali Ansari

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Dim fromDate As String = dtpDateFrom.Value.Year & "," & dtpDateFrom.Value.Month & "," & dtpDateFrom.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpDateTo.Value.Year & "," & dtpDateTo.Value.Month & "," & dtpDateTo.Value.Day & ",23,59,59"
            AddRptParam("@FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblSale_Click(sender As Object, e As EventArgs) Handles lblSale.Click

    End Sub
End Class
