'17-Aug-2015 Task#17082015 Ahmad Sharif: design and written all the logic

Imports System.Data.OleDb
Imports SBModel
Public Class FrmEmployeeSiteVisit

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.grdEmployee.RowCount < 0 Then Exit Sub

            If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                Save()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
                'If Val(Me.grdEmployee.GetRow(i).Cells("No Of Visits").Value.ToString) > 0 Then
                cmd.CommandText = String.Empty
                cmd.CommandText = "insert into tblEmployeeNoOfVisits(MY,EID,NoOfVisit,Total_Amount) values('" & Me.dtpMY.Value.ToString("dd/MMMM/yyyy") & "'," & Val(Me.grdEmployee.GetRow(i).Cells("EmpID").Value.ToString) & "," & Val(Me.grdEmployee.GetRow(i).Cells("No Of Visits").Value.ToString) & "," & Val(Me.grdEmployee.GetRow(i).Cells("Total").Value.ToString) & ")"
                cmd.ExecuteNonQuery()
                'End If
            Next


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
             CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                'Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmDefEmployee)
                    If Not dt Is Nothing Then
                        If Not dt.Rows.Count = 0 Then
                            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Else
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            End If
                           Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                Else
                    Me.Visible = False
                    Me.BtnSave.Enabled = False
                  CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    For i As Integer = 0 To Rights.Count - 1
                        If Rights.Item(i).FormControlName = "View" Then
                            Me.Visible = True
                        ElseIf Rights.Item(i).FormControlName = "Save" Then
                            If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Update" Then
                            If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                            CtrlGrdBar1.mGridPrint.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Export" Then
                            CtrlGrdBar1.mGridExport.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                            CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        End If
                    Next
                End If
            End If


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub Delete(ByVal trans As OleDbTransaction)
        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = trans.Connection
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "delete from tblEmployeeNoOfVisits where MY = Convert(DateTime,'" & Me.dtpMY.Value.ToString("MM/dd/yyyy") & "',102)"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetAllEmployees()
        Try
            Dim dt As New DataTable
            Dim strSQL As String = String.Empty
            strSQL = "SELECT Employee_ID as EmpID,Employee_Name as [Employee Name],Employee_Code as Code,IsNull(ABC.NoOfVisit,0) as [No Of Visits],IsNull([Visit_Amount],0) as [Visit Amount],IsNull(ABC.Total_Amount,0) as Total FROM tblDefEmployee inner join tblEmployeeVisitCharges  on tblDefEmployee.Employee_ID = tblEmployeeVisitCharges.EID  LEFT OUTER JOIN (SELECT EID,NOOFVISIT,Total_Amount FROM tblEmployeeNoOfVisits WHERE MY = '" & Me.dtpMY.Value.ToString("dd/MMMM/yyyy") & "') ABC ON tblDefEmployee.Employee_ID = ABC.EID"

            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            dt.Columns("Total").Expression = "[No Of Visits] * [Visit Amount]"

            Me.grdEmployee.DataSource = dt
            Me.grdEmployee.RootTable.Columns("EmpID").Visible = False
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAllEmployeeVisits()
        Try
            Dim dt As New DataTable
            Dim strSQL As String = String.Empty
            strSQL = "SELECT Employee_ID as EmpID,Employee_Name as [Employee Name],Employee_Code as Code,ABC.MY AS VisitDate,IsNull(ABC.NoOfVisit,0) as [No Of Visits],IsNull([Visit_Amount],0) as [Visit Amount],IsNull(ABC.Total_Amount,0) as Total FROM tblDefEmployee inner join tblEmployeeVisitCharges  on tblDefEmployee.Employee_ID = tblEmployeeVisitCharges.EID  LEFT OUTER JOIN (SELECT EID,NOOFVISIT,MY,Total_Amount FROM tblEmployeeNoOfVisits WHERE MY Between '" & Me.dtpFromDate.Value.ToString("dd/MMMM/yyyy") & "' AND '" & Me.dtpToDate.Value.ToString("dd/MMMM/yyyy") & "') ABC ON tblDefEmployee.Employee_ID = ABC.EID  WHERE ABC.NoOfVisit <> 0 ORDER BY ABC.MY ASC "
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "[No Of Visits] * [Visit Amount]"
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RootTable.Columns("EmpID").Visible = False
            Me.grdAllRecords.RootTable.Columns("VisitDate").FormatString = str_DisplayDateFormat
            Me.grdAllRecords.RootTable.Columns("No of Visits").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdAllRecords.RootTable.Columns("Visit Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAllRecords.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FrmEmployeeSiteVisit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub FrmEmployeeSiteVisit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdEmployee.RootTable.Columns
                If col.Index <> Me.grdEmployee.RootTable.Columns("No Of Visits").Index Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grdEmployee.RootTable.Columns("Employee Name").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdEmployee.RootTable.Columns("Code").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdAllRecords.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdAllRecords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdAllRecords.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Site Visits Detail" & vbCrLf & "Date From: " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            GetAllEmployees()
            ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            Me.ToolStrip1.Visible = True
            If e.Tab.Index = 1 Then
                ToolStrip1.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadHistory_Click(sender As Object, e As EventArgs) Handles btnLoadHistory.Click
        Try
            GetAllEmployeeVisits()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdEmployee.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee Visit"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class