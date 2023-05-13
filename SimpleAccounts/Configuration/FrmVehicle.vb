Imports SBModel
Imports SBDal
Imports SBUtility
Public Class FrmVehicle
    Implements IGeneral
    Dim Vehi As Vehicle_Log
    Dim log_id As Integer


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
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
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
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub



    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If IsValidate() = True Then
                If New Vehicle_LogDAL().Delete(Vehi) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(cmbVehicles, New Vehicle_LogDAL().strvehicle())
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Vehi = New Vehicle_Log
            Vehi.LogId = log_id
            Vehi.LogDate = Me.dtpDate.Value
            Vehi.Vehicle_Id = Me.cmbVehicles.SelectedValue
            Vehi.Person = Me.txtPerson.Text.ToString.Replace("'", "''")
            Vehi.Purpose = Me.txtDescription.Text.ToString.Replace("'", "''")
            Vehi.Previous = Me.txtPrevious.Text.ToString.Replace("'", "''")
            Vehi.Current = Me.txtCurrent.Text.ToString.Replace("'", "''")
            Vehi.EntryNo = Me.TextBox1.Text.ToString.Replace("'", "''")
            Vehi.EntryDateTime = Date.Now
            Vehi.UserName = LoginUserName
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = New Vehicle_LogDAL().GetAllRecords
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("LogId").Visible = False
            Me.grdSaved.RootTable.Columns("Vehicle_ID").Visible = False
            Me.grdSaved.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = String.Empty
            CtrlGrdBar1.Email.Subject = "Vehicle Log " & Date.Now.ToString("ddMMyyyy") & ""
            CtrlGrdBar1.Email.DocumentNo = String.Empty
            CtrlGrdBar1.Email.DocumentDate = Date.Now

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbVehicles.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Vehicle")
                Me.cmbVehicles.Focus()
                Return False
            End If

            If Me.txtPerson.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Person Name")
                Me.txtPerson.Focus()
                Return False
            End If

            If Me.txtCurrent.Text = String.Empty Then
                ShowErrorMessage("Enter The Current Meter Reading")
                Me.txtCurrent.Focus()
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
            Me.btnSave.Text = "&Save"
            log_id = 0
            Me.dtpDate.Value = Date.Now
            Me.cmbVehicles.SelectedIndex = 0
            Me.txtPerson.Text = String.Empty
            Me.txtDescription.Text = String.Empty
            Me.txtPrevious.Text = String.Empty
            Me.txtCurrent.Text = String.Empty
            Me.txtUsage.Text = String.Empty
            Me.TextBox1.Text = Vehicle_LogDAL.GetDocumentNo
            GetAllRecords()
            ApplySecurity(Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                If New Vehicle_LogDAL().Save(Vehi) = True Then
                    Return True
                Else
                    Return False
                End If
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
            If IsValidate() = True Then
                If New Vehicle_LogDAL().Update(Vehi) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Save" Then
                If Not msg_Confirm(str_ConfirmSave) = True Then
                    Exit Sub
                End If
                If Save() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    ReSetControls()
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then
                    Exit Sub
                End If
                If Update1() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informUpdate)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then
                Exit Sub
            End If
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                msg_Confirm(str_ConfirmDelete)
                ReSetControls()
            End If
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
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click, grdSaved.DoubleClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            log_id = grdSaved.GetRow.Cells("LogId").Value
            dtpDate.Value = grdSaved.GetRow.Cells("LogDate").Value
            cmbVehicles.SelectedValue = grdSaved.GetRow.Cells("Vehicle_Id").Value
            txtPerson.Text = grdSaved.GetRow.Cells("Person").Value
            txtDescription.Text = grdSaved.GetRow.Cells("Purpose").Value
            txtPrevious.Text = grdSaved.GetRow.Cells("Previouse").Value
            txtCurrent.Text = grdSaved.GetRow.Cells("Current").Value
            'txtUsage.Text = grdSaved.GetRow.Cells("Usage").Value
            Me.TextBox1.Text = Me.grdSaved.GetRow.Cells("EntryNo").Value.ToString
            btnSave.Text = "&Update"
            ApplySecurity(Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FrmVehicle_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

    
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub txtPrevious_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrevious.TextChanged, txtCurrent.TextChanged
        Try
            txtUsage.Text = Val(txtCurrent.Text) - Val(txtPrevious.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click


 
        Try
            Dim id As Integer = 0
            id = Me.cmbVehicles.SelectedValue
            FillCombos()
            Me.cmbVehicles.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)


        End Try
    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class