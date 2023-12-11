''11-Mar-2014 TASK:2485 Imran Ali    Sales and Purchase not Show On Dashboard
''28-Mar-2014 TASK:2525 Imran Ali Purchase Amount Exclusive Stock Receiving Amount in Dashboard Ok TubeWel
Public Class CtrlPurchase
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

    Private _dtPurchase As DataTable
    Public Property dtPurchase() As DataTable
        Get
            Return _dtPurchase
        End Get
        Set(ByVal value As DataTable)
            _dtPurchase = value
        End Set
    End Property

    Private _dtPurchaseReturn As DataTable
    Public Property dtPurchaseReturn() As DataTable
        Get
            Return _dtPurchaseReturn
        End Get
        Set(ByVal value As DataTable)
            _dtPurchaseReturn = value
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
    Private Sub CtrlPurchase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    Public Sub GetPurchase()
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
            If Me.bkgPurchase.IsBusy Then Exit Sub
            Me.bkgPurchase.RunWorkerAsync()
            Do While Me.bkgPurchase.IsBusy
                Application.DoEvents()
            Loop

            ''11-Mar-2014 TASK:2485 Imran Ali    Sales and Purchase not Show On Dashboard
            'If Not dt Is Nothing Then
            If Not dtPurchase Is Nothing Then
                'End Task:2485
                Me.lblPurchaseAmt.Text = FormatNumber(Val(dtPurchase.Rows(0).ItemArray(1)), 2, TriState.True)
                Me.lblPurchaseReturnAmt.Text = FormatNumber(Val(dtPurchase.Rows(1).ItemArray(1)), 2, TriState.True)
                Me.lblTotalPurchaseAmt.Text = FormatNumber(Val(dtPurchase.Rows(0).ItemArray(1)) - Val(dtPurchase.Rows(1).ItemArray(1)), 2, TriState.True)
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
    Private Sub bkgPurchase_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bkgPurchase.DoWork
        'Comment ''28-Mar-2014 TASK:2525 Imran Ali Purchase Amount Exclusive Stock Receiving Amount in Dashboard Ok TubeWel
        'Dim str As String = "SELECT 'Purchase' AS Type, Round(ISNULL(SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0) * ISNULL(dbo.ReceivingDetailTable.Price, 0))+SUM(((ISNULL(Qty,0)*ISNULL(Price,0))*ISNULL(TaxPercent,0))/100),0),0) AS Amount " _
        '                          & "  FROM dbo.ReceivingMasterTable INNER JOIN " _
        '                          & "  dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId WHERE (Convert(varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " and ReceivingMasterTable.LocationId=" & MyCompanyId & "", "") & " " _
        '                          & "  UNION " _
        '                          & "  SELECT 'Purchase Return' AS Type, Round(ISNULL(SUM(ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Price, 0))+SUM(((ISNULL(Qty,0)*ISNULL(Price,0))*ISNULL(Tax_Percent,0))/100),0),0) AS Amount " _
        '                          & "  FROM dbo.PurchaseReturnMasterTable INNER JOIN " _
        '                          & "  dbo.PurchaseReturnDetailTable ON dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId WHERE (Convert(varchar, PurchaseReturnDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " and PurchaseReturnMasterTable.LocationId=" & MyCompanyId & "", "") & "  "
        'dtPurchase = GetDataTable(str)
        'task:2525 Stock Receiving Exclusive Amount
        Dim str As String = "SELECT 'Purchase' AS Type, Round(ISNULL(SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0) * ISNULL(dbo.ReceivingDetailTable.Price, 0))+SUM(((ISNULL(Qty,0)*ISNULL(Price,0))*ISNULL(TaxPercent,0))/100),0),0) AS Amount " _
                                 & "  FROM dbo.ReceivingMasterTable INNER JOIN " _
                                 & "  dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId WHERE (Convert(varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " and ReceivingMasterTable.LocationId=" & MyCompanyId & "", "") & " AND LEFT (ReceivingNo,2) <> 'SR' " _
                                 & "  UNION " _
                                 & "  SELECT 'Purchase Return' AS Type, Round(ISNULL(SUM(ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Price, 0))+SUM(((ISNULL(Qty,0)*ISNULL(Price,0))*ISNULL(Tax_Percent,0))/100),0),0) AS Amount " _
                                 & "  FROM dbo.PurchaseReturnMasterTable INNER JOIN " _
                                 & "  dbo.PurchaseReturnDetailTable ON dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId WHERE (Convert(varchar, PurchaseReturnDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " and PurchaseReturnMasterTable.LocationId=" & MyCompanyId & "", "") & "  "
        'End Task:2525
        'Dim str As String = "SELECT 'Purchase' AS Type, Round(ISNULL(SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0) * ISNULL(dbo.ReceivingDetailTable.Price, 0)),0),0) AS Amount " _
        '                       & "  FROM dbo.ReceivingMasterTable INNER JOIN " _
        '                       & "  dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId WHERE (Convert(varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " and ReceivingMasterTable.LocationId=" & MyCompanyId & "", "") & " AND LEFT (ReceivingNo,2) <> 'SR' " _
        '                       & "  UNION " _
        '                       & "  SELECT 'Purchase Return' AS Type, Round(ISNULL(SUM(ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Price, 0)),0),0) AS Amount " _
        '                       & "  FROM dbo.PurchaseReturnMasterTable INNER JOIN " _
        '                       & "  dbo.PurchaseReturnDetailTable ON dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId WHERE (Convert(varchar, PurchaseReturnDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " and PurchaseReturnMasterTable.LocationId=" & MyCompanyId & "", "") & "  "
        dtPurchase = GetDataTable(str)

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Dim fromDate As String = dtpDateFrom.Value.Year & "," & dtpDateFrom.Value.Month & "," & dtpDateFrom.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpDateTo.Value.Year & "," & dtpDateTo.Value.Month & "," & dtpDateTo.Value.Day & ",23,59,59"
            AddRptParam("From", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("To", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("SummaryOfPurchaseInvoices", "{ReceivingMasterTable.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Dim fromDate As String = dtpDateFrom.Value.Year & "," & dtpDateFrom.Value.Month & "," & dtpDateFrom.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpDateTo.Value.Year & "," & dtpDateTo.Value.Month & "," & dtpDateTo.Value.Day & ",23,59,59"
            ShowReport("SummaryOfPurchaseReturn", "{ReceivingMasterTable.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
