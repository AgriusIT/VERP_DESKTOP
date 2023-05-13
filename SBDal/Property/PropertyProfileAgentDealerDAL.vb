Imports SBModel
Imports System
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class PropertyProfileAgentDealerDAL
    Dim detailId As Integer
    Dim VoucherId As Integer
    Dim VID As Integer = 0
    Dim costCenterId As Integer
    Dim PropertyProfileId As Integer = 0
    Function Add(ByVal objModel As PropertyProfileAgentDealerBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            ''Below row is commented against TASK TFS3672 ON 26-06-2018
            'AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As PropertyProfileAgentDealerBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If objModel.AgentId > 0 Then
                strSQL = "INSERT INTO PropertyProfileAgent (PropertyProfileId, AgentId, Activity, CommissionAmount, Remarks, VoucherNo) VALUES (" & objModel.PropertyProfileId & ", " & objModel.AgentId & ", N'" & objModel.Activity.Replace("'", "''") & "', '" & objModel.CommissionAmount & "', N'" & objModel.Remarks.Replace("'", "''") & "','" & objModel.VoucherNo & "') Select @@Identity"
                objModel.PropertyProfileAgentId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            End If
            If objModel.DealerId > 0 Then
                strSQL = "INSERT INTO PropertyProfileDealer (PropertyProfileId, DealerId, Activity, CommissionAmount, Remarks, VoucherNo) VALUES (" & objModel.PropertyProfileId & ", " & objModel.DealerId & ", N'" & objModel.Activity.Replace("'", "''") & "', '" & objModel.CommissionAmount & "', N'" & objModel.Remarks.Replace("'", "''") & "','" & objModel.VoucherNo & "') Select @@Identity"
                objModel.PropertyProfileDealerId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            End If
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Activity
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddVoucher(ByVal objModel As PropertyProfileAgentDealerBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If objModel.AgentId > 0 Then
                strSQL = "Select coa_detail_id from vwCOADetail where detail_title = '" & objModel.name & "'"
                detailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            End If
            If objModel.DealerId > 0 Then
                strSQL = "Select coa_detail_id from vwCOADetail where detail_title = '" & objModel.name & "'"
                detailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            End If


            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.VoucherDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmPropertyProfile',N'" & objModel.VoucherNo & "',N'" & objModel.Remarks & "') Select @@Identity"
            'strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'PS-18-00009',N'3/1/2018 3:07:07 PM',1,N'frmProPurchase',N'PS-18-00009',N'Item: '" & objModel.Title & " 'Price: '" & objModel.Price & " 'Remarks:'" & oobjModel.Remarks & "')' Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            AddVoucherDetail(objModel, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function AddVoucherDetail(ByVal objModel As PropertyProfileAgentDealerBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            'Debit Buyer Account
            strSQL = " select CostCenterId  from PropertyProfile where PropertyProfileId = " & objModel.PropertyProfileId & ""
            costCenterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            Dim strSQL1 As String
            Dim DocNo As String
            strSQL1 = " select DocNo  from PropertyProfile where PropertyProfileId = " & objModel.PropertyProfileId & ""
            Dim dtDOcNo As DataTable
            dtDOcNo = UtilityDAL.GetDataTable(strSQL1)
            If dtDOcNo.Rows.Count > 0 Then
                DocNo = dtDOcNo.Rows(0).Item("DocNo").ToString
            End If
            If objModel.AgentId > 0 Then
                Dim str As String
                str = "select Name from Agent where AgentId = " & objModel.AgentId & ""
                Dim dtAgent As DataTable
                dtAgent = UtilityDAL.GetDataTable(str)
                Dim AgentName As String
                If dtAgent.Rows.Count > 0 Then
                    AgentName = dtAgent.Rows(0).Item("Name").ToString
                End If
                strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID)" & _
                    "Values(" & VoucherId & ",1," & detailId & ",0," & objModel.CommissionAmount & ",N' Serial Number: " & DocNo & " Agent: " & AgentName & " PlotNo: " & objModel.PlotNo & " Remarks: " & objModel.Remarks & "',0," & objModel.CommissionAmount & ",1,1,1,1," & costCenterId & ")"

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                'Credit Property Sales Account

                strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) " & _
                    "Values(" & VoucherId & ",1," & objModel.CommissionAccount & "," & objModel.CommissionAmount & ",0,N' Serial Number: " & DocNo & " Agent: " & AgentName & " PlotNo: " & objModel.PlotNo & " Remarks: " & objModel.Remarks & "'," & objModel.CommissionAmount & ",0,1,1,1,1," & costCenterId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            If objModel.DealerId > 0 Then
                Dim str As String
                str = "select Name from Dealer where DealerId = " & objModel.DealerId & ""
                Dim dtDealer As DataTable
                dtDealer = UtilityDAL.GetDataTable(str)
                Dim DealerName As String
                If dtDealer.Rows.Count > 0 Then
                    DealerName = dtDealer.Rows(0).Item("Name").ToString
                End If
                strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) " & _
                    "Values(" & VoucherId & ",1," & detailId & ",0," & objModel.CommissionAmount & ",N' Serial Number: " & DocNo & " Dealer: " & DealerName & " PlotNo: " & objModel.PlotNo & " Remarks: " & objModel.Remarks & " ',0," & objModel.CommissionAmount & ",1,1,1,1," & costCenterId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                'Credit Property Sales Account
                strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) " & _
                    "Values(" & VoucherId & ",1," & objModel.CommissionAccount & "," & objModel.CommissionAmount & ",0,N' Serial Number: " & DocNo & " PlotNo: " & objModel.PlotNo & " Dealer: " & DealerName & "  Remarks: " & objModel.Remarks & " '," & objModel.CommissionAmount & ",0,1,1,1,1," & costCenterId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucher(ByVal objModel As PropertyProfileAgentDealerBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            Dim str As String = "Select voucher_id from tblVoucher where voucher_no ='" & objModel.VoucherNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                VID = dt.Rows(0).Item("voucher_id")

                strSQL = "Delete from tblVoucherDetail where voucher_id= '" & VID & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = "Delete from tblVoucher where voucher_id= " & VID & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS3672
    ''' </summary>
    ''' <param name="objModel"></param>
    ''' <param name="trans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteVoucher(ByVal objModelLiST As List(Of PropertyProfileAgentDealerBE), trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            For Each objModel As PropertyProfileAgentDealerBE In objModelLiST

                PropertyProfileId = objModel.PropertyProfileId
                Dim str As String = "Select voucher_id from tblVoucher where voucher_no ='" & objModel.VoucherNo & "'"
                Dim dt As DataTable = UtilityDAL.GetDataTable(str, trans)
                If dt.Rows.Count > 0 AndAlso objModel.VoucherNo.Length > 0 Then
                    VID = dt.Rows(0).Item("voucher_id")
                    strSQL = "Delete from tblVoucherDetail where voucher_id= '" & VID & "'"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    strSQL = "Delete from tblVoucher where voucher_id= " & VID & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next

            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DeleteVoucherForQuickDeal(ByVal _PropertyProfileId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            ''Start TFS4496

            Dim strQuickDeal As String = "Select voucher_id from tblVoucher where voucher_id in (Select QuickDealVoucherId from PropertyProfile where PropertyProfileId = " & _PropertyProfileId & " and QuickDealVoucherId <> null)"
            Dim dtQuickDeal As DataTable = UtilityDAL.GetDataTable(strQuickDeal, trans)
            If dtQuickDeal.Rows.Count > 0 Then
                VID = dtQuickDeal.Rows(0).Item("voucher_id")
                strSQL = "Delete from tblVoucherDetail where voucher_id= '" & VID & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "Delete from tblVoucher where voucher_id= " & VID & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            ''End TFS4496
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As PropertyProfileAgentDealerBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            ''Below two rows are commented against TASK TFS3672 ON 26-06-2018
            'DeleteVoucher(objModel, trans)
            'AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As PropertyProfileAgentDealerBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If objModel.AgentId > 0 Then
                strSQL = "UPDATE PropertyProfileAgent SET PropertyProfileId= " & objModel.PropertyProfileId & ", AgentId= " & objModel.AgentId & ", Activity= N'" & objModel.Activity.Replace("'", "''") & "', CommissionAmount= '" & objModel.CommissionAmount & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "' WHERE PropertyProfileAgentId=" & objModel.PropertyProfileAgentId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            If objModel.DealerId > 0 Then
                strSQL = "UPDATE PropertyProfileDealer SET PropertyProfileId= " & objModel.PropertyProfileId & ", DealerId= " & objModel.DealerId & ", Activity= N'" & objModel.Activity.Replace("'", "''") & "', CommissionAmount= '" & objModel.CommissionAmount & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "' WHERE PropertyProfileDealerId=" & objModel.PropertyProfileDealerId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Activity
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As PropertyProfileAgentDealerBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            ''Below two rows are commented against TASK TFS3672 ON 26-06-2018
            ''DeleteVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As PropertyProfileAgentDealerBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            If objModel.AgentId > 0 Then
                strSQL = "DELETE FROM PropertyProfileAgent  WHERE PropertyProfileAgentId= " & objModel.PropertyProfileAgentId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            If objModel.DealerId > 0 Then
                strSQL = "DELETE FROM PropertyProfileDealer WHERE PropertyProfileDealerId= " & objModel.PropertyProfileDealerId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            'objModel.ActivityLog.UserID = LoginUser.LoginUserId
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = objModel.Activity
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll(ProfileId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try

            'strSQL = "SELECT PropertyProfileAgent.PropertyProfileAgentId AS Id, PropertyProfileAgent.PropertyProfileId, PropertyProfileAgent.AgentId AS AccountId, Agent.Name, 'Agent' AS Type, PropertyProfileAgent.Activity, PropertyProfileAgent.CommissionAmount, PropertyProfileAgent.Remarks, tblVoucherDetail.voucher_id As VoucherId FROM PropertyProfileAgent LEFT OUTER JOIN Agent ON PropertyProfileAgent.AgentId = Agent.AgentId  where PropertyProfileAgent.PropertyProfileId=" & ProfileId & " UNION ALL SELECT PropertyProfileDealer.PropertyProfileDealerId AS Id, PropertyProfileDealer.PropertyProfileId, PropertyProfileDealer.DealerId AS AccountId, Dealer.Name, 'Dealer' AS Type, PropertyProfileDealer.Activity, PropertyProfileDealer.CommissionAmount, PropertyProfileDealer.Remarks FROM PropertyProfileDealer LEFT OUTER JOIN Dealer ON PropertyProfileDealer.DealerId = Dealer.DealerId where PropertyProfileDealer.PropertyProfileId=" & ProfileId

            'strSQL = "SELECT PropertyProfileAgent.PropertyProfileAgentId AS Id, PropertyProfileAgent.PropertyProfileId, PropertyProfileAgent.AgentId AS AccountId, Agent.Name, 'Agent' AS Type, PropertyProfileAgent.Activity, PropertyProfileAgent.CommissionAmount, PropertyProfileAgent.Remarks, PropertyProfileAgent.VoucherNo FROM PropertyProfileAgent LEFT OUTER JOIN tblVoucherDetail INNER JOIN Agent ON tblVoucherDetail.coa_detail_id = Agent.coa_detail_id ON PropertyProfileAgent.AgentId = Agent.AgentId where PropertyProfileAgent.PropertyProfileId=" & ProfileId & " UNION ALL SELECT PropertyProfileDealer.PropertyProfileDealerId AS Id, PropertyProfileDealer.PropertyProfileId, PropertyProfileDealer.DealerId AS AccountId, Dealer.Name, 'Dealer' AS Type, PropertyProfileDealer.Activity, PropertyProfileDealer.CommissionAmount, PropertyProfileDealer.Remarks, PropertyProfileDealer.VoucherNo FROM PropertyProfileDealer LEFT OUTER JOIN tblVoucherDetail INNER JOIN Dealer ON tblVoucherDetail.coa_detail_id = Dealer.coa_detail_id ON PropertyProfileDealer.DealerId = Dealer.DealerId where PropertyProfileDealer.PropertyProfileId=" & ProfileId
            strSQL = "SELECT PropertyProfileAgent.PropertyProfileAgentId AS Id, PropertyProfileAgent.PropertyProfileId, PropertyProfileAgent.AgentId AS AccountId, Agent.Name, 'Agent' AS Type, PropertyProfileAgent.Activity, PropertyProfileAgent.CommissionAmount, PropertyProfileAgent.Remarks, PropertyProfileAgent.VoucherNo FROM PropertyProfileAgent INNER JOIN Agent ON PropertyProfileAgent.AgentId = Agent.AgentId where PropertyProfileAgent.PropertyProfileId=" & ProfileId & " UNION ALL SELECT PropertyProfileDealer.PropertyProfileDealerId AS Id, PropertyProfileDealer.PropertyProfileId, PropertyProfileDealer.DealerId AS AccountId, Dealer.Name, 'Dealer' AS Type, PropertyProfileDealer.Activity, PropertyProfileDealer.CommissionAmount, PropertyProfileDealer.Remarks, PropertyProfileDealer.VoucherNo FROM PropertyProfileDealer INNER JOIN Dealer  ON PropertyProfileDealer.DealerId = Dealer.DealerId where PropertyProfileDealer.PropertyProfileId=" & ProfileId

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            'strSQL = " SELECT PropertyProfileAgent.PropertyProfileAgentId, PropertyProfileAgent.PropertyProfileId, PropertyProfileAgent.AgentId, PropertyProfileAgent.Activity, PropertyProfileAgent.CommissionAmount, PropertyProfileAgent.Remarks, tblVoucher.voucher_no FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id RIGHT OUTER JOIN PropertyProfileAgent LEFT OUTER JOIN Agent ON PropertyProfileAgent.AgentId = Agent.AgentId ON tblVoucherDetail.coa_detail_id = Agent.coa_detail_id WHERE PropertyProfileAgent.PropertyProfileAgentId =" & ID
            strSQL = "SELECT PropertyProfileAgent.PropertyProfileAgentId, PropertyProfileAgent.PropertyProfileId, PropertyProfileAgent.AgentId, PropertyProfileAgent.Activity, PropertyProfileAgent.CommissionAmount, PropertyProfileAgent.Remarks, PropertyProfileAgent.VoucherNo, Agent.Name FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id RIGHT OUTER JOIN PropertyProfileAgent LEFT OUTER JOIN Agent ON PropertyProfileAgent.AgentId = Agent.AgentId ON tblVoucherDetail.coa_detail_id = Agent.coa_detail_id WHERE PropertyProfileAgent.PropertyProfileAgentId =" & ID
            'strSQL = " Select PropertyProfileDealerId, PropertyProfileId, DealerId, Activity, CommissionAmount, Remarks from PropertyProfileDealer  where PropertyProfileDealerId=" & ID

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetByDealerId(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " SELECT Dealer.DealerId, tblVoucher.voucher_no, Dealer.coa_detail_id, PropertyProfileDealer.CommissionAmount, PropertyProfileDealer.Activity, PropertyProfileDealer.DealerId AS Expr1, PropertyProfileDealer.PropertyProfileId, PropertyProfileDealer.PropertyProfileDealerId, PropertyProfileDealer.Remarks FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN Dealer ON tblVoucherDetail.coa_detail_id = Dealer.coa_detail_id RIGHT OUTER JOIN PropertyProfileDealer ON Dealer.DealerId = PropertyProfileDealer.DealerId WHERE PropertyProfileDealer.PropertyProfileDealerId = " & ID

            'strSQL = " Select PropertyProfileDealerId, PropertyProfileId, DealerId, Activity, CommissionAmount, Remarks from PropertyProfileDealer  where PropertyProfileDealerId=" & ID

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetVoucherDate(ByVal VoucherNo As String) As DateTime
        Dim strSQL As String = String.Empty
        Dim VoucherDate As DateTime
        Try
            strSQL = " SELECT voucher_date FROM tblVoucher WHERE voucher_no ='" & VoucherNo & "'"

            'strSQL = " Select PropertyProfileDealerId, PropertyProfileId, DealerId, Activity, CommissionAmount, Remarks from PropertyProfileDealer  where PropertyProfileDealerId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            If dt.Rows.Count > 0 Then
                VoucherDate = dt.Rows(0).Item(0)
            Else
                VoucherDate = Now
            End If
            Return VoucherDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS3672
    ''' </summary>
    ''' <param name="objModel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddVoucher(ByVal objModel As List(Of PropertyProfileAgentDealerBE), ByVal _PropertyProfileId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            AddVoucher(objModel, _PropertyProfileId, trans)
            ''Below row is commented against TASK TFS3672 ON 26-06-2018
            'AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddVoucher(ByVal objModelList As List(Of PropertyProfileAgentDealerBE), ByVal _PropertyProfileId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Dim PropertyProfileId As Integer = 0
        Try
            PropertyProfileId = _PropertyProfileId
            For Each objModel As PropertyProfileAgentDealerBE In objModelList
                PropertyProfileId = objModel.PropertyProfileId
                objModel.VoucherNo = GetDocumentNo(trans)
                If objModel.AgentId > 0 Then
                    strSQL = "Select coa_detail_id from vwCOADetail where detail_title = '" & objModel.name & "'"
                    detailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                    ''PropertyProfileAgent
                    strSQL = "Update  PropertyProfileAgent SET VoucherNo = '" & objModel.VoucherNo & "' WHERE PropertyProfileAgentId = " & objModel.PropertyProfileAgentId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
                If objModel.DealerId > 0 Then
                    strSQL = "Select coa_detail_id from vwCOADetail where detail_title = '" & objModel.name & "'"
                    detailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                    strSQL = "Update  PropertyProfileDealer SET VoucherNo = '" & objModel.VoucherNo & "' WHERE PropertyProfileDealerId = " & objModel.PropertyProfileDealerId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
                strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.VoucherDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmPropertyProfile',N'" & objModel.VoucherNo & "',N'" & objModel.Remarks & "') Select @@Identity"
                'strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'PS-18-00009',N'3/1/2018 3:07:07 PM',1,N'frmProPurchase',N'PS-18-00009',N'Item: '" & objModel.Title & " 'Price: '" & objModel.Price & " 'Remarks:'" & oobjModel.Remarks & "')' Select @@Identity"
                VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                AddVoucherDetail(objModel, trans)
            Next
            ''
            'objModelList.Find(Function())
            strSQL = "Update PropertyProfile SET IsDealCompleted = 1 WHERE PropertyProfileId = " & PropertyProfileId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Adding voucher for Deal completion
    ''' </summary>
    ''' <param name="objModel"></param>
    ''' <param name="_PropertyProfileId"></param>
    ''' <returns></returns>
    ''' <remarks>TFS4496 : Ayesha Rehman :  09-11-2018</remarks>
    Function AddVoucherForDealCompletion(ByVal VoucherNo As String, ByVal _PropertyProfileId As Integer, ByVal SalesAmount As Double, ByVal PurchaseAmount As Double) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim BuyerAccount As Integer = 0
            Dim SellerAccount As Integer = 0
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & VoucherNo & "',N'" & Date.Now.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmPropertyProfile',N'" & VoucherNo & "',N'Deal Completion Voucher') Select @@Identity"
            'strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'PS-18-00009',N'3/1/2018 3:07:07 PM',1,N'frmProPurchase',N'PS-18-00009',N'Item: '" & objModel.Title & " 'Price: '" & objModel.Price & " 'Remarks:'" & oobjModel.Remarks & "')' Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            strSQL = " select CostCenterId  from PropertyProfile where PropertyProfileId = " & _PropertyProfileId & ""
            costCenterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            Dim strSQL1 As String
            Dim DocNoForCompletion As String
            strSQL1 = " select DocNo  from PropertyProfile where PropertyProfileId = " & _PropertyProfileId & ""
            Dim dtDOcNo As DataTable
            dtDOcNo = UtilityDAL.GetDataTable(strSQL1)
            If dtDOcNo.Rows.Count > 0 Then
                DocNoForCompletion = dtDOcNo.Rows(0).Item("DocNo").ToString
            End If
            strSQL = "Select BuyerAccountId  from PropertySales where SerialNo = '" & DocNoForCompletion & "'"
            BuyerAccount = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            strSQL = "Select SellerAccountId  from PropertyPurchase where SerialNo = '" & DocNoForCompletion & "'"
            SellerAccount = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'Debit Property Purchase Account
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) " & _
                  "Values(" & VoucherId & ",1," & BuyerAccount & ",0," & PurchaseAmount & ",N' Serial Number: " & DocNoForCompletion & "',0," & PurchaseAmount & ",1,1,1,1," & costCenterId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Credit Property Sales Account
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) " & _
                "Values(" & VoucherId & ",1," & SellerAccount & "," & SalesAmount & ",0,N' Serial Number: " & DocNoForCompletion & "'," & SalesAmount & ",0,1,1,1,1," & costCenterId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Update PropertyProfile SET IsDealCompleted = 1 WHERE PropertyProfileId = " & _PropertyProfileId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Update PropertyProfile SET QuickDealVoucherId = " & VoucherId & " WHERE PropertyProfileId = " & _PropertyProfileId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' TASK TFS3672
    ''' </summary>
    ''' <param name="objModel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CancelDeal(ByVal objModel As List(Of PropertyProfileAgentDealerBE), ByVal PropertyProfileId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            PropertyProfileId = PropertyProfileId
            DeleteVoucher(objModel, trans)
            DeleteVoucherForQuickDeal(PropertyProfileId, trans)
            CancelDeal(PropertyProfileId, trans)
            trans.Commit()
            PropertyProfileId = 0
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS3672
    ''' </summary>
    ''' <param name="PropertyProfileId"></param>
    ''' <param name="trans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CancelDeal(ByVal PropertyProfileId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Query As String = String.Empty
        Try
            Query = "Update PropertyProfile SET IsDealCompleted = 0 WHERE PropertyProfileId = " & PropertyProfileId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Query)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetDocumentNo(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim DocNo As String = String.Empty
        Try
            DocNo = GetNextDocNo("JV-" & Format(Date.Now, "yy") & Date.Now.Month.ToString("00"), 4, "tblVoucher", "voucher_no", , trans)
            Return DocNo
        Catch ex As Exception
            Throw ex
            Return ""
        End Try
    End Function

    Public Function GetNextDocNo(ByVal Prefix As String, ByVal Length As Integer, ByVal tableName As String, ByVal FieldName As String, Optional ByVal TotalLength As Integer = 0, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim str As String = 0
            'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim strSql As String
            If Prefix = "" Then
                strSql = "select  isnull(max(convert(integer," & tableName & "." & FieldName & ")),0)+1 from " & tableName & " "
            Else
                strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
                If TotalLength > 0 Then
                    strSql = strSql & " and len(" & tableName & "." & FieldName & ") <= " & TotalLength
                End If
            End If
            Dim dt As New DataTable
            'Dim adp As New OleDbDataAdapter
            'adp = New OleDbDataAdapter(strSql, New OleDbConnection(Con.ConnectionString))
            'adp.Fill(dt)
            'dt.AcceptChanges()
            dt = UtilityDAL.GetDataTable(strSql, trans)
            If dt.Rows.Count > 0 Then
                str = dt.Rows(0).Item(0).ToString
            Else
                Return "Error"
            End If
            If Prefix = "" Then
                Return str.PadLeft(Length, "0")
            End If

            Return Prefix & "-" & str.PadLeft(Length, "0")
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class