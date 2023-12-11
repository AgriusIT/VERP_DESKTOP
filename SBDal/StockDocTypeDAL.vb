Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class StockDocTypeDAL
    Public Shared Function GetStockDocTypeId(ByVal ByDocType As String) As Integer
        Try
            Dim str As String = String.Empty
            str = "Select StockDocTypeId From Stock_Document_Type WHERE StockDocType=N'" & ByDocType & "'"
            Dim dt As DataTable
            dt = UtilityDAL.GetDataTable(str)
            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
