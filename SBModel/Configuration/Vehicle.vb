Public Class Vehicle

    Private _Vehicle_ID As Integer
    Public Property Vehicle_ID() As Integer
        Get
            Return _Vehicle_ID
        End Get
        Set(ByVal value As Integer)
            _Vehicle_ID = value
        End Set
    End Property


    Private _Vehicle_Name As String
    Public Property Vehicle_Name() As String
        Get
            Return _Vehicle_Name
        End Get
        Set(ByVal value As String)
            _Vehicle_Name = value
        End Set
    End Property


    Private _Vehicle_Type As String
    Public Property Vehicle_Type() As String
        Get
            Return _Vehicle_Type
        End Get
        Set(ByVal value As String)
            _Vehicle_Type = value
        End Set
    End Property


    Private _Engine_No As String
    Public Property Engine_No() As String
        Get
            Return _Engine_No
        End Get
        Set(ByVal value As String)
            _Engine_No = value
        End Set
    End Property


    Private _Chassis_No As String
    Public Property Chassis_No() As String
        Get
            Return _Chassis_No
        End Get
        Set(ByVal value As String)
            _Chassis_No = value
        End Set
    End Property


    Private _Registration_No As String
    Public Property Registration_No() As String
        Get
            Return _Registration_No
        End Get
        Set(ByVal value As String)
            _Registration_No = value
        End Set
    End Property


    Private _Maker As String
    Public Property Maker() As String
        Get
            Return _Maker
        End Get
        Set(ByVal value As String)
            _Maker = value
        End Set
    End Property


    Private _Model As String
    Public Property Model() As String
        Get
            Return _Model
        End Get
        Set(ByVal value As String)
            _Model = value
        End Set
    End Property

    Private _Color As String
    Public Property Color() As String
        Get
            Return _Color
        End Get
        Set(ByVal value As String)
            _Color = value
        End Set
    End Property


    Private _Power As String
    Public Property Power() As String
        Get
            Return _Power
        End Get
        Set(ByVal value As String)
            _Power = value
        End Set
    End Property

End Class
