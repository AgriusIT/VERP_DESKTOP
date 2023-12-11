Imports System.Data.OleDb
Public Class frmCustomerRecoveryTarget
    Dim IsFormOpend As Boolean = False
    Dim IsEditMode As Boolean = False
    Dim CurrentId As Integer
    Enum Customer
        Id
        Name
        Code
        State
        City
        Territory
    End Enum
    Private Sub frmCustomerRecoveryTarget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmCustomerRecoveryTarget_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            FillCombo("Vendor")
            Me.IsFormOpend = True
            RefreshControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False

        End Try
    End Sub

    Private Sub FillCombo(ByVal strCondition As String)
        Try
            Dim str As String = String.Empty
            If strCondition = "Vendor" Then
                'str = "SELECT     tblCustomer.customerid AS Id, tblCustomer.CustomerName as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                '                   "tblListTerritory.TerritoryName as Territory  " & _
                '                   "FROM  tblCustomer LEFT OUTER JOIN " & _
                '                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                '                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                '                   "tblListState ON tblListCity.StateId = tblListState.StateId  " & _
                '                   "WHERE tblcustomer.customerid is not  null "
                str = "Select coa_detail_id as Id, detail_title as [Name],detail_code as [Code], StateName as State, CityName as City,TerritoryName as Area From vwCOADetail WHERE Detail_title <> ''"

                FillUltraDropDown(cmbVendor, str)
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = False
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub RefreshControls()
        Try
            Me.IsEditMode = False
            Me.txtRemarks.Text = ""
            Me.txtRecoveryAmount.Text = ""
            If Me.cmbVendor.Rows.Count > 0 Then cmbVendor.Rows(0).Activate()
            Me.BtnEdit.Visible = False
            Me.BtnPrint.Visible = False
            Me.cmbVendor.Enabled = True
            Me.dtpTargetMonth.Enabled = True
            Me.BtnSave.Text = "&Save"
            dtpTargetMonth.CustomFormat = "MMM-yyyy"
            BindGrid()
            IsEditMode = False
            BtnDelete.Visible = False
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim id As Integer
            id = 0
            id = Me.cmbVendor.Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim cm As New OleDbCommand
        If Me.cmbVendor.Value = 0 Then

            msg_Error("Please select customer ")
            Me.cmbVendor.Focus()
            Exit Sub

        End If
        If Me.txtRecoveryAmount.Text = "" Then
            msg_Error("Please enter recovery amount")
            Me.txtRecoveryAmount.Focus()
            Exit Sub
        End If




        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Try
            If Me.BtnSave.Text = "&Save" Then
                Dim strSql As String = "SELECT * from tblCustomerRecoveryTarget where CustomerId  =" & cmbVendor.Value & "  and month(recoverymonth) = '" & dtpTargetMonth.Value.Month & "' and year(recoverymonth) = '" & dtpTargetMonth.Value.Year & "'"
                Dim dt As New DataTable
                Dim adp As New OleDbDataAdapter
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                If dt.Rows.Count = 0 Then
                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    cm.CommandText = "insert into tblCustomerRecoveryTarget(CustomerId,RecoveryAmount, Remarks,RecoveryMonth ) values(" & Me.cmbVendor.Value & ",N'" & Me.txtRecoveryAmount.Text.ToString.Replace("'", "''") & "',N'" & Me.txtRemarks.Text.ToString.Replace("'", "''") & "','" & dtpTargetMonth.Value & "') Select @@Identity"
                Else
                    ShowErrorMessage("Record had already entered for current month for this customer")
                    Exit Sub
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                cm.CommandText = "update tblCustomerRecoveryTarget set RecoveryAmount = " & Val(txtRecoveryAmount.Text) & ", Remarks=N'" & Me.txtRemarks.Text.ToString.Replace("'", "''") & "' where targetid=" & Me.CurrentId
            End If

            Dim identity As Integer = Convert.ToInt32(cm.ExecuteScalar())
            Try

                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception
            End Try
            Me.CurrentId = 0
        Catch ex As Exception
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshControls()
    End Sub
    Sub BindGrid()

        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        Try
            'adp = New OleDbDataAdapter(" select a.targetid,a.CustomerId,b.customername,a.RecoveryAmount, a.Remarks, a.RecoveryMonth from tblCustomerRecoveryTarget a inner join tblcustomer b on b.customerid = a.customerid order by a.targetid desc   ", Con)
            adp = New OleDbDataAdapter(" select a.targetid,a.CustomerId,b.detail_title as customername,a.RecoveryAmount, a.Remarks, a.RecoveryMonth from tblCustomerRecoveryTarget a inner join vwCOADetail b on b.coa_detail_id = a.customerid order by a.targetid desc   ", Con)
            adp.Fill(dt)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns(1).Visible = False
            Me.grdSaved.RootTable.Columns("RecoveryAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("RecoveryAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("RecoveryAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("RecoveryAmount").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("RecoveryAmount").TotalFormatString = "N" & TotalAmountRounding
            Me.grdSaved.RootTable.Columns("RecoveryAmount").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("RecoveryAmount").TotalFormatString = "N" & TotalAmountRounding
            Me.grdSaved.RootTable.Columns("RecoveryMonth").FormatString = "MMM-yyyy"
            Me.grdSaved.RootTable.Columns("RecoveryMonth").Caption = "Month"
            Me.grdSaved.RootTable.Columns("customername").Caption = "Customer Name"
            Me.grdSaved.RootTable.Columns("Recoveryamount").Caption = "Amount"
            Me.grdSaved.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub EditRecord()
        Try
            Me.txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value.ToString
            Me.txtRecoveryAmount.Text = grdSaved.CurrentRow.Cells("RecoveryAmount").Value.ToString
            Me.CurrentId = Me.grdSaved.CurrentRow.Cells(0).Value
            Me.dtpTargetMonth.Value = grdSaved.CurrentRow.Cells("RecoveryMonth").Value.ToString
            Me.cmbVendor.Value = grdSaved.CurrentRow.Cells("CustomerId").Value
            Me.cmbVendor.Enabled = False
            Me.dtpTargetMonth.Enabled = False
            IsEditMode = True
            Me.BtnSave.Text = "&Update"
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            Me.GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        If Not Me.grdSaved.GetRow Is Nothing Then
            Me.EditRecord()
        End If
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCity)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtRecoveryAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRecoveryAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim cm As New OleDbCommand

        If Not grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        If msg_Confirm(str_ConfirmDelete) = True Then
            Try
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Con.State = ConnectionState.Closed Then Con.Open()
                cm.Connection = Con
                cm.CommandText = "delete from tblCustomerRecoveryTarget where targetid=" & Me.grdSaved.CurrentRow.Cells("TargetId").Value.ToString
                cm.ExecuteNonQuery()
                Me.CurrentId = 0

                Try
                    SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.grdSaved.CurrentRow.Cells("TargetId").Value.ToString, True)
                Catch ex As Exception
                End Try
            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)
            Finally
                Con.Close()
                Me.lblProgress.Visible = False
            End Try
            Me.RefreshControls()
        End If
    End Sub

    Private Sub UltraTabControl2_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl2.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                BindGrid()
                Me.BtnDelete.Visible = True
            Else
                If IsEditMode = False Then Me.BtnDelete.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class