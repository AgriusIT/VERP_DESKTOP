Public Class SalesQuotationMaster
    Public Property QuotationId As Integer
    Public Property LocationId As Integer
    Public Property QuotationNo As String
    Public Property QuotationDate As DateTime
    Public Property VendorId As Integer
    Public Property PartyInvoiceNo As String
    Public Property PartySlipNo As String
    Public Property SalesOrderQty As Integer
    Public Property SalesOrderAmount As Integer
    Public Property CashPaid As Decimal
    Public Property Remarks As String
    Public Property UserName As String
    Public Property Status As String
    Public Property SpecialAdjustment As Double
    Public Property Posted As Boolean
    Public Property PONo As String
    Public Property NewCustomer As String
    Public Property Delivery_Date As DateTime
    Public Property Adj_Flag As Boolean
    Public Property Adjustment As Double
    Public Property CostCenterId As Double
    Public Property PO_Date As DateTime
    Public Property EditionalTax_Percentage As Double
    Public Property SED_Percentage As Double
    Public Property Terms_And_Condition As String
    Public Property Apprved As Boolean
    Public Property CustomerPhone As String
    Public Property CustomerAddress As String
    Public Property VerifiedBy As String
    Public Property Approved_User As String
    Public Property RevisionNumber As Integer
    Public Property TermsAndConditionId As Integer
    Public Property SalesInquiryId As Integer
    Public Property DetailList As List(Of SalesQuotationDetail)
    Public Sub New()
        DetailList = New List(Of SalesQuotationDetail)
    End Sub
End Class
