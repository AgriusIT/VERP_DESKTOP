

Imports Janus.Windows.GridEX
'Imports SBModel.MaterialEstimationModel
Imports SBModel
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class TicketTrackingBAL

    Dim DAL As CRUD_db
    Public Function GetSalesOrder() As DataTable
        Dim MasterQuery As String = "select s.SalesOrderId, s.SalesOrderNo from SalesOrderMasterTable s"
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetSalesOrderByID(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select s.SalesOrderId, s.SalesOrderNo from SalesOrderMasterTable s where s.VendorId=" & id
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetPlanNo() As DataTable
        Dim MasterQuery As String = "select planId , PlanNo  from planmastertable"
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetDeptPlanNo() As DataTable
        Dim MasterQuery As String = "select m.PlanId , m.PlanNo from (select * from PlanTickets p where PlanId  not in (select isnull(PlanId,0) as PlanId from ProductionMasterTable)) p inner join planmastertable m on p.PlanId=m.PlanId group by m.PlanId , m.PlanNo order by m.planno desc"
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function

    Public Function GetPlanNoByID(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select planId , PlanNo  from planmastertable where PoId=" & id
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function

    Public Function GetTicketNo() As DataTable
        Dim MasterQuery As String = "select PlanTicketsId,TicketNo ,ArticleId from plantickets"
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetDeptTicketNo() As DataTable
        Dim MasterQuery As String = "select * from PlanTickets where PlanTicketsId  not in(select isnull (PlanTicketId,0) as PlanTicketId  from ProductionMasterTable)"
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function

    Public Function GetTicketNoByPlan(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select PlanTicketsId,TicketNo, PlanDetailId ,PlanId as PlanMasterID  from plantickets where PlanDetailId IN (select pd.PlanDetailId from planmastertable pm join PlanDetailTable pd on pm.PlanId=pd.PlanId) and  planid=)" & id
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetTicket_Tracking_Grid_ByID(ByVal ID As Integer) As DataTable
        DAL = New CRUD_db()
        Dim Command As SqlCommand = New SqlCommand()
        Command.CommandText = "Sp_Ticket_Tracking"
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.AddWithValue("TicketId", ID)
        Dim table = DAL.Read_SP_Command(Command)
        Return table
    End Function

    Public Function GetTicketDeptNoByPlan(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select * from PlanTickets where PlanTicketsId  not in(select isnull (PlanTicketId,0) as PlanTicketId  from ProductionMasterTable) and PlanId=" & id
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetDept_Tracking_Grid_ByID(ByVal TicketID As Integer, ByVal PlanID As Integer) As DataTable
        DAL = New CRUD_db()
        Dim Command As SqlCommand = New SqlCommand()
        Command.CommandText = "DepartmentWiseTracking"
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.AddWithValue("TicketId", TicketID)
        Command.Parameters.AddWithValue("PlanID", PlanID)
        Dim table = DAL.Read_SP_Command(Command)
        Return table
    End Function
    Public Function GetTicket_Tracking_Grid_ByDate(ByVal startDate As DateTime, ByVal EndDate As DateTime) As DataTable
        Dim Sdate As String = startDate.ToString("d") & " 00:00:00"
        Dim Edate As String = EndDate.ToString("d") & " 23:59:59"
        DAL = New CRUD_db()
        Dim command As SqlCommand = New SqlCommand()
        command.CommandText = "Sp_Ticket_Tracking_DateRange"
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.AddWithValue("StartDate", Sdate)
        command.Parameters.AddWithValue("EndDate", Edate)
        Dim table = DAL.Read_SP_Command(command)
        Return table
    End Function

End Class
