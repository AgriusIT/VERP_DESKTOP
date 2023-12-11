''Saba Shabbir : added form for vehicle log 4 april 2018

Imports SBModel
Imports SBDal
Imports SBUtility

Public Class frmVehicleLogList
    Implements IGeneral

    Dim Vehi As Vehicle_Log
    Dim objDAL As Vehicle_LogDAL
    Dim log_id As Integer
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    '' enum for indexing the columns
    Enum vehicleLog
        LogId
        LogNo
        LogDate
        Vehicle_Id
        Vehicle_Name
        Person
        Purpose
        Previouse
        Current
        Usage
        Fuel
    End Enum

    Private Sub frmVehicleLogList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnExport.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.Focus()
        GetAllRecords()
        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
    End Sub

    Private Sub btnExport_MouseHover(sender As Object, e As EventArgs) Handles btnExport.MouseHover
        ContextMenuStrip1.Show(btnExport, 0, btnExport.Height)
    End Sub
    ''Saba Shabbir: it will open a new form for entering new record of vehicle
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmVehicleLog.DoHaveSaveRights = DoHaveSaveRights
        frmVehicleLog.blnEditMode = False
        frmVehicleLog.ShowDialog()
        GetAllRecords()
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            '' Setting columns visibility false
            Me.grdAdvanceType.RootTable.Columns(vehicleLog.LogId).Visible = False
            Me.grdAdvanceType.RootTable.Columns(vehicleLog.Vehicle_Id).Visible = False
            Me.grdAdvanceType.RootTable.Columns(vehicleLog.LogNo).Caption = "LogNo"
            Me.grdAdvanceType.RootTable.Columns(vehicleLog.LogDate).FormatString = str_DisplayDateFormat
            '// Setting total row summary option
            Me.grdAdvanceType.RootTable.Columns(vehicleLog.Previouse).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAdvanceType.RootTable.Columns(vehicleLog.Current).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAdvanceType.RootTable.Columns(vehicleLog.Usage).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

           
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''apply security for different users 
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    'Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    'Me.CtrlGrdBar1.mGridExport.Enabled = False
                    'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                'Me.CtrlGrdBar1.mGridPrint.Enabled = False
                'Me.CtrlGrdBar1.mGridExport.Enabled = False
                'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        'Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        'Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        'Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
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
    ''Saba Shabbir: will display data table in a form for editing
    Private Sub EditRecords(dt As DataTable)

        Try
            ''Column Names:LogId, LogDate, Vehicle_Id, Person, Purpose, Previouse, [Current], Fuel, UserName, EntryDateTime, EntryNo
            Vehi = New Vehicle_Log
            Vehi.LogId = dt.Rows(0).Item("LogId").ToString
            Vehi.LogDate = dt.Rows(0).Item("LogDate").ToString
            Vehi.Vehicle_Id = Val(dt.Rows(0).Item("Vehicle_Id").ToString)
            Vehi.Person = dt.Rows(0).Item("Person").ToString
            Vehi.Purpose = dt.Rows(0).Item("Purpose").ToString
            Vehi.Previous = dt.Rows(0).Item("Previouse").ToString
            Vehi.Current = dt.Rows(0).Item("Current").ToString
            Vehi.EntryNo = dt.Rows(0).Item("EntryNo").ToString
            Vehi.EntryDateTime = dt.Rows(0).Item("LogDate").ToString
            Vehi.UserName = LoginUserName
        Catch ex As Exception
            Throw ex
        End Try


    End Sub
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            ''get all records from table and set data source of a grid and apply grid settings
            Me.grdAdvanceType.DataSource = New Vehicle_LogDAL().GetAllRecords
            Me.grdAdvanceType.RetrieveStructure()
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
    Private Sub frmVehicleLogList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Insert Then
            btnAdd_Click(Nothing, Nothing)
        End If
    End Sub
    ''Saba Shabbir: grid row double click then update record and after update the records getAll updated records
    Private Sub grdAdvanceType_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdAdvanceType.RowDoubleClick
        Try
            If Me.grdAdvanceType.RowCount > 0 AndAlso grdAdvanceType.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                frmVehicleLog.log_id = Val(Me.grdAdvanceType.CurrentRow.Cells("LogId").Value.ToString)
                frmVehicleLog.blnEditMode = True
                frmVehicleLog.ShowDialog()
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Saba Shabbir: if key delete pressed , it will delete selected row
    Private Sub grdAdvanceType_KeyDown(sender As Object, e As KeyEventArgs) Handles grdAdvanceType.KeyDown
        Try
            Vehi = New Vehicle_Log
            objDAL = New Vehicle_LogDAL
            Vehi.LogId = Val(Me.grdAdvanceType.CurrentRow.Cells("LogId").Value.ToString)
            log_id = Vehi.LogId
            If e.KeyCode = Keys.Delete Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(Vehi)
                Me.grdAdvanceType.GetRow.Delete()
                SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                GetAllRecords()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
End Class