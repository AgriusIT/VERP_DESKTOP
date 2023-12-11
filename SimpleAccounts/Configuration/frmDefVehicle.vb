Imports SBModel
Imports SBDal
Imports SBUtility


Public Class frmDefVehicle
    Implements IGeneral
    Dim Veh As Vehicle
    Dim V_id As Integer

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
                If New VehicleDAL().Delete(Veh) = True Then
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

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Veh = New Vehicle
            Veh.Vehicle_ID = V_id
            Veh.Vehicle_Name = Me.txtvname.Text.ToString.Replace("'", "''")
            Veh.Vehicle_Type = Me.Cmbvtype.Text.ToString.Replace("'", "''")
            Veh.Engine_No = Me.txtengineno.Text.ToString.Replace("'", "''")
            Veh.Chassis_No = Me.txtchassisno.Text.ToString.Replace("'", "''")
            Veh.Registration_No = Me.txtregno.Text.ToString.Replace("'", "''")
            Veh.Maker = Me.txtmaker.Text.ToString.Replace("'", "''")
            Veh.Model = Me.txtmodel.Text.ToString.Replace("'", "''")
            Veh.Color = Me.txtcolor.Text.ToString.Replace("'", "''")
            Veh.Power = Me.txtpower.Text.ToString.Replace("'", "''")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.DataGridView1.DataSource = New VehicleDAL().GetAllRecords
            Me.DataGridView1.RetrieveStructure()
            Me.DataGridView1.RootTable.Columns("Vehicle_ID").Visible = False
            Me.DataGridView1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtvname.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Vehicle Name ")
                Me.txtvname.Focus()
                Return False
            End If

            If Me.Cmbvtype.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Vehicle Type")
                Me.Cmbvtype.Focus()
                Return False
            End If

            If Me.txtchassisno.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Chassis NO")
                Me.txtchassisno.Focus()
                Return False
            End If

            If Me.txtregno.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Registration NO")
                Me.txtregno.Focus()
                Return False
            End If

            If Me.txtmodel.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Vehicle Model")
                Me.txtmodel.Focus()
                Return False
            End If

            If Me.txtpower.Text = String.Empty Then
                ShowErrorMessage("Please Enter The Vehicle Model")
                Me.txtpower.Focus()
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
            Me.txtvname.Text = String.Empty
            Me.Cmbvtype.Text = String.Empty
            Me.txtengineno.Text = String.Empty
            Me.txtchassisno.Text = String.Empty
            Me.txtregno.Text = String.Empty
            Me.txtmaker.Text = String.Empty
            Me.txtmodel.Text = String.Empty
            Me.txtcolor.Text = String.Empty
            Me.txtpower.Text = String.Empty
            Me.btnSave.Text = "&Save"
            GetAllRecords()
            ApplySecurity(Utility.EnumDataMode.New)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                If New VehicleDAL().Save(Veh) = True Then
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
                If New VehicleDAL().Update(Veh) = True Then
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

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then


                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                'If Not msg_Confirm(str_ConfirmSave) = True Then
                'Exit Sub
                'End If
                If Save() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_ConfirmSave)
                    ReSetControls()
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then
                    Exit Sub
                End If


                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Update1() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
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

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then
                Exit Sub
            End If


            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Confirm(str_ConfirmDelete)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btn_Edit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click, DataGridView1.DoubleClick
        Try
            If Me.DataGridView1.RowCount = 0 Then Exit Sub
            V_id = DataGridView1.GetRow.Cells("Vehicle_ID").Value
            txtvname.Text = DataGridView1.GetRow.Cells("Vehicle_Name").Value
            Cmbvtype.Text = DataGridView1.GetRow.Cells(2).Value
            txtengineno.Text = DataGridView1.GetRow.Cells("Engine_No").Value
            txtchassisno.Text = DataGridView1.GetRow.Cells("Chassis_No").Value
            txtregno.Text = DataGridView1.GetRow.Cells("Registartion_No").Value
            txtmaker.Text = DataGridView1.GetRow.Cells("Maker").Value
            txtmodel.Text = DataGridView1.GetRow.Cells("Model").Value
            txtcolor.Text = DataGridView1.GetRow.Cells("color").Value
            txtpower.Text = DataGridView1.GetRow.Cells("Power").Value
            btnSave.Text = "&Update"
            ApplySecurity(Utility.EnumDataMode.Edit)
            Me.txtvname.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefVehicle_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmDefVehicle_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

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

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            btn_Edit(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            CutToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub btnHeader_Click(sender As Object, e As EventArgs) Handles btnHeader.Click

    End Sub
End Class