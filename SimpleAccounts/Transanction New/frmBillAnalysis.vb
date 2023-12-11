Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.IO
'Task Against Request No 790 
'Coding By Imran Ali
'19-08-2013
Public Class frmBillAnalysis
    Implements IGeneral
    Dim DocId As Integer = 0I
    Dim BillAnalysis As BillAnalysisMasterBE
    Dim IsOpenForm As Boolean = False
    Dim RefDocNo As String = String.Empty
    Enum Customer
        Id
        Name
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
    End Enum

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            'Grid Setting of Column Hide Column With And Alignment
            If Condition = "Master" Then
                Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
                Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdSaved.RootTable.Columns("DocId").Visible = False
                Me.grdSaved.RootTable.Columns("CompanyId").Visible = False
                Me.grdSaved.RootTable.Columns("CustomerId").Visible = False
                Me.grdSaved.RootTable.Columns("Detail_Title").Caption = "Customer"
                Me.grdSaved.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("Total_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("Total_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("Total_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Total_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Total_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Total_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                Me.grdDetail.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grdDetail.RootTable.Columns("LocationId").HasValueList = True
                ''Me.grdDetail.AutoSizeColumns()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            'Call Function From Data Access Layer 
            If New BillAnalysisDAL().Delete(BillAnalysis) = True Then
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
            If Condition = "Item" Then
                Dim Str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice FROM ArticleDefView where Active=1 AND (SalesItem=1 Or ArticleGroupId in (Select ArticleGroupId From ArticleGroupDefTable WHERE ServiceItem=1))"
                'If flgCompanyRights = True Then
                '    Str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                'End If
                'If GetConfigValue("ArticleFilterByLocation") = "True" Then
                '    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                '        Str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                '    End If
                'End If
                'If Me.rbtCustomer.Checked = True Then
                '    If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
                '    Str += " AND MasterId in(Select ArticleDefId From ArticleDefCustomers WHERE CustomerId='" & Me.cmbVendor.Value & "')"
                'End If
                Str += " ORDER By ArticleDefView.SortOrder Asc "
                FillUltraDropDown(Me.cmbItem, Str)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                If rdoName.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
            ElseIf Condition = "Customer" Then
                Dim Str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                                  "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null AND vwCOADetail.Active=1"
                'If flgCompanyRights = True Then
                '    Str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                'End If
                'End If
                'If IsEditMode = False Then
                '    Str += " AND vwCOADetail.Active=1"
                'Else
                '    Str += " AND vwCOADetail.Active in(0,1,NULL)"
                'End If
                Str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
                FillUltraDropDown(cmbVendor, Str)
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = False
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Type).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                End If
            ElseIf Condition = "Company" Then
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable", False)
            ElseIf Condition = "Location" Then
                FillDropDown(Me.cmbLocation1, "Select Location_Id, Location_Code From tblDefLocation", False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            'Fill Model for Master Record
            Me.grdDetail.UpdateData()
            BillAnalysis = New BillAnalysisMasterBE
            BillAnalysis.DocId = DocId
            BillAnalysis.DocNo = Me.txtDcNo.Text
            BillAnalysis.DocDate = Me.dtpDcDate.Value
            BillAnalysis.OGPNo = Me.txtOGPNo.Text
            BillAnalysis.OGPDate = Me.dtpOGPDate.Value
            BillAnalysis.LotNo = Me.txtLotNo.Text
            BillAnalysis.CustomerId = Me.cmbVendor.Value
            BillAnalysis.Note = Me.txtNote.Text
            BillAnalysis.Rate_1 = Val(Me.txtRate1.Text)
            BillAnalysis.Rate_2 = Val(Me.txtRate2.Text)
            BillAnalysis.Rate_3 = Val(Me.txtRate3.Text)
            BillAnalysis.Unit = Me.cmbUnit.Text
            BillAnalysis.Total_Qty = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)
            BillAnalysis.Total_Amount = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            BillAnalysis.CompanyId = Me.cmbCompany.SelectedValue
            BillAnalysis.UserName = LoginUserName
            'Fill Model for Detail Record 
            BillAnalysis.BillAnalysisDetail = New List(Of BillAnalaysisDetailBE)
            Dim BillAnalysisDt As BillAnalaysisDetailBE
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows 'Set Loop 
                BillAnalysisDt = New BillAnalaysisDetailBE 'Create Object And Fill Model
                BillAnalysisDt.LocationId = r.Cells("LocationId").Value
                BillAnalysisDt.ArticleDefId = r.Cells("ArticleId").Value
                BillAnalysisDt.Fabric = r.Cells("Fabric_Detail").Value.ToString
                BillAnalysisDt.DesignNo = r.Cells("DesignNo").Value.ToString
                BillAnalysisDt.ArticleSize = "Loose"
                BillAnalysisDt.Stitches = r.Cells("Stitches").Value.ToString
                BillAnalysisDt.Sequins = r.Cells("Sequins").Value.ToString
                BillAnalysisDt.Tilla = r.Cells("Tilla").Value.ToString
                BillAnalysisDt.PackQty = Val(r.Cells("PackQty").Value.ToString)
                BillAnalysisDt.Qty = Val(r.Cells("Qty").Value.ToString)
                BillAnalysisDt.Rate = Val(r.Cells("Rate").Value.ToString)
                BillAnalysis.BillAnalysisDetail.Add(BillAnalysisDt) 'Add Class With Assign Values
            Next 'Repeat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then 'Get Master Data 
                Dim dt As New DataTable
                dt = New BillAnalysisDAL().DetailRecords
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("Master")
            ElseIf Condition = "Detail" Then 'Get Detail Data
                Dim dtDetail As New DataTable
                dtDetail = New BillAnalysisDAL().DisplayDetail(DocId)
                dtDetail.Columns("Total").Expression = "Qty*Rate"
                Me.grdDetail.DataSource = dtDetail
                Dim dt As New DataTable
                dt = GetDataTable("Select Location_Id, Location_Code From tblDefLocation")
                Me.grdDetail.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
                ApplyGridSettings("Detail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Please Define Customer.")
                Me.cmbVendor.Focus()
                Return False
            End If
            If Me.cmbVendor.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please select customer.")
                Me.cmbVendor.Focus()
                Return False
            End If
            If Me.grdDetail.RowCount = 0 Then
                ShowErrorMessage("Record not in grid")
                Me.grdDetail.Focus()
                Return False
            End If

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            'Reset Controls
            If Condition = String.Empty Then
                DocId = 0
                Me.btnSave.Text = "&Save"
                Me.dtpDcDate.Value = Now
                Me.txtDcNo.Text = BillAnalysisDAL.GetNextDocNo("SI-" & Now.ToString("yy") & "-")
                Me.dtpDcDate.Value = Now
                Me.txtOGPNo.Text = String.Empty
                Me.txtLotNo.Text = String.Empty
                Me.cmbVendor.Rows(0).Activate()
                Me.txtNote.Text = String.Empty
                Me.txtFabricDetail.Text = String.Empty
                Me.txtDesignNo.Text = String.Empty
                Me.cmbItem.Rows(0).Activate()
                Me.txtStitches.Text = String.Empty
                Me.txtSequins.Text = String.Empty
                Me.txtTilla.Text = String.Empty
                Me.txtThan.Text = String.Empty
                Me.txtQty.Text = String.Empty
                Me.txtRate.Text = String.Empty
                Me.txtTotal.Text = String.Empty
                Me.cmbUnit.SelectedIndex = 0
                Me.dtpDcDate.Focus()
                GetAllRecords("Master")
                GetAllRecords("Detail")
            ElseIf Condition = "ClearDetail" Then
                Me.cmbItem.Rows(0).Activate()
                Me.txtFabricDetail.Text = String.Empty
                Me.txtDesignNo.Text = String.Empty
                Me.txtStitches.Text = String.Empty
                Me.txtSequins.Text = String.Empty
                Me.txtTilla.Text = String.Empty
                Me.txtThan.Text = String.Empty
                Me.txtQty.Text = String.Empty
                Me.txtRate.Text = String.Empty
                Me.txtTotal.Text = String.Empty
                Me.cmbItem.Focus()
                Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            'Call Function From Data Access Layer
            'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If New BillAnalysisDAL().Save(BillAnalysis) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
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
            'Call Function From Data Access Layer
            If New BillAnalysisDAL().Update(BillAnalysis) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
    Private Sub frmBillAnalysis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Escape Then
            btnNew_Click(Nothing, Nothing)
        End If
        'If e.KeyCode = Keys.P And Keys.Control Then
        '    btnPrint_Click(Nothing, Nothing)
        'End If

        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Insert Then
            btnAdd_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmBillAnalysis_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            'Call Method of FillCombobox
            FillCombos("Company")
            FillCombos("Customer")
            FillCombos("Location")
            FillCombos("Item")
            IsOpenForm = True
            ReSetControls() ' Call Resetcontrol Method
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmBillAnalysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Me.txtRate1.Text = String.Empty
            Me.txtRate2.Text = String.Empty
            Me.txtRate3.Text = String.Empty
            ReSetControls(String.Empty)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try


            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If IsValidateDetailRecord() = False Then
                Exit Sub
            End If
            Dim dt As DataTable = CType(Me.grdDetail.DataSource, DataTable) 'Set Casting Grid Data in DataTable
            Dim dr As DataRow 'Declare Variable of DataRow
            dr = dt.NewRow 'Set New Row 
            dr(0) = Me.cmbLocation1.SelectedValue
            dr(1) = Me.cmbItem.Value
            dr(2) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            dr(3) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            dr(4) = Me.txtFabricDetail.Text
            dr(5) = Me.txtDesignNo.Text
            dr(6) = Val(Me.txtStitches.Text)
            dr(7) = Val(Me.txtSequins.Text)
            dr(8) = Val(Me.txtTilla.Text)
            dr(9) = Val(Me.txtThan.Text)
            dr(10) = Val(txtQty.Text)
            dr(11) = Math.Round(Val(txtRate.Text), 2)
            dr(12) = 0
            dt.Rows.InsertAt(dr, 0) 'Add Row With Assign Values
            dt.AcceptChanges() 'After Change
            ReSetControls("ClearDetail") 'Call Method of Clead Data 
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function IsValidateDetailRecord() As Boolean
        Try
            If Me.cmbItem.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please select any item")
                Me.cmbItem.Focus()
                Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
                Return False
            End If
            If Me.txtFabricDetail.Text = String.Empty Then
                ShowErrorMessage("Enter Fabric Detail")
                Me.txtFabricDetail.Focus()
                Return False
            End If
            If Me.txtDesignNo.Text = String.Empty Then
                ShowErrorMessage("Enter Design No")
                Me.txtDesignNo.Focus()
                Return False
            End If
            If Me.txtQty.Text = String.Empty Then
                ShowErrorMessage("Enter Qty")
                Me.txtQty.Focus()
                Return False
            End If
            If Me.txtRate.Text = String.Empty Then
                ShowErrorMessage("Enter Rate")
                Me.txtRate.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Changing Against Request No 836 By Imran Ali 
        '25-9-2013
        'Validation at update record
        Try
            If IsDateLock(Me.dtpDocumentDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    ReSetControls(String.Empty)
                Else
                    If CheckExistingRecord(DocId) = True Then 'Call Function And Compare with sales no
                        msg_Error(str_ErrorDependentUpdateRecordFound & vbCrLf & "Document No [" & RefDocNo & "]") 'Show Error Message of Dependent Record
                        Me.dtpDcDate.Focus() 'set Focus in Document date
                        Exit Sub
                    End If
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informUpdate)
                    ReSetControls(String.Empty)
                    RefDocNo = String.Empty
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            'Changing Against Request No 836 By Imran Ali 
            '25-9-2013
            'Validation at update record
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If IsDateLock(Me.dtpDocumentDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If
            If CheckExistingRecord(DocId) = True Then 'Call Function And Compare with sales no
                msg_Error(str_ErrorDependentRecordFound & vbCrLf & "Document No [" & RefDocNo & "]") 'Show Error Message of Dependent Record
                Me.dtpDcDate.Focus() 'set focus in document date
                Exit Sub
            End If
            BillAnalysis = New BillAnalysisMasterBE
            BillAnalysis.DocId = Me.grdSaved.GetRow.Cells("DocId").Value
            If Not msg_Confirm(str_informDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.grdSaved.CurrentRow.Delete()

            'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
            'msg_Information(str_informDelete)
            ReSetControls(String.Empty)
            RefDocNo = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            DocId = Me.grdSaved.GetRow.Cells("DocId").Value
            Me.txtDcNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.dtpDcDate.Value = Me.grdSaved.GetRow.Cells("DocDate").Value
            Me.txtOGPNo.Text = Me.grdSaved.GetRow.Cells("OGPNo").Value.ToString
            Me.dtpOGPDate.Value = Me.grdSaved.GetRow.Cells("OGPDate").Value
            Me.cmbCompany.SelectedValue = Me.grdSaved.GetRow.Cells("CompanyId").Value
            Me.txtLotNo.Text = Me.grdSaved.GetRow.Cells("LotNo").Value.ToString
            Me.cmbVendor.Value = Me.grdSaved.GetRow.Cells("CustomerId").Value
            Me.txtNote.Text = Me.grdSaved.GetRow.Cells("Note").Value.ToString
            Me.txtRate1.Text = Me.grdSaved.GetRow.Cells("Rate_1").Value
            Me.txtRate2.Text = Me.grdSaved.GetRow.Cells("Rate_2").Value
            Me.txtRate3.Text = Me.grdSaved.GetRow.Cells("Rate_3").Value
            Me.cmbUnit.Text = Me.grdSaved.GetRow.Cells("Unit").Value
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            Me.btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos("Customer")
            Me.cmbVendor.Value = id

            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id

            id = Me.cmbCompany.SelectedIndex
            FillCombos("Company")
            Me.cmbCompany.SelectedIndex = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try

            Dim dt As New DataTable
            dt = New BillAnalysisDAL().DetailRecords("All")
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtStitches_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStitches.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSequins_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSequins.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTilla_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTilla.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate1.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub GetTotal()
        Try
            Me.txtRate.Text = Math.Round((((Val(Me.txtStitches.Text) * 2.77) * (Val(Me.txtRate1.Text) / 1000)) * IIf(Me.cmbUnit.SelectedIndex = 0, 1, 1.0936)) + (((Val(Me.txtSequins.Text) * 2.77) * Val(Me.txtRate2.Text)) * IIf(Me.cmbUnit.SelectedIndex = 0, 1, 1.0936)) + (((Val(Me.txtTilla.Text) * 2.77) * (Val(Me.txtRate3.Text) / 1000)) * IIf(Me.cmbUnit.SelectedIndex = 0, 1, 1.0936)), 2)
            Me.txtTotal.Text = Math.Round(Val(Me.txtQty.Text) * Val(Me.txtRate.Text), 2)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtServiceRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate2.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate3.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub GetGridTotal()
        Try
            If Me.grdDetail.RowCount = 0 Then Exit Sub
            Me.grdDetail.UpdateData()
            Dim dblRate As Double = 0D
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                Double.TryParse((((Val(r.Cells("Stitches").Value.ToString) * 2.77) * (Val(Me.txtRate1.Text) / 1000)) * IIf(Me.cmbUnit.SelectedIndex = 0, 1, 1.0936)) + (((Val(r.Cells("Sequins").Value.ToString) * 2.77) * Val(Me.txtRate2.Text)) * IIf(Me.cmbUnit.SelectedIndex = 0, 1, 1.0936)) + (((Val(r.Cells("Tilla").Value.ToString) * 2.77) * (Val(Me.txtRate3.Text) / 1000)) * IIf(Me.cmbUnit.SelectedIndex = 0, 1, 1.0936)), dblRate)
                r.BeginEdit()
                r.Cells("Rate").Value = dblRate
                r.EndEdit()
                dblRate = 0D
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdDetail_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                Me.grdDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellUpdated
        Try
            GetGridTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdDetail_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetail.RecordsDeleted
        Try
            GetGridTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnLoadAll.Visible = False
            Else
                Me.btnLoadAll.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DocId", Me.grdSaved.GetRow.Cells("DocId").Value)
            ShowReport("rptBillEmbroidery")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If rdoName.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If rdoName.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabPageControl1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            GetTotal()
            GetGridTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function CheckExistingRecord(ByVal InvNo As Integer) As Boolean
        Try

            Dim strSQL As String = "Select Count(*) as Cont, SalesNo From SalesMasterTable WHERE RefDocId=" & InvNo & " Group By SalesNo"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt.Rows.Count > 0 Then
                If dt IsNot Nothing Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        RefDocNo = dt.Rows(0).Item(1).ToString
                        Return True
                    Else
                        Return False
                    End If
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

   
    'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
    Private Sub grdDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDetail.KeyDown
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub txtLotNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLotNo.TextChanged

    End Sub

    Private Sub dtpOGPDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpOGPDate.ValueChanged

    End Sub

    Private Sub txtOGPNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOGPNo.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub txtFabricDetail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFabricDetail.TextChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub
End Class