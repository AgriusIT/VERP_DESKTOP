Imports SBModel
Imports System.Data.SqlClient
Imports SBDal.StockDAL

Public Class ProductionOrderDAL
    Public Shared Function Add(ByVal objModel As ProductionOrderBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Add(ByVal objModel As ProductionOrderBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ProductionOrder (ProductionOrderNo, TicketNo, ProductionOrderDate, BatchNo, ExpiryDate, ProductId, FinishGoodId, BatchSize, Section, Remarks, CGSAccountId, Approved, TotalQuantity , DispatchId , Production_Id) values (N'" & objModel.ProductionOrderNo.Replace("'", "''") & "', N'" & objModel.TicketNo.Replace("'", "''") & "', N'" & objModel.ProductionOrderDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objModel.BatchNo.Replace("'", "''") & "', N'" & objModel.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & objModel.ProductId & ", " & objModel.FinishGoodId & ", " & objModel.BatchSize & ", N'" & objModel.Section.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "', " & objModel.CGSAccountId & ", " & IIf(objModel.Approved = True, 1, 0) & ", " & objModel.TotalQuantity & " , " & objModel.DispatchId & " , " & objModel.Production_Id & ") Select @@Identity"
            objModel.ProductionOrderId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            ProductionOrderInputMaterialDAL.Add(objModel, trans)
            ProductionOrderOutputMaterialDAL.Add(objModel, trans)
            ProductionOrderOverHeadsDAL.Add(objModel, trans)
            ProductionOrderLabourDAL.Add(objModel, trans)
            If objModel.Approved = True Then
                If objModel.Voucher.VoucherDatail.Count > 0 Then
                    Call New VouchersDAL().Add(objModel.Voucher, trans)
                End If
                If objModel.Stock.StockDetailList.Count > 0 Then
                    Call New StockDAL().Add(objModel.Stock, trans)
                End If
            End If
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Update(ByVal objModel As ProductionOrderBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Update(ByVal objModel As ProductionOrderBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ProductionOrder set ProductionOrderNo= N'" & objModel.ProductionOrderNo.Replace("'", "''") & "', TicketNo = N'" & objModel.TicketNo.Replace("'", "''") & "', ProductionOrderDate= N'" & objModel.ProductionOrderDate.ToString("yyyy-M-d h:mm:ss tt") & "', BatchNo= N'" & objModel.BatchNo.Replace("'", "''") & "', ExpiryDate= N'" & objModel.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "', ProductId= " & objModel.ProductId & ", FinishGoodId= " & objModel.FinishGoodId & ", BatchSize= " & objModel.BatchSize & ", Section= N'" & objModel.Section.Replace("'", "''") & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', CGSAccountId = " & objModel.CGSAccountId & ", Approved = " & IIf(objModel.Approved = True, 1, False) & ", TotalQuantity = " & objModel.TotalQuantity & " , DispatchId = " & objModel.DispatchId & " , Production_ID = " & objModel.Production_Id & " where ProductionOrderId=" & objModel.ProductionOrderId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ProductionOrderInputMaterialDAL.Add(objModel, trans)
            ProductionOrderOutputMaterialDAL.Add(objModel, trans)
            ProductionOrderOverHeadsDAL.Add(objModel, trans)
            ProductionOrderLabourDAL.Add(objModel, trans)
            If objModel.Approved = True Then
                If objModel.Voucher.VoucherDatail.Count > 0 Then
                    Call New VouchersDAL().Update(objModel.Voucher, trans)
                End If
                If objModel.Stock.StockDetailList.Count > 0 Then
                    Call New StockDAL().Update(objModel.Stock, trans)
                End If
            End If
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Delete(ByVal objModel As ProductionOrderBE) As Boolean
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
    Public Shared Function Delete(ByVal objModel As ProductionOrderBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProductionOrder  where ProductionOrderId= " & objModel.ProductionOrderId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ProductionOrderInputMaterialDAL.Delete(objModel.ProductionOrderId, trans)
            ProductionOrderOutputMaterialDAL.Delete(objModel.ProductionOrderId, trans)
            ProductionOrderOverHeadsDAL.Delete(objModel.ProductionOrderId, trans)
            ProductionOrderLabourDAL.Delete(objModel.ProductionOrderId, trans)
            Call New VouchersDAL().Delete(objModel.Voucher, trans)
            'Call New StockDAL().Delete(objModel.Stock, trans)
            DeleteStock(objModel.ProductionOrderNo, trans)
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
            strSQL = " Select ProductionOrder.ProductionOrderId, ProductionOrder.ProductionOrderNo, ProductionOrder.TicketNo, ProductionOrder.ProductionOrderDate, ProductionOrder.BatchNo, ProductionOrder.ExpiryDate, ProductionOrder.ProductId, ArticleDefTable.ArticleDescription, ProductionOrder.FinishGoodId, FinishGoodMaster.StandardName,  ProductionOrder.BatchSize, ProductionOrder.Section, ProductionOrder.Remarks, ProductionOrder.CGSAccountId, Account.detail_title AS Account, ISNULL(ProductionOrder.TotalQuantity, 0) AS TotalQuantity, Convert(bit, IsNull(ProductionOrder.Approved, 0)) AS Approved , ProductionOrder.DispatchId , ProductionOrder.Production_ID" _
                   & "  from ProductionOrder LEFT OUTER JOIN ArticleDefTable ON ProductionOrder.ProductId = ArticleDefTable.ArticleId " _
                   & " LEFT OUTER JOIN FinishGoodMaster ON ProductionOrder.FinishGoodId = FinishGoodMaster.Id LEFT OUTER JOIN vwCOADetail AS Account ON ProductionOrder.CGSAccountId = Account.coa_detail_id Order By ProductionOrder.ProductionOrderDate DESC "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetSingle(ByVal ProductionOrderId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProductionOrder.ProductionOrderId, ProductionOrder.ProductionOrderNo, ProductionOrder.ProductionOrderDate, ProductionOrder.BatchNo, ProductionOrder.ExpiryDate, ProductionOrder.ProductId, ArticleDefTable.ArticleDescription, ProductionOrder.FinishGoodId, FinishGoodMaster.StandardName,  ProductionOrder.BatchSize, ProductionOrder.Section, ProductionOrder.Remarks, ISNULL(ProductionOrder.TotalQuantity, 0) AS TotalQuantity, Convert(bit, IsNull(ProductionOrder.Approved, 0)) AS Approved " _
                   & "  from ProductionOrder LEFT OUTER JOIN ArticleDefTable ON ProductionOrder.ProductId = ArticleDefTable.ArticleId " _
                   & " LEFT OUTER JOIN FinishGoodMaster ON ProductionOrder.FinishGoodId = FinishGoodMaster.Id WHERE ProductionOrder.ProductionOrderId = " & ProductionOrderId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProductionOrderId, ProductionOrderNo, ProductionOrderDate, BatchNo, ExpiryDate, ProductId, FinishGoodId, BatchSize, Section, Remarks from ProductionOrder  where ProductionOrderId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetNextTicket() As String
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT ISNULL(Max(Convert(int, TicketNo)), 0) + 1 AS  TicketNo FROM ProductionOrder"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function DeleteStock(ByVal ProducionOrderNo As String, ByVal trans As SqlTransaction)
        Dim Str As String = ""
        Try
            Str = "DELETE FROM StockDetailTable WHERE StockTransId IN (SELECT StockMasterTable.StockTransId FROM StockMasterTable WHERE DocNo = '" & ProducionOrderNo & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            Str = "DELETE FROM StockMasterTable WHERE DocNo = '" & ProducionOrderNo & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Task 3394 Saad Afzaal get InputMaterial against particular BOM from finishGood material detail table'
    ''TASK TFS3577 Calculation of Qty value according to Batch Size. Dated 21-06-2018
    ''TFS3532 :25-06-2018 :  AyeshaRehman : voucher is not balance : Added In query IsNull(ArticleGroup.SubSubId, 0)
    Public Shared Function BOM_GetInputMaterial(ByVal finishGoodId As Integer, Optional ByVal BatchSize As Double = 1) As DataTable
        Dim strSQL As String = String.Empty
        Try
            If BatchSize = 0 Then
                BatchSize = 1
            End If
            strSQL = "select 0 As ID , 0 As ProductionOrderId , 1 as LocationId , MaterialArticleId  As ItemId , articledeftable.ArticleDescription As Item , ArticleSize as Unit ,  " _
                     & "isNull(FinishGoodDetail.PackQty,1) as PackQty , IsNull(FinishGoodDetail.Qty,0)  as Qty , IsNull(CostPrice,0) As Rate, IsNull(TotalQty,0) As TotalQty , " _
                     & "isNull((isNull(CostPrice,0) * (isNull(FinishGoodDetail.TotalQty,0))) , 0) as NetAmount, IsNull(ArticleGroup.SubSubId, 0) as SubSubId , IsNull(ArticleDefTableMaster.CGSAccountId,0) as CGSAccountId , " _
                     & "articledeftable.MasterID  As MasterArticleId , FinishGoodId, IsNull(FinishGoodDetail.Qty,0)  as tmpQty, IsNull(FinishGoodDetail.TotalQty,0) AS tmpTotalQty, 1 AS BatchSize " _
                     & "from FinishGoodDetail " _
                     & "Left Outer Join articledeftable ON articledeftable.ArticleId = MaterialArticleId " _
                     & "Left Outer Join ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = articledeftable.MasterID " _
                     & "Left Outer Join ArticleDefPackTable ON ArticleDefPackTable.ArticlePackId = PackingId  " _
                     & " LEFT OUTER JOIN ArticleGroupDefTable AS ArticleGroup ON articledeftable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
                     & " where FinishGoodId = " & finishGoodId
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            'dt.Columns("TotalQty").Expression = "IsNull(Qty,0)*IsNull(PackQty, 0)"
            dt.Columns("Qty").Expression = "IsNull(tmpQty,0)*IsNull(BatchSize, 0)"
            dt.Columns("TotalQty").Expression = "IsNull(tmpTotalQty,0)*IsNull(BatchSize, 0)"

            dt.Columns("NetAmount").Expression = "IsNull(TotalQty,0)*IsNull(Rate, 0)"

            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Task 3394 Saad Afzaal get GetOverHead against particular BOM from finishGood over head table'

    Public Shared Function BOM_GetOverHead(ByVal finishGoodId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " select 0 As ID , 0 As ProductionOrderId , FinishGoodOverHeads.AccountId  , vwCOADetail.detail_title As Account , Amount from FinishGoodOverHeads  " _
                   & "  Left Outer Join vwCOADetail On  vwCOADetail.coa_detail_id = FinishGoodOverHeads.AccountId " _
                   & " where FinishGoodId = " & finishGoodId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' TASK TFS3924
    ''' </summary>
    ''' <param name="FinishGoodId"></param>
    ''' <param name="LocationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function BOM_GetOutput(ByVal FinishGoodId As Integer, ByVal LocationId As Integer)
        Dim Query As String = String.Empty
        Try
            ''SELECT Id, FinishGoodId, ArticleId, Rate, Qty, Remarks FROM FinishGoodByProducts
            Query = "Select 0 AS ID, 0 AS ProductionOrderId, " & LocationId & " AS LocationId, Output.ArticleId AS ItemId, Article.ArticleDescription AS Item, '' ItemType, 'Loose' AS Unit, ISNULL(Output.Qty, 0) AS Qty, 1 AS PackQty, ISNULL(Output.Rate, 0) AS Rate, ISNULL(Output.Qty, 0) AS TotalQty, (ISNULL(Output.Rate, 0)*IsNull(Output.Qty, 0)) As NetAmount, IsNull(ArticleGroup.SubSubId, 0) As SubSubId, IsNull(ArticleGroup.CGSAccountId, 0) As CGSAccountId from FinishGoodByProducts AS Output " _
                & " INNER JOIN ArticleDefTable AS Article ON Output.ArticleId = Article.ArticleId LEFT OUTER JOIN ArticleGroupDefTable AS ArticleGroup ON Article.ArticleGroupId = ArticleGroup.ArticleGroupId WHERE Output.FinishGoodId = " & FinishGoodId & "  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.Columns("NetAmount").Expression = "IsNull(TotalQty, 0) * IsNull(Rate, 0)"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Task 3394 Saad Afzaal get GetLabour against particular BOM from finishGood Labour type table'

    Public Shared Function BOM_GetLabour(ByVal finishGoodId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " select 0 As ID , 0 As ProductionOrderId , LabourTypeId , tblLabourType.LabourType , isNull(tblLabourType.AccountId,0) As AccountId , " _
                     & " vwCOADetail.detail_title As Account , RatePerUnit As Amount from FinishGoodLabourAllocation " _
                     & " Left Outer Join tblLabourType On tblLabourType.id = LabourTypeId " _
                     & " Left Outer Join vwCOADetail On  vwCOADetail.coa_detail_id = tblLabourType.AccountId " _
                     & " where FinishGoodId = " & finishGoodId
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'End Task 3394

    'Task 3420 delete StoreIssuence latest entry from table

    Public Shared Function DeleteStroIssuence() As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim strSQL As String = String.Empty

        Try

            Dim sql As String = "select max(DispatchId) as DispatchId from DispatchMasterTable"
            Dim da As SqlDataAdapter = New SqlDataAdapter(sql, con)
            Dim cmd As SqlCommand = New SqlCommand(sql, con)
            Dim ResultStatus As String = cmd.ExecuteScalar().ToString()
            Dim DispatchId As Integer = Val(ResultStatus)

            Dim trans As SqlTransaction = Con.BeginTransaction()

            strSQL = "Delete from DispatchMasterTable where DispatchId = " & DispatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from DispatchDetailTable where DispatchId = " & DispatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()

            Con.Close()

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Task 3420 delete ProductionOrder latest entry from table

    Public Shared Function DeleteProductionOrder() As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim strSQL As String = String.Empty

        Try

            Dim sql As String = "select max(Production_ID) as Production_ID from productionMasterTable"
            Dim da As SqlDataAdapter = New SqlDataAdapter(sql, Con)
            Dim cmd As SqlCommand = New SqlCommand(sql, Con)
            Dim ResultStatus As String = cmd.ExecuteScalar().ToString()
            Dim ProductionId As Integer = Val(ResultStatus)

            Dim trans As SqlTransaction = Con.BeginTransaction()

            strSQL = "Delete from productionMasterTable where Production_ID = " & ProductionId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from productiondetailtable where Production_ID = " & ProductionId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()

            Con.Close()

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Task 3420 get latest ProductionOrderId 

    Public Shared Function getLatesProductionOrderId() As Integer

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim strSQL As String = String.Empty


        Try

            Dim sql As String = "select max(Production_ID) as Production_ID from productionMasterTable"
            Dim da As SqlDataAdapter = New SqlDataAdapter(sql, Con)
            Dim cmd As SqlCommand = New SqlCommand(sql, Con)
            Dim ResultStatus As String = cmd.ExecuteScalar().ToString()
            Dim ProductionId As Integer = Val(ResultStatus)

            Con.Close()

            Return ProductionId

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Task 3420 Delete ProductionEntry And StoreIssuance Data

    Public Shared Function DeleteProductionEntryAndStoreIssuance(ByVal DispatchId As Integer, ByVal ProductionID As Integer) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim strSQL As String = String.Empty

        Try

            'Dim sql As String = "select production_no from productionMasterTable where Production_ID = " & ProductionID

            'Dim da As SqlDataAdapter = New SqlDataAdapter(sql, Con)
            'Dim cmd As SqlCommand = New SqlCommand(sql, Con)
            'Dim ResultStatus As String = cmd.ExecuteScalar().ToString()
            'Dim ProductionNo As String = ResultStatus

            Dim trans As SqlTransaction = Con.BeginTransaction()
            Dim StockID As Integer = 0
            Dim sql As String = "SELECT production_no from productionMasterTable where Production_ID = " & ProductionID
            Dim dtProduction As DataTable = UtilityDAL.GetDataTable(sql, trans)
            If dtProduction.Rows.Count > 0 Then
                Dim ProductionNo As String = dtProduction.Rows(0).Item(0).ToString
                StockID = Convert.ToInt32(GetStockTransId(ProductionNo).ToString)
            End If

            ''TFS3532 : Ayesha Rehman : Delete Detail record first then master record,the reason being foreign keys exist causing issue in deleting

            strSQL = "Delete from DispatchDetailTable where DispatchId = " & DispatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from DispatchMasterTable where DispatchId = " & DispatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from productiondetailtable where Production_ID = " & ProductionID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from productionMasterTable where Production_ID = " & ProductionID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from StockDetailTable where StockTransId = " & StockID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from StockMasterTable where StockTransId = " & StockID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()

            Con.Close()

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
