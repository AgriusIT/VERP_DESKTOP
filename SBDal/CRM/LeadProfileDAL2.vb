Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class LeadProfileDAL2
    Dim LeadID As Integer = 0
    Function Add(ByVal objModel As LeadProfileBE2, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal LogoFiles As List(Of String)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, objPath, arrFile, LogoFiles, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As LeadProfileBE2, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal LogoFiles As List(Of String), trans As SqlTransaction) As Boolean
        Try
            '[LeadProfileId] [int] IDENTITY(1,1) NOT NULL,
            '[DocNo] [nvarchar](50) NULL,
            '[DocDate] [datetime] NULL,
            '[TypeId] [int] NULL,
            '[CompanyName] [nvarchar](250) NULL,
            '[coa_detail_id] [int] NULL,
            '[ActivityId] [int] NULL,
            '[SourceId] [int] NULL,
            '[IndustryId] [int] NULL,
            '[StatusId] [int] NULL,
            '[InterestedInId] [nvarchar](2500) NULL,
            '[BrandFocusId] [nvarchar](2500) NULL,
            '[Remarks] [nvarchar](2500) NULL,
            '[UserName] [nvarchar](50) NULL,
            '[ModifiedUser] [nvarchar](50) NULL,
            '[ModifiedDate] [datetime] NULL,
            Dim strSQL As String = String.Empty
            strSQL = "Insert Into  tblDefLeadProfile(DocNo, DocDate, TypeId, CompanyName,  ActivityId, SourceId, IndustryId, Status, InterestedInId, BrandFocusId, NoofEmployeeId, Remarks, UserName, ModifiedDate, EmployeeId, CountryId, UserId, WebSite, Address) " _
                & "    values (N'" & objModel.DocNo.Replace("'", "''") & "', N'" & objModel.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & objModel.TypeId & ", N'" & objModel.CompanyName.Replace("'", "") & "', " _
                & " " & objModel.ActivityId & ", " & objModel.SourceId & ", " & objModel.IndustryId & ", N'" & objModel.Status.Replace("'", "''") & "', '" & objModel.InterestedInId & "', '" & objModel.BrandFocusId & "', " & objModel.NoofEmployeeId & ", N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.UserName.Replace("'", "''") & "',  N'" & objModel.ModifiedDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & objModel.EmployeeId & ", " & objModel.CountryId & ", " & LoginUser.LoginUserId & ",N'" & objModel.Website.Replace("'", "''") & "', N'" & objModel.Address.Replace("'", "''") & "') Select @@Identity"
            LeadID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.LeadProfileId = LeadID
            ''Insertion into detail
            AddDetail(objModel, trans)
            ''
            ''INSERTION OF ACTIVITY REMARKS
            AddActivityRemarks(objModel, trans)
            ''
            ''TASK TFS4792
            If arrFile.Count > 0 Then
                SaveDocument(objModel.LeadProfileId, "frmLeadProfile2", objPath, arrFile, objModel.DocDate, trans)
            End If
            ''END TASK TFS4792
            ''TASK TFS4792
            If LogoFiles.Count > 0 Then
                LogoDocument(objModel.LeadProfileId, "frmLeadProfileList2", objPath, LogoFiles, objModel.DocDate, trans)
            End If
            ''END TASK TFS4792
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.User_Name = LoginUser.LoginUserName
            objModel.ActivityLog.Source = "frmLeadProfile2"
            objModel.ActivityLog.FormName = "frmLeadProfile2"
            objModel.ActivityLog.ApplicationName = "CRM"
            objModel.ActivityLog.LogDateTime = Now

            objModel.ActivityLog.FormCaption = "Lead Profile2"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.RefNo = objModel.DocNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
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

    Function Update(ByVal objModel As LeadProfileBE2, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal LogoFiles As List(Of String), ByVal ContactList As List(Of CompanyContactBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, objPath, arrFile, LogoFiles, trans, ContactList)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As LeadProfileBE2, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal LogoFiles As List(Of String), trans As SqlTransaction, ByVal ContactList As List(Of CompanyContactBE)) As Boolean
        Try
            Dim strSQL As String = String.Empty
            LeadID = objModel.LeadProfileId
            'Modified Query
            strSQL = "UPDATE tblDefLeadProfile SET DocNo = N'" & objModel.DocNo.Replace("'", "''") & "', DocDate = N'" & objModel.DocDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', TypeId = " & objModel.TypeId & ", " _
                & " CompanyName= N'" & objModel.CompanyName.Replace("'", "") & "' , ActivityId = " & objModel.ActivityId & ", SourceId = " & objModel.SourceId & ", IndustryId = " & objModel.IndustryId & ", " _
                & "  Status = '" & objModel.Status.Replace("'", "''") & "', InterestedInId= '" & objModel.InterestedInId & "', BrandFocusId= '" & objModel.BrandFocusId & "', NoofEmployeeId= " & objModel.NoofEmployeeId & ", Remarks = N'" & objModel.Remarks.Replace("'", "''") & "', " _
                & " ModifiedUser = N'" & objModel.ModifiedUser.Replace("'", "''") & "', ModifiedDate = N'" & objModel.ModifiedDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', EmployeeId = " & objModel.EmployeeId & ", CountryId = " & objModel.CountryId & ", UserId = " & LoginUser.LoginUserId & ", Website = N'" & objModel.Website.Replace("'", "''") & "', Address = N'" & objModel.Address.Replace("'", "''") & "' WHERE LeadProfileId = " & objModel.LeadProfileId & ""
            ''Insertion into detail
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            AddDetail(objModel, trans)
            ''
            AddActivityRemarks(objModel, trans)
            ''TASK TFS4792
            If arrFile.Count > 0 Then
                SaveDocument(objModel.LeadProfileId, "frmLeadProfile2", objPath, arrFile, objModel.DocDate, trans)
            End If
            ''END TASK TFS4792
            ''TASK TFS4792
            If LogoFiles.Count > 0 Then
                LogoDocument(objModel.LeadProfileId, "frmLeadProfileList2", objPath, LogoFiles, objModel.DocDate, trans)
            End If
            ''END TASK TFS4792
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.User_Name = LoginUser.LoginUserName
            objModel.ActivityLog.Source = "frmLeadProfile2"
            objModel.ActivityLog.FormName = "frmLeadProfile2"
            objModel.ActivityLog.ApplicationName = "CRM"
            objModel.ActivityLog.LogDateTime = Now
            objModel.ActivityLog.FormCaption = "Lead Profile2"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.RefNo = objModel.DocNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateAccount(ByVal list As List(Of ConcernedPersonBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            '            SELECT * FROM tblCOAMainSubSubDetail

            'SELECT * FROM tblCOAMainSubSub
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

    Function Delete(ByVal objModel As LeadProfileBE2) As Boolean
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
    Function Delete(ByVal objModel As LeadProfileBE2, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try

            If objModel.IsAccountCreated = True Then
                strSQL = "Delete FROM tblCOAMainSubsubDetail WHERE coa_detail_id = " & objModel.AccountId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ''TASK TFS4862
                strSQL = "Delete FROM TblCompanyContacts WHERE LeadProfileContactId IN (SELECT ISNULL(ContactId, 0) AS ContactId FROM tblDefLeadProfileContacts WHERE LeadProfileId = " & objModel.LeadProfileId & " ) "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ''END TASK TFS4862
            End If
            strSQL = "Delete FROM tblDefLeadProfile  WHERE LeadProfileId= " & objModel.LeadProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete FROM tblDefLeadProfileContacts  WHERE LeadProfileId= " & objModel.LeadProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete FROM tblDefLeadProfileRemarks  WHERE LeadProfileId= " & objModel.LeadProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.User_Name = LoginUser.LoginUserName
            objModel.ActivityLog.Source = "frmLeadProfile2"
            objModel.ActivityLog.FormName = "frmLeadProfile2"
            objModel.ActivityLog.ApplicationName = "CRM"
            objModel.ActivityLog.LogDateTime = Now
            objModel.ActivityLog.FormCaption = "Lead Profile2"
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.RefNo = objModel.DocNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteSingle(ByVal ContactId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            DeleteSingle(ContactId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DeleteSingle(ByVal ContactId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete FROM tblDefLeadProfileContacts  WHERE ContactId= " & ContactId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAll(Optional ByVal UserName As String = "") As DataTable
        Dim strSQL As String = String.Empty
        Try
            'Rafay:Modified query to add country name
            If LoginUser.LoginUserGroup = "Administrator" Then
                strSQL = "SELECT LeadProfile.LeadProfileId, LeadProfile.DocNo, LeadProfile.DocDate, LeadProfile.TypeId, tblDefLeadType.Title AS Type, LeadProfile.CompanyName, LeadProfile.coa_detail_id AS AccountId, LeadProfile.ActivityId, tblDefLeadActivity.Title AS Activity, " _
                        & " LeadProfile.SourceId, tblDefLeadSource.Title AS Source, " _
                        & " LeadProfile.IndustryId, tblDefLeadIndustry.Title AS Industry, LeadProfile.Status, LeadProfile.Website, " _
                        & " LeadProfile.InterestedInId, " _
                        & " LeadProfile.BrandFocusId, LeadProfile.NoofEmployeeId, tblDefNoofEmployee.NoofEmployee,  " _
                        & " LeadProfile.Remarks, LeadProfile.UserName, LeadProfile.ModifiedUser, LeadProfile.ModifiedDate, IsNull(LeadProfile.IsAccountCreated, 0) AS IsAccountCreated, Att.[No Of Attachment], Logo.Logo, LeadProfile.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, LeadProfile.CountryId, LeadProfile.Address,tblListCountry.CountryName  " _
                        & " FROM tblDefLeadProfile AS LeadProfile " _
                        & " LEFT OUTER JOIN  tblDefLeadType ON LeadProfile.TypeId = tblDefLeadType.Id " _
                        & " LEFT OUTER JOIN tblDefLeadActivity  ON LeadProfile.ActivityId = tblDefLeadActivity.Id " _
                        & " LEFT OUTER JOIN tblDefLeadSource ON LeadProfile.SourceId = tblDefLeadSource.Id " _
                        & " LEFT OUTER JOIN tblDefLeadIndustry ON LeadProfile.IndustryId = tblDefLeadIndustry.Id " _
                        & " LEFT OUTER JOIN tblDefNoofEmployee ON LeadProfile.NoofEmployeeId = tblDefNoofEmployee.Id " _
                        & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmLeadProfile2' Group By DocId) Att On Att.DocId =  LeadProfile.LeadProfileId " _
                        & " LEFT OUTER JOIN(Select Case WHEN Count(*) > 0 THEN 'Logo' ELSE '' END AS Logo, DocId From DocumentAttachment WHERE Source=N'frmLeadProfileList2' Group By DocId) Logo On Logo.DocId =  LeadProfile.LeadProfileId " _
                        & " LEFT OUTER JOIN tblDefEmployee ON LeadProfile.EmployeeId = tblDefEmployee.Employee_ID" _
                         & " LEFT OUTER JOIN tblListCountry  ON LeadProfile.CountryId = tblListCountry.CountryId " _
                        & " WHERE LeadProfile.LeadProfileId > 0 ORDER BY LeadProfile.LeadProfileId DESC"
                '" & IIf(UserName.Length > 0, " AND LeadProfile.UserName = '" & UserName & "'", "") & "
            Else
                strSQL = "SELECT LeadProfile.LeadProfileId, LeadProfile.DocNo, LeadProfile.DocDate, LeadProfile.TypeId, tblDefLeadType.Title AS Type, LeadProfile.CompanyName, LeadProfile.coa_detail_id AS AccountId, LeadProfile.ActivityId, tblDefLeadActivity.Title AS Activity, " _
                        & " LeadProfile.SourceId, tblDefLeadSource.Title AS Source, " _
                        & " LeadProfile.IndustryId, tblDefLeadIndustry.Title AS Industry, LeadProfile.Status, LeadProfile.Website, " _
                        & " LeadProfile.InterestedInId, " _
                        & " LeadProfile.BrandFocusId, LeadProfile.NoofEmployeeId, tblDefNoofEmployee.NoofEmployee,  " _
                        & " LeadProfile.Remarks, LeadProfile.UserName, LeadProfile.ModifiedUser, LeadProfile.ModifiedDate, IsNull(LeadProfile.IsAccountCreated, 0) AS IsAccountCreated, Att.[No Of Attachment], Logo.Logo, LeadProfile.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, LeadProfile.CountryId, LeadProfile.Address,tblListCountry.CountryName  " _
                        & " FROM tblDefLeadProfile AS LeadProfile " _
                        & " LEFT OUTER JOIN  tblDefLeadType ON LeadProfile.TypeId = tblDefLeadType.Id " _
                        & " LEFT OUTER JOIN tblDefLeadActivity  ON LeadProfile.ActivityId = tblDefLeadActivity.Id " _
                        & " LEFT OUTER JOIN tblDefLeadSource ON LeadProfile.SourceId = tblDefLeadSource.Id " _
                        & " LEFT OUTER JOIN tblDefLeadIndustry ON LeadProfile.IndustryId = tblDefLeadIndustry.Id " _
                        & " LEFT OUTER JOIN tblDefNoofEmployee ON LeadProfile.NoofEmployeeId = tblDefNoofEmployee.Id " _
                        & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmLeadProfile2' Group By DocId) Att On Att.DocId =  LeadProfile.LeadProfileId " _
                        & " LEFT OUTER JOIN(Select Case WHEN Count(*) > 0 THEN 'Logo' ELSE '' END AS Logo, DocId From DocumentAttachment WHERE Source=N'frmLeadProfileList2' Group By DocId) Logo On Logo.DocId =  LeadProfile.LeadProfileId " _
                        & " LEFT OUTER JOIN tblDefEmployee ON LeadProfile.EmployeeId = tblDefEmployee.Employee_ID" _
                        & " LEFT OUTER JOIN tblListCountry  ON LeadProfile.CountryId = tblListCountry.CountryId " _
                        & " WHERE LeadProfile.LeadProfileId > 0 AND LeadProfile.UserId in (Select CustomerID from tbluserwisecustomerlist where UserId = " & LoginUser.LoginUserId & ") ORDER BY LeadProfile.LeadProfileId DESC"
            End If
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            'strSQL = "SELECT LeadProfile.LeadProfileId, LeadProfile.DocNo, LeadProfile.DocDate, LeadProfile.TypeId, tblDefLeadType.Title AS Type, LeadProfile.CompanyName, LeadProfile.coa_detail_id AS AccountId, LeadProfile.ActivityId, tblDefLeadActivity.Title AS Activity, " _
            '        & " LeadProfile.SourceId, tblDefLeadSource.Title AS Source,  LeadProfile.Website, " _
            '        & " LeadProfile.IndustryId, tblDefLeadIndustry.Title AS Industry, " _
            '        & " tblDefLeadStatus.Title AS Status, LeadProfile.InterestedInId, " _
            '        & " LeadProfile.BrandFocusId, LeadProfile.NoofEmployeeId, tblDefNoofEmployee.NoofEmployee,  " _
            '        & " LeadProfile.Remarks, LeadProfile.UserName, LeadProfile.ModifiedUser, LeadProfile.ModifiedDate, LeadProfile.CountryId, LeadProfile.Address " _
            '        & " FROM tblDefLeadProfile AS LeadProfile " _
            '        & " LEFT OUTER JOIN  tblDefLeadType ON LeadProfile.TypeId = tblDefLeadType.Id " _
            '        & " LEFT OUTER JOIN tblDefLeadActivity  ON LeadProfile.ActivityId = tblDefLeadActivity.Id " _
            '        & " LEFT OUTER JOIN tblDefLeadSource ON LeadProfile.SourceId = tblDefLeadSource.Id " _
            '        & " LEFT OUTER JOIN tblDefLeadIndustry ON LeadProfile.IndustryId = tblDefLeadIndustry.Id " _
            '        & " LEFT OUTER JOIN tblDefNoofEmployee ON LeadProfile.NoofEmployeeId = tblDefNoofEmployee.Id WHERE LeadProfile.LeadProfileId = " & ID & ""
            strSQL = "SELECT LeadProfile.LeadProfileId, LeadProfile.DocNo, LeadProfile.DocDate, LeadProfile.TypeId, tblDefLeadType.Title AS Type, LeadProfile.CompanyName, LeadProfile.coa_detail_id AS AccountId, LeadProfile.ActivityId, tblDefLeadActivity.Title AS Activity, " _
                        & " LeadProfile.SourceId, tblDefLeadSource.Title AS Source, " _
                        & " LeadProfile.IndustryId, tblDefLeadIndustry.Title AS Industry, LeadProfile.Status, LeadProfile.Website, " _
                        & " LeadProfile.InterestedInId, " _
                        & " LeadProfile.BrandFocusId, LeadProfile.NoofEmployeeId, tblDefNoofEmployee.NoofEmployee,  " _
                        & " LeadProfile.Remarks, LeadProfile.UserName, LeadProfile.ModifiedUser, LeadProfile.ModifiedDate, IsNull(LeadProfile.IsAccountCreated, 0) AS IsAccountCreated, Att.[No Of Attachment], Logo.Logo, LeadProfile.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, LeadProfile.CountryId, LeadProfile.Address,tblListCountry.CountryName  " _
                        & " FROM tblDefLeadProfile AS LeadProfile " _
                        & " LEFT OUTER JOIN  tblDefLeadType ON LeadProfile.TypeId = tblDefLeadType.Id " _
                        & " LEFT OUTER JOIN tblDefLeadActivity  ON LeadProfile.ActivityId = tblDefLeadActivity.Id " _
                        & " LEFT OUTER JOIN tblDefLeadSource ON LeadProfile.SourceId = tblDefLeadSource.Id " _
                        & " LEFT OUTER JOIN tblDefLeadIndustry ON LeadProfile.IndustryId = tblDefLeadIndustry.Id " _
                        & " LEFT OUTER JOIN tblDefNoofEmployee ON LeadProfile.NoofEmployeeId = tblDefNoofEmployee.Id " _
                        & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmLeadProfile2' Group By DocId) Att On Att.DocId =  LeadProfile.LeadProfileId " _
                        & " LEFT OUTER JOIN(Select Case WHEN Count(*) > 0 THEN 'Logo' ELSE '' END AS Logo, DocId From DocumentAttachment WHERE Source=N'frmLeadProfileList2' Group By DocId) Logo On Logo.DocId =  LeadProfile.LeadProfileId " _
                        & " LEFT OUTER JOIN tblDefEmployee ON LeadProfile.EmployeeId = tblDefEmployee.Employee_ID" _
                        & " LEFT OUTER JOIN tblListCountry  ON LeadProfile.CountryId = tblListCountry.CountryId " _
                        & " WHERE LeadProfile.LeadProfileId = " & ID & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetail(ByVal LeadProfileId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            '[ContactId] [int] IDENTITY(1,1) NOT NULL,
            '[LeadProfileId] [int] NULL,
            '[Salutation] [nvarchar](250) NULL,
            '[FirstName] [nvarchar](250) NULL,
            '[LastName] [nvarchar](250) NULL,
            '[JobTitle] [nvarchar](250) NULL,
            '[DepartmentId]  [int] NULL,
            '[Email1] [nvarchar](50) NULL,
            '[Email2] [nvarchar](50) NULL,
            '[Mobile1] [nvarchar](50) NULL,
            '[Mobile2] [nvarchar](50) NULL,
            '[Phone] [nvarchar](50) NULL,
            '[Extention] [nvarchar](50) NULL,
            '[Website] [nvarchar](50) NULL,
            '[CountryId]  [int] NULL,
            '[CityId]  [int] NULL,
            strSQL = " SELECT Contacts.ContactId, Contacts.LeadProfileId, Contacts.Salutation, Contacts.FirstName, Contacts.LastName, Contacts.JobTitle, Contacts.DepartmentId, EmployeeDeptDefTable.EmployeeDeptName AS Department, " _
                    & " Contacts.Email1, Contacts.Email2, Contacts.Mobile1, Contacts.Mobile2, Contacts.Phone, Contacts.Extention, Contacts.Website, Contacts.CountryId, tblListCountry.CountryName AS Country, Contacts.CityId " _
                    & " FROM tblDefLeadProfileContacts AS Contacts " _
                    & " LEFT OUTER JOIN EmployeeDeptDefTable ON Contacts.DepartmentId = EmployeeDeptDefTable.EmployeeDeptId " _
                    & " LEFT OUTER JOIN tblListCountry ON Contacts.CountryId = tblListCountry.CountryId " _
                    & " WHERE Contacts.LeadProfileId = " & LeadProfileId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddDetail(ByVal Model As LeadProfileBE2, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Dim ContactId As Integer = 0
        Try
            '[ContactId] [int] IDENTITY(1,1) NOT NULL,
            '[LeadProfileId] [int] NULL,
            '[Salutation] [nvarchar](250) NULL,
            '[FirstName] [nvarchar](250) NULL,
            '[LastName] [nvarchar](250) NULL,
            '[JobTitle] [nvarchar](250) NULL,
            '[DepartmentId]  [int] NULL,
            '[Email1] [nvarchar](50) NULL,
            '[Email2] [nvarchar](50) NULL,
            '[Mobile1] [nvarchar](50) NULL,
            '[Mobile2] [nvarchar](50) NULL,
            '[Phone] [nvarchar](50) NULL,
            '[Extention] [nvarchar](50) NULL,
            '[Website] [nvarchar](50) NULL,
            '[CountryId]  [int] NULL,
            '[CityId]  [int] NULL,
            For Each objModel As LeadProfileContactsBE In Model.Detail
                If objModel.ContactId = 0 Then
                    strSQL = "Insert Into  tblDefLeadProfileContacts (LeadProfileId, Salutation, FirstName, LastName, JobTitle, DepartmentId, Email1, Email2, Mobile1, Mobile2, Phone, Extention, Website, CountryId, CityId) " _
                        & "    values (" & Model.LeadProfileId & ", N'" & objModel.Salutation.Replace("'", "''") & "', N'" & objModel.FirstName.Replace("'", "''") & "', " _
                        & " N'" & objModel.LastName.Replace("'", "") & "', N'" & objModel.JobTitle.Replace("'", "") & "', " _
                        & " " & objModel.DepartmentId & ", N'" & objModel.Email1.Replace("'", "") & "', N'" & objModel.Email2.Replace("'", "") & "', N'" & objModel.Mobile1.Replace("'", "") & "', " _
                        & " N'" & objModel.Mobile2.Replace("'", "") & "', N'" & objModel.Phone.Replace("'", "") & "', N'" & objModel.Extention.Replace("'", "''") & "', N'" & objModel.Website.Replace("'", "''") & "' , " & objModel.CountryId & ", '" & objModel.CityId & "') SELECT @@IDENTITY "
                    ContactId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                ElseIf objModel.ContactId > 0 Then
                    strSQL = " Update tblDefLeadProfileContacts SET LeadProfileId = " & Model.LeadProfileId & ", Salutation = N'" & objModel.Salutation.Replace("'", "''") & "', FirstName = N'" & objModel.FirstName.Replace("'", "''") & "', " _
                        & " LastName = N'" & objModel.LastName.Replace("'", "") & "', JobTitle=N'" & objModel.JobTitle.Replace("'", "") & "', DepartmentId = " & objModel.DepartmentId & ", " _
                        & " Email1 = N'" & objModel.Email1.Replace("'", "") & "', Email2 = N'" & objModel.Email2.Replace("'", "") & "', Mobile1 = N'" & objModel.Mobile1.Replace("'", "") & "', Mobile2 = N'" & objModel.Mobile2.Replace("'", "") & "', " _
                        & " Phone = N'" & objModel.Phone.Replace("'", "") & "', Extention = N'" & objModel.Extention.Replace("'", "''") & "', Website = N'" & objModel.Website.Replace("'", "''") & "', CountryId = " & objModel.CountryId & ", " _
                        & " CityId = '" & objModel.CityId & "' WHERE ContactId = " & objModel.ContactId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    ContactId = objModel.ContactId
                End If
                If Model.IsAccountCreated = True Then
                    Dim Contact As New CompanyContactBE
                    'Detail.ContactId = Val(grd.GetRows(i).Cells("ContactId").Value.ToString)
                    'Detail.LeadProfileId = Val(grd.GetRows(i).Cells("LeadProfileId").Value.ToString)
                    'Detail.Salutation = grd.GetRows(i).Cells("Salutation").Value.ToString
                    Contact.LeadProfileContactId = ContactId
                    Contact.RefCompanyId = Model.AccountId
                    Contact.ContactName = objModel.FirstName & " " & objModel.LastName
                    Contact.Designation = ""
                    Contact.Mobile = objModel.Mobile1
                    Contact.Phone = objModel.Phone
                    Contact.Fax = ""
                    Contact.Email = objModel.Email1
                    Contact.Address = IIf(objModel.CountryId > 0, objModel.Country, "")
                    Contact.Address += objModel.CityId
                    Contact.IndexNo = 0
                    Contact.Type = "Customer"
                    Contact.Company = Model.CompanyName
                    If objModel.DepartmentId > 0 Then
                        Contact.Department = objModel.Department
                    Else
                        Contact.Department = ""
                    End If
                    Contact.NamePrefix = objModel.Salutation
                    Contact.CompanyLocation = ""
                    Contact.CompanyLocationId = 0
                    AddContact(Contact, trans)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function AddActivityRemarks(ByVal objModel As LeadProfileBE2, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = " IF NOT EXISTS(SELECT * FROM  tblDefLeadProfileRemarks WHERE Remarks = N'" & objModel.Remarks.Replace("'", "''") & "' AND LeadProfileId = " & objModel.LeadProfileId & ") " _
                     & " INSERT INTO tblDefLeadProfileRemarks(LeadProfileId, Remarks, UserName, ModifiedUser, ModifiedDate) " _
                   & " VALUES (" & objModel.LeadProfileId & ", N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.UserName.Replace("'", "''") & "' , N'" & objModel.ModifiedUser.Replace("'", "''") & "', N'" & objModel.ModifiedDate.ToString("yyyy-M-d hh:mm:ss tt") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetActivityRemarks(ByVal LeadProfileId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT * FROM tblDefLeadProfileRemarks WHERE LeadProfileId = " & LeadProfileId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'cm.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title],OpeningBalance, Active, Parent_Id, AccessLevel) " & _
    '                              "VALUES( " & Me.ComboBox1.SelectedValue & ", '" & Me.txtMainCode.Text & "-" & Code & "', N'" & Me.TextBox2.Text & "'," & Me.txtOpening.Text & ", " & IIf(Me.chkActive.Checked = True, 1, 0) & ", " & Me.cmbParent.SelectedValue & ", '" & Me.cmbAccessLevel.Text.ToString & "')SELECT @@Identity"

    Public Function ConvertToAccount(ByVal SubSubAccountId As Integer, ByVal DetailCode As String, ByVal DetailTitle As String, ByVal LeadProfileId As Integer, ByVal ContactList As List(Of CompanyContactBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            ConvertToAccount(SubSubAccountId, DetailCode, DetailTitle, LeadProfileId, trans, ContactList)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ConvertToAccount(ByVal SubSubAccountId As Integer, ByVal DetailCode As String, ByVal DetailTitle As String, ByVal LeadProfileId As Integer, trans As SqlTransaction, ByVal ContactList As List(Of CompanyContactBE)) As Boolean
        Dim strSQL As String = String.Empty
        Try
            'Waqar: commented Lower lines to remove the chart of account creation at the time of Convert to Account
            'strSQL = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title],OpeningBalance, Active, Parent_Id, AccessLevel) " & _
            '                              "VALUES( " & SubSubAccountId & ", '" & DetailCode & "', N'" & DetailTitle & "', " & 0 & ", " & 1 & ", " & 0 & ", 'Everyone')SELECT @@Identity"
            'Dim AccountId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'strSQL = "Update tblDefLeadProfile SET IsAccountCreated = 1, coa_detail_id = " & AccountId & ", CompanyName = N'" & DetailTitle & "' WHERE LeadProfileId = " & LeadProfileId & ""
            strSQL = "Update tblDefLeadProfile SET IsAccountCreated = 1, CompanyName = N'" & DetailTitle & "' WHERE LeadProfileId = " & LeadProfileId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Waqar: Commented this line becuase we dont want to add every single Customer name to Company Contacts
            'AddContacts(ContactList, trans, AccountId)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsAccountExisted(ByVal AccountName As String) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT Count(*) as Count1 FROM tblCOAMainSubsubDetail WHERE Detail_title = '" & AccountName & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsCompanyExisted(ByVal CompanyName As String) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT Count(*) as Count1 FROM tblDefLeadProfile WHERE CompanyName = '" & CompanyName & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal dtpDate As DateTime, ByVal objTrans As SqlTransaction) As Boolean
        Dim cmd As New SqlCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()
            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)
            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            If arrFile.Count > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_" & dtpDate.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function LogoDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal dtpDate As DateTime, ByVal objTrans As SqlTransaction) As Boolean
        Dim cmd As New SqlCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try
            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()
            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)
            If arrFile.Count > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        'Dim New_Files As String = intId & "_" & DocId & "_" & dtpDate.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim New_Files As String = objFile.Substring(objFile.LastIndexOf("\"))
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function GetUserWiseEmployee(ByVal UserId As Integer) As DataTable
        Dim Str As String = String.Empty
        Try
            Str = "SELECT tblDefEmployee.Employee_Name AS EmployeeName, tblDefEmployee.EmpPicture, tblDefEmployee.Employee_ID AS EmployeeId FROM tblDefEmployee INNER JOIN tblUser ON tblDefEmployee.Employee_ID = tblUser.EmployeeId WHERE tblUser.User_ID =" & UserId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetEmployee(ByVal EmployeeId As Integer) As DataTable
        Dim Str As String = String.Empty
        Try
            Str = "SELECT Employee_Name AS EmployeeName,  EmpPicture, Employee_ID AS EmployeeId FROM tblDefEmployee WHERE tblDefEmployee.Employee_ID =" & EmployeeId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS4862
    ''' </summary>
    ''' <param name="ContactList"></param>
    ''' <param name="trans"></param>
    ''' <param name="AccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddContacts(ByVal ContactList As List(Of CompanyContactBE), ByVal trans As SqlTransaction, ByVal AccountId As Integer) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each objMod As CompanyContactBE In ContactList
                Dim strSQL As String = String.Empty
                strSQL = "IF NOT EXISTS(SELECT * FROM tblCompanyContacts WHERE LeadProfileContactId = " & objMod.LeadProfileContactId & ") INSERT INTO tblCompanyContacts(RefCompanyId, ContactName, Designation,Mobile, Phone,Fax,Email,Address,IndexNo,Type,Company,Department,NamePrefix, CompanyLocation, CompanyLocationId, LeadProfileContactId) " _
                & " VALUES(" & AccountId & ",'" & objMod.ContactName.Replace("'", "''") & "','" & objMod.Designation.Replace("'", "''") & "','" & objMod.Mobile.Replace("'", "''") & "', '" & objMod.Phone.Replace("'", "''") & "','" & objMod.Fax.Replace("'", "''") & "','" & objMod.Email.Replace("'", "''") & "','" & objMod.Address.Replace("'", "''") & "','" & objMod.IndexNo & "','" & objMod.Type.Replace("'", "''") & "','" & objMod.Company.Replace("'", "''") & "','" & objMod.Department.Replace("'", "''") & "','" & objMod.NamePrefix.Replace("'", "''") & "', '" & objMod.CompanyLocation.Replace("'", "''") & "', " & objMod.CompanyLocationId & ", " & objMod.LeadProfileContactId & ") " _
                & " ELSE UPDATE tblCompanyContacts SET RefCompanyId = " & AccountId & ", ContactName = '" & objMod.ContactName.Replace("'", "''") & "', Designation = N'" & objMod.Designation.Replace("'", "''") & "', Mobile = '" & objMod.Mobile.Replace("'", "''") & "', Phone = '" & objMod.Phone.Replace("'", "''") & "', Fax = '" & objMod.Fax.Replace("'", "''") & "', Email= '" & objMod.Email.Replace("'", "''") & "', Address = '" & objMod.Address.Replace("'", "''") & "', IndexNo= '" & objMod.IndexNo & "',  " _
                & " Type = '" & objMod.Type.Replace("'", "''") & "', Company = '" & objMod.Company.Replace("'", "''") & "', Department = '" & objMod.Department.Replace("'", "''") & "', NamePrefix = '" & objMod.NamePrefix.Replace("'", "''") & "', CompanyLocation = '" & objMod.CompanyLocation.Replace("'", "''") & "', CompanyLocationId = " & objMod.CompanyLocationId & " WHERE LeadProfileContactId = " & objMod.LeadProfileContactId & " "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            'trans.Commit()
            'Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
            'Finally
            'Con.Close()
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS4867
    ''' </summary>
    ''' <param name="objMod"></param>
    ''' <param name="trans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddContact(ByVal objMod As CompanyContactBE, ByVal trans As SqlTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'For Each objMod As CompanyContactBE In ContactList
            Dim strSQL As String = String.Empty
            strSQL = "IF NOT EXISTS(SELECT * FROM tblCompanyContacts WHERE LeadProfileContactId = " & objMod.LeadProfileContactId & ") INSERT INTO tblCompanyContacts(RefCompanyId, ContactName, Designation,Mobile, Phone,Fax,Email,Address,IndexNo,Type,Company,Department,NamePrefix, CompanyLocation, CompanyLocationId, LeadProfileContactId) " _
            & " VALUES(" & objMod.RefCompanyId & ",'" & objMod.ContactName.Replace("'", "''") & "','" & objMod.Designation.Replace("'", "''") & "','" & objMod.Mobile.Replace("'", "''") & "', '" & objMod.Phone.Replace("'", "''") & "','" & objMod.Fax.Replace("'", "''") & "','" & objMod.Email.Replace("'", "''") & "','" & objMod.Address.Replace("'", "''") & "','" & objMod.IndexNo & "','" & objMod.Type.Replace("'", "''") & "','" & objMod.Company.Replace("'", "''") & "','" & objMod.Department.Replace("'", "''") & "','" & objMod.NamePrefix.Replace("'", "''") & "', '" & objMod.CompanyLocation.Replace("'", "''") & "', " & objMod.CompanyLocationId & ", " & objMod.LeadProfileContactId & ") " _
            & " ELSE UPDATE tblCompanyContacts SET RefCompanyId = " & objMod.RefCompanyId & ", ContactName = '" & objMod.ContactName.Replace("'", "''") & "', Designation = N'" & objMod.Designation.Replace("'", "''") & "', Mobile = '" & objMod.Mobile.Replace("'", "''") & "', Phone = '" & objMod.Phone.Replace("'", "''") & "', Fax = '" & objMod.Fax.Replace("'", "''") & "', Email= '" & objMod.Email.Replace("'", "''") & "', Address = '" & objMod.Address.Replace("'", "''") & "', IndexNo= '" & objMod.IndexNo & "',  " _
            & " Type = '" & objMod.Type.Replace("'", "''") & "', Company = '" & objMod.Company.Replace("'", "''") & "', Department = '" & objMod.Department.Replace("'", "''") & "', NamePrefix = '" & objMod.NamePrefix.Replace("'", "''") & "', CompanyLocation = '" & objMod.CompanyLocation.Replace("'", "''") & "', CompanyLocationId = " & objMod.CompanyLocationId & " WHERE LeadProfileContactId = " & objMod.LeadProfileContactId & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Next
            'trans.Commit()
            'Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
            'Finally
            'Con.Close()
        End Try
    End Function
End Class
