Imports sbDAL
Imports sbModel
Imports System.Collections.Specialized
Imports System.Data
Imports sbUtility.Utility

Public Class frmDefSecurityGroup
    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As SecurityGroup
    Private intPkId As Integer
#End Region

#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridSecurityGroup
        GroupID
        GroupName
        GroupComments
    End Enum


#End Region


#Region "Interface Methods"

    ''This will set the images of the buttons at runtime
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

      

    End Sub

    ''Here will will use this function to fill-up all Combos and Listboxes on the form
    ''Optional condition would be used to fill-up combo or Listbox; which based on the selection of some other combo.
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

           
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will use this procedure to load all master records; respective to the screen.
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New GroupDAL().GetAll()
            Me.grdAllRecords.DataSource = dt ''New CityDAL().GetAll()
            Me.grdAllRecords.RetrieveStructure()

            ''Applying Grid Formatting Setting
            Me.ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    ''This procedure will be used to set the formatting of the grid on that form. For Example, Grid's columns show/Hide,
    '' Caption setting, columns' width etc.
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            ''Columns with In-visible setting
            Me.grdAllRecords.RootTable.Columns(EnumGridSecurityGroup.GroupID).FormatString = ""
            Me.grdAllRecords.RootTable.Columns(EnumGridSecurityGroup.GroupID).Visible = False

            ''Set columns widths for visible columns
            Me.grdAllRecords.RootTable.Columns(EnumGridSecurityGroup.GroupComments).Width = 500
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

    End Sub

    ''here we will clear all the contols of the screen for New Mode
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.intPkId = 0
            Me.txtGroupName.Text = String.Empty
            Me.txtComments.Text = String.Empty
            Me.txtGroupName.Focus()

            ''Set New Mode and Applying Security Setting

            If IsEnhancedSecurity Then
                Me.ApplySecurity(EnumDataMode.[New])
            Else
                Me.GetSecurityRights()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will pass an argument MODE (New|Edit|Disabled), which will be overwritten according to the rights 
    ''available to user on that screen
    Public Sub ApplySecurity(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
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
                'btnEdit.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.Edit)
                Me.grdAllRecords.Enabled = True

            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then


                Me.btnSave.Text = "Save"

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                If Not Me.mobjControlList.Item("btnSave") Then
                    btnSave.Enabled = True
                Else
                    btnSave.Enabled = False
                End If
                ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                'btnEdit.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                '                Me.grdAllRecords.Enabled = False

            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then


                Me.btnSave.Text = "Update"

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                If Not Me.mobjControlList.Item("btnUpdate") Then
                    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    Me.btnSave.Enabled = False
                End If

                'btnEdit.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                If Not Me.mobjControlList.Item("btnDelete") Then
                    btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                Else
                    Me.btnDelete.Enabled = False
                End If
                ' btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = True

                Me.grdAllRecords.Focus()

                ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                    btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                    'btnEdit.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                    btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                    '               btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System
                    '
                    SetNavigationButtons(Mode)

                    Me.grdAllRecords.Enabled = True

                    Me.grdAllRecords.Focus()

                End If


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
                If Me.txtGroupName.Text.Trim = String.Empty Then
                    ShowValidationMessage("Enter group name")
                    Me.txtGroupName.Focus()
                    Return False
                End If
            End If
            ''===========================================   
            ''2 Database End Validations

            ''Fill Model with the front end values
            Me.FillModel()

            If Condition = "BackEndDeleteValidation" Then
                ''Check Dependancy existance
                Return New GroupDAL().IsValidateForDelete(mobjModel)

            End If

            ''Check Name or Code Duplication
            Call New GroupDAL().IsValidateForSave(mobjModel)

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''Here we will create an instance of the class, according to the form, and will set the properties of the object
    ''Later this object will be refered in Save|Update|Delete function.
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            ''Create Model object
            mobjModel = New SecurityGroup
            With mobjModel
                ''Setting the Model Object Values
                .GroupID = intPkId
                .GroupName = Me.txtGroupName.Text.Trim
                .GroupComments = Me.txtComments.Text.Trim
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
                    result = ShowConfirmationMessage("Do you want to save data", MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New GroupDAL().Add(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        ''========
                        'to select the last updated record
                        For Rind As Int32 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridSecurityGroup.GroupID).Value = mobjModel.GroupID Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
                        ''========

                        ''Reset controls and set New Mode
                        Me.ReSetControls()

                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''Here we will call DAL Function for Update the selected record, and if the function successfully Updates the records
    ''then the function will return True, otherwise returns False
    Public Function UpdateSecurityGroup(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.Edit) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage("Do you want to update", MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Update Method by passing Model Object
                    If New GroupDAL().Update(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'to select the last updated record
                        For Rind As Int32 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridSecurityGroup.GroupID).Value = mobjModel.GroupID Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        If gblnShowAfterUpdateMessages Then
                            ''Getting Save Confirmation from User
                            ShowInformationMessage("Record Updated")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

            Throw ex
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
                result = ShowConfirmationMessage("Do you want to delete", MessageBoxDefaultButton.Button2)


                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Delete Method by passing Model Object
                    If New GroupDAL().Deleted(Me.mobjModel) Then

                        ''This will hold row index of the selected row 
                        Dim intGridRowIndex As Integer
                        intGridRowIndex = Me.grdAllRecords.Row

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        ''Call RowColumn Change Event
                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        ''Reset the row index to the grid
                        If intGridRowIndex > (Me.grdAllRecords.RowCount - 1) Then intGridRowIndex = (Me.grdAllRecords.RowCount - 1)
                        If Not intGridRowIndex < 0 Then Me.grdAllRecords.Row = intGridRowIndex
                    End If
                End If
            End If

        Catch ex As Exception
            Me.txtGroupName.Focus()
            Throw ex
        End Try
    End Function


#End Region


#Region "Local Functions and Procedures"



#End Region


#Region "Form Controls Events"

    ' ''This event will prevent the user to change the system language.
    'Private Sub frmDefCity_InputLanguageChanging(ByVal sender As System.Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles MyBase.InputLanguageChanging
    '    e.Cancel = True
    'End Sub

    Private Sub frmDefCity_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmDefCity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.mobjControlList = GetFormSecurityControls(Me.Name)

            ''Get all available record for the respective screen and fill the grid
            Call GetAllRecords()

            ''Reset the controls for new mode
            Call ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefCity_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
        '        AddHandler grdAllRecords.SelectionChanged, AddressOf Me.grdAllRecords_SelectionChanged
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click, btnSave.Click, btnEdit.Click, btnDelete.Click, btnPrint.Click

        Try


            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As ToolStripButton = CType(sender, Windows.Forms.ToolStripButton)
            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then

                ''Refresh the controls for new mode
                Me.btnSave.Text = "Save"
                Me.ReSetControls()

            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method to save the record

                If btn.Text = "Save" Or btn.Text = "&Save" Then
                    Me.Save()
                Else
                    Me.Update()
                End If


            ElseIf btn.Name = btnEdit.Name Then

                '' Call Update method to update the record

                Me.btnSave.Text = "Update"
                Me.grdAllRecords_SelectionChanged(sender, e)
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method to delete the record
                Me.Delete()
                'ElseIf btn.Name = btnCancel.Name Then
                '    ''Load Selected record in Edit Mode
                '    Me.grdAllRecords_SelectionChanged(Nothing, Nothing)


                'ElseIf btn.Name = btnExit.Name Then
                '    Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub


    Private Sub grdAllRecords_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAllRecords.SelectionChanged
        Try

            ''If there is no record found in grid then load the screen in new mode
            If grdAllRecords.RowCount = 0 Then
                Me.ReSetControls()
                Exit Sub
            End If

            If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                Exit Sub
            End If

            ''set the values of the selected record in editable controls
            ''Set ID in Tag Property
            Me.intPkId = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridSecurityGroup.GroupID).ToString), 0, Convert.ToInt32(Me.grdAllRecords.GetValue(EnumGridSecurityGroup.GroupID).ToString))
            Me.txtGroupName.Text = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridSecurityGroup.GroupName).ToString), " ", Me.grdAllRecords.GetValue(EnumGridSecurityGroup.GroupName).ToString)
            Me.txtComments.Text = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGridSecurityGroup.GroupComments).ToString), " ", Me.grdAllRecords.GetValue(EnumGridSecurityGroup.GroupComments).ToString)

            If IsEnhancedSecurity Then
                Me.ApplySecurity(EnumDataMode.Edit)
            Else
                Me.GetSecurityRights()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Private Sub frmDefSecurityGroup_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                If Me.btnEdit.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If Me.btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If btnEdit.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                'If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub GetSecurityRights()
        Try
            If RegisterStatus = EnumRegisterStatus.Expired Then
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                ' Me.PrintListToolStripMenuItem.Enabled = False
                'PrintToolStripMenuItem.Enabled = False

                Exit Sub
            End If
            'Dim dt As DataTable = GetFormRights(EnumForms.frmDefSecurityGroup)
            'If Not dt Is Nothing Then
            '    If Not dt.Rows.Count = 0 Then
            '        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
            '            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
            '        Else
            '            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
            '        End If
            '        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
            '        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
            '        'Me.PrintListToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
            '        'PrintToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
            '        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
            '    End If
            'End If
            Me.Visible = False
            Me.btnNew.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            'CtrlGrdBar1.mGridPrint.Enabled = False
            'CtrlGrdBar1.mGridExport.Enabled = False

            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "New" Then
                    Me.btnNew.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Edit" Then
                    Me.btnEdit.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.btnDelete.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                    'CtrlGrdBar1.mGridPrint.Enabled = True
                    'ElseIf Rights.Item(i).FormControlName = "Export" Then
                    'CtrlGrdBar1.mGridExport.Enabled = True
                    'ElseIf Rights.Item(i).FormControlName = "Post" Then
                    'me.chkPost.Visible = True
                End If
            Next
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class