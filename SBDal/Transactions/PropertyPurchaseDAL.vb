Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class PropertyPurchaseDAL


    Dim SerialNo As String = ""
    Dim ProPurchaseId As Integer = 0
    Dim VoucherId As Integer = 0
    Dim VID As Integer = 0
    Function Add(ByVal objModel As PropertyPurchaseBE) As Boolean
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
    Function Add(ByVal objModel As PropertyPurchaseBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            SerialNo = objModel.SerialNo
            strSQL = "insert into  PropertyPurchase (Title, PlotNo, PlotSize, Block, Sector, Location, City, SerialNo, SellerAccountId, Price, Remarks, PropertyType, CellNo, VoucherNo, PurchaseDate) values (N'" & objModel.Title.Replace("'", "''") & "', N'" & objModel.PlotNo.Replace("'", "''") & "', N'" & objModel.PlotSize.Replace("'", "''") & "', N'" & objModel.Block.Replace("'", "''") & "', N'" & objModel.Sector.Replace("'", "''") & "', N'" & objModel.Location.Replace("'", "''") & "', N'" & objModel.City.Replace("'", "''") & "', N'" & objModel.SerialNo.Replace("'", "''") & "', N'" & objModel.SellerAccountId & "', N'" & objModel.Price & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.PropertyType & "', N'" & objModel.CellNo & "', N'" & objModel.VoucherNo & "', N'" & objModel.PropertyDate.ToString("yyyy-MM-dd hh:mm:ss") & "') Select @@Identity"
            ProPurchaseId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Purchase"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function AddVoucher(ByVal objModel As PropertyPurchaseBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,6,N'" & objModel.VoucherNo & "',N'" & objModel.PropertyDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmProPurchase',N'" & objModel.VoucherNo & "',N'Item: " & objModel.Title & " is purchased at Price: " & objModel.Price & " DocNo: " & objModel.SerialNo & " Remarks: " & objModel.Remarks & "') Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            AddVoucherDetail(objModel, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddVoucherDetail(ByVal objModel As PropertyPurchaseBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            'Debit Property Purchase Account
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & VoucherId & ",1," & objModel.PurchaseId & "," & objModel.Price & ",0,N'Item: " & objModel.Title & " Price: " & objModel.Price & " DocNo: " & objModel.SerialNo & " Remarks: " & objModel.Remarks & "'," & objModel.Price & ",0,1,1,1,1," & objModel.Cost_CenterId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Credit Seller Account
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CostCenterID) Values(" & VoucherId & ",1," & objModel.SellerAccountId & ",0," & objModel.Price & ",N'Item: " & objModel.Title & " Price: " & objModel.Price & " DocNo: " & objModel.SerialNo & " Remarks: " & objModel.Remarks & "',0," & objModel.Price & ",1,1,1,1," & objModel.Cost_CenterId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddPurchaseType(ByVal list As List(Of PurchaseTypeBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As PurchaseTypeBE In list
                str = "INSERT INTO PaymentType(PropertyPurchaseId, PaymentTypeDate, PaymentTypeName, Amount, AmountPaid) Values(" & ProPurchaseId & ", N'" & obj.PaymentTypeDate & "',N'" & obj.PaymentTypeName & "',N'" & obj.Amount & "', N'" & obj.AmountPaid & "')"
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

    Public Function AddTasks(ByVal list As List(Of TaskBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As TaskBE In list
                str = "INSERT INTO tblDefTasks(Name, FormName, Ref_No, DueDate, Completed) Values('" & obj.Name & "', 'frmProPurchase' ,'" & SerialNo & "',N'" & obj.DueDate & "', " & IIf(obj.Status = True, 1, 0) & ")"
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

    Function Update(ByVal objModel As PropertyPurchaseBE) As Boolean
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
            DeleteVoucherDetail(objModel, trans)
            DeleteVoucher(objModel, trans)
            AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As PropertyPurchaseBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            SerialNo = objModel.SerialNo
            ProPurchaseId = objModel.PropertyPurchaseId
            strSQL = "update PropertyPurchase set Title= N'" & objModel.Title.Replace("'", "''") & "', PlotNo= N'" & objModel.PlotNo.Replace("'", "''") & "', PlotSize= N'" & objModel.PlotSize.Replace("'", "''") & "', Block= N'" & objModel.Block.Replace("'", "''") & "', Sector= N'" & objModel.Sector.Replace("'", "''") & "', SerialNo= N'" & objModel.SerialNo.Replace("'", "''") & "', SellerAccountId= N'" & objModel.SellerAccountId & "', Price= N'" & objModel.Price & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', PropertyType= N'" & objModel.PropertyType & "' , Location= N'" & objModel.Location & "' , City= N'" & objModel.City & "' , CellNo= N'" & objModel.CellNo & "' , VoucherNo= N'" & objModel.VoucherNo & "', PurchaseDate = N'" & objModel.PropertyDate.ToString("yyyy-MM-dd hh:mm:ss") & "' where PropertyPurchaseId=" & objModel.PropertyPurchaseId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Purchase"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdatePurchaseType(ByVal list As List(Of PurchaseTypeBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As PurchaseTypeBE In list
                str = "If Exists(Select ISNULL(PaymentTypeId, 0) as PaymentTypeId From PaymentType Where PaymentTypeId=" & obj.PaymentTypeId & ") Update PaymentType Set PropertyPurchaseId =" & ProPurchaseId & ", PaymentTypeDate=N'" & obj.PaymentTypeDate & "',PaymentTypeName =N'" & obj.PaymentTypeName & "', Amount=N'" & obj.Amount & "',AmountPaid =N'" & obj.AmountPaid & "'  WHERE PaymentTypeId = " & obj.PaymentTypeId & "" _
                 & " Else INSERT INTO PurchaseType(PropertyPurchaseId, PaymentTypeDate, PaymentTypeName, Amount, AmountPaid) Values(" & ProPurchaseId & ", N'" & obj.PaymentTypeDate & "',N'" & obj.PaymentTypeName & "',N'" & obj.Amount & "', N'" & obj.AmountPaid & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Public Function UpdateTasks(ByVal list As List(Of TaskBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As TaskBE In list
                str = "If Exists(Select ISNULL(TaskId, 0) as TaskId From tblDefTasks Where TaskId=" & obj.TaskId & ") Update tblDefTasks Set Name =N'" & obj.Name & "', FormName = 'frmProPurchase', Ref_No =N'" & SerialNo & "',DueDate =N'" & obj.DueDate & "', Completed = " & IIf(obj.Status = True, 1, 0) & "  WHERE TaskId = " & obj.TaskId & "" _
                 & " Else INSERT INTO tblDefTasks(Name, FormName, Ref_No, DueDate, Completed) Values('" & obj.Name & "', 'frmProPurchase' ,'" & SerialNo & "',N'" & obj.DueDate & "', " & IIf(obj.Status = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Function Delete(ByVal objModel As PropertyPurchaseBE) As Boolean
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
    Function Delete(ByVal objModel As PropertyPurchaseBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from PropertyPurchase  where PropertyPurchaseId= " & objModel.PropertyPurchaseId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Purchase"
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DeleteVoucher(ByVal objModel As PropertyPurchaseBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucher where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function DeleteVoucherDetail(ByVal objModel As PropertyPurchaseBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblVoucherDetail where voucher_id= '" & VID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PropertyPurchaseId, Title, PlotNo, PlotSize, Block, Sector, SerialNo, SellerAccountId, Price, Remarks, Location, City, PropertyType, VoucherNo from PropertyPurchase Order By 1 Desc "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            'strSQL = " SELECT PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, ISNULL(PropertyPurchase.PlotSize,'') AS PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, tblVoucher.voucher_no FROM tblVoucher RIGHT OUTER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id RIGHT OUTER JOIN PropertyPurchase ON tblVoucherDetail.coa_detail_id = PropertyPurchase.SellerAccountId  where PropertyPurchaseId=" & ID & "AND tblVoucher.voucher_no Like '%PP-%' "
            strSQL = " SELECT PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, ISNULL(PropertyPurchase.PlotSize,'') AS PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, PropertyPurchase.VoucherNo AS voucher_no, PurchaseDate FROM PropertyPurchase  WHERE PropertyPurchaseId=" & ID & ""

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Saba Shabbir: TFS2558 Added a new function which will make record ediable according to seller Button
    ''' </summary>
    ''' <param name="docNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateRecord(ByVal docNo As String) As DataTable

        Dim strSQL As String = String.Empty
        Try
            'str = "select * from PropertySales where SerialNo = " & docNo
            ''Below line is commented on 10-09-2018
            'strSQL = "  SELECT top 1 PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, PropertyPurchase.PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, tblVoucher.voucher_no FROM tblVoucher RIGHT OUTER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id RIGHT OUTER JOIN PropertyPurchase ON tblVoucherDetail.coa_detail_id = PropertyPurchase.SellerAccountId  where SerialNo='" & docNo & "' AND tblVoucher.voucher_no Like '%PP-%' order by 1 desc"
            strSQL = "  SELECT PropertyPurchase.PropertyPurchaseId, PropertyPurchase.Title, PropertyPurchase.PlotNo, PropertyPurchase.PlotSize, PropertyPurchase.Block, PropertyPurchase.Sector, PropertyPurchase.SerialNo, PropertyPurchase.SellerAccountId, PropertyPurchase.Price, PropertyPurchase.Remarks, PropertyPurchase.Location, PropertyPurchase.City, PropertyPurchase.PropertyType, PropertyPurchase.CellNo, PropertyPurchase.VoucherNo as voucher_no FROM PropertyPurchase WHERE PropertyPurchase.SerialNo ='" & docNo & "' "

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
