Public Class ChequeSerialBE

    Private _ChequeSerialId As Integer
    Public Property ChequeSerialId() As Integer
        Get
            Return _ChequeSerialId
        End Get
        Set(ByVal value As Integer)
            _ChequeSerialId = value
        End Set
    End Property

    Private _BackAcId As Integer
    Public Property BankAcId() As Integer
        Get
            Return _BackAcId
        End Get
        Set(ByVal value As Integer)
            _BackAcId = value
        End Set
    End Property

    Private _BranchName As String
    Public Property BranchName() As String
        Get
            Return _BranchName
        End Get
        Set(ByVal value As String)
            _BranchName = value
        End Set
    End Property

    Private _Cheque_No_From As String
    Public Property Cheque_No_From() As String
        Get
            Return _Cheque_No_From
        End Get
        Set(ByVal value As String)
            _Cheque_No_From = value
        End Set
    End Property

    Private _Cheque_No_To As String
    Public Property Cheque_No_To() As String
        Get
            Return _Cheque_No_To
        End Get
        Set(ByVal value As String)
            _Cheque_No_To = value
        End Set
    End Property

    Private _Status As Boolean
    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
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

    Private _ChequeBookDetail As List(Of ChequeBookDetailBE)
    Public Property ChequeBookDetail() As List(Of ChequeBookDetailBE)
        Get
            Return _ChequeBookDetail
        End Get
        Set(ByVal value As List(Of ChequeBookDetailBE))
            _ChequeBookDetail = value
        End Set
    End Property

End Class
