Public Class InvestorBE
    Public Property InvestorId As Integer
    Public Property Name As String
    Public Property PrimaryMobile As String
    Public Property SecondaryMobile As String
    Public Property coa_detail_id As Integer
    Public Property ProfitRatio As Decimal
    Public Property Remarks As String
    Public Property Active As Boolean
    Public Property CNIC As String
    Public Property Email As String
    Public Property AddressLine1 As String
    Public Property AddressLine2 As String
    Public Property CityId As Integer
    Public Property ActivityLog() As ActivityLog
End Class
