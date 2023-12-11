'21-Nov-2017 TFS1828 : Ali Faisal : Add new form to save update and delete records through this form.
'07-Aug-2018 TFS4144 : Ayesha Rehman : Added Smart Search on Item,Item Search By code,Name,Apply configuration Show Vendor on Sales. 
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmSalesAdjustmentVoucher
    Implements IGeneral
    Dim objDAL As SalesAdjustmentDAL
    Dim objModel As SalesAdjustmentBE
    Dim IsFormOpend As Boolean = False ''TFS4144
    ''' <summary>
    ''' Ali Faisal : set indexes of detail grid to use name of columns from enum instead of from query.
    ''' </summary>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Enum grdDetail
        AdjustmentDetailId
        AdjustmentId
        InvoiceId
        InvoiceNo
        ItemId
        Item
        ItemAccountId
        Amount
        Reason
    End Enum
    Enum grdHistory
        AdjustmentId
        DocNo
        DocDate
        CompanyId
        Company
        CostCenterId
        CostCenter
        CustomerId
        Customer
        Remarks
    End Enum
    ''' <summary>
    ''' Ali Faisal : Apply grid setings to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "History" Then
                Me.grdSaved.RootTable.Columns(grdHistory.AdjustmentId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.DocDate).FormatString = "" & str_DisplayDateFormat
                Me.grdSaved.RootTable.Columns(grdHistory.CompanyId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.CostCenterId).Visible = False
                Me.grdSaved.RootTable.Columns(grdHistory.CustomerId).Visible = False
            Else
                If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                    Me.grd.RootTable.Columns.Add("Delete")
                    Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grd.RootTable.Columns("Delete").Key = "Delete"
                    Me.grd.RootTable.Columns("Delete").Caption = "Action"
                End If
                Me.grd.RootTable.Columns(grdDetail.AdjustmentDetailId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.AdjustmentId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.InvoiceId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.ItemId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.ItemAccountId).Visible = False
                Me.grd.RootTable.Columns(grdDetail.Amount).FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns(grdDetail.Amount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(grdDetail.Amount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(grdDetail.Amount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(grdDetail.Amount).TotalFormatString = "N" & DecimalPointInValue


                grd.RootTable.Columns(grdDetail.AdjustmentDetailId).EditType = Janus.Windows.GridEX.EditType.NoEdit
                grd.RootTable.Columns(grdDetail.AdjustmentId).EditType = Janus.Windows.GridEX.EditType.NoEdit
                grd.RootTable.Columns(grdDetail.InvoiceId).EditType = Janus.Windows.GridEX.EditType.NoEdit
                grd.RootTable.Columns(grdDetail.InvoiceNo).EditType = Janus.Windows.GridEX.EditType.NoEdit
                grd.RootTable.Columns(grdDetail.Item).EditType = Janus.Windows.GridEX.EditType.NoEdit
                grd.RootTable.Columns(grdDetail.ItemAccountId).EditType = Janus.Windows.GridEX.EditType.NoEdit
                grd.RootTable.Columns(grdDetail.ItemId).EditType = Janus.Windows.GridEX.EditType.NoEdit

                grd.RootTable.Columns(grdDetail.Amount).EditType = Janus.Windows.GridEX.EditType.TextBox
                grd.RootTable.Columns(grdDetail.Reason).EditType = Janus.Windows.GridEX.EditType.TextBox

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar2.mGridPrint.Enabled = True
                Me.CtrlGrdBar2.mGridExport.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.btnDelete.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    Me.CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Calls the Delete function from DAL to remove the data of selected row.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New SalesAdjustmentDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustmentId).Value.ToString), objModel.VoucherId) = True Then
                'Insert Activity Log by Ali Faisal on 21-Nov-2017
                SaveActivityLog("Sales", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Sales, Me.txtDocNo.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : FillCombos of items, customer list and invoices.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Item" Then
                If Me.cmbSalesInvioces.Value = 0 Then
                    str = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination,ArticleDefView.PackQty, ISNULL(ArticleDefView.SalePrice, 0) AS Price, ISNULL(ArticleDefView.PurchasePrice, 0) AS PurchasePrice, ArticleDefView.SortOrder, ArticleDefView.ArticleGroupName AS Dept, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleLpoName AS Brand, ISNULL(ArticleDefView.Cost_Price, 0) AS [Cost Price], ISNULL(SalesAccountId, 0) AS SalesAccountId FROM ArticleDefView "
                    str += "WHERE Active=1"
                    FillUltraDropDown(Me.cmbItem, str)
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
                    Me.cmbItem.Rows(0).Activate()
                Else
                    str = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.PackQty, ISNULL(ArticleDefView.SalePrice, 0) AS Price, ISNULL(ArticleDefView.PurchasePrice, 0) AS PurchasePrice, ArticleDefView.SortOrder, ArticleDefView.ArticleGroupName AS Dept, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleLpoName AS Brand, ISNULL(ArticleDefView.Cost_Price, 0) AS [Cost Price], ISNULL(SalesAccountId, 0) AS SalesAccountId FROM ArticleDefView INNER JOIN SalesDetailTable ON ArticleDefView.ArticleId = SalesDetailTable.ArticleDefId"
                    str += " WHERE SalesDetailTable.SalesId = " & Me.cmbSalesInvioces.Value & ""
                    FillUltraDropDown(Me.cmbItem, str)
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
                    Me.cmbItem.Rows(0).Activate()
                End If
            ElseIf Condition = "Customer" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as [Type Id], dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                ''Start TFS4144
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str += " AND  (vwCOADetail.account_type in( 'Customer','Vendor' ))  "
                Else
                    str += " AND   (dbo.vwCOADetail.account_type = 'Customer') "
                End If
                ''End TFs4144
                str += " AND vwCOADetail.Active=1"
                FillUltraDropDown(cmbCustomer, str)
                Me.cmbCustomer.Rows(0).Activate()
                If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Type Id").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                End If
            ElseIf Condition = "SalesInvioce" Then
                If Me.cmbCustomer.Value > 0 Then
                    str = "SELECT SalesId, SalesNo, SalesDate, SalesQty, SalesAmount FROM SalesMasterTable WHERE CustomerCode = " & Me.cmbCustomer.Value & ""
                Else
                    str = "SELECT SalesId, SalesNo, SalesDate, SalesQty, SalesAmount FROM SalesMasterTable"
                End If
                FillUltraDropDown(Me.cmbSalesInvioces, str)
                Me.cmbSalesInvioces.Rows(0).Activate()
                Me.cmbSalesInvioces.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "CostCenter" Then
                str = "SELECT CostCenterID, Name, Code FROM tblDefCostCenter WHERE Active = 1"
                FillUltraDropDown(Me.cmbCostCenter, str)
                Me.cmbCostCenter.Rows(0).Activate()
                Me.cmbCostCenter.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Company" Then
                str = "SELECT CompanyId, CompanyName FROM CompanyDefTable ORDER BY CompanyId"
                FillUltraDropDown(Me.cmbCompany, str, False)
                Me.cmbCompany.Rows(0).Activate()
                Me.cmbCompany.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fill valus in controls in edit mode from history grid.
    ''' </summary>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub EditRecords()
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.txtDocNo.Text = Me.grdSaved.CurrentRow.Cells(grdHistory.DocNo).Value.ToString
            Me.dtpDocDate.Value = CType(Me.grdSaved.CurrentRow.Cells(grdHistory.DocDate).Value, Date)
            Me.cmbCompany.Value = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.CompanyId).Value.ToString)
            Me.cmbCostCenter.Value = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.CostCenterId).Value.ToString)
            Me.cmbCustomer.Value = Val(Me.grdSaved.CurrentRow.Cells(grdHistory.CustomerId).Value.ToString)
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells(grdHistory.Remarks).Value.ToString
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fillmodel to get data of Master and Detail records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New SalesAdjustmentBE
            objModel.Detail = New List(Of SalesAdjustmentDetailBE)
            If Me.btnSave.Text = "&Save" Then
                objModel.DocNo = GetDocumentNo()
            Else
                objModel.DocNo = Me.txtDocNo.Text
                objModel.AdjustmentId = Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustmentId).Value.ToString
            End If

            ''Commented Against TFS3154  Object Ref Error on save of Sale adjustment voucher

            'objModel.VoucherId = GetVoucherId("frmSalesAdjustmentVoucher", Me.grdSaved.CurrentRow.Cells(grdHistory.DocNo).Value.ToString)
            objModel.VoucherId = CInt(GetVoucherId("frmSalesAdjustmentVoucher", objModel.DocNo))
            objModel.DocDate = Me.dtpDocDate.Value
            objModel.CompanyId = Val(Me.cmbCompany.Value)
            objModel.CostCenterId = Val(Me.cmbCostCenter.Value)
            objModel.CustomerId = Val(Me.cmbCustomer.Value)
            objModel.Remarks = Me.txtRemarks.Text
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New SalesAdjustmentDetailBE
                Detail.AdjustmentDetailId = Val(Row.Cells(grdDetail.AdjustmentDetailId).Value.ToString)
                Detail.InvoiceId = Val(Row.Cells(grdDetail.InvoiceId).Value.ToString)
                Detail.ItemId = Val(Row.Cells(grdDetail.ItemId).Value.ToString)
                Detail.ItemAccountId = Val(Row.Cells(grdDetail.ItemAccountId).value)
                Detail.Amount = Val(Row.Cells(grdDetail.Amount).Value.ToString)
                Detail.Reason = Row.Cells(grdDetail.Reason).Value.ToString
                objModel.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Add data of all controls to grid
    ''' </summary>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal </remarks>
    Public Sub AddToGrid()
        Try
            Dim dt As DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr(grdDetail.AdjustmentDetailId) = 0
            dr(grdDetail.AdjustmentId) = 0
            If Me.cmbSalesInvioces.Value > 0 Then
                dr(grdDetail.InvoiceId) = Me.cmbSalesInvioces.Value
                dr(grdDetail.InvoiceNo) = Me.cmbSalesInvioces.ActiveRow().Cells(1).Value.ToString
            Else
                dr(grdDetail.InvoiceId) = 0
                dr(grdDetail.InvoiceNo) = ""
            End If
            dr(grdDetail.ItemId) = Me.cmbItem.Value
            dr(grdDetail.Item) = Me.cmbItem.ActiveRow().Cells(2).Value.ToString
            dr(grdDetail.ItemAccountId) = Val(Me.cmbItem.ActiveRow().Cells(14).Value)
            dr(grdDetail.Amount) = Me.txtAmount.Text
            dr(grdDetail.Reason) = Me.txtReason.Text
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To show all saved records in history and detail grids.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Condition = "History" Then
                str = "SELECT SalesAdjustmentMaster.AdjustmentId, SalesAdjustmentMaster.DocNo, SalesAdjustmentMaster.DocDate, SalesAdjustmentMaster.CompanyId, CompanyDefTable.CompanyName AS Company, SalesAdjustmentMaster.CostCenterId, tblDefCostCenter.Name AS CostCenter, SalesAdjustmentMaster.CustomerId, vwCOADetail.detail_title AS Customer, SalesAdjustmentMaster.Remarks FROM SalesAdjustmentMaster INNER JOIN vwCOADetail ON SalesAdjustmentMaster.CustomerId = vwCOADetail.coa_detail_id INNER JOIN CompanyDefTable ON SalesAdjustmentMaster.CompanyId = CompanyDefTable.CompanyId LEFT OUTER JOIN tblDefCostCenter ON SalesAdjustmentMaster.CostCenterId = tblDefCostCenter.CostCenterID ORDER BY SalesAdjustmentMaster.AdjustmentId DESC"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("History")
            Else
                If btnSave.Text = "&Save" Then
                    str = "SELECT AdjustmentDetailId, AdjustmentId, InvoiceId, '' AS InvoiceNo, ItemId, '' AS Item, 0 AS ItemAccountId, Amount, Reason FROM SalesAdjustmentDetail WHERE AdjustmentId = -1"
                Else
                    str = "SELECT SalesAdjustmentDetail.AdjustmentDetailId, SalesAdjustmentDetail.AdjustmentId, IsNull(SalesAdjustmentDetail.InvoiceId,0) InvoiceId, IsNull(SalesMasterTable.SalesNo,'') AS InvoiceNo, SalesAdjustmentDetail.ItemId, ArticleDefView.ArticleDescription AS Item, ArticleDefView.SalesAccountId AS ItemAccountId, SalesAdjustmentDetail.Amount, SalesAdjustmentDetail.Reason FROM SalesAdjustmentDetail INNER JOIN ArticleDefView ON SalesAdjustmentDetail.ItemId = ArticleDefView.ArticleId LEFT OUTER JOIN SalesMasterTable ON SalesAdjustmentDetail.InvoiceId = SalesMasterTable.SalesId WHERE AdjustmentId = " & Val(Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustmentId).Value) & ""
                End If
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Verify the controls are selected before save or update etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbCustomer.Value = 0 Then
                msg_Information("Please select any Customer")
                Return False
            ElseIf Me.grd.RowCount = 0 Then
                msg_Information("Detail grid is empty.")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidAddToGrid() As Boolean
        Try
            If Me.cmbItem.Value = 0 Then
                msg_Information("Please select any Item")
                Return False
            ElseIf Me.txtAmount.Text = 0 Or Me.txtAmount.Text = "" Then
                msg_Information("Please enter adjustment amount")
                Return False
            End If
            If Me.cmbItem.Value > 0 Then
                If Val(Me.cmbItem.ActiveRow().Cells(14).Value) = Val(0) Then
                    msg_Information("Sales A/C is not mapped with department of item : " & Me.cmbItem.ActiveRow().Cells(2).Value.ToString & "")
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = "History" Then
                Me.txtDocNo.Text = GetDocumentNo()
                Me.dtpDocDate.Value = Now
                Me.cmbCustomer.Value = 0
                Me.txtRemarks.Text = ""
                GetAllRecords("History")
                Me.btnSave.Text = "&Save"
                Me.btnDelete.Visible = False
                Me.btnPrint.Visible = False
                CtrlGrdBar1_Load(Nothing, Nothing)
            Else
                Me.cmbSalesInvioces.Value = 0
                Me.cmbItem.Value = 0
                Me.txtAmount.Text = 0
                Me.txtReason.Text = ""
                GetAllRecords()
            End If
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Get doc no for next document.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SJV-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "SalesAdjustmentMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SJV-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "SalesAdjustmentMaster", "DocNo")
            Else
                Return GetNextDocNo("SJV-", 6, "SalesAdjustmentMaster", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Call the save function from DAL to save the records of master and details.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New SalesAdjustmentDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    'Insert Activity Log by Ali Faisal on 21-Nov-2017
                    SaveActivityLog("Sales", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtDocNo.Text, True)
                    Return True
                Else
                    Return False
                End If
            End If
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
    ''' <summary>
    ''' Ali Faisal : Call the update function from DAL to modify the records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New SalesAdjustmentDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Ali Faisal on 21-Nov-2017
                    SaveActivityLog("Sales", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.txtDocNo.Text, True)
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Add data in Grid and After that focus on Item drop down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal </remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If IsValidAddToGrid() = True Then
                AddToGrid()
                Me.cmbItem.Value = 0
                Me.txtAmount.Text = 0
                Me.txtReason.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Set ShortKeys to perform actions on save and refresh
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                Me.btnNew_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F5 Then
                Me.btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.IsFormOpend = True ''TFS4144
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Reset all controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls("History")
            ReSetControls()
            FillCombos("Item")
            FillCombos("Customer")
            FillCombos("SalesInvioce")
            FillCombos("CostCenter")
            FillCombos("Company")
            GetAllRecords()
            GetAllRecords("History")
            Me.cmbCompany.Value = 1
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Refresh controls.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer
            id = Me.cmbCustomer.Value
            FillCombos("Customer")
            Me.cmbCustomer.Value = id

            id = Me.cmbSalesInvioces.Value
            FillCombos("SalesInvioce")
            Me.cmbSalesInvioces.Value = id

            id = Me.cmbItem.Value
            FillCombos("Item")
            Me.cmbItem.Value = id

            id = Me.cmbCompany.Value
            FillCombos("Company")
            Me.cmbCompany.Value = id

            id = Me.cmbCostCenter.Value
            FillCombos("CostCenter")
            Me.cmbCostCenter.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Save and Update the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Text = "&Save" Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    btnNew_Click(Nothing, Nothing)
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() = True Then
                    msg_Information(str_informUpdate)
                    btnNew_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                msg_Information(str_informDelete)
                btnNew_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete the record from grid and also from database.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New SalesAdjustmentDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.CurrentRow.Cells(grdDetail.AdjustmentDetailId).Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Me.btnSave.Text = "&Update"
            EditRecords()
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit the selected row from history on double click of grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnEdit.Visible = False
                Me.btnSave.Visible = True
                Me.CtrlGrdBar1.Visible = True
                Me.CtrlGrdBar2.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnEdit.Visible = True
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
                Me.btnPrint.Visible = True
                Me.CtrlGrdBar1.Visible = False
                Me.CtrlGrdBar2.Visible = True
            End If
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Adjustment Vouchers"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalesInvioces_ValueChanged(sender As Object, e As EventArgs) Handles cmbSalesInvioces.ValueChanged
        Try
            FillCombos("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            If Me.cmbCustomer.IsItemInList = False Then Exit Sub
            If Me.cmbCustomer.ActiveRow Is Nothing Then Exit Sub
            FillCombos("SalesInvioce")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AdjustmentVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentVoucherToolStripMenuItem.Click
        Try
            GetVoucherPrint(Me.grdSaved.CurrentRow.Cells(grdHistory.DocNo).Value, Me.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AdjustmentReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentReportToolStripMenuItem.Click
        Try
            AddRptParam("@AdjustmentId", Me.grdSaved.CurrentRow.Cells(grdHistory.AdjustmentId).Value.ToString)
            ShowReport("SalesAdjustmentVoucher")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Adjustment"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Start TFS4144
    Private Sub RdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles RdoCode.CheckedChanged
        Try
            If Not Me.IsFormOpend = True Then Exit Sub
            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        Try
            If Not Me.IsFormOpend = True Then Exit Sub
            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''End TSF4144


End Class