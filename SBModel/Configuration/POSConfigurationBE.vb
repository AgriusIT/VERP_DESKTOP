Public Class POSConfigurationBE

    Public Property POSId As Integer

    Public Property POSTitle As String

    Public Property CompanyId As Integer

    Public Property LocationId As Integer

    Public Property CostCenterId As Integer

    Public Property CashAccountId As Integer

    Public Property BankAccountId As Integer

    Public Property SalesPersonId As Integer

    Public Property DeliveryOption As Boolean

    Public Property DiscountPer As Double

    Public Property Active As Boolean

    Public Property Detail As List(Of POSDetailConfigurationBE)
End Class

Public Class POSDetailConfigurationBE
    Public Property BankAccountId2 As Integer

    Public Property CreditCardId As Integer

    Public Property MachineTitle As String

    Public Property POSTitle As String
End Class
