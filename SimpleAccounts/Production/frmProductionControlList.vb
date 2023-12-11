''TASK TFS3586 Muhammad Amin got hierarchical record expanded. 22-06-2018
Imports SBDal
Imports SBUtility.Utility
Public Class frmProductionControlList
    Implements IGeneral

    Dim Counter As Integer = 0
    Dim PlanTicketId As Integer = 0
    Dim PlanNo As String = String.Empty
    Dim TicketNo As String = String.Empty
    Dim MasterArticleId As Integer = 0
    Dim DetailArticleId As Integer = 0
    Dim dtResult As DataTable

    Dim MaterialCounter As Integer = 0
    Dim dtMaterialResult As DataTable

    Dim ExpenseCounter As Integer = 0
    Dim dtExpenseResult As DataTable

    Dim ComboPlanId As Integer = 0

    Private Sub frmProductionControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("SalesOrder")
            FillCombos("PlanNo")
            FillCombos("TicketNo")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        btnAddDock.FlatAppearance.BorderSize = 0
    End Sub


    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "SalesOrder" Then
                str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable order by SalesOrderID DESC"
                FillDropDown(cmbSalesOrder, str)
            ElseIf Condition = "PlanNo" Then
                str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(12), PlanDate, 113) As PlanNo From PlanMasterTable " & IIf(Me.cmbSalesOrder.SelectedValue > 0, "Where POId = " & Me.cmbSalesOrder.SelectedValue & "", "") & " Order By PlanId DESC"
                FillDropDown(cmbPlanNo, str)
            ElseIf Condition = "TicketNo" Then
                str = "Select PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo + ' ~ ' + Convert(Varchar(12), PlanTicketsMaster.TicketDate, 113) As [Ticket No], PlanTicketsMaster.PlanId FROM PlanTicketsMaster " & IIf(Me.cmbPlanNo.SelectedValue > 0, "Where PlanId = " & Me.cmbPlanNo.SelectedValue & "", "") & " Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
                FillDropDown(cmbTicketNo, str)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

        Try
            FillCombos("SalesOrder")
            FillCombos("PlanNo")
            FillCombos("TicketNo")

            If Not cmbSalesOrder.SelectedIndex = -1 Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If

            If Not cmbPlanNo.SelectedIndex = -1 Then
                Me.cmbPlanNo.SelectedIndex = 0
            End If

            If Not cmbTicketNo.SelectedIndex = -1 Then
                Me.cmbTicketNo.SelectedIndex = 0
            End If

            grdProductionProgress.DataSource = Nothing
            grdMaterial.DataSource = Nothing
            grdExpense.DataSource = Nothing
            grdWarrentyClaim.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub cmbSalesOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.SelectedIndexChanged
        Try
            FillCombos("PlanNo")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlanNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlanNo.SelectedIndexChanged
        Try
            FillCombos("TicketNo")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            If Me.cmbPlanNo.SelectedValue <= 0 Then

                ShowErrorMessage("Please Select PlanNo")

            Else

                'Task 3438 Saad Afzaal Production Process that show status of each Product that it is in process or completed (Hierarchy view of planned ticket Items)

                ShowProductionProgress()

                'Task 3439 Saad Afzaal Production Material (Hierarchy view of Material) that shows the estimation, issued and consumed Qty

                ShowMaterial()

                'Task 3438 Saad Afzaal Show Expense of each tickets including Labour Type and Overheads

                ShowExpense()


                showWarrentyClaim()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ShowProductionProgress()
        Try
            dtResult = New DataTable

            Me.Counter = 0

            Dim MaxTicketId As Integer = 0

            Dim str As String = String.Empty
            Dim Ticket_id As Integer
            Dim Ticket_No As String
            Dim Item As String = String.Empty
            Dim ArticleId As Integer = 0
            Dim Qty As Double = 0.0
            Dim MasterArticle_Id As Integer = 0

            dtResult.Columns.Add("TicketId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("PlanNo", System.Type.GetType("System.String"))
            dtResult.Columns.Add("TicketNo", System.Type.GetType("System.String"))
            dtResult.Columns.Add("Articleid", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("Item", System.Type.GetType("System.String"))
            dtResult.Columns.Add("Qty", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("UniqueId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("UniqueParentId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("Status", System.Type.GetType("System.String"))

            Dim dtMaxTicket As New DataTable
            Dim dtDetailMaxTicket As New DataTable
            Dim dtTicketDetail As New DataTable
            Dim dtTicketsMaterial As New DataTable
            Dim dtMasterTicketMaterial As New DataTable

            str = "select max (PlanTicketsMasterID) As TicketId from PlanTicketsMaster where planid  = " & cmbPlanNo.SelectedValue
            dtMaxTicket = GetDataTable(str)

            If dtMaxTicket.Rows.Count > 0 Then

                MaxTicketId = dtMaxTicket.Rows(0).Item(0)

                str = String.Empty

                str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketsMaster.MasterArticleId , " _
                      & "ArticleDeftableMaster.ArticleDescription As Item , SalesOrderDetailTable.Qty " _
                      & "from PlanTicketsMaster " _
                      & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                      & "Left Outer Join ArticleDeftableMaster On PlanTicketsMaster.MasterArticleId = ArticleDeftableMaster.ArticleId " _
                      & "Left Outer Join SalesOrderDetailTable On PlanTicketsMaster.SalesOrderID = SalesOrderDetailTable.SalesOrderId " _
                      & "where PlanTicketsMaster.PlanTicketsMasterID = " & MaxTicketId

                dtDetailMaxTicket = GetDataTable(str)

                If dtDetailMaxTicket.Rows.Count > 0 Then

                    PlanTicketId = dtDetailMaxTicket.Rows(0).Item(0)
                    PlanNo = dtDetailMaxTicket.Rows(0).Item(1)
                    TicketNo = dtDetailMaxTicket.Rows(0).Item(2)
                    ArticleId = dtDetailMaxTicket.Rows(0).Item(3)
                    Item = dtDetailMaxTicket.Rows(0).Item(4)
                    Qty = dtDetailMaxTicket.Rows(0).Item(5)

                    Dim R As DataRow = dtResult.NewRow

                    R("TicketId") = PlanTicketId
                    R("PlanNo") = PlanNo
                    R("TicketNo") = TicketNo
                    R("Articleid") = ArticleId
                    R("Item") = Item
                    R("Qty") = Qty
                    Me.Counter += 1
                    R("UniqueId") = Me.Counter
                    R("UniqueParentId") = 0

                    dtResult.Rows.Add(R)

                    str = String.Empty

                    str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId As ArticleId , " _
                          & "ArticleDeftable.ArticleDescription As Item , isNull(SUM(CASE WHEN TYPE='Plus' THEN isNull(QTY,0) WHEN TYPE = 'Minus' THEN isNull(-QTY,0) ELSE isNull(QTY,0) END),0) AS Qty , ArticleDeftable.MasterId from PlanTicketMaterialDetail " _
                          & "Left Outer Join ArticleDeftable On PlanTicketMaterialDetail.MaterialArticleId = ArticleDeftable.ArticleId " _
                          & "Left Outer Join PlanTicketsMaster On PlanTicketMaterialDetail.ticketid = PlanTicketsMaster.PlanTicketsMasterID " _
                          & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                          & "where PlanTicketMaterialDetail.ticketid = " & MaxTicketId & " " _
                          & "group by PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId , ArticleDeftable.ArticleDescription , ArticleDeftable.MasterId"

                    dtTicketDetail = GetDataTable(str)

                    For Each row As DataRow In dtTicketDetail.Rows

                        ArticleId = Val(row.Item("Articleid").ToString)
                        Item = row.Item("Item").ToString
                        Qty = Val(row.Item("Qty").ToString)
                        MasterArticle_Id = Val(row.Item("MasterId").ToString)

                        str = String.Empty

                        str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo from PlanTicketsMaster where MasterArticleId = " & MasterArticle_Id & " And PlanId = " & cmbPlanNo.SelectedValue

                        If cmbTicketNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue <> MaxTicketId Then

                            str = str & " And PlanTicketsMaster.PlanTicketsMasterID <> " & MaxTicketId & " And PlanTicketsMaster.PlanTicketsMasterID = " & cmbTicketNo.SelectedValue

                        End If

                        dtTicketsMaterial = GetDataTable(str)

                        If dtTicketsMaterial.Rows.Count > 0 Then

                            For Each row2 As DataRow In dtTicketsMaterial.Rows

                                Ticket_id = Val(row2.Item("PlanTicketsMasterID").ToString)
                                Ticket_No = row2.Item("TicketNo").ToString

                                Dim R2 As DataRow = dtResult.NewRow

                                R2("TicketId") = Ticket_id
                                R2("PlanNo") = PlanNo
                                R2("TicketNo") = Ticket_No
                                R2("Articleid") = MasterArticle_Id
                                R2("Item") = Item
                                R2("Qty") = 1
                                Me.Counter += 1
                                R2("UniqueId") = Me.Counter
                                R2("UniqueParentId") = R("UniqueId")

                                If getFinishGoodItems(Ticket_id, Ticket_No, R2("UniqueId"), "ProductionProgress") = True Then

                                    dtResult.Rows.Add(R2)

                                End If

                            Next

                        Else

                            If cmbTicketNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue <> MaxTicketId Then

                                str = String.Empty

                                str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo from PlanTicketsMaster where MasterArticleId = " & MasterArticle_Id & " And PlanId = " & cmbPlanNo.SelectedValue

                                dtMasterTicketMaterial = GetDataTable(str)

                                If dtMasterTicketMaterial.Rows.Count <= 0 Then

                                    Dim R2 As DataRow = dtResult.NewRow

                                    R2("TicketId") = PlanTicketId
                                    R2("PlanNo") = PlanNo
                                    R2("TicketNo") = TicketNo
                                    R2("Articleid") = ArticleId
                                    R2("Item") = Item
                                    R2("Qty") = Qty
                                    Me.Counter += 1
                                    R2("UniqueId") = Me.Counter
                                    R2("UniqueParentId") = 0

                                    dtResult.Rows.Add(R2)

                                End If

                            Else

                                Dim R2 As DataRow = dtResult.NewRow

                                R2("TicketId") = PlanTicketId
                                R2("PlanNo") = PlanNo
                                R2("TicketNo") = TicketNo
                                R2("Articleid") = ArticleId
                                R2("Item") = Item
                                R2("Qty") = Qty
                                Me.Counter += 1
                                R2("UniqueId") = Me.Counter
                                R2("UniqueParentId") = 0

                                dtResult.Rows.Add(R2)

                            End If

                        End If

                    Next

                End If

            End If

            Me.grdProductionProgress.DataSource = dtResult
            Me.grdProductionProgress.ExpandRecords()

            Me.grdProductionProgress.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty

            'Task 3438 Saad Afzaal Task Show Status of each Tickets Item its Produced or in Progress  

            ProgressParentsIfChildrenAreConsumed()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function getFinishGoodItems(ByVal Ticket_Id As Integer, ByVal Ticket_No As String, ByVal UniqueId As Integer, ByVal CallFrom As String) As Boolean

        Dim ReturnState As New List(Of Integer)

        Dim str As String = String.Empty

        Dim FinishGoodid As Integer = 0

        Dim Article_Id As Integer = 0
        Dim Item As String = String.Empty
        Dim Qty As Double = 0.0
        Dim MasterArticle_Id As Integer = 0

        Dim dt As New DataTable

        Dim dtFinishGoodDetail As New DataTable
        Dim dtConsumptionDetail As New DataTable
        Dim ConsumeItemQty As Integer = 0

        str = String.Empty

        If CallFrom = "ProductionProgress" Then

            str = "select PlanTicketMaterialDetail.MaterialArticleId As ArticleId , " _
                  & "ArticleDeftable.ArticleDescription As Item , isNull(SUM(CASE WHEN PlanTicketMaterialDetail.TYPE='Plus' THEN isNull(QTY,0) WHEN PlanTicketMaterialDetail.TYPE = 'Minus' THEN isNull(-QTY,0) ELSE isNull(QTY,0) END),0) As Qty  , ArticleDeftable.MasterId " _
                  & "from PlanTicketMaterialDetail " _
                  & "Left Outer Join ArticleDeftable On PlanTicketMaterialDetail.MaterialArticleId = ArticleDeftable.ArticleId " _
                  & "where ticketid = " & Ticket_Id & " " _
                  & "group by PlanTicketMaterialDetail.MaterialArticleId , ArticleDeftable.ArticleDescription , ArticleDeftable.MasterId"

            dtFinishGoodDetail = GetDataTable(str)

            For Each row As DataRow In dtFinishGoodDetail.Rows

                Item = row.Item("Item").ToString
                Article_Id = Val(row.Item("ArticleId").ToString)
                Qty = Val(row.Item("Qty").ToString)
                MasterArticle_Id = Val(row.Item("MasterId").ToString)

                str = String.Empty

                str = "select isNull(sum(Qty),0) As Qty from DispatchDetailTable where TicketId = " & Ticket_Id & " And ArticleDefId = " & Article_Id
                dtConsumptionDetail = GetDataTable(str)

                ConsumeItemQty = dtConsumptionDetail.Rows(0).Item(0)

                If ConsumeItemQty > 0 Then

                    Dim R As DataRow = dtResult.NewRow

                    R("TicketId") = Ticket_Id
                    R("PlanNo") = PlanNo
                    R("TicketNo") = Ticket_No
                    R("Articleid") = Article_Id
                    R("Item") = Item
                    R("Qty") = Qty
                    Me.Counter += 1
                    R("UniqueId") = Me.Counter
                    R("UniqueParentId") = UniqueId

                    dtResult.Rows.Add(R)

                    ReturnState.Add(1)

                Else

                    ReturnState.Add(2)

                End If

            Next

        ElseIf CallFrom = "Material" Then

            str = String.Empty

            Dim dtFinishGoodDetail_1 As New DataTable

            'str = "select PlanTicketMaterialDetail.MaterialArticleId As ArticleId , " _
            '      & "ArticleDeftable.ArticleDescription As Item , isNull(PlanTicketMaterialDetail.Qty,0) As AllowedQunatity , ArticleDeftable.MasterId " _
            '      & "from PlanTicketMaterialDetail " _
            '      & "Left Outer Join ArticleDeftable On PlanTicketMaterialDetail.MaterialArticleId = ArticleDeftable.ArticleId " _
            '      & "where ticketid = " & Ticket_Id

            str = "select PlanTicketMaterialDetail.MaterialArticleId As ArticleId , ArticleDeftable.ArticleDescription As Item , " _
                  & "isNull(SUM(CASE WHEN PlanTicketMaterialDetail.TYPE='Plus' THEN isNull(QTY,0) WHEN PlanTicketMaterialDetail.TYPE = 'Minus' THEN isNull(-QTY,0) ELSE isNull(QTY,0) END),0) As AllowedQunatity " _
                  & ", ArticleDeftable.MasterId , isNull(TypeCount.ChangeCount,0) As ChangeCount " _
                  & "from PlanTicketMaterialDetail " _
                  & "Left Outer Join ArticleDeftable On PlanTicketMaterialDetail.MaterialArticleId = ArticleDeftable.ArticleId " _
                  & "Left Outer Join (select Ticketid , MaterialArticleId , COUNT(Type) As ChangeCount from PlanTicketMaterialDetail where Type != '' group by PlanTicketMaterialDetail.MaterialArticleId , PlanTicketMaterialDetail.ticketid) As TypeCount On " _
                  & "PlanTicketMaterialDetail.MaterialArticleId = TypeCount.MaterialArticleId And PlanTicketMaterialDetail.ticketid = TypeCount.Ticketid " _
                  & "where PlanTicketMaterialDetail.ticketid = " & Ticket_Id & " " _
                  & "group by PlanTicketMaterialDetail.MaterialArticleId , ArticleDeftable.ArticleDescription , ArticleDeftable.MasterId , TypeCount.ChangeCount "

            dtFinishGoodDetail_1 = GetDataTable(str)

            For Each row As DataRow In dtFinishGoodDetail_1.Rows

                Dim item1 As String = String.Empty
                Dim ArticleId As Integer = 0
                Dim AllowedQunatity As Double = 0.0
                Dim IssuedQuantity As Double = 0.0
                Dim IssuedBlncQty As Double = 0.0
                Dim ConsumedQty As Double = 0.0
                Dim ConsumedBlncQty As Double = 0.0
                Dim MasterId As Integer = 0
                Dim ChangeCount As Integer = 0

                Dim dtIssuance As New DataTable
                Dim dtConsumption As New DataTable


                item1 = row.Item("Item").ToString
                ArticleId = Val(row.Item("ArticleId").ToString)
                AllowedQunatity = Val(row.Item("AllowedQunatity").ToString)
                ChangeCount = Val(row.Item("ChangeCount").ToString)

                str = String.Empty

                str = "select isNull(sum(isNull(DispatchDetailTable.Qty,0)),0) AS IssuedQuantity from DispatchDetailTable where TicketId = " & Ticket_Id &
                       " and ArticleDefId = " & ArticleId

                dtIssuance = GetDataTable(str)

                If dtIssuance.Rows.Count > 0 Then

                    IssuedQuantity = dtIssuance.Rows(0).Item(0)

                    IssuedBlncQty = AllowedQunatity - IssuedQuantity

                End If


                str = String.Empty

                str = "select isNull(sum(isNull(ItemConsumptionDetail.Qty,0)),0) As ConsumedQty from ItemConsumptionDetail where TicketId = " & Ticket_Id &
                       " and ArticleId = " & ArticleId

                dtConsumption = GetDataTable(str)

                If dtConsumption.Rows.Count > 0 Then

                    ConsumedQty = dtConsumption.Rows(0).Item(0)

                    ConsumedBlncQty = IssuedQuantity - ConsumedQty

                End If


                Dim R As DataRow = dtMaterialResult.NewRow

                R("TicketId") = Ticket_Id
                R("PlanNo") = PlanNo
                R("TicketNo") = Ticket_No
                R("Articleid") = ArticleId
                R("Item") = item1
                R("AllowedQunatity") = AllowedQunatity
                R("IssuedQuantity") = IssuedQuantity
                R("IssuedBlncQty") = IssuedBlncQty
                R("ConsumedQty") = ConsumedQty
                R("ConsumedBlncQty") = ConsumedBlncQty
                Me.MaterialCounter += 1
                R("UniqueId") = Me.MaterialCounter
                R("UniqueParentId") = UniqueId
                R("ChangeCount") = ChangeCount

                dtMaterialResult.Rows.Add(R)

            Next

        End If

        If ReturnState.Find(Function(bid As Integer) bid = 1) Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ProgressParentsIfChildrenAreConsumed()

        Dim CloseBatch As Boolean = False
        Dim ParentCloseBatch As Boolean = False
        Dim ReturnParentValue As Boolean = False

        Dim ReturnTicketsCloseBatch As New List(Of Integer)
        Dim ReturnCloseBatch As Boolean = False

        Dim str = String.Empty
        Dim dtPlanTicket As New DataTable

        If grdProductionProgress.GetRows.Length > 0 Then

            For Each Node As Janus.Windows.GridEX.GridEXRow In grdProductionProgress.GetRows

                If IsParentNode(Node) Then

                    If MarkParentConsumedIfChildrenAreConsumed(Node) = True Then

                        ReturnParentValue = True

                        str = "select PlanTicketsMasterId As TicketId from PlanTicketsMaster " _
                              & "inner join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanId " _
                              & "where PlanTicketsMaster.PlanTicketsMasterId <> " & Val(Node.Cells("TicketId").Value.ToString) & " and PlanMasterTable.PlanNo = '" & Val(Node.Cells("PlanNo").Value.ToString) & "'"

                        dtPlanTicket = GetDataTable(str)

                        For Each row As DataRow In dtPlanTicket.Rows

                            ReturnCloseBatch = PlanTicketsStandardDAL.getItemCloseBatch(Val(row.Item("TicketId").ToString))

                            If ReturnCloseBatch = False Then
                                ReturnTicketsCloseBatch.Add(2)
                            End If

                        Next

                        ParentCloseBatch = PlanTicketsStandardDAL.getItemCloseBatch(Val(Node.Cells("TicketId").Value.ToString))

                        If ReturnTicketsCloseBatch.Find(Function(bid As Integer) bid = 2) Then
                            ReturnCloseBatch = False
                        Else
                            ReturnCloseBatch = True
                        End If

                    Else

                        ReturnParentValue = False

                    End If

                Else

                    CloseBatch = PlanTicketsStandardDAL.getItemCloseBatch(Val(Node.Cells("TicketId").Value.ToString))

                    If CloseBatch = True Then

                        Node.BeginEdit()
                        Node.Cells("Status").Value = "Produced"
                        Node.EndEdit()

                    Else

                        ReturnParentValue = False

                        Node.BeginEdit()
                        Node.Cells("Status").Value = "In Process"
                        Node.EndEdit()

                    End If

                End If

            Next


            For Each Node As Janus.Windows.GridEX.GridEXRow In grdProductionProgress.GetRows

                If IsParentNode(Node) Then

                    If ReturnParentValue = True AndAlso ReturnCloseBatch = True AndAlso ParentCloseBatch = True Then

                        Node.BeginEdit()
                        Node.Cells("Status").Value = "Produced"
                        Node.EndEdit()

                    Else

                        Node.BeginEdit()
                        Node.Cells("Status").Value = "In Process"
                        Node.EndEdit()

                    End If

                End If

            Next

        End If

    End Sub


    Private Function MarkParentConsumedIfChildrenAreConsumed(ByRef NodeItem As Janus.Windows.GridEX.GridEXRow) As Boolean

        Dim ReturnValue As Boolean = False

        Dim CloseBatch As Boolean = False

        Dim ConsumedQtyResult As New List(Of Integer)

        Dim ReturnInteger As Integer = 2

        Dim ChildRowCount As Integer = NodeItem.GetChildRows.Length

        Dim ConsumedQty As Integer = 0

        For Each Node As Janus.Windows.GridEX.GridEXRow In NodeItem.GetChildRows

            If IsParentNode(Node) Then

                If MarkParentConsumedIfChildrenAreConsumed(Node) = True Then

                    Node.BeginEdit()
                    Node.Cells("Status").Value = "Produced"
                    Node.EndEdit()

                Else

                    Node.BeginEdit()
                    Node.Cells("Status").Value = "In Process"
                    Node.EndEdit()

                    ConsumedQtyResult.Add(2)

                End If

            Else

                CloseBatch = PlanTicketsStandardDAL.getItemCloseBatch(Val(Node.Cells("TicketId").Value.ToString))



                ConsumedQty = PlanTicketsStandardDAL.getItemConsumedQty(Val(Node.Cells("Articleid").Value.ToString), Val(Node.Cells("TicketId").Value.ToString))

                If CloseBatch = True Then

                    ReturnValue = True

                    ConsumedQtyResult.Add(1)

                Else

                    If ConsumedQty >= 0 Then

                        ReturnValue = False

                        ConsumedQtyResult.Add(2)

                    End If

                End If

            End If

        Next

        ReturnInteger = ConsumedQtyResult.Find(Function(bid As Integer) bid = 2)

        If ReturnInteger = 2 Then

            Return False

        Else
            Return True

        End If

    End Function

    Private Function IsParentNode(ByVal NodeItem As Janus.Windows.GridEX.GridEXRow)

        If NodeItem.Children > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ShowMaterial()
        Try

            dtMaterialResult = New DataTable

            Me.MaterialCounter = 0

            Dim MaxTicketId As Integer = 0

            Dim str As String = String.Empty
            Dim Ticket_id As Integer
            Dim Ticket_No As String
            Dim Item As String = String.Empty
            Dim ArticleId As Integer = 0
            Dim AllowedQunatity As Double = 0.0
            Dim IssuedQuantity As Double = 0.0
            Dim IssuedBlncQty As Double = 0.0
            Dim ConsumedQty As Double = 0.0
            Dim ConsumedBlncQty As Double = 0.0
            Dim MasterArticle_Id As Integer = 0
            Dim ChangeCount As Integer = 0
            'Dim Qty As Integer = 0

            dtMaterialResult.Columns.Add("TicketId", System.Type.GetType("System.Int32"))
            dtMaterialResult.Columns.Add("PlanNo", System.Type.GetType("System.String"))
            dtMaterialResult.Columns.Add("TicketNo", System.Type.GetType("System.String"))
            dtMaterialResult.Columns.Add("ArticleId", System.Type.GetType("System.Int32"))
            dtMaterialResult.Columns.Add("Item", System.Type.GetType("System.String"))
            dtMaterialResult.Columns.Add("AllowedQunatity", System.Type.GetType("System.Double"))
            dtMaterialResult.Columns.Add("IssuedQuantity", System.Type.GetType("System.Double"))
            dtMaterialResult.Columns.Add("IssuedBlncQty", System.Type.GetType("System.Double"))
            dtMaterialResult.Columns.Add("ConsumedQty", System.Type.GetType("System.Double"))
            dtMaterialResult.Columns.Add("ConsumedBlncQty", System.Type.GetType("System.Double"))
            dtMaterialResult.Columns.Add("UniqueId", System.Type.GetType("System.Int32"))
            dtMaterialResult.Columns.Add("UniqueParentId", System.Type.GetType("System.Int32"))
            dtMaterialResult.Columns.Add("ChangeCount", System.Type.GetType("System.Int32"))


            Dim dtMaxTicket As New DataTable
            Dim dtDetailMaxTicket As New DataTable
            Dim dtTicketDetail As New DataTable
            Dim dtTicketsMaterial As New DataTable
            Dim dtMasterTicketMaterial As New DataTable

            str = "select max (PlanTicketsMasterID) As TicketId from PlanTicketsMaster where planid  = " & cmbPlanNo.SelectedValue
            dtMaxTicket = GetDataTable(str)

            If dtMaxTicket.Rows.Count > 0 Then

                MaxTicketId = dtMaxTicket.Rows(0).Item(0)


                str = String.Empty

                str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketsMaster.MasterArticleId , " _
                      & "ArticleDeftableMaster.ArticleDescription As Item , SalesOrderDetailTable.Qty " _
                      & "from PlanTicketsMaster " _
                      & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                      & "Left Outer Join ArticleDeftableMaster On PlanTicketsMaster.MasterArticleId = ArticleDeftableMaster.ArticleId " _
                      & "Left Outer Join SalesOrderDetailTable On PlanTicketsMaster.SalesOrderID = SalesOrderDetailTable.SalesOrderId " _
                      & "where PlanTicketsMaster.PlanTicketsMasterID = " & MaxTicketId

                dtDetailMaxTicket = GetDataTable(str)

                If dtDetailMaxTicket.Rows.Count > 0 Then

                    PlanTicketId = dtDetailMaxTicket.Rows(0).Item(0)
                    PlanNo = dtDetailMaxTicket.Rows(0).Item(1)
                    TicketNo = dtDetailMaxTicket.Rows(0).Item(2)
                    ArticleId = dtDetailMaxTicket.Rows(0).Item(3)
                    Item = dtDetailMaxTicket.Rows(0).Item(4)


                    Dim R As DataRow = dtMaterialResult.NewRow

                    R("TicketId") = PlanTicketId
                    R("PlanNo") = PlanNo
                    R("TicketNo") = TicketNo
                    R("Articleid") = MasterArticleId
                    R("Item") = Item
                    R("AllowedQunatity") = DBNull.Value
                    R("IssuedQuantity") = DBNull.Value
                    R("IssuedBlncQty") = DBNull.Value
                    R("ConsumedQty") = DBNull.Value
                    R("ConsumedBlncQty") = DBNull.Value
                    Me.MaterialCounter += 1
                    R("UniqueId") = Me.MaterialCounter
                    R("UniqueParentId") = 0
                    R("ChangeCount") = 0

                    dtMaterialResult.Rows.Add(R)


                    str = String.Empty

                    str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId As ArticleId , " _
                          & "ArticleDeftable.ArticleDescription As Item , isNull(SUM(CASE WHEN PlanTicketMaterialDetail.TYPE='Plus' THEN isNull(PlanTicketMaterialDetail.QTY,0) WHEN PlanTicketMaterialDetail.TYPE = 'Minus' THEN isNull(-PlanTicketMaterialDetail.QTY,0) ELSE isNull(PlanTicketMaterialDetail.QTY,0) END),0) As AllowedQunatity , isNull(DispatchDetailTable.Qty,0) AS IssuedQuantity , " _
                          & "isNull(isNull(SUM(CASE WHEN PlanTicketMaterialDetail.TYPE='Plus' THEN isNull(PlanTicketMaterialDetail.QTY,0) WHEN PlanTicketMaterialDetail.TYPE = 'Minus' THEN isNull(-PlanTicketMaterialDetail.QTY,0) ELSE isNull(PlanTicketMaterialDetail.QTY,0) END),0) - isNull(DispatchDetailTable.Qty,0),0) As IssuedBlncQty , " _
                          & "isNull(ItemConsumptionDetail.Qty,0) As ConsumedQty , isNull(isNull(DispatchDetailTable.Qty,0) - isNull(ItemConsumptionDetail.Qty,0),0) As ConsumedBlncQty , " _
                          & "ArticleDeftable.MasterId , isNull(TypeCount.ChangeCount,0) As ChangeCount from PlanTicketMaterialDetail " _
                          & "Left Outer Join ArticleDeftable On PlanTicketMaterialDetail.MaterialArticleId = ArticleDeftable.ArticleId " _
                          & "Left Outer Join PlanTicketsMaster On PlanTicketMaterialDetail.ticketid = PlanTicketsMaster.PlanTicketsMasterID " _
                          & "Left outer join PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanID " _
                          & "Left Outer join DispatchDetailTable ON PlanTicketMaterialDetail.MaterialArticleId = DispatchDetailTable.ArticleDefId and " _
                          & "PlanTicketMaterialDetail.ticketid = DispatchDetailTable.TicketId " _
                          & "Left Outer Join ItemConsumptionDetail ON PlanTicketMaterialDetail.MaterialArticleId = ItemConsumptionDetail.ArticleId and " _
                          & "PlanTicketMaterialDetail.ticketid = ItemConsumptionDetail.TicketId " _
                          & "Left Outer Join (select Ticketid , MaterialArticleId , COUNT(Type) As ChangeCount from PlanTicketMaterialDetail where Type != '' group by PlanTicketMaterialDetail.MaterialArticleId , PlanTicketMaterialDetail.ticketid) As TypeCount On PlanTicketMaterialDetail.MaterialArticleId = TypeCount.MaterialArticleId And PlanTicketMaterialDetail.ticketid = TypeCount.Ticketid " _
                          & "where PlanTicketMaterialDetail.ticketid = " & MaxTicketId & " " _
                          & "group by PlanTicketsMaster.PlanTicketsMasterID , PlanMasterTable.PlanNo , PlanTicketsMaster.TicketNo , PlanTicketMaterialDetail.MaterialArticleId , ArticleDeftable.ArticleDescription , DispatchDetailTable.Qty , ItemConsumptionDetail.Qty , ArticleDeftable.MasterId , TypeCount.ChangeCount"

                    dtTicketDetail = GetDataTable(str)

                    For Each row As DataRow In dtTicketDetail.Rows

                        ArticleId = Val(row.Item("Articleid").ToString)
                        Item = row.Item("Item").ToString
                        AllowedQunatity = Val(row.Item("AllowedQunatity").ToString)
                        IssuedQuantity = Val(row.Item("IssuedQuantity").ToString)
                        IssuedBlncQty = Val(row.Item("IssuedBlncQty").ToString)
                        ConsumedQty = Val(row.Item("ConsumedQty").ToString)
                        ConsumedBlncQty = Val(row.Item("ConsumedBlncQty").ToString)
                        MasterArticle_Id = Val(row.Item("MasterId").ToString)
                        ChangeCount = Val(row.Item("ChangeCount").ToString)

                        str = String.Empty

                        str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo from PlanTicketsMaster where MasterArticleId = " & MasterArticle_Id & " And PlanId = " & cmbPlanNo.SelectedValue

                        If cmbTicketNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue <> MaxTicketId Then

                            str = str & " And PlanTicketsMaster.PlanTicketsMasterID <> " & MaxTicketId & " And PlanTicketsMaster.PlanTicketsMasterID = " & cmbTicketNo.SelectedValue

                        End If

                        dtTicketsMaterial = GetDataTable(str)

                        If dtTicketsMaterial.Rows.Count > 0 Then

                            For Each row2 As DataRow In dtTicketsMaterial.Rows

                                Ticket_id = Val(row2.Item("PlanTicketsMasterID").ToString)
                                Ticket_No = row2.Item("TicketNo").ToString


                                Dim R2 As DataRow = dtMaterialResult.NewRow

                                R2("TicketId") = Ticket_id
                                R2("PlanNo") = PlanNo
                                R2("TicketNo") = Ticket_No
                                R2("Articleid") = MasterArticle_Id
                                R2("Item") = Item
                                R2("AllowedQunatity") = DBNull.Value
                                R2("IssuedQuantity") = DBNull.Value
                                R2("IssuedBlncQty") = DBNull.Value
                                R2("ConsumedQty") = DBNull.Value
                                R2("ConsumedBlncQty") = DBNull.Value
                                Me.MaterialCounter += 1
                                R2("UniqueId") = Me.MaterialCounter
                                R2("UniqueParentId") = R("UniqueId")
                                R2("ChangeCount") = 0

                                dtMaterialResult.Rows.Add(R2)

                                getFinishGoodItems(Ticket_id, Ticket_No, R2("UniqueId"), "Material")

                            Next

                        Else

                            If cmbTicketNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue <> MaxTicketId Then

                                str = String.Empty

                                str = "select PlanTicketsMaster.PlanTicketsMasterID , PlanTicketsMaster.TicketNo from PlanTicketsMaster where MasterArticleId = " & MasterArticle_Id & " And PlanId = " & cmbPlanNo.SelectedValue

                                dtMasterTicketMaterial = GetDataTable(str)

                                If dtMasterTicketMaterial.Rows.Count <= 0 Then

                                    Dim R2 As DataRow = dtMaterialResult.NewRow

                                    R2("TicketId") = PlanTicketId
                                    R2("PlanNo") = PlanNo
                                    R2("TicketNo") = TicketNo
                                    R2("Articleid") = ArticleId
                                    R2("Item") = Item
                                    R2("AllowedQunatity") = AllowedQunatity
                                    R2("IssuedQuantity") = IssuedQuantity
                                    R2("IssuedBlncQty") = IssuedBlncQty
                                    R2("ConsumedQty") = ConsumedQty
                                    R2("ConsumedBlncQty") = ConsumedBlncQty
                                    Me.MaterialCounter += 1
                                    R2("UniqueId") = Me.MaterialCounter
                                    R2("UniqueParentId") = 0
                                    R2("ChangeCount") = ChangeCount

                                    dtMaterialResult.Rows.Add(R2)

                                End If

                            Else

                                Dim R2 As DataRow = dtMaterialResult.NewRow

                                R2("TicketId") = PlanTicketId
                                R2("PlanNo") = PlanNo
                                R2("TicketNo") = TicketNo
                                R2("Articleid") = ArticleId
                                R2("Item") = Item
                                R2("AllowedQunatity") = AllowedQunatity
                                R2("IssuedQuantity") = IssuedQuantity
                                R2("IssuedBlncQty") = IssuedBlncQty
                                R2("ConsumedQty") = ConsumedQty
                                R2("ConsumedBlncQty") = ConsumedBlncQty
                                Me.MaterialCounter += 1
                                R2("UniqueId") = Me.MaterialCounter
                                R2("UniqueParentId") = 0
                                R2("ChangeCount") = ChangeCount

                                dtMaterialResult.Rows.Add(R2)

                            End If

                        End If

                    Next

                End If

            End If

            Me.grdMaterial.DataSource = dtMaterialResult
            Me.grdMaterial.ExpandRecords()

            Me.grdMaterial.RootTable.Columns("AllowedQunatity").FormatString = "N" & DecimalPointInQty
            Me.grdMaterial.RootTable.Columns("IssuedQuantity").FormatString = "N" & DecimalPointInQty
            Me.grdMaterial.RootTable.Columns("IssuedBlncQty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterial.RootTable.Columns("ConsumedQty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterial.RootTable.Columns("ConsumedBlncQty").FormatString = "N" & DecimalPointInQty


        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub ShowExpense()

        Try

            dtExpenseResult = New DataTable

            Me.ExpenseCounter = 0

            Dim str As String = String.Empty
            Dim ArticleId As Integer = 0

            dtExpenseResult.Columns.Add("TicketId", System.Type.GetType("System.Int32"))
            dtExpenseResult.Columns.Add("TicketNo", System.Type.GetType("System.String"))
            dtExpenseResult.Columns.Add("FinishGoodId", System.Type.GetType("System.Int32"))
            dtExpenseResult.Columns.Add("ProductionStep", System.Type.GetType("System.String"))
            dtExpenseResult.Columns.Add("ExpenseId", System.Type.GetType("System.Int32"))
            dtExpenseResult.Columns.Add("ExpenseType", System.Type.GetType("System.String"))
            dtExpenseResult.Columns.Add("Amount", System.Type.GetType("System.Double"))
            dtExpenseResult.Columns.Add("UniqueId", System.Type.GetType("System.Int32"))
            dtExpenseResult.Columns.Add("UniqueParentId", System.Type.GetType("System.Int32"))

            Dim dtTickets As New DataTable

            dtTickets = PlanTicketsStandardDAL.getTickets(cmbPlanNo.SelectedValue, cmbTicketNo.SelectedValue)

            For Each row As DataRow In dtTickets.Rows

                PlanTicketId = row.Item("TicketId").ToString

                ArticleId = row.Item("MasterArticleId").ToString

                Dim R As DataRow = dtExpenseResult.NewRow

                R("TicketId") = PlanTicketId
                R("TicketNo") = row.Item("TicketNo").ToString
                R("FinishGoodId") = DBNull.Value
                R("ProductionStep") = DBNull.Value
                R("ExpenseId") = DBNull.Value
                R("ExpenseType") = DBNull.Value
                R("Amount") = DBNull.Value
                Me.ExpenseCounter += 1
                R("UniqueId") = Me.ExpenseCounter
                R("UniqueParentId") = 0

                If getExpenseFinishGoodItem(ArticleId, R("UniqueId")) = True Then

                    dtExpenseResult.Rows.Add(R)

                End If

            Next

            Me.grdExpense.DataSource = dtExpenseResult

            Me.grdExpense.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Function getExpenseFinishGoodItem(ByVal MasterArticleId As Integer, ByVal UniqueId As Integer) As Boolean

        Try

            Dim ExpenseReturnState As New List(Of Integer)

            Dim str As String = String.Empty

            Dim FinishGoodid As Integer = 0

            Dim dt As New DataTable

            str = "select FinishGoodMaster.ID AS FinishGoodId , FinishGoodMaster.StandardName As Item from FinishGoodMaster where MasterArticleId = " & MasterArticleId
            dt = GetDataTable(str)

            If dt.Rows.Count > 0 Then

                FinishGoodid = dt.Rows(0).Item(0)


                Dim R As DataRow = dtExpenseResult.NewRow

                R("TicketId") = PlanTicketId
                R("TicketNo") = DBNull.Value
                R("FinishGoodId") = FinishGoodid
                R("ProductionStep") = DBNull.Value
                R("ExpenseId") = DBNull.Value
                R("ExpenseType") = "OverHeads"
                R("Amount") = DBNull.Value
                Me.ExpenseCounter += 1
                R("UniqueId") = Me.ExpenseCounter
                R("UniqueParentId") = UniqueId

                If getExpenseOverHead(FinishGoodid, R("UniqueId")) = True Then

                    dtExpenseResult.Rows.Add(R)

                End If

                Dim R2 As DataRow = dtExpenseResult.NewRow

                R2("TicketId") = PlanTicketId
                R("TicketNo") = DBNull.Value
                R2("FinishGoodId") = FinishGoodid
                R("ProductionStep") = DBNull.Value
                R2("ExpenseId") = DBNull.Value
                R2("ExpenseType") = "LabourAllocation"
                R2("Amount") = DBNull.Value
                Me.ExpenseCounter += 1
                R2("UniqueId") = Me.ExpenseCounter
                R2("UniqueParentId") = UniqueId

                If getExpenseLabourAllocation(FinishGoodid, R2("UniqueId")) = True Then

                    dtExpenseResult.Rows.Add(R2)

                End If

                ExpenseReturnState.Add(1)

            Else

                ExpenseReturnState.Add(2)

            End If

            If ExpenseReturnState.Find(Function(bid As Integer) bid = 1) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function

    Private Function getExpenseOverHead(ByVal FinishGoodid As Integer, ByVal UniqueId As Integer) As Boolean

        Try

            Dim ExpenseOverHeadReturnState As New List(Of Integer)

            Dim str As String = String.Empty

            Dim dt As New DataTable

            Dim ExpenseID As Integer = 0
            Dim ExpenseType As String = String.Empty
            Dim Amount As Double = 0


            Dim dtFinishGoodOverHead As New DataTable

            str = "select FinishGoodOverHeads.AccountId As ExpenseId , tblProSteps.prod_step As ProductionStep , vwCOADetail.detail_title As ExpenseType , " _
                  & "isNull(FinishGoodOverHeads.Amount,0) As Amount " _
                  & "from FinishGoodOverHeads " _
                  & "left outer join vwCOADetail ON  FinishGoodOverHeads.AccountId = vwCOADetail.coa_detail_id " _
                  & "left outer join tblProSteps ON  FinishGoodOverHeads.ProductionStepId = tblProSteps.ProdStep_Id " _
                  & "where FinishGoodId = " & FinishGoodid


            dtFinishGoodOverHead = GetDataTable(str)

            If dtFinishGoodOverHead.Rows.Count > 0 Then

                For Each row As DataRow In dtFinishGoodOverHead.Rows

                    ExpenseID = row.Item("ExpenseId").ToString
                    ExpenseType = row.Item("ExpenseType").ToString
                    Amount = row.Item("Amount").ToString

                    Dim R As DataRow = dtExpenseResult.NewRow

                    R("TicketId") = PlanTicketId
                    R("TicketNo") = DBNull.Value
                    R("FinishGoodId") = FinishGoodid
                    R("ProductionStep") = row.Item("ProductionStep").ToString
                    R("ExpenseId") = ExpenseID
                    R("ExpenseType") = ExpenseType
                    R("Amount") = Amount
                    Me.ExpenseCounter += 1
                    R("UniqueId") = Me.ExpenseCounter
                    R("UniqueParentId") = UniqueId

                    dtExpenseResult.Rows.Add(R)

                Next

                ExpenseOverHeadReturnState.Add(1)

            Else

                ExpenseOverHeadReturnState.Add(2)

            End If

            If ExpenseOverHeadReturnState.Find(Function(bid As Integer) bid = 1) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function


    Private Function getExpenseLabourAllocation(ByVal FinishGoodid As Integer, ByVal UniqueId As Integer) As Boolean
        Try

            Dim ExpenseLabourAllocationReturnState As New List(Of Integer)

            Dim str As String = String.Empty

            Dim dt As New DataTable

            Dim ExpenseID As Integer = 0
            Dim ExpenseType As String = String.Empty
            Dim Amount As Double = 0


            Dim dtFinishLabourAllocation As New DataTable

            str = "select FinishGoodLabourAllocation.LabourTypeId As ExpenseId , tblLabourType.LabourType As ExpenseType , tblProSteps.prod_step As ProductionStep , " _
                  & "isNull(FinishGoodLabourAllocation.RatePerUnit,0) As Amount " _
                  & "from FinishGoodLabourAllocation " _
                  & "left outer join tblLabourType ON  FinishGoodLabourAllocation.LabourTypeId = tblLabourType.id " _
                  & "left outer join tblProSteps ON  FinishGoodLabourAllocation.ProductionStepId = tblProSteps.ProdStep_Id " _
                  & "where FinishGoodId = " & FinishGoodid


            dtFinishLabourAllocation = GetDataTable(str)

            If dtFinishLabourAllocation.Rows.Count > 0 Then

                For Each row As DataRow In dtFinishLabourAllocation.Rows

                    ExpenseID = row.Item("ExpenseId").ToString
                    ExpenseType = row.Item("ExpenseType").ToString
                    Amount = row.Item("Amount").ToString

                    Dim R As DataRow = dtExpenseResult.NewRow

                    R("TicketId") = PlanTicketId
                    R("TicketNo") = DBNull.Value
                    R("FinishGoodId") = FinishGoodid
                    R("ProductionStep") = row.Item("ProductionStep").ToString
                    R("ExpenseId") = ExpenseID
                    R("ExpenseType") = ExpenseType
                    R("Amount") = Amount
                    Me.ExpenseCounter += 1
                    R("UniqueId") = Me.ExpenseCounter
                    R("UniqueParentId") = UniqueId

                    dtExpenseResult.Rows.Add(R)

                Next

                ExpenseLabourAllocationReturnState.Add(1)

            Else

                ExpenseLabourAllocationReturnState.Add(2)

            End If

            If ExpenseLabourAllocationReturnState.Find(Function(bid As Integer) bid = 1) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub showWarrentyClaim()

        Try

            Dim str = String.Empty

            str = "select WarrantyMasterTable.WarrantyMasterId , WarrantyMasterTable.WarrantyNo , WarrantyMasterTable.WarrantyDate , WarrantyMasterTable.SOId , " _
                  & "SalesOrderMasterTable.SalesOrderNo As SONO  , WarrantyMasterTable.FinishGoodStandardId , FinishGoodMaster.StandardNo As FinishGoodStandardNo , " _
                  & "WarrantyMasterTable.PlanId , PlanMasterTable.PlanNo , WarrantyMasterTable.TicketId , PlanTicketsMaster.TicketNo " _
                  & "from WarrantyMasterTable " _
                  & "left outer join SalesOrderMasterTable On WarrantyMasterTable.SOId = SalesOrderMasterTable.SalesOrderId " _
                  & "left outer join FinishGoodMaster On WarrantyMasterTable.FinishGoodStandardId = FinishGoodMaster.Id " _
                  & "left outer join PlanMasterTable On PlanMasterTable.PlanId = WarrantyMasterTable.PlanId " _
                  & "left outer join PlanTicketsMaster On WarrantyMasterTable.TicketId = PlanTicketsMaster.PlanTicketsMasterID "

            If cmbSalesOrder.SelectedValue > 0 AndAlso cmbPlanNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue <= 0 Then

                str = str & "where WarrantyMasterTable.SOId = " & cmbSalesOrder.SelectedValue & " And WarrantyMasterTable.PlanId = " & cmbPlanNo.SelectedValue

            ElseIf cmbSalesOrder.SelectedValue > 0 AndAlso cmbPlanNo.SelectedValue <= 0 AndAlso cmbTicketNo.SelectedValue <= 0 Then

                str = str & "where WarrantyMasterTable.SOId = " & cmbSalesOrder.SelectedValue

            ElseIf cmbSalesOrder.SelectedValue <= 0 AndAlso cmbPlanNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue <= 0 Then

                str = str & "where WarrantyMasterTable.PlanId = " & cmbPlanNo.SelectedValue

            ElseIf cmbSalesOrder.SelectedValue <= 0 AndAlso cmbPlanNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue > 0 Then

                str = str & "where WarrantyMasterTable.PlanId = " & cmbPlanNo.SelectedValue & " And WarrantyMasterTable.TicketId = " & cmbTicketNo.SelectedValue

            ElseIf cmbSalesOrder.SelectedValue > 0 AndAlso cmbPlanNo.SelectedValue > 0 AndAlso cmbTicketNo.SelectedValue > 0 Then

                str = str & "where WarrantyMasterTable.SOId = " & cmbSalesOrder.SelectedValue & " And WarrantyMasterTable.PlanId = " & cmbPlanNo.SelectedValue & " And WarrantyMasterTable.TicketId = " & cmbTicketNo.SelectedValue

            End If

            Dim dt As New DataTable

            dt = GetDataTable(str)

            Me.grdWarrentyClaim.DataSource = dt

            Me.grdWarrentyClaim.RootTable.Columns("WarrantyDate").FormatString = str_DisplayDateFormat


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        frmProductionControl.ShowDialog()
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        btnShow_Click(sender, e)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        ReSetControls()
    End Sub

    Private Sub grdMaterial_DoubleClick(sender As Object, e As EventArgs) Handles grdMaterial.DoubleClick

        Try

            If frmPlanTicketStandard.isFormOpen = True Then
                frmPlanTicketStandard.Dispose()
            End If

            frmPlanTicketStandard.Ticket_Id = Val(Me.grdMaterial.CurrentRow.Cells("TicketId").Value.ToString)
            frmPlanTicketStandard.ShowDialog()

            Me.ComboPlanId = Me.cmbPlanNo.SelectedValue

            ReSetControls()

            Me.cmbPlanNo.SelectedValue = Me.ComboPlanId

            'Task 3440 Saad set Ticket_Id and MasterArticleId value to 0

            frmPlanTicketStandard.Ticket_Id = 0
            frmPlanTicketStandard.MasterArticleId = 0
            frmPlanTicketStandard.IsEditMode = False

            btnShow_Click(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBarProductionProgress_Load(sender As Object, e As EventArgs) Handles CtrlGrdBarProductionProgress.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdProductionProgress.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdProductionProgress.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdProductionProgress.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBarProductionProgress.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBarMaterial_Load(sender As Object, e As EventArgs) Handles CtrlGrdBarMaterial.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaterial.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaterial.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdMaterial.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBarMaterial.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBarExpense_Load(sender As Object, e As EventArgs) Handles CtrlGrdBarExpense.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdExpense.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdExpense.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdExpense.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBarExpense.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBarWarrentyClaim_Load(sender As Object, e As EventArgs) Handles CtrlGrdBarWarrentyClaim.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdWarrentyClaim.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdWarrentyClaim.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdWarrentyClaim.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBarWarrentyClaim.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging

        Try
            If e.Tab.Index = 0 Then
                Me.CtrlGrdBarProductionProgress.Visible = True
                Me.CtrlGrdBarMaterial.Visible = False
                Me.CtrlGrdBarExpense.Visible = False
                Me.CtrlGrdBarWarrentyClaim.Visible = False

            ElseIf e.Tab.Index = 1 Then
                Me.CtrlGrdBarProductionProgress.Visible = False
                Me.CtrlGrdBarMaterial.Visible = True
                Me.CtrlGrdBarExpense.Visible = False
                Me.CtrlGrdBarWarrentyClaim.Visible = False

            ElseIf e.Tab.Index = 2 Then
                Me.CtrlGrdBarProductionProgress.Visible = False
                Me.CtrlGrdBarMaterial.Visible = False
                Me.CtrlGrdBarExpense.Visible = True
                Me.CtrlGrdBarWarrentyClaim.Visible = False

            ElseIf e.Tab.Index = 3 Then
                Me.CtrlGrdBarProductionProgress.Visible = False
                Me.CtrlGrdBarMaterial.Visible = False
                Me.CtrlGrdBarExpense.Visible = False
                Me.CtrlGrdBarWarrentyClaim.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

End Class