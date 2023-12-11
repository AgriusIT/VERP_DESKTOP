Public Class AdjustmentVoucher

    Private _AdjId As Integer
    Public Property AdjId() As Integer
        Get
            Return _AdjId
        End Get
        Set(ByVal value As Integer)
            _AdjId = value
        End Set
    End Property

    Private _AdjNo As String
    Public Property AdjNo() As String
        Get
            Return _AdjNo
        End Get
        Set(ByVal value As String)
            _AdjNo = value
        End Set
    End Property

    Private _AdjDate As DateTime
    Public Property AdjDate() As DateTime
        Get
            Return _AdjDate
        End Get
        Set(ByVal value As DateTime)
            _AdjDate = value
        End Set
    End Property

    Private _Customercode As Integer
    Public Property CustomerCode() As Integer
        Get
            Return _Customercode
        End Get
        Set(ByVal value As Integer)
            _Customercode = value
        End Set
    End Property

    Private _MarketReturns As Double
    Public Property MarketReturns() As Double
        Get
            Return _MarketReturns
        End Get
        Set(ByVal value As Double)
            _MarketReturns = value
        End Set
    End Property

    Private _MarketReturnAcId As Integer
    Public Property MarketReturnAcId() As Integer
        Get
            Return _MarketReturnAcId
        End Get
        Set(ByVal value As Integer)
            _MarketReturnAcId = value
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

    Private _CustomerName As String
    Public Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property

    Private _Source As String
    Public Property source() As String
        Get
            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
        End Set
    End Property


End Class
