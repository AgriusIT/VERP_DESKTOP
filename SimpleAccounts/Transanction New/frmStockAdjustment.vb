''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''''''Task No 2619 Mughees Escape Code Updation 
'' TASK TFS1592 Ayesha Rehman on 19-10-2017 Future date entry should be rights based
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
Imports SBModel
Imports SBDal
Imports SBUtility
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Net
Public Class frmStockAdjustment
    Implements IGeneral
    Dim StockAdjustmentId As Integer = 0I
    Dim IsOpenForm As Boolean = False
    Dim StockAdjustment As StockAdjustmenMasterBE
    Dim flgSelectedItem As Boolean = False
    Dim flgAdjShort As Boolean = False
    Dim AdjTypeList As List(Of AdjustmentTypeBE)
    Dim blnUpdateAll As Boolean = False
    'Code Edit for task 1592 future date rights
    Dim IsDateChangeAllowed As Boolean = False
    Dim flgLocationWiseItems As Boolean = False ''TFS3786
    Dim DateWiseAverageRate As Boolean = False
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Enum enmGrd
        LocationId
        AdjTypeId
        ArticleCategory
        ArticleId
        ArticleCode
        ArticleDescription
        ArticleSizeName
        ArticleColorName
        ArticleSize
        PackQty
        Qty
        BatchNo
        ExpiryDate
        Origin
        TotalQty
        Price
        CurrentPrice
        Total
        Pack_Desc
    End Enum

    Private Sub frmStockAdjustment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                ''''''Task No 2619 Mughees Escape Code Updation 
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                'End Task 2619
                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                Button1_Click(Nothing, Nothing)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub
    Private Sub frmStockAdjustment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            FillCombos("item")
            FillCombos("projects")
            FillCombos("location")
            FillCombos("type")
            FillCombos("ArticlePack")
            FillCombos("BatchNo")
            If Not getConfigValueByType("DateWiseAverageRate").ToString = "Error" Then
                DateWiseAverageRate = Convert.ToBoolean(getConfigValueByType("DateWiseAverageRate").ToString)
            End If
            AdjTypeList = StockAdjustmentDAL.GetAllTypeList
            IsOpenForm = True
            'Aashir, Smart search on item combo box 
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub frmStockAdjustment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Detail" Then
                For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                    If c.Index <> enmGrd.LocationId AndAlso c.Index <> enmGrd.AdjTypeId AndAlso c.Index <> enmGrd.Qty AndAlso c.Index <> enmGrd.Price AndAlso c.Index <> enmGrd.ExpiryDate AndAlso c.Index <> enmGrd.Origin Then
                        c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                    Me.grd.RootTable.Columns("BatchNo").Visible = True
                    'Me.grd.RootTable.Columns("BatchNo").HasValueList = True
                    'Me.grd.RootTable.Columns("BatchNo").LimitToList = False
                    'Me.grd.RootTable.Columns("BatchNo").EditType = Janus.Windows.GridEX.EditType.Combo
                    Me.grd.RootTable.Columns("ExpiryDate").Visible = True
                    Me.grd.RootTable.Columns("ExpiryDate").EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                    Me.grd.RootTable.Columns("Origin").Visible = True
                    Me.grd.RootTable.Columns("Origin").HasValueList = True
                    Me.grd.RootTable.Columns("Origin").LimitToList = False
                    Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo
                Next
            ElseIf Condition = "Master" Then
                Me.grdDataHistory.RootTable.Columns.Add("Selector1")
                Me.grdDataHistory.RootTable.Columns("Selector1").UseHeaderSelector = True
                Me.grdDataHistory.RootTable.Columns("Selector1").ActAsSelector = True
                Me.grdDataHistory.RootTable.Columns("Selector1").Width = 50
                Me.grdDataHistory.RootTable.Columns("Project").Visible = False
                Me.grdDataHistory.RootTable.Columns("SA_ID").Visible = False
                Me.grdDataHistory.AutoSizeColumns()
            End If
            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("ArticleSize").Index
            Me.grd.RootTable.Columns("ArticleSize").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnAdjustmentType.Enabled = True
                Me.btnUpdateAll.Enabled = True
                Me.tsbTask.Enabled = True
                Me.tsbConfig.Enabled = True
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = True
                dtpDate.MaxDate = Date.Today.AddMonths(3)
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    'Me.btnSearchDelete.Enabled = False
                    'Me.btnSearchPrint.Enabled = False
                    Me.btnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False

                    'Task 1592 Apply future date rights
                    IsDateChangeAllowed = False
                    DateChange(False)

                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmExpense)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                'UserPostingRights = GetUserPostingRights(LoginUserId)
                'If UserPostingRights = True Then
                '    Me.chkPost.Visible = True
                'Else
                '    Me.chkPost.Visible = False
                '    Me.chkPost.Checked = False
                'End If
                'GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                'Me.btnSearchDelete.Enabled = False
                'Me.btnSearchPrint.Enabled = False
                Me.btnPrint.Enabled = False
                'Me.chkPost.Visible = False

                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)
                Me.btnAdjustmentType.Enabled = False
                Me.btnUpdateAll.Enabled = False
                Me.tsbTask.Enabled = False
                Me.tsbConfig.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False ''TFS1823
                CtrlGrdBar2.mGridExport.Enabled = False ''TFS1823
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        ' Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        'Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Add Ajustment Type" Then
                        Me.btnAdjustmentType.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update All" Then
                        Me.btnUpdateAll.Enabled = True

                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                    ElseIf RightsDt.FormControlName = "Task" Then
                        Me.tsbTask.Enabled = True
                    ElseIf RightsDt.FormControlName = "Config" Then
                        Me.tsbConfig.Enabled = True
                        'Me.btnSearchPrint.Enabled = True
                        ' CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True ''TFS1823
                        CtrlGrdBar2.mGridExport.Enabled = True ''TFS1823
                    ElseIf RightsDt.FormControlName = "Post" Then
                        'Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Task:1592 Added Future Date Rights
    Private Sub DateChange(ByRef IsDateChangeAllowed As Boolean)
        Try
            If IsDateChangeAllowed = False Then
                dtpDate.MaxDate = DateTime.Now.ToString("yyyy/M/d 23:59:59")
            Else
                dtpDate.MaxDate = Date.Today.AddMonths(3)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'END TASk:1592
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            StockAdjustment = New StockAdjustmenMasterBE
            StockAdjustment.SA_id = grdDataHistory.GetRow.Cells("SA_ID").Value
            StockAdjustment.Doc_no = grdDataHistory.GetRow.Cells("Doc_No").Value
            If New StockAdjustmentDAL().delete(StockAdjustment) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "projects" Then
                FillDropDown(cmbProject, "Select CostCenterId, Name  from tblDefCostCenter")
            ElseIf Condition = "location" Then
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strSQL = "Select location_id, location_name from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") "
                'Else
                '    strSQL = "Select location_id, location_name from tblDefLocation"
                'End If
                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
               & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
               & " Else " _
               & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillDropDown(cmbLocation, strSQL, False)
            ElseIf Condition = "type" Then
                FillDropDown(cmbType, "Select AdjType_Id, AdjType from tblAdjustmentType")
            ElseIf Condition = "item" Then
                Dim Str As String = "SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Location.Ranks as Rake, IsNull(ArticleDefView.PackQty,0) as PackQty, ArticleDefView.ArticleCompanyName  FROM ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId where Active=1"
                Me.cmbItem.DataSource = Nothing
                FillUltraDropDown(Me.cmbItem, Str)
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("PackQty").Hidden = True
                    If Me.rdoCode.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
            ElseIf Condition = "ArticlePack" Then
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            ElseIf Condition = "BatchNo" Then
                ''TASK TFS2572 (Raw material batch no) done on 28-02-2018
                Dim Str As String = "Select  DISTINCT BatchNo, BatchNo From  StockDetailTable  WHERE BatchNo NOT IN ('', '0','xxxx') AND LocationId=" & Me.cmbLocation.SelectedValue & ""
                If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                End If
                If cmbItem.SelectedRow.Index > 0 Then
                    Str += " And ArticledefId=" & Me.cmbItem.Value & " "
                End If
                Str += "  Group By BatchNo Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0 ORDER BY StockDetailTable.BatchNo ASC"
                FillDropDown(Me.cmbBatchNo, Str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try


            StockAdjustment = New StockAdjustmenMasterBE
            StockAdjustment.SA_id = StockAdjustmentId
            StockAdjustment.Doc_no = Me.txtDocNo.Text
            StockAdjustment.Doc_Date = Me.dtpDate.Value.ToString("yyyy-M-d h:mm:ss tt")
            StockAdjustment.Project = Me.cmbProject.SelectedValue
            StockAdjustment.remarks = Me.txtRemarks.Text.Replace("'", "''")
            StockAdjustment.StockMaster = New StockMaster

            Dim transId As Integer = 0
            'transId = Convert.ToInt32(GetStockTransId(Me.txtDocNo.Text).ToString)
            With StockAdjustment.StockMaster
                .StockTransId = 0 'transId
                .DocDate = Me.dtpDate.Value
                .DocNo = Me.txtDocNo.Text
                .Project = Me.cmbProject.SelectedValue
                .DocType = StockDocTypeDAL.GetStockDocTypeId("SA")
                .Remaks = Me.txtRemarks.Text
            End With
            StockAdjustment.StockAdjustmentDetail = New List(Of StockAdjustmenDetailBE)
            StockAdjustment.StockMaster.StockDetailList = New List(Of StockDetail)
            Dim StockDt As StockDetail
            Dim StockAdjustmentDt As StockAdjustmenDetailBE
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                StockAdjustmentDt = New StockAdjustmenDetailBE
                With StockAdjustmentDt
                    .location_id = r.Cells("LocationId").Value
                    .AdjustmentTypeId = r.Cells("AdjustmentTypeId").Value
                    .Artical_id = r.Cells("ArticleDefId").Value
                    .ArticalSize = r.Cells("ArticleSize").Value
                    .S1 = Val(r.Cells("PackQty").Value.ToString)
                    .S2 = 0
                    .S3 = 0
                    .S4 = 0
                    .S5 = 0
                    .S6 = 0
                    .S7 = Val(r.Cells("Qty").Value.ToString)
                    .Qty = IIf(r.Cells("ArticleSize").Value = "Loose", Val(r.Cells("Qty").Value), Val(r.Cells("Qty").Value) * Val(r.Cells("PackQty").Value))
                    .Price = Val(r.Cells("Price").Value.ToString)
                    .Current_price = Val(r.Cells("CurrentPrice").Value.ToString)
                    .Pack_Desc = r.Cells("Pack_Desc").Value.ToString
                    .BatchNo = r.Cells("BatchNo").Value.ToString
                    .ExpiryDate = CType(r.Cells("ExpiryDate").Value.ToString, Date).ToString("yyyy-M-d h:mm:ss tt")
                    .Origin = r.Cells("Origin").Value.ToString
                End With
                StockAdjustment.StockAdjustmentDetail.Add(StockAdjustmentDt)

                StockDt = New StockDetail
                With StockDt
                    .StockTransId = transId
                    .ArticleDefId = r.Cells("ArticleDefId").Value
                    .LocationId = r.Cells("LocationId").Value
                    .Remarks = String.Empty
                    ''Below lines are commented against TASK TFS4167 Because AvgRate condition should be removed and grid price value should be saved. Done by Amin on 08-08-2018

                    If getConfigValueByType("AvgRate").ToString = "True" Then
                        '    .Rate = GetAvgRateByItem(Val(r.Cells("ArticleDefId").Value.ToString)) 'r.Cells("Price").Value
                        'Else
                        Dim strData As String
                        If Val(r.Cells("AdjustmentTypeId").Value) = 1 Then
                            If Val(r.Cells("Price").Value) <> 0 Then
                                Dim str As String = "Select * from stockmastertable where docno = '" & Me.txtDocNo.Text & "'"
                                Dim dtdocdata As DataTable = GetDataTable(str)
                                If dtdocdata.Rows.Count > 0 Then
                                    strData = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (Val(r.Cells("Qty").Value)) & "  as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) + " & Val(r.Cells("Price").Value) * Val(r.Cells("Qty").Value) & " BalanceAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & r.Cells("ArticleDefId").Value & " AND StockMasterTable.DocNo <> '" & Me.txtDocNo.Text & "' AND StockMasterTable.DocDate < '" & Me.dtpDate.Value & "' Group By ArticleDefId "
                                Else
                                    strData = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (Val(r.Cells("Qty").Value)) & "  as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) + " & Val(r.Cells("Price").Value) * Val(r.Cells("Qty").Value) & " BalanceAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & r.Cells("ArticleDefId").Value & " AND StockMasterTable.DocDate < '" & Me.dtpDate.Value & "'  Group By ArticleDefId "
                                End If
                                Dim dblCostPrice As Double = 0D
                                Dim dtLastestPriceData As New DataTable
                                dtLastestPriceData = GetDataTable(strData)
                                dtLastestPriceData.AcceptChanges()
                                If dtLastestPriceData.Rows.Count > 0 Then
                                    If Val(dtLastestPriceData.Rows(0).Item(1).ToString) > 0 Then
                                        .CostPrice = (Val(dtLastestPriceData.Rows(0).Item(2).ToString) / Val(dtLastestPriceData.Rows(0).Item(1).ToString))
                                    Else
                                        .CostPrice = 0
                                    End If
                                Else
                                    .CostPrice = 0
                                End If
                                .Rate = Val(r.Cells("Price").Value)
                            Else

                                .CostPrice = 0
                            End If
                        Else
                            If Val(r.Cells("Price").Value) <> 0 Then
                                Dim str As String = "Select * from stockmastertable where docno = '" & Me.txtDocNo.Text & "'"
                                Dim dtdocdata As DataTable = GetDataTable(str)
                                If dtdocdata.Rows.Count > 0 Then
                                    strData = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0) as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) as BalanceAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & r.Cells("ArticleDefId").Value & " AND StockMasterTable.DocNo <> '" & Me.txtDocNo.Text & "' AND StockMasterTable.DocDate < '" & Me.dtpDate.Value & "' Group By ArticleDefId "
                                Else
                                    strData = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0) as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) as BalanceAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & r.Cells("ArticleDefId").Value & " AND StockMasterTable.DocDate < '" & Me.dtpDate.Value & "'  Group By ArticleDefId "
                                End If
                                Dim dblCostPrice As Double = 0D
                                Dim dtLastestPriceData As New DataTable
                                dtLastestPriceData = GetDataTable(strData)
                                dtLastestPriceData.AcceptChanges()
                                If dtLastestPriceData.Rows.Count > 0 Then
                                    If Val(dtLastestPriceData.Rows(0).Item(1).ToString) > 0 Then
                                        .Rate = (Val(dtLastestPriceData.Rows(0).Item(2).ToString) / Val(dtLastestPriceData.Rows(0).Item(1).ToString))
                                    Else
                                        .Rate = 0
                                    End If
                                Else
                                    .Rate = 0
                                End If
                            Else

                                .Rate = 0
                            End If
                        End If
                        
                    Else
                        .Rate = Val(r.Cells("Price").Value)
                    End If

                    '.Rate = Val(r.Cells("Price").Value)
                    'End If
                    .InQty = IIf(Val(r.Cells("Qty").Value) > 0, IIf(r.Cells("ArticleSize").Value = "Loose", Val(r.Cells("Qty").Value), Val(r.Cells("Qty").Value) * Val(r.Cells("PackQty").Value)), 0)
                    .InAmount = IIf(Val(r.Cells("Qty").Value) > 0, IIf(r.Cells("ArticleSize").Value = "Loose", (Val(r.Cells("Qty").Value) * Val(.Rate)), (Val(r.Cells("Qty").Value) * Val(r.Cells("PackQty").Value)) * Val(.Rate)), 0)
                    .OutQty = IIf(Val(r.Cells("Qty").Value) < 0, IIf(r.Cells("ArticleSize").Value = "Loose", Math.Abs(Val(r.Cells("Qty").Value)), Math.Abs(Val(r.Cells("Qty").Value) * Val(r.Cells("PackQty").Value))), 0)
                    .OutAmount = IIf(Val(r.Cells("Qty").Value) < 0, IIf(r.Cells("ArticleSize").Value = "Loose", Math.Abs((Val(r.Cells("Qty").Value) * Val(.Rate))), Math.Abs((Val(r.Cells("Qty").Value) * Val(r.Cells("PackQty").Value)) * Val(.Rate))), 0)
                    .In_PackQty = IIf(Val(r.Cells("Qty").Value) > 0, Val(r.Cells("Qty").Value), 0)
                    .Out_PackQty = IIf(Val(r.Cells("Qty").Value) < 0, Math.Abs(Val(r.Cells("Qty").Value)), 0)
                    .PackQty = Val(r.Cells("PackQty").Value)
                    .BatchNo = r.Cells("BatchNo").Value.ToString
                    .ExpiryDate = CType(r.Cells("ExpiryDate").Value.ToString, Date).ToString("yyyy-M-d h:mm:ss tt")
                    .Origin = r.Cells("Origin").Value.ToString
                End With
                StockAdjustment.StockMaster.StockDetailList.Add(StockDt)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function GetStockTransId(ByVal ByDocNo As String) As String
        Try
            'Get All Records From Stock Master Table
            Dim dtStockTable As DataTable
            dtStockTable = New StockDAL().GetAllRecord()
            dtStockTable.TableName = "StockTrans"
            If GetFilterDataFromDataTable(dtStockTable, "[DocNo]='" & ByDocNo & "'").ToTable("StockTrans").Rows.Count < 1 Then
                Return "0"
            Else
                Return GetFilterDataFromDataTable(dtStockTable, "[DocNo]='" & ByDocNo & "'").ToTable("StockTrans").Rows(0).Item("StockTransId").ToString()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then
                Dim dt As New DataTable
                dt = New StockAdjustmentDAL().getAll
                Me.grdDataHistory.DataSource = dt
                Me.grdDataHistory.RetrieveStructure()
                ApplyGridSettings("Master")
            ElseIf Condition = "Detail" Then
                Dim dtDetail As New DataTable
                dtDetail = New StockAdjustmentDAL().getAllDetail(StockAdjustmentId)
                ''Below line is commented against TASK TFS4169 ON 08-08-2018
                'dtDetail.Columns("Total").Expression = "IIF(ArticleSize='Loose', (Qty*Price), ((Qty*PackQty)*Price))"
                ''Below two lines are commented against TASK TFS4169 ON 08-08-2018 as per TotalQty is added.
                dtDetail.Columns("TotalQty").Expression = "Qty*PackQty"
                dtDetail.Columns("Total").Expression = "TotalQty*Price"

                Me.grd.DataSource = dtDetail
                ApplyGridSettings("Detail")

                Dim dtLocation As New DataTable
                dtLocation = GetDataTable("Select Location_Id, Location_Name From tblDefLocation")
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")

                Dim dtType As New DataTable
                dtType = GetDataTable("Select AdjType_id, AdjType From tblAdjustmentType")
                Me.grd.RootTable.Columns("AdjustmentTypeId").ValueList.PopulateValueList(dtType.DefaultView, "AdjType_id", "AdjType")

                Dim dtOrigin As New DataTable
                dtOrigin = GetDataTable("select CountryName, CountryName From tblListCountry Where Active = 1")
                Me.grd.RootTable.Columns("Origin").ValueList.PopulateValueList(dtOrigin.DefaultView, "CountryName", "CountryName")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            'Task#127062015 if grd doesn't have any row then return false
            grd.UpdateData()
            If grd.RowCount <= 0 Then
                ShowErrorMessage("You can't save because Grid has no record")
                Return False
            End If
            'End Task#127062015 
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
            ErrorProvider1.Clear()
            StockAdjustmentId = 0
            Me.dtpDate.Value = Date.Now
            Me.dtpDate.Enabled = True
            Me.txtDocNo.Text = GetDocument()
            Me.cmbProject.SelectedIndex = 0
            Me.txtRemarks.Text = String.Empty
            Me.cmbLocation.SelectedIndex = 0
            Me.cmbType.SelectedIndex = 0
            Me.cmbUnit.SelectedIndex = 0
            Me.cmbItem.Rows(0).Activate()
            If Not Me.cmbBatchNo.SelectedIndex = -1 Then Me.cmbBatchNo.SelectedIndex = 0
            Me.btnSave.Text = "&Save"
            Me.flgSelectedItem = False
            blnUpdateAll = False
            Me.GetAllRecords("Master")
            Me.GetAllRecords("Detail")
            ClearData()
            ApplySecurity(EnumDataMode.[New])
            If Not getConfigValueByType("DateWiseAverageRate").ToString = "Error" Then
                DateWiseAverageRate = Convert.ToBoolean(getConfigValueByType("DateWiseAverageRate").ToString)
            End If
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnEdit.Visible = False
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False
            '''''''''''''''''''''''''''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If New StockAdjustmentDAL().save(StockAdjustment) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New StockAdjustmentDAL().update(StockAdjustment) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDocument() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SA" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "StockAdjustmentMaster", "Doc_No")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SA" & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "StockAdjustmentMaster", "Doc_No")
            Else
                Return GetNextDocNo("SA", 6, "StockAdjustmentMaster", "Doc_No")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            If Not IsValidateGrid() = True Then Exit Sub
            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            If dtGrd Is Nothing Then Exit Sub
            Dim type As AdjustmentTypeBE = AdjTypeList.Find(AddressOf flgAdj)
            Dim dr As DataRow
            dr = dtGrd.NewRow
            dr(0) = Me.cmbLocation.SelectedValue
            dr(1) = Me.cmbType.SelectedValue
            dr(2) = Me.cmbItem.ActiveRow.Cells("ArticleCompanyName").Text.ToString
            dr(3) = Me.cmbItem.Value
            dr(4) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            dr(5) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            dr(6) = Me.cmbItem.ActiveRow.Cells("Size").Text.ToString
            dr(7) = Me.cmbItem.ActiveRow.Cells("Combination").Text.ToString
            dr(8) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            dr(9) = Val(Me.txtPQty.Text)
            dr(10) = IIf(type.AdjustmentInShort = False, Val(Me.txtQty.Text), "-" & Val(Me.txtQty.Text))
            dr.Item("BatchNo") = Me.cmbBatchNo.Text
            dr(11) = IIf(type.AdjustmentInShort = False, Val(Me.txtTotalQty.Text), "-" & Val(Me.txtTotalQty.Text))
            dr(12) = Val(Me.txtPrice.Text)
            dr(13) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value)
            dr(14) = Val(Me.txtTotal.Text)
            'dr(13) = Me.cmbUnit.Text.ToString
            dr(15) = Me.cmbUnit.Text.ToString
            dtGrd.Rows.InsertAt(dr, 0)
            dtGrd.AcceptChanges()
            flgSelectedItem = True
            ClearData()
            If cmbType.SelectedValue = 2 Then
                grd.MoveLast()
                grd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function IsValidateGrid() As Boolean
        Try
            If Me.cmbType.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Ajustment Type")
                Me.cmbType.Focus()
                Return False
            End If
            If Me.cmbItem.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please Select Any Item")
                Me.cmbItem.Focus()
                Return False
            End If
            If Me.txtQty.Text = 0 Then
                ShowErrorMessage("Please Enter Qty")
                Me.txtQty.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ClearData()
        Try
            If flgSelectedItem = False Then
                Me.cmbItem.Rows(0).Activate()
            Else
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.Focus()
                Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            End If
            Me.txtPQty.Text = 1
            Me.txtQty.Text = 0
            Me.txtPrice.Text = 0
            Me.txtTotal.Text = 0
            Me.txtTotalQty.Text = 0
            Me.cmbBatchNo.SelectedIndex = -1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged, txtPQty.TextChanged, txtPrice.TextChanged, txtTotalQty.TextChanged
        Try
            Try
                If Val(Me.txtPQty.Text) = 0 Then
                    txtPQty.Text = 1
                    txtTotalQty.Text = Val(txtQty.Text)
                Else
                    txtTotalQty.Text = Val(txtQty.Text) * Val(txtPQty.Text)
                End If
                txtTotal.Text = Val(txtTotalQty.Text) * Val(txtPrice.Text)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            'Dim txt As TextBox = CType(sender, TextBox)
            'Select Case txt.Name
            '    Case txtPQty.Name
            '        Me.txtTotal.Text = IIf(Me.cmbUnit.Text = "Loose", Val(Me.txtQty.Text) * Val(Me.txtPrice.Text), ((Val(Me.txtPQty.Text) * Val(txtQty.Text)) * Val(Me.txtPrice.Text)))
            '    Case Me.txtQty.Name
            '        Me.txtTotal.Text = IIf(Me.cmbUnit.Text = "Loose", Val(Me.txtQty.Text) * Val(Me.txtPrice.Text), ((Val(Me.txtPQty.Text) * Val(txtQty.Text)) * Val(Me.txtPrice.Text)))
            '    Case Me.txtPrice.Name
            '        Me.txtTotal.Text = IIf(Me.cmbUnit.Text = "Loose", Val(Me.txtQty.Text) * Val(Me.txtPrice.Text), ((Val(Me.txtPQty.Text) * Val(txtQty.Text)) * Val(Me.txtPrice.Text)))
            'End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try

            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try

            grdDataHistory_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdDataHistory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDataHistory.DoubleClick
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            StockAdjustmentId = Me.grdDataHistory.GetRow.Cells("SA_ID").Value
            Me.txtDocNo.Text = Me.grdDataHistory.GetRow.Cells("Doc_No").Value.ToString
            'Me.dtpDate.Value = Me.grdDataHistory.GetRow.Cells("Doc_Date").Value
            ApplySecurity(EnumDataMode.Edit)
            'Task 1592
            If Me.grdDataHistory.GetRow.Cells("Doc_Date").Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                dtpDate.MaxDate = dtpDate.Value.AddMonths(3)
                dtpDate.Value = CType(Me.grdDataHistory.GetRow.Cells("Doc_Date").Value, Date)
            Else
                dtpDate.Value = CType(Me.grdDataHistory.GetRow.Cells("Doc_Date").Value, Date)
            End If
            Me.cmbProject.SelectedValue = Val(Me.grdDataHistory.GetRow.Cells("Project").Value.ToString)
            Me.txtRemarks.Text = Me.grdDataHistory.GetRow.Cells("Remarks").Value.ToString
            btnSave.Text = "&Update"
            GetAllRecords("Detail")
            If blnUpdateAll = False Then Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            'ApplySecurity(EnumDataMode.Edit)
            '//Start TASK # 1592
            '24-OCT-2017: Ayesha Rehman: If user dont have update rights then btnsave should not be enable true
            If btnSave.Enabled = True Then
                If Me.grdDataHistory.GetRow.Cells("Doc_Date").Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                    Me.btnSave.Enabled = False
                    ErrorProvider1.SetError(Me.Label5, "Future Date can not be edit")
                    ErrorProvider1.BlinkRate = 1000
                    ErrorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                Else
                    Me.btnSave.Enabled = True
                    ErrorProvider1.Clear()
                End If
            End If
            'End Task # 1592
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpDate.Enabled = False
                Else
                    Me.dtpDate.Enabled = True
                End If
            Else
                Me.dtpDate.Enabled = True
            End If

            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnDelete.Visible = True
            Me.btnPrint.Visible = True
            ''''''''''''''''''''''''''
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If IsValidate() = True Then
                Me.grd.UpdateData()
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informUpdate)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 27-1-14 
            Me.grdDataHistory.CurrentRow.Delete()
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer = 0
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("item")
            Me.cmbItem.ActiveRow.Cells(0).Value = id
            id = Me.cmbProject.SelectedValue
            FillCombos("projects")
            Me.cmbProject.SelectedValue = id
            id = Me.cmbLocation.SelectedValue
            FillCombos("location")
            Me.cmbLocation.SelectedValue = id
            id = Me.cmbType.SelectedValue
            FillCombos("type")
            Me.cmbType.SelectedValue = id

            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''Below lines are written against TASK TFS4169
        Try
            If Me.cmbUnit.Text = "Loose" Then
                txtPQty.Text = 1
                Me.txtPQty.Enabled = False
                Me.txtPQty.TabStop = False
                Me.txtTotalQty.Enabled = False
            Else
                ''Start TFS4161 : Ayesha Rehman : 09-08-2018 : Disable Pack Quantity if configuration (Disable Pack Qty) is on 
                If IsPackQtyDisabled = True Then
                    Me.txtPQty.Enabled = False
                    Me.txtPQty.TabStop = False
                    Me.txtTotalQty.Enabled = False
                Else
                    Me.txtPQty.Enabled = True
                    Me.txtPQty.TabStop = True
                    Me.txtTotalQty.Enabled = True
                End If
                If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                    Me.txtPQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString) 'GetPackQty(Me.cmbItem.Value)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        'Try
        '    If Not Me.cmbItem.ActiveRow IsNot Nothing Then Exit Sub
        '    If Val(Me.txtPQty.Text) = 0 Then
        '        Me.txtPQty.Text = 1
        '        Me.txtPQty.ReadOnly = True
        '    Else
        '        If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
        '            Me.txtPQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString) 'GetPackQty(Me.cmbItem.Value)
        '        End If
        '        Me.txtPQty.ReadOnly = False
        '        End If

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Public Function GetPackQty(ByVal ArticleId As Integer) As Double
        Try
            Dim str As String = "Select isnull(PackQty,1) From ArticledefTable WHERE ArticleId=" & ArticleId
            Dim dt As New DataTable
            dt = GetDataTable(str)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAdjustmentType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjustmentType.Click
        Try
            Dim id As Integer = 0I
            ApplyStyleSheet(frmAdjustmentType)
            frmAdjustmentType.ShowDialog()
            AdjTypeList = StockAdjustmentDAL.GetAllTypeList
            id = Me.cmbType.SelectedIndex
            FillCombos("type")
            Me.cmbType.SelectedIndex = id

            Dim dtLocation As New DataTable
            dtLocation = GetDataTable("Select Location_Id, Location_Name From tblDefLocation")
            Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")

            Dim dtType As New DataTable
            dtType = GetDataTable("Select AdjType_id, AdjType From tblAdjustmentType")
            Me.grd.RootTable.Columns("AdjustmentTypeId").ValueList.PopulateValueList(dtType.DefaultView, "AdjType_id", "AdjType")

            Dim dtOrigin As New DataTable
            dtOrigin = GetDataTable("select CountryName, CountryName From tblListCountry Where Active = 1")
            Me.grd.RootTable.Columns("Origin").ValueList.PopulateValueList(dtOrigin.DefaultView, "CountryName", "CountryName")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.rdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function flgAdj(ByVal Type As AdjustmentTypeBE) As Boolean
        Try
            If Me.cmbType.SelectedIndex = -1 Then Exit Function
            If Type.AdjTypeId = Me.cmbType.SelectedValue Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub grd_Click(sender As Object, e As EventArgs) Handles grd.Click
        Try
            If Me.grd.RowCount > 0 AndAlso Me.grd.GetRow.Cells(enmGrd.ArticleId).Value IsNot Nothing AndAlso Me.grd.GetRow.Cells(enmGrd.AdjTypeId).Value = 2 Then
                Dim str As String = ""
                str = " Select  BatchNo,BatchNo,ExpiryDate,Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx')  And ArticledefId = " & Me.grd.GetRow.Cells(enmGrd.ArticleId).Value & " And LocationId = " & Val(Me.grd.GetRow.Cells(enmGrd.LocationId).Value.ToString) & " Group by BatchNo,ExpiryDate,Origin Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0  ORDER BY ExpiryDate Asc"
                Dim dt As DataTable = GetDataTable(str)
                ''Me.grd.RootTable.Columns(grdEnm.BatchNo).ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
                If Not dt.Rows.Count > 0 Then
                    grd.GetRow.Cells(enmGrd.BatchNo).Value = "xxxx"
                Else
                    If IsDBNull(grd.GetRow.Cells(enmGrd.BatchNo).Value) Or grd.GetRow.Cells(enmGrd.BatchNo).Value.ToString = "" Then
                        grd.GetRow.Cells(enmGrd.BatchNo).Value = dt.Rows(0).Item("BatchNo").ToString
                    End If
                End If
                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0).Item("BatchNo").ToString) Then
                        str = " Select   ExpiryDate, Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx') And BatchNo ='" & Me.grd.GetRow.Cells(enmGrd.BatchNo).Value.ToString & "'" _
                             & " And ArticledefId = " & Me.grd.GetRow.Cells(enmGrd.ArticleId).Value & "  And LocationId = " & Val(Me.grd.GetRow.Cells(enmGrd.LocationId).Value.ToString) & "  Group by BatchNo,ExpiryDate,Origin  Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0 ORDER BY ExpiryDate  Asc "
                        Dim dtExpiry As DataTable = GetDataTable(str)
                        If dtExpiry.Rows.Count > 0 Then
                            If IsDBNull(dtExpiry.Rows(0).Item("ExpiryDate")) = False Then
                                grd.GetRow.Cells("ExpiryDate").Value = CType(dtExpiry.Rows(0).Item("ExpiryDate").ToString, Date)
                                grd.GetRow.Cells("Origin").Value = dtExpiry.Rows(0).Item("Origin").ToString
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            ''Start TFS3786 :  Ayesha Rehman  : 03-07-2018
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbLocation.SelectedValue))
            End If
            ''End TFS3786
            Me.txtPrice.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            'Me.txtPQty.Text = Val(Me.cmbItem.ActiveRow.Cells("PackQty").Value.ToString)
            FillCombos("ArticlePack")
            FillCombos("BatchNo")
            Me.txtQty.Focus()
            If DateWiseAverageRate = True Then
                Me.txtPrice.Text = GetAverageRate(Me.cmbItem.Value, Me.dtpDate.Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            'If Me.cmbItem.Value Is Nothing Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            ''Start TFS3786 :  Ayesha Rehman  : 03-07-2018
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                If Me.cmbItem.Value Is Nothing Then Exit Sub
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbLocation.SelectedValue))
            End If
            ''End TFS3786
            FillCombos("ArticlePack")
            FillCombos("BatchNo")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 0 Then
                Me.btnDelete.Visible = False
                Me.btnEdit.Visible = False
                Me.btnPrint.Visible = False
                Me.btnUpdateAll.Visible = False
                ''TFS1823
                Me.CtrlGrdBar2.Visible = False
                Me.CtrlGrdBar1.Visible = True
            Else
                Me.btnPrint.Visible = True
                Me.btnDelete.Visible = True
                Me.btnEdit.Visible = True
                Me.btnUpdateAll.Visible = True
                ''TFS1823
                Me.CtrlGrdBar2.Visible = True
                Me.CtrlGrdBar1.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    btnDelete_Click(Nothing, Nothing)
        '    Exit Sub
        'End If

    End Sub

    Private Sub grdDataHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDataHistory.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 27-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        'If e.KeyCode = Keys.Delete Then
        '    If Me.grdDataHistory.RowCount <= 0 Then Exit Sub
        '    btnDelete_Click(Nothing, Nothing)
        '    Exit Sub
        'End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnUpdateAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateAll.Click
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            blnUpdateAll = True
            Me.btnUpdateAll.Enabled = False
            Dim blnStatus As Boolean = False

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDataHistory.GetCheckedRows
                Me.grdDataHistory.Row = r.Position
                grdDataHistory_DoubleClick(Nothing, Nothing)
                FillModel()
                If Update1() = True Then
                    blnStatus = True
                Else
                    blnStatus = False
                End If
            Next
            If blnStatus = True Then msg_Information(str_informUpdate)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#125062015 Add Toolstrip Print button event for printing
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@SA_id", Me.grdDataHistory.CurrentRow.Cells("SA_ID").Value)
            ShowReport("rptStockAdjustmentDocument")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#125062015

    'Task#127062015 Number validation on keypress
    Private Sub frmStockAdjustment_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPQty.KeyPress, txtQty.KeyPress, txtPrice.KeyPress, txtPrice.KeyPress, txtStock.KeyPress ''TFS3786
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    'End Task#127062015

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdDataHistory.GetRow Is Nothing AndAlso grdDataHistory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmStockAdjustment"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdDataHistory.CurrentRow.Cells(2).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Stock Adjustment (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Inventory
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Adjustment"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDataHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDataHistory.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdDataHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Adjustment"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Start TFS3786 :  Ayeshe Rehman : 03-07-2018
    Private Sub txtStock_Click(sender As Object, e As EventArgs) Handles txtStock.Click
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub

            ApplyStyleSheet(frmCurrentStock)
            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If

            Call New frmCurrentStock(cmbItem.Value, IIf(flgLocationWiseItems = True, Me.cmbLocation.SelectedValue, 0), Me.cmbItem.ActiveRow.Cells("Item").Value.ToString()).ShowDialog()
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End TFS3786

    Private Sub cmbLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLocation.SelectedIndexChanged
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbLocation.SelectedValue))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQty_TextChanged(sender As Object, e As EventArgs)
        If Val(Me.txtTotalQty.Text) > 0 Then
            txtTotal.Text = Val(txtTotalQty.Text) * Val(txtPrice.Text)
        End If
    End Sub
    'Public Function GetAverageRate(ByVal ItemId As Integer, ByVal _Date As DateTime) As Double
    '    Try
    'Dim objDt As New DataTable
    '        objDt = UtilityDAL.GetDataTable("Select ArticleDefId, SUM(Isnull(InQty,0)-IsNull(OutQty,0)) as Current_Stock, SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) as CurrentAvgRate, Case WHEN SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) < 0 THEN -SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) WHEN SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) = 0  THEN 1 ELSE SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) END / CASE WHEN SUM(Isnull(InQty,0)-IsNull(OutQty,0)) < 0 THEN -SUM(Isnull(InQty,0)-IsNull(OutQty,0)) WHEN SUM(Isnull(InQty,0)-IsNull(OutQty,0)) = 0  THEN 1 ELSE SUM(Isnull(InQty,0)-IsNull(OutQty,0)) END AS Rate From StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId WHERE ArticleDefId=" & ItemId & " And StockMasterTable.DocDate <= '" & _Date.ToString("yyyy-MM-dd ") & " 23:59:59 " & "' Group By ArticleDefId ")
    '        If objDt IsNot Nothing Then
    '            If objDt.Rows.Count > 0 Then
    '                If objDt.Rows(0).Item("Current_Stock") > 0 Then
    '                    Return objDt.Rows(0).Item("Rate")
    '                Else
    '                    Return 0
    '                End If
    '            Else
    '                Return 0
    '            End If
    '        Else
    '            Return 0
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
End Class