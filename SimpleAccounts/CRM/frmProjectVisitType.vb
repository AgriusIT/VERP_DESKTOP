Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class frmProjectVisitType
    Implements IGeneral
    Dim ProjTypeId As Integer = 0

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                '    Exit Sub
                'End If
            Else
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
            End If
            If (Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save") AndAlso Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
                Me.BtnSave.Visible = True
            Else
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.BtnSave.Visible = False
                Else
                    Me.BtnSave.Visible = True
                End If
            End If
        Catch ex As Exception
            'msg_Error(ex.Message)
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand

        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblProjectVisitType WHERE ProjectVisitTypeId=" & Val(Me.GrdTypes.GetRow.Cells("ProjectVisitTypeId").Value.ToString) & ""
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL

            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select * From tblProjectVisitType ORDER BY ProjectVisitTypeId DESC")
            dt.AcceptChanges()

            Me.GrdTypes.DataSource = dt
            Me.GrdTypes.RetrieveStructure()
            Me.GrdTypes.RootTable.Columns("ProjectVisitTypeId").Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.TxtName.Text = String.Empty Then
                ShowErrorMessage("Please enter visit type")
                Me.TxtName.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.BtnSave.Text = "&Save"
            Me.ProjTypeId = 0I
            Me.TxtId.Text = 0
            Me.TxtName.Text = String.Empty
            Me.TxtRemarks.Text = String.Empty
            Me.TxtSortOrder.Text = 1
            Me.UichkActive.Checked = True
            GetAllRecords()
            Me.TxtName.Focus()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand

        Try

            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblProjectVisitType(ProjectVisitType,Remarks,Active,SortOrder) VALUES(N'" & Me.TxtName.Text.Replace("'", "''") & "',N'" & Me.TxtRemarks.Text.Replace("'", "''") & "'," & IIf(Me.UichkActive.Checked = True, 1, 0) & "," & Val(Me.TxtSortOrder.Text) & ") Select @@Identity"
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL

            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand

        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update tblProjectVisitType SET ProjectVisitType=N'" & Me.TxtName.Text.Replace("'", "''") & "',Remarks=N'" & Me.TxtRemarks.Text.Replace("'", "''") & "',Active=" & IIf(Me.UichkActive.Checked = True, 1, 0) & ",SortOrder=" & Val(Me.TxtSortOrder.Text) & " WHERE ProjectVisitTypeId=" & Val(Me.TxtId.Text) & ""
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL

            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Private Sub GrdTypes_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdTypes.DoubleClick
        Try
            If Me.GrdTypes.RowCount = 0 Then Exit Sub
            Me.BtnSave.Text = "&Update"
            Me.TxtId.Text = Val(Me.GrdTypes.GetRow.Cells("ProjectVisitTypeId").Value.ToString)
            Me.TxtName.Text = Me.GrdTypes.GetRow.Cells("ProjectVisitType").Value.ToString
            Me.TxtRemarks.Text = Me.GrdTypes.GetRow.Cells("Remarks").Value.ToString
            Me.TxtSortOrder.Text = Val(Me.GrdTypes.GetRow.Cells("SortOrder").Value.ToString)
            If IsDBNull(Me.GrdTypes.GetRow.Cells("SortOrder").Value) Then
                Me.UichkActive.Checked = True
            Else
                Me.UichkActive.Checked = Me.GrdTypes.GetRow.Cells("SortOrder").Value
            End If
            Me.TxtName.Focus()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            GrdTypes_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = False Then Exit Sub

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then

                If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If Me.GrdTypes.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProjectVisitType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                BtnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                'btnPrint_Click(Nothing, Nothing)
                'Exit Sub
            End If
            If e.KeyCode = Keys.F2 Then
                Me.BtnEdit_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.Delete Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    If Me.GrdTypes.RowCount > 0 Then
                        Me.BtnDelete_Click(Nothing, Nothing)
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProjectVisitType_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.KeyPreview = True
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            ApplySecurity(SBUtility.Utility.EnumDataMode.ReadOnly)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class