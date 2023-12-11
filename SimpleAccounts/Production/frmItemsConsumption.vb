'' TASK:906 managing and handling all controls's code required, events and binding controls.
''TAKS: 1009 Items on consumption should be loaded ticket, tag and department wise from Estimation
''TASK: TFS1137 Account instead of CGS Account at Article Department should be debited at Consumption.
''TASK : TFS1272 Validation of estimation quantity.
''TASK TFS1436 Muhammad Ameen. Record can not be updated when two issuances are made against same ticket but consumed one. Dated 19-09-2017
Imports SBModel
Imports SBDal

Public Class frmItemsConsumption

    Implements IGeneral
    Dim ObjComsumption As ItemConsumptionMaster
    Dim ConsumptionId As Integer = 0
    Dim VoucherId As Integer = 0
    Dim Qty As Double = 0
    Dim IsDropDownCalled As Boolean = False
    Dim KeyDownQty As Double = 0
    Dim KeyDownArticleId As Integer
    Dim KeyDownParentTag As Integer
    Dim IsKeyDownCalled As Boolean = False
    Enum Master
        DocNo
        DocDate
        Remarks
        PlanId
        TicketId
        DepartmentId
        Department
        StoreIssuanceAccountId
    End Enum
    Enum Detail
        'LocationId
        'ConsumptionDetailId
        'ConsumptionId
        'ArticleId
        'ArticleCode
        'ArticleDescription
        'Color
        'Qty
        'ConsumedQty
        'AvailableQty
        'Rate
        'Total
        'DispatchId
        'DispatchDetailId
        'CGSAccountId
        'Comments
        LocationId
        Location
        ConsumptionDetailId
        ConsumptionId
        ArticleId
        ArticleCode
        ArticleDescription
        Color
        Qty
        ConsumedQty
        AvailableQty
        Rate
        Total
        DispatchId
        DispatchDetailId
        CGSAccountId
        Comments
        CheckQty
        EstimationId
        ParentTagNo
        EstimatedQty
        DepartmentId
        TotalIssuedQty
        TotalConsumedQty
        TotalReturnedQty
    End Enum

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Master" Then
                Me.grdSaved.RootTable.Columns("ConsumptionId").Visible = False
                Me.grdSaved.RootTable.Columns("DepartmentId").Visible = False
                Me.grdSaved.RootTable.Columns("StoreIssuanceAccountId").Visible = False
                Me.grdSaved.RootTable.Columns("PlanId").Visible = False
                Me.grdSaved.RootTable.Columns("TicketId").Visible = False
                Me.grdSaved.RootTable.Columns("DispatchId").Visible = False
            End If
            If Condition = "Detail" Then
                Me.grdDetail.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
                Me.grdDetail.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
                'Me.grdDetail.RootTable.Columns("Rate").TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns("Total").TotalFormatString = "N" & DecimalPointInValue
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''TASK:909 Applied security rights accomplished by Ameen
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnDelete1.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnDelete1.Enabled = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnDelete1.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnDelete1.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                IsCrystalReportExport = False
                IsCrystalReportPrint = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        Me.btnDelete1.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''End TASK:909

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New ConsumptionMasterDAL().Delete(ConsumptionId, VoucherId) Then
                SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text.Trim)
                msg_Information("Record has been deleted successfully")
                ReSetControls()
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Query As String = ""
        Try
            If Condition = "Plan" Then
                ''TFS1436 Changed combo and query to show Issue no and save DispatchId. Moreover, replaced combo with ultra combo
                'Query = "Select Distinct PlanmasterTable.PlanId, PlanNo + ' ~ ' + Convert(Varchar(9), PlanDate, 113) As PlanNo, IsNull(DispatchMasterTable.DispatchNo, 0) As [Issue No], IsNull(DispatchMasterTable.DispatchId, 0) As DispatchId From PlanmasterTable INNER JOIN DispatchMasterTable ON PlanMasterTable.PlanId = DispatchMasterTable.PlanId Order By PlanNo Desc"
                Query = "Select IsNull(DispatchMasterTable.DispatchId, 0) As DispatchId, PlanNo + ' ~ ' + Convert(Varchar(9), PlanDate, 113) As PlanNo, IsNull(DispatchMasterTable.DispatchNo, 0) As [Issue No], PlanmasterTable.PlanId From PlanmasterTable INNER JOIN DispatchMasterTable ON PlanMasterTable.PlanId = DispatchMasterTable.PlanId Order By PlanNo Desc"

                ''TAKS: 1009
                'Query = "Select Distinct PlanmasterTable.PlanId, PlanNo + ' ~ ' + Convert(Varchar(9), PlanDate, 113) As PlanNo From PlanmasterTable INNER JOIN MaterialEstimation ON PlanMasterTable.PlanId = MaterialEstimation.MasterPlanId Order By PlanNo Desc"
                ''END 1009
                FillUltraDropDown(Me.cmbPlan, Query)
                If Me.cmbPlan.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbPlan.Rows(0).Activate()
                    Me.cmbPlan.DisplayLayout.Bands(0).Columns("PlanId").Hidden = True
                    Me.cmbPlan.DisplayLayout.Bands(0).Columns("DispatchId").Hidden = True
                End If
                ''End TASK TFS1436
            End If
            If Condition = "Ticket" Then
                ''TFS1436 Changed combo and query to show Issue no and save DispatchId, Moreover, replace combo with ultra combo
                'Query = "Select Distinct PlanTicketsMaster.PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo, IsNull(DispatchMasterTable.DispatchNo, 0) As [Issue No], IsNull(DispatchMasterTable.DispatchId, 0) As DispatchId FROM PlanTicketsMaster Inner Join DispatchMasterTable ON PlanTicketsMaster.PlanTicketsMasterID=DispatchMasterTable.PlanTicketId  Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.Value & "  Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                ''TASK TFS1436
                Query = "Select Distinct PlanTicketsMaster.PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo, IsNull(DispatchMasterTable.DispatchNo, 0) As [Issue No], IsNull(DispatchMasterTable.DispatchId, 0) As DispatchId FROM PlanTicketsMaster Inner Join DispatchMasterTable ON PlanTicketsMaster.PlanTicketsMasterID=DispatchMasterTable.PlanTicketId  Where DispatchMasterTable.DispatchId = " & Val(Me.cmbPlan.ActiveRow.Cells("DispatchId").Value.ToString) & "  Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                'Query = "Select Distinct PlanTicketsMaster.PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster Inner Join MaterialEstimation ON PlanTicketsMaster.PlanTicketsMasterID=MaterialEstimation.PlanTicketId  Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.SelectedValue & " Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                FillUltraDropDown(Me.cmbTicket, Query)
                If Me.cmbTicket.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbTicket.Rows(0).Activate()
                    Me.cmbTicket.DisplayLayout.Bands(0).Columns("PlanTicketsMasterID").Hidden = True
                    Me.cmbTicket.DisplayLayout.Bands(0).Columns("DispatchId").Hidden = True
                End If
                ''End TASK TFS1436
            End If
            If Condition = "Department" Then
                Query = "Select Distinct SubDepartmentID, prod_step, prod_Less, sort_Order from tblProSteps INNER JOIN DispatchDetailTable ON tblProSteps.ProdStep_Id = DispatchDetailTable.SubDepartmentId  Inner Join DispatchMasterTable ON DispatchDetailTable.DispatchId = DispatchMasterTable.DispatchId Where DispatchMasterTable.PlanTicketId =" & Me.cmbTicket.Value & " order by sort_Order "
                ''TAKS: 1009
                'Query = "Select Distinct ProdStep_Id, prod_step, prod_Less, sort_Order from tblProSteps INNER JOIN MaterialEstimationDetailTable ON tblProSteps.ProdStep_Id = MaterialEstimationDetailTable.SubDepartmentID INNER JOIN MaterialEstimation ON MaterialEstimationDetailTable.MaterialEstMasterID = MaterialEstimation.Id Where MaterialEstimation.PlanTicketId =" & Me.cmbTicket.SelectedValue & " order by sort_Order "
                ''END 1009
                'Query = "Select Distinct ProdStep_Id, prod_step, prod_Less, sort_Order from tblProSteps INNER JOIN DispatchDetailTable ON tblProSteps.ProdStep_Id = DispatchDetailTable.SubDepartmentID INNER JOIN DispatchMasterTable ON DispatchDetailTable.DispatchId = DispatchMasterTable.DispatchId Where DispatchMasterTable.DispatchId =" & Me.cmbProduct.ActiveRow.Cells("DispatchId").Value & " order by sort_Order "
                FillDropDown(Me.cmbDepartment, Query)
            End If
            If Condition = "Issuance" Then
                Query = String.Empty
                'str = "Select DispatchMasterTable.DispatchId, DispatchMasterTable.DispatchNo,* From DispatchMasterTable WHERE DispatchId Not In(Select ReturnDispatchMasterTable.DispatchId From ReturnDispatchMasterTable INNER JOIN ReturnDispatchDetailTable ON ReturnDispatchMasterTable.ReturnDispatchId = ReturnDispatchDetailTable.ReturnDispatchId INNER JOIN DispatchDetailTable ON ReturnDispatchDetailTable.DispatchDetailId = DispatchDetailTable.DispatchDetailId Where ReturnDispatchDetailTable.Qty >= DispatchDetailTable.Qty) AND LEFT(DispatchMasterTable.DispatchNo,1)='I' ORDER BY DispatchMasterTable.DispatchId DESC"
                'Query = "Select DispatchMasterTable.DispatchId, DispatchMasterTable.DispatchNo,* From DispatchMasterTable WHERE DispatchId In (Select Distinct DispatchId From DispatchDetailTable Where IsNull(ReturnedTotalQty, 0) < IsNull(Qty,0)) And LEFT(DispatchMasterTable.DispatchNo,1)='I' ORDER BY DispatchMasterTable.DispatchId DESC "
                ''TAKS: 1009
                Query = "Select DispatchMasterTable.DispatchId, DispatchMasterTable.DispatchNo, * From DispatchMasterTable WHERE DispatchId In (Select Distinct DispatchId From DispatchDetailTable Where (IsNull(ReturnedTotalQty, 0)+IsNull(ConsumedQty, 0)) < IsNull(Qty,0)) And DispatchMasterTable.PlanTicketId =" & Me.cmbTicket.Value & " And LEFT(DispatchMasterTable.DispatchNo,1)='I' ORDER BY DispatchMasterTable.DispatchId DESC "
                'FillDropDown(Me.cmbIssuance, Query)
            End If
            If Condition = "Product" Then
                Query = String.Empty
                'Query = " Select Article.ArticleId, Article.ArticleDescription As Article, FinishGood.ArticleDescription As [Finish Good] From ArticleDefTable As Article INNER JOIN MaterialEstimationDetailTable As Detail ON Article.ArticleId = Detail.ProductId " _
                '      & " INNER JOIN MaterialEstimation ON Detail.MaterialEstMasterId = MaterialEstimation.Id LEFT OUTER JOIN ArticleDefTable As FinishGood ON Detail.ParentId = FinishGood.ArticleId "
                'Query = " Select Article.ArticleId, Article.ArticleDescription As Article, FinishGood.ArticleDescription As [Finish Good], IsNull(Parent.Tag#, 0) As Tag#, IsNull(DispatchMaster.DispatchId, 0) As DispatchId From ArticleDefTable As Article INNER JOIN DispatchDetailTable As DispatchDetail ON Article.ArticleId = DispatchDetail.ArticleDefId INNER JOIN  (SELECT ProductId, ParentId, Sum(Quantity) As Qty, ParentUniqueId From MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation ON EstimationDetail.MaterialEstMasterId= MaterialEstimation.Id Where MaterialEstimation.PlanTicketId = " & Me.cmbTicket.SelectedValue & " Group By ProductId, ParentId, ParentUniqueId) As EstimationDetail ON DispatchDetail.ArticleDefId = EstimationDetail.ProductId " _
                '     & " INNER JOIN DispatchMasterTable As DispatchMaster ON DispatchDetail.DispatchId = DispatchMaster.DispatchId LEFT OUTER JOIN ArticleDefTable As FinishGood ON EstimationDetail.ParentId = FinishGood.ArticleId LEFT OUTER JOIN(SELECT UniqueId, Tag# From MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation ON EstimationDetail.MaterialEstMasterId= MaterialEstimation.Id Where MaterialEstimation.PlanTicketId = " & Me.cmbTicket.SelectedValue & ") As Parent On EstimationDetail.ParentUniqueId = Parent.UniqueId Where DispatchMaster.PlanTicketId = " & Me.cmbTicket.SelectedValue & " "
                'Query = "SELECT  Article.ArticleId, Article.ArticleDescription AS Article, ArticleColorDefTable.ArticleColorName as Color, MasterItem.ArticleDescription As [Finish Good], Recv_D.ParentTag# As Tag#, IsNull(tblTrackEstimation.DispatchedQty, 0) AS Qty, " _
                '      & " IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(tblTrackEstimation.DispatchId, 0) As DispatchId, IsNull(tblTrackEstimation.DispatchDetailId, 0) As DispatchDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId FROM  " _
                '      & " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(Recv_D.ParentId, 0) As ParentId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(IsNull(Recv_D.Quantity, 0)) As Quantity, IsNull(Recv_D.ParentTag#, 0) As ParentTag#, IsNull(DispatchMasterTable.StoreIssuanceAccountId, 0) As StoreIssuanceAccountId FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.SelectedValue & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(Recv_D.ParentId, 0), IsNull(Recv_D.ParentTag#, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTable As MasterItem ON MasterItem.ArticleId = Recv_D.ParentId  " _
                '  & " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimation ON Recv_D.EstimationId = tblTrackEstimation.EstimationId And Recv_D.ProductId = tblTrackEstimation.ArticleId  And Recv_D.SubDepartmentID = tblTrackEstimation.DepartmentId Left Outer Join DispatchMasterTable ON tblTrackEstimation.DispatchId = DispatchMasteTable.DispatchId Where tblTrackEstimation.DepartmentId =" & Me.cmbDepartment.SelectedValue & ""



                'Query = "SELECT  Article.ArticleId, Article.ArticleCode AS ArticleCode, Article.ArticleDescription AS Article, ArticleColorDefTable.ArticleColorName as Color, MasterItem.ArticleDescription As [Finish Good], Recv_D.ParentTag# As Tag#, (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)) AS Qty, " _
                '     & " IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(tblTrackEstimationConsumption.ConsumptionId, 0) As ConsumptionId, IsNull(tblTrackEstimationConsumption.ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.Price, 0) As Price, IsNull(Recv_D.Quantity, 0) As [Estimated Qty] FROM  " _
                '     & " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(Recv_D.ParentId, 0) As ParentId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(IsNull(Recv_D.Quantity, 0)) As Quantity, IsNull(Recv_D.ParentTag#, 0) As ParentTag#, IsNull(Recv_D.Price, 0) As Price FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.SelectedValue & " And Recv_D.SubDepartmentId =" & Me.cmbDepartment.SelectedValue & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(Recv_D.ParentId, 0), IsNull(Recv_D.ParentTag#, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0), IsNull(Recv_D.Price, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTable As MasterItem ON MasterItem.ArticleId = Recv_D.ParentId  " _
                ' & " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimationConsumption ON Recv_D.EstimationId = tblTrackEstimationConsumption.EstimationId And Recv_D.ProductId = tblTrackEstimationConsumption.ArticleId And Recv_D.ParentTag# = tblTrackEstimationConsumption.ParentTag# Where  IsNull(Recv_D.Quantity, 0) > IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)"


                ''Commented on 26-07-2017
                '' TFS1137 Added SubSubId to this query which is to be saved instead of CGSAccountId
                ''Commented on 22-09-2017
                'Query = "SELECT  Article.ArticleId, Article.ArticleCode AS ArticleCode, Article.ArticleDescription AS Article, ArticleColorDefTable.ArticleColorName as Color, MasterItem.ArticleDescription As [Finish Good], Recv_D.ParentTag# As [Parent Tag#], (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)) AS Qty, " _
                '     & " IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(tblTrackEstimationConsumption.ConsumptionId, 0) As ConsumptionId, IsNull(tblTrackEstimationConsumption.ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.Price, 0) As Price, IsNull(Recv_D.Quantity, 0) As [Estimated Qty], Issue.Qty As [Total Issued Qty], Issue.ConsumedQty As [Total Consumed Qty], Issue.ReturnedTotalQty As [Total Returned Qty], IsNull(Article_Group.SubSubId, 0) As SubSubId FROM  " _
                '     & " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(Recv_D.ParentId, 0) As ParentId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(Case When Recv_D.Types='Minus' Then -IsNull(Recv_D.Quantity, 0) Else IsNull(Recv_D.Quantity, 0) End) As Quantity, IsNull(Recv_D.ParentTag#, 0) As ParentTag#, IsNull(Recv_D.Price, 0) As Price FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.Value & " " & IIf(Me.cmbDepartment.SelectedValue > 0, " And Recv_D.SubDepartmentId =" & Me.cmbDepartment.SelectedValue & " ", "") & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(Recv_D.ParentId, 0), IsNull(Recv_D.ParentTag#, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0), IsNull(Recv_D.Price, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTable As MasterItem ON MasterItem.ArticleId = Recv_D.ParentId  " _
                '     & " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimationConsumption ON Recv_D.EstimationId = tblTrackEstimationConsumption.EstimationId And Recv_D.ProductId = tblTrackEstimationConsumption.ArticleId And Recv_D.ParentTag# = tblTrackEstimationConsumption.ParentTag# INNER JOIN (Select ArticleDefId, SubDepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, IsNull(EstimationId, 0) As EstimationId From DispatchDetailTable Group by ArticleDefId, SubDepartmentId, EstimationId) As Issue ON IsNull(Recv_D.EstimationId, 0) = Issue.EstimationId And IsNull(Recv_D.SubDepartmentID , 0) = Issue.SubDepartmentId And Recv_D.ProductId = Issue.ArticleDefId " _
                '     & " LEFT OUTER JOIN (Select ArticleId, EstimationId, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, DepartmentId FROM tblTrackEstimationConsumption Group By ArticleId, EstimationId, DepartmentId) As Track ON IsNull(Track.EstimationId, 0) = Issue.EstimationId And IsNull(Track.DepartmentId , 0) = Issue.SubDepartmentId And Track.ArticleId = Issue.ArticleDefId Where  IsNull(Recv_D.Quantity, 0) > IsNull(tblTrackEstimationConsumption.ConsumedQty, 0) And (Issue.Qty > (Issue.ConsumedQty+Issue.ReturnedTotalQty))"
                ''Commented below lines on 27-09-2017
                Query = "SELECT Article.ArticleId, Article.ArticleCode AS ArticleCode, Article.ArticleDescription AS Article, ArticleColorDefTable.ArticleColorName as Color, MasterItem.ArticleDescription As [Finish Good], IsNull(Recv_D.ParentTag#, 0) As [Parent Tag#], Convert(Decimal(18, " & DecimalPointInQty & "),(IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0))) AS Qty, " _
                   & " IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(tblTrackEstimationConsumption.ConsumptionId, 0) As ConsumptionId, IsNull(tblTrackEstimationConsumption.ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.Price, 0) As Price, Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Quantity, 0)) As [Estimated Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.Qty) As [Total Issued Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.ConsumedQty) As [Total Consumed Qty], Convert(Decimal(18, " & DecimalPointInQty & "), Issue.ReturnedTotalQty) As [Total Returned Qty], IsNull(Article_Group.SubSubId, 0) As SubSubId, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Issue.Qty, 0)-(IsNull(Issue.ConsumedQty, 0)+IsNull(Issue.ReturnedTotalQty, 0)))) As [Issuance Pending] FROM  " _
                   & " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(Recv_D.ParentId, 0) As ParentId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(Case When Recv_D.Types='Minus' Then -IsNull(Recv_D.Quantity, 0) Else IsNull(Recv_D.Quantity, 0) End) As Quantity, IsNull(Recv_D.ParentTag#, 0) As ParentTag#, IsNull(Recv_D.Price, 0) As Price FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.Value & " " & IIf(Me.cmbDepartment.SelectedValue > 0, " And Recv_D.SubDepartmentId =" & Me.cmbDepartment.SelectedValue & " ", "") & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(Recv_D.ParentId, 0), IsNull(Recv_D.ParentTag#, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0), IsNull(Recv_D.Price, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTable As MasterItem ON MasterItem.ArticleId = Recv_D.ParentId  " _
                   & " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimationConsumption ON Recv_D.EstimationId = tblTrackEstimationConsumption.EstimationId And Recv_D.ProductId = tblTrackEstimationConsumption.ArticleId And Recv_D.ParentTag# = tblTrackEstimationConsumption.ParentTag# INNER JOIN (Select ArticleDefId, SubDepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, IsNull(EstimationId, 0) As EstimationId From DispatchDetailTable Where DispatchId =" & Val(Me.cmbTicket.ActiveRow.Cells("DispatchId").Value.ToString) & " Group by ArticleDefId, SubDepartmentId, EstimationId) As Issue ON IsNull(Recv_D.EstimationId, 0) = Issue.EstimationId And IsNull(Recv_D.SubDepartmentID , 0) = Issue.SubDepartmentId And Recv_D.ProductId = Issue.ArticleDefId " _
                   & " LEFT OUTER JOIN (Select ArticleId, EstimationId, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, DepartmentId FROM tblTrackEstimationConsumption Group By ArticleId, EstimationId, DepartmentId) As Track ON IsNull(Track.EstimationId, 0) = Issue.EstimationId And IsNull(Track.DepartmentId , 0) = Issue.SubDepartmentId And Track.ArticleId = Issue.ArticleDefId Where  IsNull(Recv_D.Quantity, 0) > IsNull(tblTrackEstimationConsumption.ConsumedQty, 0) And (Issue.Qty > (Issue.ConsumedQty+Issue.ReturnedTotalQty))"

                '  Query = "SELECT  Article.ArticleId, Article.ArticleCode AS ArticleCode, Article.ArticleDescription AS Article, ArticleColorDefTable.ArticleColorName as Color, MasterItem.ArticleDescription As [Finish Good], Recv_D.ParentTag# As [Parent Tag#], (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)) AS Qty, " _
                '& " IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(tblTrackEstimationConsumption.ConsumptionId, 0) As ConsumptionId, IsNull(tblTrackEstimationConsumption.ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.Price, 0) As Price, IsNull(Recv_D.Quantity, 0) As [Estimated Qty], Issue.Qty As [Total Issued Qty], Issue.ConsumedQty As [Total Consumed Qty], Issue.ReturnedTotalQty As [Total Returned Qty], IsNull(Article_Group.SubSubId, 0) As SubSubId FROM  " _
                '& " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(Recv_D.ParentId, 0) As ParentId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(Case When Recv_D.Types='Minus' Then -IsNull(Recv_D.Quantity, 0) Else IsNull(Recv_D.Quantity, 0) End) As Quantity, IsNull(Recv_D.ParentTag#, 0) As ParentTag#, IsNull(Recv_D.Price, 0) As Price FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.Value & " " & IIf(Me.cmbDepartment.SelectedValue > 0, " And Recv_D.SubDepartmentId =" & Me.cmbDepartment.SelectedValue & " ", "") & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(Recv_D.ParentId, 0), IsNull(Recv_D.ParentTag#, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0), IsNull(Recv_D.Price, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTable As MasterItem ON MasterItem.ArticleId = Recv_D.ParentId  " _
                '& " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimationConsumption ON Recv_D.EstimationId = tblTrackEstimationConsumption.EstimationId And Recv_D.ProductId = tblTrackEstimationConsumption.ArticleId And Recv_D.ParentTag# = tblTrackEstimationConsumption.ParentTag# INNER JOIN (Select ArticleDefId, SubDepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, IsNull(EstimationId, 0) As EstimationId From DispatchDetailTable Group by ArticleDefId, SubDepartmentId, EstimationId) As Issue ON IsNull(Recv_D.EstimationId, 0) = Issue.EstimationId And IsNull(Recv_D.SubDepartmentID , 0) = Issue.SubDepartmentId And Recv_D.ProductId = Issue.ArticleDefId " _
                '& " LEFT OUTER JOIN (Select ArticleId, EstimationId, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, DepartmentId FROM tblTrackEstimationConsumption Group By ArticleId, EstimationId, DepartmentId) As Track ON IsNull(Track.EstimationId, 0) = Issue.EstimationId And IsNull(Track.DepartmentId , 0) = Issue.SubDepartmentId And Track.ArticleId = Issue.ArticleDefId Where  IsNull(Recv_D.Quantity, 0) > IsNull(tblTrackEstimationConsumption.ConsumedQty, 0) And (Issue.Qty > (Issue.ConsumedQty+Issue.ReturnedTotalQty))"

                'Query = "SELECT  Article.ArticleId, Article.ArticleCode AS ArticleCode, Article.ArticleDescription AS Article, ArticleColorDefTable.ArticleColorName as Color, MasterItem.ArticleDescription As [Finish Good], Recv_D.ParentTag# As Tag#, (IsNull(Recv_D.Quantity, 0)-IsNull(tblTrackEstimationConsumption.ConsumedQty, 0)) AS Qty, " _
                '     & " IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(tblTrackEstimationConsumption.ConsumptionId, 0) As ConsumptionId, IsNull(tblTrackEstimationConsumption.ConsumptionDetailId, 0) As ConsumptionDetailId, IsNull(Article_Group.CGSAccountId, 0) As CGSAccountId, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.Price, 0) As Price, IsNull(Recv_D.Quantity, 0) As [Estimated Qty], Issue.Qty As [Total Issued Qty], Issue.ConsumedQty As [Total Consumed Qty], Issue.ReturnedTotalQty As [Total Returned Qty], Recv_Count.ProductIdCount FROM  " _
                '     & " dbo.ArticleDefTable Article Inner Join(Select IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, IsNull(Recv_D.ProductId, 0) As ProductId, IsNull(Recv_D.ParentId, 0) As ParentId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, IsNull(Recv_D.PlanItemId, 0) As PlanItemId, Sum(IsNull(Recv_D.Quantity, 0)) As Quantity, IsNull(Recv_D.ParentTag#, 0) As ParentTag#, IsNull(Recv_D.Price, 0) As Price FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.SelectedValue & " " & IIf(Me.cmbDepartment.SelectedValue > 0, " And Recv_D.SubDepartmentId =" & Me.cmbDepartment.SelectedValue & " ", "") & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(Recv_D.ParentId, 0), IsNull(Recv_D.ParentTag#, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0), IsNull(Recv_D.PlanItemId, 0), IsNull(Recv_D.Price, 0)) As Recv_D  ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId " _
                '     & " Inner Join(Select Count(IsNull(Recv_D.ProductId, 0)) As ProductIdCount, IsNull(Recv_D.MaterialEstMasterID, 0) As EstimationId, Count(IsNull(Recv_D.ProductId, 0)) As ProductId, IsNull(MaterialEstimation.PlanTicketId, 0) As PlanTicketId, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID FROM MaterialEstimationDetailTable Recv_D INNER JOIN MaterialEstimation ON Recv_D.MaterialEstMasterID = MaterialEstimation.Id Where Convert(Int, Recv_D.ParentId) <> 0 And MaterialEstimation.PlanTicketId =" & Me.cmbTicket.SelectedValue & " " & IIf(Me.cmbDepartment.SelectedValue > 0, " And Recv_D.SubDepartmentId =" & Me.cmbDepartment.SelectedValue & " ", "") & " And IsNull(Recv_D.ProductId, 0) Not In(Select IsNull(ParentId, 0) As ParentId From tblCostSheet) Group By IsNull(Recv_D.MaterialEstMasterID, 0), IsNull(Recv_D.ProductId, 0), IsNull(MaterialEstimation.PlanTicketId, 0), IsNull(Recv_D.SubDepartmentID, 0)) As Recv_Count  ON Recv_Count.ProductID = Article.ArticleId LEFT OUTER JOIN " _
                '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTable As MasterItem ON MasterItem.ArticleId = Recv_D.ParentId  " _
                '     & " LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id LEFT OUTER JOIN tblTrackEstimationConsumption ON Recv_D.EstimationId = tblTrackEstimationConsumption.EstimationId And Recv_D.ProductId = tblTrackEstimationConsumption.ArticleId And Recv_D.ParentTag# = tblTrackEstimationConsumption.ParentTag# INNER JOIN (Select ArticleDefId, SubDepartmentId, Sum(IsNull(Qty, 0)) As Qty, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, Sum(IsNull(ReturnedTotalQty, 0)) As ReturnedTotalQty, IsNull(EstimationId, 0) As EstimationId From DispatchDetailTable Group by ArticleDefId, SubDepartmentId, EstimationId) As Issue ON IsNull(Recv_D.EstimationId, 0) = Issue.EstimationId And IsNull(Recv_D.SubDepartmentID , 0) = Issue.SubDepartmentId And Recv_D.ProductId = Issue.ArticleDefId " _
                '     & " LEFT OUTER JOIN (Select ArticleId, EstimationId, Sum(IsNull(ConsumedQty, 0)) As ConsumedQty, DepartmentId FROM tblTrackEstimationConsumption Group By ArticleId, EstimationId, DepartmentId) As Track ON IsNull(Track.EstimationId, 0) = Issue.EstimationId And IsNull(Track.DepartmentId , 0) = Issue.SubDepartmentId And Track.ArticleId = Issue.ArticleDefId Where  IsNull(Recv_D.Quantity, 0) > IsNull(tblTrackEstimationConsumption.ConsumedQty, 0) And (Issue.Qty > (Issue.ConsumedQty+Issue.ReturnedTotalQty))"

                FillUltraDropDown(Me.cmbProduct, Query)
                'If Me.cmbProduct.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbProduct.Rows(0).Activate()
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("EstimationId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Color").Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("Qty").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SubDepartmentID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SubDepartment").Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("DispatchDetailId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SubSubId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ConsumptionId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ConsumptionDetailId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Estimated Qty").Hidden = True
                'Me.cmbProduct.DisplayLayout.Bands(0).Columns("Total Issued Qty").C
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Total Consumed Qty").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Total Returned Qty").Hidden = True
                If rbName.Checked = True Then
                    Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(1).Column.Key.ToString
                End If
            End If
            'End If
            If Condition = "CGAccount" Then
                FillUltraDropDown(Me.cmbCGAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code] from vwCOADetail where main_type in ('Assets','Expense')  and detail_title <> ''")
                Me.cmbCGAccount.Rows(0).Activate()
                Me.cmbCGAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
            If Condition = "Category" Then
                Dim Str As String = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                         & " Else " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillDropDown(cmbLocation, Str, False)
                ''END 1009
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            ObjComsumption = New ItemConsumptionMaster
            ObjComsumption.ComsumptionId = ConsumptionId
            ObjComsumption.DocNo = Me.txtDocNo.Text ''CreateConsumptionNo()
            ObjComsumption.DocDate = Me.dtpDate.Value
            ObjComsumption.Remarks = Me.txtRemarks.Text
            'ObjComsumption.PlanId = Me.cmbPlan.Value
            'ObjComsumption.PlanId = Val(Me.cmbPlan.ActiveRow.Cells("PlanId").Value.ToString)
            If Not Me.cmbPlan.ActiveRow Is Nothing Then
                ObjComsumption.PlanId = Val(Me.cmbPlan.ActiveRow.Cells("PlanId").Value.ToString)
            End If

            'ObjComsumption.TicketId = Me.cmbTicket.Value
            If Not Me.cmbTicket.ActiveRow Is Nothing Then
                ObjComsumption.TicketId = Me.cmbTicket.Value
            End If

            ''TASK: 1009
            ''TASK TFS1436
            'ObjComsumption.DispatchId = Me.cmbTicket.ActiveRow.Cells("DispatchId").Value
            ObjComsumption.DispatchId = 0
            'If Not Me.cmbTicket.ActiveRow Is Nothing Then
            '    ObjComsumption.DispatchId = Me.cmbTicket.ActiveRow.Cells("DispatchId").Value
            'End If

            ''END TASK TFS1436
            If Not Me.cmbDepartment.SelectedValue Is Nothing Then
                ObjComsumption.DepartmentId = Me.cmbDepartment.SelectedValue
            Else
                ObjComsumption.DepartmentId = 0
            End If
            If Not Me.cmbCGAccount.ActiveRow Is Nothing Then
                ObjComsumption.StoreIssuanceAccountId = Me.cmbCGAccount.Value
            Else
                ObjComsumption.StoreIssuanceAccountId = 0
            End If
            ''END 1009
            Me.grdDetail.UpdateData()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetDataRows
                Dim Detail As New ItemConsumptionDetail
                Detail.ConsumptionDetailId = Row.Cells("ConsumptionDetailId").Value
                Detail.ConsumptionId = Row.Cells("ConsumptionId").Value
                Detail.ArticleId = Row.Cells("ArticleId").Value
                Detail.Qty = Row.Cells("Qty").Value
                Detail.Rate = Row.Cells("Rate").Value
                Detail.DispatchId = Row.Cells("DispatchId").Value
                Detail.DispatchDetailId = Row.Cells("DispatchDetailId").Value
                Detail.LocationId = Row.Cells("LocationId").Value
                Detail.Comments = Row.Cells("Comments").Value.ToString
                Detail.CGSAccountId = Row.Cells("CGSAccountId").Value
                ''TASK: 1009
                Detail.EstimationId = Row.Cells("EstimationId").Value
                Detail.ParentTagNo = Row.Cells("ParentTag#").Value
                Detail.EstimatedQty = Row.Cells("EstimatedQty").Value
                Detail.DepartmentId = Row.Cells("DepartmentId").Value
                Detail.TicketId = Row.Cells("TicketId").Value
                '' END 1009
                ObjComsumption.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then
                Me.grdSaved.DataSource = New ConsumptionMasterDAL().GetAll()
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("Master")
            End If
            If Condition = "Detail" Then
                Me.grdDetail.DataSource = New ConsumptionDetailDAL().GetDetail(ConsumptionId)
                ApplyGridSettings("Detail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.grdDetail.RowCount = 0 Then
                msg_Error("Record is not found in grid")
                Return False : Exit Function
            ElseIf Me.txtDocNo.Text = "" Then
                msg_Error("Document No is required.")
                Me.txtDocNo.Focus()
                Return False : Exit Function
                'ElseIf Me.cmbDepartment.SelectedIndex <= 0 Then
                '    msg_Error("Department is required.")
                '    ''TASK: 1009
                '    Me.cmbDepartment.Focus()
                '    Return False : Exit Function
                'ElseIf Me.cmbCGAccount.Value <= 0 Then
                '    msg_Error("Account mapping is required.")
                '    Me.cmbCGAccount.Focus()
                '    Return False : Exit Function
                ''END 1009
            Else
                FillModel()
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = CreateConsumptionNo()
            Me.dtpDate.Value = Now
            Me.txtRemarks.Text = ""
            Me.txtRemarks.Focus()
            Me.btnSave.Text = "&Save"
            If Not Me.cmbPlan.ActiveRow Is Nothing Then
                Me.cmbPlan.Rows(0).Activate()
            End If
            If Not Me.cmbTicket.ActiveRow Is Nothing Then
                Me.cmbTicket.Rows(0).Activate()
            End If
            'If Not Me.cmbIssuance.SelectedIndex = -1 Then
            '    Me.cmbIssuance.SelectedIndex = 0
            'End If
            If Not Me.cmbDepartment.SelectedIndex = -1 Then
                Me.cmbDepartment.SelectedIndex = 0
            End If

            If Not Me.cmbCGAccount.ActiveRow Is Nothing Then
                Me.cmbCGAccount.Rows(0).Activate()
            End If
            Me.cmbPlan.Enabled = True
            Me.cmbTicket.Enabled = True
            'Me.cmbIssuance.Enabled = True
            Me.cmbDepartment.Enabled = True
            Me.cmbCGAccount.Enabled = True

            Me.rbCode.Checked = True
            ConsumptionId = -1
            VoucherId = 0
            GetAllRecords("Master")
            GetAllRecords("Detail")
            ResetDetail()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            ObjComsumption.DocNo = CreateConsumptionNo()
            If New ConsumptionMasterDAL().Insert(ObjComsumption, Me.Name, MyCompanyId) Then
                SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text.Trim)
                msg_Information("Record has been saved successfully.")
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New ConsumptionMasterDAL().Update(ObjComsumption, Me.Name, VoucherId, MyCompanyId) Then
                SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text.Trim)
                msg_Information("Record has been saved successfully.")
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function

    Private Sub frmItemsConsumption_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            ReSetControls()
            FillCombos("Plan")
            FillCombos("CGAccount")
            FillCombos("Category")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.ValueChanged
        Try
            If Not cmbPlan.ActiveRow Is Nothing Then
                FillCombos("Ticket")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Function CreateConsumptionNo() As String
        Dim MENo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                MENo = GetSerialNo("IC" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "ItemConsumptionMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                MENo = GetNextDocNo("IC" & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "ItemConsumptionMaster", "DocNo")
            Else
                MENo = GetNextDocNo("IC", 6, "ItemConsumptionMaster", "DocNo")
            End If
            Return MENo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub lnkLoad_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Try
            'If Me.cmbTicket.SelectedValue > 0 Then
            '    Me.grdDetail.DataSource = New ConsumptionMasterDAL().GetStoreIssuanceByTicket(Me.cmbTicket.SelectedValue)
            'End If
            'If Me.cmbIssuance.SelectedValue > 0 Then
            '    Me.grdDetail.DataSource = New ConsumptionMasterDAL().GetStoreIssuance(Me.cmbIssuance.SelectedValue)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        Try
            If Not Me.cmbDepartment.SelectedValue Is Nothing Then
                FillCombos("Product")
                'Me.grdDetail.DataSource = New ConsumptionMasterDAL().GetStoreIssuance(Me.cmbIssuance.SelectedValue, Me.cmbDepartment.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grdDetail.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            'ConsumptionId, DocNo, DocDate, Remarks, PlanId, TicketId, DepartmentId, tblProSteps.prod_step As Department, StoreIssuanceAccountId
            ConsumptionId = Me.grdSaved.CurrentRow.Cells("ConsumptionId").Value
            VoucherId = GetVoucherId(Me.Name, Me.grdSaved.CurrentRow.Cells("DocNo").Value.ToString)
            Me.txtDocNo.Text = Me.grdSaved.CurrentRow.Cells("DocNo").Value.ToString
            Me.dtpDate.Value = Me.grdSaved.CurrentRow.Cells("DocDate").Value
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
            'Me.cmbPlan.Value = Val(Me.grdSaved.CurrentRow.Cells("PlanId").Value.ToString)
            Me.cmbPlan.Value = Val(Me.grdSaved.CurrentRow.Cells("DispatchId").Value.ToString)

            Me.cmbTicket.Value = Val(Me.grdSaved.CurrentRow.Cells("TicketId").Value.ToString)
            'Me.cmbTicket.ActiveRow.Cells("DispatchId").Value = Val(Me.grdSaved.CurrentRow.Cells("DispatchId").Value.ToString)
            'Me.cmbIssuance.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("DispatchId").Value.ToString)
            Me.cmbDepartment.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("DepartmentId").Value.ToString)
            Me.cmbCGAccount.Value = Val(Me.grdSaved.CurrentRow.Cells("StoreIssuanceAccountId").Value.ToString)
            Me.cmbPlan.Enabled = False
            Me.cmbTicket.Enabled = False
            'Me.cmbIssuance.Enabled = False
            Me.cmbDepartment.Enabled = False
            Me.cmbCGAccount.Enabled = False
            Me.btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                If Me.btnSave.Text = "&Save" Then
                    If Save() Then
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
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
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer = 0
        Try
            RemoveHandler cmbPlan.ValueChanged, AddressOf Me.cmbPlan_SelectedIndexChanged
            RemoveHandler cmbTicket.ValueChanged, AddressOf Me.cmbTicket_ValueChanged

            Id = Me.cmbPlan.Value
            FillCombos("Plan")
            Me.cmbPlan.Value = Id

            Id = Me.cmbTicket.Value
            FillCombos("Ticket")
            Me.cmbTicket.Value = Id
            'Id = Me.cmbIssuance.SelectedValue
            'FillCombos("Issuance")
            'Me.cmbIssuance.SelectedValue = Id
            Id = Me.cmbDepartment.SelectedValue
            FillCombos("Department")
            Me.cmbDepartment.SelectedValue = Id
            Id = Me.cmbProduct.Value
            FillCombos("Product")
            Me.cmbProduct.Value = Id

            Id = Me.cmbLocation.SelectedValue
            FillCombos("Category")
            Me.cmbLocation.SelectedValue = Id

            Id = Me.cmbCGAccount.Value
            FillCombos("CGAccount")
            Me.cmbCGAccount.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            AddHandler cmbPlan.ValueChanged, AddressOf Me.cmbPlan_SelectedIndexChanged
            AddHandler cmbTicket.ValueChanged, AddressOf Me.cmbTicket_ValueChanged

        End Try
    End Sub

    Private Sub grdDetail_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellEdited
        Try
            If Not Me.grdDetail.GetRow Is Nothing Then
                If Me.grdDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If e.Column.Key = "Qty" Then
                        Me.grdDetail.UpdateData()
                        If Me.grdDetail.GetRow.Cells("Qty").Value <= 0 Then
                            msg_Error("Zero or less than zero quantity is not allowed.")
                            Me.grdDetail.GetRow.Cells("Qty").Value = Me.grdDetail.GetRow.Cells("CheckQty").Value
                            'Me.grdDetail.GetRow.Cells("TotalQty").Value = Me.grdDetail.GetRow.Cells("CheckQty").Value
                            Exit Sub
                        End If
                        Dim Qty1 As Double = 0
                        Dim ConsumedQty As Double = 0
                        '' Store Issuance
                        Dim GridexFilterConditionSI As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdDetail.GetRow.Cells("ArticleId").Value)
                        Dim GridexFilterConditionSI1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("DepartmentId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdDetail.GetRow.Cells("DepartmentId").Value)
                        Dim GridexFilterConditionSI2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("TicketId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdDetail.GetRow.Cells("TicketId").Value)
                        GridexFilterConditionSI.AddCondition(GridexFilterConditionSI1)
                        GridexFilterConditionSI.AddCondition(GridexFilterConditionSI2)

                        Qty1 = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterConditionSI)
                        'Dim BalanceIssuance As Double = (Me.grdDetail.GetRow.Cells("TotalIssuedQty").Value - (Me.grdDetail.GetRow.Cells("TotalConsumedQty").Value + Me.grdDetail.GetRow.Cells("TotalReturnedQty").Value))
                        If Me.grdDetail.GetRow.Cells("Qty").Value > Me.grdDetail.GetRow.Cells("CheckQty").Value Then
                            If Qty1 > (Me.grdDetail.GetRow.Cells("TotalIssuedQty").Value - (Me.grdDetail.GetRow.Cells("TotalConsumedQty").Value + Me.grdDetail.GetRow.Cells("TotalReturnedQty").Value)) Then
                                msg_Error("Quantity exceeds issuance quantity.")
                                Me.grdDetail.GetRow.Cells("Qty").Value = Me.grdDetail.GetRow.Cells("CheckQty").Value
                                Exit Sub
                            End If
                        End If
                        ''End Store Issuance


                        ''Estimation based

                        'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Tag#").Value)
                        'Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
                        'GridexFilterCondition.AddCondition(GridexFilterCondition1)
                        'Dim GridQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                        'Qty1 = Val(Me.cmbProduct.SelectedRow.Cells("Qty").Value.ToString)
                        '' End estimation







                        ''TASK:977 done by Ameen added checkqty column to validate the input qty on item consumption.
                        'If Me.grdDetail.GetRow.Cells("Qty").Value > (Me.grdDetail.GetRow.Cells("AvailableQty").Value + Me.grdDetail.GetRow.Cells("CheckQty").Value) Then
                        ''TFS1272 Validate greator quantity
                        If Me.grdDetail.GetRow.Cells("Qty").Value > Me.grdDetail.GetRow.Cells("CheckQty").Value Then
                            If Me.grdDetail.GetRow.Cells("Qty").Value > (Me.grdDetail.GetRow.Cells("AvailableQty").Value) Then
                                Me.grdDetail.GetRow.Cells("Qty").Value = Me.grdDetail.GetRow.Cells("CheckQty").Value
                                msg_Error("Quantity value should not be greater than available quantity available is  " & Me.grdDetail.GetRow.Cells("AvailableQty").Value & "")
                            End If
                            ''End TASK TFS1272
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdDetail_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellValueChanged
        Try
            If Me.grdDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "Qty" Then
                    Qty = Me.grdDetail.GetRow.Cells("Qty").Value
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            ''TASK:923 > Transaction print 
            AddRptParam("@ConsumptionId", Val(Me.grdSaved.CurrentRow.Cells("ConsumptionId").Value.ToString))
            ShowReport("rptItemsConsumption")
            ''End TASK:923
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm("Do you want to delete?") = False Then
                    Exit Sub
                End If
                Call New ConsumptionDetailDAL().Delete(Me.grdDetail.GetRow.Cells("ConsumptionDetailId").Value)
                Me.grdDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbIssuance_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'If Me.cmbIssuance.SelectedValue > 0 Then
            FillCombos("Department")
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete1_Click(sender As Object, e As EventArgs) Handles btnDelete1.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            VoucherId = GetVoucherId(Me.Name, Me.grdSaved.CurrentRow.Cells("DocNo").Value.ToString)
            If New ConsumptionMasterDAL().Delete(Me.grdSaved.CurrentRow.Cells("ConsumptionId").Value, VoucherId) Then
                SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.StoreIssuence, Me.txtDocNo.Text.Trim)
                msg_Information("Record has been deleted successfully")
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''TASK: 1009
    'Private Sub cmbProduct_ValueChanged(sender As Object, e As EventArgs) Handles cmbProduct.ValueChanged
    '    Try
    '        If cmbProduct.IsItemInList = False Then Exit Sub
    '        If Me.cmbProduct.Value > 0 Then
    '            Dim Qty1 As Double = 0
    '            Dim EstimatedQty As Double = 0
    '            Dim ConsumedQty As Double = 0
    '            Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Parent Tag#").Value)
    '            Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
    '            Dim GridexFilterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ConsumptionDetailId"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)
    '            GridexFilterCondition.AddCondition(GridexFilterCondition1)
    '            GridexFilterCondition.AddCondition(GridexFilterCondition2)
    '            Dim GridQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
    '            Qty1 = Val(Me.cmbProduct.SelectedRow.Cells("Qty").Value.ToString)

    '            'EstimatedQty = Val(Me.cmbProduct.SelectedRow.Cells("Estimated Qty").Value.ToString)
    '            Me.txtAvailableQty.Text = Val(Qty1 - GridQty)
    '            'Me.txtAvailableQty.Text = Val(EstimatedQty - GridQty)



    '            'If Me.grdDetail.RowCount > 0 Then
    '            '    Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
    '            '    Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("DepartmentId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbDepartment.SelectedValue)
    '            '    Dim GridexFilterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("EstimationId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.ActiveRow.Cells("EstimationId").Value)
    '            '    GridexFilterCondition.AddCondition(GridexFilterCondition1)
    '            '    GridexFilterCondition.AddCondition(GridexFilterCondition2)
    '            '    Qty1 = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
    '            '    If (Qty1 + Val(Me.txtQty.Text)) > (Me.cmbProduct.ActiveRow.Cells("Total Issued Qty").Value - (Me.cmbProduct.ActiveRow.Cells("Total Consumed Qty").Value + Me.cmbProduct.ActiveRow.Cells("Total Returned Qty").Value)) Then
    '            '        msg_Error("Quantity exceeds issuance quantity.")
    '            '        Exit Sub
    '            '    End If
    '            'ElseIf Val(Me.txtQty.Text) > (Me.cmbProduct.ActiveRow.Cells("Total Issued Qty").Value - (Me.cmbProduct.ActiveRow.Cells("Total Consumed Qty").Value + Me.cmbProduct.ActiveRow.Cells("Total Returned Qty").Value)) Then
    '            '    msg_Error("Quantity exceeds issuance quantity.")
    '            '    Exit Sub
    '            'End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub AddToGrid()
        Dim Qty1 As Double = 0
        Try
            If Me.cmbProduct.Value < 1 Then
                msg_Error("Product is required.")
                Exit Sub
            End If
            If Val(Me.txtQty.Text) < 0 Then
                msg_Error("Negative quantity is not allowed.")
                Exit Sub
            End If
            If Val(Me.txtQty.Text) = 0 Then
                msg_Error("Quantity is zero.")
                Exit Sub
            End If
            If Val(Me.txtQty.Text) > Val(Me.txtAvailableQty.Text) Then
                msg_Error("Quantity is greator than available quantity.")
                Exit Sub
            End If
            If Me.grdDetail.RowCount > 0 Then
                Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
                Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("DepartmentId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.ActiveRow.Cells("SubDepartmentID").Value)
                Dim GridexFilterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("TicketId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.ActiveRow.Cells("TicketId").Value)
                GridexFilterCondition.AddCondition(GridexFilterCondition1)
                GridexFilterCondition.AddCondition(GridexFilterCondition2)
                Qty1 = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                If (Qty1 + Val(Me.txtQty.Text)) > (Me.cmbProduct.ActiveRow.Cells("Total Issued Qty").Value - (Me.cmbProduct.ActiveRow.Cells("Total Consumed Qty").Value + Me.cmbProduct.ActiveRow.Cells("Total Returned Qty").Value)) Then
                    msg_Error("Quantity exceeds issuance quantity.")
                    Exit Sub
                End If
            ElseIf Val(Me.txtQty.Text) > (Me.cmbProduct.ActiveRow.Cells("Total Issued Qty").Value - (Me.cmbProduct.ActiveRow.Cells("Total Consumed Qty").Value + Me.cmbProduct.ActiveRow.Cells("Total Returned Qty").Value)) Then
                msg_Error("Quantity exceeds issuance quantity.")
                Exit Sub
            End If
            Dim dt As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            If IsKeyDownCalled = False Then
                dr(Detail.ArticleId) = Me.cmbProduct.Value
            Else
                dr(Detail.ArticleId) = KeyDownArticleId
            End If
            dr(Detail.ArticleCode) = Me.cmbProduct.ActiveRow.Cells("ArticleCode").Value.ToString
            dr(Detail.ArticleDescription) = Me.cmbProduct.ActiveRow.Cells("Article").Value.ToString
            dr(Detail.AvailableQty) = Val(Me.txtAvailableQty.Text)
            'dr(Detail.CGSAccountId) = Me.cmbProduct.ActiveRow.Cells("CGSAccountId").Value
            ''TFS1137 added SubSubId instead of CGSAccountId 
            dr(Detail.CGSAccountId) = Me.cmbProduct.ActiveRow.Cells("SubSubId").Value
            dr(Detail.Color) = Me.cmbProduct.ActiveRow.Cells("Color").Value.ToString
            dr(Detail.Comments) = ""
            dr(Detail.ConsumedQty) = 0
            dr(Detail.ConsumptionDetailId) = 0
            dr(Detail.ConsumptionId) = 0
            dr(Detail.DispatchDetailId) = 0
            dr(Detail.DispatchId) = 0
            If Not Me.cmbLocation.SelectedValue Is Nothing Then
                dr(Detail.LocationId) = Me.cmbLocation.SelectedValue
                dr(Detail.Location) = Me.cmbLocation.Text
            Else
                dr(Detail.LocationId) = 0
                dr(Detail.Location) = ""
            End If
            dr(Detail.Qty) = Val(Me.txtQty.Text)
            dr(Detail.Rate) = Me.cmbProduct.ActiveRow.Cells("Price").Value
            dr(Detail.CheckQty) = Val(Me.txtQty.Text)
            dr(Detail.EstimationId) = Me.cmbProduct.ActiveRow.Cells("EstimationId").Value
            If IsKeyDownCalled = False Then
                dr(Detail.ParentTagNo) = Me.cmbProduct.ActiveRow.Cells("Parent Tag#").Value
            Else
                dr(Detail.ParentTagNo) = KeyDownParentTag
            End If
            dr(Detail.EstimatedQty) = Me.cmbProduct.ActiveRow.Cells("Estimated Qty").Value
            dr(Detail.DepartmentId) = Me.cmbProduct.ActiveRow.Cells("SubDepartmentID").Value
            dr(Detail.TotalIssuedQty) = Me.cmbProduct.ActiveRow.Cells("Total Issued Qty").Value
            dr(Detail.TotalConsumedQty) = Me.cmbProduct.ActiveRow.Cells("Total Consumed Qty").Value
            dr(Detail.TotalReturnedQty) = Me.cmbProduct.ActiveRow.Cells("Total Returned Qty").Value
            'dr(Detail.Total) = 0
            dt.Rows.Add(dr)
            Me.txtAvailableQty.Text -= Val(Me.txtQty.Text)
            ResetDetail()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbIssuance_SelectedIndexChanged_1(sender As Object, e As EventArgs)
        Try
            'If Me.cmbIssuance.SelectedValue > 0 Then
            '    FillCombos("Department")
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            AddToGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetDetail()
        Try
            If Not Me.cmbDepartment.SelectedValue Is Nothing Then
                FillCombos("Product")
            End If
            'Me.cmbProduct.Rows(0).Activate()
            Me.txtAvailableQty.Text = ""
            Me.txtQty.Text = ""
            KeyDownArticleId = 0
            KeyDownParentTag = 0
            KeyDownQty = 0
            IsKeyDownCalled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        If Me.cmbProduct.ActiveRow Is Nothing Then Exit Sub

        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(1).Column.Key.ToString
        'Try
        '    If rbName.Checked = True Then
        '        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(2).Column.Key.ToString
        '    Else
        '        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(1).Column.Key.ToString
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub cmbProduct_Leave(sender As Object, e As EventArgs) Handles cmbProduct.Leave
        Try
            If cmbProduct.IsItemInList = False Then Exit Sub
            If Me.cmbProduct.Value > 0 Then
                Dim Qty1 As Double = 0
                Dim EstimatedQty As Double = 0
                Dim ConsumedQty As Double = 0
                Dim GridQty As Double = 0
                Dim dt2 As DataTable = CType(Me.grdDetail.DataSource, DataTable)
                'FillCombos("Department")
                'Me.txtAvailableQty.Text = Me.cmbProduct.ActiveRow.Cells("Qty").Value.ToString
                'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("ArticleId").Value)
                If IsKeyDownCalled = False Then
                    Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Parent Tag#").Value)
                    Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Val(Me.cmbProduct.ActiveRow.Cells("ArticleId").Value.ToString))
                    Dim GridexFilterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ConsumptionDetailId"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)
                    Qty1 = Val(Me.cmbProduct.SelectedRow.Cells("Qty").Value.ToString)
                    GridexFilterCondition.AddCondition(GridexFilterCondition1)
                    GridexFilterCondition.AddCondition(GridexFilterCondition2)
                    GridQty = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                Else
                    Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, KeyDownParentTag)
                    Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, KeyDownArticleId)
                    Dim GridexFilterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ConsumptionDetailId"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)
                    Qty1 = KeyDownQty
                    GridexFilterCondition.AddCondition(GridexFilterCondition1)
                    GridexFilterCondition.AddCondition(GridexFilterCondition2)
                    GridQty = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                End If
                'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Tag#").Value)
                'Dim TotalQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
                'PlanDetailId = Val(Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value.ToString)

                'IssuedQty = GetTicketIssuedQty(PlanDetailId)
                'Dim DifInIssuedAndNewGridQty As Double = (GridQty - IssuedQty)
                'If DifInIssuedAndNewGridQty > 0 Then
                '    RemainingQty = Qty - (IssuedQty + DifInIssuedAndNewGridQty)
                'Else
                '    RemainingQty = Qty - IssuedQty
                'End If
                Me.txtAvailableQty.Text = Val(Qty1 - GridQty)
                'EstimatedQty = Val(Me.cmbProduct.SelectedRow.Cells("Estimated Qty").Value.ToString)
                ''Me.txtAvailableQty.Text = Val(Qty1 - GridQty)
                'Me.txtAvailableQty.Text = Val(EstimatedQty - GridQty)
                '  IsKeyDownCalled = False
            End If
            '' END TASK: 1009
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_ValueChanged(sender As Object, e As EventArgs) Handles cmbTicket.ValueChanged
        Try
            If Not cmbTicket.ActiveRow Is Nothing Then
                FillCombos("Department")
                'FillCombos("Issuance")
                'FillCombos("Product")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(2).Column.Key.ToString
    End Sub

    Private Sub cmbProduct_Enter(sender As Object, e As EventArgs) Handles cmbProduct.Enter
        Try
            Me.cmbProduct.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_Leave(sender As Object, e As EventArgs) Handles cmbPlan.Leave
        Try
            If Not cmbPlan.ActiveRow Is Nothing Then
                FillCombos("Ticket")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_Leave(sender As Object, e As EventArgs) Handles cmbTicket.Leave
        Try
            If Not cmbTicket.ActiveRow Is Nothing Then
                FillCombos("Department")
                'FillCombos("Issuance")
                'FillCombos("Product")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    'Private Sub cmbProduct_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbProduct.RowSelected
    '    Try
    '        If IsDropDownCalled = True Then Exit Sub
    '        If cmbProduct.IsItemInList = False Then Exit Sub
    '        If Me.cmbProduct.Value > 0 Then
    '            Dim Qty1 As Double = 0
    '            Dim EstimatedQty As Double = 0
    '            Dim ConsumedQty As Double = 0
    '            'FillCombos("Department")
    '            'Me.txtAvailableQty.Text = Me.cmbProduct.ActiveRow.Cells("Qty").Value.ToString
    '            'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("ArticleId").Value)
    '            Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Parent Tag#").Value)
    '            Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
    '            Dim GridexFilterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ConsumptionDetailId"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)

    '            GridexFilterCondition.AddCondition(GridexFilterCondition1)
    '            GridexFilterCondition.AddCondition(GridexFilterCondition2)

    '            'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Tag#").Value)

    '            Dim GridQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
    '            'Dim TotalQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
    '            'PlanDetailId = Val(Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value.ToString)
    '            Qty1 = Val(Me.cmbProduct.SelectedRow.Cells("Qty").Value.ToString)
    '            'IssuedQty = GetTicketIssuedQty(PlanDetailId)
    '            'Dim DifInIssuedAndNewGridQty As Double = (GridQty - IssuedQty)
    '            'If DifInIssuedAndNewGridQty > 0 Then
    '            '    RemainingQty = Qty - (IssuedQty + DifInIssuedAndNewGridQty)
    '            'Else
    '            '    RemainingQty = Qty - IssuedQty
    '            'End If
    '            Me.txtAvailableQty.Text = Val(Qty1 - GridQty)
    '            'EstimatedQty = Val(Me.cmbProduct.SelectedRow.Cells("Estimated Qty").Value.ToString)
    '            ''Me.txtAvailableQty.Text = Val(Qty1 - GridQty)
    '            'Me.txtAvailableQty.Text = Val(EstimatedQty - GridQty)
    '            'IsDropDownCalled = True
    '        End If
    '        '' END TASK: 1009
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub



    'Private Sub cmbProduct_TextChanged(sender As Object, e As EventArgs) Handles cmbProduct.TextChanged
    '    Try
    '        If cmbProduct.IsItemInList = False Then Exit Sub
    '        If Me.cmbProduct.Value > 0 Then
    '            Dim Qty1 As Double = 0
    '            Dim EstimatedQty As Double = 0
    '            Dim ConsumedQty As Double = 0
    '            'FillCombos("Department")
    '            'Me.txtAvailableQty.Text = Me.cmbProduct.ActiveRow.Cells("Qty").Value.ToString
    '            'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("ArticleId").Value)
    '            Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Parent Tag#").Value)
    '            Dim GridexFilterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
    '            Dim GridexFilterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ConsumptionDetailId"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)

    '            GridexFilterCondition.AddCondition(GridexFilterCondition1)
    '            GridexFilterCondition.AddCondition(GridexFilterCondition2)

    '            'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ParentTag#"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.SelectedRow.Cells("Tag#").Value)

    '            Dim GridQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
    '            'Dim TotalQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
    '            'PlanDetailId = Val(Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value.ToString)
    '            Qty1 = Val(Me.cmbProduct.SelectedRow.Cells("Qty").Value.ToString)
    '            'IssuedQty = GetTicketIssuedQty(PlanDetailId)
    '            'Dim DifInIssuedAndNewGridQty As Double = (GridQty - IssuedQty)
    '            'If DifInIssuedAndNewGridQty > 0 Then
    '            '    RemainingQty = Qty - (IssuedQty + DifInIssuedAndNewGridQty)
    '            'Else
    '            '    RemainingQty = Qty - IssuedQty
    '            'End If
    '            Me.txtAvailableQty.Text = Val(Qty1 - GridQty)
    '            'EstimatedQty = Val(Me.cmbProduct.SelectedRow.Cells("Estimated Qty").Value.ToString)
    '            ''Me.txtAvailableQty.Text = Val(Qty1 - GridQty)
    '            'Me.txtAvailableQty.Text = Val(EstimatedQty - GridQty)
    '        End If
    '        '' END TASK: 1009
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cmbProduct_BeforeDropDown(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbProduct.BeforeDropDown
    '    Try
    '        IsDropDownCalled = False
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub frmItemsConsumption_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    Try
    '        If Keys.KeyCode = Keys.Enter Or Keys.KeyCode = Keys.Tab Then
    '            IsDropDownCalled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cmbProduct_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProduct.KeyDown
    '    Try
    '        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
    '            IsDropDownCalled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cmbProduct_AfterDropDown(sender As Object, e As EventArgs) Handles cmbProduct.AfterDropDown
    '    Try
    '        IsDropDownCalled = False
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cmbProduct_MouseClick(sender As Object, e As MouseEventArgs) Handles cmbProduct.MouseClick
    '    Try
    '        If IsDropDownCalled = False Then
    '            IsDropDownCalled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cmbProduct_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles cmbProduct.MouseDoubleClick
    '    Try
    '        If IsDropDownCalled = False Then
    '            IsDropDownCalled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub cmbProduct_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProduct.KeyDown
        Try
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
                'Dim Row As Infragistics.Win.UltraWinGrid.UltraGridRow = Me.cmbProduct.GetRow(Win.UltraWinGrid.ChildRow.First).Cells("Qty").Value
                If Me.cmbProduct.IsItemInList = False Then Exit Sub
                If Me.cmbProduct.Value > 0 AndAlso IsKeyDownCalled = False Then
                    KeyDownQty = Val(Me.cmbProduct.ActiveRow.Cells("Qty").Value.ToString)
                    KeyDownParentTag = Val(Me.cmbProduct.ActiveRow.Cells("Parent Tag#").Value.ToString)
                    KeyDownArticleId = Val(Me.cmbProduct.ActiveRow.Cells("ArticleId").Value.ToString)
                    IsKeyDownCalled = True
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProduct_AfterDropDown(sender As Object, e As EventArgs) Handles cmbProduct.AfterDropDown
        Try
            IsKeyDownCalled = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        Try
            Dim frm As New frmTicketConsumptionDisplay(False, True, Me.cmbLocation.SelectedValue, Me.cmbLocation.Text)
            frm.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub fillItemsConsumptionGrid(ByVal dt As DataTable, ByVal TicketId As Integer, ByVal PlanId As Integer)
        Dim _dt As DataTable
        Try
            'cmbPlan.Value = PlanId
            'cmbTicket.Value = TicketId
            _dt = CType(Me.grdDetail.DataSource, DataTable).Clone()
            _dt.Merge(dt)
            Me.grdDetail.DataSource = _dt
            Me.grdDetail.UpdateData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmItemsConsumption_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Try
            If e.KeyCode = Keys.F1 Then
                ToolStripButton11_Click(Nothing, Nothing)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
End Class