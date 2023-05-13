Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmInvoiceDueReport
    Implements IGeneral
    Dim LeadProfile As LeadProfileBE
    Dim objDAL As LeadProfileDAL
    Dim objCommand As New OleDbCommand
    Dim objCon As OleDbConnection
    Dim flgCompanyRights As Boolean = False

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Vendor.Enabled = True
                Customer.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            Customer.Enabled = False
            Vendor.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True
                ElseIf RightsDt.FormControlName = "Customer" Then
                    Customer.Enabled = True
                ElseIf RightsDt.FormControlName = "Vendor" Then
                    Vendor.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("Invoice Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Invoice Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Invoice Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Not Due").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Not Due").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Not Due").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Due").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Due").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Due").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Over Due").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Over Due").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Over Due").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("coa_detail_id").Visible = False
            Me.grd.RootTable.Columns("Document Id").Visible = False
            Me.grd.RootTable.Columns("Invoice Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Not Due").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Due").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Over Due").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            Dim ShowVendorOnSales As Boolean = False
            Dim ShowMiscAccountsOnSales As Boolean = False

            If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
            End If
            If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
            End If
            Str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code],  dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                      "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(tblCustomer.CridtLimt,0) as CreditLimit " & _
                      "FROM dbo.tblCustomer INNER JOIN " & _
                      "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                      "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                      "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                    "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id   " _
                      & " WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                             & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                   "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
            If flgCompanyRights = True Then
                Str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                Str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code],  dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                     "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(tblCustomer.CridtLimt,0) as CreditLimit " & _
                     "FROM dbo.tblCustomer INNER JOIN " & _
                     "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                   "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id   " _
                     & " WHERE ( dbo.vwCOADetail.detail_title Is Not NULL ) "
                Str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") ) "
                If ShowVendorOnSales = True Then
                    Str += " And dbo.vwCOADetail.account_type in ('Customer','Vendor') "
                Else
                    Str += " AND (dbo.vwCOADetail.account_type in ('Customer')) "
                End If
            End If
            str += " AND vwCOADetail.Active=1"
            str += "order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbCustomer, str)
            If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("TypeId").Hidden = True
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("CreditLimit").Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Customer.Checked = True Then
                Me.grd.DataSource = SBDal.UtilityDAL.GetDataTable("SP_CustomerInvoiceDueNotDue")
                Me.grd.RetrieveStructure()
                grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                ApplyGridSettings()
            ElseIf Vendor.Checked = True Then
                Me.grd.DataSource = SBDal.UtilityDAL.GetDataTable("SP_VendorInvoiceDueNotDue")
                Me.grd.RetrieveStructure()
                grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
    End Function
    Private Sub GrdCostCenter_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Lead Profile"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ShowDataGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            GetAllRecords()
            CtrlGrdBar1_Load(Nothing, Nothing)
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            FillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Customer_CheckedChanged(sender As Object, e As EventArgs) Handles Customer.CheckedChanged, Vendor.CheckedChanged
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@coadetail", Me.cmbCustomer.Value)
            ShowReport("AccountSummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class