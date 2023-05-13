'17-Aug-2015 Task#17082015 Ahmad Sharif design whole screen and written logic

Imports System.Data.OleDb
Imports SBModel
Public Class FrmEmployeeSiteVisitCharges

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.grdEmployee.RowCount = 0 Then Exit Sub

            If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                Save()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            Me.btnSave.Enabled = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Save" Then
                    If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                ElseIf RightsDt.FormControlName = "Grid Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub Save()
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Dim trans As OleDbTransaction

        If objCon.State = ConnectionState.Closed Then objCon.Open()
        trans = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim i As Integer = 0I

            Delete(trans)       'firstly delete all records from tblEmployeeVisitCharges and then save 

            For i = 0 To Me.grdEmployee.RowCount - 1
                If Val(Me.grdEmployee.GetRow(i).Cells("Visit Charges").Value.ToString) > 0 Then
                    cmd.CommandText = String.Empty
                    cmd.CommandText = "insert into tblEmployeeVisitCharges(EID,Visit_Amount) values(" & Val(Me.grdEmployee.GetRow(i).Cells("EmpID").Value.ToString) & "," & Val(Me.grdEmployee.GetRow(i).Cells("Visit Charges").Value.ToString) & ")"
                    cmd.ExecuteNonQuery()
                End If
            Next


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Sub

    Private Sub Delete(ByVal trans As OleDbTransaction)
        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = trans.Connection
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "delete from tblEmployeeVisitCharges"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            GetAllEmployees()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FrmEmployeeSiteVisitCharges_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If

            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FrmEmployeeSiteVisitCharges_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            GetSecurityRights()
            GetAllEmployees()
            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub GetAllEmployees()
        Try
            Dim dt As New DataTable
            Dim strSQL As String = String.Empty
            strSQL = "select Employee_ID as EmpID,Employee_Name as [Employee Name],Employee_Code as Code,IsNull([Visit_Amount],0) as [Visit Charges] from tblDefEmployee left outer join tblEmployeeVisitCharges on tblDefEmployee.Employee_ID = tblEmployeeVisitCharges.EID  where active = 1"

            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grdEmployee.DataSource = dt


            Me.grdEmployee.RootTable.Columns("EmpID").Visible = False
            Me.grdEmployee.RootTable.Columns("Visit Charges").EditType = Janus.Windows.GridEX.EditType.TextBox

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ApplyGridSettings()
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdEmployee.RootTable.Columns
                If col.Index <> Me.grdEmployee.RootTable.Columns("Visit Charges").Index Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grdEmployee.RootTable.Columns("Employee Name").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdEmployee.RootTable.Columns("Employee Name").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdEmployee.RootTable.Columns("Code").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdEmployee.RootTable.Columns("Code").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'delete employee from grid 
    Private Sub grdEmployee_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdEmployee.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grdEmployee.GetRow.Delete()
                Me.grdEmployee.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdEmployee.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class