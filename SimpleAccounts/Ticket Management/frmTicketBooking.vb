'10-Oct-2018 TFS4634 : Saad Afzaal : Add new form to save update and delete records through this form.

Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmTicketBooking
    Implements IGeneral

    Dim objDAL As TicketBookingDAL
    Dim objModel As TicketBookingBE

    Public TicketId As Integer
    Dim BookingVoucherId As Integer
    Dim AdjustmentVoucherId As Integer
    Dim CancelVoucherId As Integer
    Public editModel As TicketBookingBE

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>'10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSaveOneSided.Enabled = True
                Me.btnReturnSave.Enabled = True
                Me.btnSaveRescheduled.Enabled = True
                Me.btnSaveCancel.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSaveOneSided.Enabled = False
            Me.btnReturnSave.Enabled = False
            Me.btnSaveRescheduled.Enabled = False
            Me.btnSaveCancel.Enabled = False
            'Me.btnPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    Me.btnSaveOneSided.Enabled = True
                    Me.btnReturnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then

                    If btnSaveOneSided.Text = "Update" Then
                        Me.btnSaveOneSided.Enabled = True
                    End If

                    If btnReturnSave.Text = "Update" Then
                        Me.btnReturnSave.Enabled = True
                    End If

                    Me.btnSaveRescheduled.Enabled = True
                    Me.btnSaveCancel.Enabled = True
                    'ElseIf Rights.Item(i).FormControlName = "Print" Then
                    'Me.btnPrint.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function


    ''' <summary>
    ''' Saad Afzaal : FillCombos of items, Customer list and open SO here.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

        Try
            Dim str As String = ""

            If Condition = "ExpenseAccount" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as [Type Id], dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                str += " AND   (dbo.vwCOADetail.account_type = 'Expense') "
                str += " AND vwCOADetail.Active=1"
                FillUltraDropDown(cmbAccounts, str)
                Me.cmbAccounts.Rows(0).Activate()
                If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("Type Id").Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                End If
                UltraDropDownSearching(cmbAccounts, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "VendorAccount" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as [Type Id], dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                str += " AND   (dbo.vwCOADetail.account_type = 'Vendor') "
                str += " AND vwCOADetail.Active=1"

                FillUltraDropDown(cmbAgent, str)
                Me.cmbAgent.Rows(0).Activate()
                If Me.cmbAgent.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbAgent.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbAgent.DisplayLayout.Bands(0).Columns("Type Id").Hidden = True
                    Me.cmbAgent.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbAgent.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                End If
                UltraDropDownSearching(cmbAgent, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(cmbReturnAgent, str)
                Me.cmbReturnAgent.Rows(0).Activate()
                If Me.cmbReturnAgent.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbReturnAgent.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbReturnAgent.DisplayLayout.Bands(0).Columns("Type Id").Hidden = True
                    Me.cmbReturnAgent.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbReturnAgent.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                End If
                UltraDropDownSearching(cmbReturnAgent, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "Employee" Then
                str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1"
                FillUltraDropDown(Me.cmbEmployee, str, True)
                Me.cmbEmployee.Rows(0).Activate()
                If Me.cmbEmployee.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbEmployee.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                End If
                UltraDropDownSearching(cmbEmployee, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(Me.cmbEmployeeReturn, str, True)
                Me.cmbEmployeeReturn.Rows(0).Activate()
                If Me.cmbEmployeeReturn.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbEmployeeReturn.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                End If
                UltraDropDownSearching(cmbEmployeeReturn, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "CostCenter" Then
                str = "select CostCenterId AS Id , Name , Code , CostCenterGroup from tblDefCostCenter WHERE Active = 1"
                FillUltraDropDown(Me.cmbProject, str, True)
                Me.cmbProject.Rows(0).Activate()
                If Me.cmbProject.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbProject.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbProject, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "TravelingFromTo" Then
                str = "select CityId As Id , CityName from tblListCity"
                FillUltraDropDown(Me.cmbTravelFrom, str, True)
                Me.cmbTravelFrom.Rows(0).Activate()
                If Me.cmbTravelFrom.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbTravelFrom.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbTravelFrom, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(Me.cmbTravelFromReturn, str, True)
                Me.cmbTravelFromReturn.Rows(0).Activate()
                If Me.cmbTravelFromReturn.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbTravelFromReturn.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbTravelFromReturn, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(Me.cmbTravelTo, str, True)
                Me.cmbTravelTo.Rows(0).Activate()
                If Me.cmbTravelTo.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbTravelTo.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbTravelTo, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(Me.cmbTravelToReturn, str, True)
                Me.cmbTravelToReturn.Rows(0).Activate()
                If Me.cmbTravelToReturn.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbTravelToReturn.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbTravelToReturn, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(Me.cmbReturnFromreturn, str, True)
                Me.cmbReturnFromreturn.Rows(0).Activate()
                If Me.cmbReturnFromreturn.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbReturnFromreturn.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbReturnFromreturn, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(Me.cmbReturnToReturn, str, True)
                Me.cmbReturnToReturn.Rows(0).Activate()
                If Me.cmbReturnToReturn.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbReturnToReturn.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbReturnToReturn, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "Airline" Then
                str = "select Id , AirLine from tblListAirLines"
                FillUltraDropDown(Me.cmbAirline, str, True)
                Me.cmbAirline.Rows(0).Activate()
                If Me.cmbAirline.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbAirline.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbAirline, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
                FillUltraDropDown(Me.cmbReturnAirline, str, True)
                Me.cmbReturnAirline.Rows(0).Activate()
                If Me.cmbReturnAirline.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbReturnAirline.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
                UltraDropDownSearching(cmbReturnAirline, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Fill valus in controls in edit mode from history grid.
    ''' </summary>
    ''' <remarks>10-Oct-2018 TFS4630 : Saad Afzaal</remarks>
    Public Sub EditRecords()
        Try

            If Me.editModel.TicketType = "New" Then
                rbNew.Checked = True

                rbNew.Enabled = True
                rbReSchedule.Enabled = True
                rbCancel.Enabled = True
                btnSaveOneSided.Enabled = True

            ElseIf Me.editModel.TicketType = "Re-Schedule" Then
                rbReSchedule.Checked = True
                rbNew.Enabled = False
                rbReSchedule.Enabled = True
                rbCancel.Enabled = True
                btnSaveOneSided.Enabled = True
            ElseIf Me.editModel.TicketType = "Cancel" Then
                rbNew.Enabled = False
                rbReSchedule.Enabled = False
                rbCancel.Enabled = False
                rbCancel.Checked = True
                rbOneSided.Enabled = False
                rbReturn.Enabled = False
                btnSaveOneSided.Enabled = False
            End If
            txtDocNo.Text = Me.editModel.DocNo
            dtpDocDate.Value = Me.editModel.DocDate
            cmbProject.Value = Me.editModel.CostCenterId
            If Me.editModel.TicketCategory = "OneSided" Then
                rbOneSided.Checked = True
            ElseIf Me.editModel.TicketCategory = "Return" Then
                rbReturn.Checked = True
            End If

            If Convert.ToBoolean(getConfigValueByType("TravelingAccountFrontEndBookingTickets").ToString) = True Then
                cmbAccounts.Value = Me.editModel.TravelingExpenseAccountId
            End If

            Me.BookingVoucherId = Me.editModel.BookingVoucherId
            Me.AdjustmentVoucherId = Me.editModel.AdjustmentVoucherId
            Me.CancelVoucherId = Me.editModel.CancelVoucherId

            If rbOneSided.Checked = True Then
                'One Sided
                cmbEmployee.Value = Me.editModel.EmployeeId
                cmbTravelFrom.Value = Me.editModel.TravelFromCityId
                cmbTravelTo.Value = Me.editModel.TravelToCityId
                cmbAgent.Value = Me.editModel.AgentAccountId
                cmbAirline.Value = Me.editModel.AirlineId
                dtpFlightDate.Value = Me.editModel.FlightDate
                dtpFlightTime.Value = Me.editModel.FlightTime
                txtTicketNo.Text = Me.editModel.TicketNo
                txtTicketAmount.Text = Me.editModel.TicketAmount
                If Me.editModel.ReminderDate = Date.MinValue Then
                    Me.dtpReminderDate.Checked = False
                Else
                    Me.dtpReminderDate.Checked = True
                    Me.dtpReminderDate.Value = Me.editModel.ReminderDate
                End If
                txtAgentRef.Text = Me.editModel.AgentRefNo
                txtRemarks.Text = Me.editModel.Remarks
                txtOtherContact.Text = Me.editModel.OtherContacts
            End If

            If rbReturn.Checked = True Then
                'Return
                cmbEmployeeReturn.Value = Me.editModel.EmployeeId
                cmbTravelFromReturn.Value = Me.editModel.TravelFromCityId
                cmbTravelToReturn.Value = Me.editModel.TravelToCityId
                dtpFlightDateReturn.Value = Me.editModel.FlightDate
                dtoFlightTimeReturn.Value = Me.editModel.FlightTime

                cmbReturnFromreturn.Value = Me.editModel.ReturnFromCityId
                cmbReturnToReturn.Value = Me.editModel.ReturnToCityId
                dtpReturnFlightDate.Value = Me.editModel.ReturnFlightDate
                dtpReturnFlightTime.Value = Me.editModel.ReturnFlightTime

                cmbReturnAgent.Value = Me.editModel.AgentAccountId
                cmbReturnAirline.Value = Me.editModel.AirlineId
                txtReturnTicketNo.Text = Me.editModel.TicketNo
                txtReturnTicketAmount.Text = Me.editModel.TicketAmount
                txtReturnAgentRef.Text = Me.editModel.AgentRefNo
                If Me.editModel.ReminderDate = Date.MinValue Then
                    Me.dtpReturnReminderDate.Checked = False
                Else
                    Me.dtpReturnReminderDate.Checked = True
                    Me.dtpReturnReminderDate.Value = Me.editModel.ReminderDate
                End If
                txtReturnRemarks.Text = Me.editModel.Remarks
                txtReturnOtherContact.Text = Me.editModel.OtherContacts
            End If

            If rbReSchedule.Checked = True Then
                'Re-Schedule
                txtAdjustmentAmount.Text = Me.editModel.RescheduleAdjustmentAmount
                dtpRescheduledDate.Value = Me.editModel.RescheduleDate
                txtRescheduledRemarks.Text = Me.editModel.RescheduleRemarks
            End If

            If rbCancel.Checked = True Then
                'Cancel
                txtAdjustmentAmountCancel.Text = Me.editModel.CancelAdjustmentAmount
                dtpCancelDate.Value = Me.editModel.CancelDate
                txtCancelRemarks.Text = Me.editModel.CancelRemarks
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Fillmodel to get data of Master and Detail records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

        Try
            objModel = New TicketBookingBE

            If Me.TicketId > 0 Then
                objModel.Id = Me.TicketId
                objModel.DocNo = txtDocNo.Text
                objModel.voucher_no = GetVoucherNoByID(BookingVoucherId)
                If AdjustmentVoucherId > 0 Then
                    objModel.adj_voucher_no = GetVoucherNoByID(AdjustmentVoucherId)
                Else
                    objModel.adj_voucher_no = GetVoucherNo()
                End If
                If CancelVoucherId > 0 Then
                    objModel.cancel_voucher_no = GetVoucherNoByID(CancelVoucherId)
                Else
                    objModel.cancel_voucher_no = GetVoucherNo()
                End If
            Else
                objModel.Id = 0
                objModel.DocNo = GetDocumentNo()
                objModel.voucher_no = GetVoucherNo()
            End If

            If rbNew.Checked = True Then
                objModel.TicketType = "New"
            ElseIf rbReSchedule.Checked = True Then
                objModel.TicketType = "Re-Schedule"
            ElseIf rbCancel.Checked = True Then
                objModel.TicketType = "Cancel"
            End If

            objModel.DocDate = dtpDocDate.Value

            If rbOneSided.Checked = True Then
                objModel.TicketCategory = "OneSided"
            ElseIf rbReturn.Checked = True Then
                objModel.TicketCategory = "Return"
            End If

            If Me.cmbProject.Value > 0 Then
                objModel.CostCenterId = Me.cmbProject.Value
            Else
                objModel.CostCenterId = 0
            End If

            If Convert.ToBoolean(getConfigValueByType("TravelingAccountFrontEndBookingTickets").ToString) = True Then
                If Me.cmbAccounts.Value > 0 Then
                    objModel.TravelingExpenseAccountId = Me.cmbAccounts.Value
                    objModel.TravelingExpenseAccountName = Me.cmbAccounts.ActiveRow.Cells("Name").Value
                Else
                    objModel.TravelingExpenseAccountId = 0
                    objModel.TravelingExpenseAccountName = ""
                End If
            Else
                objModel.TravelingExpenseAccountId = Convert.ToInt32(Val(getConfigValueByType("TravelingAccountBookingTickets").ToString))

                If objModel.TravelingExpenseAccountId > 0 Then
                    Dim dt As DataTable = GetDataTable("Select detail_title From vwCOADetail WHERE coa_Detail_id = " & objModel.TravelingExpenseAccountId & "")
                    objModel.TravelingExpenseAccountName = dt.Rows(0).Item("detail_title").ToString()
                Else
                    objModel.TravelingExpenseAccountName = ""
                End If

            End If


            objModel.BookingVoucherId = Me.BookingVoucherId
            objModel.AdjustmentVoucherId = Me.AdjustmentVoucherId
            objModel.CancelVoucherId = Me.CancelVoucherId

            If rbOneSided.Checked = True Then
                'One Sided

                If Me.cmbEmployee.Value > 0 Then
                    objModel.EmployeeId = Me.cmbEmployee.Value
                    objModel.EmployeeName = Me.cmbEmployee.ActiveRow.Cells("Employee_Name").Value
                Else
                    objModel.EmployeeId = 0
                    objModel.EmployeeName = ""
                End If

                If Me.cmbTravelFrom.Value > 0 Then
                    objModel.TravelFromCityId = Me.cmbTravelFrom.Value
                Else
                    objModel.TravelFromCityId = 0
                End If

                If Me.cmbTravelTo.Value > 0 Then
                    objModel.TravelToCityId = Me.cmbTravelTo.Value
                Else
                    objModel.TravelToCityId = 0
                End If

                If Me.cmbAgent.Value > 0 Then
                    objModel.AgentAccountId = Me.cmbAgent.Value
                    objModel.AgentAccountName = Me.cmbAgent.ActiveRow.Cells("Name").Value
                Else
                    objModel.AgentAccountId = 0
                    objModel.AgentAccountName = ""
                End If

                If Me.cmbAirline.Value > 0 Then
                    objModel.AirlineId = Me.cmbAirline.Value
                Else
                    objModel.AirlineId = 0
                End If

                objModel.FlightDate = dtpFlightDate.Value
                objModel.FlightTime = dtpFlightTime.Value
                objModel.TicketNo = txtTicketNo.Text
                objModel.TicketAmount = txtTicketAmount.Text

                If Me.dtpReminderDate.Checked = True Then
                    objModel.ReminderDate = Me.dtpReminderDate.Value
                Else
                    objModel.ReminderDate = DateTime.MinValue
                End If

                objModel.AgentRefNo = txtAgentRef.Text
                objModel.Remarks = txtRemarks.Text
                objModel.OtherContacts = txtOtherContact.Text
            End If

            If rbReturn.Checked = True Then

                'Return

                If Me.cmbEmployeeReturn.Value > 0 Then
                    objModel.EmployeeId = Me.cmbEmployeeReturn.Value
                    objModel.EmployeeName = Me.cmbEmployeeReturn.ActiveRow.Cells("Employee_Name").Value
                Else
                    objModel.EmployeeId = 0
                    objModel.EmployeeName = ""
                End If

                If Me.cmbTravelFromReturn.Value > 0 Then
                    objModel.TravelFromCityId = Me.cmbTravelFromReturn.Value
                Else
                    objModel.TravelFromCityId = 0
                End If

                If Me.cmbTravelToReturn.Value > 0 Then
                    objModel.TravelToCityId = Me.cmbTravelToReturn.Value
                Else
                    objModel.TravelToCityId = 0
                End If

                objModel.FlightDate = dtpFlightDateReturn.Value
                objModel.FlightTime = dtoFlightTimeReturn.Value

                If Me.cmbReturnFromreturn.Value > 0 Then
                    objModel.ReturnFromCityId = Me.cmbReturnFromreturn.Value
                Else
                    objModel.ReturnFromCityId = 0
                End If

                If Me.cmbReturnToReturn.Value > 0 Then
                    objModel.ReturnToCityId = Me.cmbReturnToReturn.Value
                Else
                    objModel.ReturnToCityId = 0
                End If


                objModel.ReturnFlightDate = dtpReturnFlightDate.Value
                objModel.ReturnFlightTime = dtpReturnFlightTime.Value

                If Me.cmbReturnAgent.Value > 0 Then
                    objModel.AgentAccountId = Me.cmbReturnAgent.Value
                    objModel.AgentAccountName = Me.cmbReturnAgent.ActiveRow.Cells("Name").Value
                Else
                    objModel.AgentAccountId = 0
                    objModel.AgentAccountName = ""
                End If

                If Me.cmbReturnAirline.Value > 0 Then
                    objModel.AirlineId = Me.cmbReturnAirline.Value
                Else
                    objModel.AirlineId = 0
                End If

                objModel.TicketNo = txtReturnTicketNo.Text
                objModel.TicketAmount = txtReturnTicketAmount.Text
                objModel.AgentRefNo = txtReturnAgentRef.Text

                If Me.dtpReturnReminderDate.Checked = True Then
                    objModel.ReminderDate = Me.dtpReturnReminderDate.Value
                Else
                    objModel.ReminderDate = DateTime.MinValue
                End If


                objModel.Remarks = txtReturnRemarks.Text
                objModel.OtherContacts = txtReturnOtherContact.Text

            End If

            If rbReSchedule.Checked = True Then
                'Re-Schedule
                objModel.RescheduleAdjustmentAmount = txtAdjustmentAmount.Text
                objModel.RescheduleDate = dtpRescheduledDate.Value
                objModel.RescheduleRemarks = txtRescheduledRemarks.Text
                objModel.voucher_no = GetVoucherNoByID(AdjustmentVoucherId)
                If objModel.voucher_no Is Nothing Then
                    objModel.voucher_no = GetVoucherNo()
                End If
            End If


            If rbCancel.Checked = True Then
                'Cancel
                objModel.CancelAdjustmentAmount = txtAdjustmentAmountCancel.Text
                objModel.CancelDate = dtpCancelDate.Value
                objModel.CancelRemarks = txtCancelRemarks.Text
                objModel.voucher_no = GetVoucherNoByID(CancelVoucherId)
                If objModel.voucher_no Is Nothing Then
                    objModel.voucher_no = GetVoucherNo()
                End If
            End If

            ApplySecurityRights()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Get doc no for next document.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("TB-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblBookingTickets", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("TB-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblBookingTickets", "DocNo")
            Else
                Return GetNextDocNo("TB-", 6, "tblBookingTickets", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetVoucherNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("JV-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("JV-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
            Else
                Return GetNextDocNo("JV-", 6, "tblVoucher", "voucher_no")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetVoucherNoByID(ByVal Id As Integer) As String
        Try
            Dim str As String = "SELECT voucher_id, voucher_no FROM tblVoucher WHERE voucher_id = " & Id & ""
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(1).ToString()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Validate that all controls are selected before save and update functions etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbProject.Value = 0 Then
                msg_Information("Please select any CostCenter")
                Return False
            End If

            If Convert.ToBoolean(getConfigValueByType("TravelingAccountFrontEndBookingTickets").ToString) = True Then
                If Me.cmbAccounts.Value = 0 Then
                    msg_Information("Please select any Traveling Expense Account")
                    Return False
                End If
            Else
                If Convert.ToInt32(Val(getConfigValueByType("TravelingAccountBookingTickets").ToString)) = 0 Then
                    msg_Information("Please select any Traveling Expense Account from Booking Tickets Configuration")
                    Return False
                End If
            End If

            If rbOneSided.Checked = True Then
                If Me.cmbEmployee.Value = 0 Then
                    msg_Information("Please select any Employee")
                    Return False
                ElseIf Me.cmbTravelFrom.Value = 0 Then
                    msg_Information("Please select any location where you travel from")
                    Return False
                ElseIf Me.cmbTravelTo.Value = 0 Then
                    msg_Information("Please select any location where you travel to")
                    Return False
                ElseIf Me.cmbAgent.Value = 0 Then
                    msg_Information("Please select any agent account")
                    Return False
                ElseIf Me.cmbAirline.Value = 0 Then
                    msg_Information("Please select any airline")
                    Return False
                End If
            End If

            If rbReturn.Checked = True Then
                If Me.cmbEmployeeReturn.Value = 0 Then
                    msg_Information("Please select any Employee")
                    Return False
                ElseIf Me.cmbTravelFromReturn.Value = 0 Then
                    msg_Information("Please select any location where you travel from")
                    Return False
                ElseIf Me.cmbTravelToReturn.Value = 0 Then
                    msg_Information("Please select any location where you travel to")
                    Return False
                ElseIf Me.cmbReturnFromreturn.Value = 0 Then
                    msg_Information("Please select any return location where you travel to")
                    Return False
                ElseIf Me.cmbReturnToReturn.Value = 0 Then
                    msg_Information("Please select any return location where you travel to")
                    Return False
                ElseIf Me.cmbReturnAgent.Value = 0 Then
                    msg_Information("Please select any agent account")
                    Return False
                ElseIf Me.cmbReturnAirline.Value = 0 Then
                    msg_Information("Please select any airline")
                    Return False
                End If
            End If

            If rbReSchedule.Checked = True Then
                If Val(txtAdjustmentAmount.Text) = 0 Then
                    msg_Information("Please add any adjustment amount")
                    Return False
                End If
            End If

            If rbCancel.Checked = True Then
                If Val(txtAdjustmentAmountCancel.Text) = 0 Then
                    msg_Information("Please add any adjustment amount")
                    Return False
                End If
            End If

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Saad Afzaal : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = GetDocumentNo()
            Me.rbNew.Checked = True
            Me.cmbAccounts.Value = 0

            If Convert.ToBoolean(getConfigValueByType("TravelingAccountFrontEndBookingTickets").ToString) = True Then
                Me.cmbAccounts.Visible = True
                Label3.Visible = True
            Else
                Me.cmbAccounts.Visible = False
                Label3.Visible = False
            End If

            Me.dtpDocDate.Value = Now
            Me.rbOneSided.Checked = True
            Me.cmbProject.Value = 0

            'One Sided
            cmbEmployee.Value = 0
            cmbTravelFrom.Value = 0
            cmbTravelTo.Value = 0
            cmbAgent.Value = 0
            cmbAirline.Value = 0
            dtpFlightDate.Value = Now
            dtpFlightTime.Value = Now
            txtTicketNo.Text = String.Empty
            txtTicketAmount.Text = String.Empty
            Me.dtpReminderDate.Checked = False
            Me.dtpReminderDate.Value = Now
            txtAgentRef.Text = String.Empty
            txtRemarks.Text = String.Empty
            txtOtherContact.Text = String.Empty


            'Return
            cmbEmployeeReturn.Value = 0
            cmbTravelFromReturn.Value = 0
            cmbTravelToReturn.Value = 0
            dtpFlightDateReturn.Value = Now
            dtoFlightTimeReturn.Value = Now

            cmbReturnFromreturn.Value = 0
            cmbReturnToReturn.Value = 0
            dtpReturnFlightDate.Value = Now
            dtpReturnFlightTime.Value = Now

            cmbReturnAgent.Value = 0
            cmbReturnAirline.Value = 0
            txtReturnTicketNo.Text = String.Empty
            txtReturnTicketAmount.Text = String.Empty
            txtReturnAgentRef.Text = String.Empty
            Me.dtpReturnReminderDate.Checked = False
            Me.dtpReturnReminderDate.Value = Now
            txtReturnRemarks.Text = String.Empty
            txtReturnOtherContact.Text = String.Empty

            'Re-Schedule
            txtAdjustmentAmount.Text = String.Empty
            dtpRescheduledDate.Value = Now
            txtRescheduledRemarks.Text = String.Empty

            'Cancel
            txtAdjustmentAmountCancel.Text = String.Empty
            dtpCancelDate.Value = Now
            txtCancelRemarks.Text = String.Empty

            FillCombos("ExpenseAccount")
            FillCombos("VendorAccount")
            FillCombos("Employee")
            FillCombos("CostCenter")
            FillCombos("TravelingFromTo")
            FillCombos("Airline")

            btnSaveOneSided.Text = "Save"
            btnReturnSave.Text = "Save"

            Me.BookingVoucherId = 0
            Me.AdjustmentVoucherId = 0
            Me.CancelVoucherId = 0

            ApplySecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Calls the save function from DAL to save the records of master and details.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New TicketBookingDAL
            If IsValidate() = True Then

                If rbNew.Checked = True Then

                    If Me.TicketId > 0 Then

                        objModel.Id = Me.TicketId

                        If rbOneSided.Checked = True Then
                            If objDAL.UpdateNew(objModel, "OneSided") = True Then
                                'Insert Activity Log by Saad on 11-Oct-2018
                                SaveActivityLog("PM", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                                Return True
                            Else
                                Return False
                            End If
                        ElseIf rbReturn.Checked = True Then
                            If objDAL.UpdateNew(objModel, "Return") = True Then
                                'Insert Activity Log by Saad on 11-Oct-2018
                                SaveActivityLog("PM", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                                Return True
                            Else
                                Return False
                            End If
                        End If

                    Else

                        If rbOneSided.Checked = True Then
                            If objDAL.Save(objModel, "OneSided") = True Then
                                'Insert Activity Log by Saad on 11-Oct-2018
                                SaveActivityLog("PM", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                                Return True
                            Else
                                Return False
                            End If
                        ElseIf rbReturn.Checked = True Then
                            If objDAL.Save(objModel, "Return") = True Then
                                'Insert Activity Log by Saad on 11-Oct-2018
                                SaveActivityLog("PM", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                                Return True
                            Else
                                Return False
                            End If
                        End If

                    End If

                ElseIf rbReSchedule.Checked = True Then

                    objModel.Id = Me.TicketId

                    If rbOneSided.Checked = True Then
                        If objDAL.UpdateReSchedule(objModel, "OneSided") = True Then
                            'Insert Activity Log by Saad on 03-Oct-2018
                            SaveActivityLog("PM", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                            Return True
                        Else
                            Return False
                        End If
                    ElseIf rbReturn.Checked = True Then
                        If objDAL.UpdateReSchedule(objModel, "Return") = True Then
                            'Insert Activity Log by Saad on 03-Oct-2018
                            SaveActivityLog("PM", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                            Return True
                        Else
                            Return False
                        End If
                    End If

                ElseIf rbCancel.Checked = True Then

                    objModel.Id = Me.TicketId

                    If rbOneSided.Checked = True Then
                        If objDAL.UpdateCancel(objModel, "OneSided") = True Then
                            'Insert Activity Log by Saad on 03-Oct-2018
                            SaveActivityLog("PM", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                            Return True
                        Else
                            Return False
                        End If
                    ElseIf rbReturn.Checked = True Then
                        If objDAL.UpdateCancel(objModel, "Return") = True Then
                            'Insert Activity Log by Saad on 03-Oct-2018
                            SaveActivityLog("PM", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
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

''' <summary>
    ''' Saad Afzaal : Load from
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Private Sub frmTicketBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ReSetControls()

            If Me.TicketId > 0 Then
                btnSaveOneSided.Text = "Update"
                btnReturnSave.Text = "Update"
                rbReSchedule.Enabled = True
                rbCancel.Enabled = True
                rbNew.Enabled = False
                rbOneSided.Enabled = True
                rbReturn.Enabled = True
                ApplySecurityRights()
                EditRecords()
            Else
                btnSaveOneSided.Text = "Save"
                btnReturnSave.Text = "Save"
                Me.rbReSchedule.Enabled = False
                Me.rbCancel.Enabled = False
                rbNew.Enabled = True
                rbOneSided.Enabled = True
                rbReturn.Enabled = True
                btnSaveOneSided.Enabled = True
                ApplySecurityRights()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Refresh controls.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>10-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.ReSetControls()
    End Sub



    Private Sub btnSaveOneSided_Click(sender As Object, e As EventArgs) Handles btnSaveOneSided.Click, btnSaveRescheduled.Click, btnSaveCancel.Click, btnReturnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor

            If Save() = True Then
                msg_Information(str_informSave)
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub rbNew_CheckedChanged(sender As Object, e As EventArgs) Handles rbNew.CheckedChanged
        Try
            If rbNew.Checked = True Then
                rbOneSided.Checked = True
                Me.pnlOneSided.Visible = True
                Me.pnlReScheduled.Visible = False
                Me.pnlCancel.Visible = False

                Me.rbOneSided.Enabled = False
                Me.rbReturn.Enabled = False
                Me.cmbProject.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbOneSided_CheckedChanged(sender As Object, e As EventArgs) Handles rbOneSided.CheckedChanged
        Try
            If rbOneSided.Checked = True Then
                Me.pnlOneSided.Visible = True
                Me.pnlReturn.Visible = False

                Me.pnlReturn.Visible = False
                Me.pnlReScheduled.Visible = False
                Me.pnlCancel.Visible = False

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub rbReturn_CheckedChanged(sender As Object, e As EventArgs) Handles rbReturn.CheckedChanged
        Try
            If rbReturn.Checked = True Then
                If Me.cmbEmployee.Value > 0 Then
                    Me.cmbEmployeeReturn.Value = Me.cmbEmployee.Value
                End If
                If Me.cmbAgent.Value > 0 Then
                    Me.cmbReturnAgent.Value = Me.cmbAgent.Value
                End If
                If Me.cmbTravelFrom.Value > 0 Then
                    Me.cmbTravelFromReturn.Value = Me.cmbTravelFrom.Value
                End If
                If Me.cmbTravelTo.Value > 0 Then
                    Me.cmbTravelToReturn.Value = Me.cmbTravelTo.Value
                End If
                If Me.cmbAirline.Value > 0 Then
                    Me.cmbReturnAirline.Value = Me.cmbAirline.Value
                End If
                Me.dtpFlightDateReturn.Value = Me.dtpFlightDate.Value
                Me.dtoFlightTimeReturn.Value = Me.dtpFlightTime.Value
                If dtpReminderDate.Checked = True Then
                    Me.dtpReturnReminderDate.Value = Me.dtpReminderDate.Value
                End If
                Me.txtReturnTicketNo.Text = Me.txtTicketNo.Text
                Me.txtReturnTicketAmount.Text = Me.txtTicketAmount.Text
                Me.txtReturnAgentRef.Text = Me.txtAgentRef.Text
                Me.txtReturnRemarks.Text = Me.txtRemarks.Text
                Me.txtReturnOtherContact.Text = Me.txtOtherContact.Text

                Me.pnlOneSided.Visible = False
                Me.pnlReturn.Visible = True

                Me.pnlOneSided.Visible = False
                Me.pnlReScheduled.Visible = False
                Me.pnlCancel.Visible = False

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbReSchedule_CheckedChanged(sender As Object, e As EventArgs) Handles rbReSchedule.CheckedChanged
        Try
            If rbReSchedule.Checked = True Then
                Me.pnlOneSided.Visible = False
                Me.pnlReturn.Visible = False
                Me.pnlReScheduled.Visible = True
                Me.pnlCancel.Visible = False
                Me.rbNew.Enabled = False
                Me.rbOneSided.Enabled = False
                Me.rbReturn.Enabled = False
                Me.cmbProject.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCancel_CheckedChanged(sender As Object, e As EventArgs) Handles rbCancel.CheckedChanged
        Try
            If rbCancel.Checked = True Then
                Me.pnlOneSided.Visible = False
                Me.pnlReturn.Visible = False
                Me.pnlReScheduled.Visible = False
                Me.pnlCancel.Visible = True
                Me.rbNew.Enabled = False
                Me.rbReSchedule.Enabled = False
                Me.rbOneSided.Enabled = False
                Me.rbReturn.Enabled = False
                Me.cmbProject.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbEmployee_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmployee.ValueChanged
        Try
            If cmbEmployee.Value > 0 Then
                lblEmployeeName.Text = Me.cmbEmployee.ActiveRow.Cells("Employee_Name").Value.ToString
                lblDesignation.Text = Me.cmbEmployee.ActiveRow.Cells("EmployeeDesignationName").Value.ToString
                lblNumber.Text = Me.cmbEmployee.ActiveRow.Cells("Phone").Value.ToString
            Else
                lblEmployeeName.Text = ""
                lblDesignation.Text = ""
                lblNumber.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbEmployeeReturn_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmployeeReturn.ValueChanged
        Try
            If cmbEmployeeReturn.Value > 0 Then
                lblEmployeeNameReturn.Text = Me.cmbEmployeeReturn.ActiveRow.Cells("Employee_Name").Value.ToString
                lblDesignationReturn.Text = Me.cmbEmployeeReturn.ActiveRow.Cells("EmployeeDesignationName").Value.ToString
                lblEmployeeNumberReturn.Text = Me.cmbEmployeeReturn.ActiveRow.Cells("Phone").Value.ToString
            Else
                lblEmployeeNameReturn.Text = ""
                lblDesignationReturn.Text = ""
                lblEmployeeNumberReturn.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancelOneSided_Click(sender As Object, e As EventArgs) Handles btnCancelOneSided.Click, btnReturnCancel.Click, btnCancelRescheduled.Click, btnCancelCancel.Click
        Try
            ReSetControls()
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class