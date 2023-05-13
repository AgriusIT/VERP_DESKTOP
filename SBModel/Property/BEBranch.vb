Public Class BEBranch
    Sub New()

    End Sub
    Sub New(Branch_Id As Integer)
        BranchId = Branch_Id
    End Sub
    Sub New(Branch_Id As Integer, BranchName As String)
        BranchId = Branch_Id
        Name = BranchName
    End Sub

    Public Property BranchId As Integer
    Public Property Name As String
    Public Property AreaId As Integer
    Public Property Employee_ID As Integer
    Public Property AddressLine1 As String
    Public Property AddressLine2 As String
    Public Property LandlinePhone As String
    Public Property CellPhone As String
    Public Property Remarks As String
    Public Property CityId As Integer
    Public Property Active As Boolean
End Class
