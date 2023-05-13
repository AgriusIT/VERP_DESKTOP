Imports SBDAL
Imports SBModel
Imports System.Collections.Specialized
Imports System.Data
Imports SBUtility.Utility

Public Class frmDefMebers

    Implements IGeneral


#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As DefMembers
    Private intPkId As Integer


#End Region

#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridMembers
        MembersID = 0
        MembersCode = 1
        MembersName = 2
        SortOrder = 3
        Comments = 4
        Active = 5
    End Enum


#End Region


#Region "Interface Methods"

    ''This will set the images of the buttons at runtime
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

        Try

            If gEnumIsRightToLeft = Windows.Forms.RightToLeft.No Then
                '    Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                '    Me.btnFirst.ImageKey = "First"

                '    Me.btnNext.ImageList = gobjMyImageListForOperationBar
                '    Me.btnNext.ImageKey = "Next"

                '    Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                '    Me.btnPrevious.ImageKey = "Previous"

                '    Me.btnLast.ImageList = gobjMyImageListForOperationBar
                '    Me.btnLast.ImageKey = "Last"


                'Else
                '    Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                '    Me.btnFirst.ImageKey = "Last"

                '    Me.btnNext.ImageList = gobjMyImageListForOperationBar
                '    Me.btnNext.ImageKey = "Previous"

                '    Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                '    Me.btnPrevious.ImageKey = "Next"

                '    Me.btnLast.ImageList = gobjMyImageListForOperationBar
                '    Me.btnLast.ImageKey = "First"
            End If

            'Me.btnNew.ImageList = gobjMyImageListForOperationBar
            'Me.btnNew.ImageKey = "New"

            'Me.btnSave.ImageList = gobjMyImageListForOperationBar
            'Me.btnSave.ImageKey = "Save"

            'Me.btnUpdate.ImageList = gobjMyImageListForOperationBar
            'Me.btnUpdate.ImageKey = "Update"

            ' Me.btnCancel.ImageList = gobjMyImageListForOperationBar
            Me.btnCancel.ImageKey = "Cancel"

            'Me.btnDelete.ImageList = gobjMyImageListForOperationBar
            'Me.btnDelete.ImageKey = "Delete"

            'Me.btnExit.ImageList = gobjMyImageListForOperationBar
            'Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here will will use this function to fill-up all Combos and Listboxes on the form
    ''Optional condition would be used to fill-up combo or Listbox; which based on the selection of some other combo.
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    ''Here we will use this procedure to load all master records; respective to the screen.
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New DefMembersDAL().GetAll()
            Me.grdAllRecords.DataSource = dt
            ''Applying Grid Formatting Setting
            Me.grdAllRecords.RetrieveStructure()
            Me.ApplyGridSettings()

            ''This will refresh the DefMembers collection in global Hashtable
            ' Call AddDefMembersListToHashTable()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    ''This procedure will be used to set the formatting of the grid on that form. For Example, Grid's columns show/Hide,
    '' Caption setting, columns' width etc.
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            For Each col As Janus.Windows.GridEX.GridEXColumn In grdAllRecords.RootTable.Columns
                col.AutoSize()
            Next

            With Me.grdAllRecords.RootTable

                ''Columns with In-visible setting
                .Columns(EnumGridMembers.MembersID).Visible = False

                ''Set columns widths for visible columns
                .Columns(EnumGridMembers.MembersName).Width = 150
                .Columns(EnumGridMembers.Comments).Width = 300
                .Columns(EnumGridMembers.SortOrder).Width = 70
                ' .Columns(EnumGridDefMembers.SortOrder).FormatString = ""


            End With
            'Me.BindingNavigatorCountItem.Text = Me.grdAllRecords.Rows.Count
            ''Getting Language based captions for the selected Grid, after applying filter criteria on Language Based Controls List
            'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & EnumProjectForms.frmDefCities.ToString & "' AND [Control Name] like 'grdAllRecords%'")

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


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''This procedure will be used (if applicable) to set Active/Deactive or Visible/Invisible some controls on form,
    ''which are based on System level configuration
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    ''This procedure will be used to set the navigation buttons as per Mode
    Public Sub SetNavigationButtons(ByVal mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try

            If mode = EnumDataMode.[New] Then
                ' ''if New Mode then Set Disable all Navigation Buttons
                'btnFirst.Enabled = False ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'btnLast.Enabled = False ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'BindingNavigatorPositionItem.Enabled = False
                Me.ToolStrip2.Enabled = False
            ElseIf mode = EnumDataMode.Edit Then
                Me.ToolStrip2.Enabled = True
                ' ''if New Mode then Set Enable all Navigation Buttons
                'btnFirst.Enabled = True ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'btnPrevious.Enabled = True ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'btnNext.Enabled = True ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'btnLast.Enabled = True ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'BindingNavigatorPositionItem.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''here we will clear all the contols of the screen for New Mode
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.intPkId = 0
            Me.txtName.Text = String.Empty
            Me.txtName.Tag = Nothing
            Me.txtCode.Text = String.Empty
            Me.txtSortOrder.Text = 0
            Me.txtComments.Text = String.Empty
            Me.chkActive.Checked = True
            Me.txtName.Focus()

            ''Set New Mode and Applying Security Setting
            Call ApplySecurity(EnumDataMode.[New])
            Me.PictureBox1.Image = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will pass an argument MODE (New|Edit|Disabled), which will be overwritten according to the rights 
    ''available to user on that screen
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If

            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.Edit)
                Me.grdAllRecords.Enabled = True

            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'TODO: remove after security implementation
                'If mobjControlList.Item("btnSave") Is Nothing Then
                '    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'Else
                btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'End If

                btnSave.Text = "&Save" ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = True
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = False

            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System

                btnSave.Text = "&Update" ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = True

                'If mobjControlList.Item("btnUpdate") Is Nothing Then
                '    btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'Else
                '    btnUpdate.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'End If

                'If mobjControlList.Item("btnDelete") Is Nothing Then
                'btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'Else
                btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'End If
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = True

                Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                'btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = True

                Me.grdAllRecords.Focus()

            End If

            'TODO: 
            ' '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            'If mobjControlList.Item("btnExport") Is Nothing Then
            '    Me.UiCtrlGridBar1.btnExport.Enabled = False
            'End If


            ' '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            'If mobjControlList.Item("btnPrint") Is Nothing Then
            '    Me.UiCtrlGridBar1.btnPrint.Enabled = False
            'End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will apply Front End Validations.
    Public Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try

            ''1 First Check Front End Validations
            If Mode = EnumDataMode.[New] Or Mode = EnumDataMode.Edit Then
                ''Check Name is Required
                If Me.txtName.Text.Trim = String.Empty Then
                    ShowValidationMessage(gstrMsgNameRequired)
                    Me.txtName.Focus()
                    Return False

                    ''Check Code is Required
                ElseIf Me.txtCode.Text.Trim = String.Empty Then
                    ShowValidationMessage(gstrMsgCodeRequired)
                    Me.txtCode.Focus()
                    Return False

                    ''if SortOrder is not Given  
                ElseIf Me.txtSortOrder.Text.Trim = String.Empty Then
                    Me.txtSortOrder.Text = 0
                    ''if SortOrder is Given  but Not Numeric
                ElseIf Me.txtSortOrder.Text.Trim <> String.Empty And Not IsNumeric(Me.txtSortOrder.Text.Trim) Then
                    ShowValidationMessage(gstrMsgWrongInput)
                    Me.txtSortOrder.Focus()
                    Return False
                End If
            End If
            ''===========================================   
            ''2 Database End Validations

            ''Fill Model with the front end values
            Me.FillModel()

            If Condition = "BackEndDeleteValidation" Then
                ''Check Dependancy existance
                Return New DefMembersDAL().IsValidateForDelete(mobjModel)

            End If

            ''Check Name or Code Duplication
            Call New DefMembersDAL().IsValidateForSave(mobjModel)






            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''Here we will create an instance of the Members, according to the form, and will set the properties of the object
    ''Later this object will be refered in Save|Update|Delete function.
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            ''Create Model object
            mobjModel = New DefMembers
            With mobjModel
                ''Setting the Model Object Values
                .MembersID = intPkId
                .MembersName = Me.txtName.Text.Trim
                .MembersCode = Me.txtCode.Text.Trim
                .SortOrder = Me.txtSortOrder.Text.Trim
                .Comments = Me.txtComments.Text.Trim
                .Active = Me.chkActive.Checked
                '.ActivityLog.ShopID = gObjUserInfo.ShopInfo.ShopID
                '.ActivityLog.ScreenTitle = Me.Text
                '.ActivityLog.LogGroup = "Definition"
                '.ActivityLog.UserID = gObjUserInfo.UserID
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here we will call DAL Function for SAVE, and if the function successfully Saves the records
    ''then the function will return True, otherwise returns False
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage(gstrMsgSave, MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New DefMembersDAL().Add(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'TODO: Select last edited record
                        ' ''========
                        ''to select the last updated record
                        'For Rind As Int32 = 0 To (grdAllRecords.RowCount - 1)
                        '    If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridDefMembers.DefMembersID).Value = mobjModel.DefMembersID Then
                        '        Me.grdAllRecords.Row = Rind
                        '        Exit For
                        '    End If
                        'Next

                        ' Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
                        ''========

                        ''Reset controls and set New Mode
                        Me.ReSetControls()

                    End If

                End If
            End If

        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
                Me.txtName.Focus()
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
                Me.txtCode.Focus()
            Else
                Me.txtName.Focus()
                Throw ex
            End If
        End Try
    End Function

    ''Here we will call DAL Function for Update the selected record, and if the function successfully Updates the records
    ''then the function will return True, otherwise returns False
    Public Function UpdateMember(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.Edit) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage(gstrMsgUpdate, MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Update Method by passing Model Object
                    If New DefMembersDAL().Update(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'TODO: Select last updated record
                        ''to select the last updated record
                        'For Rind As Int32 = 0 To (grdAllRecords.RowCount - 1)
                        '    If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridDefMembers.DefMembersID).Value = mobjModel.DefMembersID Then
                        '        Me.grdAllRecords.Row = Rind
                        '        Exit For
                        '    End If
                        'Next

                        'Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        If gblnShowAfterUpdateMessages Then
                            ''Getting Save Confirmation from User
                            ShowInformationMessage(gstrMsgAfterUpdate)
                        End If


                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
                Me.txtName.Focus()
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
                Me.txtCode.Focus()
            Else
                Me.txtName.Focus()
                Throw ex
            End If
        End Try
    End Function

    ''Here we will call DAL Function for Delete the selected record, and if the function successfully Deletes the records
    ''then the function will return True, otherwise returns False
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(, "BackEndDeleteValidation") Then
                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage(gstrMsgDelete, MessageBoxDefaultButton.Button2)


                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Delete Method by passing Model Object
                    If New DefMembersDAL().Deleted(Me.mobjModel) Then

                        ''This will hold row index of the selected row 
                        Dim intGridRowIndex As Integer
                        intGridRowIndex = Me.grdAllRecords.GetRow.RowIndex ' Me.grdAllRecords.ActiveRow.Index

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        ''Call RowColumn Change Event
                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        ''Reset the row index to the grid
                        'If intGridRowIndex > (Me.grdAllRecords.Rows.Count - 1) Then intGridRowIndex = (Me.grdAllRecords.Rows.Count - 1)
                        'If Not intGridRowIndex < 0 Then Me.grdAllRecords.Rows(intGridRowIndex).Activate()
                    End If
                End If
            End If

        Catch ex As Exception
            Me.txtName.Focus()
            Throw ex
        End Try
    End Function


#End Region


#Region "Local Functions and Procedures"



#End Region


#Region "Form Controls Events"

    ''This event will prevent the user to change the system language.
    Private Sub frmDefDefMembers_InputLanguageChanging(ByVal sender As System.Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles MyBase.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub frmDefDefMembers_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ' Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub frmDefDefMembers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ' ''Getting all available controls list to the user on this screen; in a collection 
            'mobjControlList = GetFormSecurityControls(EnumProjectForms.frmDefCities.ToString())

            ' ''Assing Images to Buttons
            'Me.SetButtonImages()

            ' ''Get all available record for the respective screen and fill the grid
            Call GetAllRecords()

            ' ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
            ''AddHandler grdAllRecords.SelectionChanged, AddressOf Me.grdAllRecords_SelectionChanged

            ' ''Reset the controls for new mode
            'Call ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub frmDefDefMembers_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
        ' AddHandler grdAllRecords.AfterRowActivate, AddressOf Me.grdAllRecords_SelectionChanged
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click, btnSave.Click, btnDelete.Click, btnCancel.Click

        Try


            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.ToolStripButton = CType(sender, Windows.Forms.ToolStripButton)
            'Dim btn As Control = CType(sender, Control)
            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then

                ''Refresh the controls for new mode
                Me.ReSetControls()

            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method to save the record
                If btnSave.Text = "Save" Or btnSave.Text = "&Save" Then
                    Me.Save()
                Else
                    '' Call Update method to update the record
                    Me.Update()
                End If
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method to delete the record
                Me.Delete()
            ElseIf btn.Name = btnCancel.Name Then
                ''Load Selected record in Edit Mode
                Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click, btnFirst.Click, btnNext.Click, btnLast.Click

        Try

            Dim btn As ToolStripButton = CType(sender, ToolStripButton)

            ''If Move First is clicked, then navigate to first record of the grid
            'If btn.Name = Me.btnFirst.Name Then
            '    'Me.grdAllRecords.Rows(0).Activate()
            '    ' Me.grdAllRecords.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.FirstRowInGrid)
            '    ''If Move Previous is clicked, then navigate to Previous record of the grid
            'ElseIf btn.Name = Me.btnPrevious.Name Then
            '    'If Me.grdAllRecords.ActiveRow.Index > 0 Then Me.grdAllRecords.Rows(Me.grdAllRecords.ActiveRow.Index - 1).Activate()
            '    Me.grdAllRecords.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.PrevRow)
            '    ''If Move Next is clicked, then navigate to next record of the grid
            'ElseIf btn.Name = Me.btnNext.Name Then
            '    'If Me.grdAllRecords.ActiveRow.Index >= 0 Then Me.grdAllRecords.Rows(Me.grdAllRecords.ActiveRow.Index + 1).Activate()

            '    Me.grdAllRecords.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.NextRow)
            '    ''If Move Last is clicked, then navigate to Last record of the grid
            'ElseIf btn.Name = Me.btnLast.Name Then
            '    Me.grdAllRecords.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.LastRowInGrid)
            'End If
            'Me.BindingNavigatorPositionItem.Text = Me.grdAllRecords.ActiveRow.Index + 1
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdAllRecords_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAllRecords.SelectionChanged
        Try

            ''If there is no record found in grid then load the screen in new mode
            'If grdAllRecords.Rows.Count = 0 Then
            '    Me.ReSetControls()
            '    Exit Sub
            'End If

            If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                Exit Sub
            End If



            ''set the values of the selected record in editable controls
            ''Set ID in Tag Property
            ' Then Exit Sub

            With Me.grdAllRecords.GetRow
                Me.intPkId = Val(.Cells(EnumGridMembers.MembersID).Text.ToString) 'IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridDefMembers.DefMembersID).ToString), 0, Convert.ToInt32(Me.grdAllRecords.GetValue(EnumGridDefMembers.DefMembersID).ToString))
                Me.txtName.Text = .Cells(EnumGridMembers.MembersName).Text.ToString 'IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridDefMembers.DefMembersName).ToString), " ", Me.grdAllRecords.GetValue(EnumGridDefMembers.DefMembersName).ToString)
                Me.txtCode.Text = .Cells(EnumGridMembers.MembersCode).Text.ToString 'IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridDefMembers.DefMembersCode).ToString), " ", Me.grdAllRecords.GetValue(EnumGridDefMembers.DefMembersCode).ToString)
                Me.txtSortOrder.Text = Val(.Cells(EnumGridMembers.SortOrder).Text.ToString) 'IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridDefMembers.SortOrder).ToString), 0, Convert.ToInt32(Val(Me.grdAllRecords.GetValue(EnumGridDefMembers.SortOrder).ToString)))
                Me.txtComments.Text = .Cells(EnumGridMembers.Comments).Text.ToString 'IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridDefMembers.Comments).ToString), " ", Me.grdAllRecords.GetValue(EnumGridDefMembers.Comments).ToString)
                Me.chkActive.Checked = .Cells(EnumGridMembers.Active).Value

                Dim strImage As String = Application.StartupPath & "\Members Images\" & Me.txtCode.Text.Trim & ".jpg"
                If System.IO.File.Exists(strImage) Then 'Application.StartupPath & "\Members Images\" & Me.cmbMembers.ActiveRow.Cells(1).Value & ".jpg") Then
                    Me.PictureBox1.Image = Image.FromFile(strImage)
                Else
                    Me.PictureBox1.Image = Nothing
                End If

                'TODO: Apply security
                'If IIf((UCase("" & .Cells(EnumGridDefMembers.ISReadOnly).Text.ToString) = "READONLY"), True, False) = True Then
                '    Call ApplySecurity(EnumDataMode.ReadOnly)
                'Else
                Call ApplySecurity(EnumDataMode.Edit)
                'End If

            End With
            ' Me.BindingNavigatorPositionItem.Text = Me.grdAllRecords.ActiveRow.Index + 1
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Private Sub frmDefDefMembers_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then
                    If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                        Me.Save()
                    Else
                        Me.Update()
                    End If
                End If
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If Me.btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If Me.btnCancel.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        'If Me.txtCode.Text.Trim.Length = 0 Then Me.txtCode.Text = Me.txtName.Text.Trim
    End Sub

    Private Sub BindingNavigatorPositionItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingNavigatorPositionItem.Leave
        If Val(Me.BindingNavigatorPositionItem.Text) <= Val(Me.BindingNavigatorCountItem.Text) Then
            'Me.grdAllRecords.Rows(Val(Me.BindingNavigatorPositionItem.Text) - 1).Activate()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click

        Try
            Dim FilePath As String = Application.StartupPath & "\" & Me.lblHeader.Text & ".xls"
            '   Me.UltraGridExcelExporter1.Export(Me.grdAllRecords, FilePath)
            System.Diagnostics.Process.Start(FilePath)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


#End Region

End Class