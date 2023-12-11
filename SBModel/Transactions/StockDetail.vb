''06-Feb-2014          TASK:M16     Imran Ali   Add New Fields Engine No And Chassis No. on Sales  
Public Class StockDetail
    Private _StockTransDetailId As Integer
    Private _StockTransId As Integer
    Private _LocationId As Integer
    Private _ArticleDefId As Integer
    Private _InQty As Double
    Private _OutQty As Double
    Private _Rate As Double
    Private _InAmount As Double
    Private _OutAmount As Double
    Private _Remarks As String
    'rafay :task start: ADD new field PO_NO
    'Private _PO_NO As String
    'Rafay:TASK end
    Private _CostPrice As Double = 0D
    Private _BatchNo As String = String.Empty
    Private _ExpiryDate As DateTime ''TFS4181
    Private _Origin As String = String.Empty
    Public Property StockTransDetailId() As Integer
        Get
            Return _StockTransDetailId
        End Get
        Set(ByVal value As Integer)
            _StockTransDetailId = value
        End Set
    End Property
    Public Property StockTransId() As Integer
        Get
            Return _StockTransId
        End Get
        Set(ByVal value As Integer)
            _StockTransId = value
        End Set
    End Property
    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
        End Set
    End Property
    Public Property InQty() As Double
        Get
            Return _InQty
        End Get
        Set(ByVal value As Double)
            _InQty = value
        End Set
    End Property
    Public Property OutQty() As Double
        Get
            Return _OutQty
        End Get
        Set(ByVal value As Double)
            _OutQty = value
        End Set
    End Property
    Public Property Rate() As Double
        Get
            Return _Rate
        End Get
        Set(ByVal value As Double)
            _Rate = value
        End Set
    End Property
    Public Property InAmount() As Double
        Get
            Return _InAmount
        End Get
        Set(ByVal value As Double)
            _InAmount = value
        End Set
    End Property
    Public Property OutAmount() As Double
        Get
            Return _OutAmount
        End Get
        Set(ByVal value As Double)
            _OutAmount = value
        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    'Public Property PO_NO() As String
    '    Get
    '        Return _PO_NO
    '    End Get
    '    Set(ByVal value As String)
    '        _PO_NO = value
    '    End Set
    'End Property

    'Task:M16 Added Property

    Private _Engine_No As String
    Public Property Engine_No() As String
        Get
            Return _Engine_No
        End Get
        Set(ByVal value As String)
            _Engine_No = value
        End Set
    End Property

    Private _Chassis_No As String
    Public Property Chassis_No() As String
        Get
            Return _Chassis_No
        End Get
        Set(ByVal value As String)
            _Chassis_No = value
        End Set
    End Property
    'End Task:M16
    Public Property CostPrice() As Double
        Get
            Return _CostPrice
        End Get
        Set(ByVal value As Double)
            _CostPrice = value
        End Set
    End Property
    ''Start TASK-470 On 01-07-2016
    Private _PackQty As Double
    Public Property PackQty() As Double
        Get
            Return _PackQty
        End Get
        Set(ByVal value As Double)
            _PackQty = value
        End Set
    End Property
    Private _Out_PackQty As Double
    Public Property Out_PackQty() As Double
        Get
            Return _Out_PackQty
        End Get
        Set(ByVal value As Double)
            _Out_PackQty = value
        End Set
    End Property
    Private _In_PackQty As Double
    Public Property In_PackQty() As Double
        Get
            Return _In_PackQty
        End Get
        Set(ByVal value As Double)
            _In_PackQty = value
        End Set
    End Property
    ''End TASK-407
    ''Start TASK-1596 On 30-11-2017
    Public Property BatchNo() As String
        Get
            Return _BatchNo
        End Get
        Set(ByVal value As String)
            _BatchNo = value
        End Set
    End Property
    ''End TASK-1596
    ''Start TASK-4181 On 18-08-2018
    Public Property ExpiryDate() As DateTime
        Get
            Return _ExpiryDate
        End Get
        Set(ByVal value As DateTime)
            _ExpiryDate = value
        End Set
    End Property
    ''End TASK-4181
    Public Property Origin() As String
        Get
            Return _Origin
        End Get
        Set(ByVal value As String)
            _Origin = value
        End Set
    End Property
End Class
