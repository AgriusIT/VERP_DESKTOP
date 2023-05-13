''TASK TFS3577 done by Muhammad Amin to effect TotalQty values in case Qty value is changed in Input grid. Dated 20-06-2018
''TASK TFS3578 done by Muhammad Amin 'TotalQty column should be editable but NetAmount column should not be editable'. Dated 20-06-2018
''TASK TFS3577 done by Muhammad Amin to get Qty calculated in Input grid according to their finish good items detail. Dated 21-06-2018
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Public Class frmProductionOrder
    Implements IGeneral
    Dim ProductionOrderId As Integer = 0
    Dim POrder As ProductionOrderBE
    Dim _IsEditMode As Boolean = False
    Private Const Finish As String = "Finish"
    Private Const ByProduct As String = "ByProduct"
    Dim blnCGAccountOnStoreIssuance As Boolean = False
    Dim ItemWiseCGSAccountId As Integer = 0I
    Dim IsFormOpened As Boolean = False
    Dim BOMList As New List(Of Integer)

    'Task 3420 DefineStorIssuence Attributes

    Dim txtPONo As String
    Dim setVoucherNo As String = String.Empty
    Dim IsWIPAccount As Boolean = False
    Dim flgStoreIssuenceVoucher As Boolean = False
    Dim StockList As List(Of StockDetail)
    Dim StockDetail As StockDetail
    Dim flgCompanyRights As Boolean = False
    Dim setVoucherdate As DateTime
    Dim DispatchId As Integer = 0

    'Task 3420 DefineProductionEntry Attributes
    Dim txtProductionNo As String
    Dim ProductionId As Integer = 0
    Dim ProductionMaster As ProductionMaster
    Dim ProductionDetail As ProductionDetail


    'Task 3394 Add new boolean value to check update first time as well as second time'
    Dim LoadBOMUpdate As Boolean = False

    Public Sub New()
        ' This call is required by the designer.

        InitializeComponent()
        IsFormOpened = True
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal IsEditMode As Boolean, Optional ByVal Obj As ProductionOrderBE = Nothing, Optional ByVal HaveSaveRights As Boolean = True, Optional ByVal HaveUpdateRights As Boolean = True)
        Try
            ' This call is required by the designer.
            InitializeComponent()
            _IsEditMode = IsEditMode
            'Task 3394 Set value for LoadBOMUpdate same as IsEditMode
            LoadBOMUpdate = IsEditMode
            ' Add any initialization after the InitializeComponent() call.
            If IsEditMode = True Then
                FillAllCombos()
                ReSetControls()
                EditRecord(Obj)
                'Task 3420 Saad Afzaal get DispatchId and ProductionId at the time of update data 
                Me.DispatchId = Obj.DispatchId
                Me.ProductionId = Obj.Production_Id
                Me.btnSave.Enabled = HaveUpdateRights
            Else
                FillAllCombos()
                ReSetControls()
                DisplayDetail(-1)
                Me.btnSave.Enabled = HaveSaveRights
            End If
            IsFormOpened = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmProductionOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnCancel.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderSize = 0
        'Aashir: Added Smart Search on Alls ultradropdowns
        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        UltraDropDownSearching(cmbCGAccount, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        UltraDropDownSearching(cmbProductProduced, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        UltraDropDownSearching(cmbBOM, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        UltraDropDownSearching(cmbLabourType, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        UltraDropDownSearching(cmbItem1, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        ApplySecurity(SBUtility.Utility.EnumDataMode.New)
    End Sub

    Private Sub frmProductionOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub
    ''' <summary>
    ''' Muhammad Aashir : TFS3246 : Apply right for print 
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.BtnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String = String.Empty
        Try
            If Condition = "Item" Then
                If Not getConfigValueByType("AvgRate").ToString = "True" Then
                    Str = "SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefView.ArticleId)),0) as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterId , ArticleDefView.SortOrder FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId "
                    Str += " where Active=1 "
                Else
                    Str = "SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, Case When IsNull(ArticleDefView.Cost_Price,0) > 0 Then IsNull(ArticleDefView.Cost_Price,0) Else isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefView.ArticleId)),0) End as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterId , ArticleDefView.SortOrder  FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId  "
                    Str += " where Active=1 "
                End If
                'If flgCompanyRights = True Then
                '    Str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                'End If
                'If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                '    If flgLocationWiseItem = True Then
                '        Str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                '    End If
                'End If
                If ItemSortOrder = True Then
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByCode = True Then
                    Str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByName = True Then
                    Str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                Else
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                End If
                FillUltraDropDown(Me.cmbItem, Str)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Column Hidden
                'Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True 'Task:2478 Column Hidden
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True 'Task:2478 Column Hidden
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True

                Me.cmbItem.DisplayLayout.Bands(0).Columns("Category").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Model").Hidden = True

                Me.cmbItem.DisplayLayout.Bands(0).Columns("Rake").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Grade").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Combination").Hidden = True


                If Me.rdoCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            ElseIf Condition = "Item1" Then
                If Not getConfigValueByType("AvgRate").ToString = "True" Then
                    Str = "SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefView.ArticleId)),0) as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterId, ArticleDefView.SortOrder FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId "
                    Str += " where Active=1 "
                Else
                    Str = "SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, Case When IsNull(ArticleDefView.Cost_Price,0) > 0 Then IsNull(ArticleDefView.Cost_Price,0) Else isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefView.ArticleId)),0) End as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterId, ArticleDefView.SortOrder FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId "
                    Str += " where Active=1 "
                End If
                'If flgCompanyRights = True Then
                '    Str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                'End If
                'If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                '    If flgLocationWiseItem = True Then
                '        Str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                '    End If
                'End If
                If ItemSortOrder = True Then
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByCode = True Then
                    Str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByName = True Then
                    Str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                Else
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                End If
                FillUltraDropDown(Me.cmbItem1, Str)
                Me.cmbItem1.Rows(0).Activate()
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Column Hidden
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True 'Task:2478 Column Hidden
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True 'Task:2478 Column Hidden

                Me.cmbItem1.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True


                Me.cmbItem1.DisplayLayout.Bands(0).Columns("Category").Hidden = True
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("Model").Hidden = True
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("Rake").Hidden = True
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("Grade").Hidden = True
                Me.cmbItem1.DisplayLayout.Bands(0).Columns("Combination").Hidden = True


                If Me.rbCode1.Checked = True Then
                    Me.cmbItem1.DisplayMember = Me.cmbItem1.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem1.DisplayMember = Me.cmbItem1.Rows(0).Cells(2).Column.Key.ToString
                End If
            ElseIf Condition = "Category" Then
                Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                         & " Else " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillDropDown(cmbCategory, Str, False)
                FillDropDown(cmbCategory1, Str, False)
                'FillDropDown(cmbCostSheetLocation, str)
            ElseIf Condition = "LabourType" Then
                FillUltraDropDown(Me.cmbLabourType, "Select tblLabourType.Id AS LabourTypeId, tblLabourType.LabourType, ChargeType.Charge As ChargeType, ISNULL(tblLabourType.AccountId, 0) AS AccountId, Account.detail_title AS Account  From tblLabourType LEFT OUTER JOIN ChargeType ON tblLabourType.Id = ChargeType.Id LEFT OUTER JOIN vwCOADetail AS Account ON tblLabourType.AccountId = Account.coa_detail_id")
                Me.cmbLabourType.Rows(0).Activate()
                Me.cmbLabourType.DisplayLayout.Bands(0).Columns("LabourTypeId").Hidden = True
                Me.cmbLabourType.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
            ElseIf Condition = "OverHeadsAccount" Then
                'FillDropDown(Me.cmbAccount, "SELECT coa_detail_id, detail_title AS Account FROM vwCOADetail WHERE Active = 1 ")
                FillDropDown(Me.cmbAccount, "SELECT coa_detail_id, detail_title AS Account FROM vwCOADetail WHERE Active = 1 and account_type = 'Expense'")
            ElseIf Condition = "ProductToBeProduced" Then
                If Not getConfigValueByType("AvgRate").ToString = "True" Then
                    Str = "SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefView.ArticleId)),0) as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterId, ArticleDefView.SortOrder FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId "
                    Str += " where Active=1 "
                Else
                    Str = "SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, Case When IsNull(ArticleDefView.Cost_Price,0) > 0 Then IsNull(ArticleDefView.Cost_Price,0) Else isNull((SELECT isNull(receivingdetailtable.Price,0) FROM receivingdetailtable WHERE receivingdetailtable.receivingdetailId = (SELECT  MAX(receivingdetailtable.receivingdetailId) FROM receivingdetailtable WHERE receivingdetailtable.ArticleDefID = ArticleDefView.ArticleId)),0) End as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks As Rake, Isnull(SubSubId,0) as AccountId, CGSAccountId, Isnull(ServiceItem,0) as ServiceItem, IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], ArticleDefView.MasterId, ArticleDefView.SortOrder FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId "
                    Str += " where Active=1 "
                End If
                'If flgCompanyRights = True Then
                '    Str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                'End If
                'If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                '    If flgLocationWiseItem = True Then
                '        Str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                '    End If
                'End If
                If ItemSortOrder = True Then
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByCode = True Then
                    Str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByName = True Then
                    Str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                Else
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                End If
                FillUltraDropDown(Me.cmbProductProduced, Str)
                Me.cmbProductProduced.Rows(0).Activate()
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Column Hidden
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True 'Task:2478 Column Hidden
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True 'Task:2478 Column Hidden
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True


                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("Category").Hidden = True
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("Model").Hidden = True
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("Rake").Hidden = True
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("Grade").Hidden = True
                Me.cmbProductProduced.DisplayLayout.Bands(0).Columns("Combination").Hidden = True


                If Me.rbCode2.Checked = True Then
                    Me.cmbProductProduced.DisplayMember = Me.cmbProductProduced.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbProductProduced.DisplayMember = Me.cmbProductProduced.Rows(0).Cells(2).Column.Key.ToString
                End If
            ElseIf Condition = "FinishGood" Then
                Str = "SELECT Id AS FinishGoodId, Article.ArticleCode AS Code, Article.ArticleDescription AS Article, StandardNo, ISNULL(FinishGoodMaster.BatchSize, 0) AS BatchSize  FROM FinishGoodMaster LEFT OUTER JOIN ArticleDefTableMaster AS Article ON FinishGoodMaster.MasterArticleId = Article.ArticleId "
                FillUltraDropDown(Me.cmbBOM, Str)
                Me.cmbBOM.Rows(0).Activate()
                Me.cmbBOM.DisplayLayout.Bands(0).Columns("FinishGoodId").Hidden = True
                If Me.rbCode3.Checked = True Then
                    Me.cmbBOM.DisplayMember = Me.cmbBOM.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbBOM.DisplayMember = Me.cmbBOM.Rows(0).Cells(2).Column.Key.ToString
                End If
            ElseIf Condition = "Section" Then
                Str = "SELECT DISTINCT Section, Section FROM FinishGoodMaster WHERE Section <> '' "
                FillDropDown(Me.cmbSection, Str)
            ElseIf Condition = "CGAccount" Then
                FillUltraDropDown(Me.cmbCGAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code] from vwCOADetail where main_type in ('Assets','Expense','Liability')  and detail_title <> ''")
                Me.cmbCGAccount.Rows(0).Activate()
                Me.cmbCGAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "grdInputLocation" Then
                Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                       & " Else " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                Dim dt As DataTable = GetDataTable(Str)
                Me.grdInput.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            ElseIf Condition = "grdOutputLocation" Then
                Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                       & " Else " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                Dim dt As DataTable = GetDataTable(Str)
                Me.grdOutput.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            ElseIf Condition = "ArticlePack" Then
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            ElseIf Condition = "ArticlePack1" Then
                Me.cmbUnit1.ValueMember = "ArticlePackId"
                Me.cmbUnit1.DisplayMember = "PackName"
                Me.cmbUnit1.DataSource = GetPackData(Me.cmbItem.Value)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillAllCombos()
        Try
            FillCombos("Item")
            FillCombos("Item1")
            FillCombos("Category")
            FillCombos("LabourType")
            FillCombos("OverHeadsAccount")
            FillCombos("ProductToBeProduced")
            FillCombos("FinishGood")
            FillCombos("Section")
            FillCombos("CGAccount")
            FillCombos("ArticlePack")
            FillCombos("ArticlePack1")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Dim AccountId2 As Integer = 0
            Dim AccountId As Integer = 0
            If Not getConfigValueByType("PurchaseDebitAccount").ToString = "Error" Then
                AccountId = getConfigValueByType("PurchaseDebitAccount")
            End If
            If Not getConfigValueByType("CGAccountOnStoreIssuance").ToString = "Error" Then
                blnCGAccountOnStoreIssuance = Convert.ToBoolean(getConfigValueByType("CGAccountOnStoreIssuance").ToString)
            End If
            If blnCGAccountOnStoreIssuance = True Then
                AccountId2 = Me.cmbCGAccount.Value
            Else
                If Not getConfigValueByType("CGAccountOnStoreIssuance").ToString = "Error" Then
                    AccountId2 = Val(getConfigValueByType("StoreIssuenceAccount"))
                Else
                    AccountId2 = 0
                End If
            End If
            Dim GLAccountArticleDepartment As Boolean = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            POrder = New ProductionOrderBE()
            POrder.ProductionOrderId = ProductionOrderId
            POrder.ProductionOrderNo = Me.txtProductionOrderNo.Text
            POrder.ProductionOrderDate = Me.dtpProductionDate.Value
            POrder.TicketNo = Me.txtTicketNo.Text
            POrder.BatchNo = Me.txtBatchNo.Text
            POrder.ExpiryDate = Me.dtpExpiryDate.Value
            POrder.ProductId = Me.cmbProductProduced.Value
            POrder.FinishGoodId = Me.cmbBOM.Value
            POrder.BatchSize = Val(Me.txtBatchSize.Text)
            POrder.Section = Me.cmbSection.Text
            POrder.Remarks = Me.txtRemarks.Text
            POrder.CGSAccountId = Me.cmbCGAccount.Value
            POrder.Approved = Me.cbApproved.Checked
            POrder.TotalQuantity = Val(txtTotalQuantity.Text)
            'Task 3420 Set DispatchId and ProductionId Value Saad Afzaal 
            POrder.DispatchId = Me.DispatchId
            POrder.Production_Id = Me.ProductionId
            ''
            ''Voucher entry
            If Me.cbApproved.Checked = True Then
                POrder.Voucher.VoucherNo = POrder.ProductionOrderNo
                POrder.Voucher.VoucherDate = POrder.ProductionOrderDate
                POrder.Voucher.VoucherCode = POrder.ProductionOrderNo
                POrder.Voucher.VNo = POrder.ProductionOrderNo
                POrder.Voucher.Source = Me.Name
                POrder.Voucher.UserName = LoginUserName
                POrder.Voucher.LocationId = 0
                POrder.Voucher.FinancialYearId = 1
                POrder.Voucher.VoucherTypeId = 1
                POrder.Voucher.VoucherMonth = String.Empty
                POrder.Voucher.CoaDetailId = 0
                POrder.Voucher.Post = True
                POrder.Voucher.References = POrder.Remarks
                POrder.Voucher.Posted_UserName = LoginUserName
                POrder.Voucher.ActivityLog = New ActivityLog()
                POrder.Voucher.ActivityLog.FormName = Me.Name
                POrder.Voucher.ActivityLog.FormCaption = "Production Order"
                POrder.Voucher.ActivityLog.User_Name = LoginUserName
                POrder.Voucher.ActivityLog.UserID = LoginUserId
                POrder.Voucher.VoucherDatail = New List(Of VouchersDetail)
                ''Stock entry
                POrder.Stock = New StockMaster()
                POrder.Stock.DocDate = Now
                POrder.Stock.DocNo = POrder.ProductionOrderNo
                POrder.Stock.AccountId = 0
                POrder.Stock.Project = 0
                POrder.Stock.Remaks = POrder.Remarks
                POrder.Stock.StockTransId = 0
                POrder.Stock.DocType = Convert.ToInt32(GetStockDocTypeId("Production"))
                POrder.Stock.StockDetailList = New List(Of StockDetail)
            End If


            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdInput.GetRows
                Dim POrderInput As New ProductionOrderInputMaterialBE()
                POrderInput.ID = Row.Cells("ID").Value
                POrderInput.ProductionOrderId = Row.Cells("ProductionOrderId").Value
                POrderInput.LocationId = Row.Cells("LocationId").Value
                POrderInput.ItemId = Row.Cells("ItemId").Value
                POrderInput.Unit = Row.Cells("Unit").Value.ToString
                POrderInput.PackQty = Row.Cells("PackQty").Value.ToString
                POrderInput.Qty = Val(Row.Cells("Qty").Value.ToString)
                POrderInput.Rate = Val(Row.Cells("Rate").Value.ToString)
                POrderInput.TotalQty = Val(Row.Cells("TotalQty").Value.ToString)
                'Task 3394 Saad Afzaal set value for FinishGoodId from InputMaterial Grid'
                POrderInput.FinishGoodId = Val(Row.Cells("FinishGoodId").Value.ToString)
                POrder.InputList.Add(POrderInput)
                'POrderInput.Total = Row.Cells("TotalQty").Value
                If Me.cbApproved.Checked = True Then
                    If ItemWiseCGSAccount(Val(Row.Cells("MasterArticleId").Value.ToString)) = True AndAlso blnCGAccountOnStoreIssuance = False Then
                        AccountId2 = ItemWiseCGSAccountId
                        If GLAccountArticleDepartment = True Then
                            AccountId = Val(Row.Cells("SubSubId").Value.ToString)
                        End If
                    Else
                        If GLAccountArticleDepartment = True Then
                            AccountId = Val(Row.Cells("SubSubId").Value.ToString)
                            'AccountId2 = Val(dtGrd.Rows(i).Item("CGSAccountId").ToString)
                            'If AccountId2 = 0 Then
                            If blnCGAccountOnStoreIssuance = False Then
                                AccountId2 = Val(Row.Cells("CGSAccountId").Value.ToString)
                            End If
                            'End If
                        End If
                    End If
                    '' Stock entry
                    Dim StockDetail As New StockDetail()
                    StockDetail.LocationId = Val(Row.Cells("LocationId").Value.ToString)
                    StockDetail.Remarks = ""
                    StockDetail.ArticleDefId = Val(Row.Cells("ItemId").Value.ToString)
                    StockDetail.BatchNo = ""
                    StockDetail.Chassis_No = ""
                    StockDetail.Engine_No = ""
                    StockDetail.InQty = 0
                    'StockDetail.InAmount = (Val(LblUnitProduced.Text) * Val(LblPerUnitRate.Text))
                    StockDetail.OutQty = Val(Row.Cells("TotalQty").Value.ToString)
                    StockDetail.OutAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    StockDetail.Rate = Val(Row.Cells("Rate").Value.ToString)
                    StockDetail.InAmount = 0
                    StockDetail.StockTransId = 0
                    POrder.Stock.StockDetailList.Add(StockDetail)
                    ''Voucher debit entry
                    Dim voucherDetail As New VouchersDetail
                    voucherDetail.VoucherId = 0
                    voucherDetail.LocationId = Val(Row.Cells("LocationId").Value.ToString)
                    voucherDetail.CoaDetailId = AccountId2
                    voucherDetail.Comments = " " & Row.Cells("Item").Value.ToString & "( " & Row.Cells("TotalQty").Value & " X " & Row.Cells("Rate").Value & " )" & ""
                    voucherDetail.DebitAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    voucherDetail.CreditAmount = 0
                    voucherDetail.CurrencyDebitAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    voucherDetail.CurrencyCreditAmount = 0
                    voucherDetail.CostCenter = 0
                    voucherDetail.SPReference = String.Empty
                    voucherDetail.Cheque_No = String.Empty
                    voucherDetail.Cheque_Date = Nothing
                    voucherDetail.PayeeTitle = String.Empty
                    voucherDetail.ChequeDescription = String.Empty
                    voucherDetail.contra_coa_detail_id = 0
                    voucherDetail.EmpId = Nothing
                    voucherDetail.CostCenter = 0
                    POrder.Voucher.VoucherDatail.Add(voucherDetail)
                    '' Voucher credit entry
                    Dim voucherDetail1 As New VouchersDetail
                    voucherDetail1.VoucherId = 0
                    voucherDetail1.LocationId = Val(Row.Cells("LocationId").Value.ToString)
                    voucherDetail1.CoaDetailId = AccountId
                    voucherDetail1.Comments = " " & Row.Cells("Item").Value.ToString & "( " & Row.Cells("TotalQty").Value & " X " & Row.Cells("Rate").Value & " )" & ""
                    voucherDetail1.DebitAmount = 0
                    voucherDetail1.CreditAmount = Val(Row.Cells("NetAmount").Value.ToString)

                    voucherDetail1.CurrencyDebitAmount = 0
                    voucherDetail1.CurrencyCreditAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    voucherDetail1.CostCenter = 0
                    voucherDetail1.SPReference = String.Empty
                    voucherDetail1.Cheque_No = String.Empty
                    voucherDetail1.Cheque_Date = Nothing
                    voucherDetail1.PayeeTitle = String.Empty
                    voucherDetail1.ChequeDescription = String.Empty
                    voucherDetail1.contra_coa_detail_id = 0
                    voucherDetail1.EmpId = Nothing
                    voucherDetail1.CostCenter = 0
                    POrder.Voucher.VoucherDatail.Add(voucherDetail1)
                End If
            Next
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdOverHeads.GetRows
                Dim POrderOverHeads As New ProductionOrderOverHeadsBE()
                POrderOverHeads.ID = Row.Cells("ID").Value
                POrderOverHeads.ProductionOrderId = Row.Cells("ProductionOrderId").Value
                POrderOverHeads.AccountId = Row.Cells("AccountId").Value
                POrderOverHeads.Amount = Row.Cells("Amount").Value
                POrder.OverHeadList.Add(POrderOverHeads)

                '' Voucher entry
                If cbApproved.Checked = True Then
                    Dim voucherDetail As New VouchersDetail
                    voucherDetail.VoucherId = 0
                    voucherDetail.LocationId = 1
                    voucherDetail.CoaDetailId = Val(Row.Cells("AccountId").Value.ToString)
                    voucherDetail.Comments = "Production Order : Over heads "
                    voucherDetail.DebitAmount = 0
                    voucherDetail.CreditAmount = Row.Cells("Amount").Value
                    voucherDetail.CurrencyDebitAmount = 0
                    voucherDetail.CurrencyCreditAmount = Row.Cells("Amount").Value
                    voucherDetail.CostCenter = 0
                    voucherDetail.SPReference = String.Empty
                    voucherDetail.Cheque_No = String.Empty
                    voucherDetail.Cheque_Date = Nothing
                    voucherDetail.PayeeTitle = String.Empty
                    voucherDetail.ChequeDescription = String.Empty
                    voucherDetail.contra_coa_detail_id = 0
                    voucherDetail.EmpId = Nothing
                    voucherDetail.CostCenter = 0
                    POrder.Voucher.VoucherDatail.Add(voucherDetail)
                End If
            Next
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdLabourType.GetRows
                Dim POrderLabour As New ProductionOrderLabourBE()
                POrderLabour.ID = Row.Cells("ID").Value
                POrderLabour.ProductionOrderId = Row.Cells("ProductionOrderId").Value
                POrderLabour.LabourTypeId = Row.Cells("LabourTypeId").Value
                POrderLabour.Amount = Row.Cells("Amount").Value
                POrder.LabourList.Add(POrderLabour)

                ''Voucher entry
                If Me.cbApproved.Checked = True Then
                    'contra_coa_detail_id,EmpId
                    Dim voucherDetail As New VouchersDetail
                    voucherDetail.VoucherId = 0
                    voucherDetail.LocationId = 1
                    voucherDetail.CoaDetailId = Val(Row.Cells("AccountId").Value.ToString)
                    voucherDetail.Comments = "Production Order : Labour cost "
                    voucherDetail.DebitAmount = 0
                    voucherDetail.CreditAmount = Row.Cells("Amount").Value
                    voucherDetail.CurrencyDebitAmount = 0
                    voucherDetail.CurrencyCreditAmount = Row.Cells("Amount").Value
                    voucherDetail.CostCenter = 0
                    voucherDetail.SPReference = String.Empty
                    voucherDetail.Cheque_No = String.Empty
                    voucherDetail.Cheque_Date = Nothing
                    voucherDetail.PayeeTitle = String.Empty
                    voucherDetail.ChequeDescription = String.Empty
                    voucherDetail.contra_coa_detail_id = 0
                    voucherDetail.EmpId = Nothing
                    voucherDetail.CostCenter = 0
                    POrder.Voucher.VoucherDatail.Add(voucherDetail)

                End If

            Next
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdOutput.GetRows
                Dim POrderOutput As New ProductionOrderOutputMaterialBE()
                POrderOutput.ID = Row.Cells("ID").Value
                POrderOutput.ProductionOrderId = Row.Cells("ProductionOrderId").Value
                POrderOutput.LocationId = Row.Cells("LocationId").Value
                POrderOutput.ItemId = Row.Cells("ItemId").Value
                POrderOutput.ItemType = Row.Cells("ItemType").Value.ToString
                POrderOutput.Unit = Row.Cells("Unit").Value.ToString
                POrderOutput.PackQty = Row.Cells("PackQty").Value.ToString
                POrderOutput.Qty = Row.Cells("Qty").Value
                POrderOutput.Rate = Row.Cells("Rate").Value
                POrderOutput.TotalQty = Row.Cells("TotalQty").Value
                POrder.OutputList.Add(POrderOutput)
                'POrderInput.Total = Row.Cells("TotalQty").Value
                If Me.cbApproved.Checked = True Then
                    ' Voucher detail entry 
                    Dim voucherDetail As New VouchersDetail
                    voucherDetail.VoucherId = 0
                    voucherDetail.LocationId = 1
                    voucherDetail.CoaDetailId = Val(Row.Cells("SubSubId").Value.ToString)
                    voucherDetail.Comments = " " & Row.Cells("Item").Value.ToString & "( " & Row.Cells("TotalQty").Value & " X " & Row.Cells("Rate").Value & " )" & ""
                    'If rbFinish.Checked = True Then
                    voucherDetail.DebitAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    voucherDetail.CreditAmount = 0

                    voucherDetail.CurrencyDebitAmount = voucherDetail.DebitAmount
                    voucherDetail.CurrencyCreditAmount = 0
                    'Else
                    '    voucherDetail.DebitAmount = 0
                    '    voucherDetail.CreditAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    'End If
                    voucherDetail.CostCenter = 0
                    voucherDetail.SPReference = String.Empty
                    voucherDetail.Cheque_No = String.Empty
                    voucherDetail.Cheque_Date = Nothing
                    voucherDetail.PayeeTitle = String.Empty
                    voucherDetail.ChequeDescription = String.Empty
                    voucherDetail.contra_coa_detail_id = 0
                    voucherDetail.EmpId = Nothing
                    voucherDetail.CostCenter = 0
                    POrder.Voucher.VoucherDatail.Add(voucherDetail)
                    ' Voucher detail entry 1
                    'Dim voucherDetail1 As New VouchersDetail
                    'voucherDetail1.VoucherId = 0
                    'voucherDetail1.LocationId = 1
                    'voucherDetail1.CoaDetailId = Me.cmbCGAccount.Value
                    'voucherDetail1.Comments = " " & Row.Cells("Item").Value.ToString & "( " & Row.Cells("TotalQty").Value & " X " & Row.Cells("Rate").Value & " )" & ""
                    'If rbFinish.Checked = True Then
                    '    voucherDetail1.DebitAmount = 0
                    '    voucherDetail1.CreditAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    'Else
                    '    voucherDetail1.DebitAmount = Val(Row.Cells("NetAmount").Value.ToString)
                    '    voucherDetail1.CreditAmount = 0
                    'End If
                    'voucherDetail1.CostCenter = 0
                    'voucherDetail1.SPReference = String.Empty
                    'voucherDetail1.Cheque_No = String.Empty
                    'voucherDetail1.Cheque_Date = Nothing
                    'voucherDetail1.PayeeTitle = String.Empty
                    'voucherDetail1.ChequeDescription = String.Empty
                    'voucherDetail1.contra_coa_detail_id = 0
                    'voucherDetail1.EmpId = Nothing
                    'voucherDetail1.CostCenter = 0
                    'POrder.Voucher.VoucherDatail.Add(voucherDetail)
                End If
            Next
            '' Voucher entry
            If cbApproved.Checked = True Then
                Dim voucherDetail As New VouchersDetail
                voucherDetail.VoucherId = 0
                voucherDetail.LocationId = 1
                voucherDetail.CoaDetailId = AccountId2
                voucherDetail.Comments = "Production Order : Difference "
                voucherDetail.DebitAmount = 0
                voucherDetail.CreditAmount = (Me.grdOutput.GetTotal(Me.grdOutput.RootTable.Columns("NetAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - (Me.grdOverHeads.GetTotal(Me.grdOverHeads.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdLabourType.GetTotal(Me.grdLabourType.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))
                voucherDetail.CurrencyDebitAmount = 0
                voucherDetail.CurrencyCreditAmount = voucherDetail.CreditAmount
                voucherDetail.CostCenter = 0
                voucherDetail.SPReference = String.Empty
                voucherDetail.Cheque_No = String.Empty
                voucherDetail.Cheque_Date = Nothing
                voucherDetail.PayeeTitle = String.Empty
                voucherDetail.ChequeDescription = String.Empty
                voucherDetail.contra_coa_detail_id = 0
                voucherDetail.EmpId = Nothing
                voucherDetail.CostCenter = 0
                POrder.Voucher.VoucherDatail.Add(voucherDetail)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtProductionOrderNo.Text.Length = 0 Then
                ShowErrorMessage("Production Order No is required.")
                Me.txtProductionOrderNo.Focus()
                Return False
            End If
            If Me.grdInput.RowCount = 0 Then
                ShowErrorMessage("Input grid is empty")
                Return False
            End If
            If Me.cbApproved.Checked = True Then
                If Me.cmbCGAccount.Value < 1 Then
                    ShowErrorMessage("Please select CGS Account.")
                    Return False
                End If
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

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

    Private Sub cmbUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUnit.SelectedIndexChanged
        If Me.cmbUnit.Text = "Loose" Then
            txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
            Me.txtTotalQty.Enabled = False
            'Me.txtPackRate.Enabled = False
        Else
            Me.txtPackQty.Enabled = True
            Me.txtPackQty.TabStop = True
            Me.txtTotalQty.Enabled = True
            'Me.txtPackRate.Enabled = True
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
            txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        End If
        If Not Me.cmbItem.Text = String.Empty Then
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
        End If

    End Sub

    Function GetProductionNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("PRO" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "ProductionOrder", "ProductionOrderNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("PRO" & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "ProductionOrder", "ProductionOrderNo")
            Else
                Return GetNextDocNo("PRO", 6, "ProductionOrder", "ProductionOrderNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtPackQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackQty.KeyPress
        Try
            NumValidation(sender, e)
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

    Private Sub txtRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackQty1.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty1.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQty1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalQty1.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRate1.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty1_TextChanged(sender As Object, e As EventArgs) Handles txtQty1.TextChanged
        If Val(Me.txtPackQty1.Text) = 0 Then
            txtPackQty1.Text = 1
            txtTotalQty1.Text = Val(txtQty1.Text)
            txtTotal1.Text = Math.Round(Val(txtTotalQty1.Text) * Val(txtRate1.Text), DecimalPointInValue)
        Else
            txtTotalQty1.Text = Val(txtQty1.Text) * Val(txtPackQty1.Text)
            txtTotal1.Text = Math.Round((Val(txtTotalQty1.Text) * Val(txtRate1.Text)), DecimalPointInValue)
        End If
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        If Val(Me.txtPackQty.Text) = 0 Then
            txtPackQty.Text = 1
            txtTotalQty.Text = Val(txtQty.Text)
            txtTotal.Text = Math.Round(Val(txtTotalQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        Else
            txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
            txtTotal.Text = Math.Round((Val(txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
        End If
    End Sub

    Private Sub cmbItem_Leave(sender As Object, e As EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            'ClearDetailControls()
            Me.txtStock.Text = Convert.ToDouble(GetStockByIdForStoreIssuence(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, , ))
            Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ClearDetailOutputControls()
        'cmbCategory.SelectedIndex = 0
        If Not Me.cmbCategory1.SelectedIndex = -1 Then
            Me.cmbCategory.SelectedIndex = 0
        End If
        Me.cmbItem1.Rows(0).Activate()
        cmbUnit1.SelectedIndex = 0
        txtQty1.Text = ""
        txtRate1.Text = ""
        txtTotal1.Text = ""
        txtPackQty1.Text = 1
        Me.txtTotalQty1.Text = String.Empty
    End Sub
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        If Not Me.cmbCategory.SelectedIndex = -1 Then
            Me.cmbCategory.SelectedIndex = 0
        End If
        Me.cmbItem.Rows(0).Activate()
        cmbUnit.SelectedIndex = 0
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
        Me.txtTotalQty.Text = String.Empty
    End Sub
    Private Sub ClearOverHeadsControls()
        Try
            Me.cmbAccount.SelectedIndex = 0
            Me.txtAmount.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearLabourTypeControls()
        Try
            Me.cmbLabourType.Rows(0).Activate()
            Me.txtAmount1.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub cmbItem_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            'ClearDetailControls()

            If Not Me.cmbItem.Text = String.Empty Then
                Me.txtStock.Text = Convert.ToDouble(GetStockByIdForStoreIssuence(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, , ))
            End If

            'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            'If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            FillCombos("ArticlePack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem1_Leave(sender As Object, e As EventArgs) Handles cmbItem1.Leave
        Try
            If Me.cmbItem1.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            'ClearDetailOutputControls()
            Me.txtStock1.Text = Convert.ToDouble(GetStockByIdForStoreIssuence(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, , ))
            Me.txtRate1.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            If Me.rbFinish.Checked = True Then
                Me.txtRate1.Text = GetCost()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem1_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem1.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            'ClearDetailOutputControls()
            Me.txtStock1.Text = Convert.ToDouble(GetStockByIdForStoreIssuence(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, , ))
            'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            'If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            FillCombos("ArticlePack1")
            If Me.rbFinish.Checked = True Then
                Me.txtRate1.Text = GetCost()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AddInputGrid()
        Try
            Dim dtInput As DataTable = CType(Me.grdInput.DataSource, DataTable)
            Dim dr() As DataRow = dtInput.Select("ItemId=" & Me.cmbItem.Value & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Selected Item already exists.")
                Me.cmbItem.Focus()
                Exit Sub
            End If
            Dim drInput As DataRow
            drInput = dtInput.NewRow
            drInput("ID") = 0
            drInput("ProductionOrderId") = ProductionOrderId
            drInput("LocationId") = Me.cmbCategory.SelectedValue
            drInput("ItemId") = Me.cmbItem.Value
            'Task 3420 set name of product item
            drInput("Item") = Me.cmbItem.ActiveRow.Cells(2).Value.ToString
            ''
            drInput("Unit") = Me.cmbUnit.Text
            drInput("PackQty") = Val(txtPackQty.Text)
            drInput("Qty") = Val(txtQty.Text)
            drInput("Rate") = Val(txtRate.Text)
            drInput("TotalQty") = Val(txtTotalQty.Text)
            drInput("NetAmount") = Val(txtTotal.Text)
            drInput("SubSubId") = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            drInput("CGSAccountId") = Val(Me.cmbItem.ActiveRow.Cells("CGSAccountId").Value.ToString)
            drInput("MasterArticleId") = Val(Me.cmbItem.ActiveRow.Cells("MasterId").Value.ToString)
            'Task 3394 Saad Afzaal set value of finishgoodid in input material detail grid  
            drInput("FinishGoodId") = Me.cmbBOM.Value
            dtInput.Rows.Add(drInput)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AddOutputGrid()
        Try
            Dim dtOutput As DataTable = CType(Me.grdOutput.DataSource, DataTable)
            Dim dr() As DataRow = dtOutput.Select("ItemId=" & Me.cmbItem1.Value & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Selected Item already exists.")
                Me.cmbItem1.Focus()
                Exit Sub
            End If
            Dim drOutput As DataRow
            drOutput = dtOutput.NewRow
            drOutput("ID") = 0
            drOutput("ProductionOrderId") = ProductionOrderId
            drOutput("LocationId") = Me.cmbCategory1.SelectedValue
            drOutput("ItemId") = Me.cmbItem1.Value
            drOutput("Item") = Me.cmbItem1.ActiveRow.Cells(2).Value.ToString
            ''
            If rbFinish.Checked = True Then
                drOutput("ItemType") = Finish
            Else
                drOutput("ItemType") = ByProduct
            End If
            drOutput("Unit") = Me.cmbUnit1.Text
            drOutput("PackQty") = Val(txtPackQty1.Text)
            drOutput("Qty") = Val(txtQty1.Text)
            drOutput("Rate") = Val(txtRate1.Text)
            drOutput("TotalQty") = Val(txtTotalQty1.Text)
            drOutput("NetAmount") = Val(txtTotal1.Text)
            drOutput("SubSubId") = Val(Me.cmbItem1.ActiveRow.Cells("AccountId").Value.ToString)
            drOutput("CGSAccountId") = Val(Me.cmbItem1.ActiveRow.Cells("CGSAccountId").Value.ToString)
            dtOutput.Rows.Add(drOutput)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AddOverHeadsGrid()
        Try
            Dim dtOverHeads As DataTable = CType(Me.grdOverHeads.DataSource, DataTable)
            Dim dr() As DataRow = dtOverHeads.Select("AccountId=" & Me.cmbAccount.SelectedValue & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Selected Account already exists.")
                Me.cmbItem.Focus()
                Exit Sub
            End If
            Dim drOverHeads As DataRow
            drOverHeads = dtOverHeads.NewRow
            drOverHeads("ID") = 0
            drOverHeads("ProductionOrderId") = 0
            drOverHeads("AccountId") = Me.cmbAccount.SelectedValue
            drOverHeads("Account") = Me.cmbAccount.Text
            drOverHeads("Amount") = Val(Me.txtAmount.Text)
            ''
            dtOverHeads.Rows.Add(drOverHeads)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AddLabourGrid()
        Try
            Dim dtLabour As DataTable = CType(Me.grdLabourType.DataSource, DataTable)
            Dim dr() As DataRow = dtLabour.Select("AccountId=" & Val(Me.cmbLabourType.ActiveRow.Cells("AccountId").Value.ToString) & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Selected Account already exists.")
                Me.cmbItem.Focus()
                Exit Sub
            End If

            Dim drLabour As DataRow
            drLabour = dtLabour.NewRow
            drLabour("ID") = 0
            drLabour("ProductionOrderId") = 0
            drLabour("LabourTypeId") = Me.cmbLabourType.Value
            drLabour("LabourType") = Me.cmbLabourType.Text
            drLabour("AccountId") = Val(Me.cmbLabourType.ActiveRow.Cells("AccountId").Value.ToString)
            drLabour("Account") = Me.cmbLabourType.ActiveRow.Cells("Account").Value.ToString
            drLabour("Amount") = Val(Me.txtAmount1.Text)
            ''
            dtLabour.Rows.Add(drLabour)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtBatchSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBatchSize.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ResetControls()
        Try
            Me.txtProductionOrderNo.Text = GetProductionNo()
            Me.txtTicketNo.Text = ProductionOrderDAL.GetNextTicket()
            Me.txtBatchNo.Text = Me.txtProductionOrderNo.Text & "-" & Me.txtTicketNo.Text
            Me.dtpProductionDate.Value = Now
            'Me.txtBatchNo.Text = ""
            Me.dtpExpiryDate.Value = Now
            Me.cmbProductProduced.Rows(0).Activate()
            Me.cmbBOM.Rows(0).Activate()
            Me.cmbCGAccount.Rows(0).Activate()
            Me.txtBatchSize.Text = ""
            Me.cmbSection.Text = ""
            Me.txtRemarks.Text = ""
            Me.txtTotalQuantity.Text = 0
            Me.cbApproved.Checked = True
            IsFormOpened = False
            ClearDetailControls()
            ClearDetailOutputControls()
            ClearOverHeadsControls()
            ClearLabourTypeControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbBOM_ValueChanged(sender As Object, e As EventArgs) Handles cmbBOM.ValueChanged
        Try

            'Task 3394 Saad Afzaal get value from configuration to BoM Load Receipy on value changed'

            Dim BOMLoadReceipy As Boolean = False
            Dim InputMaterialDT As New DataTable
            Dim OverHeadDT As New DataTable
            Dim LabourTypeDT As New DataTable
            Dim OutputDT As New DataTable
            Dim BOMid As Integer = 0
            Dim loadBOM As Boolean = True

            BOMLoadReceipy = Convert.ToBoolean(getConfigValueByType("BOMloadReceipy").ToString)

            If BOMLoadReceipy = True Then

                BOMid = BOMList.Find(Function(bid As Integer) bid = Me.cmbBOM.Value)

                If BOMid > 0 Then
                    If msg_Confirm("This BOM is already exist do you want load again") = True Then
                        loadBOM = True
                    Else
                        loadBOM = False
                    End If
                End If

                If Me.cmbBOM.Value > 0 AndAlso loadBOM = True Then

                    'Task 3394 obtain values of InputMaterial , OverHead and LaobourType from ProductionOrderDAL class

                    InputMaterialDT = ProductionOrderDAL.BOM_GetInputMaterial(Me.cmbBOM.Value, Val(Me.cmbBOM.ActiveRow.Cells("BatchSize").Value.ToString))
                    OverHeadDT = ProductionOrderDAL.BOM_GetOverHead(Me.cmbBOM.Value)
                    LabourTypeDT = ProductionOrderDAL.BOM_GetLabour(Me.cmbBOM.Value)
                    OutputDT = ProductionOrderDAL.BOM_GetOutput(Me.cmbBOM.Value, Me.cmbCategory1.SelectedValue)

                    Dim dtGrid As DataTable = CType(Me.grdInput.DataSource, DataTable)
                    If dtGrid.Rows.Count > 0 AndAlso InputMaterialDT.Rows.Count > 0 Then
                        InputMaterialDT.Merge(dtGrid)
                        Me.grdInput.DataSource = InputMaterialDT
                    Else
                        Me.grdInput.DataSource = InputMaterialDT
                    End If

                    Dim dtGrid1 As DataTable = CType(Me.grdOverHeads.DataSource, DataTable)
                    If dtGrid1.Rows.Count > 0 AndAlso OverHeadDT.Rows.Count > 0 Then
                        OverHeadDT.Merge(dtGrid1)
                        Me.grdOverHeads.DataSource = OverHeadDT
                    Else
                        Me.grdOverHeads.DataSource = OverHeadDT
                    End If

                    Dim dtGrid2 As DataTable = CType(Me.grdLabourType.DataSource, DataTable)
                    If dtGrid2.Rows.Count > 0 AndAlso LabourTypeDT.Rows.Count > 0 Then
                        LabourTypeDT.Merge(dtGrid2)
                        Me.grdLabourType.DataSource = LabourTypeDT
                    Else
                        Me.grdLabourType.DataSource = LabourTypeDT
                    End If


                    Dim dtGrid3 As DataTable = CType(Me.grdOutput.DataSource, DataTable)
                    If dtGrid3.Rows.Count > 0 AndAlso OutputDT.Rows.Count > 0 Then
                        OutputDT.Merge(dtGrid3)
                        Me.grdOutput.DataSource = OutputDT
                    Else
                        Me.grdOutput.DataSource = OutputDT
                    End If

                    BOMList.Add(Me.cmbBOM.Value)

                End If

                'Task 3394 again getDetails of Production Order'

                If _IsEditMode = True AndAlso LoadBOMUpdate = True Then
                    Me.LoadBOMUpdate = False
                    DisplayDetail(ProductionOrderId)
                End If

            End If

            'End Task 3394
                If Me.cmbBOM.Value > 0 Then
                    Me.txtBatchSize.Text = Me.cmbBOM.ActiveRow.Cells("BatchSize").Value.ToString
                    Me.txtTotalQuantity.Text = Val(Me.txtBatchSize.Text)
                Else
                    Me.txtBatchSize.Text = String.Empty
                End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Me.cmbItem.Value < 1 Then
                ShowErrorMessage("Please selecct an item.")
                Exit Sub
            End If
            If Val(Me.txtTotalQty.Text) < 1 Then
                ShowErrorMessage("Total Qty is required.")
                Exit Sub
            End If
            AddInputGrid()
            ClearDetailControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd1_Click(sender As Object, e As EventArgs) Handles btnAdd1.Click
        Try
            If Me.cmbItem1.Value < 1 Then
                ShowErrorMessage("Please selecct an item.")
                Exit Sub
            End If
            If Val(Me.txtTotalQty1.Text) < 1 Then
                ShowErrorMessage("Total Qty is required.")
                Exit Sub
            End If
            AddOutputGrid()
            ClearDetailOutputControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddProductionOverHeads_Click(sender As Object, e As EventArgs) Handles btnAddProductionOverHeads.Click
        Try
            If Me.cmbAccount.SelectedValue < 1 Then
                ShowErrorMessage("Account is required.")
                Me.cmbAccount.Focus()
                Exit Sub
            End If
            If Val(Me.txtAmount.Text) < 1 Then
                ShowErrorMessage("Amount should be greator than zero.")
                Me.txtAmount.Focus()
                Exit Sub
            End If
            AddOverHeadsGrid()
            ClearOverHeadsControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPerUnitRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount1.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddLabourAllocation_Click(sender As Object, e As EventArgs) Handles btnAddLabourAllocation.Click
        Try
            If Me.cmbLabourType.Value < 1 Then
                ShowErrorMessage("Please select labour type.")
                Me.cmbLabourType.Focus()
                Exit Sub
            End If
            If Me.cmbLabourType.ActiveRow.Cells("AccountId").Value < 1 Then
                ShowErrorMessage("Account is not mapped with this labour type.")
                Me.cmbLabourType.Focus()
                Exit Sub
            End If
            If Val(Me.txtAmount1.Text) < 1 Then
                ShowErrorMessage("Amount should be greator than zero.")
                Me.txtAmount1.Focus()
                Exit Sub
            End If
            AddLabourGrid()
            ClearLabourTypeControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DisplayDetail(ByVal ProductionOrderId As Integer)
        Try
            '' Calling Input Grid
            Me.grdInput.DataSource = ProductionOrderInputMaterialDAL.GetInputs(ProductionOrderId)
            Me.grdInput.RootTable.Columns("PackQty").FormatString = "N" & DecimalPointInQty
            Me.grdInput.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdInput.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInQty
            Me.grdInput.RootTable.Columns("NetAmount").FormatString = "N" & DecimalPointInValue
            Me.grdInput.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            ''
            Me.grdInput.RootTable.Columns("PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("TotalQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("TotalQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdInput.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            FillCombos("grdInputLocation")

            'Task 3394 Clear BOMList then get finishgoodid value from detail table and fill in the BOMList'

            BOMList.Clear()

            For Each row As Janus.Windows.GridEX.GridEXRow In grdInput.GetRows
                BOMList.Add(Val(row.Cells("FinishGoodId").Value.ToString))
            Next


            '' Calling OverHeads Grid
            Me.grdOverHeads.DataSource = ProductionOrderOverHeadsDAL.GetOverHeads(ProductionOrderId)
            Me.grdOverHeads.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdOverHeads.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOverHeads.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            '' Calling Labour Grid
            Me.grdLabourType.DataSource = ProductionOrderLabourDAL.GetLabours(ProductionOrderId)
            Me.grdLabourType.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdLabourType.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdLabourType.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            '' Calling Output Grid
            Me.grdOutput.DataSource = ProductionOrderOutputMaterialDAL.GetOutputs(ProductionOrderId)
            Me.grdOutput.RootTable.Columns("PackQty").FormatString = "N" & DecimalPointInQty
            Me.grdOutput.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdOutput.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInQty
            Me.grdOutput.RootTable.Columns("NetAmount").FormatString = "N" & DecimalPointInValue
            Me.grdOutput.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.grdOutput.RootTable.Columns("PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("TotalQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("TotalQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutput.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            FillCombos("grdOutputLocation")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditRecord(ByVal Obj As ProductionOrderBE)
        Try
            ProductionOrderId = Obj.ProductionOrderId
            Me.txtProductionOrderNo.Text = Obj.ProductionOrderNo
            Me.txtTicketNo.Text = Obj.TicketNo
            Me.dtpProductionDate.Value = Obj.ProductionOrderDate
            Me.txtBatchNo.Text = Obj.BatchNo
            Me.dtpExpiryDate.Value = Obj.ExpiryDate
            Me.cmbProductProduced.Value = Obj.ProductId
            Me.cmbBOM.Value = Obj.FinishGoodId
            Me.txtBatchSize.Text = Obj.BatchSize
            Me.cmbSection.Text = Obj.Section
            Me.txtRemarks.Text = Obj.Remarks
            Me.cmbCGAccount.Value = Obj.CGSAccountId
            Me.cbApproved.Checked = Obj.Approved
            Me.txtTotalQuantity.Text = Obj.TotalQuantity
            DisplayDetail(ProductionOrderId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTicketNo_TextChanged(sender As Object, e As EventArgs) Handles txtTicketNo.TextChanged
        Try
            If txtTicketNo.Text.Length > 0 Then
                Me.txtBatchNo.Text = Me.txtProductionOrderNo.Text & "-" & Me.txtTicketNo.Text
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = False Then Exit Sub
            If _IsEditMode = False Then
                'If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub

                'Task 3420 Call Save StorIssuence Function to Save StoreIssuence
                If saveStoreIssuence() = True Then
                    'Task 3420 Call Save ProductionEntry Function to Save ProductionEntry
                    If saveProductionEntry() Then
                        POrder.DispatchId = Me.DispatchId
                        POrder.Production_Id = ProductionOrderDAL.getLatesProductionOrderId
                        If ProductionOrderDAL.Add(POrder) Then
                            msg_Information("Record has been saved successfully.")
                            Me.DialogResult = Windows.Forms.DialogResult.Yes
                        Else
                            'Task 3240 Delete StoreIssence in case of Production Order Entry Failed
                            ProductionOrderDAL.DeleteStroIssuence()
                            'Task 3240 Delete StoreIssence in case of Production Order Entry Failed
                            ProductionOrderDAL.DeleteProductionOrder()
                            msg_Information("Record failed to save.")
                        End If
                    Else
                        'Task 3240 Delete StoreIssence in case of Production Entry Failed
                        ProductionOrderDAL.DeleteStroIssuence()
                        msg_Information("Record failed to save.")
                    End If
                Else
                    msg_Information("Record failed to save.")
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If POrder.ProductionOrderNo.Length > 0 AndAlso cbApproved.Checked = True Then
                    POrder.Stock.StockTransId = StockTransId(POrder.ProductionOrderNo)
                End If

                'Task 3420 Call Update StorIssuence Function to Update StoreIssuence
                If updateStoreIssuence() = True Then
                    'Task 3420 Call Update ProductionEntry Function to Update ProductionEntry
                    If updateProductionEntry() = True Then
                        If ProductionOrderDAL.Update(POrder) Then
                            msg_Information("Record has been updated successfully.")
                            Me.DialogResult = Windows.Forms.DialogResult.Yes
                        Else
                            msg_Information("Record failed to update.")
                        End If
                    Else
                        msg_Information("Record failed to update.")
                    End If
                Else
                    msg_Information("Record failed to save.")
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function ItemWiseCGSAccount(ByVal ArticleId As Integer) As Boolean
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT CGSAccountId FROM ArticleDefTableMaster WHERE ArticleId = " & ArticleId & " AND CGSAccountId Is Not Null AND CGSAccountId <> 0"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                ItemWiseCGSAccountId = Val(dt.Rows(0).Item(0))
                Return True
            Else
                ItemWiseCGSAccountId = 0
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub rbCode1_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode1.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbItem1.DisplayMember = Me.cmbItem1.Rows(0).Cells(1).Column.Key.ToString
    End Sub

    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
    End Sub

    Private Sub cmbUnit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUnit1.SelectedIndexChanged
        If Me.cmbUnit1.Text = "Loose" Then
            txtTotal1.Text = Math.Round(Val(txtQty1.Text) * Val(txtRate1.Text), DecimalPointInValue)
            txtPackQty1.Text = 1
            Me.txtPackQty1.Enabled = False
            Me.txtPackQty1.TabStop = False
            Me.txtTotalQty1.Enabled = False
            'Me.txtPackRate.Enabled = False
        Else
            Me.txtPackQty1.Enabled = True
            Me.txtPackQty1.TabStop = True
            Me.txtTotalQty1.Enabled = True
            'Me.txtPackRate.Enabled = True
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty1.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
            txtTotal1.Text = Math.Round(((Val(txtQty1.Text) * Val(txtPackQty1.Text)) * Val(txtRate1.Text)), DecimalPointInValue)
        End If
        Me.txtStock1.Text = Convert.ToDouble(GetStockById(Me.cmbItem1.ActiveRow.Cells(0).Value, Me.cmbCategory1.SelectedValue, IIf(Me.cmbUnit1.Text = "Loose", "Loose", "Pack")))
    End Sub

    Private Sub txtTotalQty_LostFocus(sender As Object, e As EventArgs) Handles txtTotalQty.LostFocus
        txtTotal.Text = Math.Round((Val(txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
    End Sub

    Private Sub txtTotalQty1_LostFocus(sender As Object, e As EventArgs) Handles txtTotalQty1.LostFocus
        txtTotal1.Text = Math.Round((Val(txtTotalQty1.Text) * Val(txtRate1.Text)), DecimalPointInValue)
    End Sub

    Private Sub txtPackQty_LostFocus(sender As Object, e As EventArgs) Handles txtPackQty.LostFocus

    End Sub

    Private Sub frmProductionOrder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.Yes Then

        ElseIf e.CloseReason = CloseReason.UserClosing Then

        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub grdInput_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdInput.ColumnButtonClick
        If e.Column.Key = "Delete" Then
            If msg_Confirm("Do you want to delete record?") = False Then Exit Sub
            Dim ObjInput As New ProductionOrderInputMaterialBE
            ObjInput.ID = Val(Me.grdInput.GetRow.Cells("ID").Value.ToString)
            ProductionOrderInputMaterialDAL.Delete(ObjInput)

            'Task 3394 check if FinishGoodID is multiple time in input material grid

            Dim FinishGoodIDCount As Integer = 0

            For Each row As Janus.Windows.GridEX.GridEXRow In grdInput.GetRows
                If row.Cells("FinishGoodId").Value = Val(Me.grdInput.GetRow.Cells("FinishGoodId").Value.ToString) Then
                    FinishGoodIDCount += 1
                End If
            Next

            'Task 3394 Remove BOM item id from BOMList'
            If FinishGoodIDCount < 2 Then
                BOMList.Remove(Val(Me.grdInput.GetRow.Cells("FinishGoodId").Value.ToString))
            End If

            Me.grdInput.GetRow.Delete()
        End If
    End Sub

    Private Sub grdOutput_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOutput.ColumnButtonClick
        If e.Column.Key = "Delete" Then
            If msg_Confirm("Do you want to delete record?") = False Then Exit Sub
            Dim ObjOutput As New ProductionOrderOutputMaterialBE
            ObjOutput.ID = Val(Me.grdOutput.GetRow.Cells("ID").Value.ToString)
            ProductionOrderOutputMaterialDAL.Delete(ObjOutput)
            Me.grdOutput.GetRow.Delete()
        End If
    End Sub

    Private Sub grdOverHeads_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOverHeads.ColumnButtonClick
        If e.Column.Key = "Delete" Then
            If msg_Confirm("Do you want to delete record?") = False Then Exit Sub
            Dim ObjOverHeads As New ProductionOrderOverHeadsBE
            ObjOverHeads.ID = Val(Me.grdOverHeads.GetRow.Cells("ID").Value.ToString)
            ProductionOrderOverHeadsDAL.Delete(ObjOverHeads)
            Me.grdOverHeads.GetRow.Delete()
        End If
    End Sub

    Private Sub grdLabourType_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLabourType.ColumnButtonClick
        If e.Column.Key = "Delete" Then
            If msg_Confirm("Do you want to delete record?") = False Then Exit Sub
            Dim ObjLabourType As New ProductionOrderLabourBE
            ObjLabourType.ID = Val(Me.grdLabourType.GetRow.Cells("ID").Value.ToString)
            ProductionOrderLabourDAL.Delete(ObjLabourType)
            Me.grdLabourType.GetRow.Delete()
        End If
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        If Val(Me.txtPackQty.Text) = 0 Then
            txtPackQty.Text = 1
            txtTotalQty.Text = Val(txtQty.Text)
            txtTotal.Text = Math.Round(Val(txtTotalQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        Else
            txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
            txtTotal.Text = Math.Round((Val(txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
        End If
    End Sub

    Private Sub txtRate1_TextChanged(sender As Object, e As EventArgs) Handles txtRate1.TextChanged
        If Val(Me.txtPackQty1.Text) = 0 Then
            txtPackQty1.Text = 1
            txtTotalQty1.Text = Val(txtQty1.Text)
            txtTotal1.Text = Math.Round(Val(txtTotalQty1.Text) * Val(txtRate1.Text), DecimalPointInValue)
        Else
            txtTotalQty1.Text = Val(txtQty1.Text) * Val(txtPackQty1.Text)
            txtTotal1.Text = Math.Round((Val(txtTotalQty1.Text) * Val(txtRate1.Text)), DecimalPointInValue)
        End If
    End Sub

    Private Sub rbCode2_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode2.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbProductProduced.DisplayMember = Me.cmbProductProduced.Rows(0).Cells(1).Column.Key.ToString
        'ElseIf Me.rbName2.Checked = True Then
        'Me.cmbProductProduced.DisplayMember = Me.cmbProductProduced.Rows(0).Cells(2).Column.Key.ToString
        'End If
    End Sub

    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged
        If Val(Me.txtPackQty.Text) = 0 Then
            txtPackQty.Text = 1
            txtTotalQty.Text = Val(txtQty.Text)
            txtTotal.Text = Math.Round(Val(txtTotalQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        Else
            txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
            txtTotal.Text = Math.Round((Val(txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
        End If
    End Sub

    Private Sub txtPackQty1_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty1.TextChanged
        If Val(Me.txtPackQty1.Text) = 0 Then
            txtPackQty1.Text = 1
            txtTotalQty1.Text = Val(txtQty1.Text)
            txtTotal1.Text = Math.Round(Val(txtTotalQty1.Text) * Val(txtRate1.Text), DecimalPointInValue)
        Else
            txtTotalQty1.Text = Val(txtQty1.Text) * Val(txtPackQty1.Text)
            txtTotal1.Text = Math.Round((Val(txtTotalQty1.Text) * Val(txtRate1.Text)), DecimalPointInValue)
        End If
    End Sub


    Private Sub rbCode3_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode3.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbBOM.DisplayMember = Me.cmbBOM.Rows(0).Cells(1).Column.Key.ToString
    End Sub

    Private Sub cmbProductProduced_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProductProduced.KeyDown
        'Try
        '    If e.KeyCode = Keys.F1 Then
        '        frmItemSearch.BringToFront()
        '        frmItemSearch.ShowDialog()
        '        If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
        '            cmbProductProduced.Value = frmItemSearch.ArticleId
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown, cmbItem1.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then
                If flgCompanyRights = True Then
                    frmItemSearch.CompanyId = MyCompanyId
                End If
                If getConfigValueByType("ArticleFilterByLocation").ToString = "True" Then
                    If Me.TabControl1.TabIndex = 0 Then
                        If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                            frmItemSearch.LocationId = Me.cmbCategory.SelectedValue
                        Else
                            frmItemSearch.LocationId = 0
                        End If
                    Else
                        If GetRestrictedItemFlg(Me.cmbCategory1.SelectedValue) = True Then
                            frmItemSearch.LocationId = Me.cmbCategory1.SelectedValue
                        Else
                            frmItemSearch.LocationId = 0
                        End If
                    End If
                End If
                frmItemSearch.BringToFront()
                frmItemSearch.ShowDialog()
                If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    If Me.TabControl1.SelectedIndex = 0 Then
                        cmbItem.Value = frmItemSearch.ArticleId
                        txtQty.Text = frmItemSearch.Qty
                    Else
                        cmbItem1.Value = frmItemSearch.ArticleId
                        txtQty1.Text = frmItemSearch.Qty
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem1_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem1.KeyDown
        'Try
        '    If e.KeyCode = Keys.F1 Then
        '        frmItemSearch.BringToFront()
        '        frmItemSearch.ShowDialog()
        '        If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
        '            cmbItem1.Value = frmItemSearch.ArticleId
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub rbName3_CheckedChanged(sender As Object, e As EventArgs) Handles rbName3.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbBOM.DisplayMember = Me.cmbBOM.Rows(0).Cells(2).Column.Key.ToString
    End Sub

    Private Sub rbName2_CheckedChanged(sender As Object, e As EventArgs) Handles rbName2.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbProductProduced.DisplayMember = Me.cmbProductProduced.Rows(0).Cells(2).Column.Key.ToString
    End Sub

    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
    End Sub

    Private Sub rbName1_CheckedChanged(sender As Object, e As EventArgs) Handles rbName1.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        'If Me.rbCode2.Checked = True Then
        Me.cmbItem1.DisplayMember = Me.cmbItem1.Rows(0).Cells(2).Column.Key.ToString
    End Sub

    Private Sub txtTotalQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalQuantity.KeyPress
        NumValidation(sender, e)
    End Sub

    Public Function GetCost() As Double
        Dim Input As Double = 0
        Dim OverHeads As Double = 0
        Dim Labour As Double = 0
        Dim Output As Double = 0
        Dim NetCost As Double = 0
        Try
            Dim fltCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdOutput.RootTable.Columns("ItemType"), Janus.Windows.GridEX.ConditionOperator.Equal, "ByProduct")
            Input = Me.grdInput.GetTotal(Me.grdInput.RootTable.Columns("NetAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            OverHeads = Me.grdOverHeads.GetTotal(Me.grdOverHeads.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Labour = Me.grdLabourType.GetTotal(Me.grdLabourType.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Output = Me.grdOutput.GetTotal(Me.grdOutput.RootTable.Columns("NetAmount"), Janus.Windows.GridEX.AggregateFunction.Sum, fltCondition)
            NetCost = Math.Round((Input + OverHeads + Labour) - Output, DecimalPointInValue)
            NetCost = NetCost / Math.Round(Val(Me.txtTotalQuantity.Text), DecimalPointInValue)
            If NetCost > 0 Then
                Return NetCost
            Else
                Return 0
            End If
            Return NetCost
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub rbFinish_CheckedChanged(sender As Object, e As EventArgs) Handles rbFinish.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        If Me.rbFinish.Checked = True Then
            Me.txtRate1.Text = GetCost()
        End If
    End Sub

    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.rbFinish.Checked = True Then
                Me.txtRate1.Text = GetCost()
            End If

            If Me.grdInput.RowCount > 0 AndAlso Val(txtTotalQuantity.Text) > 0 Then
                For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdInput.GetRows
                    Me.grdInput.UpdateData()
                    _Row.BeginEdit()
                    _Row.Cells("BatchSize").Value = Val(txtTotalQuantity.Text)
                    _Row.EndEdit()
                    Me.grdInput.UpdateData()
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdOutput_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOutput.CellEdited
        If IsFormOpened = False Then Exit Sub
        If Me.rbFinish.Checked = True Then
            Me.txtRate1.Text = GetCost()
        End If
    End Sub

    Private Sub grdLabourType_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLabourType.CellEdited
        If IsFormOpened = False Then Exit Sub
        If Me.rbFinish.Checked = True Then
            Me.txtRate1.Text = GetCost()
        End If
    End Sub

    Private Sub grdOverHeads_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOverHeads.CellUpdated
        If IsFormOpened = False Then Exit Sub
        If Me.rbFinish.Checked = True Then
            Me.txtRate1.Text = GetCost()
        End If
    End Sub

    Private Sub grdInput_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdInput.CellEdited
        If IsFormOpened = False Then Exit Sub
        If Me.rbFinish.Checked = True Then
            Me.txtRate1.Text = GetCost()
        End If
    End Sub

    Private Sub rbByProduct_CheckedChanged(sender As Object, e As EventArgs) Handles rbByProduct.CheckedChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.rbByProduct.Checked = True Then
                If Me.cmbItem1.IsItemInList = False Then
                    Me.txtStock.Text = 0
                    Exit Sub
                End If
                'ClearDetailOutputControls()
                Me.txtStock1.Text = Convert.ToDouble(GetStockByIdForStoreIssuence(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, , ))
                Me.txtRate1.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
                If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Muhammad Aashir : TFS3246 : Show Print Preview At Production Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            GetCrystalReportRights()
            ShowReport("productionOrder", "{ProductionOrder.ProductionOrderId} = " & Me.ProductionOrderId.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdInput.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdInput.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdInput.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Production Order"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task 3420 Saad Afzaal Create StroeIssuenSave Function '

    Public Function saveStoreIssuence() As Boolean

        Me.grdInput.UpdateData()

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
        If blnCGAccountOnStoreIssuance = True Then
            AccountId2 = Me.cmbCGAccount.Value
        Else
            AccountId2 = getConfigValueByType("StoreIssuenceAccount")
        End If

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
            & gobjLocationId & ", N'" & txtPONo & "',N'" & dtpProductionDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," _
            & 0 & "," & 0 & ", " _
            & Val(Me.grdInput.GetTotal(Me.grdInput.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," _
            & Val(Me.grdInput.GetTotal(Me.grdInput.RootTable.Columns("NetAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " _
            & 0.0 & ", '" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" _
            & txtPONo.Replace("'", "''") & "', " & 0 _
            & ", " & 0 & ", " & 0 & ", " & 0 & ", " _
            & IIf(Me.cbApproved.Checked = True, 1, 0) & ", " & 0 & "," _
            & AccountId2 & ", " & 0 & ", " _
            & 0 & ") SELECT @@IDENTITY"

            Me.DispatchId = objCommand.ExecuteScalar()

            ''Voucher Master Entry

            'If Me.cbApproved.Checked = True Then 'Task:M21 If Issued Checked Then Update Voucher, Update Stock.
            '    If (flgStoreIssuenceVoucher = True Or flgCylinderVoucher = True) Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
            '        objCommand.CommandText = ""

            '        objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                                  & " cheque_no, cheque_date,post,Source,voucher_code,Remarks, UserName, Posted_UserName)" _
            '                                  & " VALUES(" & gobjLocationId & ", 1,  1 , N'" & Me.txtPONo & "', N'" _
            '                                  & Me.dtpProductionDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                                  & " NULL, NULL, 1,N'" & Me.Name & "',N'" & Me.txtPONo & "',N'" & Me.txtRemarks.Text.Replace("'", "''") _
            '                                  & "', N'" & LoginUserName.Replace("'", "''") & "', N'" & LoginUserName.Replace("'", "''") & "')" _
            '                                  & " SELECT @@IDENTITY"
            '        'End Task:M101
            '        lngVoucherMasterId = objCommand.ExecuteScalar
            '    End If
            'End If


            Dim dtGrd As DataTable = CType(Me.grdInput.DataSource, DataTable)

            dtGrd.AcceptChanges()

            Dim i As Integer

            For i = 0 To dtGrd.Rows.Count - 1
                If dtGrd.Rows(i).Item("TotalQty") > 0 Then

                    Dim dblPurchasePrice As Double = 0D
                    Dim dblCostPrice As Double = 0D

                    Dim strPriceData() As String = GetRateByItem(Val(dtGrd.Rows(i).Item("ItemId").ToString)).Split(",")

                    If strPriceData.Length > 1 Then
                        dblCostPrice = Val(strPriceData(0).ToString)
                        dblPurchasePrice = Val(strPriceData(1).ToString)
                    End If

                    ''Start Stock Work'

                    'If blnCheckCurrentStockByItem = True Then
                    '    CheckCurrentStockByItem(dtGrd.Rows(i).Item("ItemId").ToString, Val(dtGrd.Rows(i).Item("TotalQty").ToString), grdInput, , trans) ''TASK-408
                    'End If


                    'If ItemWiseCGSAccount(Val(dtGrd.Rows(i).Item("MasterArticleId").ToString)) = True AndAlso blnCGAccountOnStoreIssuance = False Then
                    '    AccountId2 = ItemWiseCGSAccountId
                    '    If GLAccountArticleDepartment = True Then
                    '        AccountId = Val(dtGrd.Rows(i).Item("SubSubId").ToString)
                    '    End If
                    'Else
                    '    If GLAccountArticleDepartment = True Then
                    '        AccountId = Val(dtGrd.Rows(i).Item("SubSubId").ToString)
                    '        If blnCGAccountOnStoreIssuance = False Then
                    '            AccountId2 = Val(dtGrd.Rows(i).Item("CGSAccountId").ToString)
                    '        End If
                    '    End If
                    'End If


                    'Dim CostPrice As Double = 0D
                    'Dim CrrStock As Double = 0D

                    'CostPrice = Val(dtGrd.Rows(i).Item("Rate").ToString)

                    'StockList = New List(Of StockDetail)
                    'Dim dblTotalCost As Double = 0D

                    'StockDetail = New StockDetail
                    'StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                    'StockDetail.LocationId = dtGrd.Rows(i).Item("LocationId").ToString
                    'StockDetail.ArticleDefId = dtGrd.Rows(i).Item("ItemId").ToString
                    'StockDetail.InQty = 0
                    ' ''Commented following row against TASK-408
                    ' ''StockDetail.OutQty = IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString), ((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString)))
                    'StockDetail.OutQty = Val(dtGrd.Rows(i).Item("TotalQty").ToString) ''TASK-408
                    'StockDetail.Rate = IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item("Rate").ToString), CostPrice)
                    'StockDetail.InAmount = 0
                    ' ''Commented below row against TASK-408 on 13-06-2016
                    ' ''StockDetail.OutAmount = IIf(dtGrd.Rows(i).Item(grdEnm.Unit).ToString = "Loose", ((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)), (((Val(dtGrd.Rows(i).Item(grdEnm.Qty).ToString)) * Val(dtGrd.Rows(i).Item(grdEnm.PackQty).ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item(grdEnm.Rate).ToString), CostPrice)))
                    'StockDetail.OutAmount = ((Val(dtGrd.Rows(i).Item("TotalQty").ToString)) * IIf(CostPrice = 0, Val(dtGrd.Rows(i).Item("Rate").ToString), CostPrice)) ''TASK-408
                    'StockDetail.Remarks = txtRemarks.Text

                    ' ''Start TASK-470 on 01-07-2016
                    'StockDetail.PackQty = dtGrd.Rows(i).Item("PackQty").ToString
                    'StockDetail.Out_PackQty = dtGrd.Rows(i).Item("Qty").ToString
                    'StockDetail.In_PackQty = 0
                    ' ''End TASK-470
                    'StockList.Add(StockDetail)
                    'dblTotalCost += StockDetail.OutAmount

                    ''End Stock Work


                    objCommand.CommandText = ""
                    objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, ArticleDefMasterId, Pack_Desc, CostPrice, PlanUnit, PlanQty, Lot_No,Rack_No,Comments, SubDepartmentID, AllocationDetailId, ParentId, EstimationId, TotalEstimatedQty, SubItem, PackPrice, TicketId, WIPAccountId, TicketQty) values( " _
                    & " ident_current('DispatchMasterTable'), " & Val(dtGrd.Rows(i).Item("ItemId").ToString) & ",N'" _
                    & (dtGrd.Rows(i).Item("Unit").ToString) & "'," & Val(dtGrd.Rows(i).Item("Qty").ToString) & ", " _
                    & " " & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & ", " _
                    & Val(dtGrd.Rows(i).Item("Rate").ToString) & ", " & Val(dtGrd.Rows(i).Item("PackQty").ToString) & "  , " _
                    & dblPurchasePrice & ",N'" & txtBatchNo.Text & "'," _
                    & 0 & "," & dtGrd.Rows(i).Item("LocationId").ToString & ", " _
                    & Val(dtGrd.Rows(i).Item("MasterArticleId").ToString) & ", N'" _
                    & dtGrd.Rows(i).Item("Unit").ToString.Replace("'", "''") & "', " & Val(dtGrd.Rows(i).Item("Rate").ToString) & ", N'" _
                    & String.Empty & "', " & 0 & ",N'" _
                    & String.Empty & "',N'" & String.Empty & "',N'" _
                    & txtRemarks.Text & "', " & 0 & " , " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ", " & 0 & ", " _
                    & 0 & ")"

                    objCommand.ExecuteNonQuery()



                    ''Detail Voucher Entry'


                    'If Me.cbApproved.Checked = True Then 'Task:M21 If Issued Checked Then Update Stock, Update Voucher.

                    '    If flgStoreIssuenceVoucher = True Then 'Code By Imran 3-7-2013 Ref by Request ID: 725
                    '        If flgAvrRate = True Then

                    '            objCommand.CommandText = ""
                    '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                    '                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & StockDetail.OutAmount & ", N'" & dtGrd.Rows(i).Item("Item").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")', " & 0 & " )"  ''TASK-408 added TotalQty instead of Qty
                    '            objCommand.ExecuteNonQuery()


                    '            objCommand.CommandText = ""
                    '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                    '                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & StockDetail.OutAmount & " ,0 , N'" & dtGrd.Rows(i).Item("Item").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")',  " & 0 & ")"  ''TASK-408 added TotalQty instead of Qty
                    '            objCommand.ExecuteNonQuery()

                    '        Else

                    '            objCommand.CommandText = ""
                    '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                    '                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", 0 , " & StockDetail.OutAmount & ", N'" & dtGrd.Rows(i).Item("Item").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")', " & 0 & " )" ''TASK-408 added TotalQty instead of Qty on 13-06-2016
                    '            objCommand.ExecuteNonQuery()


                    '            objCommand.CommandText = ""
                    '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                    '                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId2 & ", " & StockDetail.OutAmount & " ,0 , N'" & dtGrd.Rows(i).Item("Item").ToString.Replace("'", "''") & " (" & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & "X" & Math.Round(Val(CostPrice), 3) & ")',  " & 0 & ")" ''TASK-408 added TotalQty instead of Qty on 13-06-2016
                    '            objCommand.ExecuteNonQuery()

                    '        End If
                    '    End If
                    'End If

                End If
            Next

            setVoucherNo = Me.txtPONo
            setVoucherdate = Me.dtpProductionDate.Value.ToString("yyyy-M-d h:mm:ss tt")

            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Return False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

    End Function

    'Task 3420 Saad Afzaal Create StroeIssuenUpdate Function '

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
        If blnCGAccountOnStoreIssuance = True Then
            AccountId2 = Me.cmbCGAccount.Value
        Else
            AccountId2 = getConfigValueByType("StoreIssuenceAccount")
        End If

        objCon = Con 'New SqlConnection("Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Me.grdInput.Update()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()

            objCommand.CommandText = ""

            'TASK: TFS1136  Added SalesOrderId to be updated.
            objCommand.CommandText = "Update DispatchMasterTable set DispatchDate=N'" & dtpProductionDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & 0 & ", PurchaseOrderId=" & 0 & ", " _
                                 & " DispatchQty=" & Val(Me.grdInput.GetTotal(Me.grdInput.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",DispatchAmount=" & Val(Me.grdInput.GetTotal(Me.grdInput.RootTable.Columns("NetAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & 0 & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "' , Issued=" & IIf(Me.cbApproved.Checked = True, 1, 0) & " , StoreIssuanceAccountId=" & AccountId2 & ", UpdateUser = '" & LoginUserName & "'  Where DispatchID= " & Me.DispatchId & " " ''TASK-408 added TotalQty instead of Qty on 13-06-2016
            'Altered by Ali Ansari Task#201508020 regarding update user column
            objCommand.ExecuteNonQuery()


            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from DispatchDetailTable where DispatchID = " & Me.DispatchId
            objCommand.ExecuteNonQuery()



            Dim dtGrd As DataTable = CType(Me.grdInput.DataSource, DataTable)
            dtGrd.AcceptChanges()
            For i = 0 To dtGrd.Rows.Count - 1

                If dtGrd.Rows(i).Item("TotalQty") > 0 Then


                    Dim dblPurchasePrice As Double = 0D
                    Dim dblCostPrice As Double = 0D

                    Dim strPriceData() As String = GetRateByItem(Val(dtGrd.Rows(i).Item("ItemId").ToString)).Split(",")

                    If strPriceData.Length > 1 Then
                        dblCostPrice = Val(strPriceData(0).ToString)
                        dblPurchasePrice = Val(strPriceData(1).ToString)
                    End If


                    objCommand.CommandText = ""

                    objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1, Qty,Price, Sz7,CurrentPrice,BatchNo,LocationID, ArticleDefMasterId, Pack_Desc,CostPrice, PlanUnit, PlanQty,SubDepartmentID, AllocationDetailId) values( " _
                                                   & " " & Me.DispatchId & " ," & Val(dtGrd.Rows(i).Item("ItemId").ToString) & ",N'" & (dtGrd.Rows(i).Item("Unit").ToString) & "'," & Val(dtGrd.Rows(i).Item("Qty").ToString) & ", " _
                                                   & " " & Val(dtGrd.Rows(i).Item("TotalQty").ToString) & ", " & Val(dtGrd.Rows(i).Item("Rate").ToString) & ", " & Val(dtGrd.Rows(i).Item("PackQty").ToString) & "  , " & dblPurchasePrice & ",N'" & txtBatchNo.Text & "' , " & dtGrd.Rows(i).Item("LocationId").ToString & ", " & Val(dtGrd.Rows(i).Item("MasterArticleId").ToString) & ", N'" & dtGrd.Rows(i).Item("Unit").ToString.Replace("'", "''") & "', " & Val(dtGrd.Rows(i).Item("Rate").ToString) & ", " & 0 & ", " & 0 & ", " & 0 & ", " & 0 & ")" ''TASK-408 added TotalQty instead of Qty on 13-06-2016
                    objCommand.ExecuteNonQuery()

                End If
            Next

            setVoucherNo = Me.txtPONo
            setVoucherdate = Me.dtpProductionDate.Value.ToString("yyyy-M-d h:mm:ss tt")

            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Return False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try

    End Function


    'Task 3420 Saad Afzaal Create FillProductionEntryModels Function which fill values of productionEntry model

    Public Sub FillProductionEntryModel()

        Me.txtProductionNo = frmProductionStore.GetDocumentNo()

        ProductionMaster = New ProductionMaster
        ProductionMaster.ProductionId = Me.ProductionId
        ProductionMaster.Production_No = Me.txtProductionNo
        ProductionMaster.Production_Date = Me.dtpProductionDate.Value.ToString("yyyy-M-d h:mm:ss tt")
        ProductionMaster.Production_Store = 0
        ProductionMaster.Project = 0
        ProductionMaster.CustomerCode = 0
        ProductionMaster.Order_No = 0
        ProductionMaster.TotalQty = Me.grdOutput.GetTotal(Me.grdOutput.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)
        ProductionMaster.TotalAmount = Me.grdOutput.GetTotal(Me.grdOutput.RootTable.Columns("NetAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
        ProductionMaster.IGPNo = String.Empty
        ProductionMaster.Remarks = Me.txtRemarks.Text
        ProductionMaster.UserName = LoginUserName
        ProductionMaster.FDate = Date.Now
        ProductionMaster.Post = IIf(Me.cbApproved.Checked = True, 1, 0)
        ProductionMaster.RefDocument = String.Empty
        ProductionMaster.RefDispatchNo = String.Empty
        ProductionMaster.IssuedStore = 0
        ProductionMaster.PlanTicketId = 0
        ProductionMaster.EmployeeID = 0

        ProductionMaster.ProductionDetail = New List(Of ProductionDetail)

        ProductionMaster.StockMaster = New StockMaster
        ProductionMaster.StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtProductionNo).ToString)
        ProductionMaster.StockMaster.DocNo = Me.txtProductionNo
        ProductionMaster.StockMaster.DocDate = Me.dtpProductionDate.Value.Date
        ProductionMaster.StockMaster.DocType = Convert.ToInt32(GetStockDocTypeId("Production"))
        ProductionMaster.StockMaster.Remaks = Me.txtRemarks.Text
        ProductionMaster.StockMaster.Project = 0
        ProductionMaster.StockMaster.AccountId = 0
        ProductionMaster.StockMaster.StockDetailList = New List(Of StockDetail) 'Stock Detail Object
        ProductionMaster.PlanId = 0
        ProductionMaster.DepartmentId = 0 'Task:2690 Set DepertmentId Value.
        ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
        ProductionMaster.EmployeeID = 0
        'End Task:2753
        ProductionMaster.CGSAccountId = Me.cmbCGAccount.Value


        For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grdOutput.GetRows

            ProductionDetail = New ProductionDetail
            ProductionDetail.Location_ID = grdRow.Cells("LocationId").Value
            ProductionDetail.ArticledefID = grdRow.Cells("ItemId").Value
            ProductionDetail.ArticleSize = grdRow.Cells("Unit").Text
            ProductionDetail.Sz1 = Val(grdRow.Cells("Qty").Value)
            ProductionDetail.Sz2 = 0
            ProductionDetail.Sz3 = 0
            ProductionDetail.Sz4 = 0
            ProductionDetail.Sz5 = 0
            ProductionDetail.Sz6 = 0
            ProductionDetail.Sz7 = Val(grdRow.Cells("PackQty").Value)

            ProductionDetail.Qty = Val(grdRow.Cells("TotalQty").Value) ''TASK-408
            ProductionDetail.CurrentRate = Val(grdRow.Cells("Rate").Value.ToString)
            ''TASK TFS1496 addition of PackPrice
            ProductionDetail.PackPrice = 0
            ProductionDetail.Comments = String.Empty
            ''TASK TFS1772 addition of Dim1, Dim2
            ProductionDetail.Dim1 = 0
            ProductionDetail.Dim2 = 0
            'Altered Against Task 20150506 Ali Ansari
            ProductionDetail.EngineNo = String.Empty
            ProductionDetail.ChasisNo = String.Empty
            'Altered Against Task 20150506 Ali Ansari
            ''Task No 2555 Added The One Line Code To fill The Property of UOM 
            ProductionDetail.UOM = String.Empty
            ProductionDetail.Pack_Desc = grdRow.Cells("Unit").Text.ToString

            ''Task No 1616 Added The Lines of  Code To fill The Property of BatchNo,retailPrice,ExpiryDate, 
            ProductionDetail.BatchNo = txtBatchNo.Text
            ProductionDetail.RetailPrice = 0

            ProductionDetail.ExpiryDate = Nothing

            ProductionDetail.PurchaseAccountId = Convert.ToInt32(grdRow.Cells("SubSubId").Value.ToString)
            ProductionDetail.PlanDetailId = 0
            ProductionMaster.ProductionDetail.Add(ProductionDetail)

            StockDetail = New StockDetail 'Create New Stock Detail Object 
            StockDetail.StockTransId = ProductionMaster.StockMaster.StockTransId 'Convert.ToInt32(GetStockTransId(Me.txtProductionNo.Text).ToString)
            StockDetail.LocationId = grdRow.Cells("LocationId").Value
            StockDetail.ArticleDefId = grdRow.Cells("ItemId").Value
            StockDetail.InQty = Val(grdRow.Cells("TotalQty").Value.ToString) ''TASK-408
            StockDetail.OutQty = 0
            StockDetail.Rate = Val(grdRow.Cells("Rate").Value)

            StockDetail.InAmount = (Val(grdRow.Cells("TotalQty").Value.ToString) * Val(StockDetail.Rate)) ''TASK-408
            StockDetail.OutAmount = 0
            StockDetail.Remarks = String.Empty
            ''TASK-470 on 01-07-2016
            StockDetail.PackQty = Val(grdRow.Cells("PackQty").Value.ToString)
            StockDetail.In_PackQty = Val(grdRow.Cells("Qty").Value.ToString)
            StockDetail.Out_PackQty = 0
            ''End TASK-470
            'Ali Faisal : TFS1362 : 22-Aug-2017 : Fill model for Engine / Chassis number
            StockDetail.Engine_No = String.Empty
            StockDetail.Chassis_No = String.Empty
            'Ali Faisal : TFS1362 : 22-Aug-2017 : End
            'Ayesha Rehman : TFS1596 : 30-11-2017 
            StockDetail.BatchNo = txtBatchNo.Text
            'Ayesha Rehman : TFS1596: 30-11-2017 : End
            ProductionMaster.StockMaster.StockDetailList.Add(StockDetail) 'Collection Values of Stock Detail object 

            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "True" Then
                ProductionDetail.PurchaseAccountId = Val(getConfigValueByType("PurchaseDebitAccount").ToString)
                If Me.cmbCGAccount.ActiveRow.Cells(0).Value <= 0 Then ProductionDetail.CGSAccountId = Val(getConfigValueByType("StoreCreditAccount").ToString) Else ProductionDetail.CGSAccountId = Val(Me.cmbCGAccount.Value)
            Else
                ProductionDetail.PurchaseAccountId = Val(grdRow.Cells("SubSubId").Value.ToString)
                If Me.cmbCGAccount.ActiveRow.Cells(0).Value > 0 Then ProductionDetail.CGSAccountId = Val(Me.cmbCGAccount.Value) Else ProductionDetail.CGSAccountId = Val(getConfigValueByType("StoreCreditAccount").ToString)
            End If

        Next

    End Sub


    'Task 3420 Saad Afzaal Create saveProductionEntry Function '

    Public Function saveProductionEntry() As Boolean

        FillProductionEntryModel()

        If (New ProductionDAL().Add(ProductionMaster, True)) = True Then
            Return True
        Else
            Return False
        End If


    End Function

    'Task 3420 Saad Afzaal Create updateProductionEntry Function '

    Public Function updateProductionEntry() As Boolean

        FillProductionEntryModel()

        If New ProductionDAL().Update(ProductionMaster, True) = True Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>
    ''' TASK TFS3578
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks> User has option to have his/her own Total Qty</remarks>
    Private Sub grdInput_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdInput.CellUpdated
        Try
            If Me.grdInput.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "Qty" Or e.Column.Key = "PackQty" Then
                    If grdInput.GetRow.Cells("PackQty").Value > 0 Then
                        grdInput.GetRow.Cells("TotalQty").Value = grdInput.GetRow.Cells("PackQty").Value * grdInput.GetRow.Cells("Qty").Value
                    End If
                ElseIf e.Column.Key = "TotalQty" Then
                    If Not grdInput.GetRow.Cells("PackQty").Value > 1 Then
                        grdInput.GetRow.Cells("Qty").Value = grdInput.GetRow.Cells("TotalQty").Value
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class