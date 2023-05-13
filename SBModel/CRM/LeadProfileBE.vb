Public Class LeadProfileBE
    Public Property LeadId As Integer
    Public Property LeadTitle As String
    Public Property SectorId As Integer
    Public Property ProductName As String
    Public Property StatusId As Integer
    Public Property StatusRemarks As String
    Public Property SourceId As Integer
    Public Property SourceRemarks As String
    Public Property ResponsibleId As Integer
    Public Property InsideSalesId As Integer
    Public Property ManagerId As Integer
    Public Property ActivityLog() As ActivityLog

    Public Property Active As Boolean

    Public Property PDetail As List(Of ConcernedPersonBE)

    Public Property TDetail As List(Of LeadOfficeBE)

End Class

Public Class ConcernedPersonBE

    Public Property LeadConcernedId As Integer

    Public Property LeadId As Integer

    Public Property ConcernPerson As String

    Public Property Designation As String

    Public Property Phoneno As String

    Public Property Email As String

End Class

Public Class LeadOfficeBE

    Public Property LeadOfficeId As Integer

    Public Property LeadId As Integer

    Public Property Name As String

    Public Property Address As String

    Public Property Website As String

End Class
