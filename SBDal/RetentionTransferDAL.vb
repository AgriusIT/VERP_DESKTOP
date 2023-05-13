Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class RetentionTransferDAL
    Dim VoucherId As Integer
    Dim RetentionMasterId As Integer
    Dim VID As Integer
    Dim VendorId As Integer
    Dim RetentionAccountId As Integer
    Dim CostCenterId As Integer
    Function Add(ByVal objModel As RetentionTransferBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            If Add(objModel, trans) Then
                trans.Commit()
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As RetentionTransferBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If objModel.CurrentPayables > 0 Then
                strSQL = "insert into  RetentionMasterTable (VoucherNo, VoucherDate, VendorId, POId, VendorConractId, CostCenterId, ArticleId, Remarks) values (N'" & objModel.VoucherNo.Replace("'", "''") & "', N'" & objModel.VoucherDate & "', N'" & objModel.VendorId & "', N'" & objModel.POId & "', N'" & objModel.VendorConractId & "', N'" & objModel.CostCenterId & "', N'" & objModel.ArticleId & "', N'" & objModel.Remarks.Replace("'", "''") & "') Select @@Identity"
                RetentionMasterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                'objModel.ActivityLog.ActivityName = "Save"
                'objModel.ActivityLog.RecordType = "Configuration"
                'objModel.ActivityLog.RefNo = ""
                'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)

                strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Reference) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.VoucherDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmRetentionTransfer',N'" & objModel.VoucherNo & "',N'TransferPer: " & objModel.TransferPer & ", CurrentPayables: " & objModel.CurrentPayables & ", " & objModel.Remarks & "') Select @@Identity"
                'strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'PS-18-00009',N'3/1/2018 3:07:07 PM',1,N'frmProPurchase',N'PS-18-00009',N'Item: '" & objModel.Title & " 'Price: '" & objModel.Price & " 'Remarks:'" & oobjModel.Remarks & "')' Select @@Identity"
                VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Else
                Return False
            End If
            RetentionAccountId = objModel.RetentionAccountId
            VendorId = objModel.VendorId
            CostCenterId = objModel.CostCenterId
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Function AddVoucher(ByVal objModel As RetentionTransferBE, trans As SqlTransaction) As Boolean
    '    Try
    '        Dim strSQL As String = String.Empty
    '        strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.VoucherDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmRetentionTransfer',N'" & objModel.VoucherNo & "',N'Remarks: " & objModel.Remarks & "') Select @@Identity"
    '        'strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'PS-18-00009',N'3/1/2018 3:07:07 PM',1,N'frmProPurchase',N'PS-18-00009',N'Item: '" & objModel.Title & " 'Price: '" & objModel.Price & " 'Remarks:'" & oobjModel.Remarks & "')' Select @@Identity"
    '        VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        AddVoucherDetail(objModel, trans)
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Function AddVoucherDetail(ByVal obj As RetentionTransferBE, trans As SqlTransaction) As Boolean
    '    Try
    '        Dim strSQL As String = String.Empty
    '        'Debit Buyer Account
    '        strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & VoucherId & ",1," & obj.VendorId & "," & objModel.Curr & ",0,N'Item: " & objModel.Title & " Price: " & objModel.Price & " Remarks: " & objModel.Remarks & "'," & objModel.Price & ",0,1,1,1,1," & objModel.Cost_CenterId & ")"

    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
    '        'Credit Property Sales Account
    '        strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & VoucherId & ",1," & objModel.VendorId & ",0," & objModel.VendorId & ",N' Remarks: " & objModel.Remarks & "',0," & objModel.Price & ",1,1,1,1," & objModel.Cost_CenterId & ")"


    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function AddRetentionDetail(ByVal list As List(Of RetentionTransferDetail)) As Boolean
        Dim strSQL As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As RetentionTransferDetail In list
                If obj.CurrentPayables > 0 Then
                    strSQL = "insert into  RetentionDetailTable (RetentionMasterId, ContractId, ContractValue, AmountPaid, BalanceAmount, RententionValue, RetentionPaid, TransferPer, RealizedAmount, CurrentPayables) values (N'" & RetentionMasterId & "',N'" & obj.ContractId & "', N'" & obj.ContractValue & "', N'" & obj.AmountPaid & "', N'" & obj.BalanceAmount & "', N'" & obj.RententionValue & "', N'" & obj.RetentionPaid & "', N'" & obj.TransferPer & "', N'" & obj.RealizedAmount & "', N'" & obj.CurrentPayables & "') Select @@Identity"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                    strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & RetentionAccountId & "," & obj.CurrentPayables & ",0," & obj.CurrentPayables & ",0,1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentPayables: " & obj.CurrentPayables & ", " & obj.Remarks & "')"

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    'Credit Property Sales Account
                    strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & VendorId & ",0," & obj.CurrentPayables & ",0," & obj.CurrentPayables & ",1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentPayables: " & obj.CurrentPayables & ", " & obj.Remarks & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    Return False
                End If
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function


    Function Update(ByVal objModel As RetentionTransferBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            'AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucher(ByVal objModel As RetentionTransferBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucher where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucherDetail(ByVal objModel As RetentionTransferBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucherDetail where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As RetentionTransferBE, trans As SqlTransaction) As Boolean
        Try
            RetentionMasterId = objModel.RetentionMasterId
            Dim str As String = "Select voucher_id from tblVoucher where voucher_no = '" & objModel.VoucherNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            VID = dt.Rows(0).Item("voucher_id")
            DeleteVoucherDetail(objModel, trans)
            DeleteVoucher(objModel, trans)
            Dim strSQL As String = String.Empty
            strSQL = "update RetentionMasterTable set VoucherNo= N'" & objModel.VoucherNo.Replace("'", "''") & "', VoucherDate= N'" & objModel.VoucherDate & "', VendorId= N'" & objModel.VendorId & "', POId= N'" & objModel.POId & "', VendorConractId= N'" & objModel.VendorConractId & "', CostCenterId= N'" & objModel.CostCenterId & "', ArticleId= N'" & objModel.ArticleId & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "' where RetentionMasterId=" & objModel.RetentionMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Reference) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.VoucherDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmRetentionTransfer',N'" & objModel.VoucherNo & "',N'TransferPer: " & objModel.TransferPer & ", CurrentPayables: " & objModel.CurrentPayables & ", " & objModel.Remarks & "') Select @@Identity"
            'strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'PS-18-00009',N'3/1/2018 3:07:07 PM',1,N'frmProPurchase',N'PS-18-00009',N'Item: '" & objModel.Title & " 'Price: '" & objModel.Price & " 'Remarks:'" & oobjModel.Remarks & "')' Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            RetentionAccountId = objModel.RetentionAccountId
            VendorId = objModel.VendorId
            CostCenterId = objModel.CostCenterId
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateRetentionDetail(ByVal list As List(Of RetentionTransferDetail)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As RetentionTransferDetail In list
                str = "If Exists(Select ISNULL(RetentionMasterId, 0) as RetentionMasterId From RetentionDetailTable Where RetentionMasterId=" & RetentionMasterId & ") update RetentionDetailTable set ContractId= N'" & obj.ContractId & "', ContractValue= N'" & obj.ContractValue & "', AmountPaid= N'" & obj.AmountPaid & "', BalanceAmount= N'" & obj.BalanceAmount & "', RententionValue= N'" & obj.RententionValue & "', RetentionPaid= N'" & obj.RetentionPaid & "', TransferPer= N'" & obj.TransferPer & "', RealizedAmount= N'" & obj.RealizedAmount & "', CurrentPayables= N'" & obj.CurrentPayables & "' where RetentionMasterId=" & RetentionMasterId & "" _
                 & " Else insert into  RetentionDetailTable (RetentionMasterId, ContractId, ContractValue, AmountPaid, BalanceAmount, RententionValue, RetentionPaid, TransferPer, RealizedAmount, CurrentPayables) values (N'" & RetentionMasterId & "',N'" & obj.ContractId & "', N'" & obj.ContractValue & "', N'" & obj.AmountPaid & "', N'" & obj.BalanceAmount & "', N'" & obj.RententionValue & "', N'" & obj.RetentionPaid & "', N'" & obj.TransferPer & "', N'" & obj.RealizedAmount & "', N'" & obj.CurrentPayables & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & RetentionAccountId & "," & obj.CurrentPayables & ",0," & obj.CurrentPayables & ",0,1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentPayables: " & obj.CurrentPayables & ", " & obj.Remarks & "')"

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Credit Property Sales Account
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & VendorId & ",0," & obj.CurrentPayables & ",0," & obj.CurrentPayables & ",1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentPayables: " & obj.CurrentPayables & ", " & obj.Remarks & "')"


                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Function Delete(ByVal objModel As RetentionTransferBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As RetentionTransferBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from RetentionMasterTable  where RetentionMasterId= " & objModel.RetentionMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from RetentionDetailTable  where RetentionMasterId= " & objModel.RetentionMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str As String
            str = "Select Voucher_id from tblVoucher where Voucher_No = '" & objModel.VoucherNo & "'"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            strSQL = "Delete from tblVoucherDetail where Voucher_Id = '" & VoucherId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete From tblVoucher where Voucher_Id = '" & VoucherId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " SELECT RetentionMasterTable.RetentionMasterId, RetentionMasterTable.VoucherNo, RetentionMasterTable.VoucherDate, RetentionMasterTable.VendorId, RetentionMasterTable.POId, RetentionMasterTable.VendorConractId, RetentionMasterTable.CostCenterId, RetentionMasterTable.ArticleId, RetentionMasterTable.Remarks, vwCOADetail.detail_title as Vendor, tblVendorContractMaster.DocNo,  ArticleDefView.ArticleDescription as Item, tblDefCostCenter.Name as CostCenter, PurchaseOrderMasterTable.PurchaseOrderNo FROM RetentionMasterTable LEFT OUTER JOIN PurchaseOrderMasterTable ON RetentionMasterTable.POId = PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN tblDefCostCenter ON RetentionMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN ArticleDefView ON RetentionMasterTable.ArticleId = ArticleDefView.ArticleId LEFT OUTER JOIN tblVendorContractMaster ON RetentionMasterTable.VendorConractId = tblVendorContractMaster.ContractId LEFT OUTER JOIN vwCOADetail ON RetentionMasterTable.VendorId = vwCOADetail.coa_detail_id ORDER BY RetentionMasterId DESC  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select RetentionMasterId, VoucherNo, VoucherDate, VendorId, POId, VendorConractId, CostCenterId, ArticleId, Remarks from RetentionMasterTable  where RetentionMasterId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
