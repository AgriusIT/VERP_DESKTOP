''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
Imports SBModel
Imports SBDal
Public Class frmServicesInwardGatePass
    Implements IGeneral
    Dim _Inwardgatepass As Inwardgatepassmaster
    Dim _Inwardgatepassdetail As Inwardgatepassdetail
    Dim _MasterId As Integer = 0
    Dim PrintLog As PrintLogBE
    Enum EnmGrid
        InwardgatepassId
        Inwardgatepassdate
        InwardGatePassNo
        BillNo
        PartyName
        Category
        CityName
        Drivername
        VehicleNo
        CityId
        Remarks
    End Enum
    Enum EnmGridDt
        InwardgatepassId
        Detail
        Unit
        PreviousQty
        Qty
        Price
        Amount
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.AutomaticSort = False
            Me.grd.RootTable.Columns(EnmGridDt.InwardgatepassId).Visible = False
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If c.Index <> EnmGridDt.Qty AndAlso c.Index <> EnmGridDt.Detail AndAlso c.Index <> EnmGridDt.Unit AndAlso c.Index <> EnmGridDt.Price Then
                    c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            'Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If IsValidate() = True Then
                Call New InwardagatepassDal().Delete(_Inwardgatepass)
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(Me.cmbcity, "select CityId , CityName from tblListCity ")
            If Condition = "Unit" Then
                Me.cmbUnit.DataSource = New InwardagatepassDal().GetUnit
                Me.cmbUnit.ValueMember = "Unit"
                Me.cmbUnit.DisplayMember = "Unit1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            _Inwardgatepass = New Inwardgatepassmaster
            _Inwardgatepass.InwardGatePassNo = Me.txtdcno.Text
            _Inwardgatepass.BillNo = txtbillno.Text
            _Inwardgatepass.Category = Me.cmbcategory.Text
            _Inwardgatepass.CityId = Me.cmbcity.SelectedValue
            _Inwardgatepass.Drivername = Me.txtdrivername.Text.ToString.Replace("'", "''")
            _Inwardgatepass.Entrydate = Date.Now
            _Inwardgatepass.Inwardgatepassdate = Me.dcdate.Value
            _Inwardgatepass.InwardgatepassId = Me._MasterId
            _Inwardgatepass.PartyName = Me.txtpartname.Text.ToString.Replace("'", "''")
            _Inwardgatepass.TotalAmount = Me.grd.GetTotal(grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            _Inwardgatepass.TotalQty = Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)
            _Inwardgatepass.VehicleNo = Me.txtvehicleno.Text
            _Inwardgatepass.UserName = LoginUserName
            _Inwardgatepass.Remarks = Me.txtremarks.Text.ToString.Replace("'", "''")
            _Inwardgatepass.inwardgatepassdetail = New List(Of Inwardgatepassdetail)
            For Each _grdrow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                _Inwardgatepassdetail = New Inwardgatepassdetail
                _Inwardgatepassdetail.Detail = _grdrow.Cells("detail").Value
                _Inwardgatepassdetail.InwardgatepassId = _grdrow.Cells("inwardgatepassid").Value
                _Inwardgatepassdetail.Price = _grdrow.Cells("price").Value
                _Inwardgatepassdetail.Qty = _grdrow.Cells("qty").Value
                _Inwardgatepassdetail.PreviousQty = _grdrow.Cells("PreviousQty").Value
                _Inwardgatepassdetail.Unit = _grdrow.Cells("Unit").Value.ToString
                _Inwardgatepass.inwardgatepassdetail.Add(_Inwardgatepassdetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.grdsaved.DataSource = New InwardagatepassDal().Getallrecord(IIf(Me.ToolStripComboBox1.SelectedIndex = 0, "I", "O"))
            Me.grdsaved.RetrieveStructure()
            Me.grdsaved.RootTable.Columns(EnmGrid.InwardgatepassId).Visible = False
            Me.grdsaved.RootTable.Columns(EnmGrid.CityId).Visible = False
            Me.grdsaved.RootTable.Columns(EnmGrid.Inwardgatepassdate).Caption = "Date"
            Me.grdsaved.RootTable.Columns(EnmGrid.InwardGatePassNo).Caption = "IG No"
            Me.grdsaved.RootTable.Columns("Inwardgatepassdate").FormatString = str_DisplayDateFormat

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtpartname.Text = String.Empty Then
                ShowErrorMessage("Please enter Party name")
                Me.txtpartname.Focus()
                Return False
            End If
            'If Me.txtbillno.Text = String.Empty Then
            '    ShowErrorMessage("Please enter bill no.")
            '    Me.txtbillno.Focus()
            '    Return False
            'End If
            If Me.txtdcno.Text = String.Empty Then
                ShowErrorMessage("Please define document No")
                Me.txtdcno.Focus()
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
            If Condition = "All" Then
                Me.btnsave.Text = "&Save"
                Me.dcdate.Value = Date.Now
                Me.dcdate.Enabled = True
                If Me.ToolStripComboBox1.Text = "Inward Gatepass" Then
                    If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                        Me.txtdcno.Text = GetSerialNo("IG" + "-" + Microsoft.VisualBasic.Right(Me.dcdate.Value.Year, 2) + "-", "InwardGatepassMasterTable", "InwardGatepassNo")
                    ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                        Me.txtdcno.Text = GetNextDocNo("IG" & "-" & Format(Me.dcdate.Value, "yy") & Me.dcdate.Value.Month.ToString("00"), 4, "InwardGatepassMasterTable", "InwardGatepassNo")
                    Else
                        Me.txtdcno.Text = GetNextDocNo("IG", 6, "InwardGatepassMasterTable", "InwardGatepassNo")
                    End If
                ElseIf Me.ToolStripComboBox1.Text = "Outward Gatepass" Then
                    If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                        Me.txtdcno.Text = GetSerialNo("OG" + "-" + Microsoft.VisualBasic.Right(Me.dcdate.Value.Year, 2) + "-", "InwardGatepassMasterTable", "InwardGatepassNo")
                    ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then

                        Me.txtdcno.Text = GetNextDocNo("OG" & "-" & Format(Me.dcdate.Value, "yy") & Me.dcdate.Value.Month.ToString("00"), 4, "InwardGatepassMasterTable", "InwardGatepassNo")

                    Else
                        Me.txtdcno.Text = GetNextDocNo("OG", 6, "InwardGatepassMasterTable", "InwardGatepassNo")
                    End If
                End If
                Me.ToolStripComboBox1.SelectedIndex = 0
                Me.txtpartname.Text = String.Empty
                Me.cmbcity.SelectedIndex = 0
                Me.cmbcategory.SelectedIndex = 0
                Me.txtbillno.Text = String.Empty
                Me.txtdrivername.Text = String.Empty
                Me.txtvehicleno.Text = String.Empty
                Me.txtremarks.Text = String.Empty
                DetailRecord(-1)
                'GetAllRecords()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.btnedit.Visible = False
                Me.btndelete.Visible = False
                Me.btnprint.Visible = False
                FillCombos("Unit")
            ElseIf Condition = "ClearItemForGrid" Then
                Me.txtdetail.Text = String.Empty
                Me.txtqty.Text = String.Empty
                Me.txtprice.Text = String.Empty
                Me.txtamount.Text = String.Empty
                If Not Me.cmbUnit.SelectedIndex = -1 Then Me.cmbUnit.SelectedIndex = 0
                Me.txtdetail.Focus()
            End If
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                'Ali Faisal : TFS2098 : SMS send to admin
                'Call New InwardagatepassDal().Add(_Inwardgatepass)
                Dim InwardDAL As InwardagatepassDal
                InwardDAL = New InwardagatepassDal
                If InwardDAL.Add(_Inwardgatepass) = True Then
                    SendSMS()
                End If
                'Ali Faisal : TFS2098 : End
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

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If IsValidate() = True Then
                Call New InwardagatepassDal().update(_Inwardgatepass)
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbcategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcategory.SelectedIndexChanged

    End Sub

    Private Sub frmInwardGatePass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.F4 Then
                btnsave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnnew_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnprint_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.Insert Then
                btnadd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmInwardGatePass_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Me.ToolStripComboBox1.Text = "Inward Gatepass" Then
                Me.Text = "Inward Gatepass"
                Me.lblHeader.Text = "Inward Gatepass"
            ElseIf Me.ToolStripComboBox1.Text = "Outward Gatepass" Then
                Me.Text = "Outward Gatepass"
                Me.lblHeader.Text = "Outward Gatepass"
            End If

            FillCombos()
            ReSetControls("All")
            ReSetControls("ClearItemForGrid")
            FillCombos("Unit")
            'Me.GetAllRecords()
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Inward_Gate_Pass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Me.Cursor = Cursors.WaitCursor

        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dcdate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If
            If Me.grd.RootTable Is Nothing Then Exit Sub
            Me.grd.UpdateData()
            If Me.btnsave.Text = "&Save" Or btnsave.Text = "Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informSave)
                ReSetControls("All")
                ReSetControls("ClearItemForGrid")
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                'msg_Information(str_informUpdate)
                ReSetControls("All")
                ReSetControls("ClearItemForGrid")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                ReSetControls("All")
                ReSetControls("ClearItemForGrid")
            Else
                ReSetControls("All")
                ReSetControls("ClearItemForGrid")
            End If
            Me.lblPrintStatus.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
        End Try
    End Sub
    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Me.txtdetail.Text = String.Empty Then
                ShowErrorMessage("Please enter detail")
                Me.txtdetail.Focus()
                Exit Sub
            End If
            If Me.txtPreviousQty.Text = String.Empty Then ''Edit Agianst TFS3078
                ShowErrorMessage("Please enter quantity")
                Me.txtPreviousQty.Focus()
                Exit Sub
            End If
            'If Me.txtprice.Text = String.Empty Then
            '    ShowErrorMessage("Please enter price")
            '    Me.txtprice.Focus()
            '    Exit Sub
            'End If
            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(EnmGridDt.InwardgatepassId) = _MasterId
            dr.Item(EnmGridDt.Detail) = Me.txtdetail.Text.ToString
            dr.Item(EnmGridDt.Unit) = Me.cmbUnit.Text
            dr.Item(EnmGridDt.Qty) = Val(Me.txtqty.Text)
            dr.Item(EnmGridDt.Price) = Val(Me.txtprice.Text)
            dr.Item(EnmGridDt.PreviousQty) = Val(Me.txtPreviousQty.Text)

            'dr.Item(EnmGridDt.Amount) = (Val(Me.txtqty.Text) * Val(Me.txtprice.Text))
            dt.Rows.InsertAt(dr, 0)
            dt.AcceptChanges()
            ReSetControls("ClearItemForGrid")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dcdate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dcdate.Enabled = False
                Else
                    Me.dcdate.Enabled = True
                End If
            Else
                Me.dcdate.Enabled = True
            End If

            Me.btnsave.Text = "&Update"
            Me._MasterId = Me.grdsaved.GetRow.Cells(EnmGrid.InwardgatepassId).Value
            Me.txtdcno.Text = Me.grdsaved.GetRow.Cells(EnmGrid.InwardGatePassNo).Text.ToString
            Me.txtpartname.Text = Me.grdsaved.GetRow.Cells(EnmGrid.PartyName).Text.ToString
            Me.cmbcity.SelectedValue = Me.grdsaved.GetRow.Cells(EnmGrid.CityId).Value
            Me.cmbcategory.Text = Me.grdsaved.GetRow.Cells(EnmGrid.Category).Text
            Me.dcdate.Value = Me.grdsaved.GetRow.Cells(EnmGrid.Inwardgatepassdate).Value
            Me.txtbillno.Text = Me.grdsaved.GetRow.Cells(EnmGrid.BillNo).Text.ToString
            Me.txtdrivername.Text = Me.grdsaved.GetRow.Cells(EnmGrid.Drivername).Text.ToString
            Me.txtvehicleno.Text = Me.grdsaved.GetRow.Cells(EnmGrid.VehicleNo).Text.ToString
            Me.txtremarks.Text = Me.grdsaved.GetRow.Cells(EnmGrid.Remarks).Text.ToString
            Me.ReSetControls("ClearItemForGrid")
            Me.DetailRecord(_MasterId)
            Me.UltraTabControl2.SelectedTab = UltraTabControl2.Tabs(0).TabPage.Tab
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdsaved.GetRow.Cells("Print Status").Text.ToString
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnedit.Visible = True
            Me.btndelete.Visible = True
            Me.btnprint.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function DetailRecord(ByVal MasterId As Integer)
        Try
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = New InwardagatepassDal().DetailRecord(MasterId)
            Me.ApplyGridSettings()
            Return grd
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub txtprice_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprice.Leave, txtqty.Leave
        Try
            Me.txtamount.Text = (Val(Me.txtqty.Text) * Val(Me.txtprice.Text))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdsaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdsaved.DoubleClick
        Try
            btnedit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dcdate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("No record in grid")
                Exit Sub
            End If
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.grdsaved.CurrentRow.Delete()
            'msg_Information(str_informDelete)
            ReSetControls("All")
            ReSetControls("ClearItemForGrid")
            Call GetAllRecords()   'Task 2389 Ehtisham ul Haq, reload history after delete record 21-1-14
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False

        End Try

    End Sub
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdsaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdsaved.GetRow.Cells("Inwardgatepassno").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@InwardGatePassId", Me.grdsaved.GetRow.Cells(EnmGrid.InwardgatepassId).Value)
            ShowReport("crptInWardGatepass")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnsave.Enabled = True
                Me.btndelete.Enabled = True
                Me.btnprint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.btnsave.Enabled = False
                Me.btndelete.Enabled = False
                Me.btnprint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnsave.Text = "&Save" Then btnsave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnsave.Text = "&Update" Then btnsave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btndelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnprint.Enabled = True
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
    Private Sub UltraTabControl2_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl2.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dcdate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub frmInwardGatePass_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        Me.Text = "sss"
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Try

            If Me.ToolStripComboBox1.Text = "Inward Gatepass" Then
                Me.Text = "Inward Gatepass"
                Me.lblHeader.Text = "Inward Gatepass"
            ElseIf Me.ToolStripComboBox1.Text = "Outward Gatepass" Then
                Me.Text = "Outward Gatepass"
                Me.lblHeader.Text = "Outward Gatepass"
            End If


            If Me.ToolStripComboBox1.Text = "Inward Gatepass" Then
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Me.txtdcno.Text = GetSerialNo("IG" + "-" + Microsoft.VisualBasic.Right(Me.dcdate.Value.Year, 2) + "-", "InwardGatepassMasterTable", "InwardGatepassNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Me.txtdcno.Text = GetNextDocNo("IG" & "-" & Format(Me.dcdate.Value, "yy") & Me.dcdate.Value.Month.ToString("00"), 4, "InwardGatepassMasterTable", "InwardGatepassNo")
                Else
                    Me.txtdcno.Text = GetNextDocNo("IG", 6, "InwardGatepassMasterTable", "InwardGatepassNo")
                End If
            ElseIf Me.ToolStripComboBox1.Text = "Outward Gatepass" Then
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Me.txtdcno.Text = GetSerialNo("OG" + "-" + Microsoft.VisualBasic.Right(Me.dcdate.Value.Year, 2) + "-", "InwardGatepassMasterTable", "InwardGatepassNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then

                    Me.txtdcno.Text = GetNextDocNo("OG" & "-" & Format(Me.dcdate.Value, "yy") & Me.dcdate.Value.Month.ToString("00"), 4, "InwardGatepassMasterTable", "InwardGatepassNo")

                Else
                    Me.txtdcno.Text = GetNextDocNo("OG", 6, "InwardGatepassMasterTable", "InwardGatepassNo")
                End If
            End If

            Me.GetAllRecords()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F2 Then
            btnedit_Click(Me.btnedit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btndelete_Click(btndelete, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub grdsaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdsaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
        If e.KeyCode = Keys.F2 Then
            btnedit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdsaved.RowCount <= 0 Then Exit Sub
            btndelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdsaved.GetRow Is Nothing AndAlso grdsaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frminwardGatePass"
                'frmModProperty.ShowForm("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdsaved.CurrentRow.Cells(2).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Inward gate pass (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmModProperty.pnlMain.Controls.Contains(frmSystemConfigurationNew) Then
                frmModProperty.ShowForm("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Purchase
            frmModProperty.ShowForm("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS2098 : SMS send to admin
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendSMS()
        Try
            If GetSMSConfig("Inward / Outward Gate Pass").EnabledAdmin = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Inward / Outward Gate Pass")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@PartyName", Me.txtpartname.Text).Replace("@DocumentNo", Me.txtdcno.Text).Replace("@DocumentDate", Me.dcdate.Value.ToShortDateString).Replace("@BillNo", Me.txtbillno.Text).Replace("@Remarks", Me.txtremarks.Text).Replace("@Amount", Me.grd.GetTotal(grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@CompanyName", CompanyTitle).Replace("@Softbeats", "Automated by www.softbeats.net")
                    'SaveSMSLog(strMessage, Me.txtpartname.Text.ToString, "Inward / Outward Gate Pass")
                    If GetSMSConfig("Inward / Outward Gate Pass").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Inward / Outward Gate Pass")
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS2098 : SMS send to admin
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@PartyName")
            str.Add("@DocumentNo")
            str.Add("@DocumentDate")
            str.Add("@BillNo")
            str.Add("@Remarks")
            str.Add("@DriverName")
            str.Add("@VehicleNo")
            str.Add("@City")
            str.Add("@Quantity")
            str.Add("@Amount")
            str.Add("@CompanyName")
            str.Add("@Softbeats")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS2098 : SMS send to admin
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Inward / Outward Gate Pass")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS2098 : SMS send to admin
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSMSTemplate_Click(sender As Object, e As EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Start TFS3078 :Ayesha Rehman : 18-04-2018
    Private Sub txtPreviousQty_TextChanged(sender As Object, e As EventArgs) Handles txtPreviousQty.TextChanged
        Try
            txtqty.Text = Val(txtPreviousQty.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPreviousQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPreviousQty.KeyPress, txtqty.KeyPress, txtprice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End TFS3078

   
End Class