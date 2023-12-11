Imports SBModel
Imports System.Data.SqlClient

Public Class ProfitSharingMasterDAL
    Public Shared Function Add(ByVal objModel As ProfitSharingMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Add(ByVal objModel As ProfitSharingMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ProfitSharingMaster(PropertyProfileId, SharingDate, voucher_no, ProfitForDistribution) values (" & objModel.PropertyProfileId & ", N'" & objModel.SharingDate.ToString("yyyy-M-d h:mm:ss tt") & "', '" & objModel.voucher_no.Replace("'", "''") & "', " & objModel.ProfitForDistribution & ") Select @@Identity"
            objModel.ProfitSharingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            ProfitSharingDetailDAL.Add(objModel.Detail, objModel.ProfitSharingId, trans)
            AddVoucher(objModel.Voucher, trans)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function AddVoucher(ByVal ObjMod As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        'AssetDepriciationMasterBE.Voucher.VoucherCode = Me.TxtDocumentNo.Text
        'AssetDepriciationMasterBE.Voucher.VoucherNo = Me.TxtDocumentNo.Text
        'AssetDepriciationMasterBE.Voucher.VoucherDate = Now
        'AssetDepriciationMasterBE.Voucher.LocationId = 1
        'AssetDepriciationMasterBE.Voucher.VoucherTypeId = 7
        'AssetDepriciationMasterBE.Voucher.FinancialYearId = 1
        'AssetDepriciationMasterBE.Voucher.UserName = LoginUserName
        'AssetDepriciationMasterBE.Voucher.Source = Me.Name
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO tblVoucher(Location_Id,voucher_code,finiancial_year_id,voucher_type_id,voucher_no,voucher_date,  post, source, UserName, Remarks) " _
            & " Values(" & ObjMod.LocationId & ", N'" & ObjMod.VoucherCode & "', " & ObjMod.FinancialYearId & ", " & ObjMod.VoucherTypeId & ", N'" & ObjMod.VoucherNo & "', N'" & ObjMod.VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            & "  " & 1 & ", " _
            & " N'" & ObjMod.Source & "', N'" & ObjMod.UserName & "' , N'" & ObjMod.Remarks & "') Select @@Identity"
            ObjMod.VoucherId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            Call AddVoucherDetail(ObjMod.VoucherId, ObjMod, trans)
            'ObjMod.ActivityLog.ActivityName = "Save"
            'ObjMod.ActivityLog.RecordType = "Accounts"
            'ObjMod.ActivityLog.RefNo = ObjMod.VoucherNo
            'Call UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Shared Function AddVoucherDetail(ByVal MasterID As Integer, ByVal ObjModDt As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim ObjModDetail As List(Of VouchersDetail) = ObjModDt.VoucherDatail
        Try
            Dim str As String = String.Empty
            'CreditEntry.CoaDetailId = Row.Cells("AccumulativeAccountID").Value
            'CreditEntry.DebitAmount = 0
            'CreditEntry.CreditAmount = Row.Cells("CurrentValue").Value
            'CreditEntry.LocationId = 1
            For Each ObjMod As VouchersDetail In ObjModDetail
                Dim PayeeTitle As String = String.Empty
                If ObjMod.PayeeTitle Is Nothing Then
                    PayeeTitle = String.Empty
                    ObjMod.PayeeTitle = PayeeTitle
                End If
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,Currency_Debit_Amount, Currency_Credit_Amount, CostCenterID , BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, Comments) " _
              & "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.CostCenter & " ,1,1,1,1 , '" & ObjMod.Comments & "') Select @@Identity"
                ObjMod.VoucherDetailId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function


    Public Shared Function DeleteVoucher(ByVal voucherNo As String) As Boolean
        Dim strSQL As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim strSQL3 As String = String.Empty
        Dim voucherId As Integer = 0

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            strSQL = " select Voucher_id from tblvoucher where Voucher_code = '" & voucherNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)

            For Each row As DataRow In dt.Rows

                voucherId = row.Item("Voucher_id")

            Next

            strSQL2 = "Delete from tblvoucherdetail where Voucher_id = " & voucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL2)

            strSQL3 = "Delete from tblvoucher where voucher_id = " & voucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL3)

            trans.Commit()

            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function

    Public Shared Function Update(ByVal objModel As ProfitSharingMasterBE) As Boolean
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
    Public Shared Function Update(ByVal objModel As ProfitSharingMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ProfitSharingMaster set PropertyProfileId= N'" & objModel.PropertyProfileId & "', SharingDate=N'" & objModel.SharingDate.ToString("yyyy-M-d h:mm:ss tt") & "', voucher_no= N'" & objModel.voucher_no.Replace("'", "''") & "', ProfitForDistribution= N'" & objModel.ProfitForDistribution & "' where ProfitSharingId=" & objModel.ProfitSharingId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ProfitSharingDetailDAL.Add(objModel.Detail, objModel.ProfitSharingId, trans)
            AddVoucher(objModel.Voucher, trans)

            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Delete(ByVal objModel As ProfitSharingMasterBE) As Boolean
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
    Public Shared Function Delete(ByVal objModel As ProfitSharingMasterBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProfitSharingMaster  where ProfitSharingId= " & objModel.ProfitSharingId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProfitSharingId, PropertyProfileId, SharingDate, voucher_no, ProfitForDistribution from ProfitSharingMaster  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProfitSharingId, PropertyProfileId, SharingDate, voucher_no, ProfitForDistribution from ProfitSharingMaster  where ProfitSharingId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
