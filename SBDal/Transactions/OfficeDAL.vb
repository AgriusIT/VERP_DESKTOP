Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class OfficeDAL
    Function Add(ByVal objModel As OfficeBE) As Boolean
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
    Function Add(ByVal objModel As OfficeBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  Office (Name, LandlinePhone, CellPhone, Email, FaxNumber, AddressLine1, AddressLine2, AreaId, Remarks, EstateId, CityId) values (N'" & objModel.Name.Replace("'", "''") & "', N'" & objModel.LandlinePhone.Replace("'", "''") & "', N'" & objModel.CellPhone.Replace("'", "''") & "', N'" & objModel.Email.Replace("'", "''") & "', N'" & objModel.FaxNumber.Replace("'", "''") & "', N'" & objModel.AddressLine1.Replace("'", "''") & "', N'" & objModel.AddressLine2.Replace("'", "''") & "', " & objModel.AreaId & ", N'" & objModel.Remarks.Replace("'", "''") & "', " & objModel.EstateId & ", " & objModel.CityId & ") Select @@Identity"
            objModel.OfficeId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Office"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As OfficeBE) As Boolean
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
    Function Update(ByVal objModel As OfficeBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update Office set Name= N'" & objModel.Name.Replace("'", "''") & "', LandlinePhone= N'" & objModel.LandlinePhone.Replace("'", "''") & "', CellPhone= N'" & objModel.CellPhone.Replace("'", "''") & "', Email= N'" & objModel.Email.Replace("'", "''") & "', FaxNumber= N'" & objModel.FaxNumber.Replace("'", "''") & "', AddressLine1= N'" & objModel.AddressLine1.Replace("'", "''") & "', AddressLine2= N'" & objModel.AddressLine2.Replace("'", "''") & "', AreaId= N'" & objModel.AreaId & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', EstateId= N'" & objModel.EstateId & "', CityId= N'" & objModel.CityId & "' where OfficeId=" & objModel.OfficeId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Office"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Name.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As OfficeBE) As Boolean
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
    Function Delete(ByVal objModel As OfficeBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from Office  where OfficeId= " & objModel.OfficeId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Office"
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
            strSQL = " Select OfficeId, Name, LandlinePhone, CellPhone, Email, FaxNumber, AddressLine1, AddressLine2, AreaId, Remarks, EstateId, CityId from Office Order by 1 Desc  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select OfficeId, Name, LandlinePhone, CellPhone, Email, FaxNumber, AddressLine1, AddressLine2, AreaId, Remarks, EstateId, CityId from Office  where OfficeId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
