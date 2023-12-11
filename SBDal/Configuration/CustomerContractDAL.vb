Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel

Public Class CustomerContractDAL
    Public Function Save(ByVal Contract As CustomerContractBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "Insert Into tblCustomerContractMaster (DocNo,DocDate,CustomerId,SOId,ItemId,TermId,RetentionPercentage,RetentionValue,MobilizationPerBill,MobilizationPercentage,MobilizationValue,BankId,ChequeNo,ChequeDate,ChequeDetails,ChequeAmount,ContractValue) Values(N'" & Contract.ContractNo & "',N'" & Contract.ContractDate & "'," & Contract.CustomerId & "," & Contract.SOId & "," & Contract.ItemId & "," & Contract.TermId & ",'" & Contract.RetentionPercentage & "','" & Contract.RetentionValue & "','" & Contract.MobilizationPerBill & "','" & Contract.MobilizationPercentage & "','" & Contract.MobilizationValue & "'," & Contract.BankId & ",'" & Contract.ChequeNo & "'," & If(Contract.ChequeDate = DateTime.MinValue, "NULL", "'" & Contract.ChequeDate & "'") & ",N'" & Contract.ChequeDetails & "','" & Contract.ChequeAmount & "','" & Contract.ContractValue & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As CustomerContractDetailBE In Contract.Detail
                str = "Insert Into tblCustomerContractDetail (ContractId,TaskId,TaskTitle,TaskDetail,TaskUnit,TaskRate,TotalMeasurment,NetValue,Qty) Values(" & "ident_current('tblCustomerContractMaster')" & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "',N'" & obj.TaskUnit & "'," & obj.TaskRate & "," & obj.TotalMeasurment & "," & obj.NetValue & "," & obj.Qty & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function Update(ByVal Contract As CustomerContractBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "Update tblCustomerContractMaster Set DocNo=N'" & Contract.ContractNo & "' ,DocDate=N'" & Contract.ContractDate & "' ,CustomerId=" & Contract.CustomerId & ",SOId=" & Contract.SOId & " ,ItemId=" & Contract.ItemId & ",TermId =" & Contract.TermId & ",ContractValue = '" & Contract.ContractValue & "',RetentionPercentage = '" & Contract.RetentionPercentage & "',RetentionValue = '" & Contract.RetentionValue & "',MobilizationPerBill = '" & Contract.MobilizationPerBill & "',MobilizationPercentage = '" & Contract.MobilizationPercentage & "',MobilizationValue = '" & Contract.MobilizationValue & "',BankId = " & Contract.BankId & ",ChequeNo = '" & Contract.ChequeNo & "',ChequeAmount = '" & Contract.ChequeAmount & "',ChequeDate = " & If(Contract.ChequeDate = DateTime.MinValue, "NULL", "'" & Contract.ChequeDate & "'") & ",ChequeDetails = N'" & Contract.ChequeDetails & "' Where ContractId = " & Contract.ContractId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update/Insert Detail records
            For Each obj As CustomerContractDetailBE In Contract.Detail
                str = "If Exists(Select Id From tblCustomerContractDetail Where Id=" & obj.DetailId & ")  Update tblCustomerContractDetail Set ContractId =" & Contract.ContractId & ",TaskId = " & obj.TaskId & ",TaskTitle = N'" & obj.TaskTitle & "',TaskDetail = N'" & obj.TaskDetail & "',TaskUnit = N'" & obj.TaskUnit & "',TaskRate = " & obj.TaskRate & ",TotalMeasurment = " & obj.TotalMeasurment & ",NetValue = " & obj.NetValue & ",Qty = " & obj.Qty & " Where Id = " & obj.DetailId & "" _
                   & " Else Insert Into tblCustomerContractDetail (ContractId,TaskId,TaskTitle,TaskDetail,TaskUnit,TaskRate,TotalMeasurment,NetValue,Qty) Values(" & Contract.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "',N'" & obj.TaskUnit & "'," & obj.TaskRate & "," & obj.TotalMeasurment & "," & obj.NetValue & "," & obj.Qty & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "update IntermPaymentCertificateDetail set ContractValue = " & obj.NetValue & " where TaskId = " & obj.TaskId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
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

    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "Delete from tblCustomerContractDetail Where ContractId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "Delete from tblCustomerContractMaster Where ContractId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Sub DeleteDetail(ByVal Id As Integer)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "Delete from tblCustomerContractDetail Where Id = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub SaveVoucher(ByVal Contract As CustomerContractBE)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            If Contract.MobilizationValue > 0 Then
                str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & Contract.ContractNo & "',N'" & Contract.ContractDate & "',1,N'frmCustomerContract',N'" & Contract.ContractNo & "',N'Customer Contract voucher')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Debit
                'Narration modoifcation for 3G constructions
                'Ali Faisal : 'Narration modoifcation for 3G constructions on 19-Nov-2018
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.CustomerId & "," & Contract.MobilizationValue & ", 0 , " & Contract.CostCenterId & ",'MOBILIZATION ADVANCE RECEIVABLE FROM " & Contract.CustomerName & " AGAINST " & Contract.ItemName & " OF " & Contract.SOQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")'," & Contract.MobilizationValue & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Credit Mobilization
                'Narration modoifcation for 3G constructions
                'Ali Faisal : 'Narration modoifcation for 3G constructions on 19-Nov-2018
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.MobilizationAccountId & ", 0 ," & Contract.MobilizationValue & "," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE RECEIVED FROM " & Contract.CustomerName & " AGAINST " & Contract.ItemName & " OF " & Contract.SOQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")',0," & Contract.MobilizationValue & ",1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub UpdateVoucher(ByVal Contract As CustomerContractBE)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update or insert voucher in update
            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & Contract.VoucherId & ") Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 1, voucher_no = N'" & Contract.ContractNo & "', voucher_date =N'" & Contract.ContractDate & "', post = 1, Source = N'frmCustomerContract', voucher_code = N'" & Contract.ContractNo & "', Remarks = N'Customer Contract voucher' Where voucher_Id = " & Contract.VoucherId & "" _
                & " ELSE INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & Contract.ContractNo & "',N'" & Contract.ContractDate & "',1,N'frmCustomerContract',N'" & Contract.ContractNo & "',N'Customer Contract voucher')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Details
            str = "Delete from tblVoucherDetail Where voucher_id = " & Contract.VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Debit
            'TFS1535 : Update or insert voucher in update
            'Ali Faisal : 'Narration modoifcation for 3G constructions on 19-Nov-2018
            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & Contract.VoucherId & ") INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & Contract.VoucherId & ",1," & Contract.CustomerId & "," & Contract.MobilizationValue & ", 0," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE RECEIVABLE FROM " & Contract.CustomerName & " AGAINST " & Contract.ItemName & " OF " & Contract.SOQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")',0," & Contract.MobilizationValue & ",1,1,1,1)" _
                & " ELSE  INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.CustomerId & "," & Contract.MobilizationValue & ",0," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE RECEIVABLE FROM " & Contract.CustomerName & " AGAINST " & Contract.ItemName & " OF " & Contract.SOQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")',0," & Contract.MobilizationValue & ",1,1,1,1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Credit Mobilization
            'Update or insert voucher in update
            'Ali Faisal : 'Narration modoifcation for 3G constructions on 19-Nov-2018
            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & Contract.VoucherId & ") INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & Contract.VoucherId & ",1," & Contract.MobilizationAccountId & ", 0, " & Contract.MobilizationValue & "," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE RECEIVED FROM " & Contract.CustomerName & " AGAINST " & Contract.ItemName & " OF " & Contract.SOQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")'," & Contract.MobilizationValue & ",0,1,1,1,1)" _
                & " ELSE INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.MobilizationAccountId & ", 0, " & Contract.MobilizationValue & "," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE RECEIVED FROM " & Contract.CustomerName & " AGAINST " & Contract.ItemName & " OF " & Contract.SOQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")'," & Contract.MobilizationValue & ",0,1,1,1,1) "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub DeleteVoucher(ByVal Id As Integer)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Details
            str = "Delete from tblVoucherDetail Where voucher_id=" & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master
            str = "Delete from tblVoucher Where voucher_id= " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

End Class
