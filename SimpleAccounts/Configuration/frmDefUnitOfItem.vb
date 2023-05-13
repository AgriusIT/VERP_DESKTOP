Imports SBDal
Imports SBModel
Public Class frmDefUnitOfItem
    Implements IGeneral

    Dim objModel As ArticleUnitDefTableBE
    Dim objDAL As ArticleUnitDefTableDAL
    Dim UnitId As Integer = 0
    Private Sub frmDefUnitOfItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnShowAll.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.Focus()
        GetAllRecords()

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

    Private Sub btnAdd_KeyDown(sender As Object, e As KeyEventArgs) Handles btnAdd.KeyDown

    End Sub

    Private Sub btnShowAll_MouseHover(sender As Object, e As EventArgs) Handles btnShowAll.MouseHover
        ContextMenuStrip2.Show(btnShowAll, 0, btnShowAll.Height)
    End Sub

    Private Sub frmDefUnitOfItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Insert Then
            btnAdd_Click(Nothing, Nothing)
        End If
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.GridEX1.RootTable.Columns("ArticleUnitId").Visible = False
            Me.GridEX1.RootTable.Columns("IsDate").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
      
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New ArticleUnitDefTableDAL
            Me.GridEX1.DataSource = objDAL.GetAll()
            Me.GridEX1.RetrieveStructure()
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
    ''Saba Shabbir: it will open a new form for entering new record of vehicle
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmAddDefUnitOfItem.blnEditMode = False
        frmAddDefUnitOfItem.ShowDialog()
        GetAllRecords()
    End Sub
    ''Saba Shabbir: grid row double click then update record and after update the records getAll updated records
    Private Sub GridEX1_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles GridEX1.RowDoubleClick

        Try
            If Me.GridEX1.RowCount > 0 AndAlso GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                frmAddDefUnitOfItem.IdOfUnitItem = Val(Me.GridEX1.CurrentRow.Cells("ArticleUnitId").Value.ToString)
                frmAddDefUnitOfItem.blnEditMode = True
                frmAddDefUnitOfItem.ShowDialog()
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Saba Shabbir: if delete key pressed , it will delete selected row
    Private Sub GridEX1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEX1.KeyDown
        Try
            objModel = New ArticleUnitDefTableBE
            objDAL = New ArticleUnitDefTableDAL
            UnitId = Val(Me.GridEX1.CurrentRow.Cells("ArticleUnitId").Value.ToString)
            objModel.ArticleUnitId = UnitId
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
End Class