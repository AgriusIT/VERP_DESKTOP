Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class frmSalesInquiryApproval
    Implements IGeneral
    Dim SalesInquiryRightsList As List(Of SalesInquiryRights)
    Dim SalesInquiryRights As SalesInquiryRights
    Dim SalesInquiryRightsDAL As New SalesInquiryRightsDAL
    Dim IsUpdateMode As Boolean = False
    Dim Master As PurchaseInquiryMaster
    Dim arrFile As New List(Of String)
    Dim VendorId As Integer = 0I
    Dim SalesInquiryRightsId As Integer = 0I
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty
    Dim dtEmail As DataTable
    Dim AllFields As List(Of String)
    Dim EmailTemplate As String = String.Empty
    Dim objPath As String = String.Empty
    Dim AfterFieldsElement As String = String.Empty
    Dim CC As String = ""
    Dim BCC As String = ""
    Public Shared SalesInquiryId As String = ""
    Public Shared VendorQuotationDetailIds As String = ""
    Dim SaveRights As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim UnCheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdUnAssign.GetUncheckedRows
            Dim CheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdUnAssign.GetCheckedRows
            If CheckRows.Length <= 0 Then
                msg_Error("At least one row is required. Please select rows")
            Else
                If frmPurchaseInquiry.grdItems.DataSource Is Nothing Then
                    frmPurchaseInquiry.DisplayDetail(-1)
                End If
                For Each ROW As Janus.Windows.GridEX.GridEXRow In Me.grdUnAssign.GetCheckedRows
                    frmPurchaseInquiry.AddToGridFromSalesInquiry(ROW)
                Next
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FillCombos(ByVal Condition As String)
        Dim strQuery As String = String.Empty
        Try
            If Condition = "UnAssignedCustomer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Vendor', 'Customer')"
                    FillUltraDropDown(Me.cmbUnAssignCustomer, strQuery)
                    Me.cmbUnAssignCustomer.Rows(0).Activate()
                    Me.cmbUnAssignCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Customer')"
                    FillUltraDropDown(Me.cmbUnAssignCustomer, strQuery)
                    Me.cmbUnAssignCustomer.Rows(0).Activate()
                    Me.cmbUnAssignCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "UnAssignedInquiryNumber" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0) Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbUnAssignInquiryNumber, strQuery)
                Me.cmbUnAssignInquiryNumber.Rows(0).Activate()
                Me.cmbUnAssignInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "UnAssignedInquiryNumberAgainstCustomer" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Where SalesInquiryMaster.CustomerId =" & Me.cmbUnAssignCustomer.Value & " GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0) Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbUnAssignInquiryNumber, strQuery)
                Me.cmbUnAssignInquiryNumber.Rows(0).Activate()
                Me.cmbUnAssignInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "grdVendor" Then
                strQuery = String.Empty
                strQuery = "SELECT IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.Contact_Email as Email " _
                          & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor') and  vwCOADetail.coa_detail_id is not  null " _
                          & " Union Select 0, '" & strZeroIndexItem & "', '' "
                Dim dtVendor As DataTable = GetDataTable(strQuery)
                Me.grdUnAssign.RootTable.Columns("VendorId").ValueList.PopulateValueList(dtVendor.DefaultView, "coa_detail_id", "Name")
            
            ElseIf Condition = "AssignedCustomer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Vendor', 'Customer')"
                    FillUltraDropDown(Me.cmbAssignCustomer, strQuery)
                    Me.cmbAssignCustomer.Rows(0).Activate()
                    Me.cmbAssignCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Customer')"
                    FillUltraDropDown(Me.cmbAssignCustomer, strQuery)
                    Me.cmbAssignCustomer.Rows(0).Activate()
                    Me.cmbAssignCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "AssignedInquiryNumber" Then
                strQuery = "SELECT SalesInquiryId, SalesInquiryNo, SalesInquiryDate FROM SalesInquiryMaster Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbAssignInquiryNumber, strQuery)
                Me.cmbAssignInquiryNumber.Rows(0).Activate()
                Me.cmbAssignInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "AssignedInquiryNumberAgainstCustomer" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryMaster.CustomerId =" & Me.cmbAssignCustomer.Value & " Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbAssignInquiryNumber, strQuery)
                Me.cmbAssignInquiryNumber.Rows(0).Activate()
                Me.cmbAssignInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True

            ElseIf Condition = "PendingCustomer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Vendor', 'Customer')"
                    FillUltraDropDown(Me.cmbPendingCustomer, strQuery)
                    Me.cmbPendingCustomer.Rows(0).Activate()
                    Me.cmbPendingCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Customer')"
                    FillUltraDropDown(Me.cmbPendingCustomer, strQuery)
                    Me.cmbPendingCustomer.Rows(0).Activate()
                    Me.cmbPendingCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "PendingInquiryNumber" Then
                strQuery = "SELECT SalesInquiryId, SalesInquiryNo, SalesInquiryDate FROM SalesInquiryMaster Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbPendingInquiryNumber, strQuery)
                Me.cmbPendingInquiryNumber.Rows(0).Activate()
                Me.cmbPendingInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "PendingInquiryNumberAgainstCustomer" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryMaster.CustomerId =" & Me.cmbPendingCustomer.Value & " Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbPendingInquiryNumber, strQuery)
                Me.cmbPendingInquiryNumber.Rows(0).Activate()
                Me.cmbPendingInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True

            ElseIf Condition = "ApprovedCustomer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Vendor', 'Customer')"
                    FillUltraDropDown(Me.cmbApprovedCustomer, strQuery)
                    Me.cmbApprovedCustomer.Rows(0).Activate()
                    Me.cmbApprovedCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Customer')"
                    FillUltraDropDown(Me.cmbApprovedCustomer, strQuery)
                    Me.cmbApprovedCustomer.Rows(0).Activate()
                    Me.cmbApprovedCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "ApprovedInquiryNumber" Then
                strQuery = "SELECT SalesInquiryId, SalesInquiryNo, SalesInquiryDate FROM SalesInquiryMaster Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbApprovedInquiryNumber, strQuery)
                Me.cmbApprovedInquiryNumber.Rows(0).Activate()
                Me.cmbApprovedInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "ApprovedInquiryNumberAgainstCustomer" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryMaster.CustomerId =" & Me.cmbApprovedCustomer.Value & " Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbApprovedInquiryNumber, strQuery)
                Me.cmbApprovedInquiryNumber.Rows(0).Activate()
                Me.cmbApprovedInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnUnAssignShow_Click(sender As Object, e As EventArgs) Handles btnUnAssignShow.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Processing Please wait........"
            'DisplayDetail()
            ToolStripButton1_Click(Me, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnAssignShow_Click(sender As Object, e As EventArgs) Handles btnAssignShow.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Processing Please wait........"
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbAssignCustomer.Value > 0 Then
                CustomerId = Me.cmbAssignCustomer.Value
            End If
            If dtpAssignFromDate.Checked = True Then
                FromDate = dtpAssignFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpAssignToDate.Checked = True Then
                ToDate = dtpAssignToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbAssignInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbAssignInquiryNumber.Value
            End If
            Dim dtLoad As DataTable = SalesInquiryRightsDAL.GetAssigned(CustomerId, SalesInquiryId, FromDate, ToDate)
            Me.grdAssign.DataSource = dtLoad
            Me.grdAssign.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdAssign.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnPendingShow_Click(sender As Object, e As EventArgs) Handles btnPendingShow.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Processing Please wait........"
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbPendingCustomer.Value > 0 Then
                CustomerId = Me.cmbPendingCustomer.Value
            End If
            If dtpPendingFromDate.Checked = True Then
                FromDate = dtpPendingFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpPendingToDate.Checked = True Then
                ToDate = dtpPendingToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbPendingInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbPendingInquiryNumber.Value
            End If
            Dim dtLoad As DataTable = SalesInquiryRightsDAL.GetPending(CustomerId, SalesInquiryId, FromDate, ToDate)
            Me.grdPending.DataSource = dtLoad
            Me.grdPending.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdPending.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApprovedShow_Click(sender As Object, e As EventArgs) Handles btnApprovedShow.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Processing Please wait........"
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbApprovedCustomer.Value > 0 Then
                CustomerId = Me.cmbApprovedCustomer.Value
            End If
            If dtpApprovedFromDate.Checked = True Then
                FromDate = dtpApprovedFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpApprovedToDate.Checked = True Then
                ToDate = dtpApprovedToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbApprovedInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbApprovedInquiryNumber.Value
            End If
            Dim dtLoad As DataTable = SalesInquiryRightsDAL.GetApproved(CustomerId, SalesInquiryId, FromDate, ToDate)
            Me.grdApproved.DataSource = dtLoad
            Me.grdApproved.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdApproved.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub DisplayDetail()
        Try
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbUnAssignCustomer.Value > 0 Then
                CustomerId = Me.cmbUnAssignCustomer.Value
            End If
            If dtpUnAssignFromDate.Checked = True Then
                FromDate = dtpUnAssignFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpUnAssignToDate.Checked = True Then
                ToDate = dtpUnAssignToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbUnAssignInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbUnAssignInquiryNumber.Value
            End If
            Dim SalesInquiryDetailDAL As New SalesInquiryDetailDAL()
            Dim dt As New DataTable
            dt = SalesInquiryRightsDAL.GetAgainstSearch(CustomerId, SalesInquiryId, FromDate, ToDate)
            dt.AcceptChanges()
            Me.grdUnAssign.DataSource = dt
            Me.grdUnAssign.RootTable.Columns("VendorId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdUnAssign.RootTable.Columns("VendorId").HasValueList = True
            Me.grdUnAssign.RootTable.Columns("VendorId").LimitToList = True
            FillCombos("grdVendor")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.dtpUnAssignFromDate.Value = Date.Today.ToString("01-MMM-yyyy")
            FillCombos("UnAssignedCustomer")
            FillCombos("UnAssignedInquiryNumber")
            FillCombos("AssignedCustomer")
            FillCombos("AssignedInquiryNumber")
            FillCombos("PendingCustomer")
            FillCombos("PendingInquiryNumber")
            FillCombos("ApprovedCustomer")
            FillCombos("ApprovedInquiryNumber")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdUnAssign.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                SalesInquiryRightsDAL.Delete(Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString))
                SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, "", True)
                Me.grdUnAssign.GetRow.Delete()
                Me.grdUnAssign.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.grdUnAssign.DataSource = Nothing
            Me.grdAssign.DataSource = Nothing
            Me.grdPending.DataSource = Nothing
            Me.grdApproved.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs) Handles cmbUnAssignCustomer.ValueChanged, cmbAssignCustomer.ValueChanged, cmbPendingCustomer.ValueChanged, cmbApprovedCustomer.ValueChanged
        Try
            If Me.cmbUnAssignCustomer.Value > 0 Or Me.cmbAssignCustomer.Value > 0 Or Me.cmbPendingCustomer.Value > 0 Or Me.cmbApprovedCustomer.Value > 0 Then
                FillCombos("UnAssignedInquiryNumberAgainstCustomer")
                FillCombos("AssignedInquiryNumberAgainstCustomer")
                FillCombos("PendingInquiryNumberAgainstCustomer")
                FillCombos("ApprovedInquiryNumberAgainstCustomer")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos1(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords1(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Not grdUnAssign.RowCount > 0 Then
                msg_Error("Items grid is empty")
                Me.grdUnAssign.Focus() : IsValidate = False : Exit Function
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
        Try
            Me.grdUnAssign.UpdateData()
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Please wait........"
            SalesInquiryRightsList = New List(Of SalesInquiryRights)
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdUnAssign.GetCheckedRows
                Dim IsTrue As Boolean = row.Cells("Rights").Value
                SalesInquiryRights = New SalesInquiryRights
                SalesInquiryRights.SalesInquiryRightsId = Val(row.Cells("SalesInquiryRightsId").Value.ToString)
                SalesInquiryRights.GroupId = 0
                SalesInquiryRights.SalesInquiryDetailId = Val(row.Cells("SalesInquiryDetailId").Value.ToString)
                SalesInquiryRights.SalesInquiryId = Val(row.Cells("SalesInquiryId").Value.ToString)
                SalesInquiryRights.Rights = row.Cells("Rights").Value
                SalesInquiryRights.UserName = LoginUserName
                SalesInquiryRights.Status = "Open"
                SalesInquiryRights.Qty = Val(row.Cells("Qty").Value.ToString)
                SalesInquiryRights.PurchasedQty = Val(row.Cells("PurchasedQty").Value.ToString)
                SalesInquiryRights.VendorId = Val(row.Cells("VendorId").Value.ToString)
                SalesInquiryRights.IsPurchaseInquiry = IIf(row.Cells("IsPurchaseInquiry").Value = 1, True, False)
                SalesInquiryRights.RequirementDescription = row.Cells("RequirementDescription").Value.ToString
                For Each SelectedItem As DataRowView In Me.lstUnAssignGroups.Items
                    Dim Group As New SalesInquiryRightsGroups
                    Group.GroupId = Val(SelectedItem.Item(0).ToString)
                    SalesInquiryRights.Groups.Add(Group)
                Next
                For Each SelectedItem1 As DataRowView In Me.lstUnAssignUsers.Items
                    Dim User As New SalesInquiryRightsUsers
                    User.UserId = Val(SelectedItem1.Item(0).ToString)
                    SalesInquiryRights.Users.Add(User)
                Next
                Dim GNotification As New AgriusNotifications
                '// Preparing notification title string
                GNotification.NotificationTitle = "Sales inquiry [" & lblUnAssignInquiryNo.Text & "]  is assigned."

                '// Preparing notification description string
                GNotification.NotificationDescription = "Sales inquiry [" & lblUnAssignInquiryNo.Text & "] is assigned by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                SalesInquiryRights.Notification = GNotification

                SalesInquiryRightsDAL.Add(SalesInquiryRights)
            Next
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Public Function SaveSingle(Optional Condition As String = "") As Boolean
        Try
            Me.grdUnAssign.UpdateData()
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Please wait........"
            SalesInquiryRightsList = New List(Of SalesInquiryRights)
            Dim IsTrue As Boolean = Me.grdUnAssign.GetRow.Cells("Rights").Value
            SalesInquiryRights = New SalesInquiryRights
            SalesInquiryRights.SalesInquiryRightsId = Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString)
            SalesInquiryRights.SalesInquiryDetailId = Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryDetailId").Value.ToString)
            SalesInquiryRights.SalesInquiryId = Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryId").Value.ToString)
            SalesInquiryRights.Rights = True
            SalesInquiryRights.UserName = LoginUserName
            SalesInquiryRights.Status = "Open"
            SalesInquiryRights.Qty = Val(Me.grdUnAssign.GetRow.Cells("Qty").Value.ToString)
            SalesInquiryRights.PurchasedQty = Val(Me.grdUnAssign.GetRow.Cells("PurchasedQty").Value.ToString)
            SalesInquiryRights.VendorId = Val(Me.grdUnAssign.GetRow.Cells("VendorId").Value.ToString)
            SalesInquiryRights.IsPurchaseInquiry = True
            For Each SelectedItem As DataRowView In Me.lstUnAssignGroups.SelectedItems
                Dim Group As New SalesInquiryRightsGroups
                Group.GroupId = Val(SelectedItem.Item(0).ToString)
                SalesInquiryRights.Groups.Add(Group)
            Next
            For Each SelectedItem As DataRowView In Me.lstUnAssignUsers.SelectedItems
                Dim User As New SalesInquiryRightsUsers
                User.UserId = Val(SelectedItem.Item(0).ToString)
                SalesInquiryRights.Users.Add(User)
            Next
            SalesInquiryRightsId = SalesInquiryRightsDAL.AddSingle(SalesInquiryRights)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() Then
                If Save() Then
                    SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, "", True)
                    msg_Information("Record has been updated successfully.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim Groups As String = String.Empty
        Try
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbUnAssignCustomer.Value > 0 Then
                CustomerId = Me.cmbUnAssignCustomer.Value
            End If
            If dtpUnAssignFromDate.Checked = True Then
                FromDate = dtpUnAssignFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpUnAssignToDate.Checked = True Then
                ToDate = dtpUnAssignToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbUnAssignInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbUnAssignInquiryNumber.Value
            End If
            For Each SelectedItems As DataRowView In lstUnAssignGroups.SelectedItems
                If Groups = String.Empty Then
                    Groups = SelectedItems.Item(0).ToString
                Else
                    Groups += "," & SelectedItems.Item(0).ToString
                End If
            Next
            Dim dtLoad As DataTable = SalesInquiryRightsDAL.GetAgainstGroups(Groups, CustomerId, SalesInquiryId, FromDate, ToDate)
            Me.grdUnAssign.DataSource = dtLoad
            Me.grdUnAssign.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdUnAssign.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                SaveRights = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    SaveRights = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesInquiryRights)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If


            Else
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                SaveRights = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    ElseIf RightsDt.FormControlName = "Save" Then
                        SaveRights = True
                        Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                    ElseIf RightsDt.FormControlName = "Export" Then
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            Me.cmbUnAssignCustomer.Rows(0).Activate()
            Me.cmbUnAssignInquiryNumber.Rows(0).Activate()
            FillListBox(Me.lstUnAssignGroups, "select * from tblUserGroup WHERE GroupId in(0)  AND Active=1")
            FillListBox(Me.lstUnAssignUsers, "Select User_Id, User_Name,FullName from tblUser WHERE User_Id in(0) ")
            IsUpdateMode = False
            If Me.grdUnAssign.RowCount > 0 Then
                Me.grdUnAssign.DataSource = Nothing
            End If
            Me.txtUnAssignDescription.Text = ""
            Me.lblUnAssignCustomerName.Text = ""
            Me.lblUnAssignInquiryNo.Text = ""

            Me.cmbAssignCustomer.Rows(0).Activate()
            Me.cmbAssignInquiryNumber.Rows(0).Activate()
            IsUpdateMode = False
            If Me.grdAssign.RowCount > 0 Then
                Me.grdAssign.DataSource = Nothing
            End If
            Me.txtAssignDescription.Text = ""
            Me.lblAssignCustomerName.Text = ""
            Me.lblAssignInquiryNo.Text = ""

            Me.cmbPendingCustomer.Rows(0).Activate()
            Me.cmbPendingInquiryNumber.Rows(0).Activate()
            IsUpdateMode = False
            If Me.grdPending.RowCount > 0 Then
                Me.grdPending.DataSource = Nothing
            End If
            Me.txtPendingDescription.Text = ""
            Me.lblPendingCustomerName.Text = ""
            Me.lblPendingInquiryNo.Text = ""

            Me.cmbApprovedCustomer.Rows(0).Activate()
            Me.cmbApprovedInquiryNumber.Rows(0).Activate()
            IsUpdateMode = False
            If Me.grdApproved.RowCount > 0 Then
                Me.grdApproved.DataSource = Nothing
            End If
            Me.txtApprovedDescription.Text = ""
            Me.lblApprovedCustomerName.Text = ""
            Me.lblApprovedInquiryNo.Text = ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdUnAssign.CellEdited
        Try
            If Me.grdUnAssign.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "RequirementDescription" Then
                    SalesInquiryRightsDAL.UpdateRequirementDescription(Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryDetailId").Value.ToString), Me.grdUnAssign.GetRow.Cells("RequirementDescription").Value.ToString)
                ElseIf e.Column.Key = "VendorId" Then
                    If Val(Me.grdUnAssign.GetRow.Cells("VendorId").Value.ToString) > 0 Then
                        If SalesInquiryRightsDAL.IsPurchaseInquiry(Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryDetailId").Value.ToString)) Then
                            ShowErrorMessage("Purchase Inquiry has already been created.")
                            Exit Sub
                        Else
                            If Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString) > 0 Then
                                SavePurchaseInquiry()
                                Me.grdUnAssign.GetRow.Cells("IsPurchaseInquiry").Value = True
                                msg_Information("Record has been saved successfully.")
                                SendEmail()
                            Else
                                If Me.lstUnAssignUsers.SelectedItems.Count = 0 Then
                                    ShowErrorMessage("At least one user selection is required.")
                                    Exit Sub
                                End If
                                SaveSingle()
                                SavePurchaseInquiry()
                                Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value = SalesInquiryRightsId
                                Me.grdUnAssign.GetRow.Cells("IsPurchaseInquiry").Value = True
                                msg_Information("Record has been saved successfully.")
                                SendEmail()
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetUsers(ByVal GroupId As Integer)
        Try
            Dim dtUser As New DataTable
            dtUser = New UsersDAL().GetAllRecordByGroupId(GroupId)
            For Each dr As DataRow In dtUser.Rows
                dr.BeginEdit()
                dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                dr.EndEdit()
            Next
            dtUser.AcceptChanges()
            Me.lstUnAssignUsers.DataSource = dtUser
            Me.lstUnAssignUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
            Me.lstUnAssignUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetGroupsUsers(ByVal Groups As String)
        Try
            Dim dtUser As New DataTable
            dtUser = New UsersDAL().GetGroupsUsers(Groups)
            For Each dr As DataRow In dtUser.Rows
                dr.BeginEdit()
                dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                dr.EndEdit()
            Next
            dtUser.AcceptChanges()
            Me.lstUnAssignUsers.DataSource = dtUser
            Me.lstUnAssignUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
            Me.lstUnAssignUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdItems_DoubleClick(sender As Object, e As EventArgs) Handles grdUnAssign.DoubleClick
        Try
            If grdUnAssign.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                If Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString) > 0 Then
                    Dim dtUsers As DataTable = SalesInquiryRightsDAL.GetUsers(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString)
                    If dtUsers.Rows.Count > 0 Then
                        For Each drUser As DataRow In dtUsers.Rows
                            Me.lstUnAssignUsers.SelectedValue = drUser.Item(0)
                        Next
                    Else
                        Me.lstUnAssignUsers.SelectedItems.Clear()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SavePurchaseInquiry(Optional Condition As String = "")
        Try
            Me.grdUnAssign.UpdateData()
            Dim MasterDAL As New PurchaseInquiryDAL
            Master = New PurchaseInquiryMaster()
            Master.PurchaseInquiryId = 0
            Master.PurchaseInquiryNo = GetPIDocumentNo()
            Master.PurchaseInquiryDate = Now
            Master.IndentNo = ""
            Master.IndentingDepartment = ""
            Master.OldInquiryNo = ""
            Master.DueDate = DateTime.Now.AddDays(1)
            Master.OldInquiryDate = DateTime.MinValue
            Master.Remarks = ""
            Master.UserName = LoginUserName
            Dim objModel As New PurchaseInquiryDetail()
            objModel.PurchaseInquiryDetailId = 0
            objModel.PurchaseInquiryId = 0
            objModel.SerialNo = Me.grdUnAssign.GetRow.Cells("SerialNo").Value.ToString
            objModel.RequirementDescription = ReplaceNewLine(Me.grdUnAssign.GetRow.Cells("RequirementDescription").Value.ToString, False)
            objModel.ArticleId = Val(Me.grdUnAssign.GetRow.Cells("ArticleId").Value.ToString)
            objModel.UnitId = Val(Me.grdUnAssign.GetRow.Cells("UnitId").Value.ToString)
            objModel.ItemTypeId = Val(Me.grdUnAssign.GetRow.Cells("ItemTypeId").Value.ToString)
            objModel.OriginId = Val(Me.grdUnAssign.GetRow.Cells("OriginId").Value.ToString)
            objModel.CategoryId = Val(Me.grdUnAssign.GetRow.Cells("CategoryId").Value.ToString)
            objModel.SubCategoryId = Val(Me.grdUnAssign.GetRow.Cells("SubCategoryId").Value.ToString)
            objModel.Qty = Val(Me.grdUnAssign.GetRow.Cells("Qty").Value.ToString)
            objModel.ReferenceNo = "" 
            objModel.Comments = Me.grdUnAssign.GetRow.Cells("Comments").Value.ToString
            objModel.SalesInquiryId = Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryId").Value.ToString)
            objModel.SalesInquiryDetailId = Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryDetailId").Value.ToString)
            Master.DetailList.Add(objModel)
            Dim objVendors As New PurchaseInquiryVendors()
            objVendors.PurchaseInquiryVendorsId = 0
            objVendors.PurchaseInquiryId = 0
            objVendors.VendorId = Me.grdUnAssign.GetRow.Cells("VendorId").Value.ToString
            objVendors.Email = SalesInquiryRightsDAL.GetVendorEmail(Me.grdUnAssign.GetRow.Cells("VendorId").Value) '"" ''Me.grdItems.RootTable.Columns("VendorId").Drop
            Master.VendorsList.Add(objVendors)
            MasterDAL.Add(Master, "frmPurchaseInquiry", "", arrFile, LoginGroupId, LoginUserId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetPIDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("RFQ-" + Microsoft.VisualBasic.Right(DateTime.Now.Year, 2) + "-", "PurchaseInquiryMaster", "PurchaseInquiryNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("RFQ-" & Format(DateTime.Now, "yy") & DateTime.Now.Month.ToString("00"), 4, "PurchaseInquiryMaster", "PurchaseInquiryNo")
            Else
                Return GetNextDocNo("RFQ-", 6, "PurchaseInquiryMaster", "PurchaseInquiryNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function ReplaceNewLine(ByVal selContent As String, ByVal isReplacingNewLineWithChar As Boolean, Optional ByVal selNewLineStringToUse As String = ".:.myCooLvbNewLine.:.") As String
        Try
            If isReplacingNewLineWithChar Then : Return selContent.Replace(vbNewLine, selNewLineStringToUse)
            Else : Return selContent.Replace(selNewLineStringToUse, vbNewLine)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grdUnAssignItems_CurrentCellChanging(sender As Object, e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles grdUnAssign.CurrentCellChanging
        Try
            If Not e.Row Is Nothing Then
                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If Not e.Column Is Nothing Then
                        If e.Column.Key = "VendorId" Then
                            VendorId = Me.grdUnAssign.GetRow.Cells("VendorId").Value
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdAssignItems_CurrentCellChanging(sender As Object, e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles grdAssign.CurrentCellChanging
        Try
            If Not e.Row Is Nothing Then
                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If Not e.Column Is Nothing Then
                        If e.Column.Key = "VendorId" Then
                            VendorId = Me.grdAssign.GetRow.Cells("VendorId").Value
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdPendingItems_CurrentCellChanging(sender As Object, e As Janus.Windows.GridEX.CurrentCellChangingEventArgs)
        Try
            If Not e.Row Is Nothing Then
                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If Not e.Column Is Nothing Then
                        If e.Column.Key = "VendorId" Then
                            VendorId = Me.grdPending.GetRow.Cells("VendorId").Value
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdApprovedItems_CurrentCellChanging(sender As Object, e As Janus.Windows.GridEX.CurrentCellChangingEventArgs)
        Try
            If Not e.Row Is Nothing Then
                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If Not e.Column Is Nothing Then
                        If e.Column.Key = "VendorId" Then
                            VendorId = Me.grdApproved.GetRow.Cells("VendorId").Value
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SendEmail()
        Try
            GetTemplate("Purchase Inquiry")
            If EmailTemplate.Length > 0 Then
                GetEmailData()
                FormatStringBuilder(dtEmail)
                CreateOutLookMail()
                CC = ""
                BCC = ""
            Else
                msg_Error("No email template is found for Purchase Inquiry.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty

        Try
            dtEmail = New DataTable
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    If Me.grdUnAssign.RootTable.Columns.Contains(TrimSpace) Then
                        dtEmail.Columns.Add(TrimSpace)
                        AllFields.Add(TrimSpace)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetVendorsEmails()
        Try
            Me.grdUnAssign.UpdateData()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdUnAssign.GetCheckedRows
                If VendorEmails.Length > 0 Then
                    VendorEmails += "; " & Row.Cells("Email").Value.ToString & ""
                Else
                    VendorEmails = "" & Row.Cells("Email").Value.ToString & ""
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetEmailData()
        Dim Dr As DataRow
        Try
            Dr = dtEmail.NewRow
            For Each col As String In AllFields
                If Me.grdUnAssign.GetRow.Table.Columns.Contains(col) Then
                    Dr.Item(col) = Me.grdUnAssign.GetRow.Cells(col).Value.ToString
                End If
            Next
            dtEmail.Rows.Add(Dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")
            html.Append("<tr>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")
            For Each row As DataRow In dt.Rows
                html.Append("<tr>")
                For Each column As DataColumn In dt.Columns
                    html.Append("<td>")
                    If column.ColumnName = "RequirementDescription" Then
                        Dim var = row(column.ColumnName).ToString.Split(System.Environment.NewLine.ToCharArray())
                        Dim Lines As String = ""
                        For Each Line As String In var
                            If Line.Length > 0 Then
                                If Lines.Length > 0 Then
                                    Lines += "<br/>" & Line
                                Else
                                    Lines = Line
                                End If
                            End If
                        Next
                        html.Append(Lines)
                    Else
                        html.Append(row(column.ColumnName))
                    End If
                    html.Append("</td>")
                Next
                html.Append("</tr>")
            Next

            html.Append("</table>")
            html.Append(AfterFieldsElement)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail()
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = ""
            mailItem.To = SalesInquiryRightsDAL.GetVendorEmail(Val(Me.grdUnAssign.GetRow.Cells("VendorId").Value.ToString))

            VendorEmails = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdItems_SelectionChanged(sender As Object, e As EventArgs) Handles grdUnAssign.SelectionChanged
        Try

            Me.SplitContainer2.Panel1Collapsed = False
            If Not grdUnAssign.GetRow Is Nothing AndAlso grdUnAssign.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                Me.txtUnAssignDescription.Text = Me.grdUnAssign.GetRow.Cells("RequirementDescription").Value.ToString

                Me.lblUnAssignCustomerName.Text = Me.grdUnAssign.GetRow.Cells("detail_title").Value.ToString
                Me.lblUnAssignInquiryNo.Text = Me.grdUnAssign.GetRow.Cells("SalesInquiryNo").Value.ToString & "-" & Me.grdUnAssign.GetRow.Cells("SerialNo").Value.ToString

                If Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString) > 0 Then

                    FillListBox(Me.lstUnAssignGroups, "select * from tblUserGroup WHERE GroupId in(select GroupId from salesInquiryRightsGroups  where SalesInquiryRightsId=" & Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString) & ")  AND Active=1 ORDER BY SortOrder")

                    Dim dtUser As New DataTable
                    dtUser = GetDataTable("Select User_Id, User_Name,FullName from tblUser WHERE User_Id in (select UserId from salesInquiryRightsUsers where SalesInquiryRightsId=" & Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryRightsId").Value.ToString) & ") AND Active=1 ORDER BY SortOrder")
                    For Each dr As DataRow In dtUser.Rows
                        dr.BeginEdit()
                        dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                        dr.EndEdit()
                    Next
                    dtUser.AcceptChanges()
                    Me.lstUnAssignUsers.DataSource = dtUser
                    Me.lstUnAssignUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
                    Me.lstUnAssignUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString

                    Application.DoEvents()
                Else

                    FillListBox(Me.lstUnAssignGroups, "select * from tblUserGroup WHERE GroupId in(0)  AND Active=1 ORDER BY SortOrder")
                    FillListBox(Me.lstUnAssignUsers, "Select User_Id, User_Name,FullName from tblUser WHERE User_Id in(0) AND Active=1 ORDER BY SortOrder")

                End If

            Else

                Me.txtUnAssignDescription.Text = ""
                Me.lblUnAssignCustomerName.Text = ""
                Me.lblUnAssignInquiryNo.Text = ""

                FillListBox(Me.lstUnAssignGroups, "select * from tblUserGroup WHERE GroupId in(0)  AND Active=1 ORDER BY SortOrder")
                FillListBox(Me.lstUnAssignUsers, "Select User_Id, User_Name,FullName from tblUser WHERE User_Id in(0) AND Active=1 ORDER BY SortOrder")

            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdAssign_SelectionChanged(sender As Object, e As EventArgs) Handles grdAssign.SelectionChanged
        Try
            Me.SplitContainer2.Panel1Collapsed = False
            If Not grdAssign.GetRow Is Nothing AndAlso grdAssign.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.txtAssignDescription.Text = Me.grdAssign.GetRow.Cells("RequirementDescription").Value.ToString
                Me.lblAssignCustomerName.Text = Me.grdAssign.GetRow.Cells("detail_title").Value.ToString
                Me.lblAssignInquiryNo.Text = Me.grdAssign.GetRow.Cells("SalesInquiryNo").Value.ToString & "-" & Me.grdAssign.GetRow.Cells("SerialNo").Value.ToString
                Dim str As String = "SELECT SalesInquiryMaster.SalesInquiryId, COUNT(PurchaseInquiryMaster.PurchaseInquiryId) PurchaseInquiryCount FROM SalesInquiryMaster INNER JOIN PurchaseInquiryMaster ON SalesInquiryMaster.SalesInquiryId = PurchaseInquiryMaster.SalesInquiryId WHERE SalesInquiryMaster.SalesInquiryId = " & Val(Me.grdAssign.GetRow.Cells("SalesInquiryId").Value.ToString) & " GROUP BY SalesInquiryMaster.SalesInquiryId"
                Dim dt As DataTable = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    lblRFQCounter.Text = "RFQ Count = " & dt.Rows(0).Item(1).ToString
                Else
                    lblRFQCounter.Text = ""
                End If
                Dim str1 As String = "SELECT SalesInquiryMaster.SalesInquiryId, COUNT(VendorQuotationMaster.VendorQuotationId)VendorQuotationCount FROM SalesInquiryMaster INNER JOIN PurchaseInquiryMaster ON SalesInquiryMaster.SalesInquiryId = PurchaseInquiryMaster.SalesInquiryId INNER JOIN VendorQuotationMaster ON PurchaseInquiryMaster.PurchaseInquiryId = VendorQuotationMaster.PurchaseInquiryId WHERE SalesInquiryMaster.SalesInquiryId = " & Val(Me.grdAssign.GetRow.Cells("SalesInquiryId").Value.ToString) & " GROUP BY SalesInquiryMaster.SalesInquiryId"
                Dim dt1 As DataTable = GetDataTable(str1)
                If dt1.Rows.Count > 0 Then
                    lblQuotationCounter.Text = "Quotation Count = " & dt1.Rows(0).Item(1).ToString
                Else
                    lblQuotationCounter.Text = ""
                End If
            Else
                Me.txtAssignDescription.Text = ""
                Me.lblAssignCustomerName.Text = ""
                Me.lblAssignInquiryNo.Text = ""
                Me.lblQuotationCounter.Text = ""
                Me.lblRFQCounter.Text = ""
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdPending_SelectionChanged(sender As Object, e As EventArgs)
        Try
            Me.SplitContainer2.Panel1Collapsed = False
            If Not grdPending.GetRow Is Nothing AndAlso grdPending.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.txtPendingDescription.Text = Me.grdPending.GetRow.Cells("RequirementDescription").Value.ToString
                Me.lblPendingCustomerName.Text = Me.grdPending.GetRow.Cells("detail_title").Value.ToString
                Me.lblPendingInquiryNo.Text = Me.grdPending.GetRow.Cells("SalesInquiryNo").Value.ToString & "-" & Me.grdPending.GetRow.Cells("SerialNo").Value.ToString
            Else
                Me.txtPendingDescription.Text = ""
                Me.lblPendingCustomerName.Text = ""
                Me.lblPendingInquiryNo.Text = ""
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdApproved_SelectionChanged(sender As Object, e As EventArgs)
        Try
            Me.SplitContainer2.Panel1Collapsed = False
            If Not grdApproved.GetRow Is Nothing AndAlso grdApproved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.txtApprovedDescription.Text = Me.grdApproved.GetRow.Cells("RequirementDescription").Value.ToString
                Me.lblApprovedCustomerName.Text = Me.grdApproved.GetRow.Cells("detail_title").Value.ToString
                Me.lblApprovedInquiryNo.Text = Me.grdApproved.GetRow.Cells("SalesInquiryNo").Value.ToString & "-" & Me.grdApproved.GetRow.Cells("SerialNo").Value.ToString
            Else
                Me.txtApprovedDescription.Text = ""
                Me.lblApprovedCustomerName.Text = ""
                Me.lblApprovedInquiryNo.Text = ""
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDescription_Enter(sender As Object, e As EventArgs) Handles txtUnAssignDescription.Enter
        Try
            frmModProperty.KeyPreview = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDescription_Leave(sender As Object, e As EventArgs) Handles txtUnAssignDescription.Leave
        Try


            If grdUnAssign.GetRow.Cells("RequirementDescription").Value.ToString.Trim <> Me.txtUnAssignDescription.Text.ToString.Trim Then
                Me.lblProgress.Visible = True
                Application.DoEvents()
                SalesInquiryRightsDAL.UpdateRequirementDescription(Val(Me.grdUnAssign.GetRow.Cells("SalesInquiryDetailId").Value.ToString), Me.txtUnAssignDescription.Text)
                Me.grdUnAssign.CurrentRow.Cells("RequirementDescription").Value = Me.txtUnAssignDescription.Text
            End If

            Me.KeyPreview = True
            frmModProperty.KeyPreview = True

        Catch ex As Exception
            msg_Error(ex.Message)
        Finally

            Me.lblProgress.Visible = False

        End Try
    End Sub

    Private Sub txtDescription_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtUnAssignDescription.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            e.IsInputKey = False
        End If
    End Sub

    Private Sub lnkGroups_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUnAssignUsers.LinkClicked, lnkUnAssignGroups.LinkClicked
        Try
            frmSalesInquiryUserSelection.lblInquiryNo.Text = Me.lblUnAssignInquiryNo.Text
            frmSalesInquiryUserSelection.lblCustomerName.Text = Me.lblUnAssignCustomerName.Text

            Dim strIDs As String = String.Empty

            For Each obj As Object In Me.lstUnAssignUsers.Items
                If TypeOf obj Is DataRowView Then
                    Dim dr As DataRowView = CType(obj, DataRowView)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.lstUnAssignUsers.ValueMember).ColumnName)
                ElseIf TypeOf obj Is System.String Then
                    Dim strItem As String = CType(obj, String)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & strItem
                End If
            Next

            frmSalesInquiryUserSelection.OldUsers = strIDs

            For Each obj As Object In Me.lstUnAssignGroups.Items
                If TypeOf obj Is DataRowView Then
                    Dim dr As DataRowView = CType(obj, DataRowView)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.lstUnAssignGroups.ValueMember).ColumnName)
                ElseIf TypeOf obj Is System.String Then
                    Dim strItem As String = CType(obj, String)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & strItem
                End If
            Next

            frmSalesInquiryUserSelection.OldGroups = strIDs


            If frmSalesInquiryUserSelection.ShowDialog = Windows.Forms.DialogResult.OK Then
                If frmSalesInquiryUserSelection.lstUserGroups.SelectedIDs.Trim.Length > 0 Then
                    FillListBox(Me.lstUnAssignGroups, "select * from tblUserGroup WHERE GroupId in(" & frmSalesInquiryUserSelection.lstUserGroups.SelectedIDs & ")  AND Active=1 ORDER BY SortOrder")
                End If
                If frmSalesInquiryUserSelection.lstUsers.SelectedIDs.Trim.Length > 0 Then

                    Dim dtUser As New DataTable
                    dtUser = GetDataTable("Select User_Id, User_Name,FullName from tblUser WHERE User_Id in (" & frmSalesInquiryUserSelection.lstUsers.SelectedIDs & ") AND Active = 1 ORDER BY SortOrder ")
                    For Each dr As DataRow In dtUser.Rows
                        dr.BeginEdit()
                        dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                        dr.EndEdit()
                    Next
                    dtUser.AcceptChanges()
                    Me.lstUnAssignUsers.DataSource = dtUser
                    Me.lstUnAssignUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
                    Me.lstUnAssignUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString

                End If

                Me.BtnSave_Click(Me, Nothing)

            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSendForApproval_Click(sender As Object, e As EventArgs) Handles btnSendForApproval.Click
        Try
            If Me.grdAssign.GetCheckedRows.Length > 0 Then
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdAssign.GetCheckedRows
                    SalesInquiryRightsDAL.UpdateVendorQuotationDetail(Val(Row.Cells("VendorQuotationDetailId").Value), 2)
                Next
                msg_Information("Selected rows are sent for approval")
            Else
                msg_Error("Please select atleast one row to send for approval")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    
End Class
