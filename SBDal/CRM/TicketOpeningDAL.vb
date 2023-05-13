Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class TicketOpeningDAL
    Dim TicketId As Integer

    Public Function Save(ByVal POS As TicketOpeningBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Rafay:Task Start:Modified query to save customer name ,company name and contract id,ChkBoxBatteriesIncluded  
            str = "INSERT INTO TicketMasterTable (TicketNo ,TicketDate ,SerialNo ,CallerName ,CustomerName ,CompanyName ,ContactVia ,ContactTime ,Severity ,EngineerAssigned ,InitialProblem, Status, ContractId, Brand, ModelNo,ChkBoxBatteriesIncluded,Site,SaleOrderId) VALUES (N'" & POS.TicketNo & "', N'" & POS.TicketDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & POS.SerialNo & "', N'" & POS.CallerName & "', N'" & POS.CustomerName & "', N'" & POS.CompanyName & "',N'" & POS.ContactVia & "',N'" & POS.ContactTime.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & POS.Severity & "',N'" & POS.EngineerAssigned & "',N'" & POS.InitialProblem & "',N'" & POS.Status & "'," & POS.ContractId & " ,N'" & POS.Brand & "',N'" & POS.ModelNo & "'," & IIf(POS.ChkBoxBatteriesIncluded = True, 1, 0) & " ,N'" & POS.Site & "' ,N'" & POS.SaleOrderId & "') Select @@Identity"
            TicketId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function SaveCreditDetail(ByVal list As List(Of TicketOpeningDetailBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As TicketOpeningDetailBE In list
                str = "INSERT INTO TicketDetailTable (TicketId ,OnsiteEngineer ,TimeOnSite ,ActivityStart ,ActivityEnd ,ActivityPerformed ,TicketState ,Escalation ,PNUsed, Partno,Quantity,PartDescription,FaultyPartSN) Values(" & TicketId & ", N'" & obj.OnsiteEngineer & "', N'" & obj.TimeOnSite.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & obj.ActivityStart.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & obj.ActivityEnd.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & obj.ActivityPerformed & "', N'" & obj.TicketState & "', N'" & obj.Escalation & "', N'" & obj.PNUsed & "', N'" & obj.Partno & "', N'" & obj.Quantity & "', N'" & obj.PartDescription & "', N'" & obj.FaultyPartSN & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function Update(ByVal POS As TicketOpeningBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Rafay:Task Start:Modified query to update customer name ,company name and contract id,ChkBoxBatteriesIncluded = " & IIf(objModel.ChkBoxBatteriesIncluded = True, 1, 0) & " added on 12-4-22
            str = "Update TicketMasterTable Set TicketNo=N'" & POS.TicketNo & "',TicketDate=N'" & POS.TicketDate.ToString("yyyy-M-d h:mm:ss tt") & "',SerialNo=N'" & POS.SerialNo & "',CallerName=N'" & POS.CallerName & "',CustomerName=N'" & POS.CustomerName & "',CompanyName=N'" & POS.CompanyName & "',ContactVia=N'" & POS.ContactVia & "',ContactTime=N'" & POS.ContactTime.ToString("yyyy-M-d h:mm:ss tt") & "',Severity=N'" & POS.Severity & "',EngineerAssigned=N'" & POS.EngineerAssigned & "',InitialProblem=N'" & POS.InitialProblem & "', Status =N'" & POS.Status & "', ContractId =" & POS.ContractId & " , Brand =N'" & POS.Brand & "' , ModelNo =N'" & POS.ModelNo & "' , ChkBoxBatteriesIncluded = " & IIf(POS.ChkBoxBatteriesIncluded = True, 1, 0) & " , Site =N'" & POS.Site & "' , SaleOrderId= N'" & POS.SaleOrderId & "'   WHERE TicketId = " & POS.TicketId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            TicketId = POS.TicketId
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Public Function UpdateCreditDetail(ByVal list As List(Of TicketOpeningDetailBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As TicketOpeningDetailBE In list
                str = "If Exists(Select ISNULL(TicketDetailId, 0) as TicketDetailId From TicketDetailTable Where TicketDetailId=" & obj.TicketDetailId & ") Update TicketDetailTable Set TicketId =" & TicketId & ", OnsiteEngineer=N'" & obj.OnsiteEngineer & "',TimeOnSite =N'" & obj.TimeOnSite.ToString("yyyy-M-d h:mm:ss tt") & "',ActivityStart =N'" & obj.ActivityStart.ToString("yyyy-M-d h:mm:ss tt") & "',ActivityEnd =N'" & obj.TimeOnSite.ToString("yyyy-M-d h:mm:ss tt") & "', ActivityPerformed=N'" & obj.ActivityPerformed & "', TicketState=N'" & obj.TicketState & "', Escalation=N'" & obj.Escalation & "', PNUsed=N'" & obj.PNUsed & "', Partno=N'" & obj.Partno & "', Quantity=N'" & obj.Quantity & "', PartDescription=N'" & obj.PartDescription & "', FaultyPartSN=N'" & obj.FaultyPartSN & "'  WHERE TicketDetailId = " & obj.TicketDetailId & "" _
                 & " Else INSERT INTO TicketDetailTable (TicketId ,OnsiteEngineer ,TimeOnSite ,ActivityStart ,ActivityEnd ,ActivityPerformed ,TicketState ,Escalation ,PNUsed ,Partno,Quantity,PartDescription,FaultyPartSN) Values(" & TicketId & ", N'" & obj.OnsiteEngineer & "', N'" & obj.TimeOnSite.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & obj.ActivityStart.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & obj.ActivityEnd.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & obj.ActivityPerformed & "', N'" & obj.TicketState & "', N'" & obj.Escalation & "', N'" & obj.PNUsed & "', N'" & obj.Partno & "', N'" & obj.Quantity & "', N'" & obj.PartDescription & "', N'" & obj.FaultyPartSN & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function
    Public Function DeleteDetail(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = "Delete from TicketDetailTable Where TicketDetailId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
End Class
