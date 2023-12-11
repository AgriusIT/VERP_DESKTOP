Public Class BEAgent
    Public Property AgentId As Integer
    Public Property Name As String
    Public Property FathersName As String
    Public Property CNIC As String
    Public Property PrimaryMobile As String
    Public Property SecondaryMobile As String
    Public Property coa_detail_id As Integer
    Public Property AddressLine1 As String
    Public Property AddressLine2 As String
    Public Property CityId As Integer
    Public Property SpecialityId As Integer
    Public Property Email As String
    Public Property Active As Boolean
    Public Property Account As COADeail
    Public Property AccountTitle As String
    Public Property Remarks As String
    Public Property BloodGroup As String
    Sub New()
        Account = New COADeail()
    End Sub
End Class
