''TASK TFS4673 Inquiry should alos be filtered against date range. Done by Muhammad Amin on 05-10-2018
Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class frmSalesInquiryRights
    Implements IGeneral
    Dim SalesInquiryRightsList As List(Of SalesInquiryRights)
    Dim SalesInquiryRights As SalesInquiryRights
    Dim SalesInquiryRightsDAL As New SalesInquiryRightsDAL
    Dim IsUpdateMode As Boolean = False
    Dim Master As PurchaseInquiryMaster
    Dim arrFile As New List(Of String)
    Dim VendorId As Integer = 0I
    Dim SalesInquiryRightsId As Integer = 0I
    ''
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty
    Dim dtEmail As DataTable
    Dim AllFields As List(Of String)
    Dim EmailTemplate As String = String.Empty
    'Dim arrFile As List(Of String)
    Dim objPath As String = String.Empty
    Dim AfterFieldsElement As String = String.Empty
    Dim CC As String = ""
    Dim BCC As String = ""
    ''
    Dim SaveRights As Boolean = False
    Dim AutoEmail As Boolean = False
    Dim UsersEmail As List(Of String)
    Dim EmailBody As String = String.Empty
    Dim dsEmail As DataSet
    Dim GroupIds As String = String.Empty
    Dim IsFormOpened As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim UnCheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdItems.GetUncheckedRows
            Dim CheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdItems.GetCheckedRows
            If CheckRows.Length <= 0 Then
                msg_Error("At least one row is required. Please select rows")
            Else
                If frmPurchaseInquiry.grdItems.DataSource Is Nothing Then
                    frmPurchaseInquiry.DisplayDetail(-1)
                End If
                For Each ROW As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
                    'frmPurchaseInquiry.ReSetControls()
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
            If Condition = "Customer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Vendor', 'Customer')"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Customer')"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "InquiryNumber" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId  GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0)  " & IIf(Me.dtpInquiryFromDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) >= CONVERT(DATETIME, N'" & dtpInquiryFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "', 102) ", "") & " " & IIf(Me.dtpInquiryToDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) <= CONVERT(DATETIME, N'" & dtpInquiryToDate.Value.ToString("yyyy-M-dd 23:59:59") & "', 102) ", "") & "  Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbInquiryNumber, strQuery)
                Me.cmbInquiryNumber.Rows(0).Activate()
                Me.cmbInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "InquiryNumberAgainstCustomer" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Where SalesInquiryMaster.CustomerId =" & Me.cmbReference.Value & "   GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0)  " & IIf(Me.dtpInquiryFromDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) >= CONVERT(DATETIME, N'" & dtpInquiryFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "', 102) ", "") & " " & IIf(Me.dtpInquiryToDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) <= CONVERT(DATETIME, N'" & dtpInquiryToDate.Value.ToString("yyyy-M-dd 23:59:59") & "', 102) ", "") & " Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbInquiryNumber, strQuery)
                Me.cmbInquiryNumber.Rows(0).Activate()
                Me.cmbInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "Groups" Then
                'Dim dt As DataTable = New UserGroupDAL().Getallrecord()
                ''Dim dr As DataRow
                ''dr = dt.NewRow
                ''dr(0) = Convert.ToInt32(0)
                ''dr(1) = ".... Select any value ...."
                ''dt.Rows.InsertAt(dr, 0)
                'Me.ListBox1.DataSource = dt
                'Me.ListBox1.ValueMember = dt.Columns(0).ColumnName
                'Me.ListBox1.DisplayMember = dt.Columns(1).ColumnName
            ElseIf Condition = "grdVendor" Then
                strQuery = String.Empty
                'strQuery = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                '        & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Email as Email " _
                '        & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor') and  vwCOADetail.coa_detail_id is not  null"
                strQuery = "SELECT IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.Contact_Email as Email " _
                          & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor') and  vwCOADetail.coa_detail_id is not  null " _
                          & " Union Select 0, '" & strZeroIndexItem & "', '' "
                Dim dtVendor As DataTable = GetDataTable(strQuery)
                Me.grdItems.RootTable.Columns("VendorId").ValueList.PopulateValueList(dtVendor.DefaultView, "coa_detail_id", "Name")
                'FillUltraDropDown(Me.cmbReference, strSQL)
                'Me.cmbReference.Rows(0).Activate()
                'Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Please wait........"
            'DisplayDetail()
            'Me.lblRecords.Text = "Un-assigned Records are Loaded."
            ToolStripButton1_Click(Me, Nothing)

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
            If Me.cmbReference.Value > 0 Then
                CustomerId = Me.cmbReference.Value
            End If
            If dtpInquiryFromDate.Checked = True Then
                FromDate = dtpInquiryFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpInquiryToDate.Checked = True Then
                ToDate = dtpInquiryToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbInquiryNumber.Value
            End If
            Dim SalesInquiryDetailDAL As New SalesInquiryDetailDAL()
            Dim dt As New DataTable
            'If Not Me.grdItems.DataSource Is Nothing Then
            '    dt = CType(Me.grdItems.DataSource, DataTable)
            'End If
            'Dim dt1 As DataTable = SalesInquiryDetailDAL.GetAgainstSearch(CustomerId, SalesInquiryId, FromDate, ToDate)
            dt = SalesInquiryRightsDAL.GetAgainstSearch(CustomerId, SalesInquiryId, FromDate, ToDate)

            'dt1.AcceptChanges()
            'dt.Merge(dt1)
            dt.AcceptChanges()
            Me.grdItems.DataSource = dt
            Me.grdItems.RootTable.Columns("VendorId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdItems.RootTable.Columns("VendorId").HasValueList = True
            Me.grdItems.RootTable.Columns("VendorId").LimitToList = True
            FillCombos("grdVendor")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.dtpInquiryFromDate.Value = Date.Today.ToString("01-MMM-yyyy")
            FillCombos("Customer")
            FillCombos("InquiryNumber")
            'Ali Faisal : TFS1308 : Comment this because there is no use of this combo box
            'FillCombos("Groups")
            Me.ComboBox1.SelectedIndex = 0
            'DisplayDetail(-1)
            ''TASK TFS4456
            If getConfigValueByType("AutoEmail").ToString = "True" Then
                AutoEmail = True
            Else
                AutoEmail = False
            End If
            ''END TASK TFS4456
            IsFormOpened = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                SalesInquiryRightsDAL.Delete(Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString))
                SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, "", True)
                Me.grdItems.GetRow.Delete()
                Me.grdItems.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.grdItems.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs) Handles cmbReference.ValueChanged
        Try
            If Me.cmbReference.Value > 0 Then
                FillCombos("InquiryNumberAgainstCustomer")
            Else
                FillCombos("InquiryNumber")
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

            'If ListBox1.SelectedIndex < 0 Then
            '    msg_Error("At least one group should be selected.")
            '    ListBox1.Focus() : IsValidate = False : Exit Function
            'End If

            If Not grdItems.RowCount > 0 Then
                msg_Error("Items grid is empty")
                Me.grdItems.Focus() : IsValidate = False : Exit Function
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
            Me.grdItems.UpdateData()
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Please wait........"
            'For Each SelectedItem As DataRowView In Me.ListBox1.SelectedItems
            SalesInquiryRightsList = New List(Of SalesInquiryRights)
            'Ali Faisal : TFS1309 : To Assign the Selected rows in the Grid on 16-Aug-2017
            'Dim row As Janus.Windows.GridEX.GridEXRow = grdItems.GetRow
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
                UsersEmail = New List(Of String)
                'If SalesInquiryRightsDAL.IsAssignedAlready(Val(SelectedItem.Item(0).ToString), Val(Row.Cells("SalesInquiryDetailId").Value.ToString), Row.Cells("Rights").Value) = False Then
                Dim IsTrue As Boolean = row.Cells("Rights").Value
                'If IsTrue AndAlso Val(Row.Cells("SalesInquiryRightsId").Value.ToString) = 0 Then
                SalesInquiryRights = New SalesInquiryRights
                SalesInquiryRights.SalesInquiryRightsId = Val(row.Cells("SalesInquiryRightsId").Value.ToString)
                SalesInquiryRights.GroupId = 0 ''Val(SelectedItem.Item(0).ToString) ''Val(Row.Cells("GroupId").Value.ToString)
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
                For Each SelectedItem As DataRowView In Me.ListBox1.Items
                    Dim Group As New SalesInquiryRightsGroups
                    Group.GroupId = Val(SelectedItem.Item(0).ToString)
                    SalesInquiryRights.Groups.Add(Group)
                    ''TASK TFS4456
                    If AutoEmail = True Then
                        Dim dtUsersEmail As DataTable = SalesInquiryRightsDAL.GetUsersEmail(Group.GroupId)
                        If dtUsersEmail.Rows.Count > 0 Then
                            For Each _row As DataRow In dtUsersEmail.Rows
                                'Dim UserEmail As New SalesInquiryRightsUsers
                                'UserEmail.ID = 0
                                'UserEmail.UserId = _row.Item("User_Id")
                                'UserEmail.Email = _row.Item("Email").ToString
                                'UserEmail.SalesInquiryRightsId = 0
                                If _row.Item("Email").ToString.Length > 0 Then
                                    If UsersEmail.Contains(_row.Item("Email").ToString) = False Then
                                        UsersEmail.Add(_row.Item("Email").ToString)
                                    End If
                                End If
                            Next
                        End If
                    End If
                    ''END TASK TFS4456
                Next

                For Each SelectedItem1 As DataRowView In Me.lbUsers.Items
                    Dim User As New SalesInquiryRightsUsers
                    User.UserId = Val(SelectedItem1.Item(0).ToString)
                    SalesInquiryRights.Users.Add(User)
                    ''TASK TFS4456
                    Dim UserEmail As New SalesInquiryRightsUsers
                    'UserEmail.ID = 0
                    'UserEmail.UserId = User.UserId
                    'UserEmail.Email = SelectedItem1.Item(3).ToString
                    'UserEmail.SalesInquiryRightsId = 0
                    'UsersEmail.Add(UserEmail)
                    If SelectedItem1.Item(3).ToString.Length > 0 Then
                        If UsersEmail.Contains(SelectedItem1.Item(3).ToString) = False Then
                            UsersEmail.Add(SelectedItem1.Item(3).ToString)
                        End If
                    End If
                    ''END TASK TFS4456
                Next

                Dim GNotification As New AgriusNotifications
                '// Preparing notification title string
                GNotification.NotificationTitle = "Sales inquiry [" & lblInquiryNo.Text & "]  is assigned."

                '// Preparing notification description string
                GNotification.NotificationDescription = "Sales inquiry [" & lblInquiryNo.Text & "] is assigned by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                SalesInquiryRights.Notification = GNotification

                If SalesInquiryRightsDAL.Add(SalesInquiryRights) = True Then
                   
                End If

            Next
            'Ali Faisal : TFS1309 : End
                'If Not Me.lbUsers.SelectedItem Is Nothing Then
                '    SalesInquiryRights.UserId = CType(Me.lbUsers.SelectedItem, DataRowView).Item(0)
                'Else
                '    SalesInquiryRights.UserId = 0
                'End If
                'SalesInquiryRightsList.Add(SalesInquiryRights)
                'ElseIf Val(Row.Cells("SalesInquiryRightsId").Value.ToString) > 0 Then
                '    SalesInquiryRights = New SalesInquiryRights
                '    SalesInquiryRights.SalesInquiryRightsId = Val(Row.Cells("SalesInquiryRightsId").Value.ToString)
                '    SalesInquiryRights.GroupId = Val(SelectedItem.Item(0).ToString) ''Val(Row.Cells("GroupId").Value.ToString)
                '    SalesInquiryRights.SalesInquiryDetailId = Val(Row.Cells("SalesInquiryDetailId").Value.ToString)
                '    SalesInquiryRights.SalesInquiryId = Val(Row.Cells("SalesInquiryId").Value.ToString)
                '    SalesInquiryRights.Rights = Row.Cells("Rights").Value
                '    SalesInquiryRights.UserName = LoginUserName
                '    SalesInquiryRights.Status = Row.Cells("Status").Value.ToString
                '    SalesInquiryRights.Qty = Val(Row.Cells("Qty").Value.ToString)
                '    SalesInquiryRights.PurchasedQty = Val(Row.Cells("PurchasedQty").Value.ToString)
                '    SalesInquiryRightsList.Add(SalesInquiryRights)
                'End If
                'End If
                'row.Delete()
                'Next
                'If SalesInquiryRightsList.Count > 0 Then
                '    SalesInquiryRightsDAL.Add(SalesInquiryRightsList)
                'End If
                'Next
            'Me.grdItems.GetCheckedRows.
            'If AutoEmail = True Then
            '    If UsersEmail.Count > 0 Then
            '        SendAutoEmail("Save")
            '    End If
            'End If
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
            Me.grdItems.UpdateData()
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Please wait........"
            SalesInquiryRightsList = New List(Of SalesInquiryRights)
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
            Dim IsTrue As Boolean = Me.grdItems.GetRow.Cells("Rights").Value
            SalesInquiryRights = New SalesInquiryRights
            SalesInquiryRights.SalesInquiryRightsId = Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString)
            SalesInquiryRights.SalesInquiryDetailId = Val(Me.grdItems.GetRow.Cells("SalesInquiryDetailId").Value.ToString)
            SalesInquiryRights.SalesInquiryId = Val(Me.grdItems.GetRow.Cells("SalesInquiryId").Value.ToString)
            SalesInquiryRights.Rights = True ''Me.grdItems.GetRow.Cells("Rights").Value
            SalesInquiryRights.UserName = LoginUserName
            SalesInquiryRights.Status = "Open"
            SalesInquiryRights.Qty = Val(Me.grdItems.GetRow.Cells("Qty").Value.ToString)
            SalesInquiryRights.PurchasedQty = Val(Me.grdItems.GetRow.Cells("PurchasedQty").Value.ToString)
            SalesInquiryRights.VendorId = Val(Me.grdItems.GetRow.Cells("VendorId").Value.ToString)
            SalesInquiryRights.IsPurchaseInquiry = True
            For Each SelectedItem As DataRowView In Me.ListBox1.SelectedItems
                Dim Group As New SalesInquiryRightsGroups
                Group.GroupId = Val(SelectedItem.Item(0).ToString)
                SalesInquiryRights.Groups.Add(Group)
            Next
            For Each SelectedItem As DataRowView In Me.lbUsers.SelectedItems
                Dim User As New SalesInquiryRightsUsers
                User.UserId = Val(SelectedItem.Item(0).ToString)
                SalesInquiryRights.Users.Add(User)
            Next
            SalesInquiryRightsId = SalesInquiryRightsDAL.AddSingle(SalesInquiryRights)
            'Next
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
                    'Me.cmbReference.Rows(0).Activate()
                    'If Me.grdItems.RowCount > 0 Then
                    '    Me.grdItems.DataSource = Nothing
                    'End If
                    ''TASK TFS4456
                    If AutoEmail = True Then
                        If UsersEmail.Count > 0 Then
                            SendAutoEmail("Save")
                        End If
                    End If
                    ''END TASK TFS4456
                    SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, "", True)
                    msg_Information("Record has been updated successfully.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Try
        '    'If Me.ListBox1.SelectedItems.Count = 1 Then
        '    'Dim obj As DataRowView = Me.ListBox1.SelectedItem
        '    'If Val(obj.Item(0).ToString) > 0 Then
        '    '    GetUsers(Val(obj.Item(0).ToString))
        '    '    Me.lbUsers.SelectedItems.Clear()
        '    'End If
        '    Dim Groups As String = String.Empty
        '    For Each Group As DataRowView In ListBox1.SelectedItems
        '        If Groups = "" Then
        '            Groups = Group.Item(0).ToString
        '        Else
        '            Groups += "," & Group.Item(0).ToString
        '        End If
        '    Next
        '    If Groups.Length > 0 Then
        '        GetGroupsUsers(Groups)
        '    End If
        '    'Else
        '    '    If Me.lbUsers.Items.Count > 0 Then
        '    '        lbUsers.DataSource = Nothing
        '    '        Me.lbUsers.Items.Clear()
        '    '    End If
        '    'End If
        '    'If Me.ListBox1.SelectedItems.Count = 1 Then
        '    '    Dim obj As DataRowView = Me.ListBox1.SelectedItem
        '    '    If Val(obj.Item(0).ToString) > 0 Then
        '    '        GetUsers(Val(obj.Item(0).ToString))
        '    '        Me.lbUsers.SelectedItems.Clear()
        '    '    End If
        '    'Else
        '    '    If Me.lbUsers.Items.Count > 0 Then
        '    '        lbUsers.DataSource = Nothing
        '    '        Me.lbUsers.Items.Clear()
        '    '    End If
        '    'End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim Groups As String = String.Empty
        Try
            ''03-03-2017
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbReference.Value > 0 Then
                CustomerId = Me.cmbReference.Value
            End If
            If dtpInquiryFromDate.Checked = True Then
                FromDate = dtpInquiryFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpInquiryToDate.Checked = True Then
                ToDate = dtpInquiryToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbInquiryNumber.Value
            End If
            '' End 03-03-2017
            For Each SelectedItems As DataRowView In ListBox1.SelectedItems
                If Groups = String.Empty Then
                    Groups = SelectedItems.Item(0).ToString
                Else
                    Groups += "," & SelectedItems.Item(0).ToString
                End If
            Next
            Dim dtLoad As DataTable = SalesInquiryRightsDAL.GetAgainstGroups(Groups, CustomerId, SalesInquiryId, FromDate, ToDate)
            Me.grdItems.DataSource = dtLoad
            Me.grdItems.RootTable.Columns("VendorId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdItems.RootTable.Columns("VendorId").HasValueList = True
            Me.grdItems.RootTable.Columns("VendorId").LimitToList = True
            FillCombos("grdVendor")
            Me.lblRecords.Text = "Assigned Records are Loaded."
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
                'Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    SaveRights = False
                    'Me.BtnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
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
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If


            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                SaveRights = False

                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'CtrlGrdBar1.mGridChooseFielder.Enabled = False

                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        SaveRights = True
                        '    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Update" Then
                        '    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        'Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then

                        'End Task:2395
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        'CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            Me.cmbReference.Rows(0).Activate()
            'Ali Faisal : TFS1308 : Reset Controls at the New button click event
            Me.cmbInquiryNumber.Rows(0).Activate()
            FillListBox(Me.ListBox1, "select * from tblUserGroup WHERE GroupId in(0)  AND Active=1")
            FillListBox(Me.lbUsers, "Select User_Id, User_Name,FullName, Email from tblUser WHERE User_Id in(0) ")
            'Ali Faisal : TFS1308 : End
            IsUpdateMode = False
            If Me.grdItems.RowCount > 0 Then
                Me.grdItems.DataSource = Nothing
            End If
            Me.lblRecords.Text = ""

            Me.txtDescription.Text = ""
            Me.lblCustomerName.Text = ""
            Me.lblInquiryNo.Text = ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.CellEdited
        Try
            If Me.grdItems.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "RequirementDescription" Then
                    SalesInquiryRightsDAL.UpdateRequirementDescription(Val(Me.grdItems.GetRow.Cells("SalesInquiryDetailId").Value.ToString), Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString)
                ElseIf e.Column.Key = "VendorId" Then
                    If Val(Me.grdItems.GetRow.Cells("VendorId").Value.ToString) > 0 Then
                        If SalesInquiryRightsDAL.IsPurchaseInquiry(Val(Me.grdItems.GetRow.Cells("SalesInquiryDetailId").Value.ToString)) Then
                            ShowErrorMessage("Purchase Inquiry has already been created.")
                            Exit Sub
                        Else
                            If Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString) > 0 Then
                                SavePurchaseInquiry()
                                Me.grdItems.GetRow.Cells("IsPurchaseInquiry").Value = True
                                msg_Information("Record has been saved successfully.")
                                SendEmail()
                            Else
                                If Me.lbUsers.SelectedItems.Count = 0 Then
                                    ShowErrorMessage("At least one user selection is required.")
                                    Exit Sub
                                End If
                                SaveSingle()
                                SavePurchaseInquiry()
                                Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value = SalesInquiryRightsId
                                Me.grdItems.GetRow.Cells("IsPurchaseInquiry").Value = True
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
            Me.lbUsers.DataSource = dtUser
            Me.lbUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
            Me.lbUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString
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
            Me.lbUsers.DataSource = dtUser
            Me.lbUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
            Me.lbUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdItems_DoubleClick(sender As Object, e As EventArgs) Handles grdItems.DoubleClick
        Try
            If grdItems.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                If Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString) > 0 Then
                    'Dim dtGroups As DataTable = SalesInquiryRightsDAL.GetGroups(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString)
                    'If dtGroups.Rows.Count > 0 Then
                    '    For Each drGroup As DataRow In dtGroups.Rows
                    '        Me.ListBox1.SelectedValue = drGroup.Item(0)
                    '    Next
                    'End If
                    Dim dtUsers As DataTable = SalesInquiryRightsDAL.GetUsers(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString)
                    If dtUsers.Rows.Count > 0 Then
                        For Each drUser As DataRow In dtUsers.Rows
                            Me.lbUsers.SelectedValue = drUser.Item(0)
                        Next
                    Else
                        Me.lbUsers.SelectedItems.Clear()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SavePurchaseInquiry(Optional Condition As String = "")
        Try
            Me.grdItems.UpdateData()
            'Me.grdVendors.UpdateData()
            Dim MasterDAL As New PurchaseInquiryDAL
            Master = New PurchaseInquiryMaster()
            Master.PurchaseInquiryId = 0
            Master.PurchaseInquiryNo = GetPIDocumentNo()
            Master.PurchaseInquiryDate = Now
            Master.IndentNo = ""
            Master.IndentingDepartment = ""
            'Master.PurchaseInquiryDate = Me.dtp.Value
            Master.OldInquiryNo = ""
            Master.DueDate = DateTime.Now.AddDays(1)
            'Master.OldInquiryDate = Me.dtpOldInquiryDate.Value
            Master.OldInquiryDate = DateTime.MinValue
            Master.Remarks = ""
            Master.UserName = LoginUserName
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetRows
            Dim objModel As New PurchaseInquiryDetail()
            objModel.PurchaseInquiryDetailId = 0
            objModel.PurchaseInquiryId = 0 'Me.grdItems.GetRow.Cells("p")
            objModel.SerialNo = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString
            objModel.RequirementDescription = ReplaceNewLine(Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString, False)
            objModel.ArticleId = Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString)
            objModel.UnitId = Val(Me.grdItems.GetRow.Cells("UnitId").Value.ToString)
            objModel.ItemTypeId = Val(Me.grdItems.GetRow.Cells("ItemTypeId").Value.ToString)
            objModel.OriginId = Val(Me.grdItems.GetRow.Cells("OriginId").Value.ToString)
            objModel.CategoryId = Val(Me.grdItems.GetRow.Cells("CategoryId").Value.ToString)
            objModel.SubCategoryId = Val(Me.grdItems.GetRow.Cells("SubCategoryId").Value.ToString)
            objModel.Qty = Val(Me.grdItems.GetRow.Cells("Qty").Value.ToString)
            objModel.ReferenceNo = "" 'Val(Me.grdItems.GetRow.Cells("SerialNo").Value.ToString)
            objModel.Comments = Me.grdItems.GetRow.Cells("Comments").Value.ToString
            objModel.SalesInquiryId = Val(Me.grdItems.GetRow.Cells("SalesInquiryId").Value.ToString)
            objModel.SalesInquiryDetailId = Val(Me.grdItems.GetRow.Cells("SalesInquiryDetailId").Value.ToString)
            Master.DetailList.Add(objModel)
            'Next
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdVendors.GetRows

            Dim objVendors As New PurchaseInquiryVendors()
            objVendors.PurchaseInquiryVendorsId = 0
            objVendors.PurchaseInquiryId = 0
            objVendors.VendorId = Me.grdItems.GetRow.Cells("VendorId").Value.ToString
            objVendors.Email = SalesInquiryRightsDAL.GetVendorEmail(Me.grdItems.GetRow.Cells("VendorId").Value) '"" ''Me.grdItems.RootTable.Columns("VendorId").Drop
            Master.VendorsList.Add(objVendors)
            'Next
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

    Private Sub grdItems_CurrentCellChanging(sender As Object, e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles grdItems.CurrentCellChanging
        Try
            If Not e.Row Is Nothing Then
                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If Not e.Column Is Nothing Then
                        If e.Column.Key = "VendorId" Then
                            VendorId = Me.grdItems.GetRow.Cells("VendorId").Value
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
                'GetVendorsEmails()
                FormatStringBuilder(dtEmail)
                CreateOutLookMail()
                'SaveCCBCC(CC, BCC)
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
            'GetEmailData()
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
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    If Me.grdItems.RootTable.Columns.Contains(TrimSpace) Then
                        If dtEmail.Columns.Contains(TrimSpace) = False Then
                            dtEmail.Columns.Add(TrimSpace)
                        End If
                        If AllFields.Contains(TrimSpace) = False Then
                            AllFields.Add(TrimSpace)
                        End If
                        'Else
                        '    msg_Error("'" & TrimSpace & "'column does not exist")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetVendorsEmails()
        Try
            Me.grdItems.UpdateData()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
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
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Try
            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            Dr = dtEmail.NewRow
            For Each col As String In AllFields
                If Me.grdItems.GetRow.Table.Columns.Contains(col) Then
                    Dr.Item(col) = Me.grdItems.GetRow.Cells(col).Value.ToString
                End If
            Next
            dtEmail.Rows.Add(Dr)
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            'Dim dt As DataTable = Me.GetData()

            'Building an HTML string.
            'Dim html As New StringBuilder()

            'Table start.
            'html.Append()
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")

            'Building the Header row.
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
            'string[] textLines = text.Split(new[]{ Environment.NewLine }, StringSplitOptions.None);
            'var result = input.Split(System.Environment.NewLine.ToCharArray());
            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                html.Append("<tr>")
                For Each column As DataColumn In dt.Columns
                    html.Append("<td>")
                    If column.ColumnName = "RequirementDescription" Then
                        Dim var = row(column.ColumnName).ToString.Split(System.Environment.NewLine.ToCharArray())
                        Dim Lines As String = ""
                        For Each Line As String In var
                            'Dim Line1 As String = Line.Replace(" ", "")
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

            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)
            'html.Append("</body>")
            'html.Append("</html>")

            '     'Append the HTML string to Placeholder.
            'PlaceHolder1.Controls.Add(New Literal() With { _
            '   .Text = html.ToString() _


            'sb.Append("<table border=1 cellspacing=1 cellpadding=1><thead>")
            'For colIndx As Integer = 0 To colIndx < dt.Columns.Count
            '    sb.Append("<th>")
            '    sb.Append(dt.Columns(colIndx).ColumnName)
            '    sb.Append("</th>")
            'Next
            'sb.Append("</thead>")
            ''//add data rows to html table
            'For rowIndx As Integer = 0 To rowIndx < dt.Rows.Count Step 1
            '    sb.Append("<tr>")
            '    For colIndx As Integer = 0 To colIndx < dt.Columns.Count Step 1
            '        sb.Append("<td>")
            '        dt.Rows(rowIndx)(colIndx).ToString()
            '        sb.Append("</td>")
            '    Next
            '    sb.Append("</tr>")
            'Next
            'sb.Append("</table>")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail(Optional ByVal _AutoEmail As Boolean = False, Optional ByVal DocumentNo As String = "")
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = DocumentNo
            mailItem.To = VendorEmails
            'VendorEmails = String.Empty
            'Dim dtAttachments As DataTable = MasterDAL.GetAttachments(Me.Name, PurchaseInquiryId)
            'If dtAttachments.Rows.Count > 0 Then
            '    For Each Row As DataRow In dtAttachments.Rows
            '        mailItem.Attachments.Add(Row.Item("Path").ToString & "\" & Row.Item("FileName").ToString, Outlook.OlAttachmentType.olEmbeddeditem, Nothing, "Picture")
            '    Next
            'End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            'mailItem.
            If _AutoEmail = False Then
                mailItem.Display(mailItem)
                mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
                EmailBody = html.ToString

            Else
                mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
                EmailBody = html.ToString
                mailItem.Send()
            End If
            mailItem = Nothing
            oApp = Nothing

            '     mailItem.Display(mailItem);
            'mailItem.HTMLBody = body + mailItem.HTMLBody;
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdItems_SelectionChanged(sender As Object, e As EventArgs) Handles grdItems.SelectionChanged
        Try

            Me.SplitContainer2.Panel1Collapsed = False
            If Not grdItems.GetRow Is Nothing AndAlso grdItems.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                Me.txtDescription.Text = Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString

                Me.lblCustomerName.Text = Me.grdItems.GetRow.Cells("detail_title").Value.ToString
                Me.lblInquiryNo.Text = Me.grdItems.GetRow.Cells("SalesInquiryNo").Value.ToString & "-" & Me.grdItems.GetRow.Cells("SerialNo").Value.ToString

                If Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString) > 0 Then

                    FillListBox(Me.ListBox1, "select * from tblUserGroup WHERE GroupId in(select GroupId from salesInquiryRightsGroups  where SalesInquiryRightsId=" & Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString) & ")  AND Active=1 ORDER BY SortOrder")

                    Dim dtUser As New DataTable
                    dtUser = GetDataTable("Select User_Id, User_Name,FullName from tblUser WHERE User_Id in (select UserId from salesInquiryRightsUsers where SalesInquiryRightsId=" & Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString) & ") AND Active=1 ORDER BY SortOrder")
                    For Each dr As DataRow In dtUser.Rows
                        dr.BeginEdit()
                        dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                        dr.EndEdit()
                    Next
                    dtUser.AcceptChanges()
                    Me.lbUsers.DataSource = dtUser
                    Me.lbUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
                    Me.lbUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString
           
                    Application.DoEvents()
                Else

                    FillListBox(Me.ListBox1, "select * from tblUserGroup WHERE GroupId in(0)  AND Active=1 ORDER BY SortOrder")
                    FillListBox(Me.lbUsers, "Select User_Id, User_Name,FullName from tblUser WHERE User_Id in(0) AND Active=1 ORDER BY SortOrder")

                End If

            Else

                Me.txtDescription.Text = ""
                Me.lblCustomerName.Text = ""
                Me.lblInquiryNo.Text = ""

                FillListBox(Me.ListBox1, "select * from tblUserGroup WHERE GroupId in(0)  AND Active=1 ORDER BY SortOrder")
                FillListBox(Me.lbUsers, "Select User_Id, User_Name,FullName from tblUser WHERE User_Id in(0) AND Active=1 ORDER BY SortOrder")

            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDescription_Enter(sender As Object, e As EventArgs) Handles txtDescription.Enter
        Try
            frmModProperty.KeyPreview = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDescription_Leave(sender As Object, e As EventArgs) Handles txtDescription.Leave
        Try


            If grdItems.GetRow.Cells("RequirementDescription").Value.ToString.Trim <> Me.txtDescription.Text.ToString.Trim Then
                Me.lblProgress.Visible = True
                Application.DoEvents()
                SalesInquiryRightsDAL.UpdateRequirementDescription(Val(Me.grdItems.GetRow.Cells("SalesInquiryDetailId").Value.ToString), Me.txtDescription.Text)
                Me.grdItems.CurrentRow.Cells("RequirementDescription").Value = Me.txtDescription.Text
            End If

            Me.KeyPreview = True
            frmModProperty.KeyPreview = True

        Catch ex As Exception
            msg_Error(ex.Message)
        Finally

            Me.lblProgress.Visible = False

        End Try
    End Sub

    Private Sub txtDescription_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtDescription.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            e.IsInputKey = False
        End If
    End Sub

    Private Sub lnkGroups_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUsers.LinkClicked, lnkGroups.LinkClicked
        Try
            'If SaveRights = False Then
            '    msg_Error("Sorry! your don't have rights change")
            '    Exit Sub
            'End If
            frmSalesInquiryUserSelection.lblInquiryNo.Text = Me.lblInquiryNo.Text
            frmSalesInquiryUserSelection.lblCustomerName.Text = Me.lblCustomerName.Text

            Dim strIDs As String = String.Empty

            For Each obj As Object In Me.lbUsers.Items
                If TypeOf obj Is DataRowView Then
                    Dim dr As DataRowView = CType(obj, DataRowView)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.lbUsers.ValueMember).ColumnName)
                ElseIf TypeOf obj Is System.String Then
                    Dim strItem As String = CType(obj, String)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & strItem
                End If
            Next

            frmSalesInquiryUserSelection.OldUsers = strIDs

            For Each obj As Object In Me.ListBox1.Items
                If TypeOf obj Is DataRowView Then
                    Dim dr As DataRowView = CType(obj, DataRowView)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.ListBox1.ValueMember).ColumnName)
                ElseIf TypeOf obj Is System.String Then
                    Dim strItem As String = CType(obj, String)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & strItem
                End If
            Next

            frmSalesInquiryUserSelection.OldGroups = strIDs


            If frmSalesInquiryUserSelection.ShowDialog = Windows.Forms.DialogResult.OK Then
                If frmSalesInquiryUserSelection.lstUserGroups.SelectedIDs.Trim.Length > 0 Then
                    FillListBox(Me.ListBox1, "select * from tblUserGroup WHERE GroupId in(" & frmSalesInquiryUserSelection.lstUserGroups.SelectedIDs & ")  AND Active=1 ORDER BY SortOrder")
                End If
                If frmSalesInquiryUserSelection.lstUsers.SelectedIDs.Trim.Length > 0 Then

                    Dim dtUser As New DataTable
                    dtUser = GetDataTable("Select User_Id, User_Name,FullName, Email from tblUser WHERE User_Id in (" & frmSalesInquiryUserSelection.lstUsers.SelectedIDs & ") AND Active = 1 ORDER BY SortOrder ")
                    For Each dr As DataRow In dtUser.Rows
                        dr.BeginEdit()
                        dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                        dr.EndEdit()
                    Next
                    dtUser.AcceptChanges()
                    Me.lbUsers.DataSource = dtUser
                    Me.lbUsers.ValueMember = dtUser.Columns(0).ColumnName.ToString
                    Me.lbUsers.DisplayMember = dtUser.Columns(1).ColumnName.ToString

                End If

                Me.BtnSave_Click(Me, Nothing)

            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Sales Inquiry")
            If EmailTemplate.Length > 0 Then
                'GetAutoEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                FillDataSet()
                For Each dtEmail1 As DataTable In dsEmail.Tables
                    FormatStringBuilder(dtEmail1)
                    For Each _Email As String In UsersEmail
                        VendorEmails = "" & _Email & ""
                        If Not VendorEmails = "" Then
                            CreateOutLookMail(True, dtEmail1.TableName)
                            SaveEmailLog(dtEmail1.TableName, VendorEmails, "frmSalesInquiry", Activity)
                            VendorEmails = String.Empty
                        End If
                    Next
                Next
                'SaveCCBCC(CC, BCC)
                'CC = ""
                'BCC = ""
            Else
                ShowErrorMessage("No email template is found for Sales Inquiry.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAutoEmailData()
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Try
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row.Cells(col).Value.ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataSet()
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Dim SalesInquiryNo As String = String.Empty
        Dim PreviousSalesInquiryNo As String = String.Empty
        Dim dtEmailNew As DataTable
        Try
            dsEmail = New DataSet()
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
                SalesInquiryNo = Row.Cells("SalesInquiryNo").Value.ToString
                'If PreviousSalesInquiryNo = String.Empty Then
                '    PreviousSalesInquiryNo = SalesInquiryNo
                '    dtEmailNew = New DataTable(SalesInquiryNo)
                '    dtEmailNew = dtEmail.Clone()
                '    If dsEmail.Tables.Contains(SalesInquiryNo) = False Then
                '        dsEmail.Tables.Add(dtEmailNew)
                '    End If
                'End If
                If SalesInquiryNo = PreviousSalesInquiryNo Then
                    'dsEmail.Tables(SalesInquiryNo).Rows.Add(Dr)
                Else
                    PreviousSalesInquiryNo = SalesInquiryNo
                    dtEmailNew = New DataTable(SalesInquiryNo)
                    'dtEmailNew = dtEmail.Clone()
                    'dtEmailNew.
                    For Each Colum As DataColumn In dtEmail.Columns
                        dtEmailNew.Columns.Add(Colum.ColumnName)
                    Next
                    If dsEmail.Tables.Contains(SalesInquiryNo) = False Then
                        dsEmail.Tables.Add(dtEmailNew)
                    End If
                    'dsEmail.Tables.Add(dtEmailNew)
                    'dsEmail.Tables(SalesInquiryNo).Rows.Add(Dr)
                End If
                Dr = dtEmailNew.NewRow
                For Each col As String In AllFields
                    If Row.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row.Cells(col).Value.ToString
                    End If
                Next
                dsEmail.Tables(SalesInquiryNo).Rows.Add(Dr)
                'dtEmail.Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

   
    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseClick
        'Try
        '    If IsFormOpened = True Then
        '        GroupIds = String.Empty
        '        For Each groupId As Object In Me.ListBox1.SelectedItems
        '            If TypeOf MainMenuIds Is System.String Then
        '                MainMenuIds = MainMenuIds & IIf(MainMenuIds.Length > 0, ",", "") & "'" & listMenu & "'"
        '            End If
        '        Next
        '        GetAllRecords()
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub dtpInquiryFromDate_Leave(sender As Object, e As EventArgs) Handles dtpInquiryFromDate.Leave
        Try
            ''TASK TFS4673
            If Me.dtpInquiryFromDate.Checked = True Or Me.dtpInquiryToDate.Checked = True Then
                If Me.cmbReference.Value > 0 Then
                    FillCombos("InquiryNumberAgainstCustomer")
                Else
                    FillCombos("InquiryNumber")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpInquiryToDate_Leave(sender As Object, e As EventArgs) Handles dtpInquiryToDate.Leave
        Try
            ''TASK TFS4673
            If Me.dtpInquiryFromDate.Checked = True Or Me.dtpInquiryToDate.Checked = True Then
                If Me.cmbReference.Value > 0 Then
                    FillCombos("InquiryNumberAgainstCustomer")
                Else
                    FillCombos("InquiryNumber")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS4673
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks> Controls should be refreshed and reset to thier previous state after refreshing</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim CustomerId As Integer = 0
        Dim InquiryId As Integer = 0
        Try
            InquiryId = Me.cmbInquiryNumber.Value
            CustomerId = Me.cmbReference.Value
            FillCombos("Customer")
            Me.cmbReference.Value = CustomerId
            If CustomerId > 0 Then
                FillCombos("InquiryNumberAgainstCustomer")
            Else
                FillCombos("InquiryNumber")
            End If
            Me.cmbInquiryNumber.Value = InquiryId
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
