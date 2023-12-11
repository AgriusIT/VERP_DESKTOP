'15-Jul-2015 Task@#15072015 Ahmad Sharif Design and Develop whole screen

'Task@#15072015
Imports System.Data.OleDb
Imports SBModel

Public Class frmDefDocumentPrefix

    Private Sub ResetControls()
        Try

            Me.cmbModule.SelectedIndex = 0
            Me.dtpStartDate.Value = Date.Now
            Me.dtpEndDate.Value = Date.Now
            Me.txtPrefix.Text = String.Empty

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            GetAllRecords()
            GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If IsValidate() = True Then
                If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                    SaveRecord()
                    ResetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    UpdateRecord()
                    ResetControls()
                    Me.btnSave.Text = "&Save"
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            GrdStatus_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            DeleteRecord()
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetAllRecords()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT tblprefix.PID as ID, tblprefix.Prefix, tblprefix.Start_Date, tblprefix.End_Date, tblprefix.Module from tblprefix"

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.GrdStatus.DataSource = dt
            Me.GrdStatus.RetrieveStructure()


            Me.GrdStatus.RootTable.Columns("ID").Visible = False
            Me.GrdStatus.RootTable.Columns("Start_Date").FormatString = "dd/MMMM/yyyy"
            Me.GrdStatus.RootTable.Columns("End_Date").FormatString = "dd/MMMM/yyyy"

            Me.GrdStatus.RootTable.Columns("Start_Date").Caption = "Start Date"
            Me.GrdStatus.RootTable.Columns("End_Date").Caption = "End Date"

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.GrdStatus.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsValidate() As Boolean
        Try
            If Me.cmbModule.Text = String.Empty Then
                ShowErrorMessage("Please Enter of Select the Module")
                Me.cmbModule.Focus()
                Return False
            End If
            
            If Me.dtpEndDate.Value < Me.dtpStartDate.Value Then
                ShowErrorMessage("End Date should be greater than Start Date")
                Me.dtpEndDate.Focus()
                Return False
            End If

            If Me.txtPrefix.Text = String.Empty Then
                ShowErrorMessage("Please Enter the Prefix")
                Me.txtPrefix.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub SaveRecord()
        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        trans = Con.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "Insert into tblPrefix(Prefix,Start_Date,End_Date,Module) Values(" _
            & "'" & Me.txtPrefix.Text.Replace("'", "''").ToString & "'," _
            & "'" & Me.dtpStartDate.Value & "'," _
            & "'" & Me.dtpEndDate.Value & "'," _
            & "'" & Me.cmbModule.Text.Replace("'", "''").ToString & "')"

            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub

    Private Sub UpdateRecord()
        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        trans = Con.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty

            strSQL = "Update tblPrefix set Prefix='" & Me.txtPrefix.Text.Replace("'", "''").ToString & "'," _
            & " Start_Date='" & Me.dtpStartDate.Value & "'," _
            & " End_Date='" & Me.dtpEndDate.Value & "'," _
            & " Module='" & Me.txtPrefix.Text.Replace("'", "''").ToString & "' Where PID=" & Val(Me.GrdStatus.CurrentRow.Cells("ID").Value.ToString)

            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub


    Private Sub DeleteRecord()
        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        trans = Con.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "Delete from tblPrefix where PID=" & Val(Me.GrdStatus.CurrentRow.Cells("ID").Value.ToString)
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub

    Private Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Module" Then
                Dim strSQL As String = String.Empty
                strSQL = "SELECT distinct Module,Module  FROM tblPrefix where Module <> ''"
                FillDropDown(Me.cmbModule, strSQL, False)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmDefDocumentPrefix_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'FillCombo("Module")
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub EditRecord()
        Try
            If Me.GrdStatus.RowCount = 0 Then Exit Sub

            Me.cmbModule.Text = Me.GrdStatus.CurrentRow.Cells("Module").Value.ToString
            Me.dtpStartDate.Value = Me.GrdStatus.CurrentRow.Cells("Start_Date").Value.ToString
            Me.dtpEndDate.Value = Me.GrdStatus.CurrentRow.Cells("End_Date").Value.ToString
            Me.txtPrefix.Text = Me.GrdStatus.CurrentRow.Cells("Prefix").Value.ToString

            Me.btnSave.Text = "&Update"
            GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GrdStatus_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.GrdStatus.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False ' 'Task:2406 Added Field Chooser Rights
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesReturn)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If

            Else
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False

                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True

                    ElseIf Rightsdt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GrdStatus.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
'End Task@#15072015