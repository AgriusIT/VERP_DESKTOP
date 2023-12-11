''TASK TFS3838 Muhammad Amin has developed this form and its associated DAL and Model classes. Dated 12-07-2018
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmComplaintReturnFactoryList

    Implements IGeneral
    Dim objDAL As ReturnToFactoryMasterDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Public DoHavePostRights As Boolean = False
    Public DoHavePrintRights As Boolean = False
    Public DoHaveFieldChooserRights As Boolean = False
    Public DoHaveExportRights As Boolean = False
    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        Dim frm As New frmReturnToFactory(DoHaveSaveRights, DoHavePrintRights, DoHaveExportRights, DoHaveFieldChooserRights)
        frm.ShowDialog()
        GetAllRecords()
    End Sub

    'Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try
    '        btnAddDock.FlatAppearance.BorderSize = 0
    '        btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
    '        GetAllRecords()
    '        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
    '        CtrlGrdBar1_Load(Nothing, Nothing)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        'ID, ReturnNo, ReturnDate, PartyId, vwCOADetail.detail_title AS Party, DriverName, VehicleNo, Remarks
        Try
            If Me.grdReturnToFactoryList.RootTable.Columns.Contains("Delete") = False Then
                Me.grdReturnToFactoryList.RootTable.Columns.Add("Delete")
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").Width = 50
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").Key = "Delete"
                Me.grdReturnToFactoryList.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                DoHavePostRights = True
                DoHavePrintRights = True
                DoHaveExportRights = True
                DoHaveFieldChooserRights = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    DoHavePostRights = False
                    DoHavePrintRights = False
                    DoHaveExportRights = False
                    DoHaveFieldChooserRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                DoHavePostRights = False
                DoHavePrintRights = False
                DoHaveExportRights = False
                DoHaveFieldChooserRights = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                        DoHavePrintRights = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                        DoHaveExportRights = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        DoHaveFieldChooserRights = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        DoHavePostRights = True
                        'ElseIf RightsDt.FormControlName = "Print" Then
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'strSQL = " Select ID, ReturnNo, ReturnDate, PartyId, vwCOADetail.detail_title AS Party, DriverName, VehicleNo, Remarks from ReturnToFactoryMaster LEFT OUTER JOIN vwCOADetail AS Vendor ON ReturnToFactoryMaster.PartyId = Vendor.coa_detail_id  "

            objDAL = New ReturnToFactoryMasterDAL
            Dim dt As DataTable = objDAL.GetAll()
            Me.grdReturnToFactoryList.DataSource = dt
            Me.grdReturnToFactoryList.RetrieveStructure()
            Me.grdReturnToFactoryList.RootTable.Columns("ID").Visible = False
            Me.grdReturnToFactoryList.RootTable.Columns("VendorId").Visible = False
            'Me.grdReturnToFactoryList.RootTable.Columns("IsPosted").Visible = False
            Me.grdReturnToFactoryList.RootTable.Columns("ReturnNo").Caption = "Return No"
            Me.grdReturnToFactoryList.RootTable.Columns("ReturnDate").Caption = "Return Date"
            Me.grdReturnToFactoryList.RootTable.Columns("DriverName").Caption = "Driver Name"
            Me.grdReturnToFactoryList.RootTable.Columns("VehicleNo").Caption = "Vehicle No"
            Me.grdReturnToFactoryList.RootTable.Columns("ReturnDate").FormatString = str_DisplayDateFormat
            Me.grdReturnToFactoryList.RootTable.Columns("Vendor").Width = 300
            Me.grdReturnToFactoryList.RootTable.Columns("DriverName").Width = 300

            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

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

    'Private Sub grdBranchList_DoubleClick(sender As Object, e As EventArgs) Handles grdReturnToFactoryList.DoubleClick
    '    Try
    '        'strSQL = " Select ID, ReturnNo, ReturnDate, PartyId, vwCOADetail.detail_title AS Party, DriverName, VehicleNo, Remarks from ReturnToFactoryMaster LEFT OUTER JOIN vwCOADetail AS Vendor ON ReturnToFactoryMaster.PartyId = Vendor.coa_detail_id  "
    '        If Me.grdReturnToFactoryList.RowCount > 0 Then
    '            Dim ObjModel As New ReturnToFactoryMasterBE
    '            ObjModel.ID = Me.grdReturnToFactoryList.CurrentRow.Cells("ID").Value
    '            ObjModel.ReturnNo = Me.grdReturnToFactoryList.CurrentRow.Cells("ReturnNo").Value.ToString
    '            ObjModel.ReturnDate = Me.grdReturnToFactoryList.CurrentRow.Cells("ReturnDate").Value.ToString
    '            ObjModel.PartyId = Me.grdReturnToFactoryList.CurrentRow.Cells("VendorId").Value
    '            ObjModel.Party = Me.grdReturnToFactoryList.CurrentRow.Cells("Vendor").Value.ToString
    '            ObjModel.DriverName = Me.grdReturnToFactoryList.CurrentRow.Cells("DriverName").Value.ToString
    '            ObjModel.VehicleNo = Me.grdReturnToFactoryList.CurrentRow.Cells("VehicleNo").Value.ToString
    '            ObjModel.Remarks = Me.grdReturnToFactoryList.CurrentRow.Cells("Remarks").Value.ToString
    '            ObjModel.IsPosted = CBool(Me.grdReturnToFactoryList.CurrentRow.Cells("IsPosted").Value)
    '            Dim frm As New frmReturnToFactory(ObjModel, DoHaveUpdateRights, DoHavePostRights, DoHavePrintRights)
    '            frm.ShowDialog()
    '            GetAllRecords()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub grdReturnToFactoryList_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdReturnToFactoryList.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If DoHaveDeleteRights = True Then
                    Dim ObjModel As New ReturnToFactoryMasterBE
                    ObjModel.ID = Me.grdReturnToFactoryList.CurrentRow.Cells("ID").Value
                    ObjModel.ReturnNo = Me.grdReturnToFactoryList.CurrentRow.Cells("ReturnNo").Value.ToString
                    ObjModel.ReturnDate = Me.grdReturnToFactoryList.CurrentRow.Cells("ReturnDate").Value.ToString
                    ObjModel.PartyId = Me.grdReturnToFactoryList.CurrentRow.Cells("VendorId").Value
                    ObjModel.Party = Me.grdReturnToFactoryList.CurrentRow.Cells("Vendor").Value.ToString
                    ObjModel.DriverName = Me.grdReturnToFactoryList.CurrentRow.Cells("DriverName").Value.ToString
                    ObjModel.VehicleNo = Me.grdReturnToFactoryList.CurrentRow.Cells("VehicleNo").Value.ToString
                    ObjModel.Remarks = Me.grdReturnToFactoryList.CurrentRow.Cells("Remarks").Value.ToString
                    ObjModel.IsPosted = CBool(Me.grdReturnToFactoryList.CurrentRow.Cells("IsPosted").Value)
                    ''Activity Log entry
                    ObjModel.ActivityLog = New ActivityLog()
                    ObjModel.ActivityLog.ActivityName = "Delete"
                    ObjModel.ActivityLog.ApplicationName = "RTF"
                    ObjModel.ActivityLog.FormCaption = "Return To Factory"
                    ObjModel.ActivityLog.FormName = "frmReturnToFactory"
                    ObjModel.ActivityLog.LogDateTime = Now
                    ObjModel.ActivityLog.RecordType = String.Empty
                    ObjModel.ActivityLog.RefNo = ObjModel.ReturnNo
                    ObjModel.ActivityLog.Source = "frmReturnToFactory"
                    ObjModel.ActivityLog.User_Name = LoginUserName
                    ObjModel.ActivityLog.UserID = LoginUserId


                    Call New ReturnToFactoryMasterDAL().Delete(ObjModel)
                    Me.grdReturnToFactoryList.GetRow.Delete()
                Else
                    msg_Information("You do not have delete rights.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReturnToFactoryList.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReturnToFactoryList.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdReturnToFactoryList.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Warranty Claim"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmComplaintReturnFactoryList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnAddDock.FlatAppearance.BorderSize = 0
            btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
            GetAllRecords()
            'ApplyGridSettings()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdReturnToFactoryList_DoubleClick(sender As Object, e As EventArgs) Handles grdReturnToFactoryList.DoubleClick
        Try
            'strSQL = " Select ID, ReturnNo, ReturnDate, PartyId, vwCOADetail.detail_title AS Party, DriverName, VehicleNo, Remarks from ReturnToFactoryMaster LEFT OUTER JOIN vwCOADetail AS Vendor ON ReturnToFactoryMaster.PartyId = Vendor.coa_detail_id  "
            If Me.grdReturnToFactoryList.RowCount > 0 Then
                Dim ObjModel As New ReturnToFactoryMasterBE
                ObjModel.ID = Me.grdReturnToFactoryList.CurrentRow.Cells("ID").Value
                ObjModel.ReturnNo = Me.grdReturnToFactoryList.CurrentRow.Cells("ReturnNo").Value.ToString
                ObjModel.ReturnDate = Me.grdReturnToFactoryList.CurrentRow.Cells("ReturnDate").Value.ToString
                ObjModel.PartyId = Me.grdReturnToFactoryList.CurrentRow.Cells("VendorId").Value
                ObjModel.Party = Me.grdReturnToFactoryList.CurrentRow.Cells("Vendor").Value.ToString
                ObjModel.DriverName = Me.grdReturnToFactoryList.CurrentRow.Cells("DriverName").Value.ToString
                ObjModel.VehicleNo = Me.grdReturnToFactoryList.CurrentRow.Cells("VehicleNo").Value.ToString
                ObjModel.Remarks = Me.grdReturnToFactoryList.CurrentRow.Cells("Remarks").Value.ToString
                ObjModel.IsPosted = CBool(Me.grdReturnToFactoryList.CurrentRow.Cells("IsPosted").Value)
                Dim frm As New frmReturnToFactory(ObjModel, DoHaveUpdateRights, DoHavePostRights, DoHavePrintRights, DoHaveExportRights, DoHaveFieldChooserRights)
                frm.ShowDialog()
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
