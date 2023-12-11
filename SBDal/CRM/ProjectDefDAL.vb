Imports SBModel
Imports SBDal
Imports SBUtility
Imports System.Data.SqlClient
Public Class ProjectDefDAL
    Function Add(ByVal objModel As ProjectDefBE) As Boolean
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
    Function Add(ByVal objModel As ProjectDefBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  tblDefProject (ProjectTitle, ProjectCode, Plant, Scope, Address, RegionId, CityId, Products, ProjectStatusId, ProjectCategoryId, LeadProfileId, ContactPersonId, EngineeringConsultantId, ContractAwardedId, ResponsiblePersonId, InsideSalesPersonId, ManagerId, Details) values (N'" & objModel.ProjectTitle.Replace("'", "''") & "', N'" & objModel.ProjectCode.Replace("'", "''") & "', N'" & objModel.Plant.Replace("'", "''") & "', N'" & objModel.Scope.Replace("'", "''") & "', N'" & objModel.Address.Replace("'", "''") & "', N'" & objModel.RegionId & "', N'" & objModel.CityId & "', N'" & objModel.Products.Replace("'", "''") & "', N'" & objModel.ProjectStatusId & "', N'" & objModel.ProjectCategoryId & "', N'" & objModel.LeadProfileId & "', N'" & objModel.ContactPersonId & "', N'" & objModel.EngineeringConsultantId & "', N'" & objModel.ContractAwardedId & "', N'" & objModel.ResponsiblePersonId & "', N'" & objModel.InsideSalesPersonId & "', N'" & objModel.ManagerId & "', N'" & objModel.Details.Replace("'", "''") & "') Select @@Identity"
            objModel.ProjectId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = "CRM"
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ProjectDefBE) As Boolean
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
    Function Update(ByVal objModel As ProjectDefBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update tblDefProject set ProjectTitle= N'" & objModel.ProjectTitle.Replace("'", "''") & "', ProjectCode= N'" & objModel.ProjectCode.Replace("'", "''") & "', Plant= N'" & objModel.Plant.Replace("'", "''") & "', Scope= N'" & objModel.Scope.Replace("'", "''") & "', Address= N'" & objModel.Address.Replace("'", "''") & "', RegionId= N'" & objModel.RegionId & "', CityId= N'" & objModel.CityId & "', Products= N'" & objModel.Products.Replace("'", "''") & "', ProjectStatusId= N'" & objModel.ProjectStatusId & "', ProjectCategoryId= N'" & objModel.ProjectCategoryId & "', LeadProfileId= N'" & objModel.LeadProfileId & "', ContactPersonId= N'" & objModel.ContactPersonId & "', EngineeringConsultantId= N'" & objModel.EngineeringConsultantId & "', ContractAwardedId= N'" & objModel.ContractAwardedId & "', ResponsiblePersonId= N'" & objModel.ResponsiblePersonId & "', InsideSalesPersonId= N'" & objModel.InsideSalesPersonId & "', ManagerId= N'" & objModel.ManagerId & "', Details= N'" & objModel.Details.Replace("'", "''") & "' where ProjectId=" & objModel.ProjectId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = "CRM"
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ProjectDefBE) As Boolean
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
    Function Delete(ByVal objModel As ProjectDefBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblDefProject  where ProjectId= " & objModel.ProjectId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = "CRM"
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProjectId, ProjectTitle, ProjectCode, Plant, Scope, Address, RegionId, CityId, Products, ProjectStatusId, ProjectCategoryId, LeadProfileId, ContactPersonId, EngineeringConsultantId, ContractAwardedId, ResponsiblePersonId, InsideSalesPersonId, ManagerId, Details from tblDefProject  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProjectId, ProjectTitle, ProjectCode, Plant, Scope, Address, RegionId, CityId, Products, ProjectStatusId, ProjectCategoryId, LeadProfileId, ContactPersonId, EngineeringConsultantId, ContractAwardedId, ResponsiblePersonId, InsideSalesPersonId, ManagerId, Details from tblDefProject  where ProjectId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
