''Saba Shabbir : added form for vehicle log 4 april 2018

Imports SBDal
Imports SBModel
Public Class frmItemType
    Implements IGeneral

    Dim objModel As ArticleTypeDefTableBE
    Dim objDAL As ArticleTypeDefTableDAL
    Dim ItemId As Integer = 0
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False

    Private Sub btnAddDock_MouseHover(sender As Object, e As EventArgs)
        'ContextMenuStrip1.Show(btnAdd, 0, btnAdd.Height)
    End Sub

    Private Sub frmUnitOfItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'btnCollaspeAll.FlatAppearance.BorderSize = 0
        btnShowAll.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.Focus()
        GetAllRecords()
        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AddMainAccountToolStripMenuItem_Click(sender As Object, e As EventArgs)
        DefMainAcc.ShowDialog()
    End Sub

    Private Sub AddSubAccountToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frmSubAccount.ShowDialog()
    End Sub

    Private Sub AddSubSubAccountToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frmSubSubAccount.ShowDialog()
    End Sub

    Private Sub DetailAccountToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frmDetailAccount.ShowDialog()
    End Sub

    Private Sub ChangeDetailAccountToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frmChangeDetailAccount.ShowDialog()
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub
    ''Saba Shabbir: it will open a new form for entering new record of vehicle
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmAddItemType.blnEditMode = False
        frmAddItemType.DoHaveSaveRights = DoHaveSaveRights
        frmAddItemType.ShowDialog()
        GetAllRecords()
    End Sub
    ''Saba Shabbir: grid row double click then update record and after update the records getAll updated records
    Private Sub DataGridView1_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles DataGridView1.RowDoubleClick

        Try
            If Me.DataGridView1.RowCount > 0 AndAlso DataGridView1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                frmAddItemType.IdOfItem = Val(Me.DataGridView1.CurrentRow.Cells("ArticleTypeId").Value.ToString)
                frmAddItemType.blnEditMode = True
                frmAddItemType.DoHaveUpdateRights = DoHaveUpdateRights
                frmAddItemType.ShowDialog()
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            ''Colum names:ArticleTypeId, ArticleTypeName, Comments, Active, SortOrder, TypeCode, IsDate
            '' Setting columns visibility false
            Me.DataGridView1.RootTable.Columns("ArticleTypeId").Visible = False
            Me.DataGridView1.RootTable.Columns("IsDate").Visible = False
            Me.DataGridView1.RootTable.Columns("SortOrder").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.DataGridView1.RootTable.Columns("TypeCode").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Me.btnShowAll.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.btnShowAll.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.btnShowAll.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        'Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        'Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
       
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New ArticleTypeDefTableDAL
            Me.DataGridView1.DataSource = objDAL.GetAll()
            Me.DataGridView1.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Return True
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    ''Saba Shabbir: if delete key pressed , it will delete selected row
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Try
            objModel = New ArticleTypeDefTableBE
            objDAL = New ArticleTypeDefTableDAL
            ItemId = Val(Me.DataGridView1.CurrentRow.Cells("ArticleTypeId").Value.ToString)
            objModel.ArticleTypeId = ItemId
            objModel.ActivityLog = New ActivityLog
                If e.KeyCode = Keys.Delete Then
                    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(objModel)
                GetAllRecords()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmUnitOfItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Insert Then
            btnAdd_Click(Nothing, Nothing)
        End If
        
    End Sub

    Private Sub btnShowAll_MouseHover(sender As Object, e As EventArgs) Handles btnShowAll.MouseHover
        ContextMenuStrip2.Show(btnShowAll, 0, btnShowAll.Height)
    End Sub
End Class