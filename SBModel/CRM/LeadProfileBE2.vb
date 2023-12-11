Public Class LeadProfileBE2
    Public Property LeadProfileId As Integer
    Public Property DocNo As String
    Public Property DocDate As DateTime
    Public Property TypeId As Integer
    Public Property CompanyName As String
    Public Property Address As String
    Public Property AccountId As Integer
    Public Property ActivityId As Integer
    Public Property SourceId As Integer
    Public Property IndustryId As Integer
    Public Property Status As String
    Public Property InterestedInId As String
    Public Property BrandFocusId As String
    Public Property NoofEmployeeId As Integer
    Public Property Remarks As String
    Public Property UserName As String
    Public Property ModifiedUser As String
    Public Property ModifiedDate As DateTime
    Public Property ActivityLog() As ActivityLog
    Public Property Detail As List(Of LeadProfileContactsBE)
    Public Property IsAccountCreated As Boolean
    Public Property NoOfAttachments As Integer
    Public Property EmployeeId As Integer
    Public Property EmployeeName As String = ""
    Public Property CountryId As Integer
    Public Property Website As String
    Public Property CountryName As String


    Public Sub New()
        Detail = New List(Of LeadProfileContactsBE)
    End Sub
End Class
