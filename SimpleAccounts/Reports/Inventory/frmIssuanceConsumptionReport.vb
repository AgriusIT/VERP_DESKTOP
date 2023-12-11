''TASK:1142 New Issuance Consumption Report
Imports SBDal
Imports SBModel


Public Class frmIssuanceConsumptionReport
    Dim dtParentItems As DataTable
    Public Sub FillCombo(ByVal Condition As String)
        Dim Query As String = ""
        Try
            If Condition = "Plan" Then
                Query = "Select Distinct PlanmasterTable.PlanId, PlanNo + ' ~ ' + Convert(Varchar(9), PlanDate, 113) As PlanNo From PlanmasterTable INNER JOIN DispatchMasterTable ON PlanMasterTable.PlanId = DispatchMasterTable.PlanId Order By PlanNo Desc"
                FillDropDown(cmbPlan, Query)
            End If
            If Condition = "Ticket" Then
                'Query = " Select Ticket.PlanTicketsMasterID As TicketId, Ticket.TicketNo + '~' + CONVERT(nvarchar(9), TicketDate, 113) As [Ticket No] From PlanTicketsMaster As Ticket  INNER JOIN  MaterialEstimation As Estimation ON Ticket.PlanTicketsMasterID = Estimation.PlanTicketId Where Estimation.MasterPlanId=" & cmbPlan.SelectedValue & " Order By TicketDate DESC"
                Query = "Select Distinct PlanTicketsMaster.PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster Inner Join DispatchMasterTable ON PlanTicketsMaster.PlanTicketsMasterID=DispatchMasterTable.PlanTicketId  Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.SelectedValue & "  Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                FillDropDown(cmbTicket, Query)
            End If
            If Condition = "Estimation" Then
                Query = "Select MaterialEstimation.Id As EstimationId, MaterialEstimation.DocNo + '~' + Convert(nvarchar(9), MaterialEstimation.EstimationDate, 113) As [Estimation No] FROM MaterialEstimation Where MaterialEstimation.PlanTicketId = " & Me.cmbTicket.SelectedValue & " Order By EstimationDate DESC"
                FillDropDown(Me.cmbDepartment, Query)
            End If

            If Condition = "Product" Then
                Query = "Select ArticleDefId, Article.ArticleCode, Article.ArticleDescription From ArticleDefTable As Article INNER JOIN DispatchDetailTable As Detail ON Article.ArticleId = Detail.ArticleDefId Inner Join DispatchMasterTable As Master On Detail.DispatchId = Master.DispatchId Where Master.PlanTicketId =" & Me.cmbTicket.SelectedValue & " And Detail.SubDepartmentID = " & Me.cmbDepartment.SelectedValue & ""
                FillUltraDropDown(Me.cmbProduct, Query)
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ArticleDefId").Hidden = True
            End If

            If Condition = "Department" Then
                Query = "Select Distinct SubDepartmentID, prod_step, prod_Less, sort_Order from tblProSteps INNER JOIN DispatchDetailTable ON tblProSteps.ProdStep_Id = DispatchDetailTable.SubDepartmentId  Inner Join DispatchMasterTable ON DispatchDetailTable.DispatchId = DispatchMasterTable.DispatchId Where DispatchMasterTable.PlanTicketId =" & Me.cmbTicket.SelectedValue & " order by sort_Order "
                'Query = "Select Distinct ProdStep_Id As DepartmentId, prod_step As Department FROM tblproSteps As Department Inner Join DispatchDetailTable As Detail ON Department.ProdStep_Id = Detail.SubDepartmentID Inner Join DispatchMasterTable AS Dispatch On Detail.DispatchId = Dispatch.DispatchId Where Dispatch.PlanTicketId = " & Me.cmbTicket.SelectedValue & ""
                FillDropDown(Me.cmbDepartment, Query)
            End If
            'If Condition = "Tag1" Then
            '    Query = " Select Id, Tag# As [Tag No] FROM MaterialEstimationDetailTable As Detail "
            '    FillDropDown(Me.cmbTag, Query)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmConsumptionEstimationReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombo("Plan")
            Me.rbCode.Checked = True
            'Me.rbConsumed.Checked = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                FillCombo("Ticket")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicket.SelectedIndexChanged
        Try
            If Not Me.cmbTicket.SelectedIndex = -1 Then
                FillCombo("Department")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ID As Integer = 0
        Try
            RemoveHandler cmbPlan.SelectedIndexChanged, AddressOf Me.cmbPlan_SelectedIndexChanged
            RemoveHandler cmbTicket.SelectedIndexChanged, AddressOf Me.cmbTicket_SelectedIndexChanged
            ID = Me.cmbPlan.SelectedValue
            FillCombo("Plan")
            Me.cmbPlan.SelectedValue = ID

            ID = Me.cmbTicket.SelectedValue
            FillCombo("Ticket")
            Me.cmbTicket.SelectedValue = ID

            ID = Me.cmbDepartment.SelectedValue
            FillCombo("Department")
            Me.cmbDepartment.SelectedValue = ID

            ID = Me.cmbProduct.Value
            FillCombo("Product")
            Me.cmbProduct.Value = ID
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            AddHandler cmbPlan.SelectedIndexChanged, AddressOf Me.cmbPlan_SelectedIndexChanged
            AddHandler cmbTicket.SelectedIndexChanged, AddressOf Me.cmbTicket_SelectedIndexChanged
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Dim TicketId As Integer = 0
            Dim DepartmentId As Integer = 0
            Dim ProductId As Integer = 0
            If Not Me.cmbTicket.SelectedIndex = -1 Then
                TicketId = Me.cmbTicket.SelectedValue
            End If
            If Not Me.cmbDepartment.SelectedIndex = -1 Then
                DepartmentId = Me.cmbDepartment.SelectedValue
            End If
            If Not Me.cmbProduct.ActiveRow Is Nothing Then
                ProductId = Me.cmbProduct.Value
            End If
            Dim dt As DataTable = GetDataTable(" Exec sp_IssuanceConsumptionReport " & TicketId & ", " & DepartmentId & ", " & ProductId & "")
            Me.grd.DataSource = dt
            Me.grd.RootTable.Columns("IssuedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ReturnedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ConsumedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("BalanceQty").FormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbTag_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If rbCode.Checked Then
                cmbProduct.DisplayMember = "ArticleCode"
            Else
                cmbProduct.DisplayMember = "ArticleDescription"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow
        Try
           
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            If Not Me.cmbTicket.SelectedIndex = -1 Then
                Me.cmbTicket.SelectedIndex = 0
            End If
            If Not Me.cmbDepartment.SelectedIndex = -1 Then
                Me.cmbDepartment.SelectedIndex = 0
            End If
            If Not Me.cmbProduct.ActiveRow Is Nothing Then
                Me.cmbProduct.Rows(0).Activate()
            End If
            If Not Me.grd.DataSource Is Nothing Then
                Me.grd.DataSource = Nothing
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        If Not cmbDepartment.SelectedIndex = -1 AndAlso Me.cmbDepartment.SelectedValue > 0 Then
            FillCombo("Product")
        End If
    End Sub
End Class