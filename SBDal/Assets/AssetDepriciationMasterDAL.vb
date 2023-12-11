Imports SBModel
Imports System.Data.SqlClient
Imports SBDal.UtilityDAL

Public Class AssetDepriciationMasterDAL

    Function Add(ByVal objModel As AssetDepriciationMasterBE) As Boolean
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
    Function Add(ByVal objModel As AssetDepriciationMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  tblDepriciationMaster (DocumentNo, Details, DepriciationMonth, EntryDate) values (N'" & objModel.DocumentNo & "', N'" & objModel.Details & "', N'" & objModel.DepriciationMonth.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objModel.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "') Select @@Identity"
            objModel.DepriciationMasterID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            AddDetail(objModel, trans)
            AddVoucher(objModel.Voucher, trans)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddDetail(ByVal Obj As AssetDepriciationMasterBE, ByVal trans As SqlTransaction) As Boolean
        
        Try
            For Each detail As AssetDepriciationDetailsBE In Obj.Detail
                Dim str As String = String.Empty

                str = "insert into  tblDepriciationDetails (Asset_Id, Rate, DepriciationAmount, Closing_Value, DepriciationMasterID, DepriciationMonths, CurrentValue) values (N'" & detail.Asset_Id & "', N'" & detail.Rate & "', N'" & detail.DepriciationAmount & "', N'" & detail.Closing_Value & "', N'" & Obj.DepriciationMasterID & "' , N'" & detail.DepriciationMonths & "' , N'" & detail.CurrentValue & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Function Update(ByVal objModel As AssetDepriciationMasterBE) As Boolean
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
    Function Update(ByVal objModel As AssetDepriciationMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update tblDepriciationMaster set DocumentNo= N'" & objModel.DocumentNo & "', Details= N'" & objModel.Details & "', DepriciationMonth= N'" & objModel.DepriciationMonth & "', EntryDate= N'" & objModel.EntryDate & "' where DepriciationMasterID=" & objModel.DepriciationMasterID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Delete(ByVal objModel As AssetDepriciationMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            If Delete(objModel, trans) = True Then
                trans.Commit()
                Return True

            Else

                Try
                    trans.Rollback()
                    Return False
                Catch exx As Exception
                    Throw exx
                    Return False
                End Try


            End If

        Catch ex As Exception

            Try
                trans.Rollback()
                Return False
            Catch exx As Exception
                Throw exx
                Return False
            End Try
        End Try
    End Function
    Public Shared Function Delete(ByVal objModel As AssetDepriciationMasterBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try

            If AssetDepriciationDetailsDAL.Delete(objModel.DepriciationMasterID, trans) = True Then

                If DeleteVoucher(objModel.Voucher, trans) = True Then
                    strSQL = "Delete from tblDepriciationMaster  where DepriciationMasterID= " & objModel.DepriciationMasterID
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    'objModel.ActivityLog.ActivityName = "Delete"
                    'objModel.ActivityLog.RecordType = "Configuration"
                    'objModel.ActivityLog.RefNo = ""
                    UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Shared Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select DepriciationMasterID, DocumentNo, Details, DepriciationMonth , EntryDate from tblDepriciationMaster order by DocumentNo DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select DepriciationMasterID, DocumentNo, Details, DepriciationMonth, EntryDate from tblDepriciationMaster  where DepriciationMasterID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddVoucher(ByVal ObjMod As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
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
            str = "INSERT INTO tblVoucher(Location_Id,voucher_code,finiancial_year_id,voucher_type_id,voucher_no,voucher_date,  post, source, UserName) " _
            & " Values(" & ObjMod.LocationId & ", N'" & ObjMod.VoucherCode & "', " & ObjMod.FinancialYearId & ", " & ObjMod.VoucherTypeId & ", N'" & ObjMod.VoucherNo & "', N'" & ObjMod.VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            & "  " & 1 & ", " _
            & " N'" & ObjMod.Source & "', N'" & ObjMod.UserName & "') Select @@Identity"
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
    Public Function AddVoucherDetail(ByVal MasterID As Integer, ByVal ObjModDt As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
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
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
              & "Values(" & MasterID & ", " & ObjMod.LocationId & ", " & ObjMod.CoaDetailId & ", " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ", " & ObjMod.DebitAmount & ", " & ObjMod.CreditAmount & ",1,1,1,1) Select @@Identity"
                ObjMod.VoucherDetailId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteVoucher(ByVal ObjMod As VouchersMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim VoucherId As Integer
        'Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        'If Not Con.State = 1 Then Con.Open()
        'Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String
            Dim Dt As New DataTable
            Dim Da As SqlClient.SqlDataAdapter = Nothing

            strSQL = "Select Voucher_Id From TblVoucher WHERE Voucher_No = N'" & ObjMod.VNo & "'"

            If trans IsNot Nothing Then
                Dt = UtilityDAL.GetDataTable(strSQL, trans)
            Else
                Dt = UtilityDAL.GetDataTable(strSQL)
            End If

            If Dt.Rows.Count > 0 Then
                VoucherId = Dt.Rows(0).Item(0).ToString
            End If
            'If UtilityDAL.GetConfigValue("EnabledDuplicateVoucherLog", trans).ToString.ToUpper = "TRUE" Then
            '    UtilityDAL.CreateDuplicationVoucher(VoucherId, "Delete", LoginUser.LoginUserId, LoginUser.LoginUserName, trans) '2710151
            'End If
            Dim str As String
            str = "Delete From tblVoucherDetail WHERE Voucher_Id = " & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Dim str1 As String
            str1 = "Delete From tblVoucher WHERE Voucher_Id = " & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str1)

            'Activity Log below
            'ObjMod.ActivityLog.ActivityName = "Delete"
            'ObjMod.ActivityLog.RecordType = "Accounts"
            'ObjMod.ActivityLog.RefNo = ObjMod.VoucherNo
            'Call ActivityBuldFunction in UtilityDAL 
            'Call UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
            ' trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw ex
            Return False
            'Finally
            'Con.Close()
        End Try
    End Function
End Class
