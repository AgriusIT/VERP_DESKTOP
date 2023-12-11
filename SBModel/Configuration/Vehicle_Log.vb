Public Class Vehicle_Log

    Private _LogId As Integer
    Public Property LogId() As Integer
        Get
            Return _LogId
        End Get
        Set(ByVal value As Integer)
            _LogId = value
        End Set
    End Property


    Private _LogDate As DateTime
    Public Property LogDate() As DateTime
        Get
            Return _LogDate
        End Get
        Set(ByVal value As DateTime)
            _LogDate = value
        End Set
    End Property


    Private _Vehicle_Id As Integer
    Public Property Vehicle_Id() As Integer
        Get
            Return _Vehicle_Id
        End Get
        Set(ByVal value As Integer)
            _Vehicle_Id = value
        End Set
    End Property


    Private _Person As String
    Public Property Person() As String
        Get
            Return _Person
        End Get
        Set(ByVal value As String)
            _Person = value
        End Set
    End Property


    Private _Purpose As String
    Public Property Purpose() As String
        Get
            Return _Purpose
        End Get
        Set(ByVal value As String)
            _Purpose = value
        End Set
    End Property


    Private _Previous As Double
    Public Property Previous() As Double
        Get
            Return _Previous
        End Get
        Set(ByVal value As Double)
            _Previous = value
        End Set
    End Property


    Private _Current As Double
    Public Property Current() As Double
        Get
            Return _Current
        End Get
        Set(ByVal value As Double)
            _Current = value
        End Set
    End Property


    Private _Fuel As Double
    Public Property Fuel() As Double
        Get
            Return _Fuel
        End Get
        Set(ByVal value As Double)
            _Fuel = value
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


    Private _EntryDateTime As DateTime
    Public Property EntryDateTime() As DateTime
        Get
            Return _EntryDateTime
        End Get
        Set(ByVal value As DateTime)
            _EntryDateTime = value
        End Set
    End Property

    Private _EntryNo As String
    Public Property EntryNo() As String
        Get
            Return _EntryNo
        End Get
        Set(ByVal value As String)
            _EntryNo = value
        End Set
    End Property


End Class
