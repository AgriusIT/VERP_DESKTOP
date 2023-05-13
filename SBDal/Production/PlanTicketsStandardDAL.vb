Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class PlanTicketsStandardDAL

    Public Shared Function costcenterSave(ByVal TicketNo As String) As Integer
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblDefCostCenter(Name,Code,sortorder, CostCenterGroup, Active, OutwardGatepass, DayShift, LCDocId) values(N'" & TicketNo.Replace("'", "''") & "', '" & TicketNo.Replace("'", "''") & "', '" & 1 & "', N'" & " " & "', " & 1 & ", " & 0 & ", " & 0 & " , " & 0 & ") Select @@Identity "

            Dim CostCenterId As Integer

            CostCenterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            'Trans Commit here... 
            trans.Commit()
            Return CostCenterId
        Catch ex As Exception
            trans.Rollback()
            Return 0
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function


    Public Shared Function Save(ByVal PlanTicketsMaster As PlanTicketsMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim CostCenterId As Integer

            CostCenterId = costcenterSave(PlanTicketsMaster.TicketNo)

            If CostCenterId > 0 Then

                Dim str As String = String.Empty
                str = "Insert Into PlanTicketsMaster(TicketNo, TicketDate, CustomerID, SalesOrderID, PlanID, CostCenterId , SpecialInstructions, ExpiryDate, BatchNo, MasterArticleId, BatchSize, FinishGoodId, NoOfBatches, ParentTicketNo) " _
                & " Values(N'" & PlanTicketsMaster.TicketNo & "', N'" & PlanTicketsMaster.TicketDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & PlanTicketsMaster.CustomerID & ", " & PlanTicketsMaster.SalesOrderID & ", " & PlanTicketsMaster.PlanID & " , " & CostCenterId & " , N'" & PlanTicketsMaster.SpecialInstructions.Replace("'", "''") & "', N'" & PlanTicketsMaster.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & PlanTicketsMaster.BatchNo.Replace("'", "''") & "', " & PlanTicketsMaster.MasterArticleId & ", " & PlanTicketsMaster.BatchSize & ", " & PlanTicketsMaster.FinishGoodId & ", " & PlanTicketsMaster.NoOfBatches & ", N'" & PlanTicketsMaster.ParentTicketNo & "') Select @@Identity "

                PlanTicketsMaster.PlanTicketsMasterID = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                'Insert Stock Detail Information 
                SaveDetail(PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.Detail, trans, PlanTicketsMaster.BatchSize)
                AddStages(PlanTicketsMaster, trans)
                AddMaterialDetail(PlanTicketsMaster, trans)
                'Trans Commit here... 
                trans.Commit()
                Return True

            Else

                Return False

            End If

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Shared Function Update(ByVal PlanTicketsMaster As PlanTicketsMaster)
        'Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'PlanTicketsMasterID()
            'TicketNo()
            'TicketDate()
            'CustomerID()
            'SalesOrderID()
            'PlanID()
            'SpecialInstructions()
            Dim str As String = String.Empty
            str = "Update PlanTicketsMaster Set TicketNo = N'" & PlanTicketsMaster.TicketNo & "', TicketDate = N'" & PlanTicketsMaster.TicketDate.ToString("yyyy-M-d h:mm:ss tt") & "' , CustomerID = " & PlanTicketsMaster.CustomerID & ", SalesOrderID = " & PlanTicketsMaster.SalesOrderID & ", PlanID = " & PlanTicketsMaster.PlanID & ", SpecialInstructions = N'" & PlanTicketsMaster.SpecialInstructions.Replace("'", "''") & "', ExpiryDate = N'" & PlanTicketsMaster.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "', BatchNo = N'" & PlanTicketsMaster.BatchNo.Replace("'", "''") & "', MasterArticleId = " & PlanTicketsMaster.MasterArticleId & ", BatchSize = " & PlanTicketsMaster.BatchSize & " , FinishGoodId = " & PlanTicketsMaster.FinishGoodId & ", NoOfBatches = " & PlanTicketsMaster.NoOfBatches & ", ParentTicketNo = N'" & PlanTicketsMaster.ParentTicketNo & "'  Where PlanTicketsMasterID = " & PlanTicketsMaster.PlanTicketsMasterID & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            For Each Detail As PlanTicketsDetail In PlanTicketsMaster.Detail
                str = ""
                str = "If Exists(Select IsNull(PlanTicketsDetailID, 0) As PlanTicketsDetailID From PlanTicketsDetail Where PlanTicketsDetailID = " & Detail.PlanTicketsDetailID & ") UPDATE PlanDetailTable SET TicketIssuedQty = IsNull(TicketIssuedQty, 0) - " & Detail.Quantity & " Where PlanDetailId = " & Detail.PlanDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            str = String.Empty
            str = "Delete From PlanTicketsDetail Where PlanTicketsMasterID = " & PlanTicketsMaster.PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            SaveDetail(PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.Detail, trans, PlanTicketsMaster.BatchSize)
            AddStages(PlanTicketsMaster, trans)
            AddMaterialDetail(PlanTicketsMaster, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Shared Function Delete(ByVal PlanTicketsMasterID As Integer) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete From PlanTicketsMaster Where PlanTicketsMasterID = " & PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'str = "Delete From ProductionTicketStages Where TicketId = " & PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            DeleteDetail(PlanTicketsMasterID, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Shared Function DeleteDetail(ByVal PlanTicketsMasterID As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim dt As New DataTable
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Try
            Dim str As String = String.Empty
            str = "Select PlanDetailId, IsNull(Quantity, 0) As Quantity, ArticleId From PlanTicketsDetail Where PlanTicketsMasterID = " & PlanTicketsMasterID & ""
            dt = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            For Each dr As DataRow In dt.Rows
                SubtractQty(Val(dr.Item(0).ToString), Val(dr.Item(1).ToString), Val(dr.Item(2).ToString))
            Next
            str = ""
            str = "Delete FROM PlanTicketsDetail Where PlanTicketsMasterID = " & PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = ""
            str = "Delete FROM ProductionTicketStages Where TicketId = " & PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = ""
            str = "Delete FROM PlanTicketMaterialDetail Where TicketId = " & PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw ex
            Return False
        Finally
            'Con.Close()
        End Try
    End Function
    Public Shared Function SaveDetail(ByVal PlanTecketsMasterID As Integer, ByVal PlanTicketsDetailList As List(Of PlanTicketsDetail), ByVal trans As SqlTransaction, ByVal BatchSize As Double) As Boolean
        Try
            Dim str As String = String.Empty
            Dim PlanDetailId As Integer = 0
            For Each PlanTicketsDetail As PlanTicketsDetail In PlanTicketsDetailList
                '//PlanTicketsDetail
                'PlanTicketsDetailID()
                'PlanTicketsMasterID()
                'ArticleId()
                'PlanDetailId()
                'Quantity()
                str = "Insert Into PlanTicketsDetail(PlanTicketsMasterID, ArticleId, PlanDetailId, Quantity, PackingId, LocationId) " _
              & " Values (" & PlanTecketsMasterID & ", " & PlanTicketsDetail.ArticleId & ",  " & PlanTicketsDetail.PlanDetailId & ", " & PlanTicketsDetail.Quantity & ", " & PlanTicketsDetail.PackingId & ", " & PlanTicketsDetail.LocationId & " )"
                'End Task:M16
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                PlanDetailId = PlanTicketsDetail.PlanDetailId
                'str = ""
                str = "UPDATE PlanDetailTable SET TicketIssuedQty = IsNull(TicketIssuedQty, 0) + " & PlanTicketsDetail.Quantity & " Where PlanDetailId = " & PlanTicketsDetail.PlanDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            'str = ""
            'str = "UPDATE PlanDetailTable SET TicketIssuedQty = IsNull(TicketIssuedQty, 0) + " & BatchSize & " Where PlanDetailId = " & PlanDetailId & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw ex
            Return False
        Finally
        End Try
    End Function
    Public Shared Function GetAll() As DataTable
        Dim dt As New DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT  PlanTicketsMaster.TicketNo, PlanTicketsMaster.TicketDate,  PlanMasterTable.PlanNo, PlanMasterTable.PlanDate, PlanTicketsMaster.PlanTicketsMasterID, SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, " _
            & " PlanTicketsMaster.SpecialInstructions AS Remarks, PlanTicketsMaster.CustomerID, PlanTicketsMaster.PlanID, PlanTicketsMaster.SalesOrderID, IsNull(PlanTicketsMaster.MasterArticleId, 0) AS MasterArticleId, Article.ArticleDescription AS Product, PlanTicketsMaster.ExpiryDate, PlanTicketsMaster.BatchNo,  PlanTicketsMaster.BatchSize, PlanTicketsMaster.FinishGoodId, PlanTicketsMaster.NoOfBatches, PlanTicketsMaster.ParentTicketNo " _
            & " FROM PlanTicketsMaster LEFT OUTER JOIN " _
            & " SalesOrderMasterTable ON PlanTicketsMaster.SalesOrderID = SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN " _
            & " PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanId LEFT OUTER JOIN ArticleDefTableMaster AS Article ON PlanTicketsMaster.MasterArticleId = Article.ArticleId" _
            & " ORDER BY PlanTicketsMaster.TicketDate DESC"
            dt = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDetail(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable
        Dim Query As String = String.Empty
        Try
            '//PlanTicketsDetail
            'PlanTicketsDetailID()
            'PlanTicketsMasterID()
            'ArticleId()
            'PlanDetailId()
            'Quantity()
            Query = "Select IsNull(PlanTicketsDetail.LocationId, 0) AS LocationId, PlanTicketsDetail.PlanTicketsDetailID, PlanTicketsDetail.PlanTicketsMasterID, PlanTicketsDetail.ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(PlanTicketsDetail.PlanDetailId, 0) As PlanDetailId, IsNull(PlanTicketsDetail.Quantity, 0) As Quantity, IsNull(PlanTicketsDetail.PackingId, 0) AS PackingId From PlanTicketsDetail INNER JOIN ArticleDefTable ON PlanTicketsDetail.ArticleId = ArticleDefTable.ArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Where PlanTicketsDetail.PlanTicketsMasterID = " & ID & " "


            dt = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function SubtractQty(ByVal PlainDetailId As Integer, ByVal Quantity As Double, ByVal ArticleId As Integer) As Boolean
        'Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)
        Dim Str As String = String.Empty
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Str = ""
            Str = "UPDATE PlanDetailTable SET TicketIssuedQty = IsNull(TicketIssuedQty, 0) - " & Quantity & " Where PlanDetailId = " & PlainDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Sub DeleteDetailRow(ByVal PlanTicketsDetailId As Integer)
        'Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)
        Dim Str As String = String.Empty
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Str = ""
            Str = " Delete From PlanTicketsDetail Where PlanTicketsDetailId = " & PlanTicketsDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub

    Public Shared Function GetStages(ByVal TicketId As Integer) As DataTable
        Dim Str As String = String.Empty
        Try
            '[Id] [int] IDENTITY(1,1) NOT NULL,
            '[TicketId] [int] NULL,
            '[ProductionStageId] [int] NULL,
            '[StageDate] [datetime] NULL,
            '[Section] [nvarchar](100) NULL,
            '[ProductionInchargeId] [int] NULL,
            '[QCInchargeId] [int] NULL,
            Str = "SELECT ProductionTicketStages.Id, ProductionTicketStages.TicketId, ProductionTicketStages.ProductionStageId, ProductionStep.prod_step AS ProductionStage, ProductionTicketStages.StageDate AS StageDate, ProductionTicketStages.Section, IsNull(ProductionTicketStages.ProductionInchargeId, 0) AS ProductionInchargeId, " _
                 & " ProductionIncharge.Employee_Name ProductionIncharge, IsNull(ProductionTicketStages.QCInchargeId, 0) As QCInchargeId, QCIncharge.Employee_Name AS QCIncharge , IsNull(ProductionTicketStages.QAInchargeId, 0) AS QAInchargeId, QAIncharge.Employee_Name AS QAIncharge FROM ProductionTicketStages " _
                 & " LEFT OUTER JOIN tblDefEmployee AS ProductionIncharge ON ProductionTicketStages.ProductionInchargeId =ProductionIncharge.Employee_Id " _
                 & " LEFT OUTER JOIN tblDefEmployee AS QCIncharge ON ProductionTicketStages.QCInchargeId = QCIncharge.Employee_Id " _
                 & " LEFT OUTER JOIN tblDefEmployee AS QAIncharge ON ProductionTicketStages.QAInchargeId = QAIncharge.Employee_Id " _
                 & " LEFT OUTER JOIN tblproSteps AS ProductionStep ON ProductionTicketStages.ProductionStageId = ProductionStep.ProdStep_Id " _
                 & " WHERE ProductionTicketStages.TicketId = " & TicketId & ""
            Return UtilityDAL.GetDataTable(Str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function AddStages(ByVal Obj As PlanTicketsMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty

            For Each Detail As BEProductionTicketStages In Obj.StagesList
                '[Id] [int] IDENTITY(1,1) NOT NULL,
                '[TicketId] [int] NULL,
                '[ProductionStageId] [int] NULL,
                '[StageDate] [datetime] NULL,
                '[Section] [nvarchar](100) NULL,
                '[ProductionInchargeId] [int] NULL,
                '[QCInchargeId] [int] NULL,
                If Detail.Id = 0 Then
                    str = "INSERT INTO ProductionTicketStages(TicketId, ProductionStageId, StageDate, Section, ProductionInchargeId, QCInchargeId, QAInchargeId) Values (" & Obj.PlanTicketsMasterID & ", " & Detail.ProductionStageId & ",  N'" & Detail.StageDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Detail.Section.Replace("'", "''") & "', " & Detail.ProductionInchargeId & ", " & Detail.QCInchargeId & " , " & Detail.QAInchargeId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update ProductionTicketStages Set TicketId = " & Obj.PlanTicketsMasterID & ", ProductionStageId = " & Detail.ProductionStageId & ", StageDate = N'" & Detail.StageDate.ToString("yyyy-M-d h:mm:ss tt") & "', Section = N'" & Detail.Section.Replace("'", "''") & "', ProductionInchargeId = " & Detail.ProductionInchargeId & ", QCInchargeId = " & Detail.QCInchargeId & " , QAInchargeId = " & Detail.QAInchargeId & " WHERE Id = " & Detail.Id & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Shared Function AddMaterialDetail(ByVal Obj As PlanTicketsMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty

            For Each Detail As BEPlanTicketMaterialDetail In Obj.MaterialDetail
                '   [Id] [int] IDENTITY(1,1) NOT NULL,
                '[TicketId] [int] NULL,
                '[MaterialArticleId] [int] NULL,
                '[Qty] [float] NULL,
                '[CostPrice] [float] NULL,
                '[DepartmentId] [int] NULL,
                If Detail.Id = 0 Then
                    str = "INSERT INTO PlanTicketMaterialDetail(TicketId, MaterialArticleId, Qty, CostPrice, DepartmentId, Type, FinishGoodDetailId) Values (" & Obj.PlanTicketsMasterID & ", " & Detail.MaterialArticleId & ",  " & Detail.Qty & ", " & Detail.CostPrice & ", " & Detail.DepartmentId & ", '" & Detail.Type.Replace("'", "''") & "', " & Detail.FinishGoodDetailId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update PlanTicketMaterialDetail Set TicketId = " & Obj.PlanTicketsMasterID & ", MaterialArticleId = " & Detail.MaterialArticleId & ", Qty = " & Detail.Qty & ", CostPrice = " & Detail.CostPrice & ", DepartmentId = " & Detail.DepartmentId & ", Type = '" & Detail.Type.Replace("'", "''") & "', FinishGoodDetailId= " & Detail.FinishGoodDetailId & " WHERE Id = " & Detail.Id & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Shared Function DeleteStage(ByVal Id As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM ProductionTicketStages Where Id = " & Id & ""
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
    Public Shared Function DeleteMaterialDetail(ByVal Id As Integer) As Boolean
        Try
            Dim con As New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim trans As SqlTransaction = con.BeginTransaction
            Try
                ''delete from detail table
                Dim strSQL1 As String = "Delete FROM PlanTicketMaterialDetail Where Id = " & Id & ""
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
    Public Shared Function GetMaterialDetail(ByVal ProductId As Integer) As DataTable
        Dim Str As String = String.Empty
        Dim dt As DataTable
        Try

            '           [Id] [int] IDENTITY(1,1) NOT NULL,
            '[TicketId] [int] NULL,
            '[MaterialArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[CostPrice] [float] NULL,
            '[DepartmentId] [int] NULL,

            Str = " SELECT  0 AS Id, IsNull(Detail.SubDepartmentId, 0) As DepartmentId, tblProSteps.prod_step As Department, 0 AS TicketId, IsNull(Detail.MaterialArticleId, 0) As MaterialArticleId, ArticleDefTable.ArticleCode AS ArticleCode, ArticleDefTable.ArticleDescription AS Material, Detail.Qty AS Qty, Convert(float, 0) AS RequiredQty, Detail.CostPrice, Convert(float, 0) AS Amount, '' AS Type, 0 AS NoOfBatches " _
                        & " FROM FinishGoodDetail AS Detail INNER JOIN FinishGoodMaster AS FinishGood ON Detail.FinishGoodId = FinishGood.Id INNER JOIN  " _
                        & " ArticleDefTable ON Detail.MaterialArticleId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
                        & "  tblProSteps ON Detail.SubDepartmentId = tblProSteps.ProdStep_Id " _
                        & " WHERE FinishGood.MasterArticleId  = " & ProductId & " AND FinishGood.Default1 = 1 And IsNull(Detail.DetailArticleId, 0) = 0 Order By Detail.Id "
            dt = UtilityDAL.GetDataTable(Str)
            dt.AcceptChanges()
            dt.Columns("RequiredQty").Expression = "(IsNull(Qty, 0)*IsNull(NoOfBatches, 0))"
            dt.Columns("Amount").Expression = "(IsNull(CostPrice, 0)*IsNull(RequiredQty, 0))"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS3540
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <param name="VersionId"></param>
    ''' <param name="Batches"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMaterialDetail(ByVal ProductId As Integer, ByVal VersionId As Integer, ByVal BatchSize As Double) As DataTable
        Dim Str As String = String.Empty
        Dim dt As DataTable
        Try
            '           [Id] [int] IDENTITY(1,1) NOT NULL,
            '[TicketId] [int] NULL,
            '[MaterialArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[CostPrice] [float] NULL,
            '[DepartmentId] [int] NULL,
            ''TASK TFS3540 Record should be gotten according to ProductId and Version from Finish Good. Worked on 14-06-2018
            Str = " SELECT  0 AS Id, IsNull(Detail.SubDepartmentId, 0) As DepartmentId, tblProSteps.prod_step As Department, 0 AS TicketId, IsNull(Detail.MaterialArticleId, 0) As MaterialArticleId, ArticleDefTable.ArticleCode AS ArticleCode, ArticleDefTable.ArticleDescription AS Material, Detail.TotalQty * " & BatchSize & " AS Qty, Convert(float, 0) AS RequiredQty, Detail.CostPrice, Convert(float, 0) AS Amount, '' AS Type, 0 AS NoOfBatches, IsNull(Detail.DetailArticleId, 0) AS DetailArticleId, IsNull(Detail.PackingId, 0) AS PackingId, IsNull(Detail.Id, 0) AS FinishGoodDetailId " _
                        & " FROM FinishGoodDetail AS Detail INNER JOIN FinishGoodMaster AS FinishGood ON Detail.FinishGoodId = FinishGood.Id INNER JOIN  " _
                        & " ArticleDefTable ON Detail.MaterialArticleId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
                        & "  tblProSteps ON Detail.SubDepartmentId = tblProSteps.ProdStep_Id " _
                        & " WHERE FinishGood.MasterArticleId  = " & ProductId & " AND FinishGood.Version = " & VersionId & " And IsNull(Detail.DetailArticleId, 0) = 0 Order By Detail.Id "
            dt = UtilityDAL.GetDataTable(Str)
            dt.AcceptChanges()
            dt.Columns("RequiredQty").Expression = "(IsNull(Qty, 0)*IsNull(NoOfBatches, 0))"
            dt.Columns("Amount").Expression = "(IsNull(CostPrice, 0)*IsNull(RequiredQty, 0))"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Shared Function DisplayMaterialDetail(ByVal TicketId As Integer) As DataTable
        Dim Str As String = String.Empty
        Dim dt As DataTable
        Try

            '           [Id] [int] IDENTITY(1,1) NOT NULL,
            '[TicketId] [int] NULL,
            '[MaterialArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[CostPrice] [float] NULL,
            '[DepartmentId] [int] NULL,
            ''Edit Against TFS2977 : Ayesha Rehman : 06-04-2018
            Str = " SELECT  Detail.Id, IsNull(Detail.DepartmentId, 0) As DepartmentId, tblProSteps.prod_step As Department, Detail.TicketId, IsNull(Detail.MaterialArticleId, 0) As MaterialArticleId, ArticleDefTable.ArticleCode AS ArticleCode, ArticleDefTable.ArticleDescription AS Material, Detail.Qty,  Convert(float, IsNull(Detail.Qty, 0)*IsNull(Ticket.NoOfBatches, 0)) AS RequiredQty, ArticleDefTable.Cost_Price As CostPrice, Convert(float, 0) AS Amount, Detail.Type, IsNull(Ticket.NoOfBatches, 0) AS NoOfBatches, IsNull(FinishGoodDetail.DetailArticleId, 0) AS DetailArticleId, IsNull(FinishGoodDetail.PackingId, 0) AS PackingId, IsNull(Detail.FinishGoodDetailId, 0) AS FinishGoodDetailId " _
                        & " FROM PlanTicketMaterialDetail AS Detail INNER JOIN PlanTicketsMaster AS Ticket ON Detail.TicketId = Ticket.PlanTicketsMasterID INNER JOIN  " _
                        & " ArticleDefTable ON Detail.MaterialArticleId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
                        & "  tblProSteps ON Detail.DepartmentId = tblProSteps.ProdStep_Id LEFT OUTER JOIN FinishGoodDetail ON Detail.FinishGoodDetailId = FinishGoodDetail.Id" _
                        & " WHERE Ticket.PlanTicketsMasterID  = " & TicketId & " "
            dt = UtilityDAL.GetDataTable(Str)
            dt.AcceptChanges()
            dt.Columns("RequiredQty").Expression = "(IsNull(Qty, 0)*IsNull(NoOfBatches, 0))"
            dt.Columns("Amount").Expression = "(IsNull(CostPrice, 0)*IsNull(RequiredQty, 0))"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetTicketRecordForStoreIssuance(ByVal TicketId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = " Select PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
              & " SUM(IsNull((PlanTicketMaterialDetail.Qty),0)) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color , PlanTicketMaterialDetail.CostPrice as Rate , sum(IsNull((PlanTicketMaterialDetail.Qty),0))*PlanTicketMaterialDetail.CostPrice as Total " _
              & " FROM PlanTicketMaterialDetail " _
              & " INNER JOIN PlanTicketsMaster ON " _
              & " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
              & " LEFT OUTRER JOIN ArticleDefTable ON " _
              & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
              & " LEFT JOIN tblproSteps ON " _
              & " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
              & " LEFT JOIN ArticleColorDefTable on " _
              & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
              & " WHERE TicketId = " & TicketId & " " _
              & "group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
              & "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , PlanTicketMaterialDetail.CostPrice"
            Return UtilityDAL.GetDataTable(Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetTicketRecordForStoreIssuance(ByVal TicketId As Integer, ByVal DepartmentId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = " Select PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
              & " SUM(IsNull((PlanTicketMaterialDetail.Qty),0)) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color , PlanTicketMaterialDetail.CostPrice as Rate , sum(IsNull((PlanTicketMaterialDetail.Qty),0))*PlanTicketMaterialDetail.CostPrice as Total " _
              & " FROM PlanTicketMaterialDetail " _
              & " INNER JOIN PlanTicketsMaster ON " _
              & " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
              & " LEFT OUTRER JOIN ArticleDefTable ON " _
              & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
              & " LEFT JOIN tblproSteps ON " _
              & " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
              & " LEFT JOIN ArticleColorDefTable on " _
              & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
              & " WHERE PlanTicketsMaster.TicketId = " & TicketId & " And PlanTicketMaterialDetail.DepartmentId = " & DepartmentId & "" _
              & "group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
              & "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , PlanTicketMaterialDetail.CostPrice"
            Return UtilityDAL.GetDataTable(Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetTicketsForPlan(ByVal PlanId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT PlanTicketsMasterID AS TicketId FROM PlanTicketsMaster WHERE PlanID = " & PlanId & ""
            Return UtilityDAL.GetDataTable(Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetTicketsForSalesOrder(ByVal SalesOrderId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT PlanTicketsMasterID AS TicketId FROM PlanTicketsMaster WHERE SalesOrderId = " & SalesOrderId & ""
            Return UtilityDAL.GetDataTable(Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetTicketRecordForStoreIssuance(ByVal SalesOrderId As Integer, ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal DepartmentId As Integer, ByVal bit As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            If bit = 1 Then
                Query = " Select PlanTicketsMaster.TicketNo, ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId, ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
   & " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END - IsNull(Tracking.DispatchedQty, 0)) as PendingQty , ArticleDefTable.ArticleCode as Code, ArticleColorDefTable.ArticleColorName as Color ,  Case When IsNull(ArticleDefTable.Cost_Price,0) > 0 Then IsNull(ArticleDefTable.Cost_Price,0) Else IsNull(LastPurchasePrice.Price, 0) END as Rate, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END-IsNull(Tracking.DispatchedQty, 0)) * Case When IsNull(ArticleDefTable.Cost_Price,0) > 0 Then IsNull(ArticleDefTable.Cost_Price,0) Else IsNull(LastPurchasePrice.Price, 0) END as Total, " _
   & " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END - IsNull(Tracking.DispatchedQty, 0)) as CheckQty, SUM(Convert(Float, IsNull(Tracking.DispatchedQty, 0))) as IssuedQty, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END) AS EstimatedQty, Isnull(ArticleGroup.SubSubId,0) as PurchaseAccountId, IsNull(ArticleGroup.CGSAccountId, 0) AS CGSAccountId, IsNull(Process.WIPAccountId, 0) AS WIPAccountId, PlanTicketsMaster.ParentTicketNo, ArticleDefTableMaster.ArticleDescription AS FinishGood, Sum(IsNull(Plan1.Qty, 0)) AS PlanQty, Sum(IsNull(PlanTicketsMaster.BatchSize, 0) * IsNull(PlanTicketsMaster.NoOfBatches, 0)) AS TicketQty, IsNull(PlanTicketsMaster.PlanID, 0) AS PlanId, IsNull(PlanTicketsMaster.MasterArticleId, 0) AS MasterArticleId  FROM PlanTicketMaterialDetail " _
   & " INNER JOIN PlanTicketsMaster ON " _
   & " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
   & " INNER JOIN ArticleDefTable ON " _
   & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
   & " LEFT OUTER JOIN tblTrackEstimation As Tracking ON PlanTicketMaterialDetail.TicketId = Tracking.TicketId AND PlanTicketMaterialDetail.MaterialArticleId = Tracking.ArticleId AND PlanTicketMaterialDetail.DepartmentId = Tracking.DepartmentId " _
   & " LEFT JOIN tblproSteps ON " _
   & " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
   & " LEFT JOIN ArticleColorDefTable on " _
   & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
   & " LEFT OUTER JOIN dbo.ArticleGroupDefTable ArticleGroup ON ArticleDefTable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
   & " LEFT OUTER JOIN (SELECT Article.ArticleId, IsNull(ProductionProcess.WIPAccountId, 0) AS WIPAccountId FROM ArticleDefTableMaster AS Article LEFT OUTER JOIN ProductionProcess ON Article.ProductionProcessId = ProductionProcess.ProductionProcessId) AS Process ON  PlanTicketsMaster.MasterArticleId = Process.ArticleId " _
   & " LEFT OUTER JOIN (SELECT Max(ReceivingDetailId) AS ReceivingDetailId, ReceivingDetailTable.ArticleDefId, MAX(ISNULL(ReceivingDetailTable.Price, 0)) AS Price FROM ReceivingDetailTable INNER JOIN ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId  WHERE LEFT(ReceivingMasterTable.ReceivingNo, 3) = 'Pur' Group By ReceivingDetailTable.ArticleDefId) AS LastPurchasePrice ON ArticleDefTable.ArticleId = LastPurchasePrice.ArticleDefId " _
   & " LEFT OUTER JOIN ArticleDefTableMaster ON PlanTicketsMaster.MasterArticleId = ArticleDefTableMaster.ArticleId " _
   & " LEFT OUTER JOIN (SELECT PlanId, ArticleDefId, Sum(Qty) AS Qty  FROM PlanDetailTable Group By PlanId, ArticleDefId) AS Plan1 ON PlanTicketsMaster.PlanID = Plan1.PlanId AND PlanTicketsMaster.MasterArticleId = Plan1.ArticleDefId " _
   & " WHERE PlanTicketsMaster.TicketNo <> '' "






                '  Query = " Select PlanTicketsMaster.TicketNo, ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId, ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
                '& " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END - IsNull(Tracking.DispatchedQty, 0)) as PendingQty , ArticleDefTable.ArticleCode as Code, ArticleColorDefTable.ArticleColorName as Color , Case When IsNull(ArticleDefTable.Cost_Price,0) > 0 Then IsNull(ArticleDefTable.Cost_Price,0) Else isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefTable.ArticleId)),0) End as Rate , SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END-IsNull(Tracking.DispatchedQty, 0)) * (Case When IsNull(ArticleDefTable.Cost_Price,0) > 0 Then IsNull(ArticleDefTable.Cost_Price,0) Else isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefTable.ArticleId)),0) End) as Total, " _
                '& " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END - IsNull(Tracking.DispatchedQty, 0)) as CheckQty, SUM(Convert(Float, IsNull(Tracking.DispatchedQty, 0))) as IssuedQty, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END) AS EstimatedQty, Isnull(ArticleGroup.SubSubId,0) as PurchaseAccountId, IsNull(ArticleGroup.CGSAccountId, 0) AS CGSAccountId, IsNull(Process.WIPAccountId, 0) AS WIPAccountId, PlanTicketsMaster.ParentTicketNo FROM PlanTicketMaterialDetail " _
                '& " INNER JOIN PlanTicketsMaster ON " _
                '& " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                '& " INNER JOIN ArticleDefTable ON " _
                '& " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
                '& " LEFT OUTER JOIN tblTrackEstimation As Tracking ON PlanTicketMaterialDetail.TicketId = Tracking.TicketId AND PlanTicketMaterialDetail.MaterialArticleId = Tracking.ArticleId AND PlanTicketMaterialDetail.DepartmentId = Tracking.DepartmentId " _
                '& " LEFT JOIN tblproSteps ON " _
                '& " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
                '& " LEFT JOIN ArticleColorDefTable on " _
                '& " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '& " LEFT OUTER JOIN dbo.ArticleGroupDefTable ArticleGroup ON ArticleDefTable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
                '& " LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefTable.ArticleId " _
                '& " LEFT OUTER JOIN (SELECT Article.ArticleId, IsNull(ProductionProcess.WIPAccountId, 0) AS WIPAccountId FROM ArticleDefTableMaster AS Article LEFT OUTER JOIN ProductionProcess ON Article.ProductionProcessId = ProductionProcess.ProductionProcessId) AS Process ON  PlanTicketsMaster.MasterArticleId = Process.ArticleId " _
                '& " WHERE PlanTicketsMaster.TicketNo <> '' "

            Else
                Query = " Select PlanTicketsMaster.TicketNo, ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId, ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
             & " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END - IsNull(Tracking.DispatchedQty, 0)) as PendingQty  , ArticleDefTable.ArticleCode as Code, ArticleColorDefTable.ArticleColorName as Color, IsNull(LastPurchasePrice.Price, 0) AS Rate, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END-IsNull(Tracking.DispatchedQty, 0)) * IsNull(LastPurchasePrice.Price, 0) AS Total, " _
             & " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END - IsNull(Tracking.DispatchedQty, 0)) as CheckQty, SUM(Convert(Float, IsNull(Tracking.DispatchedQty, 0))) as IssuedQty, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END) AS EstimatedQty, Isnull(ArticleGroup.SubSubId,0) as PurchaseAccountId, IsNull(ArticleGroup.CGSAccountId, 0) AS CGSAccountId, IsNull(Process.WIPAccountId, 0) AS WIPAccountId, PlanTicketsMaster.ParentTicketNo, ArticleDefTableMaster.ArticleDescription AS FinishGood, IsNull(Plan1.Qty, 0) AS PlanQty, (IsNull(PlanTicketsMaster.BatchSize, 0) * IsNull(PlanTicketsMaster.NoOfBatches, 0)) AS TicketQty, IsNull(PlanTicketsMaster.PlanID, 0) AS PlanId, IsNull(PlanTicketsMaster.MasterArticleId, 0) AS MasterArticleId FROM PlanTicketMaterialDetail " _
             & " INNER JOIN PlanTicketsMaster ON " _
             & " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
             & " INNER JOIN ArticleDefTable ON " _
             & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
             & " LEFT OUTER JOIN tblTrackEstimation As Tracking ON PlanTicketMaterialDetail.TicketId = Tracking.TicketId AND PlanTicketMaterialDetail.MaterialArticleId = Tracking.ArticleId AND PlanTicketMaterialDetail.DepartmentId = Tracking.DepartmentId " _
             & " LEFT JOIN tblproSteps ON " _
             & " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
             & " LEFT JOIN ArticleColorDefTable on " _
             & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
             & " LEFT OUTER JOIN dbo.ArticleGroupDefTable ArticleGroup ON ArticleDefTable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
             & " LEFT OUTER JOIN (SELECT Article.ArticleId, IsNull(ProductionProcess.WIPAccountId, 0) AS WIPAccountId FROM ArticleDefTableMaster AS Article LEFT OUTER JOIN ProductionProcess ON Article.ProductionProcessId = ProductionProcess.ProductionProcessId) AS Process ON  PlanTicketsMaster.MasterArticleId = Process.ArticleId " _
             & " LEFT OUTER JOIN (SELECT Max(ReceivingDetailId) AS ReceivingDetailId, ReceivingDetailTable.ArticleDefId, MAX(ISNULL(ReceivingDetailTable.Price, 0)) AS Price FROM ReceivingDetailTable INNER JOIN ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId  WHERE LEFT(ReceivingMasterTable.ReceivingNo, 3) = 'Pur' Group By ReceivingDetailTable.ArticleDefId) AS LastPurchasePrice ON ArticleDefTable.ArticleId = LastPurchasePrice.ArticleDefId " _
             & " LEFT OUTER JOIN ArticleDefTableMaster ON PlanTicketsMaster.MasterArticleId = ArticleDefTableMaster.ArticleId " _
             & " LEFT OUTER JOIN (SELECT PlanId, ArticleDefId, Sum(Qty) AS Qty  FROM PlanDetailTable Group By PlanId, ArticleDefId) AS Plan1 ON PlanTicketsMaster.PlanID = Plan1.PlanId AND PlanTicketsMaster.MasterArticleId = Plan1.ArticleDefId " _
             & " WHERE PlanTicketsMaster.TicketNo <> '' "


                '  Query = " Select PlanTicketsMaster.TicketNo, ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId, ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
                '& " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END - IsNull(Tracking.DispatchedQty, 0)) as PendingQty , ArticleDefTable.ArticleCode as Code, ArticleColorDefTable.ArticleColorName as Color , isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefTable.ArticleId)),0) as Rate , SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END-IsNull(Tracking.DispatchedQty, 0))* isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefTable.ArticleId)),0) as Total, " _
                '& " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END - IsNull(Tracking.DispatchedQty, 0)) as CheckQty, SUM(Convert(Float, IsNull(Tracking.DispatchedQty, 0))) as IssuedQty, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END) AS EstimatedQty, Isnull(ArticleGroup.SubSubId,0) as PurchaseAccountId, IsNull(ArticleGroup.CGSAccountId, 0) AS CGSAccountId, IsNull(Process.WIPAccountId, 0) AS WIPAccountId, PlanTicketsMaster.ParentTicketNo FROM PlanTicketMaterialDetail " _
                '    & " INNER JOIN PlanTicketsMaster ON " _
                '    & " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                '    & " INNER JOIN ArticleDefTable ON " _
                '    & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
                '    & " LEFT OUTER JOIN tblTrackEstimation As Tracking ON PlanTicketMaterialDetail.TicketId = Tracking.TicketId AND PlanTicketMaterialDetail.MaterialArticleId = Tracking.ArticleId AND PlanTicketMaterialDetail.DepartmentId = Tracking.DepartmentId " _
                '    & " LEFT JOIN tblproSteps ON " _
                '    & " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
                '    & " LEFT JOIN ArticleColorDefTable on " _
                '    & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '    & " LEFT OUTER JOIN dbo.ArticleGroupDefTable ArticleGroup ON ArticleDefTable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
                '   & " LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefTable.ArticleId " _
                '    & " LEFT OUTER JOIN (SELECT Article.ArticleId, IsNull(ProductionProcess.WIPAccountId, 0) AS WIPAccountId FROM ArticleDefTableMaster AS Article LEFT OUTER JOIN ProductionProcess ON Article.ProductionProcessId = ProductionProcess.ProductionProcessId) AS Process ON  PlanTicketsMaster.MasterArticleId = Process.ArticleId " _
                '    & " WHERE PlanTicketsMaster.TicketNo <> '' "

            End If

            If DepartmentId > 0 Then
                Query += " AND PlanTicketMaterialDetail.DepartmentId = " & DepartmentId & " AND PlanTicketMaterialDetail.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId > 0 Then
                Query += " AND PlanTicketMaterialDetail.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId > 0 Then
                Query += " AND PlanTicketMaterialDetail.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE PlanId = " & PlanId & ")"
            End If

            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId < 1 AndAlso SalesOrderId > 0 Then
                Query += " AND PlanTicketMaterialDetail.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE SalesOrderID = " & SalesOrderId & ")"
            End If

            If bit = 1 Then

                Query += " group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
              & " ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , IsNull(LastPurchasePrice.Price, 0) , ArticleDefTable.Cost_Price , Isnull(ArticleGroup.SubSubId,0), IsNull(ArticleGroup.CGSAccountId, 0), IsNull(Process.WIPAccountId, 0), PlanTicketsMaster.ParentTicketNo, ArticleDefTableMaster.ArticleDescription, IsNull(PlanTicketsMaster.PlanID, 0), IsNull(PlanTicketsMaster.MasterArticleId, 0) HAVING SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END - IsNull(Tracking.DispatchedQty, 0)) > 0"

            Else
                Query += " group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
              & " ArticleDefTable.ArticleDescription, tblproSteps.prod_step, ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , IsNull(LastPurchasePrice.Price, 0) , Isnull(ArticleGroup.SubSubId,0), IsNull(ArticleGroup.CGSAccountId, 0), IsNull(Process.WIPAccountId, 0), PlanTicketsMaster.ParentTicketNo, ArticleDefTableMaster.ArticleDescription, IsNull(PlanTicketsMaster.PlanID, 0), IsNull(PlanTicketsMaster.MasterArticleId, 0) HAVING SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END - IsNull(Tracking.DispatchedQty, 0)) > 0"
            End If
            Return UtilityDAL.GetDataTable(Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetTicketRecordForStoreIssuance(ByVal SalesOrderId As Integer, ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal DepartmentId As Integer) As DataTable
        Dim Query As String = String.Empty

        Try
            ''TASK TFS4029 in case ticket is consumed partially then pending quantity is not calculated accurately. It is done by bringing Tracking query as nested query. Done by Amin on 30-07-2018
            Query = " Select PlanTicketsMaster.BatchNo, ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId, ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
& " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END) - IsNull(Tracking.DispatchedQty, 0) as PendingQty , ArticleDefTable.ArticleCode as Code, ArticleColorDefTable.ArticleColorName as Color, IsNull(ArticleDefTable.Cost_Price, 0) AS Rate, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END-IsNull(Tracking.DispatchedQty, 0)) * IsNull(ArticleDefTable.Cost_Price, 0) as Total, " _
& " SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END) - IsNull(Tracking.DispatchedQty, 0) as CheckQty, Max(Convert(Float, IsNull(Tracking.DispatchedQty, 0))) as IssuedQty, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0) * IsNull(PlanTicketsMaster.NoOfBatches, 0)) END) AS EstimatedQty, Isnull(ArticleGroup.SubSubId,0) as PurchaseAccountId, IsNull(ArticleGroup.CGSAccountId, 0) AS CGSAccountId, IsNull(Process.WIPAccountId, 0) AS WIPAccountId, PlanTicketsMaster.ParentTicketNo, ArticleDefTableMaster.ArticleDescription AS FinishGood, Sum(IsNull(Plan1.Qty, 0)) AS PlanQty, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -(IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) ELSE (IsNull(PlanTicketMaterialDetail.Qty, 0)* IsNull(PlanTicketsMaster.NoOfBatches, 0)) END) AS TicketQty, IsNull(PlanTicketsMaster.PlanID, 0) AS PlanId, Plan1.PlanNo, IsNull(PlanTicketsMaster.MasterArticleId, 0) AS MasterArticleId  FROM PlanTicketMaterialDetail " _
& " INNER JOIN PlanTicketsMaster ON " _
& " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
& " INNER JOIN ArticleDefTable ON " _
& " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
& " LEFT OUTER JOIN  (SELECT ArticleId, DepartmentId, TicketId, SUM(IsNull(DispatchedQty, 0)) AS DispatchedQty FROM tblTrackEstimation Group By ArticleId, DepartmentId, TicketId) As Tracking ON PlanTicketMaterialDetail.TicketId = Tracking.TicketId AND PlanTicketMaterialDetail.MaterialArticleId = Tracking.ArticleId AND PlanTicketMaterialDetail.DepartmentId = Tracking.DepartmentId " _
& " LEFT JOIN tblproSteps ON " _
& " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
& " LEFT JOIN ArticleColorDefTable on " _
& " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
& " LEFT OUTER JOIN dbo.ArticleGroupDefTable ArticleGroup ON ArticleDefTable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
& " LEFT OUTER JOIN (SELECT Article.ArticleId, IsNull(ProductionProcess.WIPAccountId, 0) AS WIPAccountId FROM ArticleDefTableMaster AS Article LEFT OUTER JOIN ProductionProcess ON Article.ProductionProcessId = ProductionProcess.ProductionProcessId) AS Process ON  PlanTicketsMaster.MasterArticleId = Process.ArticleId " _
& " LEFT OUTER JOIN ArticleDefTableMaster ON PlanTicketsMaster.MasterArticleId = ArticleDefTableMaster.ArticleId " _
& " LEFT OUTER JOIN (SELECT PlanDetailTable.PlanId, PlanDetailTable.ArticleDefId, Sum(PlanDetailTable.Qty) AS Qty, PlanMasterTable.PlanNo  FROM PlanDetailTable INNER JOIN PlanMasterTable ON PlanDetailTable.PlanId = PlanMasterTable.PlanId Group By PlanDetailTable.PlanId, PlanDetailTable.ArticleDefId, PlanMasterTable.PlanNo) AS Plan1 ON PlanTicketsMaster.PlanID = Plan1.PlanId AND PlanTicketsMaster.MasterArticleId = Plan1.ArticleDefId " _
& " WHERE PlanTicketsMaster.TicketNo <> '' "

            If DepartmentId > 0 Then
                Query += " AND PlanTicketMaterialDetail.DepartmentId = " & DepartmentId & " AND PlanTicketMaterialDetail.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId > 0 Then
                Query += " AND PlanTicketMaterialDetail.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId > 0 Then
                Query += " AND PlanTicketMaterialDetail.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE PlanId = " & PlanId & ")"
            End If

            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId < 1 AndAlso SalesOrderId > 0 Then
                Query += " AND PlanTicketMaterialDetail.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE SalesOrderID = " & SalesOrderId & ")"
            End If
            Query += " group By PlanTicketsMaster.BatchNo, ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
          & " ArticleDefTable.ArticleDescription, tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName, ArticleDefTable.Cost_Price, Isnull(ArticleGroup.SubSubId,0), IsNull(ArticleGroup.CGSAccountId, 0), IsNull(Process.WIPAccountId, 0), PlanTicketsMaster.ParentTicketNo, ArticleDefTableMaster.ArticleDescription, IsNull(PlanTicketsMaster.PlanID, 0), IsNull(PlanTicketsMaster.MasterArticleId, 0), Plan1.PlanNo, IsNull(Tracking.DispatchedQty, 0) HAVING SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0)*IsNull(PlanTicketsMaster.NoOfBatches, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0)*IsNull(PlanTicketsMaster.NoOfBatches, 0) END) - IsNull(Tracking.DispatchedQty, 0) > 0"
            Return UtilityDAL.GetDataTable(Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function GetTicketRecordForReturnStoreIssuance(ByVal SalesOrderId As Integer, ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal DepartmentId As Integer, ByVal bit As Integer) As DataTable

        Try

            Dim Query As String = "SELECT IsNull(Recv_D.LocationID, 0) As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS Qty, Recv_D.Price as Rate, " _
            & " Convert(float, ((IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) * IsNull(Recv_D.Price, 0))) AS Total, " _
            & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster] , Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, " _
            & " Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty, Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments, (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) As TotalQty, Recv_D.DispatchDetailId, " _
            & " (IsNull(Recv_D.Qty, 0)-(IsNull(Recv_D.ReturnedTotalQty, 0)+IsNull(Recv_D.ConsumedQty, 0))) AS CheckQty, IsNull(Recv_D.EstimationId, 0) As EstimationId, Recv_D.TicketId, planticketsMaster.BatchNo As TicketNo , IsNull(Recv_D.SubDepartmentID, 0) As DepartmentId, tblproSteps.prod_step As Department, PlanTicketsMaster.PlanId, PlanMasterTable.PlanNo,  ISNULL(Recv_D.TicketQty, 0) AS TicketQty, ISNULL(Recv_D.WIPAccountId, 0) AS WIPAccountId, ISNULL(PlanTicketsMaster.CostCenterId, 0) AS CostCenterId " _
            & " FROM dbo.DispatchDetailTable Recv_D INNER JOIN DispatchMasterTable As Recv ON Recv_D.DispatchId = Recv.DispatchId LEFT OUTER JOIN " _
            & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
            & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            & " LEFT OUTER JOIN tblproSteps On Recv_D.SubDepartmentID = tblproSteps.ProdStep_Id " _
            & " LEFT OUTER JOIN PlanTicketsMaster On PlanTicketsMaster.PlanTicketsMasterID = Recv_D.TicketId " _
            & " LEFT OUTER JOIN PlanMasterTable On PlanTicketsMaster.PlanId = PlanMasterTable.PlanId "


            If DepartmentId > 0 AndAlso TicketId < 1 Then
                Query += " Where Recv_D.SubDepartmentId =" & DepartmentId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) "

            ElseIf DepartmentId < 1 AndAlso TicketId > 0 Then

                Query += " Where  Recv_D.TicketId =" & TicketId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) "

            ElseIf DepartmentId > 0 AndAlso TicketId > 0 Then

                Query += " Where  Recv_D.TicketId =" & TicketId & " And Recv_D.SubDepartmentId =" & DepartmentId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) "

            Else

                Query += "where PlanTicketsMaster.PlanId = " & PlanId & " And IsNull(Recv_D.Qty, 0) > (IsNull(Recv_D.ConsumedQty, 0)+IsNull(Recv_D.ReturnedTotalQty, 0)) "

            End If

            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            dt.Columns("Total").Expression = "Qty*Rate"
            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function GetTicketRecordForConsumption(ByVal SalesOrderId As Integer, ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal DepartmentId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            ' Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Quantity, 0)) As [Estimated Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.Qty) As [Total Issued Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.ConsumedQty) As [Total Consumed Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.ReturnedTotalQty) As [Total Returned Qty], IsNull(Article_Group.SubSubId, 0) As SubSubId, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Issue.Qty, 0)-(IsNull(Issue.ConsumedQty, 0)+IsNull(Issue.ReturnedTotalQty, 0)))) As [Issuance Pending]
            Query = " Select PlanTicketsMaster.BatchNo, ArticleDefTable.ArticleId, IsNull(DispatchDetailTable.SubDepartmentId, 0) AS DepartmentId, DispatchDetailTable.TicketId, ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
         & " SUM(IsNull(DispatchDetailTable.Qty, 0)- IsNull(Tracking.ConsumedQty, 0)) as PendingQty , ArticleDefTable.ArticleCode as Code, ArticleColorDefTable.ArticleColorName as Color , DispatchDetailTable.Price as Rate, SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END-IsNull(Tracking.ConsumedQty, 0))*DispatchDetailTable.Price as Total, SUM(IsNull(DispatchDetailTable.ReturnedTotalQty, 0)) As TotalReturnedQty " _
         & " , SUM(CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END) as EstimatedQty, Sum(Convert(Float, IsNull(DispatchDetailTable.Qty, 0))) as TotalIssuedQty, SUM(IsNull(DispatchDetailTable.ConsumedQty, 0)) as TotalConsumedQty, SUM(IsNull(DispatchDetailTable.Qty, 0)-IsNull(DispatchDetailTable.ConsumedQty, 0)+IsNull(DispatchDetailTable.ReturnedTotalQty, 0)) AS IssuancePending, SUM(IsNull(DispatchDetailTable.Qty, 0)- IsNull(Tracking.ConsumedQty, 0)) as CheckQty, SUM((CASE WHEN PlanTicketMaterialDetail.Type ='Minus' Then -IsNull(PlanTicketMaterialDetail.Qty, 0) ELSE IsNull(PlanTicketMaterialDetail.Qty, 0) END)- IsNull(Tracking.ConsumedQty, 0)) as AvailableQty, Isnull(ArticleGroup.SubSubId,0) as PurchaseAccountId, IsNull(ArticleGroup.CGSAccountId, 0) AS CGSAccountId, PlanMasterTable.PlanNo " _
         & " FROM DispatchDetailTable " _
         & " LEFT OUTER JOIN PlanTicketMaterialDetail ON DispatchDetailTable.TicketId = PlanTicketMaterialDetail.TicketId AND DispatchDetailTable.SubDepartmentId = PlanTicketMaterialDetail.DepartmentId AND DispatchDetailTable.ArticleDefId = PlanTicketMaterialDetail.MaterialArticleId " _
         & " INNER JOIN PlanTicketsMaster ON PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN ArticleDefTable ON " _
         & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
         & " LEFT OUTER JOIN tblTrackEstimationConsumption As Tracking ON PlanTicketMaterialDetail.TicketId = Tracking.TicketId AND PlanTicketMaterialDetail.MaterialArticleId = Tracking.ArticleId AND PlanTicketMaterialDetail.DepartmentId = Tracking.DepartmentId " _
         & " LEFT JOIN tblproSteps ON " _
         & " tblproSteps.ProdStep_id = DispatchDetailTable.SubDepartmentId " _
         & " LEFT JOIN ArticleColorDefTable on " _
         & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
         & " LEFT OUTER JOIN dbo.ArticleGroupDefTable ArticleGroup ON ArticleDefTable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
         & " LEFT OUTER JOIN PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanId " _
         & " WHERE PlanTicketsMaster.TicketNo <> '' "

            'Query = "SELECT Article.ArticleId, Article.ArticleCode AS ArticleCode, Article.ArticleDescription AS Article, ArticleColorDefTable.ArticleColorName as Color, MasterItem.ArticleDescription As [Finish Good], IsNull(Recv_D.ParentTag#, 0) As [Parent Tag#], Convert(Decimal(18, " & DecimalPointInQty & "),(IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0))) AS Qty, " _
            '   & " IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(tblTrackEstimationConsumption.ConsumptionId, 0) As ConsumptionId, IsNull(tblTrackEstimationConsumption.ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.Price, 0) As Price, Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Quantity, 0)) As [Estimated Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.Qty) As [Total Issued Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.ConsumedQty) As [Total Consumed Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.ReturnedTotalQty) As [Total Returned Qty], IsNull(Article_Group.SubSubId, 0) As SubSubId, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Issue.Qty, 0)-(IsNull(Issue.ConsumedQty, 0)+IsNull(Issue.ReturnedTotalQty, 0)))) As [Issuance Pending] FROM  " _
            '   & " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(Recv_D.ParentId, 0) As ParentId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(Case When Recv_D.Types='Minus' Then -IsNull(Recv_D.Quantity, 0) Else IsNull(Recv_D.Quantity, 0) End) As Quantity, IsNull(Recv_D.ParentTag#, 0) As ParentTag#, IsNull(Recv_D.Price, 0) As Price FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.Value & " " & IIf(Me.cmbDepartment.SelectedValue > 0, " And Recv_D.SubDepartmentId =" & Me.cmbDepartment.SelectedValue & " ", "") & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(Recv_D.ParentId, 0), IsNull(Recv_D.ParentTag#, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0), IsNull(Recv_D.Price, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTable As MasterItem ON MasterItem.ArticleId = Recv_D.ParentId  " _
            '   & " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimationConsumption ON Recv_D.EstimationId = tblTrackEstimationConsumption.EstimationId And Recv_D.ProductId = tblTrackEstimationConsumption.ArticleId And Recv_D.ParentTag# = tblTrackEstimationConsumption.ParentTag# INNER JOIN (Select ArticleDefId, SubDepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, IsNull(EstimationId, 0) As EstimationId From DispatchDetailTable Where DispatchId =" & Val(Me.cmbTicket.ActiveRow.Cells("DispatchId").Value.ToString) & " Group by ArticleDefId, SubDepartmentId, EstimationId) As Issue ON IsNull(Recv_D.EstimationId, 0) = Issue.EstimationId And IsNull(Recv_D.SubDepartmentID , 0) = Issue.SubDepartmentId And Recv_D.ProductId = Issue.ArticleDefId " _
            '   & " LEFT OUTER JOIN (Select ArticleId, EstimationId, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, DepartmentId FROM tblTrackEstimationConsumption Group By ArticleId, EstimationId, DepartmentId) As Track ON IsNull(Track.EstimationId, 0) = Issue.EstimationId And IsNull(Track.DepartmentId , 0) = Issue.SubDepartmentId And Track.ArticleId = Issue.ArticleDefId Where  IsNull(Recv_D.Quantity, 0) > IsNull(tblTrackEstimationConsumption.ConsumedQty, 0) And (Issue.Qty > (Issue.ConsumedQty+Issue.ReturnedTotalQty))"


            If DepartmentId > 0 Then
                Query += " AND DispatchDetailTable.SubDepartmentId = " & DepartmentId & " AND DispatchDetailTable.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId > 0 Then
                Query += " AND DispatchDetailTable.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId > 0 Then
                Query += " AND DispatchDetailTable.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE PlanId = " & PlanId & ")"
            End If

            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId < 1 AndAlso SalesOrderId > 0 Then
                Query += " AND DispatchDetailTable.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE SalesOrderID = " & SalesOrderId & ")"
            End If
            Query += "group By PlanTicketsMaster.BatchNo , ArticleDefTable.ArticleId , DispatchDetailTable.SubDepartmentId , DispatchDetailTable.TicketId , " _
              & "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , DispatchDetailTable.Price, Isnull(ArticleGroup.SubSubId,0), IsNull(ArticleGroup.CGSAccountId, 0), PlanMasterTable.PlanNo HAVING SUM(IsNull(DispatchDetailTable.Qty, 0)- IsNull(Tracking.ConsumedQty, 0)) > 0 "
            Return UtilityDAL.GetDataTable(Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetTicketRecordForDecomposition(ByVal SalesOrderId As Integer, ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal DepartmentId As Integer, ByVal DecimalPointInQty As Integer) As DataTable
        Dim Query As String = String.Empty
        Try

            'Query = " Select  0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
            '                           & "  Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) AS Qty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS DecomposedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS WastedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS ScrappedQty, Sum((Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+ Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) AS TotalConsumedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact, Sum(IsNull(DecCount.ChildCount, 0)) AS ConsumedChildCount, IsNull(ArticleAccount.SubSubId, 0) AS SubSubId, IsNull(PlanItemAccount.SubSubId, 0) AS PlanItemSubSubId, 0 AS DValue, 0 AS WValue, 0 AS SValue " _
            '                           & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
            '                           & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
            '                           & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
            '                           & " LEFT OUTER JOIN ArticleGroupDefTable AS ArticleAccount ON Article.ArticleGroupId = ArticleAccount.ArticleGroupId " _
            '                           & " LEFT OUTER JOIN ArticleGroupDefTable AS PlanItemAccount ON PlanArticle.ArticleGroupId = PlanItemAccount.ArticleGroupId " _
            '                           & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId, Sum(IsNull(DecompositionDetail.TotalConsumedQty, 0)) AS TotalConsumedQty FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, DecompositionDetail.DepartmentId) AS Decomposition " _
            '                           & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.ParentId = Decomposition.ParentId AND EstimationDetail.SubDepartmentId = Decomposition.DepartmentId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
            '                           & " LEFT OUTER JOIN (SELECT Count(ParentTag#) As ChildCount, ParentTag#, IsNull(EstimationId, 0) AS EstimationId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where IsNull(Detail.StockImpact, 0) = 1 Group By EstimationId, ParentTag#) AS DecCount ON EstimationDetail.Tag# = DecCount.ParentTag# AND EstimationDetail.MaterialEstMasterID = DecCount.EstimationId " _
            '                           & " WHERE Estimation.Id = " & EstimationId & " AND Estimation.PlanTicketId = " & TicketId & " AND EstimationDetail.ProductId = " & ProductId & " Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action, IsNull(ArticleAccount.SubSubId, 0), IsNull(PlanItemAccount.SubSubId, 0) Having Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "


            Query = " Select PlanTicketsMaster.TicketNo, ArticleDefTable.ArticleId , EstimationDetail.DepartmentId, tblproSteps.prod_step AS Department, EstimationDetail.TicketId, ArticleDefTable.ArticleDescription as ProductName, MasterArticle.ArticleId AS MasterArticleId, MasterArticle.ArticleDescription AS MasterArticle, tblproSteps.prod_step as Stage, " _
                              & " Sum((Case When Type='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+ Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) as PendingQty , ArticleDefTable.ArticleCode as Code, ArticleColorDefTable.ArticleColorName as Color , EstimationDetail.CostPrice as Rate, Sum((Case When Type='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+ Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0)))))*EstimationDetail.CostPrice as Total, " _
                              & " Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS DecomposedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS WastedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS ScrappedQty, " _
                              & " Sum((Case When Type='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+ Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) AS DecomposableQty " _
                              & " , Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) AS TotalConsumedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact, Count(Decomposition.TicketId) AS ConsumedChildCount, " _
                              & " IsNull(ArticleGroup.SubSubId, 0) AS SubSubId, IsNull(ArticleGroup1.SubSubId, 0) AS PlanItemSubSubId, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit " _
                              & " FROM PlanTicketMaterialDetail As EstimationDetail " _
                              & " INNER JOIN PlanTicketsMaster ON " _
                              & " EstimationDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                              & " INNER JOIN ArticleDefTable ON " _
                              & " ArticleDefTable.ArticleId = EstimationDetail.MaterialArticleId " _
                              & " LEFT OUTER JOIN MaterialDecompositionDetail AS Decomposition ON EstimationDetail.TicketId = Decomposition.TicketId AND EstimationDetail.MaterialArticleId = Decomposition.ProductId AND EstimationDetail.DepartmentId = Decomposition.DepartmentId " _
                              & " LEFT JOIN tblproSteps ON " _
                              & " tblproSteps.ProdStep_id = EstimationDetail.DepartmentId " _
                              & " LEFT JOIN ArticleColorDefTable ON " _
                              & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                              & " LEFT OUTER JOIN dbo.ArticleGroupDefTable ArticleGroup ON ArticleDefTable.ArticleGroupId = ArticleGroup.ArticleGroupId " _
                              & " INNER JOIN ArticleDefTableMaster AS MasterArticle ON PlanTicketsMaster.MasterArticleId = MasterArticle.ArticleId " _
                              & " LEFT OUTER JOIN ArticleGroupDefTable ArticleGroup1 ON MasterArticle.ArticleGroupId = ArticleGroup1.ArticleGroupId " _
                              & " LEFT OUTER JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
                              & " WHERE PlanTicketsMaster.TicketNo <> '' "


            '& " LEFT OUTER JOIN (SELECT Count(TicketId) As ChildCount, ParentTag#, IsNull(EstimationId, 0) AS EstimationId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where IsNull(Detail.StockImpact, 0) = 1 Group By EstimationId, ParentTag#) AS DecCount ON EstimationDetail.Tag# = DecCount.ParentTag# AND EstimationDetail.MaterialEstMasterID = DecCount.EstimationId " _

            If DepartmentId > 0 Then
                Query += " AND EstimationDetail.DepartmentId = " & DepartmentId & " AND EstimationDetail.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId > 0 Then
                Query += " AND EstimationDetail.TicketId = " & TicketId & ""
            End If
            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId > 0 Then
                Query += " AND EstimationDetail.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE PlanId = " & PlanId & ")"
            End If

            If DepartmentId < 1 AndAlso TicketId < 1 AndAlso PlanId < 1 AndAlso SalesOrderId > 0 Then
                Query += " AND EstimationDetail.TicketId IN (SELECT PlanTicketsMasterID FROM PlanTicketsMaster WHERE SalesOrderID = " & SalesOrderId & ")"
            End If
            Query += " Group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , EstimationDetail.DepartmentId , EstimationDetail.TicketId , " _
              & " ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , EstimationDetail.CostPrice, IsNull(ArticleGroup.SubSubId, 0), IsNull(ArticleGroup1.SubSubId, 0), MasterArticle.ArticleId, MasterArticle.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName " _
              & " Having Sum(Case When Type='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Qty, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CheckTicket(ByVal TicketId As Integer) As Boolean
        Dim Query As String = String.Empty
        Dim dt As DataTable
        Dim Result As String = "False"
        Try
            Query = "if Exists ( Select TicketId from DispatchDetailTable where TicketId = " & TicketId & ") Select 'True' As Result Else Select 'False' As Result"
            dt = UtilityDAL.GetDataTable(Query)

            For Each row As DataRow In dt.Rows
                Result = row.Item("Result").ToString
            Next

            If Result = "True" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Task 3438 get items from plandetailtickets table 
    Public Shared Function getTicketDetail(ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal Ticket_Id As Integer) As DataTable
        Dim Query As String = String.Empty
        Dim dt As DataTable
        Dim Result As String = "False"
        Try

            Query = "select PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketsDetail.Articleid , ArticleDefTable.ArticleCode As Item , " _
                    & "PlanTicketsDetail.Quantity As Qty , PlanTicketsMaster.MasterArticleId " _
                    & "from PlanTicketsDetail " _
                    & "left outer join PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID " _
                    & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                    & "Left outer join ArticleDefTable ON PlanTicketsDetail.Articleid = ArticleDefTable.ArticleId "


            If PlanId > 0 AndAlso TicketId <= 0 Then

                Query = Query & "where PlanMasterTable.PlanID = " & PlanId & " And PlanTicketsDetail.PlanTicketsMasterID <> " & Ticket_Id

            ElseIf PlanId > 0 AndAlso TicketId > 0 AndAlso TicketId <> Ticket_Id Then

                Query = Query & "where PlanMasterTable.PlanID = " & PlanId & " And PlanTicketsDetail.PlanTicketsMasterID <> " & Ticket_Id _
                    & "and PlanTicketsDetail.PlanTicketsMasterID = " & TicketId

            ElseIf PlanId > 0 AndAlso TicketId > 0 AndAlso TicketId = Ticket_Id Then

                Query = Query & "where PlanMasterTable.PlanID = " & PlanId & " And PlanTicketsDetail.PlanTicketsMasterID <> " & Ticket_Id _


            ElseIf PlanId <= 0 AndAlso TicketId <= 0 Then

                Query = Query & "where PlanTicketsDetail.PlanTicketsMasterID = " & Ticket_Id

            Else

                Query = Query & "where PlanTicketsDetail.PlanTicketsMasterID = 0"

            End If

            dt = UtilityDAL.GetDataTable(Query)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Task 3438 get items consumed qty from consumption table 

    Public Shared Function getItemConsumedQty(ByVal ArticleId As Integer, ByVal TicketId As Integer) As Integer
        Dim Query As String = String.Empty
        Dim dt As DataTable
        Dim Result As String = "False"
        Try
            Query = "select Qty from ItemConsumptionDetail where TicketId = " & TicketId & " And ArticleId = " & ArticleId
            dt = UtilityDAL.GetDataTable(Query)

            For Each row As DataRow In dt.Rows
                Result = row.Item("Qty").ToString
            Next

            Return Val(Result)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Task 3439 get items closebatch from closeBatch table 

    Public Shared Function getItemCloseBatch(ByVal TicketId As Integer) As Boolean
        Dim Query As String = String.Empty
        Dim dt As DataTable
        Dim Result As String = "False"
        Try
            Query = "select isClosedBatch from closebatch where TicketId = " & TicketId
            dt = UtilityDAL.GetDataTable(Query)

            For Each row As DataRow In dt.Rows
                Result = row.Item("isClosedBatch").ToString
            Next

            If Result = "True" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Task 3439 get tickets from planmastertable agaisnt plan and ticket
    Public Shared Function getTickets(ByVal PlanId As Integer, ByVal TicketId As Integer) As DataTable
        Dim Query As String = String.Empty
        Dim dt As DataTable
        Dim Result As String = "False"
        Try
            Query = "select PlanTicketsMasterID as TicketId , TicketNo , MasterArticleId from PlanTicketsMaster"

            If PlanId > 0 AndAlso TicketId <= 0 Then

                Query = Query & " where planid = " & PlanId

            ElseIf PlanId > 0 AndAlso TicketId > 0 Then

                Query = Query & " where planid = " & PlanId & "and PlanTicketsMasterID = " & TicketId

            ElseIf PlanId <= 0 AndAlso TicketId > 0 Then

                Query = Query & " where PlanTicketsMasterID = " & TicketId

            Else

                Query = "select PlanTicketsMasterID as TicketId , TicketNo , MasterArticleId from PlanTicketsMaster where PlanTicketsMasterID = 0 "

            End If

            Query = Query & " order by ParentTicketNo ASC"

            dt = UtilityDAL.GetDataTable(Query)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function IsThisItemPartOfEstimation(ByVal ProductId As Integer, ByVal VersionId As Integer, ByVal ArticleId As Integer, ByVal PackingId As Integer, ByVal Qty As Double) As DataTable
        Dim Str As String = String.Empty
        Dim dt As DataTable
        Try
            Str = " SELECT  0 AS Id, IsNull(Detail.SubDepartmentId, 0) As DepartmentId, tblProSteps.prod_step As Department, 0 AS TicketId, IsNull(Detail.MaterialArticleId, 0) As MaterialArticleId, ArticleDefTable.ArticleCode AS ArticleCode, ArticleDefTable.ArticleDescription AS Material, Detail.TotalQty AS Qty, Convert(float, 0) AS RequiredQty, Detail.CostPrice, Convert(float, 0) AS Amount, '' AS Type, 1 * " & Qty & " AS NoOfBatches, IsNull(Detail.DetailArticleId, 0) AS DetailArticleId, IsNull(Detail.PackingId, 0) AS PackingId, IsNull(Detail.Id, 0) AS FinishGoodDetailId " _
                        & " FROM FinishGoodDetail AS Detail INNER JOIN FinishGoodMaster AS FinishGood ON Detail.FinishGoodId = FinishGood.Id INNER JOIN  " _
                        & " ArticleDefTable ON Detail.MaterialArticleId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
                        & "  tblProSteps ON Detail.SubDepartmentId = tblProSteps.ProdStep_Id " _
                        & " WHERE FinishGood.MasterArticleId  = " & ProductId & " AND FinishGood.Version = " & VersionId & " And IsNull(Detail.DetailArticleId, 0) = " & ArticleId & " And IsNull(Detail.PackingId, 0) = " & PackingId & " Order By Detail.Id "
            dt = UtilityDAL.GetDataTable(Str)
            dt.AcceptChanges()
            dt.Columns("RequiredQty").Expression = "(IsNull(Qty, 0)*IsNull(NoOfBatches, 0))"
            dt.Columns("Amount").Expression = "(IsNull(CostPrice, 0)*IsNull(RequiredQty, 0))"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Shared Function IsThisItemPartOfEstimation(ByVal ProductId As Integer, ByVal VersionId As Integer) As DataTable
    '    Dim Str As String = String.Empty
    '    Dim dt As DataTable
    '    Try
    '        Str = " SELECT  0 AS Id, IsNull(Detail.SubDepartmentId, 0) As DepartmentId, tblProSteps.prod_step As Department, 0 AS TicketId, IsNull(Detail.MaterialArticleId, 0) As MaterialArticleId, ArticleDefTable.ArticleCode AS ArticleCode, ArticleDefTable.ArticleDescription AS Material, Detail.Qty * " & BatchSize & " AS Qty, Convert(float, 0) AS RequiredQty, Detail.CostPrice, Convert(float, 0) AS Amount, '' AS Type, 0 AS NoOfBatches " _
    '                    & " FROM FinishGoodDetail AS Detail INNER JOIN FinishGoodMaster AS FinishGood ON Detail.FinishGoodId = FinishGood.Id INNER JOIN  " _
    '                    & " ArticleDefTable ON Detail.MaterialArticleId = ArticleDefTable.ArticleId LEFT OUTER JOIN " _
    '                    & "  tblProSteps ON Detail.SubDepartmentId = tblProSteps.ProdStep_Id " _
    '                    & " WHERE FinishGood.MasterArticleId  = " & ProductId & " AND FinishGood.Version = " & VersionId & " And IsNull(Detail.DetailArticleId, 0) = 0 Order By Detail.Id "
    '        dt = UtilityDAL.GetDataTable(Str)
    '        dt.AcceptChanges()
    '        dt.Columns("RequiredQty").Expression = "(IsNull(Qty, 0)*IsNull(NoOfBatches, 0))"
    '        dt.Columns("Amount").Expression = "(IsNull(CostPrice, 0)*IsNull(RequiredQty, 0))"
    '        Return dt
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
End Class
