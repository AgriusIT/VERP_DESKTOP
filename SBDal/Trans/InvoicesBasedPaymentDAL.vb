''7-Jan-2014 Task:2370 Imran Ali          Sale and purchase invoice wise aging report 
''13-Jan-2014   Task:2375        Imran Ali        Covnerter Problems And Development
''16-Jan-2014     TASK:2381        Imran Ali         Problem In Invoice Based Payment 
''16-Jan-2014      TASK:2382         Imran Ali          Add Field Payee Title In Voucher And Invoice Based Payment
''07-Mar-2014  TASK:2470 Imran Ali     Add month in invoice base payment narration
'Task 2537 Update The Save And Update Function In This File 
'26-4-2014 TASK:M35 Imran Ali Show All Record 
''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Net
Public Class InvoicesBasedPaymentDAL
    Dim VoucherDAL As VouchersDAL  'Import Voucher DAL Class
    Dim VoucherDetail As VouchersDetail

    Public Function Add(ByVal objMod As InvoicesBasedPaymentMaster) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try


            objMod.PaymentNo = GetVoucherNo(objMod.PaymentNo.Substring(0, objMod.PaymentNo.IndexOf("-")), objMod.PaymentDate, objMod.CompanyName, trans)
            objMod.PVNo = objMod.PaymentNo
            objMod.VoucherMaster.VoucherCode = objMod.PaymentNo
            objMod.VoucherMaster.VoucherNo = objMod.PaymentNo
            objMod.VoucherMaster.VNo = objMod.PaymentNo

            'Before against task:2375
            'Dim str As String = "Insert Into InvoiceBasedPayments(PaymentNo, PaymentDate, Remarks, PaymentAmount, PaymentMethod, PaymentAccountID, VendorCode, ChequeNo, ChequeDate, UserName, CostCenterId) " _
            '& " Values (N'" & objMod.PaymentNo & "', N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference.ToString.Replace("'", "''") & "', " & objMod.NetPayment & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.VendorCode & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", "N'" & objMod.ChequeNo.Replace("'", "''") & "'") & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", "N'" & CDate(IIf(objMod.ChequeDate = Nothing, Now, objMod.ChequeDate)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ") Select @@Identity "
            'Before against task:2381
            'Task:2375 Change DataType Cheque Date
            'Dim str As String = "Insert Into InvoiceBasedPayments(PaymentNo, PaymentDate, Remarks, PaymentAmount, PaymentMethod, PaymentAccountID, VendorCode, ChequeNo, ChequeDate, UserName, CostCenterId) " _
            '& " Values (N'" & objMod.PaymentNo & "', N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference.ToString.Replace("'", "''") & "', " & objMod.NetPayment & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.VendorCode & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", "N'" & objMod.ChequeNo.Replace("'", "''") & "'") & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'")) & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ") Select @@Identity "
            'End Task:2375
            'Task:2381 Change Apostrophe At Cheque No Field
            'Dim str As String = "Insert Into InvoiceBasedPayments(PaymentNo, PaymentDate, Remarks, PaymentAmount, PaymentMethod, PaymentAccountID, VendorCode, ChequeNo, ChequeDate, UserName, CostCenterId) " _
            ' & " Values (N'" & objMod.PaymentNo & "', N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference.ToString & "', " & objMod.NetPayment & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.VendorCode & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", "" & objMod.ChequeNo & "") & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'")) & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ") Select @@Identity "
            'End Task:2381
            'Task:2382 Added Column Payee Title
            'Before against task:2470
            'Dim str As String = "Insert Into InvoiceBasedPayments(PaymentNo, PaymentDate, Remarks, PaymentAmount, PaymentMethod, PaymentAccountID, VendorCode, ChequeNo, ChequeDate, UserName, CostCenterId, PayeeTitle) " _
            '& " Values (N'" & objMod.PaymentNo & "', N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference.ToString & "', " & objMod.NetPayment & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.VendorCode & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", "" & objMod.ChequeNo & "") & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'")) & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ", " & IIf(objMod.PayeeTitle.ToString = "", "NULL", "N'" & objMod.PayeeTitle.Replace("'", "''") & "'") & ") Select @@Identity "
            'End Task:2382
            ' Dim str As String = "Insert Into InvoiceBasedPayments(PaymentNo, PaymentDate, Remarks, PaymentAmount, PaymentMethod, PaymentAccountID, VendorCode, ChequeNo, ChequeDate, UserName, CostCenterId, PayeeTitle) " _
            '& " Values (N'" & objMod.PaymentNo & "', N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference.ToString & "', " & objMod.NetPayment & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.VendorCode & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", "" & objMod.ChequeNo & "") & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'")) & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ", " & IIf(objMod.PayeeTitle.ToString = "", "NULL", "N'" & objMod.PayeeTitle.Replace("'", "''") & "'") & ") Select @@Identity "
            ' objMod.PaymentID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            ' 'Call ADD Detail Function 
            'Task No 2537 Updating THe Query For Checkbox Post Updation 
            Dim str As String = "Insert Into InvoiceBasedPayments(PaymentNo, PaymentDate, Remarks, PaymentAmount, PaymentMethod, PaymentAccountID, VendorCode, ChequeNo, ChequeDate, UserName, CostCenterId, PayeeTitle,Post,ChequeLayoutIndex,DueDate) " _
          & " Values (N'" & objMod.PaymentNo & "', N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference.ToString & "', " & objMod.NetPayment & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.VendorCode & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", "" & objMod.ChequeNo & "") & ", " & IIf(objMod.ChequeNo.ToString = "", "NULL", IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'")) & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ", " & IIf(objMod.PayeeTitle.ToString = "", "NULL", "N'" & objMod.PayeeTitle.Replace("'", "''") & "'") & " ," & IIf(objMod.Post = True, 1, 0) & ", " & objMod.ChequeLayoutIndex & ", " & IIf(objMod.DueDate = "#12:00:00 AM#", "Null", "N'" & objMod.DueDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ") Select @@Identity "
            objMod.PaymentID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            'Call ADD Detail Function 
            Call Me.AddDt(objMod.PaymentID, objMod, trans)
            'Call Add Function From Voucher DAL Class
            VoucherDAL = New VouchersDAL
            VoucherDAL.Add(objMod.VoucherMaster, trans)
            'Activity Log Detail Below
            objMod.ActivityLog.ActivityName = "Save"
            objMod.ActivityLog.RecordType = "Accounts"
            objMod.ActivityLog.RefNo = objMod.PaymentNo
            'Call ActivityBuldFunction in UtilityDAL 
            Call UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As SqlClient.SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal objMod As InvoicesBasedPaymentMaster) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'Before against task:2381
            'str = "Update InvoiceBasedPayments Set PaymentNo=N'" & objMod.PVNo & "', PaymentDate=N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'" & objMod.Reference.ToString.Replace("'", "''") & "', PaymentAmount=" & objMod.NetPayment & ", PaymentMethod=" & objMod.PaymentMethod & ", PaymentAccountId=" & objMod.PaymentAccountId & ", VendorCode=" & objMod.VendorCode & ", ChequeNo=" & IIf(objMod.ChequeNo = "", "NULL", "N'" & objMod.ChequeNo.Replace("'", "''") & "'") & ", ChequeDate=" & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", CostCenterId=" & objMod.ProjectId & " WHERE PaymentId=" & objMod.PaymentID & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Before against task:2382
            'Task:2381 Change Apostrophe at Cheque No And Remarks
            'str = "Update InvoiceBasedPayments Set PaymentNo=N'" & objMod.PVNo & "', PaymentDate=N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'" & objMod.Reference.ToString & "', PaymentAmount=" & objMod.NetPayment & ", PaymentMethod=" & objMod.PaymentMethod & ", PaymentAccountId=" & objMod.PaymentAccountId & ", VendorCode=" & objMod.VendorCode & ", ChequeNo=" & IIf(objMod.ChequeNo = "", "NULL", "" & objMod.ChequeNo & "") & ", ChequeDate=" & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", CostCenterId=" & objMod.ProjectId & " WHERE PaymentId=" & objMod.PaymentID & ""
            'End Task:2381
            'Task:2382 Added Column Payee Title
            'Task 2537 Updating The query of Update 
            'str = "Update InvoiceBasedPayments Set PaymentNo=N'" & objMod.PVNo & "', PaymentDate=N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'" & objMod.Reference.ToString & "', PaymentAmount=" & objMod.NetPayment & ", PaymentMethod=" & objMod.PaymentMethod & ", PaymentAccountId=" & objMod.PaymentAccountId & ", VendorCode=" & objMod.VendorCode & ", ChequeNo=" & IIf(objMod.ChequeNo = "", "NULL", "" & objMod.ChequeNo & "") & ", ChequeDate=" & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", CostCenterId=" & objMod.ProjectId & ", PayeeTitle=" & IIf(objMod.PayeeTitle.ToString = "", "NULL", "N'" & objMod.PayeeTitle.Replace("'", "''") & "'") & " WHERE PaymentId=" & objMod.PaymentID & ""
            str = "Update InvoiceBasedPayments Set PaymentNo=N'" & objMod.PVNo & "', PaymentDate=N'" & objMod.PaymentDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'" & objMod.Reference.ToString & "', PaymentAmount=" & objMod.NetPayment & ", PaymentMethod=" & objMod.PaymentMethod & ", PaymentAccountId=" & objMod.PaymentAccountId & ", VendorCode=" & objMod.VendorCode & ", ChequeNo=" & IIf(objMod.ChequeNo = "", "NULL", "" & objMod.ChequeNo & "") & ", ChequeDate=" & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", CostCenterId=" & objMod.ProjectId & ", PayeeTitle=" & IIf(objMod.PayeeTitle.ToString = "", "NULL", "N'" & objMod.PayeeTitle.Replace("'", "''") & "'") & ", Post =" & IIf(objMod.Post = True, 1, 0) & ",ChequeLayoutIndex=" & objMod.ChequeLayoutIndex & ",DueDate=" & IIf(objMod.DueDate = "#12:00:00 AM#", "NULL", "N'" & objMod.DueDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & " WHERE PaymentId=" & objMod.PaymentID & ""
            'End Task:2382
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete From Invoice Base Payment Detail
            Dim strDelInvoiceDetail As String
            strDelInvoiceDetail = "Delete From InvoiceBasedPaymentsDetail WHERE PaymentId=" & objMod.PaymentID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDelInvoiceDetail)
            'Call Add Detail Function 
            Call AddDt(objMod.PaymentID, objMod, trans)
            'Call Add Function From Voucher DAL Class
            VoucherDAL = New VouchersDAL
            VoucherDAL.Update(objMod.VoucherMaster, trans)
            'Activity Log Detail Below 
            objMod.ActivityLog.ActivityName = "Update"
            objMod.ActivityLog.RecordType = "Accounts"
            objMod.ActivityLog.RefNo = objMod.PaymentNo
            'Call ActivityBuldFunction in UtilityDAL 
            Call UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)

            trans.Commit()
            Return True
        Catch ex As SqlClient.SqlException
            trans.Rollback()
            Throw ex


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal objMod As InvoicesBasedPaymentMaster) As Boolean
        Dim VoucherId As Integer
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String
            Dim Dt As New DataTable
            Dim Da As SqlClient.SqlDataAdapter

            strSQL = "Select PaymentId From InvoiceBasedPayments WHERE PaymentNo=N'" & objMod.PVNo & "'"
            Da = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            Da.Fill(Dt)
            If Dt.Rows.Count > 0 Then
                VoucherId = Dt.Rows(0).Item(0).ToString 'Get Voucher Id from Invoice Based Receipts
            End If
            'Delete From Invoice Based Payment Detail 
            Dim str As String = String.Empty
            str = "Delete From InvoiceBasedPaymentsDetail WHERE PaymentId=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete From Invoice Based Payment Master
            Dim str1 As String = String.Empty
            str1 = "Delete From InvoiceBasedPayments WHERE PaymentId=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str1)
            'Call Delete Function From Voucher Dal
            VoucherDAL = New VouchersDAL
            Call VoucherDAL.Delete(objMod.VoucherMaster, trans)
            'Activit Log Below 
            objMod.ActivityLog.ActivityName = "Delete"
            objMod.ActivityLog.RecordType = "Accounts"
            objMod.ActivityLog.RefNo = objMod.PaymentNo
            'Call ActivityBuldFunction in UtilityDAL 
            Call UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As SqlClient.SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function AddDt(ByVal MasterID As Integer, ByVal objModel As InvoicesBasedPaymentMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim ObjMods As List(Of InvoicesBasedPaymentDetail) = objModel.InvoiceBasedPaymentDetail
        Try
            Dim str As String = String.Empty
            For Each ObjMod As InvoicesBasedPaymentDetail In ObjMods
                ''Before against task:2370
                'str = "Insert INTO InvoiceBasedPaymentsDetail(PaymentId, InvoiceId, InvoiceNo, InvoiceAmount, PaymentAmount, BalanceAmount, Comments, Gst_Percentage, Vendor_Invoice_No)" _
                ' & " Values(" & MasterID & ", " & ObjMod.InvoiceId & ", N'" & ObjMod.InvoiceNo & "', " & ObjMod.InvoiceAmount & ", " & ObjMod.PaymentAmount & ", " & ObjMod.InvoiceBalance & ", N'" & ObjMod.Remarks.Trim.Replace("'", "''") & "', " & ObjMod.Gst_Percentage & ", N'" & ObjMod.Vendor_Invoice_No & "')"
                'Task:2370 Added Columns SalesTaxAmount, OtherTaxAmount, 
                'Before against Task:2470
                'str = "Insert INTO InvoiceBasedPaymentsDetail(PaymentId, InvoiceId, InvoiceNo, InvoiceAmount, PaymentAmount, BalanceAmount, Comments, Gst_Percentage, Vendor_Invoice_No, SalesTaxAmount,OtherTaxAmount, OtherTaxAccountId)" _
                ' & " Values(" & MasterID & ", " & ObjMod.InvoiceId & ", N'" & ObjMod.InvoiceNo & "', " & ObjMod.InvoiceAmount & ", " & ObjMod.PaymentAmount & ", " & ObjMod.InvoiceBalance & ", N'" & ObjMod.Remarks.Trim.Replace("'", "''") & "', " & ObjMod.Gst_Percentage & ", N'" & ObjMod.Vendor_Invoice_No & "', " & ObjMod.SalesTaxAmount & ", " & ObjMod.OtherTaxAmount & ", " & ObjMod.OtherTaxAccountId & ")"
                'Task:2470 Added Column Description
                'str = "Insert INTO InvoiceBasedPaymentsDetail(PaymentId, InvoiceId, InvoiceNo, InvoiceAmount, PaymentAmount, BalanceAmount, Comments, Gst_Percentage, Vendor_Invoice_No, SalesTaxAmount,OtherTaxAmount, OtherTaxAccountId, Description,OtherPayment)" _
                '& " Values(" & MasterID & ", " & ObjMod.InvoiceId & ", N'" & ObjMod.InvoiceNo & "', " & ObjMod.InvoiceAmount & ", " & ObjMod.PaymentAmount & ", " & ObjMod.InvoiceBalance & ", N'" & ObjMod.Remarks.Trim.Replace("'", "''") & "', " & ObjMod.Gst_Percentage & ", N'" & ObjMod.Vendor_Invoice_No & "', " & ObjMod.SalesTaxAmount & ", " & ObjMod.OtherTaxAmount & ", " & ObjMod.OtherTaxAccountId & ", N'" & ObjMod.Description.Replace("'", "''") & "'," & ObjMod.OtherPayment & ")"
                'End Task:2470

                str = "Insert INTO InvoiceBasedPaymentsDetail(PaymentId, InvoiceId, InvoiceNo, InvoiceAmount, PaymentAmount, BalanceAmount, Comments, Gst_Percentage, Vendor_Invoice_No, SalesTaxAmount,OtherTaxAmount, OtherTaxAccountId, Description,OtherPayment,Invoice_Tax,CostCenterId)" _
               & " Values(" & MasterID & ", " & ObjMod.InvoiceId & ", N'" & ObjMod.InvoiceNo & "', " & ObjMod.InvoiceAmount & ", " & ObjMod.PaymentAmount & ", " & ObjMod.InvoiceBalance & ", N'" & ObjMod.Remarks.Trim.Replace("'", "''") & "', " & ObjMod.Gst_Percentage & ", N'" & ObjMod.Vendor_Invoice_No & "', " & ObjMod.SalesTaxAmount & ", " & ObjMod.OtherTaxAmount & ", " & ObjMod.OtherTaxAccountId & ", N'" & ObjMod.Description.Replace("'", "''") & "'," & ObjMod.OtherPayment & "," & Val(ObjMod.InvoiceTax) & " , " & ObjMod.CostCenter & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                ''End Task:2370
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Function
    Public Function GetAllRecord(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim ClosingDate As DateTime = Convert.ToDateTime(UtilityDAL.GetConfigValue("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("PreviouseRecordShow").ToString)
            Dim strSQL As String = String.Empty
            'Before against task:2382
            ' strSQL = "SELECT " & IIf(Condition = "ALL", "", "Top 50") & " InvoiceBasedPayments.PaymentId, InvoiceBasedPayments.PaymentNo, InvoiceBasedPayments.PaymentDate, InvoiceBasedPayments.VendorCode, vwCOADetail.detail_title as [VendorName], IsNull(InvoiceBasedPayments.PaymentAmount,0) As PaymentAmount, InvoiceBasedPayments.Remarks, InvoiceBasedPayments.ChequeNo, InvoiceBasedPayments.ChequeDate, InvoiceBasedPayments.PaymentMethod, InvoiceBasedPayments.PaymentAccountId, IsNull(InvoiceBasedPayments.CostCenterId,0) as CostCenterId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            '& " FROM InvoiceBasedPayments LEFT OUTER JOIN vwCOADetail ON InvoiceBasedPayments.VendorCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InvoiceBasedPayments.PaymentNo  " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, InvoiceBasedPayments.PaymentDate,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " ORDER BY 1 Desc"
            'Task:2382 Added Column Payee Title
            ' strSQL = "SELECT " & IIf(Condition = "ALL", "", "Top 50") & " InvoiceBasedPayments.PaymentId, InvoiceBasedPayments.PaymentNo, InvoiceBasedPayments.PaymentDate, InvoiceBasedPayments.VendorCode, vwCOADetail.detail_title as [VendorName], IsNull(InvoiceBasedPayments.PaymentAmount,0) As PaymentAmount, InvoiceBasedPayments.Remarks, InvoiceBasedPayments.ChequeNo, InvoiceBasedPayments.ChequeDate, InvoiceBasedPayments.PaymentMethod, InvoiceBasedPayments.PaymentAccountId, IsNull(InvoiceBasedPayments.CostCenterId,0) as CostCenterId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            '& " FROM InvoiceBasedPayments LEFT OUTER JOIN vwCOADetail ON InvoiceBasedPayments.VendorCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InvoiceBasedPayments.PaymentNo  " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, InvoiceBasedPayments.PaymentDate,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " ORDER BY 1 Desc"
            'Task:2382 Added Column Payee Title
            'Task Noi 2537 Append New Field Of Post In Query 
            '26-4-2014 TASK:M35 Imran Ali Show All Record 
            'strSQL = "SELECT " & IIf(Condition = "All", "", "Top 50") & " InvoiceBasedPayments.PaymentId, InvoiceBasedPayments.PaymentNo, InvoiceBasedPayments.PaymentDate, InvoiceBasedPayments.VendorCode, vwCOADetail.detail_title as [VendorName], IsNull(InvoiceBasedPayments.PaymentAmount,0) As PaymentAmount, InvoiceBasedPayments.Remarks, InvoiceBasedPayments.ChequeNo, InvoiceBasedPayments.ChequeDate, InvoiceBasedPayments.PaymentMethod, InvoiceBasedPayments.PaymentAccountId, IsNull(InvoiceBasedPayments.CostCenterId,0) as CostCenterId, InvoiceBasedPayments.PayeeTitle, IsNull(InvoiceBasedPayments.Post,0) as Post, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(V.Voucher_Id,0) as VId " _
            '& " FROM InvoiceBasedPayments LEFT OUTER JOIN tblVoucher V on V.Voucher_No=InvoiceBasedPayments.PaymentNo LEFT OUTER JOIN vwCOADetail ON InvoiceBasedPayments.VendorCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InvoiceBasedPayments.PaymentNo  " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, InvoiceBasedPayments.PaymentDate,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " ORDER BY 1 Desc"
            'End Task:2382
            strSQL = "SELECT " & IIf(Condition = "All", "", "Top 50") & " InvoiceBasedPayments.PaymentId, InvoiceBasedPayments.PaymentNo, InvoiceBasedPayments.PaymentDate, InvoiceBasedPayments.VendorCode, vwCOADetail.detail_title as [VendorName], IsNull(InvoiceBasedPayments.PaymentAmount,0) As PaymentAmount, InvoiceBasedPayments.Remarks, InvoiceBasedPayments.ChequeNo, InvoiceBasedPayments.ChequeDate, InvoiceBasedPayments.PaymentMethod, InvoiceBasedPayments.PaymentAccountId, IsNull(InvoiceBasedPayments.CostCenterId,0) as CostCenterId,companyDefTable.CompanyName as [Company Name], InvoiceBasedPayments.PayeeTitle, IsNull(InvoiceBasedPayments.Post,0) as Post, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(V.Voucher_Id,0) as VId, IsNull(InvoiceBasedPayments.ChequeLayoutIndex,0) as ChequeLayoutIndex ,IsNull(InvoiceBasedPayments.DueDate,0) as DueDate  " _
            & " FROM InvoiceBasedPayments LEFT OUTER JOIN tblVoucher V on V.Voucher_No=InvoiceBasedPayments.PaymentNo LEFT OUTER JOIN vwCOADetail ON InvoiceBasedPayments.VendorCode = vwCOADetail.coa_detail_id left outer join companyDefTable On v.location_id = companyDefTable.CompanyId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InvoiceBasedPayments.PaymentNo  " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, InvoiceBasedPayments.PaymentDate,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " ORDER BY 1 Desc"
            Return UtilityDAL.GetDataTable(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetVoucherNo(VType As String, VoucherDate As DateTime, Optional CompanyId As Integer = 0, Optional trans As SqlClient.SqlTransaction = Nothing) As String
        If UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Yearly" Then
            Return UtilityDAL.GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(VoucherDate.Year, 2) + "-", "tblVoucher", "voucher_no", trans)
        Else
            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL, trans)
            If Not dr Is Nothing Then
                If dr("config_Value") = "Monthly" Then
                    Return UtilityDAL.GetNextDocNo(VType & "-" & Format(VoucherDate, "yy") & VoucherDate.Month.ToString("00"), 4, "tblVoucher", "voucher_no", trans)
                Else
                    Return UtilityDAL.GetNextDocNo(VType, 6, "tblVoucher", "voucher_no", trans)
                End If
            Else
                Return UtilityDAL.GetNextDocNo(VType, 6, "tblVoucher", "voucher_no", trans)
            End If
            Return ""
        End If
    End Function
End Class

