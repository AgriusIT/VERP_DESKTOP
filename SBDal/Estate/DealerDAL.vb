Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class DealerDAL
    Function Add(ByVal objModel As DealerBE) As Boolean
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
    Function Add(ByVal objModel As DealerBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.empty
            strSQL = "insert into  Dealer (Name, PrimaryMobile, SecondaryMobile, Email, EstateId, coa_detail_id, SpecialityId, Remarks, Active, DesignationId) values (N'" & objModel.Name.Replace("'", "''") & "', N'" & objModel.PrimaryMobile.Replace("'", "''") & "', N'" & objModel.SecondaryMobile.Replace("'", "''") & "', N'" & objModel.Email.Replace("'", "''") & "', N'" & objModel.EstateId & "', " & objModel.coa_detail_id & ", N'" & objModel.SpecialityId & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.Active & "', N'" & objModel.DesignationId & "') Select @@Identity"
            objModel.DealerId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ApplicationName = "Configuration"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As DealerBE) As Boolean
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
    Function Update(ByVal objModel As DealerBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.empty
            strSQL = "update Dealer set Name= N'" & objModel.Name.Replace("'", "''") & "', PrimaryMobile= N'" & objModel.PrimaryMobile.Replace("'", "''") & "', SecondaryMobile= N'" & objModel.SecondaryMobile.Replace("'", "''") & "', Email= N'" & objModel.Email.Replace("'", "''") & "', EstateId= N'" & objModel.EstateId & "', coa_detail_id= N'" & objModel.coa_detail_id & "', SpecialityId= N'" & objModel.SpecialityId & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', Active= N'" & objModel.Active & "', DesignationId= N'" & objModel.DesignationId & "' where DealerId=" & objModel.DealerId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ApplicationName = "Configuration"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As DealerBE) As Boolean
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
    Function Delete(ByVal objModel As DealerBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.empty
        Try
            strSQL = "Delete from Dealer  where DealerId= " & objModel.DealerId
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
        Dim strSQL As String = String.empty
        Try
            strSQL = " Select DealerId, Name, PrimaryMobile, SecondaryMobile, Email, EstateId, coa_detail_id, SpecialityId, Remarks, Active, DesignationId from Dealer  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.empty
        Try

            strSQL = " Select DealerId, Name, PrimaryMobile, SecondaryMobile, Email, EstateId, coa_detail_id, SpecialityId, Remarks, Active, DesignationId from Dealer  where DealerId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
