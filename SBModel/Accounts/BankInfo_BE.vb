'' 21-12-2013 ReqID-957   M Ijaz Javed      Bank information entry option
''28-Dec-2013 R:M6 Imran Ali                Release Bug
''4-Jan-2014 Tsk:2368         Imran Ali             Multi Cheque Layout
Public Class BankInfo_BE


    Private _AccountHolder_Id As Integer
    Public Property AccountHolder_Id() As Integer
        Get
            Return _AccountHolder_Id
        End Get
        Set(ByVal value As Integer)
            _AccountHolder_Id = value
        End Set
    End Property


    Private _Holder_Name As String
    Public Property Holder_Name() As String
        Get
            Return _Holder_Name
        End Get
        Set(ByVal value As String)
            _Holder_Name = value
        End Set
    End Property


    Private _Account_No As String
    Public Property Account_No() As String
        Get
            Return _Account_No
        End Get
        Set(ByVal value As String)
            _Account_No = value
        End Set
    End Property


    Private _Branch_Area As String
    Public Property Branch_Area() As String
        Get
            Return _Branch_Area
        End Get
        Set(ByVal value As String)
            _Branch_Area = value
        End Set
    End Property


    Private _Bank_Id As Integer
    Public Property Bank_Id() As Integer
        Get
            Return _Bank_Id
        End Get
        Set(ByVal value As Integer)
            _Bank_Id = value
        End Set
    End Property

    Private _BranchPhoneNo As String
    Public Property BranchPhoneNo() As String
        Get
            Return _BranchPhoneNo
        End Get
        Set(ByVal value As String)
            _BranchPhoneNo = value
        End Set
    End Property
    ''Task:2368 Added Property    ChequeLayId
    Private _ChequeLayoutId As Integer
    Public Property ChequeLayoutId() As Integer
        Get
            Return _ChequeLayoutId
        End Get
        Set(ByVal value As Integer)
            _ChequeLayoutId = value
        End Set
    End Property

    Private _BankType As String
    Public Property BankType() As String
        Get
            Return _BankType
        End Get
        Set(ByVal value As String)
            _BankType = value
        End Set
    End Property

    Private _DesignatedTo As String
    Public Property DesignatedTo() As String
        Get
            Return _DesignatedTo
        End Get
        Set(ByVal value As String)
            _DesignatedTo = value
        End Set
    End Property
End Class
