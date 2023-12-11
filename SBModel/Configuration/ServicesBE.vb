Public Class ServicesBE

    Private _ServicesID As Integer
    Public Property ServicesID() As Integer
        Get
            Return _ServicesID
        End Get
        Set(ByVal value As Integer)
            _ServicesID = value
        End Set
    End Property

    Private _ServicesType As String
    Public Property ServicesType() As String
        Get
            Return _ServicesType
        End Get
        Set(ByVal value As String)
            _ServicesType = value
        End Set
    End Property

    Private _Tax_Percent As Double
    Public Property Tax_Percent() As Double
        Get
            Return _Tax_Percent
        End Get
        Set(ByVal value As Double)
            _Tax_Percent = value
        End Set
    End Property

    Private _WHTax As Double
    Public Property WHTax() As Double
        Get
            Return _WHTax
        End Get
        Set(ByVal value As Double)
            _WHTax = value
        End Set
    End Property

    Private _Opex As Double
    Public Property Opex() As Double
        Get
            Return _Opex
        End Get
        Set(ByVal value As Double)
            _Opex = value
        End Set
    End Property
    Private _Region As String
    Public Property Region() As String
        Get
            Return _Region
        End Get
        Set(ByVal value As String)
            _Region = value
        End Set
    End Property




End Class
