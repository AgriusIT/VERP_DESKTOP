'Mughees 12-5-2014 Task Id 2624 Making New BE For The LateTimeSlot Table
Public Class LateTimeSlotBE
    Private _SlotId As Integer
    Private _SlotStart As String
    Private _SlotEnd As String
    Private _Active As Boolean

    Public Property SlotId() As Integer
        Get
            Return _SlotId
        End Get
        Set(ByVal value As Integer)
            _SlotId = value
        End Set
    End Property
    Public Property SlotStart() As String
        Get
            Return _SlotStart
        End Get
        Set(ByVal value As String)
            _SlotStart = value
        End Set
    End Property
    Public Property SlotEnd() As String
        Get
            Return _SlotEnd
        End Get
        Set(ByVal value As String)
            _SlotEnd = value
        End Set
    End Property
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property
    'End Task 2624
End Class
