''01-Mar-2014 Task:2449 Imran   Cost Center New Record Not Create 
''14-Mar-2014 TASK:2490 Imran Ali New Bug In Release 2.1.1.6
Public Class frmCostCenter
    Implements IGeneral

    Dim IsLoadedForm As Boolean = False
    Enum CostCenter
        CostCenterID
        Name
        Code
        SortOrder
        CostCenterGroup
        Active
        OutwardGatepass
        DayShift
        Logical
        Amount
        SalariedBudget
        DepartmentBudget
        SOBudget
        Contract
        PurchaseDemand
        RemainingAmount
    End Enum
    Private Sub GrdCostCenter_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdCostCenter.DoubleClick
        EditRecord()
    End Sub
    Private Sub costcenterResetcontrol()
        txtID.Text = ""
        TxtName.Text = ""
        TxtCode.Text = ""
        txtAmount.Text = "0"
        txtRemainingAmount.Text = "0"
        Me.GetRecored()
        FillDropDown(Me.cmbHead, "select DISTINCT 0, CostCenterGroup From tblDefCostCenter WHERE CostCenterGroup Is Not Null", False)
        cmbHead.Text = String.Empty
        Me.chkActive.Checked = True
        Me.chkOuwardgatepass.Checked = False
        Me.chkSalariedBudget.Checked = False
        Me.chkDepartmentBudget.Checked = False
        Me.chkSOBudget.Checked = False
        Me.chkContract.Checked = False
        Me.chkPurchaseDemand.Checked = False
        TxtSortOrder.Text = 1
        BtnSave.Text = "&Save"
        Me.chkShift.Checked = False
        TxtName.Focus()
        Call GetSecurityRights()
    End Sub
    Private Sub GetRecored()
        Try
            Me.GrdCostCenter.DataSource = SBDal.UtilityDAL.GetDataTable("SELECT  CostCenterID, Name, Code, SortOrder, CostCenterGroup, Active, Isnull(OutwardGatepass,0) as OutwardGatepass, isnull(DayShift,0) as DayShift, IsNull(IsLogical, 0) AS Logical, Isnull(Amount,0) as Amount, Isnull(SalaryBudget,0) as SalaryBudget, Isnull(DepartmentBudget,0) as DepartmentBudget, Isnull(SOBudget,0) as SOBudget, Isnull(Contract,0) as Contract, Isnull(PurchaseDemand,0) as PurchaseDemand, Isnull(CommissionBudget,0) as CommissionBudget  FROM dbo.tblDefCostCenter order by sortorder")
            Me.GrdCostCenter.RetrieveStructure()
            Me.GrdCostCenter.RootTable.Columns(CostCenter.CostCenterID).Visible = False
            Me.GrdCostCenter.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.GrdCostCenter.RootTable.Columns(CostCenter.CostCenterGroup).Caption = "Group"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CostcenterDelet()
        Try
            'If MsgBox("Are you sure you want to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim cm As New OleDb.OleDbCommand
            cm.Connection = Con
            If Not Con.State = ConnectionState.Open Then Con.Open()
            Dim da As New OleDb.OleDbDataAdapter
            Dim dt As New DataTable
            cm.CommandText = ""
            'Before against task:2490
            'cm.CommandText = "Select * From tblVoucherDetail WHERE CostCenterID=" & Val(txtID.Text)
            'Task:2490 Get Value Through Grid.
            cm.CommandText = "Select * From tblVoucherDetail WHERE CostCenterID=" & Val(GrdCostCenter.GetRow.Cells(CostCenter.CostCenterID).Value.ToString)
            'End Task:2490
            ''14-Mar-2014 TASK:2490 Imran Ali New Bug In Release 2.1.1.6
            da.SelectCommand = cm
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                msg_Error(str_ErrorDependentRecordFound)
                Me.TxtName.Focus()
                Exit Sub
            End If

            'Dim cn As New OleDb.OleDbConnection(My.Settings.Database1ConnectionString)
            cm.CommandText = ""
            'Before against task:2490
            'cm.CommandText = "delete from tblDefCostCenter where CostcenterID = " & Val(txtID.Text)
            'Task:2490 Get Value Through Grid.
            cm.CommandText = "delete from tblDefCostCenter where CostcenterID = " & Val(GrdCostCenter.GetRow.Cells(CostCenter.CostCenterID).Value.ToString)
            'End Task:2490
            cm.ExecuteNonQuery()
            'msg_Information(str_informDelete)
            Con.Close()
            costcenterResetcontrol()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            If Con.State = ConnectionState.Open Then Con.Close()
        End Try
    End Sub
    Private Sub costcenterSave()

        Try
            'If MsgBox("Are you sure you want to Save?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            'If Not BtnSave.Text = "Save" And Not BtnSave.Text = "&Save" Then '''01-Mar-2014 Task:2449 Cost Center New Record Not Create 
            'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub '''01-Mar-2014 Task:2449 Cost Center New Record Not Create 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim cm As New OleDb.OleDbCommand
            '   Dim con As New OleDb.OleDbConnection(con.ConnectionString)
            cm.CommandText = "insert into tblDefCostCenter(Name,Code,sortorder, CostCenterGroup, Active, OutwardGatepass, DayShift, IsLogical, Amount, SOBudget, SalaryBudget, DepartmentBudget, Contract, PurchaseDemand, RemainingAmount) values(N'" & TxtName.Text.Replace("'", "''") & "','" & TxtCode.Text.Replace("'", "''") & "','" & TxtSortOrder.Text & "', N'" & Me.cmbHead.Text.Replace("'", "''") & "', " & IIf(Me.chkActive.Checked = True, 1, 0) & ", " & IIf(Me.chkOuwardgatepass.Checked = True, 1, 0) & ", " & IIf(Me.chkShift.Checked = True, 1, 0) & ", " & IIf(Me.cbLogical.Checked = True, 1, 0) & ",'" & txtAmount.Text.Replace("'", "''") & "', " & IIf(Me.chkSOBudget.Checked = True, 1, 0) & ", " & IIf(Me.chkSalariedBudget.Checked = True, 1, 0) & ", " & IIf(Me.chkDepartmentBudget.Checked = True, 1, 0) & ", " & IIf(Me.chkContract.Checked = True, 1, 0) & ", " & IIf(Me.chkPurchaseDemand.Checked = True, 1, 0) & ",'" & txtRemainingAmount.Text.Replace("'", "''") & "')"
            cm.Connection = Con
            If Not Con.State = ConnectionState.Open Then Con.Open()
            cm.ExecuteNonQuery()

            'msg_Information(str_informSave)
            Con.Close()
            costcenterResetcontrol()
            SaveActivityLog("Accounts", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.AccountTransaction, Me.txtID.Text.Trim, True)
            'End If '''01-Mar-2014 Task:2449 Cost Center New Record Not Create s
        Catch ex As Exception

            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            If Con.State = ConnectionState.Open Then Con.Close()

        End Try
    End Sub
    Private Sub costcenterUpdate()
        Try
            'If MsgBox("Are you sure you want to Update?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim cm As New OleDb.OleDbCommand
            'Dim cn As New OleDb.OleDbConnection(My.Settings.Database1ConnectionString)
            cm.CommandText = " update tblDefCostCenter set  name=N'" & TxtName.Text.Replace("'", "''") & "', code= '" & TxtCode.Text.Replace("'", "''") & "', sortorder= '" & TxtSortOrder.Text & "', CostCenterGroup =N'" & Me.cmbHead.Text.Replace("'", "''") & "', Active=" & IIf(Me.chkActive.Checked = True, 1, 0) & ", OutwardGatepass=" & IIf(Me.chkOuwardgatepass.Checked = True, 1, 0) & ", DayShift=" & IIf(Me.chkShift.Checked = True, 1, 0) & ", IsLogical=" & IIf(Me.cbLogical.Checked = True, 1, 0) & ", Amount= '" & txtAmount.Text.Replace("'", "''") & "', SoBudget = " & IIf(Me.chkSOBudget.Checked = True, 1, 0) & ", SalaryBudget = " & IIf(Me.chkSalariedBudget.Checked = True, 1, 0) & ", DepartmentBudget = " & IIf(Me.chkDepartmentBudget.Checked = True, 1, 0) & ", Contract = " & IIf(Me.chkContract.Checked = True, 1, 0) & ", PurchaseDemand = " & IIf(Me.chkPurchaseDemand.Checked = True, 1, 0) & ", RemainingAmount= '" & txtRemainingAmount.Text.Replace("'", "''") & "' where costcenterid = " & txtID.Text
            cm.Connection = Con
            If Not Con.State = ConnectionState.Open Then Con.Open()
            cm.ExecuteNonQuery()
            'msg_Information(str_informUpdate)
            Con.Close()
            costcenterResetcontrol()
            SaveActivityLog("Accounts", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.AccountTransaction, Me.txtID.Text.Trim, True)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            If Con.State = ConnectionState.Open Then Con.Close()
        End Try
    End Sub
    Private Sub EditRecord()
        If Not GrdCostCenter.RowCount <> 0 Then Exit Sub
        Me.txtID.Text = GrdCostCenter.GetRow.Cells(CostCenter.CostCenterID).Text
        Me.TxtName.Text = GrdCostCenter.GetRow.Cells(CostCenter.Name).Text
        Me.TxtCode.Text = GrdCostCenter.GetRow.Cells(CostCenter.Code).Text
        Me.cmbHead.Text = Me.GrdCostCenter.GetRow.Cells(CostCenter.CostCenterGroup).Text
        Me.TxtSortOrder.Text = GrdCostCenter.GetRow.Cells(CostCenter.SortOrder).Text
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.Active).Value) Then
            Me.chkActive.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.Active).Value
        Else
            Me.chkActive.Checked = False
        End If
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.Active).Value) Then
            Me.chkActive.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.Active).Value
        Else
            Me.chkActive.Checked = False
        End If
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.OutwardGatepass).Value) Then
            Me.chkOuwardgatepass.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.OutwardGatepass).Value
        Else
            Me.chkOuwardgatepass.Checked = False
        End If
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.Logical).Value) Then
            Me.cbLogical.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.Logical).Value
        Else
            Me.cbLogical.Checked = False
        End If
        Me.txtAmount.Text = GrdCostCenter.GetRow.Cells(CostCenter.Amount).Text
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.SalariedBudget).Value) Then
            Me.chkSalariedBudget.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.SalariedBudget).Value
        Else
            Me.chkSalariedBudget.Checked = False
        End If
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.DepartmentBudget).Value) Then
            Me.chkDepartmentBudget.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.DepartmentBudget).Value
        Else
            Me.chkDepartmentBudget.Checked = False
        End If
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.SOBudget).Value) Then
            Me.chkSOBudget.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.SOBudget).Value
        Else
            Me.chkSOBudget.Checked = False
        End If
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.Contract).Value) Then
            Me.chkContract.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.Contract).Value
        Else
            Me.chkContract.Checked = False
        End If
        If Not IsDBNull(Me.GrdCostCenter.GetRow.Cells(CostCenter.Purchasedemand).Value) Then
            Me.chkPurchaseDemand.Checked = Me.GrdCostCenter.GetRow.Cells(CostCenter.Purchasedemand).Value
        Else
            Me.chkPurchaseDemand.Checked = False
        End If
        Me.txtRemainingAmount.Text = GrdCostCenter.GetRow.Cells(CostCenter.RemainingAmount).Text
        GetSecurityRights()
        BtnSave.Text = "&Update"
        TxtName.Focus()

    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        costcenterResetcontrol()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        EditRecord()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Me.Cursor = Cursors.WaitCursor

        If IsValidate() = True Then

            If BtnSave.Text = "Save" Or BtnSave.Text = "&Save" Then
                costcenterSave()
            Else
                costcenterUpdate()
            End If

        End If

        Me.Cursor = Cursors.Default
    End Sub
    'Task1087: Aashir:  Added Validation for name and code
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

        If TxtName.Text = "" Then
            msg_Error("Please Enter Name")
            Return False
            Exit Function

        End If
        If TxtCode.Text = "" Then
            msg_Error("Please Enter Code")
            Return False
            Exit Function
        End If
        Return True

    End Function

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            CostcenterDelet()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmCostCenter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
        If e.KeyCode = Keys.F4 Then
            SaveToolStripButton_Click(BtnSave, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.Escape Then

            NewToolStripButton_Click(BtnNew, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            BtnPrint_Click(BtnPrint, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub frmCostCenter_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try


            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            'Me.Cursor = Cursors.WaitCursor ''14-Mar-2014 TASK:2490 Imran Ali New Bug In Release 2.1.1.6
            Application.DoEvents()
            costcenterResetcontrol()
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False

        End Try
    End Sub
    Private Sub frmCostCenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub cmbHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbHead.SelectedIndexChanged

    End Sub

    Private Sub TxtSortOrder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSortOrder.TextChanged

    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged, chkSOBudget.CheckedChanged, chkSalariedBudget.CheckedChanged, chkDepartmentBudget.CheckedChanged, chkContract.CheckedChanged, chkPurchaseDemand.CheckedChanged

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
                Dim dt As DataTable = GetDataTable("Select * From tblDefCostCenter WHERE CostCenterID='" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.TxtName.Text = dt.Rows(0).Item("Name").ToString
                        Me.TxtCode.Text = dt.Rows(0).Item("Code").ToString
                        Me.txtAmount.Text = dt.Rows(0).Item("Amount").ToString
                        Me.txtRemainingAmount.Text = dt.Rows(0).Item("RemainingAmount").ToString
                        Me.cmbHead.Text = dt.Rows(0).Item("CostCenterGroup").ToString
                        Me.TxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.chkShift.Checked = Convert.ToBoolean(dt.Rows(0).Item("DayShift").ToString)
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


    Private Sub BtnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub GrdCostCenter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GrdCostCenter.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(BtnDelete, Nothing)
            Exit Sub
        End If
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
End Class