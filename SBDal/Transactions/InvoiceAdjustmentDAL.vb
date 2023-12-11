'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'7-9-2015 Task#79152 Imran Ali Added Voucher Information In GetAllRecord Method.
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility

Public Class InvoiceAdjustmentDAL

    Enum DocId
        DcoDate
        InvoiceId
        InvoiceType
        VoucherDetailId
        coa_detail_Id
        AdjustmentAmount
        Remarks
        UserName
        EntryDate
    End Enum

    Public Function Add(ByVal objInvoice As InvoiceAdjustmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            objInvoice.DocNo = UtilityDAL.GetNextDocNo("Adj-" & objInvoice.DocDate.ToString("yy"), 6, "InvoiceAdjustmentTable", "DocNo", trans)
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO InvoiceAdjustmentTable(DocNo,DocDate,InvoiceId,InvoiceType,VoucherDetailId,coa_detail_Id,AdjustmentAmount,Remarks,UserName,EntryDate) " _
            & " VALUES(N'" & objInvoice.DocNo.Replace("'", "''") & "',Convert(DateTime, N'" & objInvoice.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102), " & objInvoice.InvoiceId & ", N'" & objInvoice.InvoiceType.Replace("'", "''") & "', " & objInvoice.VoucherDetailId & ", " & objInvoice.coa_detail_id & "," & objInvoice.AdjustmentAmount & ",N'" & objInvoice.Remarks.Replace("'", "''") & "',  N'" & objInvoice.UserName.Replace("'", "''") & "', Convert(DateTime, N'" & objInvoice.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102) )"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Modify(ByVal objInvoice As InvoiceAdjustmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "UPDATE InvoiceAdjustmentTable SET DocNo=N'" & objInvoice.DocNo.Replace("'", "''") & "', DocDate=Convert(DateTime, N'" & objInvoice.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102),InvoiceId=" & objInvoice.InvoiceId & ",InvoiceType=N'" & objInvoice.InvoiceType.Replace("'", "''") & "',VoucherDetailId=" & objInvoice.VoucherDetailId & ",coa_detail_Id=" & objInvoice.coa_detail_id & ",AdjustmentAmount=" & objInvoice.AdjustmentAmount & ",Remarks=N'" & objInvoice.Remarks.Replace("'", "''") & "',UserName=N'" & objInvoice.UserName.Replace("'", "''") & "',EntryDate=Convert(DateTime, N'" & objInvoice.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102) WHERE DocId=" & objInvoice.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Remove(ByVal objInvoice As InvoiceAdjustmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From InvoiceAdjustmentTable WHERE DocId=" & objInvoice.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetDallRecords(Optional ByVal Condition As String = "")
        Try
            Dim strSQL As String = String.Empty
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'strSQL = "SELECT " & IIf(Condition = "All", "", "Top 50") & " InvAdj.VoucherDetailId, InvAdj.InvoiceId, InvAdj.DocId, V.voucher_id, InvAdj.DocNo, InvAdj.DocDate, S_L.SalesNo, V.voucher_no, V.voucher_date, " _
            '    & " dbo.vwCOADetail.sub_sub_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, InvAdj.AdjustmentAmount, InvAdj.InvoiceType " _
            '    & " FROM dbo.tblVoucherDetail AS V_D INNER JOIN dbo.tblVoucher AS V ON V_D.voucher_id = V.voucher_id INNER JOIN dbo.InvoiceAdjustmentTable AS InvAdj ON V_D.coa_detail_id = InvAdj.coa_detail_Id AND V_D.voucher_detail_id = InvAdj.VoucherDetailId INNER JOIN " _
            '    & " dbo.vwCOADetail ON V_D.coa_detail_id = dbo.vwCOADetail.coa_detail_id INNER JOIN (SELECT     SalesId, SalesNo, SalesDate, 'Sales' AS InvoiceType FROM dbo.SalesMasterTable UNION SELECT ReceivingId, ReceivingNo, ReceivingDate, 'Purchase' AS InvoiceType " _
            '    & " FROM dbo.ReceivingMasterTable) AS S_L ON S_L.SalesId = InvAdj.InvoiceId AND S_L.InvoiceType = InvAdj.InvoiceType " _
            '    & " ORDER BY InvAdj.DocNo DESC "
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'strSQL = "SELECT " & IIf(Condition = "All", "", "Top 50") & " InvAdj.VoucherDetailId, InvAdj.InvoiceId, InvAdj.DocId, V.voucher_id, InvAdj.DocNo, InvAdj.DocDate, S_L.SalesNo, V.voucher_no, V.voucher_date, " _
            '  & " dbo.vwCOADetail.sub_sub_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, InvAdj.AdjustmentAmount, InvAdj.InvoiceType,InvAdj.username as 'User Name' " _
            '  & " FROM dbo.tblVoucherDetail AS V_D INNER JOIN dbo.tblVoucher AS V ON V_D.voucher_id = V.voucher_id INNER JOIN dbo.InvoiceAdjustmentTable AS InvAdj ON V_D.coa_detail_id = InvAdj.coa_detail_Id AND V_D.voucher_detail_id = InvAdj.VoucherDetailId INNER JOIN " _
            '  & " dbo.vwCOADetail ON V_D.coa_detail_id = dbo.vwCOADetail.coa_detail_id INNER JOIN (SELECT     SalesId, SalesNo, SalesDate, 'Sales' AS InvoiceType FROM dbo.SalesMasterTable UNION SELECT ReceivingId, ReceivingNo, ReceivingDate, 'Purchase' AS InvoiceType " _
            '  & " FROM dbo.ReceivingMasterTable) AS S_L ON S_L.SalesId = InvAdj.InvoiceId AND S_L.InvoiceType = InvAdj.InvoiceType " _
            '  & " ORDER BY InvAdj.DocNo DESC "
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms


            'Task#79152 Added Voucher Information In Query
            strSQL = "SELECT " & IIf(Condition = "All", "", "Top 50") & " InvAdj.VoucherDetailId, InvAdj.InvoiceId, InvAdj.DocId, V.voucher_id, InvAdj.DocNo, InvAdj.DocDate, S_L.SalesNo, V.voucher_no, V.voucher_date, " _
         & " dbo.vwCOADetail.sub_sub_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, InvAdj.AdjustmentAmount, InvAdj.InvoiceType,InvAdj.username as 'User Name' " _
         & " FROM dbo.tblVoucherDetail AS V_D INNER JOIN dbo.tblVoucher AS V ON V_D.voucher_id = V.voucher_id INNER JOIN dbo.InvoiceAdjustmentTable AS InvAdj ON V_D.coa_detail_id = InvAdj.coa_detail_Id AND V_D.voucher_detail_id = InvAdj.VoucherDetailId INNER JOIN " _
         & " dbo.vwCOADetail ON V_D.coa_detail_id = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN (SELECT  SalesId, SalesNo, SalesDate, 'Sales' AS InvoiceType FROM dbo.SalesMasterTable UNION SELECT ReceivingId, ReceivingNo, ReceivingDate, 'Purchase' AS InvoiceType " _
         & " FROM dbo.ReceivingMasterTable Union All Select Voucher_Detail_id, Voucher_No,Voucher_Date,'Voucher' as InvoiceType From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id WHERE Voucher_Detail_Id In(Select InvoiceId From InvoiceAdjustmentTable)) AS S_L ON S_L.SalesId = InvAdj.InvoiceId AND S_L.InvoiceType = InvAdj.InvoiceType " _
         & " ORDER BY InvAdj.DocNo DESC "
            'End Task Task#79152

            Dim objDt As New DataTable
            objDt = UtilityDAL.GetDataTable(strSQL, Nothing)
            Return objDt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
