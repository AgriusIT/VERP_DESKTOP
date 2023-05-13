Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class LeadProfileDAL
    Dim LeadID As Integer = 0
    Function Add(ByVal objModel As LeadProfileBE) As Boolean
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
    Function Add(ByVal objModel As LeadProfileBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  LeadProfile (LeadTitle, SectorId, ProductName, StatusId, StatusRemarks, SourceId, SourceRemarks, ResponsibleId, InsideSalesId, ManagerId, Active, LeadDate) values (N'" & objModel.LeadTitle.Replace("'", "''") & "', N'" & objModel.SectorId & "', N'" & objModel.ProductName.Replace("'", "''") & "', N'" & objModel.StatusId & "', N'" & objModel.StatusRemarks.Replace("'", "''") & "', N'" & objModel.SourceId & "', N'" & objModel.SourceRemarks.Replace("'", "''") & "', N'" & objModel.ResponsibleId & "', N'" & objModel.InsideSalesId & "', N'" & objModel.ManagerId & "', " & IIf(objModel.Active = True, 1, 0) & ", Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "',102)) Select @@Identity"
            LeadID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Lead Profile"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.RefNo = objModel.LeadTitle.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddPerson(ByVal list As List(Of ConcernedPersonBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As ConcernedPersonBE In list
                str = "INSERT INTO tblConcernPerson(LeadId, ConcernPerson, Designation, PhoneNo, Email) Values(" & LeadID & ", N'" & obj.ConcernPerson & "',N'" & obj.Designation & "',N'" & obj.Phoneno & "', N'" & obj.Email & "')"
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

    Public Function AddOffice(ByVal list As List(Of LeadOfficeBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As LeadOfficeBE In list
                str = "INSERT INTO tblLeadOffice(LeadId, OfficeTitle, Address, Website) Values(" & LeadID & ",N'" & obj.Name & "',N'" & obj.Address & "',N'" & obj.Website & "')"
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

    Function Update(ByVal objModel As LeadProfileBE) As Boolean
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
    Function Update(ByVal objModel As LeadProfileBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            LeadID = objModel.LeadId
            strSQL = "update LeadProfile set LeadTitle= N'" & objModel.LeadTitle.Replace("'", "''") & "', SectorId= N'" & objModel.SectorId & "', ProductName= N'" & objModel.ProductName.Replace("'", "''") & "', StatusId= N'" & objModel.StatusId & "', StatusRemarks= N'" & objModel.StatusRemarks.Replace("'", "''") & "', SourceId= N'" & objModel.SourceId & "', SourceRemarks= N'" & objModel.SourceRemarks.Replace("'", "''") & "', ResponsibleId= N'" & objModel.ResponsibleId & "', InsideSalesId= N'" & objModel.InsideSalesId & "', ManagerId= " & objModel.ManagerId & " ,Active = " & IIf(objModel.Active = True, 1, 0) & " where LeadId=" & objModel.LeadId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Lead Profile"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.RefNo = objModel.LeadTitle.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdatePerson(ByVal list As List(Of ConcernedPersonBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As ConcernedPersonBE In list
                str = "If Exists(Select ISNULL(ConcernPersonId, 0) as ConcernPerson From tblConcernPerson Where ConcernPersonId =" & obj.LeadConcernedId & ") Update tblConcernPerson Set LeadId =" & LeadID & ", ConcernPerson =N'" & obj.ConcernPerson & "',Designation =N'" & obj.Designation & "', PhoneNo=N'" & obj.Phoneno & "',Email =N'" & obj.Email & "'  WHERE ConcernPersonId = " & obj.LeadConcernedId & "" _
                 & " Else INSERT INTO tblConcernPerson(LeadId, ConcernPerson, Designation, PhoneNo, Email) Values(" & LeadID & ", N'" & obj.ConcernPerson & "',N'" & obj.Designation & "',N'" & obj.Phoneno & "', N'" & obj.Email & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Public Function UpdateOffice(ByVal list As List(Of LeadOfficeBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As LeadOfficeBE In list
                str = "If Exists(Select ISNULL(LeadOfficeId, 0) as LeadOfficeId From tblLeadOffice Where LeadOfficeId=" & obj.LeadOfficeId & ") Update tblLeadOffice Set  LeadId=" & LeadID & ", OfficeTitle =N'" & obj.Name & "',Address =N'" & obj.Address & "',Website =N'" & obj.Website & "'  WHERE LeadOfficeId = " & obj.LeadOfficeId & "" _
                 & " Else INSERT INTO tblLeadOffice(LeadId, OfficeTitle, Address, Website) Values(" & LeadID & ",N'" & obj.Name & "',N'" & obj.Address & "',N'" & obj.Website & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Function Delete(ByVal objModel As LeadProfileBE) As Boolean
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
    Function Delete(ByVal objModel As LeadProfileBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from LeadProfile  where LeadId= " & objModel.LeadId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.UserID = LoginUser.LoginUserId
            'objModel.ActivityLog.FormCaption = "Lead Profile"
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "CRM"
            'objModel.ActivityLog.RefNo = objModel.LeadTitle.ToString
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT LeadProfile.LeadId, LeadProfile.LeadTitle, LeadProfile.SectorId, LeadProfile.ProductName, LeadProfile.StatusId, LeadProfile.StatusRemarks, LeadProfile.SourceId, LeadProfile.SourceRemarks, LeadProfile.ResponsibleId, LeadProfile.InsideSalesId, LeadProfile.ManagerId, tblLeadSector.SectorName, tblLeadStatus.StatusName, tblLeadSource.SourceName, tblDefEmployee.Employee_Name as Responsible, tblDefEmployee_1.Employee_Name AS InsideSales, tblDefEmployee_2.Employee_Name AS Manager " & _
                     "FROM LeadProfile LEFT OUTER JOIN " & _
                     "tblDefEmployee AS tblDefEmployee_2 ON LeadProfile.ManagerId = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadProfile.InsideSalesId = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee ON LeadProfile.ResponsibleId = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblLeadSource ON LeadProfile.SourceId = tblLeadSource.SourceId LEFT OUTER JOIN tblLeadStatus ON LeadProfile.StatusId = tblLeadStatus.StatusId LEFT OUTER JOIN tblLeadSector ON LeadProfile.SectorId = tblLeadSector.SectorId  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select LeadId, LeadTitle, SectorId, ProductName, StatusId, StatusRemarks, SourceId, SourceRemarks, ResponsibleId, InsideSalesId, ManagerId from LeadProfile  where LeadId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
