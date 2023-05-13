''TASK:907 Done by Ameen to write down this class and its functions to handle master record related queries.
''TAKS: 1009 Items on consumption should be loaded ticket, tag and department wise from Estimation
''TASK: TFS1133 Getting issuance to return issuance ticket wise.
Imports Janus.Windows.GridEX
Imports SBModel
Imports System.Data.SqlClient
Public Class ConsumptionMasterDAL

    Public Function Insert(ByVal Ojb As ItemConsumptionMaster, ByVal FormName As String, ByVal MyCompnayId As Integer) As Boolean
        Dim Query As String = ""
        Dim Connection As SqlConnection
        Dim Command As SqlCommand
        Dim Transaction As SqlTransaction
        Try
            Connection = New SqlConnection(SQLHelper.CON_STR)
            If Connection.State = ConnectionState.Closed Then
                Connection.Open()
            End If
            Transaction = Connection.BeginTransaction
            Command = New SqlCommand()
            Command.Transaction = Transaction
            Command.Connection = Connection
            '   [ComsumptionId] [int] IDENTITY(1,1) NOT NULL,
            '[DocNo] [nvarchar](200) NULL,
            '[DocDate] [datetime] NULL,
            '[Remarks] [nvarchar](500) NULL,
            '[PlanId] [int] NULL,
            '[TicketId] [int] NULL,
            '[DepartmentId] [int] NULL,
            '[StoreIssuanceAccountId] [int] NULL,

            Command.CommandText = "Insert Into ItemConsumptionMaster(DocNo, DocDate, Remarks, PlanId, TicketId, DepartmentId, StoreIssuanceAccountId, DispatchId) " _
                                & " Values(N'" & Ojb.DocNo.Replace("'", "''") & "', '" & Ojb.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Ojb.Remarks.Replace("'", "''") & "', " & Ojb.PlanId & "," & Ojb.TicketId & ", " & Ojb.DepartmentId & ", " & Ojb.StoreIssuanceAccountId & ", " & Ojb.DispatchId & ")"
            Command.ExecuteNonQuery()
            Call New ConsumptionDetailDAL().Insert(Ojb, Transaction)
            ''Below row is commented against TASK TFS2741 on 15-03-2018
            'AddVoucher(Ojb, Transaction, FormName, MyCompnayId)
            Transaction.Commit()
            Return True
        Catch ex As Exception
            If Not Transaction Is Nothing Then
                Transaction.Rollback()
            End If
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal Ojb As ItemConsumptionMaster, ByVal FormName As String, ByVal VoucherId As Integer, ByVal MyCompanyId As Integer) As Boolean
        Dim Query As String = ""
        Dim Connection As SqlConnection
        Dim Command As SqlCommand
        Dim Transaction As SqlTransaction
        Dim DataAdaptor As SqlDataAdapter
        Dim Dt As DataTable
        Try
            Connection = New SqlConnection(SQLHelper.CON_STR)
            If Connection.State = ConnectionState.Closed Then
                Connection.Open()
            End If
            Transaction = Connection.BeginTransaction
            Command = New SqlCommand()
            Command.Transaction = Transaction
            Command.Connection = Connection
            '   [ComsumptionId] [int] IDENTITY(1,1) NOT NULL,
            '[DocNo] [nvarchar](200) NULL,
            '[DocDate] [datetime] NULL,
            '[Remarks] [nvarchar](500) NULL,
            '[PlanId] [int] NULL,
            '[TicketId] [int] NULL,
            '[DepartmentId] [int] NULL,
            '[StoreIssuanceAccountId] [int] NULL,
            Command.CommandText = "Update ItemConsumptionMaster Set DocNo = N'" & Ojb.DocNo.Replace("'", "''") & "', DocDate='" & Ojb.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks =N'" & Ojb.Remarks.Replace("'", "''") & "' , PlanId=" & Ojb.PlanId & ", TicketId=" & Ojb.TicketId & ", DepartmentId=" & Ojb.DepartmentId & ", StoreIssuanceAccountId=" & Ojb.StoreIssuanceAccountId & ", DispatchId=" & Ojb.DispatchId & " " _
                                & " Where ConsumptionId=" & Ojb.ComsumptionId & ""
            Command.ExecuteNonQuery()
            ''TAKS: 1009
            Command.CommandText = "Select IsNull(ArticleId, 0) As ArticleId, IsNull(EstimationId, 0) As EstimationId, IsNull(ItemConsumptionDetail.DepartmentId, 0) As DepartmentId, IsNull(Qty, 0) As Qty, IsNull(ItemConsumptionMaster.DispatchId, 0) As DispatchId, IsNull(ItemConsumptionMaster.TicketId, 0) As TicketId From ItemConsumptionDetail Inner Join ItemConsumptionMaster On  ItemConsumptionDetail.ConsumptionId = ItemConsumptionMaster.ConsumptionId Where ItemConsumptionMaster.ConsumptionId = " & Ojb.ComsumptionId & ""
            DataAdaptor = New SqlDataAdapter
            Dt = New DataTable
            DataAdaptor.SelectCommand = Command
            DataAdaptor.Fill(Dt)
            For Each Row As DataRow In Dt.Rows
                Command.CommandText = " Update DispatchDetailTable Set ConsumedQty = IsNull(ConsumedQty, 0)- " & Row("Qty") & " Where TicketId =" & Row("TicketId") & " And ArticleDefId =" & Row("ArticleId") & " And SubDepartmentId =" & Row("DepartmentId") & ""
                Command.ExecuteNonQuery()
            Next
            Command.CommandText = "Select IsNull(ArticleId, 0) As ArticleId, IsNull(ItemConsumptionDetail.EstimationId, 0) As EstimationId, IsNull(ItemConsumptionDetail.ParentTag#, 0) As ParentTag#, IsNull(ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(ItemConsumptionDetail.DepartmentId, 0) As DepartmentId, IsNull(Qty, 0) As Qty, IsNull(ItemConsumptionDetail.TicketId, 0) As TicketId From ItemConsumptionDetail Inner Join ItemConsumptionMaster On  ItemConsumptionDetail.ConsumptionId = ItemConsumptionMaster.ConsumptionId Where ItemConsumptionMaster.ConsumptionId = " & Ojb.ComsumptionId & ""
            ''Update Qty
            DataAdaptor = New SqlDataAdapter
            Dt = New DataTable
            DataAdaptor.SelectCommand = Command
            DataAdaptor.Fill(Dt)
            'For Each Row As DataRow In Dt.Rows
            '    Command.CommandText = " Update DispatchDetailTable Set ConsumedQty = IsNull(ConsumedQty, 0)- " & Row("Qty") & " Where DispatchDetailId =" & Row("DispatchDetailId") & " And ArticleDefId =" & Row("ArticleId") & " And SubDepartmentId =" & Row("DepartmentId") & ""
            '    Command.ExecuteNonQuery()
            'Next
            For Each Row As DataRow In Dt.Rows
                Command.CommandText = " Update tblTrackEstimationConsumption Set ConsumedQty = IsNull(ConsumedQty, 0)- " & Row("Qty") & " Where  TicketId =" & Row("TicketId") & " And ArticleId =" & Row("ArticleId") & " And DepartmentId =" & Row("DepartmentId") & ""
                Command.ExecuteNonQuery()
            Next
            ''End TAKS: 1009
            '' END TAKS: 1009
            Call New ConsumptionDetailDAL().Update(Ojb, Transaction)
            ''Below row is commented against TASK TFS2741 on 15-03-2018
            'UpdateVoucher(Ojb, Transaction, FormName, MyCompanyId, VoucherId)
            Transaction.Commit()
            Return True
        Catch ex As Exception
            If Not Transaction Is Nothing Then
                Transaction.Rollback()
            End If
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal ConsumptionId As Integer, ByVal VoucherId As Integer) As Boolean
        Dim Query As String = ""
        Dim Connection As SqlConnection
        Dim Command As SqlCommand
        Dim Transaction As SqlTransaction
        Dim DataAdaptor As SqlDataAdapter
        Dim Dt As DataTable
        Try
            Connection = New SqlConnection(SQLHelper.CON_STR)
            If Connection.State = ConnectionState.Closed Then
                Connection.Open()
            End If
            Transaction = Connection.BeginTransaction
            Command = New SqlCommand()
            Command.Transaction = Transaction
            Command.Connection = Connection
            '   [ComsumptionId] [int] IDENTITY(1,1) NOT NULL,
            '[DocNo] [nvarchar](200) NULL,
            '[DocDate] [datetime] NULL,
            '[Remarks] [nvarchar](500) NULL,
            '[PlanId] [int] NULL,
            '[TicketId] [int] NULL,
            '[DepartmentId] [int] NULL,
            '[StoreIssuanceAccountId] [int] NULL,

           
            '' Update Qty
            ''TASK:917 maintainance of issued qty upon consumption. Ameen
            Command.CommandText = "Select IsNull(ArticleId, 0) As ArticleId, IsNull(EstimationId, 0) As EstimationId, IsNull(ItemConsumptionDetail.DepartmentId, 0) As DepartmentId, IsNull(Qty, 0) As Qty, IsNull(ItemConsumptionMaster.DispatchId, 0) As DispatchId, IsNull(ItemConsumptionDetail.TicketId, 0) As TicketId From ItemConsumptionDetail Inner Join ItemConsumptionMaster On  ItemConsumptionDetail.ConsumptionId = ItemConsumptionMaster.ConsumptionId Where ItemConsumptionMaster.ConsumptionId = " & ConsumptionId & ""
            DataAdaptor = New SqlDataAdapter
            Dt = New DataTable
            DataAdaptor.SelectCommand = Command
            DataAdaptor.Fill(Dt)
            For Each Row As DataRow In Dt.Rows
                Command.CommandText = " Update DispatchDetailTable Set ConsumedQty = IsNull(ConsumedQty, 0)- " & Row("Qty") & " Where TicketId =" & Row("TicketId") & " And ArticleDefId =" & Row("ArticleId") & " And SubDepartmentId =" & Row("DepartmentId") & " "
                Command.ExecuteNonQuery()
            Next
            ''TAKS: 1009
            Command.CommandText = "Select IsNull(ArticleId, 0) As ArticleId, IsNull(ItemConsumptionDetail.EstimationId, 0) As EstimationId, IsNull(ItemConsumptionDetail.ParentTag#, 0) As ParentTag#, IsNull(ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(ItemConsumptionDetail.DepartmentId, 0) As DepartmentId, IsNull(Qty, 0) As Qty, IsNull(ItemConsumptionDetail.TicketId, 0) As TicketId From ItemConsumptionDetail Inner Join ItemConsumptionMaster On  ItemConsumptionDetail.ConsumptionId = ItemConsumptionMaster.ConsumptionId Where ItemConsumptionMaster.ConsumptionId = " & ConsumptionId & ""

            ''Update Qty
            DataAdaptor = New SqlDataAdapter
            Dt = New DataTable
            DataAdaptor.SelectCommand = Command
            DataAdaptor.Fill(Dt)

            'For Each Row As DataRow In Dt.Rows
            '    Command.CommandText = " Update DispatchDetailTable Set ConsumedQty = IsNull(ConsumedQty, 0)- " & Row("Qty") & " Where DispatchDetailId =" & Row("DispatchDetailId") & " And ArticleDefId =" & Row("ArticleId") & " And SubDepartmentId =" & Row("DepartmentId") & ""
            '    Command.ExecuteNonQuery()
            'Next
            For Each Row As DataRow In Dt.Rows
                Command.CommandText = " Update tblTrackEstimationConsumption Set ConsumedQty = IsNull(ConsumedQty, 0)- " & Row("Qty") & " Where TicketId =" & Row("TicketId") & " And DepartmentId =" & Row("DepartmentId") & " And ArticleId =" & Row("ArticleId") & ""
                Command.ExecuteNonQuery()
            Next

            Command.CommandText = " Delete FROM  tblTrackEstimationConsumption WHERE ConsumedQty < 1"
            Command.ExecuteNonQuery()
            ''End TAKS: 1009
            '' End TASK:917 
            Command.CommandText = "Delete FROM  ItemConsumptionMaster  " _
                               & " Where ConsumptionId=" & ConsumptionId & ""
            Command.ExecuteNonQuery()

            Call New ConsumptionDetailDAL().Delete(ConsumptionId, Transaction)
            ''Below row is commented against TASK TFS2741 on 15-03-2018

            'DeleteVoucher(VoucherId, Transaction)
            Transaction.Commit()
            Return True
        Catch ex As Exception
            If Not Transaction Is Nothing Then
                Transaction.Rollback()
            End If
            Throw ex
        End Try
    End Function

    Public Function GetAll() As DataTable
        Try
            Dim Query As String = "Select ConsumptionId, DocNo, DocDate, Remarks, PlanId, TicketId, DepartmentId, tblProSteps.prod_step As Department, StoreIssuanceAccountId, IsNull(DispatchId, 0) As DispatchId From ItemConsumptionMaster LEFT OUTER JOIN tblProSteps ON tblProSteps.ProdStep_Id = ItemConsumptionMaster.DepartmentId Order By ConsumptionId DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStoreIssuanceByDep(ByVal TicketId As Integer, ByVal DepartmentId As Integer) As DataTable
        Try
       
            ''TASK:977 done by Ameen added checkqty column to validate the input qty on item consumption.
            Dim Query As String = "SELECT IsNull(Recv_D.LocationId, 0) As LocationId, 0 As ConsumptionDetailId, 0 As ConsumptionId, Article.ArticleId, Article.ArticleCode, Article.ArticleDescription AS ArticleDescription, ArticleColorDefTable.ArticleColorName as Color,  Cast(IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) As float) AS Qty, Convert(Float, IsNull(Recv_D.ConsumedQty, 0)) As ConsumedQty, Convert(Float, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)))) As AvailableQty, IsNull(Recv_D.Price, 0) as Rate, " _
                              & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
                              & " IsNull(Recv_D.DispatchId, 0) As DispatchId, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, Recv_D.Comments, Cast(IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) As float) AS CheckQty FROM dbo.DispatchDetailTable As Recv_D  INNER JOIN dbo.DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId  LEFT OUTER JOIN " _
                              & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                              & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
                              & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id " _
                              & " Where Recv.PlanTicketId=" & TicketId & " And Recv.DepartmentId =" & DepartmentId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStoreIssuance(ByVal IssuanceId As Integer, ByVal DepartmentId As Integer) As DataTable
        Try
            ''TASK:977 done by Ameen added checkqty column to validate the input qty on item consumption.
            Dim Query As String = "SELECT IsNull(Recv_D.LocationId, 0) As LocationId, 0 As ConsumptionDetailId, 0 As ConsumptionId, Article.ArticleId, Article.ArticleCode, Article.ArticleDescription AS ArticleDescription, ArticleColorDefTable.ArticleColorName as Color,  Cast((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) As float) AS Qty, Convert(Float, IsNull(Recv_D.ConsumedQty, 0)) As ConsumedQty, Convert(Float, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)))) As AvailableQty, IsNull(Recv_D.Price, 0) as Rate, " _
                              & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
                              & " IsNull(Recv_D.DispatchId, 0) As DispatchId, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, Recv_D.Comments, Cast((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) As float) AS CheckQty FROM dbo.DispatchDetailTable As Recv_D  INNER JOIN dbo.DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId  LEFT OUTER JOIN " _
                              & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                              & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
                              & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id " _
                              & " Where Recv.DispatchId=" & IssuanceId & " And Recv_D.SubDepartmentId =" & DepartmentId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "Qty*Rate"
            'dt.DisplayExpression()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStoreIssuance(ByVal IssuanceId As Integer) As DataTable
        Try
            ''TASK:977 done by Ameen added checkqty column to validate the input qty on item consumption.
            Dim Query As String = "SELECT IsNull(Recv_D.LocationId, 0) As LocationId, 0 As ConsumptionDetailId, 0 As ConsumptionId, Article.ArticleId, Article.ArticleCode, Article.ArticleDescription AS ArticleDescription, ArticleColorDefTable.ArticleColorName as Color,  Cast((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) As float) AS Qty, Convert(Float, IsNull(Recv_D.ConsumedQty, 0)) As ConsumedQty, Convert(Float, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)))) As AvailableQty, IsNull(Recv_D.Price, 0) as Rate, " _
                              & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
                              & " IsNull(Recv_D.DispatchId, 0) As DispatchId, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, Recv_D.Comments, Cast((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) As float) AS CheckQty FROM dbo.DispatchDetailTable As Recv_D  INNER JOIN dbo.DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId  LEFT OUTER JOIN " _
                              & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                              & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
                              & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id " _
                              & " Where Recv.DispatchId=" & IssuanceId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "Qty*Rate"
            'dt.DisplayExpression()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStoreIssuanceByTicket(ByVal TicketId As Integer) As DataTable
        Try
            ''TASK:977 done by Ameen added checkqty column to validate the input qty on item consumption.
            Dim Query As String = "SELECT IsNull(Recv_D.LocationId, 0) As LocationId, 0 As ConsumptionDetailId, 0 As ConsumptionId, Article.ArticleId, Article.ArticleCode, Article.ArticleDescription AS ArticleDescription, ArticleColorDefTable.ArticleColorName as Color,  Cast(IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) As float) AS Qty, Convert(Float, IsNull(Recv_D.ConsumedQty, 0)) As ConsumedQty, Convert(Float, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)))) As AvailableQty, IsNull(Recv_D.Price, 0) as Rate, " _
                                & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
                                & " IsNull(Recv_D.DispatchId, 0) As DispatchId, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, Recv_D.Comments, Cast(IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) As float) AS CheckQty FROM dbo.DispatchDetailTable As Recv_D  INNER JOIN dbo.DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId  LEFT OUTER JOIN " _
                                & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                                & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
                                & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id " _
                                & " Where Recv.PlanTicketId=" & TicketId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "Qty*Rate"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''TASK:919 Added this funtion to get return issuance by Ameen
    Public Function GetToReturnIssuance(ByVal DepartmentId As Integer, ByVal IssuanceId As Integer, ByVal CostSheetType As String) As DataTable
        Try
            '    Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
            '& " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) As TotalQty, Recv_D.DispatchDetailId, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS CheckQty FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where  Recv.DispatchId=" & IssuanceId & " And Recv_D.SubDepartmentId =" & DepartmentId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"
            '    Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            '       '       '    dt.AcceptChanges()
            '       Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
            '& " Convert(float, ((IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, " & IIf(CostSheetType = "Error" Or CostSheetType = "Standard Cost Sheet", " ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster]", "''") & ", Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) As TotalQty, 0 As DispatchDetailId, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) AS CheckQty, IsNull(Recv_D2.EstimationId, 0) As EstimationId, IsNull(Recv_D2.DepartmentId, 0) As DepartmentId, tblproSteps.prod_step As Department FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Left Outer Join(Select IsNull(ArticleDefId, 0) As ArticleDefId , IsNull(EstimationId, 0) As EstimationId, IsNull(SubDepartmentID, 0) As DepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty From DispatchDetailTable Group By ArticleDefId, EstimationId, SubDepartmentID) As Recv_D2 ON Recv_D.ArticleDefId = Recv_D2.ArticleDefId And Recv_D.EstimationId = Recv_D2.EstimationId And Recv_D.SubDepartmentID = Recv_D2.DepartmentId LEFT OUTER JOIN tblproSteps On Recv_D2.DepartmentId = tblproSteps.ProdStep_Id " _
            '& " Where  Recv.PlanTicketId=" & IssuanceId & " And Recv_D.SubDepartmentId =" & DepartmentId & " And IsNull(Recv_D2.Qty, 0) > (IsNull(Recv_D2.ConsumedQty, 0)+IsNull(Recv_D2.ReturnedTotalQty, 0))"
            Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
     & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
     & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, " & IIf(CostSheetType = "Error" Or CostSheetType = "Standard Cost Sheet", " ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster]", "''") & ", Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) As TotalQty, Recv_D.DispatchDetailId, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS CheckQty, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.SubDepartmentID, 0) As DepartmentId, tblproSteps.prod_step As Department FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
     & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID LEFT OUTER JOIN tblproSteps On Recv_D.SubDepartmentID = tblproSteps.ProdStep_Id " _
     & " Where  Recv.PlanTicketId=" & IssuanceId & " And Recv_D.SubDepartmentId =" & DepartmentId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"

            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "Qty*Rate"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Added this function to show issues on Return Store Issuance ticket wise
    ''' </summary>
    ''' <param name="TicketId"></param>
    ''' <param name="CostSheetType"></param>
    ''' <returns></returns>
    ''' <remarks> TASK: TFS1133</remarks>
    Public Function GetIssuanceToReturnIssuanceWithTicket(ByVal TicketId As Integer, ByVal CostSheetType As String) As DataTable
        Try
            '    Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
            '& " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) As TotalQty, Recv_D.DispatchDetailId, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS CheckQty FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where  Recv.DispatchId=" & IssuanceId & " And Recv_D.SubDepartmentId =" & DepartmentId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"
            '    Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            '       '    dt.AcceptChanges()
            ''Commented on 27-09-2017
            '       Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
            '& " Convert(float, ((IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, " & IIf(CostSheetType = "Error" Or CostSheetType = "Standard Cost Sheet", " ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster]", "''") & ", Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) As TotalQty, 0 As DispatchDetailId, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) AS CheckQty, IsNull(Recv_D2.EstimationId, 0) As EstimationId, IsNull(Recv_D2.DepartmentId, 0) As DepartmentId, tblproSteps.prod_step As Department FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Left Outer Join(Select IsNull(ArticleDefId, 0) As ArticleDefId, IsNull(EstimationId, 0) As EstimationId, IsNull(SubDepartmentID, 0) As DepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty From DispatchDetailTable Group By ArticleDefId, EstimationId, SubDepartmentID) As Recv_D2 ON Recv_D.ArticleDefId = Recv_D2.ArticleDefId And Recv_D.EstimationId = Recv_D2.EstimationId And Recv_D.SubDepartmentID = Recv_D2.DepartmentId LEFT OUTER JOIN tblproSteps On Recv_D2.DepartmentId = tblproSteps.ProdStep_Id " _
            '& " Where  Recv.PlanTicketId=" & TicketId & " And IsNull(Recv_D2.Qty, 0) > (IsNull(Recv_D2.ConsumedQty, 0)+IsNull(Recv_D2.ReturnedTotalQty, 0))"
            ''Below row is commented on 27-09-2017
            'Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
            '& " Convert(float, ((IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, " & IIf(CostSheetType = "Error" Or CostSheetType = "Standard Cost Sheet", " ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster]", "''") & ", Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) As TotalQty, 0 As DispatchDetailId, (IsNull(Recv_D2.Qty, 0)-(IsNull(Recv_D2.ReturnedTotalQty, 0)+IsNull(Recv_D2.ConsumedQty, 0))) AS CheckQty, IsNull(Recv_D2.EstimationId, 0) As EstimationId, IsNull(Recv_D2.DepartmentId, 0) As DepartmentId, tblproSteps.prod_step As Department FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Left Outer Join(Select IsNull(ArticleDefId, 0) As ArticleDefId, IsNull(EstimationId, 0) As EstimationId, IsNull(SubDepartmentID, 0) As DepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty From DispatchDetailTable Group By ArticleDefId, EstimationId, SubDepartmentID) As Recv_D2 ON Recv_D.ArticleDefId = Recv_D2.ArticleDefId And Recv_D.EstimationId = Recv_D2.EstimationId And Recv_D.SubDepartmentID = Recv_D2.DepartmentId LEFT OUTER JOIN tblproSteps On Recv_D2.DepartmentId = tblproSteps.ProdStep_Id " _
            '& " Where  Recv.PlanTicketId=" & TicketId & " And IsNull(Recv_D2.Qty, 0) > (IsNull(Recv_D2.ConsumedQty, 0)+IsNull(Recv_D2.ReturnedTotalQty, 0))"
            Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
        & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
        & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, " & IIf(CostSheetType = "Error" Or CostSheetType = "Standard Cost Sheet", " ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster]", "''") & ", Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) As TotalQty, Recv_D.DispatchDetailId, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS CheckQty, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.SubDepartmentID, 0) As DepartmentId, tblproSteps.prod_step As Department FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
        & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
        & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
        & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID LEFT OUTER JOIN tblproSteps On Recv_D.SubDepartmentID = tblproSteps.ProdStep_Id " _
        & " Where  Recv.PlanTicketId=" & TicketId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))"

            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "Qty*Rate"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddVoucher(ByVal Obj As ItemConsumptionMaster, ByVal Trans As SqlTransaction, ByVal Source As String, ByVal MyCompanyId As Integer) As Boolean
        Dim Query As String = ""
        Dim VoucherId As Integer = 0
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,

            Query = "  Insert Into tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, cheque_no, cheque_date, post, Source, voucher_code, Remarks) " _
                    & " Values(" & MyCompanyId & ", 1, 1, '" & Obj.DocNo & "', '" & Obj.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', NULL, NULL, 1, N'" & Source & "', '" & Obj.DocNo & "', N'" & Obj.Remarks.Replace("'", "''") & "') Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
            For Each Detail As ItemConsumptionDetail In Obj.Detail
                'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                  & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Obj.StoreIssuanceAccountId & ", 0, " & (Detail.Qty * Detail.Rate) & ", '" & Detail.Comments.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                 & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Detail.CGSAccountId & ", " & (Detail.Qty * Detail.Rate) & ", 0, '" & Detail.Comments.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateVoucher(ByVal Obj As ItemConsumptionMaster, ByVal Trans As SqlTransaction, ByVal Source As String, ByVal MyCompanyId As Integer, ByVal VoucherId As Integer) As Boolean
        Dim Query As String = ""
        'Dim VoucherId As Integer = 0
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,

           

            Query = "  UPDATE tblVoucher SET location_id = " & MyCompanyId & ", finiancial_year_id = 1, voucher_type_id=1, voucher_no='" & Obj.DocNo & "', voucher_date='" & Obj.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', cheque_no=NULL, cheque_date=NULL, post=1, Source= N'" & Source & "', voucher_code='" & Obj.DocNo & "', Remarks=N'" & Obj.Remarks.Replace("'", "''") & "' Where voucher_id=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            Query = "Delete FROM tblVoucherDetail Where voucher_id = " & VoucherId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            For Each Detail As ItemConsumptionDetail In Obj.Detail
                'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                  & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Obj.StoreIssuanceAccountId & ", 0, " & Detail.Qty * Detail.Rate & ", '" & Detail.Comments.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                 & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Detail.CGSAccountId & ", " & Detail.Qty * Detail.Rate & ", 0, '" & Detail.Comments.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteVoucher(ByVal VoucherId As Integer, ByVal Trans As SqlTransaction) As Boolean
        Dim Query As String = ""
        'Dim VoucherId As Integer = 0
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,

            Query = "  Delete From tblVoucherDetail Where voucher_id =" & VoucherId & " "
            SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
            Query = "  Delete From tblVoucher Where voucher_id =" & VoucherId & " "
            SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
           
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
