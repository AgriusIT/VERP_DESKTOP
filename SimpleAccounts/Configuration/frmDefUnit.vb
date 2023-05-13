Imports SBModel
Imports SBDal
Imports SBUtility
Public Class frmDefUnit
    Implements IGeneral
    Private objModel As Unit
    Dim IntID As Integer
    Dim IsLoadedForm As Boolean = False
    Enum enmUnit
        ArticleUnitId
        ArticleUnitName
        Comments
        SortOrder
        'IsDate
        Active
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
      
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        If New UnitDAL().DeleteRec(Me.objModel) Then Return True
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        objModel = New Unit
        objModel.ID = IntID
        objModel.Name = Me.txtName.Text
        objModel.Comments = Me.txtComments.Text
        objModel.Active = Me.chkActive.Checked
        objModel.SortOrder = Me.txtSortOrder.Text
        Me.objModel.ActivityLog = New ActivityLog
        ''filling activity log
        Me.objModel.ActivityLog.ApplicationName = "Config"
        Me.objModel.ActivityLog.FormCaption = Me.Text
        'TODO : Define Loging User ID
        Me.objModel.ActivityLog.UserID = 1
        Me.objModel.ActivityLog.LogDateTime = Date.Now

    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GridEX1.DataSource = New SBDal.UnitDAL().GetAllRecords
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtName.Text.Trim = String.Empty Then
                ShowErrorMessage("Please Enter Name")
                Me.txtName.Focus()
                Return False
            End If
            Me.FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        IntID = 0
        Me.txtName.Text = String.Empty
        Me.txtComments.Text = String.Empty
        Me.txtSortOrder.Text = "1"
        Me.uichkActive.Checked = True
        Me.BtnSave.Text = "&Save"
        Call GetAllRecords()
        GetSecurityRights()
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        If New UnitDAL().Add(Me.objModel) Then Return True
    End Function
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub
    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        If New UnitDAL().UpdateRec(Me.objModel) Then Return True
    End Function
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Not Me.IsValidate Then Exit Sub

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        'If Not msg_Confirm(str_ConfirmSave) Then Exit Sub
        Try
            If BtnSave.Text = "&Save" Or BtnSave.Text = "Save" Then
                If Me.Save Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informSave)
                Me.ReSetControls()
            Else

                If Not msg_Confirm(str_ConfirmUpdate) Then Exit Sub
                If Update1() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informUpdate)
                Me.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub frmDefUnit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
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
    Private Sub FrmDefUnit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 
        Try

      
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            ReSetControls()
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
              
        End Try
    End Sub
    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        EditRecord()
    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        'Me.GridEX1.Enabled = False
        ReSetControls()
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not Me.IsValidate Then Exit Sub
        If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            If Delete() Then Me.DialogResult = System.Windows.Forms.DialogResult.Yes
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub GridEX1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
        'If Not GridEX1.GetRow Is Nothing Then
        '    IntID = Me.GridEX1.GetRow.Cells(enmUnit.ArticleUnitId).Value
        '    Me.txtName.Text = Me.GridEX1.GetRow.Cells(enmUnit.ArticleUnitName).Value.ToString
        '    Me.txtComments.Text = Me.GridEX1.GetRow.Cells(enmUnit.Comments).Value.ToString
        '    Me.txtSortOrder.Text = Me.GridEX1.GetRow.Cells(enmUnit.SortOrder).Value.ToString
        '    Me.chkActive.Checked = Me.GridEX1.GetRow.Cells(enmUnit.Active).Value
        '    Me.BtnSave.Text = "&Update"
        'End If
    End Sub
    Sub EditRecord()
        If Not GridEX1.GetRow Is Nothing Then
            IntID = Me.GridEX1.GetRow.Cells(enmUnit.ArticleUnitId).Value
            Me.txtName.Text = Me.GridEX1.GetRow.Cells(enmUnit.ArticleUnitName).Value.ToString
            Me.txtComments.Text = Me.GridEX1.GetRow.Cells(enmUnit.Comments).Value.ToString
            Me.txtSortOrder.Text = Me.GridEX1.GetRow.Cells(enmUnit.SortOrder).Value.ToString
            Me.chkActive.Checked = Me.GridEX1.GetRow.Cells(enmUnit.Active).Value
            Me.BtnSave.Text = "&Update"
            GetSecurityRights()
            Me.txtName.Focus()
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        EditRecord()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From ArticleUnitDefTable WHERE ArticleUnitId='" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.uitxtName.Text = dt.Rows(0).Item("ArticleUnitName").ToString
                        Me.uitxtComments.Text = dt.Rows(0).Item("Comments").ToString
                        Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.uichkActive.Checked = dt.Rows(0).Item("Active")
                        Me.IntID = dt.Rows(0).Item("ArticleUnitId").ToString
                        Me.BtnSave.Text = "&Update"
                        Me.GetSecurityRights()
                        IsDrillDown = True
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            BtnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            BtnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
End Class