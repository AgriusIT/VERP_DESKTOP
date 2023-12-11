''13-Jan-2014   Task:2375        Imran Ali        Covnerter Problems And Development
''16-Jan-2014    TASK:2382         Imran Ali        Add Field Payee Title In Voucher And Invoice Based Payment
''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
'2015-01-07  Task#201507001 Update Voucher_Code in Update Statement
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Net
Imports System.Data.SqlClient

Public Class VouchersDAL
    Public Function Add(ByVal ObjMod As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        'Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        'If Not Con.State = 1 Then Con.Open()
        'trans = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO tblVoucher(Location_Id,voucher_code,finiancial_year_id,voucher_type_id,voucher_month,voucher_no,voucher_date, coa_detail_id, post, source, Reference,UserName,Posted_UserName,Remarks) " _
            & " Values(" & ObjMod.LocationId & ", N'" & ObjMod.VoucherCode & "', " & ObjMod.FinancialYearId & ", " & ObjMod.VoucherTypeId & ", N'" & ObjMod.VoucherMonth & "', N'" & ObjMod.VoucherNo & "', N'" & ObjMod.VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            & " " & ObjMod.CoaDetailId & ", " & IIf(ObjMod.Post = True, 1, 0) & ", " _
            & " N'" & ObjMod.Source & "', N'" & ObjMod.References.Trim.Replace("'", "''") & "', N'" & ObjMod.UserName & "', " & IIf(ObjMod.Post = True, "N'" & ObjMod.Posted_UserName & "'", "NULL") & ",N'" & ObjMod.References.Replace("'", "''") & "' ) Select @@Identity"
            ObjMod.VoucherId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            'Call Function from ADD Detail 
            Call AddDt(ObjMod.VoucherId, ObjMod, trans)
            'Activity Log Detail Below 
            ObjMod.ActivityLog.ActivityName = "Save"
            ObjMod.ActivityLog.RecordType = "Accounts"
            ObjMod.ActivityLog.RefNo = ObjMod.VoucherNo
            'Call ActivityBuldFunction in UtilityDAL 
            Call UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function AddDt(ByVal MasterID As Integer, ByVal ObjModDt As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim ObjModDetail As List(Of VouchersDetail) = ObjModDt.VoucherDatail
        Try
            Dim str As String = String.Empty

            For Each ObjMod As VouchersDetail In ObjModDetail
                'Before against task:2375
                'str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments,debit_amount,credit_amount, CostCenterId, sp_refrence, Cheque_No, Cheque_Date) " _
                '& "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", N'" & ObjMod.Comments.Trim.Replace("'", "''") & "', " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.CostCenter & ", " & IIf(ObjMod.SPReference.ToString = "", "NULL", "N'" & ObjMod.SPReference.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_No.ToString = "", "NULL", "N'" & ObjMod.Cheque_No.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_No.ToString = "", "NULL", "N'" & CDate(IIf(ObjMod.Cheque_Date = "#12:00:00 AM#", Now, ObjMod.Cheque_Date)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ") Select @@Identity"
                'Before against task:2382
                'Task:2375 Change Query Cheque Date DataType
                'str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments,debit_amount,credit_amount, CostCenterId, sp_refrence, Cheque_No, Cheque_Date) " _
                '& "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", N'" & ObjMod.Comments.Trim.Replace("'", "''") & "', " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.CostCenter & ", " & IIf(ObjMod.SPReference.ToString = "", "NULL", "N'" & ObjMod.SPReference.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_No.ToString = "", "NULL", "N'" & ObjMod.Cheque_No.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_Date = "#12:00:00 AM#", "NULL", "N'" & ObjMod.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ") Select @@Identity"
                'ObjMod.VoucherDetailId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
                'End Task:2375
                'Task:2382 Added Column PayeeTitle
                Dim PayeeTitle As String = String.Empty
                If ObjMod.PayeeTitle Is Nothing Then
                    PayeeTitle = String.Empty
                    ObjMod.PayeeTitle = PayeeTitle
                End If
                'Before against task:2745
                'str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments,debit_amount,credit_amount, CostCenterId, sp_refrence, Cheque_No, Cheque_Date, PayeeTitle) " _
                ' & "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", N'" & ObjMod.Comments.Trim.Replace("'", "''") & "', " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.CostCenter & ", " & IIf(ObjMod.SPReference.ToString = "", "NULL", "N'" & ObjMod.SPReference.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_No.ToString = "", "NULL", "N'" & ObjMod.Cheque_No.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_Date = "#12:00:00 AM#", "NULL", "N'" & ObjMod.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(ObjMod.PayeeTitle = "", "NULL", "N'" & ObjMod.PayeeTitle.Replace("'", "''") & "'") & ") Select @@Identity"
                'End Task:2382
                'Task:2745 Added Field Cheque Description
                'str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments,debit_amount,credit_amount, CostCenterId, sp_refrence, Cheque_No, Cheque_Date, PayeeTitle, ChequeDescription) " _
                '& "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", " & IIf(ObjMod.Comments = "", "NULL", "N'" & ObjMod.Comments.Trim.Replace("'", "''") & "'") & ", " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.CostCenter & ", " & IIf(ObjMod.SPReference.ToString = "", "NULL", "N'" & ObjMod.SPReference.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_No.ToString = "", "NULL", "N'" & ObjMod.Cheque_No.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_Date = "#12:00:00 AM#", "NULL", "N'" & ObjMod.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(ObjMod.PayeeTitle = "", "NULL", "N'" & ObjMod.PayeeTitle.Replace("'", "''") & "'") & ", " & IIf(ObjMod.ChequeDescription = "", "NULL", "N'" & ObjMod.ChequeDescription.Replace("'", "''") & "'") & ") Select @@Identity"
                'End Task:2745
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments,debit_amount,credit_amount, CostCenterId, sp_refrence, Cheque_No, Cheque_Date, PayeeTitle, ChequeDescription,contra_coa_detail_id,EmpId) " _
              & "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", " & IIf(ObjMod.Comments = "", "NULL", "N'" & ObjMod.Comments.Trim.Replace("'", "''") & "'") & ", " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.CostCenter & ", " & IIf(ObjMod.SPReference.ToString = "", "NULL", "N'" & ObjMod.SPReference.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_No.ToString = "", "NULL", "N'" & ObjMod.Cheque_No.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_Date = "#12:00:00 AM#", "NULL", "N'" & ObjMod.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(ObjMod.PayeeTitle = "", "NULL", "N'" & ObjMod.PayeeTitle.Replace("'", "''") & "'") & ", " & IIf(ObjMod.ChequeDescription = "", "NULL", "N'" & ObjMod.ChequeDescription.Replace("'", "''") & "'") & "," & ObjMod.contra_coa_detail_id & "," & IIf(ObjMod.EmpId = Nothing, "NULL", ObjMod.EmpId) & ") Select @@Identity"
                ObjMod.VoucherDetailId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))

            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal ObjMod As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim VoucherId As Integer
        'Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        'If Not Con.State = 1 Then Con.Open()
        'Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String
            Dim Dt As New DataTable
            'Dim Da As SqlClient.SqlDataAdapter

            strSQL = "Select Voucher_Id From TblVoucher WHERE Voucher_No=N'" & ObjMod.VNo & "'"
            If trans IsNot Nothing Then
                VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Else
                VoucherId = SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL)
            End If
            If VoucherId = 0 Then
                Add(ObjMod, trans)
                Return True
            End If
            'Da = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            'Da.Fill(Dt)
            'If Dt.Rows.Count > 0 Then
            '    VoucherId = Dt.Rows(0).Item(0).ToString
            'End If



            If UtilityDAL.GetConfigValue("EnabledDuplicateVoucherLog", trans).ToString.ToUpper = "TRUE" Then
                UtilityDAL.CreateDuplicationVoucher(VoucherId, "Update", LoginUser.LoginUserId, LoginUser.LoginUserName, trans) '2710151
            End If
            Dim str As String
            'Before against ReqId-934
            'str = "Update tblVoucher Set Location_Id=" & ObjMod.LocationId & ", Voucher_Code=N'" & ObjMod.VNo & "', finiancial_year_id='1', Voucher_Type_Id=" & ObjMod.VoucherTypeId & ",  Voucher_Month=N'" & ObjMod.VoucherMonth & "', Voucher_No=N'" & ObjMod.VNo & "', Voucher_Date=N'" & ObjMod.VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', coa_detail_id=" & ObjMod.CoaDetailId & ",  Post=" & ObjMod.Post & ", Source=N'" & ObjMod.Source & "', Reference=N'" & ObjMod.References & "'   " & IIf(ObjMod.Post = False, " , UserName=N'" & ObjMod.UserName & "'", "") & ", Posted_UserName=" & IIf(ObjMod.Post = True, "N'" & ObjMod.Posted_UserName & "'", "NULL") & " WHERE Voucher_Id=" & VoucherId & ""
            'ReqId-934 Resolve Comma Error
            'Marked Against Task#201507001 as Voucher No was inserting instead of voucher code
            'str = "Update tblVoucher Set Location_Id=" & ObjMod.LocationId & ", Voucher_Code=N'" & ObjMod.VNo & "', finiancial_year_id='1', Voucher_Type_Id=" & ObjMod.VoucherTypeId & ",  Voucher_Month=N'" & ObjMod.VoucherMonth & "', Voucher_No=N'" & ObjMod.VNo & "', Voucher_Date=N'" & ObjMod.VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', coa_detail_id=" & ObjMod.CoaDetailId & ",  Post=" & ObjMod.Post & ", Source=N'" & ObjMod.Source & "', Reference=N'" & ObjMod.References.Replace("'", "''") & "'   " & IIf(ObjMod.Post = False, " , UserName=N'" & ObjMod.UserName.Replace("'", "''") & "'", "") & ", Posted_UserName=" & IIf(ObjMod.Post = True, "N'" & ObjMod.Posted_UserName.Replace("'", "''") & "'", "NULL") & ",Remarks=N'" & ObjMod.References.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId & ""
            'Marked Against Task#201507001 as Voucher No was inserting instead of voucher code Ali Anari
            'Alter Against Task#201507001 insert voucher_code Ali Ansari
            str = "Update tblVoucher Set Location_Id=" & ObjMod.LocationId & ", Voucher_Code=N'" & ObjMod.VoucherCode & "', finiancial_year_id='1', Voucher_Type_Id=" & ObjMod.VoucherTypeId & ",  Voucher_Month=N'" & ObjMod.VoucherMonth & "', Voucher_No=N'" & ObjMod.VNo & "', Voucher_Date=N'" & ObjMod.VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', coa_detail_id=" & ObjMod.CoaDetailId & ",  Post=" & IIf(ObjMod.Post = True, 1, 0) & ", Source=N'" & ObjMod.Source & "', Reference=N'" & ObjMod.References.Replace("'", "''") & "'   " & IIf(ObjMod.Post = False, " , UserName=N'" & ObjMod.UserName.Replace("'", "''") & "'", "") & ", Posted_UserName=" & IIf(ObjMod.Post = True, "N'" & ObjMod.Posted_UserName.Replace("'", "''") & "'", "NULL") & ",Remarks=N'" & ObjMod.References.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId & ""
            'Alter Against Task#201507001 insert voucher_code Ali Ansari
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete From Voucher Detail 
            Dim strDelVoucherDetail As String
            strDelVoucherDetail = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDelVoucherDetail)
            'Call Function from ADD Detail 
            Call AddDt(VoucherId, ObjMod, trans)
            'Activity Log Detail Below 

            ObjMod.ActivityLog.ActivityName = "Update"
            ObjMod.ActivityLog.RecordType = "Accounts"
            ObjMod.ActivityLog.RefNo = ObjMod.VoucherNo
            'Call ActivityBuldFunction in UtilityDAL 
            Call UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ObjMod As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim VoucherId As Integer
        'Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        'If Not Con.State = 1 Then Con.Open()
        'Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String
            Dim Dt As New DataTable
            Dim Da As SqlClient.SqlDataAdapter = Nothing

            strSQL = "Select Voucher_Id From TblVoucher WHERE Voucher_No=N'" & ObjMod.VNo & "'"
            If trans IsNot Nothing Then
                Dt = UtilityDAL.GetDataTable(strSQL, trans)
            Else
                Dt = UtilityDAL.GetDataTable(strSQL)
            End If
            If Dt.Rows.Count > 0 Then
                VoucherId = Dt.Rows(0).Item(0).ToString
            End If
            If UtilityDAL.GetConfigValue("EnabledDuplicateVoucherLog", trans).ToString.ToUpper = "TRUE" Then
                UtilityDAL.CreateDuplicationVoucher(VoucherId, "Delete", LoginUser.LoginUserId, LoginUser.LoginUserName, trans) '2710151
            End If
            Dim str As String
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Dim str1 As String
            str1 = "Delete From tblVoucher WHERE Voucher_Id=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str1)

            'Activity Log below
            ObjMod.ActivityLog.ActivityName = "Delete"
            ObjMod.ActivityLog.RecordType = "Accounts"
            ObjMod.ActivityLog.RefNo = ObjMod.VoucherNo
            'Call ActivityBuldFunction in UtilityDAL 
            Call UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
            ' trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function

    Public Function Update1(ByVal vlist As List(Of VouchersMaster), ByVal vtype As String) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String
            For Each voucher As VouchersMaster In vlist
                Select Case vtype
                    Case "Voucher"

                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                    Case "Sale"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        str = String.Empty
                        str = "Update SalesMasterTable set post = " & IIf(voucher.Post = True, 1, 0) & " Where SalesNo = N'" & voucher.VoucherNo & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next
                    Case "Sale Return"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        str = String.Empty
                        str = "Update SalesReturnMasterTable set post = " & IIf(voucher.Post = True, 1, 0) & " Where SalesReturnNo = N'" & voucher.VoucherNo & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next

                    Case "Purchase"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        str = String.Empty
                        str = "Update ReceivingMasterTable set post = " & IIf(voucher.Post = True, 1, 0) & " Where ReceivingNo = N'" & voucher.VoucherNo & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next

                    Case "Purchase Return"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        str = String.Empty
                        str = "Update PurchaseReturnMasterTable set post = " & IIf(voucher.Post = True, 1, 0) & " Where PurchaseReturnNo = N'" & voucher.VoucherNo & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next

                    Case "Store Issuance"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'str = String.Empty
                        'str = "Update DispatchMasterTable set post = " & IIf(voucher.Post = True, 1, 0) & " Where DispatchNo = N'" & voucher.VoucherNo & "'"
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next
                    Case "Production"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)

                        str = String.Empty
                        str = "Update ProductionMasterTable set post = " & IIf(voucher.Post = True, 1, 0) & " Where Production_No = N'" & voucher.VoucherNo & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next
                    Case "Invoice Based Receipt"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)

                        str = String.Empty
                        str = "Update InvoiceBasedReceipts set post = " & IIf(voucher.Post = True, 1, 0) & " Where ReceiptNo = N'" & voucher.VoucherNo & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next
                    Case "Invoice Based Payment"
                        'For Each voucher As VouchersMaster In vlist
                        str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & ", Posted_UserName=" & IIf(voucher.Posted_UserName = Nothing, "NULL", "N'" & voucher.Posted_UserName.Replace("'", "''") & "'") & " Where voucher_id = " & voucher.VoucherId
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)

                        str = String.Empty
                        str = "Update InvoiceBasedPayments set post = " & IIf(voucher.Post = True, 1, 0) & " Where PaymentNo = N'" & voucher.VoucherNo & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        'Next
                        'Else
                        '        For Each voucher As VouchersMaster In vlist
                        '            str = "Update tblVoucher set post = " & IIf(voucher.Post = True, 1, 0) & " Where voucher_id = " & voucher.VoucherId
                        '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                        '        Next
                        '    End If
                End Select
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Sub PostVoucher(ByVal VoucherID As Integer)
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Str As String
        Try
            Str = "Update tblVoucher set post = 1  Where voucher_id = " & VoucherID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str, Nothing)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    ''For Bank Reconciliation
    'Public Function UpdateChequeStatus(ByVal vlist As List(Of VouchersMaster)) As Boolean
    '    Dim con As New SqlConnection(SQLHelper.CON_STR)
    '    If con.State = ConnectionState.Closed Then con.Open()
    '    Dim trans As SqlTransaction = con.BeginTransaction
    '    Try
    '        Dim str As String = String.Empty
    '        For Each voucher As VouchersMaster In vlist
    '            str = "Update tblVoucher set Cheque_Status = " & IIf(voucher.Post = True, 1, 0) & ", Post = " & IIf(voucher.Post = True, 1, 0) & " Where Voucher_Id = " & voucher.VoucherId & " "
    '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
    '        Next
    '        trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        con.Close()
    '    End Try
    'End Function
    'For Bank Reconciliation
    Public Function UpdateChequeStatus(ByVal vlist As List(Of VouchersDetail)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            For Each voucher As VouchersDetail In vlist

                str = "Update tblVoucherDetail set Cheque_Status=" & IIf(voucher.Cheque_Status = True, 1, 0) & ", Cheque_Clearance_Date=" & IIf(voucher.Cheque_Clearance_Date = Date.MinValue, "Null", "'" & voucher.Cheque_Clearance_Date.ToString("yyyy-M-d hh:mm:ss tt") & "'") & " Where Voucher_detail_Id = " & voucher.VoucherDetailId & ""
                'str = "Update tblVoucherDetail set Cheque_Status=" & IIf(voucher.Cheque_Status = True, 1, 0) & ", Cheque_Clearance_Date=N'" & voucher.Cheque_Clearance_Date.ToString("yyyy-M-d hh:mm:ss tt") & "' Where Voucher_detail_Id = " & voucher.VoucherDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function GetAllRec(ByVal BankId As Integer, ByVal unpresent As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT ISNULL(dbo.tblVoucher.CompanyId, 0) AS CompanyId, dbo.tblVoucher.Voucher_Id, dbo.tblVoucherType.Voucher_Type, dbo.tblVoucher.Voucher_No, dbo.tblVoucher.Voucher_Date, " _
            & " dbo.tblVoucher.Reference, dbo.tblVoucher.Cheque_No, dbo.tblVoucher.Cheque_Date, isnull(tot.Total_Amount,0) as Total_Amount,dbo.tblVoucher.Post, ISNULL(dbo.tblVoucher.Cheque_Status, 0) AS Cheque_Status " _
            & " FROM  dbo.tblVoucherType INNER JOIN " _
            & " dbo.tblVoucher ON dbo.tblVoucherType.VoucherTypeId = dbo.tblVoucher.VoucherTypeId LEFT OUTER JOIN (Select Voucher_Id, SUM(Isnull(debit_amount,0)) as Total_Amount From tblVoucherDetail Group By Voucher_Id) Tot on Tot.Voucher_Id = tblVoucher.Voucher_Id WHERE tblVoucher.Voucher_No <> '' " & IIf(BankId > 0, " AND  tblVoucher.Voucher_Id in(Select Voucher_Id From tblVoucherDetail where DetailAcId=" & BankId & ")", "") & ""
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Reverse(ByVal ObjMod As VouchersMaster) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO tblVoucher(Location_Id,voucher_code,finiancial_year_id,voucher_type_id,voucher_month,voucher_no,voucher_date, coa_detail_id, post, source, Reference,UserName,Posted_UserName,Remarks, Reversal) " _
            & " Values(" & ObjMod.LocationId & ", N'" & ObjMod.VoucherCode & "', " & ObjMod.FinancialYearId & ", " & ObjMod.VoucherTypeId & ", N'" & ObjMod.VoucherMonth & "', N'" & ObjMod.VoucherNo & "', N'" & ObjMod.VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            & " " & ObjMod.CoaDetailId & ", " & IIf(ObjMod.Post = True, 1, 0) & ", " _
            & " N'" & ObjMod.Source & "', N'" & ObjMod.References.Trim.Replace("'", "''") & "', N'" & ObjMod.UserName & "', " & IIf(ObjMod.Post = True, "N'" & ObjMod.Posted_UserName & "'", "NULL") & ",N'" & ObjMod.References.Replace("'", "''") & "', " & IIf(ObjMod.Reversal = True, 1, 0) & " ) Select @@Identity"
            ObjMod.VoucherId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            Call ReverseDetail(ObjMod.VoucherId, ObjMod, trans)
            ObjMod.ActivityLog.ActivityName = "Save"
            ObjMod.ActivityLog.RecordType = "Accounts"
            ObjMod.ActivityLog.RefNo = ObjMod.VoucherNo
            Call UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function ReverseDetail(ByVal MasterID As Integer, ByVal ObjModDt As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim ObjModDetail As List(Of VouchersDetail) = ObjModDt.VoucherDatail
        Try
          
            Dim str As String = String.Empty
            For Each ObjMod As VouchersDetail In ObjModDetail
                Dim PayeeTitle As String = String.Empty
                If ObjMod.PayeeTitle Is Nothing Then
                    PayeeTitle = String.Empty
                    ObjMod.PayeeTitle = PayeeTitle
                End If
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments,debit_amount,credit_amount, Currency_debit_amount, Currency_Credit_Amount, CostCenterId, sp_refrence, Cheque_No, Cheque_Date, PayeeTitle, ChequeDescription,contra_coa_detail_id,EmpId, ReversalVoucherDetailId, Cheque_Clearance_Date, Cheque_Status) " _
              & "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", " & IIf(ObjMod.Comments = "", "NULL", "N'" & ObjMod.Comments.Trim.Replace("'", "''") & "'") & ", " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.CurrencyDebitAmount & ", " & ObjMod.CurrencyCreditAmount & ", " & ObjMod.CostCenter & ", " & IIf(ObjMod.SPReference.ToString = "", "NULL", "N'" & ObjMod.SPReference.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_No.ToString = "", "NULL", "N'" & ObjMod.Cheque_No.Replace("'", "''") & "'") & ", " & IIf(ObjMod.Cheque_Date = "#12:00:00 AM#", "NULL", "N'" & ObjMod.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(ObjMod.PayeeTitle = "", "NULL", "N'" & ObjMod.PayeeTitle.Replace("'", "''") & "'") & ", " & IIf(ObjMod.ChequeDescription = "", "NULL", "N'" & ObjMod.ChequeDescription.Replace("'", "''") & "'") & "," & ObjMod.contra_coa_detail_id & "," & IIf(ObjMod.EmpId = Nothing, "NULL", ObjMod.EmpId) & ", " & ObjMod.ReversalVoucherDetailId & ", " & IIf(ObjMod.Cheque_Clearance_Date = "#12:00:00 AM#", "NULL", "N'" & ObjMod.Cheque_Clearance_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(ObjMod.Cheque_Status = True, 1, 0) & ") Select @@Identity"
                ObjMod.VoucherDetailId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            Next
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw ex
        End Try
    End Function
End Class
