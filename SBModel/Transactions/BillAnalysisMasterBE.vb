Public Class BillAnalysisMasterBE

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

    Private _OGPNo As String
    Public Property OGPNo() As String
        Get
            Return _OGPNo
        End Get
        Set(ByVal value As String)
            _OGPNo = value
        End Set
    End Property

    Private _OGPDate As DateTime
    Public Property OGPDate() As DateTime
        Get
            Return _OGPDate
        End Get
        Set(ByVal value As DateTime)
            _OGPDate = value
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

    Private _CustomerId As Integer
    Public Property CustomerId() As Integer
        Get
            Return _CustomerId
        End Get
        Set(ByVal value As Integer)
            _CustomerId = value
        End Set
    End Property

    Private _Note As String
    Public Property Note() As String
        Get
            Return _Note
        End Get
        Set(ByVal value As String)
            _Note = value
        End Set
    End Property

    Private _Rate_1 As Double
    Public Property Rate_1() As Double
        Get
            Return _Rate_1
        End Get
        Set(ByVal value As Double)
            _Rate_1 = value
        End Set
    End Property

    Private _Rate_2 As Double
    Public Property Rate_2() As Double
        Get
            Return _Rate_2
        End Get
        Set(ByVal value As Double)
            _Rate_2 = value
        End Set
    End Property

    Private _Rate_3 As Double
    Public Property Rate_3() As Double
        Get
            Return _Rate_3
        End Get
        Set(ByVal value As Double)
            _Rate_3 = value
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

    Private _Total_Qty As Double
    Public Property Total_Qty() As Double
        Get
            Return _Total_Qty
        End Get
        Set(ByVal value As Double)
            _Total_Qty = value
        End Set
    End Property

    Private _Total_Amount As Double
    Public Property Total_Amount() As Double
        Get
            Return _Total_Amount
        End Get
        Set(ByVal value As Double)
            _Total_Amount = value
        End Set
    End Property


    Private _CompanyId As Integer
    Public Property CompanyId() As Integer
        Get
            Return _CompanyId
        End Get
        Set(ByVal value As Integer)
            _CompanyId = value
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

    Private _BillAnalysisDetail As List(Of BillAnalaysisDetailBE)
    Public Property BillAnalysisDetail() As List(Of BillAnalaysisDetailBE)
        Get
            Return _BillAnalysisDetail
        End Get
        Set(ByVal value As List(Of BillAnalaysisDetailBE))
            _BillAnalysisDetail = value
        End Set
    End Property


End Class
