'' Task 20150204-01 Credit And Cash Sales Display, Imran Ali
Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class urPurchaseType

    Public DateFrom As DateTime  'ToDo Filter Query Date Range
    Public DateTo As DateTime  'ToDo Filter Query Date Range
    Dim flgCompanyRights As Boolean = False
    Dim _dtData As DataTable
    Private _dtpDateFrom As New DateTimePicker

    Public Property dtpDateFrom() As DateTimePicker
        Get
            Return _dtpDateFrom
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateFrom = value
        End Set
    End Property

    Private _dtpDateTo As New DateTimePicker

    Public Property dtpDateTo() As DateTimePicker
        Get
            Return _dtpDateTo
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateTo = value
        End Set
    End Property
    Public Sub GetSales(Optional ByVal Condition As String = "")
        Dim lbl As New Label
        Me.Controls.Add(lbl)
        lbl.BackColor = Color.White
        lbl.AutoSize = False
        lbl.Dock = DockStyle.Fill
        lbl.TextAlign = ContentAlignment.MiddleCenter
        lbl.BringToFront()
        Application.DoEvents()
        lbl.Text = "Loading..."
        Try

            DateFrom = dtpDateFrom.Value
            DateTo = dtpDateTo.Value

            If Me.BackgroundWorker1.IsBusy Then Exit Sub
            Me.BackgroundWorker1.RunWorkerAsync()
            Do While Me.BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop

            If Me.BackgroundWorker2.IsBusy Then Exit Sub
            Me.BackgroundWorker2.RunWorkerAsync()
            Do While Me.BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop

        Catch ex As Exception
            Throw ex
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub urSalesType_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If Con.State = ConnectionState.Closed Then Con.Open() 'Open Database Connection
        Try

            'Set SQL Select Statment in strSQL
            Dim strSQL As String = "SELECT 'Purchase' AS Type, Round(ISNULL(SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0) * ISNULL(dbo.ReceivingDetailTable.Price, 0))+SUM(((isnull(Qty,0)*ISNULL(Price,0))*isnull(TaxPercent,0))/100),0),0) AS Amount " _
                                              & "  FROM dbo.ReceivingMasterTable INNER JOIN " _
                                              & "  dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId WHERE (dbo.ReceivingMasterTable.InvoiceType='Cash') AND (Convert(varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(flgCompanyRights = True, " AND ReceivingMasterTable.LocationId=" & MyCompanyId & " ", "") & ""
            Dim dt As New DataTable 'Create object of datatable
            dt = GetDataTable(strSQL) 'Using datatable function (GetDataTable()) to assign dt object.
            dt.AcceptChanges() 'Update Record In DataTable


            _dtData = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            'Con.Close()
        End Try
    End Sub
    Public Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        If Con.State = ConnectionState.Closed Then Con.Open() 'Open Database Connection
        Try

            'Set SQL Select Statment in strSQL
            Dim strSQL As String = "SELECT 'Purchase' AS Type, Round(ISNULL(SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0) * ISNULL(dbo.ReceivingDetailTable.Price, 0))+SUM(((isnull(Qty,0)*ISNULL(Price,0))*isnull(TaxPercent,0))/100),0),0) AS Amount " _
                                              & "  FROM dbo.ReceivingMasterTable INNER JOIN " _
                                              & "  dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId WHERE (IsNull(dbo.ReceivingMasterTable.InvoiceType,'Credit')='Credit') AND (Convert(varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(flgCompanyRights = True, " AND ReceivingMasterTable.LocationId=" & MyCompanyId & " ", "") & ""
            Dim dt As New DataTable 'Create object of datatable
            dt = GetDataTable(strSQL) 'Using datatable function (GetDataTable()) to assign dt object.
            dt.AcceptChanges() 'Update Record In DataTable

            _dtData = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            'Con.Close()
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            Me.lblCashSales.Text = Val(_dtData.Rows(0).Item(1).ToString)
            Me.lblTotal.Text = Val(Me.lblCashSales.Text) + Val(Me.lblCreditSales.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        Try
            Me.lblCreditSales.Text = Val(_dtData.Rows(0).Item(1).ToString)
            Me.lblTotal.Text = Val(Me.lblCashSales.Text) + Val(Me.lblCreditSales.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
