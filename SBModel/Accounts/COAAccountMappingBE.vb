Public Class COAAccountMappingBE
    Public Property COAGroupMappingId As Integer
    Public Property COAGroupId As Integer
    Public Property AccountId As Integer
    Public Property AccountLevel As Integer

    Public Property MainAccount As List(Of COAMainAccountMappingBE)
    Public Property SubAccount As List(Of COASubAccountMappingBE)
    Public Property SubSubAccount As List(Of COASubSubAccountMappingBE)
    Public Property DetailAccount As List(Of COADetailAccountMappingBE)
    Public Property ActivityLog() As ActivityLog
End Class

Public Class COADetailAccountMappingBE
   
    Public Property AccountId As Integer
    Public Property AccountLevel As Integer

End Class
Public Class COASubSubAccountMappingBE

    Public Property AccountId As Integer
    Public Property AccountLevel As Integer

End Class
Public Class COASubAccountMappingBE

    Public Property AccountId As Integer
    Public Property AccountLevel As Integer

End Class
Public Class COAMainAccountMappingBE

    Public Property AccountId As Integer
    Public Property AccountLevel As Integer

End Class
