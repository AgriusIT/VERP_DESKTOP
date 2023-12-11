'23-Aug-2014 TAsk:2803 Imran Ali Revised Post Dated Cheque Summary On Daashboard/Reports
Public Class CrtlPostDatedCheques
    Dim lbl As New Label
    Dim dt As DataTable
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

    Private Sub CtrlCashBank_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

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
    Public Sub GetPostDatedCheque()
        Try

            DateFrom = Me.dtpDateFrom.Value
            DateTo = Me.dtpDateTo.Value

            Me.Controls.Add(lbl)
            lbl.BackColor = Color.White
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.BringToFront()
            Application.DoEvents()
            Me.lbl.Text = "Loading Please Wait ..."
            Dim lblCheques As New Label
            Dim lblChequeAmount As New Label

            IncludeUnPostedVoucher = MyCheckBox.Checked

            If bgwUpdates.IsBusy Then Exit Sub
            bgwUpdates.RunWorkerAsync()
            Do While bgwUpdates.IsBusy
                Application.DoEvents()
            Loop

            Me.Label3.Text = GetTodayCheque.Rows(0).Item(0)
            Me.Label4.Text = GetTomorrowCheque.Rows(0).Item(0)


            Me.lbl.Visible = False

        Catch ex As OleDb.OleDbException
            If ex.ErrorCode = "-2146232060" Then
                Me.lbl.Text = "Could not connect to the database server, please check server"
            Else
                Me.lbl.Text = ex.Message.ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetTodayCheque() As DataTable
        Try
            Dim str As String = String.Empty
            'Before agains task:2803
            'str = "Select Count(Cheque_No) as CntCheque From tblVoucher WHERE (Convert(varchar, Cheque_Date, 102) = Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102)) "
            'Task:2803 Change Query
            str = "Select Count(cntCheque) as CntCheque From(Select Count(Cheque_No) as CntCheque From tblVoucherDetail WHERE (Convert(varchar, Cheque_Date, 102) = Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102)) AND Cheque_No <> '' Group By Cheque_No)a"
            'End Task:2803
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTomorrowCheque() As DataTable
        Try
            Dim str As String = String.Empty
            'Before against task:2803
            'str = "Select Count(Cheque_No) as CntCheque From tblVoucher WHERE (Convert(varchar, Cheque_Date, 102) = Convert(Datetime, '" & DateFrom.AddDays(1).ToString("yyyy-M-d 00:00:00") & "', 102)) "
            'Task:2803 Change Query 
            str = "Select Count(cntCheque) as CntCheque From (Select Count(Cheque_No) as CntCheque From tblVoucherDetail WHERE (Convert(varchar, Cheque_Date, 102) = Convert(Datetime, '" & DateFrom.AddDays(1).ToString("yyyy-M-d 00:00:00") & "', 102)) AND Cheque_No <> '' Group By Cheque_No)a "
            'End Task:2803

            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
    '    Try
    '        ShowReport("PostDatedCheques", , , , False, , , Me.ReportQuery("Today"))
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
    '    Try
    '        ShowReport("PostDatedCheques", , , , False, , , Me.ReportQuery("Tomorrow"))
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Function ReportQuery(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim str As String = String.Empty
            'Before against task:2803
            'Dim dt As New DataTable
            'Dim adp As OleDb.OleDbDataAdapter
            'If Condition = "Today" Then
            '    str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
            '           & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V.Cheque_No, V.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
            '           & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
            '           & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
            '           & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
            '           & "  (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
            '           & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
            '           & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, GetDate(), 102)) " _
            '           & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
            '           & "   Where (convert(varchar, v.cheque_date,102) = Convert(Datetime, GetDate(), 102)) ORDER BY V.Voucher_code asc"
            'ElseIf Condition = "Tomorrow" Then
            '    str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
            '                           & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V.Cheque_No, V.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
            '                           & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
            '                           & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
            '                           & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
            '                           & "  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
            '                           & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
            '                           & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, GetDate(), 102)) " _
            '                           & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
            '                           & "   Where (convert(varchar, v.cheque_date,102) = Convert(Datetime, DateAdd(d,1, GetDate()), 102)) ORDER BY V.Voucher_code asc"

            'End If
            'TAsk:2803 Cheque Query
            'If Condition = "Today" Then
            '    str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
            '           & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
            '           & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
            '           & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
            '           & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
            '           & "  (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
            '           & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
            '           & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
            '           & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
            '           & "   Where (convert(varchar, V_D.Cheque_Date,102) = Convert(Datetime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) ORDER BY V.Voucher_code asc"
            'ElseIf Condition = "Tomorrow" Then
            '    str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
            '                           & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
            '                           & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
            '                           & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
            '                           & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
            '                           & "  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
            '                           & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
            '                           & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
            '                           & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
            '                           & "   Where (convert(varchar, V_D.Cheque_Date,102) = Convert(Datetime, '" & Me.dtpDateFrom.Value.AddDays(1).ToString("yyyy-M-d 00:00:00") & "', 102)) ORDER BY V.Voucher_code asc"

            'End If
            If Condition = "Today" Then
                str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
                       & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
                       & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
                       & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                       & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
                       & "  (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
                       & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                       & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
                       & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
                       & "   Where (convert(varchar, V_D.Cheque_Date,102) = Convert(Datetime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) ORDER BY V.Voucher_code asc"
            ElseIf Condition = "Tomorrow" Then
                str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
                                       & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
                                       & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
                                       & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                                       & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
                                       & "  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
                                       & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                                       & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
                                       & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
                                       & "   Where (convert(varchar, V_D.Cheque_Date,102) = Convert(Datetime, '" & Me.dtpDateFrom.Value.AddDays(1).ToString("yyyy-M-d 00:00:00") & "', 102)) ORDER BY V.Voucher_code asc"

            End If
            'End Task:2803
            'adp = New OleDb.OleDbDataAdapter(str, Con)
            'adp.Fill(dt)
            dt = GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            ShowReport("PostDatedCheques", , , , False, , , Me.ReportQuery("Today"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            ShowReport("PostDatedCheques", , , , False, , , Me.ReportQuery("Tomorrow"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bgwUpdates_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpdates.DoWork
        GetTomorrowCheque()
        GetTodayCheque()
    End Sub
End Class
