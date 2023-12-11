
Imports SBDal
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports Infragistics.Win.UltraWinTabControl

Public Class frmLeaveApplication

    Dim operations As New CRUD_db
    Enum grdEnm
        ApplicationDate
        ApplicationNo
        LeaveType
        NoOfLeaves
        FromDate
        ToDate
        Reason
        LeaveStation
        ContactNo
        Status
        EmployeeID
    End Enum
    Enum Enumpending
        LeaveType
        NoOfLeaves
    End Enum
    Enum EnumStatus
        ApprovedByName
        ApprovalRemarks
        StatusFlag
        ApprovalDate
    End Enum
    Dim AppID As Integer = 0

    Dim flagrecord As Integer = 0

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim todate As DateTime

            todate = dtpFrom.Value.AddDays(Convert.ToInt32(txtNoOfLeaves.Text))


            If (btnSave.Text = "&Save") Then
                If (txtReason.Text <> "") Then

                    operations.Save("INSERT INTO EmployeeApplicationStatus (ApplicationDate,ApplicationNo,LeaveType,NoOfLeaves,FromDate,ToDate,Reason,LeaveStation,ContactNo,Status,EmployeeID,LeaveTypeID) VALUES ( '" & dtpApplicationDate.Text & "','" & txtApplicationNo.Text & "','" & cmbLeaveType.Text & "','" & txtNoOfLeaves.Text & "','" & dtpFrom.Text & "','" & todate.ToString() & "','" & txtReason.Text & "','" & txtAddress.Text & "','" & txtContactNo.Text & "','P' , '" & cmbEmployees.Value & "'  , '" & cmbLeaveType.SelectedValue & "' )")
                    Reset()
                    History()
                Else
                    ShowErrorMessage("Please enter reason !")
                End If
            Else
                operations.Save("update EmployeeApplicationStatus set LeaveTypeID=' " & cmbLeaveType.SelectedValue & "', ApplicationDate= '" & System.DateTime.Now & "' , LeaveType ='" & cmbLeaveType.Text & "',NoOfLeaves='" & txtNoOfLeaves.Text & "',FromDate='" & dtpFrom.Text & "',ToDate='" & todate.ToString() & "',Reason='" & txtReason.Text & "',LeaveStation='" & txtAddress.Text & "',ContactNo='" & txtContactNo.Text & "'  where EmpApplicationID= '" & AppID & "'  ")
                Reset()
                History()
                btnSave.Text = "&Save"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmLeaveApplication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillUltraDropDown(cmbEmployees, "select e.Employee_ID ,e.Employee_Name, ed.EmployeeDeptName , p.EmployeeDesignationName from tblDefEmployee e inner join  EmployeeDeptDefTable ed on e.Dept_id=ed.EmployeeDeptId inner join EmployeeDesignationDefTable p on p.EmployeeDesignationId=e.Desig_ID", True)
        FillDropDown(cmbLeaveType, "Select Att_Status_Id,Att_Status_Name from tblDefAttendenceStatus where Att_Status_Name Like '%Leave%' And Active=1", True)
        cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
        Dim flagrecord As Boolean = False

        cmbEmployees.Rows(0).Activate()

        ApplyGridSetting()
        txtApplicationNo.Text = GetDocumentNo()
        History()
        GetSecurityRights()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Reset()
        btnSave.Text = "&Save"
    End Sub
    Sub History()
        grdSaved.DataSource = operations.ReadTable("SELECT es.EmpApplicationID,es.ApplicationNo,es.ApplicationDate,e.Employee_Name,l.name as LeaveType,es.NoOfLeaves,case when  es.Status ='A'  then 'Approved' when es.Status='P' then 'Pending' else 'Rejected'  end as Status  ,es.FromDate,es.ToDate,es.ContactNo,es.LeaveStation,es.Reason,es.EmployeeID FROM EmployeeApplicationStatus es inner join tblDefEmployee e on es.EmployeeID=e.Employee_ID inner join LeaveType l on l.LeaveTypeID= es.LeaveTypeID")


    End Sub
    Sub GetRecords()
        Try

            Dim dt As DataTable = operations.ReadTable("select '" & Val(getConfigValueByType("Leave_Days").ToString) & "' as TotalAllowedL, isnull( sum(NoOfLeaves),0 ) as Leaves, '" & Val(getConfigValueByType("Leave_Days").ToString) & "' - isnull(sum(NoOfLeaves) ,0) as RemainingLeaves ,EmployeeID from 	(select * from EmployeeApplicationStatus where  not Status='R' ) as  a where EmployeeID = '" & cmbEmployees.Value & "' group by EmployeeID")
            If (flagrecord > 0) Then
                If ((dt.Rows.Count) - 1 < 0) Then
                    If (cmbEmployees.Value <> 0) Then
                        Dim R As DataRow = dt.NewRow
                        R.Item(0) = "18"
                        R.Item(1) = "0"
                        R.Item(2) = "18"
                        dt.Rows.Add(R)
                    End If


                End If
            End If


            grdPending.DataSource = dt
            flagrecord = flagrecord + 1


            '  grdApproval.DataSource = operations.ReadTable("select ApprovedByName , ApprovalRemarks ,  case when status ='A'  then 'Approved'  else 'Rejected'  end as StatusFlag  , ApprovalDate from EmployeeApplicationStatus where EmployeeID = '" & cmbEmployees.Value & "' and Status in ( 'A', 'R' ) ")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            Return GetSerialNo("APL" + "-" + Microsoft.VisualBasic.Right(Me.dtpApplicationDate.Value.Year, 2) + "-", "EmployeeApplicationStatus", "ApplicationNo")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Sub Reset()

        dtpApplicationDate.Text = System.DateTime.Now
        dtpFrom.Text = System.DateTime.Now
        '     dtpTo.Text = System.DateTime.Now
        txtAddress.ResetText()
        cmbEmployees.ReadOnly = False
        txtApplicationNo.ResetText()
        txtContactNo.ResetText()
        txtNoOfLeaves.Text = "1"
        txtReason.ResetText()
        txtRemarks.ResetText()
        grdApproval.DataSource = Nothing
        grdPending.DataSource = Nothing
        AppID = 0
        btnApprove.Enabled = True
        btnReject.Enabled = True
        lblStatus.Text = "Pending"
        txtApplicationNo.Text = GetDocumentNo()
        GetSecurityRights()


    End Sub
    Private Sub cmbEmployees_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmployees.ValueChanged

        GetRecords()

    End Sub

    Private Sub grdPending_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs)
        ShowRecord()

    End Sub
    Sub ShowRecord()

        'Dim dt As DataTable = operations.ReadTable("SELECT EmpApplicationID,ApplicationDate,ApplicationNo,isnull( LeaveType ,'Select any option') as LeaveType, isnull(NoOfLeaves,0) as NoOfLeaves , FromDate,ToDate, isnull(Reason, '--') as Reason, isnull(LeaveStation, '--') as LeaveStation, isnull(ContactNo, '--') as ContactNo ,isnull(ApprovalRemarks, '--') as ApprovalRemarks,Status,ApprovedByID,ApprovedByName,EmployeeID,ApprovalDate FROM EmployeeApplicationStatus where EmpApplicationID = '" & grdPending.CurrentRow.Cells("EmpApplicationID").Value & "' ")
        Dim dt As DataTable = operations.ReadTable("SELECT EmpApplicationID,ApplicationDate,ApplicationNo,l.name as LeaveType, isnull(NoOfLeaves,0) as NoOfLeaves , FromDate,ToDate, isnull(Reason, '--') as Reason, isnull(LeaveStation, '--') as LeaveStation, isnull(ContactNo, '--') as ContactNo ,isnull(ApprovalRemarks, '--') as ApprovalRemarks,Status,ApprovedByID,ApprovedByName,EmployeeID,ApprovalDate FROM EmployeeApplicationStatus inner join LeaveType l on l.LeaveTypeID= EmployeeApplicationStatus.LeaveTypeID where EmpApplicationID = '" & grdPending.CurrentRow.Cells("EmpApplicationID").Value & "' ")
        If (dt.Rows.Count > 0) Then
            dtpApplicationDate.Text = dt.Rows(0).Item("ApplicationDate")
            dtpFrom.Text = dt.Rows(0).Item("FromDate")
            ' dtpTo.Text = dt.Rows(0).Item("ToDate")
            txtAddress.Text = dt.Rows(0).Item("LeaveStation")
            txtApplicationNo.Text = dt.Rows(0).Item("ApplicationNo")
            txtContactNo.Text = dt.Rows(0).Item("ContactNo")
            txtNoOfLeaves.Text = dt.Rows(0).Item("NoOfLeaves")
            txtReason.Text = dt.Rows(0).Item("Reason")
            cmbLeaveType.Text = dt.Rows(0).Item("LeaveType")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        operations.Save("Delete EmployeeApplicationStatus where EmpApplicationID= '" & txtApplicationNo.Text & "' ")

    End Sub

    Private Sub AppReq_Click(sender As Object, e As EventArgs) Handles AppReq.Click
        GetCrystalReportRights()
        AddRptParam("@flag", 1)
        ShowReport("rpt_EmployeeRequest")
    End Sub

    Private Sub RejReq_Click(sender As Object, e As EventArgs) Handles RejReq.Click
        GetCrystalReportRights()
        AddRptParam("@flag", 2)
        ShowReport("rpt_EmployeeRequest")
    End Sub

    Private Sub PendReq_Click(sender As Object, e As EventArgs) Handles PendReq.Click
        GetCrystalReportRights()
        AddRptParam("@flag", 3)
        ShowReport("rpt_EmployeeRequest")
    End Sub

    Private Sub AllRequests_Click(sender As Object, e As EventArgs) Handles AllRequests.Click
        GetCrystalReportRights()
        AddRptParam("@flag", 4)
        ShowReport("rpt_EmployeeRequest")
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        If (grdSaved.Row < 0) Then
            Exit Sub
        Else

            dtpApplicationDate.Text = grdSaved.CurrentRow.Cells("ApplicationDate").Value
            dtpFrom.Text = grdSaved.CurrentRow.Cells("FromDate").Value
            'dtpTo.Text = grdSaved.CurrentRow.Cells("ToDate").Value
            txtAddress.Text = grdSaved.CurrentRow.Cells("LeaveStation").Value
            txtApplicationNo.Text = grdSaved.CurrentRow.Cells("ApplicationNo").Value
            txtContactNo.Text = grdSaved.CurrentRow.Cells("ContactNo").Value
            txtNoOfLeaves.Text = grdSaved.CurrentRow.Cells("NoOfLeaves").Value
            txtReason.Text = grdSaved.CurrentRow.Cells("Reason").Value
            cmbLeaveType.Text = grdSaved.CurrentRow.Cells("LeaveType").Value
            cmbEmployees.Value = grdSaved.CurrentRow.Cells("EmployeeID").Value
            lblStatus.Text = grdSaved.CurrentRow.Cells("Status").Value
            AppID = grdSaved.CurrentRow.Cells("EmpApplicationID").Value
            If (grdSaved.CurrentRow.Cells("Status").Value = "Pending") Then
                btnApprove.Enabled = True
                btnReject.Enabled = True
            ElseIf (grdSaved.CurrentRow.Cells("Status").Value = "Approved") Then
                btnApprove.Enabled = False
                btnReject.Enabled = True
            Else
                btnApprove.Enabled = True
                btnReject.Enabled = False
            End If

            cmbEmployees.ReadOnly = True
            GetRecords()

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            btnSave.Text = "&Update"
        End If
    End Sub
    Sub ApplyGridSetting()
        btnApprove.Enabled = True
        btnReject.Enabled = True
        'If LoginGroup = "Administrator" Then
        '    grpApproval.Visible = True

        '    Exit Sub
        'End If
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdSaved.RootTable.Columns
            'If col.Index <> grdEnm.ApplicationDate AndAlso col.Index <> grdEnm.ApplicationNo AndAlso col.Index <> grdEnm.ContactNo AndAlso col.Index <> grdEnm.EmployeeID AndAlso col.Index <> grdEnm.FromDate AndAlso col.Index <> grdEnm.LeaveStation AndAlso col.Index <> grdEnm.LeaveType AndAlso col.Index <> grdEnm.LeaveType AndAlso col.Index <> grdEnm.NoOfLeaves AndAlso col.Index <> grdEnm.NoOfLeaves AndAlso col.Index <> grdEnm.Reason AndAlso col.Index <> grdEnm.Status AndAlso col.Index <> grdEnm.ToDate Then
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            ' End If
        Next

        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdPending.RootTable.Columns
            'If col.Index <> Enumpending.LeaveType AndAlso col.Index <> Enumpending.NoOfLeaves Then
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            ' End If
        Next

        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdApproval.RootTable.Columns
            'If col.Index <> EnumStatus.ApprovalDate AndAlso col.Index <> EnumStatus.ApprovalRemarks AndAlso col.Index <> EnumStatus.ApprovedByName AndAlso col.Index <> EnumStatus.StatusFlag Then
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            ' End If
        Next

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmRptGrdStockStatement)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save").ToString()
                        End If
                        If Me.btnSave.Text = "Update" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update").ToString()
                        End If
                        If Me.btnDelete.Text = "Delete" Then
                            Me.btnDelete.Enabled = dt.Rows(0).Item("Delete").ToString()
                        End If
                    End If
                End If
            Else
                Me.Visible = False
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        If Me.btnDelete.Text = "&Delete" Then btnDelete.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            operations.Save("update EmployeeApplicationStatus set Status ='A' , ApprovalDate= '" & System.DateTime.Now & "' ,ApprovalRemarks ='" & txtRemarks.Text & " ', ApprovedByID= '" & LoginUserId & "' ,ApprovedByName= '" & LoginUserName & "' where ApplicationNo= '" & txtApplicationNo.Text & "' ")

            Reset()
            GetRecords()
            History()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Try

            operations.Save("update EmployeeApplicationStatus set Status ='R' , ApprovalDate= '" & System.DateTime.Now & "' ,ApprovalRemarks ='" & txtRemarks.Text & " ', ApprovedByID= '" & LoginUserId & "' ,ApprovedByName= '" & LoginUserName & "' where ApplicationNo= '" & txtApplicationNo.Text & "' ")
            Reset()
            GetRecords()
            History()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



End Class