Public Class PropertyProfileBE
    Public Property PropertyProfileId As Integer
    Public Property DocNo As String
    Public Property DocDate As DateTime
    Public Property InvId As Integer
    Public Property Branch As New BEBranch
    Public Property Status As String
    Public Property CostCenterId As Integer
    Public Property CommissionAmount As Double
    Public Property Margin As Double
    Public Property ActivityLog() As ActivityLog

    Public Property ModifiedUser As String

    Public Property ModifiedDate As DateTime

    Public Property InvName As String
End Class
