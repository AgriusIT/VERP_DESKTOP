Public Class AssetCategoryBE


    Private _Asset_Category_Id As Integer
    Public Property Asset_Category_Id() As Integer
        Get
            Return _Asset_Category_Id
        End Get
        Set(ByVal value As Integer)
            _Asset_Category_Id = value
        End Set
    End Property


    Private _Asset_Category_Name As String
    Public Property Asset_Category_Name() As String
        Get
            Return _Asset_Category_Name
        End Get
        Set(ByVal value As String)
            _Asset_Category_Name = value
        End Set
    End Property


    Private _Asset_Category_Description As String
    Public Property Asset_Category_Description() As String
        Get
            Return _Asset_Category_Description
        End Get
        Set(ByVal value As String)
            _Asset_Category_Description = value
        End Set
    End Property


    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
        End Set
    End Property


    Private _Active As Boolean
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property
    Public Property Code As String

    Public Property AssetAccount_coa_detail_id As Integer

    Public Property Title As String
    Public Property ExpenseAccount_coa_detail_id As Integer
    Public Property AccumulativeAccount_coa_detail_id As Integer
    Public Property DepreciationMethod As String
    Public Property Frequency As String
    Public Property Rate As Decimal
    Public Property Remarks As String
    Public Property ActivityLog() As ActivityLog



End Class
