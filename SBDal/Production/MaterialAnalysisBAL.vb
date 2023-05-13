Imports SBDal
Imports SBModel
Public Class MaterialAnalysisBAL

    Dim DAL As CRUD_db
    Public Function GetCustomers() As DataTable
        Dim MasterQuery As String = "select c.CustomerID, c.CustomerName from tblCustomer c"
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetSalesOrder() As DataTable
        Dim MasterQuery As String = "select s.SalesOrderId, s.SalesOrderNo from SalesOrderMasterTable s"
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetSalesOrderByID(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select s.SalesOrderId, s.SalesOrderNo from SalesOrderMasterTable s where s.VendorId=" & id
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetPlanNo() As DataTable
        Dim MasterQuery As String = "select planId , PlanNo  from planmastertable"
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function

    Public Function GetPlanNoByID(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select planId , PlanNo  from planmastertable where PoId=" & id
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function

    Public Function GetTicketNo() As DataTable
        Dim MasterQuery As String = "select PlanTicketsId, TicketNo, TicketQuantity, ArticleId from plantickets"
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function

    Public Function GetTicketNoByPlan(ByVal PlanID As Integer) As DataTable
        Dim MasterQuery As String = "select PlanTicketsId,TicketNo, TicketQuantity, ArticleId  from plantickets where PlanId =" & PlanID & ""
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function

    Public Function GetEstimation() As DataTable
        Dim MasterQuery As String = "select Id,DocNo from MaterialEstimation"
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetEstimationByPlan(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select Id,DocNo from MaterialEstimation where MasterPlanId=" & id
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetEstimationBySaleOrder(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select Id,DocNo from MaterialEstimation where saleorderId=" & id
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function
    Public Function GetEstimationByTicket(ByVal id As Integer) As DataTable
        Dim MasterQuery As String = "select Id,DocNo from MaterialEstimation where PlanTicketId=" & id
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.ReadTable(MasterQuery)
        Return table
    End Function


    Public Sub Save_Transation(ByVal matObj As MaterialAnalysisMaster)
        '       MaterialAnalysisMasterID	int	Unchecked
        'DocNo	nvarchar(50)	Checked
        'MDate	datetime	Checked
        'CustomerID	int	Checked
        'SaleOrderID	int	Checked
        'PlanMasterID	int	Checked
        'TicketID	int	Checked
        'TicketQty	float	Checked
        'EstimationMasterID	int	Checked
        'Remarks	nvarchar(MAX)	Checked
        'ArticleId	int	Checked
        '        Unchecked()
        Dim DetailQueryList As List(Of String) = New List(Of String)
        DAL = New CRUD_db()
        'MaterialAnalysisDetailID	int	Unchecked
        'MaterialAnalysisMasterID	int	Checked
        'RawMaterialID	int	Checked
        'MatEtmQty	float	Checked
        'AllocQty	float	Checked
        'StockQty	float	Checked
        'POQty	float	Checked
        'AvailableQty	float	Checked
        'RequiredStockQty	float	Checked
        'Unchecked()
        Dim MasterQuery As String = "insert into MaterialAnalysisMaster(DocNo ,MDate, CustomerID, SaleOrderID, PlanMasterID, TicketID, TicketQty, EstimationMasterID, Remarks, ArticleId ) values( N'" & matObj.DocNo & "', N'" & matObj.MDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', " & matObj.CustomerID & ", " & matObj.SaleOrderID & ", " & matObj.PlanMasterID & ", " & matObj.TicketID & ", " & matObj.TicketQty & ", " & matObj.EstimationMasterID & ", N'" & matObj.Remarks & "', " & matObj.ProductID & " )select @@Identity"
        Dim Detailquery As String

        For Each item As MaterialAnalysisDetail In matObj.MatAnalysisDetailList
            Detailquery = "insert into MaterialAnalysisDetail(MaterialAnalysisMasterID, RawMaterialID, MatEtmQty, AllocQty, StockQty, POQty , AvailableQty, RequiredStockQty) values(`," & item.RawMaterialID & ", " & item.MatEtmQty & ", " & item.AllocQty & ", " & item.StockQty & "," & item.POQty & ", " & item.AvailableQty & ", " & item.RequiredStockQty & " )"
            DetailQueryList.Add(Detailquery)
        Next
        DAL.Master_Insertion(MasterQuery, DetailQueryList)
    End Sub
    Public Sub Update_Transation(ByVal matObj As MaterialAnalysisMaster)
        Dim DetailQueryList As List(Of String) = New List(Of String)
        DAL = New CRUD_db()

        Dim MasterQuery As String = "update  MaterialAnalysisMaster set DocNo = N'" & matObj.DocNo & "', MDate = N'" & matObj.MDate.ToString("yyyy-M-d hh:mm:ss tt") & "', CustomerID = " & matObj.CustomerID & ", SaleOrderID = " & matObj.SaleOrderID & ", PlanMasterID = " & matObj.PlanMasterID & ", TicketID= " & matObj.TicketID & ", TicketQty= " & matObj.TicketQty & ", EstimationMasterID= " & matObj.EstimationMasterID & ", Remarks = N'" & matObj.Remarks & "', ArticleId = N'" & matObj.ProductID & "' where MaterialAnalysisMasterID=" & matObj.MaterialAnalysisMasterID
        Dim Detailquery As String
        For Each item As MaterialAnalysisDetail In matObj.MatAnalysisDetailList
            Detailquery = "insert into MaterialAnalysisDetail(MaterialAnalysisMasterID, RawMaterialID, MatEtmQty, AllocQty, StockQty, POQty , AvailableQty, RequiredStockQty) values(`," & item.RawMaterialID & ", " & item.MatEtmQty & ", " & item.AllocQty & ", " & item.StockQty & ", " & item.POQty & ", " & item.AvailableQty & ", " & item.RequiredStockQty & " )"
            DetailQueryList.Add(Detailquery)
        Next
        Dim Update_Detail_Del As String = "Delete from MaterialAnalysisDetail where MaterialAnalysisMasterID=" & matObj.MaterialAnalysisMasterID
        DAL.Master_Update(matObj.MaterialAnalysisMasterID, MasterQuery, DetailQueryList, Update_Detail_Del)

    End Sub

    Public Sub DeleteMaster(ByVal MasterID As Integer)
        DAL = New CRUD_db()
        Dim MasterDetailQuery As String = "Delete from MaterialAnalysisDetail where MaterialAnalysisMasterID =" & MasterID
        Dim MasterQuery As String = "Delete from MaterialAnalysisMaster where MaterialAnalysisMasterID =" & MasterID
        DAL.Master_Deletion(MasterDetailQuery, MasterQuery)

    End Sub

    Public Function GetMatAnalysis_Grid(ByVal ID As Integer) As DataTable
        DAL = New CRUD_db()
        Dim table As DataTable = DAL.Read_SP(ID)
        Return table
    End Function

End Class
