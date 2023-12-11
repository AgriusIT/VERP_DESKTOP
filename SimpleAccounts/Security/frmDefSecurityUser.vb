
Imports SBUtility.Utility
Imports sbDAL
Imports System.Collections.Specialized
Imports System.Data
Imports SBModel

Public Class frmDefSecurityUser
    Implements IGeneral


#Region "Variables"
    Private mobjModel As SecurityUser
    Private intPkId As Integer
    Private mobjControlList As NameValueCollection
#End Region

#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGrid
        UserID
        GroupID
        UserName
        UserLogID
        UserLogPassword
        UserEmail
        UserComments
        EndDate
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

            ''filling Group  combo
            Dim dt As DataTable = New SBDal.GroupDAL().GetAll()

            Dim dr As DataRow = dt.NewRow()
            dr("Group Name") = gstrComboZeroIndexString
            dr("Group ID") = 0
            dt.Rows.InsertAt(dr, 0)

            Me.cboGroup.DataSource = dt
            Me.cboGroup.DisplayMember = "Group Name"
            Me.cboGroup.ValueMember = "Group ID"
            Me.cboGroup.SelectedIndex = 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will use this procedure to load all master records; respective to the screen.
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try

            Me.grdAllRecords.DataSource = Nothing

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New SecurityUserDAL().GetAll(Me.cboGroup.SelectedValue)
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
            Me.grdAllRecords.RootTable.Columns(EnumGrid.GroupID).FormatString = ""
            Me.grdAllRecords.RootTable.Columns(EnumGrid.UserID).FormatString = ""
            Me.grdAllRecords.RootTable.Columns(EnumGrid.GroupID).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGrid.UserID).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGrid.UserLogPassword).Visible = False

            ''Set columns widths for visible columns
            Me.grdAllRecords.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor

            Me.grdAllRecords.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''This procedure will be used (if applicable) to set Active/Deactive or Visible/Invisible some controls on form,
    ''which are based on System level configuration
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    ''This procedure will be used to set the navigation buttons as per Mode
    

    ''here we will clear all the contols of the screen for New Mode
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.intPkId = 0
            Me.txtUserName.Text = String.Empty
            Me.txtEmail.Text = String.Empty
            Me.txtComments.Text = String.Empty
            Me.dtpEndDate.Value = Now
            Me.dtpEndDate.Checked = False
            Me.txtLoginID.Text = String.Empty
            Me.txtPassword.Text = String.Empty
            Me.txtConfirmPassword.Text = String.Empty
            Me.txtUserName.Focus()

            ''Set New Mode and Applying Security Setting
            Call ApplySecurity(SBUtility.Utility.EnumDataMode.[New])

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will pass an argument MODE (New|Edit|Disabled), which will be overwritten according to the rights 
    ''available to user on that screen
    
    ''Here we will apply Front End Validations.
    

    ''Here we will create an instance of the class, according to the form, and will set the properties of the object
    ''Later this object will be refered in Save|Update|Delete function.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            ''Create Model object
            mobjModel = New SecurityUser

            With mobjModel
                ''Setting the Model Object Values
                .UserID = intPkId
                .GroupInfo.GroupID = Me.cboGroup.SelectedValue
                .UserName = Me.txtUserName.Text.Trim
                .UserEmail = Me.txtEmail.Text
                .UserComments = Me.txtComments.Text.Trim
                .UserEndDate = IIf(Me.dtpEndDate.Checked, Me.dtpEndDate.Value, DateTime.MinValue)
                .LoginID = Me.txtLoginID.Text
                .LoginPassword = Me.txtPassword.Text
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
            If Me.IsValidate(SBUtility.Utility.EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage(gstrMsgSave, MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New SecurityUserDAL().Add(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        ''========
                        'to select the last updated record
                        For Rind As Int32 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGrid.UserID).Value = mobjModel.UserID Then
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
    Public Function UpdateSecurityUser(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(SBUtility.Utility.EnumDataMode.Edit) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage(gstrMsgUpdate, MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Update Method by passing Model Object
                    If New SecurityUserDAL().Update(Me.mobjModel) Then

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()

                        'to select the last updated record
                        For Rind As Int32 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGrid.UserID).Value = mobjModel.UserID Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                        ''Getting Save Confirmation from User
                        ShowInformationMessage(gstrMsgAfterUpdate)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(, "BackEndDeleteValidation") Then
                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage("Do you want to delete", MessageBoxDefaultButton.Button2)


                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Delete Method by passing Model Object
                    If New SecurityUserDAL().Delete(Me.mobjModel) Then

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
            Throw ex
        End Try

    End Function

#End Region


#Region "Local Functions and Procedures"



#End Region


#Region "Form Controls Events"


    Private Sub frmDefSecurityUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            mobjControlList = GetFormSecurityControls(Me.Name)

            Me.FillCombos()

            ''Reset the controls for new mode
            Call ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub frmDefCity_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
        'AddHandler grdAllRecords.SelectionChanged, AddressOf Me.grdAllRecords_SelectionChanged

    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click, btnSave.Click, btnEdit.Click, btnDelete.Click, BtnPrint.Click

        Try


            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As ToolStripButton = CType(sender, Windows.Forms.ToolStripButton)
            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then

                ''Refresh the controls for new mode
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
            Me.intPkId = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGrid.UserID).ToString), 0, Convert.ToInt32(Me.grdAllRecords.GetValue(EnumGrid.UserID).ToString))
            Me.cboGroup.SelectedValue = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGrid.GroupID)), "0", Me.grdAllRecords.GetValue(EnumGrid.GroupID).ToString)
            Me.txtUserName.Text = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGrid.UserName).ToString), " ", Me.grdAllRecords.GetValue(EnumGrid.UserName).ToString)
            Me.txtEmail.Text = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGrid.UserEmail).ToString), " ", Me.grdAllRecords.GetValue(EnumGrid.UserEmail).ToString)
            Me.txtComments.Text = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGrid.UserComments).ToString), " ", Me.grdAllRecords.GetValue(EnumGrid.UserComments).ToString)
            If Not IsDBNull(Me.grdAllRecords.GetValue(EnumGrid.EndDate)) Then
                Me.dtpEndDate.Checked = True
                Me.dtpEndDate.Text = (Me.grdAllRecords.GetValue(EnumGrid.EndDate).ToString())
            Else
                Me.dtpEndDate.Text = DateTime.MinValue
                Me.dtpEndDate.Checked = False
            End If
            Me.txtLoginID.Text = IIf(IsDBNull(Me.grdAllRecords.GetValue(EnumGrid.UserLogID).ToString), " ", Me.grdAllRecords.GetValue(EnumGrid.UserLogID).ToString)
            Me.txtPassword.Text = DecryptWithALP(Me.grdAllRecords.GetValue(EnumGrid.UserLogPassword).ToString)
            Me.txtConfirmPassword.Text = Me.txtPassword.Text

            Call ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub





    Private Sub cboGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGroup.SelectedIndexChanged
        Try
            If Me.cboGroup.SelectedIndex > 0 Then
                Me.GetAllRecords()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Public Sub ApplySecurity(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
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

                'If Not Me.mobjControlList.Item("btnSave") Is Nothing Then
                Me.btnSave.Enabled = True
                'Else
                ' btnSave.Enabled = False
                'End If


                'btnEdit.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                '                Me.grdAllRecords.Enabled = False

            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then


                Me.btnSave.Text = "Update"

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                'If Not Me.mobjControlList.Get("btnUpate") Is Nothing Then
                Me.btnSave.Enabled = True
                'Else
                'btnSave.Enabled = False
                'End If

                'btnEdit.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                'If Not Me.mobjControlList.Get("btnDelete") Is Nothing Then
                Me.btnSave.Enabled = True
                'Else
                'btnSave.Enabled = False
                'End If
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

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            ''casting selecting group to datarowview
            Dim dvr As DataRowView = CType(cboGroup.SelectedItem, DataRowView)

            ''1 First Check Front End Validations
            If Mode = SBUtility.Utility.EnumDataMode.[New] Or Mode = SBUtility.Utility.EnumDataMode.Edit Then

                ''check selecton for groups
                If Me.cboGroup.SelectedIndex <= 0 Then
                    ShowValidationMessage(gMsgValueRequired)
                    Me.cboGroup.Focus()
                    Return False

                    ''Check Name is Required
                ElseIf Me.txtUserName.Text.Trim = String.Empty Then
                    ShowValidationMessage(gMsgValueRequired)
                    Me.txtUserName.Focus()
                    Return False
                End If

                If Me.txtLoginID.Text.Trim = String.Empty Then
                    ShowValidationMessage(gMsgValueRequired)
                    Me.txtLoginID.Focus()
                    Return False
                End If

                If Me.txtPassword.Text.Trim = String.Empty Then
                    ShowValidationMessage(gMsgValueRequired)
                    Me.txtPassword.Focus()
                    Return False
                End If

                If Me.txtConfirmPassword.Text.Trim = String.Empty Then
                    ShowValidationMessage(gMsgValueRequired)
                    Me.txtConfirmPassword.Focus()
                    Return False
                End If


                If Me.txtPassword.Text.Trim <> Me.txtConfirmPassword.Text.Trim Then
                    ShowValidationMessage("Password and confirm passwords are not equal")
                    Me.txtPassword.Focus()
                    Return False
                End If

            End If

            ''===========================================

            ''Fill Model with the front end values
            Me.FillModel()

            ''Check Name or Code Duplication
            If Mode = SBUtility.Utility.EnumDataMode.[New] Then
                Call New SecurityUserDAL().IsValidateForSave(mobjModel)
            Else
                Call New SecurityUserDAL().IsValidateForUpdate(mobjModel)
            End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Private Sub GetSecurityRights()
        Try
            Me.Visible = False
            Me.btnNew.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.BtnPrint.Enabled = False
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
                    Me.BtnPrint.Enabled = True
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