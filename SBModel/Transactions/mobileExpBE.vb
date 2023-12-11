Public Class mobileExpBE


    Private _mobileBill_id As Integer
    Public Property mobileBill_id() As Integer
        Get
            Return _mobileBill_id
        End Get
        Set(ByVal value As Integer)
            _mobileBill_id = value
        End Set
    End Property


    Private _mobileBill_No As Integer
    Public Property mobileBill_No() As Integer
        Get
            Return _mobileBill_No
        End Get
        Set(ByVal value As Integer)
            _mobileBill_No = value
        End Set
    End Property

    Private _mobileBill_date As DateTime
    Public Property mobileBill_date() As DateTime
        Get
            Return _mobileBill_date
        End Get
        Set(ByVal value As DateTime)
            _mobileBill_date = value
        End Set
    End Property


    Private _month As Integer
    Public Property month() As Integer
        Get
            Return _month
        End Get
        Set(ByVal value As Integer)
            _month = value
        End Set
    End Property

    Private _year As Integer
    Public Property year() As Integer
        Get
            Return _year
        End Get
        Set(ByVal value As Integer)
            _year = value
        End Set
    End Property

    Private _employee_Id As Integer
    Public Property employee_Id() As Integer
        Get
            Return _employee_Id
        End Get
        Set(ByVal value As Integer)
            _employee_Id = value
        End Set
    End Property

    Private _usedBill As String
    Public Property usedBill() As String
        Get
            Return _usedBill
        End Get
        Set(ByVal value As String)
            _usedBill = value
        End Set
    End Property

    Private _paidBill As String
    Public Property paidBill() As String
        Get
            Return _paidBill
        End Get
        Set(ByVal value As String)
            _paidBill = value
        End Set
    End Property

    Private _limit As String
    Public Property limit() As String
        Get
            Return _limit
        End Get
        Set(ByVal value As String)
            _limit = value
        End Set
    End Property

    Private _paidByEmp As String
    Public Property paidByEmp() As String
        Get
            Return _paidByEmp
        End Get
        Set(ByVal value As String)
            _paidByEmp = value
        End Set
    End Property

    Private _paidByCo As String
    Public Property paidByCo() As String
        Get
            Return _paidByCo
        End Get
        Set(ByVal value As String)
            _paidByCo = value
        End Set
    End Property

    Private _userName As String
    Public Property userName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Private _entryDate As DateTime
    Public Property entryDate() As DateTime
        Get
            Return _entryDate
        End Get
        Set(ByVal value As DateTime)
            _entryDate = value
        End Set
    End Property


    Private _totalBill As String
    Public Property totalBill() As String
        Get
            Return _totalBill
        End Get
        Set(ByVal value As String)
            _totalBill = value
        End Set
    End Property

End Class
