Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Imports System.Data
Public Class frmAdjustmentType
    Implements IGeneral
    Dim AdjTypeId As Integer = 0I
    Dim Adj As AdjustmentTypeBE

    Private Sub frmAdjustmentType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 16 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub
    Private Sub frmAdjustmentType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New AdjustmentTypeDAL().Delete(Adj) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Adj = New AdjustmentTypeBE
            Adj.AdjTypeId = AdjTypeId
            Adj.AdjTypeName = Me.txtAdjustmentType.Text.Replace("'", "''")
            Adj.Sort_Order = Val(Me.txtSortOrder.Text)
            Adj.Active = Me.chkActive.Checked
            Adj.AdjustmentInShort = Me.CheckBox1.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            dt = New AdjustmentTypeDAL().getAll
            Me.grdDataHistory.DataSource = dt
            Me.grdDataHistory.RetrieveStructure()
            Me.grdDataHistory.RootTable.Columns(0).Visible = False
            Me.grdDataHistory.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtAdjustmentType.Text = String.Empty Then
                ShowErrorMessage("Please enter adjustment type name")
                Me.txtAdjustmentType.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            AdjTypeId = 0
            Me.btnSave.Text = "&Save"
            Me.txtAdjustmentType.Text = String.Empty
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            Me.CheckBox1.Checked = False
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New AdjustmentTypeDAL().Add(Adj) Then
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

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New AdjustmentTypeDAL().Update(Adj) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If btnSave.Text = "&Save" Or btnSave.Text = "Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informUpdate)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_informDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Adj = New AdjustmentTypeBE
            Adj.AdjTypeId = AdjTypeId
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdDataHistory_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdDataHistory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDataHistory.DoubleClick
        Try
            AdjTypeId = Me.grdDataHistory.GetRow.Cells("AdjType_Id").Value
            Me.txtAdjustmentType.Text = Me.grdDataHistory.GetRow.Cells("AdjType").Value
            Me.txtSortOrder.Text = Val(Me.grdDataHistory.GetRow.Cells("Sort_Order").Value.ToString)
            Me.chkActive.Checked = Me.grdDataHistory.GetRow.Cells("Active").Value
            Me.CheckBox1.Checked = Me.grdDataHistory.GetRow.Cells("AdjustmentInShort").Value
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDataHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDataHistory.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
End Class