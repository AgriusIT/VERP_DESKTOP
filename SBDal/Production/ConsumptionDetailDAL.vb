''TASK:907 Done by Ameen to write down this class and its functions to handle detail record related queries.
''TAKS: 1009 Items on consumption should be loaded ticket, tag and department wise from Estimation
''TASK TFS1436 Muhammad Ameen : If two different issuances are made against same ticket. After that one is consumed but system does not let other issuance to be updated. on 19-09-2017
Imports Janus.Windows.GridEX
Imports SBModel
Imports System.Data.SqlClient
Public Class ConsumptionDetailDAL
    Public Function Insert(ByVal Obj As ItemConsumptionMaster, ByVal Trans As SqlTransaction) As Boolean
        Dim Query As String = ""
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,
            For Each Detail As ItemConsumptionDetail In Obj.Detail
                Query = "Insert Into ItemConsumptionDetail(ConsumptionId, ArticleId, Qty, Rate, DispatchId, DispatchDetailId, LocationId, Comments, EstimationId, ParentTag#, DepartmentId, TicketId) " _
                  & " Values(IDENT_CURRENT('ItemConsumptionMaster'), " & Detail.ArticleId & ", " & Detail.Qty & ", " & Detail.Rate & ", " & Detail.DispatchId & ", " & Detail.DispatchDetailId & ", " & Detail.LocationId & ", '" & Detail.Comments.Replace("'", "''") & "', " & Detail.EstimationId & ", " & Detail.ParentTagNo & ", " & Detail.DepartmentId & ", " & Detail.TicketId & ")"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                'TASK:917 maintainance of issued qty upon consumption. Ameen
                ''Below line is commented against TASK TFS1436
                'Query = " Update DispatchDetailTable Set ConsumedQty=IsNull(ConsumedQty, 0) + " & Detail.Qty & " Where EstimationId =" & Detail.EstimationId & " And ArticleDefId = " & Detail.ArticleId & " And SubDepartmentId =" & Detail.DepartmentId & ""
                ''TASK TFS1436. Addtion of DispatchId Column
                Query = " Update DispatchDetailTable Set ConsumedQty=IsNull(ConsumedQty, 0) + " & Detail.Qty & " Where TicketId =" & Detail.TicketId & " And ArticleDefId = " & Detail.ArticleId & " And SubDepartmentId =" & Detail.DepartmentId & ""

                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                'END TASK:917
                ''TAKS: 1009
                Query = " IF NOT EXISTS(Select IsNull(TrackEstimationId, 0) As TrackEstimationId From tblTrackEstimationConsumption Where ArticleId =" & Detail.ArticleId & " And TicketId =" & Detail.TicketId & " And DepartmentId =" & Detail.DepartmentId & ") Insert Into tblTrackEstimationConsumption(ArticleId, ConsumedQty, EstimatedQty, EstimationId, ConsumptionId, ConsumptionDetailId, DepartmentId, ParentTag#, TicketId) " _
                         & " Values(" & Detail.ArticleId & ", " & Detail.Qty & ", " & Detail.EstimatedQty & ", " & Detail.EstimationId & ", ident_current('ItemConsumptionMaster'), ident_current('ItemConsumptionDetail'), " & Detail.DepartmentId & ", " & Detail.ParentTagNo & ", " & Detail.TicketId & ") " _
                         & " Else UPDATE tblTrackEstimationConsumption Set ConsumedQty =IsNull(ConsumedQty,0) + " & Detail.Qty & " Where ArticleId =" & Detail.ArticleId & " And TicketId =" & Detail.TicketId & " And DepartmentId =" & Detail.DepartmentId & ""
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                ''END TAKS: 1009
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal Obj As ItemConsumptionMaster, ByVal Trans As SqlTransaction) As Boolean
        Dim Query As String = ""
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,
            For Each Detail As ItemConsumptionDetail In Obj.Detail
                Query = " IF NOT EXISTS(Select IsNull(ConsumptionDetailId, 0) As ConsumptionDetailId From ItemConsumptionDetail Where ConsumptionDetailId =" & Detail.ConsumptionDetailId & ") Insert Into ItemConsumptionDetail(ConsumptionId, ArticleId, Qty, Rate, DispatchId, DispatchDetailId, LocationId, Comments, EstimationId, ParentTag#, DepartmentId, TicketId) " _
                  & " Values(" & Obj.ComsumptionId & ", " & Detail.ArticleId & ", " & Detail.Qty & ", " & Detail.Rate & ", " & Detail.DispatchId & ", " & Detail.DispatchDetailId & ", " & Detail.LocationId & ", '" & Detail.Comments.Replace("'", "''") & "', " & Detail.EstimationId & ", " & Detail.ParentTagNo & ", " & Detail.DepartmentId & ", " & Detail.TicketId & ") " _
                  & " Else Update ItemConsumptionDetail Set ConsumptionId =" & Obj.ComsumptionId & ", ArticleId = " & Detail.ArticleId & ", Qty=" & Detail.Qty & ", Rate=" & Detail.Rate & ", DispatchId=" & Detail.DispatchId & ", DispatchDetailId= " & Detail.DispatchDetailId & ", LocationId = " & Detail.LocationId & ", Comments= '" & Detail.Comments.Replace("'", "''") & "', EstimationId = " & Detail.EstimationId & ", ParentTag#= " & Detail.ParentTagNo & ", DepartmentId= " & Detail.DepartmentId & ", TicketId = " & Detail.TicketId & "  Where ConsumptionDetailId = " & Detail.ConsumptionDetailId & ""
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                'TASK:917 maintainance of issued qty upon consumption. Ameen
                ''TASK TFS1436 Addition of DispatchId column
                Query = " Update DispatchDetailTable Set ConsumedQty=IsNull(ConsumedQty, 0) + " & Detail.Qty & " Where TicketId =" & Detail.TicketId & " And ArticleDefId = " & Detail.ArticleId & " And SubDepartmentId =" & Detail.DepartmentId & ""
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                'END TASK:917
                Query = " IF NOT EXISTS(Select IsNull(TrackEstimationId, 0) As TrackEstimationId From tblTrackEstimationConsumption Where ArticleId =" & Detail.ArticleId & " And TicketId =" & Detail.TicketId & " And DepartmentId =" & Detail.DepartmentId & ") Insert Into tblTrackEstimationConsumption(ArticleId, ConsumedQty, EstimatedQty, EstimationId, ConsumptionId, ConsumptionDetailId, DepartmentId, ParentTag#, TicketId) " _
                       & " Values(" & Detail.ArticleId & ", " & Detail.Qty & ", " & Detail.EstimatedQty & ", " & Detail.EstimationId & ", ident_current('ItemConsumptionMaster'), ident_current('ItemConsumptionDetail'), " & Detail.DepartmentId & ", " & Detail.ParentTagNo & ", " & Detail.TicketId & ") " _
                       & " Else UPDATE tblTrackEstimationConsumption Set ConsumedQty =IsNull(ConsumedQty, 0) + " & Detail.Qty & " Where ArticleId =" & Detail.ArticleId & " And TicketId =" & Detail.TicketId & " And DepartmentId =" & Detail.DepartmentId & ""
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal ConsumptionId As Integer, ByVal Trans As SqlTransaction) As Boolean
        Dim Query As String = ""
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,
            Query = " Delete From ItemConsumptionDetail Where ConsumptionId =" & ConsumptionId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal ConsumptionDetailId As Integer) As Boolean
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
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,
            Command.CommandText = " Delete From ItemConsumptionDetail Where ConsumptionDetailId =" & ConsumptionDetailId & ""
            Command.ExecuteNonQuery()
            Transaction.Commit()
            Return True
        Catch ex As Exception
            If Not Transaction Is Nothing Then
                Transaction.Rollback()
            End If
            Throw ex
        End Try
    End Function
    Public Function GetDetail(ByVal ConsumptionId As Integer) As DataTable
        Try
            ''TASK:977 done by Ameen added checkqty column to validate the input qty on item consumption.
            'Dim Query As String = " Select IsNull(ItemConsumptionDetail.LocationId, 0) As LocationId, IsNull(ConsumptionDetailId, 0) As ConsumptionDetailId, ItemConsumptionDetail.ConsumptionId, ItemConsumptionDetail.ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, IsNull(ItemConsumptionDetail.Qty, 0) As Qty, IsNull(DispatchDetailTable.ConsumedQty, 0) As ConsumedQty, Convert(Float, (IsNull(DispatchDetailTable.Qty, 0)-IsNull(DispatchDetailTable.ConsumedQty, 0)-IsNull(DispatchDetailTable.ReturnedTotalQty, 0))) As AvailableQty, IsNull(ItemConsumptionDetail.Rate, 0) Rate, IsNull(ItemConsumptionDetail.Qty, 0)*IsNull(ItemConsumptionDetail.Rate, 0) As Total, IsNull(ItemConsumptionDetail.DispatchId, 0) As DispatchId, IsNull(ItemConsumptionDetail.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.ArticleGroupId, 0) As CGSAccountId, ItemConsumptionDetail.Comments, IsNull(ItemConsumptionDetail.Qty, 0) As CheckQty From ItemConsumptionDetail INNER JOIN ItemConsumptionMaster ON ItemConsumptionDetail.ConsumptionId = ItemConsumptionMaster.ConsumptionId " _
            '                      & " LEFT OUTER JOIN DispatchDetailTable ON ItemConsumptionDetail.DispatchDetailId = DispatchDetailTable.DispatchDetailId INNER JOIN ArticleDefTable ON ItemConsumptionDetail.ArticleId = ArticleDefTable.ArticleId " _
            '                      & " LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN dbo.ArticleGroupDefTable Article_Group ON ArticleDefTable.ArticleGroupId = Article_Group.ArticleGroupId INNER JOIN tblDefLocation ON ItemConsumptionDetail.LocationId = tblDefLocation.Location_ID " _
            '                      & " Where ItemConsumptionMaster.ConsumptionId = " & ConsumptionId & ""

            'Dim Query As String = " Select IsNull(ItemConsumptionDetail.LocationId, 0) As LocationId, tblDefLocation.Location_Code As Location, IsNull(ConsumptionDetailId, 0) As ConsumptionDetailId, ItemConsumptionDetail.ConsumptionId, ItemConsumptionDetail.ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, IsNull(ItemConsumptionDetail.Qty, 0) As Qty, IsNull(DispatchDetailTable.ConsumedQty, 0) As ConsumedQty, Convert(Float, (IsNull(DispatchDetailTable.Qty, 0)-IsNull(DispatchDetailTable.ConsumedQty, 0)-IsNull(DispatchDetailTable.ReturnedTotalQty, 0))) As AvailableQty, IsNull(ItemConsumptionDetail.Rate, 0) Rate, IsNull(ItemConsumptionDetail.Qty, 0)*IsNull(ItemConsumptionDetail.Rate, 0) As Total, IsNull(ItemConsumptionDetail.DispatchId, 0) As DispatchId, IsNull(ItemConsumptionDetail.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.ArticleGroupId, 0) As CGSAccountId, ItemConsumptionDetail.Comments, IsNull(ItemConsumptionDetail.Qty, 0) As CheckQty From ItemConsumptionDetail INNER JOIN ItemConsumptionMaster ON ItemConsumptionDetail.ConsumptionId = ItemConsumptionMaster.ConsumptionId " _
            '                    & " LEFT OUTER JOIN DispatchDetailTable ON ItemConsumptionDetail.DispatchDetailId = DispatchDetailTable.DispatchDetailId INNER JOIN ArticleDefTable ON ItemConsumptionDetail.ArticleId = ArticleDefTable.ArticleId " _
            '                    & " LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN dbo.ArticleGroupDefTable Article_Group ON ArticleDefTable.ArticleGroupId = Article_Group.ArticleGroupId INNER JOIN tblDefLocation ON ItemConsumptionDetail.LocationId = tblDefLocation.Location_ID " _
            '                    & " Where ItemConsumptionMaster.ConsumptionId = " & ConsumptionId & ""


            ''TAKS: 1009
            ''TASK:TFS1137 Added replaced CGSAccount with SubSubId Account.
            Dim Query As String = " Select IsNull(ItemConsumptionDetail.LocationId, 0) As LocationId, tblDefLocation.Location_Code As Location, IsNull(ItemConsumptionDetail.ConsumptionDetailId, 0) As ConsumptionDetailId, ItemConsumptionDetail.ConsumptionId, ItemConsumptionDetail.ArticleId, ArticleDefTable.ArticleCode, " _
                                  & " ArticleDefTable.ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, IsNull(ItemConsumptionDetail.Qty, 0) As Qty, IsNull(ConsumptionTrack.ConsumedQty, 0) As ConsumedQty, Convert(Float, (IsNull(ConsumptionTrack.EstimatedQty, 0)-IsNull(ConsumptionTrack.ConsumedQty, 0))) As AvailableQty, " _
                                  & " IsNull(ItemConsumptionDetail.Rate, 0) Rate, IsNull(ItemConsumptionDetail.Qty, 0)*IsNull(ItemConsumptionDetail.Rate, 0) As Total, IsNull(ItemConsumptionDetail.DispatchId, 0) As DispatchId, IsNull(ItemConsumptionDetail.DispatchDetailId, 0) As DispatchDetailId, " _
                                  & " IsNull(Article_Group.SubSubId, 0) As CGSAccountId, ItemConsumptionDetail.Comments, IsNull(ItemConsumptionDetail.Qty, 0) As CheckQty, IsNull(ConsumptionTrack.EstimationId, 0) As EstimationId, IsNull(ConsumptionTrack.ParentTag#, 0) As ParentTag#, " _
                                  & " IsNull(ConsumptionTrack.EstimatedQty, 0) As EstimatedQty, IsNull(ItemConsumptionDetail.DepartmentId, 0) As DepartmentId, Issue.Qty As [TotalIssuedQty], Issue.ConsumedQty As [TotalConsumedQty], Issue.ReturnedTotalQty As [TotalReturnedQty], IsNull(ItemConsumptionDetail.TicketId, 0) As TicketId, PlanMasterTable.PlanNo, PlanTicketsMaster.BatchNo As TicketNo " _
                                  & " From ItemConsumptionDetail INNER JOIN ItemConsumptionMaster ON ItemConsumptionDetail.ConsumptionId = ItemConsumptionMaster.ConsumptionId " _
                                  & " LEFT OUTER JOIN tblTrackEstimationConsumption As ConsumptionTrack ON ItemConsumptionDetail.TicketId = ConsumptionTrack.TicketId And  ItemConsumptionDetail.DepartmentId = ConsumptionTrack.DepartmentId And  ItemConsumptionDetail.ArticleId = ConsumptionTrack.ArticleId INNER JOIN ArticleDefTable ON ItemConsumptionDetail.ArticleId = ArticleDefTable.ArticleId " _
                                  & " LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId LEFT OUTER JOIN dbo.ArticleGroupDefTable Article_Group ON ArticleDefTable.ArticleGroupId = Article_Group.ArticleGroupId INNER JOIN tblDefLocation ON ItemConsumptionDetail.LocationId = tblDefLocation.Location_ID " _
                                  & " LEFT OUTER JOIN (Select ArticleDefId, SubDepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, IsNull(TicketId, 0) As TicketId From DispatchDetailTable Group by ArticleDefId, SubDepartmentId, TicketId) As Issue ON IsNull(ItemConsumptionDetail.TicketId, 0) = Issue.TicketId And IsNull(ItemConsumptionDetail.DepartmentId , 0) = Issue.SubDepartmentId And ItemConsumptionDetail.ArticleId = Issue.ArticleDefId " _
                                  & " LEFT OUTER JOIN PlanTicketsMaster ON ItemConsumptionDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                                  & " LEFT OUTER JOIN PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanId  " _
                                  & " Where ItemConsumptionMaster.ConsumptionId = " & ConsumptionId & " "
            ''END TAKS: 1009
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "Qty*Rate"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''TAKS: 1009 getting tag wise estimation detail
    Public Function DisplayEstimationDetail(ByVal TagNo As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            'Dim flgAvrRate As Boolean = getConfigValueByType("AvgRate")
            'Dim Query As String = "SELECT IsNull(Recv_D.LocationId, 0) As LocationId, 0 As ConsumptionDetailId, 0 As ConsumptionId, Article.ArticleId, Article.ArticleCode, Article.ArticleDescription AS ArticleDescription, ArticleColorDefTable.ArticleColorName as Color,  Cast((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) As float) AS Qty, Convert(Float, IsNull(Recv_D.ConsumedQty, 0)) As ConsumedQty, Convert(Float, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)))) As AvailableQty, IsNull(Recv_D.Price, 0) as Rate, " _
            '                 & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
            '                 & " IsNull(Recv_D.DispatchId, 0) As DispatchId, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, Recv_D.Comments, Cast((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0))) As float) AS CheckQty"
            Query = "SELECT 1 As LocationID, 0 As ConsumptionDetailId, 0 As ConsumptionId, Article.ArticleId, Article.ArticleCode, Article.ArticleDescription AS ArticleDescription, ArticleColorDefTable.ArticleColorName as Color, (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)) AS Qty, IsNull(tblTrackEstimationConsumption.ConsumedQty, 0) As ConsumedQty, (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)) AS AvailableQty, IsNull(Recv_D.Price, 0) As Rate, " _
                & " (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0))  *  IsNull(Recv_D.Price, 0)  AS Total, " _
                & " 0 As DispatchId, 0 As DispatchDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, tblTrackEstimationConsumption.Comments, Cast((IsNull(Recv_D.Qty, 0)-(IsNull(tblTrackEstimationConsumption.ConsumedQty, 0))) As float) AS CheckQty, IsNull(Recv_D.EstimationId, 0) As EstimationId FROM  " _
                & " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(IsNull(Recv_D.Quantity, 0)) As Quantity FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And Recv_D.ParentTag# =" & TagNo & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Convert(Int, Recv_D.ProductId) " _
                & " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimationConsumption ON Recv_D.EstimationId = tblTrackEstimationConsumption.EstimationId And Recv_D.ProductId = tblTrackEstimationConsumption.ArticleId And Recv_D.SubDepartmentID = tblTrackEstimationConsumption.DepartmentId Where Recv_D.ParentTag# =" & TagNo & " And (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)) > 0 "
            Dim DtDisplayDetail As DataTable = UtilityDAL.GetDataTable(Query)
            Return DtDisplayDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '' END TAKS: 1009
    Private Function GetConsumedQty(ByVal ConsumptionDetailId As Integer) As Double
        Dim dt As New DataTable
        Dim Str As String = String.Empty
        Try
            Str = "Select ConsumptionDetailId, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty FROM ConsumptionDetailTable Where ConsumptionDetailId = " & ConsumptionDetailId & " Group By ConsumptionDetailId "
            dt = UtilityDAL.GetDataTable(Str)
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(1).ToString)
            Else
                Return Val(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
