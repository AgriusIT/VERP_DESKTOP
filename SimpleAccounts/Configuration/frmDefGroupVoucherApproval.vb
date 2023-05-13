Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports SBDal
Imports SBUtility
Imports SBModel
Public Class frmDefGroupVoucherApproval
    Implements IGeneral

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.DataGridView1.RootTable.Columns
                If c.Index <> 2 Then
                    c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnsave.Enabled = True
                Me.btnDelete.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnsave.Enabled = False
                    Me.btnDelete.Enabled = False
                   Exit Sub
                End If
            Else
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefGroupVoucherApproval)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        End If
                        If Me.btnDelete.Text = "Delete" Or Me.btnDelete.Text = "&Delete" Then
                            Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString()
                        End If
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                 Else
                    Me.Visible = False
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    For Each RightsDt As GroupRights In Rights
                        If RightsDt.FormControlName = "View" Then
                            Me.Visible = True
                        ElseIf RightsDt.FormControlName = "Save" Then
                            If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                       ElseIf RightsDt.FormControlName = "Delete" Then
                            Me.btnDelete.Enabled = True
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Dim cmd As New OleDbCommand
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Try
            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300
            cmd.Transaction = trans

            cmd.CommandText = ""
            cmd.CommandText = "Delete From VoucherApprovalGroupSetting"
            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select GroupId, GroupName, IsNull(grp.Index_No,0) as Index_No from tblUserGroup " _
                              & " LEFT OUTER JOIN(SELECT [ID] ,[UserGroupId] ,[Index_No] FROM [VoucherApprovalGroupSetting]) grp on  grp.UserGroupId = tblUserGroup.GroupId")
            dt.AcceptChanges()
            Me.DataGridView1.DataSource = dt
            Me.DataGridView1.RetrieveStructure()
            Me.DataGridView1.RootTable.Columns("GroupId").Visible = False
            ApplyGridSettings()
            Me.DataGridView1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        If Me.DataGridView1.RowCount = 0 Then Return False
        Me.DataGridView1.UpdateData()
        Dim cmd As New OleDbCommand
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Try
            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300
            cmd.Transaction = trans

            cmd.CommandText = ""
            cmd.CommandText = "Delete From VoucherApprovalGroupSetting"
            cmd.ExecuteNonQuery()

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.DataGridView1.GetRows
                If Val(r.Cells("Index_No").Value.ToString) > 0 Then
                    cmd.CommandText = ""
                    cmd.CommandText = "INSERT INTO VoucherApprovalGroupSetting(UserGroupId,Index_No) VALUES(" & Val(r.Cells("GroupId").Value.ToString) & ", " & Val(r.Cells("Index_No").Value.ToString) & ")"
                    cmd.ExecuteNonQuery()
                End If
            Next
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

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmDefGroupVoucherApproval_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
End Class