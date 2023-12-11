''03-Jul-2014 TASK:M61 Enabled Active Control On Designer
Imports SBModel
Imports SBDal
Public Class AddUserGroup
    Implements IGeneral
    Dim UserGroup As UserGroup
    Dim Group_Id As Integer = 0

    Private Sub AddUserGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 16 -1-14
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub AddUserGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Group_Id = 0 Then
                Me.txtGroupName.Text = String.Empty
                Me.chkActive.Checked = True
                Me.txtSortOrder.Text = 1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            UserGroup = New UserGroup
            UserGroup.GroupId = Group_Id
            UserGroup.GroupType = Me.cmbGroupType.Text
            UserGroup.GroupName = Me.txtGroupName.Text
            UserGroup.SortOrder = Val(Me.txtSortOrder.Text)
            UserGroup.Active = Me.chkActive.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtGroupName.Text = String.Empty Then Me.txtGroupName.Focus() : Exit Function Else  : FillModel() : Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                Call New UserGroupDAL().add(UserGroup)
                Return True
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function getUserViewRights(ByVal User_Id As Users)

        Try
            'Call New UserGroupDAL().add(UserGroup)
            ' Call New UserGroupDAL().getUserViewRights(User_Id)
             Catch ex As Exception

        End Try

    End Function
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub
    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If IsValidate() = True Then
                If New UserGroupDAL().update(UserGroup) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            If Group_Id = 0 Then
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                Group_Id = 0
                Me.txtGroupName.Text = String.Empty
                Me.chkActive.Checked = True
                Me.txtSortOrder.Text = 1
            Else
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                Group_Id = 0
                Me.txtGroupName.Text = String.Empty
                Me.chkActive.Checked = True
                Me.txtSortOrder.Text = 1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Close()
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Public Sub GroupDetail(ByVal GroupId As Integer)
        Try
            Dim str As String = "Select * From tblUserGroup WHERE GroupId=" & GroupId
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Group_Id = dt.Rows(0).Item("GroupId")
                    Me.cmbGroupType.Text = dt.Rows(0).Item("GroupType").ToString
                    Me.txtGroupName.Text = dt.Rows(0).Item("GroupName").ToString
                    Me.txtSortOrder.Text = dt.Rows(0).Item("SortOrder")
                    Me.chkActive.Checked = dt.Rows(0).Item("Active")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AddUserGroup_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Group_Id = 0
            Me.txtGroupName.Text = String.Empty
            Me.chkActive.Checked = True
            Me.txtSortOrder.Text = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class