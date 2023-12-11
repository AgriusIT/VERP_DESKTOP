Imports SBDal
Imports SBModel
Public Class frmProInvestorDealer
    Implements IGeneral
    Dim objModel As PropertyProfileAgentDealerBE
    Dim objDAL As PropertyProfileAgentDealerDAL
    Dim VoucherNo As String = ""
    Dim blnEditMode As Boolean = False
    Public Shared PropertyProfileId As Integer = 0I
    Public Shared AgentID As Integer = 0I
    Public Shared DealerID As Integer = 0I
    Public Shared AgentName As String
    Public Shared DealerName As String
    Public Shared editAble As Boolean = False
    Public Shared editAgentDealer As Integer = 0
    Dim DetailAcccountId As Integer = 0
    Dim DetailAcccount As COADeail
    Public DetailAcccountDAL As COADetailDAL = New COADetailDAL()
    Public Shared PSID As Integer = 0I
    Public Shared PlotNo As String
    Dim ID As Integer = 0


    Private Sub frmProInvestorDealer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            checkedAgentDealerAccount()

            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

            ' Tooltip
            Ttip.SetToolTip(Me.rbtnDealers, "Check dealer")
            Ttip.SetToolTip(Me.rbtnAgents, "Check agent")
            Ttip.SetToolTip(Me.cmbDealers, "Select dealer")
            Ttip.SetToolTip(Me.txtRemarks, "Enter remarks")
            Ttip.SetToolTip(Me.txtCommission, "Enter commission")
            Ttip.SetToolTip(Me.cmbActivity, "Select activity")

            Ttip.SetToolTip(Me.btnCancel, "Click to close the window")
            Ttip.SetToolTip(Me.btnSave, "Click to save the data")


            If (editAble = True) Then
                ReSetControls()
                'GetByID(PropertyProfileId)
                GetByID(AgentID)
                Me.cmbDealers.Enabled = False
                editAgentDealer = 0


            Else
                ReSetControls()

                GetAllRecords()
                'Me.grd.Visible = False
                Me.cmbDealers.Enabled = True
            End If




        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetByID(AgentId As Integer)
        Try


            Dim dt As DataTable = New PropertyProfileAgentDealerDAL().GetById(AgentId)

            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("AgentId") > 0 Then
                ShowAgentData(dt)
            Else
                Dim dt2 As DataTable = New PropertyProfileAgentDealerDAL().GetByDealerId(AgentId)
                If dt2.Rows.Count > 0 AndAlso dt2.Rows(0).Item("DealerId") > 0 Then
                    'ShowAgentData(dt2)
                    ShowDealerData(dt2)
                    'Else
                    'ReSetControls()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetDocumentNo() As String
        Dim DocNo As String = String.Empty
        Try
            DocNo = GetNextDocNo("JV-" & Format(Date.Now, "yy") & Date.Now.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
            Return DocNo
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Function
    Private Sub ShowAgentData(ByVal dt As DataTable)
        Try
            'If Me.grd.GetRow.Cells("Type").Value.ToString = "Dealer" Then
            '    Me.rbtnDealers.Checked = True
            '    cmbDealers.SelectedValue = dt.Rows(0).Item("DealerId").ToString
            'Else
            '    Me.rbtnAgents.Checked = True
            '    cmbDealers.Text = dt.Rows(0).Item("AgentId").ToString
            'End If
            rbtnAgents.Checked = True
            'Me.cmbDealers = dt.Rows(0).Item("")
            'Me.cmbActivity.Checked = dt.Rows(0).Item("Activity")
            cmbDealers.Text = dt.Rows(0).Item("Name")
            Me.txtCommission.Text = dt.Rows(0).Item("CommissionAmount").ToString
            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
            VoucherNo = dt.Rows(0).Item("VoucherNo")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowDealerData(ByVal dt As DataTable)
        Try
            rbtnDealers.Checked = True
            'Me.cmbDealers = dt.Rows(0).Item("")
            'Me.cmbActivity.Checked = dt.Rows(0).Item("Activity")

            cmbDealers.Value = dt.Rows(0).Item("DealerId")
            'Me.cmbDealers.Text = dt.Rows(0).Item("AccountId").ToString
            Me.txtCommission.Text = dt.Rows(0).Item("CommissionAmount").ToString
            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
            VoucherNo = dt.Rows(0).Item("voucher_no")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("Id").Visible = False
            Me.grd.RootTable.Columns("PropertyProfileId").Visible = False
            Me.grd.RootTable.Columns("AccountId").Visible = False
            Me.grd.RootTable.Columns("VoucherNo").Visible = False
            Me.grd.RootTable.Columns("CommissionAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("CommissionAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CommissionAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CommissionAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CommissionAmount").TotalFormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnCancel.Enabled = True
                Exit Sub
            End If

            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.btnCancel.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If blnEditMode = False Then Me.btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If blnEditMode = True Then Me.btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Cancel" Then
                    Me.btnCancel.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New PropertyProfileAgentDealerDAL
            objDAL.Delete(objModel)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Agent" Then
                FillUltraDropDown(Me.cmbDealers, "SELECT AgentId, Name FROM Agent WHERE Active = 1 ORDER BY AgentId", True)
                UltraDropDownSearching(Me.cmbDealers, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                Me.cmbDealers.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Dealer" Then
                FillUltraDropDown(Me.cmbDealers, "SELECT DealerId, Name FROM Dealer WHERE (Active = 1) ORDER BY DealerId", True)
                UltraDropDownSearching(Me.cmbDealers, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                Me.cmbDealers.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Activity" Then
                FillDropDown(Me.cmbActivity, "SELECT DISTINCT(Activity), Activity FROM PropertyProfileAgent UNION ALL SELECT DISTINCT(Activity), Activity FROM PropertyProfileDealer", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecords()
        Try
            If Me.grd.GetRow.Cells("Type").Value.ToString = "Dealer" Then
                Me.rbtnDealers.Checked = True
            Else
                Me.rbtnAgents.Checked = True
            End If
            ID = Val(Me.grd.GetRow.Cells("Id").Value.ToString)
            Me.cmbDealers.Value = Val(Me.grd.GetRow.Cells("AccountId").Value.ToString)
            Me.txtRemarks.Text = Me.grd.GetRow.Cells("Remarks").Value.ToString
            Me.txtCommission.Text = Me.grd.GetRow.Cells("CommissionAmount").Value.ToString
            Me.cmbActivity.Text = Me.grd.GetRow.Cells("Activity").Value.ToString
            blnEditMode = True
            PSID = 1
            Me.cmbDealers.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New PropertyProfileAgentDealerBE
            objModel.PropertyProfileId = PropertyProfileId
            If Me.grd.RowCount > 0 Then
                If blnEditMode = True Then
                    If Me.rbtnDealers.Checked = True Then
                        If ID > 0 Then
                            objModel.PropertyProfileDealerId = ID 'Val(Me.grd.GetRow.Cells("Id").Value.ToString)
                            objModel.PropertyProfileAgentId = 0
                        Else
                            objModel.PropertyProfileDealerId = AgentID 'Val(Me.grd.GetRow.Cells("Id").Value.ToString)
                            objModel.PropertyProfileAgentId = 0
                        End If

                    Else
                        If ID > 0 Then
                            objModel.PropertyProfileAgentId = ID
                            objModel.PropertyProfileDealerId = 0
                        Else
                            objModel.PropertyProfileAgentId = AgentID
                            objModel.PropertyProfileDealerId = 0
                        End If
                        End If
                End If
            End If
            If Me.rbtnDealers.Checked = True Then
                objModel.DealerId = Me.cmbDealers.Value
                objModel.AgentId = 0
            Else
                objModel.AgentId = Me.cmbDealers.Value
                objModel.DealerId = 0
            End If
            ''Below four lines are commented against TASK TFS3672 on 26-06-2018
            'If PSID = 0 Then
            '    objModel.VoucherNo = GetDocumentNo()
            'Else
            '    objModel.VoucherNo = VoucherNo
            'End If
            objModel.VoucherNo = String.Empty
            objModel.PlotNo = PlotNo
            objModel.name = Me.cmbDealers.Text
            objModel.Remarks = Me.txtRemarks.Text
            objModel.CommissionAmount = Me.txtCommission.Text
            objModel.CommissionAccount = getConfigValueByType("CommissionAccount").ToString
            objModel.Activity = Me.cmbActivity.Text
            objModel.ActivityLog = New ActivityLog
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New PropertyProfileAgentDealerDAL
            Me.grd.DataSource = objDAL.GetAll(PropertyProfileId)
            Me.grd.RetrieveStructure()
            'If (Me.grd.GetRow.Cells("Type").Value.ToString) = "Dealer" Then
            '    DealerName = Me.grd.RootTable.Columns("Name").ToString
            'Else
            '    AgentName = Me.grd.RootTable.Columns("Name").ToString
            'End If
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ValidateDealer()

        If Me.grd.RowCount > 0 Then
            If blnEditMode = True Then
                Return True
            End If
            Dim i As Integer = 0
            For i = 0 To Me.grd.RowCount - 1
                If Me.grd.GetRow.Cells("Type").Value.ToString = "Dealer" And (cmbDealers.Value = Me.grd.GetRow.Cells("AccountId").Value.ToString) Then
                    ShowErrorMessage("This Dealer Already Exists")
                    Me.cmbDealers.Focus()
                    Return False
                End If
            Next
            Return True
        End If
        Return True
    End Function
    Public Function ValidateAgent()
        'Dim strSQL As String
        'Dim Dt As DataTable
        If Me.grd.RowCount > 0 Then
            If blnEditMode = True Then
                Return True
            End If
            'strSQL = "select DealerId from PropertyProfileDealer where PropertyProfileId = " & PropertyProfileId & ""
            'Dt = GetDataTable(strSQL)
            Dim i As Integer = 0
            For i = 0 To Me.grd.RowCount - 1
                If Me.grd.GetRow.Cells("Type").Value.ToString = "Agent" And (cmbDealers.Value = Me.grd.GetRow.Cells("AccountId").Value.ToString) Then
                    ShowErrorMessage("This Agent Already Exists")
                    Me.cmbDealers.Focus()
                    Return False
                End If
            Next
            Return True
        End If
        Return True
    End Function

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbDealers.Value = 0 Then
                msg_Error("Please Select any Account")
                Return False
            End If

            If Me.rbtnDealers.Checked = True Then
                If (ValidateDealer() = False) Then
                    Exit Function
                End If
            Else
                If (ValidateAgent() = False) Then
                    Exit Function
                End If
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.rbtnAgents.Checked = True
            Me.rbtnAgents.Focus()
            FillCombos("Agent")
            FillCombos("Activity")
            Me.cmbDealers.Value = 0
            Me.txtRemarks.Text = ""
            Me.txtCommission.Text = 0
            Me.cmbActivity.SelectedValue = 0

            If (editAble = -1) Then
                blnEditMode = True
                GetAllRecords()
            Else
                blnEditMode = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New PropertyProfileAgentDealerDAL
            If IsValidate() = True Then
                objDAL.Add(objModel)
                Return True
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
        Try
            objDAL = New PropertyProfileAgentDealerDAL
            If IsValidate() = True Then
                objDAL.Update(objModel)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Me.grd.UpdateData()
            If blnEditMode = False Then
                If Save() = True Then
                    ReSetControls()
                    GetAllRecords()
                    frmPropertyProfile.GetAgentDealers()
                    msg_Information(str_informSave)
                    'Me.Close()
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Update1()
                ReSetControls()
                'AgentID = 0
                'PropertyProfileId = 0
                GetAllRecords()
                frmPropertyProfile.GetAgentDealers()

                blnEditMode = False
                msg_Information(str_informUpdate)
                'Me.Close()
            End If
            Me.DialogResult = Windows.Forms.DialogResult.None
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ReSetControls()
            blnEditMode = False
            editAble = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtnDealers_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDealers.CheckedChanged, rbtnAgents.CheckedChanged
        Try
            checkedAgentDealerAccount()
            If Me.rbtnDealers.Checked = True Then
                FillCombos("Dealer")
                lblAccount.Text = "Dealers"
            Else
                FillCombos("Agent")
                lblAccount.Text = "Agents"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub checkedAgentDealerAccount()

        If rbtnDealers.Checked = True Then

            Dim AccountSubSubId As Integer = Val(getConfigValueByType("DealerSubSub").ToString)

            If AccountSubSubId <= 0 Then

                ShowErrorMessage("Please Select a sub sub Account to map Against the Dealer")

            End If

        Else

            Dim AgentAccountId = CInt(getConfigValueByType("AgentSubSub"))

            If AgentAccountId <= 0 Then
                ShowErrorMessage("Please Select a sub sub Account to map Against the Agent")
            End If

        End If

    End Sub

    Private Sub frmProInvestorDealer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
            'Else
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            'End If

        Catch ex As Exception

        End Try
    End Sub
End Class