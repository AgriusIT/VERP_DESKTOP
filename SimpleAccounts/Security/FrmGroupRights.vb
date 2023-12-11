
Imports sbDAL
Imports sbModel
Imports System.Collections.Specialized
Imports System.Data
Imports sbUtility.Utility
Public Class FrmGroupRights
    Implements IGeneral



#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As List(Of SecurityGroupRights)
    Private intPkId As Integer
    Private int1stLvl As Integer = 0
    Private int2ndLvl As Integer = 0

#End Region

#Region "Enumerations"
    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGrid
        ControlID = 0
        FormCategory = 1
        FormLabel = 2
        ControlCaption = 3
        IsSelected = 4
    End Enum
#End Region

#Region "Interface Methods"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try
            ''Columns with In-visible setting
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlID).FormatString = ""
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlID).Visible = False

            ' ''Set columns widths for visible columns
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlCaption).Width = 350
            Me.grdAllRecords.RootTable.Columns(EnumGrid.IsSelected).Width = 75


            Me.grdAllRecords.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor
            Me.grdAllRecords.GroupByBoxVisible = False
            Me.grdAllRecords.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True

            ''define groups
            Dim grpFormCategory As New Janus.Windows.GridEX.GridEXGroup(Me.grdAllRecords.RootTable.Columns(EnumGrid.FormCategory))
            grpFormCategory.GroupPrefix = String.Empty
            Me.grdAllRecords.RootTable.Groups.Add(grpFormCategory)

            Dim grpFormLabel As New Janus.Windows.GridEX.GridEXGroup(Me.grdAllRecords.RootTable.Columns(EnumGrid.FormLabel))
            grpFormLabel.GroupPrefix = String.Empty
            Me.grdAllRecords.RootTable.Groups.Add(grpFormLabel)

            grpFormCategory.Collapse()
            grpFormLabel.Collapse()


            Me.grdAllRecords.GroupIndent = 50

            Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grdAllRecords.EmptyRows = False

            Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdAllRecords.RootTable.Columns(EnumGrid.FormLabel).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGrid.FormCategory).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlCaption).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGrid.ControlID).EditType = Janus.Windows.GridEX.EditType.NoEdit

            If Me.grdAllRecords.RowCount > 0 Then Me.grdAllRecords.Row = 0



            ''Getting Language based captions for the selected Grid, after applying filter criteria on Language Based Controls List
            'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & EnumProjectForms.frmFormManagement.ToString & "' AND [Control Name] like 'grdAllRecords%'")

            'If Not dv Is Nothing Then
            '    If Not dv.Count = 0 Then
            '        ''for each Caption Returned by Filter criteria
            '        For rowIndex As Integer = 0 To dv.Count - 1
            '            ''For each Column of the grid, against the selected caption
            '            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdAllRecords.RootTable.Columns
            '                ''As the coulumn's Name is concatinated with Grid name, therefor, firs we will split it and then will pick column name as caption from the second index of the split
            '                Dim strGridColumName() As String = dv.Item(rowIndex).Item(dv.Table.Columns("Control Name").ColumnName).Split(".")
            '                ''IF filtered column name is matched with the grid column Name then change the caption of the column as per language setting.
            '                If col.Caption = strGridColumName(1) Then
            '                    col.Caption = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
            '                    col.HeaderStyle.Font = gobjDefaultFontSettingForLables
            '                    Exit For
            '                End If
            '            Next

            '        Next
            '        grdAllRecords.Font = gobjDefaultFontSettingForInput
            '    End If
            'End If
            'ApplyGridCaptions(Me.Name, "grdAllRecords", grdAllRecords)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            ''filling Group  combo
            Me.cboFormGroups.DisplayMember = "Group Name"
            Me.cboFormGroups.ValueMember = "Group ID"
            Dim dt As DataTable = New GroupDAL().GetAll()
            If dt.Rows(0).Item("Group Name").ToString = gstrComboZeroIndexString Then dt.Rows.RemoveAt(0)
            Me.cboFormGroups.DataSource = dt

            ''filling Categories

            Me.lstFormCategories.ListItem.DisplayMember = "FORM_CATEGORY"
            Me.lstFormCategories.ListItem.ValueMember = "FORM_CATEGORY"
            Me.lstFormCategories.ListItem.DataSource = New GroupRightsDAL().GetAllMenuCategories()
            Me.lstFormCategories.ListItem.SelectedIndex = -1

            AddHandler lstFormCategories.SelectedIndexChaned, AddressOf lstFormCategories_SelectedIndexChaned

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ''Create Model object
            mobjModel = New List(Of SecurityGroupRights)

            Dim dt As DataTable = CType(Me.grdAllRecords.DataSource, DataView).Table
            If dt Is Nothing Then Exit Sub
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Dim gr As SecurityGroupRights
                For Each r As DataRow In dt.Rows
                    gr = New SecurityGroupRights
                    gr.GroupInfo = New SecurityGroup
                    gr.GroupInfo.GroupID = Me.cboFormGroups.SelectedValue
                    gr.IsSelected = r.Item(EnumGrid.IsSelected)
                    gr.ControlID = r.Item(EnumGrid.ControlID)
                    mobjModel.Add(gr)
                Next

                'filing activity log

                mobjModel(0).ActivityLog = New ActivityLog()
                'mobjModel(0).ActivityLog.ShopID = 0
                'mobjModel(0).ActivityLog.ScreenTitle = Me.TabPgGroupRights.Text
                'mobjModel(0).ActivityLog.LogGroup = "Security"
                'mobjModel(0).ActivityLog.UserID = gObjUserInfo.UserID
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.grdAllRecords.DataSource = Nothing

            If Me.cboFormGroups.SelectedIndex < 0 Then Exit Sub
            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New GroupRightsDAL().GetAll(Convert.ToInt32(cboFormGroups.SelectedValue))

            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = IIf(Me.lstFormCategories.ListItem.SelectedItems.Count > 0, String.Format("[Form Category] in ({0})", Me.lstFormCategories.SelectedItems), "")

            Me.grdAllRecords.DataSource = dv
            Me.grdAllRecords.RetrieveStructure()

            ''Applying Grid Formatting Setting


            ' If Me.grdAllRecords.RecordCount = 0 Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            Me.intPkId = 0
            Me.int1stLvl = 0
            Me.int2ndLvl = 0
            'Me.chkUpdateAll.Checked = False
            'Me.chkSaveAll.Checked = False
            'Me.chkDeleteAll.Checked = False
            'Me.chkViewAll.Checked = False
            'Me.chkPrintAll.Checked = False
            'Me.chkExportAll.Checked = False
            Me.cboFormGroups.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage("Do you want to save", MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New GroupRightsDAL().Update(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()
                        'AddUserFormListToHashTable()
                        Me.ApplyGridSettings()

                        ''Reset controls and set New Mode
                        Me.ReSetControls()

                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

   

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

#End Region

#Region "Local Functions and Procedures"


    Private Sub RestGrid()
        Try
            Me.GetAllRecords()
            Me.ApplyGridSettings()
            Me.ReSetControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Form Controls Events"


    ''This event will prevent the user to change the system language.
    'Private Sub frmDefArea_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
    '    e.Cancel = True
    'End Sub

    Private Sub frmDefArea_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmGroupRights_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)


            Me.ApplySecurity(EnumDataMode.[New])

            Call FillCombos()

            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefArea_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                'If Me.btnUpdate.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                ' If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                'If Me.btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                'If Me.btnEdit.Enabled = True Then Me.RestGrid() 'Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                'If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.ToolStripButton = CType(sender, Windows.Forms.ToolStripButton)

            If btn.Name = btnSave.Name Then
                '' Call Save method to save the record
                Me.Save()

                'ElseIf btn.Name = btnCancel.Name Then
                '    Me.RestGrid()

                'ElseIf btn.Name = btnExit.Name Then
                '    Me.Close()



            End If



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub



    Private Sub cboFormGroups_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFormGroups.SelectedIndexChanged
        Try
            Me.GetAllRecords()
            Me.ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lstFormCategories_SelectedIndexChaned(ByVal sender As Object, ByVal e As IndexEventArgs) Handles lstFormCategories.SelectedIndexChaned
        Try
            Me.GetAllRecords()
            Me.ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub chkSaveAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSaveAll.CheckedChanged, chkUpdateAll.CheckedChanged, chkDeleteAll.CheckedChanged, chkViewAll.CheckedChanged, chkPrintAll.CheckedChanged, chkExportAll.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Me.grdAllRecords.RootTable.Groups(0).Expand()
            Me.grdAllRecords.RootTable.Groups(1).Expand()
            Me.int1stLvl = 1
            Me.int2ndLvl = 1

            Select Case chk.Name
                Case Me.chkSaveAll.Name
                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataView).GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Save" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkSaveAll.Checked)
                        End If
                    End While


                Case Me.chkUpdateAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataView).GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Update" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkUpdateAll.Checked)
                        End If
                    End While


                Case Me.chkDeleteAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataView).GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Delete" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkDeleteAll.Checked)
                        End If
                    End While

                Case Me.chkViewAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataView).GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "View" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkViewAll.Checked)
                        End If
                    End While

                Case Me.chkPrintAll.Name

                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataView).GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Print" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkPrintAll.Checked)
                        End If
                    End While

                Case Me.chkExportAll.Name
                    Dim enumrator As IEnumerator = CType(Me.grdAllRecords.DataSource, DataView).GetEnumerator
                    While enumrator.MoveNext
                        Dim r As DataRowView = CType(enumrator.Current, DataRowView)
                        If r.Item(EnumGrid.ControlCaption).ToString = "Export To Excel" Then
                            r.Item(EnumGrid.IsSelected) = Convert.ToByte(Me.chkExportAll.Checked)
                        End If
                    End While
            End Select

            Me.grdAllRecords.Refetch()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If

            Me.cboFormGroups.Enabled = True
            Me.lstFormCategories.Enabled = True

            'If mobjControlList.Item("btnSave") Is Nothing Then
            '    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
            'Else
            '    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            'End If

            Me.btnNew.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnDelete.Enabled = False
            Me.SetNavigationButtons(EnumDataMode.Disabled)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            Me.FillModel()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Private Sub btnExpnd1stLevel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExpnd1stLevel.Click
        If Me.int1stLvl = 0 Then
            Me.grdAllRecords.RootTable.Groups(0).Expand()
            Me.int1stLvl = 1
        Else
            Me.grdAllRecords.RootTable.Groups(0).Collapse()
            Me.int1stLvl = 0
        End If
        
    End Sub

    Private Sub btnExpnd2ndLevel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExpnd2ndLevel.Click
        If Me.int2ndLvl = 0 Then
            Me.grdAllRecords.RootTable.Groups(1).Expand()
            Me.int2ndLvl = 1
            Me.int1stLvl = 1
        Else
            Me.grdAllRecords.RootTable.Groups(1).Collapse()
            Me.int2ndLvl = 0
        End If
    End Sub
End Class
