Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Public Class MRPDAL
    Public Function Add(ByVal MRPlan As MRPBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            MRPlan.DocNo = GetSerialNo(MRPlan.DocDate, trans)

            Dim strSQL As String = String.Empty


            strSQL = "Delete From tblMaterialRequiredPlanDetail WHERE DocId In(Select DocId From tblMaterialRequiredPlanMaster WHERE DocNo=N'" & MRPlan.DocNo.Replace("'", "''") & "') "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            strSQL = "Delete From tblMaterialRequiredPlanMaster WHERE DocNo=N'" & MRPlan.DocNo.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            strSQL = "INSERT INTO tblMaterialRequiredPlanMaster(DocNo,DocDate,PlanId,ProductionArticleDefId,Remarks,TotalQty,TotalAmount,Issued,UserName,EntryDate) VALUES( " _
            & " N'" & MRPlan.DocNo.Replace("'", "''") & "',Convert(DateTime,N'" & MRPlan.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & MRPlan.PlanId & ",  " _
            & " " & MRPlan.ProudctionArticleId & ", N'" & MRPlan.Remarks.Replace("'", "''") & "'," & MRPlan.TotalQty & "," & MRPlan.TotalAmount & ", " & IIf(MRPlan.Issued = True, 1, 0) & ",N'" & MRPlan.UserName.Replace("'", "''") & "', Convert(Datetime,N'" & MRPlan.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)) Select @@Identity"
            MRPlan.DocId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            AddDetail(MRPlan, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal MRPlan As MRPBE, ByVal trans As SqlTransaction) As Boolean
        Try


            Dim strSQL As String = String.Empty
            If MRPlan.MRPlanDetail IsNot Nothing Then
                If MRPlan.MRPlanDetail.Count > 0 Then
                    For Each MRP As MRPDetailBE In MRPlan.MRPlanDetail
                        strSQL = String.Empty
                        strSQL = "INSERT INTO tblMaterialRequiredPlanDetail(DocId,LocationId,ArticleDefId,ArticleSize,CurrentStock,SuggestedStock,Sz1,Sz7,Qty,Price,Comments) VALUES( " _
                        & " " & MRPlan.DocId & "," & MRP.LocationId & "," & MRP.ArticleDefId & ",N'" & MRP.ArticleSize.Replace("'", "''") & "'," & MRP.CurrentStock & "," & MRP.SuggestedQty & "," & MRP.Sz1 & "," & MRP.Sz7 & "," & MRP.Qty & "," & MRP.Price & ",N'" & MRP.Comments.Replace("'", "''") & "')"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    Next
                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Modify(ByVal MRPlan As MRPBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim strSQL As String = String.Empty

            strSQL = "UPDATE tblMaterialRequiredPlanMaster SET DocNo=N'" & MRPlan.DocNo.Replace("'", "''") & "',DocDate=Convert(DateTime,N'" & MRPlan.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),PlanId=" & MRPlan.PlanId & ",ProductionArticleDefId=" & MRPlan.ProudctionArticleId & ",Remarks=N'" & MRPlan.Remarks.Replace("'", "''") & "',TotalQty=" & MRPlan.TotalQty & ",TotalAmount=" & MRPlan.TotalAmount & ", Issued=" & IIf(MRPlan.Issued = True, 1, 0) & ",UserName=N'" & MRPlan.UserName.Replace("'", "''") & "',EntryDate=Convert(Datetime,N'" & MRPlan.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102) WHERE DocId=" & MRPlan.DocId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            strSQL = "Delete From tblMaterialRequiredPlanDetail WHERE DocId=" & MRPlan.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            AddDetail(MRPlan, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Remove(ByVal MRPlan As MRPBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim strSQL As String = String.Empty

            strSQL = "Delete From tblMaterialRequiredPlanDetail WHERE DocId=" & MRPlan.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            strSQL = "Delete From tblMaterialRequiredPlanMaster WHERE DocId=" & MRPlan.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            AddDetail(MRPlan, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAllRecords(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT " & IIf(Condition = "All", "", " TOP 50") & " Recv.DocId, Recv.DocNo, Recv.DocDate, Recv.PlanId, PlanMaster.PlanNo, PlanMaster.PlanDate, Recv.ProductionArticleDefId, " _
                   & " Art.ArticleDescription, Art.ArticleCode, Recv.Remarks, IsNull(Recv.TotalQty,0) as [Total Qty], IsNull(Recv.TotalAmount,0) as [Total Amount] FROM dbo.tblMaterialRequiredPlanMaster AS Recv LEFT OUTER JOIN dbo.ArticleDefView AS Art ON Recv.ProductionArticleDefId = Art.ArticleId LEFT OUTER JOIN " _
                   & " dbo.PlanMasterTable AS PlanMaster ON Recv.PlanId = PlanMaster.PlanId ORDER BY Recv.DocNo DESC  "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DisplyDetail(ByVal ReceivingId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT dbo.tblMaterialRequiredPlanDetail.LocationId, dbo.tblMaterialRequiredPlanDetail.ArticleDefId, dbo.ArticleDefView.ArticleDescription as Item, dbo.ArticleDefView.ArticleCode [Code], " _
                     & " dbo.ArticleDefView.ArticleColorName as [Color], dbo.ArticleDefView.ArticleSizeName as [Size], dbo.tblMaterialRequiredPlanDetail.ArticleSize as Unit, IsNull(dbo.tblMaterialRequiredPlanDetail.CurrentStock,0) as [Current Stock], IsNull(dbo.tblMaterialRequiredPlanDetail.SuggestedStock,0) as [Suggested Qty],  " _
                     & " Convert(float, 0) AS [Actual Qty], IsNull(dbo.tblMaterialRequiredPlanDetail.Sz1,0) as Qty,IsNull(dbo.tblMaterialRequiredPlanDetail.Price,0) as Price, CONVERT(float, 0) AS Total, " _
                     & " ISNULL(dbo.tblMaterialRequiredPlanDetail.Sz7, 0) AS [Pack Qty], dbo.tblMaterialRequiredPlanDetail.Comments " _
                     & " FROM dbo.tblMaterialRequiredPlanDetail INNER JOIN dbo.ArticleDefView ON dbo.tblMaterialRequiredPlanDetail.ArticleDefId = dbo.ArticleDefView.ArticleId " _
                     & " WHERE tblMaterialRequiredPlanDetail.DocId=" & ReceivingId & " ORDER BY tblMaterialRequiredPlanDetail.DocDetailId ASC "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PODetail(ByVal MasterID As Integer, ByVal Qty As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT Convert(int,1) AS LocationId, dbo.tblCostSheet.ArticleID AS ArticleDefId, dbo.ArticleDefView.ArticleDescription AS Item, dbo.ArticleDefView.ArticleCode AS Code, " _
                   & " dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, 'Loose' AS Unit, IsNull(Stock.[Current Stock],0)-IsNull(PendingStock,0) AS [Current Stock], 0 AS [Suggested Qty],   " _
                   & " Convert(float,0) AS [Actual Qty], (IsNull(dbo.tblCostSheet.Qty,0)*" & Qty & ") as Qty, IsNull(dbo.ArticleDefView.PurchasePrice,0)  AS Price, CONVERT(float, 0) AS Total, Convert(float,1) AS [Pack Qty], '' AS Comments  " _
                   & " FROM dbo.ArticleDefView INNER JOIN  " _
                   & " dbo.tblCostSheet ON dbo.ArticleDefView.ArticleId = dbo.tblCostSheet.ArticleId LEFT OUTER JOIN ( " _
                   & " Select StockDetailTable.ArticleDefId, Sum((IsNull(InQty,0)-IsNull(OutQty,0))) AS [Current Stock], IsNull(PendingStock.PendingQty,0)  as PendingStock " _
                   & " From StockDetailTable " _
                   & " LEFT OUTER JOIN ( " _
                   & " Select ArticleDefId, Sum(IsNull(Qty,0)) AS PendingQty  " _
                   & " From tblMaterialRequiredPlanDetail  " _
                   & " INNER JOIN tblMaterialRequiredPlanMaster On tblMaterialRequiredPlanDetail.DocId = tblMaterialRequiredPlanMaster.DocId WHERE IsNull(Issued, 0) = 0 " _
                   & " Group By tblMaterialRequiredPlanDetail.ArticleDefId " _
                   & " ) PendingStock On PendingStock.ArticleDefId = StockDetailTable.ArticleDefId " _
                   & " Group By StockDetailTable.ArticleDefId, IsNull(PendingStock.PendingQty,0) " _
                   & " ) Stock On Stock.ArticleDefId = tblCostSheet.ArticleId  " _
                   & " WHERE (dbo.tblCostSheet.MasterArticleID = " & MasterID & ")"

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSerialNo(ByVal DocDate As DateTime, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try


            Dim strSQL As String = String.Empty
            Dim Serial As Integer = 0I

            strSQL = "Select IsNull(Right(Max(DocNo),6),0)+1 AS Cont From tblMaterialRequiredPlanMaster "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL, trans)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Serial = Val(dt.Rows(0).Item(0).ToString)
                End If
            End If

            Return "MRP-" & DocDate.ToString("yy") & "-" & Right("000000" & CStr(Serial), 6)

        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
