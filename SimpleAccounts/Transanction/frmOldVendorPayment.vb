''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''19-Dec-2013 R883  Imran Ali          limit on advance Vendor payments
''30-Dec-2013 R955      Imran Ali              Payee Title 
''13-Jan-2014   Task:2375        Imran Ali        Covnerter Problems And Development
''20-Jan-2014     TASK:2383         Imran Ali         Payment Not Update If Auto Cheque book On  
''29-Jan-2014       TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''27-Feb-2014 Task:2443   Imran Ali  7-cheque no. on voucher history window
''10-Mar-2014  Task:2484  Imran Ali  Load History On Voucher Take Too Time
''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
''17-Mar-2014 TASK:M26 Payee Title Reset Control
''17-Mar-2014 TASK:M27 Editable Bank Account In Payment Voucher
''19-Apr-2014 TASK:2577 Imran Ali Send Branded SMS Functionlity
'''''Task No 2619 Mughees Escape Code Updation 
''15-May-2014 TASK:2631 Imran Ali  Change SMS Body In Payment,Receipt Screen
''26-May-2014 TASK:2647 Imran Ali Cross Cheque Printing
''26-June-2014 TASK2704 Imran Ali Add new functionality cash request in erp.
''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)
''24-Jul-2014 TASK:M73 Imran Ali Voucher Save Problem
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''26-Aug-2014 Task:2809 Imran Ali Add more option cheque print of MCB, NBP @Add New Collections of NBP AND MCB In Layout ComboBox
''04-Sep-2014 Task:2826 Imran Ali Checked Status Option on  Voucher
''11-Sep-2014 Task:M101 Imran Ali Add new field remarks 
''23-Sep-2014 Task:2854 Imran Ali Attachment Count In History And Preview On Payment/Receipt/Voucher
'28-05-2015 Task# 20150514 to send SMS on Confirm Message Ali Ansari
''04-Jun-2015 Task:2015060001 Ali Ansari Regarding Attachements 
'08-Jun-2015  Task#2015060005 to allow all files to attach
''10-June-2015 Task# 2015060008 to remove non pictures from report with attachements
'22-06-2015 Task#2015060023 Ali Ansari to save proper activity log
'25-06-2015 Task#201506026 Ali Ansari to block exceed payments
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'03-Aug-2015 Task#03082015 Ahmad Sharif: add company drop down on designer , and Fill Drop down with companies for setting company wise invoice number
'04-Aug-2015 Task#04082015 Ahmad Shari: Left Outer Join CompanyDefTable with tblVoucher and  Add Column CompanyName from CompanyDefTable in query
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.
''18-11-2015 TASK181115 Muhammad Ameen: Display current cheque no in chequebox against specific account selection in Combo and increase chequeno by one in texbox after added to grid.
''18-11-2015 TASK18112015 Muhammad Ameen: Set confirmation message on ChequeValidation when EnableChequeBook configuration is checked.
''10-08-2017 : TFS1265 : Muhammad Ameen added Memo or Reference Remarks which are configuration based to be saved to voucher detail from Payment, Expense and Receipt. on 10-08-2017
''TASK TFS1474 Muhammad Ameen on 15-09-2017. Currency rate can not be edited while base currency is selected.

Imports System.Data
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports Janus.Windows.GridEX
Imports Neodynamic.SDK.Barcode

Public Class frmOldVendorPayment
    Implements IGeneral
    ' Change on 23-11-2013  For Multiple Print Vouchers
    Dim lngSelectedVoucherId As Long
    Dim blnEditMode As Boolean = False
    Dim blnFirstTimeInvoked As Boolean = False
    Dim Mode As String = "Normal"
    Dim Email As Email
    Dim IsLoadedForm As Boolean = False
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim GetVoucherId As Integer = 0
    Dim setVoucherNo As String = String.Empty
    Dim crpt As New ReportDocument
    Dim setEditMode As Boolean = False
    Dim Total_Amount As Double = 0D
    Dim setVoucherType As String = String.Empty
    Dim Prviouse_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim DiscountAccountId As Integer = 0I
    Dim TaxReceiveableAccountId As Integer = 0I
    Dim flgCompanyRights As Boolean = False
    Dim EnabledBrandedSMS As Boolean = False 'Task:2577 Added Flag For Branded SMS
    Dim currentChequeNo As String

    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim Msgfrm As New frmMessages
    Dim CostCentreId As Integer = 0
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrFile() As String = {}
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    Dim IsGetAllAllowed As Boolean = False
    Dim IsAdminGroup As Boolean = False
    Dim flgMemoRemarks As Boolean = False

    Enum grdEnm
        'Voucher_Id
        'coa_detail_id
        'detail_title
        'detail_code
        'Amount
        'Discount
        'Tax
        'TaxAmount
        'Reference
        'Cheque_No
        'Cheque_Date
        'PayeeTitle 'R:955 Added Index
        'Phone 'Task:2577 Added Index
        'Type
        'CostCenterId
        voucher_id
        coa_detail_id
        detail_title
        detail_code
        ''TAKS-407
        CurrencyId
        CurrencyAmount
        CurrencyRate
        BaseCurrencyId
        BaseCurrencyRate
        ''END TASK-407
        Amount
        CurrencyDiscount
        Discount
        Tax
        TaxCurrencyAmount
        TaxAmount
        Reference
        Cheque_No
        Cheque_Date
        PayeeTitle
        Mobile
        Type
        CostCenterId
        VoucherDetailId
        LoanRequestId
        DetailId
    End Enum
    Sub FillPaymentMethod()
        blnFirstTimeInvoked = True
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dr1 As DataRow
        'Dim dr2 As DataRow
        dt.Columns.Add("Id")
        dt.Columns.Add("Name")
        dr = dt.NewRow
        dr1 = dt.NewRow
        'dr2 = dt.NewRow

        dr(0) = Convert.ToInt32(4)
        dr(1) = "Bank"
        dt.Rows.InsertAt(dr, 0)

        dr1(0) = Convert.ToInt32(2)
        dr1(1) = "Cash"
        dt.Rows.InsertAt(dr1, 0)

        Me.cmbVoucherType.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
        Me.cmbVoucherType.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
        Me.cmbVoucherType.DataSource = dt

        'Dim dt1 As New DataTable
        'dt1 = dt
        'dr2(0) = 0
        'dr2(1) = ".... Select any value ...."
        'dt1.Rows.InsertAt(dr2, 0)
        'Me.cmbSearchVoucherType.DisplayMember = "Name"
        'Me.cmbSearchVoucherType.ValueMember = "Id"
        'Me.cmbSearchVoucherType.DataSource = dt1

    End Sub
    ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
    Private Sub txtDiscount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscount.KeyPress
        Try
            'If (Char.IsDigit(e.KeyChar) Or Keys.Back = AscW(e.KeyChar) Or e.KeyChar.Equals("."c)) Then
            '    e.Handled = False
            'Else
            '    e.Handled = True
            'End If
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtTax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTax.KeyPress
        Try
            'If (Char.IsDigit(e.KeyChar) Or Keys.Back = AscW(e.KeyChar) Or e.KeyChar.Equals("."c)) Then
            '    e.Handled = False
            'Else
            '    e.Handled = True
            'End If
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            'If (Char.IsDigit(e.KeyChar) Or Keys.Back = AscW(e.KeyChar) Or e.KeyChar.Equals("."c)) Then
            '    e.Handled = False
            'Else
            '    e.Handled = True
            'End If
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2491
    Function GetVoucherTypeId(ByVal strVoucherType As String) As Long
        Dim lngVoucherTypeId As Long

        Dim strQuery As String
        strQuery = "SELECT Voucher_Type_ID, Voucher_Type FROM tblDefVoucherType WHERE voucher_type = N'" & strVoucherType & "'"

        Dim objCommand As New OleDbCommand(strQuery, Con)
        lngVoucherTypeId = objCommand.ExecuteScalar

        Return lngVoucherTypeId
    End Function

    Sub PopulateGrid(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim strSql As String = String.Empty
        Dim UserName As String = LoginUserName
        ''strSql = "SELECT tblVoucher.voucher_id, voucher_no [Voucher No], voucher_date [Voucher Date], Credit.PaidById, Credit.PaidBy [Paid By], Credit.account_type [Payment Method], " _
        ''       & " Debit.PaidToId, Debit.DepositedIn [Deposited In], Debit.Amount, cheque_no [Cheque No], cheque_date [Cheque Date]" _
        ''       & " FROM tblVoucher INNER JOIN " _
        ''       & " (SELECT voucher_id, tblCOAMainSubSubDetail.coa_detail_id AS PaidById, detail_title AS PaidBy" _
        ''       & " FROM tblCOAMainSubSubDetail INNER JOIN tblVoucherDetail ON tblCOAMainSubSubDetail.coa_detail_id = tblVoucherDetail.coa_detail_id" _
        ''       & " WHERE tblVoucherDetail.credit_amount > 0) AS Credit ON Credit.voucher_id = tblVoucher.voucher_id INNER JOIN " _
        ''       & " (SELECT voucher_id, tblCOAMainSubSubDetail.coa_detail_id AS PaidToId, detail_title AS DepositedIn, tblVoucherDetail.debit_amount AS Amount " _
        ''       & " FROM tblCOAMainSubSubDetail INNER JOIN tblVoucherDetail ON tblCOAMainSubSubDetail.coa_detail_id = tblVoucherDetail.coa_detail_id " _
        ''       & " WHERE tblVoucherDetail.debit_amount > 0) AS Debit ON Debit.voucher_id = tblVoucher.voucher_id"

        'strSql = "SELECT tblVoucher.voucher_id, voucher_no [Voucher No], voucher_date [Voucher Date], Credit.PaidById, Credit.PaidBy [Paid From], Credit.account_type [Payment Method], " _
        '        & " Debit.PaidToId, Debit.DepositedIn [Paid To], Debit.Amount, cheque_no [Cheque No], cheque_date [Cheque Date], Debit.comments as Comments" _
        '        & " FROM tblVoucher INNER JOIN (SELECT voucher_id, vwCOADetail.coa_detail_id AS PaidById, detail_title AS PaidBy, account_type" _
        '        & " FROM vwCOADetail INNER JOIN tblVoucherDetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id" _
        '        & " WHERE tblVoucherDetail.credit_amount > 0) AS Credit ON Credit.voucher_id = tblVoucher.voucher_id INNER JOIN " _
        '        & " (SELECT voucher_id, vwCOADetail.coa_detail_id AS PaidToId, detail_title AS DepositedIn, account_type, tblVoucherDetail.debit_amount AS Amount, comments " _
        '        & " FROM vwCOADetail INNER JOIN tblVoucherDetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id " _
        '        & " WHERE tblVoucherDetail.debit_amount > 0) AS Debit ON Debit.voucher_id = tblVoucher.voucher_id where tblVoucher.source=N'" & Me.Name & "'"

        ''If strCondition <> "" Then
        'strSql &= " and tblVoucher.voucher_type_id = N'" & Me.cmbVoucherType.SelectedValue & "'"
        ''End If
        'strSql = strSql + " order by 2 desc"
        If Mode = "Normal" Then
            'Before against task:2443
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            '         & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no LEFT OUTER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '         & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
            'Task:2443 Added Field Cheque_No In This Query
            'Before against task:2484
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            '        & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no LEFT OUTER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '        & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
            'End Task:2443
            'Task:2484 Change Join In This Query
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            '      & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '      & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
            'End Task:2484
            'Before against task:2826
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status], ISNULL(tblVoucher.CashRequestId,0) as CashRequestId, CRH.RequestNo + '~' + Convert(varchar,Convert(varchar, CRH.RequestDate,102)) as RequestNo  " _
            '    & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN CashRequestHead CRH ON CRH.RequestID=dbo.tblVoucher.CashRequestId  " _
            '    & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
            'Task:2826 Added Field Checked
            'Before against task:M101
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status],IsNull(tblVoucher.Checked,0) as Checked, ISNULL(tblVoucher.CashRequestId,0) as CashRequestId, CRH.RequestNo + '~' + Convert(varchar,Convert(varchar, CRH.RequestDate,102)) as RequestNo  " _
            '    & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN CashRequestHead CRH ON CRH.RequestID=dbo.tblVoucher.CashRequestId  " _
            '    & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
            'End Task:2826
            'Task:M101 Added Field Remarks
            'Before against task:2854
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, tblVoucher.Remarks,isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status],IsNull(tblVoucher.Checked,0) as Checked, ISNULL(tblVoucher.CashRequestId,0) as CashRequestId, CRH.RequestNo + '~' + Convert(varchar,Convert(varchar, CRH.RequestDate,102)) as RequestNo  " _
            '   & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN CashRequestHead CRH ON CRH.RequestID=dbo.tblVoucher.CashRequestId  " _
            '   & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
            'End Task:M101
            'Task:2854 Added Field No Of Attachment
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, tblVoucher.Remarks,isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status],IsNull(tblVoucher.Checked,0) as Checked, ISNULL(tblVoucher.CashRequestId,0) as CashRequestId, CRH.RequestNo + '~' + Convert(varchar,Convert(varchar, CRH.RequestDate,102)) as RequestNo, IsNull([No Of Attachment],0) as [No Of Attachment]  " _
            '& " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN CashRequestHead CRH ON CRH.RequestID=dbo.tblVoucher.CashRequestId LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  tblVoucher.Voucher_Id  " _
            '& " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""

            'Task#04082015 Left Outer Join CompanyDefTable with tblVoucher and  Add Column CompanyName from CompanyDefTable in query
            strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, tblVoucher.Remarks,isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status],IsNull(tblVoucher.Checked,0) as Checked, ISNULL(tblVoucher.CashRequestId,0) as CashRequestId, CRH.RequestNo + '~' + Convert(varchar,Convert(varchar, CRH.RequestDate,102)) as RequestNo, IsNull([No Of Attachment],0) as [No Of Attachment],tblVoucher.username as 'User Name',CompanyDefTable.CompanyName  " _
          & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id Left Outer join  CompanyDefTable on tblVoucher.location_id=CompanyDefTable.CompanyId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN CashRequestHead CRH ON CRH.RequestID=dbo.tblVoucher.CashRequestId LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  tblVoucher.Voucher_Id  " _
          & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
            'End Task#04082015

            If flgCompanyRights = True Then
                strSql += " And tblVoucher.Location_Id=" & MyCompanyId
            End If
        End If
        If strCondition <> "" Then
            'strSql &= " AND account_type = N'" & strCondition & "'"
        End If
        If Me.dtpFrom.Checked = True Then
            strSql += " AND tblVoucher.Voucher_Date >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) "
        End If
        If Me.dtpTo.Checked = True Then
            strSql += " AND tblVoucher.Voucher_Date <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102) "
        End If
        If Me.cmbSearchVoucherType.SelectedIndex > 0 Then
            strSql += " AND tblVoucher.Voucher_Type_Id=" & Me.cmbSearchVoucherType.SelectedValue
        End If
        If Me.txtSearchVoucherNo.Text <> String.Empty Then
            strSql += " AND tblvoucher.Voucher_No LIKE '%" & Me.txtSearchVoucherNo.Text & "%'"
        End If
        If Me.txtSearchChequeNo.Text <> String.Empty Then
            strSql += " AND tblVoucherDetail.Cheque_No LIKE '%" & Me.txtSearchChequeNo.Text & "%'"
        End If
        If Me.dtpSearchChequeDate.Checked = True Then
            strSql += " AND (Convert(Varchar, tblVoucherDetail.Cheque_Date,102) = Convert(Datetime, N'" & Me.dtpSearchChequeDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))"
        End If
        If Me.txtFromAmount.Text <> String.Empty Then
            If Val(Me.txtFromAmount.Text) > 0 Then
                strSql += " AND tblVoucherDetail.credit_amount >=" & Val(Me.txtFromAmount.Text) & ""
            End If
        End If
        If Me.txtToAmount.Text <> String.Empty Then
            If Val(Me.txtFromAmount.Text) > 0 Then
                strSql += " AND tblVoucherDetail.credit_amount <=" & Val(Me.txtToAmount.Text) & ""
            End If
        End If
        If cmbSearchAccount.SelectedRow IsNot Nothing Then
            If Me.cmbSearchAccount.SelectedRow.Cells(0).Value <> 0 Then
                strSql += " AND tblVoucherDetail.coa_detail_id = " & Me.cmbSearchAccount.Value
            End If
        End If
        If Me.txtSearchComments.Text <> String.Empty Then
            strSql += " AND tblVoucherDetail.Comments LIKE '%" & Me.txtSearchComments.Text & "%'"
        End If
        If UserName.Length > 0 AndAlso IsGetAllAllowed = False AndAlso IsAdminGroup = False Then
            strSql += " And tblVoucher.UserName LIKE '%" & UserName & "%'"
        End If
        'strSql = strSql + " order by 2 desc"
        strSql = strSql + " order by tblVoucher.voucher_id desc"

        FillGridEx(grdVouchers, strSql, True)

        'Task:2854 Setting Link Type Column
        Me.grdVouchers.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        'End Task:2854

        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.grdVouchers.RootTable.Columns.Add("Column1")
        Me.grdVouchers.RootTable.Columns("Column1").UseHeaderSelector = True
        Me.grdVouchers.RootTable.Columns("Column1").ActAsSelector = True
        '------------------------------------------'
        Me.grdVouchers.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        Me.grdVouchers.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        'If Not Me.grdVouchers.RowCount > 0 Then Exit Sub
        grdVouchers.RootTable.Columns(0).Visible = False  'Voucher ID
        grdVouchers.RootTable.Columns("Post").Visible = False

        grdVouchers.RootTable.Columns("CashRequestId").Visible = False

        Me.grdVouchers.RootTable.Columns("voucher_no").Caption = "Vouchr No"
        Me.grdVouchers.RootTable.Columns("voucher_date").Caption = "Date"
        Me.grdVouchers.RootTable.Columns("Amount").FormatString = "N"
        Me.grdVouchers.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grdVouchers.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grdVouchers.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Me.grdVouchers.RootTable.Columns("Voucher_Date").FormatString = str_DisplayDateFormat

    End Sub

    'Sub ClearFields()
    '    txtVoucherNo.Text = "" 'VoucherNo
    '    dtVoucherDate.Value = Date.Today  'VoucherDate
    '    Me.cmbAccounts.Rows(0).Activate() 'PaidBy
    '    txtAmount.Text = "" 'Amount
    '    Me.txtReference.Text = String.Empty
    '    'cmbVoucherType.SelectedIndex = 0    'Payment Method
    '    'cmbCashAccount.SelectedIndex = 0 'Paid To
    '    cmbVoucherType_SelectedIndexChanged(Me, New EventArgs)
    '    blnEditMode = False

    '    GetSecurityRights()
    'End Sub
    Function GetVoucherNo() As String
        Dim VoucherNo As String = String.Empty
        Dim VType As String = String.Empty
        If Me.cmbVoucherType.SelectedIndex > 0 Then
            'Task#03082015 Concatenate iif condition with prefix
            VType = "BPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
        Else
            VType = "CPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
            'End Task#03082015
        End If
        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtVoucherDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
        Else
            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
            If Not dr Is Nothing Then
                If dr("config_Value") = "Monthly" Then
                    Return GetNextDocNo(VType & "-" & Format(Me.dtVoucherDate.Value, "yy") & Me.dtVoucherDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                Else
                    VoucherNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                    Return VoucherNo
                End If
            Else
                VoucherNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                Return VoucherNo
            End If

            Return ""
        End If

    End Function
    Sub SaveRecord()
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Dim objCommand As New OleDbCommand
        Dim objTrans As OleDbTransaction
        Dim lngVoucherMasterId As Long
        Dim blnCashOptionDetail As Boolean = False
        If Not getConfigValueByType("CashAccountOptionForDetail").ToString = "Error" Then
            blnCashOptionDetail = getConfigValueByType("CashAccountOptionForDetail")
        End If

        ''TASK : TFS1265
        If Not getConfigValueByType("MemoRemarks").ToString = "Error" Then
            flgMemoRemarks = getConfigValueByType("MemoRemarks")
        End If
        ''End TASK: TFS1265
        'If Not GetConfigValue("SalesDiscountAccount").ToString = "Errro" Then
        DiscountAccountId = Val(getConfigValueByType("SalesDiscountAccount").ToString) 'Convert.ToInt32(GetConfigValue("SalesDiscountAccount").ToString)
        'Set Tax Receipt Account Id 
        TaxReceiveableAccountId = Val(getConfigValueByType("TaxreceiveableACid").ToString)
        'End If
        Dim strChequeNo As String = String.Empty
        Dim Cheque_Date As DateTime = Nothing
        'Working against request no 763
        'Enhancement By Imran Ali
        '
        If Not Me.blnEditMode Then
            '    If Me.cmbVoucherType.SelectedIndex > 0 Then
            '        Me.txtVoucherNo.Text = GetNextDocNo("BPV", 6, "tblVoucher", "voucher_no")

            '    Else
            '        Me.txtVoucherNo.Text = GetNextDocNo("CPV", 6, "tblVoucher", "voucher_no")
            '    End If

            Me.txtVoucherNo.Text = GetVoucherNo()
            setVoucherNo = Me.txtVoucherNo.Text
            setEditMode = False
        End If

        If Val(Me.txtDiscount.Text) <> 0 Then 'Discount Grater than zeror
            If DiscountAccountId <= 0 Then 'Diccount Account Id if Less Than Or Equal is Zero
                ShowErrorMessage("Disccount account is not map")
                Me.txtDiscount.Focus()
                Exit Sub
            End If
        End If
        If Val(Me.txtTax.Text) <> 0 Then 'Tax Grater than zeror
            If TaxReceiveableAccountId <= 0 Then 'Tax Account Id if Less Than Or Equal is Zero
                ShowErrorMessage("Tax account is not map")
                Me.txtTax.Focus()
                Exit Sub
            End If
        End If
        If Me.cmbCashAccount.SelectedIndex <= 0 Then
            ShowErrorMessage("Please select deposit account")
            cmbCashAccount.Focus()
            Exit Sub
        End If
        Dim enableChequeBook As Boolean = getConfigValueByType("EnableAutoChequeBook")
        If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
        objTrans = Con.BeginTransaction
        Try

            objCommand.Connection = Con

            'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                       & " cheque_no, cheque_date,post,source)" _
            '                       & " VALUES(1, 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value & "', '" _
            '                       & IIf(txtChequeNo.Visible, txtChequeNo.Text, "NULL") & "', N'" & IIf(dtChequeDate.Visible, dtChequeDate.Value, Nothing) & "', 0,N'" & Me.Name & "')" _
            '                       & " SELECT @@IDENTITY"

            objCommand.Transaction = objTrans
            If Not Me.blnEditMode Then
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '                           & " cheque_no, cheque_date,post,source)" _
                '                           & " VALUES(1, 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value & "', '" _
                '                           & IIf(txtChequeNo.Visible, txtChequeNo.Text, "") & "', " & IIf(dtChequeDate.Visible, "N'" & dtChequeDate.Value & "'", "Null") & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "')" _
                '                           & " SELECT @@IDENTITY"
                objCommand.CommandText = String.Empty
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '           & " cheque_no, cheque_date,post,source, UserName,Posted_UserName)" _
                '           & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value & "', '" _
                '           & IIf(txtChequeNo.Visible, txtChequeNo.Text, "") & "', " & IIf(dtChequeDate.Visible, "N'" & dtChequeDate.Value & "'", "Null") & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "Null") & ")" _
                '           & " SELECT @@IDENTITY"

                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '          & " post,source, UserName,Posted_UserName)" _
                '          & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '          & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "Null") & ")" _
                '          & " SELECT @@IDENTITY"
                'Before against task:2826
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '         & " post,source, UserName,Posted_UserName,CashRequestId)" _
                '         & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '         & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "Null") & ", " & IIf(Me.cmbCashRequest.SelectedIndex > 0, Me.cmbCashRequest.SelectedValue, 0) & ")" _
                '         & " SELECT @@IDENTITY"
                'TAsk:2826 Added Field Checked
                'Before againt task:M101
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '         & " post,source, UserName,Posted_UserName,CashRequestId,Checked, CheckedByUser)" _
                '         & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '         & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "Null") & ", " & IIf(Me.cmbCashRequest.SelectedIndex > 0, Me.cmbCashRequest.SelectedValue, 0) & "," & IIf(Me.chkChecked.Checked = True, 1, 0) & "," & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ")" _
                '         & " SELECT @@IDENTITY"
                'End Task:2826
                'Task:M101 Added Remarks
                'Task#03082015 Insert location_id in query from cmbCompany.Selected Value
                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                    & " post,source, UserName,Posted_UserName,CashRequestId,Checked, CheckedByUser,Remarks)" _
                    & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                    & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "Null") & ", " & IIf(Me.cmbCashRequest.SelectedIndex > 0, Me.cmbCashRequest.SelectedValue, 0) & "," & IIf(Me.chkChecked.Checked = True, 1, 0) & "," & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ",N'" & Me.txtMemo.Text.Replace("'", "''") & "')" _
                    & " SELECT @@IDENTITY"
                'End Task:M101
                lngVoucherMasterId = objCommand.ExecuteScalar
                'Marked Against Task#2015060001 Ali Ansari
                ' If arrFile.Length > 0 Then SaveDocument(lngVoucherMasterId, Me.Name, objTrans)
                'Marked Against Task#2015060001 Ali Ansari
                'Altered Against Task#2015060001 Ali Ansari
                If arrFile.Count > 0 Then
                    SaveDocument(lngVoucherMasterId, Me.Name, objTrans)
                End If
            Else
                setVoucherNo = Me.txtVoucherNo.Text
                setEditMode = True
                'objCommand.CommandText = "update tblVoucher set location_id = 1 , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '                        & " , voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value & "', " _
                '                       & " cheque_no = " & IIf(txtChequeNo.Visible, "N'" & txtChequeNo.Text & "'", "''") & " , cheque_date = " & IIf(dtChequeDate.Visible, "N'" & dtChequeDate.Value & "'", "Null") & "  ,post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "'" _
                '                       & " where voucher_id = " & Me.grd.Rows(0).Cells(0).Value & " "
                'objCommand.ExecuteNonQuery()


                If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                    Call CreateDuplicationVoucher(lngSelectedVoucherId, "Update", objTrans) 'TASKM2710151
                End If
                objCommand.CommandText = String.Empty

                'objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '                      & " , voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '                      & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "'  " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & "," _
                '                      & " Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & "  where voucher_id = " & lngSelectedVoucherId & " "
                'Before against task:2826
                'objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '                      & " , voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '                      & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "'  " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & "," _
                '                      & " Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CashRequestId=" & IIf(Me.cmbCashRequest.SelectedIndex > 0, Me.cmbCashRequest.SelectedValue, 0) & "  where voucher_id = " & lngSelectedVoucherId & " "
                'Task:2826 Added Field Checked
                'Before againt task:M101
                'objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '                      & " , voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '                      & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "'  " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & "," _
                '                      & " Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CashRequestId=" & IIf(Me.cmbCashRequest.SelectedIndex > 0, Me.cmbCashRequest.SelectedValue, 0) & ", Checked=" & IIf(Me.chkChecked.Checked = True, 1, 0) & ", CheckedByUser=" & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & "  where voucher_id = " & lngSelectedVoucherId & " "
                'TAsk:M101 Added Field Remarks
                'Task#03082015 Update location_id in query from cmbCompany.Selected Value
                objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                                     & " , voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                     & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "'  " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & "," _
                                     & " Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CashRequestId=" & IIf(Me.cmbCashRequest.SelectedIndex > 0, Me.cmbCashRequest.SelectedValue, 0) & ", Checked=" & IIf(Me.chkChecked.Checked = True, 1, 0) & ", CheckedByUser=" & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ",Remarks=N'" & Me.txtMemo.Text.Replace("'", "''") & "'  where voucher_id = " & lngSelectedVoucherId & " "
                'End Task:M101
                objCommand.ExecuteNonQuery()
                lngVoucherMasterId = lngSelectedVoucherId 'Me.grd.GetRows(0).Cells(0).Value
                'If arrFile.Length > 0 Then 
                'Marked Against Task#2015060001 Ali Ansari
                ' If arrFile.Length > 0 Then SaveDocument(lngVoucherMasterId, Me.Name, objTrans)
                'Marked Against Task#2015060001 Ali Ansari
                'Altered Against Task#2015060001 Ali Ansari

                If arrFile.Count > 0 Then
                    SaveDocument(lngVoucherMasterId, Me.Name, objTrans)
                End If

                '' Revised Update Cheque Serial No
                If enableChequeBook = True Then
                    objCommand.Transaction = objTrans
                    objCommand.CommandText = String.Empty
                    'Before againsta task:2383
                    'objCommand.CommandText = "Update ChequeDetailTable Set VoucherDetailId=0,Cheque_Issued=0  From  ChequeDetailTable, tblVoucherDetail WHERE tblVoucherDetail.Voucher_Detail_Id = ChequeDetailTable.VoucherDetailId WHERE tblVoucherDetail.Voucher_Id=" & lngVoucherMasterId & ""
                    'Task:2383 Change Filter
                    objCommand.CommandText = "Update ChequeDetailTable Set VoucherDetailId=0,Cheque_Issued=0  From  ChequeDetailTable, tblVoucherDetail WHERE tblVoucherDetail.Voucher_Detail_Id = ChequeDetailTable.VoucherDetailId AND tblVoucherDetail.Voucher_Id=" & lngVoucherMasterId & ""
                    'End Task:2383
                    objCommand.ExecuteNonQuery()
                End If
                'ask:2704 Update Status Cash Request
                If Me.cmbCashRequest.SelectedIndex > 0 Then
                    Call New CashrequestDAL().DeleteUpdateStatus(lngSelectedVoucherId, Me.cmbCashRequest.SelectedValue, objTrans)
                End If
                'End Task:2704

                'Delete From Detail Voucher
                objCommand.Transaction = objTrans
                objCommand.CommandText = String.Empty
                objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE voucher_id = " & lngSelectedVoucherId
                objCommand.ExecuteNonQuery()
            End If


            Dim strMultiChequeDetail As String = String.Empty 'Task:2443  e.g Multi Cheque Detail Value Store 

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows

                '***********************
                'Inserting Debit Amount
                '***********************
                strChequeNo = r.Cells("Cheque_No").Value.ToString
                Cheque_Date = IIf(r.Cells("Cheque_No").Value.ToString = "", Nothing, IIf(r.Cells("Cheque_Date").Value Is DBNull.Value, Now, r.Cells("Cheque_Date").Value))
                If BtnSave.Text <> "&Save" Then
                    If strChequeNo.Length = 0 AndAlso Cheque_Date <> Date.MinValue Then
                        Throw New Exception("Please Enter Cheque No.")
                    End If
                End If

                If r.Cells("Type").Value.ToString.Trim.ToUpper = "LC" Then
                    If Val(r.Cells("CostCenterId").Value.ToString) = 0 Then
                        Throw New Exception("Please select cost center.")
                    End If
                End If
                objCommand = New OleDbCommand
                objCommand.Connection = Con
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & r.Cells("CustomerID").Value & ", " & r.Cells("Amount").Value & ",  0 , N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "')"
                'Before against request no. 955
                'objCommand.CommandText = String.Empty
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Adjustment, costcenterId, Cheque_No,Cheque_Date, Tax_Percent, Tax_Amount) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & r.Cells("coa_detail_id").Value & ", " & r.Cells("Amount").Value & ",  0 , N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', " & Val(r.Cells("Discount").Value) & ", " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ")Select @@Identity"
                'R:955 Added Column Payee Title
                objCommand.CommandText = String.Empty
                'Before against task:2728
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Adjustment, costcenterId, Cheque_No,Cheque_Date, Tax_Percent, Tax_Amount, PayeeTitle) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & r.Cells("coa_detail_id").Value & ", " & r.Cells("Amount").Value & ",  0 , N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', " & Val(r.Cells("Discount").Value) & ", " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ", " & IIf(Me.cmbVoucherType.Text = "Bank", "N'" & r.Cells("PayeeTitle").Value.ToString.Replace("'", "''") & "'", "NULL") & ")Select @@Identity"
                'End R:955
                'TAsk:2728 Set Comments Cheque No., Cheque Date

                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,ChequeDescription, Adjustment, costcenterId, Cheque_No,Cheque_Date, Tax_Percent, Tax_Amount, PayeeTitle,contra_coa_detail_id) " _
                '                     & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & r.Cells("coa_detail_id").Value & ", " & r.Cells("Amount").Value & ",  0 , N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("Discount").Value) & ", " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ", " & IIf(Me.cmbVoucherType.Text = "Bank", "N'" & r.Cells("PayeeTitle").Value.ToString.Replace("'", "''") & "'", "NULL") & "," & Me.cmbCashAccount.SelectedValue & ")Select @@Identity"
                'End Task:2728
                'CurrencyId()
                'CurrencyAmount()
                'CurrencyRate()
                'BaseCurrencyId()
                'BaseCurrencyRate()
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,ChequeDescription, Adjustment, costcenterId, Cheque_No,Cheque_Date, Tax_Percent, Tax_Amount, PayeeTitle,contra_coa_detail_id,LoanRequestId, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_amount, Currency_Symbol) " _
                                     & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & r.Cells("coa_detail_id").Value & ", " & r.Cells("Amount").Value & ",  0 , N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("CurrencyDiscount").Value) & ", " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ", " & IIf(Me.cmbVoucherType.Text = "Bank", "N'" & r.Cells("PayeeTitle").Value.ToString.Replace("'", "''") & "'", "NULL") & "," & Me.cmbCashAccount.SelectedValue & ", " & Val(r.Cells("LoanRequestId").Value.ToString) & ", " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", " _
                                     & " " & Val(r.Cells("CurrencyRate").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", 0, '" & Me.cmbCurrency.Text & "')Select @@Identity"
                objCommand.Transaction = objTrans
                Dim objId As Object = objCommand.ExecuteScalar()


                If Val(r.Cells("VoucherDetailId").Value.ToString) > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update InvoiceAdjustmentTable Set VoucherDetailId=" & objId & " WHERE VoucherDetailId=" & Val(r.Cells("VoucherDetailId").Value.ToString) & ""
                    objCommand.ExecuteNonQuery()
                End If


                If blnCashOptionDetail = True Then
                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount,  comments,ChequeDescription, Adjustment, costcenterId, Cheque_No,Cheque_Date, Tax_Percent, Tax_Amount, PayeeTitle,contra_coa_detail_id) " _
                    '                        & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString)) & ",  0 , N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("Discount").Value) & ", " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ", " & IIf(Me.cmbVoucherType.Text = "Bank", "N'" & r.Cells("PayeeTitle").Value.ToString.Replace("'", "''") & "'", "NULL") & "," & r.Cells("coa_detail_id").Value & ")Select @@Identity"
                    'End Task:2728

                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount,  comments,ChequeDescription, Adjustment, costcenterId, Cheque_No,Cheque_Date, Tax_Percent, Tax_Amount, PayeeTitle,contra_coa_detail_id,LoanRequestId, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount) " _
                    '                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString)) & ",  0 , N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("Discount").Value) & ", " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ", " & IIf(Me.cmbVoucherType.Text = "Bank", "N'" & r.Cells("PayeeTitle").Value.ToString.Replace("'", "''") & "'", "NULL") & "," & r.Cells("coa_detail_id").Value & "," & Val(r.Cells("LoanRequestId").Value.ToString) & ", " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", " _
                    '                   & " " & Val(r.Cells("CurrencyRate").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ", 0 , " & Val(r.Cells("CurrencyAmount").Value.ToString) & ")Select @@Identity"
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount,  comments,ChequeDescription, Adjustment, costcenterId, Cheque_No,Cheque_Date, Tax_Percent, Tax_Amount, PayeeTitle,contra_coa_detail_id,LoanRequestId, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount, Currency_Symbol) " _
                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString)) & ",  0 , N'" & IIf(flgMemoRemarks = False, r.Cells("Reference").Value.ToString.Trim.Replace("'", "''"), Me.txtMemo.Text.Replace("'", "''")) & "', N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("Discount").Value) & ", " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ", " & IIf(Me.cmbVoucherType.Text = "Bank", "N'" & r.Cells("PayeeTitle").Value.ToString.Replace("'", "''") & "'", "NULL") & "," & r.Cells("coa_detail_id").Value & "," & Val(r.Cells("LoanRequestId").Value.ToString) & ", " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", " _
                                       & " " & Val(r.Cells("CurrencyRate").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ", 0 , " & Val(r.Cells("CurrencyAmount").Value.ToString) - (Val(r.Cells("CurrencyDiscount").Value.ToString) + Val(r.Cells("Tax_Currency_Amount").Value.ToString)) & ", '" & Me.cmbCurrency.Text & "')Select @@Identity"
                    objCommand.Transaction = objTrans
                    objCommand.ExecuteScalar()
                End If

                ''Task:2728 Set Comments Cheque No. Cheque Date. Party Name.
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount,comments,ChequeDescription, CostCenterId, Cheque_No, Cheque_Date) " _
                '                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & (Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString))) & ", " & 0 & ", N'" & Me.txtMemo.Text.Replace("'", "''") & "', N'" & GetComments(r).ToString.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.Tostring()) & ", " & IIf(strChequeNo.Length > 0, "N'" & strChequeNo.Replace("'", "''") & "'", "NULL") & ", " & IIf(strChequeNo.Length = 0, "NULL", "N'" & Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ")"
                ''End Task:2728
                'objCommand.Transaction = objTrans
                'objCommand.ExecuteScalar()


                '' Cheque Issued Status
                If enableChequeBook = True Then
                    objCommand.Transaction = objTrans
                    objCommand.CommandText = String.Empty
                    objCommand.CommandText = "UPDATE ChequeDetailTable SET VoucherDetailId=" & objId & ", Cheque_Issued=1 From ChequeDetailTable, ChequeMasterTable WHERE ChequeDetailTable.ChequeSerialId=ChequeMasterTable.ChequeSerialId AND ChequeNo=N'" & strChequeNo & "' AND BankAcId=" & Me.cmbCashAccount.SelectedValue & ""
                    objCommand.ExecuteNonQuery()
                End If


                If Val(r.Cells("Discount").Value) <> 0 Then
                    'Discount Voucher ...................
                    objCommand.Transaction = objTrans
                    objCommand.CommandText = String.Empty
                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,CostCenterId, Cheque_No, Cheque_Date) " _
                    '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(DiscountAccountId) & "," & 0 & ", " & Val(r.Cells("Discount").Value) & ", N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.ToString()) & "," & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ")"
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,CostCenterId, Cheque_No, Cheque_Date,LoanRequestId, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount, Currency_Symbol) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(DiscountAccountId) & "," & 0 & ", " & Val(r.Cells("Discount").Value) & ", N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.ToString()) & "," & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Val(r.Cells("LoanRequestId").Value.ToString) & ", " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("Discount").Value) & ", " _
                                     & " " & Val(r.Cells("CurrencyRate").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ", 0, " & Val(r.Cells("CurrencyDiscount").Value) & ", '" & Me.cmbCurrency.Text & "')"
                    objCommand.ExecuteNonQuery()
                End If


                'if Tax grater than zero  
                If Val(r.Cells("Tax").Value) <> 0 Then
                    'Discount Voucher ...................
                    objCommand.Transaction = objTrans
                    objCommand.CommandText = String.Empty
                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,CostCenterId, Cheque_No, Cheque_Date, Tax_Percent,Tax_Amount) " _
                    '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(TaxReceiveableAccountId) & "," & 0 & ", " & Val(r.Cells("Tax_Amount").Value) & ", N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.ToString()) & "," & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & ")"
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,CostCenterId, Cheque_No, Cheque_Date, Tax_Percent,Tax_Amount,LoanRequestId, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount, Currency_Symbol) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(TaxReceiveableAccountId) & "," & 0 & ", " & Val(r.Cells("Tax_Amount").Value) & ", N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.ToString()) & "," & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & Val(r.Cells("Tax").Value.ToString) & ", " & Val(r.Cells("Tax_Amount").Value.ToString) & "," & Val(r.Cells("LoanRequestId").Value.ToString) & ", " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", " _
                                     & " " & Val(r.Cells("CurrencyRate").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ", 0, " & Val(r.Cells("Tax_Currency_Amount").Value) & ", '" & Me.cmbCurrency.Text & "' )"
                    objCommand.ExecuteNonQuery()
                End If



                'Task:2443  e.g Multi Cheque Detail Value Store 
                If r.Cells("Cheque_No").Value.ToString.Length > 0 Then
                    If strMultiChequeDetail.Length > 0 Then
                        strMultiChequeDetail += "|" & r.Cells("Cheque_No").Value.ToString & ":" & IIf(r.Cells("Cheque_Date").Value.ToString <> "", CDate(r.Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    Else
                        strMultiChequeDetail = r.Cells("Cheque_No").Value.ToString & ":" & IIf(r.Cells("Cheque_Date").Value.ToString <> "", CDate(r.Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    End If
                End If
                'end Task:4243

                'Task:2577 Send Branded SMS
                'If My.Computer.Network.IsAvailable Then ''24-Jul-2014 TASK:M73 Imran Ali Voucher Save Problem
                'Marked Against Task# 20150514 to send SMS on Confirm Message Ali Ansari
                'If Me.chkPost.Checked = True Then
                '    If EnabledBrandedSMS = True Then
                '        If GetSMSConfig("Payment").Enable = True Then
                '            If (r.Cells("Mobile").Value.ToString <> "" Or r.Cells("Mobile").Value.ToString.Length >= 10) Then
                '                Try
                '                    'Before against task:2631
                '                    'Call SendBrandedSMS("92" & Microsoft.VisualBasic.Right(r.Cells("Phone").Value.ToString.Replace("-", "").Replace(".", "").Replace("+", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", ""), 10), "Dear Vendor your payment have been made throug " & IIf(Me.cmbVoucherType.Text = "Bank", " Bank " & Me.cmbCashAccount.Text & " Cheque no. " & r.Cells("Cheque_No").Value.ToString & "", " Online") & "")
                '                    'Task:2631 Added Field Mobile
                '                    If msg_Confirm(str_ConfirmSendSMSMessage) = True Then
                '                        Dim objSMSTemp As New SMSTemplateParameter
                '                        Dim strMSGBody As String = String.Empty ' Task:2631 Added object
                '                        If Me.cmbVoucherType.Text = "Bank" Then
                '                            objSMSTemp = GetSMSTemplate("Bank Payment")
                '                        Else
                '                            objSMSTemp = GetSMSTemplate("Cash Payment")
                '                        End If

                '                        If objSMSTemp IsNot Nothing Then
                '                            Dim objSMSParam As New SMSParameters
                '                            objSMSParam.AccountCode = r.Cells("detail_code").Value.ToString
                '                            objSMSParam.AccountTitle = r.Cells("detail_title").Value.ToString
                '                            objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                '                            objSMSParam.DocumentDate = Me.dtVoucherDate.Value
                '                            objSMSParam.Remarks = Me.txtMemo.Text
                '                            objSMSParam.CellNo = r.Cells("Mobile").Value.ToString.Replace("-", "").Replace("_", "").Replace(".", "").Replace("+", "").Replace("@", "").Replace(";", "") 'r.Cells("Mobile").Value.ToString
                '                            objSMSParam.Amount = Math.Round((Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString))), 0)
                '                            If Me.cmbVoucherType.Text = "Bank" Then
                '                                objSMSParam.ChequeNo = r.Cells("Cheque_No").Value.ToString
                '                                If IsDBNull(r.Cells("Cheque_Date").Value) Then
                '                                    objSMSParam.ChequeDate = Nothing
                '                                Else
                '                                    objSMSParam.ChequeDate = r.Cells("Cheque_Date").Value
                '                                End If
                '                            End If
                '                            objSMSParam.CompanyName = CompanyTitle
                '                            Dim objSMSLog As SMSLogBE
                '                            If GetSMSConfig("Payment").EnabledAdmin = True Then
                '                                For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                '                                    If strMob.Length > 10 Then
                '                                        objSMSLog = New SMSLogBE
                '                                        objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                '                                        objSMSLog.PhoneNo = strMob 'r.Cells("Mobile").Value.ToString
                '                                        objSMSLog.CreatedByUserID = LoginUserId
                '                                        Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                '                                    End If
                '                                Next
                '                            End If
                '                            objSMSLog = New SMSLogBE
                '                            objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                '                            objSMSLog.PhoneNo = r.Cells("Mobile").Value.ToString.Replace("-", "").Replace("_", "").Replace(".", "").Replace("+", "").Replace("@", "").Replace(";", "")
                '                            objSMSLog.CreatedByUserID = LoginUserId
                '                            Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                '                            'Call SendBrandedSMS("92" & Microsoft.VisualBasic.Right(r.Cells("Mobile").Value.ToString.Replace("-", "").Replace(".", "").Replace("+", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", ""), 10), strMSGBody)
                '                            'End Task:2631
                '                        End If
                '                    End If
                '                    'Marked Against Task# 20150514 to send SMS on Confirm Message Ali Ansari

                '                    'Task:2631 Changed Comments
                '                    'strMSGBody = "Dear Vendor your payment against " & Me.txtMemo.Text & " have been made Rs." & Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString)) & ""
                '                    'If Not IsDBNull(r.Cells("Cheque_Date").Value) Then
                '                    '    strMSGBody += " against cheque No " & r.Cells("Cheque_No").Value.ToString & " dated: " & CDate(r.Cells("Cheque_Date").Value.ToString).ToString("dd/MMM/yyyy") & ""
                '                    'End If
                '                    'strMSGBody += " have been paid through " & IIf(Me.cmbVoucherType.Text = "Bank", "Bank", "Cash") & ". thanks for your cooperation. Automated by www.softbeats.net"
                '                    'Task:2631 Set By Ref Value
                '                    'AccountCode()
                '                    'AccountTitle()
                '                    'DocumentNo()
                '                    'DocumentDate()
                '                    'OtherDocNo()
                '                    'Remarks()
                '                    'Amount()
                '                    'Quantity()
                '                    'ChequeNo()
                '                    'ChequeDate()
                '                    'CompanyName
                '                    'CellNo()
                '                    'Softbeats()
                '                    'Dim objSMSTemp As New SMSTemplateParameter
                '                    'If Me.cmbVoucherType.Text = "Bank" Then
                '                    '    objSMSTemp = GetSMSTemplate("Bank Payment")
                '                    'Else
                '                    '    objSMSTemp = GetSMSTemplate("Cash Payment")
                '                    'End If
                '                    'If objSMSTemp IsNot Nothing Then
                '                    '    Dim objSMSParam As New SMSParameters
                '                    '    objSMSParam.AccountCode = r.Cells("detail_code").Value.ToString
                '                    '    objSMSParam.AccountTitle = r.Cells("detail_title").Value.ToString
                '                    '    objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                '                    '    objSMSParam.DocumentDate = Me.dtVoucherDate.Value
                '                    '    objSMSParam.Remarks = Me.txtMemo.Text
                '                    '    objSMSParam.CellNo = r.Cells("Mobile").Value.ToString.Replace("-", "").Replace("_", "").Replace(".", "").Replace("+", "").Replace("@", "").Replace(";", "") 'r.Cells("Mobile").Value.ToString
                '                    '    objSMSParam.Amount = Math.Round((Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString))), 0)
                '                    '    If Me.cmbVoucherType.Text = "Bank" Then
                '                    '        objSMSParam.ChequeNo = r.Cells("Cheque_No").Value.ToString
                '                    '        If IsDBNull(r.Cells("Cheque_Date").Value) Then
                '                    '            objSMSParam.ChequeDate = Nothing
                '                    '        Else
                '                    '            objSMSParam.ChequeDate = r.Cells("Cheque_Date").Value
                '                    '        End If
                '                    '    End If
                '                    '    objSMSParam.CompanyName = CompanyTitle
                '                    '    Dim objSMSLog As SMSLogBE
                '                    '    If GetSMSConfig("Payment").EnabledAdmin = True Then
                '                    '        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                '                    '            If strMob.Length > 10 Then
                '                    '                objSMSLog = New SMSLogBE
                '                    '                objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                '                    '                objSMSLog.PhoneNo = strMob 'r.Cells("Mobile").Value.ToString
                '                    '                objSMSLog.CreatedByUserID = LoginUserId
                '                    '                Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                '                    '            End If
                '                    '        Next
                '                    '    End If
                '                    '    objSMSLog = New SMSLogBE
                '                    '    objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                '                    '    objSMSLog.PhoneNo = r.Cells("Mobile").Value.ToString.Replace("-", "").Replace("_", "").Replace(".", "").Replace("+", "").Replace("@", "").Replace(";", "")
                '                    '    objSMSLog.CreatedByUserID = LoginUserId
                '                    '    Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                '                    '    'Call SendBrandedSMS("92" & Microsoft.VisualBasic.Right(r.Cells("Mobile").Value.ToString.Replace("-", "").Replace(".", "").Replace("+", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", ""), 10), strMSGBody)
                '                    '    'End Task:2631
                '                    'End If
                '                Catch ex As Exception
                '                End Try
                '            End If
                '        End If
                '    End If
                'End If
                'End Task:2577
                'End If ' End Task:M73
            Next


            'Task:2443 Update Multi Cheque Date On Master Record
            If strMultiChequeDetail.Length > 0 AndAlso strMultiChequeDetail.Length < 8000 Then
                objCommand.CommandText = ""
                objCommand.Transaction = objTrans
                objCommand.CommandText = "Update tblVoucher SET Cheque_No=N'" & strMultiChequeDetail.Replace("'", "''") & "' WHERE Voucher_Id=" & lngVoucherMasterId
                objCommand.ExecuteNonQuery()
            End If
            'End Task:2443

            ''***********************
            ''Inserting Debit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & cmbCashAccount.SelectedValue & ", " & txtAmount.Text & ", 0)"

            'objCommand.Transaction = objTrans
            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Credit Amount
            ''***********************
            'Chaning Against Request No 801
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            If blnCashOptionDetail = False Then
                objCommand.CommandText = String.Empty
                '                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount,comments, CostCenterId, Cheque_No, Cheque_Date,ChequeDescription) " _
                '                                     & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Discount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Tax_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & 0 & ", N'" & Me.txtMemo.Text.Replace("'", "''") & "', " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(strChequeNo.Length > 0, "N'" & strChequeNo.Replace("'", "''") & "'", "NULL") & ", " & IIf(strChequeNo.Length = 0, "NULL", "N'" & Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ",'N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "')"
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount,comments, CostCenterId, Cheque_No, Cheque_Date, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount) " _
                '                      & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Discount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Tax_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & 0 & ", N'" & Me.txtMemo.Text.Replace("'", "''") & "', " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(strChequeNo.Length > 0, "N'" & strChequeNo.Replace("'", "''") & "'", "NULL") & ", " & IIf(strChequeNo.Length = 0, "NULL", "N'" & Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ")"

                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount,comments, CostCenterId, Cheque_No, Cheque_Date, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount, Currency_Symbol) " _
                                      & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Discount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Tax_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & 0 & ", N'" & Me.txtMemo.Text.Replace("'", "''") & "', " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(strChequeNo.Length > 0, "N'" & strChequeNo.Replace("'", "''") & "'", "NULL") & ", " & IIf(strChequeNo.Length = 0, "NULL", "N'" & Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & Val(Me.cmbCurrency.SelectedValue) & ", 0 , " _
                                      & Val(txtCurrencyRate.Text) & ", " & Val(Me.BaseCurrencyId) & ",1, 0 , " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyDiscount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Tax_Currency_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", '" & Me.cmbCurrency.Text & "')"

                objCommand.Transaction = objTrans
                objCommand.ExecuteNonQuery()
            End If

            'TAsk:2704 Update Status Cash Request
            If Me.cmbCashRequest.SelectedIndex > 0 Then
                Call New CashrequestDAL().UpdateStatus(Me.cmbCashRequest.SelectedValue, objTrans)
            End If
            'End Task:2704

            'If chkPost.Checked = False Then

            objCommand.CommandText = " if exists (select COUNT(*) from VoucherApprovalGroupSetting) " _
                                            & " if not exists (select * from VoucherApprovedLog where voucher_id=" & lngVoucherMasterId & ") " _
                                            & " insert into VoucherApprovedLog (Voucher_Id , UserGroupId ,VALstatus,UserId ,UserName,ModificationDate ) values (" & lngVoucherMasterId & ",1,'Pending', " & LoginUserId & ",'" & LoginUserName & "',GETDATE())"
            objCommand.ExecuteNonQuery()

            'End If


            objTrans.Commit()
            SendSMS()

            strChequeNo = String.Empty
            Cheque_Date = Nothing
            'Marked Against Task#2015060023 Ali Ansari to save proper activity log
            'SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
            'Marked Against Task#2015060023 Ali Ansari to save proper activity log
            'Altered Against Task#2015060023 Ali Ansari to save proper activity log
            If BtnSave.Text = "&Save" Then
                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
            Else
                SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
            End If
            'Altered Against Task#2015060023 Ali Ansari to save proper activity log
            GetVoucherId = lngVoucherMasterId
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex

        End Try
    End Sub
    Private Function IsValidateChequePayment(ByVal ChequeNo As String, ByVal voucherID As Int32, Optional ByVal trans As OleDbTransaction = Nothing) As Boolean ' TSK-1015-00174 
        Try

            Dim dt As DataTable = GetDataTable("Select tblVoucherDetail.Cheque_No, tblVoucherDetail.Cheque_Date From tblVoucherDetail left join tblVoucher on tblVoucherDetail.voucher_id = tblVoucher.voucher_id WHERE tblVoucherDetail.Cheque_No = '" & ChequeNo & "' And tblVoucher.voucher_type_id = 4 And tblVoucherDetail.voucher_id <> '" & voucherID & "'", trans)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString <> "" Then
                    If msg_Confirm("Cheque No: " & ChequeNo.Replace("'", "''") & " is already issued. Do you want to proceed. ?") = False Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Sub DeleteRecord(ByVal strDeleteWhat As String)
        If grdVouchers.CurrentRow Is Nothing Then
            ShowErrorMessage("Please select a row to delete")
        Else
            Dim enableChequeBook As Boolean = getConfigValueByType("EnableAutoChequeBook")
            'lngSelectedVoucherId = grdVouchers.CurrentRow.Cells(0).Value
            Dim objCommand As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
            objTrans = Con.BeginTransaction
            Try

                objCommand.Connection = Con
                objCommand.Transaction = objTrans
                objCommand.CommandType = CommandType.Text
                objCommand.CommandTimeout = 120

                lngSelectedVoucherId = Val(Me.grdVouchers.GetRow.Cells("Voucher_ID").Value.ToString)

                If strDeleteWhat = "Detail Only" Then
                    'objCommand.Transaction = objTrans
                    'objCommand.CommandText = String.Empty
                    'objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE voucher_id = " & lngSelectedVoucherId
                    'objCommand.ExecuteNonQuery()
                ElseIf strDeleteWhat = "Both" Then
                    '' Revised Update Cheque Serial No

                    If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                        Call CreateDuplicationVoucher(lngSelectedVoucherId, "Delete", objTrans) 'TASKM2710151
                    End If

                    If enableChequeBook = True Then
                        'objCommand.Transaction = objTrans
                        objCommand.CommandText = String.Empty
                        'Before against task:2383
                        'objCommand.CommandText = "Update ChequeDetailTable Set VoucherDetailId=0,Cheque_Issued=0  From  ChequeDetailTable, tblVoucherDetail WHERE tblVoucherDetail.Voucher_Detail_Id = ChequeDetailTable.VoucherDetailId WHERE tblVoucherDetail.Voucher_Id=" & lngSelectedVoucherId & ""
                        'Task:2383 Change Filter 
                        objCommand.CommandText = "Update ChequeDetailTable Set VoucherDetailId=0,Cheque_Issued=0  From  ChequeDetailTable, tblVoucherDetail WHERE tblVoucherDetail.Voucher_Detail_Id = ChequeDetailTable.VoucherDetailId And tblVoucherDetail.Voucher_Id=" & lngSelectedVoucherId & ""
                        'End Task:2383
                        objCommand.ExecuteNonQuery()
                    End If
                    'Task:2704 Update Status Cash Request
                    If Me.cmbCashRequest.SelectedIndex > 0 Then
                        Call New CashrequestDAL().DeleteUpdateStatus(lngSelectedVoucherId, Me.cmbCashRequest.SelectedValue, objTrans)
                    End If
                    'End Task:2704

                    'objCommand.Transaction = objTrans
                    'lngSelectedVoucherId = grdVouchers.GetRow.Cells(0).Value
                    objCommand.CommandText = String.Empty
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE voucher_id = " & lngSelectedVoucherId
                    objCommand.ExecuteNonQuery()

                    'objCommand = New OleDbCommand
                    'objCommand.Connection = Con
                    'objCommand.Transaction = objTrans
                    objCommand.CommandText = String.Empty
                    objCommand.CommandText = "DELETE FROM tblVoucher WHERE voucher_id = " & lngSelectedVoucherId
                    objCommand.ExecuteNonQuery()

                End If

                objTrans.Commit()
                If strDeleteWhat = "Both" Then SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Me.grdVouchers.CurrentRow.Cells(1).Value.ToString, True)

            Catch ex As Exception
                objTrans.Rollback()
            End Try

        End If
    End Sub

    Sub EditRecord()
        Try

            'cmbAccounts.Value = grdVouchers.CurrentRow.Cells(3).Value   'PaidBy
            'txtAmount.Text = grdVouchers.CurrentRow.Cells(8).Value 'Amount
            lngSelectedVoucherId = Me.grdVouchers.GetRow.Cells(0).Value
            'Me.txtChequeNo.Text = grdVouchers.CurrentRow.Cells("cheque_no").Value
            'If Not IsDBNull(grdVouchers.CurrentRow.Cells("cheque_date").Value) Then
            '    If grdVouchers.CurrentRow.Cells("cheque_date").Value <> DateTime.MinValue Then
            '        Me.dtChequeDate.Value = grdVouchers.CurrentRow.Cells("cheque_date").Value
            '    End If
            'End If
            'RemoveHandler cmbVoucherType.SelectedIndexChanged, AddressOf cmbVoucherType_SelectedIndexChanged
            cmbVoucherType.Text = grdVouchers.CurrentRow.Cells("Payment Method").Value    'Payment Method
            Me.txtVoucherNo.Text = grdVouchers.CurrentRow.Cells(1).Value 'VoucherNo
            dtVoucherDate.Value = grdVouchers.CurrentRow.Cells(2).Value 'VoucherDate
            'cmbCashAccount.SelectedValue = grdVouchers.CurrentRow.Cells(6).Value.ToString 'Paid To
            Me.btnAttachment.Text = "Attachment (" & Me.grdVouchers.GetRow.Cells("No Of Attachment").Value.ToString & ")"
            MainEditRecord()
            Me.DisplayDetail(grdVouchers.CurrentRow.Cells(0).Value)
            Prviouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.cmbVoucherType.Enabled = False
            Me.cmbCashAccount.Enabled = False
            Me.chkEnableDepositAc.Visible = True
            Me.chkEnableDepositAc.Checked = False
            If cmbVoucherType.SelectedIndex > 0 Then
                Me.GroupBox1.Visible = True
                Me.GroupBox1.Enabled = True
                Me.grd.RootTable.Columns("Cheque_No").Visible = True
                Me.grd.RootTable.Columns("Cheque_Date").Visible = True
                'Me.cmbCashAccount.Enabled = False ''17-Mar-2014 TASK:M27 Editable Bank Account In Payment Voucher
                'Me.cmbVoucherType.Enabled = False
              
                'R:955 Hide Columns
                Me.grd.RootTable.Columns("PayeeTitle").Visible = True
                Me.grd.RootTable.Columns("PrintCheque").Visible = True
                'R:955
            Else
                Me.GroupBox1.Visible = False
                Me.GroupBox1.Enabled = False
                Me.grd.RootTable.Columns("Cheque_No").Visible = False
                Me.grd.RootTable.Columns("Cheque_Date").Visible = False
                'Me.cmbCashAccount.Enabled = True
                'Me.cmbVoucherType.Enabled = True
                'R:955 Hide Columns
                Me.grd.RootTable.Columns("PayeeTitle").Visible = False
                Me.grd.RootTable.Columns("PrintCheque").Visible = False
                'End R:955
            End If

            Me.chkChecked.Checked = Me.grdVouchers.CurrentRow.Cells("Checked").Value
            Me.BtnSave.Text = "&Update" 'TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
            GetSecurityRights()
            Me.chkPost.Checked = Me.grdVouchers.CurrentRow.Cells("Post").Value
            'Me.GetTotal()
            If flgDateLock = True Then
                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtVoucherDate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtVoucherDate.Enabled = False
                Else
                    Me.dtVoucherDate.Enabled = True
                End If
            Else
                Me.dtVoucherDate.Enabled = True
            End If
            Dim objdt As DataTable = CType(Me.cmbCashRequest.DataSource, DataTable)
            Dim drfound() As DataRow
            Dim dr As DataRow

            drfound = objdt.Select("RequestId=" & Me.grdVouchers.GetRow.Cells("CashRequestId").Value.ToString & "")
            If drfound.Length = 0 Then
                dr = objdt.NewRow
                dr(0) = Me.grdVouchers.GetRow.Cells("CashRequestId").Value.ToString
                dr(1) = Me.grdVouchers.GetRow.Cells("RequestNo").Value.ToString
                objdt.Rows.Add(dr)
                objdt.AcceptChanges()
            End If
            RemoveHandler cmbCashRequest.SelectedIndexChanged, AddressOf cmbCashRequest_SelectedIndexChanged
            Me.cmbCashRequest.SelectedValue = Me.grdVouchers.GetRow.Cells("CashRequestId").Value.ToString
            Me.cmbCashRequest.Enabled = False
            AddHandler cmbCashRequest.SelectedIndexChanged, AddressOf cmbCashRequest_SelectedIndexChanged

            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdVouchers.GetRow.Cells("Print Status").Text.ToString
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            'Altered Against Task# 2015060001 Ali Ansari
            'Get no of attached files
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdVouchers.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdVouchers.CurrentRow.Cells("No Of Attachment").Value)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If

            'Task#04082015 Edit company name altered by Ahmad Sharif 
            Me.cmbCompany.Text = Me.grdVouchers.CurrentRow.Cells("CompanyName").Value.ToString
            'Task#04082015
            'Altered Against Task# 2015060001 Ali Ansari
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnPrint.Visible = True
            Me.BtnDelete.Visible = True
            'AddHandler cmbVoucherType.SelectedIndexChanged, AddressOf cmbVoucherType_SelectedIndexChanged

            If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                Dim intCountVouchers As Integer = 0
                Dim dtCountVouches As New DataTable
                dtCountVouches = GetDuplicateVouchers(Convert.ToInt32(lngSelectedVoucherId))
                dtCountVouches.AcceptChanges()
                If dtCountVouches.Rows.Count > 0 Then
                    intCountVouchers = dtCountVouches.Rows.Count
                    Me.btnUpdateTimes.Visible = True
                    btnUpdateTimes.Text = "No of update times (" & intCountVouchers & ")"
                    Call CreateContextMenu(Val(Me.grdVouchers.GetRow.Cells("Voucher_Id").Value.ToString), btnUpdateTimes)
                Else
                    Me.btnUpdateTimes.Visible = False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayDetail(ByVal VoucherID As Integer)
        Try


            Dim str As String
            'Before against R:955
            'str = " SELECT tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title, tblVoucherDetail.debit_amount as Amount, tblVoucherDetail.Adjustment as Discount, Isnull(tblVoucherDetail.Tax_Percent,0) as Tax, Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Amount, tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date " _
            '      & " FROM         tblVoucherDetail INNER JOIN " _
            '      & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
            '      & " Where voucher_id =" & VoucherID & "AND (tblVoucherDetail.debit_amount > 0)"
            'Before against task:2577
            'R:955 Added Column PayeeTitle
            'str = " SELECT tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title, tblVoucherDetail.debit_amount as Amount, tblVoucherDetail.Adjustment as Discount, Isnull(tblVoucherDetail.Tax_Percent,0) as Tax, Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Amount, tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date,tblVoucherDetail.PayeeTitle  " _
            '      & " FROM         tblVoucherDetail INNER JOIN " _
            '      & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
            '      & " Where voucher_id =" & VoucherID & "AND (tblVoucherDetail.debit_amount > 0)"
            'End R:955
            'Task:2577 Added Phone Field And Joint Vendor Information Table
            'Before against task:2631
            'str = " SELECT tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title, tblVoucherDetail.debit_amount as Amount, tblVoucherDetail.Adjustment as Discount, Isnull(tblVoucherDetail.Tax_Percent,0) as Tax, Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Amount, tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date,tblVoucherDetail.PayeeTitle, Ven.Phone  " _
            '    & " FROM         tblVoucherDetail INNER JOIN " _
            '    & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id LEFT OUTER JOIN tblVendor Ven On Ven.AccountId=vwCOADetail.coa_detail_id " _
            '    & " Where voucher_id =" & VoucherID & "AND (tblVoucherDetail.debit_amount > 0)"
            'End Task:2577
            'Task:2631 Added Field Mobile And Also remove Field Phone
            'str = " SELECT tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title, tblVoucherDetail.debit_amount as Amount, tblVoucherDetail.Adjustment as Discount, Isnull(tblVoucherDetail.Tax_Percent,0) as Tax, Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Amount, tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date,tblVoucherDetail.PayeeTitle, Ven.Mobile  " _
            '& " FROM         tblVoucherDetail INNER JOIN " _
            '& " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id LEFT OUTER JOIN tblVendor Ven On Ven.AccountId=vwCOADetail.coa_detail_id " _
            '& " Where voucher_id =" & VoucherID & "AND (tblVoucherDetail.debit_amount > 0)"
            '      str = " SELECT tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title,vwCOADetail.detail_code, tblVoucherDetail.debit_amount as Amount, tblVoucherDetail.Adjustment as Discount, Isnull(tblVoucherDetail.Tax_Percent,0) as Tax, Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Amount, tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date,tblVoucherDetail.PayeeTitle, Ven.Mobile, vwCOADetail.Account_Type as Type, IsNull(tblVoucherDetail.CostCenterId,0) as CostCenterId, IsNull(InvAdj.VoucherDetailId,0) as VoucherDetailId  " _
            '& " FROM tblVoucherDetail INNER JOIN " _
            '& " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id LEFT OUTER JOIN tblVendor Ven On Ven.AccountId=vwCOADetail.coa_detail_id LEFT OUTER JOIN (Select Distinct VoucherDetailId From InvoiceAdjustmentTable) as InvAdj on InvAdj.VoucherDetailId = tblVoucherDetail.Voucher_Detail_Id " _
            '& " Where voucher_id =" & VoucherID & "AND (tblVoucherDetail.debit_amount > 0) Order By tblVoucherDetail.Voucher_Detail_Id ASC "



            'End Task:2631

            'CurrencyId()
            'CurrencyAmount()
            'CurrencyRate()
            'BaseCurrencyId()
            'BaseCurrencyRate()

            str = " SELECT tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title,vwCOADetail.detail_code, IsNull(tblVoucherDetail.CurrencyId, 0) As CurrencyId, IsNull(tblVoucherDetail.CurrencyAmount, tblVoucherDetail.debit_amount) As CurrencyAmount, IsNull(tblVoucherDetail.CurrencyRate, 0) As CurrencyRate, IsNull(tblVoucherDetail.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(tblVoucherDetail.BaseCurrencyRate, 0) As BaseCurrencyRate, tblVoucherDetail.debit_amount as Amount, tblVoucherDetail.Adjustment as CurrencyDiscount, 0 as Discount, Isnull(tblVoucherDetail.Tax_Percent,0) as Tax, Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Currency_Amount,Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Amount, tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date,tblVoucherDetail.PayeeTitle, Ven.Mobile, vwCOADetail.Account_Type as Type, IsNull(tblVoucherDetail.CostCenterId,0) as CostCenterId, IsNull(InvAdj.VoucherDetailId,0) as VoucherDetailId, Isnull(tblVoucherDetail.LoanRequestId,0) as LoanRequestId, Isnull(tblVoucherDetail.voucher_detail_id, 0) As DetailId   " _
& " FROM tblVoucherDetail INNER JOIN " _
& " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id LEFT OUTER JOIN tblVendor Ven On Ven.AccountId=vwCOADetail.coa_detail_id LEFT OUTER JOIN (Select Distinct VoucherDetailId From InvoiceAdjustmentTable) as InvAdj on InvAdj.VoucherDetailId = tblVoucherDetail.Voucher_Detail_Id " _
& " Where voucher_id =" & VoucherID & "AND (tblVoucherDetail.debit_amount > 0) Order By tblVoucherDetail.Voucher_Detail_Id ASC "


            'Dim objCommand As New OleDbCommand

            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim dt As New DataTable

            'If Con.State = ConnectionState.Open Then Con.Close()

            'Con.Open()
            'objCommand.Connection = Con
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = str

            'objDataAdapter.SelectCommand = objCommand
            'objDataAdapter.Fill(dt)

            'grd.Rows.Clear()
            ''For i = 0 To dt.Tables(0).Rows.Count - 1
            ''    grd.Rows.Add(dt.Tables(0).Rows(i)(0), dt.Tables(0).Rows(i)(1), dt.Tables(0).Rows(i)(2), dt.Tables(0).Rows(i)(3), dt.Tables(0).Rows(i)(4), dt.Tables(0).Rows(i)(5))
            ''Next

            'For Each r As DataRow In dt.Rows
            '    'grd.Rows.Add(r.Item("voucher_id"), 0, r.Item("detail_title"), r.Item("coa_detail_id"), r.Item("credit_amount"), 0, 0, 0, 0, 0, 0, r.Item("comments"))
            '    grd.Rows.Add(r.Item("voucher_id"), 0, r.Item("detail_title"), r.Item("coa_detail_id"), r.Item("credit_amount"), 0, 0, 0, 0, 0, 0, r.Item("comments"), r.Item("Memo"))
            'Next

            'Me.cmbCashAccount.Enabled = False
            'Me.cmbVoucherType.Enabled = False

            ' Me.grd.Rows.Add(Me.txtVoucherNo.Text, Me.dtVoucherDate.Value, Me.cmbAccounts.ActiveRow.Cells(1).Value, Me.cmbAccounts.ActiveRow.Cells(0).Value, Me.txtAmount.Text.Trim, Me.cmbVoucherType.Text.ToString, Me.cmbVoucherType.SelectedValue, Me.cmbCashAccount.Text.ToString, Me.cmbCashAccount.SelectedValue, Me.txtChequeNo.Text.Trim, Me.dtChequeDate.Value, Me.txtReference.Text.Trim, Me.txtMemo.Text.Trim)
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            '            dtDisplayDetail.Columns("Tax_Amount").Expression = "(((Amount-Discount)*Tax)/100)"
            dtDisplayDetail.Columns("Tax_Amount").Expression = "(((Amount-Discount)*Tax)/100)"
            dtDisplayDetail.Columns("Tax_Currency_Amount").Expression = "(((CurrencyAmount-CurrencyDiscount)*Tax)/100)"

            dtDisplayDetail.Columns("Discount").Expression = "(CurrencyDiscount)* (CurrencyRate)"

            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    'Me.cmbCurrency.Enabled = False
                Else
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
            End If



            Dim strSQL As String = "Select RequestId, RequestNo,RequestDate,EmployeeId from AdvanceRequestTable INNER JOIN tblDefEmployee on tblDefEmployee.Employee_Id = AdvanceRequestTable.EmployeeId WHERE AdvanceRequestTable.RequestStatus='Approved' Union All Select 0 as RequestId, '.... Select any Value ....' RequestNo,null as RequestDate,0 as EmployeeId "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Me.grd.RootTable.Columns("LoanRequestId").ValueList.PopulateValueList(dt.DefaultView, "RequestId", "RequestNo")

            FillAccounts("GrdCostCenter")
            ApplyGridSettings()
            ''TASK-407
            Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
            Me.grd.RootTable.Columns("Amount").Caption = "" & GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)) & " Amount"
            If Me.cmbCurrency.Text.ToUpper.ToString = GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)).ToUpper.ToString Then
                Me.grd.RootTable.Columns("Amount").Visible = False
            Else
                Me.grd.RootTable.Columns("Amount").Visible = True
            End If
            ''END TAKS-407
            'Me.grd_Click(grd, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub CashRequestDetail(ByVal RequestId As Integer)
        Try
            'str = " SELECT tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title,vwCOADetail.detail_code, IsNull(tblVoucherDetail.CurrencyId, 0) As CurrencyId, IsNull(tblVoucherDetail.CurrencyAmount, tblVoucherDetail.debit_amount) As CurrencyAmount, IsNull(tblVoucherDetail.CurrencyRate, 0) As CurrencyRate, IsNull(tblVoucherDetail.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(tblVoucherDetail.BaseCurrencyRate, 0) As BaseCurrencyRate, tblVoucherDetail.debit_amount as Amount, 
            'tblVoucherDetail.Adjustment as CurrencyDiscount, 0 as Discount, Isnull(tblVoucherDetail.Tax_Percent,0) as Tax, Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Currency_Amount,Isnull(tblVoucherDetail.Tax_Amount,0) as Tax_Amount, 
            'tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date,tblVoucherDetail.PayeeTitle, Ven.Mobile, vwCOADetail.Account_Type as Type, IsNull(tblVoucherDetail.CostCenterId,0) as CostCenterId, IsNull(InvAdj.VoucherDetailId,0) as VoucherDetailId, Isnull(tblVoucherDetail.LoanRequestId,0) as LoanRequestId, Isnull(tblVoucherDetail.voucher_detail_id, 0) As DetailId  " _

            '' 12-06-2017 Task 898 done by Ameen added new columns which were not included when multi currency was implemented.
            '' 13-06-2017 Task 913 & 914 done by Ameen showed the values of cost center, comments and restrict zero amount.
            Dim str As String = String.Empty
            str = " SELECT 0 AS Voucher_Id, dbo.CashRequestDetail.coa_detail_id, vwCOADetail.detail_title, vwCOADetail.detail_code, 1 As CurrencyId, ISNULL(dbo.CashRequestDetail.Amount, 0) - ISNULL(dbo.CashRequestDetail.Paid_Amount, 0) As CurrencyAmount, 1 As CurrencyRate, 1 As BaseCurrencyId, 1 As BaseCurrencyRate, ISNULL(dbo.CashRequestDetail.Amount, 0) - ISNULL(dbo.CashRequestDetail.Paid_Amount, 0) AS Amount, " _
                   & " 0 AS CurrencyDiscount, 0 AS Discount, 0 AS Tax, 0 as Tax_Currency_Amount, 0 AS Tax_Amount, dbo.CashRequestDetail.Comments AS Reference, '' AS Cheque_No, GETDATE() AS Cheque_Date, '' AS PayeeTitle, '' AS Mobile, '' As Type, IsNull(dbo.CashRequestDetail.CostCenterId, 0) As CostCenterId, 0 As VoucherDetailId, 0 As LoanRequestId, 0 As DetailId  " _
                   & " FROM dbo.CashRequestDetail LEFT OUTER JOIN " _
                   & " dbo.vwCOADetail ON dbo.CashRequestDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id WHERE CashRequestDetail.RequestId=" & RequestId & " AND (ISNULL(dbo.CashRequestDetail.Amount, 0) - ISNULL(dbo.CashRequestDetail.Paid_Amount, 0)) > 0"


            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            dtDisplayDetail.Columns("Tax_Amount").Expression = "(((Amount-Discount)*Tax)/100)"
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            ApplyGridSettings()
            'Me.grd_Click(grd, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ValidateInput() As Boolean
        Try


            If cmbAccounts.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please select customer account")
                cmbAccounts.Focus()
                Return False
            End If

            If txtAmount.Text.Trim = "" Then
                ShowErrorMessage("Please enter amount")
                txtAmount.Focus()
                Return False
            End If

            If cmbCashAccount.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select deposit account")
                cmbCashAccount.Focus()
                Return False
            End If

            'If cmbVoucherType.Text = "Bank" Then
            '    If txtChequeNo.Text.Trim = "" Then
            '        ShowErrorMessage("Please enter cheque No.")
            '        txtChequeNo.Focus()
            '        Return False
            '    End If
            'End If
            If Me.cmbAccounts.ActiveRow.Cells("Type").Value.ToString.Trim.ToUpper = "LC" Then
                If Me.cmbCostCenter.SelectedIndex <= 0 Then
                    ShowErrorMessage("Please select cost center")
                    Me.cmbCostCenter.Focus()
                    Return False
                End If
            End If

            ''ReqId-883 Validate Credit Limit
            Dim dblAmount As Double = 0D
            '' with out request
            If Val(Me.txtCustomerBalance.Text) > 0 Then
                dblAmount = (Val(Me.txtAmount.Text) + Val(Me.txtCustomerBalance.Text))
            Else
                dblAmount = 0
            End If
            ''End Request
            If IsCreditLimit(Me.cmbAccounts.Value) > 0 Then
                If IsCreditLimit(Me.cmbAccounts.Value) < dblAmount Then
                    ShowErrorMessage("Advance Limit Exceeded")
                    Me.txtAmount.Focus()
                    Return False
                End If
            End If
            'TASK-407
            If Val(txtCurrencyRate.Text) = 0 Then
                ShowErrorMessage("Currency rate value more than 0 is required")
                Me.txtCurrencyRate.Focus()
                Return False
            End If
            'END TASK-407

            'End ReqId-883
            '' Validate Cheque No
            'If cmbVoucherType.SelectedIndex > 0 Then
            '    If IsValidateChequePayment(Me.txtChequeNo.Text.Trim, lngSelectedVoucherId) = False Then
            '        Me.txtChequeNo.Focus()
            '        Return False
            '    End If
            'End If
            If Me.cmbVoucherType.Text = "Bank" Then
                If getConfigValueByType("EnableAutoChequeBook").ToString = "True" Then
                    If Me.cmbVoucherType.SelectedIndex > 0 Then
                        If IsValidateChequeSerialNo(Me.cmbCashAccount.SelectedValue, Me.txtChequeNo.Text.Trim) = False Then
                            Me.txtChequeNo.Focus()
                            Return False
                        End If
                        If Me.grd.RowCount > 0 Then
                            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
                            Dim dr() As DataRow
                            dr = dt.Select("Cheque_No='" & Me.txtChequeNo.Text.Trim & "'")
                            If dr IsNot Nothing Then
                                If dr.Length > 0 Then
                                    msg_Error("Cheque No: [" & Me.txtChequeNo.Text.Trim & "] is already added")
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmOldVendorPayment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                ''''''Task No 2619 Mughees Escape Code Updation 
                If Me.grd.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                End If
                'End Task
                NewToolStripButton_Click(BtnNew, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                PrintToolStripButton_Click(BtnPrint, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Update" Then
                    If Me.BtnSave.Enabled = False Then
                        RemoveHandler Me.BtnSave.Click, AddressOf Me.SaveToolStripButton_Click
                    End If
                End If
            End If
            If e.KeyCode = Keys.D AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Delete" Then
                    If Me.BtnDelete.Enabled = False Then
                        RemoveHandler Me.BtnDelete.Click, AddressOf Me.DeleteToolStripButton_Click
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmOldVendorPayment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"
            FillCombos("Company")       'Task#03082015 Calling FillCombos for filling company drop down wiht companies name


            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            'TASK:2577 Get Branded SMS Configuration
            If Not getConfigValueByType("EnabledBrandedSMS").ToString = "Error" Then
                EnabledBrandedSMS = getConfigValueByType("EnabledBrandedSMS")
            End If
            'End Task:2577
            ''TASK : TFS1265
            'If Not getConfigValueByType("MemoRemarks").ToString = "Error" Then
            '    flgMemoRemarks = getConfigValueByType("MemoRemarks")
            'End If
            ''End TASK: TFS1265
            Me.FillPaymentMethod()
            'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title from vwCoaDetail  where detail_title is not null order by 2") 'where account_type='Vendor'")
            FillAccounts()
            Me.chkAllAccounts.Checked = False
            Me.cmbAccounts.Rows(0).Activate()
            'FillCombos("Currency") 'TASK-407

            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)

            Me.ClearFields()
            'PopulateGrid() R933 Commented History Data
            IsLoadedForm = True
            Get_All(frmMain.Tags)
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            If frmMain.Tags.Length > 0 Then frmMain.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub

    Private Sub cmbVoucherType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherType.SelectedIndexChanged
        Try
            'If IsLoadedForm = True Then Me.cmbAccounts.Focus()
            Dim Str As String = "If  exists(select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1 And Account_Id Is Not Null) " _
                   & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbVoucherType.Text & "' And coa_detail_id in (select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1) " & IIf(flgCompanyRights = True, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " " _
                   & " Else " _
                   & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbVoucherType.Text & "' " & IIf(flgCompanyRights = True, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " "

            'FillDropDown(Me.cmbCashAccount, "select coa_detail_id,detail_title from vwCoaDetail where account_type=N'" & Me.cmbVoucherType.Text & "' " & IIf(flgCompanyRights = True, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " ")
            FillDropDown(Me.cmbCashAccount, Str)

            If blnEditMode = True Then Exit Sub
            If Me.cmbVoucherType.SelectedIndex > 0 Then
                ' Me.txtVoucherNo.Text = GetNextDocNo("BPV", 6, "tblVoucher", "voucher_no")
                Me.txtVoucherNo.Text = GetVoucherNo()
                Me.GroupBox1.Visible = True
                Me.GroupBox1.Enabled = True
                Me.grd.RootTable.Columns("Cheque_No").Visible = True
                Me.grd.RootTable.Columns("Cheque_Date").Visible = True
            Else
                '  Me.txtVoucherNo.Text = GetNextDocNo("CPV", 6, "tblVoucher", "voucher_no")
                Me.txtVoucherNo.Text = GetVoucherNo()
                Me.GroupBox1.Visible = False
                Me.GroupBox1.Enabled = False
                Me.grd.RootTable.Columns("Cheque_No").Visible = False
                Me.grd.RootTable.Columns("Cheque_Date").Visible = False
            End If
            If Not blnFirstTimeInvoked Then
                PopulateGrid(cmbVoucherType.Text)
            End If
            setVoucherType = Me.cmbVoucherType.Text
            blnFirstTimeInvoked = False
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Dim NetAmount As Integer = 0I
        Try


            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtVoucherDate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace
            If IsAllowPayment() = False Then
                If Val(Me.txtPaymentBeforeBalance.Text) < Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) Then
                    ShowErrorMessage("Amount exceeds from available balance")
                    Me.cmbCashAccount.Focus()
                    Exit Sub
                End If
            End If
            'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace





            If IsDateLock(Me.dtVoucherDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If Me.dtVoucherDate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtVoucherDate.Focus()
                Exit Sub
            End If
            If cmbCashAccount.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select deposit account")
                cmbCashAccount.Focus()
                Exit Sub
            End If

            For Each r As GridEXRow In Me.grd.GetRows
                If r.Cells("Cheque_No").Value.ToString.Length > 0 Then
                    'If ValidateduplicateChequeInGrid(r.Cells("Cheque_No").Value.ToString) = False Then
                    '    Exit Sub
                    'End If
                    Dim rowStyle As Janus.Windows.GridEX.GridEXFormatStyle = New Janus.Windows.GridEX.GridEXFormatStyle()
                    Dim strCheque As String = r.Cells("Cheque_No").Value.ToString
                    Dim vendorID As Integer = r.Cells("coa_detail_id").Value
                    If strCheque.Length > 0 Then
                        If IsValidateChequePayment(strCheque, lngSelectedVoucherId, Nothing) = False Then
                            'Throw New Exception("This cheque is already issued, Please issue another cheque.")
                            rowStyle.BackColor = Color.Ivory
                            r.RowStyle = rowStyle
                            Exit Sub
                        Else
                            rowStyle.BackColor = Color.White
                            r.RowStyle = rowStyle
                        End If
                    Else
                        rowStyle.BackColor = Color.White
                        r.RowStyle = rowStyle
                    End If
                Else
                    Exit For
                End If
            Next

            If Not Me.grd.RowCount = 0 Then
                Me.grd.UpdateData()
                If blnEditMode Then
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then
                        Exit Sub
                    Else
                        ''21-02-2017
                        For Each drCC As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                            If IsVoucherCostCentreReshuffled(Val(drCC.Cells("DetailId").Value.ToString)) Then
                                ShowErrorMessage("Record can not be deleted because cost centre has been shifted.")
                                Exit Sub
                            End If
                        Next
                        ''End 21-02-2017
                        setEditMode = True
                        Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)

                        'DeleteRecord("Detail Only")
                        SaveRecord()

                        'MessageBox.Show(str_informUpdate, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'msg_Information(str_informUpdate)
                        Dim Printing As Boolean
                        Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        If Printing = True Then
                            If msg_Confirm("Do you want to print") = True Then
                                DualPrinting()
                            End If
                        End If
                        'If msg_Confirm(str_ConfirmPrintVoucher) = True Then
                        '    If Me.grdVouchers.RowCount = 0 Then Exit Sub
                        '    AddRptParam("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
                        '    ShowReport("rptVoucher", , , , True)
                        'End If
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        'EmailSave()
                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop

                        ClearFields()
                    End If
                Else
                    'If Not msg_Confirm(str_ConfirmSave) = True Then
                    'Exit Sub
                    'Else
                    setEditMode = True
                    Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    SaveRecord()
                    ClearFields()
                    'MessageBox.Show(str_informSave, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ' msg_Information(str_informSave)

                    Dim Printing As Boolean
                    Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                    If Printing = True Then
                        If msg_Confirm("Do you want to print") = True Then
                            DualPrinting()
                        End If
                    End If
                    'If msg_Confirm(str_ConfirmPrintVoucher) = True Then
                    '    If Me.grdVouchers.RowCount = 0 Then Exit Sub
                    '    AddRptParam("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
                    '    ShowReport("rptVoucher", , , , True)
                    'End If

                    If BackgroundWorker1.IsBusy Then Exit Sub
                    BackgroundWorker1.RunWorkerAsync()
                    'Do While BackgroundWorker1.IsBusy
                    '    Application.DoEvents()
                    'Loop
                    'EmailSave()
                    If BackgroundWorker2.IsBusy Then Exit Sub
                    BackgroundWorker2.RunWorkerAsync()
                    'Do While BackgroundWorker2.IsBusy
                    '    Application.DoEvents()
                    'Loop

                End If

                blnEditMode = False
                Total_Amount = 0D
            Else
                msg_Error("Enter atleast one record in the grid")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If grdVouchers.CurrentRow Is Nothing Then
                ShowErrorMessage("Please select a row to delete")
            Else
                blnEditMode = True
                lngSelectedVoucherId = CLng(grdVouchers.CurrentRow.Cells(0).Value)
                EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtVoucherDate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtVoucherDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If
            For Each drCC As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                If IsVoucherCostCentreReshuffled(Val(drCC.Cells("DetailId").Value.ToString)) Then
                    ShowErrorMessage("Record can not be deleted because cost centre has been shifted.")
                    Exit Sub
                End If
            Next

            If Not msg_Confirm(str_ConfirmDelete) = True Then
                Exit Sub
            Else

                If Me.grdVouchers.RowCount = 0 Then Exit Sub
                If CheckInvAdjustmentDependedVoucher(Val(Me.grdVouchers.GetRow.Cells("Voucher_Id").Value.ToString)) = True Then
                    ShowErrorMessage("Record can't be deleted, voucher adjusted against invoice.")
                    Exit Sub
                End If
                DeleteRecord("Both")
                'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
                Me.grdVouchers.CurrentRow.Delete()

                'MessageBox.Show(str_informDelete, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'msg_Information(str_informDelete)
                Me.ClearFields()
                'PopulateGrid() R933 Commented History Data
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Private Sub grdVouchers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVouchers.DoubleClick
        Try


            blnEditMode = True
            lngSelectedVoucherId = CLng(grdVouchers.CurrentRow.Cells(0).Value)
            Me.chkAllAccounts.Checked = True
            FillAccounts()
            EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            ClearFields()
            cmbAccounts.Focus()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick
        If Me.grdVouchers.RowCount = 0 Then Exit Sub
        'PrintLog = New SBModel.PrintLogBE
        'PrintLog.DocumentNo = grdVouchers.GetRow.Cells("Voucher_No").Value.ToString
        'PrintLog.UserName = LoginUserName
        'PrintLog.PrintDateTime = Date.Now
        'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
        ''ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
        ''Chaning Against Request No 798
        'AddRptParam("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
        'ShowReport("rptVoucher")
        PrintVoucherBC(Me.grdVouchers.GetRow.Cells("voucher_id").Value, Me.grdVouchers.GetRow.Cells("Voucher_No").Value.ToString())
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.btnSearchDelete.Enabled = True ''R934 Added Dlete Button
                Me.bntSearchPrint.Enabled = True   ''R934 Added Print Button
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = True 'Task:28226 Set Default Security.
                Me.btnGetAllRecord.Enabled = True
                IsGetAllAllowed = True
                IsAdminGroup = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.btnSearchDelete.Enabled = False
                    Me.bntSearchPrint.Enabled = False
                    Me.BtnPrint.Enabled = False
                    'Me.btnGetAllRecord.Enabled = False
                    IsGetAllAllowed = False
                    IsAdminGroup = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmOldVendorPayment)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.bntSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    Me.chkPost.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                Me.bntSearchPrint.Enabled = False
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                Me.chkPost.Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = False
                Me.chkChecked.Visible = False
                'Me.btnGetAllRecord.Enabled = False
                IsGetAllAllowed = False
                IsAdminGroup = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        Me.bntSearchPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Visible = True
                        Me.chkPost.Visible = True
                        'Task:2826 Apply Checked Security Rights
                    ElseIf RightsDt.FormControlName = "Checked" Then
                        If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = True
                        Me.chkChecked.Visible = True
                    ElseIf RightsDt.FormControlName = "GetAll" Then
                        'Me.btnGetAllRecord.Enabled = True
                        IsGetAllAllowed = True
                        'End Task:2826
                    End If
                Next

                If getConfigValueByType("UpdatePostedVoucher").ToString = "False" Then
                    If Me.BtnSave.Text <> "&Save" Then
                        If grdVouchers.RowCount > 0 Then
                            If Me.grdVouchers.GetRow.Cells("Post").Value = True Then
                                Me.BtnSave.Enabled = False
                                'ShowErrorMessage("Your not update this record.")
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Me.ValidateInput Then

                'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace
                If IsAllowPayment() = False Then
                    If Val(Me.txtPaymentBeforeBalance.Text) < (Val(txtAmount.Text) + Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
                        ShowErrorMessage("Payment amount exceeds available balance")
                        Me.cmbCashAccount.Focus()
                        Exit Sub
                    End If
                End If
                'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace



                'Me.grd.Rows.Add(Me.txtVoucherNo.Text, Me.dtVoucherDate.Value, Me.cmbAccounts.ActiveRow.Cells(1).Value, Me.cmbAccounts.ActiveRow.Cells(0).Value, Me.txtAmount.Text.Trim, Me.cmbVoucherType.Text.ToString, Me.cmbVoucherType.SelectedValue, Me.cmbCashAccount.Text.ToString, Me.cmbCashAccount.SelectedValue, Me.txtChequeNo.Text.Trim, Me.dtChequeDate.Value, Me.txtReference.Text.Trim, Me.txtMemo.Text.Trim)
                ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
                If CheckNumericValue(Me.txtAmount.Text, Me.txtAmount) = False Then
                    Throw New Exception("Amount is not valid.")
                End If
                If CheckNumericValue(Me.txtDiscount.Text, Me.txtDiscount) = False Then
                    Throw New Exception("Amount is not valid.")
                End If
                If CheckNumericValue(Me.txtTax.Text, Me.txtTax) = False Then
                    Throw New Exception("Amount is not valid.")
                End If
                'End Task:2491
                Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
                dtGrd.AcceptChanges()
                Dim drGrd As DataRow
                drGrd = dtGrd.NewRow
                drGrd.Item(0) = 0
                'drGrd.Item(1) = Me.cmbAccounts.ActiveRow.Cells(0).Value
                'drGrd.Item(2) = Me.cmbAccounts.ActiveRow.Cells(1).Text
                'drGrd.Item(3) = Val(Me.txtAmount.Text)
                'drGrd.Item(4) = Val(Me.txtDiscount.Text)
                'drGrd.Item(5) = Val(Me.txtTax.Text)
                'drGrd.Item(6) = Val(Me.txtTaxAmount.Text)
                'drGrd.Item(7) = Me.txtReference.Text.Trim.ToString.Replace("'", "''")
                'drGrd.Item(8) = IIf(Me.GroupBox1.Visible = True, Me.txtChequeNo.Text, DBNull.Value)
                'drGrd.Item(9) = IIf(Me.GroupBox1.Visible = True, Me.dtChequeDate.Value, DBNull.Value)
                drGrd.Item(grdEnm.coa_detail_id) = Me.cmbAccounts.ActiveRow.Cells(0).Value
                drGrd.Item(grdEnm.detail_title) = Me.cmbAccounts.ActiveRow.Cells(1).Text
                drGrd.Item(grdEnm.detail_code) = Me.cmbAccounts.ActiveRow.Cells(2).Text
                ''TASK-407
                drGrd.Item(grdEnm.CurrencyId) = Me.cmbCurrency.SelectedValue
                drGrd.Item(grdEnm.CurrencyAmount) = Val(Me.txtAmount.Text)
                'drGrd.Item(grdEnm.CurrencyAmount) = 
                drGrd.Item(grdEnm.CurrencyRate) = Val(txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drGrd.Item(grdEnm.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drGrd.Item(grdEnm.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If
                ''End TASK-407
                drGrd.Item(grdEnm.Amount) = Val(Me.txtAmount.Text) * Val(txtCurrencyRate.Text)
                drGrd.Item(grdEnm.CurrencyDiscount) = Val(Me.txtDiscount.Text)
                drGrd.Item(grdEnm.Tax) = Val(Me.txtTax.Text)
                drGrd.Item(grdEnm.TaxAmount) = Val(Me.txtTaxAmount.Text)
                'If flgMemoRemarks = False Then
                drGrd.Item(grdEnm.Reference) = Me.txtReference.Text.Trim.ToString.Replace("'", "''")
                'Else
                'drGrd.Item(grdEnm.Reference) = Me.txtMemo.Text.Trim.ToString.Replace("'", "''")
                'End If
                If Me.txtChequeNo.Text.Length > 0 Then
                    currentChequeNo = IIf(Me.GroupBox1.Visible = True, Me.txtChequeNo.Text, DBNull.Value)
                    drGrd.Item(grdEnm.Cheque_No) = currentChequeNo

                    Dim lng As Long = currentChequeNo.Length
                    Dim strLength As String = String.Empty
                    For i As Integer = 0 To lng - 1
                        strLength += "0"
                    Next
                    Dim intChequeNo As Integer
                    Dim flg As Boolean = Int32.TryParse(currentChequeNo, intChequeNo)
                    If flg = True Then
                        currentChequeNo = Microsoft.VisualBasic.Right(strLength + CStr((Val(currentChequeNo) + 1)), lng)
                    Else
                        currentChequeNo = currentChequeNo
                    End If
                    Me.txtChequeNo.Text = currentChequeNo
                    currentChequeNo = 0
                End If

                drGrd.Item(grdEnm.Cheque_Date) = IIf(Me.GroupBox1.Visible = True, Me.dtChequeDate.Value, DBNull.Value)


                'R955 R@!   Adding value in grid col payee
                drGrd.Item(grdEnm.PayeeTitle) = IIf(Me.GroupBox1.Visible = True, Me.txtPayee.Text, DBNull.Value)
                'End R955
                'Task:2577 Add Phone Value 
                'Before against task:2631
                'drGrd.Item(11) = Me.cmbAccounts.ActiveRow.Cells("Phone").Value.ToString
                'End Task:2577
                'Task:2631 Changed Column Index
                drGrd.Item(grdEnm.Mobile) = Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString
                drGrd.Item(grdEnm.CostCenterId) = Me.cmbCostCenter.SelectedValue
                drGrd.Item(grdEnm.Type) = Me.cmbAccounts.ActiveRow.Cells("Type").Value.ToString
                drGrd.Item(grdEnm.LoanRequestId) = IIf(Me.cmbLoanRequest.SelectedIndex = -1, 0, Me.cmbLoanRequest.SelectedValue)
                'End Task:2631
                dtGrd.Rows.InsertAt(drGrd, 0)
                dtGrd.AcceptChanges()

                Me.ClearFields("Detail")
                If Me.cmbCashAccount.SelectedIndex > 0 Then
                    Me.cmbVoucherType.Enabled = False
                    Me.cmbCashAccount.Enabled = False
                End If
                'If Me.cmbVoucherType.SelectedIndex > 0 Then
                '    Me.GroupBox1.Enabled = True
                'Else
                '    Me.GroupBox1.Enabled = False
                'End If
                'Me.GetTotal()
                Me.txtPayee.Text = String.Empty ''17-Mar-2014 TASK:M26 Payee Title Reset Control
                Me.cmbAccounts.Focus()
                Me.cmbCurrency.Enabled = False
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Sub ClearFields(Optional ByVal condition As String = "")
        Try
            'dtVoucherDate.Value = Date.Today  'VoucherDate
            'Me.cmbAccounts.Rows(0).Activate()
            ''cmbAccounts.Focus()
            'Me.dtVoucherDate.Focus()
            'Me.dtVoucherDate.Enabled = True
            'txtAmount.Text = "" 'Amount
            GetSecurityRights()
            If condition.Length > 0 Then
                'txtVoucherNo.Text = "" 'VoucherNo
                'cmbVoucherType.SelectedIndex = 0    'Payment Method
                Me.GroupBox1.Enabled = True
                Me.cmbVoucherType.Enabled = True
                Me.cmbCostCenter.Enabled = True
                'cmbVoucherType_SelectedIndexChanged(Me, New EventArgs)
                'GroupBox1.Enabled = True
                Me.txtAmount.Text = String.Empty
                Me.txtDiscount.Text = String.Empty
                Me.txtTax.Text = String.Empty
                Me.txtTaxAmount.Text = String.Empty
                Me.txtDiscount.Text = String.Empty
                'Me.txtChequeNo.Text = String.Empty
                'Me.dtChequeDate.Value = Now
                Me.cmbAccounts.Rows(0).Activate()
                Me.cmbAccounts.Focus()
            End If
            'cmbCashAccount.SelectedIndex = 0 'Paid To
            Me.txtReference.Text = String.Empty
            If condition.Length = 0 Then
                'cmbAccounts.Focus()
                Me.dtVoucherDate.Focus()
                Me.dtVoucherDate.Enabled = True
                If Not Me.cmbAccounts.ActiveRow Is Nothing Then
                    Me.cmbAccounts.Rows(0).Activate()
                End If
                txtAmount.Text = "" 'Amount
                blnEditMode = False
                lngSelectedVoucherId = 0
                Me.BtnSave.Text = "&Save" 'Me.BtnSave.Text = "&Save" 'TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
                Me.chkAllAccounts.Checked = False
                dtVoucherDate.Value = Date.Now 'Voucher Date
                cmbVoucherType.SelectedIndex = 0    'Payment Method
                Me.cmbVoucherType.Enabled = True
                cmbVoucherType_SelectedIndexChanged(Me, New EventArgs)
                Me.txtChequeNo.Text = String.Empty
                'Me.grd.Rows.Clear()
                Me.txtChequeNo.Enabled = True
                Me.dtChequeDate.Enabled = True
                Me.cmbCashAccount.Enabled = True
                FillComboByEdit()
                Me.DisplayDetail(-1)
                Me.dtChequeDate.Value = Date.Now
                Me.txtDiscount.Text = String.Empty
                Me.txtMemo.Text = String.Empty
                Me.txtPayee.Text = String.Empty ''17-Mar-2014 TASK:M26 Payee Title Reset Control
                'AddHandler grd.Click, AddressOf grd_Click
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                Me.dtpFrom.Value = Date.Now.AddMonths(-1)
                Me.dtpTo.Value = Date.Now
                Me.dtpSearchChequeDate.Value = Date.Now
                Me.dtpFrom.Checked = False
                Me.dtpTo.Checked = False
                Me.dtpSearchChequeDate.Checked = False
                Me.cmbSearchVoucherType.SelectedIndex = 0
                Me.txtSearchVoucherNo.Text = String.Empty
                Me.txtSearchChequeNo.Text = String.Empty
                Me.txtFromAmount.Text = String.Empty
                Me.txtToAmount.Text = String.Empty
                Me.txtSearchComments.Text = String.Empty
                Me.SplitContainer1.Panel1Collapsed = True
                Me.lblPrintStatus.Text = String.Empty
                PopulateGrid()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
                Me.cmbLayout.SelectedIndex = 0 'Task:2375 Reset Index
                FillAccounts("CashRequest")

                'FillCombos("Company")
                Me.cmbCompany.SelectedValue = 1
                If Not Me.cmbCashRequest.SelectedIndex = -1 Then Me.cmbCashRequest.SelectedIndex = 0
                Me.cmbCashRequest.Enabled = True
                FillCombos("LoanRequest")
                FillCombos("Currency")
                If Not Me.cmbLoanRequest.SelectedIndex = -1 Then Me.cmbLoanRequest.SelectedIndex = 0
                'Marked Against Task#2015060001
                'Array.Clear(arrFile, 0, arrFile.Length)
                'Marked Against Task#2015060001 Ali Ansari
                'Altered Against Task#2015060001 Ali Ansari
                'Clear arrfile
                arrFile = New List(Of String)
                'Altered Against Task#2015060001 Ali Ansari
                Me.btnAttachment.Text = "Attachment"
                Me.chkEnableDepositAc.Visible = False
                Me.chkEnableDepositAc.Checked = False
                btnUpdateTimes.Text = "No of update times"
                Me.btnUpdateTimes.Visible = False
                Me.cmbCurrency.SelectedValue = BaseCurrencyId
                Me.cmbCurrency.Enabled = True
                Me.txtCurrencyRate.Enabled = False
                'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"

                'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "C Amount"
                'Me.grd.RootTable.Columns("Amount").Caption = "Amount"
                FillCombos("Company")
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub GetTotal()
    '    Dim i As Integer
    '    Dim dblTotalAmount As Double
    '    For i = 0 To grd.Rows.Count - 1
    '        dblTotalAmount = dblTotalAmount + Val(grd.Rows(i).Cells("Amount").Value)
    '    Next
    '    Me.txtGridTotal.Text = dblTotalAmount
    'End Sub


    'Private Sub grd_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
    '    Me.GetTotal()
    'End Sub

    'Private Sub grd_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    With Me.grd.CurrentRow
    '        GetTotal()
    '    End With
    'End Sub
    'Private Sub grd_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    If grd.Rows.Count = 0 Then Exit Sub
    '    If Me.blnEditMode = False Then Exit Sub

    '    Me.cmbAccounts.Value = grd.CurrentRow.Cells(3).Value.ToString
    '    Me.txtAmount.Text = grd.CurrentRow.Cells(4).Value.ToString
    '    Me.txtReference.Text = Me.grd.CurrentRow.Cells("Reference").Value.ToString
    '    Me.txtMemo.Text = Me.grd.CurrentRow.Cells("Memo").Value.ToString
    '    ''get Deposit in 

    '    Dim str As String

    '    str = " SELECT DISTINCT vwCOADetail.detail_title, tblVoucherDetail.coa_detail_id" _
    '          & " FROM         tblVoucherDetail INNER JOIN" _
    '          & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
    '          & " Where voucher_id =" & grd.Rows(0).Cells(0).Value.ToString & " AND (tblVoucherDetail.credit_amount > 0)"

    '    Dim objCommand As New OleDbCommand

    '    Dim objDataAdapter As New OleDbDataAdapter
    '    Dim dt As New DataTable

    '    If Con.State = ConnectionState.Open Then Con.Close()

    '    Con.Open()
    '    objCommand.Connection = Con
    '    objCommand.CommandType = CommandType.Text


    '    objCommand.CommandText = str

    '    objDataAdapter.SelectCommand = objCommand
    '    objDataAdapter.Fill(dt)

    '    If dt.Rows.Count = 0 Then Exit Sub

    '    Me.cmbCashAccount.SelectedValue = dt.Rows(0)(1).ToString

    'End Sub

    Private Sub cmbAccounts_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccounts.Enter
        Me.cmbAccounts.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try
            If Me.ToolStripComboBox1.SelectedIndex = 0 Then
                Dim CustId As Integer = 0
                CustId = Me.cmbAccounts.Value
                FrmAddCustomers.FormType = "Vendor"
                FrmAddCustomers.ShowDialog()
                FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c], vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id  where detail_title is not null AND vwCOADetail.Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "  order by 2")
                '"select coa_detail_id,detail_title, sub_sub_title AS [Sub Sub A/c] from vwCoaDetail where account_type = 'Vendor' AND vwCOADetail.Active=1 and detail_title is not null order by 2 ")
                If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("PayeeTitle").Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(1).AutoSizeMode = Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
                End If
                Me.cmbAccounts.Value = CustId
            ElseIf Me.ToolStripComboBox1.SelectedIndex = 1 Then
                Dim CustId As Integer = 0
                CustId = Me.cmbAccounts.Value
                FrmAddCustomers.FormType = "Expense"
                FrmAddCustomers.ShowDialog()
                FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c], vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id  where detail_title is not null AND vwCOADetail.Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "  order by 2")
                '"select coa_detail_id,detail_title,sub_sub_title AS [Sub Sub A/c] from vwCoaDetail where account_type = 'Expense' AND vwCOADetail.Active=1 and detail_title is not null order by 2 ")
                If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("PayeeTitle").Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(1).AutoSizeMode = Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
                End If
                Me.cmbAccounts.Value = CustId
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadAll.Click
    '    Try
    '        Me.PopulateGrid("All")
    '        DisplayDetail(-1)
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try
    'End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    Try

    '        If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '            Me.BtnLoadAll.Visible = False
    '            Me.ToolStripButton1.Visible = False
    '            ToolStripSeparator2.Visible = False
    '        Else
    '            Me.BtnLoadAll.Visible = True
    '            Me.ToolStripButton1.Visible = True
    '            ToolStripSeparator2.Visible = True
    '        End If
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer = 0
            id = Me.cmbAccounts.SelectedRow.Cells(0).Value
            FillAccounts()
            Me.cmbAccounts.Value = id

            id = Me.cmbCostCenter.SelectedValue
            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name From tblDefCostCenter ORDER BY 2 ASC")
            Me.cmbCostCenter.SelectedValue = id

            If Not getConfigValueByType("SalesDiscountAccount").ToString = "Errro" Then
                DiscountAccountId = Convert.ToInt32(getConfigValueByType("SalesDiscountAccount").ToString)
            End If

            id = Me.cmbCashRequest.SelectedIndex
            FillAccounts("CashRequest")
            Me.cmbCashRequest.SelectedIndex = id

            FillAccounts("GrdCostCenter")
            FillCombos("Company")
            'Altered Against Task#2015060001 Ali Ansari
            'Clear Attached file records
            'arrFile = New List(Of String)
            'Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Sub FillAccounts(Optional ByVal Condition As String = "")
        Try

            If blnEditMode = False Then
                If Me.chkAllAccounts.Checked = True Then
                    'Before against task:2577 
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c] from vwCoaDetail  where detail_title is not null AND vwCOADetail.Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "  order by 2") 'where account_type='Vendor'")
                    'Task:2577 Added Field Phone And Join Vendor Information Table
                    'Before against task:2631
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c], Ven.Phone from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id  where detail_title is not null AND vwCOADetail.Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "  order by 2") 'where account_type='Vendor'")
                    'End Task:2577
                    'Task:2631 Added Field Mobile
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c], vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id  where detail_title is not null AND vwCOADetail.Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "  order by 2") 'where account_type='Vendor'")
                    FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c], vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle, Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id  where detail_title is not null AND vwCOADetail.Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & "  order by 2") 'where account_type='Vendor'")
                    'End Task:2631
                    Me.cmbAccounts.Rows(0).Activate()
                    If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("AccessLevel").Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("PayeeTitle").Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns(1).AutoSizeMode = Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
                    End If
                Else
                    'Before against task:2577
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c] from vwCoaDetail  where detail_title is not null AND Account_Type='Vendor' AND vwCOADetail.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'Task:2577 Added Field Phone And Join Vendor Information Table
                    'Before against task:2631
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],Ven.Phone from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null AND Account_Type='Vendor' AND vwCOADetail.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'Task:2631 Added Field Mobile 
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null AND Account_Type='Vendor' AND vwCOADetail.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle, Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null AND Account_Type='Vendor' AND vwCOADetail.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'End Task:2631
                    'End Task:2577
                    Me.cmbAccounts.Rows(0).Activate()
                    If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("AccessLevel").Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("PayeeTitle").Hidden = True
                    End If
                End If
            Else
                If Me.chkAllAccounts.Checked = True Then
                    'Before against task:2577
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c] from vwCoaDetail  where detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'Task:2577 Added Field Phone And Join Vendor Information Table
                    'Before against task:2631
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],Ven.Phone from vwCoaDetail  LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'Task:2631 Added Field Mobile
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle from vwCoaDetail  LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle, Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail  LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'End Task:2631
                    'End Task:2577

                    Me.cmbAccounts.Rows(0).Activate()
                    If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("AccessLevel").Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("PayeeTitle").Hidden = True
                    End If
                Else
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c] from vwCoaDetail  where detail_title is not null AND Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'Before against task:2631
                    'Task:2577 Added Field Phone And Join Vendor Information Table
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],Ven.Phone from vwCoaDetail  LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null AND Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'End Task:2577
                    'Task:2631 Added Field Mobile
                    'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle from vwCoaDetail  LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null AND Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code, dbo.vwCOADetail.sub_sub_title AS [Sub Sub A/c],vwCoaDetail.Contact_Phone as Phone, dbo.vwCoaDetail.Contact_Mobile as Mobile, Ven.PayeeTitle, Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail  LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOADetail.coa_detail_id where detail_title is not null AND Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & " order by 2") 'where account_type='Vendor'")
                    'End Task:2631
                    Me.cmbAccounts.Rows(0).Activate()
                    If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("AccessLevel").Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("PayeeTitle").Hidden = True
                        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("Phone").Hidden = True 'Task:2577 Set Hidden Field
                    End If
                End If
            End If



            Dim dt1 As New DataTable
            dt1.Columns.Add("Id", GetType(Integer))
            dt1.Columns.Add("Name", GetType(String))

            Dim dr As DataRow
            dr = dt1.NewRow
            dr(0) = Convert.ToInt32(4)
            dr(1) = "Bank"
            dt1.Rows.InsertAt(dr, 0)

            Dim dr1 As DataRow
            dr1 = dt1.NewRow
            dr1(0) = Convert.ToInt32(2)
            dr1(1) = "Cash"
            dt1.Rows.InsertAt(dr1, 0)

            Dim dr2 As DataRow
            dr2 = dt1.NewRow
            dr2(0) = Convert.ToInt32(0)
            dr2(1) = ".... Select any value ...."
            dt1.Rows.InsertAt(dr2, 0)

            Me.cmbSearchVoucherType.DisplayMember = "Name"
            Me.cmbSearchVoucherType.ValueMember = "Id"
            Me.cmbSearchVoucherType.DataSource = dt1


            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name From tblDefCostCenter ORDER BY 2 ASC")

            If Condition = "CashRequest" Then
                FillDropDown(Me.cmbCashRequest, "Select RequestId, RequestNo + '~' + Convert(Varchar,Convert(Varchar, RequestDate,102)) as RequestNo From CashRequestHead WHERE RequestId In(Select RequestId From CashRequestDetail Group By RequestId Having SUM(IsNull(Amount,0)-IsNull(Paid_Amount,0)) > 0) AND Approved=1")
            End If

            If Condition = "GrdCostCenter" Then
                Dim str As String = "Select CostCenterId, Name From tblDefCostCenter Union Select 0, '" & strZeroIndexItem & "' Order by 2 ASC"
                Dim dt As New DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()
                Me.grd.RootTable.Columns("CostCenterId").ValueList.PopulateValueList(dt.DefaultView, "CostCenterId", "Name")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkAllAccounts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllAccounts.Click
        Try
            FillAccounts()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub cmbCashAccount_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCashAccount.SelectedValueChanged
        Try
            If Me.cmbCashAccount.SelectedIndex > 0 Then
                Try
                    Dim objCommand As New OleDbCommand("SELECT SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount) FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id" _
                                                    & " WHERE tblVoucherDetail.coa_detail_id = " & Me.cmbCashAccount.SelectedValue _
                                                                           & " AND tblVoucher.Post=1", Con)
                    If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
                    objCommand.Connection = Con
                    Me.txtPaymentBeforeBalance.Text = CInt(Val(objCommand.ExecuteScalar))
                    If Val(txtPaymentBeforeBalance.Text) < 0 Then
                        Me.txtPaymentBeforeBalance.Text = "(" & Replace(Me.txtPaymentBeforeBalance.Text, "-", "") & ")"
                    End If
                Catch ex As Exception
                    txtPaymentBeforeBalance.Text = 0
                End Try
            Else
                Me.txtPaymentBeforeBalance.Text = 0
            End If


            '' Get Cheque No By Serial No
            If Me.cmbVoucherType.SelectedIndex > 0 Then
                If getConfigValueByType("EnableAutoChequeBook").ToString = "True" Then
                    If Me.cmbCashAccount.SelectedIndex > 0 Then
                        FillCombos("Cheque")
                        End If
                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub cmbAccounts_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccounts.Leave
        Try
            If Me.cmbAccounts.IsItemInList = False Then Exit Sub
            If cmbAccounts.ActiveRow.Cells(0).Value > 0 AndAlso (LoginGroup = "Administrator" Or (Me.cmbAccounts.ActiveRow.Cells("AccessLevel").Value.ToString = "Everyone")) Then
                Try

                    FillCombos("LoanRequest")

                    Dim objCommand As New OleDbCommand("SELECT SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount) FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id" _
                                                                           & " WHERE tblVoucherDetail.coa_detail_id = " & cmbAccounts.Value _
                                                                            & "AND tblVoucher.Post=1", Con)
                    If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
                    objCommand.Connection = Con

                    txtCustomerBalance.Text = Math.Round(Val(objCommand.ExecuteScalar), 0)
                    If Val(txtCustomerBalance.Text) < 0 Then
                        Me.txtCustomerBalance.Text = "(" & Replace(Me.txtCustomerBalance.Text, "-", "") & ")"
                    End If
                    Me.txtPayee.Text = Me.cmbAccounts.ActiveRow.Cells("PayeeTitle").Value.ToString

                Catch ex As Exception
                    txtCustomerBalance.Text = 0
                End Try
            Else
                Me.txtCustomerBalance.Text = 0
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub FillComboByEdit()
        Try
            FillAccounts()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''R955 Added Open Cheque Print Event
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try

            'If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub 'Before against r:955
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub 'R:955 Change Location Of Confirm Message
                If Not Val(Me.grd.GetRow.Cells("VoucherDetailId").Value.ToString) > 0 Then
                    Me.grd.GetRow.Delete()
                    If Me.grd.RowCount = 0 Then
                        Me.cmbCurrency.Enabled = True
                    End If
                Else
                    Exit Sub
                End If
            ElseIf e.Column.Key = "PrintCheque" Then
                AddRptParam("@Account_Name", IIf(grd.GetRow.Cells("PayeeTitle").Value.ToString <> "", grd.GetRow.Cells("PayeeTitle").Value.ToString, "-"))
                AddRptParam("@Amount", Val(grd.GetRow.Cells("Amount").Value.ToString) - Val(grd.GetRow.Cells("Discount").Value.ToString) - Val(grd.GetRow.Cells("Tax_Amount").Value.ToString))
                'TFS1201:Rai Haider:25-08-17:Check box Enable for ChequeDate for Enable or Disable Date print on Cheque Print
                'Start Task
                If Me.dtChequeDate.Checked = True Then
                    AddRptParam("@Cheque_Date", IIf(grd.GetRow.Cells("Cheque_No").Value.ToString = "", Now, "" & grd.GetRow.Cells("Cheque_Date").Value.ToString & "") & "")
                Else
                    AddRptParam("@Cheque_Date", Date.MinValue)
                End If
                'Rai Haider:TFS1201
                'End Task
                'Task:2647 Cross Cheque Printing
                AddRptParam("@CrossCheque", IIf(grd.GetRow.Cells("CrossCheq").Value = True, 1, 0))
                frmRptChequePrintReportViewer.ReportName = "rptChequePrint" & Me.cmbLayout.SelectedIndex
                frmRptChequePrintReportViewer.Show()
            End If
            'End Task:2647
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End R:955
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> grdEnm.Reference AndAlso col.Index <> grdEnm.CurrencyDiscount AndAlso col.Index <> grdEnm.Cheque_No AndAlso col.Index <> grdEnm.Cheque_Date AndAlso col.Index <> grdEnm.Tax AndAlso col.Index <> grdEnm.TaxAmount AndAlso col.Index <> grdEnm.PayeeTitle AndAlso col.Index <> grdEnm.CostCenterId AndAlso col.Index <> grdEnm.LoanRequestId AndAlso col.Index <> grdEnm.CurrencyAmount AndAlso col.Index <> grdEnm.CurrencyRate Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next

            Me.grd.RootTable.Columns(grdEnm.TaxAmount).FormatString = "N" & DecimalPointInValue  'Task:2647 Set Rounding Format
            Me.grd.RootTable.Columns(grdEnm.TaxAmount).TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns(grdEnm.TaxCurrencyAmount).FormatString = "N" & DecimalPointInValue  'Task:2647 Set Rounding Format
            Me.grd.RootTable.Columns(grdEnm.TaxCurrencyAmount).TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Amount").TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.grd.RootTable.Columns("CurrencyDiscount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CurrencyDiscount").TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.grd.RootTable.Columns("Discount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Discount").TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    'Task#03082015 Fill Drop down with companies
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Company" Then
                strSQL = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
                FillDropDown(Me.cmbCompany, strSQL, False)
            ElseIf Condition = "LoanRequest" Then
                strSQL = "Select RequestId, RequestNo,RequestDate,EmployeeId from AdvanceRequestTable INNER JOIN tblDefEmployee on tblDefEmployee.Employee_Id = AdvanceRequestTable.EmployeeId WHERE EmpSalaryAccountId=" & Me.cmbAccounts.Value & " AND AdvanceRequestTable.RequestStatus='Approved' Order By RequestId DESC "
                FillDropDown(Me.cmbLoanRequest, strSQL, True)
            ElseIf Condition = "Currency" Then
                strSQL = String.Empty
                strSQL = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbCurrency, strSQL, False)
                Me.cmbCurrency.SelectedValue = BaseCurrencyId
            ElseIf Condition = "Cheque" Then
                strSQL = "SELECT    distinct ChequeMasterTable.ChequeSerialId, ChequeMasterTable.Cheque_No_From +' - '+ ChequeMasterTable.Cheque_No_To As ChequeBookName " _
                        & " FROM         ChequeMasterTable INNER JOIN ChequeDetailTable ON ChequeMasterTable.ChequeSerialId = ChequeDetailTable.ChequeSerialId " _
                        & " where ChequeDetailTable.Cheque_Issued=0 and ChequeMasterTable.BankAcId=" & Me.cmbCashAccount.SelectedValue
                FillDropDown(Me.cmbChequeBook, strSQL, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'End Task#03082015

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    'Private Sub grd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.Click
    '    Try
    '        If grd.RowCount = 0 Then Exit Sub
    '        If Me.blnEditMode = False Then Exit Sub
    '        If Me.grd.GetRow.RowIndex = -1 Then Exit Sub

    '        Me.cmbAccounts.Value = grd.CurrentRow.Cells("coa_detail_id").Value.ToString
    '        Me.txtAmount.Text = grd.CurrentRow.Cells("Amount").Value.ToString
    '        Me.txtDiscount.Text = grd.CurrentRow.Cells("Discount").Value.ToString
    '        Me.txtReference.Text = Me.grd.CurrentRow.Cells("Reference").Value.ToString
    '        'Me.txtMemo.Text = Me.grd.CurrentRow.Cells("Memo").Value.ToString
    '        ''get Deposit in 

    '        Dim str As String

    '        str = " SELECT DISTINCT vwCOADetail.detail_title, tblVoucherDetail.coa_detail_id, tblVoucherDetail.Comments, isnull(tblVoucherDetail.CostCenterId,0) as CostCenterId " _
    '              & " FROM         tblVoucherDetail INNER JOIN" _
    '              & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
    '              & " Where voucher_id =" & Me.grdVouchers.GetRow.Cells("Voucher_Id").Value & " AND (tblVoucherDetail.credit_amount > 0) AND Account_Type IN('Cash','Bank')"

    '        Dim objCommand As New OleDbCommand

    '        Dim objDataAdapter As New OleDbDataAdapter
    '        Dim dt As New DataTable

    '        If Con.State = ConnectionState.Open Then Con.Close()

    '        Con.Open()
    '        objCommand.Connection = Con
    '        objCommand.CommandType = CommandType.Text


    '        objCommand.CommandText = str

    '        objDataAdapter.SelectCommand = objCommand
    '        objDataAdapter.Fill(dt)

    '        If dt.Rows.Count = 0 Then Exit Sub
    '        Me.cmbCashAccount.SelectedValue = Val(dt.Rows(0)(1).ToString)
    '        Me.txtMemo.Text = dt.Rows(0)(2).ToString
    '        Me.cmbCostCenter.SelectedValue = dt.Rows(0)(3).ToString


    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim toEmail As String = String.Empty
        Dim flg As Boolean = False
        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name=N'" & Me.Name & "' AND EmailAlert=1")
            If dtForm.Rows.Count > 0 Then
                flg = True
            Else
                flg = False
            End If
            If flg = True Then
                If AdminEmail <> "" Then
                    Email = New SBModel.Email
                    Email.ToEmail = AdminEmail
                    Email.CCEmail = String.Empty
                    Email.BccEmail = String.Empty
                    Email.Attachment = SourceFile
                    Email.Subject = "" & IIf(setVoucherType = "Cash", "Cash Payment", "Bank Payment") & " " & setVoucherNo & " "
                    Email.Body = "" & IIf(setVoucherType = "Cash", "Cash Payment", "Bank Payment") & " " _
                    & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Prviouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By Softbeats ERP System"
                    Email.Status = "Pending"
                    Call New MailSentDAL().Add(Email)
                End If
            End If
        End If
        SourceFile = String.Empty
        setVoucherNo = String.Empty
        Return EmailSave
    End Function

    'Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
    '    Try
    '        If Me.SplitContainer1.Panel1Collapsed = True Then
    '            Me.SplitContainer1.Panel1Collapsed = False
    '        Else
    '            Me.SplitContainer1.Panel1Collapsed = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search.Click
        Try
            PopulateGrid("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal Voucher_No As String)
        Try
            Get_All = Nothing
            If Not Voucher_No.Length > 0 Then Exit Try
            If IsLoadedForm = True Then
                'Dim dt As DataTable = GetDataTable("Select * From tblVoucher WHERE Voucher_No=N'" & Voucher_No & "'")
                'If dt IsNot Nothing Then
                '    If dt.Rows.Count > 0 Then
                '        Me.BtnSave.Text = "&Update"
                '        Me.chkAllAccounts.Checked = True
                '        blnEditMode = True
                '        'Before against task:2631
                '        'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title, sub_sub_title AS [Sub Sub A/c] from vwCoaDetail  where detail_title is not null AND vwCOADetail.Active=1 order by 2") 'where account_type='Vendor'")
                '        'Task:2631 Added Fields Phone, Mobile
                '        FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id, detail_title, sub_sub_title AS [Sub Sub A/c],Ven.Phone, Ven.Mobile  from vwCoaDetail LEFT OUTER JOIN tblVendor Ven On Ven.AccountId = vwCOAdetail.coa_detail_id where detail_title is not null AND vwCOADetail.Active=1 order by 2") 'where account_type='Vendor'")
                '        'End Task:2631
                '        Me.cmbAccounts.Rows(0).Activate()
                '        If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                '            Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                '            Me.cmbAccounts.DisplayLayout.Bands(0).Columns(1).AutoSizeMode = Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
                '        End If
                '        lngSelectedVoucherId = dt.Rows(0).Item("Voucher_Id").ToString
                '        'Me.txtChequeNo.Text = dt.Rows(0).Item("cheque_no").ToString
                '        'If Not IsDBNull(dt.Rows(0).Item("cheque_date")) Then
                '        '    If dt.Rows(0).Item("cheque_date") <> DateTime.MinValue Then
                '        '        Me.dtChequeDate.Value = dt.Rows(0).Item("cheque_date")
                '        '    End If
                '        'End If
                '        cmbVoucherType.SelectedValue = dt.Rows(0).Item("Voucher_Type_Id").ToString    'Payment Method
                '        txtVoucherNo.Text = dt.Rows(0).Item("Voucher_No").ToString  'VoucherNo
                '        dtVoucherDate.Value = dt.Rows(0).Item("Voucher_Date").ToString  'VoucherDate
                '        'cmbCashAccount.SelectedValue = grdVouchers.CurrentRow.Cells(6).Value.ToString 'Paid To
                '        'Me.cmbBank.Text = dt.Rows(0).Item("BankDesc").Text.ToString
                '        RemoveHandler grd.Click, AddressOf grd_Click
                '        Me.DisplayDetail(lngSelectedVoucherId)
                '        Me.cmbVoucherType.Enabled = False
                '        If cmbVoucherType.SelectedIndex > 0 Then
                '            Me.GroupBox1.Visible = True
                '            Me.GroupBox1.Enabled = True
                '        Else
                '            Me.GroupBox1.Visible = False
                '            Me.GroupBox1.Enabled = False
                '        End If
                '        'Me.cmbCashAccount.Enabled = False
                '        'Me.txtChequeNo.Enabled = False
                '        'Me.dtChequeDate.Enabled = False
                '        Me.chkPost.Checked = dt.Rows(0).Item("Post")
                '        GetSecurityRights()
                '        'Me.GetTotal()
                '        Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
                '        IsDrillDown = True
                '        Me.cmbAccounts.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)



                '        Dim str As String

                '        str = " SELECT DISTINCT vwCOADetail.detail_title, tblVoucherDetail.coa_detail_id, tblVoucherDetail.Comments, isnull(tblVoucherDetail.CostCenterId,0) as CostCenterId " _
                '              & " FROM         tblVoucherDetail INNER JOIN" _
                '              & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
                '              & " Where voucher_id =" & lngSelectedVoucherId & " AND (tblVoucherDetail.credit_amount > 0) AND Account_Type IN('Cash','Bank')"

                '        Dim objCommand As New OleDbCommand
                '        Dim objDataAdapter As New OleDbDataAdapter
                '        Dim dt1 As New DataTable
                '        If Con.State = ConnectionState.Open Then Con.Close()

                '        Con.Open()
                '        objCommand.Connection = Con
                '        objCommand.CommandType = CommandType.Text
                '        objCommand.CommandText = str

                '        objDataAdapter.SelectCommand = objCommand
                '        objDataAdapter.Fill(dt1)

                '        If dt1.Rows.Count = 0 Then Exit Function
                '        If cmbCashAccount IsNot Nothing Then
                '            Me.cmbCashAccount.SelectedValue = dt1.Rows(0)(1).ToString
                '            Me.cmbCostCenter.SelectedValue = dt1.Rows(0)(3).ToString
                '            Me.txtMemo.Text = dt1.Rows(0)(2).ToString
                '        Else
                '            Me.cmbCashAccount.SelectedValue = 0
                '        End If



                '        If flgDateLock = True Then
                '            If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtVoucherDate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                '                'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                '                Me.dtVoucherDate.Enabled = False
                '            Else
                '                Me.dtVoucherDate.Enabled = True
                '            End If
                '        Else
                '            Me.dtVoucherDate.Enabled = True
                '        End If


                '    End If
                'End If
                IsDrillDown = True
                If Me.grdVouchers.RowCount <= 50 Then
                    Me.btnSearchLoadAll_Click(Nothing, Nothing)
                End If
                Me.grdVouchers.Find(Me.grdVouchers.RootTable.Columns("Voucher_No"), Janus.Windows.GridEX.ConditionOperator.Equal, Voucher_No, 0, 1)
                Me.grdVouchers_DoubleClick(Nothing, Nothing)
                'IsDrillDown = False
            End If

            Return Get_All
        Catch ex As Exception
            Throw ex
        Finally
            frmMain.Tag = String.Empty
        End Try
    End Function

    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            OpenToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bntSearchPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntSearchPrint.ButtonClick
        Try
            PrintToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            DeleteToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLoadAll.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.PopulateGrid("All")
            DisplayDetail(-1)
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSearchDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDocument.Click
        Try

            If Not Me.cmbSearchAccount.IsItemInList Then
                Dim Str As String = "SELECT dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                              " dbo.tblListTerritory.TerritoryName as Territory " & _
                                              " FROM         dbo.tblCustomer INNER JOIN " & _
                                              " dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                              " dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                              " dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                              " dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                              " WHERE (dbo.vwCOADetail.account_type <> 'Inventory') and detail_title is not null"
                If flgCompanyRights = True Then
                    Str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If
                Str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
                FillUltraDropDown(Me.cmbSearchAccount, Str)
                Me.cmbSearchAccount.Rows(0).Activate()
                If Me.cmbSearchAccount.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            Else
                Me.cmbSearchAccount.Rows(0).Activate()
            End If
            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                'PopulateGrid()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnPrint.Visible = True
                Me.BtnDelete.Visible = True
            Else
                If blnEditMode = False Then Me.BtnDelete.Visible = False
                If blnEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt = New ReportDocument
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\rptVoucher.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\rptVoucher.rpt", DBServerName)
                    If DBUserName <> "" Then
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                        crpt.DataSourceConnections.Item(0).SetLogon(DBName, DBPassword)
                    Else
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
                    End If

                    Dim ConnectionInfo As New ConnectionInfo
                    With ConnectionInfo
                        .ServerName = DBServerName
                        .DatabaseName = DBName
                        If DBUserName <> "" Then
                            .UserID = DBUserName
                            .Password = DBPassword
                            .IntegratedSecurity = False
                        Else
                            .IntegratedSecurity = True
                        End If
                    End With
                    Dim tbLogOnInfo As New TableLogOnInfo
                    For Each dt As Table In crpt.Database.Tables
                        tbLogOnInfo = dt.LogOnInfo
                        tbLogOnInfo.ConnectionInfo = ConnectionInfo
                        dt.ApplyLogOnInfo(tbLogOnInfo)
                    Next

                    'crpt.RecordSelectionFormula = "{VwGlVoucherSingle.VoucherId}=" & VoucherId & ""
                    crpt.SetParameterValue("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value) '"{VwGlVoucherSingle.VoucherId}=" & VoucherId & ""
                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Voucher" & "-" & setVoucherNo & ""
                    SourceFile = String.Empty
                    SourceFile = _FileExportPath & "\" & FileName & ".pdf"
                    crDiskOps.DiskFileName = SourceFile
                    crExportOps = crpt.ExportOptions
                    With crExportOps
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                        .ExportDestinationOptions = crDiskOps
                        .ExportFormatOptions = crExportType
                    End With
                    'crpt.Refresh()
                    crpt.Export()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            ExportFile(GetVoucherId)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            EmailSave()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtReference_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtReference.Validating
        Try
            SpellChecker(Me.txtReference)
            If Me.txtMemo.Text.Trim.ToString.Length = 0 Then
                Me.txtMemo.Text = txtReference.Text
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMemo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMemo.Validating
        Try
            SpellChecker(Me.txtMemo)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReminder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReminder.Click
        Try
            Dim frm As New frmreminder
            If Me.grdVouchers.RowCount = 0 Then Exit Sub
            frm.txtSubject.Text = "" & IIf(Me.cmbVoucherType.Text = "Cash", "Cash Receipt", "Bank Receipt") & " " & Me.grdVouchers.GetRow.Cells("Voucher_No").Text & " "
            frm.txtmessage.Text = "" & IIf(setVoucherType = "Cash", "Cash Receipt", "Bank Receipt") & " " _
                    & " " & IIf(setEditMode = False, "of amount " & Me.grdVouchers.GetRow.Cells("Amount").Value & " is made", "of amount " & Me.grdVouchers.GetRow.Cells("Amount").Value & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By Softbeats ERP System"
            frm.ShowDialForm = True
            frm.ComboBox1.Text = "Only My"
            frm.ShowDialog()
            frm.Close()
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReminder1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReminder1.Click
        Try
            btnReminder_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtTax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTax.TextChanged
        Try
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If CheckNumericValue(Me.txtTax.Text, Me.txtTax) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            'End Task:2491
            Me.txtTaxAmount.Text = (((Val(Me.txtAmount.Text) - Val(Me.txtDiscount.Text)) * Val(Me.txtTax.Text)) / 100)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscount.TextChanged
        Try
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If CheckNumericValue(Me.txtDiscount.Text, Me.txtDiscount) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            'End Task:2491
            txtTax_TextChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged
        Try
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If CheckNumericValue(Me.txtAmount.Text, Me.txtAmount) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            'End task
            txtTax_TextChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPaymentToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdVouchers.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdVouchers.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
            'Chaning Against Request No 798
            AddRptParam("@vid", Me.grdVouchers.CurrentRow.Cells(0).Value)
            ShowReport("rptCashPayment")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintPaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPaymentToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdVouchers.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdVouchers.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
            'Chaning Against Request No 798
            AddRptParam("@vid", Me.grdVouchers.CurrentRow.Cells(0).Value)
            ShowReport("rptCashPayment")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintSelectedVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSelectedVouchersToolStripMenuItem.Click
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdVouchers.RowCount = 0 Then Exit Sub
            Dim cont As Integer = Me.grdVouchers.GetCheckedRows.Length
            If cont > 0 Then
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdVouchers.GetCheckedRows
                    'AddRptParam("@vid", r.Cells(0).Value)
                    'ShowReport("rptCashPayment", , , , True)
                    'PrintLog = New SBModel.PrintLogBE
                    'PrintLog.DocumentNo = r.Cells("Voucher_No").Value.ToString
                    'PrintLog.UserName = LoginUserName
                    'PrintLog.PrintDateTime = Date.Now
                    'Call SBDal.PrintLogDAL.PrintLog(PrintLog)

                    PrintVoucherBC(r.Cells("voucher_id").Value, r.Cells("Voucher_No").Value.ToString())
                Next
            Else
                PrintVoucherBC(Me.grdVouchers.GetRow.Cells("voucher_id").Value, Me.grdVouchers.GetRow.Cells("Voucher_No").Value.ToString())
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    ''ReqId 833 Added Function Is Credit Limit
    Public Function IsCreditLimit(ByVal AccountId As Integer) As Double
        Try
            Dim Query As String = String.Empty 'Create Variable
            Query = "SELECT Isnull(V.Credit_Limit,0) as Credit_Limit FROM dbo.vwCOADetail COA INNER JOIN dbo.tblVendor V ON COA.coa_detail_id = V.AccountId WHERE (COA.coa_detail_id = " & AccountId & " And COA.Account_Type='Vendor')"
            Dim dt As New DataTable 'Create Object DataTable 
            dt = GetDataTable(Query) 'Fill DataTable
            If dt IsNot Nothing Then 'Validate DataTable After Changing
                If dt.Rows.Count > 0 Then 'Validate Row Count
                    Return dt.Rows(0).Item(0) 'Return Value
                Else
                    Return 0 'Return Zero
                End If
            Else
                Return 0 'Return Zero
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
        'Comment against TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(Nothing, Nothing)
        '    Exit Sub
        'End If
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub grdVouchers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVouchers.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdVouchers.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub
    Private Sub cmbCashRequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCashRequest.SelectedIndexChanged
        Try
            If IsLoadedForm = False Then Exit Sub
            If Me.cmbCashRequest.SelectedIndex > 0 Then
                CashRequestDetail(Me.cmbCashRequest.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task:2728 Added Function Comments
    Public Function GetComments(ByVal Row As Janus.Windows.GridEX.GridEXRow) As String
        Try
            Dim str As String = String.Empty
            Dim blnCommentsChequeNo As Boolean = Boolean.Parse(getConfigValueByType("CommentsChequeNo").ToString)
            Dim blnCommentsChequeDate As Boolean = Boolean.Parse(getConfigValueByType("CommentsChequeDate").ToString)
            Dim blnCommentsPartyName As Boolean = Boolean.Parse(getConfigValueByType("CommentsPartyName").ToString)
            If Me.cmbVoucherType.Text = "Bank" Then
                If Row IsNot Nothing Then
                    If blnCommentsChequeNo = True Then
                        str += " Chq No. " & Row.Cells("Cheque_No").Value.ToString & ""
                    End If
                    If blnCommentsChequeDate = True Then
                        str += " Chq Date. " & Row.Cells("Cheque_Date").Value.ToString & ""
                    End If
                    If blnCommentsPartyName = True Then
                        str += " Party Name. " & Row.Cells("detail_title").Value.ToString & ""
                    End If
                End If
            End If
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2728

    Private Sub btnAttachment_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachment.ButtonClick
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            'Marked Against Task#2015060005 
            'OpenFileDialog1.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Marked Against Task#2015060005 

            '            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Altered Against Task#2015060006 to make all files attachement physible
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
           "All files (*.*)|*.*"

            'Altered Against Task#2015060006 to make all files attachement physible
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                'Altered Against Task#2015060001 Ali Ansari
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                'Altered Against Task#2015060001 Ali Ansari

                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdVouchers.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdVouchers.CurrentRow.Cells("No Of Attachment").Value)
                    End If
                End If

                'Marked Against Task#2015060001 Ali Ansari
                'Me.btnAttachment.Text = "Attachment (" & arrFile.Length + intCountAttachedFiles & ")"
                'Marked Against Task#2015060001 Ali Ansari
                'Altered Against Task#2015060001 Ali Ansari
                Me.btnAttachment.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
                'Altered Against Task#2015060001 Ali Ansari
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString
            'Marked Against Task#2015060001 Ali Ansari
            'If arrFile.Length > 0 Then
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                'Altered Against Task#2015060001 Ali Ansari
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_" & Me.dtVoucherDate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function

    Private Sub PrintAttachmentVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentVoucherToolStripMenuItem.Click, PrintAttachmentVoucherToolStripMenuItem1.Click
        Try
            If Me.grdVouchers.RowCount = 0 Then Exit Sub
            'AddRptParam("Pm-dtVoucher.Voucher_Id", Me.grdVouchers.GetRow.Cells(0).Value)
            DataSetShowReport("RptVoucherDocument", GetVoucherRecord())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub txtReference_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReference.TextChanged
    '    Try
    '        If Me.txtMemo.Text.Trim.Length = 0 Then
    '            Me.txtMemo.Text = Me.txtReference.Text
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Function GetVoucherRecord() As DataSet
        Try

            Dim strSQL As String = String.Empty
            Dim ds As New dsVoucherDocumentAttachment
            ds.Tables.Clear()
            strSQL = "SELECT  TOP 100 PERCENT V.voucher_id, V.voucher_no, V.voucher_date, V.voucher_code, VTP.voucher_type, V.Reference, V.post, V.BankDesc, V.UserName, " _
                    & " V.Posted_UserName, V.CheckedByUser, V.Checked, VD.voucher_detail_id, VD.coa_detail_id, COA.detail_code, COA.detail_title, VD.comments, VD.debit_amount,  " _
                    & " VD.credit_amount, VD.sp_refrence, VD.direction, VD.CostCenterID, VD.Adjustment, VD.Cheque_No, VD.Cheque_Date, VD.BankDescription, VD.Tax_Percent,  " _
                    & " VD.Tax_Amount, VD.Cheque_Clearance_Date, VD.PayeeTitle, VD.Cheque_Status, VD.ChequeDescription, COA.sub_sub_code, COA.sub_sub_title " _
                    & " FROM dbo.tblVoucher AS V INNER JOIN " _
                    & " dbo.tblVoucherDetail AS VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
                    & " dbo.vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN  " _
                    & " dbo.tblDefVoucherType AS VTP ON V.voucher_type_id = VTP.voucher_type_id WHERE (V.voucher_id=" & Me.grdVouchers.GetRow.Cells(0).Value & ") " _
                    & " ORDER BY VD.voucher_detail_id "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtVoucher"


            strSQL = String.Empty
            'Marked Against Task#2015060008 To Discard Non Images
            'strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Me.grdVouchers.GetRow.Cells(0).Value & ") AND Source=N'" & Me.Name & "'"
            'Marked Against Task#2015060008 To Discard Non Images
            'Altered Against Task#2015060008 To Discard Non Images
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image,1 as pic From DocumentAttachment  where right(filename,3) in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG')  and  DocId=" & Val(Me.grdVouchers.GetRow.Cells(0).Value.ToString) & " AND Source=N'" & Me.Name & "'"
            'Altered Against Task#2015060008 To Discard Non Images
            Dim dtAttach As New DataTable
            dtAttach.TableName = "dtAttachment"
            dtAttach = GetDataTable(strSQL)

            If dtAttach IsNot Nothing Then
                If dtAttach.Rows.Count > 0 Then
                    For Each r As DataRow In dtAttach.Rows
                        If r.Item("PIC").ToString = "1" Then
                            r.BeginEdit()

                            LoadPicture(r, "Attachment_Image", CStr(r("Path").ToString & "\" & r("FileName").ToString))
                            r.EndEdit()
                        End If
                    Next

                End If

            End If

            ds.Tables.Add(dtAttach)
            ds.Tables(1).TableName = "dtAttachment"
            ds.AcceptChanges()

            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''23-Sep-2014 Task:2854 Imran Ali Attachment Count In History And Preview On Payment/Receipt/Voucher
    Private Sub grdVouchers_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdVouchers.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Me.grdVouchers.GetRow.Cells(0).Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub chkEnableDepositAc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEnableDepositAc.CheckedChanged
        Try
            If Me.BtnSave.Text <> "&Save" Then
                If Me.chkEnableDepositAc.Checked = True Then
                    Me.cmbCashAccount.Enabled = True
                Else
                    Me.cmbCashAccount.Enabled = False
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2854
    'Altered Agains Task#20150514 to send SMS after Message Ali Ansari
#Region "SMS Template Setting"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@AccountCode")
            str.Add("@AccountTitle")
            str.Add("@DocumentNo")
            str.Add("@DocumentDate")
            str.Add("@OtherDocNo")
            str.Add("@Remarks")
            str.Add("@Amount")
            str.Add("@ChequeNo")
            str.Add("@ChequeDate")
            str.Add("@CompanyName")
            str.Add("@CellNo")
            str.Add("@Softbeats")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Cash Payment")
            str.Add("Bank Payment")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region
    Public Sub SendSMS()

        Try
            If Me.chkPost.Checked = True Then
                If EnabledBrandedSMS = True Then
                    If GetSMSConfig("Payment").Enable = True Or GetSMSConfig("Payment").EnabledAdmin = True Then
                        If msg_Confirm(str_ConfirmSendSMSMessage) = True Then
                            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                                'If (r.Cells("Mobile").Value.ToString <> "" Or r.Cells("Mobile").Value.ToString.Length >= 10) Then
                                Dim objSMSTemp As New SMSTemplateParameter
                                Dim strMSGBody As String = String.Empty ' Task:2631 Added object
                                If Me.cmbVoucherType.Text = "Bank" Then
                                    objSMSTemp = GetSMSTemplate("Bank Payment")
                                Else
                                    objSMSTemp = GetSMSTemplate("Cash Payment")
                                End If
                                If objSMSTemp IsNot Nothing Then
                                    Dim objSMSParam As New SMSParameters
                                    objSMSParam.AccountCode = r.Cells("detail_code").Value.ToString
                                    objSMSParam.AccountTitle = r.Cells("detail_title").Value.ToString
                                    objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                                    objSMSParam.DocumentDate = Me.dtVoucherDate.Value
                                    objSMSParam.Remarks = Me.txtMemo.Text
                                    objSMSParam.CellNo = r.Cells("Mobile").Value.ToString.Replace("-", "").Replace("_", "").Replace(".", "").Replace("+", "").Replace("@", "").Replace(";", "") 'r.Cells("Mobile").Value.ToString
                                    objSMSParam.Amount = Math.Round((Val(r.Cells("Amount").Value.ToString) - (Val(r.Cells("Discount").Value.ToString) + Val(r.Cells("Tax_Amount").Value.ToString))), 0)
                                    If Me.cmbVoucherType.Text = "Bank" Then
                                        objSMSParam.ChequeNo = r.Cells("Cheque_No").Value.ToString
                                        If IsDBNull(r.Cells("Cheque_Date").Value) Then
                                            objSMSParam.ChequeDate = Nothing
                                        Else
                                            objSMSParam.ChequeDate = r.Cells("Cheque_Date").Value
                                        End If
                                    End If
                                    objSMSParam.CompanyName = CompanyTitle
                                    Dim objSMSLog As SMSLogBE
                                    If GetSMSConfig("Payment").EnabledAdmin = True Then
                                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                                            If strMob.Length > 10 Then
                                                objSMSLog = New SMSLogBE
                                                objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                                                objSMSLog.PhoneNo = strMob 'r.Cells("Mobile").Value.ToString
                                                objSMSLog.CreatedByUserID = LoginUserId
                                                Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                                            End If
                                        Next
                                    End If

                                    If GetSMSConfig("Payment").Enable = True AndAlso (r.Cells("Mobile").Value.ToString.Trim.Length >= 10) Then
                                        objSMSLog = New SMSLogBE
                                        objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                                        objSMSLog.PhoneNo = r.Cells("Mobile").Value.ToString.Replace("-", "").Replace("_", "").Replace(".", "").Replace("+", "").Replace("@", "").Replace(";", "")
                                        objSMSLog.CreatedByUserID = LoginUserId
                                        Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                                    End If

                                End If
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    'Altered Agains Task#20150514 to send SMS after Message Ali Ansari
    Private Sub Btn_SaveAttachmen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SaveAttachmen.Click
        'Dim objTrans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDbTransaction = Con.BeginTransaction

        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty

            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
                        "All files (*.*)|*.*"

            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a

                If arrFile.Count > 0 Then
                    SaveDocument(Me.grdVouchers.CurrentRow.Cells(0).Value, Me.Name, trans)
                    trans.Commit()
                    ClearFields()
                    PopulateGrid()

                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    'Task#04082015 company changed from combo box
    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If Not blnEditMode = True Then
                Me.txtVoucherNo.Text = GetVoucherNo()
            End If
            'Ali Faisal : TFS1296 : Select the Cost Center for seleted company if that cost center is associated with that company : 21-Aug-2017
            If Not Me.cmbCompany.SelectedIndex = -1 Then
                Me.cmbCostCenter.SelectedValue = Val(CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString)
            End If
            'Ali Faisal : TFS1296 : End
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#04082015

    Public Sub MainEditRecord()
        Try
            Dim str As String = " SELECT DISTINCT vwCOADetail.detail_title, tblVoucherDetail.coa_detail_id, tblVoucherDetail.Comments, isnull(tblVoucherDetail.CostCenterId,0) as CostCenterId " _
                  & " FROM         tblVoucherDetail INNER JOIN" _
                  & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
                  & " Where voucher_id =" & Me.grdVouchers.GetRow.Cells("Voucher_Id").Value & " AND (tblVoucherDetail.credit_amount > 0) AND Account_Type IN('Cash','Bank')"
            Dim objCommand As New OleDbCommand
            Dim objDataAdapter As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim objCon As New OleDbConnection(Con.ConnectionString)
            If objCon.State = ConnectionState.Open Then objCon.Close()

            objCon.Open()
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = str

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)
            dt.AcceptChanges()
            objCon.Close()

            If dt.Rows.Count = 0 Then Exit Sub
            If cmbCashAccount IsNot Nothing Then
                Me.cmbCashAccount.SelectedValue = dt.Rows(0)(1).ToString
                Me.cmbCostCenter.SelectedValue = dt.Rows(0)(3).ToString
                Me.txtMemo.Text = dt.Rows(0)(2).ToString
            Else
                Me.cmbCashAccount.SelectedValue = 0
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnUpdatedVoucher_Click(sender As Object, e As EventArgs) Handles btnUpdatedVoucher.Click
        Try
            If grdVouchers.RowCount = 0 Then Exit Sub
            AddRptParam("@VoucherId", Val(Me.grdVouchers.GetRow.Cells("Voucher_Id").Value.ToString))
            ShowReport("rptVoucherUpdated")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintUpdateVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintUpdateVoucherToolStripMenuItem.Click
        Try
            Me.btnUpdatedVoucher_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function DTFromGrid(ByVal voucherID As Int32) As DataTable 'TASK42
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdVouchers.RowCount = 0 Then Exit Function
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            Dim DT As New DataTable
            DT = GetDataTable("SP_RptVoucher " & voucherID & "") ' r.Cells(EnumGridMaster.Voucher_Id).Value
            DT.AcceptChanges()
            'DT.Columns.Add("Convert(image, null) as BarCode")
            'Next
            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                'bcp.Symbology = Symbology.Code39
                bcp.Symbology = Symbology.Code128
                'bcp.Symbology = Symbology.Code93



                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                'bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                'bcp.BarWidth = 3
                'bcp.BarHeight = 0.04F
                'DR.Item("Convert(image, null) as BarCode")
                bcp.Code = "?" & DR.Item("voucher_no").ToString
                'bcp.Code = DR.Item("Employee_Code").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                'LoadPicture(DR, "Picture", DR.Item("EmpPicture"))
                DR.EndEdit()


            Next
            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub PrintVoucherBC(ByVal voucherID As Integer, Optional ByVal voucherNo As String = Nothing) 'TASK42
        Dim DT As New DataTable
        DT = DTFromGrid(voucherID) 'GetDataTable("SP_RptVoucher " & r.Cells(EnumGridMaster.Voucher_Id).Value & "")
        DT.AcceptChanges()


        '   AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
        ShowReport("rptVoucher", , , , , , , DT)
        PrintLog = New SBModel.PrintLogBE
        PrintLog.DocumentNo = voucherNo 'r.Cells("Voucher_No").Value.ToString
        PrintLog.UserName = LoginUserName
        PrintLog.PrintDateTime = Date.Now
        Call SBDal.PrintLogDAL.PrintLog(PrintLog)

    End Sub

    Private Sub cmbCashAccount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCashAccount.SelectedIndexChanged

    End Sub

    Private Sub grd_CellEdited(sender As Object, e As ColumnActionEventArgs)

    End Sub

    Private Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintUpdatedVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                Me.txtCurrencyRate.Text = dr.Row.Item("CurrencyRate").ToString

                ''TASK TFS1474
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True
                End If
                ''END TASK TFS1474

                Me.grd.RootTable.Columns("CurrencyAmount").Caption = "Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns("CurrencyDiscount").Caption = "Discount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(grdEnm.TaxCurrencyAmount).Caption = "Tax (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns("Amount").Caption = "Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Discount").Caption = "Discount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns(grdEnm.TaxAmount).Caption = "Tax (" & Me.BaseCurrencyName & ")"



                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns("Amount").Visible = False
                    Me.grd.RootTable.Columns("Discount").Visible = False
                    Me.grd.RootTable.Columns(grdEnm.TaxAmount).Visible = False

                Else
                    Me.grd.RootTable.Columns("Amount").Visible = True
                    Me.grd.RootTable.Columns("Discount").Visible = True
                    Me.grd.RootTable.Columns(grdEnm.TaxAmount).Visible = True

                End If

                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellEdited_1(sender As Object, e As ColumnActionEventArgs) Handles grd.CellEdited
        Try
            Me.grd.GetRow.Cells(grdEnm.Amount).Value = Me.grd.GetRow.Cells(grdEnm.CurrencyAmount).Value * Me.grd.GetRow.Cells(grdEnm.CurrencyRate).Value
            If e.Column.Key = "CostCenterId" Then
                If IsVoucherCostCentreReshuffled(Me.grd.GetRow.Cells("DetailId").Value) Then
                    ShowErrorMessage("Cost Centre can not be changed because it has been shifted.")
                    Me.grd.GetRow.Cells(grdEnm.CostCenterId).Value = CostCentreId
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetCurrencyRate(ByVal currencyId As Integer) As Double
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Dim currencyRate As Double = 0
        Try
            str = " Select CurrencyRate, CurrencyId From tblCurrencyRate Where CurrencyRateId in ( Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId) And CurrencyId = " & currencyId & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                currencyRate = Val(dt.Rows.Item(0).Item(0).ToString)
            End If

            Return currencyRate

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub DualPrinting()

        If msg_Confirm_DualPrint(str_ConfirmPrintVoucher, True, True, Msgfrm) = True Then
            If Me.grdVouchers.RowCount = 0 Then Exit Sub

            If Msgfrm.chkEnableVoucherPrints.Checked = True AndAlso Msgfrm.chkEnableSlipPrints.Checked = True Then


                AddRptParam("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
                ShowReport("rptVoucher", , , , True)


                PrintLog = New SBModel.PrintLogBE
                PrintLog.DocumentNo = grdVouchers.GetRow.Cells("Voucher_No").Value.ToString
                PrintLog.UserName = LoginUserName
                PrintLog.PrintDateTime = Date.Now
                Call SBDal.PrintLogDAL.PrintLog(PrintLog)
                AddRptParam("@vid", Me.grdVouchers.CurrentRow.Cells(0).Value)
                ShowReport("rptCashPayment")

            ElseIf Msgfrm.chkEnableVoucherPrints.Checked = True Then
                AddRptParam("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
                ShowReport("rptVoucher", , , , True)

            ElseIf Msgfrm.chkEnableSlipPrints.Checked = True Then
                PrintLog = New SBModel.PrintLogBE
                PrintLog.DocumentNo = grdVouchers.GetRow.Cells("Voucher_No").Value.ToString
                PrintLog.UserName = LoginUserName
                PrintLog.PrintDateTime = Date.Now
                Call SBDal.PrintLogDAL.PrintLog(PrintLog)
                AddRptParam("@vid", Me.grdVouchers.CurrentRow.Cells(0).Value)
                ShowReport("rptCashPayment")
            Else
                msg_Error("Please Select any one Print option")
                DualPrinting()

            End If

        End If
    End Sub
    Private Sub GetAllUsersRecord(Optional ByVal strCondition As String = "")
        Try
            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Dim strSql As String = String.Empty

            If Mode = "Normal" Then

                strSql = "SELECT DISTINCT   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, tblVoucher.Remarks,isnull(PaymentAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(printLog.Cont,0)=0 then 'Print Pending' ELSE 'Printed' end as [Print Status],IsNull(tblVoucher.Checked,0) as Checked, ISNULL(tblVoucher.CashRequestId,0) as CashRequestId, CRH.RequestNo + '~' + Convert(varchar,Convert(varchar, CRH.RequestDate,102)) as RequestNo, IsNull([No Of Attachment],0) as [No Of Attachment],tblVoucher.username as 'User Name',CompanyDefTable.CompanyName  " _
              & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id Left Outer join  CompanyDefTable on tblVoucher.location_id=CompanyDefTable.CompanyId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) PaymentAmount On PaymentAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN CashRequestHead CRH ON CRH.RequestID=dbo.tblVoucher.CashRequestId LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  tblVoucher.Voucher_Id  " _
              & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
                If flgCompanyRights = True Then
                    strSql += " And tblVoucher.Location_Id=" & MyCompanyId
                End If
            End If
            If strCondition <> "" Then
                'strSql &= " AND account_type = N'" & strCondition & "'"
            End If
            If Me.dtpFrom.Checked = True Then
                strSql += " AND tblVoucher.Voucher_Date >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) "
            End If
            If Me.dtpTo.Checked = True Then
                strSql += " AND tblVoucher.Voucher_Date <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102) "
            End If
            If Me.cmbSearchVoucherType.SelectedIndex > 0 Then
                strSql += " AND tblVoucher.Voucher_Type_Id=" & Me.cmbSearchVoucherType.SelectedValue
            End If
            If Me.txtSearchVoucherNo.Text <> String.Empty Then
                strSql += " AND tblvoucher.Voucher_No LIKE '%" & Me.txtSearchVoucherNo.Text & "%'"
            End If
            If Me.txtSearchChequeNo.Text <> String.Empty Then
                strSql += " AND tblVoucherDetail.Cheque_No LIKE '%" & Me.txtSearchChequeNo.Text & "%'"
            End If
            If Me.dtpSearchChequeDate.Checked = True Then
                strSql += " AND (Convert(Varchar, tblVoucherDetail.Cheque_Date,102) = Convert(Datetime, N'" & Me.dtpSearchChequeDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))"
            End If
            If Me.txtFromAmount.Text <> String.Empty Then
                If Val(Me.txtFromAmount.Text) > 0 Then
                    strSql += " AND tblVoucherDetail.credit_amount >=" & Val(Me.txtFromAmount.Text) & ""
                End If
            End If
            If Me.txtToAmount.Text <> String.Empty Then
                If Val(Me.txtFromAmount.Text) > 0 Then
                    strSql += " AND tblVoucherDetail.credit_amount <=" & Val(Me.txtToAmount.Text) & ""
                End If
            End If
            If cmbSearchAccount.SelectedRow IsNot Nothing Then
                If Me.cmbSearchAccount.SelectedRow.Cells(0).Value <> 0 Then
                    strSql += " AND tblVoucherDetail.coa_detail_id = " & Me.cmbSearchAccount.Value
                End If
            End If
            If Me.txtSearchComments.Text <> String.Empty Then
                strSql += " AND tblVoucherDetail.Comments LIKE '%" & Me.txtSearchComments.Text & "%'"
            End If
            strSql = strSql + " order by tblVoucher.voucher_id desc"
            FillGridEx(grdVouchers, strSql, True)
            Me.grdVouchers.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdVouchers.RootTable.Columns.Add("Column1")
            Me.grdVouchers.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grdVouchers.RootTable.Columns("Column1").ActAsSelector = True
            Me.grdVouchers.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdVouchers.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            grdVouchers.RootTable.Columns(0).Visible = False  'Voucher ID
            grdVouchers.RootTable.Columns("Post").Visible = False
            grdVouchers.RootTable.Columns("CashRequestId").Visible = False
            Me.grdVouchers.RootTable.Columns("voucher_no").Caption = "Vouchr No"
            Me.grdVouchers.RootTable.Columns("voucher_date").Caption = "Date"
            Me.grdVouchers.RootTable.Columns("Amount").FormatString = "N"
            Me.grdVouchers.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdVouchers.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdVouchers.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdVouchers.RootTable.Columns("Voucher_Date").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CurrentCellChanging(sender As Object, e As CurrentCellChangingEventArgs) Handles grd.CurrentCellChanging
        Try
            If Not e.Row Is Nothing Then
                If e.Row.RowType = RowType.Record Then
                    If Not e.Column Is Nothing Then
                        If e.Column.Key = "CostCenterId" Then
                            CostCentreId = Val(Me.grd.GetRow.Cells("CostCenterId").Value.ToString)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnGetAllRecord_Click(sender As Object, e As EventArgs) Handles btnGetAllRecord.Click
        Try
            GetAllUsersRecord()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbChequeBook_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChequeBook.SelectedIndexChanged
        Try
            'TFS1198:Rai Haider:18-Aug-17: On value Change for chque book dropdown Chque no change according to Selected Cheque book
            'Start Task
            If Me.cmbChequeBook.SelectedValue > 0 Then
                Me.txtChequeNo.Text = getChequeSerialNo(Me.cmbChequeBook.SelectedValue).ToString
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
        'End Task:TFS1198

    End Sub
End Class