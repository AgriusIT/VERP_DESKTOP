'Rafay:created this screen to show detail report
Imports SBDal
Imports SBModel
Imports System.Data.OleDb



Public Class frmCentralizeOpportunity
    Implements IGeneral

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strSQL As String = String.Empty
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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS1_DB].[dbo].tblDefOpportunity   " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityType ON tblDefOpportunity.TypeId = [SIRIUS1_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfile ON tblDefOpportunity.CompanyId = [SIRIUS1_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts ON tblDefOpportunity.ContactId = [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblcurrency ON tblDefOpportunity.CurrencyId =[SIRIUS1_DB].[dbo].tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityStage ON tblDefOpportunity.StageId = [SIRIUS1_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason ON tblDefOpportunity.LoosReasonId = [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityProbability ON tblDefOpportunity.ProbabilityId = [SIRIUS1_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityPayment ON tblDefOpportunity.PaymentId = [SIRIUS1_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery ON tblDefOpportunity.DeliveryId = [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency ON tblDefOpportunity.FrequencyId = [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType ON tblDefOpportunity.SupportTypeId = [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance ON tblDefOpportunity.MaintenanceId = [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefEmployee ON tblDefOpportunity.EmployeeId = [SIRIUS1_DB].[dbo].tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId " _
                     & " union all" _
                     & " Select tblDefOpportunity.OpportunityId, tblDefOpportunity.DocNo, tblDefOpportunity.DocDate, " _
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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS_UAE_DB].[dbo].tblDefOpportunity   " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.TypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.CompanyId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ContactId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblcurrency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.CurrencyId = [SIRIUS_UAE_DB].[dbo].tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.StageId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.LoosReasonId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ProbabilityId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.PaymentId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.DeliveryId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.FrequencyId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.SupportTypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.MaintenanceId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefEmployee ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.EmployeeId = [SIRIUS_UAE_DB].[dbo].tblDefEmployee.Employee_ID " _
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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS1_DB].[dbo].tblDefOpportunity  " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityType ON [SIRIUS1_DB].[dbo].tblDefOpportunity.TypeId = [SIRIUS1_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfile ON [SIRIUS1_DB].[dbo].tblDefOpportunity.CompanyId = [SIRIUS1_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts ON [SIRIUS1_DB].[dbo].tblDefOpportunity.ContactId = [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblcurrency ON [SIRIUS1_DB].[dbo].tblDefOpportunity.CurrencyId = tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityStage ON [SIRIUS1_DB].[dbo].tblDefOpportunity.StageId = [SIRIUS1_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason ON [SIRIUS1_DB].[dbo].tblDefOpportunity.LoosReasonId = [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityProbability ON [SIRIUS1_DB].[dbo].tblDefOpportunity.ProbabilityId = [SIRIUS1_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityPayment ON [SIRIUS1_DB].[dbo].tblDefOpportunity.PaymentId = [SIRIUS1_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery ON [SIRIUS1_DB].[dbo].tblDefOpportunity.DeliveryId = [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency ON [SIRIUS1_DB].[dbo].tblDefOpportunity.FrequencyId = [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType ON [SIRIUS1_DB].[dbo].tblDefOpportunity.SupportTypeId = [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance ON [SIRIUS1_DB].[dbo].tblDefOpportunity.MaintenanceId = [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefEmployee ON [SIRIUS1_DB].[dbo].tblDefOpportunity.EmployeeId = [SIRIUS1_DB].[dbo].tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId WHERE tblDefOpportunityStage.id not in (8,9,10) " _
                     & " union all" _
                     & " Select tblDefOpportunity.OpportunityId, tblDefOpportunity.DocNo, tblDefOpportunity.DocDate, " _
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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS_UAE_DB].[dbo].tblDefOpportunity  " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.TypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile ON [SIRIUS_UAE_DB].[dbo]. tblDefOpportunity.CompanyId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ContactId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblcurrency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.CurrencyId = [SIRIUS_UAE_DB].[dbo].tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.StageId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.LoosReasonId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ProbabilityId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.PaymentId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.DeliveryId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.FrequencyId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.SupportTypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.MaintenanceId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefEmployee ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.EmployeeId = [SIRIUS_UAE_DB].[dbo].tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId where tblDefOpportunityStage.id not in (8,9,10) " _
                     & " ORDER BY tblDefOpportunity.OpportunityId DESC "

            End If
            Dim dt As DataTable
            dt = GetDataTable(strSQL)
            'grd.RootTable.Columns("ContractId").Visible = False
            'grd.RootTable.Columns("TicketId").Visible = False
            'grd.RootTable.Columns("Id").Visible = False
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub


    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        '    Try
        '        If LoginGroup = "Administrator" Then
        '            Me.Visible = True
        '            DoHaveSaveRights = True
        '            DoHaveUpdateRights = True
        '            DoHaveDeleteRights = True
        '            Me.CtrlGrdBar1.mGridPrint.Enabled = True
        '            Me.CtrlGrdBar1.mGridExport.Enabled = True
        '            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
        '            Exit Sub
        '        End If
        '        If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
        '            If RegisterStatus = EnumRegisterStatus.Expired Then
        '                Me.Visible = False
        '                DoHaveSaveRights = False
        '                DoHaveUpdateRights = False
        '                DoHaveDeleteRights = False
        '                Me.CtrlGrdBar1.mGridPrint.Enabled = False
        '                Me.CtrlGrdBar1.mGridExport.Enabled = False
        '                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
        '                Exit Sub
        '            End If
        '        Else
        '            Me.Visible = False
        '            DoHaveSaveRights = False
        '            DoHaveUpdateRights = False
        '            DoHaveDeleteRights = False
        '            Me.CtrlGrdBar1.mGridPrint.Enabled = False
        '            Me.CtrlGrdBar1.mGridExport.Enabled = False
        '            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
        '            For Each RightsDt As GroupRights In Rights
        '                If RightsDt.FormControlName = "View" Then
        '                    Me.Visible = True
        '                ElseIf RightsDt.FormControlName = "Print" Then
        '                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Export" Then
        '                    Me.CtrlGrdBar1.mGridExport.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Field Chooser" Then
        '                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Save" Then
        '                    DoHaveSaveRights = True
        '                ElseIf RightsDt.FormControlName = "Update" Then
        '                    DoHaveUpdateRights = True
        '                ElseIf RightsDt.FormControlName = "Done" Then
        '                    DoHaveDeleteRights = True
        '                End If
        '            Next
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
    End Sub


    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs)

    End Sub

    Private Sub frmTicketDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GetAllRecords()
        GetSecurityRights()
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                ''  Me.btnSave.Enabled = True
                'Rafay:give rights to admin to view print ,export,choosefielder 
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                '' DoHaveDeleteRights = True
                'Rafay:Task End
                Exit Sub
            End If
            Me.Visible = False
            'Me.btnSave.Enabled = False
            'Rafay:Task Start
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            'Rafay:Task End
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True

                End If

                'Rafay:Task End
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim strSQL As String = String.Empty

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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS1_DB].[dbo].tblDefOpportunity   " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityType ON tblDefOpportunity.TypeId = [SIRIUS1_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfile ON tblDefOpportunity.CompanyId = [SIRIUS1_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts ON tblDefOpportunity.ContactId = [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblcurrency ON tblDefOpportunity.CurrencyId =[SIRIUS1_DB].[dbo].tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityStage ON tblDefOpportunity.StageId = [SIRIUS1_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason ON tblDefOpportunity.LoosReasonId = [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityProbability ON tblDefOpportunity.ProbabilityId = [SIRIUS1_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityPayment ON tblDefOpportunity.PaymentId = [SIRIUS1_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery ON tblDefOpportunity.DeliveryId = [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency ON tblDefOpportunity.FrequencyId = [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType ON tblDefOpportunity.SupportTypeId = [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance ON tblDefOpportunity.MaintenanceId = [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefEmployee ON tblDefOpportunity.EmployeeId = [SIRIUS1_DB].[dbo].tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId where (((tblDefOpportunity.DocDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (tblDefOpportunity.DocDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "'))" _
                     & " union all" _
                     & " Select tblDefOpportunity.OpportunityId, tblDefOpportunity.DocNo, tblDefOpportunity.DocDate, " _
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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS_UAE_DB].[dbo].tblDefOpportunity   " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.TypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.CompanyId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ContactId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblcurrency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.CurrencyId = [SIRIUS_UAE_DB].[dbo].tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.StageId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.LoosReasonId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ProbabilityId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.PaymentId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.DeliveryId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.FrequencyId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.SupportTypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.MaintenanceId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefEmployee ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.EmployeeId = [SIRIUS_UAE_DB].[dbo].tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId  where (((tblDefOpportunity.DocDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (tblDefOpportunity.DocDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "'))  " _
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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS1_DB].[dbo].tblDefOpportunity  " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityType ON [SIRIUS1_DB].[dbo].tblDefOpportunity.TypeId = [SIRIUS1_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfile ON [SIRIUS1_DB].[dbo].tblDefOpportunity.CompanyId = [SIRIUS1_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts ON [SIRIUS1_DB].[dbo].tblDefOpportunity.ContactId = [SIRIUS1_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblcurrency ON [SIRIUS1_DB].[dbo].tblDefOpportunity.CurrencyId = tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityStage ON [SIRIUS1_DB].[dbo].tblDefOpportunity.StageId = [SIRIUS1_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason ON [SIRIUS1_DB].[dbo].tblDefOpportunity.LoosReasonId = [SIRIUS1_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityProbability ON [SIRIUS1_DB].[dbo].tblDefOpportunity.ProbabilityId = [SIRIUS1_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityPayment ON [SIRIUS1_DB].[dbo].tblDefOpportunity.PaymentId = [SIRIUS1_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery ON [SIRIUS1_DB].[dbo].tblDefOpportunity.DeliveryId = [SIRIUS1_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency ON [SIRIUS1_DB].[dbo].tblDefOpportunity.FrequencyId = [SIRIUS1_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType ON [SIRIUS1_DB].[dbo].tblDefOpportunity.SupportTypeId = [SIRIUS1_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance ON [SIRIUS1_DB].[dbo].tblDefOpportunity.MaintenanceId = [SIRIUS1_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS1_DB].[dbo].tblDefEmployee ON [SIRIUS1_DB].[dbo].tblDefOpportunity.EmployeeId = [SIRIUS1_DB].[dbo].tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId WHERE tblDefOpportunityStage.id not in (8,9,10) and (((tblDefOpportunity.DocDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (tblDefOpportunity.DocDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "')) " _
                     & " union all" _
                     & " Select tblDefOpportunity.OpportunityId, tblDefOpportunity.DocNo, tblDefOpportunity.DocDate, " _
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
                     & " tblDefOpportunity.ModifiedUser, tblDefOpportunity.ModifiedDate, tblDefOpportunity.OpportunityType, Att.[No Of Attachment], tblDefOpportunity.EmployeeId, tblDefEmployee.Employee_Name AS EmployeeName, tblDefOpportunity.RequestTime, tblDefOpportunity.QuoteTime,tblDefOpportunity.OnsiteId, tblDefOpportunity.CoverageWindow, tblDefOpportunity.OnsiteIntervention, tblDefOpportunity.TotalAmount, tblDefOpportunity.CountryId " _
                     & " FROM [SIRIUS_UAE_DB].[dbo].tblDefOpportunity  " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.TypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile ON [SIRIUS_UAE_DB].[dbo]. tblDefOpportunity.CompanyId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfile.LeadProfileId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ContactId = [SIRIUS_UAE_DB].[dbo].tblDefLeadProfileContacts.ContactId " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblcurrency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.CurrencyId = [SIRIUS_UAE_DB].[dbo].tblcurrency.currency_id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.StageId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityStage.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.LoosReasonId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityLoosReason.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.ProbabilityId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityProbability.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.PaymentId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityPayment.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.DeliveryId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityDelivery.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.FrequencyId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityFrequency.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.SupportTypeId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunitySupportType.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.MaintenanceId = [SIRIUS_UAE_DB].[dbo].tblDefOpportunityMaintenance.Id " _
                     & " LEFT OUTER JOIN [SIRIUS_UAE_DB].[dbo].tblDefEmployee ON [SIRIUS_UAE_DB].[dbo].tblDefOpportunity.EmployeeId = [SIRIUS_UAE_DB].[dbo].tblDefEmployee.Employee_ID " _
                     & " LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'frmopportunity' Group By DocId) Att On Att.DocId =  tblDefOpportunity.OpportunityId where tblDefOpportunityStage.id not in (8,9,10) and (((tblDefOpportunity.DocDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (tblDefOpportunity.DocDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "')) " _
                     & " ORDER BY tblDefOpportunity.OpportunityId DESC "

            End If
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnLoadAll.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSearch_Click_1(sender As Object, e As EventArgs) Handles btnSearch.Click

    End Sub

    Private Sub CtrlGrdBar1_Load_1(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load

    End Sub
End Class
