Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmTermsandConditions
    Implements IGeneral
    Dim Terms As TermsAndConditions
    Dim TermId As Integer = 0I
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns("ID").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Sub ApplySecurityRights()
        Try
            'TFS# 934: Apply Security for Standard User by Ali Faisal on 15-June-2017
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.BtnNew.Enabled = True
                Me.BtnEdit.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.BtnSave.Enabled = False
            Me.BtnDelete.Enabled = False
            Me.BtnPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Dim str As String = ""
            Dim conn As New SqlConnection(SQLHelper.CON_STR)
            Dim Trans As SqlTransaction
            Try
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Trans = conn.BeginTransaction
                'TFS# 934: Delete the Detail and Master both records by Ali Faisal on 15-June-2017
                str = "Delete from tblTermsAndConditionDetail Where TermId= " & Me.grdSaved.GetRow.Cells("ID").Value.ToString & ""
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
                str = "Delete from tblTermsAndConditionType Where ID= " & Me.grdSaved.GetRow.Cells("ID").Value.ToString & ""
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
                'Insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtTermsName.Text, True)
                Trans.Commit()
                Return True
            Catch ex As Exception
                Trans.Rollback()
                Throw ex
            Finally
                conn.Close()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Terms = New TermsAndConditions
            Terms.TermName = Me.txtTermsName.Text
            Terms.Description = Me.txtDescription.Text
            Terms.SortOrder = Me.txtSortOrder.Text
            Terms.Active = IIf(Me.chkActive.Checked = True, 1, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'TFS# 934: Get History records sorted by Sort Order by Ali Faisal on 15-June-2017
            Dim str As String = String.Empty
            Dim dt As DataTable
            str = "SELECT ID, Term_Title, Term_Condition, IsNull(SortOrder,0) As SortOrder, IsNull(Active,0) As Active FROM tblTermsAndConditionType ORDER BY SortOrder"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub DisplayDetails(ByVal Id As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select * from tblTermsAndConditionDetail Where TermId=" & Id & ""
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyDetailGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyDetailGridSettings()
        Try
            Me.grd.RootTable.Columns("Id").Visible = False
            Me.grd.RootTable.Columns("TermId").Visible = False
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub AddtoGrid()
        Try
            'TFS# 934: Check if there is no text in Heading and Details box then Exit Sub by Ali Faisal on 15-June-2017
            If Me.txtHeading.Text = "" AndAlso Me.txtDetail.Text = "" Then Exit Sub
            Dim dt As DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("Id") = 0
            dr("TermId") = 0
            dr("Heading") = Me.txtHeading.Text
            dr("Details") = Me.txtDetail.Text
            dt.Rows.Add(dr)
            ReSetDetails()
            ApplyDetailGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecords()
        Try
            'TFS# 934: Setting the values in text boxes and other controls from History Grid by Ali Faisal on 15-June-2017
            TermId = Me.grdSaved.GetRow.Cells("ID").Value.ToString
            Me.txtTermsName.Text = Me.grdSaved.GetRow.Cells("Term_Title").Value.ToString
            Me.txtDescription.Text = Me.grdSaved.GetRow.Cells("Term_Condition").Value.ToString
            Me.txtSortOrder.Text = Me.grdSaved.GetRow.Cells("SortOrder").Value.ToString
            Me.chkActive.Checked = Me.grdSaved.GetRow.Cells("Active").Value.ToString
            Me.BtnSave.Text = "&Update"
            'TFS# 934: Call the display details for specific term id from History Grid by Ali Faisal on 15-June-2017
            DisplayDetails(TermId)
            UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            'TFS# 934: Validating that Terms can not be saved without their name by Ali Faisal on 15-June-2017
            If Me.txtTermsName.Text = "" Then
                msg_Error("Please enter the Terms & Conditions Name.")
                Me.txtTermsName.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'TFS# 934: Reset master controls like text boxes and checks by Ali Faisal on 15-June-2017
            Me.txtTermsName.Text = ""
            Me.txtDescription.Text = ""
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            Me.BtnSave.Text = "&Save"
            ReSetDetails()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ReSetDetails()
        Try
            'TFS# 934: Reset detail controls like text boxes by Ali Faisal on 15-June-2017
            Me.txtHeading.Text = ""
            Me.txtDetail.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim str As String = ""
            Dim conn As New SqlConnection(SQLHelper.CON_STR)
            Dim Trans As SqlTransaction
            Try
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Trans = conn.BeginTransaction
                'TFS# 934: Insert master records by Ali Faisal on 15-June-2017
                str = "Insert into tblTermsAndConditionType (Term_Title,Term_Condition,SortOrder,Active) Values ('" & Me.txtTermsName.Text & "','" & Me.txtDescription.Text & "'," & Me.txtSortOrder.Text & "," & IIf(Me.chkActive.Checked = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
                'TFS# 934: Check if Detail grid is empty then bipass this condition by Ali Faisal on 15-June-2017
                If Me.grd.RowCount > 0 Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                        str = "Insert into tblTermsAndConditionDetail (TermId,Heading,Details) Values (Ident_Current('tblTermsAndConditionType'),N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                        SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
                    Next
                End If
                'TFS# 934: Insert Activity Log by Ali Faisal on 15-June-2017
                SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtTermsName.Text, True)
                Trans.Commit()
                Return True
            Catch ex As Exception
                Trans.Rollback()
                Throw ex
            Finally
                conn.Close()
            End Try
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
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim Trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Trans = conn.BeginTransaction
            'TFS# 934: Update master record by Ali Faisal on 15-June-2017
            str = "Update tblTermsAndConditionType Set Term_Title=N'" & Me.txtTermsName.Text & "',Term_Condition=N'" & Me.txtDescription.Text & "',SortOrder=" & Me.txtSortOrder.Text & ",Active=" & IIf(Me.chkActive.Checked = True, 1, 0) & " Where ID=" & TermId & " "
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
            'TFS# 934: Check if Detail grid is empty then bipass this condition by Ali Faisal on 15-June-2017
            'TFS# 934: First delete the detail record then save them by Ali Faisal on 15-June-2017
            str = "Delete tblTermsAndConditionDetail Where TermId= " & TermId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
            If Me.grd.RowCount > 0 Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                    str = "Insert into tblTermsAndConditionDetail (TermId,Heading,Details) Values (" & TermId & ",N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str)
                Next
            End If
            'TFS# 934: Insert Activity Log by Ali Faisal on 15-June-2017
            SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtTermsName.Text, True)
            Trans.Commit()
            Return True
            Me.BtnSave.Text = "&Save"
        Catch ex As Exception
            Trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            AddtoGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
            TermId = 0I
            DisplayDetails(-1)
            GetAllRecords()
            Me.Cursor = Cursors.Default
            UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.BtnSave.Text = "&Save" Then
                If IsValidate() = True Then
                    If Save() Then
                        ReSetControls()
                        GetAllRecords()
                        DisplayDetails(-1)
                    End If
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() Then
                    ReSetControls()
                    GetAllRecords()
                    DisplayDetails(-1)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            If Delete() Then
                ReSetControls()
                GetAllRecords()
                DisplayDetails(-1)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmTermsandConditions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                BtnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmTermsandConditions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            ReSetControls()
            GetAllRecords()
            DisplayDetails(-1)
            Me.txtTermsName.Focus()
            ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Terms and Conditions"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class