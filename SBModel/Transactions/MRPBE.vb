Public Class MRPBE

    Private _DocId As Integer
    Public Property DocId() As Integer
        Get
            Return _DocId
        End Get
        Set(ByVal value As Integer)
            _DocId = value
        End Set
    End Property

    Private _DocNo As String
    Public Property DocNo() As String
        Get
            Return _DocNo
        End Get
        Set(ByVal value As String)
            _DocNo = value
        End Set
    End Property

    Private _DocDate As DateTime
    Public Property DocDate() As DateTime
        Get
            Return _DocDate
        End Get
        Set(ByVal value As DateTime)
            _DocDate = value
        End Set
    End Property

    Private _PlanId As Integer
    Public Property PlanId() As Integer
        Get
            Return _PlanId
        End Get
        Set(ByVal value As Integer)
            _PlanId = value
        End Set
    End Property

    Private _ProudctionArticleId As Integer
    Public Property ProudctionArticleId() As Integer
        Get
            Return _ProudctionArticleId
        End Get
        Set(ByVal value As Integer)
            _ProudctionArticleId = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _TotalQty As Double
    Public Property TotalQty() As Double
        Get
            Return _TotalQty
        End Get
        Set(ByVal value As Double)
            _TotalQty = value
        End Set
    End Property

    Private _TotalAmount As Double
    Public Property TotalAmount() As Double
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Double)
            _TotalAmount = value
        End Set
    End Property

    Private _Issued As Boolean
    Public Property Issued() As Boolean
        Get
            Return _Issued
        End Get
        Set(ByVal value As Boolean)
            _Issued = value
        End Set
    End Property

    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _EntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As DateTime)
            _EntryDate = value
        End Set
    End Property

    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property

    Private _MRPlanDetail As List(Of MRPDetailBE)
    Public Property MRPlanDetail() As List(Of MRPDetailBE)
        Get
            Return _MRPlanDetail
        End Get
        Set(ByVal value As List(Of MRPDetailBE))
            _MRPlanDetail = value
        End Set
    End Property

End Class
Public Class MRPDetailBE

    Private _DocDetailId As Integer
    Public Property DocDetailId() As Integer
        Get
            Return _DocDetailId
        End Get
        Set(ByVal value As Integer)
            _DocDetailId = value
        End Set
    End Property

    Private _DocId As Integer
    Public Property DocId() As Integer
        Get
            Return _DocId
        End Get
        Set(ByVal value As Integer)
            _DocId = value
        End Set
    End Property

    Private _LocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property

    Private _ArticleDefId As Integer
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
        End Set
    End Property

    Private _ArticleSize As String
    Public Property ArticleSize() As String
        Get
            Return _ArticleSize
        End Get
        Set(ByVal value As String)
            _ArticleSize = value
        End Set
    End Property

    Private _CurrentStock As Double
    Public Property CurrentStock() As Double
        Get
            Return _CurrentStock
        End Get
        Set(ByVal value As Double)
            _CurrentStock = value
        End Set
    End Property

    Private _SuggestedQty As Double
    Public Property SuggestedQty() As Double
        Get
            Return _SuggestedQty
        End Get
        Set(ByVal value As Double)
            _SuggestedQty = value
        End Set
    End Property


    Private _Sz1 As Double
    Public Property Sz1() As Double
        Get
            Return _Sz1
        End Get
        Set(ByVal value As Double)
            _Sz1 = value
        End Set
    End Property

    Private _Sz7 As Double
    Public Property Sz7() As Double
        Get
            Return _Sz7
        End Get
        Set(ByVal value As Double)
            _Sz7 = value
        End Set
    End Property

    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property

    Private _Price As Double
    Public Property Price() As Double
        Get
            Return _Price
        End Get
        Set(ByVal value As Double)
            _Price = value
        End Set
    End Property

    Private _Comments As String
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property


End Class
