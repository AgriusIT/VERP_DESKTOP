''24-Feb-2014 TASK:M22 Imran Ali Leave Wise Salary Calculation
Imports System
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal

Public Class frmDefPayRollDivision

    Implements IGeneral
    Dim pid As Integer = 0
    Dim payroll As PayRollDivisionBE

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmDefPayRollDivision_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 11 -1-14
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

    Private Sub frmDefPayRollDivision_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos("division")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading the record" & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'Me.chkPost.Visible = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        'Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New PayRollpayisionDAL().Delete_Record(payroll) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "division" Then
                FillDropDown(cmbDivision, "Select Division_Id, Division_Name from tblDefDivision")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            payroll = New PayRollDivisionBE
            payroll.PayRollDivision_Id = pid
            payroll.Division_Id = Me.cmbDivision.SelectedValue
            payroll.PayRollDivisionName = Me.txtPayRollName.Text
            payroll.Level = Me.Level.Value
            payroll.Sort_Order = Me.txtSortOrder.Text
            payroll.Active = Me.chkActive.Checked
            payroll.AnnualAllowedLeave = Val(Me.txtAnnualAllowedLeave.Text) ' Task:M22 Set Annual Allowed Leave Value.

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.DataGridView1.DataSource = New PayRollpayisionDAL().GetAll()
            Me.DataGridView1.RetrieveStructure()
            Me.DataGridView1.RootTable.Columns("PayRollDivision_Id").Visible = False
            Me.DataGridView1.RootTable.Columns("Division_Id").Visible = False
            Me.DataGridView1.RootTable.Columns("Division_Name").Caption = "Division Name"
            Me.DataGridView1.RootTable.Columns("PayRollDivisionName").Caption = "Pay Roll Name"
            Me.DataGridView1.RootTable.Columns("AnnualAllowedLeave").Caption = "Leave" 'Task:M22 Field Format.
            Me.DataGridView1.RootTable.Columns("Sort_Order").Caption = "Sort Order"
            Me.DataGridView1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbDivision.SelectedIndex = 0 Then
                ShowErrorMessage("Please select a division")
                Me.cmbDivision.Focus()
                Return False
            End If

            If Me.txtPayRollName.Text = String.Empty Then
                ShowErrorMessage("Please enter a payroll name")
                Me.txtPayRollName.Focus()
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
            If Not Me.cmbDivision.SelectedIndex = -1 Then Me.cmbDivision.SelectedIndex = 0
            Me.txtPayRollName.Text = String.Empty
            Me.Level.Value = 1
            Me.txtAnnualAllowedLeave.Text = 0 'Task:M22 Reset Annual Allowed Leave.
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            GetAllRecords()
            Me.btnSave.Text = "&Save"
            Me.cmbDivision.Focus()
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New PayRollpayisionDAL().Save(payroll) = True Then
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
            If New PayRollpayisionDAL().Update(payroll) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub EditRecords()
        Try
            payroll = New PayRollDivisionBE
            pid = Me.DataGridView1.CurrentRow.Cells("PayRollDivision_Id").Value
            Me.cmbDivision.SelectedValue = Me.DataGridView1.CurrentRow.Cells("Division_Id").Value
            Me.txtPayRollName.Text = Me.DataGridView1.CurrentRow.Cells("PayRollDivisionName").Value
            Me.Level.Value = Me.DataGridView1.CurrentRow.Cells("Level").Value
            Me.txtSortOrder.Text = Me.DataGridView1.CurrentRow.Cells("Sort_Order").Value
            Me.txtAnnualAllowedLeave.Text = Val(Me.DataGridView1.CurrentRow.Cells("AnnualAllowedLeave").Value.ToString) ' Task:M22 Get Annual Allowed Leave Value.
            If Me.DataGridView1.CurrentRow.Cells("Active").Value.ToString = "True" Then
                Me.chkActive.Checked = True
            Else
                Me.chkActive.Checked = False
            End If
            Me.btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
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

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        ' msg_Information(str_informSave)
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information(str_informUpdate)
                        ReSetControls()
                    End If
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
            If Me.DataGridView1.RowCount = 0 Then Exit Sub
            payroll = New PayRollDivisionBE
            payroll.PayRollDivision_Id = Me.DataGridView1.CurrentRow.Cells("PayRollDivision_Id").Value
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informDelete)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim id As Integer
            id = Me.cmbDivision.SelectedValue
            FillCombos("division")
            Me.cmbDivision.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14
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