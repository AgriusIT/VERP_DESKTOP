Imports SBModel
Imports SBDal

Public Class frmLogicalBifurcationList
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Me.CtrlGrdBar3.mGridPrint.Enabled = True
                Me.CtrlGrdBar3.mGridExport.Enabled = True
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.CtrlGrdBar3.mGridPrint.Enabled = False
                    Me.CtrlGrdBar3.mGridExport.Enabled = False
                    Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.CtrlGrdBar3.mGridPrint.Enabled = False
                Me.CtrlGrdBar3.mGridExport.Enabled = False
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar3.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar3.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAll()
        Try
            Me.grdBifurcation.DataSource = New LogicalBifurcationDAL().GetAll()
            Me.grdBifurcation.RootTable.Columns("DocumentDate").FormatString = str_DisplayDateFormat
            Me.grdBifurcation.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmLogicalBifurcationList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetAll()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdBifurcation_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdBifurcation.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If DoHaveDeleteRights = True Then
                    If msg_Confirm("Do you want to delete selected row?") = False Then Exit Sub
                    If Me.grdBifurcation.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Dim LogicalB As New LogicalBifurcationBE
                        LogicalB.LogicalBifurcationId = Me.grdBifurcation.GetRow.Cells("LogicalBifurcationId").Value
                        LogicalB.DocumentNo = Me.grdBifurcation.GetRow.Cells("DocumentNo").Value.ToString
                        'LogicalB.DocumentDate = Me.grdBifurcation.GetRow.Cells("DocumentDate").Value
                        'LogicalB.FromCostCenterId = Me.grdBifurcation.GetRow.Cells("FromCostCenterId").Value
                        'LogicalB.Remarks = Me.grdBifurcation.GetRow.Cells("Remarks").Value.ToString
                        LogicalB.ActivityLog.ActivityName = "Delete"
                        LogicalB.ActivityLog.ApplicationName = "Accounts"
                        LogicalB.ActivityLog.RefNo = LogicalB.DocumentNo
                        LogicalB.ActivityLog.FormCaption = "Logical Bifurcation"
                        LogicalB.ActivityLog.FormName = "frmLogicalBifurcation"
                        LogicalB.ActivityLog.LogDateTime = Now
                        LogicalB.ActivityLog.User_Name = LoginUserName
                        LogicalB.ActivityLog.UserID = LoginUserId
                        LogicalB.ActivityLog.Source = "frmLogicalBifurcation"
                        If New LogicalBifurcationDAL().Delete(LogicalB) = True Then
                            msg_Information("Record has been deleted successfully.")
                            GetAll()
                        End If
                    End If
                    GetAll()
                Else
                    msg_Information("You do not have rights to delete.")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdBifurcation.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdBifurcation.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdBifurcation.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdBifurcation_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdBifurcation.RowDoubleClick
        Try
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim LogicalB As New LogicalBifurcationBE
                LogicalB.LogicalBifurcationId = Me.grdBifurcation.GetRow.Cells("LogicalBifurcationId").Value
                LogicalB.DocumentNo = Me.grdBifurcation.GetRow.Cells("DocumentNo").Value.ToString
                LogicalB.DocumentDate = Me.grdBifurcation.GetRow.Cells("DocumentDate").Value
                LogicalB.StartDate = Me.grdBifurcation.GetRow.Cells("StartDate").Value
                LogicalB.FromCostCenterId = Me.grdBifurcation.GetRow.Cells("FromCostCenterId").Value
                LogicalB.Remarks = Me.grdBifurcation.GetRow.Cells("Remarks").Value.ToString
                Dim frm As New frmLogicalBifurcation(LogicalB, DoHaveUpdateRights)
                frm.ShowDialog()
                GetAll()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        Try
            'Me.Panel1.BackColor = Color.Blue
            'Me.Label2.ForeColor = Color.White
            Dim frm As New frmLogicalBifurcation(DoHaveSaveRights)
            frm.ShowDialog()
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class