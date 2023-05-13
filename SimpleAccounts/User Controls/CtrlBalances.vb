Public Class CtrlBalances
    Dim lbl As New Label
    Dim flgCompanyRights As Boolean = False

    Private _MyCheckBox As New CheckBox
    Public Property MyCheckBox() As CheckBox
        Get
            Return _MyCheckBox
        End Get
        Set(ByVal value As CheckBox)
            _MyCheckBox = value
        End Set
    End Property

    Private _IncludeUnPostedVoucher As Boolean=False

    Public Property IncludeUnPostedVoucher() As Boolean
        Get
            Return _IncludeUnPostedVoucher
        End Get
        Set(ByVal value As Boolean)
            _IncludeUnPostedVoucher = value
        End Set
    End Property

    Private _dtPayables As DataTable
    Public Property dtPayables() As DataTable
        Get
            Return _dtPayables
        End Get
        Set(ByVal value As DataTable)
            _dtPayables = value
        End Set
    End Property
    Private _dtReceiveables As DataTable
    Public Property dtReceiveables() As DataTable
        Get
            Return _dtReceiveables
        End Get
        Set(ByVal value As DataTable)
            _dtReceiveables = value
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
    Public Sub GetBalances()
        Try

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            Me.Controls.Add(lbl)
            lbl.BackColor = Color.White
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.BringToFront()
            Application.DoEvents()
            Me.lbl.Text = "Loading..."

            IncludeUnPostedVoucher = Me.MyCheckBox.Checked

            If bgwUpdates.IsBusy Then Exit Sub
            bgwUpdates.RunWorkerAsync()
            Do While bgwUpdates.IsBusy
                Application.DoEvents()
            Loop

            Dim AmountPayable As Double = 0D
            Dim AmountReceivable As Double = 0D
            AmountPayable = Val(dtPayables.Rows(0).Item("Amount").ToString)
            AmountReceivable = Val(dtReceiveables.Rows(0).Item("Amount").ToString)

            Me.Label6.Text = FormatNumber(AmountPayable, 2, TriState.True)
            Me.Label7.Text = FormatNumber(AmountReceivable, 2, TriState.True)

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
    Public Function GetPayables() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select Round(ISNULL(SUM(Isnull(b.credit_amount,0)-Isnull(b.debit_Amount,0)),0),0) as Amount From tblVoucher a INNER JOIN tblVoucherDetail b On a.Voucher_Id = b.Voucher_Id INNER JOIN vwCOADetail v on b.coa_detail_id = v.coa_detail_id WHERE v.Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND v.CompanyId=" & MyCompanyId & "", "") & " AND a.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ")"
            dtPayables = GetDataTable(str)
            Return dtReceiveables
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetReceiveables() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select Round(ISNULL(SUM(Isnull(b.debit_amount,0)-Isnull(b.credit_Amount,0)),0),0) as Amount From tblVoucher a INNER JOIN tblVoucherDetail b On a.Voucher_Id = b.Voucher_Id INNER JOIN vwCOADetail v on b.coa_detail_id = v.coa_detail_id WHERE v.Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND v.CompanyId=" & MyCompanyId & "", "") & "  AND a.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ")"
            dtReceiveables = GetDataTable(str)
            Return dtReceiveables
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
    '    Try
    '        ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
    '    Try
    '        ShowReport("AgeingReceivable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            'AddRptParam("@1stAging", 60)
            'AddRptParam("@1stAgingName", "30_60")
            'AddRptParam("@1stAging", 90)
            'AddRptParam("@1stAgingName", "60_90")
            'AddRptParam("@1stAging", 90)
            'AddRptParam("@1stAgingName", "90+")
            'AddRptParam("@Aging", 30)
            'AddRptParam("@1stAging", 60)
            'AddRptParam("@1stAgingName", "30_60")
            'AddRptParam("@2ndAging", 90)
            'AddRptParam("@2ndAgingName", "60_90")
            'AddRptParam("@3rdAging", 90)
            'AddRptParam("@3rdAgingName", "90+")
            'ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)


            frmMain.LoadControl("rptTrial")
            rptTrialBalance.NoteId = 9
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Detail A/c"
            rptTrialBalance.DateTimePicker1.Value = CDate("2001-1-1 00:00:00") ' Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Date.Now.ToString("yyyy-M-d 23:59:59")
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = False
            rptTrialBalance.GetDetailAccountsTrial("Vendor")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try

            'AddRptParam("@Aging", 30)
            'AddRptParam("@1stAging", 60)
            'AddRptParam("@1stAgingName", "30_60")
            'AddRptParam("@2ndAging", 90)
            'AddRptParam("@2ndAgingName", "60_90")
            'AddRptParam("@3rdAging", 90)
            'AddRptParam("@3rdAgingName", "90+")
            'ShowReport("AgeingReceivable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)



            frmMain.LoadControl("rptTrial")
            rptTrialBalance.NoteId = 9
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Detail A/c"
            rptTrialBalance.DateTimePicker1.Value = CDate("2001-1-1 00:00:00") ' Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Date.Now.ToString("yyyy-M-d 23:59:59")
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = False
            rptTrialBalance.GetDetailAccountsTrial("Customer")


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bgwUpdates_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpdates.DoWork
        Try
            GetReceiveables()
            GetPayables()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlBalances_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork

    End Sub
End Class
