''TASK TFS4844 Customer should be selected automatically againt Complain No. Done on 25-10-2018 by Muhammad Amin 
''TASK TFS4847 Complaint No is not selected againt Edit Mode. Done by Muhammad Amin on 25-10-2018
''TASK TFS4846. If request is consmed partial then remaing items are not loaded. Done by Muhammad Amin on 26-10-2018
''TASK TFS4845 Location should be dropdown in grid and should be displayed location name instead of location code. Done on 29-10-2018 by Muhammad Amin 
''TASK TFS4848 Muhammad Amin: Voucher should be made againts any return entry. Done on 29-10-2018 by Muhammad Amin
''TASK TFS4895 Muhammad Amin : Purchase Price should be saved in Stock and new column Purchase Price in ComplaintReturnDetail as well. Done on 11-05-2018 
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmComplaintReturn

    Implements IGeneral
    Dim objModel As ComplaintReturnMasterBE
    Dim Detail As List(Of ComplaintReturnDetailBE)
    Dim objDal As New ComplaintReturnMasterDAL
    Public Shared ComplainReturnId As Integer
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHavePostRights As Boolean = False
    Public DoHavePrintRights As Boolean = False
    Public DoHaveFieldChooserRights As Boolean = False
    Public DoHaveExportRights As Boolean = False
    Dim IsFormOpened As Boolean = False
    Public Shared EditMode As Boolean = False
    Public Shared Post As Boolean = False
    Dim txtPONo As String
    Dim setVoucherNo As String = String.Empty
    Dim IsWIPAccount As Boolean = False
    Dim flgStoreIssuenceVoucher As Boolean = False
    Dim StockList As List(Of StockDetail)
    Dim StockDetail As StockDetail
    Dim flgCompanyRights As Boolean = False
    Dim setVoucherdate As DateTime
    Dim DispatchId As Integer = 0
    Dim Voucher As VouchersMaster


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub frmComplaintRequest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            frmComplaintReturnList.GetAllRecords()
            Me.dtpReturnDate.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmComplaintRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("Vendor")
            FillCombos("Complaint")
            UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbComplaintNo, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            'UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            Dim dt As DataTable
            dt = New ComplaintReturnMasterDAL().GetById(ComplainReturnId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    Me.txtComplaintReturnNo.Text = dt.Rows(0).Item("ComplaintReturnNo")
                    Me.dtpReturnDate.Value = dt.Rows(0).Item("ComplaintReturnDate")
                    Me.cmbVendor.Value = dt.Rows(0).Item("CustomerId")
                    ''TASK TFS4847
                    FillCombos("Complaints")
                    ''END TASK TFS4847
                    Me.cmbComplaintNo.Value = dt.Rows(0).Item("ComplaintId")
                    Me.txtReceivedBy.Text = dt.Rows(0).Item("ReceivedBy")
                    Me.txtContactNo.Text = dt.Rows(0).Item("ContactNo")
                    Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                    btnSave.Enabled = DoHaveUpdateRights
                    btnPost.Enabled = DoHavePostRights
                    ''TASK TFS4880
                    Me.CtrlGrdBar1.mGridPrint.Enabled = DoHavePrintRights
                    Me.CtrlGrdBar1.mGridExport.Enabled = DoHaveExportRights
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = DoHaveFieldChooserRights
                    ''END TASK TFS4880
                    Post = dt.Rows(0).Item("Posted")
                    EditMode = True
                    If Post = True Then
                        btnSave.Visible = False
                        btnPrint.Visible = True
                        btnPrint.Enabled = DoHavePrintRights
                        btnPost.Text = "UnPost"
                    Else
                        btnSave.Visible = True
                        btnSave.Text = "Update"
                        btnPrint.Visible = False
                        btnPost.Text = "Post"
                    End If
                Next
            Else
                ReSetControls()
            End If
            IsFormOpened = True
            'Me.dtpReturnDate.Focus()
            DisplayRecords(ComplainReturnId)
            Me.dtpReturnDate.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grd.RootTable.Columns("LocationId").Visible = True
            Me.grd.RootTable.Columns("ItemId").Visible = False
            Me.grd.RootTable.Columns("AlternateId").Visible = False
            Me.grd.RootTable.Columns("PurchasePrice").Visible = False

            Me.grd.RootTable.Columns("AlternateItem").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Item").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("TotalQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("CheckQty").EditType = Janus.Windows.GridEX.EditType.NoEdit

            Me.grd.RootTable.Columns("Item").Width = 250
            Me.grd.RootTable.Columns("AlternateItem").Width = 250

            Me.grd.RootTable.Columns("LocationId").Caption = "Location"

            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("TotalQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TotalQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("PurchasePrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PurchasePrice").TotalFormatString = "N" & DecimalPointInValue
            '' TASK TFS
            Dim dtLocation As DataTable = GetDataTable("SELECT location_id AS LocationId, location_name AS LocationName FROM tblDefLocation")
            Me.grd.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grd.RootTable.Columns("LocationId").HasValueList = True
            Me.grd.RootTable.Columns("LocationId").LimitToList = True
            Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "LocationId", "LocationName")
            '' 
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Vendor" Then
                Dim ShowVendorOnSales As Boolean = False
                Dim ShowMiscAccountsOnSales As Boolean = False
                If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                    ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
                End If
                If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                    ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
                End If
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                 "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.SaleMan, tblCustomer.Email, tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                                 "FROM dbo.tblCustomer LEFT OUTER JOIN " & _
                                 "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
                                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                 "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                 "WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                                 & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                       "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
                If flgCompanyRights = True Then
                    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If
                If LoginGroup = "Administrator" Then
                ElseIf GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) Then
                    str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.SaleMan, tblCustomer.Email, tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                                "FROM dbo.tblCustomer LEFT OUTER JOIN " & _
                                "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
                                "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                "WHERE (dbo.vwCOADetail.detail_title Is Not NULL) "
                    str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") ) "
                End If
                str += " AND vwCOADetail.Active=1"

                str += "order by tblCustomer.Sortorder, vwCOADetail.detail_title"
                FillUltraDropDown(cmbVendor, str)
                If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("TypeId").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("SaleMan").Hidden = False
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
                End If
            ElseIf Condition = "Complaint" Then
                ''Aashir Returned Complaint should not show.
                '' TASK TFS4844 : Addition of CustomerId field to select a Customer when a ComplainNo is selected. Done on 25-10-2018  
                'str = "SELECT ComplaintRequestMaster.ComplaintId, ComplaintRequestMaster.ComplaintNo, ISNULL(ComplaintRequestMaster.CustomerId, 0) AS CustomerId FROM ComplaintRequestMaster LEFT OUTER JOIN ComplaintReturnMaster ON ComplaintRequestMaster.ComplaintId = ComplaintReturnMaster.ComplaintId WHERE (ComplaintRequestMaster.Posted = 'True') AND ComplaintRequestMaster.ComplaintId NOT IN (Select ComplaintId from ComplaintReturnMaster)" & IIf(cmbVendor.Value > 0, "AND CustomerId = " & cmbVendor.Value & "", "") & " ORDER BY ComplaintRequestMaster.ComplaintId DESC"
                ''TASK TFS4846 Gotten RequestNo against Status
                str = "SELECT ComplaintRequestMaster.ComplaintId, ComplaintRequestMaster.ComplaintNo, ISNULL(ComplaintRequestMaster.CustomerId, 0) AS CustomerId FROM ComplaintRequestMaster  WHERE ISNULL(ComplaintRequestMaster.Posted, 0) = 1 AND Case When IsNull(ComplaintRequestMaster.Status, '') = '' Then 'Open' Else ComplaintRequestMaster.Status End = 'Open' " & IIf(cmbVendor.Value > 0, "AND CustomerId = " & cmbVendor.Value & "", "") & " ORDER BY ComplaintRequestMaster.ComplaintId DESC"
                FillUltraDropDown(cmbComplaintNo, str)
                If Me.cmbComplaintNo.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbComplaintNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbComplaintNo.DisplayLayout.Bands(0).Columns("CustomerId").Hidden = True
                End If
            ElseIf Condition = "Complaints" Then
                ''Aashir Returned Complaint should not show.
                '' TASK TFS4844 : Addition of CustomerId field to select a Customer when a ComplainNo is selected. Done on 25-10-2018  
                str = "SELECT ComplaintRequestMaster.ComplaintId, ComplaintRequestMaster.ComplaintNo, ISNULL(ComplaintRequestMaster.CustomerId, 0) AS CustomerId FROM ComplaintRequestMaster WHERE ISNULL(ComplaintRequestMaster.Posted, 0) = 1 " & IIf(cmbVendor.Value > 0, "AND CustomerId = " & cmbVendor.Value & "", "") & " ORDER BY ComplaintRequestMaster.ComplaintId DESC"
                FillUltraDropDown(cmbComplaintNo, str)
                If Me.cmbComplaintNo.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbComplaintNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbComplaintNo.DisplayLayout.Bands(0).Columns("CustomerId").Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New ComplaintReturnMasterBE
            objModel.ComplaintReturnId = ComplainReturnId
            objModel.ComplaintReturnNo = txtComplaintReturnNo.Text
            objModel.ComplaintReturnDate = dtpReturnDate.Value
            objModel.CustomerId = cmbVendor.Value
            objModel.ComplaintId = cmbComplaintNo.Value
            objModel.ReceivedBy = txtReceivedBy.Text
            objModel.ContactNo = txtContactNo.Text
            objModel.Remarks = txtRemarks.Text
            objModel.Post = Post
            ''TASK TFS4849 > Voucher entry
            Voucher = New VouchersMaster()
            Voucher.VoucherId = 0
            Voucher.BankDescription = ""
            Voucher.VoucherCode = txtComplaintReturnNo.Text
            Voucher.VoucherNo = Voucher.VoucherCode
            Voucher.VNo = Voucher.VoucherCode
            Voucher.VoucherDate = Now
            Voucher.Cheque_Status = False
            Voucher.ChequeDate = Now
            Voucher.FinancialYearId = 1
            Voucher.LocationId = 1
            Voucher.Post = True
            Voucher.Posted_UserName = LoginUserName
            Voucher.References = String.Empty
            Voucher.Remarks = txtRemarks.Text
            Voucher.Source = Me.Name
            Voucher.UserName = LoginUserName
            Voucher.VoucherTypeId = 1
            Voucher.CoaDetailId = 0
            Voucher.VoucherMonth = Now.Month
            Voucher.Reversal = True
            Voucher.ActivityLog = New ActivityLog()
            Voucher.ActivityLog.ApplicationName = "Accounts"
            Voucher.ActivityLog.FormName = Me.Name
            Voucher.ActivityLog.FormCaption = Me.Text
            Voucher.ActivityLog.User_Name = LoginUserName
            Voucher.ActivityLog.UserID = LoginUserId
            Voucher.ActivityLog.Source = Me.Name
            Voucher.ActivityLog.LogComments = String.Empty
            Voucher.ActivityLog.LogDateTime = Now
            Voucher.VoucherDatail = New List(Of VouchersDetail)
            ''END TASK TFS4849

            Detail = New List(Of ComplaintReturnDetailBE)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim WDetail As New ComplaintReturnDetailBE
                WDetail.LocationId = Row.Cells("LocationId").Value.ToString
                WDetail.ItemId = Row.Cells("ItemId").Value.ToString
                WDetail.AlternateId = Row.Cells("AlternateId").Value.ToString
                WDetail.Unit = Row.Cells("Unit").Value.ToString
                WDetail.Price = Row.Cells("Price").Value.ToString
                WDetail.PurchasePrice = Val(Row.Cells("PurchasePrice").Value.ToString)
                WDetail.Sz1 = Row.Cells("Qty").Value.ToString
                WDetail.Sz7 = Row.Cells("PackQty").Value.ToString
                WDetail.Qty = Row.Cells("TotalQty").Value.ToString
                WDetail.Comments = Row.Cells("Comments").Value.ToString
                WDetail.RequestDetailId = Row.Cells("RequestDetailId").Value.ToString
                Detail.Add(WDetail)
                ''TASK TFS4849
                ''Debit
                If WDetail.Price > 0 Then
                    Dim VoucherDetail As New VouchersDetail
                    VoucherDetail.CoaDetailId = Me.cmbVendor.Value
                    VoucherDetail.LocationId = Val(Row.Cells("LocationId").Value.ToString)
                    VoucherDetail.CostCenter = 1
                    VoucherDetail.VoucherDetailId = 0
                    VoucherDetail.VoucherId = 0
                    VoucherDetail.ReversalVoucherDetailId = 0
                    VoucherDetail.Comments = Row.Cells("Comments").Value.ToString
                    VoucherDetail.Cheque_No = ""
                    VoucherDetail.Cheque_Date = Nothing
                    VoucherDetail.DebitAmount = Val(Row.Cells("Price").Value.ToString) * Val(Row.Cells("TotalQty").Value.ToString)
                    VoucherDetail.CreditAmount = 0
                    VoucherDetail.CurrencyDebitAmount = Val(Row.Cells("Price").Value.ToString) * Val(Row.Cells("TotalQty").Value.ToString)
                    VoucherDetail.CurrencyCreditAmount = 0
                    VoucherDetail.Cheque_Clearance_Date = Nothing
                    VoucherDetail.Cheque_Status = False
                    VoucherDetail.contra_coa_detail_id = 0
                    VoucherDetail.EmpId = 0
                    Voucher.VoucherDatail.Add(VoucherDetail)
                    ''Credit
                    Dim VoucherDetail1 As New VouchersDetail
                    VoucherDetail1.CoaDetailId = Val(Row.Cells("SubSubId").Value.ToString)
                    VoucherDetail1.LocationId = Val(Row.Cells("LocationId").Value.ToString)
                    VoucherDetail1.CostCenter = 1
                    VoucherDetail1.VoucherDetailId = 0
                    VoucherDetail1.VoucherId = 0
                    VoucherDetail1.ReversalVoucherDetailId = 0
                    VoucherDetail1.Comments = Row.Cells("Comments").Value.ToString
                    VoucherDetail1.Cheque_No = ""
                    VoucherDetail.Cheque_Date = Nothing
                    VoucherDetail1.DebitAmount = 0
                    VoucherDetail1.CreditAmount = Val(Row.Cells("Price").Value.ToString) * Val(Row.Cells("TotalQty").Value.ToString)
                    VoucherDetail1.CurrencyDebitAmount = 0
                    VoucherDetail1.CurrencyCreditAmount = Val(Row.Cells("Price").Value.ToString) * Val(Row.Cells("TotalQty").Value.ToString)
                    VoucherDetail.Cheque_Clearance_Date = Nothing
                    VoucherDetail1.Cheque_Status = False
                    VoucherDetail1.contra_coa_detail_id = 0
                    VoucherDetail1.EmpId = 0
                    Voucher.VoucherDatail.Add(VoucherDetail1)
                End If
                ''END TASK TFS4849
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub
    Public Sub DisplayRecords(ByVal ComplainReturnId As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = " SELECT ComplaintReturnDetail.ComplaintReturnDetailId, ComplaintReturnDetail.ComplaintReturnId, ComplaintReturnDetail.LocationId, ComplaintReturnDetail.ItemId, ArticleDefView.ArticleDescription as Item, ComplaintReturnDetail.AlternateId, ArticleDefView_1.ArticleDescription AS AlternateItem,  ComplaintReturnDetail.Unit, ComplaintReturnDetail.Sz1 AS Qty, ComplaintReturnDetail.Sz7 AS PackQty, ComplaintReturnDetail.Qty AS TotalQty, ISNULL(ComplaintReturnDetail.PurchasePrice, 0) AS PurchasePrice, ComplaintReturnDetail.Price, ComplaintReturnDetail.Comments, ComplaintReturnDetail.RequestDetailId, ComplaintReturnDetail.Sz1 as CheckQty, ISNULL(ComplaintRequestDetail.Sz1, 0) AS RequestQty, ISNULL(ComplaintRequestDetail.DeliverdQty, 0) AS DeliveredQty, ISNULL(ArticleDefView.SubSubId, 0) AS SubSubId FROM ComplaintReturnDetail INNER JOIN tblDefLocation ON ComplaintReturnDetail.LocationId = tblDefLocation.location_id INNER JOIN ArticleDefView ON ComplaintReturnDetail.ItemId = ArticleDefView.ArticleId INNER JOIN ArticleDefView AS ArticleDefView_1 ON ComplaintReturnDetail.AlternateId = ArticleDefView_1.ArticleId " _
                & " LEFT OUTER JOIN  ComplaintRequestDetail ON ComplaintReturnDetail.RequestDetailId = ComplaintRequestDetail.ComplaintDetailId LEFT OUTER JOIN ArticleGroupDefTable ON ArticleDefView.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId WHERE ComplaintReturnDetail.ComplaintReturnId = " & ComplainReturnId & ""
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            dt.Columns("TotalQty").Expression = "Qty*PackQty"
            Me.grd.RootTable.Columns("ComplaintReturnDetailId").Visible = False
            Me.grd.RootTable.Columns("ComplaintReturnId").Visible = False
            Me.grd.RootTable.Columns("RequestDetailId").Visible = False
            Me.grd.RootTable.Columns("CheckQty").Visible = False
            Me.grd.RootTable.Columns("RequestQty").Visible = False
            Me.grd.RootTable.Columns("DeliveredQty").Visible = False
            Me.grd.RootTable.Columns("SubSubId").Visible = False
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.grd.RowCount = 0 Then
                msg_Error("No record found in grid")
                Return False
            End If
            If Me.txtReceivedBy.Text = "" Then
                msg_Error("Please Enter Received By")
                Return False
            End If
            If Me.txtContactNo.Text = "" Then
                msg_Error("Please Enter Contact No")
                Return False
            End If
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
            '    ''TASK TFS4848 
            '    If Val(Row.Cells("Price").Value.ToString) <= 0 Then
            '        msg_Error("Price greater than zero is required.")
            '        Return False
            '    End If
            '    ''END TASK TFS4848
            'Next
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtComplaintReturnNo.Text = GetDocumentNo()
            dtpReturnDate.Value = Date.Now
            If Me.cmbVendor.Rows.Count > 0 Then cmbVendor.Rows(0).Activate()
            If Me.cmbComplaintNo.Rows.Count > 0 Then cmbComplaintNo.Rows(0).Activate()
            txtReceivedBy.Text = ""
            Me.txtContactNo.Text = ""
            txtRemarks.Text = ""
            btnSave.Enabled = DoHaveSaveRights
            Me.btnSave.Text = "Save"
            Me.dtpReturnDate.Focus()
            EditMode = False
            ''TASK TFS4880
            Me.CtrlGrdBar1.mGridPrint.Enabled = DoHavePrintRights
            Me.CtrlGrdBar1.mGridExport.Enabled = DoHaveExportRights
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = DoHaveFieldChooserRights
            ''END TASK TFS4880
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo() As String
        Dim DocNo As String = String.Empty
        Try
            DocNo = GetNextDocNo("CRR-" & Format(Me.dtpReturnDate.Value, "yy") & Me.dtpReturnDate.Value.Month.ToString("00"), 4, "ComplaintReturnMaster", "ComplaintReturnNo")
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If ComplainReturnId = 0 Then
                    If objDal.Add(objModel) Then
                        If objDal.AddComplaintReturnDetail(Detail) Then
                            msg_Information("Record has been saved successfully.")
                            SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, txtComplaintReturnNo.Text, True)
                            ReSetControls()
                            ReSetControls("Detail")
                            Me.Close()
                        End If
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If objDal.Update(objModel, Voucher) Then
                        If objDal.AddComplaintReturnDetail(Detail) Then
                            msg_Information("Record has been Updated successfully.")
                            SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtComplaintReturnNo.Text, True)
                            ReSetControls()
                            Me.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If btnPost.Text = "Post" Then
                Post = True
            Else
                Post = False
            End If
            If IsValidate() = True Then
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                objModel.StockTransId = StockTransId(Me.txtComplaintReturnNo.Text)
                If objDal.Update(objModel, Voucher) Then
                    If objDal.AddComplaintReturnDetail(Detail) Then
                        msg_Information("Record has been Updated successfully.")
                        SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtComplaintReturnNo.Text, True)
                        ReSetControls()
                        Me.Close()
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbComplaintNo_ValueChanged(sender As Object, e As EventArgs) Handles cmbComplaintNo.ValueChanged
        Try

            If Me.cmbComplaintNo.ActiveRow Is Nothing Then Exit Sub
            Dim str As String = ""
            Dim dt As DataTable
            'str = "SELECT ComplaintReturnDetail.ComplaintReturnDetailId, ComplaintReturnDetail.ComplaintReturnId, ComplaintReturnDetail.LocationId, tblDefLocation.location_name as LocationName, ComplaintReturnDetail.ItemId, ArticleDefView.ArticleDescription as Item, ComplaintReturnDetail.AlternateId, ArticleDefView_1.ArticleDescription AS AlternateItem,  ComplaintReturnDetail.Unit, ComplaintReturnDetail.Sz1 AS Qty, ComplaintReturnDetail.Sz7 AS PackQty, ComplaintReturnDetail.Qty AS TotalQty, ComplaintReturnDetail.Price, ComplaintReturnDetail.Comments, ComplaintReturnDetail.RequestDetailId, (ISNULL(ComplaintRequestDetail.Sz1, 0) - ISNULL(ComplaintRequestDetail.DeliverdQty, 0)) as CheckQty FROM ComplaintReturnDetail INNER JOIN tblDefLocation ON ComplaintReturnDetail.LocationId = tblDefLocation.location_id INNER JOIN ArticleDefView ON ComplaintReturnDetail.ItemId = ArticleDefView.ArticleId INNER JOIN ArticleDefView AS ArticleDefView_1 ON ComplaintReturnDetail.AlternateId = ArticleDefView_1.ArticleId WHERE ComplaintReturnDetail.ComplaintReturnId = " & ComplainReturnId & ""

            '' Set zero price against TASK TFS4848 
            'str = "SELECT ComplaintRequestDetail.ComplaintDetailId as RequestDetailId, ComplaintRequestDetail.ComplaintId, ComplaintRequestDetail.LocationId, tblDefLocation.location_name as LocationName, ComplaintRequestDetail.ItemId, ArticleDefView.ArticleDescription as Item, ComplaintRequestDetail.ItemId as AlternateId, ArticleDefView.ArticleDescription as AlternateItem, ComplaintRequestDetail.Unit, (ISNULL(ComplaintRequestDetail.Sz1, 0) - ISNULL(ComplaintRequestDetail.DeliverdQty, 0)) as Qty, ComplaintRequestDetail.Sz7 as PackQty, ComplaintRequestDetail.Qty as TotalQty, ComplaintRequestDetail.Price, ComplaintRequestDetail.Comments FROM ComplaintRequestDetail LEFT OUTER JOIN ArticleDefView ON ComplaintRequestDetail.ItemId = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation ON ComplaintRequestDetail.LocationId = tblDefLocation.location_id where ComplaintRequestDetail.ComplaintId = " & cmbComplaintNo.Value & "AND (ISNULL(ComplaintRequestDetail.Sz1, 0) - ISNULL(ComplaintRequestDetail.DeliverdQty, 0)) <> 0"
            str = "SELECT ComplaintRequestDetail.ComplaintDetailId as RequestDetailId, ComplaintRequestDetail.ComplaintId, ComplaintRequestDetail.LocationId,  ComplaintRequestDetail.ItemId, ArticleDefView.ArticleDescription as Item, ComplaintRequestDetail.ItemId as AlternateId, ArticleDefView.ArticleDescription as AlternateItem, ComplaintRequestDetail.Unit, (ISNULL(ComplaintRequestDetail.Sz1, 0) - ISNULL(ComplaintRequestDetail.DeliverdQty, 0)) as Qty, ComplaintRequestDetail.Sz7 as PackQty, ComplaintRequestDetail.Qty as TotalQty, IsNull(ArticleDefView.PurchasePrice, 0) AS PurchasePrice, 0 AS Price, ComplaintRequestDetail.Comments, (ISNULL(ComplaintRequestDetail.Sz1, 0) - ISNULL(ComplaintRequestDetail.DeliverdQty, 0)) as CheckQty, ISNULL(ComplaintRequestDetail.Sz1, 0) AS RequestQty, ISNULL(ComplaintRequestDetail.DeliverdQty, 0) AS DeliveredQty, ISNULL(ArticleDefView.SubSubId, 0) AS SubSubId FROM ComplaintRequestDetail LEFT OUTER JOIN ArticleDefView ON ComplaintRequestDetail.ItemId = ArticleDefView.ArticleId LEFT OUTER JOIN ArticleGroupDefTable ON ArticleDefView.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId WHERE ComplaintRequestDetail.ComplaintId = " & cmbComplaintNo.Value & "AND (ISNULL(ComplaintRequestDetail.Sz1, 0) - ISNULL(ComplaintRequestDetail.DeliverdQty, 0)) <> 0"
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            dt.Columns("TotalQty").Expression = "Qty*PackQty"
            Me.grd.RootTable.Columns("ComplaintId").Visible = False
            Me.grd.RootTable.Columns("RequestDetailId").Visible = False
            Me.grd.RootTable.Columns("CheckQty").Visible = False
            Me.grd.RootTable.Columns("RequestQty").Visible = False
            Me.grd.RootTable.Columns("DeliveredQty").Visible = False
            Me.grd.RootTable.Columns("SubSubId").Visible = False

            ApplyGridSettings()
            ''TASK TFS4844 
            If cmbVendor.ActiveRow IsNot Nothing Then
                If Val(Me.cmbComplaintNo.ActiveRow.Cells("CustomerId").Value.ToString) > 0 Then
                    Me.cmbVendor.Value = Me.cmbComplaintNo.ActiveRow.Cells("CustomerId").Value
                End If
            End If
            ''END TASK TFS4844 
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_ValueChanged(sender As Object, e As EventArgs) Handles cmbVendor.ValueChanged
        ''Commented below lines agains TASK TFS4847
        'Try
        '    FillCombos("Complaint")
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtContactNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContactNo.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            frmFactoryAlternateItems.ShowDialog()
            If frmFactoryAlternateItems.ItemId > 0 Then
                Me.grd.GetRow.BeginEdit()
                Me.grd.GetRow.Cells("AlternateId").Value = frmFactoryAlternateItems.ItemId
                Me.grd.GetRow.Cells("ALternateItem").Value = frmFactoryAlternateItems.ItemName
                Me.grd.GetRow.EndEdit()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@ID", ComplainReturnId)
            ShowReport("ComplaintReturn")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_Leave(sender As Object, e As EventArgs) Handles cmbVendor.Leave
        ''called this event agains TASK TFS4847
        Try
            FillCombos("Complaint")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellValueChanged
        ''TASK TFS4852 
        'If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
        '    'Me.grd.UpdateData()
        '    If e.Column.Key = "PackQty" Then
        '        If grd.GetRow.Cells("Unit").Value.ToString = "Loose" Then
        '            grd.CancelCurrentEdit()
        '        End If
        '    ElseIf e.Column.Key = "Qty" Then
        '        If grd.GetRow.Cells("Qty").Value > grd.GetRow.Cells("CheckQty").Value Then
        '            grd.CancelCurrentEdit()
        '            msg_Error("You have entered greater Qty than available Qty")
        '        End If
        '    End If
        'End If
        ''END TASK TFS4852
    End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                'Me.grd.UpdateData()
                If e.Column.Key = "PackQty" Then
                    If grd.GetRow.Cells("Unit").Value.ToString = "Loose" Then
                        grd.CancelCurrentEdit()
                    End If
                ElseIf e.Column.Key = "Qty" Then
                    '' TASK TFS4846
                    'If (grd.GetRow.Cells("Qty").Value + grd.GetRow.Cells("CheckQty").Value) > grd.GetRow.Cells("RequestQty").Value Then
                    If grd.GetRow.Cells("Qty").Value > grd.GetRow.Cells("CheckQty").Value Then
                        Dim ChangedQty As Double = grd.GetRow.Cells("Qty").Value - grd.GetRow.Cells("CheckQty").Value
                        If EditMode = False Then
                            grd.CancelCurrentEdit()
                            msg_Error("You have entered greater Qty than available Qty")
                            Exit Sub
                        End If
                        If (ChangedQty + grd.GetRow.Cells("DeliveredQty").Value) > grd.GetRow.Cells("RequestQty").Value Then
                            grd.CancelCurrentEdit()
                            msg_Error("You have entered greater Qty than available Qty")
                        End If
                    End If
                    ''END TASK TFS4846
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS4880
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Complaint Return"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
