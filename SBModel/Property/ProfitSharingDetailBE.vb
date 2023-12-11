Public Class ProfitSharingDetailBE
    Public Property ProfitSharingDetailId As Integer
    Public Property ProfitSharingId As Integer
    Public Property InvestorId As Integer
    Public Property AdjustmentAmount As Decimal
    Public Property NetProfitAmount As Decimal
    Public Property InvestmentBookingId As Integer
    Public Property InvestmentAccountId As Integer
    Public Property ProfitExpenseAccountId As Integer
    Public Property ActivityLog() As ActivityLog
End Class
