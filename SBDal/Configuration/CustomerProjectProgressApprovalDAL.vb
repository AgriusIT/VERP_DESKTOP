'04-Oct-2018 TFS# 4630 : Saad Afzaal : Add Save, Update and Delete functions to insert update and delete the records form Database.
Imports System
Imports System.Data.SqlClient
Imports SBModel

''' <summary>
''' Saad Afzaal : Save master record in approval table
''' </summary>
''' <remarks>'04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
Public Class CustomerProjectProgressApprovalDAL

    Public Function Save(ByVal Approval As CustomerProjectProgressApprovalBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "Insert Into CustomerProjectProgressApproval (CustomerId,SOId,ContractId,ProgressId,CurrentProgress,RetentionPercentage,RetentionValue,MobilizationPercentage,MobilizationValue,BillAmount,TotalDeduction,NetValue,Remarks) Values (" & Approval.CustomerId & "," & Approval.SOId & "," & Approval.ContractId & "," & Approval.ProgressId & ",'" & Approval.CurrentProgress & "','" & Approval.RetentionPercentage & "','" & Approval.RetentionValue & "','" & Approval.MobilizationPercentage & "','" & Approval.MobilizationValue & "','" & Approval.BillAmount & "','" & Approval.TotalDeduction & "','" & Approval.NetValue & "',N'" & Approval.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update CustomerProjectProgressApproval Approved column
            'str = "Update IntermPaymentCertificateMaster Set Approved='1' Where Id = " & Approval.ProgressId & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            For Each obj As CustomerProjectProgressApprovalExpenseBE In Approval.ExpenseList
                str = "Insert Into CustomerProjectProgressApprovalExpense (ApprovalId,AccountId,detail_code,detail_title,Amount) Values(" & "ident_current('CustomerProjectProgressApproval')" & "," & obj.AccountId & ",N'" & obj.detail_code & "',N'" & obj.detail_title & "'," & obj.Amount & ")"
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
    ''' Saad Afzaal : Update master record of approval table
    ''' </summary>
    ''' <param name="Approval"></param>
    ''' <returns>'04-Oct-2018 TFS# 4630 : Saad Afzaal</returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Approval As CustomerProjectProgressApprovalBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "Update CustomerProjectProgressApproval Set CustomerId = " & Approval.CustomerId & ",SOId = " & Approval.SOId & ",ContractId = " & Approval.ContractId & ",ProgressId = " & Approval.ProgressId & ",CurrentProgress = '" & Approval.CurrentProgress & "',RetentionPercentage = '" & Approval.RetentionPercentage & "',RetentionValue = '" & Approval.RetentionValue & "',MobilizationPercentage = '" & Approval.MobilizationPercentage & "',MobilizationValue = '" & Approval.MobilizationValue & "',BillAmount = '" & Approval.BillAmount & "',TotalDeduction = '" & Approval.TotalDeduction & "',NetValue = '" & Approval.NetValue & "',Remarks = N'" & Approval.Remarks & "' Where ApprovalId = " & Approval.ApprovalId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Saad Afzaal : Delete records from approval table
    ''' </summary>
    ''' <param name="Approval"></param>
    ''' <returns></returns>
    ''' <remarks>'04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Function Delete(ByVal Approval As CustomerProjectProgressApprovalBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Master records
            str = "Delete From CustomerProjectProgressApproval Where ApprovalId = " & Approval.ApprovalId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Delete From CustomerProjectProgressApprovalExpense Where ApprovalId = " & Approval.ApprovalId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Saad Afzaal : Delete voucher
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Approval"></param>
    ''' <remarks>'04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub DeleteVoucher(ByVal Id As Integer, ByVal Approval As CustomerProjectProgressApprovalBE)
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
            If Approval.RejectedId > 0 Then
                'Update CustomerProjectProgressApproval Approved and Rejected column
                str = "Update IntermPaymentCertificateMaster Set Approved = '0',Rejected = '1',SendForApproval = '0',Voucher = '0' Where Id = " & Approval.ProgressId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                'Update CustomerProjectProgressApproval Approved column
                str = "Update IntermPaymentCertificateMaster Set Approved = '0',Rejected = '0',SendForApproval = '1',Voucher = '0' Where Id = " & Approval.ProgressId & ""
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

    ''' <summary>
    ''' Saad Afzaal : Save Voucher of Approved Amount
    ''' </summary>
    ''' <param name="Approval"></param>
    ''' <remarks>'04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub SaveVoucher(ByVal Approval As CustomerProjectProgressApprovalBE)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master
            str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & Approval.ProgressNo & "',N'" & Date.Now & "',1,N'frmCustomerProjectProgressApproval',N'" & Approval.ProgressNo & "',N'Customer progress approval voucher')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Credit Sale
            'Saad Afzaal : TFS4630 : If amount is zero then restric to enter voucher
            If Approval.BillAmount > 0 Then
                'Saad Afzaal : TFS4630 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.AccountId & ", 0 , " & Approval.BillAmount & "," & Approval.CostCenterId & ",'" & Approval.ItemName & " BILL OF " & Approval.CustomerName & " AT " & Approval.TotalProgress & " % OF TOTAL WORK WITH AMOUNT OF RS. " & String.Format("{0:#,##0.00}", CInt(Val(Approval.TotalProgress) * Val(Approval.ContractValue) / 100)) & " AFTER ADJUSTMENT OF PREVIOUS BILLS AMOUNTING TO RS. " & String.Format("{0:#,##0.00}", CInt(Approval.PreviousAmount)) & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")',0," & Approval.BillAmount & ",1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Insert Details Debit Retention
            If Approval.RetentionValue > 0 Then
                'Saad Afzaal : TFS4630 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.RetentionAccountId & "," & Approval.RetentionValue & " ,0," & Approval.CostCenterId & ",'" & Approval.RetentionPercentage & " % RETENTION DEDUCTED ON " & Approval.ItemName & " BILL NO " & Approval.ProgressNo & " OF " & Approval.CustomerName & " (RS. " & String.Format("{0:#,##0.00}", CInt(Approval.BillAmount)) & " * " & Approval.RetentionPercentage & ") AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")'," & Approval.RetentionValue & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Insert Details Debit Mobilization
            If Approval.MobilizationValue > 0 Then
                'Saad Afzaal : TFS4630 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.MobilizationAccountId & "," & Approval.MobilizationValue & " ,0," & Approval.CostCenterId & ",'MOBILIZATION ADVANCE DEDUCTED ON " & Approval.ItemName & " BILL NO " & Approval.ProgressNo & " OF " & Approval.CustomerName & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & " OF VALUE " & String.Format("{0:#,##0.00}", CInt(Approval.ContractValue)) & ")'," & Approval.MobilizationValue & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Insert Details Debit
            If Approval.NetValue > 0 Then
                Dim ExpenseAmount As Double = 0
                For Each obj As CustomerProjectProgressApprovalExpenseBE In Approval.ExpenseList
                    If obj.Amount > 0 Then
                        ExpenseAmount += obj.Amount
                    ElseIf obj.Amount < 0 Then
                        ExpenseAmount += obj.Amount
                    End If
                Next
                'Saad Afzaal : TFS4630 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.CustomerId & "," & Approval.NetValue - ExpenseAmount & ",0," & Approval.CostCenterId & ",'" & Approval.ItemName & " BILL OF " & Approval.CustomerName & " AT " & Approval.TotalProgress & " % OF TOTAL WORK WITH AMOUNT OF RS. " & String.Format("{0:#,##0.00}", CInt(Val(Approval.TotalProgress) * Val(Approval.ContractValue) / 100)) & " AFTER ADJUSTMENT OF PREVIOUS BILLS AMOUNTING TO RS. " & String.Format("{0:#,##0.00}", CInt(Approval.PreviousAmount)) & " WITH NET BILL AMOUNT RS. " & String.Format("{0:#,##0.00}", CInt(Approval.BillAmount)) & " LESS " & Approval.RetentionPercentage & " % RETENTION RS. " & String.Format("{0:#,##0.00}", CInt(Approval.RetentionValue)) & " AND " & Approval.MobilizationPercentage & " % MOBILIZATION ADVANCE RS. " & String.Format("{0:#,##0.00}", CInt(Approval.MobilizationValue)) & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")'," & Approval.NetValue - ExpenseAmount & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If

            For Each obj As CustomerProjectProgressApprovalExpenseBE In Approval.ExpenseList
                If obj.Amount > 0 Then
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.AccountId & "," & obj.Amount & ",0," & Approval.CostCenterId & ",'" & obj.Comments & " " & Approval.ItemName & " BILL OF " & Approval.CustomerName & " AT " & Approval.TotalProgress & " % OF TOTAL WORK WITH AMOUNT OF RS. " & String.Format("{0:#,##0.00}", CInt(Val(Approval.TotalProgress) * Val(Approval.ContractValue) / 100)) & " AFTER ADJUSTMENT OF PREVIOUS BILLS AMOUNTING TO RS. " & String.Format("{0:#,##0.00}", CInt(Approval.PreviousAmount)) & " WITH NET BILL AMOUNT RS. " & String.Format("{0:#,##0.00}", CInt(Approval.BillAmount)) & " LESS " & Approval.RetentionPercentage & " % RETENTION RS. " & String.Format("{0:#,##0.00}", CInt(Approval.RetentionValue)) & " AND " & Approval.MobilizationPercentage & " % MOBILIZATION ADVANCE RS. " & String.Format("{0:#,##0.00}", CInt(Approval.MobilizationValue)) & " DEDUCTION AMOUNT OF RS. " & obj.Amount & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")'," & obj.Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                ElseIf obj.Amount < 0 Then
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.AccountId & ",0," & -1 * obj.Amount & "," & Approval.CostCenterId & ",'" & obj.Comments & " " & Approval.ItemName & " BILL OF " & Approval.CustomerName & " AT " & Approval.TotalProgress & " % OF TOTAL WORK WITH AMOUNT OF RS. " & String.Format("{0:#,##0.00}", CInt(Val(Approval.TotalProgress) * Val(Approval.ContractValue) / 100)) & " AFTER ADJUSTMENT OF PREVIOUS BILLS AMOUNTING TO RS. " & String.Format("{0:#,##0.00}", CInt(Approval.PreviousAmount)) & " WITH NET BILL AMOUNT RS. " & String.Format("{0:#,##0.00}", CInt(Approval.BillAmount)) & " LESS " & Approval.RetentionPercentage & " % RETENTION RS. " & String.Format("{0:#,##0.00}", CInt(Approval.RetentionValue)) & " AND " & Approval.MobilizationPercentage & " % MOBILIZATION ADVANCE RS. " & String.Format("{0:#,##0.00}", CInt(Approval.MobilizationValue)) & " DEDUCTION AMOUNT OF RS. " & obj.Amount & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")',0," & -1 * (obj.Amount) & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next

            'Saad Afzaal : TFS4630 : End
            'Update IntermPaymentCertificateMaster Approved column
            str = "Update IntermPaymentCertificateMaster Set Approved = '1',Rejected = '0', SendForApproval = '0',Voucher = ident_current('tblVoucher') Where Id = " & Approval.ProgressId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Delete records from Approval Expense table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>'10-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Function DeleteExpense(ByVal ApprovalId As Integer, ByVal AccountId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Master records
            str = "Delete From CustomerProjectProgressApprovalExpense Where ApprovalId = " & ApprovalId & " And AccountId = " & AccountId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

End Class
