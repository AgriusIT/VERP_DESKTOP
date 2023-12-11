''20-June-2014 TASK:2701 IMRAN ALI Expense Entry on CMFA Document(Ravi)
Public Class CMFAExpVoucherBE

    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Private _DocID As Integer
    Public Property DocID() As Integer
        Get
            Return _DocID
        End Get
        Set(ByVal value As Integer)
            _DocID = value
        End Set
    End Property

    Private _coa_detail_id As Integer
    Public Property coa_detail_id() As Integer
        Get
            Return _coa_detail_id
        End Get
        Set(ByVal value As Integer)
            _coa_detail_id = value
        End Set
    End Property

    Private _Amount As Double
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            _Amount = value
        End Set
    End Property



End Class
