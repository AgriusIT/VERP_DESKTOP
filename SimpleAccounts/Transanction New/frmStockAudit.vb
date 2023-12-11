Imports SBModel
Imports SBDal
Public Class frmStockAudit

    Dim Obj As StockAuditTableBE
    Dim ObjDAL As StockAuditDAL
    Dim LocationDAL As StockAuditLocationsDAL
    Dim ID As Integer = 0
    Dim IsEditMode As Boolean = False
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IsValid() Then
            ObjDAL = New StockAuditDAL()
            If IsEditMode = False Then
                Obj.ActivityLog.ActivityName = "Save"
                Obj.ActivityLog.ApplicationName = String.Empty
                Obj.ActivityLog.FormCaption = "Stock Audit"
                Obj.ActivityLog.FormName = "frmStockAudit"
                Obj.ActivityLog.LogDateTime = Now
                Obj.ActivityLog.RecordType = String.Empty
                Obj.ActivityLog.RefNo = String.Empty
                Obj.ActivityLog.Source = "frmStockAudit"
                Obj.ActivityLog.User_Name = LoginUserName
                Obj.ActivityLog.UserID = LoginUserId
                If ObjDAL.Add(Obj) Then
                    msg_Information("Record has been saved successfully.")
                    RefreshControls()
                End If
            Else
                Obj.ActivityLog.ActivityName = "Update"
                Obj.ActivityLog.ApplicationName = String.Empty
                Obj.ActivityLog.FormCaption = "Stock Audit"
                Obj.ActivityLog.FormName = "frmStockAudit"
                Obj.ActivityLog.LogDateTime = Now
                Obj.ActivityLog.RecordType = String.Empty
                Obj.ActivityLog.RefNo = String.Empty
                Obj.ActivityLog.Source = "frmStockAudit"
                Obj.ActivityLog.User_Name = LoginUserName
                Obj.ActivityLog.UserID = LoginUserId
                If ObjDAL.Update(Obj) Then
                    msg_Information("Record has been updated successfully.")
                    RefreshControls()
                End If
            End If
        End If
    End Sub

    Private Sub FillModel()
        Try
            Obj = New StockAuditTableBE()
            Obj.StockDate = dtpStockAudit.Value
            Obj.StockAuditName = txtStockAuditName.Text
            Obj.SessionName = txtSessionName.Text
            Obj.Remarks = txtRemarks.Text
            Obj.IsClosed = cbStatus.Checked
            Obj.ID = ID
            For Each LocationId As String In Me.lstLocations.SelectedIDs.Split(",")
                Dim Location As New StockAuditLocationsBE
                Location.ID = 0
                Location.LocationId = Val(LocationId)
                Location.StockAuditId = ID
                Obj.Locations.Add(Location)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsValid() As Boolean
        Try
            If Me.txtStockAuditName.Text.Length = 0 Then
                ShowErrorMessage("Stock Audit Name is required.")
                txtStockAuditName.Focus()
                Return False
            End If
            If Me.txtSessionName.Text.Length = 0 Then
                ShowErrorMessage("Session Name is required.")
                txtSessionName.Focus()
                Return False
            End If
            If Me.lstLocations.SelectedItems.Length = 0 Then
                ShowErrorMessage("At least one location is required.")
                txtSessionName.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub RefreshControls()
        Try
            Me.btnSave.Text = "&Save"
            GetSecurityRights()
            ID = 0
            Me.dtpStockAudit.Value = Now
            Me.txtStockAuditName.Text = String.Empty
            Me.txtSessionName.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.lstLocations.DeSelect()
            Me.cbStatus.Checked = False
            GetAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditRecord()
        Try
            ID = Me.grdStockAudit.GetRow.Cells("ID").Value
            Me.dtpStockAudit.Value = Me.grdStockAudit.GetRow.Cells("AuditDate").Value
            Me.txtStockAuditName.Text = Me.grdStockAudit.GetRow.Cells("StockAuditName").Value.ToString
            Me.txtSessionName.Text = Me.grdStockAudit.GetRow.Cells("SessionName").Value.ToString
            Me.txtRemarks.Text = Me.grdStockAudit.GetRow.Cells("Remarks").Value.ToString
            Me.cbStatus.Checked = CBool(Me.grdStockAudit.GetRow.Cells("IsClosed").Value)


            LocationDAL = New StockAuditLocationsDAL()
            Dim dt As DataTable = LocationDAL.GetAll(ID)
            Dim LocationIds As String = String.Empty
            For Each Row As DataRow In dt.Rows
                If Val(Row.Item("LocationId").ToString) > 0 Then
                    If LocationIds.Length > 0 Then
                        LocationIds = LocationIds & "," & Row.Item("LocationId").ToString
                    Else
                        LocationIds = Row.Item("LocationId").ToString
                    End If
                End If
            Next
            If LocationIds.Length > 0 Then
                lstLocations.SelectItemsByIDs(LocationIds)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdStockAudit_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdStockAudit.RowDoubleClick
        Try
            If Me.grdStockAudit.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.EditRecord()
                IsEditMode = True
                Me.btnSave.Text = "&Update"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdStockAudit_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStockAudit.ColumnButtonClick
        Try
            If Me.grdStockAudit.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Obj = New StockAuditTableBE()
                ID = Me.grdStockAudit.GetRow.Cells("ID").Value
                Obj.ID = ID
                Obj.StockDate = Me.grdStockAudit.GetRow.Cells("AuditDate").Value
                Obj.StockAuditName = Me.grdStockAudit.GetRow.Cells("StockAuditName").Value.ToString
                Obj.SessionName = Me.grdStockAudit.GetRow.Cells("SessionName").Value.ToString
                Obj.SessionName = Me.grdStockAudit.GetRow.Cells("Remarks").Value.ToString
                ObjDAL = New StockAuditDAL()
                If ObjDAL.Delete(Obj) = True Then
                    msg_Information("Record has been successfully deleted.")
                    Me.grdStockAudit.GetRow.Delete()
                Else
                    ShowErrorMessage("Record could not delete.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmStockAudit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillListBox(Me.lstLocations.ListItem, "Select Location_Id, Location_Name, Location_Type From tblDefLocation")
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAll()
        Try
            ObjDAL = New StockAuditDAL()
            Me.grdStockAudit.DataSource = ObjDAL.GetAll()
            Me.grdStockAudit.RootTable.Columns("AuditDate").FormatString = str_DisplayDateFormat
            'Me.grdStockAudit.RootTable.Columns("ID").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnsave.Text = "Save" Or Me.btnsave.Text = "&Save" Then
                            Me.btnsave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnsave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString

                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then

                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True

                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True

                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True

                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            FillListBox(Me.lstLocations.ListItem, "Select Location_Id, Location_Name, Location_Type From tblDefLocation")
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStockAudit.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStockAudit.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdStockAudit.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Stock Audit"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class