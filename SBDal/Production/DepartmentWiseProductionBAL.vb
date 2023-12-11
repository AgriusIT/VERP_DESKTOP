''TASK: TFS1083 New Material Estimation screen to load  old cost sheet and then estimation should be loaded to old store issuance.
Imports Janus.Windows.GridEX
Imports SBModel.DepartmentWiseProductionModel
Imports SBModel
Imports System.Data.SqlClient

Public Class DepartmentWiseProductionBAL
    Dim DwpDal As DepartmentWiseproductionDAL
    Dim DwpdDal As DepartmentWiseProductionDetailDAL

    Public Function Save(ByVal deptwiseprobj As DepartmentWiseProductionModel) As List(Of String)
        Try
            Dim DetailQueryList As List(Of String) = New List(Of String)
            DwpDal = New DepartmentWiseproductionDAL
            DwpdDal = New DepartmentWiseProductionDetailDAL
            Dim MasterQuery As String = "insert into DepartmentWiseProduction(DocNo, Date, SpecialInstructions, DepartmentID, ReferenceNo) Values(N'" & deptwiseprobj.DocNo & "', N'" & deptwiseprobj.DwpDate.ToString("yyyy-M-d hh:mm:ss tt") & "', N'" & deptwiseprobj.SpecialInstructions & "', " & deptwiseprobj.DepartmentID & ", N'" & deptwiseprobj.ReferenceNo & "') Select @@Identity"
            Dim Detailquery As String
            For Each obj As DepartmentWiseProductionDetailsModel In deptwiseprobj.Detail
                Detailquery = "insert into DepartmentWiseProductionDetail(DepartmentWiseProductionID, SalesOrderID, PlanID, TicketID, ArticleID, Qty, Remarks, DepartmentID, LocationId) Values(`, " & obj.SalesOrderID & ", " & obj.PlanID & ", " & obj.TicketID & ", " & obj.ArticleID & ", " & Val(obj.Qty) & ", N'" & obj.Remarks & "', " & obj.DepartmentID & ", " & obj.LocationId & ")"
                DetailQueryList.Add(Detailquery)
            Next
            DwpDal.Master_Insertion(MasterQuery, DetailQueryList)
            Return DetailQueryList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMaster() As DataTable
        Try
            Dim MasterQuery As String = "Select DWP.ID, DWP.DocNo, DWP.Date, DWP.DepartmentID, tblproSteps.prod_step As Department, DWP.SpecialInstructions, DWP.ReferenceNo from DepartmentWiseProduction DWP left Join tblproSteps on DWP.DepartmentID=tblproSteps.prodStep_Id Order By DWP.Date DESC"
            DwpDal = New DepartmentWiseproductionDAL()
            Dim table As DataTable = DwpDal.ReadTable(MasterQuery)
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetails(ByVal ID As Integer) As DataTable
        Try
            Dim table As New DataTable
            'Dim DetailQuery As String = "Select DWPD.ID, DWPD.DepartmentWiseProductionID, DWPD.SalesOrderID, DWPD.PlanID,DWPD.TicketID, DWPD.ArticleID, ArticleDefTableMaster.ArticleCode, ArticleDefTableMaster.ArticleDescription, ArticleUnitDefTable.ArticleUnitName As UnitName, DWPD.Qty, DWPD.Remarks, IsNull(DWPD.DepartmentID, 0) As DepartmentID from DepartmentWiseProductionDetail DWPD left Join ArticleDefTableMaster on DWPD.ArticleID=ArticleDefTableMaster.ArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Where DWPD.DepartmentWiseProductionID = " & ID & ""
            ''TASK TFS1083 replaced ArticleDefTableMaster with ArticleDefTable 
            Dim DetailQuery As String = "Select DWPD.ID, DWPD.DepartmentWiseProductionID, DWPD.SalesOrderID, DWPD.PlanID,DWPD.TicketID, DWPD.ArticleID, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleUnitDefTable.ArticleUnitName As UnitName, DWPD.Qty, DWPD.Remarks, IsNull(DWPD.DepartmentID, 0) As DepartmentID, IsNull(DWPD.LocationId, 0) As LocationId from DepartmentWiseProductionDetail DWPD left Join ArticleDefTable on DWPD.ArticleID=ArticleDefTable.ArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Where DWPD.DepartmentWiseProductionID = " & ID & ""
            ''End TASK TFS1083
            DwpdDal = New DepartmentWiseProductionDetailDAL()
            table = DwpdDal.ReadTable(DetailQuery)
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal DwpId As Integer)
        DwpDal = New DepartmentWiseproductionDAL()
        DwpdDal = New DepartmentWiseProductionDetailDAL()
        Dim MasterQuery As String = "Delete from DepartmentWiseProduction where ID=" & DwpId
        Dim DetailQuery As String = "Delete from DepartmentWiseProductionDetail  where DepartmentWiseProductionID =" & DwpId
        DwpDal.Master_Deletion(MasterQuery)
        DwpdDal.Master_Deletion(DetailQuery)
        Return True
    End Function
    Public Function Update(ByVal DWPM As DepartmentWiseProductionModel) As List(Of String)
        Try
            Dim DetailQueryList As List(Of String) = New List(Of String)
            DwpDal = New DepartmentWiseproductionDAL()
            DwpdDal = New DepartmentWiseProductionDetailDAL()
            Dim MasterQuery As String = "update  DepartmentWiseProduction set DocNo = N'" & DWPM.DocNo & "', Date = N'" & DWPM.DwpDate.ToString("yyyy-M-d hh:mm:ss tt") & "', SpecialInstructions= N'" & DWPM.SpecialInstructions & "', DepartmentID= " & DWPM.DepartmentID & ", ReferenceNo= N'" & DWPM.ReferenceNo & "' where ID=" & DWPM.ID
            Dim Detailquery As String
            For Each obj As DepartmentWiseProductionDetailsModel In DWPM.Detail
                Detailquery = "insert into DepartmentWiseProductionDetail(DepartmentWiseProductionID, SalesOrderID, PlanID, TicketID, ArticleID, Qty, Remarks, DepartmentID, LocationId) Values(`, " & obj.SalesOrderID & ", " & obj.PlanID & ", " & obj.TicketID & ", " & obj.ArticleID & ", " & obj.Qty & ", N'" & obj.Remarks & "', " & obj.DepartmentID & ", " & obj.LocationId & ")"
                DetailQueryList.Add(Detailquery)
            Next
            Dim Update_Detail_Del As String = "Delete from  DepartmentWiseProductionDetail where DepartmentWiseProductionID =" & DWPM.ID
            DwpDal.Master_Update(MasterQuery)
            'DB_Detail_Insertion(MasterID, trans, Detailquery)
            DwpdDal.Detail_Update(DWPM.ID, DetailQueryList, Update_Detail_Del)
            Return DetailQueryList
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Sub DeleteDetailRow(ByVal DetailID As Integer)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        Dim Str As String = String.Empty
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Conn.Open()
        objCommand.Connection = Conn

        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            objCommand.Transaction = trans
            objCommand.CommandText = "Delete FROM DepartmentWiseProductionDetail Where ID =" & DetailID & ""
            objCommand.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS2780 done on 15-03-2018
    ''' </summary>
    ''' <param name="TicketId"></param>
    ''' <param name="StageId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsStageApproved(ByVal TicketId As Integer, ByVal StageId As Integer) As Boolean
        Try
            Dim table As New DataTable
            Dim DetailQuery As String = "If Exists(SELECT COUNT(CASE WHEN QCVerificationRequired = 0 THEN NULL ELSE QCVerificationRequired END) AS Count1 FROM tblproSteps WHERE ProdStep_Id = " & StageId & " HAVING COUNT(CASE WHEN QCVerificationRequired = 0 THEN NULL ELSE QCVerificationRequired END) > 0) " _
                                        & " SELECT ISNULL(ResultStatus, 0) AS Approved FROM LabResultMasterTable AS Result INNER JOIN LabTestSampleObvMaster AS Observation ON Result.QCId = Observation.ObserverSampleMasterID INNER JOIN LabRequestMasterTable AS LabRequest ON Observation.TestReq_Id = LabRequest.TestReq_Id WHERE LabRequest.StageId = " & StageId & " AND LabRequest.TicketId = " & TicketId & "" _
                                        & " ELSE SELECT 1 "
            table = UtilityDAL.GetDataTable(DetailQuery)
            If table.Rows.Count > 0 Then
                If table.Rows(0).Item(0) > 0 Then
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


End Class

