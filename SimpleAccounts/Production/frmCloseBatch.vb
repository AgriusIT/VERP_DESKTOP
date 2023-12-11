Imports SBModel
Imports SBDal
Public Class frmCloseBatch
    Implements IGeneral
    Dim CBObject As BECloseBatch
    Dim CBDAL As CloseBatchDAL
    Dim CloseBatchId As Integer = 0
    Dim ProductionId As Integer = 0
    Dim ProductionNo As String = ""
    Dim IsFormLoaded As Boolean = False
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdStageProduction.RootTable.Columns("ArticleDescription").Width = 250
            Me.grdStageProduction.RootTable.Columns("ArticleId").Visible = False
            Me.grdStageProduction.RootTable.Columns("SubSubId").Visible = False
            Me.grdStageProduction.RootTable.Columns("LocationId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            'Dim Voucher As New VouchersMaster()
            'Voucher.VoucherNo = Me.
            If New CloseBatchDAL().Delete(Val(Me.grdSaved.CurrentRow.Cells("CloseBatchId").Value.ToString), Me.grdSaved.CurrentRow.Cells("ProductionNo").Value.ToString) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, "", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String = String.Empty
        Try
            If Condition = "" Then

            ElseIf Condition = "PlanUpdateMode" Then
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable Order By PlanId DESC"

                FillDropDown(cmbPlan, Str)

            ElseIf Condition = "Plan" Then
                'Str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable Order By PlanId DESC"


                Str = "Select Distinct PlanMasterTable.PlanId, PlanMasterTable.PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo " _
                      & "From PlanMasterTable " _
                      & "INNER JOIN PlanTicketsMaster ON  PlanTicketsMaster.PlanID = PlanMasterTable.PlanId " _
                      & "WHERE PlanMasterTable.PlanId NOT IN (SELECT PlanID FROM CloseBatch) OR PlanTicketsMaster.PlanTicketsMasterID NOT IN (SELECT TicketId FROM CloseBatch) " _
                      & "Order By PlanMasterTable.PlanId DESC"


                FillDropDown(cmbPlan, Str)

            ElseIf Condition = "TicketUpdateMode" Then
                Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId, ISNULL(PlanTicketsMaster.CostCenterId, 0) AS CostCenterId, IsNull(ProductionProcess.WIPAccountId, 0) AS WIPAccountId " _
                    & " FROM PlanTicketsMaster LEFT OUTER JOIN (SELECT ISNULL(ProductionProcess.WIPAccountId, 0) AS WIPAccountId, ISNULL(Article.ArticleId, 0) AS ArticleId FROM ProductionProcess INNER JOIN ArticleDefTableMaster AS Article ON ProductionProcess.ProductionProcessId = Article.ProductionProcessId ) AS ProductionProcess ON  PlanTicketsMaster.MasterArticleId = ProductionProcess.ArticleId Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.SelectedValue & "  Order By PlanTicketsMaster.PlanTicketsMasterID DESC"

                FillDropDown(cmbTicket, Str)

            ElseIf Condition = "Ticket" Then
                'Str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId, ISNULL(PlanTicketsMaster.CostCenterId, 0) AS CostCenterId FROM PlanTicketsMaster Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.SelectedValue & "  Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                Str = " Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.BatchNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], " _
                       & " PlanTicketsMaster.PlanId, ISNULL(PlanTicketsMaster.CostCenterId, 0) AS CostCenterId, IsNull(ProductionProcess.WIPAccountId, 0) AS WIPAccountId " _
                       & " From PlanMasterTable " _
                       & " INNER JOIN PlanTicketsMaster ON  PlanTicketsMaster.PlanID = PlanMasterTable.PlanId " _
                       & " LEFT OUTER JOIN (SELECT ISNULL(ProductionProcess.WIPAccountId, 0) AS WIPAccountId, ISNULL(Article.ArticleId, 0) AS ArticleId FROM ProductionProcess INNER JOIN ArticleDefTableMaster AS Article ON ProductionProcess.ProductionProcessId = Article.ProductionProcessId ) AS ProductionProcess ON  PlanTicketsMaster.MasterArticleId = ProductionProcess.ArticleId " _
                       & " WHERE (PlanMasterTable.PlanId NOT IN (SELECT PlanID FROM CloseBatch) OR PlanTicketsMaster.PlanTicketsMasterID NOT IN (SELECT TicketId FROM CloseBatch)) " _
                       & "And (PlanMasterTable.PlanId = " & Me.cmbPlan.SelectedValue & ") " _
                       & "Order By PlanTicketsMaster.PlanTicketsMasterID DESC"


                FillDropDown(cmbTicket, Str)
            ElseIf Condition = "TicketProduct" Then
                'Str = "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleDescription Item, ArticleDefTable.ArticleCode Code, ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(PlanTicketsDetail.PlanTicketsDetailID, 0) As PlanTicketsDetailID, Sum(PlanTicketsDetail.Quantity) As Qty FROM ArticleDefTable INNER JOIN PlanTicketsDetail ON ArticleDefTable.ArticleId = PlanTicketsDetail.ArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where PlanTicketsDetail.PlanTicketsMasterID = " & Me.cmbTicket.SelectedValue & " Group By ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleCode, PlanTicketsDetail.PlanTicketsDetailID, ArticleUnitDefTable.ArticleUnitName "
                Str = "SELECT ArticleDefTableMaster.ArticleId as Id, ArticleDefTableMaster.ArticleDescription Item, ArticleDefTableMaster.ArticleCode Code, ArticleUnitDefTable.ArticleUnitName As UnitName , isNull(PlanTicketsMaster.batchsize,0)*isNull(PlanTicketsMaster.Noofbatches,0) as Qty FROM ArticleDefTableMaster INNER JOIN PlanTicketsMaster ON ArticleDefTableMaster.ArticleId = PlanTicketsMaster.MasterArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where PlanTicketsMaster.PlanTicketsMasterID = " & Me.cmbTicket.SelectedValue & " Group By ArticleDefTableMaster.ArticleId, ArticleDefTableMaster.ArticleDescription, ArticleDefTableMaster.ArticleCode, ArticleUnitDefTable.ArticleUnitName, PlanTicketsMaster.batchsize , PlanTicketsMaster.Noofbatches"
                FillUltraDropDown(Me.cmbProductionItem, Str)
                Me.cmbProductionItem.Rows(0).Activate()
                If Me.cmbProductionItem.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbProductionItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    'Me.cmbProductionItem.DisplayLayout.Bands("WIPAccountId").Columns(0).Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Dim CostCenterId As Integer = 0
        Try
            CBObject = New BECloseBatch()
            CBObject.CloseBatchId = CloseBatchId
            CBObject.PlanId = Me.cmbPlan.SelectedValue
            CBObject.TicketId = Me.cmbTicket.SelectedValue
            CBObject.ProductId = Me.cmbProductionItem.Value
            CBObject.BatchNo = Me.cmbBatchNo.Text
            CBObject.IsClosedBatch = Me.cbCloseBatch.Checked



            If Me.cbCloseBatch.Checked = True Then
                ''TAST TFS2784 -> Production entry
                CBObject.Production.ProductionId = ProductionId
                CBObject.Production.Production_No = ProductionNo
                CBObject.Production.Production_Date = Now
                CBObject.Production.UserName = LoginUserName
                CBObject.Production.Remarks = String.Empty
                CBObject.Production.Post = False
                CBObject.Production.IGPNo = ""
                CBObject.Production.RefDispatchNo = ""
                CBObject.Production.RefDocument = ""
                CBObject.Production.FDate = Now
                CBObject.Production.ProductionDetail = New List(Of ProductionDetail)
                CBObject.Production.StockMaster = New StockMaster()
                CBObject.Production.StockMaster.DocDate = Now
                CBObject.Production.StockMaster.DocNo = ProductionNo
                CBObject.Production.StockMaster.AccountId = 0
                CBObject.Production.StockMaster.Project = 0
                CBObject.Production.StockMaster.Remaks = ""
                CBObject.Production.StockMaster.StockTransId = 0
                CBObject.Production.StockMaster.StockDetailList = New List(Of StockDetail)
                ''End TASK TFS2784
            End If
            ''TASK TFS2784 -> Voucher entry
            If Me.cbCloseBatch.Checked = True Then
                CBObject.Voucher.VoucherNo = CBObject.Production.Production_No
                CBObject.Voucher.VNo = CBObject.Production.Production_No
                CBObject.Voucher.VoucherDate = Now
                CBObject.Voucher.VoucherCode = CBObject.Production.Production_No
                CBObject.Voucher.Source = Me.Name
                CBObject.Voucher.UserName = LoginUserName
                CBObject.Voucher.LocationId = 0
                CBObject.Voucher.FinancialYearId = 1
                CBObject.Voucher.VoucherTypeId = 1
                CBObject.Voucher.VoucherMonth = String.Empty
                CBObject.Voucher.CoaDetailId = 0
                CBObject.Voucher.Post = True
                CBObject.Voucher.References = String.Empty
                CBObject.Voucher.Posted_UserName = LoginUserName
                CBObject.Voucher.ActivityLog = New ActivityLog()
                CBObject.Voucher.ActivityLog.FormName = Me.Name
                CBObject.Voucher.ActivityLog.FormCaption = "Close Batch"
                CBObject.Voucher.ActivityLog.User_Name = LoginUserName
                CBObject.Voucher.ActivityLog.UserID = LoginUserId
                CBObject.Voucher.VoucherDatail = New List(Of VouchersDetail)
            End If

            ''END TASK TFS2784

            ''TASK TFS2784 -> WIPAccount Entry
            CostCenterId = Val(CType(Me.cmbTicket.SelectedItem, DataRowView).Item("CostCenterId").ToString)
            If Me.cbCloseBatch.Checked = True Then
                '   contra_coa_detail_id,EmpId
                Dim voucherDetail As New VouchersDetail
                Dim WIPAccountId As Integer = Val(CType(Me.cmbTicket.SelectedItem, DataRowView).Row.Item("WIPAccountId").ToString)
                voucherDetail.VoucherId = 0
                voucherDetail.LocationId = 1
                voucherDetail.CoaDetailId = WIPAccountId
                voucherDetail.Comments = " " & Me.cmbProductionItem.Text & " ,  Work In Process "
                'voucherDetail.DebitAmount = (Val(LblPerUnitRate.Text) * Val(LblUnitProduced.Text)) - (Val(Me.grdByProduct.GetTotal(Me.grdByProduct.RootTable.Columns("Value"), Janus.Windows.GridEX.AggregateFunction.Sum)) + Val(Me.grdByProduct.GetTotal(Me.grdOverHeads.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))
                voucherDetail.CreditAmount = Val(Me.grdMaterialCost.GetTotal(Me.grdMaterialCost.RootTable.Columns("Value"), Janus.Windows.GridEX.AggregateFunction.Sum))
                voucherDetail.DebitAmount = 0
                'voucherDetail. = (Val(LblPerUnitRate.Text) * Val(LblUnitProduced.Text)) - (Val(Me.grdByProduct.GetTotal(Me.grdByProduct.RootTable.Columns("Value"), Janus.Windows.GridEX.AggregateFunction.Sum)) + Val(Me.grdByProduct.GetTotal(Me.grdOverHeads.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))
                'voucherDetail.CreditAmount = 0
                voucherDetail.CostCenter = 0
                voucherDetail.SPReference = String.Empty
                voucherDetail.Cheque_No = String.Empty
                voucherDetail.Cheque_Date = Nothing
                voucherDetail.PayeeTitle = String.Empty
                voucherDetail.ChequeDescription = String.Empty
                voucherDetail.contra_coa_detail_id = 0
                voucherDetail.EmpId = Nothing
                voucherDetail.CostCenter = CostCenterId
                CBObject.Voucher.VoucherDatail.Add(voucherDetail)
            End If
            ''MC
            Me.grdMaterialCost.UpdateData()
            If Me.grdMaterialCost.RowCount > 0 Then
                For Each Detail As Janus.Windows.GridEX.GridEXRow In grdMaterialCost.GetRows
                    Dim CloseBatchMCDetail As New BECloseBatchMCDetail()
                    CloseBatchMCDetail.CloseBatchMCDetailId = Detail.Cells("CloseBatchMCDetailId").Value
                    CloseBatchMCDetail.CloseBatchId = Detail.Cells("CloseBatchId").Value
                    CloseBatchMCDetail.ArticleId = Detail.Cells("ArticleId").Value
                    CloseBatchMCDetail.ItemConsumptionDetailId = Detail.Cells("ItemConsumptionDetailId").Value
                    CloseBatchMCDetail.Qty = Detail.Cells("Qty").Value
                    CloseBatchMCDetail.Rate = Detail.Cells("Rate").Value
                    CBObject.CloseBatchMCDetail.Add(CloseBatchMCDetail)
                Next
            End If
            ''DE
            Me.grdDirectExpense.UpdateData()
            If Me.grdDirectExpense.RowCount > 0 Then
                For Each Detail As Janus.Windows.GridEX.GridEXRow In grdDirectExpense.GetRows
                    Dim CloseBatchDEDetail As New BECloseBatchDEDetail()
                    CloseBatchDEDetail.CloseBatchDEDetailId = Detail.Cells("CloseBatchDEDetailId").Value
                    CloseBatchDEDetail.CloseBatchId = Detail.Cells("CloseBatchId").Value
                    CloseBatchDEDetail.AccountId = Detail.Cells("AccountId").Value
                    CloseBatchDEDetail.Amount = Detail.Cells("Amount").Value
                    CBObject.CloseBatchDEDetail.Add(CloseBatchDEDetail)
                Next
            End If
            ''OH
            Me.grdOverHeads.UpdateData()
            If Me.grdOverHeads.RowCount > 0 Then
                For Each Detail As Janus.Windows.GridEX.GridEXRow In grdOverHeads.GetRows
                    Dim CloseBatchOHDetail As New BECloseBatchOHDetail()
                    CloseBatchOHDetail.CloseBatchOHDetailId = Detail.Cells("CloseBatchOHDetailId").Value
                    CloseBatchOHDetail.CloseBatchId = Detail.Cells("CloseBatchId").Value
                    CloseBatchOHDetail.AccountId = Detail.Cells("AccountId").Value
                    CloseBatchOHDetail.Amount = Detail.Cells("Amount").Value
                    CloseBatchOHDetail.ProductionOverHeadsId = 0
                    CBObject.CloseBatchOHDetail.Add(CloseBatchOHDetail)
                    If Me.cbCloseBatch.Checked = True Then
                        '     contra_coa_detail_id,EmpId
                        Dim voucherDetail As New VouchersDetail
                        voucherDetail.VoucherId = 0
                        voucherDetail.LocationId = 1
                        voucherDetail.CoaDetailId = Val(Detail.Cells("AccountId").Value.ToString)
                        voucherDetail.Comments = "Close Batch : Over heads "
                        voucherDetail.DebitAmount = 0
                        voucherDetail.CreditAmount = Detail.Cells("Amount").Value
                        voucherDetail.CostCenter = 0
                        voucherDetail.SPReference = String.Empty
                        voucherDetail.Cheque_No = String.Empty
                        voucherDetail.Cheque_Date = Nothing
                        voucherDetail.PayeeTitle = String.Empty
                        voucherDetail.ChequeDescription = String.Empty
                        voucherDetail.contra_coa_detail_id = 0
                        voucherDetail.EmpId = Nothing
                        voucherDetail.CostCenter = CostCenterId
                        CBObject.Voucher.VoucherDatail.Add(voucherDetail)

                    End If
                Next
            End If
            ''LC
            Me.grdLabourCost.UpdateData()
            If Me.grdLabourCost.RowCount > 0 Then
                For Each Detail As Janus.Windows.GridEX.GridEXRow In grdLabourCost.GetRows
                    Dim CloseBatchLCDetail As New BECloseBatchLCDetail()
                    CloseBatchLCDetail.CloseBatchLCDetailId = Detail.Cells("CloseBatchLCDetailId").Value
                    CloseBatchLCDetail.CloseBatchId = Detail.Cells("CloseBatchId").Value
                    CloseBatchLCDetail.LabourTypeId = Detail.Cells("LabourTypeId").Value
                    CloseBatchLCDetail.Amount = Detail.Cells("Amount").Value
                    CBObject.CloseBatchLCDetail.Add(CloseBatchLCDetail)
                    If Me.cbCloseBatch.Checked = True Then
                        'contra_coa_detail_id,EmpId
                        Dim voucherDetail As New VouchersDetail
                        voucherDetail.VoucherId = 0
                        voucherDetail.LocationId = 1
                        voucherDetail.CoaDetailId = Val(Detail.Cells("AccountId").Value.ToString)
                        voucherDetail.Comments = "Close Batch : Labour cost "
                        voucherDetail.DebitAmount = 0
                        voucherDetail.CreditAmount = Detail.Cells("Amount").Value
                        voucherDetail.CostCenter = 0
                        voucherDetail.SPReference = String.Empty
                        voucherDetail.Cheque_No = String.Empty
                        voucherDetail.Cheque_Date = Nothing
                        voucherDetail.PayeeTitle = String.Empty
                        voucherDetail.ChequeDescription = String.Empty
                        voucherDetail.contra_coa_detail_id = 0
                        voucherDetail.EmpId = Nothing
                        voucherDetail.CostCenter = CostCenterId
                        CBObject.Voucher.VoucherDatail.Add(voucherDetail)
                    End If
                Next
            End If
            ''ByProduct
            Me.grdByProduct.UpdateData()
            If Me.grdByProduct.RowCount > 0 Then
                For Each Detail As Janus.Windows.GridEX.GridEXRow In grdByProduct.GetRows
                    Dim CloseBatchByProductDetail As New BECloseBatchByProductDetail()
                    CloseBatchByProductDetail.CloseBatchByProductDetailId = Detail.Cells("CloseBatchByProductDetailId").Value
                    CloseBatchByProductDetail.CloseBatchId = Detail.Cells("CloseBatchId").Value
                    CloseBatchByProductDetail.ByProductsId = Detail.Cells("ByProductsId").Value
                    CloseBatchByProductDetail.ArticleId = Detail.Cells("ArticleId").Value
                    CloseBatchByProductDetail.Qty = Detail.Cells("Qty").Value
                    CloseBatchByProductDetail.Rate = Detail.Cells("Rate").Value
                    CBObject.CloseBatchByProductDetail.Add(CloseBatchByProductDetail)
                    ''TASK TFS2784 Production entry
                    If Me.cbCloseBatch.Checked = True Then
                        Dim ProductionDetail As New ProductionDetail()
                        ProductionDetail.ArticledefID = Detail.Cells("ArticleId").Value
                        ProductionDetail.Location_ID = 1
                        ProductionDetail.CurrentRate = Detail.Cells("Rate").Value
                        ProductionDetail.Qty = Detail.Cells("Qty").Value
                        ProductionDetail.Sz1 = Detail.Cells("Qty").Value
                        ProductionDetail.ArticleSize = ""
                        ProductionDetail.UOM = String.Empty
                        ProductionDetail.Comments = ""
                        ProductionDetail.BatchNo = ""
                        ProductionDetail.EngineNo = ""
                        ProductionDetail.ChasisNo = ""
                        ProductionDetail.Pack_Desc = ""
                        CBObject.Production.ProductionDetail.Add(ProductionDetail)
                    End If
                    ''End TASK TFS2784
                    ''End TASK TFS2784
                    If Me.cbCloseBatch.Checked = True Then
                        '     contra_coa_detail_id,EmpId
                        Dim voucherDetail As New VouchersDetail
                        voucherDetail.VoucherId = 0
                        voucherDetail.LocationId = 1
                        voucherDetail.CoaDetailId = Val(Detail.Cells("SubSubId").Value.ToString)
                        voucherDetail.Comments = " " & Detail.Cells("ArticleDescription").Value.ToString & "( " & Detail.Cells("Qty").Value & " X " & Detail.Cells("Rate").Value & " )" & ""
                        voucherDetail.DebitAmount = Val(Detail.Cells("Value").Value.ToString)
                        voucherDetail.CreditAmount = 0
                        voucherDetail.CostCenter = 0
                        voucherDetail.SPReference = String.Empty
                        voucherDetail.Cheque_No = String.Empty
                        voucherDetail.Cheque_Date = Nothing
                        voucherDetail.PayeeTitle = String.Empty
                        voucherDetail.ChequeDescription = String.Empty
                        voucherDetail.contra_coa_detail_id = 0
                        voucherDetail.EmpId = Nothing
                        voucherDetail.CostCenter = CostCenterId
                        CBObject.Voucher.VoucherDatail.Add(voucherDetail)
                    End If
                Next
            End If
            ''Finish Goods
            Me.grdFinishGoods.UpdateData()
            If Me.grdFinishGoods.RowCount > 0 Then
                For Each Detail As Janus.Windows.GridEX.GridEXRow In grdFinishGoods.GetRows
                    Dim CloseBatchFinishGoodsDetail As New BECloseBatchFinishGoodsDetail()
                    CloseBatchFinishGoodsDetail.CloseBatchFinishGoodsDetailId = Detail.Cells("CloseBatchFinishGoodsDetailId").Value
                    CloseBatchFinishGoodsDetail.CloseBatchId = Detail.Cells("CloseBatchId").Value
                    CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = Detail.Cells("DepartmentWiseProductionDetailId").Value
                    CloseBatchFinishGoodsDetail.ArticleId = Detail.Cells("ArticleId").Value
                    CloseBatchFinishGoodsDetail.Qty = Detail.Cells("Qty").Value
                    CBObject.CloseBatchFinishGoodsDetail.Add(CloseBatchFinishGoodsDetail)
                Next
            End If
            If Me.grdDetail.RowCount > 0 Then
                For Each Detail As Janus.Windows.GridEX.GridEXRow In grdDetail.GetRows
                    Dim QtyPer As Double = Detail.Cells("Quantity").Value * 100 / Val(LblUnitProduced.Text)
                    Dim ActualQty As Double = Val(LblUnitProduced.Text) * QtyPer / 100
                    Dim CBDetail As New BECloseBatchDetail()
                    CBDetail.ID = Val(Detail.Cells("ID").Value.ToString)
                    CBDetail.CloseBatchId = Val(Detail.Cells("CloseBatchId").Value.ToString)
                    CBDetail.LocationId = Val(Detail.Cells("LocationId").Value.ToString)
                    CBDetail.ArticleId = Val(Detail.Cells("ArticleId").Value.ToString)
                    CBDetail.PackingId = Val(Detail.Cells("PackingId").Value.ToString)
                    CBDetail.Quantity = Val(Detail.Cells("Quantity").Value.ToString)
                    CBObject.CloseBatchDetail.Add(CBDetail)
                    If Me.cbCloseBatch.Checked = True Then
                        Dim ProductionDetail As New ProductionDetail()
                        ProductionDetail.ArticledefID = Detail.Cells("ArticleId").Value
                        ProductionDetail.Location_ID = Detail.Cells("LocationId").Value
                        ProductionDetail.CurrentRate = LblPerUnitRate.Text
                        ProductionDetail.Qty = ActualQty
                        ProductionDetail.Sz1 = ActualQty
                        ProductionDetail.ArticleSize = ""
                        ProductionDetail.UOM = String.Empty
                        ProductionDetail.Comments = ""
                        ProductionDetail.BatchNo = ""
                        ProductionDetail.EngineNo = ""
                        ProductionDetail.ChasisNo = ""
                        ProductionDetail.Pack_Desc = ""
                        CBObject.Production.ProductionDetail.Add(ProductionDetail)
                        Dim StockDetail As New StockDetail()
                        StockDetail.LocationId = Detail.Cells("LocationId").Value
                        StockDetail.Remarks = ""
                        StockDetail.ArticleDefId = Detail.Cells("ArticleId").Value
                        StockDetail.BatchNo = ""
                        StockDetail.Chassis_No = ""
                        StockDetail.Engine_No = ""
                        StockDetail.InQty = ActualQty
                        StockDetail.OutQty = 0
                        StockDetail.OutAmount = 0
                        StockDetail.Rate = LblPerUnitRate.Text
                        StockDetail.InAmount = StockDetail.InQty * StockDetail.Rate
                        StockDetail.StockTransId = 0
                        CBObject.Production.StockMaster.StockDetailList.Add(StockDetail)
                    End If
                    If Me.cbCloseBatch.Checked = True Then
                        Dim ActualValue As Double = 0
                        Dim voucherDetail As New VouchersDetail
                        voucherDetail.VoucherId = 0
                        voucherDetail.LocationId = Detail.Cells("LocationId").Value
                        voucherDetail.CoaDetailId = Val(Detail.Cells("SubSubId").Value.ToString)
                        voucherDetail.Comments = " Close Batch : Stage Wise Production : Product " & Detail.Cells("ArticleDescription").Value.ToString & ""
                        voucherDetail.DebitAmount = (Me.grdMaterialCost.GetTotal(Me.grdMaterialCost.RootTable.Columns("Value"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdLabourCost.GetTotal(Me.grdLabourCost.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdOverHeads.GetTotal(Me.grdOverHeads.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - (Me.grdByProduct.GetTotal(Me.grdByProduct.RootTable.Columns("Value"), Janus.Windows.GridEX.AggregateFunction.Sum))
                        Dim UnitProduced As Double = LblUnitProduced.Text
                        ActualValue = voucherDetail.DebitAmount / UnitProduced
                        voucherDetail.DebitAmount = ActualValue * ActualQty
                        voucherDetail.CreditAmount = 0
                        voucherDetail.CostCenter = 0
                        voucherDetail.SPReference = String.Empty
                        voucherDetail.Cheque_No = String.Empty
                        voucherDetail.Cheque_Date = Nothing
                        voucherDetail.PayeeTitle = String.Empty
                        voucherDetail.ChequeDescription = String.Empty
                        voucherDetail.contra_coa_detail_id = 0
                        voucherDetail.EmpId = Nothing
                        voucherDetail.CostCenter = CostCenterId
                        CBObject.Voucher.VoucherDatail.Add(voucherDetail)
                    End If
                Next
            End If
            'If Me.grdStageProduction.RowCount > 0 Then
            '    For Each Detail As Janus.Windows.GridEX.GridEXRow In grdStageProduction.GetRows
            '        If Me.cbCloseBatch.Checked = True Then
            '            Dim ProductionDetail As New ProductionDetail()
            '            ProductionDetail.ArticledefID = Detail.Cells("ArticleId").Value
            '            ProductionDetail.Location_ID = Detail.Cells("LocationId").Value
            '            ProductionDetail.CurrentRate = LblPerUnitRate.Text
            '            ProductionDetail.Qty = LblUnitProduced.Text
            '            ProductionDetail.Sz1 = LblUnitProduced.Text
            '            ProductionDetail.ArticleSize = ""
            '            ProductionDetail.UOM = String.Empty
            '            ProductionDetail.Comments = ""
            '            ProductionDetail.BatchNo = ""
            '            ProductionDetail.EngineNo = ""
            '            ProductionDetail.ChasisNo = ""
            '            ProductionDetail.Pack_Desc = ""
            '            CBObject.Production.ProductionDetail.Add(ProductionDetail)
            '            '' Stock entry
            '            Dim StockDetail As New StockDetail()
            '            StockDetail.LocationId = Detail.Cells("LocationId").Value
            '            StockDetail.Remarks = ""
            '            StockDetail.ArticleDefId = Detail.Cells("ArticleId").Value
            '            StockDetail.BatchNo = ""
            '            StockDetail.Chassis_No = ""
            '            StockDetail.Engine_No = ""
            '            StockDetail.InQty = LblUnitProduced.Text
            '            'StockDetail.InAmount = (Val(LblUnitProduced.Text) * Val(LblPerUnitRate.Text))
            '            StockDetail.OutQty = 0
            '            StockDetail.OutAmount = 0
            '            StockDetail.Rate = LblPerUnitRate.Text
            '            StockDetail.InAmount = StockDetail.InQty * StockDetail.Rate
            '            StockDetail.StockTransId = 0
            '            CBObject.Production.StockMaster.StockDetailList.Add(StockDetail)
            '            '' End Stock entry
            '        End If
            '        If Me.cbCloseBatch.Checked = True Then
            '            '     contra_coa_detail_id,EmpId
            '            Dim voucherDetail As New VouchersDetail
            '            voucherDetail.VoucherId = 0
            '            voucherDetail.LocationId = Detail.Cells("LocationId").Value
            '            voucherDetail.CoaDetailId = Val(Detail.Cells("SubSubId").Value.ToString)
            '            voucherDetail.Comments = " Close Batch : Stage Wise Production : Product " & Detail.Cells("ArticleDescription").Value.ToString & ""
            '            'voucherDetail.DebitAmount = Val(LblPerUnitRate.Text) * Val(LblUnitProduced.Text)
            '            voucherDetail.DebitAmount = (Me.grdMaterialCost.GetTotal(Me.grdMaterialCost.RootTable.Columns("Value"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdLabourCost.GetTotal(Me.grdLabourCost.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdOverHeads.GetTotal(Me.grdOverHeads.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - (Me.grdByProduct.GetTotal(Me.grdByProduct.RootTable.Columns("Value"), Janus.Windows.GridEX.AggregateFunction.Sum))
            '            voucherDetail.CreditAmount = 0
            '            voucherDetail.CostCenter = 0
            '            voucherDetail.SPReference = String.Empty
            '            voucherDetail.Cheque_No = String.Empty
            '            voucherDetail.Cheque_Date = Nothing
            '            voucherDetail.PayeeTitle = String.Empty
            '            voucherDetail.ChequeDescription = String.Empty
            '            voucherDetail.contra_coa_detail_id = 0
            '            voucherDetail.EmpId = Nothing
            '            voucherDetail.CostCenter = CostCenterId
            '            CBObject.Voucher.VoucherDatail.Add(voucherDetail)
            '        End If
            '        'Exit For
            '    Next
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdSaved.DataSource = CloseBatchDAL.GetAll()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("PlanId").Visible = False
            Me.grdSaved.RootTable.Columns("CloseBatchId").Visible = False
            Me.grdSaved.RootTable.Columns("TicketId").Visible = False
            Me.grdSaved.RootTable.Columns("ProductId").Visible = False
            Me.grdSaved.RootTable.Columns("ProductionId").Visible = False
            'Me.grdSaved.RootTable.Columns("ProductionNo").Visible = False
            Me.grdSaved.RootTable.Columns("IsClosedBatch").Caption = "Close Batch"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Dim AreAllGridsEmpties As Boolean = False
        Try
            If Me.cmbTicket.SelectedIndex < 1 Then
                ShowErrorMessage("Please select ticket.")
                Return False
            End If
            grdMaterialCost.UpdateData()
            If Me.grdMaterialCost.RowCount > 0 Then
                AreAllGridsEmpties = True
            End If
            grdDirectExpense.UpdateData()
            If Me.grdDirectExpense.RowCount > 0 Then
                AreAllGridsEmpties = True
            End If

            grdOverHeads.UpdateData()
            If Me.grdOverHeads.RowCount > 0 Then
                AreAllGridsEmpties = True
            End If
            If Me.grdByProduct.RowCount > 0 Then
                AreAllGridsEmpties = True
            End If
            grdLabourCost.UpdateData()
            If Me.grdLabourCost.RowCount > 0 Then
                AreAllGridsEmpties = True
            End If
            grdFinishGoods.UpdateData()
            If Me.grdFinishGoods.RowCount > 0 Then
                AreAllGridsEmpties = True
            End If
            If AreAllGridsEmpties = False Then
                ShowErrorMessage("All grids are empty.")
                Return False
            End If
            If grdStageProduction.RowCount < 1 Then
                ShowErrorMessage("No record found in Stage Wise Production")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            GetSecurityRights()

            Dim Id As Integer = 0

            FillCombos("Plan")
            Me.cmbPlan.SelectedValue = Id
            Id = Me.cmbTicket.SelectedValue
            FillCombos("Ticket")
            Me.cmbTicket.SelectedValue = Id
            Id = Me.cmbProductionItem.Value
            FillCombos("TicketProduct")
            Me.cmbProductionItem.Value = Id


            If Not Me.cmbPlan.SelectedIndex = -1 Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            If Not Me.cmbTicket.SelectedIndex = -1 Then
                Me.cmbTicket.SelectedIndex = 0
            End If
            If Not Me.cmbProductionItem.ActiveRow Is Nothing Then
                Me.cmbProductionItem.Rows(0).Activate()
            End If

            Me.txtQty.Text = String.Empty
            Me.cmbBatchNo.Text = String.Empty
            GetAllRecords()
            DisplayMaterialCost(-1)
            DisplayDirectExpense(-1)
            DisplayOverHeads(-1)
            DisplayLabourCost(-1)
            DisplayByProducts(-1)
            DisplayFinishGoods(-1)
            DisplayStageWiseProductionGrid(-1)

            LblUnitProduced.Text = 0
            LblByProduct.Text = " - " & 0
            LblNetCost.Text = 0
            LblPerUnitRate.Text = 0
            LblTotalExpense.Text = 0
            LblUnitProduced.Text = 0

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New CloseBatchDAL().Save(CBObject) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, "", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub EditRecord()
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            CloseBatchId = Val(Me.grdSaved.GetRow.Cells("CloseBatchId").Value.ToString)
            FillCombos("PlanUpdateMode")
            Me.cmbPlan.SelectedValue = Val(Me.grdSaved.GetRow.Cells("PlanId").Value.ToString)
            FillCombos("TicketUpdateMode")
            Me.cmbTicket.SelectedValue = Val(Me.grdSaved.GetRow.Cells("TicketId").Value.ToString)
            FillCombos("TicketProduct")
            Me.cmbProductionItem.Value = Val(Me.grdSaved.GetRow.Cells("ProductId").Value.ToString)
            Me.cmbBatchNo.Text = Me.grdSaved.GetRow.Cells("BatchNo").Value.ToString
            Me.cbCloseBatch.Checked = Me.grdSaved.GetRow.Cells("IsClosedBatch").Value
            ProductionId = Val(Me.grdSaved.GetRow.Cells("ProductionId").Value.ToString)
            ProductionNo = Me.grdSaved.GetRow.Cells("ProductionNo").Value.ToString
            DisplayMaterialCost(CloseBatchId)
            DisplayDirectExpense(CloseBatchId)
            DisplayOverHeads(CloseBatchId)
            DisplayLabourCost(CloseBatchId)
            DisplayByProducts(CloseBatchId)
            DisplayFinishGoods(CloseBatchId)
            DisplayStageWiseProductionGrid(CloseBatchId)
            DispalyDetail(CloseBatchId)
            '(CloseBatchId)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If cbCloseBatch.Checked = True Then
                If CBObject.Production.Production_No.Length > 0 Then
                    CBObject.Production.StockMaster.StockTransId = StockTransId(CBObject.Production.Production_No)
                End If
            End If
            If New CloseBatchDAL().Update(CBObject) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, "", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmCloseBatch_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("Plan")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try
            If Not cmbPlan.SelectedIndex = -1 Then
                FillCombos("Ticket")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicket.SelectedIndexChanged
        Try
            If Not cmbTicket.SelectedIndex = -1 Then
                FillCombos("TicketProduct")
                GetMaterialCost(Me.cmbTicket.SelectedValue)
                GetDirectExpense(Val(CType(Me.cmbTicket.SelectedItem, DataRowView).Item("CostCenterId").ToString))
                GetOverHeads(Me.cmbTicket.SelectedValue)
                GetLabourCost(Me.cmbTicket.SelectedValue)
                GetByProducts(Me.cmbTicket.SelectedValue)
                GetFinishGoods(Me.cmbTicket.SelectedValue)
                GetStageWiseProductionGrid(Me.cmbTicket.SelectedValue)
                GetDetail(Me.cmbTicket.SelectedValue)
                If Me.cmbProductionItem.Rows.Count > 1 Then
                    Me.cmbProductionItem.Rows(1).Activate()
                    Me.txtQty.Text = Me.cmbProductionItem.ActiveRow.Cells(4).Value
                End If
                CalculateAmounts()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DisplayMaterialCost(ByVal CloseBatchId As Integer)
        Try
            Me.grdMaterialCost.DataSource = CloseBatchDAL.DisplayMaterialCost(CloseBatchId)

            'Me.grdMaterialCost.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.TextBox
            'Me.grdMaterialCost.RootTable.Columns("Rate").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdMaterialCost.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialCost.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.grdMaterialCost.RootTable.Columns("Value").FormatString = "N" & DecimalPointInValue
            Me.grdMaterialCost.RootTable.Columns("Value").TotalFormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDirectExpense(ByVal CloseBatchId As Integer)
        Try
            Me.grdDirectExpense.DataSource = CloseBatchDAL.DisplayDirectExpense(CloseBatchId)
            Me.grdDirectExpense.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdDirectExpense.RootTable.Columns("Amount").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDirectExpense.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayOverHeads(ByVal CloseBatchId As Integer)
        Try
            Me.grdOverHeads.DataSource = CloseBatchDAL.DisplayOverHeads(CloseBatchId)
            Me.grdOverHeads.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdOverHeads.RootTable.Columns("Amount").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdOverHeads.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayLabourCost(ByVal CloseBatchId As Integer)
        Try
            Me.grdLabourCost.DataSource = CloseBatchDAL.DisplayLabourCost(CloseBatchId)
            Me.grdLabourCost.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdLabourCost.RootTable.Columns("Amount").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdLabourCost.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayByProducts(ByVal CloseBatchId As Integer)
        Try
            Me.grdByProduct.DataSource = CloseBatchDAL.DisplayByProducts(CloseBatchId)
            Me.grdByProduct.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdByProduct.RootTable.Columns("Rate").EditType = Janus.Windows.GridEX.EditType.TextBox

            Me.grdByProduct.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdByProduct.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.grdByProduct.RootTable.Columns("Value").FormatString = "N" & DecimalPointInValue
            Me.grdByProduct.RootTable.Columns("Value").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayFinishGoods(ByVal CloseBatchId As Integer)
        Try
            Me.grdFinishGoods.DataSource = CloseBatchDAL.DisplayFinishGoods(CloseBatchId)
            Me.grdFinishGoods.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdFinishGoods.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DispalyDetail(ByVal CloseBatchId As Integer)
        Try
            Me.grdDetail.DataSource = CloseBatchDAL.DisplayDetail(CloseBatchId)
            Me.grdDetail.RootTable.Columns("Quantity").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDetail.RootTable.Columns("Quantity").FormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''Get
    Private Sub GetMaterialCost(ByVal CloseBatchId As Integer)
        Try
            Me.grdMaterialCost.DataSource = CloseBatchDAL.GetMaterialCost(CloseBatchId)
            Me.grdMaterialCost.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdMaterialCost.RootTable.Columns("Rate").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdMaterialCost.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialCost.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.grdMaterialCost.RootTable.Columns("Value").FormatString = "N" & DecimalPointInValue
            Me.grdMaterialCost.RootTable.Columns("Value").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetDirectExpense(ByVal CloseBatchId As Integer)
        Try
            Me.grdDirectExpense.DataSource = CloseBatchDAL.GetDirectExpense(CloseBatchId)
            Me.grdDirectExpense.RootTable.Columns("Amount").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDirectExpense.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdDirectExpense.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetOverHeads(ByVal CloseBatchId As Integer)
        Try
            Me.grdOverHeads.DataSource = CloseBatchDAL.GetOverHeads(CloseBatchId)
            Me.grdOverHeads.RootTable.Columns("Amount").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdOverHeads.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdOverHeads.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetLabourCost(ByVal CloseBatchId As Integer)
        Try
            Me.grdLabourCost.DataSource = CloseBatchDAL.GetLabourCost(CloseBatchId)
            'Me.grdLabourCost.RootTable.Columns("Rate").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdLabourCost.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdLabourCost.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetByProducts(ByVal CloseBatchId As Integer)
        Try
            Me.grdByProduct.DataSource = CloseBatchDAL.GetByProducts(CloseBatchId)
            Me.grdByProduct.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdByProduct.RootTable.Columns("Rate").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdByProduct.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdByProduct.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.grdByProduct.RootTable.Columns("Value").FormatString = "N" & DecimalPointInValue
            Me.grdByProduct.RootTable.Columns("Value").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetFinishGoods(ByVal CloseBatchId As Integer)
        Try
            Me.grdFinishGoods.DataSource = CloseBatchDAL.GetFinishGoods(CloseBatchId)
            Me.grdFinishGoods.RootTable.Columns("Qty").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdFinishGoods.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetDetail(ByVal CloseBatchId As Integer)
        Try
            Me.grdDetail.DataSource = CloseBatchDAL.GetDetail(CloseBatchId)
            Me.grdDetail.RootTable.Columns("Quantity").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDetail.RootTable.Columns("Quantity").FormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                If btnSave.Text = "&Save" Or btnSave.Text = "Save" Then
                    If Save() Then
                        ReSetControls()
                    End If

                Else
                    If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                    If Update1() Then
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
        If Delete() Then
            ReSetControls()
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbPlan.SelectedValue
            FillCombos("Plan")
            Me.cmbPlan.SelectedValue = Id
            Id = Me.cmbTicket.SelectedValue
            FillCombos("Ticket")
            Me.cmbTicket.SelectedValue = Id
            Id = Me.cmbProductionItem.Value
            FillCombos("TicketProduct")
            Me.cmbProductionItem.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                ''TASK TFS2664 tabs rights done on 07-03-2018
                UltraTabControl2.Tabs(0).Visible = True
                UltraTabControl2.Tabs(1).Visible = True
                UltraTabControl2.Tabs(2).Visible = True
                UltraTabControl2.Tabs(3).Visible = True
                UltraTabControl2.Tabs(4).Visible = True
                UltraTabControl2.Tabs(5).Visible = True
                UltraTabControl2.Tabs(6).Visible = True
                ''
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.btnSave.Enabled = False

                    ''TASK TFS2664 tabs rights done on 07-03-2018
                    UltraTabControl2.Tabs(0).Visible = False
                    UltraTabControl2.Tabs(1).Visible = False
                    UltraTabControl2.Tabs(2).Visible = False
                    UltraTabControl2.Tabs(3).Visible = False
                    UltraTabControl2.Tabs(4).Visible = False
                    UltraTabControl2.Tabs(5).Visible = False
                    UltraTabControl2.Tabs(6).Visible = False
                    ''
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False

                ''TASK TFS2664 tabs rights done on 07-03-2018
                UltraTabControl2.Tabs(0).Visible = False
                UltraTabControl2.Tabs(1).Visible = False
                UltraTabControl2.Tabs(2).Visible = False
                UltraTabControl2.Tabs(3).Visible = False
                UltraTabControl2.Tabs(4).Visible = False
                UltraTabControl2.Tabs(5).Visible = False
                UltraTabControl2.Tabs(6).Visible = False
                ''
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        'Me.btnEdit.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        ''End TASK TFS1384

                        ''TASK TFS2664 done on 07-03-2018
                        'UltraTabControl2.Tabs(0).Visible = False
                        'UltraTabControl2.Tabs(1).Visible = False
                        'UltraTabControl2.Tabs(2).Visible = False
                        'UltraTabControl2.Tabs(3).Visible = False
                        'UltraTabControl2.Tabs(4).Visible = False
                        'UltraTabControl2.Tabs(5).Visible = False
                    ElseIf RightsDt.FormControlName = "Cost Tab" Then
                        UltraTabControl2.Tabs(0).Visible = True
                    ElseIf RightsDt.FormControlName = "Material Cost" Then
                        UltraTabControl2.Tabs(1).Visible = True
                    ElseIf RightsDt.FormControlName = "Direct Expense" Then
                        UltraTabControl2.Tabs(2).Visible = True
                    ElseIf RightsDt.FormControlName = "Over Heads" Then
                        UltraTabControl2.Tabs(3).Visible = True
                    ElseIf RightsDt.FormControlName = "Labour Cost" Then
                        UltraTabControl2.Tabs(4).Visible = True
                    ElseIf RightsDt.FormControlName = "By Product" Then
                        UltraTabControl2.Tabs(5).Visible = True
                    ElseIf RightsDt.FormControlName = "Finish Goods" Then
                        UltraTabControl2.Tabs(6).Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdSaved.RowDoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdMaterialCost_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMaterialCost.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim CloseBatchMCDetailId = Me.grdMaterialCost.GetRow.Cells("CloseBatchMCDetailId").Value
                If CloseBatchMCDetailId > 0 Then
                    If CloseBatchDAL.DeleteMC(CloseBatchMCDetailId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdMaterialCost.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDirectExpense_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDirectExpense.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim CloseBatchDEDetailId = Me.grdDirectExpense.GetRow.Cells("CloseBatchDEDetailId").Value
                If CloseBatchDEDetailId > 0 Then
                    If CloseBatchDAL.DeleteDE(CloseBatchDEDetailId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdDirectExpense.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdOverHeads_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOverHeads.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim CloseBatchOHDetailId = Me.grdOverHeads.GetRow.Cells("CloseBatchOHDetailId").Value
                If CloseBatchOHDetailId > 0 Then
                    If CloseBatchDAL.DeleteOH(CloseBatchOHDetailId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdOverHeads.GetRow.Delete()

                CalculateAmounts()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLabourCost_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLabourCost.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim CloseBatchLCDetailId = Me.grdLabourCost.GetRow.Cells("CloseBatchLCDetailId").Value
                If CloseBatchLCDetailId > 0 Then
                    If CloseBatchDAL.DeleteLC(CloseBatchLCDetailId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdLabourCost.GetRow.Delete()

                CalculateAmounts()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdByProduct_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdByProduct.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim CloseBatchByProductDetailId = Me.grdByProduct.GetRow.Cells("CloseBatchByProductDetailId").Value
                If CloseBatchByProductDetailId > 0 Then
                    If CloseBatchDAL.DeleteByProduct(CloseBatchByProductDetailId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdByProduct.GetRow.Delete()

                CalculateAmounts()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdFinishGoods_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdFinishGoods.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim CloseBatchFinishGoodsDetailId = Me.grdFinishGoods.GetRow.Cells("CloseBatchFinishGoodsDetailId").Value
                If CloseBatchFinishGoodsDetailId > 0 Then
                    If CloseBatchDAL.DeleteFinishGoods(CloseBatchFinishGoodsDetailId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdFinishGoods.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 0 Then
                Me.btnSave.Visible = True
                Me.btnNew.Visible = True
                Me.btnRefresh.Visible = True
                'Me.btnEdit.Visible = False
                CtrlGrdBar1.Visible = False
            ElseIf e.Tab.Index = 1 Then
                Me.btnSave.Visible = False
                Me.btnNew.Visible = False
                Me.btnRefresh.Visible = False
                'Me.btnEdit.Visible = True
                CtrlGrdBar1.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@CloseBatchId", Me.grdSaved.GetRow.Cells("CloseBatchId").Value)
            ShowReport("rptCloseBatch")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetStageWiseProductionGrid(ByVal TicketId As Integer)
        Try

            Dim dtStages As New DataTable
            Dim dtDeparmentWiseStages As New DataTable

            Dim StagesU As String = ""
            Dim StagesD As String = ""

            dtStages = CloseBatchDAL.GetStages(TicketId, "T")

            If dtStages.Rows.Count > 0 Then

                For Each row As DataRow In dtStages.Rows

                    If Not row.Item("Stage").ToString = String.Empty Then

                        StagesD += "[" & row.Item("Stage").ToString & "],"

                        StagesU += "ISNULL([" & row.Item("Stage").ToString & "],0) As [" & row.Item("Stage").ToString & "],"

                    End If

                Next

            End If

            If Not StagesD = String.Empty And Not StagesU = String.Empty Then

                StagesU = StagesU.Trim.Substring(0, StagesU.Length - 1)
                StagesD = StagesD.Trim.Substring(0, StagesD.Length - 1)


                dtDeparmentWiseStages = CloseBatchDAL.GetDeparmentWiseStages("SELECT ArticleId, ArticleDescription, SubSubId, LocationId, " & StagesU & " FROM " _
                                                                             & "(Select Detail.ArticleId, Article.ArticleDescription, IsNull(Detail.Qty, 0) AS Qty , " _
                                                                             & "tblprosteps.prod_step as Stage, IsNull(ArticleGroupDefTable.SubSubId, 0) AS SubSubId, IsNull(Detail.LocationId, 0) As LocationId FROM DepartmentWiseProductionDetail AS Detail " _
                                                                             & "LEFT OUTER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId " _
                                                                             & "LEFT OUTER JOIN tblprosteps ON Detail.DepartmentId = tblprosteps.ProdStep_Id " _
                                                                             & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId " _
                                                                             & " WHERE  Detail.TicketId = " & TicketId & ") AS Pro " _
                                                                             & "PIVOT (SUM(Qty) for Stage IN(" & StagesD & ")) As PVT")
                Me.grdStageProduction.DataSource = dtDeparmentWiseStages
                Me.grdStageProduction.RetrieveStructure()
                ApplyGridSettings()

            Else
                Me.grdStageProduction.DataSource = Nothing
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DisplayStageWiseProductionGrid(ByVal CloseBatchId As Integer)
        Try

            Dim dtStages As New DataTable
            Dim dtDeparmentWiseStages As New DataTable

            Dim StagesU As String = ""
            Dim StagesD As String = ""

            dtStages = CloseBatchDAL.GetStages(CloseBatchId, "C")

            'dtStages = CloseBatchDAL.GetStages()


            If dtStages.Rows.Count > 0 Then

                For Each row As DataRow In dtStages.Rows

                    If Not row.Item("Stage").ToString = String.Empty Then

                        'StagesD += row.Item("Stage").ToString & ","

                        'StagesU += "ISNULL(" & row.Item("Stage").ToString & ",0) As " & row.Item("Stage").ToString & ","
                        StagesD += "[" & row.Item("Stage").ToString & "],"

                        StagesU += "ISNULL([" & row.Item("Stage").ToString & "],0) As [" & row.Item("Stage").ToString & "],"

                    End If

                Next

            End If

            If Not StagesD = String.Empty And Not StagesU = String.Empty Then

                StagesU = StagesU.Trim.Substring(0, StagesU.Length - 1)
                StagesD = StagesD.Trim.Substring(0, StagesD.Length - 1)


                dtDeparmentWiseStages = CloseBatchDAL.GetDeparmentWiseStages("SELECT ArticleId, ArticleDescription, SubSubId, LocationId, " & StagesU & " FROM " _
                                                                             & " (Select CloseBatchFinishGoodsDetail.CloseBatchId, CloseBatchFinishGoodsDetail.ArticleId, Article.ArticleDescription, " _
                                                                             & " IsNull(CloseBatchFinishGoodsDetail.Qty, 0) AS Qty , tblprosteps.prod_step as Stage, IsNull(ArticleGroupDefTable.SubSubId, 0) AS SubSubId, IsNull(DepartmentWiseProductionDetail.LocationId, 0) AS LocationId  FROM CloseBatchFinishGoodsDetail " _
                                                                             & " INNER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                                                                             & " INNER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id  " _
                                                                             & " LEFT OUTER JOIN ArticleGroupDefTable ON Article.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId " _
                                                                             & "INNER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id WHERE CloseBatchFinishGoodsDetail.CloseBatchId = " & CloseBatchId & ") AS Pro " _
                                                                             & "PIVOT (SUM(Qty) for Stage IN(" & StagesD & ")) As PVT")
                Me.grdStageProduction.DataSource = dtDeparmentWiseStages
                Me.grdStageProduction.RetrieveStructure()
                ApplyGridSettings()

            Else

                Me.grdStageProduction.DataSource = Nothing

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CalculateAmounts()

        Try

            Dim i As Integer

            Dim DirectExpense As Double = 0.0

            Dim OverHeads As Double = 0.0
            Dim Labour As Double = 0.0
            Dim ByProduct As Double = 0.0

            Dim TotalExpense As Double = 0.0
            Dim NetCost As Double = 0.0

            Dim UnitProduced As Double = 0.0

            Dim MaterialCost As Double = 0.0

            Dim PerUnitRate As Double = 0.0

            Dim strVal As String
            Dim pointValue As String = String.Empty


            For i = 0 To grdDirectExpense.GetRows.Length - 1

                DirectExpense += Me.grdDirectExpense.GetRows(i).Cells("Amount").Value()

            Next

            For i = 0 To grdOverHeads.GetRows.Length - 1

                OverHeads += Me.grdOverHeads.GetRows(i).Cells("Amount").Value()

            Next


            For i = 0 To grdLabourCost.GetRows.Length - 1

                Labour += Me.grdLabourCost.GetRows(i).Cells("Amount").Value()

            Next



            TotalExpense = DirectExpense + OverHeads + Labour

            If (TotalExpense.ToString Like "*.*") Then

                pointValue = TotalExpense.ToString.Split(".")(1)

                strVal = lableInCommaFormate(TotalExpense.ToString.Split(".")(0))

            Else
                pointValue = String.Empty

                strVal = lableInCommaFormate(TotalExpense.ToString)

            End If



            If strVal = String.Empty Then

                Me.LblTotalExpense.Text = 0

            Else

                If (pointValue = String.Empty) Then

                    pointValue = "00"

                End If

                strVal = strVal & "." & pointValue

                Me.LblTotalExpense.Text = strVal

            End If






            For i = 0 To grdByProduct.GetRows.Length - 1

                ByProduct += Me.grdByProduct.GetRows(i).Cells("Value").Value()

            Next


            If (ByProduct.ToString Like "*.*") Then

                pointValue = ByProduct.ToString.Split(".")(1)

                strVal = lableInCommaFormate(ByProduct.ToString.Split(".")(0))

            Else

                pointValue = String.Empty

                strVal = lableInCommaFormate(ByProduct.ToString)

            End If



            If strVal = String.Empty Then

                Me.LblByProduct.Text = " - " & 0

            Else


                If (pointValue = String.Empty) Then

                    pointValue = "00"

                End If

                strVal = strVal & "." & pointValue

                Me.LblByProduct.Text = " - " & strVal

            End If




            For i = 0 To grdMaterialCost.GetRows.Length - 1

                MaterialCost += Me.grdMaterialCost.GetRows(i).Cells("Value").Value()

            Next


            If (MaterialCost.ToString Like "*.*") Then

                pointValue = MaterialCost.ToString.Split(".")(1)

                strVal = lableInCommaFormate(MaterialCost.ToString.Split(".")(0))

            Else

                pointValue = String.Empty

                strVal = lableInCommaFormate(MaterialCost.ToString)

            End If


            If strVal = String.Empty Then

                LblMaterialCost.Text = 0

            Else

                If (pointValue = String.Empty) Then

                    pointValue = "00"

                End If

                strVal = strVal & "." & pointValue

                LblMaterialCost.Text = strVal

            End If




            NetCost = (TotalExpense + MaterialCost) - ByProduct


            If (NetCost.ToString Like "*.*") Then

                pointValue = NetCost.ToString.Split(".")(1)

                strVal = lableInCommaFormate(NetCost.ToString.Split(".")(0))

            Else

                pointValue = String.Empty

                strVal = lableInCommaFormate(NetCost.ToString)

            End If


            If strVal = String.Empty Then

                Me.LblNetCost.Text = 0

            Else

                If (pointValue = String.Empty) Then

                    pointValue = "00"

                End If

                strVal = strVal & "." & pointValue

                Me.LblNetCost.Text = strVal

            End If


            If grdStageProduction.GetRows.Length > 0 Then

                UnitProduced = Me.grdStageProduction.GetRow.Cells(Me.grdStageProduction.CurrentRow.Table.Columns.Count - 1).Value

            Else

                LblUnitProduced.Text = 0

            End If



            If UnitProduced > 0 Then

                PerUnitRate = NetCost / UnitProduced


                If (PerUnitRate.ToString Like "*.*") Then

                    pointValue = PerUnitRate.ToString.Split(".")(1)

                    strVal = lableInCommaFormate(PerUnitRate.ToString.Split(".")(0))

                Else

                    pointValue = String.Empty

                    strVal = lableInCommaFormate(PerUnitRate.ToString)

                End If

                If strVal = String.Empty Then

                    LblPerUnitRate.Text = 0

                Else

                    If (pointValue = String.Empty) Then

                        pointValue = "00"

                    End If

                    strVal = strVal & "." & pointValue

                    LblPerUnitRate.Text = strVal

                End If

            Else

                LblPerUnitRate.Text = 0

            End If




            If grdStageProduction.GetRows.Length > 0 Then

                UnitProduced = Me.grdStageProduction.GetRow.Cells(Me.grdStageProduction.CurrentRow.Table.Columns.Count - 1).Value


                If (UnitProduced.ToString Like "*.*") Then

                    pointValue = UnitProduced.ToString.Split(".")(1)

                    strVal = lableInCommaFormate(UnitProduced.ToString.Split(".")(0))

                Else

                    pointValue = String.Empty

                    strVal = lableInCommaFormate(UnitProduced.ToString)

                End If


                If strVal = String.Empty Then

                    LblUnitProduced.Text = 0

                Else

                    If (pointValue = String.Empty) Then

                        pointValue = "00"

                    End If

                    strVal = strVal & "." & pointValue

                    LblUnitProduced.Text = strVal

                End If

            Else

                LblUnitProduced.Text = 0

            End If

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Function lableInCommaFormate(ByVal strVal As String)

        strVal = FormatCurrency(strVal, 0, 0, vbFalse, vbTrue)
        strVal = strVal.Replace("£", String.Empty)
        strVal = strVal.Replace("$", String.Empty)

        Return strVal

    End Function

    Private Sub grdByProduct_RecordUpdated(sender As Object, e As EventArgs) Handles grdByProduct.RecordUpdated

        Try

            grdByProduct.UpdateData()

            CalculateAmounts()

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub grdLabourCost_RecordUpdated(sender As Object, e As EventArgs) Handles grdLabourCost.RecordUpdated

        Try

            grdByProduct.UpdateData()

            CalculateAmounts()

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub grdOverHeads_RecordUpdated(sender As Object, e As EventArgs) Handles grdOverHeads.RecordUpdated

        Try

            grdByProduct.UpdateData()

            CalculateAmounts()

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub
    Function GetProductionNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("PRD" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "ProductionMasterTable", "Production_No")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("PRD" & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "ProductionMasterTable", "Production_No")
            Else
                Return GetNextDocNo("PRD", 6, "ProductionMasterTable", "Production_no")
            End If
            'Else
            'Return ""
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub LblUnitProduced_TextChanged(sender As Object, e As EventArgs) Handles LblUnitProduced.TextChanged
        Try
            If Val(Me.LblUnitProduced.Text) > 0 Then
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdLabourCost.GetRows
                    Row.BeginEdit()
                    Row.Cells("Amount").Value = Row.Cells("Amount").Value * Val(Me.LblUnitProduced.Text)
                    Row.EndEdit()
                Next

                CalculateAmounts()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal ReceivingNo As String)
        Try
            If Me.grdSaved.RowCount < 1 Then Exit Function
            Get_All = Nothing
            If Not ReceivingNo.Length > 0 Then Exit Try
            'Dim dt As DataTable = Me.grdSaved.DataSource
            If IsFormLoaded = True Then
                IsDrillDown = True
                Dim flag As Boolean = False
                'Me.grdSaved.
                Dim Row1 As Integer = Me.grdSaved.FindAll(Me.grdSaved.RootTable.Columns("ProductionNo"), Janus.Windows.GridEX.ConditionOperator.Equal, ReceivingNo)
                'flag = Me.grdSaved.Find(Me.grdSaved.RootTable.Columns("ProductionNo"), Janus.Windows.GridEX.ConditionOperator.Equal, ReceivingNo, 0, 1)
                'flag = Me.grdSaved.Find()
                Me.grdSaved_RowDoubleClick(Nothing, Nothing)
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmCloseBatch_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            IsFormLoaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim ID = Me.grdDetail.GetRow.Cells("ID").Value
                If ID > 0 Then
                    If CloseBatchDAL.DeleteDetail(ID) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdDetail.GetRow.Delete()
                CalculateAmounts()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class