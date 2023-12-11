Public Class StockMaster
    Private _StockTransId As Integer = 0I
    Private _DocNo As String = String.Empty
    Private _DocDate As DateTime
    Private _DocType As Integer = 0I
    Private _Remarks As String = String.Empty
    Private _StockDetailList As List(Of StockDetail)
    Private _StockListForAssembly As List(Of StockDetail) ''TFS1957
    Private _Project As Integer = 0I
    Private _AccountId As Integer = 0I

    Public Property StockTransId() As Integer
        Get
            Return _StockTransId
        End Get
        Set(ByVal value As Integer)
            _StockTransId = value
        End Set
    End Property
    Public Property DocNo() As String
        Get
            Return _DocNo
        End Get
        Set(ByVal value As String)
            _DocNo = value
        End Set
    End Property
    Public Property DocDate() As DateTime
        Get
            Return _DocDate
        End Get
        Set(ByVal value As DateTime)
            _DocDate = value
        End Set
    End Property
    Public Property DocType() As Integer
        Get
            Return _DocType
        End Get
        Set(ByVal value As Integer)
            _DocType = value
        End Set
    End Property
    Public Property Remaks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    Public Property StockDetailList() As List(Of StockDetail)
        Get
            Return _StockDetailList
        End Get
        Set(ByVal value As List(Of StockDetail))
            _StockDetailList = value
        End Set
    End Property
    ''Start TFS1957
    Public Property StockListForAssembly() As List(Of StockDetail)
        Get
            Return _StockListForAssembly
        End Get
        Set(ByVal value As List(Of StockDetail))
            _StockListForAssembly = value
        End Set
    End Property
    ''End TFS1957
    Public Property Project() As Integer
        Get
            Return _Project
        End Get
        Set(ByVal value As Integer)
            _Project = value
        End Set
    End Property
    Public Property AccountId() As Integer
        Get
            Return _AccountId
        End Get
        Set(ByVal value As Integer)
            _AccountId = value
        End Set
    End Property

End Class
