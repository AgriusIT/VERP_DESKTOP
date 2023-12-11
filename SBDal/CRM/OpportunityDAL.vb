Imports SBModel
Imports System.Data.SqlClient

Public Class OpportunityDAL
    Function Add(ByVal objModel As OpportunityBE, ByVal objPath As String, ByVal arrFile As List(Of String)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, objPath, arrFile, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As OpportunityBE, ByVal objPath As String, ByVal arrFile As List(Of String), trans As SqlTransaction) As Boolean
        Try
            'Rafay MOdoefied query to add check box includebatteries 4-11-2022
            Dim strSQL As String = String.Empty
            strSQL = "insert into  tblDefOpportunity(DocNo, DocDate, CompanyId, ContactId, EndUserId, OpportunityName, TypeId, CurrencyId, OpportunityOwner, CloseDate, StageId, LoosReasonId, ProbabilityId, HardwareContact, TaxAmount, Duration, StartDate, PaymentId, DeliveryId, FrequencyId, Freight, ImplementationTime, SupportTypeId, MaterialLocation, TargetPrice, MaintenanceId, UserName, ContactName, OpportunityType, LeadTimeInDays, EmployeeId, UserId, CountryId, RequestTime, OnsiteId, CoverageWindow, OnsiteIntervention, TotalAmount, ChkBoxBatteriesIncluded, DurationofMonth, InvoicePattern, ArticleId, PMFrequency) values (N'" & objModel.DocNo.Replace("'", "''") & "', N'" & objModel.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & objModel.CompanyId & ", " & objModel.ContactId & ", '" & objModel.EndUserId & "', N'" & objModel.OpportunityName.Replace("'", "''") & "', " & objModel.TypeId & ", " & objModel.CurrencyId & ", N'" & objModel.OpportunityOwner.Replace("'", "''") & "', N'" & objModel.CloseDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & objModel.StageId & ", " & objModel.LoosReasonId & ", " & objModel.ProbabilityId & ", N'" & objModel.HardwareContact.Replace("'", "''") & "', " & objModel.TaxAmount & ", N'" & objModel.Duration.Replace("'", "''") & "', N'" & objModel.StartDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & objModel.PaymentId & ", " & objModel.DeliveryId & ", " & objModel.FrequencyId & ", " & objModel.Freight & ", N'" & objModel.ImplementationTime.Replace("'", "''") & "', " & objModel.SupportTypeId & ", N'" & objModel.MaterialLocation.Replace("'", "''") & "', " & objModel.TargetPrice & ", " & objModel.MaintenanceId & ", N'" & objModel.UserName.Replace("'", "''") & "', N'" & objModel.ContactName.Replace("'", "''") & "', N'" & objModel.OpportunityType.Replace("'", "''") & "', N'" & objModel.LeadTimeInDays.Replace("'", "''") & "', " & objModel.EmployeeId & ", " & objModel.UserId & ", " & objModel.CountryId & ", '" & IIf(objModel.StageId = 2, Date.Now.ToString("dd-MMM-yyy hh:mm:ss"), "") & "', N'" & objModel.OnsiteId.Replace("'", "''") & "', N'" & objModel.CoverageWindow & "', N'" & objModel.OnsiteIntervention & "', " & objModel.TotalAmount & ", " & IIf(objModel.ChkBoxBatteriesIncluded = True, 1, 0) & " , N'" & objModel.DurationofMonth.Replace("'", "''") & "', N'" & objModel.InvoicePattern.Replace("'", "''") & "', " & objModel.ArticleId & ", N'" & objModel.PMFrequency.Replace("'", "''") & "')Select @@Identity"
            objModel.OpportunityId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            If objModel.OpportunityType = "Hardware" Then
                Call New OpportunityHardwareDetailDAL().Add(objModel, trans)
            ElseIf objModel.OpportunityType = "Support" Then
                Call New OpportunitySupportDetailDAL().Add(objModel, trans)
            End If
            ''TASK TFS4792
            If arrFile.Count > 0 Then
                SaveDocument(objModel.OpportunityId, "frmopportunity", objPath, arrFile, objModel.DocDate, trans)
            End If
            ''END TASK TFS4792
            objModel.ActivityLog.ApplicationName = "CRM"
            objModel.ActivityLog.FormName = "frmopportunity"
            objModel.ActivityLog.FormCaption = "Opportunity"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.Source = "frmopportunity"
            objModel.ActivityLog.User_Name = LoginUser.LoginUserName
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.LogDateTime = Now
            objModel.ActivityLog.RefNo = objModel.DocNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As OpportunityBE, ByVal objPath As String, ByVal arrFile As List(Of String)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, objPath, arrFile, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As OpportunityBE, ByVal objPath As String, ByVal arrFile As List(Of String), trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            'Rafay MOdoefied query to add check box includebatteries 4-11-2022
            strSQL = "update tblDefOpportunity set DocNo= N'" & objModel.DocNo.Replace("'", "''") & "', DocDate= N'" & objModel.DocDate & "', CompanyId= " & objModel.CompanyId & ", ContactId= " & objModel.ContactId & ", EndUserId= '" & objModel.EndUserId & "', OpportunityName= N'" & objModel.OpportunityName.Replace("'", "''") & "', TypeId= N'" & objModel.TypeId & "', CurrencyId= N'" & objModel.CurrencyId & "', OpportunityOwner= N'" & objModel.OpportunityOwner.Replace("'", "''") & "', CloseDate= N'" & objModel.CloseDate & "', StageId= N'" & objModel.StageId & "', LoosReasonId= N'" & objModel.LoosReasonId & "', ProbabilityId= N'" & objModel.ProbabilityId & "', HardwareContact= N'" & objModel.HardwareContact.Replace("'", "''") & "', TaxAmount= N'" & objModel.TaxAmount & "', Duration= N'" & objModel.Duration.Replace("'", "''") & "', StartDate= N'" & objModel.StartDate & "', PaymentId= N'" & objModel.PaymentId & "', DeliveryId= N'" & objModel.DeliveryId & "', FrequencyId= N'" & objModel.FrequencyId & "', Freight= N'" & objModel.Freight & "', ImplementationTime= N'" & objModel.ImplementationTime.Replace("'", "''") & "', SupportTypeId= N'" & objModel.SupportTypeId & "', MaterialLocation= N'" & objModel.MaterialLocation.Replace("'", "''") & "', TargetPrice= N'" & objModel.TargetPrice & "', MaintenanceId= N'" & objModel.MaintenanceId & "', UserName= N'" & objModel.UserName.Replace("'", "''") & "', ModifiedUser= N'" & objModel.ModifiedUser.Replace("'", "''") & "', ModifiedDate= N'" & objModel.ModifiedDate & "', ContactName= N'" & objModel.ContactName.Replace("'", "''") & "', OpportunityType= N'" & objModel.OpportunityType.Replace("'", "''") & "', LeadTimeInDays= N'" & objModel.LeadTimeInDays.Replace("'", "''") & "', EmployeeId = " & objModel.EmployeeId & ", UserId = " & objModel.UserId & ", CountryId= N'" & objModel.CountryId & "',RequestTime= N'" & IIf(objModel.StageId = 2, Date.Now.ToString("dd-MMM-yyy hh:mm:ss"), "") & "', OnsiteId= N'" & objModel.OnsiteId.Replace("'", "''") & "', CoverageWindow = N'" & objModel.CoverageWindow & "', OnsiteIntervention = N'" & objModel.OnsiteIntervention & "', TotalAmount = " & objModel.TotalAmount & " , ChkBoxBatteriesIncluded = " & IIf(objModel.ChkBoxBatteriesIncluded = True, 1, 0) & ", DurationofMonth= N'" & objModel.DurationofMonth.Replace("'", "''") & "', InvoicePattern= N'" & objModel.InvoicePattern.Replace("'", "''") & "', ArticleId= " & objModel.ArticleId & ", PMFrequency= N'" & objModel.PMFrequency.Replace("'", "''") & "'  where OpportunityId=" & objModel.OpportunityId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            If objModel.OpportunityType = "Hardware" Then
                Call New OpportunityHardwareDetailDAL().Add(objModel, trans)
            ElseIf objModel.OpportunityType = "Support" Then
                Call New OpportunitySupportDetailDAL().Add(objModel, trans)
            End If
            ''TASK TFS4792
            If arrFile.Count > 0 Then
                SaveDocument(objModel.OpportunityId, "frmopportunity", objPath, arrFile, objModel.DocDate, trans)
            End If
            ''END TASK TFS4792
            objModel.ActivityLog.ApplicationName = "CRM"
            objModel.ActivityLog.FormName = "frmopportunity"
            objModel.ActivityLog.FormCaption = "Opportunity"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.Source = "frmopportunity"
            objModel.ActivityLog.User_Name = LoginUser.LoginUserName
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.LogDateTime = Now
            objModel.ActivityLog.RefNo = objModel.DocNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As OpportunityBE) As Boolean
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
    Function Delete(ByVal objModel As OpportunityBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblDefOpportunity  where OpportunityId= " & objModel.OpportunityId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''Delete detail
            If objModel.OpportunityType = "Hardware" Then
                strSQL = "Delete from tblDefOpportunityHardwareDetail  WHERE OpportunityId= " & objModel.OpportunityId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ElseIf objModel.OpportunityType = "Support" Then
                strSQL = "Delete from tblDefOpportunitySupportDetail  WHERE OpportunityId= " & objModel.OpportunityId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            objModel.ActivityLog.ApplicationName = "CRM"
            objModel.ActivityLog.FormCaption = "Opportunity"
            objModel.ActivityLog.FormName = "frmopportunity"
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "CRM"
            objModel.ActivityLog.Source = "frmopportunity"
            objModel.ActivityLog.User_Name = LoginUser.LoginUserName
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.LogDateTime = Now
            objModel.ActivityLog.RefNo = objModel.DocNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        '[OpportunityId] [int] IDENTITY(1,1) NOT NULL,
        '[DocNo] [nvarchar](50) NULL,
        '[DocDate] [datetime] NULL,
        '[CompanyName] [nvarchar](250) NULL,
        '[ContactName] [nvarchar](250) NULL,
        '[EndUserName] [nvarchar](250) NULL,
        '[OpportunityName] [nvarchar](500) NULL,
        '[TypeId] [int] NULL,
        '[CurrencyId] [int] NULL,
        '[OpportunityOwner] [nvarchar](250) NULL,
        '[CloseDate] [datetime] NULL,
        '[StageId] [int] NULL,
        '[LoosReasonId] [int] NULL,
        '[ProbabilityId] [int] NULL,
        '[HardwareContact] [nvarchar](50) NULL,
        '[TaxAmount] [float] NULL,
        '[Duration] [nvarchar](50) NULL,
        '[StartDate] [datetime] NULL,
        '[PaymentId] [int] NULL,
        '[DeliveryId] [int] NULL,
        '[FrequencyId] [int] NULL,
        '[Freight] [float] NULL,
        '[ImplementationTime] [nvarchar](50) NULL,
        '[SupportTypeId] [int] NULL,
        '[MaterialLocation] [nvarchar](500) NULL,
        '[TargetPrice] [float] NULL,
        '[MaintenanceId] [int] NULL,
        '[UserName] [nvarchar](50) NULL,
        '[ModifiedUser] [nvarchar](50) NULL,
        '[ModifiedDate] [datetime] NULL,
        '[OpportunityType] [nvarchar](50) NULL,
        Try
            If LoginUser.LoginUserGroup = "Administrator" Then
                strSQL = " Select tblDefOpportunity.OpportunityId, tblDefOpportunity.DocNo, tblDefOpportunity.DocDate, " _
                     & " tblDefOpportunity.CompanyId, tblDefLeadProfile.CompanyName, tblDefOpportunity.ContactId, tblDefLeadProfileContacts.FirstName +' '+ tblDefLeadProfileContacts.LastName AS ContactPerson, tblDefOpportunity.EndUserId, " _
                     & " tblDefOpportunity.OpportunityName, tblDefOpportunity.TypeId, tblDefOpportunityType.Title AS Type, tblDefOpportunity.CurrencyId, " _
                     & " tblcurrency.currency_code AS Currency, tblDefOpportunity.OpportunityOwner, " _
                     & " tblDefOpportunity.CloseDate, tblDefOpportunity.StageId, tblDefOpportunityStage.Title AS Stage, " _
                     & " tblDefOpportunity.LoosReasonId, tblDefOpportunityLoosReason.Title AS LoosReason, " _
                     & " tblDefOpportunity.ProbabilityId, tblDefOpportunityProbability.Title AS Probability, tblDefOpportunity.ContactName, " _
                     & " tblDefOpportunity.HardwareContact, tblDefOpportunity.TaxAmount, tblDefOpportunity.Duration, tblDefOpportunity.StartDate, " _
                     & " tblDefOpportunity.PaymentId, tblDefOpportunityPayment.Title AS Payment, " _
                     & " tblDefOpportunity.DeliveryId, tblDefOpportunityDelivery.Title AS Delivery, " _
                     & " tblDefOpportunity.FrequencyId, tblDefOpportunityFrequency.Title AS Frequency, tblDefOpportunity.Freight, tblDefOpportunity.ImplementationTime, " _
                     & " tblDefOpportunity.SupportTypeId, tblDefOpportunitySupportType.Title AS SupportType, tblDefOpportunity.MaterialLocation, " _
                     & " tblDefOpportunity.TargetPrice, tblDefOpportunity.MaintenanceId, tblDefOpportunityMaintenance.Title AS Maintenance, tblDefOpportunity.LeadTimeInDays, tblDefOpportunity.UserName, " _
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ChkBoxBatteriesIncluded, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId, tblDefOpportunity.DurationofMonth, tblDefOpportunity.InvoicePattern, tblDefOpportunity.ArticleId, tblDefOpportunity.PMFrequency " _
                     & " FROM tblDefOpportunity  " _
                     & " LEFT OUTER JOIN tblDefOpportunityType ON tblDefOpportunity.TypeId = tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN tblDefLeadProfile ON tblDefOpportunity.CompanyId = tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN tblDefLeadProfileContacts ON tblDefOpportunity.ContactId = tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN tblcurrency ON tblDefOpportunity.CurrencyId = tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN tblDefOpportunityStage ON tblDefOpportunity.StageId = tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityLoosReason ON tblDefOpportunity.LoosReasonId = tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityProbability ON tblDefOpportunity.ProbabilityId = tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityPayment ON tblDefOpportunity.PaymentId = tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityDelivery ON tblDefOpportunity.DeliveryId = tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityFrequency ON tblDefOpportunity.FrequencyId = tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunitySupportType ON tblDefOpportunity.SupportTypeId = tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityMaintenance ON tblDefOpportunity.MaintenanceId = tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN tblDefEmployee ON tblDefOpportunity.EmployeeId = tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId " _
                     & " ORDER BY tblDefOpportunity.OpportunityId DESC "
            Else
                strSQL = " Select tblDefOpportunity.OpportunityId, tblDefOpportunity.DocNo, tblDefOpportunity.DocDate, " _
                     & " tblDefOpportunity.CompanyId, tblDefLeadProfile.CompanyName, tblDefOpportunity.ContactId, tblDefLeadProfileContacts.FirstName +' '+ tblDefLeadProfileContacts.LastName AS ContactPerson, tblDefOpportunity.EndUserId, " _
                     & " tblDefOpportunity.OpportunityName, tblDefOpportunity.TypeId, tblDefOpportunityType.Title AS Type, tblDefOpportunity.CurrencyId, " _
                     & " tblcurrency.currency_code AS Currency, tblDefOpportunity.OpportunityOwner, " _
                     & " tblDefOpportunity.CloseDate, tblDefOpportunity.StageId, tblDefOpportunityStage.Title AS Stage, " _
                     & " tblDefOpportunity.LoosReasonId, tblDefOpportunityLoosReason.Title AS LoosReason, " _
                     & " tblDefOpportunity.ProbabilityId, tblDefOpportunityProbability.Title AS Probability, tblDefOpportunity.ContactName, " _
                     & " tblDefOpportunity.HardwareContact, tblDefOpportunity.TaxAmount, tblDefOpportunity.Duration, tblDefOpportunity.StartDate, " _
                     & " tblDefOpportunity.PaymentId, tblDefOpportunityPayment.Title AS Payment, " _
                     & " tblDefOpportunity.DeliveryId, tblDefOpportunityDelivery.Title AS Delivery, " _
                     & " tblDefOpportunity.FrequencyId, tblDefOpportunityFrequency.Title AS Frequency, tblDefOpportunity.Freight, tblDefOpportunity.ImplementationTime, " _
                     & " tblDefOpportunity.SupportTypeId, tblDefOpportunitySupportType.Title AS SupportType, tblDefOpportunity.MaterialLocation, " _
                     & " tblDefOpportunity.TargetPrice, tblDefOpportunity.MaintenanceId, tblDefOpportunityMaintenance.Title AS Maintenance, tblDefOpportunity.LeadTimeInDays, tblDefOpportunity.UserName, " _
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ChkBoxBatteriesIncluded, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId, tblDefOpportunity.DurationofMonth, tblDefOpportunity.InvoicePattern, tblDefOpportunity.ArticleId, tblDefOpportunity.PMFrequency " _
                     & " FROM tblDefOpportunity  " _
                     & " LEFT OUTER JOIN tblDefOpportunityType ON tblDefOpportunity.TypeId = tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN tblDefLeadProfile ON tblDefOpportunity.CompanyId = tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN tblDefLeadProfileContacts ON tblDefOpportunity.ContactId = tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN tblcurrency ON tblDefOpportunity.CurrencyId = tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN tblDefOpportunityStage ON tblDefOpportunity.StageId = tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityLoosReason ON tblDefOpportunity.LoosReasonId = tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityProbability ON tblDefOpportunity.ProbabilityId = tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityPayment ON tblDefOpportunity.PaymentId = tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityDelivery ON tblDefOpportunity.DeliveryId = tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityFrequency ON tblDefOpportunity.FrequencyId = tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunitySupportType ON tblDefOpportunity.SupportTypeId = tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN tblDefOpportunityMaintenance ON tblDefOpportunity.MaintenanceId = tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN tblDefEmployee ON tblDefOpportunity.EmployeeId = tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId WHERE tblDefOpportunity.UserId in (Select CustomerID from tbluserwisecustomerlist where UserId = " & LoginUser.LoginUserId & " AND tblDefOpportunityStage.id not in (8,9,10)) " _
                     & " ORDER BY tblDefOpportunity.OpportunityId DESC "
            End If
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select OpportunityId, DocNo, DocDate, CompanyName, ContactName, EndUserName, OpportunityName, TypeId, CurrencyId, OpportunityOwner, CloseDate, StageId, LoosReasonId, ProbabilityId, HardwareContact, TaxAmount, Duration, StartDate, PaymentId, DeliveryId, FrequencyId, Freight, ImplementationTime, SupportTypeId, MaterialLocation, TargetPrice, MaintenanceId, UserName, ModifiedUser, ModifiedDate, OpportunityType, OnsiteId, CoverageWindow, OnsiteIntervention, TotalAmount, CountryId from tblDefOpportunity  where OpportunityId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsOpportunityExisted(ByVal OpportunityName As String) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT Count(*) as Count1 FROM tblDefOpportunity WHERE OpportunityName = '" & OpportunityName & "'"
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
    'rafay
    Public Function IsSerialNoExisted(ByVal SerialNo As String) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT Count(*) as Count1 FROM tblDefOpportunitySupportDetail WHERE SerialNo = '" & SerialNo & "'"
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

            'Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                'Altered Against Task#2015060001 Ali Ansari
                'Marked Against Task#2015060001 Ali Ansari
                '            If arrFile.Length > 0 Then
                'Marked Against Task#2015060001 Ali Ansari
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
End Class
