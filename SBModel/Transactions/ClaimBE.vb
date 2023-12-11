'Task No 2638 BE For Rikshaw Claim Record
Public Class ClaimBE


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


    Private _CustomerCode As Integer
    Public Property CustomerCode() As Integer
        Get
            Return _CustomerCode
        End Get
        Set(ByVal value As Integer)
            _CustomerCode = value
        End Set
    End Property

    Private _ProjectId As Integer
    Public Property ProjectId() As Integer
        Get
            Return _ProjectId
        End Get
        Set(ByVal value As Integer)
            _ProjectId = value
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

    Private _Post As Boolean
    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
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

    Private _Adjustment As Double
    Public Property Adjustment() As Double
        Get
            Return _Adjustment
        End Get
        Set(ByVal value As Double)
            _Adjustment = value
        End Set
    End Property

    Private _UserId As Integer
    Public Property UserId() As Integer
        Get
            Return _UserId
        End Get
        Set(ByVal value As Integer)
            _UserId = value
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

    Private _WarrantyClaimDetail As List(Of WarrantyClaimDetailBE)
    Public Property WarrantyClaimDetail() As List(Of WarrantyClaimDetailBE)
        Get
            Return _WarrantyClaimDetail
        End Get
        Set(ByVal value As List(Of WarrantyClaimDetailBE))
            _WarrantyClaimDetail = value
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

    Private _StockTransId As Integer
    Public Property StockTransId() As Integer
        Get
            Return _StockTransId
        End Get
        Set(ByVal value As Integer)
            _StockTransId = value
        End Set
    End Property

    Private _VoucherId As Integer
    Public Property VoucherId() As Integer
        Get
            Return _VoucherId
        End Get
        Set(ByVal value As Integer)
            _VoucherId = value
        End Set
    End Property

    Private _CGSAccountId As Integer
    Public Property CGSAccountId() As Integer
        Get
            Return _CGSAccountId
        End Get
        Set(ByVal value As Integer)
            _CGSAccountId = value
        End Set
    End Property

    Private _OtherLossAccountId As Integer
    Public Property OtherLossAccountId() As Integer
        Get
            Return _OtherLossAccountId
        End Get
        Set(ByVal value As Integer)
            _OtherLossAccountId = value
        End Set
    End Property

    Private _TaxAccountId As Integer
    Public Property TaxAccountId() As Integer
        Get
            Return _TaxAccountId
        End Get
        Set(ByVal value As Integer)
            _TaxAccountId = value
        End Set
    End Property

    Private _DeliveryID As Integer
    Public Property DeliveryID() As Integer
        Get
            Return _DeliveryID
        End Get
        Set(ByVal value As Integer)
            _DeliveryID = value
        End Set
    End Property

End Class

Public Class WarrantyClaimDetailBE

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

    Private _Sz1 As Integer=0
    Public Property Sz1() As Integer
        Get
            Return _Sz1
        End Get
        Set(ByVal value As Integer)
            _Sz1 = value
        End Set
    End Property
    Private _Sz2 As Double = 0
    Public Property Sz2() As Double
        Get
            Return _Sz2
        End Get
        Set(ByVal value As Double)
            _Sz2 = value
        End Set
    End Property
    Private _Sz3 As Double = 0
    Public Property Sz3() As Double
        Get
            Return _Sz3
        End Get
        Set(ByVal value As Double)
            _Sz3 = value
        End Set
    End Property
    Private _Sz4 As Double = 0
    Public Property Sz4() As Double
        Get
            Return _Sz4
        End Get
        Set(ByVal value As Double)
            _Sz4 = value
        End Set
    End Property
    Private _Sz5 As Double = 0
    Public Property Sz5() As Double
        Get
            Return _Sz5
        End Get
        Set(ByVal value As Double)
            _Sz5 = value
        End Set
    End Property
    Private _Sz6 As Integer = 0
    Public Property Sz6() As Double
        Get
            Return _Sz6
        End Get
        Set(ByVal value As Double)
            _Sz6 = value
        End Set
    End Property
    Private _Sz7 As Integer = 0
    Public Property Sz7() As Double
        Get
            Return _Sz7
        End Get
        Set(ByVal value As Double)
            _Sz7 = value
        End Set
    End Property
    Private _Qty As Integer = 0
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

    Private _Current_Price As Double
    Public Property Current_Price() As Double
        Get
            Return _Current_Price
        End Get
        Set(ByVal value As Double)
            _Current_Price = value
        End Set
    End Property

    Private _Tax_Percent As Double=0
    Public Property Tax_Percent() As Double
        Get
            Return _Tax_Percent
        End Get
        Set(ByVal value As Double)
            _Tax_Percent = value
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

    Private _WarrantyAble As Boolean
    Public Property WarrantyAble() As Boolean
        Get
            Return _WarrantyAble
        End Get
        Set(ByVal value As Boolean)
            _WarrantyAble = value
        End Set
    End Property

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

    Private _PurchaseAccountId As Integer
    Public Property PurchaseAccountId() As Integer
        Get
            Return _PurchaseAccountId
        End Get
        Set(ByVal value As Integer)
            _PurchaseAccountId = value
        End Set
    End Property

    Private _PackDesc As String
    Public Property PackDesc() As String
        Get
            Return _PackDesc
        End Get
        Set(ByVal value As String)
            _PackDesc = value
        End Set
    End Property

    Private _ArticleDescription As String
    Public Property ArticleDescription() As String
        Get
            Return _ArticleDescription
        End Get
        Set(ByVal value As String)
            _ArticleDescription = value
        End Set
    End Property



End Class
