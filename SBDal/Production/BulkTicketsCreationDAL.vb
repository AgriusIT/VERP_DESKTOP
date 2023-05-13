
''TASK TFS3587 Muhammad Ameen has done defining new cost center upon every new ticket creation. ON 22-06-2018
Imports SBModel
Imports System.Data.SqlClient

Public Class BulkTicketsCreationDAL
    Public Shared Query As String = String.Empty
    Shared TicketList As List(Of PlanTicketsMaster)
    Dim TicketCount As Integer = 0
    Public Shared Function Process() As Boolean
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function IsIndividual(ByVal ProductId As Integer) As Boolean
        Try
            Query = "Select IsNull(IsIndividual, 0) AS IsIndividual From ArticleDefTableMaster WHERE ArticleId = " & ProductId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Dim IsIndividual1 As Boolean = CBool(dt.Rows(0).Item(0))
            Return IsIndividual1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function MakeTickets(ByVal MasterArticleId As Integer, ByVal TicketNo As String, ByVal ParentTicketNo As String, ByVal CustomerId As Integer, ByVal SalesOrderId As Integer, ByVal PlanId As Integer, ByVal Qty As Double, ByVal Detail As PlanDetailTableBE, Optional ByVal UserName As String = "", Optional ByVal UserId As Integer = 0) As List(Of PlanTicketsMaster)
        Dim NEW_TICKET As String = String.Empty
        Dim TNUMBER As String = "000"
        Try
            Query = " Select Detail.MaterialArticleId As ArticleId, IsNull(Detail.DetailArticleId, 0) AS DetailArticleId, " _
                   & " IsNull(Detail.PackingId, 0) AS PackingId, IsNull(Detail.Qty, 0) AS Qty, IsNull(Detail.TotalQty, 0) AS TotalQty, Detail.ArticleSize, Detail.CostPrice, IsNull(Detail.SubDepartmentId, 0) AS DepartmentId, FinishGood.Id AS FinishGoodId, FinishGood.BatchSize,  " _
                   & " Article.MasterID AS MasterId From FinishGoodMaster As FinishGood INNER JOIN FinishGoodDetail AS Detail ON FinishGood.Id = Detail.FinishGoodId  INNER JOIN ArticleDefTable Article ON Detail.MaterialArticleId = Article.ArticleId " _
                   & " WHERE FinishGood.MasterArticleId = " & MasterArticleId & " And Default1 = 1"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            If dt.Rows.Count > 0 Then
                'Dim TicketNumber As Integer = 
                NEW_TICKET = TicketNo
                If IsIndividual(MasterArticleId) Then
                    For i As Integer = 1 To Qty
                        Dim Ticket As New PlanTicketsMaster
                        TNUMBER = CInt(i).ToString.PadLeft(3, "0")
                        Ticket.TicketDate = Now
                        Ticket.TicketNo = TicketNo & "-" & TNUMBER
                        Ticket.BatchSize = dt.Rows(0).Item("BatchSize")
                        'Task 3439 Set Batch No As Ticket No
                        Ticket.BatchNo = TicketNo & "-" & TNUMBER
                        Ticket.CustomerID = CustomerId
                        Ticket.FinishGoodId = dt.Rows(0).Item("FinishGoodId")
                        Ticket.PlanID = PlanId
                        Ticket.MasterArticleId = MasterArticleId
                        Ticket.NoOfBatches = 1
                        Ticket.ParentTicketNo = ParentTicketNo
                        Ticket.SpecialInstructions = String.Empty
                        Ticket.SalesOrderID = SalesOrderId
                        ''
                        ''Activity Log
                        Ticket.ActivityLog = New ActivityLog()
                        Ticket.ActivityLog.ActivityName = "Save"
                        Ticket.ActivityLog.ApplicationName = "Production"
                        Ticket.ActivityLog.FormCaption = "Plan Ticket Standard"
                        Ticket.ActivityLog.FormName = "frmPlanTicketStandard"
                        Ticket.ActivityLog.LogDateTime = Date.Now
                        Ticket.ActivityLog.LogComments = String.Empty
                        Ticket.ActivityLog.User_Name = UserName
                        Ticket.ActivityLog.UserID = UserId
                        Ticket.ActivityLog.Source = "frmPlanTicketStandard"

                        ''


                        'Ticket.BatchSize = dt.Rows(0).Item("BatchSize")
                        For Each _Row As DataRow In dt.Rows
                            Dim MaterialDetail As New BEPlanTicketMaterialDetail
                            MaterialDetail.Id = 0
                            MaterialDetail.CostPrice = _Row.Item("CostPrice")
                            MaterialDetail.DepartmentId = _Row.Item("DepartmentId")
                            MaterialDetail.FinishGoodId = _Row.Item("FinishGoodId")
                            MaterialDetail.MaterialArticleId = _Row.Item("ArticleId")
                            MaterialDetail.TicketId = 0
                            MaterialDetail.Type = String.Empty
                            MaterialDetail.Qty = _Row.Item("TotalQty")
                            Ticket.MaterialDetail.Add(MaterialDetail)
                            MakeTickets(_Row.Item("MasterId"), Ticket.TicketNo, Ticket.TicketNo, CustomerId, SalesOrderId, PlanId, _Row.Item("TotalQty"), Detail)
                        Next

                        Dim dtArticles As DataTable = GetDetailArticles(MasterArticleId)
                        If dtArticles.Rows.Count > 0 Then
                            For Each DATA_ROW As DataRow In dtArticles.Rows
                                Dim TicketDetail As New PlanTicketsDetail
                                TicketDetail.ArticleId = DATA_ROW.Item("ArticleId")
                                TicketDetail.PackingId = 0
                                TicketDetail.PlanDetailId = Detail.PlanDetailId
                                TicketDetail.PlanTicketsDetailID = 0
                                TicketDetail.PlanTicketsMasterID = 0
                                TicketDetail.Quantity = dt.Rows(0).Item("BatchSize") / dtArticles.Rows.Count

                                'TicketDetail.
                                Ticket.Detail.Add(TicketDetail)
                            Next
                        End If
                        TicketList.Add(Ticket)
                    Next
                Else
                    Dim Ticket As New PlanTicketsMaster
                    TNUMBER = CInt(1).ToString.PadLeft(3, "0")
                    Ticket.TicketDate = Now
                    Ticket.TicketNo = TicketNo & "-" & TNUMBER
                    Ticket.BatchSize = dt.Rows(0).Item("BatchSize")
                    Ticket.BatchNo = TicketNo & "-" & TNUMBER
                    Ticket.CustomerID = CustomerId
                    Ticket.FinishGoodId = dt.Rows(0).Item("FinishGoodId")
                    Ticket.PlanID = PlanId
                    Ticket.MasterArticleId = MasterArticleId
                    Ticket.NoOfBatches = 1
                    Ticket.ParentTicketNo = ParentTicketNo
                    Ticket.SpecialInstructions = String.Empty
                    Ticket.SalesOrderID = SalesOrderId

                    ''Activity Log
                    Ticket.ActivityLog = New ActivityLog()
                    Ticket.ActivityLog.ActivityName = "Save"
                    Ticket.ActivityLog.ApplicationName = "Production"
                    Ticket.ActivityLog.FormCaption = "Plan Ticket Standard"
                    Ticket.ActivityLog.FormName = "frmPlanTicketStandard"
                    Ticket.ActivityLog.LogDateTime = Date.Now
                    Ticket.ActivityLog.LogComments = String.Empty
                    Ticket.ActivityLog.User_Name = UserName
                    Ticket.ActivityLog.UserID = UserId
                    Ticket.ActivityLog.Source = "frmPlanTicketStandard"
                    ''
                    For Each _Row As DataRow In dt.Rows
                        Dim MaterialDetail As New BEPlanTicketMaterialDetail
                        MaterialDetail.Id = 0
                        MaterialDetail.CostPrice = _Row.Item("CostPrice")
                        MaterialDetail.DepartmentId = _Row.Item("DepartmentId")
                        MaterialDetail.FinishGoodId = _Row.Item("FinishGoodId")
                        MaterialDetail.MaterialArticleId = _Row.Item("ArticleId")
                        MaterialDetail.TicketId = 0
                        MaterialDetail.Type = String.Empty
                        MaterialDetail.Qty = _Row.Item("TotalQty")
                        Ticket.MaterialDetail.Add(MaterialDetail)
                        MakeTickets(_Row.Item("MasterId"), Ticket.TicketNo, Ticket.TicketNo, CustomerId, SalesOrderId, PlanId, _Row.Item("TotalQty"), Detail)
                    Next

                    Dim dtArticles As DataTable = GetDetailArticles(MasterArticleId)
                    If dtArticles.Rows.Count > 0 Then
                        For Each DATA_ROW As DataRow In dtArticles.Rows
                            Dim TicketDetail As New PlanTicketsDetail
                            TicketDetail.ArticleId = DATA_ROW.Item("ArticleId")
                            TicketDetail.PackingId = 0
                            TicketDetail.PlanDetailId = Detail.PlanDetailId
                            TicketDetail.PlanTicketsDetailID = 0
                            TicketDetail.PlanTicketsMasterID = 0
                            TicketDetail.Quantity = dt.Rows(0).Item("BatchSize") / dtArticles.Rows.Count
                            'TicketDetail.
                            Ticket.Detail.Add(TicketDetail)
                        Next
                    End If
                    TicketList.Add(Ticket)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetSODetails(ByVal SalesOrderId As Integer) As List(Of PlanDetailTableBE)
        '        SalesOrderDetailId	int	Unchecked
        'SalesOrderId	int	Unchecked
        'LocationId	int	Checked
        'ArticleDefId	int	Checked
        'ArticleSize	varchar(10)	Checked
        'Sz1	float	Checked
        'Sz2	float	Checked
        'Sz3	float	Checked
        'Sz4	float	Checked
        'Sz5	float	Checked
        'Sz6	float	Checked
        'Sz7	float	Checked
        'Qty	float	Checked
        'Price	float	Checked
        'CurrentPrice	float	Checked
        'DeliveredQty	float	Checked
        'TradePrice	float	Checked
        'SalesTax_Percentage	float	Checked
        'SchemeQty	float	Checked
        'Discount_Percentage	float	Checked
        'Freight	float	Checked
        'MarketReturns	float	Checked
        'DeliveredSchemeQty	float	Checked
        'PurchasePrice	float	Checked
        'PackPrice	float	Checked
        'Pack_Desc	varchar(300)	Checked
        'Comments	varchar(1000)	Checked
        'Engine_No	nvarchar(1000)	Checked
        'Chassis_No	nvarchar(1000)	Checked
        'PlanedQty	float	Checked
        'ExPlantPrice	float	Checked
        'Other_Comments	nvarchar(300)	Checked
        'Pack_40Kg_Weight	float	Checked
        'CostPrice	float	Checked
        'SED_Tax_Percent	float	Checked
        'SED_Tax_Amount	float	Checked
        'QuotationDetailId	int	Checked
        'ArticleAliasName	nvarchar(1500)	Checked
        'SaleOrderType	nvarchar(100)	Checked
        'DeliveredTotalQty	float	Checked
        'BaseCurrencyId	int	Checked
        'BaseCurrencyRate	float	Checked
        'CurrencyId	int	Checked
        'CurrencyRate	float	Checked
        'CurrencyAmount	float	Checked
        'SerialNo	nvarchar(100)	Checked
        'PurchaseInquiryDetailId	int	Checked
        'TicketQty	float	Checked
        '        Unchecked()
        Dim Detail As New List(Of PlanDetailTableBE)
        Try
            Query = "Select Detail.LocationId, Detail.ArticleDefId, Detail.ArticleSize, Detail.Sz1, Detail.Sz2, Detail.Sz3, Detail.Sz4, Detail.Sz5, Detail.Sz6, Detail.Sz7, " _
                  & " Detail.Qty, Detail.Price, Detail.CurrentPrice, Detail.TicketQty, Detail.SalesOrderId, Detail.SalesOrderDetailId, Article.MasterID AS MasterArticleId From SalesOrderDetailTable AS Detail INNER JOIN ArticleDefTable AS Article ON Detail.ArticleDefId = Article.ArticleId  WHERE SalesOrderId = " & SalesOrderId & " And IsNull(Detail.Qty, 0) > IsNull(Detail.TicketQty, 0) "
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            If dt.Rows.Count > 0 Then
                'Detail = New List(Of PlanDetailTableBE)
                For Each Row As DataRow In dt.Rows
                    Dim Obj As New PlanDetailTableBE
                    Obj.PlanTicketsDetailID = 0
                    Obj.ArticleAliasName = String.Empty
                    Obj.LocationId = Val(Row.Item("LocationId").ToString)
                    Obj.ArticleDefId = Val(Row.Item("ArticleDefId").ToString)
                    Obj.ArticleSize = Row.Item("ArticleSize").ToString
                    Obj.Sz1 = Row.Item("Sz1").ToString
                    Obj.Sz2 = Val(Row.Item("Sz2").ToString)
                    Obj.Sz3 = Val(Row.Item("Sz3").ToString)
                    Obj.Sz4 = Val(Row.Item("Sz4").ToString)
                    Obj.Sz5 = Val(Row.Item("Sz5").ToString)
                    Obj.Sz6 = Val(Row.Item("Sz6").ToString)
                    Obj.Sz7 = Val(Row.Item("Sz7").ToString)
                    Obj.Qty = Val(Row.Item("Qty").ToString)
                    Obj.Price = Val(Row.Item("Price").ToString)
                    Obj.MasterArticleId = Val(Row.Item("MasterArticleId").ToString)
                    Obj.CurrentPrice = Val(Row.Item("CurrentPrice").ToString)
                    Obj.TicketIssuedQty = Val(Row.Item("TicketQty").ToString)
                    Obj.SalesOrderId = Val(Row.Item("SalesOrderId").ToString)
                    Obj.SODetailId = Val(Row.Item("SalesOrderDetailId").ToString)
                    Detail.Add(Obj)
                Next
            End If
            Return Detail
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function InsertPlan(ByVal objModel As PlanMasterTableBE, Optional ByVal UserName As String = "", Optional ByVal UserId As Integer = 0) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            InsertPlan(objModel, trans, UserName, UserId)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function InsertPlan(ByVal objModel As PlanMasterTableBE, trans As SqlTransaction, Optional ByVal UserName As String = "", Optional ByVal UserId As Integer = 0) As Boolean
        'Plan.PlanNo = GetNextPlanNo()
        'Plan.PlanDate = Now
        'Plan.StartDate = Me.dtpStartDate.Value
        'Plan.CustomerId = Val(CType(Me.cmbSalesOrder.SelectedItem, DataRowView).Item("CustomerId").ToString)
        'Plan.LocationId = Val(CType(Me.cmbSalesOrder.SelectedItem, DataRowView).Item("LocationId").ToString)
        'Plan.PoId = Me.cmbSalesOrder.SelectedValue
        'Plan.UserName = LoginUserName
        'Plan.Remarks = String.Empty
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  PlanMasterTable (LocationId, PlanNo, PlanDate, CustomerId, Remarks, UserName, PoId, StartDate) values (N'" & objModel.LocationId & "', N'" & objModel.PlanNo.Replace("'", "''") & "', N'" & objModel.PlanDate & "', N'" & objModel.CustomerId & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.UserName.Replace("'", "''") & "', N'" & objModel.PoId & "', N'" & objModel.StartDate & "') Select @@Identity "
            objModel.PlanId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            InsertPlanDetail(objModel, trans, UserName, UserId)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function InsertPlanDetail(ByVal Obj As PlanMasterTableBE, trans As SqlTransaction, Optional ByVal UserName As String = "", Optional ByVal UserId As Integer = 0) As Boolean
        Try
            For Each objModel As PlanDetailTableBE In Obj.Detail

                Dim strSQL As String = String.Empty
                strSQL = " insert into  PlanDetailTable (PlanId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2, Sz3, Sz4, Sz5, Sz6, Sz7, Qty, Price, CurrentPrice, SODetailId, SOId) " _
                         & " values (" & Obj.PlanId & ", " & objModel.LocationId & ", " & objModel.MasterArticleId & ", N'" & objModel.ArticleSize.Replace("'", "''") & "', " _
                         & " " & objModel.Sz1 & ", " & objModel.Sz2 & ", " & objModel.Sz3 & ", " & objModel.Sz4 & ", " & objModel.Sz5 & ", " & objModel.Sz6 & ", " _
                         & " " & objModel.Sz7 & ", " & objModel.Qty & ", " & objModel.Price & ", " & objModel.CurrentPrice & ", " & objModel.SODetailId & ", " & Obj.PoId & ") Select @@Identity"
                objModel.PlanDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

                strSQL = ""
                strSQL = "UPDATE SalesOrderDetailtable SET TicketQty = IsNull(TicketQty, 0) + " & objModel.Qty & " Where SalesOrderDetailId = " & objModel.SODetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                TicketList = New List(Of PlanTicketsMaster)
                MakeTickets(objModel.MasterArticleId, Obj.PlanNo, "", Obj.CustomerId, Obj.PoId, Obj.PlanId, objModel.Qty, objModel)
                InsertTicket(TicketList, trans, objModel.LocationId)
                'objModel.ActivityLog.ActivityName = "Save"
                'objModel.ActivityLog.RecordType = "Configuration"
                ''objModel.ActivityLog.RefNo = ""
                'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Shared Function InsertTicket(ByVal List As List(Of PlanTicketsMaster), ByVal trans As SqlTransaction, Optional ByVal LocationId As Integer = 0) As Boolean

        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each PlanTicketsMaster As PlanTicketsMaster In List

                ''TASK TFS3587
                Dim CostCenterId As Integer = CreateCostCenter(PlanTicketsMaster.TicketNo, trans)
                ''END TASK TFS3587

                Dim str As String = String.Empty
                str = "Insert Into PlanTicketsMaster(TicketNo, TicketDate, CustomerID, SalesOrderID, PlanID, SpecialInstructions, BatchNo, MasterArticleId, BatchSize, FinishGoodId, NoOfBatches, ParentTicketNo, CostCenterId) " _
                & " Values(N'" & PlanTicketsMaster.TicketNo & "', N'" & PlanTicketsMaster.TicketDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & PlanTicketsMaster.CustomerID & ", " & PlanTicketsMaster.SalesOrderID & ", " & PlanTicketsMaster.PlanID & ", N'" & PlanTicketsMaster.SpecialInstructions.Replace("'", "''") & "', N'" & PlanTicketsMaster.BatchNo.Replace("'", "''") & "', " & PlanTicketsMaster.MasterArticleId & ", " & PlanTicketsMaster.BatchSize & ", " & PlanTicketsMaster.FinishGoodId & ", " & PlanTicketsMaster.NoOfBatches & ", N'" & PlanTicketsMaster.ParentTicketNo & "', " & CostCenterId & ") Select @@Identity "

                PlanTicketsMaster.PlanTicketsMasterID = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                'Insert Stock Detail Information 
                InsertMaterialDetail(PlanTicketsMaster, trans)
                InsertTicketDetail(PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.Detail, trans, LocationId)
                UtilityDAL.BuildActivityLog(PlanTicketsMaster.ActivityLog, trans)
                'Trans Commit here... 
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
            'Finally
            '    Con.Close()
        End Try
    End Function
    Public Shared Function InsertTicketDetail(ByVal PlanTecketsMasterID As Integer, ByVal PlanTicketsDetailList As List(Of PlanTicketsDetail), ByVal trans As SqlTransaction, Optional ByVal LocationId As Integer = 0) As Boolean
        Try
            Dim str As String = String.Empty
            For Each PlanTicketsDetail As PlanTicketsDetail In PlanTicketsDetailList
                ''TASK TFS3588 Addition of locationId columns value of this column is taken from Sales Order. 22-06-2018
                str = "Insert Into PlanTicketsDetail(PlanTicketsMasterID, ArticleId, PlanDetailId, Quantity, PackingId, LocationId) " _
            & " Values (" & PlanTecketsMasterID & ", " & PlanTicketsDetail.ArticleId & ",  " & PlanTicketsDetail.PlanDetailId & ", " & PlanTicketsDetail.Quantity & ", " & PlanTicketsDetail.PackingId & ", " & LocationId & ")"
                'End Task:M16
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                'str = ""
                'str = "UPDATE SalesOrderDetailtable SET TicketQty = IsNull(TicketQty, 0) + " & PlanTicketsDetail.Quantity & " Where PlanDetailId = " & PlanTicketsDetail.PlanDetailId & ""
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
        End Try
    End Function
    Public Function GetNextTicket(ByVal PlanNo As String, ByVal PlanId As Integer, ByVal trans As SqlTransaction) As String
        Try
            Dim str As String = 0
            'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim strSql As String
            'select substring(@str, 3, charindex('\', @str, 3) - 3)
            strSql = "select IsNull(Max(Convert(Integer, Right(TicketNo, CHARINDEX('-', REVERSE('-' + TicketNo)) - 1))), 0) from PlanTicketsMaster Where PlanId = " & PlanId & "" ' "
            'Else
            '    strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim dt As New DataTable
            'Dim adp As New OleDbDataAdapter
            'adp = New OleDbDataAdapter(strSql, New OleDbConnection(Con.ConnectionString))
            dt = UtilityDAL.GetDataTable(PlanNo, trans)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 AndAlso Not dt.Rows(0).Item(0).ToString = "0" Then
                str = dt.Rows(0).Item(0).ToString
                str = PlanNo & "-" & str + 1
            Else
                str = PlanNo & "-" & 1
            End If
            Return str
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function InsertMaterialDetail(ByVal Obj As PlanTicketsMaster, ByVal trans As SqlTransaction) As Boolean
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
                    str = "INSERT INTO PlanTicketMaterialDetail(TicketId, MaterialArticleId, Qty, CostPrice, DepartmentId, Type) Values (" & Obj.PlanTicketsMasterID & ", " & Detail.MaterialArticleId & ",  " & Detail.Qty & ", " & Detail.CostPrice & ", " & Detail.DepartmentId & ", '" & Detail.Type.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Update PlanTicketMaterialDetail Set TicketId = " & Obj.PlanTicketsMasterID & ", MaterialArticleId = " & Detail.MaterialArticleId & ", Qty = " & Detail.Qty & ", CostPrice = " & Detail.CostPrice & ", DepartmentId = " & Detail.DepartmentId & ", Type = '" & Detail.Type.Replace("'", "''") & "' WHERE Id = " & Detail.Id & " "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Shared Function GetTickets(ByVal SalesOrderId As Integer, ByVal PlanId As Integer) As DataTable
        Dim Query As String = String.Empty
        Dim dt As DataTable
        Try
            Query = "SELECT PlanTicketsMasterID As TicketId, TicketNo AS Ticket, ParentTicketNo AS ParentTicket, Article.ArticleId AS ProductId, Article.ArticleDescription AS Product  FROM PlanTicketsMaster AS Ticket INNER JOIN ArticleDefTableMaster AS Article ON Ticket.MasterArticleId = Article.ArticleId WHERE PlanTicketsMasterID > 0 "
            If SalesOrderId > 0 Then
                Query += " And SalesOrderId = " & SalesOrderId & ""
            End If
            If PlanId > 0 Then
                Query += " And PlanId = " & PlanId & ""
            End If
            If SalesOrderId > 0 AndAlso PlanId > 0 Then
                Query += " And SalesOrderId = " & SalesOrderId & " And PlanId = " & PlanId & ""
            End If
            Query += " ORDER BY TicketDate "
            dt = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDetailArticles(ByVal MasterArticleId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT ArticleId FROM ArticleDefTable WHERE MasterID = " & MasterArticleId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetTicket(ByVal TicketId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT * FROM PlanTicketsMaster WHERE PlanTicketsMasterID = " & TicketId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS3587
    ''' </summary>
    ''' <param name="TicketNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateCostCenter(ByVal TicketNo As String, ByVal trans As SqlTransaction) As Integer
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO tblDefCostCenter(Name,Code,sortorder, CostCenterGroup, Active, OutwardGatepass, DayShift, LCDocId) values(N'" & TicketNo.Replace("'", "''") & "','" & TicketNo.Replace("'", "''") & "','" & 1 & "', N'" & " " & "', " & 1 & ", " & 0 & ", " & 0 & " , " & 0 & ") Select @@Identity "
            Dim CostCenterId As Integer
            CostCenterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Trans Commit here... 
            'trans.Commit()
            Return CostCenterId
        Catch ex As Exception
            'trans.Rollback()
            Return 0
            Throw ex
        End Try
    End Function
End Class
