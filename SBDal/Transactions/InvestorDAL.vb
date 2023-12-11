Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class InvestorDAL
    Function Add(ByVal objModel As InvestorBE) As Boolean
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
    Function Add(ByVal objModel As InvestorBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  Investor (Name, PrimaryMobile, SecondaryMobile, coa_detail_id, ProfitRatio, Remarks, Active, CNIC, Email, AddressLine1, AddressLine2, CityId) values (N'" & objModel.Name.Replace("'", "''") & "', N'" & objModel.PrimaryMobile.Replace("'", "''") & "', N'" & objModel.SecondaryMobile.Replace("'", "''") & "', " & objModel.coa_detail_id & ", N'" & objModel.ProfitRatio & "', N'" & objModel.Remarks.Replace("'", "''") & "', " & IIf(objModel.Active = True, 1, 0) & ", N'" & objModel.CNIC & "', N'" & objModel.Email & "', N'" & objModel.AddressLine1.Replace("'", "''") & "', N'" & objModel.AddressLine2.Replace("'", "''") & "'," & objModel.CityId & ") Select @@Identity"
            objModel.InvestorId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Investor"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As InvestorBE) As Boolean
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
    Function Update(ByVal objModel As InvestorBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update Investor set Name= N'" & objModel.Name.Replace("'", "''") & "', PrimaryMobile= N'" & objModel.PrimaryMobile.Replace("'", "''") & "', SecondaryMobile= N'" & objModel.SecondaryMobile.Replace("'", "''") & "', coa_detail_id= " & objModel.coa_detail_id & ", ProfitRatio= N'" & objModel.ProfitRatio & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', Active= " & IIf(objModel.Active = True, 1, 0) & ", CNIC= N'" & objModel.CNIC & "', Email= N'" & objModel.Email & "', AddressLine1= N'" & objModel.AddressLine1.Replace("'", "''") & "', AddressLine2= N'" & objModel.AddressLine2.Replace("'", "''") & "', CityId= " & objModel.CityId & " where InvestorId=" & objModel.InvestorId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Investor"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As InvestorBE) As Boolean
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
    Function Delete(ByVal objModel As InvestorBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from Investor  where InvestorId= " & objModel.InvestorId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Investor"
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select InvestorId, Name, PrimaryMobile, SecondaryMobile, coa_detail_id, ProfitRatio, Remarks, Active, CNIC, Email, AddressLine1, AddressLine2, CityId from Investor Order By 1 Desc  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select InvestorId, Name, PrimaryMobile, SecondaryMobile, coa_detail_id, ProfitRatio, Remarks, Active, CNIC, Email, AddressLine1, AddressLine2, CityId from Investor  where InvestorId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
