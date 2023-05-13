'11-Oct-2018 TFS4634 : Saad Afzaal  : Add new functions to save update and delete records.
Imports System
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal

Public Class TicketBookingDAL

    ''' <summary>
    ''' Saad Afzaal : Save the Data in tblBookingTickets
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function Save(ByVal obj As TicketBookingBE, Optional ByVal type As String = "") As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records

            If type = "OneSided" Then

                str = "Insert Into tblBookingTickets (TicketType,DocNo,DocDate,CostCenterId,TicketCategory,TravelingExpenseAccountId,EmployeeId,TravelFromCityId,TravelToCityId,AgentAccountId,AirlineId,FlightDate,FlightTime,TicketNo,TicketAmount,ReminderDate,AgentRefNo,Remarks,OtherContacts) Values(N'" & obj.TicketType & "',N'" & obj.DocNo & "',N'" & obj.DocDate & "'," & obj.CostCenterId & ",N'" & obj.TicketCategory & "', " & obj.TravelingExpenseAccountId & " , " & obj.EmployeeId & " , " & obj.TravelFromCityId & " , " & obj.TravelToCityId & " , " & obj.AgentAccountId & " , " & obj.AirlineId & " , N'" & obj.FlightDate & "' , N'" & obj.FlightTime & "' , N'" & obj.TicketNo & "' , " & obj.TicketAmount & " , " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & " , N'" & obj.AgentRefNo & "' , N'" & obj.Remarks & "' , N'" & obj.OtherContacts & "') Select @@Identity"

            ElseIf type = "Return" Then

                str = "Insert Into tblBookingTickets (TicketType,DocNo,DocDate,CostCenterId,TicketCategory,TravelingExpenseAccountId,EmployeeId,TravelFromCityId,TravelToCityId,AgentAccountId,AirlineId,FlightDate,FlightTime,TicketNo,TicketAmount,ReminderDate,AgentRefNo,Remarks,OtherContacts,ReturnFromCityId,ReturnToCityId,ReturnFlightDate,ReturnFlightTime) Values(N'" & obj.TicketType & "',N'" & obj.DocNo & "',N'" & obj.DocDate & "'," & obj.CostCenterId & ",N'" & obj.TicketCategory & "', " & obj.TravelingExpenseAccountId & " , " & obj.EmployeeId & " , " & obj.TravelFromCityId & " , " & obj.TravelToCityId & " , " & obj.AgentAccountId & " , " & obj.AirlineId & " , N'" & obj.FlightDate & "' , N'" & obj.FlightTime & "' , N'" & obj.TicketNo & "' , " & obj.TicketAmount & " , " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & " , N'" & obj.AgentRefNo & "' , N'" & obj.Remarks & "' , N'" & obj.OtherContacts & "' , N'" & obj.ReturnFromCityId & "' , N'" & obj.ReturnToCityId & "' , N'" & obj.ReturnFlightDate & "' , N'" & obj.ReturnFlightTime & "') Select @@Identity"

            End If

            Dim TicketBookingId As Integer
            TicketBookingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & obj.voucher_no & "',N'" & obj.DocDate & "',1,N'frmTicketBooking',N'" & obj.DocNo & "',N'Ticket Booking voucher') Select @@Identity"
            SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Details Debit
            'Narration modoifcation for 3G constructions
            str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.TravelingExpenseAccountId & "," & obj.TicketAmount & ", 0 , " & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS PURCHASED FROM " & obj.TravelingExpenseAccountName & " AT " & obj.TicketAmount & " FOR " & obj.EmployeeName & "' ," & obj.TicketAmount & ",0,1,1,1,1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Credit Mobilization
            'Narration modoifcation for 3G constructions
            str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.AgentAccountId & ", 0 ," & obj.TicketAmount & "," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS PURCHASE FROM " & obj.AgentAccountName & " AT " & obj.TicketAmount & " FOR " & obj.EmployeeName & "' ,0," & obj.TicketAmount & ",1,1,1,1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "update tblBookingTickets set BookingVoucherId = ident_current('tblVoucher') where Id = " & TicketBookingId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Saad Afzaal : update the Data in tblBookingTickets
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function UpdateNew(ByVal obj As TicketBookingBE, Optional ByVal type As String = "") As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records

            If type = "OneSided" Then

                str = "Update tblBookingTickets Set TicketType=N'" & obj.TicketType & "' ,DocNo=N'" & obj.DocNo & "' ,DocDate=N'" & obj.DocDate & "',CostCenterId=" & obj.CostCenterId & " ,TicketCategory=N'" & obj.TicketCategory & "',TravelingExpenseAccountId =" & obj.TravelingExpenseAccountId & ",EmployeeId = " & obj.EmployeeId & ",TravelFromCityId = " & obj.TravelFromCityId & ",TravelToCityId = " & obj.TravelToCityId & ",AgentAccountId = " & obj.AgentAccountId & ",AirlineId = " & obj.AirlineId & ",FlightDate = '" & obj.FlightDate & "',FlightTime = '" & obj.FlightTime & "',TicketNo = '" & obj.TicketNo & "',TicketAmount = " & obj.TicketAmount & ",ReminderDate = " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & ",AgentRefNo = N'" & obj.AgentRefNo & "' , Remarks = N'" & obj.Remarks & "' , OtherContacts = N'" & obj.OtherContacts & "' Where Id = " & obj.Id & ""

            ElseIf type = "Return" Then

                str = "Update tblBookingTickets Set TicketType=N'" & obj.TicketType & "' ,DocNo=N'" & obj.DocNo & "' ,DocDate=N'" & obj.DocDate & "',CostCenterId=" & obj.CostCenterId & " ,TicketCategory=N'" & obj.TicketCategory & "',TravelingExpenseAccountId =" & obj.TravelingExpenseAccountId & ",EmployeeId = " & obj.EmployeeId & ",TravelFromCityId = " & obj.TravelFromCityId & ",TravelToCityId = " & obj.TravelToCityId & ",AgentAccountId = " & obj.AgentAccountId & ",AirlineId = " & obj.AirlineId & ",FlightDate = '" & obj.FlightDate & "',FlightTime = '" & obj.FlightTime & "',TicketNo = '" & obj.TicketNo & "',TicketAmount = " & obj.TicketAmount & ",ReminderDate = " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & ",AgentRefNo = N'" & obj.AgentRefNo & "' , Remarks = N'" & obj.Remarks & "' , OtherContacts = N'" & obj.OtherContacts & "' , ReturnFromCityId = " & obj.ReturnFromCityId & " , ReturnToCityId = " & obj.ReturnToCityId & " , ReturnFlightDate = N'" & obj.ReturnFlightDate & "' , ReturnFlightTime = N'" & obj.ReturnFlightTime & "' Where Id = " & obj.Id & ""

            End If

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 1, voucher_no = N'" & obj.voucher_no & "', voucher_date =N'" & obj.DocDate & "', post = 1, Source = N'frmTicketBooking', voucher_code = N'" & obj.DocNo & "', Remarks = N'Ticket Booking voucher' Where voucher_Id = " & obj.BookingVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Details
            str = "Delete from tblVoucherDetail Where voucher_id = " & obj.BookingVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Debit
            'TFS1535 : Update or insert voucher in update
            str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & obj.BookingVoucherId & ",1," & obj.TravelingExpenseAccountId & "," & obj.TicketAmount & ",0," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS PURCHASED FROM " & obj.TravelingExpenseAccountName & " AT " & obj.TicketAmount & " FOR " & obj.EmployeeName & "' ," & obj.TicketAmount & ",0,1,1,1,1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Details Credit Mobilization
            'Update or insert voucher in update
            str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & obj.BookingVoucherId & ",1," & obj.AgentAccountId & ", 0, " & obj.TicketAmount & "," & obj.CostCenterId & ", 'TICKET No. " & obj.TicketNo & " IS PURCHASED FROM " & obj.AgentAccountName & " AT " & obj.TicketAmount & " FOR " & obj.EmployeeName & "' ,0," & obj.TicketAmount & ",1,1,1,1) "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            If Not obj.BookingVoucherId > 0 Then
                str = "update tblBookingTickets set BookingVoucherId = " & obj.BookingVoucherId & " where Id = " & obj.Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    ''' <summary>
    ''' Saad Afzaal : update the Data in tblBookingTickets
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function UpdateReSchedule(ByVal obj As TicketBookingBE, Optional ByVal type As String = "") As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records

            If type = "OneSided" Then

                str = "Update tblBookingTickets Set TicketType=N'" & obj.TicketType & "' ,DocNo=N'" & obj.DocNo & "' ,DocDate=N'" & obj.DocDate & "',CostCenterId=" & obj.CostCenterId & " ,TicketCategory=N'" & obj.TicketCategory & "',TravelingExpenseAccountId =" & obj.TravelingExpenseAccountId & ",EmployeeId = " & obj.EmployeeId & ",TravelFromCityId = " & obj.TravelFromCityId & ",TravelToCityId = " & obj.TravelToCityId & ",AgentAccountId = " & obj.AgentAccountId & ",AirlineId = " & obj.AirlineId & ",FlightDate = '" & obj.FlightDate & "',FlightTime = '" & obj.FlightTime & "',TicketNo = '" & obj.TicketNo & "',TicketAmount = " & obj.TicketAmount & ",ReminderDate = " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & ",AgentRefNo = N'" & obj.AgentRefNo & "' , Remarks = N'" & obj.Remarks & "' , OtherContacts = N'" & obj.OtherContacts & "' , RescheduleAdjustmentAmount = " & obj.RescheduleAdjustmentAmount & " , RescheduleDate = N'" & obj.RescheduleDate & "' , RescheduleRemarks = N'" & obj.RescheduleRemarks & "' Where Id = " & obj.Id & ""

            ElseIf type = "Return" Then

                str = "Update tblBookingTickets Set TicketType=N'" & obj.TicketType & "' ,DocNo=N'" & obj.DocNo & "' ,DocDate=N'" & obj.DocDate & "',CostCenterId=" & obj.CostCenterId & " ,TicketCategory=N'" & obj.TicketCategory & "',TravelingExpenseAccountId =" & obj.TravelingExpenseAccountId & ",EmployeeId = " & obj.EmployeeId & ",TravelFromCityId = " & obj.TravelFromCityId & ",TravelToCityId = " & obj.TravelToCityId & ",AgentAccountId = " & obj.AgentAccountId & ",AirlineId = " & obj.AirlineId & ",FlightDate = '" & obj.FlightDate & "',FlightTime = '" & obj.FlightTime & "',TicketNo = '" & obj.TicketNo & "',TicketAmount = " & obj.TicketAmount & ",ReminderDate = " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & ",AgentRefNo = N'" & obj.AgentRefNo & "' , Remarks = N'" & obj.Remarks & "' , OtherContacts = N'" & obj.OtherContacts & "' , RescheduleAdjustmentAmount = " & obj.RescheduleAdjustmentAmount & " , RescheduleDate = N'" & obj.RescheduleDate & "' , RescheduleRemarks = N'" & obj.RescheduleRemarks & "' , ReturnFromCityId = " & obj.ReturnFromCityId & " , ReturnToCityId = " & obj.ReturnToCityId & " , ReturnFlightDate = N'" & obj.ReturnFlightDate & "' , ReturnFlightTime = N'" & obj.ReturnFlightTime & "' Where Id = " & obj.Id & ""

            End If

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)


            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & obj.AdjustmentVoucherId & ") Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 1, voucher_no = N'" & obj.adj_voucher_no & "', voucher_date =N'" & obj.DocDate & "', post = 1, Source = N'frmTicketBooking', voucher_code = N'" & obj.DocNo & "', Remarks = N'Ticket Re-schedule voucher' Where voucher_Id = " & obj.AdjustmentVoucherId & "" _
                & " ELSE INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & obj.adj_voucher_no & "',N'" & obj.DocDate & "',1,N'frmTicketBooking',N'" & obj.DocNo & "',N'Ticket Re-schedule voucher')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Details
            str = "Delete from tblVoucherDetail Where voucher_id = " & obj.AdjustmentVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            If obj.AdjustmentVoucherId > 0 Then
                'Insert Details Debit
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & obj.AdjustmentVoucherId & ",1," & obj.TravelingExpenseAccountId & "," & obj.RescheduleAdjustmentAmount & ",0," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS RE-SCHEDULED FROM " & obj.TravelingExpenseAccountName & " AT " & obj.RescheduleAdjustmentAmount & " FOR " & obj.EmployeeName & "' ," & obj.RescheduleAdjustmentAmount & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Credit Mobilization
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & obj.AdjustmentVoucherId & ",1," & obj.AgentAccountId & ", 0, " & obj.RescheduleAdjustmentAmount & "," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS RE-SCHEDULED FROM " & obj.AgentAccountName & " AT " & obj.RescheduleAdjustmentAmount & " FOR " & obj.EmployeeName & "' ,0," & obj.RescheduleAdjustmentAmount & ",1,1,1,1) "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "update tblBookingTickets set AdjustmentVoucherId = " & obj.AdjustmentVoucherId & " where Id = " & obj.Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                'Insert Details Debit
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.TravelingExpenseAccountId & "," & obj.RescheduleAdjustmentAmount & ",0," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS RE-SCHEDULED FROM " & obj.TravelingExpenseAccountName & " AT " & obj.RescheduleAdjustmentAmount & " FOR " & obj.EmployeeName & "' ," & obj.RescheduleAdjustmentAmount & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Credit Mobilization
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.AgentAccountId & ", 0, " & obj.RescheduleAdjustmentAmount & "," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS RE-SCHEDULED FROM " & obj.AgentAccountName & " AT " & obj.RescheduleAdjustmentAmount & " FOR " & obj.EmployeeName & "' ,0," & obj.RescheduleAdjustmentAmount & ",1,1,1,1) "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "update tblBookingTickets set AdjustmentVoucherId = ident_current('tblVoucher') where Id = " & obj.Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    ''' <summary>
    ''' Saad Afzaal : update the Data in tblBookingTickets
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>11-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function UpdateCancel(ByVal obj As TicketBookingBE, Optional ByVal type As String = "") As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records

            If type = "OneSided" Then

                str = "Update tblBookingTickets Set TicketType=N'" & obj.TicketType & "' ,DocNo=N'" & obj.DocNo & "' ,DocDate=N'" & obj.DocDate & "',CostCenterId=" & obj.CostCenterId & " ,TicketCategory=N'" & obj.TicketCategory & "',TravelingExpenseAccountId =" & obj.TravelingExpenseAccountId & ",EmployeeId = " & obj.EmployeeId & ",TravelFromCityId = " & obj.TravelFromCityId & ",TravelToCityId = " & obj.TravelToCityId & ",AgentAccountId = " & obj.AgentAccountId & ",AirlineId = " & obj.AirlineId & ",FlightDate = '" & obj.FlightDate & "',FlightTime = '" & obj.FlightTime & "',TicketNo = '" & obj.TicketNo & "',TicketAmount = " & obj.TicketAmount & ",ReminderDate = " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & ",AgentRefNo = N'" & obj.AgentRefNo & "' , Remarks = N'" & obj.Remarks & "' , OtherContacts = N'" & obj.OtherContacts & "' , CancelAdjustmentAmount = " & obj.CancelAdjustmentAmount & " , CancelDate = N'" & obj.CancelDate & "' , CancelRemarks = N'" & obj.CancelRemarks & "' Where Id = " & obj.Id & ""

            ElseIf type = "Return" Then

                str = "Update tblBookingTickets Set TicketType=N'" & obj.TicketType & "' ,DocNo=N'" & obj.DocNo & "' ,DocDate=N'" & obj.DocDate & "',CostCenterId=" & obj.CostCenterId & " ,TicketCategory=N'" & obj.TicketCategory & "',TravelingExpenseAccountId =" & obj.TravelingExpenseAccountId & ",EmployeeId = " & obj.EmployeeId & ",TravelFromCityId = " & obj.TravelFromCityId & ",TravelToCityId = " & obj.TravelToCityId & ",AgentAccountId = " & obj.AgentAccountId & ",AirlineId = " & obj.AirlineId & ",FlightDate = '" & obj.FlightDate & "',FlightTime = '" & obj.FlightTime & "',TicketNo = '" & obj.TicketNo & "',TicketAmount = " & obj.TicketAmount & ",ReminderDate = " & If(obj.ReminderDate = DateTime.MinValue, "NULL", "'" & obj.ReminderDate & "'") & ",AgentRefNo = N'" & obj.AgentRefNo & "' , Remarks = N'" & obj.Remarks & "' , OtherContacts = N'" & obj.OtherContacts & "' , CancelAdjustmentAmount = " & obj.CancelAdjustmentAmount & " , CancelDate = N'" & obj.CancelDate & "' , CancelRemarks = N'" & obj.CancelRemarks & "' , ReturnFromCityId = " & obj.ReturnFromCityId & " , ReturnToCityId = " & obj.ReturnToCityId & " , ReturnFlightDate = N'" & obj.ReturnFlightDate & "' , ReturnFlightTime = N'" & obj.ReturnFlightTime & "' Where Id = " & obj.Id & ""

            End If

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)


            str = "If Exists(Select voucher_Id From tblVoucher Where voucher_Id = " & obj.CancelVoucherId & ") Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 1, voucher_no = N'" & obj.cancel_voucher_no & "', voucher_date =N'" & obj.DocDate & "', post = 1, Source = N'frmTicketBooking', voucher_code = N'" & obj.DocNo & "', Remarks = N'Ticket Cancel voucher' Where voucher_Id = " & obj.CancelVoucherId & "" _
                & " ELSE INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & obj.cancel_voucher_no & "',N'" & obj.DocDate & "',1,N'frmTicketBooking',N'" & obj.DocNo & "',N'Ticket Cancel voucher')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Details
            str = "Delete from tblVoucherDetail Where voucher_id = " & obj.CancelVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            If obj.CancelVoucherId > 0 Then
                'Insert Details Debit
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & obj.CancelVoucherId & ",1," & obj.AgentAccountId & "," & obj.TicketAmount - obj.CancelAdjustmentAmount & ",0," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " iS CANCELED FROM " & obj.TravelingExpenseAccountName & " AT " & obj.TicketAmount - obj.CancelAdjustmentAmount & " FOR " & obj.EmployeeName & "' ," & obj.TicketAmount - obj.CancelAdjustmentAmount & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Credit Mobilization
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & obj.CancelVoucherId & ",1," & obj.TravelingExpenseAccountId & ", 0, " & obj.TicketAmount - obj.CancelAdjustmentAmount & "," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS CANCELED FROM " & obj.AgentAccountName & " AT " & obj.TicketAmount - obj.CancelAdjustmentAmount & " FOR " & obj.EmployeeName & "' ,0," & obj.TicketAmount - obj.CancelAdjustmentAmount & ",1,1,1,1) "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "update tblBookingTickets set CancelVoucherId = " & obj.CancelVoucherId & " where Id = " & obj.Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                'Insert Details Debit
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.AgentAccountId & "," & obj.TicketAmount - obj.CancelAdjustmentAmount & ",0," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " iS CANCELED FROM " & obj.TravelingExpenseAccountName & " AT " & obj.TicketAmount - obj.CancelAdjustmentAmount & " FOR " & obj.EmployeeName & "' ," & obj.TicketAmount - obj.CancelAdjustmentAmount & ",0,1,1,1,1)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Insert Details Credit Mobilization
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(ident_current('tblVoucher'),1," & obj.TravelingExpenseAccountId & ", 0, " & obj.TicketAmount - obj.CancelAdjustmentAmount & "," & obj.CostCenterId & ", 'TICKET NO. " & obj.TicketNo & " IS CANCELED FROM " & obj.AgentAccountName & " AT " & obj.TicketAmount - obj.CancelAdjustmentAmount & " FOR " & obj.EmployeeName & "' ,0," & obj.TicketAmount - obj.CancelAdjustmentAmount & ",1,1,1,1) "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "update tblBookingTickets set CancelVoucherId = ident_current('tblVoucher') where Id = " & obj.Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function


    ''' <summary>
    ''' Saad Afzaal : delete Data from tblBookingTickets
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>11-Oct-2018 TFS4634 : Saad Afzaal</remarks>
    Public Function Delete(ByVal Id As Integer, ByVal BookingVoucherId As Integer, ByVal AdjustmentVoucherId As Integer,
                      ByVal CancelVoucherId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete tblBookingTickets
            str = "Delete from tblBookingTickets Where id = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Details
            str = "Delete from tblVoucherDetail Where voucher_id = " & BookingVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Delete from tblVoucherDetail Where voucher_id = " & AdjustmentVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Delete from tblVoucherDetail Where voucher_id = " & CancelVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master
            str = "Delete from tblVoucher Where voucher_id = " & BookingVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Delete from tblVoucher Where voucher_id = " & AdjustmentVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Delete from tblVoucher Where voucher_id = " & CancelVoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

End Class
