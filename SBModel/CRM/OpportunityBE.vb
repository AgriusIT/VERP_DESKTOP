Public Class OpportunityBE
    Public Property OpportunityId As Integer
    Public Property DocNo As String = ""
    Public Property DocDate As DateTime
    Public Property CompanyId As Integer
    Public Property ContactId As Integer
    Public Property EndUserId As String
    Public Property OpportunityName As String = ""
    Public Property TypeId As Integer
    Public Property CurrencyId As Integer
    Public Property OpportunityOwner As String = ""
    Public Property CloseDate As DateTime
    Public Property StageId As Integer
    Public Property OnsiteId As String
    Public Property LoosReasonId As Integer
    Public Property ProbabilityId As Integer
    Public Property ContactName As String = ""
    Public Property HardwareContact As String = ""
    Public Property TaxAmount As Double
    Public Property Duration As String = ""
    Public Property StartDate As DateTime
    Public Property PaymentId As Integer
    Public Property CountryId As Integer
    Public Property DeliveryId As Integer
    Public Property FrequencyId As Integer
    Public Property Freight As Double
    Public Property ImplementationTime As String = ""
    Public Property CoverageWindow As String = ""
    Public Property TotalAmount As Double
    Public Property OnsiteIntervention As String = ""
    Public Property SupportTypeId As Integer
    Public Property MaterialLocation As String = ""
    Public Property TargetPrice As Double
    Public Property MaintenanceId As Integer
    Public Property UserName As String = ""
    Public Property ModifiedUser As String = ""
    Public Property ModifiedDate As DateTime
    Public Property LeadTimeInDays As String = ""
    Public Property OpportunityType As String = ""
    Public Property ActivityLog() As ActivityLog
    Public Property SupportDetail As List(Of OpportunitySupportDetailBE)
    Public Property HardwareDetail As List(Of OpportunityHardwareDetailBE)
    Public Property NoOfAttachments As Integer
    Public Property EmployeeId As Integer
    Public Property UserId As Integer
    'rafay add ChkBoxBatteriesIncluded 4-11-22
    Public Property ChkBoxBatteriesIncluded As Boolean
    Public Property EmployeeName As String = ""
    Public Property InvoicePattern As String = ""
    Public Property DurationofMonth As String = ""
    Public Property ArticleId As Integer



    Public Sub New()
        SupportDetail = New List(Of OpportunitySupportDetailBE)
        HardwareDetail = New List(Of OpportunityHardwareDetailBE)
        ActivityLog = New ActivityLog()
    End Sub
End Class
