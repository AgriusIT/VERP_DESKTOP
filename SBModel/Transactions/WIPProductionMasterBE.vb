Public Class WIPProductionMasterBE

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


    Private _LotNo As String
    Public Property LotNo() As String
        Get
            Return _LotNo
        End Get
        Set(ByVal value As String)
            _LotNo = value
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

    Private _RefReceivingId As Integer
    Public Property RefReceivingId() As Integer
        Get
            Return _RefReceivingId
        End Get
        Set(ByVal value As Integer)
            _RefReceivingId = value
        End Set
    End Property



    Private _WIPProductionDetail As List(Of WIPProductionDetailBE)
    Public Property WIPProductionDetail() As List(Of WIPProductionDetailBE)
        Get
            Return _WIPProductionDetail
        End Get
        Set(ByVal value As List(Of WIPProductionDetailBE))
            _WIPProductionDetail = value
        End Set
    End Property

    Private _Voucher As VouchersMaster
    Public Property Voucher() As VouchersMaster
        Get
            Return _Voucher
        End Get
        Set(ByVal value As VouchersMaster)
            _Voucher = value
        End Set
    End Property

    ''' <summary>
    ''' '''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Private _TransType As Double

    Public Property TransType() As Double
        Get
            Return _TransType
        End Get
        Set(ByVal value As Double)
            _TransType = value
        End Set
    End Property

    End Class
