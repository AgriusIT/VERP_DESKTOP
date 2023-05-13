Imports SBDal
Imports SBModel
Public Class frmAreaDef
    Implements IGeneral


    Dim objModel As AreaDefBE
    Dim objDAL As AreaDefDAL
    Public Shared TerritoryId As Integer = 0
    Public Shared blnEditMode As Boolean = False
    'Private Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub
    'Public Sub New(ByVal _TerritoryId As Integer, ByVal _blEditMode As Boolean)

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.
    '    Try
    '        If blnEditMode = True Then
    '            GetById(_TerritoryId)
    '        Else
    '            ReSetControls()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try

    'End Sub
    Private Sub frmAreaDef_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If blnEditMode = True Then
                GetById(TerritoryId)
            Else
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub GetById(ItemId As Integer)
        Try
            Dim dt As DataTable = New AreaDefDAL().GetById(ItemId)

            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("TerritoryId") > 0 Then
                ShowData(dt)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowData(ByVal dt As DataTable)
        Try
            'getting columns name ArticleTypeId, ArticleTypeName, Comments, Active, SortOrder, TypeCode, IsDate
            ' Me.cmbProvinceName.Text = dt.Rows(0).Item("CityName").ToString
            FillCombos("City")
            Me.txtName.Text = dt.Rows(0).Item("CityName").ToString
            Me.txtComments.Text = dt.Rows(0).Item("Comments").ToString
            Me.txtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
            Me.uichkActive.Checked = dt.Rows(0).Item("Active").ToString

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmAreaDef_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F4 Then
            If blnEditMode = False Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    Me.Close()
                Else
                    Me.txtName.Focus()
                End If

            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Update1()
                ReSetControls()
                blnEditMode = False
                msg_Information(str_informUpdate)
                Me.Close()
            End If

        End If
        If e.KeyCode = Keys.Insert Then
            If blnEditMode = False Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    ReSetControls()
                Else
                    Me.txtName.Focus()
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Update1()
                ReSetControls()
                blnEditMode = False
                msg_Information(str_informUpdate)
                'Me.Close()
            End If
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.F5 Then
            'Refresh Function
        End If
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim str As String = ""
        If Condition = "City" Then
            str = "select TerritoryId, TerritoryName  from tblListTerritory"
            FillDropDown(Me.cmbCity, str, False)
        End If
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New AreaDefBE
            objModel.TerritoryId = TerritoryId
            objModel.TerritoryName = Me.txtName.Text
            objModel.Comments = Me.txtComments.Text
            objModel.IsDate = Date.Now
            If Me.uichkActive.Checked = False Then
                objModel.Active = 0
            Else
                objModel.Active = 1
            End If
            objModel.ActivityLog = New ActivityLog
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtName.Text = String.Empty Then
                msg_Error("Please add Territory type")
                Return False
            End If
            'If Me.txtTypeCode.Text = String.Empty Then
            '    msg_Error("Please add article type code")
            '    Return False
            'End If

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        FillCombos("City")
        Me.txtName.Text = ""
        Me.txtComments.Text = ""
        Me.txtSortOrder.Text = 1
        Me.uichkActive.Checked = True
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New AreaDefDAL
            If IsValidate() = True Then
                objDAL.Add(objModel)
                Return True
            Else
                Return False

            End If
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
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If blnEditMode = False Then
                If Save() = True Then
                    ReSetControls()
                    'GetAllRecords()
                    msg_Information(str_informSave)
                    'Me.Close()
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Update1()
                ReSetControls()
                'GetAllRecords()
                blnEditMode = False
                msg_Information(str_informUpdate)
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class