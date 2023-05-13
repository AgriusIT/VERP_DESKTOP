'05-July-2017 TFS1040 : Ali Faisal : Add save,update and delete functions to save update and remove data of Vendor Contract
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class VendorContractDAL
    ''' <summary>
    ''' Ali Faisal : Save the Data of Master and Detail in Vendor Contract tables.
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <returns></returns>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Function Save(ByVal Contract As VendorContractBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "Insert Into tblVendorContractMaster (DocNo,DocDate,VendorId,POId,ItemId,TermId,RetentionPercentage,RetentionValue,MobilizationPerBill,MobilizationPercentage,MobilizationValue,BankId,ChequeNo,ChequeDate,ChequeDetails,ChequeAmount,ContractValue) Values(N'" & Contract.ContractNo & "',N'" & Contract.ContractDate & "'," & Contract.VendorId & "," & Contract.POId & "," & Contract.ItemId & "," & Contract.TermId & ",'" & Contract.RetentionPercentage & "','" & Contract.RetentionValue & "','" & Contract.MobilizationPerBill & "','" & Contract.MobilizationPercentage & "','" & Contract.MobilizationValue & "'," & Contract.BankId & ",'" & Contract.ChequeNo & "'," & If(Contract.ChequeDate = DateTime.MinValue, "NULL", "'" & Contract.ChequeDate & "'") & ",N'" & Contract.ChequeDetails & "','" & Contract.ChequeAmount & "','" & Contract.ContractValue & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As VendorContractDetailBE In Contract.Detail
                str = "Insert Into tblVendorContractDetail (ContractId,TaskId,TaskTitle,TaskDetail,TaskUnit,TaskRate,TotalMeasurment,NetValue,Qty) Values(" & "ident_current('tblVendorContractMaster')" & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "',N'" & obj.TaskUnit & "'," & obj.TaskRate & "," & obj.TotalMeasurment & "," & obj.NetValue & "," & obj.Qty & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Update Master and Details records and also Check if detail records exists already then update else insert.
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <returns></returns>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Function Update(ByVal Contract As VendorContractBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "Update tblVendorContractMaster Set DocNo=N'" & Contract.ContractNo & "' ,DocDate=N'" & Contract.ContractDate & "' ,VendorId=" & Contract.VendorId & ",POId=" & Contract.POId & " ,ItemId=" & Contract.ItemId & ",TermId =" & Contract.TermId & ",ContractValue = '" & Contract.ContractValue & "',RetentionPercentage = '" & Contract.RetentionPercentage & "',RetentionValue = '" & Contract.RetentionValue & "',MobilizationPerBill = '" & Contract.MobilizationPerBill & "',MobilizationPercentage = '" & Contract.MobilizationPercentage & "',MobilizationValue = '" & Contract.MobilizationValue & "',BankId = " & Contract.BankId & ",ChequeNo = '" & Contract.ChequeNo & "',ChequeAmount = '" & Contract.ChequeAmount & "',ChequeDate = " & If(Contract.ChequeDate = DateTime.MinValue, "NULL", "'" & Contract.ChequeDate & "'") & ",ChequeDetails = N'" & Contract.ChequeDetails & "' Where ContractId = " & Contract.ContractId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update/Insert Detail records
            For Each obj As VendorContractDetailBE In Contract.Detail
                str = "If Exists(Select Id From tblVendorContractDetail Where Id=" & obj.DetailId & ")  Update tblVendorContractDetail Set ContractId =" & Contract.ContractId & ",TaskId = " & obj.TaskId & ",TaskTitle = N'" & obj.TaskTitle & "',TaskDetail = N'" & obj.TaskDetail & "',TaskUnit = N'" & obj.TaskUnit & "',TaskRate = " & obj.TaskRate & ",TotalMeasurment = " & obj.TotalMeasurment & ",NetValue = " & obj.NetValue & ",Qty = " & obj.Qty & " Where Id = " & obj.DetailId & "" _
                   & " Else Insert Into tblVendorContractDetail (ContractId,TaskId,TaskTitle,TaskDetail,TaskUnit,TaskRate,TotalMeasurment,NetValue,Qty) Values(" & Contract.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "',N'" & obj.TaskUnit & "'," & obj.TaskRate & "," & obj.TotalMeasurment & "," & obj.NetValue & "," & obj.Qty & ")"
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
    ''' <summary>
    ''' Ali Faisal : Delete the Detail and Master records.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
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
            str = "Delete from tblVendorContractDetail Where ContractId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "Delete from tblVendorContractMaster Where ContractId = " & Id & ""
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
    ''' <summary>
    ''' Ali Faisal : Delete the detail record.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
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
            str = "Delete from tblVendorContractDetail Where Id = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub
    Public Sub SaveVoucher(ByVal Contract As VendorContractBE)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master
            'Ali Faisal : TFS1460 : Restrict to enter voucher if mobilization value is zero
            If Contract.MobilizationValue > 0 Then
                str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,6,N'" & Contract.ContractNo & "',N'" & Contract.ContractDate & "',1,N'frmVendorContract',N'" & Contract.ContractNo & "',N'Vendor Contract voucher')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Debit Mobilization
                'Ali Faisal : TFS1520 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.MobilizationAccountId & "," & Contract.MobilizationValue & ",0," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE PAID TO " & Contract.VendorName & " AGAINST " & Contract.ItemName & " OF " & Contract.POQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")'," & Contract.MobilizationValue & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Credit
                'Ali Faisal : TFS1521 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.VendorId & ",0," & Contract.MobilizationValue & "," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE PAYABLE TO " & Contract.VendorName & " AGAINST " & Contract.ItemName & " OF " & Contract.POQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")',0," & Contract.MobilizationValue & ",1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Ali Faisal : TFS1460 : End
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub
    Public Sub UpdateVoucher(ByVal Contract As VendorContractBE)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master
            'str = "Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 6, voucher_no = N'" & Contract.ContractNo & "', voucher_date =N'" & Contract.ContractDate & "', post = 1, Source = N'frmVendorContract', voucher_code = N'" & Contract.ContractNo & "', Remarks = N'Vendor Contract voucher' Where voucher_Id = " & Contract.VoucherId & ""
            'Ali Faisal : TFS1535 : Update or insert voucher in update
            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & Contract.VoucherId & ") Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 6, voucher_no = N'" & Contract.ContractNo & "', voucher_date =N'" & Contract.ContractDate & "', post = 1, Source = N'frmVendorContract', voucher_code = N'" & Contract.ContractNo & "', Remarks = N'Vendor Contract voucher' Where voucher_Id = " & Contract.VoucherId & "" _
                & " ELSE INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,6,N'" & Contract.ContractNo & "',N'" & Contract.ContractDate & "',1,N'frmVendorContract',N'" & Contract.ContractNo & "',N'Vendor Contract voucher')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Details
            str = "Delete from tblVoucherDetail Where voucher_id = " & Contract.VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Debit Mobilization
            'Ali Faisal : TFS1535 : Update or insert voucher in update
            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & Contract.VoucherId & ") INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & Contract.VoucherId & ",1," & Contract.MobilizationAccountId & "," & Contract.MobilizationValue & ",0," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE PAID TO " & Contract.VendorName & " AGAINST " & Contract.ItemName & " OF " & Contract.POQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")'," & Contract.MobilizationValue & ",0,1,1,1,1)" _
                & " ELSE INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.MobilizationAccountId & "," & Contract.MobilizationValue & ",0," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE PAID TO " & Contract.VendorName & " AGAINST " & Contract.ItemName & " OF " & Contract.POQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")'," & Contract.MobilizationValue & ",0,1,1,1,1) "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Credit
            'Ali Faisal : TFS1535 : Update or insert voucher in update
            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & Contract.VoucherId & ") INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & Contract.VoucherId & ",1," & Contract.VendorId & ",0," & Contract.MobilizationValue & "," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE PAYABLE TO " & Contract.VendorName & " AGAINST " & Contract.ItemName & " OF " & Contract.POQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")',0," & Contract.MobilizationValue & ",1,1,1,1)" _
                & " ELSE  INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Contract.VendorId & ",0," & Contract.MobilizationValue & "," & Contract.CostCenterId & ",'MOBILIZATION ADVANCE PAYABLE TO " & Contract.VendorName & " AGAINST " & Contract.ItemName & " OF " & Contract.POQty & " HOUSES AT " & Contract.CostCenter & " - (" & Contract.ContractNo & ")',0," & Contract.MobilizationValue & ",1,1,1,1)"
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
