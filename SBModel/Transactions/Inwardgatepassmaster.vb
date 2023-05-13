Public Class Inwardgatepassmaster
    Private _InwardgatepassId As Integer
    Public Property InwardgatepassId() As Integer
        Get
            Return _InwardgatepassId
        End Get
        Set(ByVal value As Integer)
            _InwardgatepassId = value
        End Set
    End Property


    Private _Inwardgatepassdate As DateTime
    Public Property Inwardgatepassdate() As DateTime
        Get
            Return _Inwardgatepassdate
        End Get
        Set(ByVal value As DateTime)
            _Inwardgatepassdate = value
        End Set
    End Property

    Private _InwardGatePassNo As String
    Public Property InwardGatePassNo() As String
        Get
            Return _InwardGatePassNo
        End Get
        Set(ByVal value As String)
            _InwardGatePassNo = value
        End Set
    End Property


    Private _BillNo As String
    Public Property BillNo() As String
        Get
            Return _BillNo
        End Get
        Set(ByVal value As String)
            _BillNo = value
        End Set
    End Property

    Private _PartyName As String
    Public Property PartyName() As String
        Get
            Return _PartyName
        End Get
        Set(ByVal value As String)
            _PartyName = value
        End Set
    End Property

    Private _Category As String
    Public Property Category() As String
        Get
            Return _Category
        End Get
        Set(ByVal value As String)
            _Category = value
        End Set
    End Property

    Private _CityId As Integer
    Public Property CityId() As Integer
        Get
            Return _CityId
        End Get
        Set(ByVal value As Integer)
            _CityId = value
        End Set
    End Property

    Private _Drivername As String
    Public Property Drivername() As String
        Get
            Return _Drivername
        End Get
        Set(ByVal value As String)
            _Drivername = value
        End Set
    End Property

    Private _VehicleNo As String
    Public Property VehicleNo() As String
        Get
            Return _VehicleNo
        End Get
        Set(ByVal value As String)
            _VehicleNo = value
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

    Private _Entrydate As DateTime
    Public Property Entrydate() As DateTime
        Get
            Return _Entrydate
        End Get
        Set(ByVal value As DateTime)
            _Entrydate = value
        End Set
    End Property

    Private _Inwardgatepassdetail As List(Of Inwardgatepassdetail)
    Public Property inwardgatepassdetail() As List(Of Inwardgatepassdetail)
        Get
            Return _Inwardgatepassdetail
        End Get
        Set(ByVal value As List(Of Inwardgatepassdetail))
            _Inwardgatepassdetail = value
        End Set
    End Property

    Private _Unit As String
    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
        End Set
    End Property

End Class
