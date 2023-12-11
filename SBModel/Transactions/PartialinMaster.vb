''12-jan-2018 task# 2087   Muhammad Abdullah new form created
Public Class PartialinMasterBE

    Public Property Id As Integer
    Public Property DocNo As String
    Public Property ReceivingDate As DateTime
    Public Property Remarks As String
    Public Property Detail As List(Of PartialinDetailBE)
    Sub New()
        Detail = New List(Of PartialinDetailBE)
    End Sub
End Class
Public Class PartialinDetailBE

    Public Property InDetailId As Integer
    Public Property PartialInMasterId As Integer
    Public Property outDetailSrNo As Integer
    Public Property outMasterId As Integer
    Public Property InQuantity As Integer
    Public Property Remarks As String
End Class
