''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''21-Jan-2014   TASK:2388             Imran Ali                  Service Item Check Not Working Properly  
''31-Jan-2014     TASK:2401            Imran ali                    Store Issuence Problem 
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''03-Feb-2014         Task:2412     Imran Ali        Store Issuance against Plan No  
''07-Feb-2014             TASK:2416     Imran Ali       Minus Stock Allowed Work Not Properly
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''22-Feb-2014 TASK:M21   Imran Ali Issued Based Stock Update In Store Issuance.
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''10-Mar-2014 Task:2478   Imran Ali    Bug fix
''18-Mar-2014 TASK:2506 Imran ali      Add batch quantity and finish goods name in store issue detail report
''8-May-2014 TASK:M36 Imran Cost Of Goods Voucher Problem.
''16-June-2014 TASK:2689 Imran Ali Add Department Field On Store Issuance
'' 02-Jul-2014 TASK:2712 Imran Ali Purchase Price Update through Adjustment Average Rate
'' 04-Jul-2014 TASK:2716 Imran Ali Selected Store Issuance Update 
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''11-Sep-2014 Task:M101 Imran Ali Add new field remarks 
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''16-Nov-2015 TASK-TFS-46 Cost Price For Production And Store Issuence.
'' TASK-470 Muhammad Ameen on 01-07-2016: Stock Statement Report By Pack
''TASK TFS2417 UserName should be saved into voucher. Ameen done on 13-09-2017
''TASK TFS1773 Muhammad Ameen on 16-11-2017. Issuance was not loading. Fixed it 
'' TASK TFS1592 Ayesha Rehman on 19-10-2017 Future date entry should be rights based
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
Imports System.Data.OleDb
Imports SBDal
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports SBUtility.Utility
Imports SBModel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms

Public Class frmReturnStoreIssuence
    Implements IGeneral
    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsEditMode As Boolean = False
    Dim IsFormOpen As Boolean = False
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim Email As Email
    Dim crpt As New ReportDocument
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim getVoucher_Id As Integer = 0
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim Previouse_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim _strImagePath As String = String.Empty
    Dim flgCompanyRights As Boolean = False
    Dim flgStoreIssuenceVoucher As Boolean = False
    Dim StoreIssuanceDependonProductionPlan As Boolean = False
    Dim grpArticleDescriptionMaster As Janus.Windows.GridEX.GridEXGroup
    Dim dtDataEmail As New DataTable
    Dim setVoucherdate As DateTime
    Dim StockList As List(Of StockDetail)
    Dim flgLocationWiseItem As Boolean = False
    Dim blnCGAccountOnStoreIssuance As Boolean = False
    Private blnDisplayAll As Boolean = False
    Private IsStoreIssuance As Boolean = False
    Public CheckQty As Double = 0
    Dim CostSheetType As String = ""
    'Code Edit for task 1592 future date rights
    Dim IsDateChangeAllowed As Boolean = False
    Dim IsWIPAccount As Boolean = False
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False
    Enum grdEnm
        LocationId
        ArticleCode
        Item
        Color
        BatchNo
        Unit
        Qty
        Rate
        Total
        CategoryId
        ItemId
        PackQty
        CurrentPrice
        BatchId
        ArticleDefMasterId
        ArticleDescriptionMaster
        Pack_Desc
        AccountId
        CGSAccountId
        CostPrice
        PlanUnit
        PlanNo
        PlanQty
        TicketNo
        TicketQty
        LotNo
        Rack_No
        Comments
        TotalQty '' TASK-408
        DispatchDetailId
        CheckQty
        EstimationId
        TicketId
        DepartmentId
        Department
        WIPAccountId
    End Enum
    Private Sub frmStoreIssuence_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            If Not getConfigValueByType("StoreIssuenceVoucher").ToString = "Error" Then
                flgStoreIssuenceVoucher = getConfigValueByType("StoreIssuenceVoucher")
            End If

            If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
                StoreIssuanceDependonProductionPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
            End If


            ''Task:2366 Added Location Wise Filter Configuration
            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''End Task:2366

            If Not getConfigValueByType("CGAccountOnStoreIssuance").ToString = "Error" Then
                blnCGAccountOnStoreIssuance = Convert.ToBoolean(getConfigValueByType("CGAccountOnStoreIssuance").ToString)
            End If

            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            If getConfigValueByType("BarcodeEnabled") = "True" Then
                Mode = "BarcodeEnabled"
                Me.Panel1.Visible = True

                'pnlSimple.TabStop = False
                'pnlSimple.Enabled = False
                Me.txtBarcodescan.Focus()
            Else
                Panel1.Visible = False
            End If
            CostSheetType = getConfigValueByType("CostSheetType")
            'If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then

            'Else
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            FillCombo("Vendor")
            FillCombo("SM")
            FillCombo("SO")
            FillCombo("Category")
            FillCombo("Item")
            FillCombo("Plan")
            FillCombo("Tickets")
            FillCombo("CostSheetItem")
            FillCombo("Employees")
            FillCombo("ArticlePack")
            FillCombo("FixedAssetAccount")
            FillCombo("SecurityAccount")
            FillCombo("CGAccount")
            RefreshControls()
            'Me.cmbPo.Focus()
            'Me.cmbVendor.Focus()
            'Me.DisplayRecord() R933 Commented History Data
            'Me.cmbVendor.Enabled = False6
            '//This will hide Master grid
            Me.grdSaved.Visible = CType(getConfigValueByType("ShowMasterGrid"), Boolean)
            IsFormOpen = True

            'Before against Task:2412 
            'If StoreIssuanceDependonProductionPlan = False Then
            '    grpArticleDescriptionMaster = New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("ArticleDescriptionMaster"))
            '    Me.grd.RootTable.Groups.Remove(grpArticleDescriptionMaster)
            'End If
            'End Task:2412

            Get_All(frmModProperty.Tags)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty
        'str = "SELECT     Recv.ReturnDispatchNo, Recv.ReturnDispatchDate AS Date, vwCOADetail.detail_title as CustomerName, V.PurchaseOrderNo, Recv.ReturnDispatchQty, Recv.ReturnDispatchAmount, Recv.ReturnDispatchId,  " & _
        '        "Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId " & _
        '        "FROM         ReturnDispatchMasterTable Recv INNER JOIN " & _
        '        "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
        '        "EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
        '        "PurchaseOrderMasterTable V ON Recv.POId = V.PurchaseOrderId " & _
        '        "ORDER BY Recv.ReturnDispatchNo DESC"

        'FillGrid(grdSaved, str)
        'grdSaved.Columns(10).Visible = False
        ''grdSaved.Columns(4).Visible = False
        'grdSaved.Columns(6).Visible = False
        'grdSaved.Columns(7).Visible = False
        'grdSaved.Columns("EmployeeCode").Visible = False
        'grdSaved.Columns("PoId").Visible = False

        'grdSaved.Columns(0).HeaderText = "Issue No"
        'grdSaved.Columns(1).HeaderText = "Date"
        'grdSaved.Columns(2).HeaderText = "Customer"
        'grdSaved.Columns(3).HeaderText = "S-Order"
        'grdSaved.Columns(4).HeaderText = "Qty"
        'grdSaved.Columns(5).HeaderText = "Amount"
        'grdSaved.Columns(8).HeaderText = "Employee"

        'grdSaved.Columns(0).Width = 100
        'grdSaved.Columns(1).Width = 100
        'grdSaved.Columns(2).Width = 150
        'grdSaved.Columns(4).Width = 50
        'grdSaved.Columns(5).Width = 80
        'grdSaved.Columns(8).Width = 100
        'grdSaved.Columns(9).Width = 150
        'grdSaved.RowHeadersVisible = False
        'If Mode = "Normal" Then
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReturnDispatchNo, Recv.ReturnDispatchDate AS Date, dbo.tblDefCostCenter.Name as [Cost Center], V.detail_title, Recv.ReturnDispatchQty, " & _
        '        "Recv.ReturnDispatchAmount, Recv.ReturnDispatchId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
        '        "Recv.PurchaseOrderID " & _
        '        "FROM         dbo.ReturnDispatchMasterTable Recv Left Outer JOIN " & _
        '        "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
        '        "dbo.tblDefCostCenter ON Recv.PurchaseOrderID = dbo.tblDefCostCenter.CostCenterId " & _
        '        " where Recv.ReturnDispatchNo  like 'I%'   order by 1 desc"
        'Before against task:M21
        'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReturnDispatchNo, Recv.ReturnDispatchDate AS Date, dbo.tblDefCostCenter.Name as [Cost Center], V.detail_title, Recv.ReturnDispatchQty, " & _
        '      "Recv.ReturnDispatchAmount, Recv.ReturnDispatchId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
        '      "Recv.PurchaseOrderID, cd.LocationId, cd.ArticleDefId, cd.Qty, cd.CurrentPrice, IsNull(tblDefCostCenter.OutwardGatePass,0) as OutwaredGatepass, Recv.PONo as [PO No], Isnull(PlanId,0) as PlanId, Isnull(Recv.EmpId,0) as EmpId, Isnull(Recv.DeptId,0) as DeptId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Recv.FixedAssetAccountId,0) as FixedAssetAccountId, Isnull(Recv.CylinderSecurityAccountId,0) as CylinderSecurityAccountId  " & _
        '      "FROM dbo.ReturnDispatchMasterTable Recv Left Outer JOIN " & _
        '      "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
        '      "dbo.tblDefCostCenter ON Recv.PurchaseOrderID = dbo.tblDefCostCenter.CostCenterId LEFT OUTER JOIN CostingDetailTable  cd ON cd.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select DISTINCT ReturnDispatchId, LocationId From ReturnDispatchDetailTable) as Location On Location.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReturnDispatchNo " & _
        '      " where Recv.ReturnDispatchNo  like 'I%'  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReturnDispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        ''Task:M21 Added Column Issued
        'Before against task:2689
        'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReturnDispatchNo, Recv.ReturnDispatchDate AS Date, dbo.tblDefCostCenter.Name as [Cost Center], V.detail_title, Recv.ReturnDispatchQty, " & _
        '      "Recv.ReturnDispatchAmount, Recv.ReturnDispatchId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
        '      "Recv.PurchaseOrderID, cd.LocationId, cd.ArticleDefId, cd.Qty, cd.CurrentPrice, IsNull(tblDefCostCenter.OutwardGatePass,0) as OutwaredGatepass, Recv.PONo as [PO No], Isnull(PlanId,0) as PlanId, Isnull(Recv.EmpId,0) as EmpId, Isnull(Recv.DeptId,0) as DeptId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Recv.FixedAssetAccountId,0) as FixedAssetAccountId, Isnull(Recv.CylinderSecurityAccountId,0) as CylinderSecurityAccountId, Isnull(Recv.Issued,0) as Issued  " & _
        '      "FROM dbo.ReturnDispatchMasterTable Recv Left Outer JOIN " & _
        '      "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
        '      "dbo.tblDefCostCenter ON Recv.PurchaseOrderID = dbo.tblDefCostCenter.CostCenterId LEFT OUTER JOIN CostingDetailTable  cd ON cd.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select DISTINCT ReturnDispatchId, LocationId From ReturnDispatchDetailTable) as Location On Location.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReturnDispatchNo " & _
        '      " where Recv.ReturnDispatchNo  like 'I%'  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReturnDispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'Task:M21 Added Column Issued
        'Task:2689 Added Field DepartmentId
        'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReturnDispatchNo, Recv.ReturnDispatchDate AS Date, dbo.tblDefCostCenter.Name as [Cost Center], V.detail_title, Recv.ReturnDispatchQty, " & _
        '      "Recv.ReturnDispatchAmount, Recv.ReturnDispatchId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
        '      "Recv.PurchaseOrderID, cd.LocationId, cd.ArticleDefId, cd.Qty, cd.CurrentPrice, IsNull(tblDefCostCenter.OutwardGatePass,0) as OutwaredGatepass, Recv.PONo as [PO No], Isnull(PlanId,0) as PlanId, Isnull(Recv.EmpId,0) as EmpId, Isnull(Recv.DeptId,0) as DeptId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Recv.FixedAssetAccountId,0) as FixedAssetAccountId, Isnull(Recv.CylinderSecurityAccountId,0) as CylinderSecurityAccountId, Isnull(Recv.Issued,0) as Issued, IsNull(Recv.DepartmentId,0)  as DepartmentId " & _
        '      "FROM dbo.ReturnDispatchMasterTable Recv Left Outer JOIN " & _
        '      "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
        '      "dbo.tblDefCostCenter ON Recv.PurchaseOrderID = dbo.tblDefCostCenter.CostCenterId LEFT OUTER JOIN CostingDetailTable  cd ON cd.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select DISTINCT ReturnDispatchId, LocationId From ReturnDispatchDetailTable) as Location On Location.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReturnDispatchNo " & _
        '      " where Recv.ReturnDispatchNo  like 'I%'  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReturnDispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""


        'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReturnDispatchNo, Recv.ReturnDispatchDate AS Date, dbo.tblDefCostCenter.Name as [Cost Center], V.detail_title, Recv.ReturnDispatchQty, " & _
        '     "Recv.ReturnDispatchAmount, Recv.ReturnDispatchId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
        '     "Recv.PurchaseOrderID, cd.LocationId, cd.ArticleDefId, cd.Qty, cd.CurrentPrice, IsNull(tblDefCostCenter.OutwardGatePass,0) as OutwaredGatepass, Recv.PONo as [PO No], Isnull(PlanId,0) as PlanId, Isnull(Recv.EmpId,0) as EmpId, Isnull(Recv.DeptId,0) as DeptId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Recv.FixedAssetAccountId,0) as FixedAssetAccountId, Isnull(Recv.CylinderSecurityAccountId,0) as CylinderSecurityAccountId, Isnull(Recv.Issued,0) as Issued, IsNull(Recv.DepartmentId,0)  as DepartmentId, IsNull(Recv.StoreIssuanceAccountId,0) as StoreIssuanceAccountId " & _
        '     "FROM dbo.ReturnDispatchMasterTable Recv Left Outer JOIN " & _
        '     "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
        '     "dbo.tblDefCostCenter ON Recv.PurchaseOrderID = dbo.tblDefCostCenter.CostCenterId LEFT OUTER JOIN CostingDetailTable  cd ON cd.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select DISTINCT ReturnDispatchId, LocationId From ReturnDispatchDetailTable) as Location On Location.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReturnDispatchNo " & _
        '     " where Recv.ReturnDispatchNo  like 'I%'  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReturnDispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReturnDispatchNo, Recv.ReturnDispatchDate AS Date, dbo.tblDefCostCenter.Name as [Cost Center], V.detail_title, Recv.ReturnDispatchQty, " & _
                   "Recv.ReturnDispatchAmount, Recv.ReturnDispatchId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
                   " IsNull(Recv.PurchaseOrderID,0) as PurchaseOrderID, IsNull(cd.LocationId,0) as LocationId, cd.ArticleDefId, cd.Qty, cd.CurrentPrice, IsNull(tblDefCostCenter.OutwardGatePass,0) as OutwaredGatepass, Recv.PONo as [PO No],Isnull(PlanId,0) as PlanId, Isnull(Recv.EmpId,0) as EmpId, Isnull(Recv.DeptId,0) as DeptId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Isnull(Recv.FixedAssetAccountId,0) as FixedAssetAccountId, Isnull(Recv.CylinderSecurityAccountId,0) as CylinderSecurityAccountId, Isnull(Recv.Issued,0) as Issued, IsNull(Recv.DepartmentId,0)  as DepartmentId, IsNull(Recv.StoreIssuanceAccountId,0) as StoreIssuanceAccountId, IsNull(Recv.DispatchId,0) as DispatchId, Disp.DispatchNo, IsNull(Recv.TicketID, 0) As TicketID " & _
                   "FROM dbo.ReturnDispatchMasterTable Recv Left Outer JOIN " & _
                   "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN(Select DispatchId, DispatchNo From DispatchMasterTable) Disp On Disp.DispatchId = Recv.DispatchId LEFT OUTER JOIN " & _
                   "dbo.tblDefCostCenter ON Recv.PurchaseOrderID = dbo.tblDefCostCenter.CostCenterId LEFT OUTER JOIN CostingDetailTable  cd ON cd.DispatchId = Recv.DispatchId LEFT OUTER JOIN(Select DISTINCT ReturnDispatchId, LocationId From ReturnDispatchDetailTable) as Location On Location.ReturnDispatchId = Recv.ReturnDispatchId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReturnDispatchNo " & _
                   " where Recv.ReturnDispatchNo  like 'RI%'  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReturnDispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        If blnDisplayAll = False Then
            If flgCompanyRights = True Then
                str += " AND Recv.LocationId=" & MyCompanyId
            End If
        End If
        If Me.dtpFrom.Checked = True Then
            str += " AND Recv.ReturnDispatchDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
        End If
        If Me.dtpTo.Checked = True Then
            str += " AND Recv.ReturnDispatchDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
        End If
        If Me.txtSearchDocNo.Text <> String.Empty Then
            str += " AND Recv.ReturnDispatchNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
        End If
        If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
            If Me.cmbSearchLocation.SelectedIndex <> 0 Then
                str += " AND Location.LocationId=" & Me.cmbSearchLocation.SelectedValue
            End If
        End If
        If Me.txtFromAmount.Text <> String.Empty Then
            If Val(Me.txtFromAmount.Text) > 0 Then
                str += " AND Recv.ReturnDispatchAmount >= " & Val(Me.txtFromAmount.Text) & " "
            End If
        End If
        If Me.txtToAmount.Text <> String.Empty Then
            If Val(Me.txtToAmount.Text) > 0 Then
                str += " AND Recv.ReturnDispatchAmount <= " & Val(Me.txtToAmount.Text) & ""
            End If
        End If
        If Not Me.cmbSearchCostCenter.SelectedIndex = -1 Then
            If Me.cmbSearchCostCenter.SelectedIndex <> 0 Then
                str += " AND Recv.PurchaseOrderID = " & Me.cmbSearchCostCenter.SelectedValue
            End If
        End If
        If Me.txtSearchRemarks.Text <> String.Empty Then
            str += " AND Recv.Remarks LIKE '%" & Me.txtSearchRemarks.Text & "%'"
        End If
        If Me.txtPurchaseNo.Text <> String.Empty Then
            str += " AND Recv.ReturnDispatchNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
        End If
        str += "  order by 1 desc"
        ' End If
        FillGridEx(grdSaved, str, True)

        Me.grdSaved.RootTable.Columns.Add("UpdateStoreIssuence")
        Me.grdSaved.RootTable.Columns("UpdateStoreIssuence").ActAsSelector = True
        Me.grdSaved.RootTable.Columns("UpdateStoreIssuence").UseHeaderSelector = True
        Me.grdSaved.RootTable.Columns("UpdateStoreIssuence").Width = 25

        grdSaved.RootTable.Columns(4).Visible = False
        grdSaved.RootTable.Columns(5).Visible = False
        grdSaved.RootTable.Columns(6).Visible = False
        grdSaved.RootTable.Columns(7).Visible = False
        grdSaved.RootTable.Columns(9).Visible = False
        grdSaved.RootTable.Columns(10).Visible = False
        grdSaved.RootTable.Columns(11).Visible = False
        grdSaved.RootTable.Columns(12).Visible = False
        grdSaved.RootTable.Columns(13).Visible = False
        grdSaved.RootTable.Columns(14).Visible = False
        grdSaved.RootTable.Columns(15).Visible = False
        grdSaved.RootTable.Columns("DispatchId").Visible = False
        grdSaved.RootTable.Columns("StoreIssuanceAccountId").Visible = False

        grdSaved.RootTable.Columns("PlanId").Visible = False
        grdSaved.RootTable.Columns("EmpId").Visible = False
        grdSaved.RootTable.Columns("DeptId").Visible = False
        grdSaved.RootTable.Columns("FixedAssetAccountId").Visible = False
        grdSaved.RootTable.Columns("CylinderSecurityAccountId").Visible = False
        grdSaved.RootTable.Columns("Issued").Visible = False ' Task:M20 Set Hidden Column Issued
        grdSaved.RootTable.Columns("DepartmentId").Visible = False 'TAsk:2689 Hidden Column

        grdSaved.RootTable.Columns(0).Caption = "Doc No"
        grdSaved.RootTable.Columns(1).Caption = "Doc Date"
        grdSaved.RootTable.Columns(2).Caption = "Plan No"
        grdSaved.RootTable.Columns(3).Caption = "Customer Name"
        grdSaved.RootTable.Columns(9).Caption = "Cash Paid"
        grdSaved.RootTable.Columns(8).Caption = "Remarks"

        grdSaved.RootTable.Columns(0).Width = 100
        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(2).Width = 100
        grdSaved.RootTable.Columns(3).Width = 200
        grdSaved.RootTable.Columns(8).Width = 200

        'Task:2759 Set rounded foramt
        Me.grdSaved.RootTable.Columns("ReturnDispatchAmount").FormatString = "N" & DecimalPointInValue
        'End Task:2759

        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim btn As System.Windows.Forms.Button
        btn = sender
        If Not Validate_AddToGrid() Then
            If Not Mode = "Normal" Then Me.txtBarcodescan.Focus()
            Exit Sub
        End If
        'If Me.cmbCostSheetItems.ActiveRow IsNot Nothing Then
        '    Me.cmbCostSheetItems.Rows(0).Activate()
        'Else
        '    Exit Sub
        'End If
        'Me.txtCostSheetQty.Text = 0
        If btnAdd.Name = "btnAdd" Then
            If Validate_AddToGrid() Then
                AddItemToGrid()
                'GetTotal()
                ClearDetailControls()
                cmbItem.Focus()
                'FillCombo("Item")
            End If

        Else
            'If Not Val(Me.cmbCostSheetItems.ActiveRow.Cells(0).Value) > 0 Then
            '    ShowErrorMessage("Please select an item")
            '    cmbCostSheetItems.Focus()
            'End If

            'Dim dt As DataTable
            'Dim str As String = String.Empty
            'str = "SELECT    ArticleDefTable.ArticleId as Id, ArticleDescription Item, ArticleCode Code, PurchasePrice as Price, tblCostSheet.Qty  FROM         ArticleDefTable, tblcostsheet " _
            '& " where ArticleDefTable.ArticleId = tblCostSheet.ArticleId " _
            '& " and tblCostSheet.MasterArticleId=" & Me.cmbCostSheetItems.ActiveRow.Cells(0).Value

            'dt = GetDataTable(str)

            'For Each dtRow As DataRow In dt.Rows
            '    Me.cmbCategory.SelectedValue = Me.cmbCategory.SelectedValue
            '    Me.cmbItem.Value = Val(dtRow.Item(0).ToString)
            '    Me.txtQty.Text = Val(dtRow.Item("Qty").ToString) * Val(txtCostSheetQty.Text)
            '    btnAdd_Click(btnAdd, Nothing)
            'Next
        End If

    End Sub
    Private Sub RefreshControls()

        Try
            'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
            ErrorProvider1.Clear()
            If getConfigValueByType("BarcodeEnabled") = "True" Then
                Mode = "BarcodeEnabled"
            Else
                Mode = "Normal"
            End If
            Dim flgAvrRate As Boolean = getConfigValueByType("AvgRate") '' Avrage Rate Apply
            IsEditMode = False
            txtPONo.Text = ""
            dtpPODate.Value = Now
            Me.dtpPODate.Enabled = True
            txtRemarks.Text = ""
            txtPaid.Text = ""
            Me.txtReceivingID.Text = ""
            'txtAmount.Text = ""
            txtTotal.Text = ""
            'txtTotalQty.Text = ""
            txtBalance.Text = ""
            txtPackQty.Text = 1
            Me.btnsave.Text = "&Save"
            Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("I", 6, "ReturnDispatchMasterTable", "ReturnDispatchNo")
            FillCombo("SO")
            FillCombo("Plan")
            FillCombo("Tickets")
            FillCombo("Issuence")
            Me.cmbPo.Enabled = True
            cmbUnit.SelectedIndex = 0
            Me.cmbSalesMan.SelectedIndex = -1
            cmbItem.Rows(0).Activate()
            'grd.Rows.Clear()
            'Me.cmbPo.Focus()
            If Me.cmbBatchNo.Value = Nothing Then
                Me.cmbBatchNo.Enabled = False
            Else
                Me.cmbBatchNo.Enabled = True
            End If
            'me.cmbVendor
            'Me.Button1.Enabled = True
            'Me.cmbCostSheetItems.Enabled = True
            Me.cmbPlan.Enabled = True
            'Me.cmbCostSheetItems.Rows(0).Activate()
            'Me.cmbCostSheetLocation.SelectedIndex = 0
            'Me.txtCostSheetQty.Text = 0
            'FillComboByEdit() R933 Commented Customer List
            Me.DisplayPODetail(-1)
            Me.DisplayDetail(-1)
            Me.dtpFrom.Value = Date.Now.AddMonths(-1)
            Me.dtpTo.Value = Date.Now
            Me.dtpFrom.Checked = False
            Me.dtpTo.Checked = False
            Me.txtSearchDocNo.Text = String.Empty
            'Me.cmbSearchLocation.SelectedIndex = 0
            Me.txtFromAmount.Text = String.Empty
            Me.txtToAmount.Text = String.Empty
            Me.txtPurchaseNo.Text = String.Empty
            'Me.cmbSearchCostCenter.SelectedIndex = 0
            Me.txtSearchRemarks.Text = String.Empty
            Me.SplitContainer1.Panel1Collapsed = True
            'DisplayRecord()
            Me.lblPrintStatus.Text = String.Empty
            Me.txtPO_No.Text = String.Empty
            Me.txtLotNo.Text = String.Empty
            If Not Me.cmbPlan.SelectedIndex = -1 Then Me.cmbPlan.SelectedIndex = 0
            If Not Me.cmbTicketNo.SelectedIndex = -1 Then Me.cmbTicketNo.SelectedIndex = 0
            'Me.cmbUnitCostSheet.SelectedIndex = 0
            Me.cmbFixedAssets.Rows(0).Activate()
            Me.cmbSecurity.Rows(0).Activate()
            If Me.cmbCGAccount.ActiveRow IsNot Nothing Then Me.cmbCGAccount.Rows(0).Activate()
            GetPicture(-1)
            If Mode = "BarcodeEnabled" Then
                If Me.cmbVendor.Rows.Count > 1 Then Me.cmbVendor.Rows(1).Activate()
            Else
                Me.cmbVendor.Rows(0).Activate()
                : Me.dtpPODate.Focus()
            End If
            If blnCGAccountOnStoreIssuance = True Then
                Me.grpCostofGoodsAccount.Enabled = True
            Else
                Me.grpCostofGoodsAccount.Enabled = False
            End If
            If Not Mode = "Normal" Then Me.txtBarcodescan.Focus()
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnEdit.Visible = False
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False
            ''''''''''''''''''''''''''''''
            If Not Me.ComboBox1.SelectedIndex = -1 Then Me.ComboBox1.SelectedIndex = 0
            If Not Me.cmbDepartment.SelectedIndex = -1 Then Me.cmbDepartment.SelectedIndex = 0 'Task:2689 Reseting Department 
            If Not Me.cmbTicketIssuance.SelectedIndex = -1 Then Me.cmbTicketIssuance.SelectedIndex = 0
            If flgAvrRate = True Then Me.txtRate.Enabled = False Else Me.txtRate.Enabled = True 'Task:2712 Price Disabled If aaply average rate.
            GetSecurityRights()
            IsStoreIssuance = False
            IsWIPAccount = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
        Me.txtLotNo.Text = String.Empty
        Me.txtTotalQty.Text = String.Empty ''TASK-408
    End Sub

    Private Function Validate_AddToGrid() As Boolean

        Try
            'Before against task:2388
            'Dim IsMinus As Boolean = getConfigValueByType("AllowMinusStock")
            'Task:2388 Added Validate Service item
            'Dim IsMinus As Boolean = True
            'If Not IsDBNull(CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value) Then
            '    If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
            '        IsMinus = getConfigValueByType("AllowMinusStock")
            '    End If
            'End If
            ''End Task:2388
            'If Mode = "Normal" Then
            '    If Not IsMinus Then
            '        'Comment against task:2416
            '        '    If Val(Me.txtStock.Text) <= 0 Then
            '        '        ShowErrorMessage("Stock does not exist against this item")
            '        '        Return False
            '        '    End If
            '        'Else
            '        'Edn Task:2416
            '        If Val(Me.txtStock.Text) - IIf(Val(txtPackQty.Text) = 0, 1, Val(txtPackQty.Text)) * Val(Me.txtQty.Text) <= 0 Then
            '            msg_Error("Stock does not exist against this item...")
            '            Me.txtQty.Focus() : Validate_AddToGrid = False : Exit Function
            '        End If
            '    End If
            'End If

            If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
                msg_Error("Please select an item")
                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
            End If

            'If Val(txtQty.Text) < 0 Then
            '    msg_Error("Quantity is not greater than 0")
            '    txtQty.Focus() : Validate_AddToGrid = False : Exit Function
            'End If
            'If Val(txtRate.Text) <= 0 Then
            '    msg_Error("Rate must be greater than 0")
            '    txtRate.Focus() : Validate_AddToGrid = False : Exit Function
            'End If

            Validate_AddToGrid = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1

        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'Else
        '    txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        'End If
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        'If Val(Me.txtPackQty.Text) = 0 Then

        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)

        'Else
        '    txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        'End If
        Me.txtTotal.Text = Math.Round(Val(txtTotalQty.Text) * Val(txtRate.Text), DecimalPointInValue)

    End Sub

    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''get the qty in case of pack unit
        If Me.cmbUnit.Text = "Loose" Then
            txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
            Me.txtTotalQty.Enabled = False
            ''TASK TFS1490
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
            ''END TASK TFS1490
        Else
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                Me.txtTotalQty.Enabled = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtPackQty.TabStop = True
                Me.txtTotalQty.Enabled = True
            End If

            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            'If objCon.State = ConnectionState.Open Then objCon.Close()

            'objCon.Open()
            'objCommand.Connection = objCon
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.ActiveRow.Cells(0).Value

            'txtPackQty.Text = objCommand.ExecuteScalar()
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
            txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
            ''TASK TFS1490
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
            ''END TASK TFS1490

        End If

    End Sub
    Private Sub AddItemToGrid()
        'grd.Rows.Add(cmbCategory.Text, cmbItem.Text, Me.cmbBatchNo.Text, cmbUnit.Text, Val(txtQty.Text), Val(txtRate.Text), Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Val(Me.txtPackQty.Text), Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue)

        'Dim dt As DataTable = CType(grd.DataSource, DataTable)
        'Dim dr As DataRow

        'Dim objGridItem As DataGridView
        'Dim intLoopCounter As Integer = 0


        'Dim strCategory As New DataColumn("Category", GetType(String))
        'Dim strItem As New DataColumn("Item", GetType(String))
        'Dim strUnit As New DataColumn("Unit", GetType(String))
        'Dim intQty As New DataColumn("Qty", GetType(Integer))
        'Dim dblRate As New DataColumn("Rate", GetType(Double))
        'Dim dblTotal As New DataColumn("Total", GetType(Double))
        'Dim intCategoryID As New DataColumn("CategoryID", GetType(Integer))
        'Dim intItemID As New DataColumn("ItemID", GetType(Integer))

        'dt.Columns.Add(strCategory)
        'dt.Columns.Add(strItem)
        'dt.Columns.Add(strUnit)
        'dt.Columns.Add(intQty)
        'dt.Columns.Add(dblRate)
        'dt.Columns.Add(dblTotal)
        'dt.Columns.Add(intCategoryID)
        'dt.Columns.Add(intItemID)

        'dr = dt.NewRow
        'dr("Category") = cmbCategory.SelectedText
        'dr("Item") = cmbItem.SelectedText
        'dr("Unit") = cmbUnit.SelectedText
        'dr("Qty") = txtQty.Text
        'dr("Rate") = txtRate.Text
        'dr("Total") = Val(txtTotal.Text)
        ''        dr("CategoryID") = cmbCategory.SelectedValue
        ''        dr("ItemID") = cmbItem.SelectedValue
        'dt.Rows.Add(dr)
        'grd.DataSource = dt
        Dim dblPurchasePrice As Double = 0D
        Dim dblCostPrice As Double = 0D

        Dim strPriceData() As String = GetRateByItem(Me.cmbItem.Value).Split(",")

        If strPriceData.Length > 1 Then
            dblCostPrice = Val(strPriceData(0).ToString)
            dblPurchasePrice = Val(strPriceData(1).ToString)

            If dblCostPrice = 0 Then
                dblCostPrice = dblPurchasePrice
            End If

        End If

        Try
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            'drGrd.Item(grdEnm.Category) = Me.cmbCategory.Text
            drGrd.Item(grdEnm.LocationId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text
            drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text
            drGrd.Item(grdEnm.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Text
            drGrd.Item(grdEnm.BatchNo) = Me.cmbBatchNo.Text
            drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(grdEnm.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(grdEnm.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(grdEnm.Total) = Val(Me.txtTotal.Text)
            drGrd.Item(grdEnm.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
            drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(grdEnm.CurrentPrice) = dblPurchasePrice 'Me.cmbItem.ActiveRow.Cells("Price").Value
            drGrd.Item(grdEnm.BatchId) = Me.cmbBatchNo.Value
            drGrd.Item(grdEnm.ArticleDefMasterId) = 0 'Val(Me.cmbCostSheetItems.Value.ToString)
            drGrd.Item(grdEnm.ArticleDescriptionMaster) = String.Empty 'IIf(Me.cmbCostSheetItems.Value > 0, Me.cmbCostSheetItems.Text.ToString, String.Empty)
            drGrd.Item("Pack_Desc") = Me.cmbUnit.Text.ToString
            drGrd.Item("PurchaseAccountId") = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            'drGrd.Item("CGSAccountId") = Val(Me.cmbItem.ActiveRow.Cells("CGSAccountId").Value.ToString)
            If blnCGAccountOnStoreIssuance = False Then
                drGrd.Item("CGSAccountId") = Val(Me.cmbItem.ActiveRow.Cells("CGSAccountId").Value.ToString)
            Else
                drGrd.Item("CGSAccountId") = Me.cmbCGAccount.Value
            End If
            ''18-Mar-2014 TASK:2506 Imran ali      Add batch quantity and finish goods name in store issue detail report
            'If Val(Me.txtCostSheetQty.Text) > 0 Then
            '    drGrd.Item("PlanUnit") = Me.cmbUnitCostSheet.Text.ToString
            '    drGrd.Item("PlanQty") = Val(Me.txtCostSheetQty.Text)
            'Else
            '    drGrd.Item("PlanUnit") = DBNull.Value
            '    drGrd.Item("PlanQty") = DBNull.Value
            'End If
            drGrd.Item("LotNo") = Me.txtLotNo.Text
            drGrd.Item("Comments") = String.Empty
            drGrd.Item(grdEnm.CostPrice) = dblCostPrice 'IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
            drGrd.Item(grdEnm.TotalQty) = Val(Me.txtTotalQty.Text) ''TASK-408
            drGrd.Item(grdEnm.DepartmentId) = 0
            drGrd.Item(grdEnm.EstimationId) = 0
            'End Task:2506
            dtGrd.Rows.InsertAt(drGrd, 0)
            dtGrd.AcceptChanges()
            Me.grd.UpdateData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged

        'If cmbCategory.SelectedIndex > 0 Then
        '    'FillCombo("ItemFilter")
        'End If
        Try
            'Before against task:2366
            'If IsFormOpen = True Then FillCombo("Item")
            If IsFormOpen = True Then
                If flgLocationWiseItem = True Then FillCombo("Item")
            End If
            ''End Task:2366
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub GetTotal()
    '    Dim i As Integer
    '    Dim dblTotalAmount As Double
    '    Dim dblTotalQty As Double
    '    For i = 0 To grd.Rows.Count - 1
    '        dblTotalAmount = dblTotalAmount + Val(grd.Rows(i).Cells(6).Value)
    '        dblTotalQty = dblTotalQty + Val(grd.Rows(i).Cells(4).Value)

    '    Next
    '    txtAmount.Text = dblTotalAmount
    '    txtTotalQty.Text = dblTotalQty
    '    txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)

    '    Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount


    'End Sub

    Private Sub FillCombo(ByVal strCondition As String)

        Dim str As String


        If strCondition = "Item" Then
            'Before against task:2388
            'str = "SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, Isnull(PurchasePrice,0) as Price , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
            'Task:2388 Added Column SerivceItem
            'Before against task:2478
            'str = "SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, Isnull(PurchasePrice,0) as Price , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
            'End Task:2388
            'Task:2478 added column SortOrder
            'str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, Isnull(PurchasePrice,0) as Price , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId,IsNull(CGSAccountId,0) as CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.SortOrder,0) as SortOrder FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
            'end Task:2478
            'str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, Isnull(PurchasePrice,0) as Price , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId,IsNull(CGSAccountId,0) as CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.SortOrder,0) as SortOrder,IsNull(ArticleDefView.Cost_Price,0) as [Cost Price] FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
            If Not getConfigValueByType("AvgRate").ToString = "True" Then
                str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, Isnull(PurchasePrice,0) as Price , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId,IsNull(CGSAccountId,0) as CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.SortOrder,0) as SortOrder,IsNull(ArticleDefView.Cost_Price,0) as [Cost Price] FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
            Else
                str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, Case When IsNull(ArticleDefView.Cost_Price,0) > 0 Then IsNull(ArticleDefView.Cost_Price,0) Else IsNull(PurchasePrice,0) End as Price , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId,IsNull(CGSAccountId,0) as CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.SortOrder,0) as SortOrder,IsNull(ArticleDefView.Cost_Price,0) as [Cost Price] FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId "
            End If
            str += " where Active=1 "
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                'Comment against Task:2366 
                'If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                If flgLocationWiseItem = True Then
                    str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                    'Else
                    '    str += str
                End If
            End If
            'End Task:2366
            ''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
            If ItemSortOrder = True Then
                str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
            'End Task:2452
            'FillDropDown(cmbItem, str)
            FillUltraDropDown(Me.cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Column Hidden
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True 'Task:2478 Column Hidden
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True 'Task:2478 Column Hidden
            If ItemFilterByName = True Then
                Me.rdoName.Checked = True
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                If Me.rdoName.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        ElseIf strCondition = "CostSheetItem" Then
            'Before against task:2412
            'str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, " & IIf(StoreIssuanceDependonProductionPlan = True, " Isnull(PL.qty,0)  as [Plan Qty] ", "0  as [Plan Qty]") & ", PurchasePrice as Price, isnull(PackQty,0) as PackQty  FROM  ArticleDefTableMaster  "
            'Task:2412 Change Validation On Plan No'
            str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, " & IIf(Me.cmbPlan.SelectedIndex > 0, " Isnull(PL.qty,0)  as [Plan Qty] ", "0  as [Plan Qty]") & ", PurchasePrice as Price, isnull(PackQty,0) as PackQty  FROM  ArticleDefTableMaster  "

            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                If Not Me.cmbPlan.SelectedIndex = -1 Then
                    'str += " LEFT OUTER JOIN (SELECT  ArticleDefView.MasterId as ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM   dbo.PlanDetailTable INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanId=" & Me.cmbPlan.SelectedValue & " GROUP BY PlanDetailTable.ArticleDefId,ArticleDefView.MasterId) PL on PL.ArticleDefId =  ArticleDefTableMaster.ArticleId"
                    str += " LEFT OUTER JOIN (SELECT  ArticleDefView.MasterId as ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM   dbo.PlanDetailTable INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanId=" & Me.cmbPlan.SelectedValue & " GROUP BY PlanDetailTable.ArticleDefId,ArticleDefView.MasterId Having SUM(ISNULL(Qty, 0)) <> 0) PL on PL.ArticleDefId =  ArticleDefTableMaster.ArticleId"
                End If
            End If
            str += " where Active=1  and ArticleId in (select distinct masterarticleID from tblcostsheet)"
            If flgCompanyRights = True Then
                str += " AND ArticleDefTableMaster.ArticleGroupId IN(Select ArticleGroupId From ArticleGroupDefTable WHERE SubSubId in (Select main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))"
            End If
            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            'If Not Me.cmbPlan.SelectedIndex < 0 Then
            If Me.cmbPlan.SelectedIndex > 0 Then
                str += " AND ArticleId In (Select DISTINCT MasterId From PlanMasterTable INNER JOIN PlanDetailTable On PlanMasterTable.PlanId = PlanDetailTable.PlanId INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanMasterTable.PlanId=" & Me.cmbPlan.SelectedValue & ")"
            End If
            'End If
            'End Task:2412
            'FillDropDown(cmbItem, str)
            'FillUltraDropDown(Me.cmbCostSheetItems, str)
            'Me.cmbCostSheetItems.Rows(0).Activate()
            'Me.cmbCostSheetItems.DisplayLayout.Bands(0).Columns(0).Hidden = True
            'Me.cmbCostSheetItems.DisplayLayout.Bands(0).Columns("PackQty").Hidden = True
        ElseIf strCondition = "Category" Then
            'str = "Select ArticleGroupID, ArticleGroupName from ArticleGroupDefTable where Active=1"
            'FillDropDown(cmbCategory, str)
            'Task#16092015 Load Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            FillDropDown(cmbCategory, str, False)
            'FillDropDown(cmbCostSheetLocation, str)
            'FillDropDown(Me.cmbDepartment, str) 'Task:2689 Fill Department Dropdown
        ElseIf strCondition = "SearchLocation" Then
            str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            FillDropDown(Me.cmbSearchLocation, str)

        ElseIf strCondition = "ItemFilter" Then
            'Before against task:2388
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,PurchasePrice as Price , ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue & ""
            'Task:2388 Added Column ServiceItem
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,PurchasePrice as Price , ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue & ""
            'End Task:2388
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CopmanyId=" & MyCompanyId
            End If
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Column Hidden
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblCustomer.AccountId AS ID, tblCustomer.CustomerName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblCustomer.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory"
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                "dbo.tblListTerritory.TerritoryName as Territory, tblCustomer.Email, tblCustomer.Phone " & _
                                "FROM  dbo.tblCustomer INNER JOIN " & _
                                "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                "WHERE (dbo.vwCOADetail.account_type IN('Customer','Customer Service'))  "
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            ' If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforInventory(Me.Name) And LoginGroup <> "Administrator" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                              "dbo.tblListTerritory.TerritoryName as Territory, tblCustomer.Email, tblCustomer.Phone " & _
                              "FROM  dbo.tblCustomer INNER JOIN " & _
                              "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                              "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                              "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                              "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                              "WHERE (dbo.vwCOADetail.detail_title Is Not NULL )  "
                str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") ) "
                str += " And (dbo.vwCOADetail.account_type IN('Customer','Customer Service')) " ''TFS4689 : Getting Relevent Accounts according to the screen
            End If
            ''End TFS3322
            If IsEditMode = False Then
                str += " and vwCOADetail.Active=1"
            Else
                str += " and vwCOADetail.Active in(0,1,NULL)"
            End If
            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbVendor, str)
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        ElseIf strCondition = "SO" Then
        'str = "Select PurchaseOrderID, PurchaseOrderNo from PurchaseOrderMasterTable where PurchaseorderId not in(select PurchaseOrderId from ReturnDispatchMasterTable)"
        ' str = "Select PlanID, PlanNo from PlanMasterTable where  CustomerID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & ""
        str = "Select CostCenterID, Name from tblDefCostCenter"
        If IsEditMode = False Then
            str += " WHERE Active=1"
        Else
            str += " WHERE Active IN(1,0,NULL)"
        End If
        str += " ORDER BY Name"
        FillDropDown(cmbPo, str)

        ElseIf strCondition = "SearchCostCenter" Then
        str = "Select CostCenterID, Name from tblDefCostCenter"
        FillDropDown(Me.cmbSearchCostCenter, str)

        ElseIf strCondition = "SOSpecific" Then
        'str = "Select PurchaseOrderID, PurchaseOrderNo from PurchaseOrderMasterTable where PurchaseorderId not in(select PurchaseOrderId from ReturnDispatchMasterTable) and vendorid=" & Me.cmbVendor.Value & ""
        str = "Select PlanID, PlanNo from PlanMasterTable where CustomerID=" & Me.cmbVendor.ActiveRow.Cells(0).Value & ""
        FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOComplete" Then
        'str = "Select PurchaseOrderID, PurchaseOrderNo from PurchaseOrderMasterTable "
        str = "Select PlanID, PlanNo from PlanMasterTable where CustomerID = " & Me.cmbVendor.Value & ""
        FillDropDown(cmbPo, str)
        ElseIf strCondition = "SM" Then
        str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee Where Active=1 " ''TASKTFS75 added and set active =1
        FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "grdLocation" Then
        'Task#16092015 Load Locations user wise
        'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
        '    str = "Select Location_Id, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") "
        'Else
        '    str = "Select Location_Id, Location_Name From tblDefLocation"
        'End If
        'str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ") " _
        '    & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
        '    & "Else " _
        '    & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
        str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
             & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
             & " Else " _
             & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
        Dim dt As DataTable = GetDataTable(str)
        Me.grd.RootTable.Columns(grdEnm.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "Plan" Then
        'str = "Select PlanId, PlanNo From PlanmasterTable " & IIf(IsEditMode = False, " WHERE Status='Open'", "") & "  Order By PlanNo Desc"
        str = "Select PlanId, PlanNo From PlanmasterTable Order By PlanNo Desc"
        FillDropDown(Me.cmbPlan, str)
        ElseIf strCondition = "Employees" Then
        str = "Select Employee_Id, Employee_Name From tblDefEmployee Where Active=1" ''TASKTFS75 added and set active =1
        FillDropDown(Me.ComboBox1, str)
        ElseIf strCondition = "ArticlePack" Then
        Me.cmbUnit.ValueMember = "ArticlePackId"
        Me.cmbUnit.DisplayMember = "PackName"
        Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "FixedAssetAccount" Then
        FillUltraDropDown(Me.cmbFixedAssets, "Select coa_detail_id, detail_title As Name from vwcoadetail where account_type='General'")
        Me.cmbFixedAssets.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Me.cmbFixedAssets.Rows(0).Activate()
        ElseIf strCondition = "SecurityAccount" Then
        FillUltraDropDown(Me.cmbSecurity, "Select coa_detail_id, detail_title as Name from vwcoadetail where account_type='General'")
        Me.cmbSecurity.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Me.cmbSecurity.Rows(0).Activate()
        ElseIf strCondition = "CGAccount" Then
        FillUltraDropDown(Me.cmbCGAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code] from vwCOADetail WHERE detail_title <> ''")
        Me.cmbCGAccount.Rows(0).Activate()
        Me.cmbCGAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
        ElseIf strCondition = "Issuence" Then
        str = String.Empty
        'str = "Select DispatchMasterTable.DispatchId, DispatchMasterTable.DispatchNo,* From DispatchMasterTable WHERE DispatchId Not In(Select ReturnDispatchMasterTable.DispatchId From ReturnDispatchMasterTable INNER JOIN ReturnDispatchDetailTable ON ReturnDispatchMasterTable.ReturnDispatchId = ReturnDispatchDetailTable.ReturnDispatchId INNER JOIN DispatchDetailTable ON ReturnDispatchDetailTable.DispatchDetailId = DispatchDetailTable.DispatchDetailId Where ReturnDispatchDetailTable.Qty >= DispatchDetailTable.Qty) AND LEFT(DispatchMasterTable.DispatchNo,1)='I' ORDER BY DispatchMasterTable.DispatchId DESC"
        str = "Select DispatchMasterTable.DispatchId, DispatchMasterTable.DispatchNo,* From DispatchMasterTable WHERE DispatchId In (Select Distinct DispatchId From DispatchDetailTable Where IsNull(ReturnedTotalQty, 0) < IsNull(Qty,0)) And LEFT(DispatchMasterTable.DispatchNo,1)='I' ORDER BY DispatchMasterTable.DispatchId DESC "
        FillUltraDropDown(Me.cmbIssuence, str)
        If Me.cmbIssuence.DisplayLayout.Bands(0).Columns.Count > 0 Then
            For c As Integer = 0 To Me.cmbIssuence.DisplayLayout.Bands(0).Columns.Count - 1
                Me.cmbIssuence.DisplayLayout.Bands(0).Columns(c).Hidden = True
            Next
            Me.cmbIssuence.Rows(0).Activate()
            Me.cmbIssuence.DisplayLayout.Bands(0).Columns("DispatchNo").Hidden = False
            Me.cmbIssuence.DisplayLayout.Bands(0).Columns("DispatchDate").Hidden = False
        End If
        'ElseIf strCondition = "Ticket" Then
        '    'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
        '    str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join AllocationMaster ON Ticket.PlanTicketsId = AllocationMaster.TicketID Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And AllocationMaster.Status = 1"
        '    FillDropDown(cmbTicketNo, str)
        'ElseIf strCondition = "Tickets" Then
        '    'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
        '    str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join AllocationMaster ON Ticket.PlanTicketsId = AllocationMaster.TicketID Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where AllocationMaster.Status = 1"
        '    FillDropDown(cmbTicketNo, str)
        'ElseIf strCondition = "Plan" Then
        '    str = "Select PlanId, PlanNo + ' ~ ' + Convert(Varchar(9), PlanDate, 113) As PlanNo From PlanmasterTable INNER JOIN DispatchMasterTable ON PlanMasterTable.PlanId = DispatchMasterTable.PlanId Order By PlanNo Desc"
        '    FillDropDown(Me.cmbPlan, str)

        ''TASK:919 adding new combo of issuance and modified current.
        ElseIf strCondition = "Ticket" Then
        str = String.Empty
        'str = "Select PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo, IsNull(DispatchMasterTable.StoreIssuanceAccountId, 0) As StoreIssuanceAccountId FROM PlanTicketsMaster INNER JOIN DispatchMasterTable ON PlanTicketsMaster.PlanTicketsMasterID = DispatchMasterTable.PlanTicketId  Where PlanTicketsMaster.PlanId = " & Me.cmbPlan.SelectedValue & " Order By PlanTicketsMaster.PlanTicketsMasterID DESC"
        str = "Select PlanTicketsMasterID, TicketNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster  Where PlanId = " & Me.cmbPlan.SelectedValue & " Order By PlanTicketsMasterID DESC"

        FillDropDown(Me.cmbTicketNo, str)
        ElseIf strCondition = "Department" Then
        str = String.Empty
        'str = "Select Distinct ProdStep_Id, prod_step, prod_Less, sort_Order, IsNull(DispatchMasterTable.StoreIssuanceAccountId, 0) As StoreIssuanceAccountId from tblProSteps INNER JOIN DispatchMasterTable ON tblProSteps.ProdStep_Id = DispatchMasterTable.DepartmentId  Where DispatchMasterTable.PlanTicketId =" & Me.cmbTicketNo.SelectedValue & " order by sort_Order "
        'str = "Select Distinct ProdStep_Id, prod_step, prod_Less, sort_Order from tblProSteps INNER JOIN DispatchDetailTable ON tblProSteps.ProdStep_Id = DispatchDetailTable.SubDepartmentID INNER JOIN DispatchMasterTable ON DispatchDetailTable.DispatchId = DispatchMasterTable.DispatchId Where DispatchMasterTable.DispatchId =" & Me.cmbTicketIssuance.SelectedValue & " order by sort_Order "
        str = "Select Distinct ProdStep_Id, prod_step, prod_Less, sort_Order from tblProSteps INNER JOIN DispatchDetailTable ON tblProSteps.ProdStep_Id = DispatchDetailTable.SubDepartmentID INNER JOIN DispatchMasterTable ON DispatchDetailTable.DispatchId = DispatchMasterTable.DispatchId Where DispatchMasterTable.PlanTicketId =" & Me.cmbTicketNo.SelectedValue & " order by sort_Order "

        FillDropDown(Me.cmbDepartment, str)
        'ElseIf strCondition = "CGAccountAgainstDepartment" Then
        '    FillUltraDropDown(Me.cmbCGAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code] from vwCOADetail WHERE detail_title <> '' And coa_detail_id =" & CType(Me.cmbDepartment.SelectedItem, DataRowView).Item("StoreIssuanceAccountId") & "")
        '    Me.cmbCGAccount.Rows(0).Activate()
        '    Me.cmbCGAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
        ElseIf strCondition = "TicketIssuance" Then
        str = String.Empty
        str = "Select DispatchMasterTable.DispatchId, DispatchMasterTable.DispatchNo,* From DispatchMasterTable WHERE DispatchId In (Select Distinct DispatchId From DispatchDetailTable Where (IsNull(ReturnedTotalQty, 0)+IsNull(ConsumedQty, 0)) < IsNull(Qty,0)) And DispatchMasterTable.PlanTicketId =" & Me.cmbTicketNo.SelectedValue & " And LEFT(DispatchMasterTable.DispatchNo,1)='I' ORDER BY DispatchMasterTable.DispatchId DESC "
        FillDropDown(Me.cmbTicketIssuance, str)
        ''End TASK:919
        End If

    End Sub

    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
        txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
    End Sub

    Private Function Save() As Boolean
        Me.grd.UpdateData()
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("I", 6, "ReturnDispatchMasterTable", "ReturnDispatchNo")
        setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection

        Dim i As Integer

        objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        gobjLocationId = MyCompanyId
        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
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
        If blnCGAccountOnStoreIssuance = True Then
            AccountId2 = Me.cmbCGAccount.Value
        Else
            AccountId2 = getConfigValueByType("StoreIssuenceAccount")
        End If
        Dim StoreIssuanceId As Integer = 0
        If Not Me.cmbTicketIssuance.SelectedValue Is Nothing AndAlso Me.cmbTicketIssuance.SelectedValue > 0 Then
            StoreIssuanceId = Me.cmbTicketIssuance.SelectedValue
        ElseIf Not Me.cmbIssuence.ActiveRow Is Nothing Then
            StoreIssuanceId = cmbIssuence.Value
        End If
        Dim dtWIP As DataTable = CType(Me.grd.DataSource, DataTable)
        dtWIP.AcceptChanges()
        For i = 0 To dtWIP.Rows.Count - 1
            If Val(dtWIP.Rows(i).Item("WIPAccountId").ToString) Then
                IsWIPAccount = True
                Exit For
            End If
        Next


        'For Each _row As Janus.Windows.GridEX.GridEXRow In grd.GetRows
        '    If Val(_row.Cells("WIPAccountId").Value.ToString) > 0 Then
        '        IsWIPAccount = True
        '        Exit For
        '    End If
        'Next
        If IsWIPAccount = False Then
            If flgStoreIssuenceVoucher = True Then
                If AccountId <= 0 Then
                    ShowErrorMessage("Purchase account is not map.")
                    Me.dtpPODate.Focus()
                    Return False
                End If
                If AccountId2 <= 0 Then
                    ShowErrorMessage("Cost of good account is not map.")
                    Me.dtpPODate.Focus()
                    Return False
                End If
            ElseIf flgCylinderVoucher = True Then
                If CylinderStockAccountId <= 0 Then
                    ShowErrorMessage("Cylinder stock account is not map.")
                    Me.dtpPODate.Focus()
                    Return False
                End If
            End If
        End If
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try

            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against ReqId-934 
            'objCommand.CommandText = "Insert into ReturnDispatchMasterTable (locationId,ReturnDispatchNo,ReturnDispatchDate,VendorId,PurchaseOrderId, ReturnDispatchQty,ReturnDispatchAmount, CashPaid, Remarks,UserName, PONo, PlanId, EmpId, FixedAssetAccountId,CylinderSecurityAccountId) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & Me.txtPO_No.Text & "', " & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & ", " & Me.ComboBox1.SelectedValue & ", " & Me.cmbFixedAssets.Value & ", " & Me.cmbSecurity.Value & ")"
            'ReqId-934 Resolve Comma Error
            'Before against task:M21
            'objCommand.CommandText = "Insert into ReturnDispatchMasterTable (locationId,ReturnDispatchNo,ReturnDispatchDate,VendorId,PurchaseOrderId, ReturnDispatchQty,ReturnDispatchAmount, CashPaid, Remarks,UserName, PONo, PlanId, EmpId, FixedAssetAccountId,CylinderSecurityAccountId) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & Me.txtPO_No.Text.Replace("'", "''") & "', " & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & ", " & Me.ComboBox1.SelectedValue & ", " & Me.cmbFixedAssets.Value & ", " & Me.cmbSecurity.Value & ")"
            'Task:M21 Added Column Issued.
            'Before against task:2689
            'objCommand.CommandText = "Insert into ReturnDispatchMasterTable (locationId,ReturnDispatchNo,ReturnDispatchDate,VendorId,PurchaseOrderId, ReturnDispatchQty,ReturnDispatchAmount, CashPaid, Remarks,UserName, PONo, PlanId, EmpId, FixedAssetAccountId,CylinderSecurityAccountId, Issued) values( " _
            '                       & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & Me.txtPO_No.Text.Replace("'", "''") & "', " & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & ", " & Me.ComboBox1.SelectedValue & ", " & Me.cmbFixedAssets.Value & ", " & Me.cmbSecurity.Value & ", " & IIf(Me.chkIssued.Checked = True, 1, 0) & ")"
            'End Task:M21
            'Task:2689 Added Field DepartmentId
            'objCommand.CommandText = "Insert into ReturnDispatchMasterTable (locationId,ReturnDispatchNo,ReturnDispatchDate,VendorId,PurchaseOrderId, ReturnDispatchQty,ReturnDispatchAmount, CashPaid, Remarks,UserName, PONo, PlanId, EmpId, FixedAssetAccountId,CylinderSecurityAccountId, Issued, DepartmentId) values( " _
            '                                   & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & Me.txtPO_No.Text.Replace("'", "''") & "', " & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & ", " & Me.ComboBox1.SelectedValue & ", " & Me.cmbFixedAssets.Value & ", " & Me.cmbSecurity.Value & ", " & IIf(Me.chkIssued.Checked = True, 1, 0) & ", " & IIf(Me.cmbDepartment.SelectedIndex = -1, "NULL", Me.cmbDepartment.SelectedValue) & ")"
            'objCommand.CommandText = "Insert into ReturnDispatchMasterTable (locationId,ReturnDispatchNo,ReturnDispatchDate,VendorId,PurchaseOrderId, ReturnDispatchQty,ReturnDispatchAmount, CashPaid, Remarks,UserName, PONo, PlanId, EmpId, FixedAssetAccountId,CylinderSecurityAccountId, Issued, DepartmentId,StoreIssuanceAccountId) values( " _
            '                                   & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & Me.txtPO_No.Text.Replace("'", "''") & "', " & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", " & Me.ComboBox1.SelectedValue & ", " & Me.cmbFixedAssets.Value & ", " & Me.cmbSecurity.Value & ", " & IIf(Me.chkIssued.Checked = True, 1, 0) & ", " & IIf(Me.cmbDepartment.SelectedIndex = -1, "NULL", Me.cmbDepartment.SelectedValue) & "," & AccountId2 & ")"
            objCommand.CommandText = "Insert into ReturnDispatchMasterTable(locationId,ReturnDispatchNo,ReturnDispatchDate,VendorId,PurchaseOrderId, ReturnDispatchQty,ReturnDispatchAmount, CashPaid, Remarks,UserName, PONo, PlanId, EmpId, FixedAssetAccountId,CylinderSecurityAccountId, Issued, DepartmentId,StoreIssuanceAccountId, DispatchId, TicketID) values( " _
                                               & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & Me.txtPO_No.Text.Replace("'", "''") & "', " & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", " & Me.ComboBox1.SelectedValue & ", " & Me.cmbFixedAssets.Value & ", " & Me.cmbSecurity.Value & ", " & IIf(Me.chkIssued.Checked = True, 1, 0) & ", " & IIf(Me.cmbDepartment.SelectedIndex = -1, "NULL", Me.cmbDepartment.SelectedValue) & "," & AccountId2 & ", " & StoreIssuanceId & ", " & IIf(Me.cmbTicketNo.SelectedIndex = -1, 0, Me.cmbTicketNo.SelectedValue) & ")"
            objCommand.ExecuteNonQuery()

            If Me.chkIssued.Checked = True Then 'Task:M21 If Issued Checked Then Update Voucher, Update Stock.
                If (flgStoreIssuenceVoucher = True Or flgCylinderVoucher = True) Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
                    objCommand.CommandText = ""
                    'Before against task:M101
                    'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                    '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
                    '                           & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                    '                           & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
                    '                           & " SELECT @@IDENTITY"
                    'Task:M101 Added Field Remarks
                    ''TASK TFS1427 added UserName and Posted_UserName
                    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                              & " cheque_no, cheque_date,post,Source,voucher_code,Remarks, UserName, Posted_UserName)" _
                                              & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                              & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "',N'" & Me.txtRemarks.Text.Replace("'", "''") & "', N'" & LoginUserName.Replace("'", "''") & "', N'" & LoginUserName.Replace("'", "''") & "')" _
                                              & " SELECT @@IDENTITY"
                    'End Task:M101
                    lngVoucherMasterId = objCommand.ExecuteScalar
                End If
            End If
            'End Task:M21

            ''Production Planing... 
            'objCommand.CommandText = ""
            'objCommand.CommandText = "Insert into CostingDetailTable (ReturnDispatchId, LocationId, ArticleDefId, Qty, CurrentPrice) Values " _
            '& " ( ident_current('ReturnDispatchMasterTable'), " & Me.cmbCostSheetLocation.SelectedValue & ",   " & Me.cmbCostSheetItems.ActiveRow.Cells(0).Value & ", " & Val(Me.txtCostSheetQty.Text) & ", " & Val(Me.cmbCostSheetItems.ActiveRow.Cells("Price").Value) & ")"
            'objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""
            'For i = 0 To grd.RowCount - 1
            '    objCommand.CommandText = ""
            '    'objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID) values( " _
            '    '                        & " ident_current('ReturnDispatchMasterTable')," & Val(grd.GetRows(i).Cells(8).Value) & ",N'" & (grd.GetRows(i).Cells(3).Value) & "'," & Val(grd.GetRows(i).Cells(4).Value) & ", " _
            '    '                        & " " & IIf(grd.GetRows(i).Cells(3).Value = "Loose", Val(grd.GetRows(i).Cells(4).Value), (Val(grd.GetRows(i).Cells(4).Value) * Val(grd.GetRows(i).Cells(9).Value))) & ", " & Val(grd.GetRows(i).Cells(5).Value) & ", " & Val(grd.GetRows(i).Cells(9).Value) & " , " & Val(grd.GetRows(i).Cells(10).Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "'," & grd.GetRows(i).Cells("BatchID").Value & "," & Val(grd.GetRows(i).Cells("LocationID").Value) & ")"

            '    'objCommand.ExecuteNonQuery()
            '    objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId) values( " _
            '                                           & " ident_current('ReturnDispatchMasterTable'), " & Val(grd.GetRows(i).Cells(grdEnm.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(grdEnm.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) & ", " _
            '                                           & " " & IIf(grd.GetRows(i).Cells(grdEnm.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(grdEnm.Qty).Value), (Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) * Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(grdEnm.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(grdEnm.CurrentPrice).Value) & ",N'" & grd.GetRows(i).Cells(grdEnm.BatchNo).Value & "'," & grd.GetRows(i).Cells(grdEnm.BatchId).Value & "," & grd.GetRows(i).Cells(grdEnm.LocationId).Value & ", " & Val(grd.GetRows(i).Cells(grdEnm.ArticleDefMasterId).Value.ToString) & ")"

            '    objCommand.ExecuteNonQuery()

            '    'Val(grd.Rows(i).Cells(5).Value)
            '    If StoreIssuanceDependonProductionPlan = True Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Update PlanCostSheetDetailTable Set IssuedQty=IssuedQty+" & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value.ToString) & " WHERE PlanCostSheetDetailTable.PlanId=" & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & " AND PlanCostSheetDetailTable.ArticleDefId=" & grd.GetRows(i).Cells(grdEnm.ItemId).Value & " AND PlanCostSheetDetailTable.ArticleMasterId=" & Val(grd.GetRows(i).Cells(grdEnm.ArticleDefMasterId).Value.ToString)
            '        objCommand.ExecuteNonQuery()
            '    End If

            'Next

            'If flgStoreIssuenceVoucher = True Then 'Code By Imran 3-7-2013 Ref by Request ID: 725

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                               & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '                               & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                               & " NULL, NULL, 0,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
            '                               & " SELECT @@IDENTITY"
            '    lngVoucherMasterId = objCommand.ExecuteScalar

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            '    objCommand.ExecuteNonQuery()

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Store Issuence Ref: " & Me.txtPONo.Text & "', " & Me.cmbPo.SelectedValue & " )"
            '    objCommand.ExecuteNonQuery()


            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & " ,0 , 'Store Issuence Ref: " & Me.txtPONo.Text & "',  " & Me.cmbPo.SelectedValue & ")"
            '    objCommand.ExecuteNonQuery()

            'End If
            StockList = New List(Of StockDetail)
            Dim dblTotalCost As Double = 0D
            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            For i = 0 To dtGrd.Rows.Count - 1
                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString, Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString), grd, , trans)
                End If

                Dim CostPrice As Double = 0D
                Dim CrrStock As Double = 0D
                If GLAccountArticleDepartment = True Then
                    AccountId = Val(dtGrd.Rows(i).Item("PurchaseAccountId").ToString)
                    If blnCGAccountOnStoreIssuance = False Then
                        AccountId2 = Val(dtGrd.Rows(i).Item("CGSAccountId").ToString)
                    End If
                End If
                If Val(dtGrd.Rows(i).Item("WIPAccountId").ToString) > 0 Then
                    AccountId2 = Val(dtGrd.Rows(i).Item("WIPAccountId").ToString)
                End If
                'If flgAvrRate = True Then


                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, Round(ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))),1) as Amount  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & " " _
                '                                    & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                '    Dim dtCrrStock As New DataTable
                '    Dim daCrrStock As New OleDbDataAdapter(objCommand)
                '    daCrrStock.Fill(dtCrrStock)

                '    If dtCrrStock IsNot Nothing Then
                '        If dtCrrStock.Rows.Count > 0 Then
                '            'Before against task:2401
                '            'If Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) <> 0 Then
                '            'Task:2401 Add more validation at qty
                '            If Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) <> 0 AndAlso Val(dtCrrStock.Rows(0).Item("qty").ToString) <> 0 Then
                '                'End Task:2401
                '                CrrStock = dtCrrStock.Rows(0).Item(2)
                '                CostPrice = IIf(Val(dtCrrStock.Rows(0).Item(3).ToString) = 0, 0, Val(dtCrrStock.Rows(0).Item(3).ToString) / CrrStock)
                '            Else
                '                CostPrice = Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)
                '            End If
                '        Else
                '            CostPrice = Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)
                '        End If
                '    Else 'Task:M36 Set CostPrice against normal rate
                '        CostPrice = Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)
                '    End If
                'Else 'Task:M36 Set CostPrice against normal rate
                '    CostPrice = Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)
                'End If

                'Dim dtLastReturnData As DataTable = GetDataTable("Select IsNull(Rate,0) as Rate, IsNull(OutQty,0) as OutQty, IsNull(StockMasterTable.StockTransId,0) as StockTransId,IsNull(StockTransDetailId,0) as StockTransDetailId From StockDetailTable INNER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefId=" & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & " AND LEFT(StockMasterTable.DocNo,1) ='I' AND IsNull(OutQty,0) <> 0 ORDER BY Convert(DateTime,StockMasterTable.DocDate,102),StockDetailTable.StockTransDetailId DESC ", trans)
                'dtLastReturnData.AcceptChanges()
                'Dim remainReturnQty As Double = 0D
                'If dtLastReturnData Is Nothing Then Return 0
                'Dim dblActualReturn As Double = 0D
                'Dim dblTotalQty As Double = 0D

                'If dtLastReturnData.Rows.Count > 0 Then
                '    For Each r As DataRow In dtLastReturnData.Rows
                '        If dblTotalQty = Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString)))) Then
                '            Exit For
                '        End If
                '        If remainReturnQty > 0 Then
                '            If Val(r.Item("OutQty").ToString) <= remainReturnQty Then
                '                dblActualReturn = Val(r.Item("OutQty").ToString)
                '                remainReturnQty = Val(r.Item("OutQty").ToString) - dblActualReturn
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            Else
                '                'Val(r.Item("OutQty").ToString) >= remainReturnQty 
                '                dblActualReturn = remainReturnQty
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            End If
                '        Else
                '            If Val(r.Item("OutQty").ToString) >= Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString)))) Then
                '                dblActualReturn = Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString))))
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            Else
                '                remainReturnQty = (Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString)))) - Val(r.Item("OutQty").ToString))
                '                dblActualReturn = (Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString))))) - remainReturnQty
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            End If
                '        End If
                '        'End If
                '        StockDetail = New StockDetail
                '        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                '        StockDetail.LocationId = Val(dtGrd.Rows(i).Item("LocationId").ToString)
                '        StockDetail.ArticleDefId = Val(dtGrd.Rows(i).Item("ItemId").ToString)
                '        StockDetail.InQty = dblActualReturn 'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))) - remainReturnQty
                '        StockDetail.OutQty = 0
                '        StockDetail.Rate = IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item("Rate").ToString), CostPrice)
                '        StockDetail.InAmount = StockDetail.InQty * StockDetail.Rate  'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)), (((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)))
                '        StockDetail.OutAmount = 0
                '        StockDetail.Remarks = String.Empty
                '        'Task:M16 Set Values In Engine_No and Chassis_No 
                '        StockDetail.Engine_No = String.Empty 'grd.GetRows(i).Cells("Engine_No").Value.ToString
                '        StockDetail.Chassis_No = String.Empty 'grd.GetRows(i).Cells("Chassis_No").Value.ToString
                '        'End Task:M16
                '        StockList.Add(StockDetail)
                '        dblTotalQty += dblActualReturn
                '    Next
                'Else
                'CostPrice = GetAvgRateByItem(Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString))
                'If (Val(dtGrd.Rows(i).Item("Qty").Value.ToString) > 0 Or Val(dtGrd.Rows(i).Item("Sample Qty").Value.ToString) > 0) Then

                Dim dblPurchasePrice As Double = 0D
                Dim dblCostPrice As Double = 0D

                Dim strPriceData() As String = GetRateByItem(Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString)).Split(",")

                If strPriceData.Length > 1 Then
                    dblCostPrice = Val(strPriceData(0).ToString)
                    dblPurchasePrice = Val(strPriceData(1).ToString)
                End If
                If flgAvrRate = True Then
                    If CostPrice = 0 Then
                        CostPrice = dblPurchasePrice  'GetAvgRateByItem(Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString))
                    Else
                        CostPrice = dblCostPrice
                    End If
                Else
                    CostPrice = Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)
                End If

                StockDetail = New StockDetail
                StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                StockDetail.LocationId = dtGrd.Rows(i).Item(grdEnm.LocationId).ToString
                StockDetail.ArticleDefId = dtGrd.Rows(i).Item(grdEnm.ItemId).ToString
                ''StockDetail.InQty = IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString), ((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString)))
                StockDetail.InQty = Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) ''TASK-408 to add TotalQty instead of Qty
                StockDetail.OutQty = 0
                StockDetail.Rate = IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)
                ''StockDetail.InAmount = IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", ((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)), (((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)))
                StockDetail.InAmount = ((Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice))
                StockDetail.OutAmount = 0
                StockDetail.Remarks = dtGrd.Rows(i).Item("Comments").ToString

                ''Start TASK-470 on 01-07-2016
                StockDetail.PackQty = dtGrd.Rows(i).Item("PackQty").ToString
                StockDetail.In_PackQty = dtGrd.Rows(i).Item("Qty").ToString
                StockDetail.Out_PackQty = 0
                ''End TASK-470

                StockList.Add(StockDetail)
                'dblTotalCost += StockDetail.OutAmount
                'End If

                'End If

                If Me.cmbIssuence.Value > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Select IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0) As Quantity FROM DispatchDetailTable  WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    Dim DA As New OleDbDataAdapter
                    Dim DADt As New DataTable
                    DA.SelectCommand = objCommand
                    DA.Fill(DADt)
                    If DADt.Rows.Count > 0 Then
                        If Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) > Val(DADt.Rows(0).Item(0).ToString) Then
                            If msg_Confirm("Product '" & dtGrd.Rows(i).Item(grdEnm.Item).ToString & "' Quantity is higher than allocated quantity. Do you want to proceed?") = False Then
                                dtGrd.Rows(i).Item(grdEnm.TotalQty) = Val(DADt.Rows(0).Item(0).ToString)
                                dtGrd.Rows(i).Item(grdEnm.Qty) = Val(DADt.Rows(0).Item(0).ToString)
                                'dtGrd.AcceptChanges()
                            End If
                        End If
                    End If

                    objCommand.CommandText = ""
                    objCommand.CommandText = "UPDATE  DispatchDetailTable " _
                                                  & " SET ReturnedQty = isnull(ReturnedQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", ReturnedTotalQty = isnull(ReturnedTotalQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & " " _
                                                  & " WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    objCommand.ExecuteNonQuery()
                End If

                '' on 19-06-20147
                If Me.cmbTicketNo.SelectedValue > 0 Then
                    'objCommand.CommandText = ""
                    'objCommand.CommandText = "Select IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0) As Quantity FROM DispatchDetailTable  WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    'Dim DA As New OleDbDataAdapter
                    'Dim DADt As New DataTable
                    'DA.SelectCommand = objCommand
                    'DA.Fill(DADt)
                    'If DADt.Rows.Count > 0 Then
                    '    If Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) > Val(DADt.Rows(0).Item(0).ToString) Then
                    '        If msg_Confirm("Product '" & dtGrd.Rows(i).Item(grdEnm.Item).ToString & "' Quantity is higher than allocated quantity. Do you want to proceed?") = False Then
                    '            dtGrd.Rows(i).Item(grdEnm.TotalQty) = Val(DADt.Rows(0).Item(0).ToString)
                    '            dtGrd.Rows(i).Item(grdEnm.Qty) = Val(DADt.Rows(0).Item(0).ToString)
                    '            'dtGrd.AcceptChanges()
                    '        End If
                    '    End If
                    'End If

                    objCommand.CommandText = ""
                    objCommand.CommandText = "UPDATE  DispatchDetailTable " _
                                                  & " SET ReturnedQty = isnull(ReturnedQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", ReturnedTotalQty = isnull(ReturnedTotalQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & " " _
                                                  & " WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (EstimationId =" & Val(dtGrd.Rows(i).Item("EstimationId").ToString) & ") And (SubDepartmentID =" & Val(dtGrd.Rows(i).Item("DepartmentId").ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    objCommand.ExecuteNonQuery()
                End If
                '' End on 19-06-2017


                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID) values( " _
                '                        & " ident_current('ReturnDispatchMasterTable')," & Val(grd.GetRows(i).Cells(8).Value) & ",N'" & (grd.GetRows(i).Cells(3).Value) & "'," & Val(grd.GetRows(i).Cells(4).Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(3).Value = "Loose", Val(grd.GetRows(i).Cells(4).Value), (Val(grd.GetRows(i).Cells(4).Value) * Val(grd.GetRows(i).Cells(9).Value))) & ", " & Val(grd.GetRows(i).Cells(5).Value) & ", " & Val(grd.GetRows(i).Cells(9).Value) & " , " & Val(grd.GetRows(i).Cells(10).Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "'," & grd.GetRows(i).Cells("BatchID").Value & "," & Val(grd.GetRows(i).Cells("LocationID").Value) & ")"

                'objCommand.ExecuteNonQuery()
                'Before against task:2435
                'objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId, Pack_Desc) values( " _
                '                                       & " ident_current('ReturnDispatchMasterTable'), " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ",N'" & (dtGrd.Rows(i).Item(grdEnm.Unit).ToString) & "'," & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", " _
                '                                       & " " & IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString), (Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString))) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString) & "  , " & Val(dtGrd.Rows(i).Item(grdEnm.CurrentPrice).ToString) & ",N'" & dtGrd.Rows(i).Item(grdEnm.BatchNo).ToString & "'," & dtGrd.Rows(i).Item(grdEnm.BatchId).ToString & "," & dtGrd.Rows(i).Item(grdEnm.LocationId).ToString & ", " & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString) & ", N'" & dtGrd.Rows(i).Item(grdEnm.Pack_Desc).ToString.Replace("'", "''") & "')"

                'Task:2435 Added Column CostPrice
                'Before against task:2506
                'objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId, Pack_Desc, CostPrice) values( " _
                '                                      & " ident_current('ReturnDispatchMasterTable'), " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ",N'" & (dtGrd.Rows(i).Item(grdEnm.Unit).ToString) & "'," & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", " _
                '                                      & " " & IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString), (Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString))) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString) & "  , " & Val(dtGrd.Rows(i).Item(grdEnm.CurrentPrice).ToString) & ",N'" & dtGrd.Rows(i).Item(grdEnm.BatchNo).ToString & "'," & dtGrd.Rows(i).Item(grdEnm.BatchId).ToString & "," & dtGrd.Rows(i).Item(grdEnm.LocationId).ToString & ", " & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString) & ", N'" & dtGrd.Rows(i).Item(grdEnm.Pack_Desc).ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ")"
                'End Task:2435
                'Task:2506 Added Field PlanUnit and PlanQty
                ''TASK: TFS1133 gotton saved estimationid and departmentid for estimation wise store issuance. 
                objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId, Pack_Desc, CostPrice, PlanUnit, PlanQty, Lot_No,Rack_No,Comments, DispatchDetailId, EstimationId, DepartmentId, TicketId) values( " _
                                                    & " ident_current('ReturnDispatchMasterTable'), " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ",N'" & (dtGrd.Rows(i).Item(grdEnm.Unit).ToString) & "'," & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", " _
                                                    & " " & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString) & "  , " & Val(dtGrd.Rows(i).Item(grdEnm.CurrentPrice).ToString) & ",N'" & dtGrd.Rows(i).Item(grdEnm.BatchNo).ToString & "'," & dtGrd.Rows(i).Item(grdEnm.BatchId).ToString & "," & dtGrd.Rows(i).Item(grdEnm.LocationId).ToString & ", " & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString) & ", N'" & dtGrd.Rows(i).Item(grdEnm.Pack_Desc).ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ", N'" & dtGrd.Rows(i).Item("PlanUnit").ToString.Replace("'", "''") & "', " & Val(dtGrd.Rows(i).Item("PlanQty").ToString) & ",N'" & dtGrd.Rows(i).Item("LotNo").ToString.Replace("'", "''") & "',N'" & dtGrd.Rows(i).Item("Rack_No").ToString.Replace("'", "''") & "',N'" & dtGrd.Rows(i).Item("Comments").ToString.Replace("'", "''") & "', " & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ", " & Val(dtGrd.Rows(i).Item("EstimationId").ToString) & ", " & Val(dtGrd.Rows(i).Item("DepartmentId").ToString) & ", " & Val(dtGrd.Rows(i).Item("TicketId").ToString) & ")" ''TASK-408 added TotalQty instead of Qty for Qty assignment value
                'End Task:2506
                objCommand.ExecuteNonQuery()

                'Val(grd.Rows(i).Cells(5).Value)
                'Before against task:2412
                'If StoreIssuanceDependonProductionPlan = True Then
                'Task:2412 Change Validation
                If Me.cmbPlan.SelectedIndex > 0 Then
                    'End Task:2412
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update PlanCostSheetDetailTable Set IssuedQty=IssuedQty-" & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & " WHERE PlanCostSheetDetailTable.PlanId=" & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & " AND PlanCostSheetDetailTable.ArticleDefId=" & dtGrd.Rows(i).Item(grdEnm.ItemId).ToString & " AND PlanCostSheetDetailTable.ArticleMasterId=" & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString) ''TASK-408 added TotalQty instead Qty
                    objCommand.ExecuteNonQuery()
                End If

                If Me.chkIssued.Checked = True Then 'Task:M21 If Issued Checked Then Update Stock, Update Voucher.
                    If flgCylinderVoucher = True Then
                        '' Customer Voucher Debit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, credit_amount, debit_amount,comments) Values(Ident_Current('tblVoucher'), 1," & Me.cmbVendor.Value & ", " & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " '' TASK-408 added TotalQty instead of Qty
                        objCommand.ExecuteNonQuery()

                        '' Security Deposit Voucher Credit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) Values(Ident_Current('tblVoucher'), 1," & Me.cmbSecurity.Value & ", " & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " '' TASK-408 added TotalQty instead of Qty
                        objCommand.ExecuteNonQuery()

                        '' Fixed Asset Voucher Debit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, credit_amount,debit_amount, comments) Values(Ident_Current('tblVoucher'), 1," & Me.cmbFixedAssets.Value & ", " & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " '' TASK-408 added TotalQty instead of Qty
                        objCommand.ExecuteNonQuery()

                        '' Cylinder Stock Voucher Credit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id,  debit_amount, credit_amount,comments) Values(Ident_Current('tblVoucher'), 1," & CylinderStockAccountId & ", " & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " '' TASK-408 added TotalQty instead of Qty
                        objCommand.ExecuteNonQuery()

                    End If


                    If flgStoreIssuenceVoucher = True Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
                        If flgAvrRate = True Then

                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount, comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & StockDetail.InAmount & ", N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")', " & Me.cmbPo.SelectedValue & " )"
                            objCommand.ExecuteNonQuery()


                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,  credit_amount,debit_amount, comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & StockDetail.InAmount & " ,0 , N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")',  " & Me.cmbPo.SelectedValue & ")"
                            objCommand.ExecuteNonQuery()

                        Else
                            'objCommand.CommandText = ""
                            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                            '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & StockDetail.OutAmount & ", N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")', " & Me.cmbPo.SelectedValue & " )"
                            'objCommand.ExecuteNonQuery()


                            'objCommand.CommandText = ""
                            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                            '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & StockDetail.OutAmount & " ,0 , N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")',  " & Me.cmbPo.SelectedValue & ")"
                            'objCommand.ExecuteNonQuery()
                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,credit_amount, debit_amount,  comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & StockDetail.InAmount & ", N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")', " & Me.cmbPo.SelectedValue & " )"
                            objCommand.ExecuteNonQuery()


                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount, comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & StockDetail.InAmount & " ,0 , N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")',  " & Me.cmbPo.SelectedValue & ")"
                            objCommand.ExecuteNonQuery()

                        End If
                    End If
                End If
                'Task:M21
            Next

            'If flgStoreIssuenceVoucher = True Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
            '    If flgAvrRate = True Then
            '        'objCommand.CommandText = ""
            '        'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '        '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '        '                           & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '        '                           & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
            '        '                           & " SELECT @@IDENTITY"
            '        'lngVoucherMasterId = objCommand.ExecuteScalar

            '        'objCommand.CommandText = ""
            '        'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            '        'objCommand.ExecuteNonQuery()

            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & dblTotalCost & ", 'Store Issuence Ref: " & Me.txtPONo.Text & "', " & Me.cmbPo.SelectedValue & " )"
            '        objCommand.ExecuteNonQuery()


            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & dblTotalCost & " ,0 , 'Store Issuence Ref: " & Me.txtPONo.Text & "',  " & Me.cmbPo.SelectedValue & ")"
            '        objCommand.ExecuteNonQuery()

            '    Else
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", 'Store Issuence Ref: " & Me.txtPONo.Text & "', " & Me.cmbPo.SelectedValue & " )"
            '        objCommand.ExecuteNonQuery()


            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & " ,0 , 'Store Issuence Ref: " & Me.txtPONo.Text & "',  " & Me.cmbPo.SelectedValue & ")"
            '        objCommand.ExecuteNonQuery()

            '    End If
            'End If



            'Code By Imran Ali 10-July-2013
            'Request Against 732
            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            'Task:24112 Change Validation
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT ISNULL(SUM(ISNULL(PlanQty, 0) + ISNULL(IssuedQty, 0)), 0) AS Qty FROM  dbo.PlanCostSheetDetailTable WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ""
                Dim objDa As New OleDbDataAdapter
                Dim objDt As New DataTable
                objDa.SelectCommand = objCommand
                objDa.Fill(objDt)
                If objDt IsNot Nothing Then
                    If objDt.Rows.Count > 0 Then
                        If objDt.Rows(0).Item(0) <> 0 Then
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Open.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            objCommand.ExecuteNonQuery()
                        End If
                    Else
                        objCommand.CommandText = ""
                        objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                        objCommand.ExecuteNonQuery()
                    End If
                End If
            End If
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '                           & " VALUES(" & gobjLocationId& ", 1,  6 , N'" & strvoucherNo & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', '" _
            '                           & "NULL" & "', N'" & Nothing & "', 0,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
            '                           & " SELECT @@IDENTITY"


            'lngVoucherMasterId = objCommand.ExecuteScalar

            ''***********************
            ''Deleting Detail
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId

            'objCommand.Transaction = trans

            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Debit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & Val(Me.txtAmount.Text) & ", 0)"

            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Credit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAmount.Text) & ")"
            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()
            If IsValidate() = True Then
                Call New StockDAL().Add(StockMaster, trans)
            End If

            trans.Commit()
            Save = True
            'Task:M21 Stock Update Issued Rights Based 
            'If Me.chkIssued.Checked = True Then
            'Call Save1() 'Upgrading Stock ...
            'End Task:M21
            setEditMode = False
            setVoucherNo = Me.txtPONo.Text
            'getVoucher_Id = Me.txtReceivingID.Text
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            setVoucherdate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
            dtDataEmail = CType(Me.grd.DataSource, DataTable)
            dblTotalCost = 0
            Try



                ''insert Activity Log
                SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.StoreIssuence, Me.txtPONo.Text.Trim)

            Catch ex As Exception

            End Try
            'insertvoucher()

        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try




    End Function

    Sub InsertVoucher()

        Try
            'SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt"), "", Nothing, GetConfigValue("ReturnDispatchCreditAccount"), Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), Val(Me.txtAmount.Text), Val(Me.txtAmount.Text), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        'If txtPONo.Text = "" Then
        '    msg_Error("You must enter PO No.")
        '    txtPONo.Focus() : FormValidate = False : Exit Function
        'End If



        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If

        Return True

    End Function

    Sub EditRecord()
        Try


            'If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            'If Me.grd.RowCount > 0 Then
            '    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            'End If

            'txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            'dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            'txtReceivingID.Text = grdSaved.CurrentRow.Cells("ReturnDispatchId").Value
            ''TODO. ----
            'cmbVendor.Value = grdSaved.CurrentRow.Cells(3).Value

            'txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            'txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            ''Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            'Call DisplayDetail(grdSaved.CurrentRow.Cells("ReturnDispatchId").Value)
            'GetTotal()
            'Me.SaveToolStripButton.Text = "&Update"
            'Me.cmbPo.Enabled = False



            IsEditMode = True
            'FillCombo("Vendor")
            'FillCombo("SO")

            'If StoreIssuanceDependonProductionPlan = True Then
            '    FillCombo("Plan")
            'End If
            '' 04-Jul-2014 TASK:2716 Imran Ali Selected Store Issuance Update 
            If Me.btnSelectedIssuenceUpdate.Enabled = False Then
                If Not Me.grdSaved.RowCount > 0 Then Exit Sub
                If Me.grd.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                End If
            End If
            'End Task:2716
            'Me.FillCombo("SOComplete") 'R933 Commented Plan Data
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value
            Me.GetSecurityRights()
            'Task 1592
            If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = dtpPODate.Value.AddMonths(3)
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            Else
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            End If
            txtReceivingID.Text = grdSaved.CurrentRow.Cells(6).Value
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value 'cmbVendor.FindStringExact((grdSaved.CurrentRow.Cells(3).Value))
            ''R933  Validate Vendor Data 
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If
            cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PurchaseOrderid").Value
            txtRemarks.Text = grdSaved.CurrentRow.Cells(8).Value & ""
            txtPaid.Text = grdSaved.CurrentRow.Cells(9).Value & ""
            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("ArticleDefId").Value) Then
            '    Me.cmbCostSheetItems.Value = Me.grdSaved.GetRow.Cells("ArticleDefId").Value
            'Else
            '    Me.cmbCostSheetItems.Rows(0).Activate()
            'End If
            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("LocationId").Value) Then
            '    Me.cmbCostSheetLocation.SelectedValue = Me.grdSaved.GetRow.Cells("LocationId").Value
            'Else
            '    Me.cmbCostSheetLocation.SelectedIndex = 0
            'End If
            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("Qty").Value) Then
            '    Me.txtCostSheetQty.Text = Me.grdSaved.GetRow.Cells("Qty").Value
            'Else
            '    Me.txtCostSheetQty.Text = 0
            'End If
            Me.txtPO_No.Text = Me.grdSaved.GetRow.Cells("PO No").Text.ToString
            Me.cmbPlan.Enabled = False


            Dim dt As DataTable = CType(Me.cmbIssuence.DataSource, DataTable)
            Dim drFound() As DataRow
            Dim dr As DataRow
            drFound = dt.Select("DispatchId=" & Val(Me.grdSaved.GetRow.Cells("DispatchId").Value.ToString) & "")
            If drFound.Length = 0 Then
                dr = dt.NewRow
                dr(0) = Val(Me.grdSaved.GetRow.Cells("DispatchId").Value.ToString)
                dr(1) = Me.grdSaved.GetRow.Cells("DispatchNo").Value.ToString
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If

            Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdEnm.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.cmbPlan.SelectedValue = Me.grdSaved.GetRow.Cells("PlanId").Value
            Me.cmbTicketNo.SelectedValue = Me.grdSaved.GetRow.Cells("TicketID").Value
            Me.cmbIssuence.Value = Val(Me.grdSaved.GetRow.Cells("DispatchId").Value.ToString)
            Me.cmbTicketIssuance.SelectedValue = Val(Me.grdSaved.GetRow.Cells("DispatchId").Value.ToString)
            Me.ComboBox1.SelectedValue = Me.grdSaved.GetRow.Cells("EmpId").Value
            Me.cmbFixedAssets.Value = Me.grdSaved.GetRow.Cells("FixedAssetAccountId").Value
            Me.cmbSecurity.Value = Me.grdSaved.GetRow.Cells("CylinderSecurityAccountId").Value
            Me.cmbDepartment.SelectedValue = Me.grdSaved.GetRow.Cells("DepartmentId").Value 'Task:2689 Get DepartmentId By User
            If blnCGAccountOnStoreIssuance = True Then
                Me.cmbCGAccount.Value = Me.grdSaved.GetRow.Cells("StoreIssuanceAccountId").Value
            Else
                Me.cmbCGAccount.Value = Convert.ToInt32(getConfigValueByType("StoreIssuenceAccount").ToString)
            End If
            'Me.cmbCostSheetLocation.SelectedValue = Me.grdSaved.GetRow.Cells("DeptId").Value
            'GetTotal()
            'If Me.cmbPo.Items.Count <> 0 AndAlso Me.cmbPo.SelectedValue > 0 Then
            '    Me.cmbPo.Enabled = False
            'Else
            '    Me.cmbPo.Enabled = True
            'End If
            Call DisplayDetail(grdSaved.CurrentRow.Cells(6).Value)
            IsStoreIssuance = False
            Me.btnsave.Text = "&Update"
            'GetSecurityRights()
            Me.chkIssued.Checked = Me.grdSaved.GetRow.Cells("Issued").Value 'Task:M21 Get Issued Value.
            'Comment Against task:2412
            'If StoreIssuanceDependonProductionPlan = False Then
            '    Me.Button1.Enabled = False
            '    Me.cmbCostSheetItems.Enabled = False
            'Else
            '    Me.Button1.Enabled = True
            '    Me.cmbCostSheetItems.Enabled = True
            'End If
            'End Task:2412
            If getConfigValueByType("StoreIssuenceWithProduction").ToString = "True" Then
                If Microsoft.VisualBasic.Left(Me.txtPONo.Text, 2) = "I1" Then
                    btnsave.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnsave.Enabled = True
                    btnDelete.Enabled = True
                End If
            End If
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString
            '//Start TASK # 1592
            '24-OCT-2017: Ayesha Rehman: If user dont have update rights then btnsave should not be enable true
            If btnsave.Enabled = True Then
                If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                    Me.btnsave.Enabled = False
                    ErrorProvider1.SetError(Me.Label2, "Future Date can not be edit")
                    ErrorProvider1.BlinkRate = 1000
                    ErrorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                Else
                    Me.btnsave.Enabled = True
                    ErrorProvider1.Clear()
                End If
            End If
            'End Task # 1592
            If flgDateLock = True Then
                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpPODate.Enabled = False
                Else
                    Me.dtpPODate.Enabled = True
                End If
            Else
                Me.dtpPODate.Enabled = True
            End If
            If Not Mode = "Normal" Then Me.txtBarcodescan.Focus()
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnDelete.Visible = True
            Me.btnPrint.Visible = True
            '''''''''''''''''''''''''''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayPODetail(ByVal ReceivingID As Integer, Optional ByVal Condition As String = "")
        Dim str As String = String.Empty
        'Dim i As Integer
        If Condition = String.Empty Then
            'Before against task:2435
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float,((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '      & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, 0 as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Article.MasterId " _
            '      & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            'Task:2435 Added Column CostPrice
            'Before againt task:2506
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '   & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float,((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '   & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, 0 as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, 0 as CostPrice  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Article.MasterId " _
            '   & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            'End Task:2435
            'Task:2506 Added Column PlanUnit and PlanQty
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float,((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '  & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, 0 as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, 0 as CostPrice,  '' as PlanUnit, 0 as PlanQty  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Article.MasterId " _
            '  & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            'End Task:2506
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '             & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float,((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '             & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, 0 as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, 0 as CostPrice,  '' as PlanUnit, 0 as PlanQty, '' as LotNo, '' as Rack_No, Recv_D.Comments as Comments  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '             & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '             & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Article.MasterId " _
            '             & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
                       & " Convert(float, (Recv_D.Qty * Recv_D.Price)) AS Total, " _
                       & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, 0 as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId,Article_Group.CGSAccountId, 0 as CostPrice,  '' as PlanUnit, '' AS PlanNo, 0 as PlanQty, '' AS TicketNo, 0 as TicketQty,  '' as LotNo, '' as Rack_No, Recv_D.Comments as Comments, IsNull(Recv_D.Qty, 0) As TotalQty, 0 As DispatchDetailId, 0 As CheckQty, 0 As EstimationId, 0 As TicketId, 0 As DepartmentId, '' As Department, 0 AS WIPAccountId   FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
                       & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                       & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Article.MasterId " _
                       & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
        ElseIf Condition = "Plan" Then
            'Before against task:2435
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) AS Qty, Recv_D.Price as Rate, " _
            '                 & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, ((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) * Recv_D.Price)) ELSE Convert(float,(((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0))  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '                 & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.PackQty, Recv_D.Price as CurrentPrice, 0 as BatchID, Recv_D.ArticleMasterId as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId  FROM dbo.PlanCostSheetDetailTable Recv_D INNER JOIN " _
            '                 & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '                 & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleMasterId " _
            '                 & " Where Recv_D.PlanId =" & Me.cmbPlan.SelectedValue & " AND (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) <> 0  AND Recv_D.PlanId in (Select PlanId From PlanDetailTable) "

            'Task:2435 Added Column CostPrice
            'Before against task:2506
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) AS Qty, Recv_D.Price as Rate, " _
            '               & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, ((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) * Recv_D.Price)) ELSE Convert(float,(((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0))  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '               & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.PackQty, Recv_D.Price as CurrentPrice, 0 as BatchID, Recv_D.ArticleMasterId as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, 0 as CostPrice  FROM dbo.PlanCostSheetDetailTable Recv_D INNER JOIN " _
            '               & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '               & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleMasterId " _
            '               & " Where Recv_D.PlanId =" & Me.cmbPlan.SelectedValue & " AND (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) <> 0  AND Recv_D.PlanId in (Select PlanId From PlanDetailTable) "
            'End Task:2435
            'Task:2506 Added Column PlanUnit And PlanQty
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) AS Qty, Recv_D.Price as Rate, " _
            '                         & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, ((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) * Recv_D.Price)) ELSE Convert(float,(((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0))  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '                         & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.PackQty, Recv_D.Price as CurrentPrice, 0 as BatchID, Recv_D.ArticleMasterId as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.ArticleSize as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, 0 as CostPrice, '' as PlanUnit, 0 as PlanQty  FROM dbo.PlanCostSheetDetailTable Recv_D INNER JOIN " _
            '                         & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '                         & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleMasterId " _
            '                         & " Where Recv_D.PlanId =" & Me.cmbPlan.SelectedValue & " AND (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) <> 0  AND Recv_D.PlanId in (Select PlanId From PlanDetailTable) "
            'End Task:2506
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) AS Qty, Recv_D.Price as Rate, " _
            '                                   & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, ((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) * Recv_D.Price)) ELSE Convert(float,(((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0))  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '                                   & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.PackQty, Recv_D.Price as CurrentPrice, 0 as BatchID, Recv_D.ArticleMasterId as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.ArticleSize as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, 0 as CostPrice, '' as PlanUnit, 0 as PlanQty, '' as LotNo, '' as Rack_No, '' as Comments  FROM dbo.PlanCostSheetDetailTable Recv_D INNER JOIN " _
            '                                   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '                                   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleMasterId " _
            '                                   & " Where Recv_D.PlanId =" & Me.cmbPlan.SelectedValue & " AND (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) <> 0  AND Recv_D.PlanId in (Select PlanId From PlanDetailTable) "

            str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item , ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) AS Qty, Recv_D.Price as Rate, " _
                                              & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, ((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) * Recv_D.Price)) ELSE Convert(float,(((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0))  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
                                              & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.PackQty, Recv_D.Price as CurrentPrice, 0 as BatchID, Recv_D.ArticleMasterId as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as ArticleDescriptionMaster, Recv_D.ArticleSize as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId,0 as CostPrice, '' as PlanUnit, '' AS PlanNo, 0 as PlanQty, '' AS TicketNo, 0 as TicketQty, '' as LotNo, '' as Rack_No, '' as Comments, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) AS TotalQty, 0 As DispatchDetailId, 0 As CheckQty, 0 As EstimationId, 0 As TicketId, 0 As DepartmentId, '' As Department, 0 AS WIPAccountId   FROM dbo.PlanCostSheetDetailTable Recv_D INNER JOIN " _
                                              & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
                                              & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleMasterId " _
                                              & " Where Recv_D.PlanId =" & Me.cmbPlan.SelectedValue & " AND (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) <> 0  AND Recv_D.PlanId in (Select PlanId From PlanDetailTable) "


        ElseIf Condition = "Issuence" Then

            '    str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments  FROM dbo.DispatchDetailTable Recv_D LEFT OUTER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where Recv_D.DispatchID =" & ReceivingID & ""
            ''-IsNull(RDD.Sz1, 0)


            str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1, 0)-IsNull(Recv_D.ReturnedQty, 0)) AS Qty, Recv_D.Price as Rate, " _
        & " Convert(float, (Recv_D.Qty * Recv_D.Price)) AS Total, " _
        & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, " & IIf(CostSheetType = "Error" Or CostSheetType = "Standard Cost Sheet", " ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster]", "''") & ", Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, PlanMasterTable.PlanNo, Isnull(Recv_D.PlanQty,0) as PlanQty, PlanTicketsMaster.TicketNo, Isnull(Recv_D.TicketQty,0) as TicketQty, Recv_D.Lot_No as LotNo, Recv_D.Rack_No, Recv_D.Comments, (IsNull(Recv_D.Qty, 0)-IsNull(Recv_D.ReturnedTotalQty, 0)) As TotalQty, Recv_D.DispatchDetailId,  (IsNull(Recv_D.Qty, 0)-IsNull(Recv_D.ReturnedTotalQty, 0)) As CheckQty,  IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.TicketId, 0) As TicketId, IsNull(Recv_D.SubDepartmentID, 0) As DepartmentId, tblproSteps.prod_step As Department, ISNULL(Recv_D.WIPAccountId, 0) AS WIPAccountId FROM dbo.DispatchDetailTable Recv_D LEFT OUTER JOIN " _
        & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
        & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
        & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID LEFT OUTER JOIN tblproSteps On Recv_D.SubDepartmentID = tblproSteps.ProdStep_Id " _
        & " LEFT OUTER JOIN PlanTicketsMaster ON Recv_D.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
        & " LEFT OUTER JOIN PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanId " _
        & " Where Recv_D.DispatchID =" & ReceivingID & " And IsNull(Recv_D.Qty, 0) > IsNull(Recv_D.ReturnedTotalQty, 0)"

        End If
        'Dim objCommand As New OleDbCommand
        'Dim objCon As OleDbConnection
        'Dim objDataAdapter As New OleDbDataAdapter
        'Dim objDataSet As New DataSet

        'objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        'If objCon.State = ConnectionState.Open Then objCon.Close()

        'objCon.Open()
        'objCommand.Connection = objCon
        'objCommand.CommandType = CommandType.Text


        'objCommand.CommandText = str

        'objDataAdapter.SelectCommand = objCommand
        'objDataAdapter.Fill(objDataSet)

        'grd.Rows.Clear()
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        'Next
        Dim DtDisplayDetail As DataTable = GetDataTable(str)
        DtDisplayDetail.AcceptChanges()
        DtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((PackQty * Qty) * Rate), Qty * Rate)"
        'DtDisplayDetail.Columns("Total").Expression = "(TotalQty*Qty)"
        Me.grd.DataSource = Nothing
        grd.DataSource = DtDisplayDetail
        ApplyGridSettings()
        FillCombo("grdLocation")
        'If IsFormOpen = True Then
        '    If StoreIssuanceDependonProductionPlan = True Then
        '        Me.grd.RootTable.Groups.Remove(grpArticleDescriptionMaster)
        '        Me.grd.RootTable.Groups.Add(grpArticleDescriptionMaster)
        '    End If
        'End If
    End Sub

    Private Sub DisplayDetail(ByVal ReceivingID As Integer)
        Dim str As String = String.Empty
        'Dim i As Integer
        'Before against task:2435
        'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
        '      & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId  FROM dbo.ReturnDispatchDetailTable Recv_D LEFT OUTER JOIN " _
        '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
        '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
        '      & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
        '      & " Where Recv_D.ReturnDispatchID =" & ReceivingID & ""

        'Task:2435 Added Column CostPrice
        'Before against task:2506
        'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
        '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
        '     & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice  FROM dbo.ReturnDispatchDetailTable Recv_D LEFT OUTER JOIN " _
        '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
        '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
        '     & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
        '     & " Where Recv_D.ReturnDispatchID =" & ReceivingID & ""
        'End Task:2435
        'TASK:2506 Added Column PlanUnit And PlanQty
        'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
        '      & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty  FROM dbo.ReturnDispatchDetailTable Recv_D LEFT OUTER JOIN " _
        '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
        '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
        '      & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
        '      & " Where Recv_D.ReturnDispatchID =" & ReceivingID & ""
        'End Task:2506

        'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
        ' & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
        ' & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, Isnull(Recv_D.PlanQty,0) as PlanQty,Recv_D.Lot_No as LotNo, Recv_D.Rack_No,Recv_D.Comments  FROM dbo.ReturnDispatchDetailTable Recv_D LEFT OUTER JOIN " _
        ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
        ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
        ' & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
        ' & " Where Recv_D.ReturnDispatchID =" & ReceivingID & ""

        str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, Recv_D.BatchNo , Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
       & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
       & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.ArticleDefMasterId,0) as ArticleDefMasterId, " & IIf(CostSheetType = "Error" Or CostSheetType = "Standard Cost Sheet", " ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster]", "''") & ", Recv_D.Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.PlanUnit, PlanMasterTable.PlanNo, Isnull(Recv_D.PlanQty,0) as PlanQty, PlanTicketsMaster.BatchNo As TicketNo, ISNULL(DispatchDetailTable.TicketQty, 0) as TicketQty, Recv_D.Lot_No as LotNo, Recv_D.Rack_No, Recv_D.Comments, IsNull(Recv_D.Qty, 0) As TotalQty, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Recv_D.Qty, 0) As CheckQty, IsNull(Recv_D.EstimationId, 0) As EstimationId, IsNull(Recv_D.TicketId, 0) As TicketId, IsNull(Recv_D.DepartmentId, 0) As DepartmentId, tblproSteps.prod_step As Department, ISNULL(DispatchDetailTable.WIPAccountId, 0) AS WIPAccountId FROM dbo.ReturnDispatchDetailTable Recv_D LEFT OUTER JOIN " _
       & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
       & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleDefMasterId " _
       & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
       & " LEFT OUTER JOIN DispatchDetailTable ON Recv_D.DispatchDetailId = DispatchDetailTable.DispatchDetailId " _
       & " LEFT OUTER JOIN tblproSteps On Recv_D.DepartmentId = tblproSteps.ProdStep_Id " _
       & " LEFT OUTER JOIN PlanTicketsMaster ON Recv_D.TicketId = PlanTicketsMaster.PlanTicketsMasterID LEFT OUTER JOIN PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanId " _
       & " Where Recv_D.ReturnDispatchID =" & ReceivingID & ""

        'Dim objCommand As New OleDbCommand
        'Dim objCon As OleDbConnection
        'Dim objDataAdapter As New OleDbDataAdapter
        'Dim objDataSet As New DataSet

        'objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        'If objCon.State = ConnectionState.Open Then objCon.Close()

        'objCon.Open()
        'objCommand.Connection = objCon
        'objCommand.CommandType = CommandType.Text


        'objCommand.CommandText = str

        'objDataAdapter.SelectCommand = objCommand
        'objDataAdapter.Fill(objDataSet)

        'grd.Rows.Clear()
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationID"))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)
        'Next
        Dim DtDisplayDetail As DataTable = GetDataTable(str)
        DtDisplayDetail.AcceptChanges()
        ''DtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((PackQty * Qty) * Rate), (Qty * Rate))"
        DtDisplayDetail.Columns("Total").Expression = "(TotalQty*Rate)" ''TASK-408
        Me.grd.DataSource = Nothing
        grd.DataSource = DtDisplayDetail
        ApplyGridSettings()
        FillCombo("grdLocation")
        'If IsFormOpen = True Then
        '    If StoreIssuanceDependonProductionPlan = False Then
        '        Me.grd.RootTable.Groups.Remove(grpArticleDescriptionMaster)
        '        Me.grd.RootTable.Groups.Add(grpArticleDescriptionMaster)
        '    End If
        'End If

    End Sub
    Private Sub DisplayAllocationDetail(ByVal TicketID As Integer)
        Dim str As String = String.Empty

        str = "SELECT 1 As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, '' As BatchNo , '' AS unit, IsNull(Recv_D.Quantity, 0) AS Qty, 0 as Rate, 0 AS Total, " _
       & " Article.ArticleGroupId as CategoryId, Recv_D.ProductID as ItemId, 0 as PackQty, 0 As CurrentPrice, 0 As BatchID, Isnull(Recv_D.ProductMasterID,0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], '' As Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, 0 as CostPrice, '' PlanUnit, 0 as PlanQty, '' as LotNo, '' As Rack_No, Recv_D.Remarks As Comments, IsNull(Recv_D.Quantity, 0) As TotalQty, 0 As DispatchDetailId FROM AllocationDetail Recv_D INNER JOIN AllocationMaster ON Recv_D.Master_Allocation_ID = AllocationMaster.Master_Allocation_ID LEFT OUTER JOIN " _
       & " dbo.ArticleDefTable Article ON Recv_D.ProductID = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
       & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ProductMasterID " _
       & " Where AllocationMaster.TicketID =" & TicketID & ""
        Dim DtDisplayDetail As DataTable = GetDataTable(str)
        DtDisplayDetail.AcceptChanges()
        ''DtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((PackQty * Qty) * Rate), (Qty * Rate))"
        'DtDisplayDetail.Columns("Total").Expression = "(TotalQty*Rate)" ''TASK-408
        Me.grd.DataSource = Nothing
        grd.DataSource = DtDisplayDetail
        ApplyGridSettings()
        FillCombo("grdLocation")
    End Sub
    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
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
        If blnCGAccountOnStoreIssuance = True Then
            AccountId2 = Me.cmbCGAccount.Value
        Else
            AccountId2 = getConfigValueByType("StoreIssuenceAccount")
        End If
        Dim StoreIssuanceId As Integer = 0
        If Not Me.cmbTicketIssuance.SelectedValue Is Nothing AndAlso Me.cmbTicketIssuance.SelectedValue > 0 Then
            StoreIssuanceId = Me.cmbTicketIssuance.SelectedValue
        ElseIf Not Me.cmbIssuence.ActiveRow Is Nothing Then
            StoreIssuanceId = cmbIssuence.Value
        End If
        Dim dtWIP As DataTable = CType(Me.grd.DataSource, DataTable)
        dtWIP.AcceptChanges()
        For i = 0 To dtWIP.Rows.Count - 1
            If Val(dtWIP.Rows(i).Item("WIPAccountId").ToString) Then
                IsWIPAccount = True
                Exit For
            End If
        Next

        If IsWIPAccount = False Then
            If flgStoreIssuenceVoucher = True Then
                If AccountId <= 0 Then
                    ShowErrorMessage("Purchase account is not map.")
                    Me.dtpPODate.Focus()
                    Return False
                End If
                If AccountId2 <= 0 Then
                    ShowErrorMessage("Cost of good account is not map.")
                    Me.dtpPODate.Focus()
                    Return False
                End If
            ElseIf flgCylinderVoucher = True Then
                If CylinderStockAccountId <= 0 Then
                    ShowErrorMessage("Cylinder stock account is not map.")
                    Me.dtpPODate.Focus()
                    Return False
                End If
            End If
        End If
        objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Me.grd.Update()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()

            objCommand.CommandText = ""
            'Before against request no. 934
            'objCommand.CommandText = "Update ReturnDispatchMasterTable set ReturnDispatchNo =N'" & txtPONo.Text & "',ReturnDispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            '& " ReturnDispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReturnDispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text & "',UserName=N'" & LoginUserName & "', PONo=N'" & Me.txtPO_No.Text & "', PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", EmpId=" & Me.ComboBox1.SelectedValue & ", FixedAssetAccountId=" & Me.cmbFixedAssets.Value & ",  CylinderSecurityAccountId=" & Me.cmbSecurity.Value & " Where ReturnDispatchID= " & txtReceivingID.Text & " "
            'ReqId-934 Resolve Comma Error
            'Before against task:M21
            'objCommand.CommandText = "Update ReturnDispatchMasterTable set ReturnDispatchNo =N'" & txtPONo.Text & "',ReturnDispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            '& " ReturnDispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReturnDispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', PONo=N'" & Me.txtPO_No.Text.Replace("'", "''") & "', PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", EmpId=" & Me.ComboBox1.SelectedValue & ", FixedAssetAccountId=" & Me.cmbFixedAssets.Value & ",  CylinderSecurityAccountId=" & Me.cmbSecurity.Value & " Where ReturnDispatchID= " & txtReceivingID.Text & " "
            'Task:M21 Added Column Issued


            'Before against task:2689
            'objCommand.CommandText = "Update ReturnDispatchMasterTable set ReturnDispatchNo =N'" & txtPONo.Text & "',ReturnDispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            '  & " ReturnDispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReturnDispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', PONo=N'" & Me.txtPO_No.Text.Replace("'", "''") & "', PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", EmpId=" & Me.ComboBox1.SelectedValue & ", FixedAssetAccountId=" & Me.cmbFixedAssets.Value & ",  CylinderSecurityAccountId=" & Me.cmbSecurity.Value & ", Issued=" & IIf(Me.chkIssued.Checked = True, 1, 0) & " Where ReturnDispatchID= " & txtReceivingID.Text & " "
            'End Task:M21
            'Task:2689 Added Field DepartmentId
            'objCommand.CommandText = "Update ReturnDispatchMasterTable set ReturnDispatchNo =N'" & txtPONo.Text & "',ReturnDispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            '& " ReturnDispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReturnDispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', PONo=N'" & Me.txtPO_No.Text.Replace("'", "''") & "', PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", EmpId=" & Me.ComboBox1.SelectedValue & ", FixedAssetAccountId=" & Me.cmbFixedAssets.Value & ",  CylinderSecurityAccountId=" & Me.cmbSecurity.Value & ", Issued=" & IIf(Me.chkIssued.Checked = True, 1, 0) & ", DepartmentId=" & IIf(Me.cmbDepartment.SelectedIndex = -1, "NULL", Me.cmbDepartment.SelectedValue) & " Where ReturnDispatchID= " & txtReceivingID.Text & " "
            'End Task:2689
            'objCommand.CommandText = "Update ReturnDispatchMasterTable set ReturnDispatchNo =N'" & txtPONo.Text & "',ReturnDispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            '           & " ReturnDispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReturnDispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', PONo=N'" & Me.txtPO_No.Text.Replace("'", "''") & "', PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", EmpId=" & Me.ComboBox1.SelectedValue & ", FixedAssetAccountId=" & Me.cmbFixedAssets.Value & ",  CylinderSecurityAccountId=" & Me.cmbSecurity.Value & ", Issued=" & IIf(Me.chkIssued.Checked = True, 1, 0) & ", DepartmentId=" & IIf(Me.cmbDepartment.SelectedIndex = -1, "NULL", Me.cmbDepartment.SelectedValue) & ",StoreIssuanceAccountId=" & AccountId2 & " Where ReturnDispatchID= " & txtReceivingID.Text & " "
            objCommand.CommandText = "Update ReturnDispatchMasterTable set ReturnDispatchNo =N'" & txtPONo.Text & "',ReturnDispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
                                 & " ReturnDispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReturnDispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', PONo=N'" & Me.txtPO_No.Text.Replace("'", "''") & "', PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", EmpId=" & Me.ComboBox1.SelectedValue & ", FixedAssetAccountId=" & Me.cmbFixedAssets.Value & ",  CylinderSecurityAccountId=" & Me.cmbSecurity.Value & ", Issued=" & IIf(Me.chkIssued.Checked = True, 1, 0) & ", DepartmentId=" & IIf(Me.cmbDepartment.SelectedIndex = -1, "NULL", Me.cmbDepartment.SelectedValue) & ",StoreIssuanceAccountId=" & AccountId2 & ", DispatchId= " & StoreIssuanceId & ", TicketID = " & IIf(Me.cmbTicketNo.SelectedIndex = -1, 0, Me.cmbTicketNo.SelectedValue) & " Where ReturnDispatchID= " & txtReceivingID.Text & " "

            objCommand.ExecuteNonQuery()

            If Me.chkIssued.Checked = True Then 'Task:M21 If Issued Checked Then Update Voucher
                If (flgStoreIssuenceVoucher = True Or flgCylinderVoucher = True) Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
                    If lngVoucherMasterId > 0 Then
                        objCommand.CommandText = ""
                        'Before against task:M101
                        'objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=1 " _
                        '                        & "   where voucher_id=" & lngVoucherMasterId
                        'Task:M101 Added Field Remarks
                        ''TASK TFS1427 added column of Posted_UserName
                        objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=1, Remarks=N'" & Me.txtRemarks.Text.Replace("'", "''") & "', Posted_UserName=N'" & LoginUserName.Replace("'", "''") & "' " _
                                 & "   where voucher_id=" & lngVoucherMasterId
                        objCommand.ExecuteNonQuery()

                        objCommand.CommandText = ""
                        objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
                        objCommand.ExecuteNonQuery()

                    Else

                        objCommand.CommandText = ""
                        'Before against task:M101 
                        'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                        '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
                        '                           & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                        '                           & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
                        '                           & " SELECT @@IDENTITY"
                        'TAsk:M101 Added Field Remarks
                        ''TASK TFS1427 added UserName and Posted_UserName
                        objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                                  & " cheque_no, cheque_date,post,Source,voucher_code,Remarks, UserName, Posted_UserName)" _
                                                  & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                                  & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "',N'" & Me.txtRemarks.Text.Replace("'", "''") & "', N'" & LoginUserName.Replace("'", "''") & "', N'" & LoginUserName.Replace("'", "''") & "') " _
                                                  & " SELECT @@IDENTITY"
                        'End Task:M101
                        lngVoucherMasterId = objCommand.ExecuteScalar
                    End If

                End If
            End If
            'End Task:M21
            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT dbo.ReturnDispatchDetailTable.ArticleDefId, Isnull(ReturnDispatchDetailTable.ArticleDefMasterId,0) as ArticleDefMasterId,  SUM(ISNULL(dbo.ReturnDispatchDetailTable.Qty, 0)) AS Qty  FROM  dbo.ReturnDispatchDetailTable INNER JOIN   dbo.ReturnDispatchMasterTable ON dbo.ReturnDispatchDetailTable.ReturnDispatchId = dbo.ReturnDispatchMasterTable.ReturnDispatchId WHERE ReturnDispatchMasterTable.ReturnDispatchId=" & Val(Me.txtReceivingID.Text) & " AND Isnull(dbo.ReturnDispatchMasterTable.PlanId,0) =" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & "  GROUP BY dbo.ReturnDispatchDetailTable.ArticleDefId, Isnull(ReturnDispatchDetailTable.ArticleDefMasterId,0) "
                Dim objDaData As New OleDbDataAdapter
                Dim objDtData As New DataTable
                objDaData.SelectCommand = objCommand
                objDaData.Fill(objDtData)

                If objDtData IsNot Nothing Then
                    If objDtData.Rows.Count > 0 Then
                        For Each r As DataRow In objDtData.Rows
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Update PlanCostSheetDetailTable Set IssuedQty=IssuedQty+" & Val(r.Item("Qty")) & " WHERE PlanCostSheetDetailTable.PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & " AND PlanCostSheetDetailTable.ArticleDefId=" & r.Item("ArticleDefId") & " AND PlanCostSheetDetailTable.ArticleMasterId=" & r.Item("ArticleDefMasterId")
                            objCommand.ExecuteNonQuery()
                        Next
                    End If
                End If
            End If

            objCommand.CommandText = ""
            objCommand.CommandText = "Update DispatchDetailTable Set ReturnedQty=IsNull(ReturnedQty,0)-IsNull(ReturnDispatchDetail.Qty,0),  ReturnedTotalQty=IsNull(ReturnedTotalQty,0)-IsNull(ReturnDispatchDetail.TotalQty,0) From DispatchDetailTable,(Select DispatchDetailId, ArticleDefId,  SUM(IsNull(Sz1,0)) As Qty, SUM(IsNull(Qty,0)) as TotalQty From ReturnDispatchDetailTable WHERE ReturnDispatchId=" & Val(Me.txtReceivingID.Text) & " AND IsNull(DispatchDetailId,0) <> 0 Group By DispatchDetailId, ArticleDefId) as ReturnDispatchDetail WHERE ReturnDispatchDetail.DispatchDetailId = DispatchDetailTable.DispatchDetailId And ReturnDispatchDetail.ArticleDefId = DispatchDetailTable.ArticleDefId"
            objCommand.ExecuteNonQuery()

            ''Update dispatch detail against ticket transaction.
            objCommand.CommandText = ""
            objCommand.CommandText = "Update DispatchDetailTable Set ReturnedQty=IsNull(ReturnedQty,0)-IsNull(ReturnDispatchDetail.Qty,0),  ReturnedTotalQty=IsNull(ReturnedTotalQty,0)-IsNull(ReturnDispatchDetail.TotalQty,0) From DispatchDetailTable,(Select IsNull(EstimationId, 0) As EstimationId, IsNull(ArticleDefId, 0) As ArticleDefId, IsNull(DepartmentId, 0) As DepartmentId, IsNull(DispatchDetailId, 0) As DispatchDetailId,   SUM(IsNull(Sz1,0)) As Qty, SUM(IsNull(Qty,0)) as TotalQty From ReturnDispatchDetailTable WHERE ReturnDispatchId=" & Val(Me.txtReceivingID.Text) & " AND IsNull(EstimationId,0) <> 0 Group By ArticleDefId, EstimationId, DepartmentId, DispatchDetailId) as ReturnDispatchDetail WHERE ReturnDispatchDetail.EstimationId = DispatchDetailTable.EstimationId And ReturnDispatchDetail.ArticleDefId = DispatchDetailTable.ArticleDefId And ReturnDispatchDetail.DepartmentId = DispatchDetailTable.SubDepartmentID And ReturnDispatchDetail.DispatchDetailId = DispatchDetailTable.DispatchDetailId"
            objCommand.ExecuteNonQuery()
            ''

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from ReturnDispatchDetailTable where ReturnDispatchID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()

            ''Production Planing Delete....
            'objCommand.CommandText = ""
            'objCommand.CommandText = "Delete from CostingDetailTable where ReturnDispatchID = " & txtReceivingID.Text
            'objCommand.ExecuteNonQuery()


            ''Production Planing... 
            'objCommand.CommandText = ""
            'objCommand.CommandText = "Insert into CostingDetailTable (ReturnDispatchId, LocationId, ArticleDefId, Qty, CurrentPrice) Values " _
            '& " ( " & Val(txtReceivingID.Text) & ", " & Me.cmbCostSheetLocation.SelectedValue & ",   " & Me.cmbCostSheetItems.ActiveRow.Cells(0).Value & ", " & Val(Me.txtCostSheetQty.Text) & ", " & Val(Me.cmbCostSheetItems.ActiveRow.Cells("Price").Value) & ")"
            'objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            'For i = 0 To grd.RowCount - 1
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId) values( " _
            '                            & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells(grdEnm.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(grdEnm.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) & ", " _
            '                            & " " & IIf(grd.GetRows(i).Cells(grdEnm.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(grdEnm.Qty).Value), (Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) * Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(grdEnm.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(grdEnm.CurrentPrice).Value) & ",N'" & grd.GetRows(i).Cells(grdEnm.BatchNo).Value & "'," & grd.GetRows(i).Cells(grdEnm.BatchId).Value & "," & grd.GetRows(i).Cells(grdEnm.LocationId).Value & ", " & Val(grd.GetRows(i).Cells(grdEnm.ArticleDefMasterId).Value.ToString) & ")"

            '    objCommand.ExecuteNonQuery()
            '    'Val(grd.Rows(i).Cells(5).Value)

            '    If StoreIssuanceDependonProductionPlan = True Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Update PlanCostSheetDetailTable Set IssuedQty=IssuedQty+" & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value.ToString) & " WHERE PlanCostSheetDetailTable.PlanId=" & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & " AND PlanCostSheetDetailTable.ArticleDefId=" & grd.GetRows(i).Cells(grdEnm.ItemId).Value & " AND PlanCostSheetDetailTable.ArticleMasterId=" & Val(grd.GetRows(i).Cells(grdEnm.ArticleDefMasterId).Value.ToString)
            '        objCommand.ExecuteNonQuery()
            '    End If

            'Next
            StockList = New List(Of StockDetail)
            Dim dblTotalCost As Double = 0D
            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            For i = 0 To dtGrd.Rows.Count - 1

                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString, Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString), Me.grd, Me.txtPONo.Text, trans)
                End If

                Dim CostPrice As Double = 0D
                Dim CrrStock As Double = 0D
                If GLAccountArticleDepartment = True Then
                    AccountId = Val(dtGrd.Rows(i).Item("PurchaseAccountId").ToString)
                    If blnCGAccountOnStoreIssuance = False Then
                        AccountId2 = Val(dtGrd.Rows(i).Item("CGSAccountId").ToString)
                    End If
                End If
                If Val(dtGrd.Rows(i).Item("WIPAccountId").ToString) > 0 Then
                    AccountId2 = Val(dtGrd.Rows(i).Item("WIPAccountId").ToString)
                End If

                'objCommand.CommandText = ""
                'objCommand.CommandText = "Select StockTransDetailId From StockDetailTable Where StockTransId In(Select StockTransId From StockMasterTable WHERE DocNo='" & Me.txtPONo.Text & "')"
                'Dim intCurrentStockTransDetailId As Integer = objCommand.ExecuteScalar

                'Dim dtLastReturnData As DataTable = GetDataTable("Select IsNull(Rate,0) as Rate, IsNull(OutQty,0) as OutQty, IsNull(StockMasterTable.StockTransId,0) as StockTransId,IsNull(StockTransDetailId,0) as StockTransDetailId From StockDetailTable INNER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefId=" & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & " AND LEFT(StockMasterTable.DocNo,1) ='I' AND IsNull(OutQty,0) <> 0 AND  StockTransDetailId <= " & intCurrentStockTransDetailId & " ORDER BY Convert(DateTime,StockMasterTable.DocDate,102),StockDetailTable.StockTransDetailId DESC ", trans)
                'dtLastReturnData.AcceptChanges()
                'Dim remainReturnQty As Double = 0D
                'If dtLastReturnData Is Nothing Then Return 0
                'Dim dblActualReturn As Double = 0D
                'Dim dblTotalQty As Double = 0D

                'If dtLastReturnData.Rows.Count > 0 Then
                '    For Each r As DataRow In dtLastReturnData.Rows
                '        If dblTotalQty = Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString)))) Then
                '            Exit For
                '        End If
                '        If remainReturnQty > 0 Then
                '            If Val(r.Item("OutQty").ToString) <= remainReturnQty Then
                '                dblActualReturn = Val(r.Item("OutQty").ToString)
                '                remainReturnQty = Val(r.Item("OutQty").ToString) - dblActualReturn
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            Else
                '                'Val(r.Item("OutQty").ToString) >= remainReturnQty 
                '                dblActualReturn = remainReturnQty
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            End If
                '        Else
                '            If Val(r.Item("OutQty").ToString) >= Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString)))) Then
                '                dblActualReturn = Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString))))
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            Else
                '                remainReturnQty = (Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString)))) - Val(r.Item("OutQty").ToString))
                '                dblActualReturn = (Val(IIf(dtGrd.Rows(i).Item("Unit").ToString = "Loose", Val(dtGrd.Rows(i).Item("Qty").ToString), ((Val(dtGrd.Rows(i).Item("Qty").ToString)) * Val(dtGrd.Rows(i).Item("PackQty").ToString))))) - remainReturnQty
                '                CostPrice = Val(r.Item("Rate").ToString)
                '            End If
                '        End If
                '        'End If
                '        StockDetail = New StockDetail
                '        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                '        StockDetail.LocationId = Val(dtGrd.Rows(i).Item("LocationId").ToString)
                '        StockDetail.ArticleDefId = Val(dtGrd.Rows(i).Item("ItemId").ToString)
                '        StockDetail.InQty = dblActualReturn 'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))) - remainReturnQty
                '        StockDetail.OutQty = 0
                '        StockDetail.Rate = IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item("Rate").ToString), CostPrice)
                '        StockDetail.InAmount = StockDetail.InQty * StockDetail.Rate  'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)), (((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)))
                '        StockDetail.OutAmount = 0
                '        StockDetail.Remarks = String.Empty
                '        'Task:M16 Set Values In Engine_No and Chassis_No 
                '        StockDetail.Engine_No = String.Empty 'grd.GetRows(i).Cells("Engine_No").Value.ToString
                '        StockDetail.Chassis_No = String.Empty 'grd.GetRows(i).Cells("Chassis_No").Value.ToString
                '        'End Task:M16
                '        StockList.Add(StockDetail)
                '        dblTotalQty += dblActualReturn
                '    Next
                'Else

                CostPrice = Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) 'GetAvgRateByItem(Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString))
                'If (Val(dtGrd.Rows(i).Item("Qty").Value.ToString) > 0 Or Val(dtGrd.Rows(i).Item("Sample Qty").Value.ToString) > 0) Then
                StockDetail = New StockDetail
                StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                StockDetail.LocationId = dtGrd.Rows(i).Item(grdEnm.LocationId).ToString
                StockDetail.ArticleDefId = dtGrd.Rows(i).Item(grdEnm.ItemId).ToString
                ''StockDetail.InQty = IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString), ((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString)))
                StockDetail.InQty = Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) ''TASK-408 added TotalQty instead of Qty
                StockDetail.OutQty = 0
                StockDetail.Rate = IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)
                ''StockDetail.InAmount = IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", ((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)), (((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)))
                StockDetail.InAmount = ((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)) '' TASK-408 added TotalQty instead of Qty
                StockDetail.OutAmount = 0
                StockDetail.Remarks = dtGrd.Rows(i).Item("Comments").ToString

                ''Start TASK-470 on 01-07-2016
                StockDetail.PackQty = dtGrd.Rows(i).Item("PackQty").ToString
                StockDetail.In_PackQty = dtGrd.Rows(i).Item("Qty").ToString
                StockDetail.Out_PackQty = 0
                ''End TASK-470
                StockList.Add(StockDetail)
                'dblTotalCost += StockDetail.OutAmount

                'End If
                If Me.cmbIssuence.Value > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Select IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0) As Quantity FROM DispatchDetailTable  WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    Dim DA As New OleDbDataAdapter
                    Dim DADt As New DataTable
                    DA.SelectCommand = objCommand
                    DA.Fill(DADt)
                    If DADt.Rows.Count > 0 Then
                        If Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) > Val(DADt.Rows(0).Item(0).ToString) Then
                            If msg_Confirm("Product '" & dtGrd.Rows(i).Item(grdEnm.Item).ToString & "' Quantity is higher than allocated quantity. Do you want to proceed?") = False Then
                                dtGrd.Rows(i).Item(grdEnm.TotalQty) = Val(DADt.Rows(0).Item(0).ToString)
                                dtGrd.Rows(i).Item(grdEnm.Qty) = Val(DADt.Rows(0).Item(0).ToString)
                                'dtGrd.AcceptChanges()
                            End If
                        End If
                    End If

                    objCommand.CommandText = ""
                    objCommand.CommandText = "UPDATE  DispatchDetailTable " _
                                                  & " SET ReturnedQty = isnull(ReturnedQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", ReturnedTotalQty = isnull(ReturnedTotalQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & " " _
                                                  & " WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    objCommand.ExecuteNonQuery()
                End If
                ''TASK: TFS1133 maintained status of Quantity at Store issuance
                If Me.cmbTicketNo.SelectedValue > 0 Then
                    'objCommand.CommandText = ""
                    'objCommand.CommandText = "Select IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0) As Quantity FROM DispatchDetailTable  WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    'Dim DA As New OleDbDataAdapter
                    'Dim DADt As New DataTable
                    'DA.SelectCommand = objCommand
                    'DA.Fill(DADt)
                    'If DADt.Rows.Count > 0 Then
                    '    If Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) > Val(DADt.Rows(0).Item(0).ToString) Then
                    '        If msg_Confirm("Product '" & dtGrd.Rows(i).Item(grdEnm.Item).ToString & "' Quantity is higher than allocated quantity. Do you want to proceed?") = False Then
                    '            dtGrd.Rows(i).Item(grdEnm.TotalQty) = Val(DADt.Rows(0).Item(0).ToString)
                    '            dtGrd.Rows(i).Item(grdEnm.Qty) = Val(DADt.Rows(0).Item(0).ToString)
                    '            'dtGrd.AcceptChanges()
                    '        End If
                    '    End If
                    'End If

                    objCommand.CommandText = ""
                    objCommand.CommandText = "UPDATE  DispatchDetailTable " _
                                                  & " SET ReturnedQty = isnull(ReturnedQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", ReturnedTotalQty = isnull(ReturnedTotalQty,0) +  " & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & " " _
                                                  & " WHERE (ArticleDefId = " & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ") And (EstimationId =" & Val(dtGrd.Rows(i).Item("EstimationId").ToString) & ") And (SubDepartmentID =" & Val(dtGrd.Rows(i).Item("DepartmentId").ToString) & ") And (DispatchDetailId =" & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ")"
                    objCommand.ExecuteNonQuery()
                End If
                objCommand.CommandText = ""
                'Before against task:2435
                'objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId, Pack_Desc) values( " _
                '                        & " " & txtReceivingID.Text & " ," & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ",N'" & (dtGrd.Rows(i).Item(grdEnm.Unit).ToString) & "'," & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", " _
                '                        & " " & IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString), (Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString))) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString) & "  , " & Val(dtGrd.Rows(i).Item(grdEnm.CurrentPrice).ToString) & ",N'" & dtGrd.Rows(i).Item(grdEnm.BatchNo).ToString & "'," & dtGrd.Rows(i).Item(grdEnm.BatchId).ToString & "," & dtGrd.Rows(i).Item(grdEnm.LocationId).ToString & ", " & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString) & ", N'" & dtGrd.Rows(i).Item(grdEnm.Pack_Desc).ToString.Replace("'", "''") & "')"
                'Task:2435 Added Column CostPrice
                'Before against task:2506
                'objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId, Pack_Desc,CostPrice) values( " _
                '                       & " " & txtReceivingID.Text & " ," & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ",N'" & (dtGrd.Rows(i).Item(grdEnm.Unit).ToString) & "'," & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", " _
                '                       & " " & IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString), (Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString))) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString) & "  , " & Val(dtGrd.Rows(i).Item(grdEnm.CurrentPrice).ToString) & ",N'" & dtGrd.Rows(i).Item(grdEnm.BatchNo).ToString & "'," & dtGrd.Rows(i).Item(grdEnm.BatchId).ToString & "," & dtGrd.Rows(i).Item(grdEnm.LocationId).ToString & ", " & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString) & ", N'" & dtGrd.Rows(i).Item(grdEnm.Pack_Desc).ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ")"
                'Task:2506 Added Field PlanUnit And PlanQty
                objCommand.CommandText = "Insert into ReturnDispatchDetailTable (ReturnDispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId, Pack_Desc,CostPrice, PlanUnit, PlanQty, Lot_No, Rack_No,Comments, DispatchDetailId, EstimationId, DepartmentId, TicketId) values( " _
                                       & " " & txtReceivingID.Text & " ," & Val(dtGrd.Rows(i).Item(grdEnm.ItemId).ToString) & ",N'" & (dtGrd.Rows(i).Item(grdEnm.Unit).ToString) & "'," & Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString) & ", " _
                                       & " " & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString) & ", " & Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString) & "  , " & Val(dtGrd.Rows(i).Item(grdEnm.CurrentPrice).ToString) & ",N'" & dtGrd.Rows(i).Item(grdEnm.BatchNo).ToString & "'," & dtGrd.Rows(i).Item(grdEnm.BatchId).ToString & "," & dtGrd.Rows(i).Item(grdEnm.LocationId).ToString & ", " & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString) & ", N'" & dtGrd.Rows(i).Item(grdEnm.Pack_Desc).ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ", N'" & dtGrd.Rows(i).Item("PlanUnit").ToString.Replace("'", "''") & "', " & Val(dtGrd.Rows(i).Item("PlanQty").ToString) & ", N'" & dtGrd.Rows(i).Item("LotNo").ToString.Replace("'", "''") & "',N'" & dtGrd.Rows(i).Item("Rack_No").ToString.Replace("'", "''") & "', N'" & dtGrd.Rows(i).Item("Comments").ToString.Replace("'", "''") & "', " & Val(dtGrd.Rows(i).Item("DispatchDetailId").ToString) & ", " & Val(dtGrd.Rows(i).Item("EstimationId").ToString) & ", " & Val(dtGrd.Rows(i).Item("DepartmentId").ToString) & ", " & Val(dtGrd.Rows(i).Item("TicketId").ToString) & ")" ''TASK-408 added TotalQty of instead of Pack condition
                'End Task:2506
                objCommand.ExecuteNonQuery()
                'End Task:2435
                'Val(grd.Rows(i).Cells(5).Value)
                'Before against task:2412
                'If StoreIssuanceDependonProductionPlan = True Then
                'Task:2412 Change Validation 
                If Me.cmbPlan.SelectedIndex > 0 Then
                    'End Task:2412
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update PlanCostSheetDetailTable Set IssuedQty=IssuedQty-" & Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) & " WHERE PlanCostSheetDetailTable.PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & " AND PlanCostSheetDetailTable.ArticleDefId=" & dtGrd.Rows(i).Item(grdEnm.ItemId).ToString & " AND PlanCostSheetDetailTable.ArticleMasterId=" & Val(dtGrd.Rows(i).Item(grdEnm.ArticleDefMasterId).ToString)
                    objCommand.ExecuteNonQuery()
                End If


                'If flgCylinderVoucher = True Then
                '    '' Customer Voucher Debit
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, debit_amount,credit_amount, comments) Values(" & lngVoucherMasterId & ", 1," & Me.cmbVendor.Value & ", " & Val(StockDetail.OutAmount) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")') "
                '    objCommand.ExecuteNonQuery()

                '    '' Security Deposit Voucher Credit
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, credit_amount, debit_amount, comments) Values(" & lngVoucherMasterId & ", 1," & Me.cmbSecurity.Value & ", " & Val(StockDetail.OutAmount) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")') "
                '    objCommand.ExecuteNonQuery()

                '    '' Fixed Asset Voucher Debit
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, debit_amount,credit_amount, comments) Values(" & lngVoucherMasterId & ",1," & Me.cmbFixedAssets.Value & ", " & Val(StockDetail.OutAmount) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")') "
                '    objCommand.ExecuteNonQuery()

                '    '' Cylinder Stock Voucher Credit
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_ID, coa_detail_id, credit_amount, debit_amount, comments) Values(" & lngVoucherMasterId & ", 1," & CylinderStockAccountId & ", " & Val(StockDetail.OutAmount) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("Qty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")') "
                '    objCommand.ExecuteNonQuery()
                'End If
                If Me.chkIssued.Checked = True Then ' If Issued Checked Then Update Stock And Update Voucher.
                    If flgCylinderVoucher = True Then
                        '' Customer Voucher Debit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, credit_amount, debit_amount,comments) Values(" & lngVoucherMasterId & ", 1," & Me.cmbVendor.Value & ", " & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " ''TASK-408 added TotalQty instead of Pack condition
                        objCommand.ExecuteNonQuery()

                        '' Security Deposit Voucher Credit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id,  debit_amount, credit_amount,comments) Values(" & lngVoucherMasterId & ", 1," & Me.cmbSecurity.Value & "," & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " ''TASK-408 added TotalQty instead of Pack condtion Qty
                        objCommand.ExecuteNonQuery()

                        '' Fixed Asset Voucher Debit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, credit_amount,debit_amount, comments) Values(" & lngVoucherMasterId & ", 1," & Me.cmbFixedAssets.Value & ", " & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " ''TASK-408 added TotalQty instead of Pack condition Qty
                        objCommand.ExecuteNonQuery()

                        '' Cylinder Stock Voucher Credit
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id,  debit_amount, credit_amount,comments) Values(" & lngVoucherMasterId & ", 1, " & CylinderStockAccountId & "," & (Val(dtGrd.Rows(i).Item(grdEnm.TotalQty).ToString) * Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString)) & ",0,N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), 3) & ")') " ''TASK-408 added TotalQty instead of Pack condition Qty
                        objCommand.ExecuteNonQuery()

                    End If

                    If flgStoreIssuenceVoucher = True Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
                        If flgAvrRate = True Then

                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,  credit_amount, debit_amount,comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & StockDetail.InAmount & ", N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")', " & Me.cmbPo.SelectedValue & " )" ''TASK-408 added TotalQty instead of Qty
                            objCommand.ExecuteNonQuery()


                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount,  debit_amount,comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & StockDetail.InAmount & " ,0 , N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")',  " & Me.cmbPo.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty
                            objCommand.ExecuteNonQuery()

                        Else
                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,  credit_amount,debit_amount, comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & StockDetail.InAmount & ", N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")', " & Me.cmbPo.SelectedValue & " )" ''TASK-408 added TotalQty instead of Qty
                            objCommand.ExecuteNonQuery()


                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,  credit_amount,debit_amount, comments, CostCenterId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & StockDetail.InAmount & " ,0 , N'" & dtGrd.Rows(i).Item(grdEnm.Item).ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")',  " & Me.cmbPo.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty
                            objCommand.ExecuteNonQuery()

                        End If
                    End If

                End If 'End Task:M21
            Next


            'Dim str As String
            'Dim sarray() As String = str.Split("|")


            'If flgStoreIssuenceVoucher = False Then
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "delete from tblVoucher where voucher_Id =" & lngVoucherMasterId
            '    objCommand.ExecuteNonQuery()

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            '    objCommand.ExecuteNonQuery()

            'End If

            'If flgStoreIssuenceVoucher = True Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
            '    If flgAvrRate = True Then
            '        'If lngVoucherMasterId > 0 Then
            '        '    objCommand.CommandText = ""
            '        '    objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=1 " _
            '        '                            & "   where voucher_id=" & lngVoucherMasterId
            '        '    objCommand.ExecuteNonQuery()
            '        'Else

            '        '    objCommand.CommandText = ""
            '        '    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '        '                               & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '        '                               & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '        '                               & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
            '        '                               & " SELECT @@IDENTITY"
            '        '    lngVoucherMasterId = objCommand.ExecuteScalar

            '        'End If
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & dblTotalCost & ", 'Store Issuence Ref: " & Me.txtPONo.Text & "',  " & Me.cmbPo.SelectedValue & ")"

            '        objCommand.ExecuteNonQuery()


            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & dblTotalCost & " ,0 , 'Store Issuence Ref: " & Me.txtPONo.Text & "',  " & Me.cmbPo.SelectedValue & " )"

            '        objCommand.ExecuteNonQuery()

            '    Else

            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", 'Store Issuence Ref: " & Me.txtPONo.Text & "',  " & Me.cmbPo.SelectedValue & ")"

            '        objCommand.ExecuteNonQuery()


            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & " ,0 , 'Store Issuence Ref: " & Me.txtPONo.Text & "',  " & Me.cmbPo.SelectedValue & " )"

            '        objCommand.ExecuteNonQuery()

            '    End If
            'End If

            'objCommand.CommandText = ""
            'objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'" _
            '                        & "   where voucher_id=" & lngVoucherMasterId

            'objCommand.Transaction = trans

            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Deleting Detail
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId

            'objCommand.Transaction = trans

            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Debit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & Val(Me.txtAmount.Text) & ", 0)"

            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Credit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAmount.Text) & ")"
            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()





            'Code By Imran Ali 10-July-2013
            'Request Against 732
            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            'Task:2412 Change Validation
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT ISNULL(SUM(ISNULL(PlanQty, 0) + ISNULL(IssuedQty, 0)), 0) AS Qty FROM  dbo.PlanCostSheetDetailTable WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ""
                Dim objDa As New OleDbDataAdapter
                Dim objDt As New DataTable
                objDa.SelectCommand = objCommand
                objDa.Fill(objDt)
                If objDt IsNot Nothing Then
                    If objDt.Rows.Count > 0 Then
                        If objDt.Rows(0).Item(0) <> 0 Then
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Open.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            objCommand.ExecuteNonQuery()
                        End If
                    Else
                        objCommand.CommandText = ""
                        objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                        objCommand.ExecuteNonQuery()
                    End If
                End If
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If IsValidate() = True Then
                StockMaster.StockTransId = StockTransId(Me.txtPONo.Text, trans)
                Call New StockDAL().Update(StockMaster, trans)
            End If

            trans.Commit()
            Update_Record = True
            'insertvoucher()
            'Task:M21 Stock Update Issued Rights Based
            'If Me.chkIssued.Checked = True Then
            'Call Update1() 'Upgrading Stock ...
            'End Task:M21
            setVoucherNo = Me.txtPONo.Text
            getVoucher_Id = Me.txtReceivingID.Text
            setEditMode = True
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            setVoucherdate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
            dtDataEmail = CType(Me.grd.DataSource, DataTable)
            dblTotalCost = 0
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try

        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.StoreIssuence, Me.txtPONo.Text.Trim, True)



    End Function

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If Me.btnsave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            Mode = "Normal"
            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpPODate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpPODate.Focus()
                Exit Sub
            End If

            If FormValidate() Then
                Me.grd.UpdateData()
                If Me.btnsave.Text = "Save" Or Me.btnsave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() Then

                        '' Made changes to  against TASK TFS1462 on 13-09-2017
                        Dim Printing As Boolean = False
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Dim DirectVoucherPrinting As Boolean
                        DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim Print1 As Boolean = frmMessages.Print
                                Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                                'If Print1 = True Then
                                '    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                                'End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtPONo.Text, Me.Name, "PKR", 1, True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1462
                        'EmailSave()
                        '   msg_Information(str_informSave)
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data

                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '------------------------------
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop

                    Else
                        Exit Sub 'MsgBox("Record has not been added")
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    If Update_Record() Then

                        '' Made changes to  against TASK TFS1462 on 13-09-2017
                        Dim Printing As Boolean = False
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Dim DirectVoucherPrinting As Boolean
                        DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim Print1 As Boolean = frmMessages.Print
                                Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                                'If Print1 = True Then
                                '    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                                'End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtPONo.Text, Me.Name, "PKR", 1, True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1462
                        'EmailSave()
                        ' msg_Information(str_informUpdate)
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data

                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '------------------------------
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                    Else
                        Exit Sub 'MsgBox("Record has not been updated")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage("Error occured while saving:" & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'Me.Cursor = Cursors.WaitCursor
        'Dim s As String
        's = "1234-567-890"
        'MsgBox(Microsoft.VisualBasic.Right(s, InStr(1, s, "-") - 2))

        If Me.grd.RowCount > 0 Then
            If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        End If

        RefreshControls()
        'Me.Cursor = Cursors.Default
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        EditRecord()
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
    End Sub

    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Me.DisplayPODetail(Me.cmbPo.SelectedValue)
        If Me.cmbPo.SelectedIndex > 0 Then
            'Dim adp As New OleDbDataAdapter
            'Dim dt As New DataTable
            'Dim Sql As String = "SELECT     dbo.PlanMasterTable.CustomerID, dbo.vwCOADetail.detail_title FROM         dbo.PlanMasterTable INNER JOIN                       dbo.vwCOADetail ON dbo.PlanMasterTable.CustomerID = dbo.vwCOADetail.coa_detail_id where PlanMasterTable.PlanId=" & Me.cmbPo.SelectedValue
            'adp = New OleDbDataAdapter(Sql, Con)
            'adp.Fill(dt)

            'If Not dt.Rows.Count > 0 Then
            '    'Me.cmbVendor.Enabled = True : Me.cmbVendor.Rows(0).Activate()
            'Else
            '    Me.cmbVendor.Value = dt.Rows(0).Item("CustomerID").ToString
            '    Me.cmbVendor.Enabled = False
            'End If
            'GetTotal()
        Else
            'Me.cmbVendor.Enabled = True
            'Me.cmbVendor.Rows(0).Activate()
        End If
    End Sub

    'Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    With Me.grd.CurrentRow

    '        If Me.grd.CurrentRow.Cells("Unit").Value = "Loose" Then
    '            'txtPackQty.Text = 1
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
    '        Else
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
    '        End If
    '        GetTotal()
    '    End With
    'End Sub
    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            ClearDetailControls()
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            GetRawMaterialPrice(Me.cmbItem.Value)
            'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)

            Dim strSQl As String = String.Empty

            'If GetConfigValue("WithSizeRange") = "False" Then
            '    strSQl = "SELECT Stock, BatchNo FROM         dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            If getConfigValueByType("WithSizeRange") = "True" Then
                strSQl = "SELECT     ISNULL(a.Stock, 0) AS Stock, PurchaseBatchTable.BatchNo AS [Batch No] , PurchaseBatchTable.BatchID" _
                        & " FROM         SizeRangeTable INNER JOIN" _
                        & "                   PurchaseBatchTable ON SizeRangeTable.BatchID = PurchaseBatchTable.BatchID LEFT OUTER JOIN " _
                        & "                    (SELECT     * " _
                        & "   FROM vw_Batch_Stock " _
                        & "                WHERE      articleID = " & Me.cmbItem.Value & ") a ON PurchaseBatchTable.BatchID = a.BatchID " _
                        & "WHERE(SizeRangeTable.SizeID = " & IIf(Val(Me.cmbItem.ActiveRow.Cells("Size ID").Value.ToString) > 0, Me.cmbItem.ActiveRow.Cells("Size ID").Value, 0) & ") "
            Else
                strSQl = "SELECT Stock, BatchNo as [Batch No], BatchID FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            End If

            'FillUltraDropDown(cmbBatchNo, strSQl, False)
            cmbBatchNo.DataSource = Nothing


            Dim objCommand As New OleDbCommand
            Dim objDataAdapter As New OleDbDataAdapter
            Dim objDataSet As New DataSet

            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSQl


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            dr = dt.NewRow
            dr.Item(1) = "xxxx"
            dr.Item(2) = 0
            dr.Item(0) = 0
            dt.Rows.Add(dr)

            cmbBatchNo.DataSource = dt
            cmbBatchNo.ValueMember = "BatchID"
            'cmbBatchNo.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            cmbBatchNo.DisplayMember = "Batch No" 'dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName"
            cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchID").Hidden = True
            Me.cmbBatchNo.Rows(0).Activate()
            Con.Close()
            Me.cmbBatchNo.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Dim cmb As Infragistics.Win.UltraWinGrid.UltraCombo
        cmb = sender
        cmb.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
    Private Sub cmbVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.Enter
        Me.cmbVendor.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'ValidateDateLock()
        'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
        'If flgDateLock = True Then
        '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt"))) Then
        '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
        '    End If
        'End If
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)
        Me.Cursor = Cursors.WaitCursor
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()

        Try
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction
            cm.Connection = Con
            cm.Transaction = objTrans

            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            'Task:2412 Change Validation
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                cm.CommandText = ""
                cm.CommandText = "SELECT dbo.ReturnDispatchDetailTable.ArticleDefId, Isnull(ReturnDispatchDetailTable.ArticleDefMasterId,0) as ArticleDefMasterId,  SUM(ISNULL(dbo.ReturnDispatchDetailTable.Qty, 0)) AS Qty  FROM  dbo.ReturnDispatchDetailTable INNER JOIN   dbo.ReturnDispatchMasterTable ON dbo.ReturnDispatchDetailTable.ReturnDispatchId = dbo.ReturnDispatchMasterTable.ReturnDispatchId WHERE ReturnDispatchMasterTable.ReturnDispatchId=" & Val(Me.txtReceivingID.Text) & " AND Isnull(dbo.ReturnDispatchMasterTable.PlanId,0) =" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & "  GROUP BY dbo.ReturnDispatchDetailTable.ArticleDefId, Isnull(ReturnDispatchDetailTable.ArticleDefMasterId,0) "
                Dim objDaData As New OleDbDataAdapter
                Dim objDtData As New DataTable
                'cm.Connection = Con
                'cm.Transaction = objTrans
                objDaData.SelectCommand = cm
                objDaData.Fill(objDtData)
                objDtData.AcceptChanges()

                If objDtData IsNot Nothing Then
                    If objDtData.Rows.Count > 0 Then
                        For Each r As DataRow In objDtData.Rows
                            cm.CommandText = ""
                            cm.CommandText = "Update PlanCostSheetDetailTable Set IssuedQty=IssuedQty-" & Val(r.Item("Qty")) & " WHERE PlanCostSheetDetailTable.PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & " AND PlanCostSheetDetailTable.ArticleDefId=" & r.Item("ArticleDefId") & " AND PlanCostSheetDetailTable.ArticleMasterId=" & r.Item("ArticleDefMasterId")
                            'cm.Connection = Con
                            'cm.Transaction = objTrans
                            cm.ExecuteNonQuery()
                        Next
                    End If
                End If
            End If

            cm.CommandText = ""
            cm.CommandText = "Update DispatchDetailTable Set ReturnedQty=IsNull(ReturnedQty,0)-IsNull(ReturnDispatchDetail.Qty,0),  ReturnedTotalQty=IsNull(ReturnedTotalQty,0)-IsNull(ReturnDispatchDetail.TotalQty,0) From DispatchDetailTable,(Select DispatchDetailId, ArticleDefId,  SUM(IsNull(Sz1,0)) As Qty, SUM(IsNull(Qty,0)) as TotalQty From ReturnDispatchDetailTable WHERE ReturnDispatchId=" & Val(Me.txtReceivingID.Text) & " AND IsNull(DispatchDetailId,0) <> 0 Group By DispatchDetailId, ArticleDefId) as ReturnDispatchDetail WHERE ReturnDispatchDetail.DispatchDetailId = DispatchDetailTable.DispatchDetailId And ReturnDispatchDetail.ArticleDefId = DispatchDetailTable.ArticleDefId"
            cm.ExecuteNonQuery()

            ''Reduce quantity against ticket and estimation
            cm.CommandText = ""
            cm.CommandText = "Update DispatchDetailTable Set ReturnedQty=IsNull(ReturnedQty,0)-IsNull(ReturnDispatchDetail.Qty,0),  ReturnedTotalQty=IsNull(ReturnedTotalQty,0)-IsNull(ReturnDispatchDetail.TotalQty,0) From DispatchDetailTable,(Select IsNull(EstimationId, 0) As EstimationId, IsNull(ArticleDefId, 0) As ArticleDefId, IsNull(DepartmentId, 0) As DepartmentId, IsNull(DispatchDetailId, 0) As DispatchDetailId, SUM(IsNull(Sz1,0)) As Qty, SUM(IsNull(Qty,0)) as TotalQty From ReturnDispatchDetailTable WHERE ReturnDispatchId=" & Val(Me.txtReceivingID.Text) & " AND IsNull(EstimationId,0) <> 0 Group By  ArticleDefId, EstimationId, DepartmentId, DispatchDetailId) as ReturnDispatchDetail WHERE ReturnDispatchDetail.EstimationId = DispatchDetailTable.EstimationId And ReturnDispatchDetail.ArticleDefId = DispatchDetailTable.ArticleDefId And ReturnDispatchDetail.DepartmentId = DispatchDetailTable.SubDepartmentId And ReturnDispatchDetail.DispatchDetailId = DispatchDetailTable.DispatchDetailId"
            cm.ExecuteNonQuery()
            'cm.Connection = Con
            cm.CommandText = String.Empty
            cm.CommandText = "delete from ReturnDispatchDetailTable where ReturnDispatchid=" & Me.grdSaved.CurrentRow.Cells(6).Value.ToString
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()



            'cm = New OleDbCommand
            'cm.Connection = Con
            cm.CommandText = String.Empty
            cm.CommandText = "delete from ReturnDispatchMasterTable where ReturnDispatchid=" & Me.grdSaved.CurrentRow.Cells(6).Value.ToString
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()


            'cm = New OleDbCommand
            'cm.Connection = Con
            cm.CommandText = String.Empty
            cm.CommandText = "delete from tblvoucherdetail where voucher_id=" & lngVoucherMasterId
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            'cm = New OleDbCommand
            'cm.Connection = Con
            cm.CommandText = String.Empty
            cm.CommandText = "delete from tblvoucher where voucher_id=" & lngVoucherMasterId
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            'cm = New OleDbCommand
            'cm.Connection = Con
            'cm.CommandText = "delete from CostingDetailTable where DispatchId=" & Me.grdSaved.CurrentRow.Cells(6).Value.ToString

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            'Task:2412 Change Validation
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                cm = New OleDbCommand
                cm.Connection = Con
                cm.Transaction = objTrans
                cm.CommandText = ""
                cm.CommandText = "SELECT ISNULL(SUM(ISNULL(PlanQty, 0) - ISNULL(IssuedQty, 0)), 0) AS Qty FROM  dbo.PlanCostSheetDetailTable WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ""
                Dim objDa As New OleDbDataAdapter
                Dim objDt As New DataTable
                objDa.SelectCommand = cm
                objDa.Fill(objDt)
                objDt.AcceptChanges()
                If objDt IsNot Nothing Then
                    If objDt.Rows.Count > 0 Then
                        If objDt.Rows(0).Item(0) <> 0 Then
                            'cm = New OleDbCommand
                            'cm.Connection = Con
                            'cm.Transaction = objTrans
                            cm.CommandText = ""
                            cm.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Open.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            cm.ExecuteNonQuery()
                        Else
                            'cm = New OleDbCommand
                            'cm.Connection = Con
                            'cm.Transaction = objTrans
                            cm.CommandText = ""
                            cm.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            cm.ExecuteNonQuery()
                        End If
                    Else
                        'cm = New OleDbCommand
                        'cm.Connection = Con
                        'cm.Transaction = objTrans
                        cm.CommandText = ""
                        cm.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                        cm.ExecuteNonQuery()
                    End If
                End If
            End If

            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(StockTransId(grdSaved.CurrentRow.Cells(0).Value, objTrans))
            Call New StockDAL().Delete(StockMaster, objTrans)
            objTrans.Commit()
            'Call Delete() 'Upgrading Stock ...
            'msg_Information(str_informDelete)
            Me.txtReceivingID.Text = 0
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 27-1-14 

        Catch ex As Exception
            msg_Error("Error occured while deleting record " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
        SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.StoreIssuence, grdSaved.CurrentRow.Cells(0).Value.ToString, True)
        Me.grdSaved.CurrentRow.Delete()
        Me.RefreshControls()


    End Sub
    'Private Sub grd_CellEndEdit1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    With Me.grd.CurrentRow

    '        If Me.grd.CurrentRow.Cells("Unit").Value = "Loose" Then
    '            'txtPackQty.Text = 1
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
    '        Else
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
    '        End If
    '        GetTotal()
    '    End With

    'End Sub


    'Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
    '    Me.GetTotal()
    'End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnsave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnBarcode.Enabled = True
                Me.btnPrintVoucher.Enabled = True
                Me.btnPrintVoucher1.Enabled = True
                'Me.chkIssued.Checked = True 'TASK:M21 Set Issued Checked 
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = True
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnsave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnSearchDelete.Enabled = False
                    Me.btnSearchPrint.Enabled = False
                    Me.btnPrint.Enabled = False
                    Me.btnPrintVoucher.Enabled = False
                    Me.btnPrintVoucher1.Enabled = False
                    'Me.chkIssued.Checked = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False

                    'Task 1592 Apply future date rights
                    IsDateChangeAllowed = False
                    DateChange(False)
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnsave.Text = "Save" Or Me.btnsave.Text = "&Save" Then
                            Me.btnsave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnsave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                'GetSecurityByPostingUser(UserPostingRights, Me.SaveToolStripButton, Me.DeleteToolStripButton)
            Else
                'Me.Visible = False
                Me.btnsave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.btnSearchPrint.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False  ''TFS1823
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnBarcode.Enabled = False

                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)
                'Task:2406 Added Field Chooser Rights
                ' Me.chkIssued.Checked = False 'TASK:M21 Set Issued Checked 
                Me.btnSelectedIssuenceUpdate.Enabled = False
                Me.btnPrintVoucher.Enabled = False
                Me.btnPrintVoucher1.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnsave.Text = "&Save" Then btnsave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnsave.Text = "&Update" Then btnsave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        Me.btnSearchPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar2.mGridExport.Enabled = True   ''TFS1823
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        '    Me.chkPost.Visible = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Issued" Then
                        Me.chkIssued.Checked = True
                        '' 04-Jul-2014 TASK:2716 Imran Ali Selected Store Issuance Update 

                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                    ElseIf RightsDt.FormControlName = "Selected Issuence Update" Then
                        Me.btnSelectedIssuenceUpdate.Enabled = True
                    ElseIf RightsDt.FormControlName = "Bar Code" Then
                        Me.btnBarcode.Enabled = True
                        'End Task:2716
                        ''TASK TFS1458 Print Voucher rights
                    ElseIf RightsDt.FormControlName = "Print Voucher" Then
                        Me.btnPrintVoucher.Enabled = True
                        Me.btnPrintVoucher1.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Task:1592 Added Future Date Rights
    Private Sub DateChange(ByRef IsDateChangeAllowed As Boolean)
        Try
            If IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = DateTime.Now.ToString("yyyy/M/d 23:59:59 ")
            Else
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'END TASk:1592
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoadAll.Visible = False
    '        Me.ToolStripButton2.Visible = False
    '        Me.ToolStripSeparator1.Visible = False
    '    Else
    '        Me.BtnLoadAll.Visible = True
    '        Me.ToolStripButton2.Visible = True
    '        Me.ToolStripSeparator1.Visible = True
    '    End If
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If Not getConfigValueByType("StoreIssuenceVoucher").ToString = "Error" Then
                flgStoreIssuenceVoucher = getConfigValueByType("StoreIssuenceVoucher")
            End If

            If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
                StoreIssuanceDependonProductionPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
            End If

            ''Task:2366 Added Location Wise Filter Configuration
            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''End Task:2366

            If Not getConfigValueByType("CGAccountOnStoreIssuance").ToString = "Error" Then
                blnCGAccountOnStoreIssuance = Convert.ToBoolean(getConfigValueByType("CGAccountOnStoreIssuance").ToString)
            End If

            ''Start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161

            'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14 
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            Dim id As Integer = 0

            id = Me.cmbVendor.SelectedRow.Cells(0).Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = id
            id = Me.cmbFixedAssets.SelectedRow.Cells(0).Value
            FillCombo("FixedAssetAccount")
            Me.cmbFixedAssets.Value = id
            id = Me.cmbSecurity.SelectedRow.Cells(0).Value
            FillCombo("SecurityAccount")
            Me.cmbSecurity.Value = id

            id = Me.cmbSalesMan.SelectedValue
            FillCombo("SM")
            Me.cmbSalesMan.SelectedValue = id

            id = Me.cmbPo.SelectedValue
            FillCombo("SO")
            Me.cmbPo.SelectedValue = id

            id = Me.cmbCategory.SelectedValue
            FillCombo("Category")
            Me.cmbCategory.SelectedValue = id

            id = Me.cmbItem.SelectedRow.Cells(0).Value
            FillCombo("Item")
            Me.cmbItem.Value = id

            'id = Me.cmbCostSheetLocation.SelectedValue
            'FillCombo("Category")
            'Me.cmbCostSheetLocation.SelectedValue = id

            'id = Me.cmbCostSheetItems.SelectedRow.Cells(0).Value
            'FillCombo("CostSheetItem")
            'Me.cmbCostSheetItems.Value = id

            id = Me.cmbPlan.SelectedIndex
            FillCombo("Plan")
            Me.cmbPlan.SelectedIndex = id

            FillCombo("grdLocation")

            id = Me.ComboBox1.SelectedIndex
            FillCombo("Employees")
            Me.ComboBox1.SelectedIndex = id

            id = Me.cmbCGAccount.ActiveRow.Cells(0).Value
            FillCombo("CGAccount")
            Me.cmbCGAccount.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Me.Cursor = Cursors.WaitCursor
        'Try
        '    '' Request No 871 ...
        '    '' 18-11-2013 By Imran Ali
        '    '' Batch Wise Cost Sheet  ''

        '    If Not Me.cmbCostSheetItems.IsItemInList Then Exit Sub
        '    Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
        '    dtData.TableName = "StockIssue"
        '    If SBUtility.Utility.GetFilterDataFromDataTable(dtData, "[ArticleDefMasterId]=N'" & Me.cmbCostSheetItems.Value & "'").ToTable("StockIssue").Rows.Count > 0 Then
        '        ShowErrorMessage("Already Exists")
        '        Me.cmbCostSheetItems.Focus()
        '        Exit Sub
        '    End If
        '    'Before against task:2412
        '    'If StoreIssuanceDependonProductionPlan = True Then
        '    'Task:2412 Change Validation 
        '    If Me.cmbPlan.SelectedIndex > 0 Then
        '        'End Task:2412
        '        If Me.cmbCostSheetItems.SelectedRow.Cells(0).Value = 0 AndAlso Me.cmbPlan.SelectedIndex <> 0 Then
        '            If Me.cmbPlan.SelectedIndex > 0 Then
        '                DisplayPODetail(Me.cmbPlan.SelectedValue, "Plan")
        '                Exit Sub
        '            End If
        '        Else
        '            If Val(Me.txtPlanQty.Text) > 0 Then
        '                If Val(Me.txtPlanQty.Text) < Val(Me.txtCostSheetQty.Text) Then
        '                    ShowErrorMessage("Issue qty is grater than the plan qty.")
        '                    Me.txtCostSheetQty.Focus()
        '                    Exit Sub
        '                End If
        '            End If

        '            Dim Dt As New DataTable
        '            Dim Da As OleDb.OleDbDataAdapter
        '            Dim str As String
        '            '' Add New Column of Article Size in this query against request no 871
        '            str = "SELECT a.CostSheetID, a.ArticleID, a.Qty, a.MasterArticleID, b.PurchasePrice, a.ArticleSize FROM tblCostSheet a INNER JOIN ArticleDefTable b ON a.ArticleID = b.ArticleId WHERE a.MasterArticleID =" & Me.cmbCostSheetItems.Value & ""
        '            N'" & Me.cmbCostSheetItems.Value
        '            If Not Con.State = 1 Then Con.Open()
        '            Da = New OleDb.OleDbDataAdapter(str, Con)
        '            Da.Fill(Dt)




        '            If Dt.Rows.Count > 0 Then
        '                For Each Row As DataRow In Dt.Rows
        '                    Me.cmbCategory.SelectedValue = Me.cmbCategory.SelectedValue
        '                    Me.cmbItem.Value = Row.Item("ArticleID").ToString
        '                    Me.cmbItem_Leave(Nothing, Nothing)
        '                    Me.txtQty.Text = 1
        '                    Me.txtRate.Text = Row.Item("PurchasePrice").ToString
        '                    'Me.txtQty.Text = IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
        '                    If Me.cmbUnitCostSheet.Text = "Batch" Then ''
        '                        If Val(Me.txtCostSheetQty.Text) <> 0 Then
        '                            If Not Me.cmbUnitCostSheet.Text = "Batch" Then
        '                                Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
        '                                ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
        '                            Else
        '                                Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
        '                            End If
        '                        End If
        '                    ElseIf Me.cmbUnitCostSheet.Text = "Loose" Then
        '                        If Val(Me.txtCostSheetQty.Text) = 0 Then
        '                            Me.txtCostSheetQty.Text = 1
        '                        End If
        '                        If Val(Me.txtCostSheetQty.Text) <> 0 Then
        '                            If Row.Item("ArticleSize").ToString = "Loose" Then
        '                                Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
        '                                ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
        '                            Else
        '                                Me.txtQty.Text = (Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)))
        '                            End If
        '                        End If
        '                    End If
        '                    Me.txtRate_LostFocus(Nothing, Nothing)


        '                    str = String.Empty
        '                    str = "SELECT ArticleDefId, ArticleMasterId, SUM(ISNULL(PlanQty, 0) - ISNULL(IssuedQty, 0)) AS BalanceQty FROM dbo.PlanCostSheetDetailTable WHERE ArticleMasterId=" & Row.Item("MasterArticleID").ToString & " AND ArticleDefId=" & Row.Item("ArticleId").ToString & " AND PlanId=" & Me.cmbPlan.SelectedValue & "  GROUP BY ArticleDefId, ArticleMasterId HAVING      (SUM(ISNULL(PlanQty, 0) - ISNULL(IssuedQty, 0)) <> 0) "
        '                    Dim dtBAL As New DataTable
        '                    dtBAL = GetDataTable(str)

        '                    If dtBAL IsNot Nothing Then
        '                        If dtBAL.Rows.Count > 0 Then
        '                            If Row.Item("ArticleID").ToString = dtBAL.Rows(0).Item("ArticleDefId").ToString Then
        '                                If Val(Me.txtQty.Text) > Val(dtBAL.Rows(0).Item("BalanceQty").ToString) Then
        '                                    Me.txtQty.Text = Val(dtBAL.Rows(0).Item("BalanceQty").ToString)
        '                                Else
        '                                    Me.txtQty.Text = Val(Me.txtQty.Text)
        '                                End If
        '                                Me.btnAdd_Click(Nothing, Nothing)
        '                                Me.txtRate.Text = Val(0)
        '                                Application.DoEvents()
        '                            End If
        '                        End If
        '                    End If
        '                Next
        '                'If StoreIssuanceDependonProductionPlan = False Then Me.Button1.Enabled = False
        '            Else
        '                Me.Button1.Enabled = True
        '            End If
        '        End If
        '    Else

        '        If Val(Me.txtPlanQty.Text) > 0 Then
        '            If Val(Me.txtPlanQty.Text) < Val(Me.txtCostSheetQty.Text) Then
        '                ShowErrorMessage("Issue qty is grater than the plan qty.")
        '                Me.txtCostSheetQty.Focus()
        '                Exit Sub
        '            End If
        '        End If

        '        Dim Dt As New DataTable
        '        Dim Da As OleDb.OleDbDataAdapter
        '        Dim str As String
        '        '' Add New Column of Article Size in this query against request no 871
        '        str = "SELECT a.CostSheetID, a.ArticleID, a.Qty, a.MasterArticleID, b.PurchasePrice, a.ArticleSize FROM tblCostSheet a INNER JOIN ArticleDefTable b ON a.ArticleID = b.ArticleId WHERE a.MasterArticleID =" & Me.cmbCostSheetItems.Value & ""
        '        N'" & Me.cmbCostSheetItems.Value
        '        If Not Con.State = 1 Then Con.Open()
        '        Da = New OleDb.OleDbDataAdapter(str, Con)
        '        Da.Fill(Dt)
        '        If Dt.Rows.Count > 0 Then
        '            For Each Row As DataRow In Dt.Rows
        '                Me.cmbCategory.SelectedValue = Me.cmbCategory.SelectedValue
        '                Me.cmbItem.Value = Row.Item("ArticleID").ToString
        '                Me.cmbItem_Leave(Nothing, Nothing)
        '                Me.txtQty.Text = 1
        '                Me.txtRate.Text = Row.Item("PurchasePrice").ToString
        '                If Me.cmbUnitCostSheet.Text = "Batch" Then
        '                    If Val(Me.txtCostSheetQty.Text) <> 0 Then
        '                        If Me.cmbUnitCostSheet.Text = "Batch" Then
        '                            Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
        '                            ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
        '                        Else
        '                            Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
        '                        End If
        '                    End If
        '                ElseIf Me.cmbUnitCostSheet.Text = "Loose" Then
        '                    If Val(Me.txtCostSheetQty.Text) = 0 Then
        '                        Me.txtCostSheetQty.Text = 1
        '                    End If
        '                    If Val(Me.txtCostSheetQty.Text) <> 0 Then
        '                        If Row.Item("ArticleSize").ToString = "Loose" Then
        '                            Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
        '                            ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
        '                        Else
        '                            Me.txtQty.Text = (Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)))
        '                        End If
        '                    End If
        '                End If
        '                Me.txtRate_LostFocus(Nothing, Nothing)
        '                Me.btnAdd_Click(Nothing, Nothing)
        '                Me.txtRate.Text = Val(0)
        '                Application.DoEvents()
        '            Next
        '            'If StoreIssuanceDependonProductionPlan = False Then Me.Button1.Enabled = False
        '        Else
        '            Me.Button1.Enabled = True
        '        End If
        '    End If

        '    'Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
        '    'dtData.TableName = "StockIssue"
        '    'If SBUtility.Utility.GetFilterDataFromDataTable(dtData, "[ArticleDefMasterId]=N'" & Me.cmbCostSheetItems.Value & "'").ToTable("StockIssue").Rows.Count > 0 Then
        '    '    ShowErrorMessage("Already Exists")
        '    '    Me.cmbCostSheetItems.Focus()
        '    '    Exit Sub
        '    'End If
        '    'Dim Dt As New DataTable
        '    'Dim Da As OleDb.OleDbDataAdapter
        '    'Dim str As String

        '    'str = "SELECT a.CostSheetID, a.ArticleID, a.Qty, a.MasterArticleID, b.PurchasePrice FROM tblCostSheet a INNER JOIN ArticleDefTable b ON a.ArticleID = b.ArticleId WHERE a.MasterArticleID =" & Me.cmbCostSheetItems.Value & ""
        '    'N'" & Me.cmbCostSheetItems.Value
        '    'If Not Con.State = 1 Then Con.Open()
        '    'Da = New OleDb.OleDbDataAdapter(str, Con)
        '    'Da.Fill(Dt)
        '    'If Dt.Rows.Count > 0 Then
        '    '    For Each Row As DataRow In Dt.Rows
        '    '        Me.cmbCategory.SelectedValue = Me.cmbCostSheetLocation.SelectedValue
        '    '        Me.cmbItem.Value = Row.Item("ArticleID").ToString
        '    '        Me.cmbItem_Leave(Nothing, Nothing)
        '    '        Me.txtQty.Text = 1
        '    '        Me.txtRate.Text = Row.Item("PurchasePrice").ToString
        '    '        Me.txtQty.Text = Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)
        '    '        Me.txtRate_LostFocus(Nothing, Nothing)
        '    '        Me.btnAdd_Click(Nothing, Nothing)
        '    '        Me.txtRate.Text = Val(0)
        '    '        Application.DoEvents()
        '    '    Next
        '    '    If StoreIssuanceDependonProductionPlan = False Then Me.Button1.Enabled = False
        '    'Else
        '    '    Me.Button1.Enabled = True
        '    'End If

        '    Con.Close()
        '    dt = Nothing
        '    'Da = Nothing

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub
    'Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    DisplayRecord("All")
    '    DisplayDetail(-1)
    '    Me.Cursor = Cursors.Default
    'End Sub
    Private Sub FillComboByEdit()
        Try
            FillCombo("Vendor")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> grdEnm.LocationId AndAlso col.Index <> grdEnm.Qty AndAlso col.Index <> grdEnm.TotalQty AndAlso col.Index <> grdEnm.Rate AndAlso col.Index <> grdEnm.LotNo AndAlso col.Index <> grdEnm.Rack_No AndAlso col.Index <> grdEnm.Comments Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
            Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
            'Task:2759 Set Rounded Amount Format
            Me.grd.RootTable.Columns(grdEnm.Total).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdEnm.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            'End Task:2759
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(grdSaved.CurrentRow.Cells(0).Value))
            Return New StockDAL().Delete(StockMaster)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Dim transId As Integer = 0
            'transId = Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            StockMaster = New StockMaster
            StockMaster.StockTransId = 0I 'transId
            StockMaster.DocNo = Me.txtPONo.Text.ToString.Replace("'", "''")
            StockMaster.DocDate = Me.dtpPODate.Value.Date
            StockMaster.DocType = 6 'Convert.ToInt32(GetStockDocTypeId("StoreIssuence").ToString)
            StockMaster.Remaks = Me.txtRemarks.Text.ToString.Replace("'", "''")
            StockMaster.Project = Me.cmbPo.SelectedValue
            StockMaster.AccountId = Me.cmbVendor.Value
            StockMaster.StockDetailList = StockList 'New List(Of StockDetail)
            'For Each grdRow As DataRow In CType(Me.grd.DataSource, DataTable).Rows
            '    StockDetail = New StockDetail
            '    StockDetail.StockTransId = transId 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            '    StockDetail.LocationId = Val(grdRow.Item("LocationId").ToString)
            '    StockDetail.ArticleDefId = Val(grdRow.Item("ItemId").ToString)
            '    StockDetail.InQty = 0
            '    StockDetail.OutQty = IIf(grdRow.Item("Unit").ToString = "Loose", Val(grdRow.Item("Qty").ToString), (Val(grdRow.Item("Qty").ToString) * Val(grdRow.Item("PackQty").ToString)))
            '    StockDetail.Rate = Val(grdRow.Item("Rate").ToString)
            '    StockDetail.InAmount = 0
            '    StockDetail.OutAmount = IIf(grdRow.Item("Unit").ToString = "Loose", (Val(grdRow.Item("Qty").ToString) * Val(grdRow.Item("Rate").ToString)), ((Val(grdRow.Item("Qty").ToString) * Val(grdRow.Item("PackQty").ToString)) * Val(grdRow.Item("Rate").ToString)))
            '    StockDetail.Remarks = String.Empty
            '    StockMaster.StockDetailList.Add(StockDetail)
            'Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                Return New StockDAL().Add(StockMaster)
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
            If IsValidate() = True Then
                Return New StockDAL().Update(StockMaster)
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub grdSaved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.Click
        If grdSaved.RowCount = 0 Then Exit Sub
        If grdSaved.GetRow.Cells("OutwaredGatepass").Value = 0 Then
            OutwardGatepassToolStripMenuItem.Enabled = False
        Else
            OutwardGatepassToolStripMenuItem.Enabled = True
        End If
    End Sub
    Private Sub StoreIssuanceInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StoreIssuanceInvoiceToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("ReturnDispatchNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("ReturnStoreIssuanceInvoice", "{ReturnDispatchMasterTable.ReturnDispatchId}=" & grdSaved.CurrentRow.Cells(6).Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub OutwardGatepassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutwardGatepassToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("StoreIssuanceInwardGatePass", "{ReturnDispatchMasterTable.ReturnDispatchId}=" & grdSaved.CurrentRow.Cells(6).Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.CurrentRow.Delete()
                grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridBarUserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub AddNewCostCenterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewCostCenterToolStripMenuItem.Click
        Try

            Dim id As Integer = 0
            frmAddCostCenter.ShowDialog()
            id = Me.cmbPo.SelectedValue
            FillCombo("SO")
            Me.cmbPo.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        If Val(Me.txtPackQty.Text) = 0 Then
            txtPackQty.Text = 1
            Me.txtTotalQty.Text = Val(txtQty.Text)
            txtTotal.Text = Math.Round(Val(txtTotalQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        Else
            Me.txtTotalQty.Text = (Val(txtQty.Text) * Val(txtPackQty.Text))
            txtTotal.Text = Math.Round((Val(Me.txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStock.TextChanged

    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                If cmbItem.Value Is Nothing Then Exit Sub
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
                'If grd.CurrentRow Is Nothing Then
                '    Exit Sub
                'End If
                'If Me.grd.CurrentRow IsNot Nothing Then
                If getConfigValueByType("ArticleShowImageOnStoreIssuance").ToString = "True" Then
                    Me.PictureBox.ImageLocation = GetPicture(Me.cmbItem.Value)
                End If
                'End If
                FillCombo("ArticlePack")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged, rdoName.CheckedChanged
        Try
            If Not Mode = "Normal" Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If rdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPONo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPONo.TextChanged

    End Sub

    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
        Try


        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Return Store Issuance"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("RI" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "ReturnDispatchMasterTable", "ReturnDispatchNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("RI" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "ReturnDispatchMasterTable", "ReturnDispatchNo")
            Else
                Return GetNextDocNo("RI", 6, "ReturnDispatchMasterTable", "ReturnDispatchNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            frmAddItem.ShowDialog()
            FillCombo("Item")
        Catch ex As Exception

        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function

        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmStoreIssuence' AND EmailAlert=1")
            If dtForm.Rows.Count > 0 Then
                flg = True
            Else
                flg = False
            End If
            If flg = True Then
                Email = New Email
                Email.ToEmail = AdminEmail
                Email.CCEmail = String.Empty
                Email.BccEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text.ToString
                Email.Attachment = SourceFile
                Email.Subject = "Store Issuence " & setVoucherNo & ""
                'Email.Body = "Store Issuence " _
                '& " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Dim strBody As String = String.Empty
                Dim str As String = String.Empty
                strBody += StringFixedLength("Article Description", 50) & " " & StringFixedLength("Qty", 10) & " " & StringFixedLength("Price", 10) & " " & StringFixedLength("Amount", 10) & Chr(10)
                For Each r As DataRow In dtDataEmail.Rows
                    If Val(r("Qty").ToString) > 0 Then
                        strBody += StringFixedLength(r.Item("Item").ToString, 50) & " " & StringFixedLength(Math.Round(Val(r.Item("Qty").ToString), 2), 10) & " " & StringFixedLength(Math.Round(Val(r.Item("Rate").ToString), 2), 10) & " " & StringFixedLength(Math.Round(Val(r.Item("Total").ToString), 2), 10) & Chr(10)
                    End If
                Next
                Email.Body = strBody
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function
    'Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
    '    Try
    '        If Me.SplitContainer1.Panel1Collapsed = True Then
    '            Me.SplitContainer1.Panel1Collapsed = False
    '        Else
    '            Me.SplitContainer1.Panel1Collapsed = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal ReturnDispatchNo As String)
        Try
            Get_All = Nothing
            If IsFormOpen = True Then
                If ReturnDispatchNo.Length > 0 Then
                    'Dim str As String = "Select * From ReturnDispatchMasterTable WHERE ReturnDispatchNo=N'" & ReturnDispatchNo & "'"
                    'Dim dt As DataTable = GetDataTable(str)
                    'If dt IsNot Nothing Then
                    '    If dt.Rows.Count > 0 Then
                    '        'IsEditMode = True
                    '        'FillCombo("Vendor")
                    '        'FillCombo("CostCenter")
                    '        If Not Mode = "Normal" Then Me.txtBarcodescan.Focus()
                    '        Me.txtReceivingID.Text = dt.Rows(0).Item("ReturnDispatchId")
                    '        Me.txtPONo.Text = dt.Rows(0).Item("ReturnDispatchNo")
                    '        Me.dtpPODate.Value = dt.Rows(0).Item("ReturnDispatchDate")
                    '        Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                    '        'Me.cmbLocation.SelectedValue = dt.Rows(0).Item("VendorId")
                    '        Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                    '        Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")
                    '        Me.cmbPo.SelectedValue = dt.Rows(0).Item("PurchaseOrderID")
                    '        If IsDBNull(dt.Rows(0).Item("PlanId")) Then
                    '            IIf(Me.cmbPlan.SelectedIndex = -1, Me.cmbPlan.SelectedIndex - 1, Me.cmbPlan.SelectedIndex = 0)
                    '        Else
                    '            Me.cmbPlan.SelectedValue = dt.Rows(0).Item("PlanId")
                    '        End If
                    '        If IsDBNull(dt.Rows(0).Item("PlanId")) Then
                    '            IIf(Me.cmbPlan.SelectedIndex = -1, Me.cmbPlan.SelectedIndex - 1, Me.cmbPlan.SelectedIndex = 0)
                    '        Else
                    '            Me.cmbPlan.SelectedValue = dt.Rows(0).Item("PlanId")
                    '        End If
                    '        If IsDBNull(dt.Rows(0).Item("CylinderSecurityAccountId")) Then
                    '            Me.cmbSecurity.Value = 0
                    '        Else
                    '            Me.cmbSecurity.Value = dt.Rows(0).Item("CylinderSecurityAccountId")
                    '        End If
                    '        If IsDBNull(dt.Rows(0).Item("FixedAssetAccountId")) Then
                    '            Me.cmbFixedAssets.Value = 0
                    '        Else
                    '            Me.cmbFixedAssets.Value = dt.Rows(0).Item("FixedAssetAccountId")
                    '        End If

                    '        'Me.chkPost.Checked = dt.Rows(0).Item("Post")
                    '        'Me.txtVhNo.Text = dt.Rows(0).Item("Vehicle_No")
                    '        'Me.txtDriverName.Text = dt.Rows(0).Item("Driver_Name")
                    '        'If Not IsDBNull(dt.Rows(0).Item("Dcdate")) Then
                    '        '    Me.dtpDcDate.Value = dt.Rows(0).Item("Dcdate")
                    '        '    Me.dtpDcDate.Checked = True
                    '        'Else
                    '        '    Me.dtpDcDate.Value = Date.Today
                    '        '    Me.dtpDcDate.Checked = False
                    '        'End If
                    '        'Me.txtInvoiceNo.Text = dt.Rows(0).Item("Vendor_Invoice_No")
                    '        'Me.cmbProject.SelectedValue = dt.Rows(0).Item("CostCenterId")
                    '        DisplayDetail(Val(Me.txtReceivingID.Text))
                    '        'If Me.cmbPo.SelectedValue > 0 Then
                    '        '    Me.cmbVendor.Enabled = False
                    '        'Else
                    '        '    Me.cmbVendor.Enabled = True
                    '        'End If
                    '        Me.btnsave.Text = "&Update"
                    '        Me.GetSecurityRights()
                    '        Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
                    '        'VoucherDetail(dt.Rows(0).Item("ReceivingId"))
                    '        IsDrillDown = True
                    '        Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
                    '        If flgDateLock = True Then
                    '            If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                    '                'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    '                Me.dtpPODate.Enabled = False
                    '            Else
                    '                Me.dtpPODate.Enabled = True
                    '            End If
                    '        Else
                    '            Me.dtpPODate.Enabled = True
                    '        End If
                    '    Else
                    '        Exit Function
                    '    End If
                    'Else
                    '    Exit Function
                    'End If
                    IsDrillDown = True
                    If Me.grdSaved.RowCount <= 50 Then
                        blnDisplayAll = True
                        Me.btnSearchLoadAll_Click(Nothing, Nothing)
                        blnDisplayAll = False
                    End If
                    Dim flag As Boolean = False
                    flag = Me.grdSaved.Find(Me.grdSaved.RootTable.Columns("ReturnDispatchNo"), Janus.Windows.GridEX.ConditionOperator.Equal, (ReturnDispatchNo).Trim, -1, 1)

                    If flag = True Then
                        Me.grdSaved_CellDoubleClick(Nothing, Nothing)
                    Else
                        Exit Function
                    End If
                End If
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            OpenToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            DeleteToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLoadAll.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            DisplayRecord("All")
            DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub StoreIssuenceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StoreIssuenceToolStripMenuItem.Click
        Try
            StoreIssuanceInvoiceToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OutwardGatepassToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutwardGatepassToolStripMenuItem1.Click
        Try
            OutwardGatepassToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDocument.Click
        Try
            If Not Me.cmbSearchLocation.Items.Count > 0 Then
                FillCombo("SearchLocation")
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            Else
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            End If
            If Not Me.cmbSearchCostCenter.Items.Count > 0 Then
                FillCombo("SearchCostCenter")
                If Not Me.cmbSearchCostCenter.SelectedIndex = -1 Then Me.cmbSearchCostCenter.SelectedIndex = 0
            Else
                If Not Me.cmbSearchCostCenter.SelectedIndex = -1 Then Me.cmbSearchCostCenter.SelectedIndex = 0
            End If

            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                ''TFS1823
                Me.CtrlGrdBar1.Visible = False
                Me.CtrlGrdBar2.Visible = True
                DisplayRecord()
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Else
                If IsEditMode = False Then Me.btnPrint.Visible = False
                If IsEditMode = False Then Me.btnDelete.Visible = False
                Me.btnEdit.Visible = False
                ''TFS1823
                Me.CtrlGrdBar1.Visible = True
                Me.CtrlGrdBar2.Visible = False
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            'If IsEmailAlert = True Then
            '    If IsAttachmentFile = True Then
            '        crpt.Load(str_ApplicationStartUpPath & "\Reports\StoreIssuanceInvoice.rpt", DBServerName)
            '        If DBUserName <> "" Then
            '            crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
            '            crpt.DataSourceConnections.Item(0).SetLogon(DBName, DBPassword)
            '        Else
            '            crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
            '        End If

            '        Dim ConnectionInfo As New ConnectionInfo
            '        With ConnectionInfo
            '            .ServerName = DBServerName
            '            .DatabaseName = DBName
            '            If DBUserName <> "" Then
            '                .UserID = DBUserName
            '                .Password = DBPassword
            '                .IntegratedSecurity = False
            '            Else
            '                .IntegratedSecurity = True
            '            End If
            '        End With
            '        Dim tbLogOnInfo As New TableLogOnInfo
            '        For Each dt As Table In crpt.Database.Tables
            '            tbLogOnInfo = dt.LogOnInfo
            '            tbLogOnInfo.ConnectionInfo = ConnectionInfo
            '            dt.ApplyLogOnInfo(tbLogOnInfo)
            '        Next
            '        crpt.RecordSelectionFormula = "{ReturnDispatchMasterTable.ReturnDispatchId}=" & VoucherId



            '        Dim crExportOps As New ExportOptions
            '        Dim crDiskOps As New DiskFileDestinationOptions
            '        Dim crExportType As New PdfRtfWordFormatOptions


            '        If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
            '            IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
            '        Else
            '        End If
            '        FileName = String.Empty
            '        FileName = "Store Issuence" & "-" & setVoucherNo & ""
            '        SourceFile = String.Empty
            '        SourceFile = _FileExportPath & "\" & FileName & ".pdf"
            '        crDiskOps.DiskFileName = SourceFile
            '        crExportOps = crpt.ExportOptions
            '        With crExportOps
            '            .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            '            .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            '            .ExportDestinationOptions = crDiskOps
            '            .ExportFormatOptions = crExportType
            '        End With
            '        crpt.Refresh()
            '        Try
            '            crpt.SetParameterValue("@CompanyName", CompanyTitle)
            '            crpt.SetParameterValue("@CompanyAddress", CompanyAddHeader)
            '            crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
            '        Catch ex As Exception
            '            'IsCompanyInfo = False
            '            'CompanyTitle = String.Empty
            '            'CompanyAddHeader = String.Empty
            '        End Try
            '        crpt.Export(crExportOps)

            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            EmailSave()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            ExportFile(getVoucher_Id)
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetPicture(ByVal articleid As Integer) As String
        Try
            Dim str As String = String.Empty
            str = "SELECT     dbo.ArticleDefTableMaster.ArticlePicture" _
            & " FROM         dbo.ArticleDefTable INNER JOIN" _
            & " dbo.ArticleDefTableMaster ON dbo.ArticleDefTable.MasterID = dbo.ArticleDefTableMaster.ArticleId WHERE dbo.ArticleDefTable.ArticleId = " & articleid & ""
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    _strImagePath = dt.Rows(0).Item(0).ToString
                Else
                    _strImagePath = Nothing
                End If
            Else
                _strImagePath = Nothing
            End If
            Return _strImagePath
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(Nothing, Nothing)
        '    Exit Sub
        'End If
    End Sub
    Private Sub grd_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.SelectionChanged
        Try
            If grd.CurrentRow Is Nothing Then
                Exit Sub
            End If
            If Me.grd.CurrentRow IsNot Nothing Then
                If getConfigValueByType("ArticleShowImageOnStoreIssuance").ToString = "True" Then
                    Me.PictureBox.ImageLocation = GetPicture(Me.grd.GetRow.Cells("ItemId").Value)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt").ToString("yyyy-M-d 00:00:00") Then
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

    'Private Sub cmbPlan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPlan.SelectedIndexChanged
    '    Try
    '        If IsFormOpen = True Then
    '            If Me.cmbPlan.SelectedIndex = -1 Then Exit Sub
    '            FillCombo("CostSheetItem")
    '            If IsEditMode = False Then Call DisplayPODetail(Me.cmbPlan.SelectedValue, "Plan")
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim frm As New frmPlanList
            If Me.cmbPlan.SelectedIndex = -1 Then Exit Sub
            frm.PlanId = Me.cmbPlan.SelectedValue
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                FillCombo("CostSheetItem")
                If IsEditMode = False Then Call DisplayPODetail(Me.cmbPlan.SelectedValue, "Plan")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPlan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try
            If IsFormOpen = True Then
                'Before against task:2412
                'If StoreIssuanceDependonProductionPlan = True Then
                'Task:2412 Change Validation 
                If Not Me.cmbPlan.SelectedIndex = -1 Then
                    'End Task:2412
                    FillCombo("CostSheetItem")
                    FillCombo("Ticket")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox.Click

    End Sub

    Private Sub txtCostSheetQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub cmbCostSheetItems_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs)
        'Try
        '    If Me.cmbCostSheetItems.ActiveRow Is Nothing Then Exit Sub
        '    Me.txtPlanQty.Text = Val(Me.cmbCostSheetItems.ActiveRow.Cells("Plan Qty").Text.ToString)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtBarcodescan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarcodescan.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                Me.ErrorProvider1.Clear()
                Me.lblError.Text = ""
                If Me.txtBarcodescan.Text.Length = 0 Then Exit Sub
                lblError.Text = String.Empty
                If Me.cmbItem.Focused Then
                    If Me.cmbItem.ActiveRow.Index = 0 Then Exit Sub
                    Me.txtBarcodescan.Text = Me.cmbItem.ActiveRow.Cells("ItemId").Value
                    Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.CloseDropdown)
                End If

                If Me.cmbItem.IsItemInList(Me.txtBarcodescan.Text) Then
                    'Me.cmbCategory.SelectedValue = objDataSet.Tables(0).Rows(i)("ArticleGroupId")
                    'Me.cmbCategory_SelectedIndexChanged(Nothing, Nothing)

                    ''selecting the item
                    ' Me.cmbItem.Value = Me.txtBarcode.Text
                    Me.cmbItem.Text = Me.cmbItem.Text
                    'me.cmbItem.ActiveRow=me.cmbItem.Rows(me.cmbItem.S               'Me.cmbItem.Text = Me.txtBarcode.Text
                    'Me.cmbItem.Select()

                    'Me.cmbItem.SelectedRow=me
                    Me.cmbItem_Leave(Nothing, Nothing)

                    ''selecting batch no
                    ' Me.cmbBatchNo.Text = objDataSet.Tables(0).Rows(i)("BatchNo")
                    'Me.cmbBatchNo_Leave(Nothing, Nothing)

                    ''selecting unit
                    Me.cmbUnit.SelectedIndex = 0
                    Me.cmbUnit_SelectedIndexChanged(Nothing, Nothing)

                    ''selecting qty
                    Me.txtQty.Text = 1
                    txtPackQty.Text = "1"
                    Me.txtQty_LostFocus(Nothing, Nothing)
                    'Me.txtQty_TextChanged(Nothing, Nothing)

                    ''selecing rate
                    'Me.txtRate.Text = objDataSet.Tables(0).Rows(i)(4)

                    Me.txtTotal.Text = Val(Me.txtRate.Text) * Val(Me.txtQty.Text)

                    Me.btnAdd_Click(Nothing, Nothing)


                    'lblError.Text = "Code Found"
                    Me.txtBarcodescan.Text = String.Empty
                    Me.txtBarcodescan.Focus()

                Else
                    Me.txtBarcodescan.Focus()
                    Me.txtBarcodescan.SelectAll()
                    lblError.Text = "Invalid code"
                    Me.ErrorProvider1.SetError(Me.txtBarcodescan, "Invalid Code")
                    'Spech.SelectVoice("Error")
                    'Spech.SpeakAsync("Try again")

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetItemIdByBarCode(ByVal Item As String) As Integer
        Try
            If Not Item.Contains("|") Then Return 0
            Dim intItem As Integer = 0I
            If IsNumeric(Item.Substring(0, Item.LastIndexOf("|"))) Then
                intItem = Item.Substring(0, Item.LastIndexOf("|"))
                Return intItem
            Else
                Return 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub btnBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarcode.Click
        Try
            If getConfigValueByType("BarcodeEnabled") = "True" Then
                If Me.Panel1.Visible = True Then
                    Mode = "Normal"
                    Me.Panel1.Visible = False
                    Me.cmbItem.Focus()
                Else
                    Mode = "BarcodeEnabled"
                    Me.Panel1.Visible = True
                    Me.txtBarcodescan.Focus()
                End If
            Else
                Mode = "Normal"
                Me.Panel1.Visible = False
                Me.cmbItem.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmStoreIssuence_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If getConfigValueByType("BarcodeEnabled") = "True" Then
                If e.KeyCode = Keys.OemQuestion Then
                    Me.txtBarcodescan.Focus()
                    e.SuppressKeyPress = True
                End If
            End If
            If e.KeyCode = Keys.F1 Then
                btnTicketSearch_Click(Nothing, Nothing)
            End If
            'R-974 Ehtisham ul Haq user friendly system modification on 20 -1-14
            If e.KeyCode = Keys.F4 Then
                If btnsave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.btnsave.Text = "&Update" Then
                    If Me.btnsave.Enabled = False Then
                        RemoveHandler Me.btnsave.Click, AddressOf Me.SaveToolStripButton_Click
                    End If
                End If
            End If
            If e.KeyCode = Keys.D AndAlso e.Alt Then
                If Me.btnsave.Text = "&Delete" Then
                    If Me.btnDelete.Enabled = False Then
                        RemoveHandler Me.btnDelete.Click, AddressOf Me.DeleteToolStripButton_Click
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 28-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    '' 04-Jul-2014 TASK:2716 Imran Ali Selected Store Issuance Update 
    Private Sub btnSelectedIssuenceUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectedIssuenceUpdate.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Me.grdSaved.GetCheckedRows IsNot Nothing Then

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()

                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                    Me.grdSaved.Row = r.RowIndex
                    EditRecord()
                    Update_Record()
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    'End Task:2716
    Private Sub cmbIssuence_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIssuence.Leave
        Try
            If cmbIssuence.ActiveRow Is Nothing Then Exit Sub
            If cmbIssuence.IsItemInList = False Then Exit Sub


            If IsEditMode = True Then Exit Sub
            Me.txtPO_No.Text = Me.cmbIssuence.ActiveRow.Cells("PONo").Value.ToString
            Me.cmbPlan.SelectedValue = Val(Me.cmbIssuence.ActiveRow.Cells("PlanId").Value.ToString)
            Me.cmbDepartment.SelectedValue = Val(Me.cmbIssuence.ActiveRow.Cells("DepartmentId").Value.ToString)
            Me.cmbCGAccount.Value = Val(Me.cmbIssuence.ActiveRow.Cells("StoreIssuanceAccountId").Value.ToString)
            Me.cmbVendor.Value = Val(Me.cmbIssuence.ActiveRow.Cells("VendorId").Value.ToString)
            Me.cmbPo.SelectedValue = Val(Me.cmbIssuence.ActiveRow.Cells("PurchaseOrderId").Value.ToString)
            Me.ComboBox1.SelectedValue = Val(Me.cmbIssuence.ActiveRow.Cells("EmpID").Value.ToString)
            Me.cmbFixedAssets.Value = Val(Me.cmbIssuence.ActiveRow.Cells("FixedAssetAccountId").Value.ToString)
            Me.cmbSecurity.Value = Val(Me.cmbIssuence.ActiveRow.Cells("CylinderSecurityAccountId").Value.ToString)
            Me.txtRemarks.Text = Me.cmbIssuence.ActiveRow.Cells("Remarks").Value.ToString
            DisplayPODetail(Me.cmbIssuence.Value, "Issuence")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'TASK-TFS-46 Get RawMaterial Price
    Public Sub GetRawMaterialPrice(ArticleId As Integer)
        Try
            Dim dtprice As New DataTable
            dtprice = GetCostPriceForRawMaterial(Val(ArticleId))
            dtprice.AcceptChanges()
            Dim dblCostPrice As Decimal = 0D
            Dim dblPurchasePrice As Decimal = 0D
            Dim dblPrice As Decimal = 0D
            If dtprice.Rows.Count > 0 Then
                dblCostPrice = Val(dtprice.Rows(0).Item("CostPrice").ToString)
                dblPurchasePrice = Val(dtprice.Rows(0).Item("PurchasePrice").ToString)
            End If
            If getConfigValueByType("AvgRate").ToString = "True" Then
                dblPrice = dblCostPrice
            Else
                dblPrice = dblPurchasePrice
            End If
            If dblPrice = 0 Then
                dblPrice = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            End If
            Me.txtRate.Text = dblPrice
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'END TASK-TFS-46

    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged

    End Sub

    Private Sub txtTotalQty_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQty.TextChanged
        Try
            txtTotal.Text = Math.Round((Val(Me.txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_TextChanged_1(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        Try
            Me.txtTotal.Text = Math.Round(Val(txtTotalQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grd.UpdateData()
            If e.Column.Index = grdEnm.Qty Or e.Column.Index = grdEnm.PackQty Then
                If Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(grdEnm.TotalQty).Value = (Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) * Val(Me.grd.GetRow.Cells(grdEnm.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grd.GetRow.Cells(grdEnm.TotalQty).Value = Val(Me.grd.GetRow.Cells(grdEnm.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = grdEnm.TotalQty Then
                If Not Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(grdEnm.Qty).Value = Val(Me.grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.Qty).Value
                End If
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            GetGridDetailQtyCalculate(e) ''TASK-408
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbTicketNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicketNo.SelectedIndexChanged
        Try
            If Me.cmbTicketNo.SelectedValue > 0 Then
                'FillCombo("TicketIssuance")
                FillCombo("Department")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        Try
            If cmbDepartment.SelectedValue > 0 Then
                'Me.cmbCGAccount.Value = CType(Me.cmbTicketIssuance.SelectedItem, DataRowView).Item("StoreIssuanceAccountId")
                Me.grd.DataSource = New ConsumptionMasterDAL().GetToReturnIssuance(Me.cmbDepartment.SelectedValue, Me.cmbTicketNo.SelectedValue, CostSheetType)
                IsStoreIssuance = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicketIssuance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicketIssuance.SelectedIndexChanged
        Try
            ''TASK:920
            If cmbTicketIssuance.SelectedValue > 0 Then
                'Me.cmbCGAccount.Value = CType(Me.cmbTicketIssuance.SelectedItem, DataRowView).Item("StoreIssuanceAccountId")
                'FillCombo("Department")
            End If
            ''END TASK:920
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        Try
            ''TASK: TFS1133 Qty should not exceed issuance quantity.
            If Me.grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.grd.UpdateData()
                If IsStoreIssuance = True Then
                    Me.grd.UpdateData()
                    If Me.grd.GetRow.Cells("Qty").Value > Me.grd.GetRow.Cells("CheckQty").Value Then
                        msg_Error("Quantity exceeds issued quantity.")
                        Me.grd.GetRow.Cells("Qty").Value = Me.grd.GetRow.Cells("CheckQty").Value
                        Me.grd.GetRow.Cells("TotalQty").Value = Me.grd.GetRow.Cells("CheckQty").Value
                    End If
                End If
                If IsEditMode = True AndAlso Me.grd.GetRow.Cells("EstimationId").Value > 0 Then
                    Dim RemainingQty As Double = 0
                    RemainingQty = GetIssuedQty(Me.grd.GetRow.Cells("DepartmentId").Value, Me.grd.GetRow.Cells("ItemId").Value, Me.grd.GetRow.Cells("EstimationId").Value)
                    If Me.grd.GetRow.Cells("Qty").Value > (Me.grd.GetRow.Cells("CheckQty").Value + RemainingQty) Then
                        'Dim Sum As Double = Me.grd.GetRow.Cells("Qty").Value - CheckQty
                        'If Sum > RemainingQty Then
                        msg_Error("Quantity exceeds issued quantity.")
                        Me.grd.GetRow.Cells("Qty").Value = Me.grd.GetRow.Cells("CheckQty").Value
                        Me.grd.GetRow.Cells("TotalQty").Value = Me.grd.GetRow.Cells("CheckQty").Value
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetIssuedQty(ByVal DepartmentId As Integer, ByVal ArticleId As Integer, ByVal EstimationId As Integer) As Double
        Dim Str As String = ""
        Dim Qty As Double = 0
        Try
            Str = "Select IsNull(Qty, 0)-(IsNull(ReturnedTotalQty, 0)+IsNull(ConsumedQty, 0)) As Qty From DispatchDetailTable  " _
                 & " Where EstimationId = " & EstimationId & " And ArticleDefId = " & ArticleId & " And IsNull(SubDepartmentID, 0) = " & DepartmentId & ""
            Dim dt As DataTable = GetDataTable(Str)
            If dt.Rows.Count > 0 Then
                Qty = Val(dt.Rows(0).Item("Qty").ToString)
            End If
            Return Qty
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grd_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellValueChanged
        Try
            CheckQty = Me.grd.GetRow.Cells("Qty").Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblLoad_Click(sender As Object, e As EventArgs) Handles lblLoad.Click
        Try
            If cmbTicketNo.SelectedValue > 0 Then
                'Me.cmbCGAccount.Value = CType(Me.cmbTicketIssuance.SelectedItem, DataRowView).Item("StoreIssuanceAccountId")
                Me.grd.DataSource = New ConsumptionMasterDAL().GetIssuanceToReturnIssuanceWithTicket(Me.cmbTicketNo.SelectedValue, CostSheetType)
                IsStoreIssuance = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPrintVoucher_Click(sender As Object, e As EventArgs) Handles btnPrintVoucher.Click
        Try
            GetVoucherPrint(Me.grdSaved.CurrentRow.Cells("ReturnDispatchNo").Value.ToString, Me.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintVoucher1_Click(sender As Object, e As EventArgs) Handles btnPrintVoucher1.Click
        Try
            GetVoucherPrint(Me.grdSaved.CurrentRow.Cells("ReturnDispatchNo").Value.ToString, Me.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    'Public Sub fillReturnStoreIssuenceGrid(ByVal dt As DataTable, ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal DepartmentId As Integer, Optional ByVal IsWIPAccount As Boolean = False, Optional ByVal CostCenterId As Integer = 0)
    '    Try
    '        Dim frm As New frmReturnTicketProducts(True, False, Me.cmbCategory.SelectedValue)

    '        frm.ShowDialog()

    '    Catch ex As Exception

    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Sub

    Public Sub fillReturnStoreIssuenceGrid(ByVal dt As DataTable, ByVal PlanId As Integer, ByVal TicketId As Integer, ByVal DepartmentId As Integer, Optional ByVal IsWIPAccount As Boolean = False, Optional ByVal CostCenterId As Integer = 0)
        Try
            Dim _dt As DataTable

            _dt = CType(Me.grd.DataSource, DataTable).Clone()
            _dt.Merge(dt)

            'For Each row As DataRow In _dt.Rows

            '    cmbPlan.SelectedValue = Val(row.Item("PlanId").ToString)

            'Next

            Me.grd.DataSource = _dt
            cmbPlan.SelectedValue = PlanId
            cmbTicketNo.SelectedValue = TicketId
            cmbDepartment.SelectedValue = DepartmentId
            Me.cmbPo.SelectedValue = CostCenterId
            'Me.IsWIPAccount = IsWIPAccount
            'Me.grd.UpdateData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lblLoad_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblLoad.LinkClicked

    End Sub

    Private Sub btnTicketSearch_Click(sender As Object, e As EventArgs) Handles btnTicketSearch.Click
        Try
            Dim frm As New frmReturnTicketProducts(True, False, Me.cmbCategory.SelectedValue)
            frm.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Return Store Issuance"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
