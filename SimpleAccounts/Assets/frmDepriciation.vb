Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmDepriciation
    Implements IGeneral
    Dim DepreciationId As Integer = 0
    Dim IsEditMode As Boolean = False

    Public Sub New()

        InitializeComponent()

        ' fillGrid(DateTime.Now.ToString("yyyy-MM-dd"))

    End Sub

    Dim AssetDepriciationMasterBE As AssetDepriciationMasterBE
    Dim AssetDepriciationMasterId As Integer = 0
    Dim AssetDepriciationDetailsId As Integer = 0

    Private Sub frmDepriciation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'fillGrid(Me.dtpDate.Value.ToString("yyyy-MM-dd"))

        Try
            ReSetControls()
            btnSave.Enabled = True
            dtpDate.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub fillGrid(_date As String)

        Try
            Dim dt As New DataTable
            dt = New AssetsDAL().GetAssetsDepriciations(_date)

            For Each Row As DataRow In dt.Rows

                'If Not IsDBNull(Val(Row.Item("DepriciationAmount").ToString)) And Not IsDBNull(Val(Row.Item("ClosingValue").ToString)) Then

                If (Val(Row.Item("DepriciationAmount").ToString) <= 0 Or Val(Row.Item("ClosingValue") < 0).ToString) Then
                    Row.BeginEdit()
                    Row.Delete()
                    Row.EndEdit()
                End If

                ' End If
            Next

            Me.grdItems.DataSource = dt

            Me.grdItems.RootTable.Columns("DepriciationAmount").FormatString = "N" & DecimalPointInValue
            Me.grdItems.RootTable.Columns("CurrentValue").FormatString = "N" & DecimalPointInValue
            Me.grdItems.RootTable.Columns("ClosingValue").FormatString = "N" & DecimalPointInValue
            Me.grdItems.RootTable.Columns("AcquireCost").FormatString = "N" & DecimalPointInValue
            Me.grdItems.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information("Please record has been saved successfully.")
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information("Please record has been updated successfully.")
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try

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

        Try
            'Me.grdDetail.UpdateData()

            AssetDepriciationMasterBE = New AssetDepriciationMasterBE
            AssetDepriciationMasterBE.DepriciationMasterID = AssetDepriciationMasterId
            AssetDepriciationMasterBE.DocumentNo = Me.TxtDocumentNo.Text
            AssetDepriciationMasterBE.Details = Me.TxtDetails.Text
            AssetDepriciationMasterBE.DepriciationMonth = Me.dtpDate.Value.ToString("yyyy-MM-dd h:mm:ss tt")
            AssetDepriciationMasterBE.EntryDate = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt")

          

            '' Voucher entry
            AssetDepriciationMasterBE.Voucher.VoucherCode = Me.TxtDocumentNo.Text
            AssetDepriciationMasterBE.Voucher.VoucherNo = Me.TxtDocumentNo.Text
            AssetDepriciationMasterBE.Voucher.VoucherDate = Now
            AssetDepriciationMasterBE.Voucher.LocationId = 1
            AssetDepriciationMasterBE.Voucher.VoucherTypeId = 1
            AssetDepriciationMasterBE.Voucher.FinancialYearId = 1
            AssetDepriciationMasterBE.Voucher.UserName = LoginUserName
            AssetDepriciationMasterBE.Voucher.Source = Me.Name
            'AssetDepriciationMasterBE.Voucher.VoucherMonth = Now.m
            ''
            AssetDepriciationMasterBE.Voucher.VoucherDatail = New List(Of VouchersDetail)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetRows

                Dim AssetDepriciationDetails As New AssetDepriciationDetailsBE()
                AssetDepriciationDetails.DepriciationDetailsID = AssetDepriciationDetailsId
                AssetDepriciationDetails.Asset_Id = Row.Cells("Asset_Id").Value
                AssetDepriciationDetails.Rate = Row.Cells("Rate").Value
                AssetDepriciationDetails.DepriciationAmount = Row.Cells("DepriciationAmount").Value
                AssetDepriciationDetails.Closing_Value = Row.Cells("ClosingValue").Value
                AssetDepriciationDetails.DepriciationMasterID = AssetDepriciationMasterId
                AssetDepriciationDetails.DepriciationMonths = Row.Cells("DepriciationMonths").Value
                AssetDepriciationDetails.CurrentValue = Row.Cells("CurrentValue").Value
                AssetDepriciationMasterBE.Detail.Add(AssetDepriciationDetails)


                ''Voucher Detail Debit
                Dim DebitEntry As New VouchersDetail()
                DebitEntry.CoaDetailId = Val(Row.Cells("ExpenseAccountID").Value.ToString())
                DebitEntry.DebitAmount = Row.Cells("DepriciationAmount").Value
                DebitEntry.CreditAmount = 0
                DebitEntry.LocationId = 1
                AssetDepriciationMasterBE.Voucher.VoucherDatail.Add(DebitEntry)
                ''Voucher Detail Credit
                Dim CreditEntry As New VouchersDetail()
                CreditEntry.CoaDetailId = Val(Row.Cells("AccumulativeAccountID").Value.ToString())
                CreditEntry.DebitAmount = 0
                CreditEntry.CreditAmount = Row.Cells("DepriciationAmount").Value
                CreditEntry.LocationId = 1
                AssetDepriciationMasterBE.Voucher.VoucherDatail.Add(CreditEntry)
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'DepriciationMasterID, DocumentNo, Details, DepriciationMonth, EntryDate
            Me.grdSaved.DataSource = AssetDepriciationMasterDAL.GetAll()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("DepriciationMasterID").Visible = False
            Me.grdSaved.RootTable.Columns("DocumentNo").Caption = "Document No"
            Me.grdSaved.RootTable.Columns("DepriciationMonth").Caption = "Depriciation Month"
            Me.grdSaved.RootTable.Columns("DepriciationMonth").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DepriciationMonth").Width = 150
            Me.grdSaved.RootTable.Columns("EntryDate").Caption = "Date"
            Me.grdSaved.RootTable.Columns("EntryDate").FormatString = str_DisplayDateFormat

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try

            If Me.TxtDocumentNo.Text = String.Empty Then
                ShowErrorMessage("Please enter document number")
                Me.TxtDocumentNo.Focus()
                Return False
            End If

            If Me.TxtDetails.Text = String.Empty Then
                ShowErrorMessage("Please enter details")
                Me.TxtDetails.Focus()
                Return False
            End If

            If Me.grdItems.RowCount < 1 Then
                ShowErrorMessage("No row is found in the Grid")
                Me.grdItems.Focus()
                Return False
            End If

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'Me.grdItems.DataSource.clear()
            GetSecurityRights()
            Me.TxtDocumentNo.Text = GetStandardNo()
            Me.TxtDetails.Text = String.Empty
            'Me.btnDelete.Visible = False
            IsEditMode = False
            GetAllRecords()
            GetDetail(-1)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

        Try
            '' Activity Log 
            AssetDepriciationMasterBE.ActivityLog.ActivityName = "Save"
            AssetDepriciationMasterBE.ActivityLog.ApplicationName = "Assets"
            AssetDepriciationMasterBE.ActivityLog.FormCaption = Me.Text
            AssetDepriciationMasterBE.ActivityLog.FormName = Me.Name
            AssetDepriciationMasterBE.ActivityLog.LogDateTime = Now
            AssetDepriciationMasterBE.ActivityLog.User_Name = LoginUserName
            AssetDepriciationMasterBE.ActivityLog.UserID = LoginUserId
            AssetDepriciationMasterBE.ActivityLog.Source = Me.Name
            ''
            If New AssetDepriciationMasterDAL().Add(AssetDepriciationMasterBE) Then
                'SaveActivityLog("Production", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, "", True)
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

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDate.ValueChanged
        Try
            If IsEditMode = False Then
                ReSetControls()
            End If

            IsEditMode = False

            fillGrid(Me.dtpDate.Value.ToString("yyyy-MM-dd"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetStandardNo() As String
        Dim StandardNo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                StandardNo = GetSerialNo("DEP" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "tblDepriciationMaster", "DocumentNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                StandardNo = GetNextDocNo("DEP" & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "tblDepriciationMaster", "DocumentNo")
            Else
                StandardNo = GetNextDocNo("DEP", 6, "tblDepriciationMaster", "DocumentNo")
            End If
            Return StandardNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub EditRecord()
        Try
            IsEditMode = True
            btnSave.Enabled = False
            dtpDate.Enabled = False
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            DepreciationId = Me.grdSaved.CurrentRow.Cells("DepriciationMasterID").Value
            Me.TxtDocumentNo.Text = Me.grdSaved.CurrentRow.Cells("DocumentNo").Value.ToString
            Me.TxtDetails.Text = Me.grdSaved.CurrentRow.Cells("Details").Value.ToString
            Me.dtpDate.Value = Me.grdSaved.CurrentRow.Cells("DepriciationMonth").Value
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            GetDetail(DepreciationId)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetDetail(ByVal DepreciationId As Integer)
        Try
            Me.grdItems.DataSource = AssetDepriciationDetailsDAL.GetDetail(DepreciationId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdSaved.RowDoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            dtpDate.Value = Now
            ReSetControls()
            btnSave.Enabled = True
            dtpDate.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar3.mGridPrint.Enabled = True
                Me.CtrlGrdBar3.mGridExport.Enabled = True
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True

                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar3.mGridPrint.Enabled = False
                    Me.CtrlGrdBar3.mGridExport.Enabled = False
                    Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
                    Me.btnSave.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar3.mGridPrint.Enabled = False
                Me.CtrlGrdBar3.mGridExport.Enabled = False
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar3.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar3.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        Me.btnSave.Enabled = True
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub

            If Me.grdSaved.CurrentRow.RowIndex = 0 Then

                AssetDepriciationMasterBE = New AssetDepriciationMasterBE()
                AssetDepriciationMasterBE.DepriciationMasterID = Val(Me.grdSaved.CurrentRow.Cells("DepriciationMasterID").Value.ToString)

                AssetDepriciationMasterBE.ActivityLog.ActivityName = "Delete"
                AssetDepriciationMasterBE.ActivityLog.ApplicationName = "Assets"
                AssetDepriciationMasterBE.ActivityLog.FormCaption = Me.Text
                AssetDepriciationMasterBE.ActivityLog.FormName = Me.Name
                AssetDepriciationMasterBE.ActivityLog.LogDateTime = Now
                AssetDepriciationMasterBE.ActivityLog.User_Name = LoginUserName
                AssetDepriciationMasterBE.ActivityLog.UserID = LoginUserId
                AssetDepriciationMasterBE.ActivityLog.Source = Me.Name

                ''
                AssetDepriciationMasterBE.Voucher.VNo = Me.grdSaved.CurrentRow.Cells("DocumentNo").Value.ToString
                If AssetDepriciationMasterDAL.Delete(AssetDepriciationMasterBE) = True Then
                    msg_Information("Record has been deleted successfully.")
                    ReSetControls()

                Else
                    ShowErrorMessage("Record could not be deleted successfully")
                End If
                'AssetDepriciationMaster()

            Else

                ShowErrorMessage("You only delete the max date depriciation beacuse previous months depriciatiomn exist")

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Me.grdItems.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub UltraTabControl2_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
            ElseIf e.Tab.Index = 0 AndAlso IsEditMode = False Then
                Me.btnDelete.Visible = False
                Me.btnSave.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & CtrlGrdBar3.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.CtrlGrdBar3.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                grdItems.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
