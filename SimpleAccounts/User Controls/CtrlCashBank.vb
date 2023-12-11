Public Class CtrlCashBank
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

    Private _dtBank As DataTable
    Public Property dtBank() As DataTable
        Get
            Return _dtBank
        End Get
        Set(ByVal value As DataTable)
            _dtBank = value
        End Set
    End Property

    Private _dtCash As DataTable
    Public Property dtCash() As DataTable
        Get
            Return _dtCash
        End Get
        Set(ByVal value As DataTable)
            _dtCash = value
        End Set
    End Property

    Private _dtBankReceipt As DataTable
    Public Property dtBankReceipt() As DataTable
        Get
            Return _dtBankReceipt
        End Get
        Set(ByVal value As DataTable)
            _dtBankReceipt = value
        End Set
    End Property
    Private _dtCashReceipt As DataTable
    Public Property dtCashReceipt() As DataTable
        Get
            Return _dtCashReceipt
        End Get
        Set(ByVal value As DataTable)
            _dtCashReceipt = value
        End Set
    End Property
    Private _dtBankPayment As DataTable
    Public Property dtBankPayment() As DataTable
        Get
            Return _dtBankPayment
        End Get
        Set(ByVal value As DataTable)
            _dtBankPayment = value
        End Set
    End Property
    Private _dtCashPayment As DataTable
    Public Property dtCashPayment() As DataTable
        Get
            Return _dtCashPayment
        End Get
        Set(ByVal value As DataTable)
            _dtCashPayment = value
        End Set
    End Property

    Private _dtExpense As DataTable
    Public Property dtExpense() As DataTable
        Get
            Return _dtExpense
        End Get
        Set(ByVal value As DataTable)
            _dtExpense = value
        End Set
    End Property

    Dim DateFrom As DateTime
    Dim DateTo As DateTime


    Private Sub CtrlCashBank_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If dtpDateFrom Is Nothing Then
                dtpDateFrom = New DateTimePicker
                dtpDateFrom.Value = Now
            Else
                Me.DateFrom = dtpDateFrom.Value
            End If
            If dtpDateTo Is Nothing Then
                dtpDateTo = New DateTimePicker
                dtpDateTo.Value = Now
            Else
                Me.DateTo = dtpDateTo.Value
            End If
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetCash()
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
            Me.lbl.Text = "Loading..."


            IncludeUnPostedVoucher = Me.MyCheckBox.Checked
            If bgwUpdates.IsBusy Then Exit Sub
            bgwUpdates.RunWorkerAsync()
            Do While bgwUpdates.IsBusy
                Application.DoEvents()
            Loop

            Dim CashReceiptAmt As Double = 0
            Dim BankReceiptAmt As Double = 0
            Dim CashPaymentAmt As Double = 0
            Dim BankPaymentAmt As Double = 0
            Dim CashAmt As Double = 0
            Dim BankAmt As Double = 0

            '
            'Total Cash Receipt 
            '

            CashReceiptAmt = Val(dtCashReceipt.Rows(0).ItemArray(1))
            Me.lblCashReceiptAmount.Text = FormatNumber(CashReceiptAmt, 2, TriState.True)

            '
            'Bank Receipt 
            '

            BankReceiptAmt = dtBankReceipt.Rows(0).ItemArray(1)
            Me.lblBankReceiptAmount.Text = FormatNumber(BankReceiptAmt, 2, TriState.True)

            '
            'Total Receipt
            '
            Me.lblTotalReceiptAmount.Text = FormatNumber(CashReceiptAmt + BankReceiptAmt, 2, TriState.True)
            '

            '
            'Bank Payment
            '

            BankPaymentAmt = Val(dtBankPayment.Rows(0).ItemArray(1))
            Me.lblBankPaymentAmount.Text = FormatNumber(BankPaymentAmt, 2, TriState.True)

            '
            'Cash Payment
            '
            CashPaymentAmt = Val(dtCashPayment.Rows(0).ItemArray(1))
            Me.lblCashPaymentAmount.Text = FormatNumber(CashPaymentAmt, 2, TriState.True)

            '
            'Total Payments
            '
            Me.lblTotalPayment.Text = FormatNumber(CashPaymentAmt + BankPaymentAmt, 0)

            '
            'Cash Balance
            '

            CashAmt = Val(dtCash.Rows(0).ItemArray(1))
            Me.Label6.Text = FormatNumber(CashAmt, 2, TriState.True)

            '
            'Bank Balance
            '

            BankAmt = Val(dtBank.Rows(0).ItemArray(1))
            Me.Label9.Text = FormatNumber(BankAmt, 2, TriState.True)

            '
            'Total Balance
            '
            Me.Label7.Text = FormatNumber(BankAmt + CashAmt, 2, TriState.True)
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
    Public Function Expense() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select 'Expense'  as Detail, ISNULL(Round(sum(isnull(a.debit_Amount,0)),0),0)  as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Expense' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(varchar, b.voucher_date,102) between  Convert(DateTime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""

            'str = "select 'Expense'  as Detail, ISNULL(Round(sum(isnull(a.debit_Amount,0)),0),0)  as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Expense' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(varchar, b.voucher_date,102) <= Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            dtExpense = GetDataTable(str)
            Return dtExpense
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CashReceipt() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select  'Cash Receipt' as Detail, ISNULL(Round(sum(isnull(a.debit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Cash' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) between Convert(DateTime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""

            'str = "select  'Cash Receipt' as Detail, ISNULL(Round(sum(isnull(a.debit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Cash' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) <= Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            dtCashReceipt = GetDataTable(str)
            Return dtCashReceipt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CashPayment() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select  'Cash Payment' as Detail, ISNULL(Round(sum(isnull(a.credit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Cash' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) between  Convert(DateTime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""

            'str = "select  'Cash Payment' as Detail, ISNULL(Round(sum(isnull(a.credit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Cash' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) <= Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            dtCashPayment = GetDataTable(str)
            Return dtCashPayment
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BankPayment() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select  'Bank Payment' as Detail, ISNULL(Round(sum(isnull(a.credit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Bank' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) between  Convert(DateTime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""

            'str = "select  'Bank Payment' as Detail, ISNULL(Round(sum(isnull(a.credit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Bank' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) <= Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            dtBankPayment = GetDataTable(str)
            Return dtBankPayment
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BankReceipt() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select  'Bank Receipt' as Detail, ISNULL(Round(sum(isnull(a.debit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Bank' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) between  Convert(DateTime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""

            'str = "select  'Bank Receipt' as Detail, ISNULL(Round(sum(isnull(a.debit_amount,0)),0),0) as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Bank' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) <= Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            dtBankReceipt = GetDataTable(str)
            Return dtBankReceipt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Cash() As DataTable
        Try
            Dim str As String = String.Empty
            'Without Dashboard date condition
            str = "SELECT 'Cash' AS Type, ISNULL(SUM(ISNULL(a.debit_amount, 0) - ISNULL(a.credit_amount, 0)),0) AS Amount FROM  dbo.tblVoucherDetail a INNER JOIN  dbo.tblVoucher b ON a.voucher_id = b.voucher_id INNER JOIN dbo.vwCOADetail c ON a.coa_detail_id = c.coa_detail_id WHERE (c.account_type = 'Cash') AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ")  " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            'With Dashboard Date Condition
            'str = "SELECT 'Cash' AS Type, ISNULL(SUM(ISNULL(a.debit_amount, 0) - ISNULL(a.credit_amount, 0)),0) AS Amount FROM  dbo.tblVoucherDetail a INNER JOIN  dbo.tblVoucher b ON a.voucher_id = b.voucher_id INNER JOIN dbo.vwCOADetail c ON a.coa_detail_id = c.coa_detail_id WHERE (c.account_type = 'Cash') AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) <= Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102))" & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            dtCash = GetDataTable(str)
            Return dtCash
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Bank() As DataTable
        Try
            Dim str As String = String.Empty
            ' Without Dashboard Date Condtion
            str = "SELECT  'Bank' AS Type, ISNULL(SUM(ISNULL(a.debit_amount, 0) - ISNULL(a.credit_amount, 0)),0) AS Amount FROM  dbo.tblVoucherDetail a INNER JOIN  dbo.tblVoucher b ON a.voucher_id = b.voucher_id INNER JOIN dbo.vwCOADetail c ON a.coa_detail_id = c.coa_detail_id WHERE (c.account_type = 'Bank') AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ")  " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            ' With Dashboard Date Condition
            'str = "SELECT  'Bank' AS Type, ISNULL(SUM(ISNULL(a.debit_amount, 0) - ISNULL(a.credit_amount, 0)),0) AS Amount FROM  dbo.tblVoucherDetail a INNER JOIN  dbo.tblVoucher b ON a.voucher_id = b.voucher_id INNER JOIN dbo.vwCOADetail c ON a.coa_detail_id = c.coa_detail_id WHERE (c.account_type = 'Bank') AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) <= Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & "", "") & ""
            dtBank = GetDataTable(str)
            Return dtBank
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub bgwUpdates_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpdates.DoWork
        'GetHomeDate()
        Expense()
        CashReceipt()
        CashPayment()
        BankPayment()
        BankReceipt()
        Cash()
        Bank()
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Dim opening As Integer = GetAccountOpeningBalance(0, Date.Now.Year & "-" & Date.Now.Month & "-" & Date.Now.Day & " 00:00:00", "Cash")
            'AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            'AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("FromDate", DateFrom.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", DateTo.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptCashFlowStatementNew", , , , , Val(opening).ToString, , GetCashAndBankData("Cash"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Dim opening As Integer = GetAccountOpeningBalance(0, Date.Now.Year & "-" & Date.Now.Month & "-" & Date.Now.Day & " 00:00:00", "Bank")
            'AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            'AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("FromDate", DateFrom.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", DateTo.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptCashFlowStatementNew", , , , , Val(opening).ToString, , GetCashAndBankData("Bank"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetCashAndBankData(Optional ByVal Condition As String = "") As DataTable
        Try

            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT dbo.tblDefVoucherType.voucher_type, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, dbo.tblVoucherDetail.coa_detail_id, " _
            & "           dbo.tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.credit_amount, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title,  " _
            & "           ISNULL(dbo.tblVoucherDetail.CostCenterID, 0) AS CostCenterID, dbo.tblDefCostCenter.Name AS CostCenter, dbo.tblVoucher.post,  " _
            & "           dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, tblVoucherDetail.Comments as Description " _
            & "           FROM dbo.tblVoucherDetail INNER JOIN " _
            & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
            & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
            & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            & "           dbo.tblDefCostCenter ON dbo.tblVoucherDetail.CostCenterID = dbo.tblDefCostCenter.CostCenterID " _
            & "           WHERE (dbo.tblVoucher.voucher_id IN " _
            & "           (SELECT DISTINCT tblvoucher.voucher_id " _
            & "           FROM dbo.tblVoucherDetail INNER JOIN " _
            & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
            & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
            & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id "
            str += " WHERE (Convert(varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) AND dbo.tblVoucher.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and dbo.vwCOADetail.detail_code  is not null " & IIf(MyCompanyId > 0, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & ""
            str += " " & IIf(Condition = "Cash", " AND vwcoadetail.account_type = 'Cash')) AND (dbo.vwCOADetail.account_type NOT IN ('Cash')) ", "") & " "
            str += " " & IIf(Condition = "Bank", " AND vwcoadetail.account_type = 'Bank')) AND (dbo.vwCOADetail.account_type NOT IN ('Bank')) ", "") & " "
            str += " " & IIf(Condition = "All", " AND(vwcoadetail.account_type = 'Cash' OR vwcoadetail.account_type = 'Bank'))) AND (dbo.vwCOADetail.account_type NOT IN ('Cash','Bank')) ", "") & ""
            'str += " " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & ""
            dt = GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetCash(ByVal TransType As String, Optional ByVal Condition As String = "") As DataTable
        Try

            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT dbo.tblDefVoucherType.voucher_type, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, dbo.tblVoucherDetail.coa_detail_id, " _
            & "           " & IIf(TransType = "Payment", "tblvoucherdetail.debit_amount, tblvoucherdetail.credit_amount", "tblvoucherdetail.debit_amount, tblvoucherdetail.credit_amount") & ",  dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title,  " _
            & "           ISNULL(dbo.tblVoucherDetail.CostCenterID, 0) AS CostCenterID, dbo.tblDefCostCenter.Name AS CostCenter, dbo.tblVoucher.post,  " _
            & "           dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, tblVoucherDetail.Comments as Description " _
            & "           FROM dbo.tblVoucherDetail INNER JOIN " _
            & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
            & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
            & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            & "           dbo.tblDefCostCenter ON dbo.tblVoucherDetail.CostCenterID = dbo.tblDefCostCenter.CostCenterID " _
            & "           WHERE (dbo.tblVoucher.voucher_id IN " _
            & "           (SELECT DISTINCT tblvoucher.voucher_id " _
            & "           FROM dbo.tblVoucherDetail INNER JOIN " _
            & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
            & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
            & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id "
            str += " WHERE (Convert(varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(Datetime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) AND dbo.tblVoucher.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") " & IIf(MyCompanyId > 0, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & ""
            str += " " & IIf(Condition = "Cash", " AND vwcoadetail.account_type = 'Cash' AND tblvoucher.voucher_type_id=" & IIf(TransType = "Payment", 2, 3) & ")) AND (dbo.vwCOADetail.account_type NOT IN ('Cash')) ", "") & " "
            str += " " & IIf(Condition = "Bank", " AND vwcoadetail.account_type = 'Bank' AND tblvoucher.voucher_type_id=" & IIf(TransType = "Payment", 4, 5) & ")) AND (dbo.vwCOADetail.account_type NOT IN ('Bank')) ", "") & " "
            str += " " & IIf(Condition = "All", " AND(vwcoadetail.account_type = 'Cash' OR vwcoadetail.account_type = 'Bank'))) AND (dbo.vwCOADetail.account_type NOT IN ('Cash','Bank')) ", "") & ""
            'str += " " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & ""
            dt = GetDataTable(str)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

 
    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Try
            Dim opening As Integer = GetAccountOpeningBalance(1, Date.Now.Year & "-" & Date.Now.Month & "-" & Date.Now.Day & " 00:00:00", "Cash")
            'AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            'AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("FromDate", DateFrom.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", DateTo.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptReceiptDetail", , , , , Val(opening).ToString, , GetCash("Receipt", "Cash"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel4_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Try
            Dim opening As Integer = GetAccountOpeningBalance(1, Date.Now.Year & "-" & Date.Now.Month & "-" & Date.Now.Day & " 00:00:00", "Bank")
            'AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            'AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("FromDate", DateFrom.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", DateTo.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptReceiptDetail", , , , , Val(opening).ToString, , GetCash("Receipt", "Bank"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel5_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Try
            Dim opening As Integer = GetAccountOpeningBalance(1, Date.Now.Year & "-" & Date.Now.Month & "-" & Date.Now.Day & " 00:00:00", "Cash")
            'AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            'AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("FromDate", DateFrom.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", DateTo.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptPaymentDetail", , , , , Val(opening).ToString, , GetCash("Payment", "Cash"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel6_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        Try
            Dim opening As Integer = GetAccountOpeningBalance(1, Date.Now.Year & "-" & Date.Now.Month & "-" & Date.Now.Day & " 00:00:00", "Bank")
            'AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            'AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("FromDate", DateFrom.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", DateTo.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptPaymentDetail", , , , , Val(opening).ToString, , GetCash("Payment", "Bank"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Public Sub GetHomeDate()
    '    Try

    '        Dim home As New frmHome
    '        DateFrom = home.FromDate.Value
    '        DateTo = home.ToDate.Value
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

End Class
