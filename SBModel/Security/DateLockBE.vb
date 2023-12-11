'25-Jun-2015  Task#125062015 Ahmad Sharif   Add Properties _DateLockType and _NoOfDays

Public Class DateLockBE

    Private _DateLockId As Integer
    Public Property DateLockId() As Integer
        Get
            Return _DateLockId
        End Get
        Set(ByVal value As Integer)
            _DateLockId = value
        End Set
    End Property

    Private _DateLock As DateTime
    Public Property DateLock() As DateTime
        Get
            Return _DateLock
        End Get
        Set(ByVal value As DateTime)
            _DateLock = value
        End Set
    End Property

    Private _Lock As Boolean
    Public Property Lock() As Boolean
        Get
            Return _Lock
        End Get
        Set(ByVal value As Boolean)
            _Lock = value
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

    Private _Username As String
    Public Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property


    'Task#125062015 Add Property for Date Lock type (Fixed or Relevant)
    Private _DateLockType As String
    Public Property DateLockType() As String
        Get
            Return _DateLockType
        End Get
        Set(ByVal value As String)
            _DateLockType = value
        End Set
    End Property

    Private _NoOfDays As Integer
    Public Property NoOfDays() As Integer
        Get
            Return _NoOfDays
        End Get
        Set(ByVal value As Integer)
            _NoOfDays = value
        End Set
    End Property
    'End Task#125062015

End Class
