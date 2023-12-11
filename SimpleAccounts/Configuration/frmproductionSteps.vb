Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDAL

Public Class frmproductionSteps
    Implements IGeneral
    Dim Production As proStepBE
    Dim int As Integer = 0I
    Dim ProdId As Integer = 0I
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            grdSaved.RootTable.Columns("ProdStep_Id").Visible = False
            Me.grdSaved.RootTable.Columns("prod_step").Caption = "Production Step"
            Me.grdSaved.RootTable.Columns("Prod_Less").Caption = "Less"
            Me.grdSaved.RootTable.Columns("sort_Order").Caption = "Sort"
            Me.grdSaved.RootTable.Columns("QCVerificationRequired").Caption = "QC Verification"
            Me.grdSaved.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            Production = New proStepBE
            Production.ProdStep_Id = Me.grdSaved.GetRow.Cells("ProdStep_Id").Value
            If proStepDAL.IsStepConsumed(Production.ProdStep_Id) = True Then
                ShowErrorMessage("You can not delete. This prodcution step is existed at production process.")
                Exit Function
            End If
            If New proStepDAL().Delete(Production) Then
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
            Production = New proStepBE
            Production.ProdStep_Id = ProdId
            Production.Prod_Step = Me.txtStepName.Text.Replace("'", "''")
            Production.Prod_Less = Me.chkProdLess.Checked
            Production.Sort_order = Val(Me.txtSortOrder.Text)
            Production.Active = Me.chkActive.Checked
            Production.QCVerificationRequired = Me.cbQCVerificationRequired.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim dt As New DataTable
            dt = New proStepDAL().getAll
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtStepName.Text = String.Empty Then
                ShowErrorMessage("Please enter production step name")
                Me.txtStepName.Focus()
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
            ProdId = 0
            Me.btnSave.Text = "&Save"
            Me.txtStepName.Text = String.Empty
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            Me.cbQCVerificationRequired.Checked = False
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New proStepDAL().Save(Production) Then
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
            If New proStepDAL().Update(Production) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmproductionSteps_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
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

    Private Sub frmproductionSteps_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Me.txtStepName.Focus()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Me.grdSaved.RowCount = 0 Then Exit Sub



            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            'If proStepDAL.IsStepConsumed
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ' msg_Information(str_informSave)
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

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
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ProdId = Me.grdSaved.GetRow.Cells("ProdStep_Id").Value
            Me.txtStepName.Text = Me.grdSaved.GetRow.Cells("Prod_Step").Value.ToString
            Me.txtSortOrder.Text = Me.grdSaved.GetRow.Cells("Sort_Order").Value
            Me.chkActive.Checked = CBool(Me.grdSaved.GetRow.Cells("Active").Value)
            Me.cbQCVerificationRequired.Checked = CBool(Me.grdSaved.GetRow.Cells("QCVerificationRequired").Value)
            Me.btnSave.Text = "&Update"
            Me.txtStepName.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdSaved_DoubleClick(Nothing, Nothing)
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

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub

    Private Sub btnHeader_Click(sender As Object, e As EventArgs) Handles btnHeader.Click

    End Sub
End Class