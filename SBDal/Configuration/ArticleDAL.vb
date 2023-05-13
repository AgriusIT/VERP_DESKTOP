'' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
'' 2015060011   
'' 17-6-2015 TASKM176151 Imran Ali Add Field Of Remarks In Cost Sheet Define.
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class ArticleDAL
    Public Shared ArticleMasterId As Integer
    Public Shared ArticleIdForArticleAlias As Integer = 0I
    Public Function GetAll(Optional ByVal Source As String = "") As DataTable
        Try
            'Dim strSQL As String = "SELECT dbo.ArticleDefTableMaster.ArticleId, dbo.ArticleDefTableMaster.ArticleCode as [Article Code], dbo.ArticleDefTableMaster.ArticleDescription as[Article Name], dbo.ArticleGroupDefTable.ArticleGroupName as [Group], " & _
            '         "dbo.ArticleGroupDefTable.ArticleGroupId,dbo.ArticleDefTableMaster.PurchasePrice as [Purchase Price], dbo.ArticleDefTableMaster.SalePrice as [Sale Price],  dbo.ArticleDefTableMaster.PackQty as [Pack Qty],dbo.ArticleDefTableMaster.StockLevel as [Stock Level Min],dbo.ArticleDefTableMaster.StockLevelopt as [Stock Level Opt],dbo.ArticleDefTableMaster.StockLevelmax as [Stock Level Max], " & _
            '         "dbo.ArticleDefTableMaster.Active, dbo.ArticleDefTableMaster.SortOrder as [Sort Order],dbo.ArticleDefTableMaster.ArticleTypeId , " & _
            '         "  dbo.ArticleTypeDefTable.ArticleTypeName as Type,   dbo.ArticleGenderDefTable.ArticleGenderName as [Gender],   dbo.ArticleUnitDefTable.ArticleUnitName as [Unit], dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS [Category > Sub Category], dbo.ArticleDefTableMaster.AccountID, dbo.ArticleDefTableMaster.Remarks, ISNULL(ArticleDefTableMaster.ServiceItem,0) as ServiceItem, ArticlePicture, ISNULL(TradePrice,0) as TradePrice, Isnull(Freight,0) as Freight, ISNULL(MarketReturns,0) as MarketReturns, ISNULL(GST_Applicable,0) as GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable, ISNULL(FlatRate,0) as FlatRate, ISNULL(ItemWeight,0) as ItemWeight, HS_Code, Isnull(LargestPackQty,0) as LargestPackQty FROM dbo.ArticleDefTableMaster " & _
            '         "INNER JOIN " & _
            '             " dbo.ArticleGroupDefTable ON dbo.ArticleDefTableMaster.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId INNER JOIN " & _
            '             " dbo.ArticleTypeDefTable ON dbo.ArticleDefTableMaster.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId LEFT OUTER JOIN " & _
            '              "dbo.ArticleUnitDefTable ON dbo.ArticleDefTableMaster.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " & _
            '              "dbo.ArticleGenderDefTable ON dbo.ArticleDefTableMaster.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId LEFT OUTER JOIN " & _
            '              "dbo.ArticleLpoDefTable ON dbo.ArticleDefTableMaster.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN " & _
            '              "dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId "
            ''" where ArticleDefTableMaster.ArticleGroupId =" & GroupID
            'Dim strSQL As String = "SELECT dbo.ArticleDefTableMaster.ArticleId, dbo.ArticleDefTableMaster.ArticleCode as [Article Code], dbo.ArticleDefTableMaster.ArticleDescription as[Article Name], dbo.ArticleGroupDefTable.ArticleGroupName as [Group], " & _
            '         "dbo.ArticleGroupDefTable.ArticleGroupId,dbo.ArticleDefTableMaster.PurchasePrice as [Purchase Price], dbo.ArticleDefTableMaster.SalePrice as [Sale Price],  dbo.ArticleDefTableMaster.PackQty as [Pack Qty],dbo.ArticleDefTableMaster.StockLevel as [Stock Level Min],dbo.ArticleDefTableMaster.StockLevelopt as [Stock Level Opt],dbo.ArticleDefTableMaster.StockLevelmax as [Stock Level Max], " & _
            '         "dbo.ArticleDefTableMaster.Active, dbo.ArticleDefTableMaster.SortOrder as [Sort Order],dbo.ArticleDefTableMaster.ArticleTypeId , " & _
            '         "  dbo.ArticleTypeDefTable.ArticleTypeName as Type,   dbo.ArticleGenderDefTable.ArticleGenderName as [Origin],   dbo.ArticleUnitDefTable.ArticleUnitName as [Unit], dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS [Category > Sub Category], dbo.ArticleDefTableMaster.AccountID, dbo.ArticleDefTableMaster.Remarks, ISNULL(ArticleDefTableMaster.ServiceItem,0) as ServiceItem, ArticlePicture, ISNULL(TradePrice,0) as TradePrice, Isnull(Freight,0) as Freight, ISNULL(MarketReturns,0) as MarketReturns, ISNULL(GST_Applicable,0) as GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable, ISNULL(FlatRate,0) as FlatRate, ISNULL(ItemWeight,0) as ItemWeight, HS_Code, Isnull(LargestPackQty,0) as LargestPackQty FROM dbo.ArticleDefTableMaster " & _
            '         "INNER JOIN " & _
            '             " dbo.ArticleGroupDefTable ON dbo.ArticleDefTableMaster.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId INNER JOIN " & _
            '             " dbo.ArticleTypeDefTable ON dbo.ArticleDefTableMaster.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId LEFT OUTER JOIN " & _
            '              "dbo.ArticleUnitDefTable ON dbo.ArticleDefTableMaster.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " & _
            '              "dbo.ArticleGenderDefTable ON dbo.ArticleDefTableMaster.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId LEFT OUTER JOIN " & _
            '              "dbo.ArticleLpoDefTable ON dbo.ArticleDefTableMaster.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN " & _
            '              "dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId "
            '" where ArticleDefTableMaster.ArticleGroupId =" & GroupID
            'Marked Against Task 2015060011 Ali Ansari
            'Dim strSQL As String = "SELECT dbo.ArticleDefTableMaster.ArticleId, dbo.ArticleDefTableMaster.ArticleCode as [Article Code], dbo.ArticleDefTableMaster.ArticleDescription as[Article Name], dbo.ArticleGroupDefTable.ArticleGroupName as [Group], " & _
            '        "dbo.ArticleGroupDefTable.ArticleGroupId,dbo.ArticleDefTableMaster.PurchasePrice as [Purchase Price], dbo.ArticleDefTableMaster.SalePrice as [Sale Price],  dbo.ArticleDefTableMaster.PackQty as [Pack Qty],dbo.ArticleDefTableMaster.StockLevel as [Stock Level Min],dbo.ArticleDefTableMaster.StockLevelopt as [Stock Level Opt],dbo.ArticleDefTableMaster.StockLevelmax as [Stock Level Max], " & _
            '        "dbo.ArticleDefTableMaster.Active, dbo.ArticleDefTableMaster.SortOrder as [Sort Order],dbo.ArticleDefTableMaster.ArticleTypeId , " & _
            '        "  dbo.ArticleTypeDefTable.ArticleTypeName as Type,   dbo.ArticleGenderDefTable.ArticleGenderName as [Origin],   dbo.ArticleUnitDefTable.ArticleUnitName as [Unit], dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS [Category > Sub Category], dbo.ArticleDefTableMaster.AccountID, dbo.ArticleDefTableMaster.Remarks, ISNULL(ArticleDefTableMaster.ServiceItem,0) as ServiceItem, ArticlePicture, ISNULL(TradePrice,0) as TradePrice, Isnull(Freight,0) as Freight, ISNULL(MarketReturns,0) as MarketReturns, ISNULL(GST_Applicable,0) as GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable, ISNULL(FlatRate,0) as FlatRate, ISNULL(ItemWeight,0) as ItemWeight, HS_Code, Isnull(LargestPackQty,0) as LargestPackQty, IsNull(ArticleDefTableMaster.Cost_Price,0) as Cost_Price FROM dbo.ArticleDefTableMaster " & _
            '        "INNER JOIN " & _
            '            " dbo.ArticleGroupDefTable ON dbo.ArticleDefTableMaster.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId INNER JOIN " & _
            '            " dbo.ArticleTypeDefTable ON dbo.ArticleDefTableMaster.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId LEFT OUTER JOIN " & _
            '             "dbo.ArticleUnitDefTable ON dbo.ArticleDefTableMaster.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " & _
            '             "dbo.ArticleGenderDefTable ON dbo.ArticleDefTableMaster.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId LEFT OUTER JOIN " & _
            '             "dbo.ArticleLpoDefTable ON dbo.ArticleDefTableMaster.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN " & _
            '             "dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId "
            'Marked Against Task 2015060011 Ali Ansari
            'Altered Against Task 2015060011 Ali Ansari to make data consistent
            'Dim strSQL As String = "SELECT dbo.ArticleDefTableMaster.ArticleId, dbo.ArticleDefTableMaster.ArticleCode as [Item Code], dbo.ArticleDefTableMaster.ArticleDescription as[Item Name], dbo.ArticleGroupDefTable.ArticleGroupName as Department, " & _
            '        "dbo.ArticleGroupDefTable.ArticleGroupId,dbo.ArticleDefTableMaster.PurchasePrice as [Purchase Price], dbo.ArticleDefTableMaster.SalePrice as [Sale Price],  dbo.ArticleDefTableMaster.PackQty as [Pack Qty],dbo.ArticleDefTableMaster.StockLevel as [Stock Level Min],dbo.ArticleDefTableMaster.StockLevelopt as [Stock Level Opt],dbo.ArticleDefTableMaster.StockLevelmax as [Stock Level Max], " & _
            '        "dbo.ArticleDefTableMaster.Active, IsNull(dbo.ArticleDefTableMaster.ArticleStatusID,0) as [Status ID], dbo.ArticleDefTableMaster.SortOrder as [Sort Order],dbo.ArticleDefTableMaster.ArticleTypeId , " & _
            '        "  dbo.ArticleTypeDefTable.ArticleTypeName as Type,   dbo.ArticleGenderDefTable.ArticleGenderName as [Origin],   dbo.ArticleUnitDefTable.ArticleUnitName as [Unit], IsNull(ArticleCategoryId,0) as ArticleCategoryId, dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS [Category > Sub Category], dbo.ArticleDefTableMaster.AccountID, dbo.ArticleDefTableMaster.Remarks, ISNULL(ArticleDefTableMaster.ServiceItem,0) as ServiceItem, ArticlePicture, ISNULL(TradePrice,0) as TradePrice, Isnull(Freight,0) as Freight, ISNULL(MarketReturns,0) as MarketReturns, ISNULL(GST_Applicable,0) as GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable, ISNULL(FlatRate,0) as FlatRate, ISNULL(ItemWeight,0) as ItemWeight, HS_Code, Isnull(LargestPackQty,0) as LargestPackQty, IsNull(ArticleDefTableMaster.Cost_Price,0) as Cost_Price FROM dbo.ArticleDefTableMaster " & _
            '        "INNER JOIN " & _
            '            " dbo.ArticleGroupDefTable ON dbo.ArticleDefTableMaster.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId INNER JOIN " & _
            '            " dbo.ArticleTypeDefTable ON dbo.ArticleDefTableMaster.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId LEFT OUTER JOIN " & _
            '             "dbo.ArticleUnitDefTable ON dbo.ArticleDefTableMaster.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " & _
            '             "dbo.ArticleGenderDefTable ON dbo.ArticleDefTableMaster.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId LEFT OUTER JOIN " & _
            '             "dbo.ArticleLpoDefTable ON dbo.ArticleDefTableMaster.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN " & _
            '             "dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId "
            'Altered Against Task 2015060011 Ali Ansari to make data consistent

            ''TASK TFS1779 Addition of attachment column on 17-11-2017
            ''TASK TFS1772 Addition of MultiDimentionalItem column on 5-12-2017
            ''TASK TFS1957 Addition of LogicalItem column on 10-01-2018
            ''TASK TFS1799 Addition of ArticleTaxId column on 18-01-2018
            ''TASK TFS3763 : Ayesha Rehman : Addition of ArticleBarCode column on 17-07-2018
            ''TASK TFS3764 : Ayesha Rehman : Addition of ArticleDisabledBarCode column on 17-07-2018
            Dim strSQL As String = "SELECT dbo.ArticleDefTableMaster.ArticleId, dbo.ArticleDefTableMaster.ArticleCode as [Item Code], dbo.ArticleDefTableMaster.ArticleDescription as[Item Name], dbo.ArticleGroupDefTable.ArticleGroupName as Department, " & _
                   "dbo.ArticleGroupDefTable.ArticleGroupId,dbo.ArticleDefTableMaster.PurchasePrice as [Purchase Price], dbo.ArticleDefTableMaster.SalePrice as [Sale Price],  dbo.ArticleDefTableMaster.PackQty as [Pack Qty],dbo.ArticleDefTableMaster.StockLevel as [Stock Level Min],dbo.ArticleDefTableMaster.StockLevelopt as [Stock Level Opt],dbo.ArticleDefTableMaster.StockLevelmax as [Stock Level Max], " & _
                   "dbo.ArticleDefTableMaster.Active, IsNull(dbo.ArticleDefTableMaster.ArticleStatusID,0) as [Status ID], dbo.ArticleDefTableMaster.SortOrder as [Sort Order],dbo.ArticleDefTableMaster.ArticleTypeId , " & _
                   "  dbo.ArticleTypeDefTable.ArticleTypeName as [Type],  ArticleBrandDefTable.ArticleBrandName as [Brand], dbo.ArticleGenderDefTable.ArticleGenderId, dbo.ArticleGenderDefTable.ArticleGenderName as [Origin],   dbo.ArticleUnitDefTable.ArticleUnitName as [Unit], IsNull(ArticleCategoryId,0) as ArticleCategoryId, IsNull(dbo.ArticleLpoDefTable.ArticleLPOId,0) as ArticleLPOId, dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS [Category > Sub Category], dbo.ArticleDefTableMaster.AccountID, dbo.ArticleDefTableMaster.Remarks, ISNULL(ArticleDefTableMaster.ServiceItem,0) as ServiceItem, ArticlePicture, ISNULL(TradePrice,0) as TradePrice, Isnull(Freight,0) as Freight, ISNULL(MarketReturns,0) as MarketReturns, ISNULL(GST_Applicable,0) as GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable, ISNULL(FlatRate,0) as FlatRate, ISNULL(ItemWeight,0) as ItemWeight, HS_Code, Isnull(LargestPackQty,0) as LargestPackQty, IsNull(ArticleDefTableMaster.Cost_Price,0) as Cost_Price,IsNull(ArticleDefTableMaster.ArticleBrandId,0) as ArticleBrandId, IsNull(ArticleDefTableMaster.ApplyAdjustmentFuelExp,0) as ApplyAdjustmentFuelExp , IsNull(dbo.ArticleDefTableMaster.CGSAccountId,0) AS CGSAccountId, IsNull([No Of Attachment],0) as  [No Of Attachment] , IsNull(ArticleDefTableMaster.MultiDimentionalItem,0) as MultiDimentionalItem , IsNull(ArticleDefTableMaster.LogicalItem,0) as LogicalItem,dbo.tblDefServices.ServicesID, ISNULL(ArticleDefTableMaster.ProductionProcessId, 0) AS ProductionProcessId, IsNull(IsIndividual, 0) AS IsIndividual , dbo.ArticleDefTableMaster.ArticleBARCode , IsNull(dbo.ArticleDefTableMaster.ArticleBARCodeDisable,0) FROM dbo.ArticleDefTableMaster " & _
                   "LEFT OUTER JOIN " & _
                       " dbo.ArticleGroupDefTable ON dbo.ArticleDefTableMaster.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId LEFT OUTER JOIN " & _
                       " dbo.ArticleTypeDefTable ON dbo.ArticleDefTableMaster.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId LEFT OUTER JOIN " & _
                       " dbo.tblDefServices ON dbo.ArticleDefTableMaster.ArticleTaxId = dbo.tblDefServices.ServicesID LEFT OUTER JOIN " & _
                        "dbo.ArticleUnitDefTable ON dbo.ArticleDefTableMaster.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " & _
                        "dbo.ArticleGenderDefTable ON dbo.ArticleDefTableMaster.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId LEFT OUTER JOIN " & _
                        "dbo.ArticleLpoDefTable ON dbo.ArticleDefTableMaster.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN " & _
                        "dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId LEFT OUTER JOIN ArticleBrandDefTable on ArticleBrandDefTable.ArticleBrandId = ArticleDefTableMaster.ArticleBrandId " & _
                        " LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = dbo.ArticleDefTableMaster.ArticleId"
            Return UtilityDAL.GetDataTable(strSQL)
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPackQty(ByVal id As Integer) As DataTable
        Try
            ''TFS1964
            Dim strSQL As String = "Select ArticlePackId,ArticleMasterId,PackName,IsNull(PackQty,0) as PackQty ,IsNull(PackRate,0) as PackRate from ArticleDefPackTable Where ArticleMasterId = " & id
            Return UtilityDAL.GetDataTable(strSQL)
        Catch ex As SqlException
            Throw ex
        End Try
    End Function

    Public Function Add(ByVal objModel As Article) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            'insert into accounts
            'Remove Article's Account against request no 812 by imran ali 
            '
            'objModel.COADetail.COADetailID = New COADetailDAL().Add(objModel.COADetail, objModel.AccountID, trans)
            'insert master
            'Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleGroupId," & _
            '" ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
            '" StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID, Remarks, ServiceItem, ArticlePicture, TradePrice, Freight, MarketReturns, GST_Applicable, FlatRate_Applicable,FlatRate, ItemWeight, HS_Code, LargestPackQty)" & _
            '" values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
            'objModel.ArticleGroupID & "," & objModel.ArticleTypeID & "," & objModel.ArticleGenderID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
            'objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & objModel.StockLevel & "," & _
            'objModel.StockLevelOpt & "," & objModel.StockLevelMax & "," & IIf(objModel.Active = True, 1, 0) & "," & objModel.SortOrder & ",N'" & _
            'objModel.IsDate & "'," & objModel.COADetail.COADetailID & ",N'" & objModel.ArticleRemarks.ToString & "', " & IIf(objModel.ServiceItem = True, 1, 0) & ", N'" & objModel.ArticlePicture & "', " & objModel.TradePrice & ", " & objModel.Freight & ", " & objModel.MarketReturns & ", " & IIf(objModel.GST_Applicable = True, 1, 0) & ", " & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", " & objModel.FlatRate & ", " & objModel.ItemWeight & ", N'" & objModel.HS_Code.Replace("'", "''") & "', " & objModel.LargestPackQty & ") Select @@Identity"

            ' Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleGroupId," & _
            '" ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
            '" StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID, Remarks, ServiceItem, ArticlePicture, TradePrice, Freight, MarketReturns, GST_Applicable, FlatRate_Applicable,FlatRate, ItemWeight, HS_Code, LargestPackQty)" & _
            '" values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
            'objModel.ArticleGroupID & "," & objModel.ArticleTypeID & "," & objModel.ArticleGenderID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
            'objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & objModel.StockLevel & "," & _
            'objModel.StockLevelOpt & "," & objModel.StockLevelMax & "," & IIf(objModel.Active = True, 1, 0) & "," & objModel.SortOrder & ",N'" & _
            'objModel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & objModel.AccountID & ",N'" & objModel.ArticleRemarks.ToString & "', " & IIf(objModel.ServiceItem = True, 1, 0) & ", N'" & objModel.ArticlePicture & "', " & objModel.TradePrice & ", " & objModel.Freight & ", " & objModel.MarketReturns & ", " & IIf(objModel.GST_Applicable = True, 1, 0) & ", " & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", " & objModel.FlatRate & ", " & objModel.ItemWeight & ", N'" & objModel.HS_Code.Replace("'", "''") & "', " & objModel.LargestPackQty & ") Select @@Identity"
            'Added field AutoCode
            'If objModel.AutoCode = True Then
            '    objModel.ArticleCode = GetArticleCode(objModel.Prefix, objModel.Length, trans)
            'End If

            objModel.CostPrice = objModel.PurchasePrice
            '   Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleGroupId," & _
            '" ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
            '" StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID, Remarks, ServiceItem, ArticlePicture, TradePrice, Freight, MarketReturns, GST_Applicable, FlatRate_Applicable,FlatRate, ItemWeight, HS_Code, LargestPackQty,AutoCode,Cost_Price)" & _
            '" values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
            'objModel.ArticleGroupID & "," & objModel.ArticleTypeID & "," & objModel.ArticleGenderID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
            'objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & objModel.StockLevel & "," & _
            'objModel.StockLevelOpt & "," & objModel.StockLevelMax & "," & IIf(objModel.Active = True, 1, 0) & "," & objModel.SortOrder & ",N'" & _
            'objModel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & objModel.AccountID & ",N'" & objModel.ArticleRemarks.ToString & "', " & IIf(objModel.ServiceItem = True, 1, 0) & ", N'" & objModel.ArticlePicture & "', " & objModel.TradePrice & ", " & objModel.Freight & ", " & objModel.MarketReturns & ", " & IIf(objModel.GST_Applicable = True, 1, 0) & ", " & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", " & objModel.FlatRate & ", " & objModel.ItemWeight & ", N'" & objModel.HS_Code.Replace("'", "''") & "', " & objModel.LargestPackQty & ", " & IIf(objModel.AutoCode = True, 1, 0) & ", " & objModel.CostPrice & ") Select @@Identity"


            'Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleGroupId," & _
            '                    " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
            '                    " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID, Remarks, ServiceItem, ArticlePicture, TradePrice, Freight, MarketReturns, GST_Applicable, FlatRate_Applicable,FlatRate, ItemWeight, HS_Code, LargestPackQty,AutoCode,Cost_Price)" & _
            '                    " values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
            '                    objModel.ArticleGroupID & "," & objModel.ArticleTypeID & "," & objModel.ArticleGenderID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
            '                    objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & objModel.StockLevel & "," & _
            '                    objModel.StockLevelOpt & "," & objModel.StockLevelMax & "," & IIf(objModel.Active = True, 1, 0) & "," & objModel.SortOrder & ",N'" & _
            '                    objModel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & objModel.AccountID & ",N'" & objModel.ArticleRemarks.ToString & "', " & IIf(objModel.ServiceItem = True, 1, 0) & ", N'" & objModel.ArticlePicture & "', " & objModel.TradePrice & ", " & objModel.Freight & ", " & objModel.MarketReturns & ", " & IIf(objModel.GST_Applicable = True, 1, 0) & ", " & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", " & objModel.FlatRate & ", " & objModel.ItemWeight & ", N'" & objModel.HS_Code.Replace("'", "''") & "', " & objModel.LargestPackQty & ", " & IIf(objModel.AutoCode = True, 1, 0) & ", " & objModel.CostPrice & ") Select @@Identity"


            'Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleGroupId," & _
            '                    " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
            '                    " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID, Remarks, ServiceItem, ArticlePicture, TradePrice, Freight, MarketReturns, GST_Applicable, FlatRate_Applicable,FlatRate, ItemWeight, HS_Code, LargestPackQty,AutoCode,Cost_Price,ArticleCategoryId, ArticleStatusID)" & _
            '                    " values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
            '                    objModel.ArticleGroupID & "," & objModel.ArticleTypeID & "," & objModel.ArticleGenderID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
            '                    objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & objModel.StockLevel & "," & _
            '                    objModel.StockLevelOpt & "," & objModel.StockLevelMax & "," & IIf(objModel.Active = True, 1, 0) & "," & objModel.SortOrder & ",N'" & _
            '                    objModel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & objModel.AccountID & ",N'" & objModel.ArticleRemarks.ToString & "', " & IIf(objModel.ServiceItem = True, 1, 0) & ", N'" & objModel.ArticlePicture & "', " & objModel.TradePrice & ", " & objModel.Freight & ", " & objModel.MarketReturns & ", " & IIf(objModel.GST_Applicable = True, 1, 0) & ", " & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", " & objModel.FlatRate & ", " & objModel.ItemWeight & ", N'" & objModel.HS_Code.Replace("'", "''") & "', " & objModel.LargestPackQty & ", " & IIf(objModel.AutoCode = True, 1, 0) & ", " & objModel.CostPrice & "," & objModel.ArticleCategoryId & "," & objModel.ArticleStatusID & ") Select @@Identity"



            ''TFS1772
            ''TFS1957 : Added column Logical item In the query
            ''TFS1799 : Added column ArticleTaxId In the query
            ''TFS3763 : Ayesha Rehman : Added column ArticleBARCodeDisable In the query
            Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleBARCode,ArticleGroupId," & _
                                " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                                " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID, Remarks, ServiceItem, ArticlePicture, TradePrice, Freight, MarketReturns, GST_Applicable, FlatRate_Applicable,FlatRate, ItemWeight, HS_Code, LargestPackQty,AutoCode,Cost_Price,ArticleCategoryId, ArticleStatusID,ArticleBrandId,ApplyAdjustmentFuelExp,MultiDimentionalItem , LogicalItem, CGSAccountId, ProductionProcessId,ArticleTaxId, IsIndividual,ArticleBARCodeDisable)" & _
                                " values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "',N'" & objModel.ArticleBARCode.Trim.Replace("'", "''") & "'," & _
                                objModel.ArticleGroupID & "," & objModel.ArticleTypeID & "," & objModel.ArticleGenderID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
                                objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & objModel.StockLevel & "," & _
                                objModel.StockLevelOpt & "," & objModel.StockLevelMax & "," & IIf(objModel.Active = True, 1, 0) & "," & objModel.SortOrder & ",N'" & _
                                objModel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & objModel.AccountID & ",N'" & objModel.ArticleRemarks.ToString.Replace("'", "''") & "', " & IIf(objModel.ServiceItem = True, 1, 0) & ", N'" & objModel.ArticlePicture & "', " & objModel.TradePrice & ", " & objModel.Freight & ", " & objModel.MarketReturns & ", " & IIf(objModel.GST_Applicable = True, 1, 0) & ", " & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", " & objModel.FlatRate & ", " & objModel.ItemWeight & ", N'" & objModel.HS_Code.Replace("'", "''") & "', " & objModel.LargestPackQty & ", " & IIf(objModel.AutoCode = True, 1, 0) & ", " & objModel.CostPrice & "," & objModel.ArticleCategoryId & "," & objModel.ArticleStatusID & "," & objModel.ArticleBrandId & "," & IIf(objModel.ApplyAdjustmentFuelExp = True, 1, 0) & "," & IIf(objModel.MultiDimentionalItem = True, 1, 0) & "," & IIf(objModel.LogicalItem = True, 1, 0) & "," & objModel.CGSAccountId & ", " & IIf(objModel.ProductionProcessId > 0, objModel.ProductionProcessId, "NULL") & "," & objModel.ArticleTaxID & ", " & IIf(objModel.IsIndividual = True, 1, 0) & ", " & IIf(objModel.ArticleBARCodeDisable = True, 1, 0) & ") Select @@Identity"  ''TFS1772


            objModel.ArticleID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))
            ArticleMasterId = objModel.ArticleID

            strSQL = String.Empty
            strSQL = "INSERT INTO ArticleDefPackTable(ArticleMasterId, PackName, PackQty) Values(" & ArticleMasterId & ", 'Loose', 1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "INSERT INTO ArticleDefPackTable(ArticleMasterId, PackName, PackQty) Values(" & ArticleMasterId & ", 'Pack', " & objModel.PackQty & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''add deltail
            'If Me.AddDetail(objModel.ArticleID, objModel, trans, objModel.COADetail.COADetailID) Then
            If Me.AddDetail(objModel.ArticleID, objModel, trans, objModel.AccountID) Then

                'add increment reduction
                'objModel.IncrementReduction.ArticleID = objModel.ArticleID
                'Call New IncrementReductionDAL().Add(objModel.IncrementReduction, trans)
                ''add activity log
                objModel.ActivityLog.ActivityName = "Save"
                objModel.ActivityLog.RecordType = "Configuration"
                UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
                ''TASK TFS1779
                SaveDocument(objModel.ArticleID, objModel.Source, objModel.AttachmentPath, objModel.ArrFile, objModel.IsDate, trans)
                ''END TASK TFS1779

                trans.Commit()
                Return True
            End If

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Private Function AddDetail(ByVal MasterID As Integer, ByVal objArticle As Article, ByVal trans As SqlTransaction, ByVal COADetailID As Integer) As Boolean
        Try

            Dim strSQL As String = String.Empty
            Dim objmodels As List(Of ArticleDetail) = objArticle.ArticleDetails

            If objmodels Is Nothing Then Return False
            If objmodels.Count = 0 Then Return False

            For Each objmodel As ArticleDetail In objmodels
                objmodel.CostPrice = objArticle.CostPrice
                'strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId," & _
                '                       " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                '                       " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, Remarks, ServiceItem, TradePrice,Freight, MarketReturns, GST_Applicable, FlatRate_Applicable, FlatRate, ItemWeight, HS_Code, LargestPackQty,Cost_Price)" & _
                '                       " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
                '                       objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                '                       objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                '                       objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                '                       objmodel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & COADetailID & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ",'', " & IIf(objmodel.ServiceItem = True, 1, 0) & ", " & objmodel.TradePrice & ", " & objmodel.Freight & ", " & objmodel.MarketReturns & ", " & IIf(objmodel.GST_Applicable = True, 1, 0) & ", " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & "," & objmodel.FlatRate & ", " & objmodel.ItemWeight & ", N'" & objmodel.HS_Code.Replace("'", "''") & "', " & objmodel.LargestPackQty & "," & objmodel.CostPrice & ") select @@Identity"
                'strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId," & _
                '                      " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                '                      " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, Remarks, ServiceItem, TradePrice,Freight, MarketReturns, GST_Applicable, FlatRate_Applicable, FlatRate, ItemWeight, HS_Code, LargestPackQty,Cost_Price)" & _
                '                      " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
                '                      objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                '                      objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                '                      objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                '                      objmodel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & COADetailID & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ",'', " & IIf(objmodel.ServiceItem = True, 1, 0) & ", " & objmodel.TradePrice & ", " & objmodel.Freight & ", " & objmodel.MarketReturns & ", " & IIf(objmodel.GST_Applicable = True, 1, 0) & ", " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & "," & objmodel.FlatRate & ", " & objmodel.ItemWeight & ", N'" & objmodel.HS_Code.Replace("'", "''") & "', " & objmodel.LargestPackQty & "," & objmodel.CostPrice & ") select @@Identity"

                'strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId," & _
                '                    " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                '                    " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, Remarks, ServiceItem, TradePrice,Freight, MarketReturns, GST_Applicable, FlatRate_Applicable, FlatRate, ItemWeight, HS_Code, LargestPackQty,Cost_Price,ArticleCategoryId, ArticleStatusID)" & _
                '                    " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
                '                    objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                '                    objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                '                    objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                '                    objmodel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & COADetailID & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ",'', " & IIf(objmodel.ServiceItem = True, 1, 0) & ", " & objmodel.TradePrice & ", " & objmodel.Freight & ", " & objmodel.MarketReturns & ", " & IIf(objmodel.GST_Applicable = True, 1, 0) & ", " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & "," & objmodel.FlatRate & ", " & objmodel.ItemWeight & ", N'" & objmodel.HS_Code.Replace("'", "''") & "', " & objmodel.LargestPackQty & "," & objmodel.CostPrice & "," & objmodel.ArticleCategoryId & " ," & objmodel.ArticleStatusID & ") select @@Identity"

                ''TFS1957 :Added Column Logical Item in the query
                ''TFS1799 : Added column ArticleTaxId In the query
                ''TFS3763 : Ayesha Rehman : Added column ArticleBARCodeDisable In the query
                strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleBARCode,ArticleGroupId," & _
                                 " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                                 " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, Remarks, ServiceItem, TradePrice,Freight, MarketReturns, GST_Applicable, FlatRate_Applicable, FlatRate, ItemWeight, HS_Code, LargestPackQty,Cost_Price,ArticleCategoryId, ArticleStatusID,ArticleBrandId,ApplyAdjustmentFuelExp,MultiDimentionalItem,LogicalItem,ArticleTaxId,ArticleBARCodeDisable)" & _
                                 " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleBARCode.Trim.Replace("'", "''") & "'," & _
                                 objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                                 objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                                 objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                                 objmodel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & COADetailID & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ",'', " & IIf(objmodel.ServiceItem = True, 1, 0) & ", " & objmodel.TradePrice & ", " & objmodel.Freight & ", " & objmodel.MarketReturns & ", " & IIf(objmodel.GST_Applicable = True, 1, 0) & ", " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & "," & objmodel.FlatRate & ", " & objmodel.ItemWeight & ", N'" & objmodel.HS_Code.Replace("'", "''") & "', " & objmodel.LargestPackQty & "," & objmodel.CostPrice & "," & objmodel.ArticleCategoryId & " ," & objmodel.ArticleStatusID & "," & objmodel.ArticleBrandId & "," & IIf(objmodel.ApplyAdjustmentFuelExp = True, 1, 0) & "," & IIf(objmodel.MultiDimentionalItem = True, 1, 0) & "," & IIf(objmodel.LogicalItem = True, 1, 0) & "," & objmodel.ArticleTaxID & ", " & IIf(objmodel.ArticleBARCodeDisable = True, 1, 0) & ") select @@Identity"

                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ArticleIdForArticleAlias = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))
                ''Start TFS4395
                strSQL = "Insert into ArticleBarcodeDefTable (ArticleMasterID,ArticleId, ArticleBARCode,ArticleCode,ArticleName) values ( " & MasterID & "," & ArticleIdForArticleAlias & ",'" & objmodel.ArticleBARCode.Trim.Replace("'", "''") & "','" & objmodel.ArticleCode.Trim.Replace("'", "''") & "', '" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "')Select @@Identity"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ''End TFS4395
                Dim objDt As New DataTable
                objDt = Nothing
                objDt = UtilityDAL.GetDataTable(" select articleDefTable.ArticleId from articleDefTable where MasterId=" & MasterID & " and SizeRangeId=" & objmodel.SizeRangeID & " and ArticleColorId=" & objmodel.ArticleColorID, trans)
                If objDt.Rows.Count > 0 Then objArticle.IncrementReduction.ArticleID = objDt.Rows(0).Item(0)
                objArticle.IncrementReduction.New_Cost_Price = objmodel.CostPrice
                objArticle.IncrementReduction.Old_Cost_Price = 0D
                Call New IncrementReductionDAL().Add(objArticle.IncrementReduction, trans)
                objmodel.ArticleID = ArticleIdForArticleAlias
                Call New ArticleModelsDAL().Insert(objmodel, MasterID, trans)
            Next
            Dim objALRList As List(Of ArticalLocationRank) = objArticle.ArticalLocationRank
            For Each objALR As ArticalLocationRank In objALRList
                strSQL = "Insert Into ArticalDefLocation(ArticalID,LocationID,Ranks) Values (" & MasterID & "," & objALR.LocationID & ",N'" & objALR.Rank & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next

            ''TASK TFS1777
            For Each _RelatedItem As RelatedItem In objArticle.RelatedItemList
                If _RelatedItem.RelationId < 1 Then
                    strSQL = "Insert Into tblRelatedItem(ArticleId, RelatedArticleId) Values (" & MasterID & ", " & _RelatedItem.RelatedArticleId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            ''END TASK TFS1777
            Return True
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Update(ByVal objModel As Article) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            'upate Accounts
            'Remove Article's Account against request no 812 by imran ali
            'Call New COADetailDAL().Update(objModel.COADetail, trans)

            'Update master
            'Dim strSQL As String = " update ArticleDefTableMaster set ArticleCode=N'" & objModel.ArticleCode.Trim.Replace("'", "''") & _
            '"',ArticleDescription=N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & _
            '"', ArticleGroupId=N'" & objModel.ArticleGroupID & "',ArticleTypeId=" & objModel.ArticleTypeID & _
            '",ArticleGenderId=" & objModel.ArticleGenderID & ",ArticleUnitId=" & objModel.ArticleUnitID & ",ArticlelpoId=" & objModel.ArticleLPOID & _
            '",PurchasePrice=" & objModel.PurchasePrice & ",SalePrice=" & objModel.SalePrice & _
            '",PackQty=" & objModel.PackQty & ",StockLevel=" & objModel.StockLevel & _
            '",StockLevelopt=" & objModel.StockLevelOpt & ",StockLevelMax=" & objModel.StockLevelMax & _
            '",Active=" & IIf(objModel.Active = True, 1, 0) & ",SortOrder=" & objModel.SortOrder & ", Remarks=N'" & objModel.ArticleRemarks.ToString & "', ServiceItem=" & IIf(objModel.ServiceItem = True, 1, 0) & ", ArticlePicture=N'" & objModel.ArticlePicture & "' " & _
            '",TradePrice = " & objModel.TradePrice & ", Freight=" & objModel.Freight & ", MarketReturns=" & objModel.MarketReturns & ", GST_Applicable=" & IIf(objModel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable=" & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objModel.FlatRate & ", ItemWeight=" & objModel.ItemWeight & ", HS_Code=N'" & objModel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objModel.LargestPackQty & " " & _
            '" where ArticleId=" & objModel.ArticleID

            '  Dim strSQL As String = " update ArticleDefTableMaster set ArticleCode=N'" & objModel.ArticleCode.Trim.Replace("'", "''") & _
            '"',ArticleDescription=N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & _
            '"', ArticleGroupId=N'" & objModel.ArticleGroupID & "',ArticleTypeId=" & objModel.ArticleTypeID & _
            '",ArticleGenderId=" & objModel.ArticleGenderID & ",ArticleUnitId=" & objModel.ArticleUnitID & ",ArticlelpoId=" & objModel.ArticleLPOID & _
            '",PurchasePrice=" & objModel.PurchasePrice & ",SalePrice=" & objModel.SalePrice & _
            '",PackQty=" & objModel.PackQty & ",StockLevel=" & objModel.StockLevel & _
            '",StockLevelopt=" & objModel.StockLevelOpt & ",StockLevelMax=" & objModel.StockLevelMax & _
            '",Active=" & IIf(objModel.Active = True, 1, 0) & ",SortOrder=" & objModel.SortOrder & ", Remarks=N'" & objModel.ArticleRemarks.ToString & "', ServiceItem=" & IIf(objModel.ServiceItem = True, 1, 0) & ", ArticlePicture=N'" & objModel.ArticlePicture & "' " & _
            '",TradePrice = " & objModel.TradePrice & ", Freight=" & objModel.Freight & ", MarketReturns=" & objModel.MarketReturns & ", GST_Applicable=" & IIf(objModel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable=" & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objModel.FlatRate & ", ItemWeight=" & objModel.ItemWeight & ", HS_Code=N'" & objModel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objModel.LargestPackQty & " " & _
            '" where ArticleId=" & objModel.ArticleID

            'Added Field AutoCode


            Dim strQuery As String = String.Empty
            Dim dblCostPrice As Double = 0D

            strQuery = "Select  Case When IsNull(Qty,1) > 0 then (IsNull(Amount,0)/IsNull(Qty,1)) else 0 end as CostPrice From(Select SUM(IsNull(InQty,0)-IsNull(OutQty,0))+1 as Qty, SUM((IsNull(InQty,0)-IsNull(OutQty,0))*IsNull(Rate,0))+" & Val(objModel.PurchasePrice) & " as Amount From StockDetailTable INNER JOIN ArticleDefTable On ArticleDefTable.ArticleId = StockDetailTable.ArticleDefID WHERE ArticleDefTable.MasterID=" & objModel.ArticleID & ") a"
            Dim dtCostPrice As New DataTable
            dtCostPrice = UtilityDAL.GetDataTable(strQuery, trans)
            dtCostPrice.AcceptChanges()

            If dtCostPrice.Rows.Count > 0 Then
                If Val(dtCostPrice.Rows(0).Item(0).ToString) > 0 Then
                    dblCostPrice = Val(dtCostPrice.Rows(0).Item(0).ToString)
                Else
                    dblCostPrice = objModel.PurchasePrice
                End If
            Else
                dblCostPrice = objModel.PurchasePrice
            End If

            objModel.CostPrice = dblCostPrice


            '  Dim strSQL As String = " update ArticleDefTableMaster set ArticleCode=N'" & objModel.ArticleCode.Trim.Replace("'", "''") & _
            '"',ArticleDescription=N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & _
            '"', ArticleGroupId=N'" & objModel.ArticleGroupID & "',ArticleTypeId=" & objModel.ArticleTypeID & _
            '",ArticleGenderId=" & objModel.ArticleGenderID & ",ArticleUnitId=" & objModel.ArticleUnitID & ",ArticlelpoId=" & objModel.ArticleLPOID & _
            '",PurchasePrice=" & objModel.PurchasePrice & ",SalePrice=" & objModel.SalePrice & _
            '",PackQty=" & objModel.PackQty & ",StockLevel=" & objModel.StockLevel & _
            '",StockLevelopt=" & objModel.StockLevelOpt & ",StockLevelMax=" & objModel.StockLevelMax & _
            '",Active=" & IIf(objModel.Active = True, 1, 0) & ",SortOrder=" & objModel.SortOrder & ", Remarks=N'" & objModel.ArticleRemarks.ToString & "', ServiceItem=" & IIf(objModel.ServiceItem = True, 1, 0) & ", ArticlePicture=N'" & objModel.ArticlePicture & "' " & _
            '",TradePrice = " & objModel.TradePrice & ", Freight=" & objModel.Freight & ", MarketReturns=" & objModel.MarketReturns & ", GST_Applicable=" & IIf(objModel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable=" & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objModel.FlatRate & ", ItemWeight=" & objModel.ItemWeight & ", HS_Code=N'" & objModel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objModel.LargestPackQty & ", AutoCode=" & IIf(objModel.AutoCode = True, 1, 0) & ", Cost_Price=" & objModel.CostPrice & " " & _
            '" where ArticleId=" & objModel.ArticleID
            '      Dim strSQL As String = " update ArticleDefTableMaster set ArticleCode=N'" & objModel.ArticleCode.Trim.Replace("'", "''") & _
            '"',ArticleDescription=N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & _
            '"', ArticleGroupId=N'" & objModel.ArticleGroupID & "',ArticleTypeId=" & objModel.ArticleTypeID & _
            '",ArticleGenderId=" & objModel.ArticleGenderID & ",ArticleUnitId=" & objModel.ArticleUnitID & ",ArticlelpoId=" & objModel.ArticleLPOID & _
            '",PurchasePrice=" & objModel.PurchasePrice & ",SalePrice=" & objModel.SalePrice & _
            '",PackQty=" & objModel.PackQty & ",StockLevel=" & objModel.StockLevel & _
            '",StockLevelopt=" & objModel.StockLevelOpt & ",StockLevelMax=" & objModel.StockLevelMax & _
            '",Active=" & IIf(objModel.Active = True, 1, 0) & ",SortOrder=" & objModel.SortOrder & ", Remarks=N'" & objModel.ArticleRemarks.ToString & "', ServiceItem=" & IIf(objModel.ServiceItem = True, 1, 0) & ", ArticlePicture=N'" & objModel.ArticlePicture & "' " & _
            '",TradePrice = " & objModel.TradePrice & ", Freight=" & objModel.Freight & ", MarketReturns=" & objModel.MarketReturns & ", GST_Applicable=" & IIf(objModel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable=" & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objModel.FlatRate & ", ItemWeight=" & objModel.ItemWeight & ", HS_Code=N'" & objModel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objModel.LargestPackQty & ", AutoCode=" & IIf(objModel.AutoCode = True, 1, 0) & ", Cost_Price=" & objModel.CostPrice & " " & _
            '" where ArticleId=" & objModel.ArticleID


            'Dim strSQL As String = " update ArticleDefTableMaster set ArticleCode=N'" & objModel.ArticleCode.Trim.Replace("'", "''") & _
            '                    "',ArticleDescription=N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & _
            '                    "', ArticleGroupId=N'" & objModel.ArticleGroupID & "',ArticleTypeId=" & objModel.ArticleTypeID & _
            '                    ",ArticleGenderId=" & objModel.ArticleGenderID & ",ArticleUnitId=" & objModel.ArticleUnitID & ",ArticlelpoId=" & objModel.ArticleLPOID & _
            '                    ",PurchasePrice=" & objModel.PurchasePrice & ",SalePrice=" & objModel.SalePrice & _
            '                    ",PackQty=" & objModel.PackQty & ",StockLevel=" & objModel.StockLevel & _
            '                    ",StockLevelopt=" & objModel.StockLevelOpt & ",StockLevelMax=" & objModel.StockLevelMax & _
            '                    ",Active=" & IIf(objModel.Active = True, 1, 0) & ",SortOrder=" & objModel.SortOrder & ", Remarks=N'" & objModel.ArticleRemarks.ToString & "', ServiceItem=" & IIf(objModel.ServiceItem = True, 1, 0) & ", ArticlePicture=N'" & objModel.ArticlePicture & "' " & _
            '                    ",TradePrice = " & objModel.TradePrice & ", Freight=" & objModel.Freight & ", MarketReturns=" & objModel.MarketReturns & ", GST_Applicable=" & IIf(objModel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable=" & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objModel.FlatRate & ", ItemWeight=" & objModel.ItemWeight & ", HS_Code=N'" & objModel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objModel.LargestPackQty & ", AutoCode=" & IIf(objModel.AutoCode = True, 1, 0) & ", Cost_Price=" & objModel.CostPrice & ",ArticleCategoryId=" & objModel.ArticleCategoryId & ", ArticleStatusID= " & objModel.ArticleStatusID & " " & _
            '                    " where ArticleId=" & objModel.ArticleID

            ''TFS1772
            ''TFS1957
            ''TFS1799
            ''TFS3763 : Ayesha Rehman : Updated column ArticleBARCodeDisable In the query
            Dim strSQL As String = " update ArticleDefTableMaster set ArticleCode=N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',ArticleBARCode = N'" & objModel.ArticleBARCode.Trim.Replace("'", "''") & _
                                "',ArticleDescription=N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & _
                                "', ArticleGroupId=N'" & objModel.ArticleGroupID & "',ArticleTypeId=" & objModel.ArticleTypeID & _
                                ",ArticleGenderId=" & objModel.ArticleGenderID & ",ArticleUnitId=" & objModel.ArticleUnitID & ",ArticleTaxId=" & objModel.ArticleTaxID & ",ArticlelpoId=" & objModel.ArticleLPOID & _
                                ",PurchasePrice=" & objModel.PurchasePrice & ",SalePrice=" & objModel.SalePrice & _
                                ",PackQty=" & objModel.PackQty & ",StockLevel=" & objModel.StockLevel & _
                                ",StockLevelopt=" & objModel.StockLevelOpt & ",StockLevelMax=" & objModel.StockLevelMax & _
                                ",Active=" & IIf(objModel.Active = True, 1, 0) & ",SortOrder=" & objModel.SortOrder & ", Remarks=N'" & objModel.ArticleRemarks.ToString.Replace("'", "''") & "', ServiceItem=" & IIf(objModel.ServiceItem = True, 1, 0) & ", ArticlePicture=N'" & objModel.ArticlePicture & "' " & _
                                ",TradePrice = " & objModel.TradePrice & ", Freight=" & objModel.Freight & ", MarketReturns=" & objModel.MarketReturns & ", GST_Applicable=" & IIf(objModel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable=" & IIf(objModel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objModel.FlatRate & ", ItemWeight=" & objModel.ItemWeight & ", HS_Code=N'" & objModel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objModel.LargestPackQty & ", AutoCode=" & IIf(objModel.AutoCode = True, 1, 0) & ", Cost_Price=" & objModel.CostPrice & ",ArticleCategoryId=" & objModel.ArticleCategoryId & ", ArticleStatusID= " & objModel.ArticleStatusID & ",ArticleBrandId=" & objModel.ArticleBrandId & ", ApplyAdjustmentFuelExp=" & IIf(objModel.ApplyAdjustmentFuelExp = True, 1, 0) & ",CGSAccountId = " & objModel.CGSAccountId & ",LogicalItem= " & IIf(objModel.LogicalItem = True, 1, 0) & ", MultiDimentionalItem =" & IIf(objModel.MultiDimentionalItem = True, 1, 0) & ", ProductionProcessId= " & IIf(objModel.ProductionProcessId > 0, objModel.ProductionProcessId, "NULL") & " " & _
                                " , IsIndividual =" & IIf(objModel.IsIndividual = True, 1, 0) & ",ArticleBARCodeDisable = " & IIf(objModel.ArticleBARCodeDisable = True, 1, 0) & " where ArticleId=" & objModel.ArticleID

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            ArticleMasterId = objModel.ArticleID
            ''update deltail

            ' If Me.UpdateDetail(objModel.ArticleID, objModel, trans, objModel.COADetail.COADetailID) Then
            If Me.UpdateDetail(objModel.ArticleID, objModel, trans, objModel.AccountID) Then

                'objModel.IncrementReduction.ArticleID = objModel.ArticleID
                'Call New IncrementReductionDAL().Add(objModel.IncrementReduction, trans)

                ''add activity log
                objModel.ActivityLog.ActivityName = "Update"
                objModel.ActivityLog.RecordType = "Configuration"
                UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
                ''TASK TFS1779
                SaveDocument(objModel.ArticleID, objModel.Source, objModel.AttachmentPath, objModel.ArrFile, objModel.IsDate, trans)
                ''END TASK TFS1779
                trans.Commit()
                Return True
            End If
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Private Function UpdateDetail(ByVal MasterID As Integer, ByVal objArticle As Article, ByVal trans As SqlTransaction, ByVal COADetailId As Integer) As Boolean
        Try

            Dim objmodels As List(Of ArticleDetail) = objArticle.ArticleDetails
            Dim strSQL As String = String.Empty

            If objmodels Is Nothing Then Return False
            If objmodels.Count = 0 Then Return False

            '// Commented delete insert
            '' ''deleting the deltail
            ''Me.Delete(MasterID, trans)

            '' ''insert detail
            ''Me.AddDetail(MasterID, objmodels, trans, COADetailId)
            '// End Comments
            For Each objmodel As ArticleDetail In objmodels
                objmodel.CostPrice = objArticle.CostPrice

                If UtilityDAL.GetDataTable("select * from articleDefTable where masterId=" & MasterID & " and ArticleColorId=" & objmodel.ArticleColorID & " and SizeRangeId=" & objmodel.SizeRangeID & " ", trans).Rows.Count > 0 Then

                    'strSQL = "update ArticleDefTable set ArticleCode=N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & _
                    '           "',ArticleDescription=N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & _
                    '           "', ArticleGroupId=N'" & objmodel.ArticleGroupID & "',ArticleTypeId=" & objmodel.ArticleTypeID & _
                    '           ",ArticleGenderId=" & objmodel.ArticleGenderID & ",ArticleUnitId=" & objmodel.ArticleUnitID & ",ArticlelpoId=" & objmodel.ArticleLPOID & _
                    '           ",PurchasePrice=" & objmodel.PurchasePrice & ",SalePrice=" & objmodel.SalePrice & _
                    '           ",PackQty=" & objmodel.PackQty & ",StockLevel=" & objmodel.StockLevel & _
                    '           ",StockLevelopt=" & objmodel.StockLevelOpt & ",StockLevelMax=" & objmodel.StockLevelMax & _
                    '           ",Active=" & IIf(objmodel.Active = True, 1, 0) & ",SortOrder=" & objmodel.SortOrder & _
                    '           ",ServiceItem=" & IIf(objmodel.ServiceItem = True, 1, 0) & _
                    '           ",TradePrice = " & objmodel.TradePrice & ", Freight=" & objmodel.Freight & ", MarketReturns=" & objmodel.MarketReturns & ", GST_Applicable=" & IIf(objmodel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable= " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objmodel.FlatRate & ", ItemWeight=" & objmodel.ItemWeight & ", HS_Code=N'" & objmodel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objmodel.LargestPackQty & ", Cost_Price=" & objmodel.CostPrice & " " & _
                    '           " where MasterId=" & MasterID & " and SizeRangeId=" & objmodel.SizeRangeID & " and ArticleColorId=" & objmodel.ArticleColorID
                    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                    'strSQL = "update ArticleDefTable set ArticleCode=N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & _
                    '           "',ArticleDescription=N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & _
                    '           "', ArticleGroupId=N'" & objmodel.ArticleGroupID & "',ArticleTypeId=" & objmodel.ArticleTypeID & _
                    '           ",ArticleGenderId=" & objmodel.ArticleGenderID & ",ArticleUnitId=" & objmodel.ArticleUnitID & ",ArticlelpoId=" & objmodel.ArticleLPOID & _
                    '           ",PurchasePrice=" & objmodel.PurchasePrice & ",SalePrice=" & objmodel.SalePrice & _
                    '           ",PackQty=" & objmodel.PackQty & ",StockLevel=" & objmodel.StockLevel & _
                    '           ",StockLevelopt=" & objmodel.StockLevelOpt & ",StockLevelMax=" & objmodel.StockLevelMax & _
                    '           ",Active=" & IIf(objmodel.Active = True, 1, 0) & ",SortOrder=" & objmodel.SortOrder & _
                    '           ",ServiceItem=" & IIf(objmodel.ServiceItem = True, 1, 0) & _
                    '           ",TradePrice = " & objmodel.TradePrice & ", Freight=" & objmodel.Freight & ", MarketReturns=" & objmodel.MarketReturns & ", GST_Applicable=" & IIf(objmodel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable= " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objmodel.FlatRate & ", ItemWeight=" & objmodel.ItemWeight & ", HS_Code=N'" & objmodel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objmodel.LargestPackQty & ", Cost_Price=" & objmodel.CostPrice & ", ArticleCategoryId=" & objmodel.ArticleCategoryId & " , ArticleStatusID=" & objmodel.ArticleStatusID & " " & _
                    '           " where MasterId=" & MasterID & " and SizeRangeId=" & objmodel.SizeRangeID & " and ArticleColorId=" & objmodel.ArticleColorID


                    ''TFS1772
                    ''TFS1957
                    ''TFS1799
                    ''TFS3763 : Ayesha Rehman : Updated column ArticleBARCodeDisable In the query
                    strSQL = "update ArticleDefTable set ArticleCode=N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & _
                               "',ArticleDescription=N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "',ArticleBARCode = N'" & objmodel.ArticleBARCode.Trim.Replace("'", "''") & _
                               "', ArticleGroupId=N'" & objmodel.ArticleGroupID & "',ArticleTypeId=" & objmodel.ArticleTypeID & _
                               ",ArticleGenderId=" & objmodel.ArticleGenderID & ",ArticleUnitId=" & objmodel.ArticleUnitID & ",ArticleTaxId=" & objmodel.ArticleTaxID & ",ArticlelpoId=" & objmodel.ArticleLPOID & _
                               ",PurchasePrice=" & objmodel.PurchasePrice & ",SalePrice=" & objmodel.SalePrice & _
                               ",PackQty=" & objmodel.PackQty & ",StockLevel=" & objmodel.StockLevel & _
                               ",StockLevelopt=" & objmodel.StockLevelOpt & ",StockLevelMax=" & objmodel.StockLevelMax & _
                               ",Active=" & IIf(objmodel.Active = True, 1, 0) & ",SortOrder=" & objmodel.SortOrder & _
                               ",ServiceItem=" & IIf(objmodel.ServiceItem = True, 1, 0) & _
                               ",TradePrice = " & objmodel.TradePrice & ", Freight=" & objmodel.Freight & ", MarketReturns=" & objmodel.MarketReturns & ", GST_Applicable=" & IIf(objmodel.GST_Applicable = True, 1, 0) & ", FlatRate_Applicable= " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & ", FlatRate=" & objmodel.FlatRate & ", ItemWeight=" & objmodel.ItemWeight & ", HS_Code=N'" & objmodel.HS_Code.Replace("'", "''") & "', LargestPackQty=" & objmodel.LargestPackQty & ", Cost_Price=" & objmodel.CostPrice & ", ArticleCategoryId=" & objmodel.ArticleCategoryId & " , ArticleStatusID=" & objmodel.ArticleStatusID & ",ArticleBrandId=" & objmodel.ArticleBrandId & ",ApplyAdjustmentFuelExp=" & IIf(objmodel.ApplyAdjustmentFuelExp = True, 1, 0) & ",LogicalItem=" & IIf(objmodel.LogicalItem = True, 1, 0) & ",MultiDimentionalItem=" & IIf(objmodel.MultiDimentionalItem = True, 1, 0) & ",ArticleBARCodeDisable= " & IIf(objmodel.ArticleBARCodeDisable = True, 1, 0) & " " & _
                               " where MasterId=" & MasterID & " and SizeRangeId=" & objmodel.SizeRangeID & " and ArticleColorId=" & objmodel.ArticleColorID

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    ''TASK TFS2128 added trans in order to use ArticleDAL's trans.
                    Call New ArticleModelsDAL().Remove(objmodel.ArticleID, trans)
                    Call New ArticleModelsDAL().Insert(objmodel, MasterID, trans)
                Else
                    'strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId," & _
                    '                       " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                    '                       " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, ServiceItem,TradePrice,Freight, MarketReturns, GST_Applicable, FlatRate_Applicable, FlatRate, ItemWeight, HS_Code, LargestPackQty,Cost_Price)" & _
                    '                       " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
                    '                       objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                    '                       objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                    '                       objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                    '                       objmodel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & COADetailId & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ", " & IIf(objmodel.ServiceItem = True, 1, 0) & "," & objmodel.TradePrice & ", " & objmodel.Freight & ", " & objmodel.MarketReturns & ", " & IIf(objmodel.GST_Applicable = True, 1, 0) & ", " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & ", " & objmodel.FlatRate & ", " & objmodel.ItemWeight & ", N'" & objmodel.HS_Code.Replace("'", "''") & "', " & objmodel.LargestPackQty & "," & objmodel.CostPrice & ") Select @@Identity "
                    'strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId," & _
                    '                      " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                    '                      " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, ServiceItem,TradePrice,Freight, MarketReturns, GST_Applicable, FlatRate_Applicable, FlatRate, ItemWeight, HS_Code, LargestPackQty,Cost_Price,ArticleCategoryId,ArticleStatusId)" & _
                    '                      " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
                    '                      objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                    '                      objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                    '                      objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                    '                      objmodel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & COADetailId & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ", " & IIf(objmodel.ServiceItem = True, 1, 0) & "," & objmodel.TradePrice & ", " & objmodel.Freight & ", " & objmodel.MarketReturns & ", " & IIf(objmodel.GST_Applicable = True, 1, 0) & ", " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & ", " & objmodel.FlatRate & ", " & objmodel.ItemWeight & ", N'" & objmodel.HS_Code.Replace("'", "''") & "', " & objmodel.LargestPackQty & "," & objmodel.CostPrice & "," & objmodel.ArticleCategoryId & "," & objmodel.ArticleStatusID & ") Select @@Identity "
                    ''TFS1772
                    ''TFS1957 :Ayesha Rehman :Added coulmn Logical Item
                    ''TFS1799
                    strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId," & _
                                       " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                                       " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, ServiceItem,TradePrice,Freight, MarketReturns, GST_Applicable, FlatRate_Applicable, FlatRate, ItemWeight, HS_Code, LargestPackQty,Cost_Price,ArticleCategoryId,ArticleStatusId,ArticleBrandId,ApplyAdjustmentFuelExp,MultiDimentionalItem,LogicalItem,ArticleTaxId)" & _
                                       " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
                                       objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                                       objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                                       objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                                       objmodel.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & COADetailId & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ", " & IIf(objmodel.ServiceItem = True, 1, 0) & "," & objmodel.TradePrice & ", " & objmodel.Freight & ", " & objmodel.MarketReturns & ", " & IIf(objmodel.GST_Applicable = True, 1, 0) & ", " & IIf(objmodel.FlatRate_Applicable = True, 1, 0) & ", " & objmodel.FlatRate & ", " & objmodel.ItemWeight & ", N'" & objmodel.HS_Code.Replace("'", "''") & "', " & objmodel.LargestPackQty & "," & objmodel.CostPrice & "," & objmodel.ArticleCategoryId & "," & objmodel.ArticleStatusID & "," & objmodel.ArticleBrandId & "," & IIf(objmodel.ApplyAdjustmentFuelExp = True, 1, 0) & "," & IIf(objmodel.MultiDimentionalItem = True, 1, 0) & "," & IIf(objmodel.LogicalItem = True, 1, 0) & "," & objmodel.ArticleTaxID & ") Select @@Identity "
                    Dim ArticleId As Integer = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))
                    objmodel.ArticleID = ArticleId
                    Call New ArticleModelsDAL().Insert(objmodel, MasterID, trans)
                End If
                Dim objDt As New DataTable

                objDt = Nothing
                objDt = UtilityDAL.GetDataTable(" select articleDefTable.ArticleId from articleDefTable where MasterId=" & MasterID & " and SizeRangeId=" & objmodel.SizeRangeID & " and ArticleColorId=" & objmodel.ArticleColorID, trans)
                If objDt.Rows.Count > 0 Then objArticle.IncrementReduction.ArticleID = objDt.Rows(0).Item(0)

                objArticle.IncrementReduction.Old_Cost_Price = 0I
                objArticle.IncrementReduction.New_Cost_Price = objmodel.CostPrice
                Call New IncrementReductionDAL().Add(objArticle.IncrementReduction, trans)

            Next

            Dim objALRList As List(Of ArticalLocationRank) = objArticle.ArticalLocationRank

            For Each objALR As ArticalLocationRank In objALRList
                If UtilityDAL.GetDataTable("SELECT * FROM ArticalDefLocation Where ArticalId=" & MasterID & " And LocationID=" & objALR.LocationID, trans).Rows.Count > 0 Then
                    strSQL = "Update ArticalDefLocation Set Ranks=N'" & objALR.Rank & "' Where ArticalId=" & MasterID & " And LocationID=" & objALR.LocationID
                Else
                    strSQL = "Insert Into ArticalDefLocation (ArticalID,LocationID,Ranks) Values (" & MasterID & "," & objALR.LocationID & ",N'" & objALR.Rank & "')"
                End If

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            Next
            ''TASK TFS1777
            For Each _RelatedItem As RelatedItem In objArticle.RelatedItemList
                If _RelatedItem.RelationId < 1 Then
                    strSQL = "Insert Into tblRelatedItem(ArticleId, RelatedArticleId) Values (" & MasterID & ", " & _RelatedItem.RelatedArticleId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            For Each _RelatedItem As RelatedItem In objArticle.RelatedItemList
                If _RelatedItem.RelationId < 1 Then
                    strSQL = "Insert Into tblRelatedItem(ArticleId, RelatedArticleId) Values (" & MasterID & ", " & _RelatedItem.RelatedArticleId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            ''END TASK TFS1777
            '********************** Delete From ArticleDefVendors *****************************
            Dim str As String = "Delete From ArticleDefVendors WHERE ArticleDefId= " & objArticle.ArticleID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Dim str1 As String = "Delete From ArticleDefCustomers WHERE ArticleDefId= " & objArticle.ArticleID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str1)

            AddVendorsItem(objArticle, trans)
            AddCustomerItem(objArticle, trans)
            Return True

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Delete(ByVal objmodel As Article) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            ''delete from detail table
            Dim strSQL As String = "Delete from ArticleDefTable where MasterId = " & objmodel.ArticleID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''delete from Master table
            strSQL = "Delete from ArticleDefTableMaster where ArticleID = " & objmodel.ArticleID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            '' delete from Cost Sheet
            strSQL = "Delete from tblCostSheet WHERE MasterArticleID=" & objmodel.ArticleID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            '' Delete from Article Pack Table
            strSQL = "Delete from ArticleDefPackTable WHERE ArticleMasterId=" & objmodel.ArticleID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''TASK TFS1777
            strSQL = "Delete from tblRelatedItem WHERE ArticleId=" & objmodel.ArticleID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''END TASK TFS1777

            ''add activity log
            objmodel.ActivityLog.ActivityName = "Delete"
            objmodel.ActivityLog.RecordType = "Configuration"
            UtilityDAL.BuildActivityLog(objmodel.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Private Function Delete(ByVal ID As Integer, ByVal trans As SqlTransaction) As Boolean
        Try

            ''delete from detail table
            Dim strSQL As String = "Delete from ArticleDefTable where MasterId = " & ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            Return True

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteRawItem(ByVal ArticleId As Integer, ByVal ParentId As Integer) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            ''delete from detail table
            Dim strSQL As String = "Delete from tblCostSheet Where ArticleId = " & ArticleId & " And ParentId = " & ParentId & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub AddCostSheetNew(ByVal data As DataTable, Optional ByVal Id As Integer = 0, Optional ByVal objMArticle As MasterArticle = Nothing)
        '' Request No 871
        '' 11-18-2013 by Imran Ali
        '' Cost Sheet Batch Wise for City Bread
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty
            If data.Rows.Count > 0 Then

                ''Commented on 10-05-2017 while adding update section to save query
                'strSQL = "delete from tblcostsheet where masterarticleID = " & data.Rows(0).Item("MasterArticleID")
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                '' End
                '' Add New Column of ArticleSize in this insert query
                If Not objMArticle Is Nothing Then
                    strSQL = " If Not Exists(Select CostSheetID FROM tblCostSheet Where ArticleID = " & objMArticle.ArticleID & " And MasterArticleID= " & objMArticle.MasterArticleID & " ) insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent,Remarks,CostPrice, SubDepartmentID, Percentage_Con, Total_Qty, ParentId) " _
                     & " Values (" & objMArticle.ArticleID & " , " & objMArticle.Qty & " , " & objMArticle.MasterArticleID & ", N'" & objMArticle.ArticleSize.Replace("'", "''") & "',N'" & objMArticle.Category.Replace("'", "''") & "', " & objMArticle.Tax_Percent & ", '" & objMArticle.Remarks.Replace("'", "''") & "'," & objMArticle.CostPrice & ", " & objMArticle.SubDepartmentID & ", " & objMArticle.Percentage_Con & ", " & objMArticle.Total_Qty & ", " & objMArticle.ParentId & ") " _
                     & " Else Update tblCostSheet Set ArticleID = " & objMArticle.ArticleID & ", Qty = " & objMArticle.Qty & ", MasterArticleID = " & objMArticle.MasterArticleID & ", ArticleSize = N'" & objMArticle.ArticleSize.Replace("'", "''") & "', Category = N'" & objMArticle.Category.Replace("'", "''") & "', Tax_Percent = " & objMArticle.Tax_Percent & ", Remarks = '" & objMArticle.Remarks.ToString.Replace("'", "''") & "', CostPrice = " & objMArticle.CostPrice & ", SubDepartmentID = " & objMArticle.SubDepartmentID & ", Percentage_Con = " & objMArticle.Percentage_Con & ", Total_Qty = " & objMArticle.Total_Qty & ", ParentId = " & objMArticle.ParentId & " Where ArticleID = " & objMArticle.ArticleID & " And ParentId= " & objMArticle.ParentId & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                End If
                For Each dr As DataRow In data.Rows
                    ' Before ReqId-968
                    'strSQL = "insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize) " _
                    '& " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "')"
                    '' After 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
                    'TASKM176151 Before 
                    'strSQL = "insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent) " _
                    '& " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "',N'" & dr("Category").ToString.Replace("'", "''") & "', " & Val(dr("Tax").ToString) & ")"
                    'TASKM176151 Add Column Remarks 
                    'strSQL = "insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent,Remarks,CostPrice, SubDepartmentID) " _
                    '& " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "',N'" & dr("Category").ToString.Replace("'", "''") & "', " & Val(dr("Tax").ToString) & ", '" & dr("Remarks").ToString.Replace("'", "''") & "'," & Val(dr("Purchase Price").ToString) & ", " & Val(dr("SubDepartmentID").ToString) & ")"
                    'End TaskM176151

                    strSQL = " If Not Exists(Select CostSheetID FROM tblCostSheet Where ArticleID = " & dr(0) & " And ParentId= " & Val(dr("ParentId").ToString) & ") insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent,Remarks,CostPrice, SubDepartmentID, Percentage_Con, Total_Qty, ParentId, UniqueId, UniqueParentId) " _
                    & " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "',N'" & dr("Category").ToString.Replace("'", "''") & "', " & Val(dr("Tax").ToString) & ", '" & dr("Remarks").ToString.Replace("'", "''") & "'," & Val(dr("Purchase Price").ToString) & ", " & Val(dr("SubDepartmentID").ToString) & ", " & Val(dr("Percentage").ToString) & ", " & Val(dr("TotalQty").ToString) & ", " & Val(dr("ParentId").ToString) & ", " & Val(dr("UniqueId").ToString) & ", " & Val(dr("UniqueParentId").ToString) & ") " _
                    & " Else Update tblCostSheet Set ArticleID = " & dr(0) & ", Qty = " & dr("Qty") & ", MasterArticleID = " & data.Rows(0).Item("MasterArticleID") & ", ArticleSize = N'" & dr("ArticleSize").ToString.Replace("'", "''") & "', Category = N'" & dr("Category").ToString.Replace("'", "''") & "', Tax_Percent = " & Val(dr("Tax").ToString) & ", Remarks = '" & dr("Remarks").ToString.Replace("'", "''") & "', CostPrice = " & Val(dr("Purchase Price").ToString) & ", SubDepartmentID = " & Val(dr("SubDepartmentID").ToString) & ", Percentage_Con = " & Val(dr("Percentage").ToString) & ", Total_Qty = " & Val(dr("TotalQty").ToString) & ", ParentId = " & Val(dr("ParentId").ToString) & " Where ArticleID = " & dr(0) & " And ParentId= " & Val(dr("ParentId").ToString) & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                Next
            Else
                strSQL = "delete from tblcostsheet where masterarticleID = " & Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            End If
            trans.Commit()
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Sub
    Public Sub AddCostSheet(ByVal data As DataTable, Optional ByVal Id As Integer = 0, Optional ByVal objMArticle As MasterArticle = Nothing, Optional ByVal ProductionOverHeadsList As List(Of BEProductionOverHeads) = Nothing, Optional ByVal ByProductList As List(Of BEByProducts) = Nothing, Optional ByVal LabourAllocationList As List(Of BELabourAllocation) = Nothing)
        '' Request No 871
        '' 11-18-2013 by Imran Ali
        '' Cost Sheet Batch Wise for City Bread
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty
            If data.Rows.Count > 0 Then

                ''Commented on 10-05-2017 while adding update section to save query
                strSQL = "delete from tblcostsheet where masterarticleID = " & data.Rows(0).Item("MasterArticleID")
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                '' End
                '' Add New Column of ArticleSize in this insert query
                'If Not objMArticle Is Nothing Then
                '    strSQL = " If Not Exists(Select CostSheetID FROM tblCostSheet Where ArticleID = " & objMArticle.ArticleID & " And MasterArticleID= " & objMArticle.MasterArticleID & ") insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent,Remarks,CostPrice, SubDepartmentID, Percentage_Con, Total_Qty, ParentId) " _
                '     & " Values (" & objMArticle.ArticleID & " , " & objMArticle.Qty & " , " & objMArticle.MasterArticleID & ", N'" & objMArticle.ArticleSize.Replace("'", "''") & "',N'" & objMArticle.Category.Replace("'", "''") & "', " & objMArticle.Tax_Percent & ", '" & objMArticle.Remarks.Replace("'", "''") & "'," & objMArticle.CostPrice & ", " & objMArticle.SubDepartmentID & ", " & objMArticle.Percentage_Con & ", " & objMArticle.Total_Qty & ", " & objMArticle.ParentId & ") " _
                '     & " Update tblCostSheet Set ArticleID = " & objMArticle.ArticleID & ", Qty = " & objMArticle.Qty & ", MasterArticleID = " & objMArticle.MasterArticleID & ", ArticleSize = N'" & objMArticle.ArticleSize.Replace("'", "''") & "', Category = N'" & objMArticle.Category.Replace("'", "''") & "', Tax_Percent = " & objMArticle.Tax_Percent & ", Remarks = '" & objMArticle.Remarks.ToString.Replace("'", "''") & "', CostPrice = " & objMArticle.CostPrice & ", SubDepartmentID = " & objMArticle.SubDepartmentID & ", Percentage_Con = " & objMArticle.Percentage_Con & ", Total_Qty = " & objMArticle.Total_Qty & ", ParentId = " & objMArticle.ParentId & " Where ArticleID = " & objMArticle.ArticleID & " And ParentId= " & objMArticle.ParentId & " "
                '    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                'End If
                For Each dr As DataRow In data.Rows
                    ' Before ReqId-968
                    'strSQL = "insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize) " _
                    '& " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "')"
                    '' After 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
                    'TASKM176151 Before 
                    'strSQL = "insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent) " _
                    '& " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "',N'" & dr("Category").ToString.Replace("'", "''") & "', " & Val(dr("Tax").ToString) & ")"
                    'TASKM176151 Add Column Remarks 
                    'strSQL = "insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent,Remarks,CostPrice, SubDepartmentID) " _
                    '& " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "',N'" & dr("Category").ToString.Replace("'", "''") & "', " & Val(dr("Tax").ToString) & ", '" & dr("Remarks").ToString.Replace("'", "''") & "'," & Val(dr("Purchase Price").ToString) & ", " & Val(dr("SubDepartmentID").ToString) & ")"
                    'End TaskM176151
                    ''Commented on 01-06-2017
                    'strSQL = " If Not Exists(Select CostSheetID FROM tblCostSheet Where ArticleID = " & dr(0) & " And ParentId= " & Val(dr("ParentId").ToString) & ") insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent,Remarks,CostPrice, SubDepartmentID, Percentage_Con, Total_Qty, ParentId) " _
                    '& " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "',N'" & dr("Category").ToString.Replace("'", "''") & "', " & Val(dr("Tax").ToString) & ", '" & dr("Remarks").ToString.Replace("'", "''") & "'," & Val(dr("Purchase Price").ToString) & ", " & Val(dr("SubDepartmentID").ToString) & ", " & Val(dr("Percentage").ToString) & ", " & Val(dr("TotalQty").ToString) & ", " & Val(dr("ParentId").ToString) & ") " _
                    '& " Update tblCostSheet Set ArticleID = " & dr(0) & ", Qty = " & dr("Qty") & ", MasterArticleID = " & data.Rows(0).Item("MasterArticleID") & ", ArticleSize = N'" & dr("ArticleSize").ToString.Replace("'", "''") & "', Category = N'" & dr("Category").ToString.Replace("'", "''") & "', Tax_Percent = " & Val(dr("Tax").ToString) & ", Remarks = '" & dr("Remarks").ToString.Replace("'", "''") & "', CostPrice = " & Val(dr("Purchase Price").ToString) & ", SubDepartmentID = " & Val(dr("SubDepartmentID").ToString) & ", Percentage_Con = " & Val(dr("Percentage").ToString) & ", Total_Qty = " & Val(dr("TotalQty").ToString) & ", ParentId = " & Val(dr("ParentId").ToString) & " Where ArticleID = " & dr(0) & " And ParentId= " & Val(dr("ParentId").ToString) & " "
                    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

                    strSQL = " Insert into tblcostsheet(ArticleID , Qty, MasterArticleID, ArticleSize, Category,Tax_Percent,Remarks,CostPrice, SubDepartmentID, Percentage_Con, Total_Qty, ParentId) " _
                   & " Values (" & dr(0) & " , " & dr("Qty") & " , " & data.Rows(0).Item("MasterArticleID") & ", N'" & dr("ArticleSize").ToString.Replace("'", "''") & "',N'" & dr("Category").ToString.Replace("'", "''") & "', " & Val(dr("Tax").ToString) & ", '" & dr("Remarks").ToString.Replace("'", "''") & "'," & Val(dr("Purchase Price").ToString) & ", " & Val(dr("SubDepartmentID").ToString) & ", " & Val(dr("Percentage").ToString) & ", " & Val(dr("TotalQty").ToString) & ", " & Val(dr("ParentId").ToString) & ") "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                Next
            Else
                strSQL = "delete from tblcostsheet where masterarticleID = " & Id
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            End If
            If Not ProductionOverHeadsList Is Nothing Then
                AddProductionOverHeads(ProductionOverHeadsList, trans)
            End If
            ''
            If Not ByProductList Is Nothing Then
                AddByProducts(ByProductList, trans)
            End If
            ''
            If Not LabourAllocationList Is Nothing Then
                AddLabourAllocation(LabourAllocationList, trans)
            End If

            trans.Commit()
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Sub
    Public Function AddVendorsItem(ByVal ArticleDef As Article, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            Dim VendorsItemDt As List(Of ArticleDefVendorsItem) = ArticleDef.ArticleDefVendorsitem
            For Each ArtricleVendor As ArticleDefVendorsItem In VendorsItemDt
                str = "INSERT INTO ArticleDefVendors(ArticleDefId, VendorId, UserName, DateLog) Values (" & ArtricleVendor.ArticleDefId & ", " & ArtricleVendor.VendorId & ", N'" & ArtricleVendor.UserName & "', N'" & ArtricleVendor.DateLog & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddCustomerItem(ByVal ArticleDef As Article, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            Dim VendorsItemDt As List(Of ArticleDefCustomersItem) = ArticleDef.ArticleCustomerItem
            For Each ArtricleVendor As ArticleDefCustomersItem In VendorsItemDt
                str = "INSERT INTO ArticleDefCustomers(ArticleDefId, CustomerId, UserName, DateLog) Values (" & ArtricleVendor.ArticleDefId & ", " & ArtricleVendor.CustomerId & ", N'" & ArtricleVendor.UserName & "', N'" & ArtricleVendor.DateLog & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddOpeningStock(ByVal OpeningStock As OpeningStockMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            'Master Information Here .... 
            Dim str As String = String.Empty
            str = "INSERT INTO ReceivingMasterTable(ReceivingNo, ReceivingDate, VendorId, UserName, ReceivingAmount, ReceivingQty, PurchaseOrderId) " _
            & " Values(N'" & OpeningStock.Document & "', N'" & OpeningStock.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & OpeningStock.Supplier & ", N'" & OpeningStock.UserName & "', " & OpeningStock.NetAmount & ", " & OpeningStock.TotalQty & ", 0) Select @@Identity"
            OpeningStock.ReceivingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            'Stock Master Information Here ...
            str = String.Empty
            str = "INSERT INTO StockMasterTable(DocNo,DocDate,DocType,Remarks) " _
            & " Values(N'" & OpeningStock.Document & "', N'" & OpeningStock.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & OpeningStock.DocTypeId & "', Null) Select @@Identity"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Detail Information Here .... 
            Dim OpeningStockList As List(Of OpeningStockDetail) = OpeningStock.OpeningStockDetail
            For Each OpeningStock_List As OpeningStockDetail In OpeningStockList
                str = String.Empty
                str = "INSERT INTO ReceivingDetailTable(ReceivingId, LocationId, ArticleDefId, ArticleSize, Sz1,Sz7, Qty, ReceivedQty, Price,CurrentPrice) " _
                & " Values(" & OpeningStock.ReceivingId & ", " & OpeningStock_List.LocationId & ", " & OpeningStock_List.ArticleId & ",  N'" & OpeningStock_List.ArticleSize & "', " & OpeningStock_List.Qty & ", " & OpeningStock_List.PackQty & "," & OpeningStock_List.Qty & ", " & OpeningStock_List.Qty & ", " & OpeningStock_List.Price & ", " & OpeningStock_List.Price & ") "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                'Stock Detail Information Here ...
                str = String.Empty
                str = "INSERT INTO StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount) " _
                & " Values(Ident_Current('StockMasterTable'), " & OpeningStock_List.LocationId & ", " & OpeningStock_List.ArticleId & ", " & OpeningStock_List.Qty & ", 0, " & OpeningStock_List.Price & ", " & (Val(OpeningStock_List.Qty) * Val(OpeningStock_List.Price)) & ", 0)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Next

            'Voucher Master Information Here ...
            str = String.Empty
            str = "INSERT INTO tblVoucher(location_id, voucher_code, voucher_type_id, voucher_no, voucher_date, UserName, Source) " _
            & " Values(0, N'" & OpeningStock.Document & "', " & OpeningStock.DocTypeId & ", N'" & OpeningStock.Document & "', N'" & OpeningStock.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & OpeningStock.UserName & "', 'frmDefArticle') "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Debit Amount Voucher Detail Information Here ... 
            str = String.Empty
            str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments, debit_amount, credit_amount)" _
            & " Values(Ident_Current('tblVoucher'), 0, " & OpeningStock.StockAccountId & ", 'Opening Stock As " & OpeningStock.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & OpeningStock.NetAmount & ", 0)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Credit Amount Voucher Detail Information Here ... 
            str = String.Empty
            str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,comments, debit_amount, credit_amount)" _
            & " Values(Ident_Current('tblVoucher'), 0, " & OpeningStock.Supplier & ", 'Opening Stock As " & OpeningStock.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', 0, " & OpeningStock.NetAmount & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    'Public Shared Function UpdatePicture(ByVal ArticleId As Integer, ByVal strPath As String) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try
    '        Dim str As String = String.Empty
    '        str = "Update ArticleDefTableMaster Set ArticlePicture=N'" & strPath & "' WHERE ArticleId=" & ArticleId
    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '        trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
    Public Function GetAllCategorys() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select Distinct Category from tblCostSheet Where Category <> ''"
            Dim dt As DataTable
            dt = UtilityDAL.GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''

    Public Shared Function GetArticleCode(ByVal Prefix As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try


            Dim strSQL As String = String.Empty
            Dim Serial As Integer = 0I
            Dim SerialNo As String = String.Empty

            Dim intLength As Integer = Val(UtilityDAL.GetConfigValue("ArticleCodePrefixLength").ToString())
            If intLength = 0 Then
                intLength = 3
            End If

            strSQL = "SELECT IsNull(MAX(Right(ArticleCode," & intLength & ")),0)+1 As ArticleNo From ArticleDefTableMaster WHERE IsNull(AutoCode,0)=1  AND Left(ArticleCode," & Prefix.Length & ")=N'" & Prefix.Replace("'", "''") & "' AND Len(SubString(ArticleCode," & (Prefix.Length + 1) & "," & intLength & ")) >= " & intLength & ""
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL, trans)
            dt.AcceptChanges()

            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    Serial = Val(dt.Rows(0).Item(0).ToString)
                End If
            End If

            Dim strSerialCode As String = String.Empty
            For i As Integer = 1 To intLength
                If strSerialCode.ToString.Length > 0 Then
                    strSerialCode += "0"
                Else
                    strSerialCode = "0"
                End If
            Next
            SerialNo = Prefix & Right(strSerialCode.ToString + CStr(Serial), intLength)
            Return SerialNo
        Catch ex As Exception
            If trans IsNot Nothing Then trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function IsAlreadyAliasExists(ByVal ArticleDescription As String) As Boolean
        Try
            Dim str As String = "Select ArticleId, ArticleCode, ArticleDescription From ArticleDefTableMaster WHERE ArticleDescription='" & ArticleDescription.Replace("'", "''") & "'"
            Dim dtItemCode As DataTable = UtilityDAL.GetDataTable(str)
            If dtItemCode.Rows.Count > 0 Then
                'ShowErrorMessage("Item Code Already Exist " & vbCrLf & dtItemCode.Rows(0).Item(1).ToString + "-" + dtItemCode.Rows(0).Item(2).ToString)
                'Me.uitxtItemCode.Focus()
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCreatedAlias(ByVal ArticleDescription As String) As DataTable
        Try
            Dim str As String = "Select ArticleId, ArticleCode, ArticleDescription From ArticleDefTable WHERE ArticleDescription='" & ArticleDescription.Replace("'", "''") & "'"
            Dim dtItemCode As DataTable = UtilityDAL.GetDataTable(str)
            dtItemCode.AcceptChanges()
            Return dtItemCode
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, objPath As String, ByVal arrFile As List(Of String), ByVal Date1 As DateTime, ByVal objTrans As SqlTransaction) As Boolean
        Dim cmd As New SqlCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            'Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            If Not arrFile Is Nothing AndAlso arrFile.Count > 0 Then ''TFS1772
                'Altered Against Task#2015060001 Ali Ansari
                'Marked Against Task#2015060001 Ali Ansari
                '            If arrFile.Length > 0 Then
                'Marked Against Task#2015060001 Ali Ansari
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        'Dim New_Files As String = intId & "_" & DocId & "_SI" & CompanyId & "_" & Date1.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim New_Files As String = intId & "_" & DocId & "_" & Date1.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)

                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteRelatedItem(ByVal RelationId As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM tblRelatedItem Where RelationId = " & RelationId & ""
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
    ''2103
    Public Function AddProductionOverHeads(ByVal ProductionOverHeadsList As List(Of BEProductionOverHeads), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            For Each POH As BEProductionOverHeads In ProductionOverHeadsList
                If POH.ProductionOverHeadsId = 0 Then
                    str = "INSERT INTO ProductionOverHeads(ArticleId, ProductionStepId, AccountId, Amount, Remarks) Values (" & POH.ArticleId & ", " & POH.ProductionStepId & ", " & POH.AccountId & ", " & POH.Amount & ", N'" & POH.Remarks.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update ProductionOverHeads Set ArticleId = " & POH.ArticleId & ", ProductionStepId = " & POH.ProductionStepId & ", AccountId = " & POH.AccountId & ", Amount = " & POH.Amount & ", Remarks = N'" & POH.Remarks.Replace("'", "''") & "' WHERE ProductionOverHeadsId = " & POH.ProductionOverHeadsId & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetProductionOverHeads(ByVal ArticleId As Integer) As DataTable
        Try
            Dim str As String = " SELECT Heads.ProductionOverHeadsId, Heads.ArticleId, Heads.ProductionStepId, ProductionStep.prod_step AS ProductionStep, Heads.AccountId, COA.detail_title As Account, Heads.Amount, Heads.Remarks FROM ProductionOverHeads AS Heads LEFT OUTER JOIN  tblproSteps AS ProductionStep ON  Heads.ProductionStepId = ProductionStep.ProdStep_Id  " _
                              & " LEFT OUTER JOIN vwCOADetail AS COA ON Heads.AccountId = COA.coa_detail_id WHERE Heads.ArticleId = " & ArticleId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteProductionOverHeads(ByVal ProductionOverHeadsId As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM ProductionOverHeads Where ProductionOverHeadsId = " & ProductionOverHeadsId & ""
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
    Public Function AddByProducts(ByVal ByProductList As List(Of BEByProducts), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            For Each ByProduct As BEByProducts In ByProductList
                If ByProduct.ByProductsId = 0 Then
                    str = "INSERT INTO ByProducts(ArticleId, MasterArticleId, Rate, Qty, Remarks) Values (" & ByProduct.ArticleId & ", " & ByProduct.MasterArticleId & ", " & ByProduct.Rate & ", " & ByProduct.Qty & ", N'" & ByProduct.Remarks.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update ByProducts Set ArticleId = " & ByProduct.ArticleId & ", MasterArticleId = " & ByProduct.MasterArticleId & ", Rate = " & ByProduct.Rate & ", Qty = " & ByProduct.Qty & ", Remarks = N'" & ByProduct.Remarks.Replace("'", "''") & "' WHERE ByProductsId = " & ByProduct.ByProductsId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetByProducts(ByVal MasterArticleId As Integer) As DataTable
        Try
            Dim str As String = " SELECT ByProducts.ByProductsId, ByProducts.ArticleId, Article.ArticleDescription AS Product, ByProducts.MasterArticleId AS MasterArticleId, IsNull(ByProducts.Rate, 0) AS Rate, IsNull(ByProducts.Qty, 0) AS Qty, ByProducts.Remarks FROM ByProducts LEFT OUTER JOIN  ArticleDefTable AS Article ON  ByProducts.ArticleId = Article.ArticleId  " _
                              & " WHERE ByProducts.MasterArticleId = " & MasterArticleId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteByProduct(ByVal ByProductId As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM ByProducts Where ByProductsId = " & ByProductId & ""
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
    Public Function AddLabourAllocation(ByVal LabourAllocationList As List(Of BELabourAllocation), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            For Each LabourAllocation As BELabourAllocation In LabourAllocationList
                If LabourAllocation.LabourAllocationId = 0 Then
                    str = "INSERT INTO LabourAllocation(ProductionStepId, LabourTypeId, RatePerUnit, ArticleId) Values (" & LabourAllocation.ProductionStepId & ", " & LabourAllocation.LabourTypeId & ", " & LabourAllocation.RatePerUnit & ", " & LabourAllocation.ArticleId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update LabourAllocation Set ProductionStepId = " & LabourAllocation.ProductionStepId & ", LabourTypeId = " & LabourAllocation.LabourTypeId & ", RatePerUnit = " & LabourAllocation.RatePerUnit & "  WHERE LabourAllocationId = " & LabourAllocation.LabourAllocationId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLabourAllocations(ByVal MasterArticleId As Integer) As DataTable
        Try
            Dim str As String = " SELECT LabourAllocation.LabourAllocationId, LabourAllocation.ArticleId, LabourAllocation.ProductionStepId, ProductionStep.Prod_Step AS ProductionStep, LabourAllocation.LabourTypeId, LabourType.LabourType, ChargeType.Charge AS ChargeType, LabourAllocation.RatePerUnit FROM LabourAllocation LEFT OUTER JOIN  tblproSteps AS ProductionStep ON  LabourAllocation.ProductionStepId = ProductionStep.ProdStep_Id  " _
                              & " LEFT OUTER JOIN tblLabourType AS LabourType ON LabourAllocation.LabourTypeId = LabourType.Id LEFT OUTER JOIN ChargeType ON LabourType.ChargeTypeId = ChargeType.Id  WHERE LabourAllocation.ArticleId = " & MasterArticleId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function DeleteLabourAllocation(ByVal LabourAllocationId As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM LabourAllocation Where LabourAllocationId = " & LabourAllocationId & ""
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

    'Task 3420 Saad Afzaal Create new FinishGoodLabourAllocation Delete function which delete from Finish Good Labour Allocation table with specific Id 

    Public Shared Function DeleteFinishGoodLabourAllocation(ByVal Id As Integer) As Boolean
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

End Class
