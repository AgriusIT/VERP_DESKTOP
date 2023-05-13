Public Class DefCommissionBE


    Private _SeqId As Integer
    Public Property SeqId() As Integer
        Get
            Return _SeqId
        End Get
        Set(ByVal value As Integer)
            _SeqId = value
        End Set
    End Property


    Private _SalemanId As Integer
    Public Property SalemanId() As Integer
        Get
            Return _SalemanId
        End Get
        Set(ByVal value As Integer)
            _SalemanId = value
        End Set
    End Property


    Private _Start_Value As Double
    Public Property Start_Value() As Double
        Get
            Return _Start_Value
        End Get
        Set(ByVal value As Double)
            _Start_Value = value
        End Set
    End Property


    Private _End_Value As Double
    Public Property End_Value() As Double
        Get
            Return _End_Value
        End Get
        Set(ByVal value As Double)
            _End_Value = value
        End Set
    End Property


    Private _Percentage As Double
    Public Property Percentage() As Double
        Get
            Return _Percentage
        End Get
        Set(ByVal value As Double)
            _Percentage = value
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


    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
        End Set
    End Property


End Class
