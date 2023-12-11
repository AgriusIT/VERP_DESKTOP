Public Class AssetDepriciationMasterBE

    Public Property DepriciationMasterID As Integer
    Public Property DocumentNo As String
    Public Property Details As String
    Public Property DepriciationMonth As DateTime
    Public Property EntryDate As DateTime
    Public Property ActivityLog() As ActivityLog
    Public Property Voucher As VouchersMaster
    Public Property Detail As List(Of AssetDepriciationDetailsBE)

    Sub New()
        ActivityLog = New ActivityLog()

        Detail = New List(Of AssetDepriciationDetailsBE)

        Voucher = New VouchersMaster()

    End Sub

End Class

