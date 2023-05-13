''TASK TFS3444 Muhammad Amin verified Production Ticket List View Report on 29-05-2018.
''TASK TFS3586 Muhammad Amin got hierarchical items expanded. 22-06-2018
Imports SBDal
Imports SBModel
Public Class frmProductionTicketsView


    Private Sub FillCombos(ByVal Condition As String)
        Dim Query As String = String.Empty
        Try
            If Condition = "SO" Then
                'Query = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable AS SalesOrder INNER JOIN PlanTicketsMaster AS Ticket ON SalesOrder.POId = Ticket.SalesOrderId ORDER BY SalesOrderMasterTable.SalesOrderDate DESC "
                Query = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable ORDER BY SalesOrderMasterTable.SalesOrderDate DESC "

                FillDropDown(cmbSalesOrder, Query)
            ElseIf Condition = "Plan" Then
                Query = "Select PlanId, PlanNo + ' ~ ' + Convert(varchar(12), PlanDate,113) as PlanNo, PlanNo As UsedForTicket from PlanMasterTable " & IIf(Me.cmbSalesOrder.SelectedValue > 0, " Where PlanMasterTable.POId = " & Me.cmbSalesOrder.SelectedValue & "", "") & " Order by PlanDate DESC "
                FillDropDown(cmbPlan, Query)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmProductionTicketsView_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("SO")
            FillCombos("Plan")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalesOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.SelectedIndexChanged
        Try
            If Not cmbSalesOrder.SelectedIndex = -1 Then
                FillCombos("Plan")
                If Me.cmbSalesOrder.SelectedValue > 0 Then
                    Me.grdDetail.DataSource = BulkTicketsCreationDAL.GetTickets(Me.cmbSalesOrder.SelectedValue, Me.cmbPlan.SelectedValue)
                    Me.grdDetail.ExpandRecords()
                Else
                    Me.grdDetail.DataSource = BulkTicketsCreationDAL.GetTickets(Me.cmbSalesOrder.SelectedValue, Me.cmbPlan.SelectedValue)
                    Me.grdDetail.ExpandRecords()

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.LinkClicked
        Try
            If e.Column.Key = "Ticket" Then

                'Task 3551 Track Ticket and Open it as popup from frmPlanTicketStandard

                If frmPlanTicketStandard.isFormOpen = True Then
                    frmPlanTicketStandard.Dispose()
                End If
                'frmPlanTicketStandard
                frmPlanTicketStandard.IsEditMode = True
                frmPlanTicketStandard.Ticket_Id = Val(Me.grdDetail.CurrentRow.Cells("TicketId").Value.ToString)
                frmPlanTicketStandard.ShowDialog()

                frmPlanTicketStandard.Ticket_Id = 0
                frmPlanTicketStandard.MasterArticleId = 0

                'Dim dt As DataTable = BulkTicketsCreationDAL.GetTicket(Val(Me.grdDetail.GetRow.Cells("TicketId").Value.ToString))
                'If dt.Rows.Count > 0 Then
                '    frmMain.LoadControl("frmPlanTicketStandard")
                '    frmPlanTicketStandard.DisplayTicket(dt)
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbSalesOrder.SelectedValue
            FillCombos("SO")
            Me.cmbSalesOrder.SelectedIndex = 0
            Id = Me.cmbPlan.SelectedValue
            FillCombos("Plan")
            Me.cmbPlan.SelectedIndex = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                If Me.cmbPlan.SelectedValue > 0 Then
                    Me.grdDetail.DataSource = BulkTicketsCreationDAL.GetTickets(Me.cmbSalesOrder.SelectedValue, Me.cmbPlan.SelectedValue)
                    Me.grdDetail.ExpandRecords()
                End If
            End If
            'Dim dt As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            ''Dim _Rows() As Janus.Windows.GridEX.GridEXRow = Me.grdDetail.GetDataRows
            'Dim dr() As DataRow = dt.Select("TicketId = " & 1 & "")
            'For Each Row As DataRow In dr

            'Next


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            If Not Me.cmbSalesOrder.SelectedIndex = -1 Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            Me.grdDetail.DataSource = BulkTicketsCreationDAL.GetTickets(-1, -1)
            Me.grdDetail.ExpandRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class