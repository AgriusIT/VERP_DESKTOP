Public Class AssetDepriciationDetailsBE

    Public Property DepriciationDetailsID As Integer
    Public Property Asset_Id As Integer
    Public Property Rate As Decimal
    Public Property DepriciationAmount As Decimal
    Public Property Closing_Value As Decimal
    Public Property DepriciationMasterID As Integer

    Public Property DepriciationMonths As Integer

    Public Property CurrentValue As Integer
    Public Property ActivityLog() As ActivityLog


End Class
