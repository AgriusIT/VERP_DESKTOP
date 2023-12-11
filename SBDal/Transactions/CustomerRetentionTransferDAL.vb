Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class CustomerRetentionTransferDAL
    Dim VoucherId As Integer
    Dim RetentionMasterId As Integer
    Dim VID As Integer
    Dim CustomerId As Integer
    Dim RetentionAccountId As Integer
    Dim CostCenterId As Integer
    Function Add(ByVal objModel As CustomerRetentionTransferBE) As Boolean
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
    Function Add(ByVal objModel As CustomerRetentionTransferBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If objModel.CurrentReceivables > 0 Then
                strSQL = "insert into  CustomerRetentionMasterTable (VoucherNo, VoucherDate, CustomerId, SOId, ContractId, CostCenterId, ArticleId, Remarks) values (N'" & objModel.VoucherNo.Replace("'", "''") & "', N'" & objModel.VoucherDate & "', N'" & objModel.CustomerId & "', N'" & objModel.SOId & "', N'" & objModel.ContractId & "', N'" & objModel.CostCenterId & "', N'" & objModel.ArticleId & "', N'" & objModel.Remarks.Replace("'", "''") & "') Select @@Identity"
                RetentionMasterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

                strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Reference) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.VoucherDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmCustomerRetentionTransfer',N'" & objModel.VoucherNo & "',N'TransferPer: " & objModel.TransferPer & ", CurrentReceivables: " & objModel.CurrentReceivables & ", " & objModel.Remarks & "') Select @@Identity"
                VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Else
                Return False
            End If
            RetentionAccountId = objModel.RetentionAccountId
            CustomerId = objModel.CustomerId
            CostCenterId = objModel.CostCenterId
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddRetentionDetail(ByVal list As List(Of CustomerRetentionTransferDetail)) As Boolean
        Dim strSQL As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As CustomerRetentionTransferDetail In list
                If obj.CurrentReceivables > 0 Then
                    strSQL = "insert into  CustomerRetentionDetailTable (RetentionMasterId, ContractId, ContractValue, AmountReceived, BalanceAmount, RententionValue, RetentionReceived, TransferPer, RealizedAmount, CurrentReceivables) values (N'" & RetentionMasterId & "',N'" & obj.ContractId & "', N'" & obj.ContractValue & "', N'" & obj.AmountReceived & "', N'" & obj.BalanceAmount & "', N'" & obj.RententionValue & "', N'" & obj.RetentionReceived & "', N'" & obj.TransferPer & "', N'" & obj.RealizedAmount & "', N'" & obj.CurrentReceivables & "') Select @@Identity"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                    strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & RetentionAccountId & ",0," & obj.CurrentReceivables & ",0," & obj.CurrentReceivables & ",1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentReceivables: " & obj.CurrentReceivables & ", " & obj.Remarks & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                    strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & CustomerId & "," & obj.CurrentReceivables & ",0," & obj.CurrentReceivables & ",0,1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentReceivables: " & obj.CurrentReceivables & ", " & obj.Remarks & "')"
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


    Function Update(ByVal objModel As CustomerRetentionTransferBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucher(ByVal objModel As CustomerRetentionTransferBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucher where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucherDetail(ByVal objModel As CustomerRetentionTransferBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucherDetail where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As CustomerRetentionTransferBE, trans As SqlTransaction) As Boolean
        Try
            RetentionMasterId = objModel.RetentionMasterId
            Dim str As String = "Select voucher_id from tblVoucher where voucher_no = '" & objModel.VoucherNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            VID = dt.Rows(0).Item("voucher_id")
            DeleteVoucherDetail(objModel, trans)
            DeleteVoucher(objModel, trans)
            Dim strSQL As String = String.Empty
            strSQL = "update CustomerRetentionMasterTable set VoucherNo= N'" & objModel.VoucherNo.Replace("'", "''") & "', VoucherDate= N'" & objModel.VoucherDate & "', CustomerId= N'" & objModel.CustomerId & "', SOId= N'" & objModel.SOId & "', ContractId= N'" & objModel.ContractId & "', CostCenterId= N'" & objModel.CostCenterId & "', ArticleId= N'" & objModel.ArticleId & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "' where RetentionMasterId=" & objModel.RetentionMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Reference) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.VoucherDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmCustomerRetentionTransfer',N'" & objModel.VoucherNo & "',N'TransferPer: " & objModel.TransferPer & ", CurrentReceivables: " & objModel.CurrentReceivables & ", " & objModel.Remarks & "') Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            RetentionAccountId = objModel.RetentionAccountId
            CustomerId = objModel.CustomerId
            CostCenterId = objModel.CostCenterId
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateRetentionDetail(ByVal list As List(Of CustomerRetentionTransferDetail)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As CustomerRetentionTransferDetail In list
                str = "If Exists(Select ISNULL(RetentionMasterId, 0) as RetentionMasterId From CustomerRetentionDetailTable Where RetentionMasterId=" & RetentionMasterId & ") update CustomerRetentionDetailTable set ContractId= N'" & obj.ContractId & "', ContractValue= N'" & obj.ContractValue & "', AmountReceived= N'" & obj.AmountReceived & "', BalanceAmount= N'" & obj.BalanceAmount & "', RententionValue= N'" & obj.RententionValue & "', RetentionReceived= N'" & obj.RetentionReceived & "', TransferPer= N'" & obj.TransferPer & "', RealizedAmount= N'" & obj.RealizedAmount & "', CurrentReceivables= N'" & obj.CurrentReceivables & "' where RetentionMasterId=" & RetentionMasterId & "" _
                 & " Else insert into  CustomerRetentionDetailTable (RetentionMasterId, ContractId, ContractValue, AmountReceived, BalanceAmount, RententionValue, RetentionReceived, TransferPer, RealizedAmount, CurrentReceivables) values (N'" & RetentionMasterId & "',N'" & obj.ContractId & "', N'" & obj.ContractValue & "', N'" & obj.AmountReceived & "', N'" & obj.BalanceAmount & "', N'" & obj.RententionValue & "', N'" & obj.RetentionReceived & "', N'" & obj.TransferPer & "', N'" & obj.RealizedAmount & "', N'" & obj.CurrentReceivables & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & RetentionAccountId & ",0," & obj.CurrentReceivables & ",0," & obj.CurrentReceivables & ",1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentReceivables: " & obj.CurrentReceivables & ", " & obj.Remarks & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID, Comments) Values(" & VoucherId & ",1," & CustomerId & "," & obj.CurrentReceivables & ",0," & obj.CurrentReceivables & ",0,1,1,1,1," & CostCenterId & ", N'TransferPer: " & obj.TransferPer & ", CurrentReceivables: " & obj.CurrentReceivables & ", " & obj.Remarks & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Function Delete(ByVal objModel As CustomerRetentionTransferBE) As Boolean
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
    Function Delete(ByVal objModel As CustomerRetentionTransferBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from CustomerRetentionMasterTable  where RetentionMasterId= " & objModel.RetentionMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from CustomerRetentionDetailTable  where RetentionMasterId= " & objModel.RetentionMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str As String
            str = "Select Voucher_id from tblVoucher where Voucher_No = '" & objModel.VoucherNo & "'"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            strSQL = "Delete from tblVoucherDetail where Voucher_Id = '" & VoucherId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete From tblVoucher where Voucher_Id = '" & VoucherId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " SELECT CustomerRetentionMasterTable.RetentionMasterId, CustomerRetentionMasterTable.VoucherNo, CustomerRetentionMasterTable.VoucherDate, CustomerRetentionMasterTable.CustomerId, CustomerRetentionMasterTable.SOId, CustomerRetentionMasterTable.ContractId, CustomerRetentionMasterTable.CostCenterId, CustomerRetentionMasterTable.ArticleId, CustomerRetentionMasterTable.Remarks, vwCOADetail.detail_title as Customer, tblCustomerContractMaster.DocNo,  ArticleDefView.ArticleDescription as Item, tblDefCostCenter.Name as CostCenter, SalesOrderMasterTable.SalesOrderNo FROM CustomerRetentionMasterTable LEFT OUTER JOIN SalesOrderMasterTable ON CustomerRetentionMasterTable.SOId = SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN tblDefCostCenter ON CustomerRetentionMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN ArticleDefView ON CustomerRetentionMasterTable.ArticleId = ArticleDefView.ArticleId LEFT OUTER JOIN tblCustomerContractMaster ON CustomerRetentionMasterTable.ContractId = tblCustomerContractMaster.ContractId LEFT OUTER JOIN vwCOADetail ON CustomerRetentionMasterTable.CustomerId = vwCOADetail.coa_detail_id ORDER BY RetentionMasterId DESC  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select RetentionMasterId, VoucherNo, VoucherDate, CustomerId, SOId, ContractId, CostCenterId, ArticleId, Remarks from CustomerRetentionMasterTable  where RetentionMasterId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
