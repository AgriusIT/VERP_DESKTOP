Imports SBDal
Imports SBModel
Imports Janus.Windows.GridEX.GridEXRow


Public Class frmTicketTracking
    Enum grdEnum
        PlanTicketId
        PlanTicket_No
        MaterialEstID
        MaterialEst_DocNo
        MaterialAnalysis_ID
        MaterialAnalysis_DocNo
        MaterialAllocation_ID
        MaterialAllocation_DocNo
        Dispatch_ID
        Dispatch_No
        Production_ID
        Production_No
    End Enum
    Dim table As DataTable
    Dim TicketObj As TicketTrackingBAL
    Private Sub GridEX1_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.BackColor = Color.Blue
        fillcombobox()
    End Sub
    Private Sub fillcombobox()
        TicketObj = New TicketTrackingBAL()
        cmbsaleorder.DataSource = TicketObj.GetSalesOrder()
        cmbsaleorder.DisplayMember = "SalesOrderNo"
        cmbsaleorder.ValueMember = "SalesOrderId"

        cmbplanno.DataSource = TicketObj.GetPlanNo()
        cmbplanno.DisplayMember = "PlanNo"
        cmbplanno.ValueMember = "planId"

        FillDropDown(cmbticketno, "SELECT PlanTicketsMasterID, TicketNo FROM PlanTicketsMaster")

        FillDropDown(cmbplannoDept, "Select m.PlanId , m.PlanNo from (select * from PlanTicketsMaster p where PlanId  not in (select isnull(PlanId,0) as PlanId from ProductionMasterTable)) p inner join planmastertable m on p.PlanId=m.PlanId group by m.PlanId , m.PlanNo order by m.planno desc")


        FillDropDown(cmbticketnoDetp, "Select * from PlanTicketsMaster where PlanTicketsMasterID  not in(select isnull (PlanTicketID,0) as PlanTicketId  from ProductionMasterTable)")
        'cmbplanno.DataSource = TicketObj.GetPlanNo()
        'cmbplanno.DisplayMember = "PlanNo"
        'cmbplanno.ValueMember = "planId"

        'cmbticketno.DataSource = TicketObj.GetTicketNo()
        'cmbticketno.DisplayMember = "TicketNo"
        'cmbticketno.ValueMember = "PlanTicketsId"



        'cmbplannoDept.DataSource = TicketObj.GetDeptPlanNo()
        'cmbplannoDept.DisplayMember = "PlanNo"
        'cmbplannoDept.ValueMember = "planId"

        'cmbticketnoDetp.DataSource = TicketObj.GetDeptTicketNo()
        'cmbticketnoDetp.DisplayMember = "TicketNo"
        'cmbticketnoDetp.ValueMember = "PlanTicketsId"

    End Sub

    Private Sub cmbsaleorder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsaleorder.SelectedIndexChanged
        Try

            Dim Id As Integer = Val(cmbsaleorder.SelectedValue)
            FillDropDown(cmbticketnoDetp, "select planId , PlanNo  from planmastertable where PoId=" & Id)
            'cmbplanno.DataSource = TicketObj.GetPlanNoByID(Id)
            'cmbplanno.DisplayMember = "PlanNo"
            'cmbplanno.ValueMember = "planId"
            ' FillUltraDropDown(cmbplanno,
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbplanno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbplanno.SelectedIndexChanged
        Try
            Dim Id As Integer = Val(cmbplanno.SelectedValue)
            FillDropDown(cmbticketnoDetp, "select PlanTicketsId,TicketNo, PlanDetailId ,PlanId as PlanMasterID  from plantickets where PlanDetailId IN (select pd.PlanDetailId from planmastertable pm join PlanDetailTable pd on pm.PlanId=pd.PlanId) and  planid=)" & Id)

            'cmbticketno.DataSource = TicketObj.GetTicketNoByPlan(Id)
            'cmbticketno.DisplayMember = "TicketNo"
            'cmbticketno.ValueMember = "PlanTicketsId"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbticketno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbticketno.SelectedIndexChanged

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            If (dtpfromdate.Checked = True And dtptodate.Checked = True) Then
                grdTicketTracking.DataSource = TicketObj.GetTicket_Tracking_Grid_ByDate(dtpfromdate.Value, dtptodate.Value)
            ElseIf (Val(cmbticketno.SelectedValue > 0)) Then
                grdTicketTracking.DataSource = TicketObj.GetTicket_Tracking_Grid_ByID(Val(cmbticketno.SelectedValue))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            ShowReport("rptTicketTracking", "Sp_Ticket_Tracking =" & grdTicketTracking.CurrentRow.Cells("PlanTicketId").Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            grdTicketTracking.DataSource = TicketObj.GetTicket_Tracking_Grid_ByID(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            grdTicketTracking.DataSource = TicketObj.GetTicket_Tracking_Grid_ByID(-1)
            grdDeptTracking.DataSource = TicketObj.GetDept_Tracking_Grid_ByID(-1, -1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabPageControl1_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim TicketNo As Integer = Val(cmbticketnoDetp.SelectedValue)
        Dim PlanNo As Integer = Val(cmbplannoDept.SelectedValue)
        grdDeptTracking.DataSource = TicketObj.GetDept_Tracking_Grid_ByID(TicketNo, PlanNo)
    End Sub

    Private Sub cmbplannoDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbplannoDept.SelectedIndexChanged
        Try
            Dim Id As Integer = Val(cmbplannoDept.SelectedValue)
            FillDropDown(cmbticketnoDetp, "Select * from PlanTicketsMaster where PlanTicketsMasterID  not in(select isnull (PlanTicketID,0) as PlanTicketId  from ProductionMasterTable) and PlanId=" & Id)

            'cmbticketnoDetp.DataSource = TicketObj.GetTicketDeptNoByPlan(Id)
            'cmbticketnoDetp.DisplayMember = "TicketNo"
            'cmbticketnoDetp.ValueMember = "PlanTicketsId"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbticketnoDetp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbticketnoDetp.SelectedIndexChanged

    End Sub
End Class
