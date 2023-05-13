''12-jan-2018 task# 2087   Muhammad Abdullah new form created
Imports SBDal
Imports SBModel

Public Class frmPartialReceiptGatePass
    Implements IGeneral
    Dim MasterId As Integer = 0
    Public ResultGateList As List(Of GatePassM)
    Dim GrdDataTable As New DataTable
    Public objDAL As PartialInMasterDAL = New PartialInMasterDAL()


    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Function
            If MasterId > 0 And (BtnSave.Text <> "Save" Or BtnSave.Text <> "&Save") Then
                objDAL.Delete(MasterId)
                SaveActivityLog("Purchase", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, Me.txtDocNo.Text.Trim, True)
                ReSetControls()
                GetAllRecords()
            End If
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub


    Public Sub GetNextDocumentNo()
        Try

            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then

                txtDocNo.Text = GetNextDocNo("RGP-" & Format(Me.dtReceivingDate.Value, "yy"), 4, "PartialInMasterTable", "DocNo")

            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                txtDocNo.Text = GetNextDocNo("RGP-" & Format(Me.dtReceivingDate.Value, "yy") & Me.dtReceivingDate.Value.Month.ToString("00"), 4, "PartialInMasterTable", "DocNo")
            Else
                txtDocNo.Text = GetNextDocNo("RGP", 6, "PartialInMasterTable", "DocNo")
            End If

        Catch ex As Exception

            Throw ex

        End Try

    End Sub
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Fill Grid Master Data
            Me.grdSaved.DataSource = New PartialInMasterDAL().GetAllRecords
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns("ReceivingDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("DocNo").Caption = "Doc No"
            Me.grdSaved.RootTable.Columns("Remarks").Caption = "Remarks"
            Me.grdSaved.RootTable.Columns("InDetailId").Caption = ""
            Me.grdSaved.RootTable.Columns("InDetailId").Visible = False
            Me.grdSaved.RootTable.Columns("PartialInMasterId").Caption = ""
            Me.grdSaved.RootTable.Columns("PartialInMasterId").Visible = False
            Me.grdSaved.RootTable.Columns("outDetailSrNo").Caption = "Sr No"
            Me.grdSaved.RootTable.Columns("outMasterId").Caption = ""
            Me.grdSaved.RootTable.Columns("outMasterId").Visible = False
            Me.grdSaved.RootTable.Columns("InQuantity").Caption = "In Quantity"
            Me.grdSaved.RootTable.Columns("Reference").Caption = "Reference"
            'Me.grdSaved.RootTable.Columns("Issue_Date").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Public Function GetColumnList(TableName As String, Optional IncludePrimayKey As Boolean = True, Optional IncludeBracketsArroundColumnNames As Boolean = False) As String
    '    Dim resultString As String = ""
    '    Dim strQuery As String = " SELECT Column_name FROM   INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = N'" & TableName & "'"
    '    Dim dt As New DataTable
    '    dt = UtilityDAL.GetDataTable(strQuery)
    '    If IncludePrimayKey = False Then
    '        strQuery = "SELECT column_name FROM INFORMATION_SCHEMA.COLUMNS  where table_name = N '" & TableName & "' " &
    '            "and column_name not in (select column_Name from information_schema.key_column_usage where table_name = N'" & TableName & "') order by column_name "
    '        dt = UtilityDAL.GetDataTable(strQuery)
    '    End If
    '    If (IncludeBracketsArroundColumnNames = False) Then
    '        For i As Integer = 0 To dt.Rows.Count() - 1
    '            resultString += dt.Rows(i)("Column_name").ToString()
    '            If i < dt.Rows.Count() - 1 Then
    '                resultString += ", "
    '            End If
    '        Next
    '        Return resultString
    '    Else
    '        For i As Integer = 0 To dt.Rows.Count() - 1
    '            resultString += "[" & dt.Rows(i)("Column_name").ToString() & "]"
    '            If i < dt.Rows.Count() - 1 Then
    '                resultString += ", "
    '            End If
    '        Next
    '        Return resultString
    '    End If


    '    Return strQuery
    'End Function
    Private Sub LoadToGrid()
        Try
            GrdDataTable.Rows.Clear()
            GrdDataTable = CType(Me.grd.DataSource, DataTable)
            For i As Integer = 0 To ResultGateList.Count - 1
                Dim Dr As DataRow
                Dr = GrdDataTable.NewRow
                'Dim result As String = GetColumnList("PartialInDetailTable", True) 
                Dr.Item("DocNo") = txtDocNo.Text
                Dr.Item("ReceivingDate") = dtReceivingDate.Value.ToString()
                Dr.Item("IssueDetail") = ResultGateList(i).IssueDetail
                Dr.Item("Remarks") = ResultGateList(i).Reference
                Dr.Item("outDetailSrNo") = ResultGateList(i).SrNo
                Dr.Item("outMasterId") = ResultGateList(i).Issue_id
                Dr.Item("InQuantity") = ResultGateList(i).Balance
                Dr.Item("limitQuantity") = ResultGateList(i).Balance
                GrdDataTable.Rows.Add(Dr)
            Next

            'GrdDataTable.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If (txtDocNo.Text.Trim.ToString() = "") Then
                msg_Error("Fill * Field First")
                Return False
            ElseIf CheckGridInput() = False Then
                msg_Error("Quantity must be less than Balance Quantity")
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
       

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.dtReceivingDate.Value = Date.Now
            Me.txtRemarks.Text = String.Empty
            Me.BtnSave.Text = "&Save"
            GetNextDocumentNo()

            Dim objDal As New PartialInDetailDAL
            Me.grd.DataSource = objDal.GetDetail(-1)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Function FillModel() As PartialinMasterBE
        Try
            grd.UpdateData()
            Dim obj As PartialinMasterBE = New PartialinMasterBE ' Create new Object Returnable GatePass
            obj.DocNo = txtDocNo.Text
            obj.ReceivingDate = dtReceivingDate.Value.ToString() ' Set Value of Issue No in Object
            obj.Remarks = Me.txtRemarks.Text

            obj.Detail = New List(Of PartialinDetailBE) 'Create new object gatepassdetail in arrray
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                Dim GatePassList As PartialinDetailBE = New PartialinDetailBE 'Create new object Returnable Gatepass Detail

                GatePassList.outDetailSrNo = row.Cells(ColumnNames.outDetailSrNo).Text.ToString
                GatePassList.outMasterId = row.Cells(ColumnNames.outMasterId).Value
                GatePassList.InQuantity = row.Cells(ColumnNames.InQuantity).Text.ToString
                GatePassList.Remarks = row.Cells(ColumnNames.Remarks).Text.ToString
                obj.Detail.Add(GatePassList) 'Collection Values in Array from Gatepass list object
            Next
            Return obj
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Dim objDAL As New PartialInMasterDAL()
        Try
            grd.UpdateData()
            If (IsValidate() = False) Then

                Return False
            End If
            Dim list As PartialinMasterBE = FillModel()
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                objDAL.save(list)
                SaveActivityLog("Purchase", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.txtDocNo.Text.Trim, True)
                ReSetControls()
                GetSecurityRights()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Function
                objDAL.Update(list)
                SaveActivityLog("Purchase", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, Me.txtDocNo.Text.Trim, True)
                ReSetControls()
                GetSecurityRights()
            End If
            Return True


            'If (objDAL.Add(obj)) Then
            '    msg_Information("Record Successfully Added !")
            'End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Function
    Public Structure ColumnNames ' DocNo, ReceivingDate, Remarks' , , outDetailSrNo, outMasterId, InQuantity, Remarks'
        Public Const DocNo As String = "DocNo"
        Public Const PartialInMasterId As String = "Id"
        Public Const ReceivingDate As String = "ReceivingDate"
        Public Const Remarks As String = "Remarks"
        Public Const outDetailSrNo As String = "outDetailSrNo"
        Public Const outMasterId As String = "outMasterId"
        Public Const InQuantity As String = "InQuantity"

    End Structure
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnLoad_Click(sender As Object, e As EventArgs)
        GetAllRecords()
    End Sub



    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If Save() = False Then
                Exit Sub
            End If
            'SaveActivityLog("Sales", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text, True, , , , Me.Name.ToString())
            ReSetControls()
            GetAllRecords()
            GetNextDocumentNo()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try


    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm("Do you want to clear existing data?") = True Then
                    Exit Sub
                End If
            End If
            ReSetControls()
            GetNextDocumentNo()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub


    Private Sub btnLoadDemand_Click(sender As Object, e As EventArgs) Handles btnLoadDemand.Click
        Try
            frmGatePassSearch.ShowDialog()
            If frmGatePassSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                Me.ResultGateList = frmGatePassSearch.ResultGateList
                LoadToGrid()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPartialReceiptGatePass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CtrlGrdBarFront.Visible = False
            CtrlGrdBarHistory.Visible = False
            If UltraTabPageControl1.Focused = True Then
                CtrlGrdBarFront.Visible = True
            Else
                CtrlGrdBarHistory.Visible = True
            End If
            GetSecurityRights()
            ToolTip1.SetToolTip(Me.txtDocNo, "Please enter Doc No")
            ToolTip1.SetToolTip(Me.txtRemarks, "Please enter Remarks")
            Try
                'Dim master As String = GetColumnList("PartialInMasterTable")
                'Dim detial As String = GetColumnList("GatepassDetailTable")
                ReSetControls()
                GetAllRecords()
            Catch ex As Exception
                msg_Error(ex.Message)
            End Try
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
       


    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

            MasterId = Me.grdSaved.GetRow.Cells(0).Value
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells(2).Value
            Me.dtReceivingDate.Value = Me.grdSaved.GetRow.Cells(1).Value
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells(3).Value.ToString


            ''END TASKT TFS1556
            Me.BtnSave.Text = "&Update"

            GrdDataTable = New PartialInMasterDAL().GetRecordById(MasterId)
            Me.grd.DataSource = GrdDataTable


            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab


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



    Private Sub grd_EditingCell(sender As Object, e As Janus.Windows.GridEX.EditingCellEventArgs) Handles grd.EditingCell

    End Sub


    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

    End Sub

    Private Sub frmPartialReceiptGatePass_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                Me.BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F2 Then
                Me.BtnEdit_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                'If Me.grdCostSheet.RecordCount > 0 Then
                '    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                '    ResetControls()
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                'Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmMaterialEstimation)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.PrintListToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'PrintToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString ''R934 Added Dlete Button
                        'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString  ''R934 Added Print Button
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If

            Else
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = False
                If Rights Is Nothing Then Exit Sub
                For Each RightstDt As GroupRights In Rights
                    If RightstDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightstDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightstDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'ElseIf RightstDt.FormControlName = "Approve" Then
                        '    'Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = True
                        '    'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightstDt.FormControlName = "Export" Then
                        CtrlGrdBarHistory.mGridExport.Enabled = True
                        'ElseIf RightstDt.FormControlName = "Post" Then

                    ElseIf RightstDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBarHistory.mGridChooseFielder.Enabled = True
                        'End Task:2406
                        'ElseIf RightstDt.FormControlName = "Attachment" Then
                        'Me.btnAttachment.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function CheckGridInput() As Boolean
        Try
            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                'grd.UpdateData() Dim limit As Double = Convert.ToDouble(grd.GetRow().Cells("limitQuantity").Value.ToString())
                For Each row As Janus.Windows.GridEX.GridEXRow In grd.GetRows()

                    Dim input As Double = Convert.ToDouble(row.Cells("InQuantity").Value.ToString())
                    Dim limit As Double = Convert.ToDouble(grd.GetRow().Cells("limitQuantity").Value.ToString())
                    If input > limit Then
                        Return False
                    End If
                Next
                Return True
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Delete()
    End Sub
End Class

