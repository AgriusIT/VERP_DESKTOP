Public Class ReturnFromFactoryMasterBE
    Public Property ID As Integer
    Public Property ReturnNo As String
    Public Property ReturnDate As DateTime
    Public Property PartyId As Integer
    Public Property Party As String
    Public Property DriverName As String
    Public Property VehicleNo As String
    Public Property Remarks As String
    Public Property Detail As List(Of ReturnFromFactoryDetailBE)
    Public Property Stock As StockMaster
    Public Property IsPosted As Boolean
    Public Property ReturnToFactoryId As Integer
    Public Property ActivityLog() As ActivityLog
    Public Sub New()
        Detail = New List(Of ReturnFromFactoryDetailBE)
        Stock = New StockMaster()
    End Sub
End Class
