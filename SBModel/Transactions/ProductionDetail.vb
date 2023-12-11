'Task No 2555 Mughees 13-apil 2014 Add9ing New Feild Of UOM In this Concerned Class 
''16-June-2014 TASK:2690 Imran Ali Add Department and Employee Fields On Production Entry
Public Class ProductionDetail
    Private _Productiondetail_ID As Integer
    Private _Production_ID As Integer
    Private _Location_ID As Integer
    Private _ArticleDefID As Integer
    Private _ArticleSize As String
    Private _Sz1 As Double
    Private _Sz2 As Double
    Private _Sz3 As Double
    Private _Sz4 As Double
    Private _Sz5 As Double
    Private _Sz6 As Double
    Private _Sz7 As Double
    Private _Qty As Double
    Private _CurrentRate As Double
    Private _Comments As String
    'Task No 2555 Mughees 13-apil 2014 Added New Feild Of UOM In this Concerned Class 
    Private _Uom As String
    Private _PurchaseAccountId As Integer = 0I
    Private _CGSAccountId As Integer = 0I
    Private _EngineNo As String
    Private _ChasisNo As String
    'Task No 1616 Adding Coloumns 
    Private _BatchNo As String
    Private _RetailPrice As Double
    Private _ExpiryDate As DateTime
    'Task No 1772 Adding Coloumns 
    Private _Dim1 As Double
    Private _Dim2 As Double


   

    
 


    Public Property ProductionDetailId() As Integer
        Get
            Return _Productiondetail_ID
        End Get
        Set(ByVal value As Integer)
            _Productiondetail_ID = value
        End Set
    End Property
    Public Property Production_ID() As Integer
        Get
            Return _Production_ID
        End Get
        Set(ByVal value As Integer)
            _Production_ID = value
        End Set
    End Property
    Public Property Location_ID() As Integer
        Get
            Return _Location_ID
        End Get
        Set(ByVal value As Integer)
            _Location_ID = value
        End Set
    End Property
    Public Property ArticledefID() As Integer
        Get
            Return _ArticleDefID
        End Get
        Set(ByVal value As Integer)
            _ArticleDefID = value
        End Set
    End Property
    Public Property ArticleSize() As String
        Get
            Return _ArticleSize
        End Get
        Set(ByVal value As String)
            _ArticleSize = value
        End Set
    End Property
    Public Property Sz1() As Double
        Get
            Return _Sz1
        End Get
        Set(ByVal value As Double)
            _Sz1 = value
        End Set
    End Property
    Public Property Sz2() As Double
        Get
            Return _Sz2
        End Get
        Set(ByVal value As Double)
            _Sz2 = value
        End Set
    End Property
    Public Property Sz3() As Double
        Get
            Return _Sz3
        End Get
        Set(ByVal value As Double)
            _Sz3 = value
        End Set
    End Property
    Public Property Sz4() As Double
        Get
            Return _Sz4
        End Get
        Set(ByVal value As Double)
            _Sz4 = value
        End Set
    End Property
    Public Property Sz5() As Double
        Get
            Return _Sz5
        End Get
        Set(ByVal value As Double)
            _Sz5 = value
        End Set
    End Property
    Public Property Sz6() As Double
        Get
            Return _Sz6
        End Get
        Set(ByVal value As Double)
            _Sz6 = value
        End Set
    End Property
    Public Property Sz7() As Double
        Get
            Return _Sz7
        End Get
        Set(ByVal value As Double)
            _Sz7 = value
        End Set
    End Property
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property
    Public Property CurrentRate() As Double
        Get
            Return _CurrentRate
        End Get
        Set(ByVal value As Double)
            _CurrentRate = value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property

    Private _Pack_Desc As String
    Public Property Pack_Desc() As String
        Get
            Return _Pack_Desc
        End Get
        Set(ByVal value As String)
            _Pack_Desc = value
        End Set
    End Property

    'Task No 2555 Mughees 13-apil 2014 Added Property of UOM In this Concerned Class 

    Public Property UOM() As String
        Get
            Return _Uom
        End Get
        Set(ByVal value As String)
            _Uom = value
        End Set
    End Property
    'Task:2690 Added Property   
    Private _EmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return _EmployeeId
        End Get
        Set(ByVal value As Integer)
            _EmployeeId = value
        End Set
    End Property
    'End Task:2690
    Public Property PurchaseAccountId() As Integer
        Get
            Return _PurchaseAccountId
        End Get
        Set(ByVal value As Integer)
            _PurchaseAccountId = value
        End Set
    End Property
    Public Property CGSAccountId() As Integer
        Get
            Return _CGSAccountId
        End Get
        Set(ByVal value As Integer)
            _CGSAccountId = value
        End Set
    End Property
    Public Property EngineNo() As String
        Get
            Return _EngineNo
        End Get
        Set(ByVal value As String)
            _EngineNo = value
        End Set
    End Property
    Public Property ChasisNo() As String
        Get
            Return _ChasisNo
        End Get
        Set(ByVal value As String)
            _ChasisNo = value
        End Set
    End Property
    Private _PlanDetailId As Integer
    Public Property PlanDetailId() As Integer
        Get
            Return _PlanDetailId
        End Get
        Set(ByVal value As Integer)
            _PlanDetailId = value
        End Set
    End Property
    ''TASK TFS1496
    Public Property PackPrice As Double
    ''TASK TFS1616
    Public Property BatchNo() As String
        Get
            Return _BatchNo
        End Get
        Set(ByVal value As String)
            _BatchNo = value
        End Set
    End Property
    Public Property RetailPrice() As Double
        Get
            Return _RetailPrice
        End Get
        Set(ByVal value As Double)
            _RetailPrice = value
        End Set
    End Property

    Public Property ExpiryDate() As DateTime
        Get
            Return _ExpiryDate
        End Get
        Set(ByVal value As DateTime)
            _ExpiryDate = value
        End Set
    End Property
    ' End Task 1616
    ''TFS1772
    Public Property Dim1() As Double
        Get
            Return _Dim1
        End Get
        Set(ByVal value As Double)
            _Dim1 = value
        End Set
    End Property
    Public Property Dim2() As Double
        Get
            Return _Dim2
        End Get
        Set(ByVal value As Double)
            _Dim2 = value
        End Set
    End Property
    ' End Task 1772
End Class
