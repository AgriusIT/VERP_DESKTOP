''TASK:1011 to build new report named Consumption Estimation Report done by Ameen 
Imports SBDal
Imports SBModel


Public Class frmConsumptionEstimationReport
    Dim dtParentItems As DataTable
    Public Sub FillCombo(ByVal Condition As String)
        Dim Query As String = ""
        Try
            If Condition = "Plan" Then
                Query = " Select PlanId, PlanNo + '~' + Convert(nvarchar(9),  PlanDate, 113) As [Plan No] From PlanMasterTable Order By PlanDate DESC"
                FillDropDown(cmbPlan, Query)
            End If
            If Condition = "Ticket" Then
                Query = " Select Ticket.PlanTicketsMasterID As TicketId, Ticket.TicketNo + '~' + CONVERT(nvarchar(9), TicketDate, 113) As [Ticket No] From PlanTicketsMaster As Ticket  INNER JOIN  MaterialEstimation As Estimation ON Ticket.PlanTicketsMasterID = Estimation.PlanTicketId Where Estimation.MasterPlanId=" & cmbPlan.SelectedValue & " Order By TicketDate DESC"
                FillDropDown(cmbTicket, Query)
            End If
            If Condition = "Estimation" Then
                Query = "Select MaterialEstimation.Id As EstimationId, MaterialEstimation.DocNo + '~' + Convert(nvarchar(9), MaterialEstimation.EstimationDate, 113) As [Estimation No] FROM MaterialEstimation Where MaterialEstimation.PlanTicketId = " & Me.cmbTicket.SelectedValue & " Order By EstimationDate DESC"
                FillDropDown(Me.cmbEstimation, Query)
            End If
            If Condition = "Tag" Then
                Query = " Select Tag# As TagId, Convert(nvarchar(9), Tag#, 113) As [Tag No], Article.ArticleDescription As Article, Estimation.DocNo As Estimation, Ticket.TicketNo As Ticket, PlanMasterTable.PlanNo As [Plan No] FROM MaterialEstimationDetailTable As Detail INNER JOIN ArticleDefTable As Article ON Detail.ProductId = Article.ArticleId Inner Join MaterialEstimation As Estimation ON Detail.MaterialEstMasterID=Estimation.Id " _
                       & " LEFT OUTER JOIN PlanTicketsMaster As Ticket ON Estimation.PlanTicketId = Ticket.PlanTicketsMasterID LEFT OUTER JOIN PlanMasterTable ON Estimation.MasterPlanId = PlanMasterTable.PlanId Where Estimation.Id =" & Me.cmbEstimation.SelectedValue & " And Tag# <> 0"
                FillUltraDropDown(Me.cmbTag, Query)
                Me.cmbTag.Rows(0).Activate()
                Me.cmbTag.DisplayLayout.Bands(0).Columns("TagId").Hidden = True

            End If
            If Condition = "Tags" Then
                Query = " Select Tag# As TagId, Convert(nvarchar(9), Tag#, 113) As [Tag No], Article.ArticleDescription As Article, Estimation.DocNo As Estimation, Ticket.TicketNo As Ticket, PlanMasterTable.PlanNo As [Plan No] FROM MaterialEstimationDetailTable As Detail INNER JOIN ArticleDefTable As Article ON Detail.ProductId = Article.ArticleId Inner Join MaterialEstimation As Estimation ON Detail.MaterialEstMasterID=Estimation.Id " _
                       & " LEFT OUTER JOIN PlanTicketsMaster As Ticket ON Estimation.PlanTicketId = Ticket.PlanTicketsMasterID LEFT OUTER JOIN PlanMasterTable ON Estimation.MasterPlanId = PlanMasterTable.PlanId Where Tag# <> 0"
                FillUltraDropDown(Me.cmbTag, Query)
                Me.cmbTag.Rows(0).Activate()
                Me.cmbTag.DisplayLayout.Bands(0).Columns("TagId").Hidden = True

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
            FillCombo("Tag1")
            FillCombo("Tags")
            Me.rbTag.Checked = True
            Me.rbConsumed.Checked = True
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
                FillCombo("Estimation")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbEstimation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEstimation.SelectedIndexChanged
        Try
            If Not Me.cmbEstimation.SelectedIndex = -1 Then
                FillCombo("Tag")
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

            ID = Me.cmbEstimation.SelectedValue
            FillCombo("Estimation")
            Me.cmbEstimation.SelectedValue = ID

            ID = Me.cmbTag.Value
            FillCombo("Tags")
            Me.cmbTag.Value = ID
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            AddHandler cmbPlan.SelectedIndexChanged, AddressOf Me.cmbPlan_SelectedIndexChanged
            AddHandler cmbTicket.SelectedIndexChanged, AddressOf Me.cmbTicket_SelectedIndexChanged
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If Not Me.cmbTag.ActiveRow Is Nothing And Me.cmbTag.Value > 0 Then
                Dim EstimationBAL As New MaterialEstimationBAL()
                Dim IsConsumed As Integer = 0
                If Me.rbConsumed.Checked = True Then
                    IsConsumed = 1
                Else
                    IsConsumed = 0
                End If
                Dim dtEC As DataTable = EstimationBAL.GetEstimationConsumption(Me.cmbTag.Value, IsConsumed)
                If Me.rbConsumed.Checked = True Then
                    Dim dtConsumed As DataTable
                    dtParentItems = New DataTable
                    Dim Rows() As DataRow = dtEC.Select("ConsumedQty > 0")
                    dtConsumed = dtEC.Clone
                    For Each DRow As DataRow In Rows
                        GetParents(dtEC, DRow.Item("ParentTag#"))
                    Next
                    'Dim Rows1() As DataRow = dtEC.Select("Tag# > 0")
                    Dim dt1 As New DataTable
                    If Rows.Length > 0 Then
                        dt1 = Rows.CopyToDataTable
                    End If
                    'Dim dt2 As DataTable = Rows1.CopyToDataTable
                    dt1.Merge(dtParentItems)
                    Me.grd.DataSource = dt1
                Else
                    Me.grd.DataSource = dtEC
                End If

                'Me.grd.DataSource = EstimationBAL.GetEstimationConsumption(Me.cmbTag.Value, IsConsumed)
            Else
                ShowErrorMessage("Tag number is required.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbTag_CheckedChanged(sender As Object, e As EventArgs) Handles rbTag.CheckedChanged, rbConsumed.CheckedChanged
        Try
            If rbTag.Checked Then
                cmbTag.DisplayMember = "Tag No"
            Else
                cmbTag.DisplayMember = "Article"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow
        Try
            If Not e.Row Is Nothing Then
                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                    Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
                    If e.Row.Cells("ConsumedQty").Value > 0 AndAlso e.Row.Cells("ConsumedQty").Value < e.Row.Cells("Qty").Value Then
                        'rowStyle.BackColor = Color.LightYellow
                        rowStyle.BackColor = Color.LightYellow

                        e.Row.RowStyle = rowStyle
                    ElseIf e.Row.Cells("ConsumedQty").Value = 0 Then
                        rowStyle.BackColor = Color.LightGray
                        e.Row.RowStyle = rowStyle
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetParents(ByVal dt As DataTable, ByVal ParentTagId As Integer)
        Try
            Dim drArray() As DataRow = dt.Select("[Tag#]='" & ParentTagId & "'")
            If Not drArray Is Nothing AndAlso drArray.Length > 0 Then
                dtParentItems.Merge(drArray.CopyToDataTable)
                Dim ParentTagId1 As Integer = drArray(0).Item("ParentTag#")
                Dim CountBefore As Integer = dt.Rows.Count
                dt.BeginInit()
                dt.Rows.Remove(drArray(0))
                dt.EndInit()
                Dim CountAfter As Integer = dt.Rows.Count
                If ParentTagId1 > 0 Then
                    GetParents(dt, ParentTagId1)
                End If
            End If
        Catch ex As Exception
            Throw ex
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
            If Not Me.cmbEstimation.SelectedIndex = -1 Then
                Me.cmbEstimation.SelectedIndex = 0
            End If
            If Not Me.cmbTag.ActiveRow Is Nothing Then
                Me.cmbTag.Rows(0).Activate()
            End If
            If Not Me.grd.DataSource Is Nothing Then
                Me.grd.DataSource = Nothing
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class