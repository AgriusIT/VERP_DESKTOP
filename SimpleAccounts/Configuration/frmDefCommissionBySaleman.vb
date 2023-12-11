Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal

Public Class frmDefCommissionBySaleman
    Implements IGeneral
    Dim comm As DefCommissionBE
    Dim cid As Integer = 0

    Private Sub frmDefCommissionBySaleman_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmDefCommissionBySaleman_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillCombos("vendor")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading the record" & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("SeqId").Visible = False
            Me.grd.RootTable.Columns("SaleManId").Visible = False
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
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
            comm = New DefCommissionBE
            comm.SeqId = Me.grd.GetRow.Cells("SeqId").Value
            If New DefCommissionDAL().Delete(comm) = True Then
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
            If Condition = "vendor" Then
                FillDropDown(Me.cmbVendor, "Select coa_detail_id,detail_title from vwCOADetail Where account_type = 'Customer'")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            comm = New DefCommissionBE
            comm.SeqId = cid
            comm.SalemanId = Me.cmbVendor.SelectedValue
            comm.Start_Value = Me.txtStartValue.Text.Replace("'", "''")
            comm.End_Value = Me.txtEndValue.Text.Replace("'", "''")
            comm.Percentage = Me.txtPercentage.Text.Replace("'", "''")
            comm.Sort_Order = Me.txtSortOrder.Text.Replace("'", "''")
            comm.Active = Me.chkActive.Checked

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grd.DataSource = New DefCommissionDAL().GetAllREcords()
            Me.grd.RetrieveStructure()

            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbVendor.SelectedIndex = 0 Then
                ShowErrorMessage("Please select a saleman")
                Me.cmbVendor.Focus()
                Return False
            End If

            If Me.txtStartValue.Text = String.Empty Then
                ShowErrorMessage("Please enter start value")
                Me.txtStartValue.Focus()
                Return False
            End If

            If Me.txtEndValue.Text = String.Empty Then
                ShowErrorMessage("Please enter end value")
                Me.txtEndValue.Focus()
                Return False
            End If

            If Me.txtPercentage.Text = String.Empty Then
                ShowErrorMessage("Please enter percentage")
                Me.txtPercentage.Focus()
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
            Me.cmbVendor.SelectedIndex = 0
            Me.txtStartValue.Text = String.Empty
            Me.txtEndValue.Text = String.Empty
            Me.txtPercentage.Text = String.Empty
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            Me.btnSave.Text = "&Save"
            GetAllRecords()
            Me.cmbVendor.Focus()
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New DefCommissionDAL().Save(comm) = True Then
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
            If New DefCommissionDAL().Update(comm) = True Then
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
            If Me.grd.RowCount = 0 Then Exit Sub
            cid = Me.grd.CurrentRow.Cells("SeqId").Value
            Me.cmbVendor.SelectedValue = Val(Me.grd.CurrentRow.Cells("SalemanId").Value.ToString)
            Me.txtStartValue.Text = Val(Me.grd.CurrentRow.Cells("Start_Value").Value.ToString)
            Me.txtEndValue.Text = Val(Me.grd.CurrentRow.Cells("End_Value").Value.ToString)
            Me.txtPercentage.Text = Val(Me.grd.CurrentRow.Cells("Percentage").Value)
            Me.txtSortOrder.Text = Me.grd.CurrentRow.Cells("Sort_Order").Value
            If Me.grd.CurrentRow.Cells("Active").Value = True Then
                Me.chkActive.Checked = True
            Else
                Me.chkActive.Checked = False
            End If
            'Me.chkActive.Checked = Me.grd.CurrentRow.Cells("").Value
            Me.btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record" & Chr(10) & ex.Message)
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
            ShowErrorMessage("An error occured while editing record" & Chr(10) & ex.Message)
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
                        'msg_Information(str_informUpdate)
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
            'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.grd.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informDelete)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage("An error occured while deleting record" & Chr(10) & ex.Message)
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
            Dim id As Integer = 0
            id = Me.cmbVendor.SelectedValue
            FillCombos("vendor")
            Me.cmbVendor.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim str As String = String.Empty
            str = ""
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

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