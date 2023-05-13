Public Class ProfitSharingMasterBE
    Public Property ProfitSharingId As Integer
    Public Property PropertyProfileId As Integer
    Public Property SharingDate As DateTime
    Public Property voucher_no As String
    Public Property ProfitForDistribution As Decimal
    Public Property Detail As List(Of ProfitSharingDetailBE)
    Public Property ActivityLog() As ActivityLog
    Public Property Voucher As VouchersMaster

    Sub New()
        Detail = New List(Of ProfitSharingDetailBE)

        Voucher = New VouchersMaster()
    End Sub
End Class
