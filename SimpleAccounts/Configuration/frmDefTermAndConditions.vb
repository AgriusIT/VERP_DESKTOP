Imports System.Data
Imports System
Imports System.Data.OleDb

Public Class frmDefTermAndConditions
    Implements IGeneral
    Dim intID As Integer = 0I

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCity)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else

                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objtrans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand
        Try


            cmd.Connection = objCon
            cmd.Transaction = objtrans
            cmd.CommandTimeout = 300
            cmd.CommandType = CommandType.Text

            If Me.grd.RowCount = 0 Then Return False

            cmd.CommandText = "Delete From tblTermsAndConditionType WHERE ID=" & Val(Me.grd.GetRow.Cells("ID").Value.ToString) & ""

            cmd.ExecuteNonQuery()
            objtrans.Commit()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select ID, Term_Title, Term_Condition From tblTermsAndConditionType Order By ID  DESC")
            dt.AcceptChanges()

            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("Term_Title").Width = 120
            Me.grd.RootTable.Columns("Term_Condition").Width = 300
            Me.grd.RootTable.Columns("Term_Condition").WordWrap = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try


            If Me.txtTermTitle.Text = String.Empty Then
                ShowErrorMessage("Please Enter Term Title.")
                Me.txtTermTitle.Focus()
                Return False
            End If
            If Me.txtTermCondition.Text = String.Empty Then
                ShowErrorMessage("Please Enter Term Condition.")
                Me.txtTermCondition.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try

            intID = 0
            Me.btnSave.Text = "&Save"
            Me.txtTermTitle.Text = String.Empty
            Me.txtTermCondition.Text = String.Empty
            GetAllRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            Me.txtTermTitle.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

        If IsValidate() = False Then Return False

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objtrans As OleDbTransaction = objCon.BeginTransaction

        Dim cmd As New OleDbCommand

        Try

            cmd.Connection = objCon
            cmd.Transaction = objtrans
            cmd.CommandTimeout = 300
            cmd.CommandType = CommandType.Text
            cmd.CommandText = ""

            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO tblTermsAndConditionType(Term_Title,Term_Condition) Values(N'" & Me.txtTermTitle.Text.Replace("'", "''") & "',N'" & Me.txtTermCondition.Text.Replace("'", "''") & "')"
                cmd.ExecuteNonQuery()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Return False
                cmd.CommandText = ""
                cmd.CommandText = "Update tblTermsAndConditionType SET Term_Title=N'" & Me.txtTermTitle.Text.Replace("'", "''") & "',Term_Condition=N'" & Me.txtTermCondition.Text.Replace("'", "''") & "' WHERE ID=" & Val(Me.grd.GetRow.Cells("ID").Value.ToString) & ""
                cmd.ExecuteNonQuery()
            End If

            objtrans.Commit()
            ReSetControls()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Save()
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
    Private Sub frmDefTermAndConditions_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If grd.RowCount = 0 Then Exit Sub
            Me.txtTermTitle.Text = Me.grd.GetRow.Cells("Term_Title").Value.ToString
            Me.txtTermCondition.Text = Me.grd.GetRow.Cells("Term_Condition").Value.ToString
            Me.btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class