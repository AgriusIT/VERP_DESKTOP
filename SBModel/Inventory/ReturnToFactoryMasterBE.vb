Public Class ReturnToFactoryMasterBE
    Public Property ID As Integer
    Public Property ReturnNo As String
    Public Property ReturnDate As datetime
    Public Property PartyId As Integer
    Public Property Party As String
    Public Property DriverName As String
    Public Property VehicleNo As String
    Public Property Remarks As String
    Public Property Detail As List(Of ReturnToFactoryDetailBE)
    Public Property Stock As StockMaster
    Public Property IsPosted As Boolean
    Public Property Status As String
    Public Property ActivityLog() As ActivityLog
    Public Sub New()
        Detail = New List(Of ReturnToFactoryDetailBE)
        Stock = New StockMaster()
    End Sub
End Class

