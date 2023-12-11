''TASK : TFS1373 against said task made two functions named SaveCreditLimit and GetCreditLimit to save and get credit limit of LC Bank. Done by Ameen on 23-08-2017
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.SqlHelper
Imports SBDal



Public Class letterCreditDAL

    Public Function save(ByVal letter As lettercreditBE) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty



            'Dim dtchk As New DataTable
            'dtchk = UtilityDAL.GetDataTable("Select Count(*) From tblDefCostCenter WHERE Name=N'" & letter.LCdoc_No.Replace("'", "''") & "'", trans)
            'dtchk.AcceptChanges()

            'If dtchk.Rows.Count > 0 Then
            '    If Val(dtchk.Rows(0).Item(0).ToString) > 0 Then
            '        Throw New Exception("Project [" & letter.LCdoc_No.Replace("'", "''") & "] is already exists.")
            '    End If
            'End If
            Dim intcounter As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select Count(*) From tblDefCostCenter WHERE Name=N'" & letter.LCdoc_No.Replace("'", "''") & "'")
            If intcounter > 0 Then
                Throw New Exception("Project [" & letter.LCdoc_No.Replace("'", "''") & "] is already exists.")
            End If
            'str = "insert into TBLLETTEROFCREDIT(LCdoc_Type,LCdoc_No,LCdoc_Date,Bank,LCAmount,PaidAmount,LCtype,LCdescription,VoucherTypeId,coa_detail_id,Cheque_No,Cheque_Date,Active,vendorId, CostCenter) values(N'" & letter.LCdoc_Type & "',N'" & letter.LCdoc_No & "',N'" & letter.LCdoc_Date.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & letter.Bank & "'," & letter.LCAmount & "," & letter.PaidAmount & ", N'" & letter.LCtype & "',N'" & letter.LCdescription & "'," & letter.VoucherTypeId & ", " & letter.coa_detail_id & ", " & IIf(letter.Cheque_No = Nothing, "NULL", "N'" & letter.Cheque_No & "'") & ", " & IIf(letter.Cheque_Date = Nothing, "NULL", "N'" & letter.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(letter.Active = True, 1, 0) & ", " & letter.vendorId & ", " & letter.CostCenter & ")"
            str = "insert into TBLLETTEROFCREDIT(LCdoc_Type,LCdoc_No,LCdoc_Date,Bank,LCAmount,PaidAmount,LCtype,LCdescription,VoucherTypeId,coa_detail_id,Cheque_No,Cheque_Date,Active,vendorId, CostCenter,Advising_Bank,Special_Instruction,Reference_No,Performa_No,Performa_date,Vessel,BL_No,BL_Date,ETD_Date, ETA_Date,Clearing_Agent,TransporterID,OpenedBy,Expiry_Date,CurrencyType,CurrencyRate, PortOfDischarge,PortOfLoading,LatestDateShipment,LastShipmentDateBefore,InsurranceValue,NN_Date,BDR_Date,DD_Date,DTB_Date,Freight,Remarks, Origin, Status, CostOfMaterial, Closed) values(N'" & letter.LCdoc_Type & "',N'" & letter.LCdoc_No & "',N'" & letter.LCdoc_Date.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & letter.Bank & "'," & letter.LCAmount & "," & letter.PaidAmount & ", N'" & letter.LCtype & "',N'" & letter.LCdescription & "'," & letter.VoucherTypeId & ", " & letter.coa_detail_id & ", " & IIf(letter.Cheque_No = Nothing, "NULL", "N'" & letter.Cheque_No & "'") & ", " & IIf(letter.Cheque_Date = Nothing, "NULL", "N'" & letter.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(letter.Active = True, 1, 0) & ", " & letter.vendorId & ", " & letter.CostCenter & ",N'" & letter.Advising_Bank.Replace("'", "''") & "',N'" & letter.Special_Instruction.Replace("'", "''") & "',N'" & letter.Reference_No.Replace("'", "''") & "',N'" & letter.Performa_No.Replace("'", "''") & "',Convert(Datetime,N'" & letter.Performa_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & letter.Vessel.Replace("'", "''") & "', N'" & letter.BL_No.Replace("'", "''") & "',Convert(DateTime,N'" & letter.BL_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & IIf(letter.ETD_Date = Date.MinValue, "NULL", "Convert(DateTime,N'" & letter.ETD_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & IIf(letter.ETA_Date = Date.MinValue, "NULL", "Convert(DateTime,N'" & letter.ETA_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",N'" & letter.Clearing_Agent.Replace("'", "''") & "'," & letter.TransporterID & "," & letter.OpendBy & ", " & IIf(letter.Expiry_Date = Date.MinValue, "NULL", "Convert(DateTime,N'" & letter.Expiry_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & letter.CurrencyType & "," & letter.CurrencyRate & ", '" & letter.PortDischarge.Replace("'", "''") & "','" & letter.PortLoading.Replace("'", "''") & "', " & IIf(letter.LatestDateShipment = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.LatestDateShipment.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", " & IIf(letter.LastDateShipmentBefore = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.LastDateShipmentBefore.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & Val(letter.InsurranceValue) & "," & IIf(letter.NN_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.NN_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & IIf(letter.BDR_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.BDR_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & IIf(letter.DD_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.DD_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & IIf(letter.DTB_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.DTB_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & "," & Val(letter.Freight) & ", N'" & letter.Remarks.Replace("'", "''") & "', N'" & letter.Origin.Replace("'", "''") & "', N'" & letter.Status.Replace("'", "''") & "', '" & letter.CostOfMaterial & "', '" & letter.Closed & "') Select @@Identity"
            letter.LCdoc_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            str = String.Empty
            str = "INSERT INTO tblDefCostCenter(Name,Code,CostCenterGroup,Active,LCDocId) VALUES(N'" & letter.LCdoc_No.Replace("'", "''") & "',N'" & letter.Reference_No.Replace("'", "''") & "','LC',1," & letter.LCdoc_Id & ")Select @@Identity"
            letter.CostCenter = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            str = String.Empty
            str = "Update tblLetterOfCredit Set CostCenter=" & letter.CostCenter & " WHERE LCDoc_Id=" & letter.LCdoc_Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            If letter.PaidAmount > 0 Then
                str = String.Empty
                str = "INSERT INTO tblVoucher(location_id, Voucher_No, voucher_date, voucher_type_id,coa_detail_id, cheque_no, cheque_date,post,source,UserName) Values(0, N'" & letter.LCdoc_No & "', N'" & letter.LCdoc_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & letter.VoucherTypeId & ", " & letter.coa_detail_id & ", " & IIf(letter.Cheque_No = Nothing, "NULL", "N'" & letter.Cheque_No & "'") & ", " & IIf(letter.Cheque_Date = Nothing, "NULL", "N'" & letter.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", 1,'frmVoucher', N'" & letter.UserName & "')Select @@Identity "
                Dim obj As Object = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

                str = String.Empty

                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(obj) & ", 0, " & letter.vendorId & ", N'" & letter.LCdescription & "', " & Val(letter.PaidAmount) & ",0," & letter.CostCenter & ") "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                ''''' Opening LC Charges ------------------
                'str = String.Empty
                'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(obj) & ", 0, " & letter.vendorId & ", N'" & letter.LCdescription & "', " & Val(letter.LCAmount) - Val(letter.PaidAmount) & ",0," & letter.CostCenter & ") "
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = String.Empty
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(obj) & ", 0, " & letter.coa_detail_id & ", N'" & letter.LCdescription & "', 0," & Val(letter.PaidAmount) & "," & letter.CostCenter & ") "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                ''''' Retire Charges ----------------------
                'str = String.Empty
                'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(obj) & ", 0, " & letter.coa_detail_id & ", N'" & letter.LCdescription & "', 0," & Val(letter.LCAmount) - Val(letter.PaidAmount) & "," & letter.CostCenter & ") "
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Function

    Public Function update(ByVal letter As lettercreditBE) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim str As String = String.Empty


            'Dim dtchk As New DataTable
            'dtchk = UtilityDAL.GetDataTable("Select Count(*) From tblDefCostCenter WHERE Name=N'" & letter.LCdoc_No.Replace("'", "''") & "' AND CostCenterID <> " & letter.CostCenter & "", trans)
            'dtchk.AcceptChanges()

            'If dtchk.Rows.Count > 0 Then
            '    If Val(dtchk.Rows(0).Item(0).ToString) > 0 Then
            '        Throw New Exception("Project [" & letter.LCdoc_No.Replace("'", "''") & "] is already exists.")
            '    End If
            'End If
            Dim intcounter As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select Count(*) From tblDefCostCenter WHERE Name=N'" & letter.LCdoc_No.Replace("'", "''") & "' AND CostCenterID <> " & letter.CostCenter & "")
            If intcounter > 0 Then
                Throw New Exception("Project [" & letter.LCdoc_No.Replace("'", "''") & "] is already exists.")
            End If

            str = "Update TBLLETTEROFCREDIT set lcdoc_type=N'" & letter.LCdoc_Type & "', lcdoc_no=N'" & letter.LCdoc_No & "',lcdoc_date=N'" & letter.LCdoc_Date.ToString("yyyy-M-d h:mm:ss tt") & "', bank=N'" & letter.Bank & "', lcdescription=N'" & letter.LCdescription & "',lctype=N'" & letter.LCtype & "', LCAmount=' " & letter.LCAmount & "', PaidAmount=N'" & letter.PaidAmount & "', VoucherTypeId=N'" & letter.VoucherTypeId & "', coa_detail_id=N'" & letter.coa_detail_id & "',Cheque_No=" & IIf(letter.Cheque_No = Nothing, "null", "N'" & letter.Cheque_No & "'") & ",Cheque_Date=" & IIf(letter.Cheque_Date = Nothing, "NULL", "N'" & letter.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", active=" & IIf(letter.Active = True, 1, 0) & ",vendorId= " & letter.vendorId & ", CostCenter=" & letter.CostCenter & ",Advising_Bank=N'" & letter.Advising_Bank.Replace("'", "''") & "',Special_Instruction=N'" & letter.Special_Instruction.Replace("'", "''") & "',Reference_No=N'" & letter.Reference_No.Replace("'", "''") & "',Performa_No=N'" & letter.Performa_No.Replace("'", "''") & "',Performa_date=Convert(Datetime,N'" & letter.Performa_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Vessel=N'" & letter.Vessel.Replace("'", "''") & "',BL_No=N'" & letter.BL_No.Replace("'", "''") & "',BL_Date=Convert(DateTime,N'" & letter.BL_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),ETD_Date=" & IIf(letter.ETA_Date = Date.MinValue, "NULL", "Convert(DateTime,N'" & letter.ETD_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", ETA_Date=" & IIf(letter.ETA_Date = Date.MinValue, "NULL", "Convert(DateTime,N'" & letter.ETA_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",Clearing_Agent=N'" & letter.Clearing_Agent.Replace("'", "''") & "',TransporterID=" & letter.TransporterID & ",OpenedBy=" & letter.OpendBy & ",Expiry_Date=" & IIf(letter.Expiry_Date = Date.MinValue, "NULL", "Convert(DateTime,N'" & letter.Expiry_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",CurrencyType=" & letter.CurrencyType & ",CurrencyRate=" & letter.CurrencyRate & " , PortOfDischarge='" & letter.PortDischarge.Replace("'", "''") & "',PortOfLoading='" & letter.PortLoading.Replace("'", "''") & "',LatestDateShipment=" & IIf(letter.LatestDateShipment = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.LatestDateShipment.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",LastShipmentDateBefore=" & IIf(letter.LastDateShipmentBefore = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.LastDateShipmentBefore.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",InsurranceValue=" & Val(letter.InsurranceValue) & ",NN_Date=" & IIf(letter.NN_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.NN_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",BDR_Date=" & IIf(letter.BDR_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.BDR_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", DD_Date=" & IIf(letter.DD_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.DD_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", DTB_Date=" & IIf(letter.DTB_Date = Date.MinValue, "NULL", " Convert(DateTime,'" & letter.DTB_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",Freight=" & Val(letter.Freight) & ",Remarks=N'" & letter.Remarks.Replace("'", "''") & "', Origin =N'" & letter.Origin.Replace("'", "''") & "', Status = N'" & letter.Status.Replace("'", "''") & "', CostOfMaterial = '" & letter.CostOfMaterial & "', Closed = '" & letter.Closed & "' WHERE LCDoc_Id=" & letter.LCdoc_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "UPDATE tblDefCostCenter Set Name=N'" & letter.LCdoc_No.Replace("'", "''") & "',Code=N'" & letter.Reference_No.Replace("'", "''") & "' WHERE CostCenterID=" & letter.CostCenter & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Dim Voucher_Id As Integer = 0I
            'Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & letter.LCdoc_No & "'", trans)
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        Voucher_Id = dt.Rows(0)(0).ToString
            '    End If
            'End If

            Voucher_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & letter.LCdoc_No & "'")
            If letter.PaidAmount > 0 Then


                Dim dtVoucher As New DataTable
                dtVoucher = UtilityDAL.GetDataTable("Select * from TBLLETTEROFCREDIT WHERE LCDoc_No='" & letter.LCdoc_No & "'", trans)
                dtVoucher.AcceptChanges()

                If dtVoucher.HasErrors = False Then
                    If dtVoucher.Rows.Count > 0 Then
                        str = String.Empty
                        str = "UPDATE tblVoucher SET  voucher_no= N'" & letter.LCdoc_No & "',  voucher_date=N'" & letter.LCdoc_Date.ToString("yyyy-M-d h:mm:ss tt") & "', voucher_type_id=" & letter.VoucherTypeId & ",coa_detail_id=" & letter.coa_detail_id & ", cheque_no=" & IIf(letter.Cheque_No = Nothing, "NULL", "N'" & letter.Cheque_No & "'") & ", cheque_date=" & IIf(letter.Cheque_Date = Nothing, "NULL", "N'" & letter.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & " WHERE Voucher_Id=" & Voucher_Id
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    Else
                        str = String.Empty
                        str = "INSERT INTO tblVoucher(location_id, Voucher_No, voucher_date, voucher_type_id,coa_detail_id, cheque_no, cheque_date,post,source,UserName) Values(0, N'" & letter.LCdoc_No & "', N'" & letter.LCdoc_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & letter.VoucherTypeId & ", " & letter.coa_detail_id & ", " & IIf(letter.Cheque_No = Nothing, "NULL", "N'" & letter.Cheque_No & "'") & ", " & IIf(letter.Cheque_Date = Nothing, "NULL", "N'" & letter.Cheque_Date.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", 1,'frmVoucher', N'" & letter.UserName & "')Select @@Identity "
                        Dim obj As Object = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                        Voucher_Id = Convert.ToInt32(obj)
                    End If
                End If


                str = String.Empty
                str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & Voucher_Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = String.Empty
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(Voucher_Id) & ", 0, " & letter.vendorId & ", N'" & letter.LCdescription & "', " & Val(letter.PaidAmount) & ",0," & letter.CostCenter & ") "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                'str = String.Empty
                'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(Voucher_Id) & ", 0, " & letter.vendorId & ", N'" & letter.LCdescription & "', " & Val(letter.LCAmount) - Val(letter.PaidAmount) & ",0," & letter.CostCenter & ") "
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = String.Empty
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(Voucher_Id) & ", 0, " & letter.coa_detail_id & ", N'" & letter.LCdescription & "', 0," & Val(letter.PaidAmount) & "," & letter.CostCenter & ") "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                'str = String.Empty
                'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount,CostCenterID) Values(" & Val(Voucher_Id) & ", 0, " & letter.coa_detail_id & ", N'" & letter.LCdescription & "', 0," & Val(letter.LCAmount) - Val(letter.PaidAmount) & "," & letter.CostCenter & ") "
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Function
    Public Function delete(ByVal letter As lettercreditBE) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty

            'Dim dtchk As New DataTable
            'dtchk = UtilityDAL.GetDataTable("Select Count(*) From tblVoucherDetail WHERE CostCenterId=" & letter.CostCenter & "", trans)
            'dtchk.AcceptChanges()

            'If dtchk.Rows.Count > 0 Then
            '    If Val(dtchk.Rows(0).Item(0).ToString) > 0 Then
            '        Throw New Exception("You can't delete record, because project [" & letter.LCdoc_No.Replace("'", "''") & "] is dependent record exist.")
            '    End If
            'End If

            Dim intcounter As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select Count(*) From tblVoucherDetail WHERE CostCenterId=" & letter.CostCenter & "")
            If intcounter > 0 Then
                Throw New Exception("You can't delete record, because project [" & letter.LCdoc_No.Replace("'", "''") & "] is dependent record exist.")
            End If

            str = "delete From TBLLETTEROFCREDIT where LCdoc_Id=" & letter.LCdoc_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "delete From tblDefCostCenter WHERE LCDocId=" & letter.LCdoc_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Function

    Public Function getall() As DataTable
        Try
            Dim str As String

            str = " SELECT LCdoc_Id, LCdoc_Type, LCdoc_No, LCdoc_Date, Bank, LCAmount, " _
                & " PaidAmount, LCtype, LCdescription, VoucherTypeId, coa_detail_id, Cheque_No, Cheque_Date," _
                & " tblLetterOfCredit.Active, vendorId, isnull(costcenter,0) as CostCenter, tblDefCostCenter.name as Project,Advising_Bank,Special_Instruction,Reference_No,Performa_No,Performa_date,Vessel,BL_No,BL_Date,ETD_Date, ETA_Date, LatestDateShipment as LDS, LastShipmentDateBefore as LSDB, Clearing_Agent, PortOfDischarge, PortOfLoading, TransporterID,OpenedBy,Expiry_Date,CurrencyType,CurrencyRate,InsurranceValue,NN_Date,BDR_Date,DD_Date,DTB_Date,Freight,Remarks, Origin, tblLetterOfCredit.Status, ISNULL(tblLetterOfCredit.CostOfMaterial,0) CostOfMaterial, ISNULL(tblLetterOfCredit.Closed,'False') Closed " _
                & " FROM  dbo.tblLetterOfCredit LEFT OUTER JOIN tblDefCostCenter On tblDefCostCenter.CostCenterId = tblLetterOfCredit.CostCenter ORDER BY LCDoc_ID Desc "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
   
    ''' <summary>
    ''' This function helps to save Bank Credit Limit From LC
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="CreditLimit"></param>
    ''' <returns></returns>
    ''' <remarks> TASK: TFS1373</remarks>
    Public Function SaveBankCredit(ByVal BankName As String, ByVal CreditLimit As Decimal) As Boolean
        Dim trans As SqlTransaction
        Dim conn As SqlConnection
        Try
            conn = New SqlConnection(SQLHelper.CON_STR)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            Dim str As String = String.Empty
            str = " If Not Exists(Select ID From tblLCBankCreditLimit Where BankName = N'" & BankName & "') Insert Into tblLCBankCreditLimit(BankName, CreditLimit) Values(N'" & BankName & "', " & CreditLimit & ") " & _
                  " Else Update tblLCBankCreditLimit Set BankName = '" & BankName & "', CreditLimit = " & CreditLimit & " Where BankName = N'" & BankName & "'"
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
    ''' <summary>
    ''' This function helps to get Bank Credit
    ''' </summary>
    ''' <param name="BankName"></param>
    ''' <returns></returns>
    ''' <remarks>TASK : TFS1373</remarks>
    Public Function GetBankCredit(ByVal BankName As String) As Decimal
        Dim dtBankCredit As DataTable
        Dim strSQL As String = ""
        Dim CreditLimit As Decimal = 0.0
        Try
            'strSQL = " Select Sum(BankCredit.CreditLimit+BankCredit.ShippedAmount) As CreditLimit From (Select (IsNull(CreditLimit, 0) -IsNull(LC.Amount, 0)) As CreditLimit, 0 As ShippedAmount FROM tblLCBankCreditLimit " & _
            '         " LEFT OUTER JOIN (Select Sum(IsNull(LCAmount, 0)-IsNull(PaidAmount, 0)) As Amount, Bank From tblLetterOfCredit Group By Bank) As LC ON tblLCBankCreditLimit.BankName = LC.Bank " & _
            '         " Where BankName ='" & BankName & "' " & _
            '         " UNION " & _
            '         " Select 0 As CreditLimit, Sum(IsNull(Detail.Qty, 0)*IsNull(Detail.Price, 0)) As ShippedAmount  FROM ReceivingNoteMasterTable As Receiving " & _
            '         " INNER JOIN ReceivingNoteDetailTable As Detail ON Receiving.ReceivingNoteId = Detail.ReceivingNoteId Where Receiving.LCId IN (Select LCdoc_Id from tblLetterOfCredit Where Bank ='" & BankName & "')) As BankCredit "
            strSQL = "SELECT SUM((IsNull(tblLetterOfCredit.CostOfMaterial,0) + IsNull(tblLCBankCreditLimit.CreditLimit,0)) - IsNull(tblLetterOfCredit.LCAmount,0)) AS Creditlimit FROM tblLetterOfCredit LEFT OUTER JOIN tblLCBankCreditLimit ON tblLetterOfCredit.Bank = tblLCBankCreditLimit.BankName where tblLetterOfCredit.Bank = '" & BankName & "'"
            dtBankCredit = UtilityDAL.GetDataTable(strSQL)
            If dtBankCredit.Rows.Count Then
                CreditLimit = Val(dtBankCredit.Rows(0).Item(0).ToString)
                End If
            Return CreditLimit
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
