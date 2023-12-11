''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
'' TASK TFS1556 Muhammad Ameen on 10-02-2017: Two new fields have to be added named Driver Name and Vehicle no. 
Imports SBDal
Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class frmReturnablegatepass
    Implements IGeneral
    Dim GrdDataTable As New DataTable
    Dim GatePass As ReturnAbleGatePassMaster
    Dim GatePassList As ReturnAbleGatePassDetail
    Dim MasterId As Integer = 0
    Dim IsEditMode As Boolean = False
    Dim PrintLog As PrintLogBE

    Private Sub frmReturnablegatepass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)

            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnPrint_Click(Nothing, Nothing)

            End If
            If e.KeyCode = Keys.Insert Then
                btnadd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmReturnablegatepass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            GetAllRecords()
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
            Return New ReturnAbleGatePassDAL().Delete(GatePass)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            GatePass = New ReturnAbleGatePassMaster ' Create new Object Returnable GatePass
            GatePass.Issue_id = MasterId
            GatePass.Issue_No = Me.txtdcno.Text ' Set Value of Issue No in Object
            GatePass.Issue_date = Me.dtpdcdate.Value.Date
            GatePass.Issue_to = Me.txtissueto.Text
            GatePass.Remarks = Me.txtremarks.Text
            GatePass.Username = LoginUserName
            GatePass.Fdate = Date.Now
            ''TASK TFS1556
            GatePass.DriverName = Me.txtDriverName.Text
            GatePass.VehicleNo = Me.txtVehicleNo.Text
            ''END TASK TFS1556
            GatePass.ReturnableGatePassDetail = New List(Of ReturnAbleGatePassDetail) 'Create new object gatepassdetail in arrray
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdissuedetail.GetRows
                GatePassList = New ReturnAbleGatePassDetail 'Create new object Returnable Gatepass Detail
                GatePassList.Issue_id = MasterId
                GatePassList.IssueDetail = row.Cells("IssueDetail").Text.ToString
                GatePassList.IssueQty = row.Cells("IssueQty").Value
                GatePassList.Reference = row.Cells("Reference").Text.ToString
                GatePass.ReturnableGatePassDetail.Add(GatePassList) 'Collection Values in Array from Gatepass list object
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            'Document No
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Me.txtdcno.Text = GetSerialNo("RG" + "-" + Microsoft.VisualBasic.Right(Me.dtpdcdate.Value.Year, 2) + "-", "GatePassMasterTable", "Issue_No")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then

                Me.txtdcno.Text = GetNextDocNo("RG" & "-" & Format(Me.dtpdcdate.Value, "yy") & Me.dtpdcdate.Value.Month.ToString("00"), 4, "GatePassMasterTable", "Issue_No")
            Else
                Me.txtdcno.Text = GetNextDocNo("RG", 6, "GatePassMasterTable", "Issue_No")
            End If
            'Fill Grid Master Data
            Me.grdSaved.DataSource = New ReturnAbleGatePassDAL().GetAllRecords
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns("Issue_Date").FormatString = str_DisplayDateFormat
            'Me.grdSaved.RootTable.Columns("Issue_Date").FormatString = str_DisplayDateFormat

            'Fill Grid Detail Data
            GrdDataTable = New ReturnAbleGatePassDAL().GetRecordById(-1)
            Me.grdissuedetail.DataSource = GrdDataTable



        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtdcno.Text = String.Empty Then
                ShowErrorMessage("Please Enter Document No")
                Me.txtdcno.Focus()
                Return False
            End If
            If Me.txtissueto.Text = String.Empty Then
                ShowErrorMessage("Please Enter Issue To")
                Me.txtissueto.Focus()
                Return False
            End If
            If Me.txtremarks.Text = String.Empty Then
                ShowErrorMessage("Please Enter Remarks")
                Me.txtremarks.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            Me.btnSave.Text = "&Save"
            Me.dtpdcdate.Value = Now
            Me.txtremarks.Text = String.Empty
            Me.dtpdcdate.Value = Date.Now.Date
            Me.txtissueto.Text = String.Empty


            Me.txtDescription.Text = String.Empty
            Me.txtIssueQty.Text = String.Empty
            Me.txtReference.Text = String.Empty

            ''TASK TFS1556
            Me.txtDriverName.Text = String.Empty
            Me.txtVehicleNo.Text = String.Empty
            ''END TASK TFS1556

            'Me.GetAllRecords()
            Me.lblPrintStatus.Text = String.Empty
            Me.dtpdcdate.Enabled = True
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnEdit.Visible = False
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False
            '''''''''''''''''''''''''''

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Return New ReturnAbleGatePassDAL().save(GatePass)
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
            Return New ReturnAbleGatePassDAL().Update(GatePass)
        Catch ex As Exception
            Throw ex

        End Try
    End Function
    Private Sub AddItemToGrid()
        Try
            GrdDataTable = CType(Me.grdissuedetail.DataSource, DataTable)
            Dim Dr As DataRow
            Dr = GrdDataTable.NewRow
            Dr.Item("Issue_Id") = 0
            Dr.Item("IssueDetail") = Me.txtDescription.Text.ToString
            Dr.Item("IssueQty") = Val(Me.txtIssueQty.Text.ToString)
            Dr.Item("Reference") = Me.txtReference.Text.ToString
            GrdDataTable.Rows.InsertAt(Dr, 0)
            GrdDataTable.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Me.txtDescription.Text = String.Empty Then
                ShowErrorMessage("Please enter description")
                Me.txtDescription.Focus()
                Exit Sub
            End If
            AddItemToGrid()
            Me.txtDescription.Text = String.Empty
            Me.txtDescription.Focus()
            Me.txtIssueQty.Text = String.Empty
            Me.txtReference.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdissuedetail_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdissuedetail.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then grdissuedetail.CurrentRow.Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            GetAllRecords()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpdcdate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If
            If Me.dtpdcdate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpdcdate.Focus()
                Exit Sub
            End If

            If Not IsValidate() = True Then
                Exit Sub
            Else
                FillModel()
                Me.grdissuedetail.UpdateData()
                'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                    ' msg_Information(str_informSave)
                    ReSetControls()
                    GetAllRecords()
                    Me.grdissuedetail.DataSource = New ReturnAbleGatePassDAL().GetRecordById(-1)

                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                    ' msg_Information(str_informUpdate)
                    ReSetControls()
                    GetAllRecords()
                    Me.grdissuedetail.DataSource = New ReturnAbleGatePassDAL().GetRecordById(-1)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpdcdate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpdcdate.Enabled = False
                Else
                    Me.dtpdcdate.Enabled = True
                End If
            Else
                Me.dtpdcdate.Enabled = True
            End If

            MasterId = Me.grdSaved.GetRow.Cells(0).Value
            Me.txtdcno.Text = Me.grdSaved.GetRow.Cells(1).Value
            Me.dtpdcdate.Value = Me.grdSaved.GetRow.Cells(2).Value
            Me.txtissueto.Text = Me.grdSaved.GetRow.Cells(3).Value.ToString
            Me.txtremarks.Text = Me.grdSaved.GetRow.Cells(4).Value.ToString
            ''TASK TFS1556
            Me.txtDriverName.Text = Me.grdSaved.GetRow.Cells("DriverName").Value.ToString
            Me.txtVehicleNo.Text = Me.grdSaved.GetRow.Cells("VehicleNo").Value.ToString

          
            ''END TASKT TFS1556
            Me.btnSave.Text = "&Update"

            GrdDataTable = New ReturnAbleGatePassDAL().GetRecordById(MasterId)
            Me.grdissuedetail.DataSource = GrdDataTable

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnPrint.Visible = True
            Me.btnDelete.Visible = True
            '''''''''''''''''''''''''''
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpdcdate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If
            If Not IsValidate() = True Then
                Exit Sub
            Else
                FillModel()
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
                'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 27-1-14 
                Me.txtReference.Text = 0
                Me.grdSaved.CurrentRow.Delete()
                'msg_Information(str_informDelete)
                ReSetControls()
                Me.grdissuedetail.DataSource = New ReturnAbleGatePassDAL().GetRecordById(-1)
                Call GetAllRecords()  'Task 2389 Ehtisham ul Haq, reload history after delete record 21-1-14
            End If
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("Issue_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@Issue_Id", Me.grdSaved.GetRow.Cells("Issue_Id").Value)
            ShowReport("rptReturnableGatepassInvoice")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True " Then
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        ' Me.Visible = True
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
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                GetAllRecords()
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.btnDelete.Visible = True
                Me.btnEdit.Visible = True
                Me.btnPrint.Visible = False
            Else
                Me.btnDelete.Visible = False
                Me.btnPrint.Visible = False
                Me.btnEdit.Visible = False
                '''''''''''''''''''''''''''
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpdcdate.Value.ToString("yyyy-M-d 00:00:00") Then
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

    Private Sub grdissuedetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdissuedetail.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            NewToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                'Lcontrol = frmMain.LastControlName.Name
                control = "frmReturnablegatepass"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(0).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Return able gate pass (" & frmtask.Ref_No & ") "
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
            'If Not frmMain.SplitContainer.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
            '    frmMain.LoadControl("frmSystemConfiguration")
            'End If
            'frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Purchase
            'frmMain.LoadControl("frmSystemConfiguration")
            'frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class