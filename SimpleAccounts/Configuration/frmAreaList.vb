Imports SBDal
Imports SBModel
Public Class frmAreaList
    Implements IGeneral

    Dim objModel As AreaDefBE
    Dim objDAL As AreaDefDAL
    Dim TerritoryId As Integer = 0
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            'Dim frmArea As New frmAreaDef(0, False)
            'frmAreaDef.TerritoryId = Val(Me.DataGridView1.CurrentRow.Cells("TerritoryId").Value.ToString)
            frmAreaDef.blnEditMode = False
            frmAreaDef.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAreaList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Control AndAlso Keys.P Then
            'Print Function
        End If
        If e.KeyCode = Keys.Insert Then
            frmAreaDef.blnEditMode = False
            frmAreaDef.ShowDialog()
        End If
    End Sub

    Private Sub btnShowAll_MouseHover(sender As Object, e As EventArgs) Handles btnExport.MouseHover
        ContextMenuStrip1.Show(btnExport, 0, btnExport.Height)
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

        ''Columns name:TerritoryId, TerritoryName, CityId, SortOrder, Comments, Active, IsDate

        Me.DataGridView1.RootTable.Columns("TerritoryId").Visible = False
        Me.DataGridView1.RootTable.Columns("IsDate").Visible = False

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        objDAL = New AreaDefDAL
        If IsValidate() = True Then
            FillModel()
            Me.DataGridView1.DataSource = objDAL.Delete(objModel)
            GetAllRecords()
        Else
            Return False
        End If
        
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New AreaDefBE
            objDAL = New AreaDefDAL

            If Me.DataGridView1.RowCount > 0 AndAlso DataGridView1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                TerritoryId = Val(Me.DataGridView1.CurrentRow.Cells("TerritoryId").Value.ToString)
                Dim dt As DataTable = objDAL.GetById(TerritoryId)
                If dt.Rows.Count > 0 Then
                    objModel.TerritoryId = TerritoryId 'dt.Rows("").Item
                    objModel.CityId = TerritoryId
                    objModel.TerritoryName = dt.Rows(0).Item("TerritoryName").ToString
                    objModel.Comments = dt.Rows(0).Item("Comments").ToString
                    If IsDBNull(dt.Rows(0).Item("IsDate")) Then
                        objModel.IsDate = Date.Now
                    Else
                        objModel.IsDate = dt.Rows(0).Item("IsDate")
                    End If
                    objModel.SortOrder = dt.Rows(0).Item("SortOrder").ToString

                    objModel.ActivityLog = New ActivityLog
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New AreaDefDAL
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

    Private Sub frmAreaList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnExport.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderSize = 0
        GetAllRecords()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Delete()
            msg_Information(str_informDelete)
        End If
    End Sub

    Private Sub DataGridView1_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles DataGridView1.RowDoubleClick
       Try
            If Me.DataGridView1.RowCount > 0 AndAlso DataGridView1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                frmAreaDef.TerritoryId = Val(Me.DataGridView1.CurrentRow.Cells("TerritoryId").Value.ToString)
                frmAreaDef.blnEditMode = True
                frmAreaDef.ShowDialog()
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class