Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class FinishGoodDAL
    Public Function Add(ByVal Obj As BEFinishGood) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction

        Try
            Dim strSQL As String = String.Empty

            '           [Id] [int] IDENTITY(1,1) NOT NULL,
            '[StandardNo] [nvarchar](100) NULL,
            '[StandardName] [nvarchar](100) NULL,
            '[Version] [int] NULL,
            '[MasterArticleId] [int] NULL,
            '[BatchSize] [Float] NULL,
            '[Default] [bit] NULL,

            If Obj.Default1 = True Then
                strSQL = "Update FinishGoodMaster Set Default1 = 0 Where MasterArticleId = " & Obj.MasterArticleId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            End If

            strSQL = " Insert into FinishGoodMaster(StandardNo , StandardName, Version, MasterArticleId, BatchSize, Default1) " _
           & " Values (N'" & Obj.StandardNo.Replace("'", "''") & "' , N'" & Obj.StandardName.Replace("'", "''") & "' , " & Obj.Version & ", " & Obj.MasterArticleId & ", " & Obj.BatchSize & ", " & IIf(Obj.Default1 = True, 1, 0) & ") SELECT @@IDENTITY"
            Dim FinishGoodId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
            Obj.Id = FinishGoodId

            AddDetail(Obj, trans)
            If Not Obj.OverHeadsList Is Nothing Then
                AddFinishGoodOverHeads(Obj, trans)
            End If
            ''
            If Not Obj.ByProductList Is Nothing Then
                AddFinishGoodByProducts(Obj, trans)
            End If
            ''
            If Not Obj.LabourAllocationList Is Nothing Then
                AddFinishGoodLabourAllocation(Obj, trans)
            End If
          

            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Return False
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex
        Finally
            con.Close()
        End Try
    End Function

    Public Function Update(ByVal Obj As BEFinishGood) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            '[Id] [int] IDENTITY(1,1) NOT NULL,
            '[StandardNo] [nvarchar](100) NULL,
            '[StandardName] [nvarchar](100) NULL,
            '[Version] [int] NULL,
            '[MasterArticleId] [int] NULL,
            '[BatchSize] [Float] NULL,
            '[Default] [bit] NULL,
            If Obj.Default1 = True Then
                strSQL = "Update FinishGoodMaster Set Default1 = 0 Where MasterArticleId = " & Obj.MasterArticleId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            End If
            strSQL = " Update FinishGoodMaster SET StandardNo = N'" & Obj.StandardNo.Replace("'", "''") & "' , StandardName = N'" & Obj.StandardName.Replace("'", "''") & "', Version =  " & Obj.Version & ", MasterArticleId = " & Obj.MasterArticleId & ", BatchSize = " & Obj.BatchSize & ", Default1 = " & IIf(Obj.Default1 = True, 1, 0) & " WHERE Id = " & Obj.Id & ""
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
            AddDetail(Obj, trans)
            If Not Obj.OverHeadsList Is Nothing Then
                AddFinishGoodOverHeads(Obj, trans)
            End If
            ''
            If Not Obj.ByProductList Is Nothing Then
                AddFinishGoodByProducts(Obj, trans)
            End If
            ''
            If Not Obj.LabourAllocationList Is Nothing Then
                AddFinishGoodLabourAllocation(Obj, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Return False
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex
        Finally
            con.Close()
        End Try
    End Function

    Private Function AddDetail(ByVal Obj As BEFinishGood, ByVal trans As SqlTransaction)
        Try
            '           	[Id]  [int] IDENTITY(1,1) NOT NULL,
            '[FinishGoodId] [int] NOT NULL,
            '[MaterialArticleId] [int] NULL,
            '[DetailArticleId] [int] NULL,
            '[PackingId] [int] NULL,
            '[Qty] [float] NULL,
            '[ArticleSize] [nvarchar](1000) NULL,
            '[Category] [nvarchar](500) NULL,
            '[Tax_Percent] [float] NULL,
            '[Tax_Amount] [float] NULL,
            '[Tax_Apply] [varchar](20) NULL,
            '[Remarks] [nvarchar](300) NULL,
            '[CostPrice] [float] NULL,
            '[ItemSpecificationId] [int] NULL,
            '[SubDepartmentId] [int] NULL,
            '[Percentage_Con] [float] NULL,
            '[TotalQty] [float] NULL,
            For Each Detail As BEFinishGoodDetail In Obj.Detail
                If Detail.Id = 0 Then
                    Dim strSQL As String = " Insert into FinishGoodDetail(FinishGoodId , MaterialArticleId, DetailArticleId, PackingId, PackQty, Qty,ArticleSize,Category,Tax_Percent, Remarks, CostPrice, SubDepartmentId, Percentage_Con, TotalQty) " _
                        & " Values (" & Obj.Id & " , " & Detail.MaterialArticleId & " , " & Detail.DetailArticleId & ", " & Detail.PackingId & ", " & Detail.PackQty & ", " & Detail.Qty & ", N'" & Detail.ArticleSize.Replace("'", "''") & "', " & Detail.Tax_Percent & ", " & Detail.Tax_Amount & ", N'" & Detail.Remarks.Replace("'", "''") & "', " & Detail.CostPrice & ", " & Detail.SubDepartmentId & ", " & Detail.Percentage_Con & ", " & Detail.TotalQty & ") "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                Else
                    Dim strSQL As String = " Update FinishGoodDetail Set FinishGoodId = " & Detail.FinishGoodId & ", MaterialArticleId = " & Detail.MaterialArticleId & ", DetailArticleId =" & Detail.DetailArticleId & ", PackingId  =" & Detail.PackingId & ", PackQty=" & Detail.PackQty & ", Qty=" & Detail.Qty & ",ArticleSize= N'" & Detail.ArticleSize.Replace("'", "''") & "', Category = N'" & Detail.Category.Replace("'", "''") & "',Tax_Percent = " & Detail.Tax_Percent & ", " _
                                           & "  Remarks =N'" & Detail.Remarks.Replace("'", "''") & "' , CostPrice= " & Detail.CostPrice & ", SubDepartmentId = " & Detail.SubDepartmentId & ", Percentage_Con = " & Detail.Percentage_Con & ", TotalQty = " & Detail.TotalQty & " WHERE Id =" & Detail.Id & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''2103
    Public Function AddFinishGoodOverHeads(ByVal Obj As BEFinishGood, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty

            '           [Id] [int] IDENTITY(1,1) NOT NULL,
            '[FinishGoodId] [int] NULL,
            '[ProductionStepId] [int] NULL,
            '[AccountId] [int] NULL,
            '[Amount] [float] NULL,
            '[Remarks] [nvarchar](300) NULL,
            For Each Detail As BEFinishGoodOverHeads In Obj.OverHeadsList
                If Detail.Id = 0 Then
                    str = "INSERT INTO FinishGoodOverHeads(FinishGoodId, ProductionStepId, AccountId, Amount, Remarks) Values (" & Obj.Id & ", " & Detail.ProductionStepId & ", " & Detail.AccountId & ", " & Detail.Amount & ", N'" & Detail.Remarks.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update FinishGoodOverHeads Set FinishGoodId = " & Obj.Id & ", ProductionStepId = " & Detail.ProductionStepId & ", AccountId = " & Detail.AccountId & ", Amount = " & Detail.Amount & ", Remarks = N'" & Detail.Remarks.Replace("'", "''") & "' WHERE Id = " & Detail.Id & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetFinishGoodOverHeads(ByVal FinishGoodId As Integer) As DataTable
        Try
            Dim str As String = " SELECT Heads.Id, Heads.FinishGoodId, Heads.ProductionStepId, ProductionStep.prod_step AS ProductionStep, Heads.AccountId, COA.detail_title As Account, Heads.Amount, Heads.Remarks FROM FinishGoodOverHeads AS Heads LEFT OUTER JOIN  tblproSteps AS ProductionStep ON  Heads.ProductionStepId = ProductionStep.ProdStep_Id  " _
                              & " LEFT OUTER JOIN vwCOADetail AS COA ON Heads.AccountId = COA.coa_detail_id WHERE Heads.FinishGoodId = " & FinishGoodId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteFinishGoodOverHeads(ByVal Id As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM FinishGoodOverHeads Where Id = " & Id & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL1)
                trans.Commit()
                Return True
            Catch ex As Exception
                trans.Rollback()
                Return False
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''2104
    Public Function AddFinishGoodByProducts(ByVal Obj As BEFinishGood, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            For Each ByProduct As BEFinishGoodByProducts In Obj.ByProductList
                If ByProduct.Id = 0 Then
                    str = "INSERT INTO FinishGoodByProducts(ArticleId, FinishGoodId, Rate, Qty, Remarks) Values (" & ByProduct.ArticleId & ", " & Obj.Id & ", " & ByProduct.Rate & ", " & ByProduct.Qty & ", N'" & ByProduct.Remarks.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update FinishGoodByProducts Set ArticleId = " & ByProduct.ArticleId & ", FinishGoodId = " & Obj.Id & ", Rate = " & ByProduct.Rate & ", Qty = " & ByProduct.Qty & ", Remarks = N'" & ByProduct.Remarks.Replace("'", "''") & "' WHERE Id = " & ByProduct.Id & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetFinishGoodByProducts(ByVal FinishGoodId As Integer) As DataTable
        Try
            Dim str As String = " SELECT ByProducts.Id, ByProducts.ArticleId, Article.ArticleDescription AS Product, ByProducts.FinishGoodId AS FinishGoodId, IsNull(ByProducts.Rate, 0) AS Rate, IsNull(ByProducts.Qty, 0) AS Qty, ByProducts.Remarks FROM FinishGoodByProducts AS ByProducts LEFT OUTER JOIN  ArticleDefTable AS Article ON  ByProducts.ArticleId = Article.ArticleId  " _
                              & " WHERE ByProducts.FinishGoodId = " & FinishGoodId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteFinishGoodByProduct(ByVal Id As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM FinishGoodByProducts where Id = " & Id & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL1)
                trans.Commit()
                Return True
            Catch ex As Exception
                trans.Rollback()
                Return False
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''END 2104
    ''2113
    Public Function AddFinishGoodLabourAllocation(ByVal Obj As BEFinishGood, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            For Each LabourAllocation As BEFinishGoodLabourAllocation In Obj.LabourAllocationList
                If LabourAllocation.Id = 0 Then
                    str = "INSERT INTO FinishGoodLabourAllocation(ProductionStepId, LabourTypeId, RatePerUnit, FinishGoodId) Values (" & LabourAllocation.ProductionStepId & ", " & LabourAllocation.LabourTypeId & ", " & LabourAllocation.RatePerUnit & ", " & Obj.Id & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update FinishGoodLabourAllocation Set ProductionStepId = " & LabourAllocation.ProductionStepId & ", LabourTypeId = " & LabourAllocation.LabourTypeId & ", RatePerUnit = " & LabourAllocation.RatePerUnit & "  WHERE Id = " & LabourAllocation.Id & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetFinishGoodLabourAllocations(ByVal FinishGoodId As Integer) As DataTable
        Try
            Dim str As String = " SELECT LabourAllocation.Id, LabourAllocation.FinishGoodId, LabourAllocation.ProductionStepId, ProductionStep.Prod_Step AS ProductionStep, LabourAllocation.LabourTypeId, LabourType.LabourType, ChargeType.Charge AS ChargeType, LabourAllocation.RatePerUnit FROM FinishGoodLabourAllocation AS LabourAllocation LEFT OUTER JOIN  tblproSteps AS ProductionStep ON  LabourAllocation.ProductionStepId = ProductionStep.ProdStep_Id  " _
                              & " LEFT OUTER JOIN tblLabourType AS LabourType ON LabourAllocation.LabourTypeId = LabourType.Id LEFT OUTER JOIN ChargeType ON LabourType.ChargeTypeId = ChargeType.Id  WHERE LabourAllocation.FinishGoodId = " & FinishGoodId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteFinishGoodAllocation(ByVal Id As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM FinishGoodLabourAllocation Where Id = " & Id & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL1)
                trans.Commit()
                Return True
            Catch ex As Exception
                trans.Rollback()
                Return False
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDetail(ByVal FinishGoodId As Integer) As DataTable
        '       [Id]  [int] IDENTITY(1,1) NOT NULL,
        '[FinishGoodId] [int] NOT NULL,
        '[MaterialArticleId] [int] NULL,
        '[DetailArticleId] [int] NULL,
        '[PackingId] [int] NULL,
        '[Qty] [float] NULL,
        '[ArticleSize] [nvarchar](1000) NULL,
        '[Category] [nvarchar](500) NULL,
        '[Tax_Percent] [float] NULL,
        '[Tax_Amount] [float] NULL,
        '[Tax_Apply] [varchar](20) NULL,
        '[Remarks] [nvarchar](300) NULL,
        '[CostPrice] [float] NULL,
        '[ItemSpecificationId] [int] NULL,
        '[SubDepartmentId] [int] NULL,
        '[Percentage_Con] [float] NULL,
        '[TotalQty] [float] NULL,



        Try
            Dim strsql As String = "SELECT  ISNULL(FinishGood.StandardNo, '') AS StandardNo, Detail.Id, IsNull(Detail.FinishGoodId, 0) AS FinishGoodId, IsNull(Detail.SubDepartmentId, 0) As SubDepartmentId, tblProSteps.prod_step As SubDepartment, ArticleDefTable.ArticleId AS MaterialArticleId, ArticleDefTable.ArticleCode AS [Article Code], ArticleDefTable.ArticleDescription AS [Article Description], ArticleColorDefTable.ArticleColorName as Color , ArticleSizeDefTable.ArticleSizeName as Size, ArticleUnitDefTable.ArticleUnitName As Unit, ISNULL(Detail.ArticleSize,'Loose') as ArticleSize, IsNull(Detail.DetailArticleId, 0) AS DetailArticleId, IsNull(Detail.PackingId, 0) As PackingId, " _
                       & " Case When IsNull(Detail.CostPrice,0)=0 Then IsNull(ArticleDefTable.PurchasePrice,0.0) Else IsNull(Detail.CostPrice,0.0) End AS [Purchase Price], IsNull(ArticleDefTable.SalePrice,0.0) AS [Sale Price], IsNull(Detail.Tax_Percent,0) as Tax, ISNULL(Detail.PackQty, 1) As PackQty, ISNULL(Detail.Qty, 0) As Qty, Detail.Percentage_Con As Percentage, " _
                       & " ISNULL(Detail.TotalQty,0) as TotalQty, Convert(float,0) as [Total Purchase Value], Convert(float,0) as [Total Sale Value], Convert(float,0) as [Purchase Tax], Convert(float,0) as [Sale Tax], Convert(float,0) as [Net Purchase Value], Convert(float,0) as [Net Sales Value], ISNULL(Detail.Category,'') as Category, ISNULL(Detail.Remarks,'') as Remarks, IsNull(FinishGood.StandardId, 0) AS StandardId " _
                       & " FROM FinishGoodDetail AS Detail INNER JOIN  " _
                       & " ArticleDefTable ON Detail.MaterialArticleId = ArticleDefTable.ArticleId INNER JOIN " _
                       & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId INNER JOIN " _
                       & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT JOIN tblProSteps ON Detail.SubDepartmentId = tblProSteps.ProdStep_Id LEFT JOIN " _
                       & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN (SELECT StandardNo, MasterArticleId, Id AS StandardId FROM FinishGoodMaster WHERE ISNULL(Default1, 0) = 1) AS FinishGood ON ArticleDefTable.MasterID = FinishGood.MasterArticleId " _
                       & " where Detail.FinishGoodId  = " & FinishGoodId
            Dim dt As DataTable = UtilityDAL.GetDataTable(strsql)
            dt.Columns("TotalQty").Expression = "IsNull(PackQty, 1)*IsNull(Qty, 1)"
            dt.Columns("Total Purchase Value").Expression = "(isnull([Purchase Price],0) * IsNull(TotalQty,0))"
            dt.Columns("Total Sale Value").Expression = "(isnull([Sale Price],0) * IsNull(TotalQty,0))"
            dt.Columns("Purchase Tax").Expression = "(([Tax]/100)*(isnull([Purchase Price],0) * IsNull(TotalQty,0)))"
            dt.Columns("Sale Tax").Expression = "(([Tax]/100)*(isnull([Sale Price],0) * IsNull(TotalQty,0)))"
            dt.Columns("Net Purchase Value").Expression = "[Total Purchase Value]+[Purchase Tax]"
            dt.Columns("Net Sales Value").Expression = "[Total Sale Value]+[Sale Tax]"
            dt.Columns.Add("Delete", GetType(System.String))
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAll() As DataTable
        '[Id] [int] IDENTITY(1,1) NOT NULL,
        '[StandardNo] [nvarchar](100) NULL,
        '[StandardName] [nvarchar](100) NULL,
        '[Version] [int] NULL,
        '[MasterArticleId] [int] NULL,
        '[BatchSize] [Float] NULL,
        '[Default1] [bit] NULL,
        Try
            Dim Strng As String = "SELECT Id, StandardNo, StandardName, IsNull(Version, 0) AS Version, FinishGoodMaster.MasterArticleId, Article.ArticleDescription, IsNull(BatchSize, 0) AS BatchSize, IsNull(Default1, 0) As Default1 FROM FinishGoodMaster INNER JOIN ArticleDefTableMaster AS Article ON FinishGoodMaster.MasterArticleId = Article.ArticleId order by Id DESC"

            Dim dt As DataTable = UtilityDAL.GetDataTable(Strng)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetSingle(ByVal FinishGoodId As Integer) As DataTable
        '[Id] [int] IDENTITY(1,1) NOT NULL,
        '[StandardNo] [nvarchar](100) NULL,
        '[StandardName] [nvarchar](100) NULL,
        '[Version] [int] NULL,
        '[MasterArticleId] [int] NULL,
        '[BatchSize] [Float] NULL,
        '[Default1] [bit] NULL,
        Try
            Dim Strng As String = "SELECT Id, StandardNo, StandardName, IsNull(Version, 0) AS Version, FinishGoodMaster.MasterArticleId, Article.ArticleDescription, IsNull(BatchSize, 0) AS BatchSize, IsNull(Default1, 0) As Default1 FROM FinishGoodMaster INNER JOIN ArticleDefTableMaster AS Article ON FinishGoodMaster.MasterArticleId = Article.ArticleId WHERE Id = " & FinishGoodId & " order by Id DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Strng)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetVersion(ByVal MasterArticleId As Integer) As Integer
        Dim VersionId As Integer = 0
        Try
            Dim Strng As String = "SELECT Max(IsNull(Version, 0)) AS VersionId FROM FinishGoodMaster WHERE IsNull(MasterArticleId, 0) = " & MasterArticleId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Strng)
            If dt.Rows.Count > 0 Then
                VersionId = Val(dt.Rows(0).Item("VersionId").ToString) + 1
            End If
            Return VersionId
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function DeleteFinishGoodDetail(ByVal Id As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM FinishGoodDetail where Id = " & Id & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL1)
                trans.Commit()
                Return True
            Catch ex As Exception
                trans.Rollback()
                Return False
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
