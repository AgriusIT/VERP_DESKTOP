'18-July-2017 TFS# 1091 : Ali Faisal : Add Save, Update and Delete functions to insert update and delete the records form Database.
Imports System
Imports System.Data.SqlClient
Imports SBModel
''' <summary>
''' Ali Faisal : Save master record in approval table
''' </summary>
''' <remarks>'18-July-2017 TFS# 1091 : Ali Faisal</remarks>
Public Class ProjectProgressApprovalDAL
    Public Function Save(ByVal Approval As ProjectProgressApprovalBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "Insert Into tblProjectProgressApproval (VendorId,POId,ContractId,ProgressId,CurrentProgress,RetentionPercentage,RetentionValue,MobilizationPercentage,MobilizationValue,BillAmount,TotalDeduction,NetValue,Remarks) Values (" & Approval.VendorId & "," & Approval.POId & "," & Approval.ContractId & "," & Approval.ProgressId & ",'" & Approval.CurrentProgress & "','" & Approval.RetentionPercentage & "','" & Approval.RetentionValue & "','" & Approval.MobilizationPercentage & "','" & Approval.MobilizationValue & "','" & Approval.BillAmount & "','" & Approval.TotalDeduction & "','" & Approval.NetValue & "',N'" & Approval.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update tblTaskProgressMaster Approved column
            str = "Update tblTaskProgressMaster Set Approved='1' Where Id = " & Approval.ProgressId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Update master record of approval table
    ''' </summary>
    ''' <param name="Approval"></param>
    ''' <returns>'18-July-2017 TFS# 1091 : Ali Faisal</returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Approval As ProjectProgressApprovalBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "Update tblProjectProgressApproval Set VendorId = " & Approval.VendorId & ",POId = " & Approval.POId & ",ContractId = " & Approval.ContractId & ",ProgressId = " & Approval.ProgressId & ",CurrentProgress = '" & Approval.CurrentProgress & "',RetentionPercentage = '" & Approval.RetentionPercentage & "',RetentionValue = '" & Approval.RetentionValue & "',MobilizationPercentage = '" & Approval.MobilizationPercentage & "',MobilizationValue = '" & Approval.MobilizationValue & "',BillAmount = '" & Approval.BillAmount & "',TotalDeduction = '" & Approval.TotalDeduction & "',NetValue = '" & Approval.NetValue & "',Remarks = N'" & Approval.Remarks & "' Where ApprovalId = " & Approval.ApprovalId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Delete records from approval table
    ''' </summary>
    ''' <param name="Approval"></param>
    ''' <returns></returns>
    ''' <remarks>'18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Function Delete(ByVal Approval As ProjectProgressApprovalBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Master records
            str = "Delete From tblProjectProgressApproval Where ApprovalId = " & Approval.ApprovalId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Save Voucher of Approved Amount
    ''' </summary>
    ''' <param name="Approval"></param>
    ''' <remarks>'18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub SaveVoucher(ByVal Approval As ProjectProgressApprovalBE)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master
            str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,6,N'" & Approval.ProgressNo & "',N'" & Date.Now & "',1,N'frmProjectProgressApproval',N'" & Approval.ProgressNo & "',N'Project progress approval voucher')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Debit Purchase
            'Ali Faisal : TFS1469 : If amount is zero then restric to enter voucher
            If Approval.BillAmount > 0 Then
                'Ali Faisal : TFS1521 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.AccountId & "," & Approval.BillAmount & ",0," & Approval.CostCenterId & ",'" & Approval.ItemName & " BILL OF " & Approval.VendorName & " AT " & Approval.TotalProgress & " % OF TOTAL WORK WITH AMOUNT OF RS. " & String.Format("{0:#,##0.00}", CInt(Val(Approval.TotalProgress) * Val(Approval.ContractValue) / 100)) & " AFTER ADJUSTMENT OF PREVIOUS BILLS AMOUNTING TO RS. " & String.Format("{0:#,##0.00}", CInt(Approval.PreviousAmount)) & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")'," & Approval.BillAmount & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Insert Details Credit Retention
            If Approval.RetentionValue > 0 Then
                'Ali Faisal : TFS1521 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.RetentionAccountId & ",0," & Approval.RetentionValue & "," & Approval.CostCenterId & ",'" & Approval.RetentionPercentage & " % RETENTION DEDUCTED ON " & Approval.ItemName & " BILL NO " & Approval.ProgressNo & " OF " & Approval.VendorName & " (RS. " & String.Format("{0:#,##0.00}", CInt(Approval.BillAmount)) & " * " & Approval.RetentionPercentage & ") AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")',0," & Approval.RetentionValue & ",1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Insert Details Credit Mobilization
            If Approval.MobilizationValue > 0 Then
                'Ali Faisal : TFS1521 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.MobilizationAccountId & ",0," & Approval.MobilizationValue & "," & Approval.CostCenterId & ",'MOBILIZATION ADVANCE DEDUCTED ON " & Approval.ItemName & " BILL NO " & Approval.ProgressNo & " OF " & Approval.VendorName & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & " OF VALUE " & String.Format("{0:#,##0.00}", CInt(Approval.ContractValue)) & ")',0," & Approval.MobilizationValue & ",1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Insert Details Credit
            If Approval.NetValue > 0 Then
                'Ali Faisal : TFS1521 : Narration modoifcation for 3G constructions
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & Approval.VendorId & ",0," & Approval.NetValue & "," & Approval.CostCenterId & ",'" & Approval.ItemName & " BILL OF " & Approval.VendorName & " AT " & Approval.TotalProgress & " % OF TOTAL WORK WITH AMOUNT OF RS. " & String.Format("{0:#,##0.00}", CInt(Val(Approval.TotalProgress) * Val(Approval.ContractValue) / 100)) & " AFTER ADJUSTMENT OF PREVIOUS BILLS AMOUNTING TO RS. " & String.Format("{0:#,##0.00}", CInt(Approval.PreviousAmount)) & " WITH NET BILL AMOUNT RS. " & String.Format("{0:#,##0.00}", CInt(Approval.BillAmount)) & " LESS " & Approval.RetentionPercentage & " % RETENTION RS. " & String.Format("{0:#,##0.00}", CInt(Approval.RetentionValue)) & " AND " & Approval.MobilizationPercentage & " % MOBILIZATION ADVANCE RS. " & String.Format("{0:#,##0.00}", CInt(Approval.MobilizationValue)) & " AGAINST " & Approval.CostCenter & " - (CONTRACT NO. " & Approval.ContractNo & ")',0," & Approval.NetValue & ",1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'Ali Faisal : TFS1469 : End
            'Update tblTaskProgressMaster Approved column
            str = "Update tblTaskProgressMaster Set Approved = '1',Rejected = '0', SendForApproval = '0',Voucher = '1' Where Id = " & Approval.ProgressId & ""
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
    ''' Ali Faisal : Delete voucher
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Approval"></param>
    ''' <remarks>'18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub DeleteVoucher(ByVal Id As Integer, ByVal Approval As ProjectProgressApprovalBE)
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
                'Update tblTaskProgressMaster Approved and Rejected column
                str = "Update tblTaskProgressMaster Set Approved = '0',Rejected = '1',SendForApproval = '0',Voucher = '0' Where Id = " & Approval.ProgressId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                'Update tblTaskProgressMaster Approved column
                str = "Update tblTaskProgressMaster Set Approved = '0',Rejected = '0',SendForApproval = '1',Voucher = '0' Where Id = " & Approval.ProgressId & ""
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
End Class
