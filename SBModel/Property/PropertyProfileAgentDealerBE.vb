Public Class PropertyProfileAgentDealerBE
    Public Property PropertyProfileAgentId As Integer
    Public Property PropertyProfileDealerId As Integer
    Public Property PropertyProfileId As Integer
    Public Property name As String
    Public Property AgentId As Integer
    Public Property DealerId As Integer
    Public Property Activity As String
    Public Property CommissionAmount As Decimal
    Public Property Remarks As String
    Public Property CommissionAccount As String
    Public Property VoucherNo As String
    Public Property VoucherDate As DateTime
    Public Property PlotNo As String
    Public Property ActivityLog() As ActivityLog
End Class