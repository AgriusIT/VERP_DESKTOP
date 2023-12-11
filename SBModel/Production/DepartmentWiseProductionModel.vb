Imports SBModel
Public Class DepartmentWiseProductionModel
    Public Property ID As Integer
    Public Property DocNo As String
    Public Property DwpDate As DateTime
    Public Property DepartmentID As Integer
    Public Property ReferenceNo As String
    Public Property SpecialInstructions As String
    Public Property Detail As List(Of DepartmentWiseProductionDetailsModel)
    Public Sub New()
        Detail = New List(Of DepartmentWiseProductionDetailsModel)
    End Sub
End Class
