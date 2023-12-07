Public Class TicketOpeningBE
    Public Property TicketId As Integer
    Public Property TicketNo As String
    Public Property TicketDate As DateTime
    Public Property SerialNo As String
    Public Property CallerName As String
    Public Property Site As String
    'Murtaza
    Public Property SaleOrderId As String
    'Murtaza
    'rafay
    Public Property CustomerName As String
    Public Property CompanyName As String
    'rafay
    Public Property ContactVia As String
    Public Property ContactTime As DateTime
    Public Property Severity As String
    Public Property Status As String
    Public Property EngineerAssigned As String
    Public Property InitialProblem As String
    'rafay
    Public Property ContractId As Integer

    Public Property Brand As String
    Public Property ModelNo As String

    Public Property ChkBoxBatteriesIncluded As Boolean
    Public Property TicketHistory As String
    Public Property PartUsed As Boolean
    'rafay
    Public Property Detail As List(Of TicketOpeningDetailBE)
End Class

Public Class TicketOpeningDetailBE
    Public Property TicketDetailId As Integer
    Public Property TicketId As Integer
    Public Property OnsiteEngineer As String
    Public Property TimeOnSite As DateTime
    Public Property ActivityStart As DateTime
    Public Property ActivityEnd As DateTime
    Public Property ActivityPerformed As String
    Public Property TicketState As String
    Public Property Escalation As String
    Public Property PNUsed As String
    Public Property Partno As String
    Public Property Quantity As Integer
    Public Property PartDescription As String
    Public Property FaultyPartSN As String


End Class

