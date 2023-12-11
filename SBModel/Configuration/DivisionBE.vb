Public Class DivisionBE


    Private _Division_Id As Integer
    Public Property Division_Id() As Integer
        Get
            Return _Division_Id
        End Get
        Set(ByVal value As Integer)
            _Division_Id = value
        End Set
    End Property


    Private _Dept_Id As Integer
    Public Property Dept_Id() As Integer
        Get
            Return _Dept_Id
        End Get
        Set(ByVal value As Integer)
            _Dept_Id = value
        End Set
    End Property


    Private _Division_Code As String
    Public Property Division_Code() As String
        Get
            Return _Division_Code
        End Get
        Set(ByVal value As String)
            _Division_Code = value
        End Set
    End Property


    Private _Division_Name As String
    Public Property Division_Name() As String
        Get
            Return _Division_Name
        End Get
        Set(ByVal value As String)
            _Division_Name = value
        End Set
    End Property


    Private _Active As Boolean
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property


    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
        End Set
    End Property


End Class
