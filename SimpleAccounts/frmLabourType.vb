''TASK TFS2116 NEW FORM Labour type 
Imports SBDal
Imports SBModel

Class frmLabourType
    Implements IGeneral

    Dim MasterId As Integer = 0
    Private Sub frmLabourType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
            GetAllRecords()
            FillCombos()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Me.cmbChargeType.DisplayMember = "Charge"
            Me.cmbChargeType.ValueMember = "Id"
            Dim strQuery As String = String.Empty
            strQuery = " SELECT Id, Charge from ChargeType  "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            Me.cmbChargeType.DataSource = dt
            FillDropDown(Me.cmbAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code] from vwCOADetail where main_type in ('Assets','Expense', 'Liability')  and detail_title <> ''")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    Public Function FillModel() As LabourTypeBE
        Try
            Dim obj As LabourTypeBE = New LabourTypeBE ' Create new Object Returnable GatePass
            obj.Id = MasterId
            obj.Active = chActive.Checked
            obj.ChargeTypeId = cmbChargeType.SelectedValue ' Set Value of Issue No in Object
            obj.LabourType = txtLabourType.Text
            obj.Remarks = txtRemarks.Text
            obj.SortOrder = Convert.ToInt32(txtSortOrder.Text)
            obj.AccountId = Me.cmbAccount.SelectedValue
            Return obj
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Fill Grid Master Data
            Me.grdSaved.DataSource = New LabourTypeDAL().GetAllRecords

            'Me.grdSaved.RetrieveStructure()
            'Me.grdSaved.RootTable.Columns("Id").Visible = False
            'Me.grdSaved.RootTable.Columns("LabourType").Caption = "Labour Type"
            'Me.grdSaved.RootTable.Columns("ChargeTypeId").Caption = ""
            'Me.grdSaved.RootTable.Columns("ChargeTypeId").Visible = False
            'Me.grdSaved.RootTable.Columns("Remarks").Caption = "Reference"
            'Me.grdSaved.RootTable.Columns("SortOrder").Caption = ""
            'Me.grdSaved.RootTable.Columns("SortOrder").Visible = False
            'Me.grdSaved.RootTable.Columns("Active").Caption = "Reference"
            'Me.grdSaved.RootTable.Columns("Active").Visible = False
            'Me.grdSaved.RootTable.Columns("Issue_Date").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        If (Me.txtLabourType.Text.Trim.ToString() = "") Then
            ShowErrorMessage("Labour type is required.")
            Me.txtLabourType.Focus()
            Return False

            'Task 3420 Saad Afzaal require validation for Account'

        ElseIf cmbAccount.SelectedValue <= 0 Then
            ShowErrorMessage("Please Select Account.")
            Me.cmbAccount.Focus()
            Return False

        End If
        'If Me.cmbAccount.SelectedValue < 1 Then
        '    ShowErrorMessage("Account is required.")
        '    Me.cmbAccount.Focus()
        '    Return False
        'End If
        Return True
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        GetSecurityRights()
        txtLabourType.Text = ""
        txtRemarks.Text = ""
        txtSortOrder.Text = 1
        Me.BtnSave.Text = "&Save"
        If Not Me.cmbAccount.SelectedIndex = -1 Then
            Me.cmbAccount.SelectedIndex = 0
        End If
        chActive.Checked = True
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If (IsValidate() = False) Then Exit Function

            Dim list As LabourTypeBE = FillModel()
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                If New LabourTypeDAL().save(list) Then
                    SaveActivityLog("Production", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtLabourType.Text, True)
                    Return True
                Else
                    Return False
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Function
                If New LabourTypeDAL().Update(list) Then
                    SaveActivityLog("Production", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtLabourType.Text, True)
                    Return True
                Else
                    Return False
                End If
            End If
            'If (objDAL.Add(obj)) Then
            '    msg_Information("Record Successfully Added !")
            'End If
        Catch ex As Exception
            msg_Error(ex.Message)
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

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            'If Me.grdSaved.RowCount > 0 Then
            '    If Not msg_Confirm("Do you want to clear existing data?") = True Then
            '        Exit Sub
            '    End If
            'End If
            ReSetControls()
            GetAllRecords()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            Save()
            ReSetControls()
            GetAllRecords()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdSaved_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdSaved.RowDoubleClick

    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            'If flgDateLock = True Then
            '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpdcdate.Value.ToString("yyyy-M-d 00:00:00") Then
            '        'ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '        Me.dtpdcdate.Enabled = False
            '    Else
            '        Me.dtpdcdate.Enabled = True
            '    End If
            'Else
            '    Me.dtpdcdate.Enabled = True
            'End If

            'select tblLabourType.Id, dbo.tblLabourType.LabourType, ChargeType.Charge, ISNULL(dbo.tblLabourType.AccountId, 0) AS AccountId, Account.detail_title AS Account, dbo.tblLabourType.Remarks, dbo.tblLabourType.SortOrder, Convert(bit, IsNull(dbo.tblLabourType.Active, 0)) AS Active , ChargeType.Id as chId, ISNULL(dbo.tblLabourType.AccountId, 0) AS AccountId, Account.detail_title AS Account 


            MasterId = Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString)
            Me.txtLabourType.Text = Me.grdSaved.GetRow.Cells("LabourType").Value.ToString()

            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString()
            Me.txtSortOrder.Text = Val(Me.grdSaved.GetRow.Cells("SortOrder").Value.ToString)
            chActive.Checked = Me.grdSaved.GetRow.Cells("Active").Value
            cmbChargeType.SelectedValue = Val(Me.grdSaved.GetRow.Cells("chId").Value.ToString)
            cmbAccount.SelectedValue = Val(Me.grdSaved.GetRow.Cells("AccountId").Value.ToString)
            ''END TASKT TFS1556
            Me.BtnSave.Text = "&Update"

            'GrdDataTable = New PartialInMasterDAL().GetRecordById(MasterId)
            'Me.grd.DataSource = GrdDataTable

            'Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            'Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString
            ' ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            'Me.BtnPrint.Visible = True

            ' '''''''''''''''''''''''''''
            'Dim objDal As New ReturnAbleGatePassDAL()
            'If objDal.CheckReturnTaken(MasterId) = True Then
            '    Me.BtnDelete.Visible = False
            '    Me.BtnSave.Enabled = False
            'Else
            '    Me.BtnDelete.Visible = True
            '    GetSecurityRights()

            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If msg_Confirm("Do you want to delete record?") = False Then Exit Sub
        If New LabourTypeDAL().Delete(Me.grdSaved.GetRow.Cells(0).Value) Then
            msg_Information("Record has been deleted successfully.")
            ReSetControls()
            GetAllRecords()
        End If
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True

                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.btnSave.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        Me.btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        Me.btnEdit.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class