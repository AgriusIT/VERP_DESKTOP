''7-Jan-2014 Task:2370 Imran Ali          Sale and purchase invoice wise aging report 
''13-Jan-2014   Task:2375        Imran Ali        Covnerter Problems And Development
''16-Jan-2014     TASK:2381        Imran Ali         Problem In Invoice Based Payment 
''07-Mar-2014 Task:2470 Add month in invoice base payment narration
'Task 2537 Update The Save And Update Function In This File 
'26-4-2014 TASK:M35 Imran Ali Show All Record 
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Net
Public Class InvoicesBasedReceiptDAL

    Dim VoucherDAl As VouchersDAL
    Dim VouchersDetail As VouchersDetail
    Public Function Add(ByVal objMod As InvoicesBasedReceiptMaster) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            'Before against task:2375
            'Dim str As String = "Insert Into InvoiceBasedReceipts(ReceiptNo, ReceiptDate, Remarks, ReceiptAmount, PaymentMethod, PaymentAccountID, CustomerCode, ChequeNo, ChequeDate, UserName, CostCenterId) " _
            '& " Values (N'" & objMod.ReceiptNo & "', N'" & objMod.ReceiptDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference & "', " & objMod.NetReceipt & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.CustomerCode & ", " & IIf(objMod.ChequeNo = "", "NULL", "N'" & objMod.ChequeNo.Replace("'", "''") & "'") & ", " & IIf(objMod.ChequeDate = Nothing, "NULL", "N'" & CDate(IIf(objMod.ChequeDate = Nothing, Now, objMod.ChequeDate)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ") Select @@Identity "
            'Before against task:2381
            'Task:2375 Cheque DataType Cheque Date
            ' Dim str As String = "Insert Into InvoiceBasedReceipts(ReceiptNo, ReceiptDate, Remarks, ReceiptAmount, PaymentMethod, PaymentAccountID, CustomerCode, ChequeNo, ChequeDate, UserName, CostCenterId) " _
            '& " Values (N'" & objMod.ReceiptNo & "', N'" & objMod.ReceiptDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference & "', " & objMod.NetReceipt & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.CustomerCode & ", " & IIf(objMod.ChequeNo = "", "NULL", "N'" & objMod.ChequeNo.Replace("'", "''") & "'") & ", " & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ") Select @@Identity "
            'Task:2381 Change Apostrophe At Cheque No Field  And Remarks
            'Dim str As String = "Insert Into InvoiceBasedReceipts(ReceiptNo, ReceiptDate, Remarks, ReceiptAmount, PaymentMethod, PaymentAccountID, CustomerCode, ChequeNo, ChequeDate, UserName, CostCenterId) " _
            '& " Values (N'" & objMod.ReceiptNo & "', N'" & objMod.ReceiptDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference & "', " & objMod.NetReceipt & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.CustomerCode & ", " & IIf(objMod.ChequeNo = "", "NULL", "" & objMod.ChequeNo & "") & ", " & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & objMod.UserName & "', " & objMod.ProjectId & ") Select @@Identity "
            ''End Task:2381
            'Task No 2537 Apend AND Update the Query of Saving the data in to table

            ''objMod.ReceiptNo = GetVoucherNo(objMod.ReceiptNo.Substring(0, objMod.ReceiptNo.IndexOf("-")), objMod.ReceiptDate, objMod.CompanyName, trans)
            objMod.RVNo = objMod.ReceiptNo
            ''Start Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports
            'objMod.VoucherMaster.VNo = objMod.ReceiptNo
            'objMod.VoucherMaster.VoucherCode = objMod.ReceiptNo
            'objMod.VoucherMaster.VoucherNo = objMod.ReceiptNo
            ''End Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports

            Dim str As String = "Insert Into InvoiceBasedReceipts(ReceiptNo, ReceiptDate, Remarks, ReceiptAmount, PaymentMethod, PaymentAccountID, CustomerCode, ChequeNo, ChequeDate, UserName, CostCenterId,Post) " _
          & " Values (N'" & objMod.ReceiptNo & "', N'" & objMod.ReceiptDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.Reference & "', " & objMod.NetReceipt & ", " & objMod.PaymentMethod & ", " & objMod.PaymentAccountId & ", " & objMod.CustomerCode & ", " & IIf(objMod.ChequeNo = "", "NULL", "" & objMod.ChequeNo & "") & ", " & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & objMod.UserName & "', " & objMod.ProjectId & " ," & IIf(objMod.Post = True, 1, 0) & ") Select @@Identity "
            'End Task:2381
            objMod.ReceiptID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            'Call ADD Detail Function
            Call Me.AddDt(objMod.ReceiptID, objMod, trans)
            'Call Add Function From Voucher DAL Class
            '' Start Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports
            'VoucherDAl = New VouchersDAL
            'Call VoucherDAl.Add(objMod.VoucherMaster, trans)

            'Activity Log Detail Below
            'objMod.ActivityLog.ActivityName = "Save"
            'objMod.ActivityLog.RecordType = "Accounts"
            'objMod.ActivityLog.RefNo = objMod.ReceiptNo
            'Call ActivityBuldFunction in UtilityDAL 
            'Call UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)
            '' End Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports
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
    Public Function Update(ByVal objMod As InvoicesBasedReceiptMaster) As Boolean

        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = String.Empty
            'Before against task:2375
            'str = "Update InvoiceBasedReceipts Set ReceiptNo=N'" & objMod.RVNo & "', ReceiptDate=N'" & objMod.ReceiptDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'" & objMod.Reference & "',  ReceiptAmount=" & objMod.NetReceipt & ",  PaymentMethod=" & objMod.PaymentMethod & ", PaymentAccountId=" & objMod.PaymentAccountId & ", CustomerCode=" & objMod.CustomerCode & ", ChequeNo=" & IIf(objMod.ChequeNo = "", "NULL", "N'" & objMod.ChequeNo.Replace("'", "''") & "'") & ", ChequeDate=" & IIf(objMod.ChequeDate = Nothing, "NULL", "N'" & CDate(IIf(objMod.ChequeDate = Nothing, Now, objMod.ChequeDate)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", CostCenterId=" & objMod.ProjectId & " WHERE ReceiptId=" & objMod.ReceiptID & ""
            'Before against task:2381
            'Task:2375 Cheque DataType Cheque Date
            'str = "Update InvoiceBasedReceipts Set ReceiptNo=N'" & objMod.RVNo & "', ReceiptDate=N'" & objMod.ReceiptDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'" & objMod.Reference & "',  ReceiptAmount=" & objMod.NetReceipt & ",  PaymentMethod=" & objMod.PaymentMethod & ", PaymentAccountId=" & objMod.PaymentAccountId & ", CustomerCode=" & objMod.CustomerCode & ", ChequeNo=" & IIf(objMod.ChequeNo = "", "NULL", "N'" & objMod.ChequeNo.Replace("'", "''") & "'") & ", ChequeDate=" & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", CostCenterId=" & objMod.ProjectId & " WHERE ReceiptId=" & objMod.ReceiptID & ""
            'Task:2381 Change Apostrophe At Cheque No.
            'Task No 2537 Update the query of Update Query 
            'Call UtilityDAL.CreateDuplicationVoucher(objMod.VoucherMaster.VoucherId, 1, objMod.VoucherMaster.UserName, trans)
            str = "Update InvoiceBasedReceipts Set ReceiptNo=N'" & objMod.RVNo & "', ReceiptDate=N'" & objMod.ReceiptDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'" & objMod.Reference & "',  ReceiptAmount=" & objMod.NetReceipt & ",  PaymentMethod=" & objMod.PaymentMethod & ", PaymentAccountId=" & objMod.PaymentAccountId & ", CustomerCode=" & objMod.CustomerCode & ", ChequeNo=" & IIf(objMod.ChequeNo = "", "NULL", "" & objMod.ChequeNo & "") & ", ChequeDate=" & IIf(objMod.ChequeDate = "#12:00:00 AM#", "NULL", "N'" & objMod.ChequeDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", CostCenterId=" & objMod.ProjectId & ", Post =" & IIf(objMod.Post = True, 1, 0) & " WHERE ReceiptId=" & objMod.ReceiptID & ""
            'End Task:2381
            'End Task:2375
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Dim str1 As String = String.Empty
            'Delete From Invoice Base Receipt Detail
            Dim strDelInvoiceDetail As String
            strDelInvoiceDetail = "Delete From InvoiceBasedReceiptsDetails WHERE ReceiptId=" & objMod.ReceiptID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strDelInvoiceDetail)
            'Call Add Detail Function 
            Call AddDt(objMod.ReceiptID, objMod, trans)
            'Call Add Function From Voucher DAL Class
            ''Start Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports
            'VoucherDAl = New VouchersDAL
            'VoucherDAl.Update(objMod.VoucherMaster, trans)
            'Activity Log Detail Below 
            'objMod.ActivityLog.ActivityName = "Update"
            'objMod.ActivityLog.RecordType = "Accounts"
            'objMod.ActivityLog.RefNo = objMod.ReceiptNo
            'Call ActivityBuldFunction in UtilityDAL 
            'Call UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)
            ''End Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports
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
    Public Function Delete(ByVal objMod As InvoicesBasedReceiptMaster) As Boolean
        Dim VoucherId As Integer
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String
            Dim Dt As New DataTable
            Dim Da As SqlClient.SqlDataAdapter
            strSQL = "Select ReceiptId From InvoiceBasedReceipts WHERE ReceiptNo=N'" & objMod.RVNo & "'"
            Da = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            Da.Fill(Dt)
            If Dt.Rows.Count > 0 Then
                VoucherId = Dt.Rows(0).Item(0).ToString 'Get Voucher Id from Invoice Based Receipts
            End If
            'Delete From Invoice Based Receipt Detail 
            Dim str As String = String.Empty
            str = "Delete From InvoiceBasedReceiptsDetails WHERE ReceiptId=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete From Invoice Based Receipt Master
            Dim str1 As String
            str1 = "Delete From InvoiceBasedReceipts WHERE ReceiptId=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str1)
            'Call Delete Function From Voucher Dal
            ''Start Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports
            'VoucherDAl = New VouchersDAL
            'Call VoucherDAl.Delete(objMod.VoucherMaster, trans)
            ''Acitvity Log Below
            'objMod.ActivityLog.ActivityName = "Delete"
            'objMod.ActivityLog.RecordType = "Accounts"
            'objMod.ActivityLog.RefNo = objMod.ReceiptNo
            ''Call ActivityBuldFunction in UtilityDAL 
            'Call UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)
            ''End Waqar Commented these lines for entry in InvoiceBasedReceipts from Receipt Screen for Further Reporting like Account statement and Invoice Due/Not Due Reports
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
    Public Function AddDt(ByVal MasterID As Integer, ByVal objModel As InvoicesBasedReceiptMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim ObjMods As List(Of InvoicesBasedReceiptDetail) = objModel.InvoiceBasedReceiptDetail
        Try
            Dim str As String = String.Empty
            For Each ObjMod As InvoicesBasedReceiptDetail In ObjMods
                ''Before against task:2370
                'str = "Insert INTO InvoiceBasedReceiptsDetails(ReceiptId, InvoiceId, InvoiceNo, InvoiceAmount, ReceiptAmount, BalanceAmount, Comments, Gst_Percentage)" _
                ' & " Values(" & MasterID & ", " & ObjMod.InvoiceId & ", N'" & ObjMod.InvoiceNo & "', " & ObjMod.InvoiceAmount & ", " & ObjMod.ReceiptAmount & ", " & ObjMod.InvoiceBalance & ", N'" & ObjMod.Remarks.Trim.Replace("'", "''") & "', " & ObjMod.Gst_Percentage & ")"
                'Task:2370 Added Column SalesTaxAmount ,OtherTaxAmount, OtherTaxAccountId
                'str = "Insert INTO InvoiceBasedReceiptsDetails(ReceiptId, InvoiceId, InvoiceNo, InvoiceAmount, ReceiptAmount, BalanceAmount, Comments, Gst_Percentage,SalesTaxAmount, OtherTaxAmount, OtherTaxAccountId)" _
                '& " Values(" & MasterID & ", " & ObjMod.InvoiceId & ", N'" & ObjMod.InvoiceNo & "', " & ObjMod.InvoiceAmount & ", " & ObjMod.ReceiptAmount & ", " & ObjMod.InvoiceBalance & ", N'" & ObjMod.Remarks.Trim.Replace("'", "''") & "', " & ObjMod.Gst_Percentage & ", " & ObjMod.SalesTaxAmount & ", " & ObjMod.OtherTaxAmount & ", " & ObjMod.OtherTaxAccountId & ")"
                'Task:2470 Added Column Description
                str = "Insert INTO InvoiceBasedReceiptsDetails(ReceiptId, InvoiceId, InvoiceAmount, ReceiptAmount, BalanceAmount, Comments, Gst_Percentage,SalesTaxAmount, OtherTaxAmount, OtherTaxAccountId, CostCenterId)" _
               & " Values(" & MasterID & ", " & ObjMod.InvoiceId & ", " & ObjMod.InvoiceAmount & ", " & ObjMod.ReceiptAmount & ", " & ObjMod.InvoiceBalance & ", N'" & ObjMod.Remarks.Trim.Replace("'", "''") & "', " & ObjMod.Gst_Percentage & ", " & ObjMod.SalesTaxAmount & ", " & ObjMod.OtherTaxAmount & ", " & ObjMod.OtherTaxAccountId & ", " & ObjMod.CostCenter & ")"
                'End Task:2470
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'End Task:2370
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
            'strSQL = "SELECT " & IIf(Condition = "ALL", "", "Top 50") & " InvoiceBasedReceipts.ReceiptId, InvoiceBasedReceipts.ReceiptNo, InvoiceBasedReceipts.ReceiptDate, InvoiceBasedReceipts.CustomerCode, vwCOADetail.detail_title as [CustomerName], InvoiceBasedReceipts.ReceiptAmount, InvoiceBasedReceipts.Remarks, InvoiceBasedReceipts.ChequeNo, InvoiceBasedReceipts.ChequeDate, InvoiceBasedReceipts.PaymentMethod, InvoiceBasedReceipts.PaymentAccountId, IsNull(InvoiceBasedReceipts.CostCenterId,0) as CostCenterId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            '           & " FROM InvoiceBasedReceipts LEFT OUTER JOIN vwCOADetail ON InvoiceBasedReceipts.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InvoiceBasedReceipts.ReceiptNo  " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, InvoiceBasedReceipts.ReceiptDate,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " ORDER BY 1 Desc"
            'Task No 2537 Append New Field Of Post In Query 
            '26-4-2014 TASK:M35 Imran Ali Show All Record 
            strSQL = "SELECT " & IIf(Condition = "All", "", "Top 50") & " InvoiceBasedReceipts.ReceiptId, InvoiceBasedReceipts.ReceiptNo, InvoiceBasedReceipts.ReceiptDate, InvoiceBasedReceipts.CustomerCode, vwCOADetail.detail_title as [CustomerName], InvoiceBasedReceipts.ReceiptAmount, InvoiceBasedReceipts.Remarks, InvoiceBasedReceipts.ChequeNo, InvoiceBasedReceipts.ChequeDate, InvoiceBasedReceipts.PaymentMethod, InvoiceBasedReceipts.PaymentAccountId, IsNull(InvoiceBasedReceipts.CostCenterId,0) as CostCenterId,companyDefTable.CompanyName as [Company Name], IsNull(InvoiceBasedReceipts.Post,0) as Post, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Isnull(Voucher_Id,0) as VId " _
           & " FROM InvoiceBasedReceipts LEFT OUTER JOIN tblVoucher V on V.Voucher_No = InvoiceBasedReceipts.ReceiptNo LEFT OUTER JOIN vwCOADetail ON InvoiceBasedReceipts.CustomerCode = vwCOADetail.coa_detail_id left outer join companyDefTable On v.location_id = companyDefTable.CompanyId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InvoiceBasedReceipts.ReceiptNo  " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, InvoiceBasedReceipts.ReceiptDate,102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " ORDER BY 1 Desc"
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

