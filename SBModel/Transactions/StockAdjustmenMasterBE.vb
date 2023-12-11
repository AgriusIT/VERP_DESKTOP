Public Class StockAdjustmenMasterBE

    Private _SA_id As Integer
    Public Property SA_id() As Integer
        Get
            Return _SA_id
        End Get
        Set(ByVal value As Integer)
            _SA_id = value
        End Set
    End Property

    Private _Doc_no As String
    Public Property Doc_no() As String
        Get
            Return _Doc_no
        End Get
        Set(ByVal value As String)
            _Doc_no = value
        End Set
    End Property

    Private _Doc_Date As DateTime
    Public Property Doc_Date() As DateTime
        Get
            Return _Doc_Date
        End Get
        Set(ByVal value As DateTime)
            _Doc_Date = value
        End Set
    End Property

    Private _Project As Integer
    Public Property Project() As Integer
        Get
            Return _Project
        End Get
        Set(ByVal value As Integer)
            _Project = value
        End Set
    End Property

    Private _remarks As String
    Public Property remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property

    Private _StockAdjustmentDetail As List(Of StockAdjustmenDetailBE)
    Public Property StockAdjustmentDetail() As List(Of StockAdjustmenDetailBE)
        Get
            Return _StockAdjustmentDetail
        End Get
        Set(ByVal value As List(Of StockAdjustmenDetailBE))
            _StockAdjustmentDetail = value
        End Set
    End Property

    Private _StockMaster As StockMaster
    Public Property StockMaster() As StockMaster
        Get
            Return _StockMaster
        End Get
        Set(ByVal value As StockMaster)
            _StockMaster = value
        End Set
    End Property
End Class
