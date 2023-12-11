Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class PropertySalesDAL
    Dim SerialNo As String = ""
    Dim ProSalesId As Integer = 0
    Dim VoucherId As Integer = 0
    Dim AdjustedVoucherId As Integer = 0
    Dim VID As Integer = 0
    Dim AdjustedVID As Integer = 0
    Function Add(ByVal objModel As PropertySalesBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As PropertySalesBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            SerialNo = objModel.SerialNo
            strSQL = "insert into  PropertySales (Title, PlotNo, PlotSize, Block, Sector, Location, City, SerialNo, BuyerAccountId, Price, Remarks, PropertyType, CellNo, VoucherNo, AdjustedVoucherNo, SalesDate) values (N'" & objModel.Title.Replace("'", "''") & "', N'" & objModel.PlotNo.Replace("'", "''") & "', N'" & objModel.PlotSize.Replace("'", "''") & "', N'" & objModel.Block.Replace("'", "''") & "', N'" & objModel.Sector.Replace("'", "''") & "', N'" & objModel.Location.Replace("'", "''") & "', N'" & objModel.City.Replace("'", "''") & "', N'" & objModel.SerialNo.Replace("'", "''") & "', N'" & objModel.BuyerAccountId & "', N'" & objModel.Price & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.PropertyType.Replace("'", "''") & "', N'" & objModel.CellNo.Replace("'", "''") & "', N'" & objModel.VoucherNo.Replace("'", "''") & "', N'" & objModel.AdjustedVoucherNo.Replace("'", "''") & "', N'" & objModel.PropertyDate.ToString("yyyy-MM-dd hh:mm:ss") & "') Select @@Identity"
            ProSalesId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'Saba Shabbir TFS2564 when inventory sale then it will not diplayed in Property inventory,update PropertyItem and set active false according to = PropertyItemId 
            strSQL = "update PropertyItem set Active=0 where PropertyItemId=" & objModel.ItemId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Sales"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function AddVoucher(ByVal objModel As PropertySalesBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'" & objModel.VoucherNo & "',N'" & objModel.PropertyDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmProSales',N'" & objModel.VoucherNo & "',N'Item:  " & objModel.Title & " Price: " & objModel.Price & " Remarks: " & objModel.Remarks & "') Select @@Identity"
            'strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,7,N'PS-18-00009',N'3/1/2018 3:07:07 PM',1,N'frmProPurchase',N'PS-18-00009',N'Item: '" & objModel.Title & " 'Price: '" & objModel.Price & " 'Remarks:'" & oobjModel.Remarks & "')' Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            ''TASK TFS4412
            If objModel.SellerPrice > 0 Then
                strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & objModel.AdjustedVoucherNo & "',N'" & objModel.PropertyDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmProSales',N'" & objModel.AdjustedVoucherNo & "',N'Item:  " & objModel.Title & " Price: " & objModel.Price & " Remarks: " & objModel.Remarks & "') Select @@Identity"
                AdjustedVoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            End If
            ''END TASK TFS4412
            AddVoucherDetail(objModel, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function AddVoucherDetail(ByVal objModel As PropertySalesBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            'Debit Buyer Account
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & VoucherId & ",1," & objModel.BuyerAccountId & "," & objModel.Price & ",0,N'Item: " & objModel.Title & " Price: " & objModel.Price & " PlotNo: " & objModel.PlotNo & " DocNo: " & objModel.SerialNo & " Remarks: " & objModel.Remarks & "'," & objModel.Price & ",0,1,1,1,1," & objModel.Cost_CenterId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Credit Property Sales Account
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & VoucherId & ",1," & objModel.SalesId & ",0," & objModel.Price & ",N'Item: " & objModel.Title & " Price: " & objModel.Price & " PlotNo: " & objModel.PlotNo & " DocNo: " & objModel.SerialNo & " Remarks: " & objModel.Remarks & "',0," & objModel.Price & ",1,1,1,1," & objModel.Cost_CenterId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''TASK TFS4412
            If objModel.SellerPrice > 0 Then
                strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & AdjustedVoucherId & ",1," & objModel.CostOfSalesAccountId & "," & objModel.SellerPrice & ",0,N'Item: " & objModel.Title & " Price: " & objModel.SellerPrice & " PlotNo: " & objModel.PlotNo & " DocNo: " & objModel.SerialNo & " Remarks: " & objModel.Remarks & "'," & objModel.SellerPrice & ",0,1,1,1,1," & objModel.Cost_CenterId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & AdjustedVoucherId & ",1," & objModel.PurchaseAccountId & ",0," & objModel.SellerPrice & ",N'Item: " & objModel.Title & " Price: " & objModel.SellerPrice & " PlotNo: " & objModel.PlotNo & " DocNo: " & objModel.SerialNo & " Remarks: " & objModel.Remarks & "',0," & objModel.SellerPrice & ",1,1,1,1," & objModel.Cost_CenterId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            ''END TASK TFS4412

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddPurchaseType(ByVal list As List(Of PurchaseTypeSalesBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As PurchaseTypeSalesBE In list
                str = "INSERT INTO PaymentType(PropertySalesId, PaymentTypeDate, PaymentTypeName, Amount, AmountPaid) Values(" & ProSalesId & ", N'" & obj.PaymentTypeDate & "',N'" & obj.PaymentTypeName & "',N'" & obj.Amount & "', N'" & obj.AmountPaid & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function AddTasks(ByVal list As List(Of TaskSalesBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As TaskSalesBE In list
                str = "INSERT INTO tblDefTasks(Name, FormName, Ref_No, DueDate, Completed) Values('" & obj.Name & "', 'frmProSales', '" & SerialNo & "',N'" & obj.DueDate & "', " & IIf(obj.Status = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Function Update(ByVal objModel As PropertySalesBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            Dim str As String = "Select voucher_id from tblVoucher where voucher_no = '" & objModel.VoucherNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                VID = dt.Rows(0).Item("voucher_id")
            End If

            ''TASK TFS4412
            Dim str1 As String = "Select voucher_id from tblVoucher where voucher_no = '" & objModel.AdjustedVoucherNo & "'"
            Dim dt1 As DataTable = UtilityDAL.GetDataTable(str1)
            If dt1.Rows.Count > 0 Then
                AdjustedVID = dt1.Rows(0).Item("voucher_id")
            End If
            ''END TASK TFS4412

            DeleteVoucherDetail(objModel, trans)
            DeleteVoucher(objModel, trans)
            AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As PropertySalesBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            SerialNo = objModel.SerialNo
            ProSalesId = objModel.PropertySalesId
            strSQL = "update PropertySales set Title= N'" & objModel.Title.Replace("'", "''") & "', PlotNo= N'" & objModel.PlotNo.Replace("'", "''") & "', PlotSize= N'" & objModel.PlotSize.Replace("'", "''") & "', Block= N'" & objModel.Block.Replace("'", "''") & "', Sector= N'" & objModel.Sector.Replace("'", "''") & "', Location= N'" & objModel.Location.Replace("'", "''") & "', City= N'" & objModel.City.Replace("'", "''") & "', SerialNo= N'" & objModel.SerialNo.Replace("'", "''") & "', BuyerAccountId= N'" & objModel.BuyerAccountId & "', Price= N'" & objModel.Price & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', PropertyType= N'" & objModel.PropertyType.Replace("'", "''") & "', CellNo= N'" & objModel.CellNo.Replace("'", "''") & "' , VoucherNo= N'" & objModel.VoucherNo.Replace("'", "''") & "', AdjustedVoucherNo= N'" & objModel.AdjustedVoucherNo.Replace("'", "''") & "', SalesDate = N'" & objModel.PropertyDate.ToString("yyyy-MM-dd hh:mm:ss") & "' where PropertySalesId=" & objModel.PropertySalesId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Sales"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdatePurchaseType(ByVal list As List(Of PurchaseTypeSalesBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As PurchaseTypeSalesBE In list
                str = "If Exists(Select ISNULL(PaymentTypeId, 0) as PaymentTypeId From PaymentType Where PaymentTypeId=" & obj.PaymentTypeId & ") Update PaymentType Set PropertySalesId =" & ProSalesId & ", PaymentTypeDate=N'" & obj.PaymentTypeDate & "',PaymentTypeName =N'" & obj.PaymentTypeName & "', Amount=N'" & obj.Amount & "',AmountPaid =N'" & obj.AmountPaid & "'  WHERE PaymentTypeId = " & obj.PaymentTypeId & "" _
                 & " Else INSERT INTO PurchaseType(PropertySalesId, PaymentTypeDate, PaymentTypeName, Amount, AmountPaid) Values(" & ProSalesId & ", N'" & obj.PaymentTypeDate & "',N'" & obj.PaymentTypeName & "',N'" & obj.Amount & "', N'" & obj.AmountPaid & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Public Function UpdateTasks(ByVal list As List(Of TaskSalesBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As TaskSalesBE In list
                str = "If Exists(Select ISNULL(TaskId, 0) as TaskId From tblDefTasks Where TaskId=" & obj.TaskId & ") Update tblDefTasks Set Name =N'" & obj.Name & "', FormName = 'frmProSales', Ref_No=N'" & SerialNo & "',DueDate =N'" & obj.DueDate & "', Completed = " & IIf(obj.Status = True, 1, 0) & "  WHERE TaskId = " & obj.TaskId & "" _
                 & " Else INSERT INTO tblDefTasks(Name, FormName, Ref_No, DueDate, Completed) Values('" & obj.Name & "', 'frmProSales', '" & SerialNo & "',N'" & obj.DueDate & "', " & IIf(obj.Status = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Function Delete(ByVal objModel As PropertySalesBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As PropertySalesBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from PropertySales  where PropertySalesId= " & objModel.PropertySalesId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.UserID = LoginUser.LoginUserId
            'objModel.ActivityLog.FormCaption = "Sales"
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = objModel.Title.ToString
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucher(ByVal objModel As PropertySalesBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucher where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''TASK TFS4412
            strSQL = "Delete from tblVoucher where voucher_id= '" & AdjustedVID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''END TASK TFS4412


            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucherDetail(ByVal objModel As PropertySalesBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucherDetail where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''TASK TFS4412
            strSQL = "Delete from tblVoucherDetail where voucher_id= '" & AdjustedVID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''END TASK TFS4412
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PropertySalesId, Title, PlotNo, PlotSize, Block, Sector, Location, City, SerialNo, BuyerAccountId, Price, Remarks, PropertyType, CellNo, VoucherNo from PropertySales Order By 1 Desc "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            'strSQL = " SELECT PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, ISNULL(PropertySales.PlotSize,'') AS PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.Location, PropertySales.City, PropertySales.PropertyType, PropertySales.CellNo, tblVoucher.voucher_no FROM tblVoucher RIGHT OUTER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id RIGHT OUTER JOIN PropertySales ON tblVoucherDetail.coa_detail_id = PropertySales.buyerAccountId  where PropertySalesId=" & ID & "AND tblVoucher.voucher_no Like '%PS-%'"
            strSQL = " SELECT PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, ISNULL(PropertySales.PlotSize,'') AS PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.Location, PropertySales.City, PropertySales.PropertyType, PropertySales.CellNo, PropertySales.VoucherNo as voucher_no, PropertySales.AdjustedVoucherNo, PropertySales.SalesDate FROM PropertySales  WHERE PropertySalesId=" & ID & ""

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Saba Shabbir: TFS2558 Added a new function which will make record ediable according to Buyer button
    ''' </summary>
    ''' <param name="docNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateRecord(ByVal docNo As String) As DataTable
        Dim strSQL As String = String.Empty
        Try
            'str = "select * from PropertySales where SerialNo = " & docNo
            ''Below line is commented on 10-09-2018
            'strSQL = "SELECT top 1 PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, PropertySales.PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.Location, PropertySales.City, PropertySales.PropertyType, PropertySales.CellNo, tblVoucher.voucher_no FROM tblVoucher RIGHT OUTER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id RIGHT OUTER JOIN PropertySales ON tblVoucherDetail.coa_detail_id = PropertySales.buyerAccountId  where SerialNo='" & docNo & "' AND tblVoucher.voucher_no Like '%PS-%' order by 1 desc"
            strSQL = "SELECT PropertySales.PropertySalesId, PropertySales.Title, PropertySales.PlotNo, PropertySales.PlotSize, PropertySales.Block, PropertySales.Sector, PropertySales.SerialNo, PropertySales.BuyerAccountId, PropertySales.Price, PropertySales.Remarks, PropertySales.Location, PropertySales.City, PropertySales.PropertyType, PropertySales.CellNo, PropertySales.VoucherNo AS voucher_no FROM PropertySales WHERE PropertySales.SerialNo='" & docNo & "' "

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
