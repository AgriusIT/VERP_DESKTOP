Imports SBDal
Imports SBUtility
Imports SBModel
Imports SBUtility.Utility
Imports System.Data
Imports System.Data.SqlClient

Public Class ProducionLevelDAL

    Public Function Save(ByVal ProductionLevel As ProdcutionLevelMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO ProductionLevelMasterTable(Doc_No, Doc_Date, ProjectId, PlanId, PStepsId, Total_Qty, Total_Amount, Remarks, UserName) " _
            & " VALUES(N'" & ProductionLevel.Doc_No & "', N'" & ProductionLevel.Doc_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & ProductionLevel.ProjectId & ", " & ProductionLevel.PlanId & ", " & ProductionLevel.StepsId & ", " & ProductionLevel.TotalQty & ", " & ProductionLevel.TotalAmount & ", N'" & ProductionLevel.Remarks & "', N'" & ProductionLevel.UserName & "') Select @@Identity"
            ProductionLevel.PLevelId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Call SaveDetail(ProductionLevel, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function SaveDetail(ByVal ProductionLevel As ProdcutionLevelMasterBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each Production As ProductionLevelDetailBE In ProductionLevel.ProductionLevelDetail
                strSQL = "INSERT INTO ProductionLevelDetailTable(PLevelId, LocationId, ArticleDefId, ArticleSize, Price, Sz1, Sz7, Qty) " _
                & " Values(" & ProductionLevel.PLevelId & ", " & Production.LocationId & ", " & Production.ArticleDefId & ", N'" & Production.Articlesize & "', " & Production.Price & ", " & Production.Sz1 & ", " & Production.Sz2 & ", " & Production.Qty & " )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal ProductionLevel As ProdcutionLevelMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ProductionLevelMasterTable SET Doc_No=N'" & ProductionLevel.Doc_No & "', Doc_Date=N'" & ProductionLevel.Doc_Date.ToString("yyyy-M-d h:mm:ss tt") & "', ProjectId=" & ProductionLevel.ProjectId & ", PlanId=" & ProductionLevel.PlanId & ", PStepsId=" & ProductionLevel.StepsId & ", Total_Qty=" & ProductionLevel.TotalQty & ", Total_Amount=" & ProductionLevel.TotalAmount & ", Remarks=N'" & ProductionLevel.Remarks & "', UserName=N'" & ProductionLevel.UserName & "' WHERE PLevelId=" & ProductionLevel.PLevelId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = String.Empty
            strSQL = "DELETE  From ProductionLevelDetailTable  WHERE PLevelId=" & ProductionLevel.PLevelId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Call SaveDetail(ProductionLevel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ProductionLevel As ProdcutionLevelMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "DELETE  From ProductionLevelDetailTable  WHERE PLevelId=" & ProductionLevel.PLevelId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = String.Empty
            strSQL = "DELETE  From ProductionLevelMasterTable  WHERE PLevelId=" & ProductionLevel.PLevelId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Try
            Return UtilityDAL.GetDataTable("SELECT b.PLevelId, b.Doc_No, b.Doc_Date, b.ProjectId, b.PlanId, b.PStepsId, b.Total_Qty, b.Total_Amount, b.Remarks, a.Name as [Project], c.PlanNo, d.prod_step as [Production Step] FROM    dbo.tblDefCostCenter AS a RIGHT OUTER JOIN    dbo.ProductionLevelMasterTable AS b LEFT OUTER JOIN     dbo.PlanMasterTable AS c ON b.PlanId = c.PlanId LEFT OUTER JOIN   dbo.tblproSteps AS d ON b.PStepsId = d.ProdStep_Id ON a.CostCenterID = b.ProjectId ORDER BY Doc_No DESC")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetailRecord(ByVal ReceivingId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName AS Color, " _
                    & " dbo.ArticleDefView.ArticleSizeName AS Size, ISNULL(Production.LocationId, 0) AS LocationId, Case When Production.ArticleSize is null then 'Loose' else Production.ArticleSize end as ArticleSize, case when (Production.PackQty=0 Or Production.PackQty is null) then ArticleDefView.PackQty ELSE Isnull(Production.PackQty,0) end as PackQty, Convert(float, 0) as PlanQty, ISNULL(Production.Sz1, 0) as RecvQty, ISNULL(Production.Qty, 0) AS Qty " _
                     & " FROM  dbo.ArticleDefView LEFT OUTER JOIN " _
                      & " (SELECT ISNULL(LocationId, 0) AS LocationId, ISNULL(ArticleDefId, 0) AS ArticleDefId, ArticleSize, Isnull(Sz1,0) as Sz1, Isnull(Sz7,0) as PackQty, ISNULL(Qty, 0) AS Qty " _
                        & " FROM dbo.ProductionLevelDetailTable WHERE ProductionLevelDetailTable.PLevelId=" & ReceivingId & ") AS Production ON Production.ArticleDefId = dbo.ArticleDefView.ArticleId"

            strSQL += " WHERE ArticleDefView.SalesItem=1 AND ArticleDefView.Active=1  "
            strSQL += " ORDER BY ArticleDefView.SortOrder ASC"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)

            dt.Columns("Qty").Expression = "IIF(ArticleSize='Pack',(PackQty*RecvQty), RecvQty)"

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function





End Class
