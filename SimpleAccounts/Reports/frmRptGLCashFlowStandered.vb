''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : GL Voucher .. 
''// Programmer	     : R@! Shahid
''// Creation Date	 : 09-Sep-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports SBDal
Imports SBModel
Imports System.Collections.Specialized
Imports System.Data
Imports SBUtility.Utility
Imports Microsoft.VisualBasic

Public Class frmRptGLCashFlowStandered

    Dim dblCashBankOpening As Double
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection


    Private Sub frmGLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            ' mobjControlList = GetFormSecurityControls(Me.Name)
            'Me.ApplySecurity(EnumDataMode.Disabled)
            Me.SetButtonImages()
            dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
            dtToDate.Value = Now

            ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
            FillCombos()

            SetConfigurationBaseSetting()
        Catch ex As Exception

        End Try


    End Sub

#Region "Report Interface Metholds .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "")

        ''Dim strSQL As String = ""
        ''Dim ObjDataTable As DataTable
        ''Dim ObjDataRow As DataRow


        ' '' Binding Financial Year Combo .. 
        ' '' =========================================================================================
        ' '' =========================================================================================
        ''Dim ObjDalFinancialYear As New FiniancialYearDefDAL
        ''ObjDataTable = ObjDalFinancialYear.GetAll()


        ''ObjDataRow = ObjDataTable.NewRow
        ''ObjDataRow.Item("FYear Code") = gstrComboZeroIndexString
        ''ObjDataRow.Item("FYear ID") = 0
        ''ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        ''cmbFinancialYear.DataSource = ObjDataTable.Copy


        ''cmbFinancialYear.DisplayMember = "FYear Code"
        ''cmbFinancialYear.ValueMember = "FYear ID"

        ''ObjDalFinancialYear = Nothing

        ''If Me.cmbFinancialYear.Items.Count > 1 Then
        ''    Me.cmbFinancialYear.SelectedValue = gObjFinancialYearInfo.FYearID

        ''End If
        ' '' =========================================================================================
        ' '' =========================================================================================



        ' '' Binding Voucher Type .. 
        ' '' =========================================================================================
        ' '' =========================================================================================
        ''strSQL = " SELECT voucher_type VoucherType, voucher_type_id TypeID FROM tblGlDefVoucherType "
        ''ObjDataTable = UtilityDAL.GetDataTable(strSQL)

        ''ObjDataRow = ObjDataTable.NewRow
        ''ObjDataRow.Item("VoucherType") = gstrComboZeroIndexString
        ''ObjDataRow.Item("TypeID") = 0
        ''ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        ' ''cmbVoucherType.DataSource = ObjDataTable.Copy

        ' ''cmbVoucherType.DisplayMember = "VoucherType"
        ' ''cmbVoucherType.ValueMember = "TypeID"
        ' '' =========================================================================================
        ' '' =========================================================================================




        ' '' Binding Company .. 
        ' '' =========================================================================================
        ' '' =========================================================================================
        ''Dim ObjDalCompany As New CompanyDAL
        ''ObjDataTable = ObjDalCompany.GetAll(gObjUserInfo.UserID)


        ''ObjDataRow = ObjDataTable.NewRow
        ''ObjDataRow.Item("Company Name") = gstrComboZeroIndexString
        ''ObjDataRow.Item("Company ID") = 0
        ''ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        ''cmbCompany.DataSource = ObjDataTable.Copy


        ''cmbCompany.DisplayMember = "Company Name"
        ''cmbCompany.ValueMember = "Company ID"

        ''ObjDalCompany = Nothing

        ''If Me.cmbCompany.Items.Count > 1 Then
        ''    Me.cmbCompany.SelectedValue = gobjLocationInfo.CompanyID

        ''End If
        ' '' =========================================================================================
        ' '' =========================================================================================


    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "")
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
    End Sub

  
    Public Sub ReSetControls(Optional ByVal Condition As String = "")
    End Sub
    Public Sub SetButtonImages()
        Try

            'Me.btnFirst.ImageList = gobjMyImageListForOperationBar
            'Me.btnFirst.ImageKey = "First"

            'Me.btnNext.ImageList = gobjMyImageListForOperationBar
            'Me.btnNext.ImageKey = "Next"

            'Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
            'Me.btnPrevious.ImageKey = "Previous"

            'Me.btnLast.ImageList = gobjMyImageListForOperationBar
            'Me.btnLast.ImageKey = "Last"


            'Me.btnNew.ImageList = gobjMyImageListForOperationBar
            'Me.btnNew.ImageKey = "New"

            'Me.btnSave.ImageList = gobjMyImageListForOperationBar
            'Me.btnSave.ImageKey = "Save"

            'Me.btnUpdate.ImageList = gobjMyImageListForOperationBar
            'Me.btnUpdate.ImageKey = "Update"

            'Me.btnCancel.ImageList = gobjMyImageListForOperationBar
            'Me.btnCancel.ImageKey = "Cancel"

            'Me.btnDelete.ImageList = gobjMyImageListForOperationBar
            'Me.btnDelete.ImageKey = "Delete"

            'Me.btnExit.ImageList = gobjMyImageListForOperationBar
            'Me.btnExit.ImageKey = "Exit"

            'Me.btnPrint.ImageList = gobjMyImageListForOperationBar
            'Me.btnPrint.ImageKey = "Print"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetConfigurationBaseSetting()
        'Try
        '    chkOtherVoucher.Visible = gblnShowOtherVoucher
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean
    End Function

#End Region

#Region "Report Interface Metholds .. "

    Public Function FunAddReportCriteria() As String

        Dim strSql As String
        Dim strCondAccount As String = String.Empty
        Dim strYearCriteria As String
        Dim strLocationCriteria As String

        Dim pbDateFormat As String = "dd/MMM/yyyy"

        '=========================================================================================
        '-- Selection Criteia Building in case of Financial Year selection
        '=========================================================================================
        If Me.cmbFinancialYear.SelectedIndex > 0 Then

            strYearCriteria = " (dbo.tblVoucher.finiancial_year_id = " & cmbFinancialYear.SelectedValue & " ) AND "
        Else

            strYearCriteria = " ( dbo.tblVoucher.Voucher_no <> '000000' ) AND  "
        End If

        '=========================================================================================
        '-- Selection Criteia Building in case of Location selection
        '=========================================================================================
        If Me.cmbCompany.SelectedIndex > 0 Then

            strLocationCriteria = " (dbo.tblVoucher.location_id = " & cmbCompany.SelectedValue & ") AND "
        Else
            strLocationCriteria = "  "
        End If

        Dim strPostCriteria As String = String.Empty
        Dim strOther_Voucher_Criteria As String = String.Empty
        Dim intlocation_id As Integer

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        'If chkunposted.Value = vbUnchecked Then

        '    strPostCriteria = "  (tblVoucher.post = 1) AND "
        'Else

        '    strPostCriteria = ""
        'End If

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        If Me.chkOtherVoucher.Checked = True Then

            strOther_Voucher_Criteria = "  (tblVoucher.Other_Voucher = 0) AND "
        Else

            strOther_Voucher_Criteria = ""
        End If

        '   get the location id
        If Me.cmbCompany.SelectedIndex > 0 Then

            intlocation_id = Me.cmbCompany.SelectedValue
        Else

            intlocation_id = 0
        End If

        Dim ReceiptType As String
        Dim PaymentType As String
        Dim AccType As String

        If Me.optCash.Checked = True Then
            ReceiptType = "'CR'"
            PaymentType = "'CP'"
            AccType = "'Cash'"
        ElseIf Me.optBank.Checked = True Then
            ReceiptType = "'BR'"
            PaymentType = "'BP'"
            AccType = "'Bank'"
        Else
            ReceiptType = "'BR', 'CR'"
            PaymentType = "'BP', 'CP'"
            AccType = "'Cash','Bank'"
        End If

        strSql = "SELECT SUM(credit_amount)-SUM(debit_amount) from ("
        strSql = strSql & "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
                         "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(Me.dtFromDate.MinDate, pbDateFormat) & "' AND '" & Format(Me.dtFromDate.Value.Date.AddDays(-1), pbDateFormat) & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0) " & _
                         "Union " & _
                         "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
                         "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(dtFromDate.MinDate, pbDateFormat) & "' AND '" & Format(dtFromDate.Value.AddDays(-1), pbDateFormat) & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0) "

        strSql = strSql & ")tblOpeningBalance"


        'dblCashBankOpening = Val(UtilityDAL.ReturnDataRow(strSql).Item(0).ToString)


        strSql = "Alter View vwCashFlowPeriodRPT As "

        'strSql = strSql & "SELECT  0 AS Tr_Type ,   dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(dtFromDate.Value, pbDateFormat) & "' AND '" & Format(dtToDate.Value, pbDateFormat) & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0) " & _
        '                 "Union " & _
        '                 "SELECT    1 AS Tr_Type , dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(dtFromDate.Value, pbDateFormat) & "' AND '" & Format(dtToDate.Value, pbDateFormat) & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0) "

        strSql = strSql & "SELECT     CASE WHEN (dbo.tblVoucherDetail.credit_amount > 0) THEN 1 ELSE 0 END AS tr_type, dbo.tblVoucherDetail.coa_detail_id, " _
                        & " dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, dbo.tblVoucherDetail.comments, " _
                        & " dbo.vwCOADetail.detail_title, dbo.tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.credit_amount, dbo.tblVoucher.post " _
                        & " FROM         dbo.tblVoucherDetail INNER JOIN " _
                        & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
                        & " WHERE     (dbo.tblVoucher.voucher_id IN " _
                        & " (SELECT DISTINCT tblVoucher_1.voucher_id " _
                        & " FROM          dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN" _
                        & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType AS tblDefVoucherType_1 ON  " _
                        & " tblVoucher_1.voucher_type_id = tblDefVoucherType_1.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id " _
                        & " WHERE      (vwCOADetail_1.account_type in( " & AccType & ")))) AND (dbo.vwCOADetail.account_type NOT IN ( " & AccType & "))" _
                        & " and " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & " " _
                        & "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(dtFromDate.Value, pbDateFormat) & "' AND '" & Format(dtToDate.Value, pbDateFormat) & "')"
        UtilityDAL.ExecuteQuery(strSql)

        'Dim ObjDAL As New DAL.RptCashFlowDal

        'If ObjDAL.InsertDataForReport("Stander") Then

        strSql = " truncate table TblrptCashFlowStander "
        UtilityDAL.ExecuteQuery(strSql)


        strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                      "Select Tr_Type ,Tr_Type + 1, coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwCashFlowPeriodRPT"

        UtilityDAL.ExecuteQuery(strSql)

        If 1 = 1 Then
            strSql = "SELECT     tblCOAMainSubSubDetail.coa_detail_id, tblCOAMainSubSubDetail.detail_title, dbo.tblCOAMainSubSub.account_type " & _
                                 "FROM         dbo.tblCOAMainSubSubDetail AS tblCOAMainSubSubDetail INNER JOIN " & _
                                 "dbo.tblCOAMainSubSub ON tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " & _
                                 "WHERE     (dbo.tblCOAMainSubSub.account_type IN ( " & AccType & "))"

            Dim dt As DataTable
            dt = UtilityDAL.GetDataTable(strSql).Copy

            Dim ilocation_id As Integer

            ' Get Location ID .. 
            If Me.cmbCompany.SelectedIndex > 0 Then
                ilocation_id = cmbCompany.SelectedValue
            Else
                ilocation_id = 0

            End If

            Dim strProcedure As String = " usp_AccTrialOpeningBalance '','" & Format(Me.dtFromDate.Value.Date, "yyyy/MM/dd") & "'," & ilocation_id & "," & Val(Me.chkOtherVoucher.Checked) & "," & Val(Me.chkOtherVoucher.Checked)
            'UtilityDAL.ExecuteQuery(strProcedure)

            '//Preparing Query string to insert opening balance
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     1 AS tr_Type, 0 AS Sort, coa_detail_id, '" & Me.dtFromDate.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Opening Balance' AS Comments, detail_title, 0 AS Credit, OpeningBalance, " & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                        " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (dbo.tblVoucher.voucher_date < '" & Me.dtFromDate.Value.Date & "') GROUP BY dbo.tblVoucherDetail.coa_detail_id)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.dtFromDate.Value.Date.AddDays(-1), pbDateFormat) & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Opening Balance
            UtilityDAL.ExecuteQuery(strSql)


            ''//Preparing Query string to insert opening balance
            strProcedure = " usp_AccTrialOpeningBalance '','" & Format(Me.dtToDate.Value.Date.AddDays(1), "yyyy/MM/dd") & "'," & ilocation_id & "," & Val(Me.chkOtherVoucher.Checked) & "," & Val(Me.chkOtherVoucher.Checked)
            ' UtilityDAL.ExecuteQuery(strProcedure)

            '//Preparing Query string to insert Closing balance
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     0 AS tr_Type, 3 AS Sort, coa_detail_id, '" & Me.dtToDate.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Closing Balance' AS Comments, detail_title, OpeningBalance,  0 AS Credit," & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                        " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (dbo.tblVoucher.voucher_date <= '" & Me.dtToDate.Value.Date & "') GROUP BY dbo.tblVoucherDetail.coa_detail_id)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.dtFromDate.Value.Date.AddDays(-1), pbDateFormat) & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Closing Balance
            UtilityDAL.ExecuteQuery(strSql)
            'strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) values(" & _
            '                             "0 ,3, " & dtRow.Item(0).ToString & " ,'" & Format(Me.dtFromDate.Value.Date.AddDays(-1), pbDateFormat) & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ,0  ," & dblCashBankOpening & ",'True')"
            ''//Inserting Opening Balance
            'UtilityDAL.ExecuteQuery(strSql)

            '    Next

            'End If
        Else
        End If

        Return ""

    End Function

    Private Sub InsertOpening()

    End Sub

    Public Sub FunAddReportPramaters()

        Try

            ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
            FunAddReportCriteria()



            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptGlCashFlow1.rpt")



            Dim ObjCompanyData As New DataTable
            'ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)



            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            objHashTableParamter.Add("fromdate", Format(Me.dtFromDate.Value.Date, "dd/MMM/yyyy"))
            objHashTableParamter.Add("todate", Format(Me.dtToDate.Value.Date, "dd/MMM/yyyy"))

            '' Adding Description Parameter .. 
            'If cmbVoucherType.SelectedIndex > 0 Then
            objHashTableParamter.Add("OpeningBalance", dblCashBankOpening)

            'Else
            '    objHashTableParamter.Add("description", "(All Vouchers) From " & Format(dtFromDate.Value.Date, "dd-MMM-yyyy") & " To  " & Format(dtToDate.Value.Date, "dd-MMM-yyyy"))


            'End If


            ' Adding Location Parameter .. 
            If cmbCompany.SelectedIndex > 0 Then
                objHashTableParamter.Add("Location", cmbCompany.Text)
            Else
                objHashTableParamter.Add("Location", "ALL")

            End If


            '' '' Adding A Parameter OF Show Header, 
            ' ''If Convert.ToBoolean(GetSystemConfigurationValue("Show_Report_Header").ToString) = True Then
            ' ''    objHashTableParamter.Add("ShowHeader", True)

            ' ''Else
            ' ''    objHashTableParamter.Add("ShowHeader", False)

            ' ''End If



            ' Adding Parameter Of Print And Export Button .. 
            ' =======================================================
            If mobjControlList.Item("btnPrint") Is Nothing Then
                objHashTableParamter.Add("PrintRights", "False")
            Else
                objHashTableParamter.Add("PrintRights", "True")
            End If


            If mobjControlList.Item("btnExport") Is Nothing Then
                objHashTableParamter.Add("ExportRights", "False")
            Else
                objHashTableParamter.Add("ExportRights", "True")
            End If
            ' =======================================================
            ' =======================================================



        Catch ex As Exception
            Throw ex

        End Try

    End Sub

#End Region


    Private Sub btnGenerateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try

            If dtFromDate.Value.Date > dtToDate.Value.Date Then
                ShowErrorMessage("FromDate should be less than ToDate")
                dtFromDate.Focus()
                Exit Sub
            End If


            ' Implemented Interface Method .. 
            ' Used To Add Report Parameters .. (Also Report Name Is Given In This Function .. )
            Call FunAddReportPramaters()


            ' Create A Object Of Report Viewer .. And Calls His Show Method, To Show The Report .. 
            ' ------------------------------------------------------------------------------------
            Dim rptViewer As New rptViewer
            rptViewer.Text = Me.Text
            rptViewer.Show()
            ' ------------------------------------------------------------------------------------

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub cmbFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFinancialYear.SelectedIndexChanged


        If cmbFinancialYear.SelectedIndex > 0 Then
            Dim dtRow As DataRowView = CType(cmbFinancialYear.SelectedItem, DataRowView)

            dtFromDate.MinDate = dtRow("Start Date")
            dtToDate.MaxDate = dtRow("End Date")

            dtFromDate.Value = dtRow("Start Date")
            dtToDate.Value = dtRow("End Date")

        Else

            dtFromDate.MinDate = CDate("01/07/1980")
            dtToDate.MaxDate = CDate("01/01/3000")

            Me.dtFromDate.Value = Now.AddMonths(-1)
            Me.dtToDate.Value = Now

        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class