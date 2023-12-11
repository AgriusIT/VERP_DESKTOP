Imports SBDal
Imports SBModel
Imports System.Data.OleDb

Public Class frmWarrantyClaim
    Implements IGeneral
    Dim objModel As WarrantyMasterTable
    Dim Detail As List(Of WarrantyDetailTable)
    Dim objDal As New WarrantyClaimDAL
    Public Shared WarrantyMasterId As Integer
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Dim IsFormOpened As Boolean = False


    Dim txtPONo As String
    Dim setVoucherNo As String = String.Empty
    Dim IsWIPAccount As Boolean = False
    Dim flgStoreIssuenceVoucher As Boolean = False
    Dim StockList As List(Of StockDetail)
    Dim StockDetail As StockDetail
    Dim flgCompanyRights As Boolean = False
    Dim setVoucherdate As DateTime
    Dim DispatchId As Integer = 0

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmWarrantyClaim_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            frmWarrantyClaimList.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmWarrantyClaim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnSave.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.BorderSize = 0
            FillCombos("SalesOrder")
            FillCombos("FinishGood")
            FillCombos("Plan")
            FillCombos("Ticket")
            FillCombos("Item")
            UltraDropDownSearching(cmbProductMfd, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            'UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            Dim dt As DataTable
            
            dt = New WarrantyClaimDAL().GetById(WarrantyMasterId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Me.txtDocNo.Text = dt.Rows(0).Item("WarrantyNo")
                    Me.dtpDocNo.Value = dt.Rows(0).Item("WarrantyDate")
                    Me.cmbSaleOrder.SelectedValue = dt.Rows(0).Item("SOId")
                    Me.cmbPlan.SelectedValue = dt.Rows(0).Item("PlanId")
                    Me.cmbTicket.SelectedValue = dt.Rows(0).Item("TicketId")
                    cmbProductMfd.Value = dt.Rows(0).Item("FinishGoodStandardId")
                    btnSave.Enabled = DoHaveUpdateRights
                Next
            Else
                ReSetControls()
            End If
            IsFormOpened = True
            Me.dtpDocNo.Focus()
            DisplayRecords(WarrantyMasterId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdWarrantyClaim.RootTable.Columns("WarrantyMasterId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("WarrantyDetailId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("FinishGoodId").Visible = False
            Me.grdWarrantyClaim.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdWarrantyClaim.RootTable.Columns("ArticleDescription").Width = 250
            If Me.grdWarrantyClaim.RootTable.Columns.Contains("Delete") = False Then
                Me.grdWarrantyClaim.RootTable.Columns.Add("Delete")
                Me.grdWarrantyClaim.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdWarrantyClaim.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdWarrantyClaim.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdWarrantyClaim.RootTable.Columns("Delete").Key = "Delete"
                Me.grdWarrantyClaim.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grdWarrantyClaim.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdWarrantyClaim.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            'objDal.Delete(objModel.ItemId)
            'SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.cmbItem.Value, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "SalesOrder" Then
                str = "select SalesOrderId, SalesOrderNo from SalesOrderMasterTable where Status = 'Open' ORDER BY SalesOrderDate DESC"
                FillDropDown(cmbSaleOrder, str, True)
            ElseIf Condition = "FinishGood" Then
                str = "SELECT FinishGoodMaster.Id, FinishGoodMaster.StandardName FROM ArticleDefView Left Outer JOIN SalesOrderDetailTable ON ArticleDefView.ArticleId = SalesOrderDetailTable.ArticleDefId LEFT OUTER JOIN FinishGoodMaster ON ArticleDefView.MasterID = FinishGoodMaster.MasterArticleId " & IIf(cmbSaleOrder.SelectedValue > 0, "where SalesOrderId = " & cmbSaleOrder.SelectedValue & "", "") & " Group By FinishGoodMaster.Id, FinishGoodMaster.StandardName"
                FillUltraDropDown(cmbProductMfd, str, True)
                Me.cmbProductMfd.Rows(0).Activate()
                Me.cmbProductMfd.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbProductMfd.DisplayLayout.Bands(0).Columns("StandardName").Width = 200
            ElseIf Condition = "Item" Then
                'Aashir: Removed Item filteration based on plan
                'start
                'str = "SELECT FinishGoodDetail.MaterialArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription FROM FinishGoodDetail LEFT OUTER JOIN ArticleDefView ON FinishGoodDetail.MaterialArticleId = ArticleDefView.ArticleId " & IIf(cmbProductMfd.Value > 0, "Where FinishGoodDetail.FinishGoodId = " & cmbProductMfd.Value & "", "") & ""
                str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item,   ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(PurchasePrice,0) as Price,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model,ArticleDefView.SortOrder, ISNull(ArticleDefView.SubSubId,0) as AccountId,Isnull(ServiceItem,0) as ServiceItem FROM  ArticleDefView where Active=1"
                If ItemSortOrder = True Then
                    str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByCode = True Then
                    str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByName = True Then
                    str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                Else
                    str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                End If
                FillUltraDropDown(cmbItem, str, True)
                Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                'End

                'Me.cmbItem.DisplayLayout.Bands(0).Columns("MaterialArticleId").Hidden = True
                If Me.rdoCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            ElseIf Condition = "Plan" Then
                str = "select PlanId, PlanNo from PlanMasterTable " & IIf(cmbSaleOrder.SelectedValue > 0, "where POId = " & cmbSaleOrder.SelectedValue & "", "") & " Order By 1 DESC"
                FillDropDown(cmbPlan, str, True)
            ElseIf Condition = "Ticket" Then
                str = "select PlanTicketsMasterID, TicketNo from PlanTicketsMaster " & IIf(cmbPlan.SelectedValue > 0, "Where PlanId = " & cmbPlan.SelectedValue & "", "") & " Order By 1 DESC"
                FillDropDown(cmbTicket, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New WarrantyMasterTable
            objModel.WarrantyMasterId = WarrantyMasterId
            objModel.WarrantyNo = txtDocNo.Text
            objModel.WarrantyDate = dtpDocNo.Value
            objModel.SOId = cmbSaleOrder.SelectedValue
            objModel.PlanId = cmbPlan.SelectedValue
            objModel.TicketId = cmbTicket.SelectedValue
            objModel.FinishGoodStandardId = cmbProductMfd.Value
            Detail = New List(Of WarrantyDetailTable)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdWarrantyClaim.GetDataRows
                Dim WDetail As New WarrantyDetailTable
                WDetail.WarrantyDetailId = Val(Row.Cells("WarrantyDetailId").Value.ToString)
                WDetail.FinishGoodId = Row.Cells("FinishGoodId").Value.ToString
                WDetail.Qty = Row.Cells("Qty").Value.ToString
                WDetail.Amount = Row.Cells("Amount").Value.ToString
                WDetail.Remarks = Row.Cells("Remarks").Value.ToString
                Detail.Add(WDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        
    End Sub

    Public Sub DisplayRecords(ByVal WarrantyMasterId As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT WarrantyDetailTable.WarrantyDetailId, WarrantyDetailTable.WarrantyMasterId, WarrantyDetailTable.FinishGoodId, ArticleDefView.ArticleDescription, WarrantyDetailTable.Qty, WarrantyDetailTable.Amount, WarrantyDetailTable.Remarks FROM WarrantyDetailTable Left Outer JOIN ArticleDefView ON WarrantyDetailTable.FinishGoodId = ArticleDefView.ArticleId where WarrantyMasterId = " & WarrantyMasterId & ""
            dt = GetDataTable(str)
            Me.grdWarrantyClaim.DataSource = dt
            Me.grdWarrantyClaim.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.grdWarrantyClaim.RowCount = 0 Then
                msg_Error("No record found in grid")
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
            txtDocNo.Text = GetDocumentNo()
            dtpDocNo.Value = Date.Now
            If Me.cmbItem.Rows.Count > 0 Then cmbItem.Rows(0).Activate()
            txtQuantity.Text = ""
            txtRemarks.Text = ""
            txtAmount.Text = ""
            btnSave.Enabled = DoHaveSaveRights
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GetDocumentNo() As String
        Dim DocNo As String = String.Empty
        Try
            DocNo = GetNextDocNo("WC-" & Format(Me.dtpDocNo.Value, "yy") & Me.dtpDocNo.Value.Month.ToString("00"), 4, "WarrantyMasterTable", "WarrantyNo")
            Return DocNo
        Catch ex As Exception
            msg_Error(ex.Message)
            Return String.Empty
        End Try
    End Function

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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Not Me.cmbItem.Value = 0 AndAlso Not txtQuantity.Text = "" AndAlso Not txtAmount.Text = "" Then
                AddToGrid()
                ReSetControls()
                Me.cmbItem.Focus()
            Else
                msg_Error("Please Enter Complete Data to add in Grid")
                Me.cmbItem.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub AddToGrid()
        Try
            Dim dt As DataTable
            dt = CType(Me.grdWarrantyClaim.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("WarrantyDetailId") = 0
            dr("FinishGoodId") = Me.cmbItem.Value
            dr("ArticleDescription") = Me.cmbItem.Text
            dr("Qty") = Me.txtQuantity.Text
            dr("Amount") = Me.txtAmount.Text
            dr("Remarks") = Me.txtRemarks.Text
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function saveStoreIssuence() As Boolean

        Me.txtPONo = frmStoreIssuenceNew.GetDocumentNo  'GetNextDocNo("I", 6, "DispatchMasterTable", "DispatchNo")
        setVoucherNo = Me.txtPONo
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection

        objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        gobjLocationId = MyCompanyId
        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo)
        Dim AccountId As Integer = getConfigValueByType("PurchaseDebitAccount")
        Dim AccountId2 As Integer = 0I 'getConfigValueByType("StoreIssuenceAccount")
        Dim flgCylinderVoucher As Boolean = getConfigValueByType("CylinderVoucher")
        Dim CylinderStockAccountId As Integer = getConfigValueByType("CylinderStockAccount")
        Dim flgAvrRate As Boolean = getConfigValueByType("AvgRate") '' Avrage Rate Apply
        Dim GLAccountArticleDepartment As Boolean = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        'Dim strvoucherNo As String = GetNextDocNo("JV", 6, "tblVoucher", "voucher_no")
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        AccountId2 = getConfigValueByType("StoreIssuenceAccount")
        
        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If

        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans

            objCommand.CommandText = "Insert into DispatchMasterTable(locationId,DispatchNo,DispatchDate,VendorId,PurchaseOrderId, DispatchQty,DispatchAmount, CashPaid, Remarks,UserName, PONo, PlanId, EmpId, FixedAssetAccountId,CylinderSecurityAccountId, Issued, DepartmentId,StoreIssuanceAccountId, PlanTicketId, SalesOrderId) values( " _
            & gobjLocationId & ", N'" & txtPONo & "',N'" & dtpDocNo.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," _
            & 0 & "," & 0 & ", " _
            & Val(Me.grdWarrantyClaim.GetTotal(Me.grdWarrantyClaim.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," _
            & Val(Me.grdWarrantyClaim.GetTotal(Me.grdWarrantyClaim.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " _
            & 0.0 & ", 'Warranty Claim Issuance',N'" & LoginUserName & "', N'" _
            & txtDocNo.Text.Replace("'", "''") & "', " & 0 _
            & ", " & 0 & ", " & 0 & ", " & 0 & ", " _
            & 1 & ", " & 0 & "," _
            & AccountId2 & ", " & 0 & ", " _
            & 0 & ") SELECT @@IDENTITY"

            Me.DispatchId = objCommand.ExecuteScalar()

            ''Voucher Master Entry

            objCommand.CommandText = ""

            objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                      & " cheque_no, cheque_date,post,Source,voucher_code,Remarks, UserName, Posted_UserName)" _
                                      & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo & "', N'" _
                                      & Me.dtpDocNo.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                      & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtDocNo.Text & "',N'Warranty Claim Issuance Voucher', N'" & LoginUserName.Replace("'", "''") & "', N'" & LoginUserName.Replace("'", "''") & "')" _
                                      & " SELECT @@IDENTITY"
                    'End Task:M101
                    lngVoucherMasterId = objCommand.ExecuteScalar
                

            Dim dtGrd As DataTable = CType(Me.grdWarrantyClaim.DataSource, DataTable)

            dtGrd.AcceptChanges()

            Dim i As Integer

            For i = 0 To dtGrd.Rows.Count - 1
                If dtGrd.Rows(i).Item("Qty") > 0 Then

                    'Dim dblPurchasePrice As Double = 0D
                    'Dim dblCostPrice As Double = 0D

                    'Dim strPriceData() As String = GetRateByItem(Val(dtGrd.Rows(i).Item("ItemId").ToString)).Split(",")

                    'If strPriceData.Length > 1 Then
                    '    dblCostPrice = Val(strPriceData(0).ToString)
                    '    dblPurchasePrice = Val(strPriceData(1).ToString)
                    'End If

                    objCommand.CommandText = ""
                    objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Pack_Desc, CostPrice, PlanQty, Lot_No,Rack_No,Comments, SubDepartmentID, AllocationDetailId, ParentId, EstimationId, TotalEstimatedQty, SubItem, PackPrice, TicketId, WIPAccountId, TicketQty) values( " _
                    & " ident_current('DispatchMasterTable'), " & Val(dtGrd.Rows(i).Item("FinishGoodId").ToString) & ",'Loose'," & Val(dtGrd.Rows(i).Item("Qty").ToString) & ", " _
                    & " " & Val(dtGrd.Rows(i).Item("Qty").ToString) & ", " _
                    & Val(dtGrd.Rows(i).Item("Amount").ToString) & ",1, " _
                    & Val(dtGrd.Rows(i).Item("Amount").ToString) & ",0," _
                    & 0 & "," & 1 & ", " _
                    & Val(dtGrd.Rows(i).Item("Amount").ToString) & ", N'" _
                    & String.Empty & "', " & 0 & ",N'" _
                    & String.Empty & "',N'" & String.Empty & "',N'" _
                    & dtGrd.Rows(i).Item("Remarks").ToString & "', " & 0 & " , " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ")"

                    objCommand.ExecuteNonQuery()



                    ''Detail Voucher Entry'


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & Val(dtGrd.Rows(i).Item("Qty").ToString) * Val(dtGrd.Rows(i).Item("Amount").ToString) & ", N'" & dtGrd.Rows(i).Item("ArticleDescription").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Val(dtGrd.Rows(i).Item("Amount").ToString) & ")', " & 0 & " )"  ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & Val(dtGrd.Rows(i).Item("Qty").ToString) * Val(dtGrd.Rows(i).Item("Amount").ToString) & " ,0 , N'" & dtGrd.Rows(i).Item("ArticleDescription").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Val(dtGrd.Rows(i).Item("Amount").ToString) & ")',  " & 0 & ")"  ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()


                End If
            Next

            setVoucherNo = Me.txtPONo
            setVoucherdate = Me.dtpDocNo.Value.ToString("yyyy-M-d h:mm:ss tt")

            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Return False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

    End Function





    Public Function updateStoreIssuence() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo)
        Dim AccountId As Integer = getConfigValueByType("PurchaseDebitAccount")
        Dim AccountId2 As Integer = 0I 'getConfigValueByType("StoreIssuenceAccount")
        Dim flgCylinderVoucher As Boolean = getConfigValueByType("CylinderVoucher")
        Dim CylinderStockAccountId As Integer = getConfigValueByType("CylinderStockAccount")
        Dim flgAvrRate As Boolean = getConfigValueByType("AvgRate") '' Avrage Rate Apply 
        Dim GLAccountArticleDepartment As Boolean = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        AccountId2 = getConfigValueByType("StoreIssuenceAccount")
        
        objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Me.grdWarrantyClaim.Update()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            Dim str1 As String
            str1 = "Select DispatchId from DispatchMasterTable where PONo = '" & objModel.WarrantyNo & "'"
            Dim dt1 As DataTable = UtilityDAL.GetDataTable(str1)
            Dim DispatchId As Integer
            If dt1.Rows.Count > 0 Then
                DispatchId = dt1.Rows(0).Item("DispatchId")
            End If
            objCommand.CommandText = ""

            'TASK: TFS1136  Added SalesOrderId to be updated.
            objCommand.CommandText = "Update DispatchMasterTable set DispatchDate=N'" & dtpDocNo.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & 0 & ", PurchaseOrderId=" & 0 & ", " _
                                 & " DispatchQty=" & Val(Me.grdWarrantyClaim.GetTotal(Me.grdWarrantyClaim.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",DispatchAmount=" & Val(Me.grdWarrantyClaim.GetTotal(Me.grdWarrantyClaim.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & 0 & ", Remarks=N'Warranty Claim Issuance' , Issued=" & 1 & " , StoreIssuanceAccountId=" & AccountId2 & ", UpdateUser = '" & LoginUserName & "', PONo = '" & txtDocNo.Text & "'  Where DispatchID= " & Me.DispatchId & " " ''TASK-408 added TotalQty instead of Qty on 13-06-2016
            'Altered by Ali Ansari Task#201508020 regarding update user column
            objCommand.ExecuteNonQuery()
            If lngVoucherMasterId > 0 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpDocNo.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=1, Remarks=N'" & Me.txtRemarks.Text.Replace("'", "''") & "', Posted_UserName=N'" & LoginUserName.Replace("'", "''") & "' " _
                         & "   where voucher_id=" & lngVoucherMasterId
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
                objCommand.ExecuteNonQuery()

            Else

                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                          & " cheque_no, cheque_date,post,Source,voucher_code,Remarks, UserName, Posted_UserName)" _
                                          & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo & "', N'" & Me.dtpDocNo.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                          & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtDocNo.Text & "',N'Warranty Claim Issuance Voucher', N'" & LoginUserName.Replace("'", "''") & "', N'" & LoginUserName.Replace("'", "''") & "') " _
                                          & " SELECT @@IDENTITY"
                lngVoucherMasterId = objCommand.ExecuteScalar
            End If

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from DispatchDetailTable where DispatchID = " & Me.DispatchId
            objCommand.ExecuteNonQuery()



            Dim dtGrd As DataTable = CType(Me.grdWarrantyClaim.DataSource, DataTable)
            dtGrd.AcceptChanges()
            For i = 0 To dtGrd.Rows.Count - 1

                If dtGrd.Rows(i).Item("Qty") > 0 Then


                    'Dim dblPurchasePrice As Double = 0D
                    'Dim dblCostPrice As Double = 0D

                    'Dim strPriceData() As String = GetRateByItem(Val(dtGrd.Rows(i).Item("ItemId").ToString)).Split(",")

                    'If strPriceData.Length > 1 Then
                    '    dblCostPrice = Val(strPriceData(0).ToString)
                    '    dblPurchasePrice = Val(strPriceData(1).ToString)
                    'End If


                    objCommand.CommandText = ""

                    objCommand.CommandText = ""
                    objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Pack_Desc, CostPrice, PlanQty, Lot_No,Rack_No,Comments, SubDepartmentID, AllocationDetailId, ParentId, EstimationId, TotalEstimatedQty, SubItem, PackPrice, TicketId, WIPAccountId, TicketQty) values( " _
                    & " ident_current('DispatchMasterTable'), " & Val(dtGrd.Rows(i).Item("FinishGoodId").ToString) & ",'Loose'," & Val(dtGrd.Rows(i).Item("Qty").ToString) & ", " _
                    & " " & Val(dtGrd.Rows(i).Item("Qty").ToString) & ", " _
                    & Val(dtGrd.Rows(i).Item("Amount").ToString) & ",1, " _
                    & Val(dtGrd.Rows(i).Item("Amount").ToString) & ",0," _
                    & 0 & "," & 1 & ", " _
                    & Val(dtGrd.Rows(i).Item("Amount").ToString) & ", N'" _
                    & String.Empty & "', " & 0 & ",N'" _
                    & String.Empty & "',N'" & String.Empty & "',N'" _
                    & dtGrd.Rows(i).Item("Remarks").ToString & "', " & 0 & " , " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ")"

                    objCommand.ExecuteNonQuery()


                    ''Detail Voucher Entry'


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 ,  " & Val(dtGrd.Rows(i).Item("Qty").ToString) * Val(dtGrd.Rows(i).Item("Amount").ToString) & ", N'" & dtGrd.Rows(i).Item("ArticleDescription").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Val(dtGrd.Rows(i).Item("Amount").ToString) & ")', " & 0 & " )"  ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ",  " & Val(dtGrd.Rows(i).Item("Qty").ToString) * Val(dtGrd.Rows(i).Item("Amount").ToString) & " ,0 , N'" & dtGrd.Rows(i).Item("ArticleDescription").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Val(dtGrd.Rows(i).Item("Amount").ToString) & ")',  " & 0 & ")"  ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()


                End If
            Next

            setVoucherNo = Me.txtPONo
            setVoucherdate = Me.dtpDocNo.Value.ToString("yyyy-M-d h:mm:ss tt")

            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Return False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try

    End Function



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If WarrantyMasterId = 0 Then
                    If saveStoreIssuence() Then
                        If objDal.Add(objModel) Then
                            If objDal.AddWarrantyDetail(Detail) Then
                                msg_Information("Record has been saved successfully.")
                                ReSetControls()
                                Me.Close()
                            End If
                        End If
                    End If
                    SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, txtDocNo.Text, True)
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If updateStoreIssuence() Then
                        objModel.DispatchId = Me.DispatchId
                        objModel.StockTransId = StockTransId(Me.txtDocNo.Text)
                        If objDal.Update(objModel) Then
                            If objDal.AddWarrantyDetail(Detail) Then
                                msg_Information("Record has been Updated successfully.")
                                ReSetControls()
                                Me.Close()
                            End If
                        End If
                    End If
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtDocNo.Text, True)
                    End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSaleOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSaleOrder.SelectedIndexChanged
        Try
            FillCombos("FinishGood")
            FillCombos("Plan")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProductMfd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProductMfd.ValueChanged
        Try
            FillCombos("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdWarrantyClaim_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdWarrantyClaim.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grdWarrantyClaim.GetRow.Delete()
                grdWarrantyClaim.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try
            FillCombos("Ticket")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged
        Try
            If IsFormOpened = False Then Exit Sub
            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
    End Sub
End Class