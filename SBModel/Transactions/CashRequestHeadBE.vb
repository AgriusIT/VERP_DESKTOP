''26-June-2014 TASK2704 Imran Ali Add new functionality cash request in erp.
Public Class CashRequestHeadBE


    Private _RequestId As Integer
    Public Property RequestId() As Integer
        Get
            Return _RequestId
        End Get
        Set(ByVal value As Integer)
            _RequestId = value
        End Set
    End Property

    Private _RequestNo As String
    Public Property RequestNo() As String
        Get
            Return _RequestNo
        End Get
        Set(ByVal value As String)
            _RequestNo = value
        End Set
    End Property

    Private _RequestDate As DateTime
    Public Property RequestDate() As DateTime
        Get
            Return _RequestDate
        End Get
        Set(ByVal value As DateTime)
            _RequestDate = value
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

    Private _EmpID As Integer
    Public Property EmpID() As Integer
        Get
            Return _EmpID
        End Get
        Set(ByVal value As Integer)
            _EmpID = value
        End Set
    End Property

    Private _CMFADocId As Integer
    Public Property CMFADocId() As Integer
        Get
            Return _CMFADocId
        End Get
        Set(ByVal value As Integer)
            _CMFADocId = value
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

    Private _Approved As Boolean
    Public Property Approved() As Boolean
        Get
            Return _Approved
        End Get
        Set(ByVal value As Boolean)
            _Approved = value
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

    Private _ApprovedUserId As Integer
    Public Property ApprovedUserId() As Integer
        Get
            Return _ApprovedUserId
        End Get
        Set(ByVal value As Integer)
            _ApprovedUserId = value
        End Set
    End Property

    Private _ModifyUserId As Integer
    Public Property ModifyUserId() As Integer
        Get
            Return _ModifyUserId
        End Get
        Set(ByVal value As Integer)
            _ModifyUserId = value
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

    Private _ApprovedDate As DateTime
    Public Property ApprovedDate() As DateTime
        Get
            Return _ApprovedDate
        End Get
        Set(ByVal value As DateTime)
            _ApprovedDate = value
        End Set
    End Property

    Private _ModifyDate As DateTime
    Public Property ModifyDate() As DateTime
        Get
            Return _ModifyDate
        End Get
        Set(ByVal value As DateTime)
            _ModifyDate = value
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

    Private _CashRequestDetail As List(Of CashRequestDetailBE)
    Public Property CashRequstDetail() As List(Of CashRequestDetailBE)
        Get
            Return _CashRequestDetail
        End Get
        Set(ByVal value As List(Of CashRequestDetailBE))
            _CashRequestDetail = value
        End Set
    End Property

    ''TASK TFS4439
    Public Property ArrFile As List(Of String)
    Public Property Source As String
    Public Property AttachmentPath As String
    ''END TFS4439

End Class
