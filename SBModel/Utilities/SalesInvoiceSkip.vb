Public Class SalesInvoiceSkip
    '   [SalesInvoiceSkipId] [int] IDENTITY(1,1) NOT NULL,
    '[SalesInvoiceSkipNo] [nvarchar](50) NULL,
    '[Reason] [nvarchar](300) NULL,

    Public Property SalesInvoiceSkipId As Integer
    Public Property SalesInvoiceSkipNo As String
    Public Property Reason As String
    Public Property CompanyId As Integer
End Class
