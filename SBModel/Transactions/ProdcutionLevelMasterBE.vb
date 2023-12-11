Public Class ProdcutionLevelMasterBE

    Private _PLevelId As Integer
    Public Property PLevelId() As Integer
        Get
            Return _PLevelId
        End Get
        Set(ByVal value As Integer)
            _PLevelId = value
        End Set
    End Property

    Private _Doc_No As String
    Public Property Doc_No() As String
        Get
            Return _Doc_No
        End Get
        Set(ByVal value As String)
            _Doc_No = value
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

    Private _ProjectId As Integer
    Public Property ProjectId() As Integer
        Get
            Return _ProjectId
        End Get
        Set(ByVal value As Integer)
            _ProjectId = value
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

    Private _StepsId As Integer
    Public Property StepsId() As Integer
        Get
            Return _StepsId
        End Get
        Set(ByVal value As Integer)
            _StepsId = value
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


    Private _ProductionLevelDetail As List(Of ProductionLevelDetailBE)
    Public Property ProductionLevelDetail() As List(Of ProductionLevelDetailBE)
        Get
            Return _ProductionLevelDetail
        End Get
        Set(ByVal value As List(Of ProductionLevelDetailBE))
            _ProductionLevelDetail = value
        End Set
    End Property

End Class
