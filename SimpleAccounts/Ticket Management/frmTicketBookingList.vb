'10-Oct-2018 TFS4634 : Saad Afzaal : Add new form to save update and delete records through this form.

Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmTicketBookingList
    Implements IGeneral

    Dim objDAL As TicketBookingDAL
    Dim objModel As TicketBookingBE

    Dim DeleteRights As Boolean

    ''' <summary>
    ''' Saad Afzaal : Apply grid seeting to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>'10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Me.grdTicketBooking.RootTable.Columns.Contains("Delete") = False Then
                Me.grdTicketBooking.RootTable.Columns.Add("Delete")
                Me.grdTicketBooking.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdTicketBooking.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdTicketBooking.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdTicketBooking.RootTable.Columns("Delete").Key = "Delete"
                Me.grdTicketBooking.RootTable.Columns("Delete").Caption = "Action"
            End If

            Me.grdTicketBooking.RootTable.Columns("Id").Visible = False
            Me.grdTicketBooking.RootTable.Columns("BookingVoucherId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("AdjustmentVoucherId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("TravelingExpenseAccountId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("CostCenterId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("EmployeeId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("TravelFromCityId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("TravelToCityId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("AgentAccountId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("AirlineId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("FlightTime").Visible = False
            Me.grdTicketBooking.RootTable.Columns("ReminderDate").Visible = False
            Me.grdTicketBooking.RootTable.Columns("TicketCategory").Visible = False
            Me.grdTicketBooking.RootTable.Columns("OtherContacts").Visible = False
            Me.grdTicketBooking.RootTable.Columns("ReturnFromCityId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("ReturnToCityId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("RescheduleAdjustmentAmount").Visible = False
            Me.grdTicketBooking.RootTable.Columns("RescheduleDate").Visible = False
            Me.grdTicketBooking.RootTable.Columns("ReturnFlightDate").Visible = False
            Me.grdTicketBooking.RootTable.Columns("ReturnFlightTime").Visible = False
            Me.grdTicketBooking.RootTable.Columns("RescheduleRemarks").Visible = False
            Me.grdTicketBooking.RootTable.Columns("CancelAdjustmentAmount").Visible = False
            Me.grdTicketBooking.RootTable.Columns("CancelDate").Visible = False
            Me.grdTicketBooking.RootTable.Columns("CancelRemarks").Visible = False
            Me.grdTicketBooking.RootTable.Columns("CancelVoucherId").Visible = False
            Me.grdTicketBooking.RootTable.Columns("Remarks").Visible = False

            Me.grdTicketBooking.RootTable.Columns("TicketAmount").FormatString = "N" & DecimalPointInValue
            Me.grdTicketBooking.RootTable.Columns("TicketAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdTicketBooking.RootTable.Columns("TicketAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdTicketBooking.RootTable.Columns("TicketAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdTicketBooking.RootTable.Columns("TicketAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdTicketBooking.RootTable.Columns("DocDate").FormatString = str_DisplayDateFormat
            Me.grdTicketBooking.RootTable.Columns("FlightDate").FormatString = str_DisplayDateFormat

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub


    ''' <summary>
    ''' Saad Afzaal : Calls the Delete function from DAL to remove the data of selected row.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>'10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New TicketBookingDAL

            Dim Id As Integer = Val(Me.grdTicketBooking.CurrentRow.Cells("Id").Value.ToString)
            Dim BookingVoucherId As Integer = Val(Me.grdTicketBooking.CurrentRow.Cells("BookingVoucherId").Value.ToString)
            Dim AdjustmentVoucherId As Integer = Val(Me.grdTicketBooking.CurrentRow.Cells("AdjustmentVoucherId").Value.ToString)
            Dim CancelVoucherId As Integer = Val(Me.grdTicketBooking.CurrentRow.Cells("CancelVoucherId").Value.ToString)

            If objDAL.Delete(Id, BookingVoucherId, AdjustmentVoucherId, CancelVoucherId) = True Then
                '    'Insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.grdTicketBooking.CurrentRow.Cells("DocNo").Value.ToString, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub


    ''' <summary>
    ''' Saad Afzaal : To show all saved records in history gidr.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "select tblBookingTickets.id , tblBookingTickets.DocNo , tblBookingTickets.DocDate , tblBookingTickets.TicketNo , " _
                & "tblBookingTickets.TicketType , tblBookingTickets.TicketAmount, tblListCityFrom.CityName As FromCity, tblListCityTo.CityName As ToCity, " _
                & "tblListAirLines.AirLine, tblBookingTickets.FlightDate, tblBookingTickets.AgentRefNo, " _
                & "tblBookingTickets.BookingVoucherId, tblBookingTickets.AdjustmentVoucherId, " _
                & "TravelingExpenseAccountId,CostCenterId,EmployeeId,TravelFromCityId,TravelToCityId,AgentAccountId,AirlineId,FlightTime, " _
                & "ReminderDate, TicketCategory, OtherContacts, ReturnFromCityId, ReturnToCityId, RescheduleAdjustmentAmount, RescheduleDate, " _
                & "ReturnFlightDate, ReturnFlightTime, RescheduleRemarks, CancelAdjustmentAmount, CancelDate, CancelRemarks , CancelVoucherId , Remarks " _
                & "from tblBookingTickets " _
                & "Left Outer Join tblListCity As tblListCityFrom On tblBookingTickets.TravelFromCityId = tblListCityFrom.CityId " _
                & "Left Outer Join tblListCity As tblListCityTo On tblBookingTickets.TravelToCityId = tblListCityTo.CityId " _
                & "Left Outer Join tblListAirLines On tblBookingTickets.AirlineId = tblListAirLines.id " _
                & "Order By tblBookingTickets.id DESC"
            dt = GetDataTable(str)
            Me.grdTicketBooking.DataSource = dt
            Me.grdTicketBooking.RetrieveStructure()

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

    ''' <summary>
    ''' Saad Afzaal : To add new record.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        Try
            frmTicketBooking.TicketId = 0
            frmTicketBooking.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Load from
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Private Sub frmTicketBookingList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplySecurityRights()
            GetAllRecords()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>'10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.DeleteRights = True
                Exit Sub
            End If
            Me.Visible = False
            Me.DeleteRights = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.DeleteRights = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Delete the record from grid and also from database.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Private Sub grdTicketBooking_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTicketBooking.ColumnButtonClick
        Try
            objDAL = New TicketBookingDAL
            If e.Column.Key = "Delete" Then
                If Me.DeleteRights = True Then
                    If msg_Confirm(str_ConfirmDelete) = True Then
                        If Delete() = True Then
                            msg_Information(str_informDelete)
                            GetAllRecords()
                            'Me.grdCustomerContact.GetRow.Delete()
                        End If
                    End If
                Else
                    ShowErrorMessage("You do not have rights to delete this document")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Edit records on double click of History grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Private Sub grdTicketBooking_DoubleClick(sender As Object, e As EventArgs) Handles grdTicketBooking.DoubleClick
        Try
            frmTicketBooking.TicketId = Val(Me.grdTicketBooking.CurrentRow.Cells("Id").Value.ToString)

            frmTicketBooking.editModel = New TicketBookingBE

            frmTicketBooking.editModel.BookingVoucherId = Val(Me.grdTicketBooking.CurrentRow.Cells("BookingVoucherId").Value.ToString)
            frmTicketBooking.editModel.AdjustmentVoucherId = Val(Me.grdTicketBooking.CurrentRow.Cells("AdjustmentVoucherId").Value.ToString)
            frmTicketBooking.editModel.CancelVoucherId = Val(Me.grdTicketBooking.CurrentRow.Cells("CancelVoucherId").Value.ToString)

            'One Sided
            frmTicketBooking.editModel.TicketType = Me.grdTicketBooking.CurrentRow.Cells("TicketType").Value.ToString
            frmTicketBooking.editModel.DocNo = Me.grdTicketBooking.CurrentRow.Cells("DocNo").Value.ToString
            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("DocDate").Value) = True Then
                frmTicketBooking.editModel.DocDate = Now
            Else
                frmTicketBooking.editModel.DocDate = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("DocDate").Value)
            End If

            frmTicketBooking.editModel.CostCenterId = Val(Me.grdTicketBooking.CurrentRow.Cells("CostCenterId").Value.ToString)
            frmTicketBooking.editModel.TicketCategory = Me.grdTicketBooking.CurrentRow.Cells("TicketCategory").Value.ToString
            frmTicketBooking.editModel.TravelingExpenseAccountId = Val(Me.grdTicketBooking.CurrentRow.Cells("TravelingExpenseAccountId").Value.ToString)
            frmTicketBooking.editModel.EmployeeId = Val(Me.grdTicketBooking.CurrentRow.Cells("EmployeeId").Value.ToString)
            frmTicketBooking.editModel.TravelFromCityId = Val(Me.grdTicketBooking.CurrentRow.Cells("TravelFromCityId").Value.ToString)
            frmTicketBooking.editModel.TravelToCityId = Val(Me.grdTicketBooking.CurrentRow.Cells("TravelToCityId").Value.ToString)
            frmTicketBooking.editModel.AgentAccountId = Val(Me.grdTicketBooking.CurrentRow.Cells("AgentAccountId").Value.ToString)
            frmTicketBooking.editModel.AirlineId = Val(Me.grdTicketBooking.CurrentRow.Cells("AirlineId").Value.ToString)
            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("FlightDate").Value) = True Then
                frmTicketBooking.editModel.FlightDate = Now
            Else
                frmTicketBooking.editModel.FlightDate = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("FlightDate").Value)
            End If
            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("FlightTime").Value) = True Then
                frmTicketBooking.editModel.FlightTime = Now
            Else
                frmTicketBooking.editModel.FlightTime = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("FlightTime").Value)
            End If

            frmTicketBooking.editModel.TicketNo = Me.grdTicketBooking.CurrentRow.Cells("TicketNo").Value.ToString
            frmTicketBooking.editModel.TicketAmount = Val(Me.grdTicketBooking.CurrentRow.Cells("TicketAmount").Value.ToString)

            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("ReminderDate").Value) = True Then
                frmTicketBooking.editModel.ReminderDate = Date.MinValue
            Else
                frmTicketBooking.editModel.ReminderDate = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("ReminderDate").Value)
            End If

            frmTicketBooking.editModel.AgentRefNo = Me.grdTicketBooking.CurrentRow.Cells("AgentRefNo").Value.ToString
            frmTicketBooking.editModel.Remarks = Me.grdTicketBooking.CurrentRow.Cells("Remarks").Value.ToString
            frmTicketBooking.editModel.OtherContacts = Me.grdTicketBooking.CurrentRow.Cells("OtherContacts").Value.ToString

            'Return 
            frmTicketBooking.editModel.ReturnFromCityId = Val(Me.grdTicketBooking.CurrentRow.Cells("ReturnFromCityId").Value.ToString)
            frmTicketBooking.editModel.ReturnToCityId = Val(Me.grdTicketBooking.CurrentRow.Cells("ReturnToCityId").Value.ToString)
            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("ReturnFlightDate").Value) = True Then
                frmTicketBooking.editModel.ReturnFlightDate = Now
            Else
                frmTicketBooking.editModel.ReturnFlightDate = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("ReturnFlightDate").Value)
            End If
            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("ReturnFlightTime").Value) = True Then
                frmTicketBooking.editModel.ReturnFlightTime = Now
            Else
                frmTicketBooking.editModel.ReturnFlightTime = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("ReturnFlightTime").Value)
            End If

            'Re-Schedule
            frmTicketBooking.editModel.RescheduleAdjustmentAmount = Val(Me.grdTicketBooking.CurrentRow.Cells("RescheduleAdjustmentAmount").Value.ToString)

            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("RescheduleDate").Value) = True Then
                frmTicketBooking.editModel.RescheduleDate = Now
            Else
                frmTicketBooking.editModel.RescheduleDate = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("RescheduleDate").Value)
            End If

            frmTicketBooking.editModel.RescheduleRemarks = Me.grdTicketBooking.CurrentRow.Cells("RescheduleRemarks").Value.ToString

            'Cancel
            frmTicketBooking.editModel.CancelAdjustmentAmount = Val(Me.grdTicketBooking.CurrentRow.Cells("CancelAdjustmentAmount").Value.ToString)
            If IsDBNull(Me.grdTicketBooking.CurrentRow.Cells("CancelDate").Value) = True Then
                frmTicketBooking.editModel.CancelDate = Now
            Else
                frmTicketBooking.editModel.CancelDate = Convert.ToDateTime(Me.grdTicketBooking.CurrentRow.Cells("CancelDate").Value)
            End If
            frmTicketBooking.editModel.CancelRemarks = Me.grdTicketBooking.CurrentRow.Cells("CancelRemarks").Value.ToString

            frmTicketBooking.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdTicketBooking.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdTicketBooking.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdTicketBooking.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Ticket Booking"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class