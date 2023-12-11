Public Class InvestmentBookingBE
    Public Property InvestmentBookingId As Integer
    Public Property PropertyProfileId As Integer

    Public Property PropertyProfile As String
    Public Property BookingDate As datetime
    Public Property VoucherNo As String
    Public Property InvestorId As Integer

    Public Property PLotNo As String
    Public Property Investor As String
    Public Property InvestmentAmount As Decimal
    Public Property Remarks As String
    Public Property InvestmentAccountId As Integer
    Public Property VoucherId As Integer
    Public Property ProfitPercentage As Decimal
    Public Property InvestmentRequired As Decimal
    Public Property ActivityLog() As ActivityLog
End Class