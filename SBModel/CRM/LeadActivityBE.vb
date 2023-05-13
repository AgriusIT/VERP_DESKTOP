Public Class LeadActivityBE
    Public Property ActivityId As Integer
    Public Property LeadId As Integer
    Public Property LeadContactId As Integer
    Public Property LeadOfficeId As Integer
    Public Property LeadActivityTypeID As Integer
    Public Property ActivityDate As DateTime


    Public Property Address As String
    Public Property ActivityTime As DateTime
    Public Property IsConfirmed As Boolean
    Public Property ResponsiblePerson_Employee_Id As Integer
    Public Property InsideSalesPerson_Employee_Id As String
    Public Property Manager_Employee_Id As Integer
    Public Property Objective As String

    Public Property ProjectId As Integer
    Public Property ActivityLog() As ActivityLog

End Class
