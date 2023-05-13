Public Class ItemConsumptionVoucher
    'location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
    '                                              & " cheque_no, cheque_date,post,Source,voucher_code,Remarks
    Public Property LocationId As Integer
    Public Property FinancialYearId As Integer
    Public Property VoucherTypeId As Integer
    Public Property VoucherNo As String
    Public Property VoucherDate As DateTime
    Public Property ChequeNo As String
    Public Property ChequeDate As DateTime
    Public Property Post As Boolean
    Public Property Source As String
    Public Property VoucherCode As String
    Public Property Remarks As String
    Public Property Detail As List(Of ItemConsumptionVoucherDetail)
    Sub New()
        Detail = New List(Of ItemConsumptionVoucherDetail)
    End Sub
End Class
