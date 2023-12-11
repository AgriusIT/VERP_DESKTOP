Public Class OpportunitySupportDetailBE
    Public Property OpportunitySupportDetailId As Integer
    Public Property OpportunityId As Integer
    Public Property Brand As String
    Public Property ModelNo As String
    Public Property SerialNo As String
    Public Property SLA As String
    Public Property OnsiteIntervention As String
    Public Property SLAFixTime As String
    Public Property SLAInterventionTime As String
    Public Property SLACoverage As String
    Public Property Address As String
    Public Property City As String
    Public Property Province As String
    Public Property Country As String
    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
    Public Property Type As String
    Public Property UnitPrice As Double
    Public Property ActivityLog() As ActivityLog
    Public Property FilePath As String


End Class
