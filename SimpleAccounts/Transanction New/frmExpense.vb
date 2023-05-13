''16-Dec-2013 R933   Imran Ali Slow working save/update in transaction forms
''29-Jan-2014      TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''27-Feb-2014 Task:2443   Imran Ali  7-cheque no. on voucher history window
''10-Mar-2014  Task:2484  Imran Ali  Load History On Voucher Take Too Time
''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
''17-Mar-2014 TASK:M27 Editable Bank Account In Payment Voucher
'''''Task No 2619 Mughees Escape Code Updation 
''03-Jul-2014 TASK:M60 CMFA Expense 
''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)
''24-Jul-2014 TASK:M73 Imran Ali Voucher Save Problem
''04-Sep-2014 Task:2826 Imran Ali Checked Status Option on  Voucher
''11-Sep-2014 Task:M101 Imran Ali Add new field remarks 
''04-Jun-2015 Task:2015060001 Ali Ansari Regarding Attachements 
'08-Jun-2015  Task#2015060005 to allow all files to attach
''10-June-2015 Task# 2015060008 to remove non pictures from report with attachements
'22-06-2015 Task#2015060023 Ali Ansari to save proper activity log
'25-06-2015 Task#201506026 Ali Ansari to block exceed payments
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'03-Aug-2015 Task#03082015 Ahmad Sharif: add company drop down on designer , and Fill Drop down with companies for setting company wise invoice number
'04-Aug-2015 Task#04082015 Ahmad Sharif: Left Outer Join CompanyDefTable with tblVoucher and  Add Column CompanyName from CompanyDefTable in query
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.
''18-11-2015 TASK271115 Muhammad Ameen: Display current cheque no in chequebox against specific account selection in Combo and increase chequeno by one in texbox after added to grid.
''12-02-2015 TASKTFS75 Muhammad Ameen: Remove left or inactive employees from drop down lists and reports or add criteria option.
''TASK: TFS1057 done by Ameen Cost Center is not deselected upon new button click.  
''TASK TFS1474 Muhammad Ameen on 15-09-2017. Currency rate can not be edited while base currency is selected.
''TASK TFS2018 Muhammad Ameen done on 02-01-2018. Posted and Checked by user should be displayed in case exists while on edit mode.
''TASK TFS2702 Ayesha Rehman  on 08-03-2018. Expense Approval  hierarchy
''TFS2988 Ayesha Rehman : 09-04-2018 : If document is approved from one stage then it should not change able for previous stage
''TFS2989 Ayesha Rehman : 10-04-2018 : If document is rejected from one stage then it should open for previous stage for approval
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
Imports System.Data
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility
Imports System.IO
Imports SBDal
Imports SBModel
Imports Neodynamic.SDK.Barcode
Imports Janus.Windows.GridEX

Public Class frmExpense
    Implements IGeneral
    ' Change on 23-11-2013  For Multiple Print Vouchers

    Dim lngSelectedVoucherId As Long
    Dim blnEditMode As Boolean = False
    Dim blnFirstTimeInvoked As Boolean = False
    Dim Mode As String = "Normal"
    Dim IsLoadedForm As Boolean = False
    Dim crpt As New ReportDocument
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim getVoucherId As Integer
    Dim setVoucherNo As String = String.Empty
    Dim Email As SBModel.Email
    Dim setEditMode As Boolean = False
    Dim setVoucherType As String = String.Empty
    Dim Total_Amount As Double = 0D
    Dim Privouce_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgCompanyRights As Boolean = False
    Dim Msgfrm As New frmMessages
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    Dim EnabledBrandedSMS As Boolean = False
    Dim currentChequeNo As String = String.Empty

    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim CostCentreId As Integer = 0I
    Dim IsGetAllAllowed As Boolean = False
    Dim IsAdminGroup As Boolean = False
    ''TFS2702 : Ayesha Rehman : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS2702 : Ayesha Rehman :End
    ''TASK TFS3111
    Dim RestrictEntryInParentDetailAC As Boolean = False
    'END TASK TFS3111
    Dim Row_Index As Integer = 0
    Enum grdEnm
        Voucher_Id
        coa_detail_id
        detail_title
        detail_code
        CurrencyId
        CurrencyAmount
        CurrencyRate
        BaseCurrencyId
        BaseCurrencyRate
        Amount
        Reference
        Cheque_No
        Cheque_Date
        Type
        CostCenterId
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
        Dim UserName As String = LoginUserName
        Dim strSql As String = String.Empty
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
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Employee_Id,0) as Employee_Id " _
            '         & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no LEFT OUTER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '         & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'Task:2443 Added Field Cheque_No In This Query
            'Before against task:2484
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Employee_Id,0) as Employee_Id " _
            '         & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no LEFT OUTER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '         & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'End Task:2443
            'Task:2484 Change Join In This Query 
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Employee_Id,0) as Employee_Id " _
            '       & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '       & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'End Task:2484
            'Task:M60 Added Field CMFADocId
            'Before against task:2826
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Employee_Id,0) as Employee_Id, IsNull(tblVoucher.CMFADocId,0) as CMFADocId, CMFA.DocNo + '~' + Convert(Varchar, CMFA.DocDate,102) as CMFADocNo " _
            '                  & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN CMFAMasterTable CMFA On CMFA.DocId = tblVoucher.CMFADocId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '                  & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'Task:2826 Added Field Checked
            'Before against task:M101
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status,IsNull(tblVoucher.Checked,0) as Checked, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],  Isnull(Employee_Id,0) as Employee_Id, IsNull(tblVoucher.CMFADocId,0) as CMFADocId, CMFA.DocNo + '~' + Convert(Varchar, CMFA.DocDate,102) as CMFADocNo " _
            '                  & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN CMFAMasterTable CMFA On CMFA.DocId = tblVoucher.CMFADocId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id " _
            '                  & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'End Task:2826
            'Task:M101 Added Field Remarks
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, tblVoucher.Remarks, isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status,IsNull(tblVoucher.Checked,0) as Checked, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],  Isnull(Employee_Id,0) as Employee_Id, IsNull(tblVoucher.CMFADocId,0) as CMFADocId, CMFA.DocNo + '~' + Convert(Varchar, CMFA.DocDate,102) as CMFADocNo, IsNull([No Of Attachment],0) as  [No Of Attachment] " _
            '                  & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id LEFT OUTER JOIN CMFAMasterTable CMFA On CMFA.DocId = tblVoucher.CMFADocId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = tblVoucher.Voucher_Id " _
            '                  & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            ''End Task:M101
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms

            'Task#04082015 Ahmad Sharif: Left Outer Join CompanyDefTable with tblVoucher and  Add Column CompanyName from CompanyDefTable in query
            strSql = "SELECT DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, tblVoucher.Remarks, isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status,IsNull(tblVoucher.Checked,0) as Checked, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],  Isnull(Employee_Id,0) as Employee_Id, IsNull(tblVoucher.CMFADocId,0) as CMFADocId, CMFA.DocNo + '~' + Convert(Varchar, CMFA.DocDate,102) as CMFADocNo, IsNull([No Of Attachment],0) as  [No Of Attachment],tblvoucher.username as 'User Name',CompanyDefTable.CompanyName, Posted_UserName AS PostedUser, CheckedByUser AS CheckedUser " _
                              & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id  Left Outer join  CompanyDefTable on tblVoucher.location_id=CompanyDefTable.CompanyId  LEFT OUTER JOIN CMFAMasterTable CMFA On CMFA.DocId = tblVoucher.CMFADocId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = tblVoucher.Voucher_Id " _
                              & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'End Task#04082015

            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            If flgCompanyRights = True Then
                strSql += " AND tblVoucher.Location_Id=" & MyCompanyId
            End If
        End If
        If strCondition <> "" Then
            ' strSql &= " AND account_type = N'" & strCondition & "'"
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
            strSql += " AND tblVoucher.UserName LIKE '%" & UserName & "%'"
        End If
        'strSql = strSql + " order by 2 desc"
        strSql = strSql + " order by tblVoucher.voucher_id desc"
        FillGridEx(grdVouchers, strSql, True)

        Me.grdVouchers.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.grdVouchers.RootTable.Columns.Add("Column1")
        Me.grdVouchers.RootTable.Columns("Column1").ActAsSelector = True
        Me.grdVouchers.RootTable.Columns("Column1").UseHeaderSelector = True
        '-----------------------------------------------'
        Me.grdVouchers.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        Me.grdVouchers.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        'If Not Me.grdVouchers.GetRows.Length > 0 Then Exit Sub
        grdVouchers.RootTable.Columns(0).Visible = False  'Voucher ID
        grdVouchers.RootTable.Columns("Post").Visible = False
        grdVouchers.RootTable.Columns("Employee_Id").Visible = False
        grdVouchers.RootTable.Columns("CMFADocId").Visible = False 'Task:M60 Hidden Column
        grdVouchers.RootTable.Columns("PostedUser").Visible = False
        grdVouchers.RootTable.Columns("CheckedUser").Visible = False
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
            VType = "BPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
        Else
            VType = "CPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
        End If
        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            'rafay
            ' Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtVoucherDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            If CompanyPrefix = "V-ERP (UAE)" Then
                ' companyinitials = "UE"
                Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtVoucherDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                companyinitials = "PK"
                Return GetNextDocNo(VType & "-" & companyinitials & "-" & Format(Me.dtVoucherDate.Value, "yy"), 4, "tblVoucher", "voucher_no")
            End If
            'rafay
        Else
            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
            If Not dr Is Nothing Then
                If dr("config_Value") = "Monthly" Then
                    'rafay
                    ' Return GetNextDocNo(VType & "-" & Format(Me.dtVoucherDate.Value, "yy") & Me.dtVoucherDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                    If CompanyPrefix = "V-ERP (UAE)" Then
                        ' companyinitials = "UE"
                        Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtVoucherDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
                    Else
                        companyinitials = "PK"
                        Return GetNextDocNo(VType & "-" & companyinitials & "-" & Format(Me.dtVoucherDate.Value, "yy"), 4, "tblVoucher", "voucher_no")
                    End If
                    'rafay

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
        If ApprovalProcessId = 0 Then
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
            ''Start TFS2702
            If Me.chkChecked.Visible = False Then
                Me.chkChecked.Checked = False
            End If
            ''End 
        Else
            'Me.chkPost.Visible = False
            'Me.chkChecked.Visible = False
        End If
        Dim objCommand As New OleDbCommand
        Dim objTrans As OleDbTransaction
        Dim lngVoucherMasterId As Long
        Dim strChequeNo As String = String.Empty
        Dim Cheque_Date As DateTime = Nothing
        Dim blnCashOptionDetail As Boolean = False
        If Not getConfigValueByType("CashAccountOptionForDetail").ToString = "Error" Then
            blnCashOptionDetail = getConfigValueByType("CashAccountOptionForDetail")
        End If
        Dim enableChequeBook As Boolean = getConfigValueByType("EnableAutoChequeBook")
        If Not Me.blnEditMode Then
            '    If Me.cmbVoucherType.SelectedIndex > 0 Then
            '        Me.txtVoucherNo.Text = GetNextDocNo("BPV", 6, "tblVoucher", "voucher_no")

            '    Else
            '        Me.txtVoucherNo.Text = GetNextDocNo("CPV", 6, "tblVoucher", "voucher_no")
            '    End If

            Me.txtVoucherNo.Text = GetVoucherNo()
            setVoucherNo = Me.txtVoucherNo.Text
        End If
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
                objCommand.CommandText = String.Empty
                'Before against task:M60
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '                           & " cheque_no, cheque_date,post,source,UserName,Employee_Id,Posted_UserName)" _
                '                           & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value & "', '" _
                '                           & IIf(txtChequeNo.Visible, txtChequeNo.Text, "") & "', " & IIf(dtChequeDate.Visible, "N'" & dtChequeDate.Value & "'", "Null") & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & Me.cmbEmployee.SelectedValue & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ")" _
                '                           & " SELECT @@IDENTITY"
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '                          & " post,source,UserName,Employee_Id,Posted_UserName)" _
                '                          & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '                          & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & Me.cmbEmployee.SelectedValue & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ")" _
                '                          & " SELECT @@IDENTITY"
                'Task:M60 Added Field CMFADocId
                'Before against task:2826
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '                          & " post,source,UserName,Employee_Id,Posted_UserName, CMFADocId)" _
                '                          & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '                          & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & Me.cmbEmployee.SelectedValue & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", " & IIf(Me.cmbCMFA.SelectedIndex > 0, Me.cmbCMFA.SelectedValue, 0) & ")" _
                '                          & " SELECT @@IDENTITY"
                'Task:2826 Added Field Checked
                'Before against task:M101
                'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                '                         & " post,source,UserName,Employee_Id,Posted_UserName, CMFADocId,Checked,CheckedByUser)" _
                '                         & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '                         & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & Me.cmbEmployee.SelectedValue & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", " & IIf(Me.cmbCMFA.SelectedIndex > 0, Me.cmbCMFA.SelectedValue, 0) & "," & IIf(Me.chkChecked.Checked = True, 1, 0) & "," & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ")" _
                '                         & " SELECT @@IDENTITY"
                'End Task:2826
                'Task:M101 Added Field Remakrs
                'Task#03082015 Insert location_id in query from cmbCompany.Selected Value
                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                         & " post,source,UserName,Employee_Id,Posted_UserName, CMFADocId,Checked,CheckedByUser,Remarks)" _
                                         & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & ", 1, " & cmbVoucherType.SelectedValue & ", N'" & txtVoucherNo.Text & "', N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                         & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "', N'" & LoginUserName & "', " & Me.cmbEmployee.SelectedValue & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", " & IIf(Me.cmbCMFA.SelectedIndex > 0, Me.cmbCMFA.SelectedValue, 0) & "," & IIf(Me.chkChecked.Checked = True, 1, 0) & "," & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ", N'" & Me.txtMemo.Text.Replace("'", "''") & "')" _
                                         & " SELECT @@IDENTITY"
                'End Task:M101
                lngVoucherMasterId = objCommand.ExecuteScalar

                If arrFile.Count > 0 Then
                    SaveDocument(lngVoucherMasterId, Me.Name, objTrans)
                End If


            Else
                setVoucherNo = Me.txtVoucherNo.Text


                If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                    Call CreateDuplicationVoucher(lngSelectedVoucherId, "Update", objTrans) 'TASKM2710151
                End If
                objCommand.CommandText = String.Empty

                'objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '                        & " , voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value & "', " _
                '                       & " cheque_no = " & IIf(txtChequeNo.Visible, "N'" & txtChequeNo.Text & "'", "''") & " , cheque_date = " & IIf(dtChequeDate.Visible, "N'" & dtChequeDate.Value & "'", "Null") & "  ,post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "' " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & ", Employee_Id=" & Me.cmbEmployee.SelectedValue & ", Posted_Username=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & "    " _
                '                       & " where voucher_id = " & lngSelectedVoucherId & " "
                'Before against task:M60
                'objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '                     & " ,voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '                     & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "' " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & ", Employee_Id=" & Me.cmbEmployee.SelectedValue & ", Posted_Username=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & "    " _
                '                     & " where voucher_id = " & lngSelectedVoucherId & " "
                'Task:M60 Added Field CMFADocId
                'Before against task:2826
                'objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '     & " ,voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '     & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "' " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & ", Employee_Id=" & Me.cmbEmployee.SelectedValue & ", Posted_Username=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CMFADocId=" & IIf(Me.cmbCMFA.SelectedIndex > 0, Me.cmbCMFA.SelectedValue, 0) & "    " _
                '     & " where voucher_id = " & lngSelectedVoucherId & " "
                'Task:2826 Added Field Checked
                'Before against task:M101
                'objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
                '    & " ,voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                '    & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "' " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & ", Employee_Id=" & Me.cmbEmployee.SelectedValue & ", Posted_Username=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CMFADocId=" & IIf(Me.cmbCMFA.SelectedIndex > 0, Me.cmbCMFA.SelectedValue, 0) & ", Checked=" & IIf(Me.chkChecked.Checked = True, 1, 0) & ", CheckedByUser=" & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & " " _
                '    & " where voucher_id = " & lngSelectedVoucherId & ""
                'End Task:2826
                'Task:M101 Added Field Remarks
                'Task#03082015 Update location_id in query from cmbCompany.Selected Value
                objCommand.CommandText = "update tblVoucher set location_id = " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & " , finiancial_year_id = 1 , voucher_type_id = " & cmbVoucherType.SelectedValue & "" _
              & " ,voucher_no =N'" & txtVoucherNo.Text & "' , voucher_date = N'" & dtVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
              & " post = " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,source = N'" & Me.Name & "' " & IIf(Me.chkPost.Checked = False, " , UserName=N'" & LoginUserName & "'", "") & ", Employee_Id=" & Me.cmbEmployee.SelectedValue & ", Posted_Username=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CMFADocId=" & IIf(Me.cmbCMFA.SelectedIndex > 0, Me.cmbCMFA.SelectedValue, 0) & ", Checked=" & IIf(Me.chkChecked.Checked = True, 1, 0) & ", CheckedByUser=" & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ",Remarks=N'" & Me.txtMemo.Text.Replace("'", "''") & "' " _
              & " where voucher_id = " & lngSelectedVoucherId & ""
                'End Task:M101
                objCommand.ExecuteNonQuery()
                lngVoucherMasterId = lngSelectedVoucherId

                If arrFile.Count > 0 Then
                    SaveDocument(lngVoucherMasterId, Me.Name, objTrans)
                End If

                '' Revised Update Cheque Serial No
                If enableChequeBook = True Then
                    objCommand.Transaction = objTrans
                    objCommand.CommandText = String.Empty
                    objCommand.CommandText = "Update ChequeDetailTable Set VoucherDetailId=0,Cheque_Issued=0  From  ChequeDetailTable, tblVoucherDetail WHERE tblVoucherDetail.Voucher_Detail_Id = ChequeDetailTable.VoucherDetailId And tblVoucherDetail.Voucher_Id=" & lngVoucherMasterId & ""
                    objCommand.ExecuteNonQuery()
                End If


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
                '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & r.Cells("CustomerID").Value & ", " & r.Cells("Amount").Value & ",  0, N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "' )"
                objCommand.CommandText = String.Empty
                'Before against task:2728
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, Cheque_No, Cheque_Date) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & r.Cells("coa_detail_id").Value & ", " & r.Cells("Amount").Value & ",  0, N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "', " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ")Select @@Identity"
                'TAsk:2728 Set Comments Cheque No, Cheque Date
                'CurrencyId()
                'CurrencyAmount()
                'CurrencyRate()
                'BaseCurrencyId()
                'BaseCurrencyRate()

                'Dim str As String = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,ChequeDescription,CostCenterId, Cheque_No, Cheque_Date,contra_coa_detail_id, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate) " _
                '                     & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & r.Cells("coa_detail_id").Value & ", " & r.Cells("Amount").Value & ",  0, N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "',N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Me.cmbCashAccount.SelectedValue & ", " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", " & r.Cells("CurrencyRate").Value.ToString & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ")Select @@Identity"

                'End Task:2728
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,ChequeDescription,CostCenterId, Cheque_No, Cheque_Date,contra_coa_detail_id, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount, Currency_Symbol) " _
                                      & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(r.Cells("coa_detail_id").Value.ToString) & ", " & r.Cells("Amount").Value & ",  0, N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "',N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & Me.cmbCashAccount.SelectedValue & ", " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", " & Val(r.Cells("CurrencyRate").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", 0, '" & Me.cmbCurrency.Text & "')Select @@Identity"
                'End Task:2728
                objCommand.Transaction = objTrans
                Dim objId As Object = objCommand.ExecuteScalar()


                If Val(r.Cells("VoucherDetailId").Value.ToString) > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update InvoiceAdjustmentTable Set VoucherDetailId=" & objId & " WHERE VoucherDetailId=" & Val(r.Cells("VoucherDetailId").Value.ToString) & ""
                    objCommand.ExecuteNonQuery()
                End If


                If blnCashOptionDetail = True Then
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount,  comments,ChequeDescription,CostCenterId, Cheque_No, Cheque_Date,contra_coa_detail_id, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_Debit_Amount, Currency_Credit_Amount, Currency_Symbol) " _
                                     & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & r.Cells("Amount").Value & ",  0, N'" & r.Cells("Reference").Value.ToString.Trim.Replace("'", "''") & "',N'" & GetComments(r).Replace("Party Name.", "").Replace("" & r.Cells("detail_title").Value.ToString & "", "").ToString.Replace("'", "''") & "', " & Val(r.Cells("CostCenterId").Value.ToString()) & ", " & IIf(r.Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & r.Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(r.Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(r.Cells("Cheque_Date").Value), Now, r.Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & r.Cells("coa_detail_id").Value & ",  " & Val(r.Cells("CurrencyId").Value.ToString) & ", " & Val(r.Cells("CurrencyAmount").Value.ToString) & ", " & r.Cells("CurrencyRate").Value.ToString & ", " & Val(r.Cells("BaseCurrencyId").Value.ToString) & ", " & Val(r.Cells("BaseCurrencyRate").Value.ToString) & ", 0, " & r.Cells("CurrencyAmount").Value & ", '" & Me.cmbCurrency.Text & "')Select @@Identity"
                    'End Task:2728
                    objCommand.Transaction = objTrans
                    objCommand.ExecuteNonQuery()
                End If
                ''TAsk:2728 Set Comments Cheque No, Cheque Date, Party Name
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount, comments,ChequeDescription, CostCenterId, Cheque_No,Cheque_Date) " _
                '                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(r.Cells("Amount").Value.ToString) & ",  " & 0 & ", N'" & Me.txtMemo.Text.Trim.Replace("'", "''") & "',N'" & GetComments(r).ToString.Replace("'", "''") & "', " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(strChequeNo.Length > 0, "N'" & strChequeNo.Replace("'", "''") & "'", "NULL") & ", " & IIf(strChequeNo.Length = 0, "NULL", "N'" & Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ")"
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


                'Task:2443  e.g Multi Cheque Detail Value Store 

                If r.Cells("Cheque_No").Value.ToString.Length > 0 Then
                    If strMultiChequeDetail.Length > 0 Then
                        strMultiChequeDetail += "|" & r.Cells("Cheque_No").Value.ToString & ":" & IIf(r.Cells("Cheque_Date").Value.ToString <> "", CDate(r.Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    Else
                        strMultiChequeDetail = r.Cells("Cheque_No").Value.ToString & ":" & IIf(r.Cells("Cheque_Date").Value.ToString <> "", CDate(r.Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    End If
                End If
                'end Task:4243




                If Me.chkPost.Checked = True Then
                    If EnabledBrandedSMS = True Then
                        If GetSMSConfig("Expense").Enable = True Then
                            Try
                                Dim strMSGBody As String = String.Empty ' Task:2631 Added object
                                Dim objSMSTemp As New SMSTemplateParameter
                                If Me.cmbVoucherType.Text = "Bank" Then
                                    objSMSTemp = GetSMSTemplate("Expense By Bank")
                                Else
                                    objSMSTemp = GetSMSTemplate("Expense By Cash")
                                End If
                                If objSMSTemp IsNot Nothing Then
                                    Dim objSMSParam As New SMSParameters
                                    objSMSParam.AccountCode = r.Cells("detail_code").Value.ToString
                                    objSMSParam.AccountTitle = r.Cells("detail_title").Value.ToString
                                    objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                                    objSMSParam.DocumentDate = Me.dtVoucherDate.Value
                                    objSMSParam.Remarks = Me.txtMemo.Text
                                    objSMSParam.CellNo = String.Empty ''r.Cells("Mobile").Value.ToString.Replace("-", "").Replace("_", "").Replace(".", "").Replace("+", "").Replace("@", "").Replace(";", "") 'r.Cells("Mobile").Value.ToString
                                    objSMSParam.Amount = Math.Round(Val(r.Cells("Amount").Value.ToString), 0)
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
                                    If GetSMSConfig("Expense").EnabledAdmin = True Then
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
                                End If
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                End If



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
            'Comment against task:2728
            'Chaning Against Request No 801
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount, comments) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbCashAccount.SelectedValue & ", " & Me.txtGridTotal.Text & ",  " & 0 & ", N'" & Me.txtReference.Text.Trim.Replace("'", "''") & "')"
            If blnCashOptionDetail = False Then
                objCommand.CommandText = String.Empty
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, Credit_amount, Debit_amount, comments, CostCenterId, Cheque_No,Cheque_Date, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_debit_amount, Currency_Credit_Amount, Currency_Symbol) " _
                                      & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbCashAccount.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",  " & 0 & ", N'" & Me.txtMemo.Text.Trim.Replace("'", "''") & "', " & Val(Me.cmbCostCenter.SelectedValue) & ", " & IIf(strChequeNo.Length > 0, "N'" & strChequeNo.Replace("'", "''") & "'", "NULL") & ", " & IIf(strChequeNo.Length = 0, "NULL", "N'" & Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ",  " & Val(cmbCurrency.SelectedValue) & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & txtCurrencyRate.Text & ", " & BaseCurrencyId & ", 1 ,0 , " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", '" & Me.cmbCurrency.Text & "')"
                objCommand.Transaction = objTrans
                objCommand.ExecuteNonQuery()
            End If
            'End Task:2728
            objTrans.Commit()
            strChequeNo = String.Empty
            Cheque_Date = Nothing
            'Marked Against Task#2015060023 Ali Ansari to save proper activity log
            'SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
            'Marked Against Task#2015060023 Ali Ansari to save proper activity log
            'Altered Against Task#2015060023 Ali Ansari to save proper activity log
            If BtnSave.Text = "&Save" Then
                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
                ''Start TFS2702
                ''insert Approval Log
                SaveApprovalLog(EnumReferenceType.Expense, lngVoucherMasterId, Me.txtVoucherNo.Text.Trim, Me.dtVoucherDate.Value.Date, "Expense," & cmbCompany.Text & "", Me.Name, cmbVoucherType.SelectedValue)
                ''End TFS2702
            Else
                SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
                ''Start TFS2989
                If ValidateApprovalProcessMapped(Me.txtVoucherNo.Text.Trim, Me.Name) Then
                    If ValidateApprovalProcessIsInProgressAgain(Me.txtVoucherNo.Text.Trim, Me.Name) = False Then
                        SaveApprovalLog(EnumReferenceType.Expense, lngVoucherMasterId, Me.txtVoucherNo.Text.Trim, Me.dtVoucherDate.Value.Date, "Expense," & cmbCompany.Text & "", Me.Name, cmbVoucherType.SelectedValue)
                    End If
                End If
                ''End TFS2989
            End If
            'Altered Against Task#2015060023 Ali Ansari to save proper activity log

            getVoucherId = lngVoucherMasterId

        Catch ex As Exception
            objTrans.Rollback()
        End Try
    End Sub

    Sub DeleteRecord(ByVal strDeleteWhat As String)
        If grdVouchers.CurrentRow Is Nothing Then
            ShowErrorMessage("Please select a row to delete")
        Else
            Dim enableChequeBook As Boolean = getConfigValueByType("EnableAutoChequeBook")
            'lngSelectedVoucherId = grdVouchers.GetRow.Cells(0).Value
            Dim objCommand As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
            objTrans = Con.BeginTransaction
            Try

                objCommand.Connection = Con
                objCommand.Transaction = objTrans


                lngSelectedVoucherId = grdVouchers.GetRow.Cells("Voucher_ID").Value
                If strDeleteWhat = "Detail Only" Then
                    'objCommand.Transaction = objTrans
                    'objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE voucher_id = " & lngSelectedVoucherId
                    'objCommand.ExecuteNonQuery()

                ElseIf strDeleteWhat = "Both" Then

                    If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                        Call CreateDuplicationVoucher(lngSelectedVoucherId, "Delete", objTrans) 'TASKM2710151
                    End If

                    '' Revised Update Cheque Serial No
                    If enableChequeBook = True Then

                        objCommand.CommandText = String.Empty
                        objCommand.CommandText = "Update ChequeDetailTable Set VoucherDetailId=0,Cheque_Issued=0  From  ChequeDetailTable, tblVoucherDetail WHERE tblVoucherDetail.Voucher_Detail_Id = ChequeDetailTable.VoucherDetailId And tblVoucherDetail.Voucher_Id=" & lngSelectedVoucherId & ""
                        objCommand.ExecuteNonQuery()

                    End If


                    'objCommand.Transaction = objTrans
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



            Me.cmbAccounts.Rows(0).Activate()
            'FillComboByEdit()
            lngSelectedVoucherId = Me.grdVouchers.GetRow.Cells(0).Value
            'cmbAccounts.Value = grdVouchers.CurrentRow.Cells(3).Value   'PaidBy
            'txtAmount.Text = grdVouchers.CurrentRow.Cells(8).Value 'Amount
            'Me.txtChequeNo.Text = grdVouchers.CurrentRow.Cells("cheque_no").Value
            'If Not IsDBNull(grdVouchers.CurrentRow.Cells("cheque_date").Value) Then
            '    If grdVouchers.CurrentRow.Cells("cheque_date").Value <> DateTime.MinValue Then
            '        Me.dtChequeDate.Value = grdVouchers.CurrentRow.Cells("cheque_date").Value
            '    End If
            'End If
            'RemoveHandler cmbVoucherType.SelectedIndexChanged, AddressOf Me.cmbVoucherType_SelectedIndexChanged
            cmbVoucherType.Text = grdVouchers.CurrentRow.Cells("Payment Method").Value    'Payment Method
            txtVoucherNo.Text = grdVouchers.CurrentRow.Cells(1).Value 'VoucherNo
            dtVoucherDate.Value = grdVouchers.CurrentRow.Cells(2).Value 'VoucherDate
            'cmbCashAccount.SelectedValue = grdVouchers.CurrentRow.Cells(6).Value.ToString 'Paid To
            MainEditRecord()
            Me.DisplayDetail(grdVouchers.CurrentRow.Cells(0).Value)
            Privouce_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.btnAttachment.Text = "Attachment (" & Me.grdVouchers.GetRow.Cells("No Of Attachment").Value.ToString & ")"
            Me.cmbVoucherType.Enabled = False
            Me.cmbCashAccount.Enabled = False

            If Me.cmbVoucherType.SelectedIndex > 0 Then
                Me.GroupBox1.Visible = True
                Me.GroupBox1.Enabled = True
                Me.grd.RootTable.Columns("Cheque_No").Visible = True
                Me.grd.RootTable.Columns("Cheque_Date").Visible = True
                'Me.cmbCashAccount.Enabled = True ''17-Mar-2014 TASK:M27 Editable Bank Account In Payment Voucher
                'Me.cmbVoucherType.Enabled = False
            Else
                Me.GroupBox1.Visible = False
                Me.GroupBox1.Enabled = False
                Me.grd.RootTable.Columns("Cheque_No").Visible = False
                Me.grd.RootTable.Columns("Cheque_Date").Visible = False
                'Me.cmbCashAccount.Enabled = True
                'Me.cmbVoucherType.Enabled = True
            End If

            Me.chkChecked.Checked = grdVouchers.CurrentRow.Cells("Checked").Value 'Task:2826 Retrive Checked Security

            ''TASK TFS2018
            If Me.chkChecked.Checked = True Then
                Me.chkChecked.Text = "Checked By"
                Me.lblCheckedBy.Text = Me.grdVouchers.CurrentRow.Cells("CheckedUser").Value.ToString
            Else
                Me.chkChecked.Text = "Checked"
                Me.lblCheckedBy.Text = ""
            End If

            ''END TASK TFS2018
            Me.cmbEmployee.SelectedValue = Me.grdVouchers.GetRow.Cells("Employee_Id").Value
            Me.BtnSave.Text = "&Update" ' TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
            GetSecurityRights()
            'Me.GetTotal()

            ''Ayesha Rehman :TFS2702 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("ExpenseApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("ExpenseApproval")
            End If
            If ApprovalProcessId = 0 Then
                Me.btnApprovalHistory.Visible = False
                Me.btnApprovalHistory.Enabled = False
            Else
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
                Me.lblPostedBy.Visible = False
                Me.chkPost.Visible = True
                Me.lblCheckedBy.Visible = False
                Me.chkChecked.Visible = True
            End If
            ''Ayesha Rehman :TFS2702 :End
            Me.chkPost.Checked = grdVouchers.CurrentRow.Cells("Post").Value

            ''TASK TFS2018
            If Me.chkPost.Checked = True Then
                Me.chkPost.Text = "Posted By"
                Me.lblPostedBy.Text = Me.grdVouchers.CurrentRow.Cells("PostedUser").Value.ToString
            Else
                Me.chkPost.Text = "Posted"
                Me.lblPostedBy.Text = ""
            End If

            ''END TASK TFS2018
            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdVouchers.GetRow.Cells("Print Status").Text.ToString

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

            'Task:M60 Get CMFA 
            Dim dt As DataTable = CType(Me.cmbCMFA.DataSource, DataTable)
            Dim drFound() As DataRow
            Dim dr As DataRow
            drFound = dt.Select("DocId=" & Val(Me.grdVouchers.GetRow.Cells("CMFADocId").Value.ToString) & "")
            If drFound.Length <= 0 Then
                dr = dt.NewRow
                dr(0) = Val(Me.grdVouchers.GetRow.Cells("CMFADocId").Value.ToString)
                dr(1) = Me.grdVouchers.GetRow.Cells("CMFADocNo").Value.ToString
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()
            End If
            Me.cmbCMFA.SelectedValue = Val(Me.grdVouchers.GetRow.Cells("CMFADocId").Value.ToString)
            'End Task:M60
            Me.chkEnableDepositAc.Visible = True
            Me.chkEnableDepositAc.Checked = False
            'AddHandler cmbVoucherType.SelectedIndexChanged, AddressOf Me.cmbVoucherType_SelectedIndexChanged

            'Altered Against Task# 2015060001 Ali Ansari
            'Get no of attached files
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdVouchers.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdVouchers.CurrentRow.Cells("No Of Attachment").Value)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            'Altered Against Task# 2015060001 Ali Ansari

            'Task#04082015 Edit Company Name altered by ahmad sharif
            Me.cmbCompany.Text = Me.grdVouchers.CurrentRow.Cells("CompanyName").Value.ToString
            'End Task#04082015
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
            Me.btnCancel.Visible = False
            Me.btnReplace.Visible = False
            Me.btnAdd.Visible = True
            Me.grd.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal VoucherID As Integer)
        Try

            Dim str As String
            str = " SELECT  tblVoucherDetail.voucher_id, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_title, vwCOADetail.detail_code, IsNull(tblVoucherDetail.CurrencyId, 0) As CurrencyId, IsNull(tblVoucherDetail.CurrencyAmount, tblVoucherDetail.debit_amount) As CurrencyAmount, IsNull(tblVoucherDetail.CurrencyRate, 0) As CurrencyRate, IsNull(tblVoucherDetail.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(tblVoucherDetail.BaseCurrencyRate, 0) As BaseCurrencyRate, tblVoucherDetail.debit_amount as Amount, tblVoucherDetail.comments as Reference, Cheque_No, Isnull(Cheque_Date,GetDate()) as Cheque_Date, vwCOADetail.Account_Type as Type, IsNull(tblVoucherDetail.CostCenterId,0) as CostCenterId , IsNull(InvAdj.VoucherDetailId,0) as VoucherDetailId, tblVoucherDetail.voucher_detail_id As DetailId  " _
                  & " FROM  tblVoucherDetail INNER JOIN " _
                  & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id LEFT OUTER JOIN (Select Distinct VoucherDetailId From InvoiceAdjustmentTable) as InvAdj on InvAdj.VoucherDetailId = tblVoucherDetail.Voucher_Detail_Id " _
                  & " Where voucher_id =" & VoucherID & "AND (tblVoucherDetail.debit_amount > 0) Order By tblVoucherDetail.voucher_detail_id ASC "

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
            'For i = 0 To dt.Tables(0).Rows.Count - 1
            '    grd.Rows.Add(dt.Tables(0).Rows(i)(0), dt.Tables(0).Rows(i)(1), dt.Tables(0).Rows(i)(2), dt.Tables(0).Rows(i)(3), dt.Tables(0).Rows(i)(4), dt.Tables(0).Rows(i)(5))
            'Next

            'For Each r As DataRow In dt.Rows
            '    grd.Rows.Add(r.Item("voucher_id"), 0, r.Item("detail_title"), r.Item("coa_detail_id"), r.Item("credit_amount"), 0, 0, 0, 0, 0, 0, r.Item("comments"))
            'Next

            'Me.cmbCashAccount.Enabled = False
            'Me.cmbVoucherType.Enabled = False

            'Me.grd.Rows.Add(Me.txtVoucherNo.Text, Me.dtVoucherDate.Value, Me.cmbAccounts.ActiveRow.Cells(1).Value, Me.cmbAccounts.ActiveRow.Cells(0).Value, Me.txtAmount.Text.Trim, Me.cmbVoucherType.Text.ToString, Me.cmbVoucherType.SelectedValue, Me.cmbCashAccount.Text.ToString, Me.cmbCashAccount.SelectedValue, Me.txtChequeNo.Text.Trim, Me.dtChequeDate.Value, Me.txtReference.Text.Trim, Me.txtMemo.Text.Trim)
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    'Me.cmbCurrency.Enabled = False
                Else
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    Me.txtCurrencyRate.Text = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.Enabled = False
                End If


            End If

            Dim str1 As String = "Select CostCenterId, Name From tblDefCostCenter Union Select 0, '" & strZeroIndexItem & "' Order by 2 ASC"
            Dim dt As New DataTable
            dt = GetDataTable(str1)
            dt.AcceptChanges()
            Me.grd.RootTable.Columns("CostCenterId").ValueList.PopulateValueList(dt.DefaultView, "CostCenterId", "Name")

            ApplyGridSettings()
            ''TASK-407
            Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
            Me.grd.RootTable.Columns("Amount").Caption = "" & GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)) & " Amount"
            dtDisplayDetail.Columns("Amount").Expression = "(CurrencyAmount*CurrencyRate)"
            'dtDisplayDetail.Columns("Tax").Expression = "((Tax_Amount/(CurrencyAmount-Discount))*100)"
            'dtDisplayDetail.Columns("Tax_Currency_Amount").Expression = "(((CurrencyAmount-CurrencyDiscount)*Tax)/100)"
            If Me.cmbCurrency.Text.ToUpper.ToString = GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)).ToUpper.ToString Then
                Me.grd.RootTable.Columns("Amount").Visible = False
            Else
                Me.grd.RootTable.Columns("Amount").Visible = True
            End If

            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)

            '' END TASK-407
            'grd_Click(grd, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ValidateInput() As Boolean
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
        'TASK-407
        If Val(txtCurrencyRate.Text) = 0 Then
            ShowErrorMessage("Currency rate value more than 0 is required")
            Me.txtCurrencyRate.Focus()
            Return False
        End If
        'END TASK-407

        'If cmbVoucherType.Text = "Bank" Then
        '    If txtChequeNo.Text.Trim = "" Then
        '        ShowErrorMessage("Please enter cheque No.")
        '        txtChequeNo.Focus()
        '        Return False
        '    End If
        'End If
        '' Validate Cheque No

        If Me.cmbAccounts.ActiveRow.Cells("Type").Value.ToString.Trim.ToUpper = "LC" Then
            If Me.cmbCostCenter.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select cost center")
                Me.cmbCostCenter.Focus()
                Return False
            End If
        End If
        ''TASK TFS3111
        'If RestrictEntryInParentDetailAC = True Then
        '    If IsParentAccount(Me.cmbAccounts.Value) Then
        '        ShowErrorMessage("Account enrty is restricted. It is a parent account.")
        '        cmbAccounts.Focus()
        '        Return False
        '    End If
        'End If
        ''END TASK TFS3111
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
        'Change by murtaza default currency rate(10/25/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : ValidateInput = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/25/2022)
        Return True
    End Function

    Private Sub frmExpense_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                ''''''Task No 2619 Mughees Escape Code Updation 
                If Me.grd.RowCount > 0 Then
                    'If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                    NewToolStripButton_Click(BtnNew, Nothing)
                    Exit Sub
                End If
                'End Task
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
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmExpense_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try


            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()


            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"
            FillCombos("Company")

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            'TASK:2577 Get Branded SMS Configuration
            If Not getConfigValueByType("EnabledBrandedSMS").ToString = "Error" Then
                EnabledBrandedSMS = getConfigValueByType("EnabledBrandedSMS")
            End If
            'End Task:2577
            'TASK TFS3111
            If Not getConfigValueByType("RestrictEntryInParentDetailAC").ToString = "Error" Then
                RestrictEntryInParentDetailAC = CBool(getConfigValueByType("RestrictEntryInParentDetailAC"))
            End If
            ''END TASK TFS3111

            Me.FillPaymentMethod()
            Dim Str As String = String.Empty
            If blnEditMode = False Then
                Str = "select coa_detail_id,detail_title,detail_code as [Account Code], sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel from vwCoaDetail where account_type = 'Expense' AND Active=1 and detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " "
                ''Start TFS3322 : Ayesha Rehman : 15-05-2018
                ' If LoginGroup = "Administrator" Then
                If GetMappedUserId() > 0 And getGroupAccountsConfigforAccounts(Me.Name) And LoginGroup <> "Administrator" Then
                    Str = "select coa_detail_id,detail_title,detail_code as [Account Code], sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel from vwCoaDetail where Active=1 and detail_title is not null "
                    Str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & "))"
                    Str += " And account_type = 'Expense' " ''TFS4689
                End If
                ''End TFS3322
                Str += " order by 2 "
                FillUltraDropDown(Me.cmbAccounts, Str)
                ' FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code as [Account Code], sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel from vwCoaDetail where account_type = 'Expense' AND Active=1 and detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " order by 2 ") 'where account_type='Vendor'")
            Else
                Str = "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel from vwCoaDetail where account_type = 'Expense' and detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " "
                ''Start TFS3322 : Ayesha Rehman : 15-05-2018
                ' If LoginGroup = "Administrator" Then
                If GetMappedUserId() > 0 And getGroupAccountsConfigforAccounts(Me.Name) And LoginGroup <> "Administrator" Then
                    Str = "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel from vwCoaDetail where detail_title is not null "
                    Str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
                    Str += " And account_type = 'Expense' " ''TFS4689
                End If
                ''End TFS3322
                Str += " order by 2 "
                FillUltraDropDown(Me.cmbAccounts, Str)
                'FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel from vwCoaDetail where account_type = 'Expense' and detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " order by 2 ") 'where account_type='Vendor'")
            End If
            Me.cmbAccounts.Rows(0).Activate()
            Me.cmbAccounts.DisplayLayout.Bands(0).Columns("AccessLevel").Hidden = True

            FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee Where Active = 1") '' TASKTFS75
            'Task:M60 Gell CMFA List
            FillDropDown(Me.cmbCMFA, "Select DocId, DocNo + '~' + Convert(Varchar,DocDate,102) as DocNo from CMFAMasterTable  ORDER BY 2 DESC")
            'End Task:M60


            'If Condition = "GrdCostCenter" Then
            'Dim str As String = "Select CostCenterId, Name From tblDefCostCenter Union Select 0, '" & strZeroIndexItem & "' Order by 2 ASC"
            'Dim dt As New DataTable
            'dt = GetDataTable(str)
            'dt.AcceptChanges()
            'Me.grd.RootTable.Columns("CostCenterId").ValueList.PopulateValueList(dt.DefaultView, "CostCenterId", "Name")
            'End If

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


            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)

            'FillCombos("Currency")

            Me.ClearFields()
            'PopulateGrid()
            FillDropDown(Me.cmbCostCenter, "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " and ISNULL(CostCentre_Id, 0) > 0) " _
                    & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                    & "Else " _
                    & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder")
            'Me.PopulateGrid() 'R933 Commented History Data
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)

            UltraDropDownSearching(cmbAccounts, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub frmCustomerPayment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub
    Private Sub cmbVoucherType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherType.SelectedIndexChanged
        'If IsLoadedForm = True Then Me.cmbAccounts.Focus()
        Dim Str As String = "If  exists(select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1 And Account_Id Is Not Null) " _
                  & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbVoucherType.Text & "' And coa_detail_id in (select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1) " & IIf(flgCompanyRights = True, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " " _
                  & " Else " _
                  & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbVoucherType.Text & "' " & IIf(flgCompanyRights = True, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " "
        'FillDropDown(Me.cmbCashAccount, "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type from vwCoaDetail where account_type=N'" & Me.cmbVoucherType.Text & "' " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " ")
        FillDropDown(Me.cmbCashAccount, Str)

        'Start Task 3913 Saad Afzaal Adjust combo box drop down list width to longest string width 

        SetComboBoxMaxWidth(cmbCashAccount)

        'End Task

        Me.cmbCashAccount.Enabled = True
        If blnEditMode = True Then Exit Sub
        If Me.cmbVoucherType.SelectedIndex > 0 Then
            '  Me.txtVoucherNo.Text = GetNextDocNo("BPV", 6, "tblVoucher", "voucher_no")
            Me.txtVoucherNo.Text = GetVoucherNo()
            Me.GroupBox1.Visible = True
            Me.GroupBox1.Enabled = True
            Me.grd.RootTable.Columns("Cheque_No").Visible = True
            Me.grd.RootTable.Columns("Cheque_Date").Visible = True
        Else
            'Me.txtVoucherNo.Text = GetNextDocNo("CPV", 6, "tblVoucher", "voucher_no")
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
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
        Me.Cursor = Cursors.WaitCursor

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
                If Val(Me.txtCashBalances.Text) < Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) Then
                    ShowErrorMessage("Amount exceeds available balance")
                    Me.cmbCashAccount.Focus()
                    Exit Sub
                End If
            End If
            'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace
            'Change by murtaza default currency rate(10/25/2022) 
            If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
                msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
                txtCurrencyRate.Focus()
                Exit Sub
            End If
            'Change by murtaza default currency rate(10/25/2022)
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
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If r.Cells("Cheque_No").Value.ToString.Length > 0 Then
                    'If ValidateduplicateChequeInGrid(r.Cells("Cheque_No").Value.ToString) = False Then
                    '    Exit Sub
                    'End If
                    Dim rowStyle As Janus.Windows.GridEX.GridEXFormatStyle = New Janus.Windows.GridEX.GridEXFormatStyle()
                    Dim strCheque As String = r.Cells("Cheque_No").Value.ToString
                    Dim vendorID As Integer = r.Cells("coa_detail_id").Value
                    If strCheque.Length > 0 Then
                        If IsValidateChequePayment(strCheque, lngSelectedVoucherId) = False Then
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

            'If Not Me.grd.Rows.Count = 0 Then
            If Not Me.grd.RowCount = 0 Then
                Me.grd.UpdateData()

                If blnEditMode Then
                    ''21-02-2017
                    For Each drCC As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                        If IsVoucherCostCentreReshuffled(Val(drCC.Cells("DetailId").Value.ToString)) Then
                            ShowErrorMessage("Record can not be deleted because cost centre has been shifted.")
                            Exit Sub
                        End If
                    Next
                    ''Start TFS2988
                    If ValidateApprovalProcessMapped(Me.txtVoucherNo.Text.Trim, Me.Name) Then
                        If ValidateApprovalProcessInProgress(Me.txtVoucherNo.Text.Trim, Me.Name) Then
                            msg_Error("Document is in Approval Process ") : Exit Sub
                        End If
                    End If
                    ''End TFS2988
                    '' End 21-02-2017
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then
                        Exit Sub
                    Else
                        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                        Me.lblProgress.Text = "Processing Please Wait ..."
                        Me.lblProgress.Visible = True
                        Application.DoEvents()
                        setEditMode = True
                        Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                        'DeleteRecord("Detail Only")
                        SaveRecord()
                        'MessageBox.Show(str_informUpdate, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
                        'msg_Information(str_informUpdate)

                        Dim Printing As Boolean
                        Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        If Printing = True Then
                            'If msg_Confirm("Do you want to print") = True Then
                            DualPrinting()
                            'End If
                        End If
                        'If msg_Confirm(str_ConfirmPrintVoucher) = True Then
                        '    If Me.grdVouchers.RowCount = 0 Then Exit Sub
                        '    AddRptParam("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
                        '    ShowReport("rptVoucher", , , , True)
                        'End If
                        'Me.Cursor = Cursors.Default
                        ''Me.lblProgress.Visible = False
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        ''ExportFile(getVoucherId)
                        ''EmailSave()

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
                    'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    setEditMode = False
                    Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    SaveRecord()
                    ClearFields()
                    'MessageBox.Show(str_informSave, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
                    'msg_Information(str_informSave)

                    Dim Printing As Boolean
                    Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                    If Printing = True Then
                        'If msg_Confirm("Do you want to print") = True Then
                        DualPrinting()
                        'End If
                    End If
                    'If msg_Confirm(str_ConfirmPrintVoucher) = True Then
                    '    If Me.grdVouchers.RowCount = 0 Then Exit Sub
                    '    AddRptParam("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
                    '    ShowReport("rptVoucher", , , , True)
                    'End If


                    'Me.Cursor = Cursors.Default
                    If BackgroundWorker1.IsBusy Then Exit Sub
                    BackgroundWorker1.RunWorkerAsync()
                    'Do While BackgroundWorker1.IsBusy
                    '    Application.DoEvents()
                    'Loop
                    '' ExportFile(getVoucherId)
                    ''EmailSave()
                    If BackgroundWorker2.IsBusy Then Exit Sub
                    BackgroundWorker2.RunWorkerAsync()
                    'Do While BackgroundWorker2.IsBusy
                    '    Application.DoEvents()
                    'Loop
                    'End If

                End If

                setEditMode = False
                Total_Amount = 0D
            Else
                msg_Error("Enter atleast one record in the grid")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        If grdVouchers.CurrentRow Is Nothing Then
            ShowErrorMessage("Please select a row to delete")
        Else
            blnEditMode = True
            lngSelectedVoucherId = CLng(grdVouchers.CurrentRow.Cells(0).Value)
            EditRecord()
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Me.Cursor = Cursors.WaitCursor
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
            ''21-02-2017
            For Each drCC As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                If IsVoucherCostCentreReshuffled(Val(drCC.Cells("DetailId").Value.ToString)) Then
                    ShowErrorMessage("Record can not be deleted because cost centre has been shifted.")
                    Exit Sub
                End If
            Next
            '' End 21-02-2017

            ''Start TFS2988 : Ayesha Rehman : 09-04-2018
            If blnEditMode = True Then
                If ValidateApprovalProcessMapped(Me.txtVoucherNo.Text.Trim, Me.Name) Then
                    If ValidateApprovalProcessInProgress(Me.txtVoucherNo.Text.Trim, Me.Name) Then
                        msg_Error("Document is in Approval Process ") : Exit Sub
                    End If
                End If
            End If
            ''End TFS2988
            If Not msg_Confirm(str_ConfirmDelete) = True Then
                Exit Sub
            Else
                'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()

                If Me.grdVouchers.RowCount = 0 Then Exit Sub
                If CheckInvAdjustmentDependedVoucher(Val(Me.grdVouchers.GetRow.Cells("Voucher_Id").Value.ToString)) = True Then
                    ShowErrorMessage("Record can't be deleted, voucher adjusted against invoice.")
                    Exit Sub
                End If


                DeleteRecord("Both")
                'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
                Me.grdVouchers.CurrentRow.Delete()
                'MessageBox.Show(str_informDelete, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
                'msg_Information(str_informDelete)
                Me.ClearFields()
                'PopulateGrid() 'R933 Commented History Data
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub grdVouchers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVouchers.DoubleClick
        blnEditMode = True
        lngSelectedVoucherId = CLng(grdVouchers.CurrentRow.Cells(0).Value)
        EditRecord()
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            ClearFields()
            Me.cmbAccounts.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbAccounts_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAccounts.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'Expense'"
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbAccounts.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbAccounts_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccounts.Leave
        Try
            If Me.cmbAccounts.IsItemInList = False Then Exit Sub
            If cmbAccounts.ActiveRow.Cells(0).Value > 0 AndAlso (LoginGroup = "Administrator" Or (Me.cmbAccounts.ActiveRow.Cells("AccessLevel").Value.ToString = "Everyone")) Then
                Try
                    Dim objCommand As New OleDbCommand("SELECT SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount) FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id " _
                                                                           & " WHERE tblVoucherDetail.coa_detail_id = " & cmbAccounts.ActiveRow.Cells(0).Value _
                                                                            & " AND tblVoucher.Post=1", Con)
                    If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
                    objCommand.Connection = Con

                    txtCustomerBalance.Text = Math.Round(Val(objCommand.ExecuteScalar), 0)
                    If Val(txtCustomerBalance.Text) < 0 Then
                        Me.txtCustomerBalance.Text = "(" & Replace(Me.txtCustomerBalance.Text, "-", "") & ")"
                    End If
                Catch ex As Exception
                    txtCustomerBalance.Text = 0
                End Try
            Else
                Me.txtCustomerBalance.Text = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick
        Me.Cursor = Cursors.WaitCursor
        Try
            'If Me.grdVouchers.RowCount = 0 Then Exit Sub
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
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                Me.chkPost.Visible = True
                If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = True
                Me.chkChecked.Visible = True
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
                    Me.btnSearchPrint.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Me.btnGetAllRecord.Enabled = False
                    IsGetAllAllowed = False
                    IsAdminGroup = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmExpense)
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
                        Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
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
                Me.btnSearchPrint.Enabled = False
                Me.BtnPrint.Enabled = False
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                Me.chkPost.Visible = False
                If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = False
                Me.chkChecked.Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                'Me.btnGetAllRecord.Enabled = False
                IsGetAllAllowed = False
                IsAdminGroup = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        ' Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        Me.btnSearchPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                        Me.chkPost.Visible = True
                        'Task:2826 Apply Checked Security 
                    ElseIf RightsDt.FormControlName = "Checked" Then
                        If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = True
                        Me.chkChecked.Visible = True
                    ElseIf RightsDt.FormControlName = "GetAll" Then
                        IsGetAllAllowed = True
                        'Me.btnGetAllRecord.Enabled = True
                        'End Task:2826
                    End If
                Next
                If getConfigValueByType("UpdatePostedVoucher").ToString = "False" Then
                    If grdVouchers.RowCount > 0 Then
                        If Me.BtnSave.Text <> "&Save" Then
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
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try


            If Me.ValidateInput Then
                'Me.grd.Rows.Add(Me.txtVoucherNo.Text, 
                'Me.dtVoucherDate.Value, 
                'Me.cmbAccounts.ActiveRow.Cells(1).Value,
                'Me.cmbAccounts.ActiveRow.Cells(0).Value,
                'Me.txtAmount.Text.Trim,
                'Me.cmbVoucherType.Text.ToString, 
                'Me.cmbVoucherType.SelectedValue,
                'Me.cmbCashAccount.Text.ToString, 
                'Me.cmbCashAccount.SelectedValue, 
                'Me.txtChequeNo.Text.Trim, 
                'Me.dtChequeDate.Value,
                'Me.txtReference.Text.Trim,
                'Me.txtMemo.Text.Trim)
                ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
                If CheckNumericValue(Me.txtAmount.Text, Me.txtAmount) = False Then
                    Throw New Exception("Amount is not valid.")
                End If
                'End Task:2491
                'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace
                If IsAllowPayment() = False Then
                    If Val(Me.txtCashBalances.Text) < (Val(txtAmount.Text) + Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
                        ShowErrorMessage("Expense amount exceeds available balance")
                        Me.cmbCashAccount.Focus()
                        Exit Sub
                    End If
                End If
                'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace


                Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
                dtGrd.AcceptChanges()
                Dim drGrd As DataRow
                drGrd = dtGrd.NewRow

                drGrd.Item(0) = 0
                drGrd.Item(grdEnm.coa_detail_id) = Me.cmbAccounts.ActiveRow.Cells(0).Value
                drGrd.Item(grdEnm.detail_title) = Me.cmbAccounts.ActiveRow.Cells(1).Text
                drGrd.Item(grdEnm.detail_code) = Me.cmbAccounts.ActiveRow.Cells("Account Code").Text
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
                drGrd.Item(grdEnm.Reference) = Me.txtReference.Text.Trim
                If Me.txtChequeNo.Text.Length > 0 Then
                    currentChequeNo = IIf(Me.GroupBox1.Visible = True, Me.txtChequeNo.Text, DBNull.Value) 'TASK271115
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
                drGrd.Item(grdEnm.Type) = Me.cmbAccounts.ActiveRow.Cells("Type").Value.ToString
                drGrd.Item(grdEnm.CostCenterId) = Me.cmbCostCenter.SelectedValue
                dtGrd.Rows.InsertAt(drGrd, 0)
                dtGrd.AcceptChanges()

                Me.ClearFields("Detail")
                If Me.cmbCashAccount.SelectedIndex > 0 Then
                    Me.cmbVoucherType.Enabled = False
                    Me.cmbCashAccount.Enabled = False
                End If
                'Me.GroupBox1.Enabled = False
                'Me.GetTotal()
                Me.cmbAccounts.Focus()
                Me.cmbCurrency.Enabled = False
                'Me.grd.RootTable.Columns("Amount").Caption = "" & GetBasicCurrencyName(Val(ConfigCurrencyVal)) & " Amount"

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub ClearFields(Optional ByVal condition As String = "")
        'dtVoucherDate.Value = Date.Today  'VoucherDate
        'Me.cmbAccounts.Rows(0).Activate()
        'cmbAccounts.Focus()
        'Me.dtVoucherDate.Focus()
        'Me.dtVoucherDate.Enabled = True
        'txtAmount.Text = "" 'Amount
        If Not getConfigValueByType("RestrictEntryInParentDetailAC").ToString = "Error" Then
            RestrictEntryInParentDetailAC = CBool(getConfigValueByType("RestrictEntryInParentDetailAC"))
        End If
        GetSecurityRights()
        If condition.Length > 0 Then
            'txtVoucherNo.Text = "" 'VoucherNo
            'cmbVoucherType.SelectedIndex = 0    'Payment Method
            'Me.cmbVoucherType.Enabled = True
            'cmbVoucherType_SelectedIndexChanged(Me, New EventArgs)
            GroupBox1.Enabled = True
            'Me.txtChequeNo.Text = String.Empty
            'Me.dtChequeDate.Value = Now
            Me.cmbVoucherType.Enabled = True
            Me.cmbCostCenter.Enabled = True
            'cmbVoucherType_SelectedIndexChanged(Me, New EventArgs)
            'GroupBox1.Enabled = True
            Me.txtAmount.Text = ""
            Me.cmbAccounts.Rows(0).Activate()
            Me.cmbAccounts.Focus()
            ' Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
            'Ayesha Rehman : TFS2702 : Enable Approval History button only in Eidt Mode
            If blnEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Ayesha Rehman : TFS2702 : End
            ''Ayesha Rehman :TFS2702 : if Approval Process Mapped then handle Visibilty of checks of post and check
            If Not getConfigValueByType("ExpenseApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("ExpenseApproval")
            End If
            If ApprovalProcessId = 0 Then
                Me.chkPost.Visible = True
                Me.chkPost.Enabled = True
                Me.chkChecked.Visible = True
                Me.chkChecked.Enabled = True
                Me.lblPostedBy.Visible = True
                Me.lblCheckedBy.Visible = True
            Else
                Me.chkPost.Visible = True
                Me.chkPost.Enabled = False
                Me.chkPost.Checked = False
                Me.chkChecked.Visible = True
                Me.chkChecked.Enabled = False
                Me.chkChecked.Checked = False
                Me.lblPostedBy.Visible = False
                Me.lblCheckedBy.Visible = False
            End If
            ''Ayesha Rehman :TFS2702 :End
        End If
        'cmbCashAccount.SelectedIndex = 0 'Paid To
        Me.txtReference.Text = String.Empty
        If condition.Length = 0 Then
            Me.dtVoucherDate.Focus()
            Me.dtVoucherDate.Enabled = True
            blnEditMode = False
            lngSelectedVoucherId = 0
            Me.BtnSave.Text = "&Save" 'TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
            dtVoucherDate.Value = Date.Now 'Voucher Date
            Me.cmbVoucherType.Enabled = True
            Me.cmbVoucherType.SelectedIndex = 0
            cmbVoucherType_SelectedIndexChanged(Me, New EventArgs)
            Me.txtChequeNo.Text = String.Empty
            'Me.grd.Rows.Clear()
            Me.txtChequeNo.Enabled = True
            Me.dtChequeDate.Enabled = True
            Me.cmbCashAccount.Enabled = True
            FillComboByEdit()
            Me.DisplayDetail(-1)
            Me.dtChequeDate.Value = Date.Now
            Me.cmbEmployee.SelectedIndex = 0
            Me.txtMemo.Text = String.Empty
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
            Me.BtnDelete.Visible = False 'R933 Delete Button Hide
            Me.BtnPrint.Visible = False 'R933 Print Button Hide
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnEdit.Visible = False
            PopulateGrid()
            Me.lblPrintStatus.Text = String.Empty
            'Clear Attached file records
            arrFile = New List(Of String)
            Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
            'Array.Clear(arrFile, 0, arrFile.Length)
            'Me.btnAttachment.Text = "Attachment"
            Me.chkEnableDepositAc.Visible = False
            Me.chkEnableDepositAc.Checked = False
            btnUpdateTimes.Text = "No of update times"
            Me.btnUpdateTimes.Visible = False

            FillCombos("Currency")

            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            Me.cmbCurrency.Enabled = True
            Me.txtCurrencyRate.Enabled = False
            ''TASK: TFS1057 done by Ameen Cost Center is not deselected upon new button click.  
            If Not Me.cmbCostCenter.SelectedIndex = -1 Then
                Me.cmbCostCenter.SelectedIndex = 0
            End If
            ''END TFS1057 
            'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "C Amount"
            'Me.grd.RootTable.Columns("Amount").Caption = "Amount"
            FillCombos("Company")
            ' Me.cmbCurrency.SelectedValue = BaseCurrencyId

            'Ayesha Rehman : TFS2702 : Enable Approval History button only in Eidt Mode
            If blnEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Ayesha Rehman : TFS2702 : End
            ''Ayesha Rehman :TFS2702 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("ExpenseApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("ExpenseApproval")
            End If
            If ApprovalProcessId = 0 Then
                Me.chkPost.Visible = True
                Me.chkPost.Enabled = True
                Me.chkChecked.Visible = True
                Me.chkChecked.Enabled = True
                Me.lblPostedBy.Visible = True
                Me.lblCheckedBy.Visible = True
            Else
                Me.chkPost.Visible = False
                Me.chkPost.Enabled = False
                Me.chkPost.Checked = False
                Me.chkChecked.Visible = False
                Me.chkChecked.Enabled = False
                Me.chkChecked.Checked = False
                Me.lblPostedBy.Visible = False
                Me.lblCheckedBy.Visible = False
            End If
            ''Ayesha Rehman :TFS2702 :End
            ''TASK TFS2018
            Me.chkChecked.Text = "Checked"
            Me.chkPost.Text = "Posted"
            Me.lblPostedBy.Text = ""
            Me.lblCheckedBy.Text = ""
            ''END TASK TFS2018
        End If
        Me.btnCancel.Visible = False
        Me.btnReplace.Visible = False
        Me.btnAdd.Visible = True
        Me.grd.Enabled = True

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
    '    'If grd.Rows.Count = 0 Then Exit Sub
    '    If grd.RowCount = 0 Then Exit Sub
    '    If Me.blnEditMode = False Then Exit Sub

    '    Me.cmbAccounts.Value = grd.CurrentRow.Cells(3).Value.ToString
    '    Me.txtAmount.Text = grd.CurrentRow.Cells(4).Value.ToString

    '    ''get Deposit in 

    '    Dim str As String

    '    str = " SELECT DISTINCT vwCOADetail.detail_title, tblVoucherDetail.coa_detail_id" _
    '          & " FROM         tblVoucherDetail INNER JOIN" _
    '          & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
    '          & " Where voucher_id =" & grd.GetRows(0).Cells(0).Value.ToString & " AND (tblVoucherDetail.credit_amount > 0)"

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

    Private Sub ExpensesDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpensesDetailToolStripMenuItem.Click
        frmMain.LoadControl("rptExpense")
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim CustId As Integer = 0
        CustId = Me.cmbAccounts.Value

        ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working 
        'FrmAddCustomers.FormType = "Expense"
        'FrmAddCustomers.ShowDialog()
        frmMain.LoadControl("AddExpense")

        Dim str As String = String.Empty
        str = "select coa_detail_id,detail_title,detail_code as [Account_Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where account_type = 'Expense' AND Active=1 and detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " "
        ''Start TFS3322 : Ayesha Rehman : 15-05-2018
        'If LoginGroup = "Administrator" Then
        If GetMappedUserId() > 0 And getGroupAccountsConfigforAccounts(Me.Name) And LoginGroup <> "Administrator" Then
            str = "select coa_detail_id,detail_title,detail_code as [Account_Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where Active=1 and detail_title is not null "
            str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                   & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                   & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                   & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
            str += " And account_type = 'Expense' " ''TFS4689
        End If
        ''End TFS3322
        str += " order by 2 "
        FillUltraDropDown(Me.cmbAccounts, str)
        ' FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code as [Account_Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where account_type = 'Expense' AND Active=1 and detail_title is not null " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " order by 2")
        Me.cmbAccounts.Value = CustId
        Me.cmbAccounts.DisplayLayout.Bands(0).Columns("AccessLevel").Hidden = True

    End Sub
    'Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Me.PopulateGrid("All")
    '    DisplayDetail(-1)
    '    Me.Cursor = Cursors.Default
    'End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoadAll.Visible = False
    '        Me.ToolStripButton1.Visible = False
    '        ToolStripSeparator2.Visible = False
    '    Else
    '        Me.BtnLoadAll.Visible = True
    '        Me.ToolStripButton1.Visible = True
    '        ToolStripSeparator2.Visible = True
    '    End If
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            FillComboByEdit() ''TFS3322 : Ayesha Rehman : 23-05-2018
            Dim id As Integer = 0
            id = Me.cmbAccounts.SelectedRow.Cells(0).Value
            Me.cmbAccounts.Value = id
            id = Me.cmbEmployee.SelectedValue
            FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee Where Active = 1")
            Me.cmbEmployee.SelectedValue = id
            id = Me.cmbCostCenter.SelectedValue
            FillDropDown(Me.cmbCostCenter, "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                    & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                    & "Else " _
                    & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder")
            Me.cmbCostCenter.SelectedValue = id
            'Task:M60 Gell CMFA List
            id = Me.cmbCMFA.SelectedIndex
            FillDropDown(Me.cmbCMFA, "Select DocId, DocNo + '~' + Convert(Varchar,DocDate,102) as DocNo from CMFAMasterTable  ORDER BY 2 DESC")
            Me.cmbCMFA.SelectedIndex = id
            'End Task:M60

            Dim str As String = "Select CostCenterId, Name From tblDefCostCenter Union Select 0, '" & strZeroIndexItem & "' Order by 2 ASC"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grd.RootTable.Columns("CostCenterId").ValueList.PopulateValueList(dt.DefaultView, "CostCenterId", "Name")


            FillCombos("Company")
            'Altered Against Task#2015060001 Ali Ansri
            'Clear Attached file records
            ' arrFile = New List(Of String)
            ' Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub cmbCashAccount_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbCashAccount.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'" & Me.cmbVoucherType.Text & "'"
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbCashAccount.SelectedValue = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub cmbCashAccount_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCashAccount.SelectedValueChanged
        Try
            If Me.cmbCashAccount.SelectedIndex > 0 Then
                Try
                    Dim objCommand As New OleDbCommand("SELECT SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount) FROM tblVoucherDetail  INNER JOIN tblVoucher On tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id" _
                                                                     & " WHERE tblVoucherDetail.coa_detail_id = " & Me.cmbCashAccount.SelectedValue _
                                                                           & " AND tblVoucher.Post=1", Con)
                    If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
                    objCommand.Connection = Con

                    Me.txtCashBalances.Text = CInt(Val(objCommand.ExecuteScalar))
                    If Val(txtCashBalances.Text) < 0 Then
                        Me.txtCashBalances.Text = "(" & Replace(Me.txtCashBalances.Text, "-", "") & ")"
                    End If
                Catch ex As Exception
                    txtCashBalances.Text = 0
                End Try
            Else
                Me.txtCashBalances.Text = 0
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
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillComboByEdit()
        Try
            Dim str As String = String.Empty
            If blnEditMode = True Then
                str = "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where account_type = 'Expense' and detail_title is not null " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " "
                ''Start TFS3322 : Ayesha Rehman : 15-05-2018
                ' If LoginGroup = "Administrator" Then
                If GetMappedUserId() > 0 And getGroupAccountsConfigforAccounts(Me.Name) And LoginGroup <> "Administrator" Then
                    str = "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where  detail_title is not null "
                    str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
                    str += " And account_type = 'Expense' " ''TFS4689
                End If
                ''End TFS3322
                str += " order by 2 "
                FillUltraDropDown(Me.cmbAccounts, str)
                ' FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where account_type = 'Expense' and detail_title is not null " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " order by 2 ") 'where account_type='Vendor'")
            Else
                str = "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where account_type = 'Expense' and detail_title is not null And Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " "
                ''Start TFS3322 : Ayesha Rehman : 15-05-2018
                ' If LoginGroup = "Administrator" Then
                If GetMappedUserId() > 0 And getGroupAccountsConfigforAccounts(Me.Name) And LoginGroup <> "Administrator" Then
                    str = "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where detail_title is not null And Active=1  "
                    str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
                    str += " And account_type = 'Expense' " ''TFS4689
                End If
                ''End TFS3322
                str += " order by 2 "
                FillUltraDropDown(Me.cmbAccounts, str)
                ' FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c], Account_Type as Type, Isnull(vwCOADetail.AccessLevel,'Everyone') as AccessLevel  from vwCoaDetail where account_type = 'Expense' and detail_title is not null And Active=1  " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " " & IIf(RestrictEntryInParentDetailAC = True, " AND dbo.vwCOADetail.coa_detail_id NOT IN (SELECT DISTINCT Parent_Id From dbo.vwCOADetail WHERE ISNULL(Parent_Id, 0) > 0) ", "") & " order by 2 ") 'where account_type='Vendor'")
            End If
            Me.cmbAccounts.DisplayLayout.Bands(0).Columns("AccessLevel").Hidden = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                If Not Val(Me.grd.GetRow.Cells("VoucherDetailId").Value.ToString) > 0 Then
                    Me.grd.GetRow.Delete()
                    grd.UpdateData()
                    If Me.grd.RowCount = 0 Then
                        Me.cmbCurrency.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> grdEnm.Reference AndAlso col.Index <> grdEnm.Cheque_No AndAlso col.Index <> grdEnm.Cheque_Date AndAlso col.Index <> grdEnm.CostCenterId AndAlso col.Index <> grdEnm.CurrencyAmount Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grd.RootTable.Columns(grdEnm.Amount).FormatString = "N"
            Me.grd.RootTable.Columns(grdEnm.Amount).TotalFormatString = "N"
            Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).FormatString = "N"
            Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).TotalFormatString = "N"
            Me.grd.RootTable.Columns(grdEnm.Amount).FormatString = "N" & DecimalPointInValue  'Task:2647 Set Rounding Format
            Me.grd.RootTable.Columns(grdEnm.Amount).TotalFormatString = "N" & TotalAmountRounding

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
            If Condition = "Company" Then
                Dim strSQL As String = String.Empty
                strSQL = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
                FillDropDown(Me.cmbCompany, strSQL, False)
            ElseIf Condition = "Currency" Then
                Dim Str As String = String.Empty
                Str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbCurrency, Str, False)
                Me.cmbCurrency.SelectedValue = BaseCurrencyId
            ElseIf Condition = "Cheque" Then
                Dim Str As String = String.Empty
                Str = "SELECT    distinct ChequeMasterTable.ChequeSerialId, ChequeMasterTable.Cheque_No_From +' - '+ ChequeMasterTable.Cheque_No_To As ChequeBookName " _
                        & " FROM         ChequeMasterTable INNER JOIN ChequeDetailTable ON ChequeMasterTable.ChequeSerialId = ChequeDetailTable.ChequeSerialId " _
                        & " where ChequeDetailTable.Cheque_Issued=0 and ChequeMasterTable.BankAcId=" & Me.cmbCashAccount.SelectedValue
                FillDropDown(Me.cmbChequeBook, Str, False)
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

    '        ''get Deposit in 

    '        Dim str As String

    '        str = " SELECT DISTINCT vwCOADetail.detail_title, tblVoucherDetail.coa_detail_id, tblVoucherDetail.Comments, ISNULL(tblVoucherDetail.CostCenterId,0) as CostCenterId " _
    '              & " FROM         tblVoucherDetail INNER JOIN" _
    '              & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id" _
    '              & " Where voucher_id =" & Me.grdVouchers.GetRow.Cells("Voucher_Id").Value & " AND (tblVoucherDetail.credit_amount > 0)"

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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Expense"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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

                '        blnEditMode = True
                '        FillUltraDropDown(Me.cmbAccounts, "select coa_detail_id,detail_title,detail_code as [Account Code],sub_sub_title AS [Sub Sub A/c] from vwCoaDetail where account_type = 'Expense' and detail_title is not null order by 2 ") 'where account_type='Vendor'")
                '        lngSelectedVoucherId = dt.Rows(0).Item("Voucher_Id").ToString
                '        'Me.txtChequeNo.Text = dt.Rows(0).Item("cheque_no").ToString
                '        'If Not IsDBNull(dt.Rows(0).Item("cheque_date")) Then
                '        '    If dt.Rows(0).Item("cheque_date") <> DateTime.MinValue Then
                '        '        Me.dtChequeDate.Value = dt.Rows(0).Item("cheque_date")
                '        '    End If
                '        'End If
                '        cmbVoucherType.SelectedValue = dt.Rows(0).Item("Voucher_Type_Id").ToString     'Payment Method
                '        txtVoucherNo.Text = dt.Rows(0).Item("Voucher_No").ToString  'VoucherNo
                '        dtVoucherDate.Value = dt.Rows(0).Item("Voucher_Date").ToString  'VoucherDate
                '        'cmbCashAccount.SelectedValue = grdVouchers.CurrentRow.Cells(6).Value.ToString 'Paid To
                '        'Me.cmbBank.Text = dt.Rows(0).Item("BankDesc").Text.ToString
                '        RemoveHandler grd.Click, AddressOf grd_Click
                '        Me.DisplayDetail(lngSelectedVoucherId)
                '        Me.cmbVoucherType.Enabled = False
                '        If Me.cmbVoucherType.SelectedIndex > 0 Then
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
                '        'Me.chkAll.Checked = True
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
                'IsDrillDown = False
                IsDrillDown = True
                If Me.grdVouchers.RowCount <= 50 Then
                    Me.btnSearchLoadAll_Click(Nothing, Nothing)
                End If
                Dim flag As Boolean = False
                flag = Me.grdVouchers.Find(Me.grdVouchers.RootTable.Columns("Voucher_No"), Janus.Windows.GridEX.ConditionOperator.Equal, Voucher_No, 0, 1)
                'If flag = True Then
                Me.grdVouchers_DoubleClick(Nothing, Nothing)
                'Else
                'Exit Function
                'End If
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        Finally
            frmModProperty.Tag = String.Empty
        End Try
    End Function
    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            OpenToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPrint.ButtonClick
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
            ShowErrorMessage(ex.Message)
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
                                                          " WHERE (dbo.vwCOADetail.account_type = 'Expense') and detail_title is not null"
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
                ''R933 Some button show
                Me.BtnEdit.Visible = True
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
            Else
                Me.BtnEdit.Visible = False
                If blnEditMode = False Then Me.BtnDelete.Visible = False
                If blnEditMode = False Then Me.BtnPrint.Visible = False
            End If
            'R933
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsEmailAttachment() = True Then
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
                    crpt.SetParameterValue("@VoucherId", Me.grdVouchers.CurrentRow.Cells(0).Value)
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
            ExportFile(getVoucherId)
        Catch ex As Exception

        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim toEmail As String = String.Empty
        Dim flg As Boolean = False
        If IsEmailAlert Then
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
                    & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Privouce_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                    Email.Status = "Pending"
                    Call New MailSentDAL().Add(Email)
                End If
            End If
        End If
        SourceFile = String.Empty
        Return EmailSave
    End Function
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
                    & " " & IIf(setEditMode = False, "of amount " & Me.grdVouchers.GetRow.Cells("Amount").Value & " is made", "of amount " & Me.grdVouchers.GetRow.Cells("Amount").Value & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
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
    'Private Sub txtReference_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReference.TextChanged
    '    Try
    '        If Me.txtMemo.Text.Trim.Length = 0 Then
    '            Me.txtMemo.Text = Me.txtReference.Text
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub PrintPaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPaymentToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdVouchers.RowCount = 0 Then Exit Sub
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
            'Chaning Against Request No 798
            AddRptParam("@vid", Me.grdVouchers.CurrentRow.Cells(0).Value)
            ShowReport("rptCashPayment")
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdVouchers.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
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
                    'ShowReport("rptCashPayment")
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


    Private Sub frmExpense_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If
        'Comment against TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(BtnDelete, Nothing)
        '    Exit Sub
        'End If
    End Sub

    Private Sub grdVouchers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdVouchers.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
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
    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged
        Try
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If CheckNumericValue(Me.txtAmount.Text, Me.txtAmount) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            'End Task:2491

            'start task 3217
            If Me.txtAmount.Text = "" Then
                lblAmountNumberConvertor.Text = ""
            Else
                lblAmountNumberConvertor.Text = ModGlobel.NumberToWords(Me.txtAmount.Text)
            End If
            'end task 3217

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

            'If arrFile.Length > 0 Then
            If arrFile.Count > 0 Then
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
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image,1 as pic From DocumentAttachment  where right(filename,3) in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG')  and  DocId=" & Val(Me.grdVouchers.GetRow.Cells(0).Value.ToString) & " AND Source=N'" & Me.Name & "' "
            'Altered Against Task#2015060008 To Discard Non Images

            Dim dtAttach As New DataTable
            dtAttach.TableName = "dtAttachment"
            dtAttach = GetDataTable(strSQL)

            If dtAttach IsNot Nothing Then
                If dtAttach.Rows.Count > 0 Then
                    For Each r As DataRow In dtAttach.Rows
                        If r.Item("PIC").ToString = "1" Then
                            r.BeginEdit()
                            If IO.File.Exists(CStr(r("Path").ToString & "\" & r("FileName").ToString)) Then
                                LoadPicture(r, "Attachment_Image", CStr(r("Path").ToString & "\" & r("FileName").ToString))
                            End If
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
    Private Sub PrintVoucherAttachmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintVoucherAttachmentToolStripMenuItem.Click
        Try
            If Me.grdVouchers.RowCount = 0 Then Exit Sub
            'AddRptParam("Pm-dtVoucher.Voucher_Id", Me.grdVouchers.GetRow.Cells(0).Value)
            DataSetShowReport("RptVoucherDocument", GetVoucherRecord())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintAttachmentVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentVoucherToolStripMenuItem.Click
        Try
            PrintVoucherAttachmentToolStripMenuItem_Click(Nothing, Nothing)
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

#Region "SMS Template"
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
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Expense By Cash")
            str.Add("Expense By Bank")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
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

    Private Sub Btn_SaveAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SaveAttachment.Click
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
                FillDropDown(Me.cmbCostCenter, "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                    & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                    & "Else " _
                    & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder")
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

    Private Sub PrintUpdatedVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintUpdatedVoucherToolStripMenuItem.Click
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

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                If blnEditMode = True Then
                Else
                    Me.txtCurrencyRate.Text = Math.Round(Convert.ToDouble(dr.Row.Item("CurrencyRate").ToString), 4)
                End If

                ''TASK TFS1474
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True
                End If
                ''END TFS1474


                Me.grd.RootTable.Columns("CurrencyAmount").Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns("Amount").Caption = "Amount (" & BaseCurrencyName & ")"

                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns("Amount").Visible = False
                Else
                    Me.grd.RootTable.Columns("Amount").Visible = True
                End If

                If Val(Me.txtCurrencyRate.Text.ToString) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
            End If
            'Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
            'Me.txtCurrencyRate.Text = dr.Row.Item("CurrencyRate").ToString
            'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
            'Me.grd.RootTable.Columns("Amount").Caption = "" & GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)) & " Amount"
            'If Me.cmbCurrency.Text.ToUpper.ToString = GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)).ToUpper.ToString Then
            '    Me.grd.RootTable.Columns("Amount").Visible = False
            'Else
            '    Me.grd.RootTable.Columns("Amount").Visible = True
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        Try
            Me.grd.GetRow.Cells(grdEnm.Amount).Value = Me.grd.GetRow.Cells(grdEnm.CurrencyAmount).Value * Me.grd.GetRow.Cells(grdEnm.CurrencyRate).Value
            If e.Column.Key = "CostCenterId" Then
                If IsVoucherCostCentreReshuffled(Val(Me.grd.GetRow.Cells("DetailId").Value.ToString)) Then
                    ShowErrorMessage("Cost Centre can not be changed because it has been shifted.")
                    Me.grd.GetRow.Cells(grdEnm.CostCenterId).Value = CostCentreId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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

    Private Sub grd_CurrentCellChanging(sender As Object, e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles grd.CurrentCellChanging
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
    Private Sub GetAllUsersRecord(Optional ByVal strCondition As String = "")
        Try
            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Dim strSql As String = String.Empty
            If Mode = "Normal" Then
                strSql = "SELECT DISTINCT tblVoucher.voucher_id, voucher_no, voucher_date, CASE WHEN (voucher_Type_ID = 4) THEN 'Bank' ELSE 'Cash' END AS [Payment Method], tblVoucher.Cheque_No, tblVoucher.Remarks, isnull(ExpenseAmount.Amount,0) as Amount, IsNull(Post,0) as Post, Case When IsNull(Post,0)=1 then 'Posted' else 'UnPosted' end as Status,IsNull(tblVoucher.Checked,0) as Checked, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],  Isnull(Employee_Id,0) as Employee_Id, IsNull(tblVoucher.CMFADocId,0) as CMFADocId, CMFA.DocNo + '~' + Convert(Varchar, CMFA.DocDate,102) as CMFADocNo, IsNull([No Of Attachment],0) as  [No Of Attachment],tblvoucher.username as 'User Name',CompanyDefTable.CompanyName " _
                                  & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id  Left Outer join  CompanyDefTable on tblVoucher.location_id=CompanyDefTable.CompanyId  LEFT OUTER JOIN CMFAMasterTable CMFA On CMFA.DocId = tblVoucher.CMFADocId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = voucher_no INNER JOIN(Select voucher_id, SUM(ISNULL(credit_amount,0)) as Amount From tblVoucherDetail Group By Voucher_Id) ExpenseAmount On ExpenseAmount.Voucher_Id = tblVoucher.Voucher_Id LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = tblVoucher.Voucher_Id " _
                                  & " WHERE (source = N'" & Me.Name & "') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, voucher_date,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
                If flgCompanyRights = True Then
                    strSql += " AND tblVoucher.Location_Id=" & MyCompanyId
                End If
            End If
            If strCondition <> "" Then
                ' strSql &= " AND account_type = N'" & strCondition & "'"
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
            'strSql = strSql + " order by 2 desc"
            strSql = strSql + " order by tblVoucher.voucher_id desc"
            FillGridEx(grdVouchers, strSql, True)
            Me.grdVouchers.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdVouchers.RootTable.Columns.Add("Column1")
            Me.grdVouchers.RootTable.Columns("Column1").ActAsSelector = True
            Me.grdVouchers.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grdVouchers.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdVouchers.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            grdVouchers.RootTable.Columns(0).Visible = False  'Voucher ID
            grdVouchers.RootTable.Columns("Post").Visible = False
            grdVouchers.RootTable.Columns("Employee_Id").Visible = False
            grdVouchers.RootTable.Columns("CMFADocId").Visible = False 'Task:M60 Hidden Column
            Me.grdVouchers.RootTable.Columns("voucher_no").Caption = "Vouchr No"
            Me.grdVouchers.RootTable.Columns("voucher_date").Caption = "Date"
            Me.grdVouchers.RootTable.Columns("Amount").FormatString = "N"
            Me.grdVouchers.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdVouchers.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdVouchers.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdVouchers.RootTable.Columns("Voucher_Date").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnGetAllRecord_Click(sender As Object, e As EventArgs) Handles btnGetAllRecord.Click
        Try
            GetAllUsersRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCashAccount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCashAccount.SelectedIndexChanged

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
        'End Tas:TFS1198
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : TFS2702 : Show Approval History of the current Document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnApprovalHistory_Click(sender As Object, e As EventArgs) Handles btnApprovalHistory.Click
        Try
            frmApprovalHistory.DocumentNo = Me.txtVoucherNo.Text
            frmApprovalHistory.Source = Me.Name
            frmApprovalHistory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomerBalance_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerBalance.TextChanged
        If Me.txtCustomerBalance.Text = "" Then
            lblBalanceNumberConvertor.Text = ""
        Else
            lblBalanceNumberConvertor.Text = ModGlobel.NumberToWords(Me.txtCustomerBalance.Text)
        End If
    End Sub

    Private Sub txtCashBalances_TextChanged(sender As Object, e As EventArgs) Handles txtCashBalances.TextChanged
        If Me.txtCashBalances.Text = "" Then
            lblCashBlncNumberConvertor.Text = ""
        Else
            lblCashBlncNumberConvertor.Text = ModGlobel.NumberToWords(Me.txtCashBalances.Text)
        End If
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Expense"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdVouchers.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdVouchers.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdVouchers.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Expense"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Row_Index = Me.grd.CurrentRow.RowIndex
            Me.cmbAccounts.Value = Me.grd.GetRow.Cells(grdEnm.coa_detail_id).Value
            Me.txtAmount.Text = Me.grd.GetRow.Cells(grdEnm.CurrencyAmount).Value
            If (Me.grd.GetRow.Cells(grdEnm.Cheque_No).Text.ToString()) = "" Then
                GroupBox1.Visible = False
            Else
                GroupBox1.Visible = True
                txtChequeNo.Text = Me.grd.GetRow.Cells(grdEnm.Cheque_No).Text.ToString()
                dtChequeDate.Value = Me.grd.GetRow.Cells(grdEnm.Cheque_Date).Value
            End If
            Me.cmbCostCenter.Text = Me.grd.GetRow.Cells(grdEnm.CostCenterId).Text.ToString()
            Me.txtReference.Text = Me.grd.GetRow.Cells(grdEnm.Reference).Text.ToString()
            Dim rowCol As New Janus.Windows.GridEX.GridEXFormatStyle
            rowCol.BackColor = Color.AntiqueWhite
            Me.grd.CurrentRow.RowStyle = rowCol

            Me.grd.Enabled = False
            Me.btnAdd.Visible = False
            Me.btnReplace.Visible = True
            Me.btnCancel.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReplace_Click(sender As Object, e As EventArgs) Handles btnReplace.Click
        Try
            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtGrd.Rows.RemoveAt(Row_Index)
            dtGrd.AcceptChanges()
            Dim dr As DataRow
            dr = dtGrd.NewRow

            dr.Item(grdEnm.coa_detail_id) = Me.cmbAccounts.ActiveRow.Cells(0).Text
            dr.Item(grdEnm.detail_title) = Me.cmbAccounts.ActiveRow.Cells(1).Text
            dr.Item(grdEnm.detail_code) = Me.cmbAccounts.ActiveRow.Cells(2).Text
            dr.Item(grdEnm.CurrencyId) = Me.cmbCurrency.SelectedValue
            dr.Item(grdEnm.CurrencyAmount) = Val(Me.txtAmount.Text)
            dr.Item(grdEnm.CurrencyRate) = Val(txtCurrencyRate.Text)
            Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
            If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                dr.Item(grdEnm.BaseCurrencyId) = Val(ConfigCurrencyVal)
                dr.Item(grdEnm.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
            End If
            dr.Item(grdEnm.Amount) = Val(Me.txtAmount.Text) * Val(txtCurrencyRate.Text)
            dr.Item(grdEnm.Reference) = Me.txtReference.Text.Trim.ToString.Replace("'", "''")
            dr.Item(grdEnm.Cheque_No) = IIf(Me.GroupBox1.Visible = True, Me.txtChequeNo.Text, DBNull.Value)
            dr.Item(grdEnm.Cheque_Date) = IIf(Me.GroupBox1.Visible = True, Me.dtChequeDate.Value, DBNull.Value)
            dr.Item(grdEnm.Type) = Me.cmbAccounts.ActiveRow.Cells("Type").Value.ToString
            dr.Item(grdEnm.CostCenterId) = Me.cmbCostCenter.SelectedValue
            dtGrd.Rows.InsertAt(dr, Row_Index)
            Me.ClearFields("Detail")
            If Me.cmbCashAccount.SelectedIndex > 0 Then
                Me.cmbVoucherType.Enabled = False
                Me.cmbCashAccount.Enabled = False
            End If
            Me.cmbCurrency.Enabled = False
            Me.grd.Enabled = True
            Me.btnAdd.Visible = True
            Me.btnCancel.Visible = False
            Me.btnReplace.Visible = False
            cmbAccounts.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.ClearFields("Detail")
        If Me.cmbCashAccount.SelectedIndex > 0 Then
            Me.cmbVoucherType.Enabled = False
            Me.cmbCashAccount.Enabled = False
        End If
        Me.cmbCurrency.Enabled = False
        Me.grd.Enabled = True
        Me.btnAdd.Visible = True
        Me.btnCancel.Visible = False
        Me.btnReplace.Visible = False
        cmbAccounts.Focus()
    End Sub

    'Change by Murtaza for txtcurrencyrate text change (11/09/2022) 
    Private Sub txtCurrencyRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrencyRate.TextChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True

                End If
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If

                Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(grdEnm.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                'Me.grd.RootTable.Columns(grdEnm.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"

                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(grdEnm.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(grdEnm.CurrencyRate).Visible = False
                    'Me.grd.RootTable.Columns(grdEnm.CurrencyTaxAmount).Visible = False
                    If Me.grd.RowCount > 0 Then
                        For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                            GriDEX.BeginEdit()
                            GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                            GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                            GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                            GriDEX.Cells("BaseCurrencyRate").Value = 1
                            GriDEX.EndEdit()
                        Next
                    End If
                Else
                    Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(grdEnm.CurrencyRate).Visible = True
                    'Me.grd.RootTable.Columns(grdEnm.CurrencyTaxAmount).Visible = False
                    If Me.grd.RowCount > 0 Then
                        For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                            GriDEX.BeginEdit()
                            GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                            GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                            GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                            GriDEX.Cells("BaseCurrencyRate").Value = 1
                            GriDEX.EndEdit()
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Change by Murtaza for txtcurrencyrate text change (11/09/2022)

End Class