Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDAL
Imports SBDAL.SqlHelper
Public Class CloseBatchDAL
    Const _New As String = "New"
    Const _Update As String = "Update"
    Const _Delete As String = "Delete"
    Dim ProductionId As Integer = 0
    Public Function Save(ByVal Obj As BECloseBatch) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            '           [CloseBatchId] [int] IDENTITY(1,1) NOT NULL,
            '[PlanId] [int] NULL,
            '[TicketId] [int] NULL,
            '[ProductId] [int] NULL,
            '[BatchNo] [nvarchar](100) NULL,
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Dim str As String = String.Empty
            If Obj.IsClosedBatch = True Then
                Obj.Voucher.VoucherNo = GetProductionNo(Obj.Production.Production_Date, trans)
                Obj.Voucher.VoucherCode = Obj.Voucher.VoucherNo
                Obj.Production.StockMaster.DocNo = Obj.Voucher.VoucherNo
                Obj.Production.ProductionId = New ProductionDAL().Add(Obj.Production, trans)
            End If

            str = "INSERT INTO CloseBatch(PlanId, TicketId, ProductId, BatchNo, IsClosedBatch, ProductionId) Values (" & Obj.PlanId & ", " & Obj.TicketId & ", " & Obj.ProductId & ", N'" & Obj.BatchNo & "', " & IIf(Obj.IsClosedBatch = True, 1, 0) & ", " & Obj.Production.ProductionId & ") SELECT @@IDENTITY"
            Obj.CloseBatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            SaveDetails(Obj, trans)
            If Obj.IsClosedBatch = True Then
                ''TASK TFS2784
                If Obj.Voucher.VoucherDatail.Count > 0 Then
                    Call New VouchersDAL().Add(Obj.Voucher, trans)
                End If
                ''END TASK TFS2784
            End If
            ''TASK TFS2674
            DoInActiveCostCentre(Obj.TicketId)
            ''END TASK TFS2674
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Public Function SaveDetails(ByVal Obj As BECloseBatch, ByVal trans As SqlTransaction) As Boolean
        Try
            ''MC
            For Each Detail As BECloseBatchMCDetail In Obj.CloseBatchMCDetail
                Dim str As String = String.Empty
                If Detail.CloseBatchMCDetailId = 0 Then
                    str = "INSERT INTO CloseBatchMCDetail(CloseBatchId, ItemConsumptionDetailId, ArticleId, Qty, Rate) Values (" & Obj.CloseBatchId & ", " & Detail.ItemConsumptionDetailId & ", " & Detail.ArticleId & " , " & Detail.Qty & " , " & Detail.Rate & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update CloseBatchMCDetail Set CloseBatchId = " & Obj.CloseBatchId & ", ItemConsumptionDetailId= " & Detail.ItemConsumptionDetailId & ", ArticleId = " & Detail.ArticleId & ", Qty = " & Detail.Qty & ", Rate = " & Detail.Rate & " WHERE CloseBatchMCDetailId = " & Detail.CloseBatchMCDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            ''DE
            For Each Detail As BECloseBatchDEDetail In Obj.CloseBatchDEDetail
                '              CREATE TABLE [dbo].[CloseBatchDEDetail](
                '[CloseBatchDEDetailId] [int] IDENTITY(1,1) NOT NULL,
                '[CloseBatchId] [int] NULL,
                '[AccountId] [int] NULL,
                '[Amount] [float] NULL,
                Dim str As String = String.Empty
                If Detail.CloseBatchDEDetailId = 0 Then
                    str = "INSERT INTO CloseBatchDEDetail(CloseBatchId, AccountId, Amount) Values (" & Obj.CloseBatchId & ", " & Detail.AccountId & ", " & Detail.Amount & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update CloseBatchDEDetail Set CloseBatchId = " & Obj.CloseBatchId & ", AccountId= " & Detail.AccountId & ", Amount = " & Detail.Amount & " WHERE CloseBatchDEDetailId = " & Detail.CloseBatchDEDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            ''OH
            For Each Detail As BECloseBatchOHDetail In Obj.CloseBatchOHDetail
                'CREATE TABLE [dbo].[CloseBatchOHDetail](
                '[CloseBatchOHDetailId] [int] IDENTITY(1,1) NOT NULL,
                '[CloseBatchId] [int] NULL,
                '[ProductionOverHeadsId] [int] NULL,
                '[AccountId] [int] NULL,
                '[Amount] [float] NULL,
                Dim str As String = String.Empty
                If Detail.CloseBatchOHDetailId = 0 Then
                    str = "INSERT INTO CloseBatchOHDetail(CloseBatchId, ProductionOverHeadsId, AccountId, Amount) Values (" & Obj.CloseBatchId & ", " & Detail.ProductionOverHeadsId & ", " & Detail.AccountId & ", " & Detail.Amount & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update CloseBatchOHDetail Set CloseBatchId = " & Obj.CloseBatchId & ", ProductionOverHeadsId= " & Detail.ProductionOverHeadsId & ", AccountId = " & Detail.AccountId & " , Amount = " & Detail.Amount & " WHERE CloseBatchOHDetailId = " & Detail.CloseBatchOHDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            ''Labour Cost
            For Each Detail As BECloseBatchLCDetail In Obj.CloseBatchLCDetail

                Dim str As String = String.Empty
                If Detail.CloseBatchLCDetailId = 0 Then
                    str = "INSERT INTO CloseBatchLCDetail(CloseBatchId, LabourTypeId, Amount) Values (" & Obj.CloseBatchId & ", " & Detail.LabourTypeId & ", " & Detail.Amount & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update CloseBatchLCDetail Set CloseBatchId = " & Obj.CloseBatchId & ", LabourTypeId= " & Detail.LabourTypeId & ",  Amount = " & Detail.Amount & " WHERE CloseBatchLCDetailId = " & Detail.CloseBatchLCDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            ''ByProduct
            For Each Detail As BECloseBatchByProductDetail In Obj.CloseBatchByProductDetail
                Dim str As String = String.Empty
                If Detail.CloseBatchByProductDetailId = 0 Then
                    str = "INSERT INTO CloseBatchByProductDetail(CloseBatchId, ByProductsId, ArticleId, Rate, Qty) Values (" & Obj.CloseBatchId & ", " & Detail.ByProductsId & ", " & Detail.ArticleId & ", " & Detail.Rate & ", " & Detail.Qty & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update CloseBatchByProductDetail Set CloseBatchId = " & Obj.CloseBatchId & ", ByProductsId= " & Detail.ByProductsId & ", ArticleId = " & Detail.ArticleId & ", Rate = " & Detail.Rate & " , Qty = " & Detail.Qty & " WHERE CloseBatchByProductDetailId = " & Detail.CloseBatchByProductDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            ''Finish Goods
            For Each Detail As BECloseBatchFinishGoodsDetail In Obj.CloseBatchFinishGoodsDetail
                Dim str As String = String.Empty
                If Detail.CloseBatchFinishGoodsDetailId = 0 Then
                    str = "INSERT INTO CloseBatchFinishGoodsDetail(CloseBatchId, DepartmentWiseProductionDetailId, ArticleId, Qty) Values (" & Obj.CloseBatchId & ", " & Detail.DepartmentWiseProductionDetailId & ", " & Detail.ArticleId & "," & Detail.Qty & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update CloseBatchFinishGoodsDetail Set CloseBatchId = " & Obj.CloseBatchId & ", DepartmentWiseProductionDetailId= " & Detail.DepartmentWiseProductionDetailId & ", ArticleId = " & Detail.ArticleId & ", Qty = " & Detail.Qty & " WHERE DepartmentWiseProductionDetailId = " & Detail.DepartmentWiseProductionDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            For Each Detail As BECloseBatchDetail In Obj.CloseBatchDetail
                'ID()
                'CloseBatchId()
                'LocationId()
                'Location()
                'ArticleId()
                'ArticleCode()
                'ArticleDescription()
                'UnitName()
                'Quantity()
                'PackingId()
                'Packing()
                Dim str As String = String.Empty
                If Detail.ID = 0 Then
                    str = "INSERT INTO CloseBatchDetail(CloseBatchId, LocationId, ArticleId, Quantity, PackingId) Values (" & Obj.CloseBatchId & ", " & Detail.LocationId & ", " & Detail.ArticleId & "," & Detail.Quantity & ", " & Detail.PackingId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update CloseBatchDetail Set CloseBatchId = " & Obj.CloseBatchId & ", ArticleId= " & Detail.ArticleId & ", Quantity = " & Detail.Quantity & ", PackingId = " & Detail.PackingId & " WHERE ID = " & Detail.ID & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function


    Public Function Update(ByVal Obj As BECloseBatch) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Dim str As String = String.Empty
            If Obj.IsClosedBatch = True Then
                If Obj.Production.Production_No = "" Then
                    Obj.Voucher.VoucherNo = GetProductionNo(Obj.Production.Production_Date, trans)
                    Obj.Voucher.VoucherCode = Obj.Voucher.VoucherNo
                    Obj.Production.StockMaster.DocNo = Obj.Voucher.VoucherNo
                    Obj.Production.ProductionId = New ProductionDAL().Add(Obj.Production, trans)
                Else
                    Obj.Production.StockMaster.DocNo = Obj.Production.Production_No
                    Obj.Production.ProductionId = New ProductionDAL().Update(Obj.Production, trans)
                End If
            End If
            'str = "INSERT INTO ProductionProcess(ProcessName, Remarks) Values (N'" & Obj.ProcessName & "', N'" & Obj.Remarks & "') SELECT @@IDENTITY"
            str = "Update CloseBatch SET PlanId = " & Obj.PlanId & ", TicketId= " & Obj.TicketId & ", ProductId = " & Obj.ProductId & ", BatchNo = N'" & Obj.BatchNo & "', IsClosedBatch = " & IIf(Obj.IsClosedBatch = True, 1, 0) & ", ProductionId = " & Obj.Production.ProductionId & " WHERE CloseBatchId = " & Obj.CloseBatchId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            SaveDetails(Obj, trans)
            If Obj.IsClosedBatch = True Then
                ''TASK TFS2784
                If Obj.Voucher.VoucherDatail.Count > 0 Then
                    If Obj.Production.Production_No = "" Then
                        Call New VouchersDAL().Add(Obj.Voucher, trans)
                    Else
                        Call New VouchersDAL().Update(Obj.Voucher, trans)
                    End If
                End If
                ''END TASK TFS2784
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Public Function Delete(ByVal CloseBatchId As Integer, Optional ByVal VoucherNo As String = "") As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            DeleteDetails(CloseBatchId, trans)

            str = "DELETE FROM CloseBatch WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ''TASK TFS2784
            Dim _Voucher As New VouchersMaster
            _Voucher.VoucherNo = VoucherNo
            _Voucher.VoucherCode = VoucherNo
            _Voucher.VNo = VoucherNo
            _Voucher.ActivityLog = New ActivityLog()
            Call New VouchersDAL().Delete(_Voucher, trans)
            ''END TASK TFS2784
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function DeleteDetails(ByVal CloseBatchId As Integer, trans As SqlTransaction) As Boolean
        'Dim Conn As New SqlConnection(CON_STR)
        'If Conn.State = ConnectionState.Closed Then Conn.Open()
        'Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "DELETE FROM CloseBatchMCDetail WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM CloseBatchDEDetail WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM CloseBatchOHDetail WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM CloseBatchLCDetail WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM CloseBatchByProductDetail WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM CloseBatchFinishGoodsDetail WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM CloseBatchDetail WHERE CloseBatchId = " & CloseBatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ''
            str = "DELETE FROM StockDetailTable WHERE StockTransId IN (SELECT StockMasterTable.StockTransId FROM StockMasterTable WHERE DocNo In (SELECT Production_No FROM ProductionMasterTable INNER JOIN CloseBatch ON ProductionMasterTable.Production_ID = CloseBatch.ProductionId   WHERE CloseBatch.CloseBatchId = " & CloseBatchId & "))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM StockMasterTable WHERE DocNo IN (SELECT Production_No FROM ProductionMasterTable INNER JOIN  CloseBatch ON ProductionMasterTable.Production_ID = CloseBatch.ProductionId  WHERE CloseBatch.CloseBatchId = " & CloseBatchId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM ProductionDetailTable WHERE Production_ID IN ( SELECT ISNULL(ProductionId, 0) AS ProductionId FROM CloseBatch WHERE CloseBatchId = " & CloseBatchId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM ProductionMasterTable WHERE Production_ID IN ( SELECT ISNULL(ProductionId, 0) AS ProductionId FROM CloseBatch WHERE CloseBatchId = " & CloseBatchId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "Update tblDefCostCenter SET Active = 1 WHERE Name IN (SELECT TicketNo FROM PlanTicketsMaster Inner Join CloseBatch ON  PlanTicketsMaster.PlanTicketsMasterID = CloseBatch.TicketId WHERE CloseBatch.CloseBatchId = " & CloseBatchId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ''Deletion of Stock
            ''
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteProduction(ByVal CloseBatchId As Integer, trans As SqlTransaction) As Boolean
        'Dim Conn As New SqlConnection(CON_STR)
        'If Conn.State = ConnectionState.Closed Then Conn.Open()
        'Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "DELETE FROM ProductionMasterTable WHERE Production_ID IN ( SELECT ISNULL(ProductionId, 0) AS ProductionId FROM CloseBatch WHERE CloseBatchId = " & CloseBatchId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "DELETE FROM ProductionDetailTable WHERE Production_ID IN ( SELECT ISNULL(ProductionId, 0) AS ProductionId FROM CloseBatch WHERE CloseBatchId = " & CloseBatchId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = " SELECT ProductionMasterTable.Production_no AS ProductionNo, CloseBatchId, CloseBatch.PlanId, PlanMasterTable.PlanNo, CloseBatch.TicketId, Ticket.TicketNo, CloseBatch.ProductId, Article.ArticleDescription AS [Production Item], CloseBatch.BatchNo, ISNULL(CloseBatch.IsClosedBatch, 0) AS IsClosedBatch, IsNull(ProductionId, 0) AS ProductionId FROM CloseBatch LEFT OUTER JOIN PlanMasterTable  ON CloseBatch.PlanId = PlanMasterTable.PlanId LEFT OUTER JOIN PlanTicketsMaster AS Ticket ON CloseBatch.TicketId = Ticket.PlanTicketsMasterID LEFT OUTER JOIN ArticleDefTableMaster AS Article ON CloseBatch.ProductId = Article.ArticleId " _
                  & " LEFT OUTER JOIN ProductionMasterTable ON CloseBatch.ProductionId = ProductionMasterTable.Production_ID order by CloseBatchId desc"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAllDetail(ByVal ProductionProcessId As Integer) As DataTable
        Try
            Dim str As String = String.Empty

            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function ValidateProcessName(ByVal ProcessName As String) As Boolean
        Try
            Dim str As String = String.Empty
            str = "SELECT Count(*) As Count1 FROM ProductionProcess WHERE ProcessName ='" & ProcessName & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''MC
    Public Shared Function DisplayMaterialCost(ByVal CloseBatchId As Integer) As DataTable
        Try
            Dim Str As String = " Select CloseBatchMCDetail.CloseBatchMCDetailId, CloseBatchMCDetail.CloseBatchId, CloseBatchMCDetail.ItemConsumptionDetailId, CloseBatchMCDetail.ArticleId, Article.ArticleDescription, CloseBatchMCDetail.Qty, CloseBatchMCDetail.Rate, (CloseBatchMCDetail.Qty*CloseBatchMCDetail.Rate) AS Value FROM CloseBatchMCDetail LEFT  OUTER JOIN ArticleDefTable AS Article ON CloseBatchMCDetail.ArticleId = Article.ArticleId WHERE  CloseBatchId = " & CloseBatchId & ""
            Dim dtMC As DataTable = UtilityDAL.GetDataTable(Str)
            dtMC.Columns("Value").Expression = "Qty*Rate"
            Return dtMC
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''DE
    Public Shared Function DisplayDirectExpense(ByVal CloseBatchId As Integer) As DataTable
        Try
            Dim Str As String = " Select CloseBatchDEDetail.CloseBatchDEDetailId, CloseBatchDEDetail.CloseBatchId, CloseBatchDEDetail.AccountId, Account.detail_title AS Account, CloseBatchDEDetail.Amount FROM CloseBatchDEDetail LEFT OUTER JOIN vwCOADetail AS Account ON CloseBatchDEDetail.AccountId = Account.coa_detail_id WHERE  CloseBatchId = " & CloseBatchId & ""
            Dim dtDE As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtDE
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''OH
    Public Shared Function DisplayOverHeads(ByVal CloseBatchId As Integer) As DataTable
        Try
            Dim Str As String = " Select CloseBatchOHDetail.CloseBatchOHDetailId, CloseBatchOHDetail.CloseBatchId, CloseBatchOHDetail.AccountId, Account.detail_title AS Account, CloseBatchOHDetail.Amount FROM CloseBatchOHDetail LEFT OUTER JOIN vwCOADetail AS Account ON CloseBatchOHDetail.AccountId = Account.coa_detail_id WHERE  CloseBatchOHDetail.CloseBatchId = " & CloseBatchId & ""
            Dim dtOH As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtOH
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''LC
    Public Shared Function DisplayLabourCost(ByVal CloseBatchId As Integer) As DataTable
        Try
            Dim Str As String = " Select CloseBatchLCDetail.CloseBatchLCDetailId, CloseBatchLCDetail.CloseBatchId, CloseBatchLCDetail.LabourTypeId, LabourType.LabourType, LabourType.AccountId AS AccountId, CloseBatchLCDetail.Amount FROM CloseBatchLCDetail LEFT OUTER JOIN tblLabourType AS LabourType ON CloseBatchLCDetail.LabourTypeId = LabourType.Id WHERE  CloseBatchLCDetail.CloseBatchId = " & CloseBatchId & ""
            Dim dtLC As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtLC
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''By Product
    Public Shared Function DisplayByProducts(ByVal CloseBatchId As Integer) As DataTable
        Try
            ''CloseBatchId, ByProductsId, ArticleId, Rate, Qty
            Dim Str As String = " Select CloseBatchByProductDetail.CloseBatchByProductDetailId, CloseBatchByProductDetail.CloseBatchId, CloseBatchByProductDetail.ByProductsId, CloseBatchByProductDetail.ArticleId, Article.ArticleDescription, IsNull(ArticleGroupDefTable.SubSubId, 0) AS SubSubId, CloseBatchByProductDetail.Qty, CloseBatchByProductDetail.Rate, (CloseBatchByProductDetail.Qty*CloseBatchByProductDetail.Rate) AS Value FROM CloseBatchByProductDetail LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchByProductDetail.ArticleId = Article.ArticleId " _
                                & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId WHERE  CloseBatchByProductDetail.CloseBatchId = " & CloseBatchId & ""
            Dim dtByProducts As DataTable = UtilityDAL.GetDataTable(Str)
            dtByProducts.Columns("Value").Expression = "Qty*Rate"

            Return dtByProducts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''Finish Goods
    Public Shared Function DisplayFinishGoods(ByVal CloseBatchId As Integer) As DataTable
        Try
            ''CloseBatchId, DepartmentWiseProductionDetailId, ArticleId, Qty
            Dim Str As String = " Select CloseBatchFinishGoodsDetail.CloseBatchFinishGoodsDetailId, CloseBatchFinishGoodsDetail.CloseBatchId, CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId, CloseBatchFinishGoodsDetail.ArticleId, Article.ArticleDescription, ISNULL(ArticleGroupDefTable.SubSubId, 0) AS SubSubId, IsNull(CloseBatchFinishGoodsDetail.Qty, 0) AS Qty , tblprosteps.prod_step as Stage FROM CloseBatchFinishGoodsDetail LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                                & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId WHERE CloseBatchFinishGoodsDetail.CloseBatchId = " & CloseBatchId & ""
            Dim dtFinishGoods As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtFinishGoods
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function DisplayDetail(ByVal CloseBatchId As Integer) As DataTable
        Dim dt As New DataTable
        Dim Query As String = String.Empty
        Try
            'ID()
            'CloseBatchId()
            'LocationId()
            'Location()
            'ArticleId()
            'ArticleCode()
            'ArticleDescription()
            'UnitName()
            'Quantity()
            'PackingId()
            'Packing()
            Query = " Select Detail.ID, Detail.CloseBatchId, IsNull(Detail.LocationId, 0) AS LocationId, Location.location_name AS Location, Detail.ArticleId, Article.ArticleCode, Article.ArticleDescription,  ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(Detail.Quantity, 0) As Quantity, IsNull(Detail.PackingId, 0) AS PackingId, ArticleDefPackTable.PackName As Packing, ISNULL(ArticleGroupDefTable.SubSubId, 0) AS SubSubId From CloseBatchDetail AS Detail INNER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join ArticleDefPackTable ON Detail.PackingId = ArticleDefPackTable.ArticlePackId " _
                    & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId LEFT OUTER JOIN tblDefLocation AS Location ON Detail.LocationId = Location.location_id Where Detail.CloseBatchId = " & CloseBatchId & " "
            dt = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDetail(ByVal TicketId As Integer) As DataTable
        Dim dt As New DataTable
        Dim Query As String = String.Empty
        Try
            'ID()
            'CloseBatchId()
            'LocationId()
            'Location()
            'ArticleId()
            'ArticleCode()
            'ArticleDescription()
            'UnitName()
            'Quantity()
            'PackingId()
            'Packing()
            'Query = " Select 0 AS ID, 0 AS CloseBatchId, IsNull(Detail.LocationId, 0) AS LocationId, Location.location_name AS Location, Detail.ArticleId, Article.ArticleCode, Article.ArticleDescription,  ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(Detail.Quantity, 0) As Quantity, IsNull(Detail.PackingId, 0) AS PackingId, ArticleDefPackTable.PackName As Packing, ISNULL(ArticleGroupDefTable.SubSubId, 0) AS SubSubId From PlanTicketsDetail AS Detail INNER JOIN ArticleDefTable As Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join ArticleDefPackTable ON Detail.PackingId = ArticleDefPackTable.ArticlePackId " _
            '        & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId LEFT OUTER JOIN tblDefLocation AS Location ON Detail.LocationId = Location.location_id Where Detail.PlanTicketsMasterID = " & TicketId & " "
            'Ali Faisal : Query edited for Food Trends to get only last Department wise production on Closing batch on 04-Sep-2018
            Query = "SELECT  0 AS ID, 0 AS CloseBatchId, ISNULL(Detail.LocationId, 0) AS LocationId, Location.location_name AS Location, Detail.ArticleId, Article.ArticleCode, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitName AS UnitName, ISNULL(DepartPro.Qty, 0) AS Quantity, ISNULL(Detail.PackingId, 0) AS PackingId, ArticleDefPackTable.PackName AS Packing, ISNULL(ArticleGroupDefTable.SubSubID, 0) AS SubSubId" _
                    & " FROM PlanTicketsDetail AS Detail INNER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN ArticleDefPackTable ON Detail.PackingId = ArticleDefPackTable.ArticlePackId LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId LEFT OUTER JOIN tblDefLocation AS Location ON Detail.LocationId = Location.location_id LEFT OUTER JOIN  (SELECT  TOP(1)     DepartmentWiseProductionDetail.TicketID, DepartmentWiseProductionDetail.Qty FROM            DepartmentWiseProductionDetail LEFT OUTER JOIN tblproSteps ON DepartmentWiseProductionDetail.DepartmentID = tblproSteps.ProdStep_Id ORDER BY tblproSteps.sort_Order DESC) AS DepartPro ON Detail.PlanTicketsMasterID = DepartPro.TicketID WHERE  Detail.PlanTicketsMasterID = " & TicketId & " "
            dt = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    ''Get section
    ''MC
    Public Shared Function GetMaterialCost(ByVal TicketId As Integer) As DataTable
        Try
            Dim Str As String = " Select 0 AS CloseBatchMCDetailId, 0 AS CloseBatchId, Detail.ConsumptionDetailId AS ItemConsumptionDetailId, Article.ArticleDescription , Detail.ArticleId, Detail.Qty, Detail.Rate, (Detail.Qty*Detail.Rate) AS Value FROM ItemConsumptionDetail AS Detail INNER JOIN ItemConsumptionMaster AS Master ON Detail.ConsumptionId = Master.ConsumptionId LEFT OUTER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId WHERE Detail.TicketId = " & TicketId & ""
            Dim dtMC As DataTable = UtilityDAL.GetDataTable(Str)
            dtMC.Columns("Value").Expression = "Qty*Rate"
            Return dtMC
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''DE
    Public Shared Function GetDirectExpense(ByVal CostCenterId As Integer) As DataTable
        Try
            ''" Select 0 AS CloseBatchDEDetailId, 0 AS CloseBatchId, SUM(IsNull(debit_amount,0)) as ExpAmount, tblVoucherDetail.coa_detail_id From tblVoucherDetail INNER JOIN tblVoucher On tblVoucher.Voucher_Id =  tblVoucherDetail.Voucher_Id INNER JOIN vwCOADetail  on vwCOADetail.coa_detail_id  = tblVoucherDetail.coa_detail_id WHERE tblVoucherDetail.CostCenterID=" & CostCenterID & "  AND tblVoucher.Source <> 'frmImport' and debit_amount>0 Group By tblVoucherDetail.CostCenterID, tblVoucherDetail.coa_detail_id"
            Dim Str As String = "Select 0 AS CloseBatchDEDetailId, 0 AS CloseBatchId, tblVoucherDetail.coa_detail_id AS AccountId, Account.detail_title AS Account, SUM(IsNull(debit_amount,0)) AS Amount From tblVoucherDetail INNER JOIN tblVoucher On tblVoucher.Voucher_Id =  tblVoucherDetail.Voucher_Id INNER JOIN vwCOADetail AS Account  ON Account.coa_detail_id  = tblVoucherDetail.coa_detail_id WHERE tblVoucherDetail.CostCenterID=" & CostCenterId & "  AND debit_amount > 0 AND ISNULL(tblVoucherDetail.CostCenterID, 0) > 0 AND LEFT(tblVoucher.voucher_no, 1) NOT IN ('I') AND LEFT(tblVoucher.voucher_no, 2) NOT IN ('RI') AND LEFT(tblVoucher.voucher_no, 3) NOT IN ('PRD') Group By tblVoucherDetail.coa_detail_id, Account.detail_title "
            Dim dtDE As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtDE
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''OH
    Public Shared Function GetOverHeads(ByVal TicketId As Integer) As DataTable
        Try
            'Dim Str As String = " Select 0 AS CloseBatchOHDetailId, 0 AS CloseBatchId, OverHeads.AccountId, Account.detail_title AS Account, OverHeads.Amount FROM ProductionOverHeads OverHeads LEFT OUTER JOIN vwCOADetail AS Account ON OverHeads.AccountId = Account.coa_detail_id WHERE  OverHeads.ArticleId In (Select Distinct ArticleId From PlanTicketsDetail WHERE PlanTicketsMasterID = " & TicketId & ")"
            '[Id] [int] IDENTITY(1,1) NOT NULL,
            '[FinishGoodId] [int] NULL,
            '[ProductionStepId] [int] NULL,
            '[AccountId] [int] NULL,
            '[Amount] [float] NULL,
            '[Remarks] [nvarchar](300) NULL,
            Dim Str As String = " Select 0 AS CloseBatchOHDetailId, 0 AS CloseBatchId, OverHeads.AccountId, Account.detail_title AS Account, Isnull(OverHeads.Amount,0)*IsNull(PlanTicketsMaster.noofbatches,0) as Amount FROM FinishGoodOverHeads OverHeads INNER JOIN FinishGoodMaster AS FinishGood ON OverHeads.FinishGoodId = FinishGood.Id INNER JOIN PlanTicketsMaster ON PlanTicketsMaster.MasterArticleId = FinishGood.MasterArticleId LEFT OUTER JOIN vwCOADetail AS Account ON OverHeads.AccountId = Account.coa_detail_id WHERE FinishGood.Default1 = 1 AND PlanTicketsMaster.PlanTicketsMasterID = " & TicketId & ""     'FinishGood.MasterArticleId In (SELECT DISTINCT MasterArticleId FROM PlanTicketsMaster WHERE PlanTicketsMasterID = " & TicketId & ")"
            Dim dtOH As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtOH
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''LC
    Public Shared Function GetLabourCost(ByVal TicketId As Integer) As DataTable
        Try
            '[Id] [int] IDENTITY(1,1) NOT NULL,
            '[FinishGoodId] [int] NULL,
            '[ProductionStepId] [int] NULL,
            '[LabourTypeId] [int] NULL,
            '[RatePerUnit] [float] NULL,
            'Dim Str As String = " Select 0 AS CloseBatchLCDetailId, 0 AS CloseBatchId, LabourAllocation.LabourTypeId, LabourType.LabourType, LabourAllocation.RatePerUnit As Amount FROM LabourAllocation LEFT OUTER JOIN tblLabourType AS LabourType ON LabourAllocation.LabourTypeId = LabourType.Id WHERE  LabourAllocation.ArticleId In (Select Distinct ArticleId From PlanTicketsDetail WHERE PlanTicketsMasterID = " & TicketId & ")"
            Dim Str As String = " Select 0 AS CloseBatchLCDetailId, 0 AS CloseBatchId, LabourAllocation.LabourTypeId, LabourType.LabourType, LabourType.AccountId AS AccountId, LabourAllocation.RatePerUnit As Amount FROM FinishGoodLabourAllocation AS LabourAllocation INNER JOIN FinishGoodMaster AS FinishGood ON LabourAllocation.FinishGoodId = FinishGood.Id LEFT OUTER JOIN tblLabourType AS LabourType ON LabourAllocation.LabourTypeId = LabourType.Id WHERE  FinishGood.Default1 = 1 AND FinishGood.MasterArticleId In (SELECT DISTINCT MasterArticleId FROM PlanTicketsMaster WHERE PlanTicketsMasterID = " & TicketId & ")"

            Dim dtLC As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtLC
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''By Product
    Public Shared Function GetByProducts(ByVal TicketId As Integer) As DataTable
        Try
            '           [Id] [int] IDENTITY(1,1) NOT NULL,
            '[FinishGoodId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Rate] [float] NULL,
            '[Qty] [float] NULL,
            '[Remarks] [nvarchar](300) NULL,


            ''CloseBatchId, ByProductsId, ArticleId, Rate, Qty
            'Dim Str As String = " Select 0 AS CloseBatchByProductDetailId, 0 AS CloseBatchId, ByProducts.ByProductsId, ByProducts.ArticleId, Article.ArticleDescription, ByProducts.Qty, ByProducts.Rate, (ByProducts.Qty*ByProducts.Rate) AS Value FROM ByProducts LEFT OUTER JOIN ArticleDefTable AS Article ON ByProducts.ArticleId = Article.ArticleId WHERE  ByProducts.MasterArticleId IN (Select Distinct ArticleId From PlanTicketsDetail WHERE PlanTicketsMasterID = " & TicketId & ")"
            'ByProductsId
            Dim Str As String = " Select 0 AS CloseBatchByProductDetailId, 0 AS CloseBatchId, ByProducts.Id AS ByProductsId, ByProducts.ArticleId, Article.ArticleDescription, IsNull(ArticleGroupDefTable.SubSubId, 0) AS SubSubId, ByProducts.Qty, ByProducts.Rate, (ByProducts.Qty*ByProducts.Rate) AS Value FROM FinishGoodByProducts AS ByProducts INNER JOIN FinishGoodMaster AS FinishGood ON ByProducts.FinishGoodId = FinishGood.Id LEFT OUTER JOIN ArticleDefTable AS Article ON ByProducts.ArticleId = Article.ArticleId " _
                                & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId WHERE FinishGood.Default1 = 1 AND FinishGood.MasterArticleId In (SELECT DISTINCT MasterArticleId FROM PlanTicketsMaster WHERE PlanTicketsMasterID = " & TicketId & ")"

            Dim dtByProducts As DataTable = UtilityDAL.GetDataTable(Str)
            dtByProducts.Columns("Value").Expression = "Qty*Rate"
            Return dtByProducts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''Finish Goods
    Public Shared Function GetFinishGoods(ByVal TicketId As Integer) As DataTable
        Try
            ''CloseBatchId, DepartmentWiseProductionDetailId, ArticleId, Qty
            Dim Str As String = " Select 0 AS CloseBatchFinishGoodsDetailId, 0 AS CloseBatchId, Detail.ID AS DepartmentWiseProductionDetailId, Detail.ArticleId, Article.ArticleDescription, IsNull(ArticleGroupDefTable.SubSubId, 0) AS SubSubId, IsNull(Detail.Qty, 0) AS Qty , tblprosteps.prod_step as Stage FROM DepartmentWiseProductionDetail AS Detail LEFT OUTER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN tblprosteps ON Detail.DepartmentId = tblprosteps.ProdStep_Id " _
                               & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId WHERE  Detail.TicketId = " & TicketId & ""
            Dim dtFinishGoods As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtFinishGoods
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetStages(ByVal Id As Integer, ByVal key As String) As DataTable
        Try
            ''CloseBatchId, DepartmentWiseProductionDetailId, ArticleId, Qty
            'Dim Str As String = "SELECT tblproSteps.prod_step as Stage FROM DepartmentWiseProductionDetail LEFT JOIN tblproSteps ON DepartmentWiseProductionDetail.DepartmentID = tblproSteps.ProdStep_Id order by tblproSteps.sort_Order ASC"

            Dim Str As String

            If key = "C" Then
                Str = "SELECT distinct tblproSteps.prod_step as Stage , tblproSteps.sort_Order FROM DepartmentWiseProductionDetail LEFT JOIN tblproSteps ON DepartmentWiseProductionDetail.DepartmentID = tblproSteps.ProdStep_Id LEFT JOIN CloseBatchFinishGoodsDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailid = DepartmentWiseProductionDetail.id WHERE CloseBatchFinishGoodsDetail.closebatchid = " & Id & " order by tblproSteps.sort_Order ASC"
            Else
                Str = "SELECT distinct tblproSteps.prod_step as Stage , tblproSteps.sort_Order FROM DepartmentWiseProductionDetail LEFT JOIN tblproSteps ON DepartmentWiseProductionDetail.DepartmentID = tblproSteps.ProdStep_Id WHERE DepartmentWiseProductionDetail.Ticketid = " & Id & " order by tblproSteps.sort_Order ASC"
            End If
            Dim dtStages As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtStages
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function GetDeparmentWiseStages(ByVal Str As String) As DataTable
        Try
            ''CloseBatchId, DepartmentWiseProductionDetailId, ArticleId, Qty
            'Dim Str As String = "SELECT tblproSteps.prod_step as Stage FROM CloseBatchFinishGoodsDetail LEFT JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.ID LEFT JOIN tblproSteps ON DepartmentWiseProductionDetail.DepartmentID = tblproSteps.ProdStep_Id"
            Dim dtDeparmentWiseStages As DataTable = UtilityDAL.GetDataTable(Str)
            Return dtDeparmentWiseStages
        Catch ex As Exception
            Throw ex
        End Try
    End Function







    '     str = "DELETE FROM CloseBatchFinishGoodsDetail WHERE CloseBatchId = " & CloseBatchId
    Public Shared Function DeleteFinishGoods(ByVal CloseBatchFinishGoodsDetailId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM CloseBatchFinishGoodsDetail WHERE CloseBatchFinishGoodsDetailId = " & CloseBatchFinishGoodsDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Shared Function DeleteByProduct(ByVal CloseBatchByProductDetailId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM CloseBatchByProductDetail WHERE CloseBatchByProductDetailId = " & CloseBatchByProductDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Shared Function DeleteLC(ByVal CloseBatchLCDetailId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM CloseBatchLCDetail WHERE CloseBatchLCDetailId = " & CloseBatchLCDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Shared Function DeleteOH(ByVal CloseBatchOHDetailId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM CloseBatchOHDetail WHERE CloseBatchOHDetailId = " & CloseBatchOHDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Shared Function DeleteDE(ByVal CloseBatchDEDetailId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM CloseBatchDEDetail WHERE CloseBatchDEDetailId = " & CloseBatchDEDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Shared Function DeleteMC(ByVal CloseBatchMCDetailId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM CloseBatchMCDetail WHERE CloseBatchMCDetailId = " & CloseBatchMCDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Shared Function DeleteDetail(ByVal CloseBatchDetailId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM CloseBatchDetail WHERE ID = " & CloseBatchDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Function GetProductionNo(dtpPODate As DateTime, trans As SqlTransaction) As String
        Try
            'If Me.txtPONo.Text = "" Then
            If UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Yearly" Then
                Return UtilityDAL.GetSerialNo("PRD" + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "ProductionMasterTable", "Production_No", trans)
            ElseIf UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Monthly" Then
                Return UtilityDAL.GetNextDocNo("PRD" & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "ProductionMasterTable", "Production_No", trans)
            Else
                Return UtilityDAL.GetNextDocNo("PRD", 6, "ProductionMasterTable", "Production_No", trans)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' TASK TFS2674
    ''' </summary>
    ''' <param name="TicketId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DoInActiveCostCentre(ByVal TicketId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "Update tblDefCostCenter SET Active = 0 WHERE Name IN (SELECT TicketNo FROM PlanTicketsMaster WHERE PlanTicketsMasterID = " & TicketId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
            Return False
        Finally
            Conn.Close()
        End Try
    End Function

    Public Shared Function GetVoucherNo(ByVal CloseBatchId As Integer) As String
        Dim ProductionNo As String = ""
        Try
            Dim str As String = String.Empty
            str = "SELECT IsNull(ProductionMasterTable.Production_No, '') AS ProductionNo FROM CloseBatch INNER JOIN ProductionMasterTable AS Production ON CloseBatch.ProductionId = Production.Production_ID WHERE CloseBatch.CloseBatchId = " & CloseBatchId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                ProductionNo = dt.Rows(0).Item(0).ToString
            End If
            Return ProductionNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

