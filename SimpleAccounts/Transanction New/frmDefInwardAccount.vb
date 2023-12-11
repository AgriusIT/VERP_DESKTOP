Imports System.Data
Imports System.Data.OleDb


Public Class frmDefInwardAccount

    Implements IGeneral
    Dim _ID As Integer = 0I

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        Try

            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text


            cmd.CommandText = ""
            cmd.CommandText = "DELETE From tblDefInwardAccounts WHERE ID=" & Me.grdSaved.GetRow.Cells("ID").Value.ToString & ""
            cmd.ExecuteNonQuery()


            trans.Commit()

            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            FillUltraDropDown(Me.cmbAccounts, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Ac/Code], sub_sub_title as [Account Head],Account_Type as [Ac Type] From vwCOADetail WHERE Detail_Title <> ''")
            Me.cmbAccounts.Rows(0).Activate()
            Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select ID,InwardAccountId, Detail_Title as [Account Title], Detail_Code as [A/c Code], sub_sub_title as [Account Head], Account_Type as [Type] From tblDefInwardAccounts LEFT OUTER JOIN vwCOADetail on vwCOADetail.coa_detail_id = tblDefInwardAccounts.InwardAccountId ORDER BY ID DESC")

            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()

            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns(1).Visible = False
            Me.grdSaved.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbAccounts.IsItemInList = False Then Return False
            If Me.cmbAccounts.ActiveRow Is Nothing Then Return False

            If Me.cmbAccounts.Value <= 0 Then
                ShowErrorMessage("Please select Account Title.")
                Return False
                Me.cmbAccounts.Focus()
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            _ID = 0
            Me.btnSave.Text = "&Save"
            Me.cmbAccounts.Rows(0).Activate()
            Me.chkActive.Checked = True
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        Try

            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text




            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO tblDefInwardAccounts(InwardAccountId,Active) VALUES(" & Me.cmbAccounts.Value & "," & IIf(Me.chkActive.Checked = True, 1, 0) & ")"
            cmd.ExecuteNonQuery()


            trans.Commit()

            Return True



        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        Try

            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text




            cmd.CommandText = ""
            cmd.CommandText = "UPDATE tblDefInwardAccounts SET InwardAccountId=" & Me.cmbAccounts.Value & ",Active=" & IIf(Me.chkActive.Checked = True, 1, 0) & " WHERE ID=" & _ID & ""
            cmd.ExecuteNonQuery()


            trans.Commit()

            Return True



        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Private Sub frmDefInwardAccount_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords()
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            _ID = Val(Me.grdSaved.GetRow.Cells("ID").Value.ToString)
            Me.btnSave.Text = "&Update"
            Me.cmbAccounts.Value = Val(Me.grdSaved.GetRow.Cells("InwardAccountId").Value.ToString)
            Me.chkActive.Checked = Me.grdSaved.GetRow.Cells("Active").Value
            Me.cmbAccounts.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class