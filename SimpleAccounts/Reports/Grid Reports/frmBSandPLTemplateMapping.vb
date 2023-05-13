Imports System
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal
Public Class frmBSandPLTemplateMapping
    Implements IGeneral
    Dim ReportTemplateDetailId As Integer
    Dim Template As BSandPLTemplateMappingBE
    Dim objDAL As BSandPLTemplateMappingDAL
    Dim isFormLoaded As Boolean = False
    Private Sub frmBSandPLTemplateMapping_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If frmBSandPLReports.Type = "BS" Then
                rbtBalanceSheet.Checked = True
                FillCombos("BalanceSheet")
            Else
                rbtProfitandLoss.Checked = True
                FillCombos("ProfitLoss")
            End If
            Dim str As String
            Dim dt As DataTable
            str = "Select * from ReportTemplateDetail where ReportTemplateId = " & frmBSandPLReports.TemplateId & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                GetAllRecords("SubSubAccount")
                GetAllRecords("DetailAccount")
                GetAllRecords("Category")
                'Else
                '    GetAllRecords()
            End If
            Me.txtTitle.Text = frmBSandPLReports.Title
            FillCombos("SubSubAccount")
            FillCombos("DetailAccount")
            FillCombos("Category")
            isFormLoaded = True
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

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "SubSubAccount" Then
                str = "SELECT DISTINCT(main_sub_sub_id) ,sub_sub_code as [Sub Sub Code], sub_sub_title as [Sub Sub Title] FROM vwCOADetail"
                FillUltraDropDown(cmbSubSubAccount, str)
                Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns("main_sub_sub_id").Hidden = True
            ElseIf Condition = "DetailAccount" Then
                str = "SELECT coa_detail_id,detail_code as [Detail Code], detail_title as [Detail Title] FROM vwCOADetail"
                FillUltraDropDown(cmbDetailAccount, str)
                Me.cmbDetailAccount.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "BalanceSheet" Then
                FillListBox(Me.lstNotes.ListItem, "select * from tblDefGLNotes where note_type='BS' order by 1")
            ElseIf Condition = "ProfitLoss" Then
                FillListBox(Me.lstNotes.ListItem, "SELECT * FROM tblDefGLNotes WHERE note_type='PL' order by 1")
            ElseIf Condition = "Category" Then
                FillDropDown(Me.cmbCategory, "SELECT * FROM ReportTemplateNotesCategory " & IIf(Me.rbtBalanceSheet.Checked = True, " WHERE BSNotesId > 0 ", " WHERE PLNotesId > 0 ") & " ORDER BY 1")
            ElseIf Condition = "DetailCategory" Then
                FillDropDown(Me.cmbDetailCategory, "SELECT * FROM ReportTemplateNotesCategory " & IIf(Me.rbtBalanceSheet.Checked = True, " WHERE BSNotesId > 0 AND BSNotesId IN (" & IIf(rbtBalanceSheet.Checked = True, lstNotes.SelectedIDs, 0) & ") ", " WHERE PLNotesId > 0 AND PLNotesId IN (" & IIf(rbtProfitandLoss.Checked = True, lstNotes.SelectedIDs, 0) & ") ") & " ORDER BY 1")
            ElseIf Condition = "SubSubCategory" Then
                FillDropDown(Me.cmbSubSubCategory, "SELECT * FROM ReportTemplateNotesCategory " & IIf(Me.rbtBalanceSheet.Checked = True, " WHERE BSNotesId > 0 AND BSNotesId IN (" & IIf(rbtBalanceSheet.Checked = True, lstNotes.SelectedIDs, 0) & ") ", " WHERE PLNotesId > 0 AND PLNotesId IN (" & IIf(rbtProfitandLoss.Checked = True, lstNotes.SelectedIDs, 0) & ") ") & " ORDER BY 1")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Template = New BSandPLTemplateMappingBE
            Template.ReportTemplateDetailId = ReportTemplateDetailId
            Template.ReportTemplateId = frmBSandPLReports.TemplateId
            If UltraTabControl1.SelectedTab.Index = 1 Then
                Template.AccountLevel = "Sub Sub"
                Template.AccountId = Me.cmbSubSubAccount.Value
                Template.CategoryId = Me.cmbSubSubCategory.SelectedValue
            ElseIf UltraTabControl1.SelectedTab.Index = 2 Then
                Template.AccountLevel = "Detail"
                Template.AccountId2 = Me.cmbDetailAccount.Value
                Template.CategoryId = Me.cmbDetailCategory.SelectedValue
            End If
            If rbtBalanceSheet.Checked = True Then
                Template.BSNotesId = Me.lstNotes.SelectedIDs
                Template.PLNotesId = 0
            ElseIf rbtProfitandLoss.Checked = True Then
                Template.PLNotesId = Me.lstNotes.SelectedIDs
                Template.BSNotesId = 0
            End If
            Template.CategoryTitle = Me.cmbCategory.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "SubSubAccount" Then
                Dim str As String = "SELECT  DISTINCT(ReportTemplateDetail.AccountId), ReportTemplateDetail.BSNotesId, ReportTemplateDetail.ReportTemplateDetailId, ReportTemplateDetail.PLNotesId,  vwCOADetail.sub_sub_title as SubSubTitle,ISNULL(ReportTemplateDetail.CategoryId,0) AS CategoryId, ISNULL(ReportTemplateNotesCategory.CategoryTitle,'') AS CategoryTitle FROM ReportTemplateDetail LEFT OUTER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId LEFT OUTER JOIN vwCOADetail ON ReportTemplateDetail.AccountId = vwCOADetail.main_sub_sub_id Where ReportTemplateDetail.BSNotesId IN (" & IIf(rbtBalanceSheet.Checked = True, lstNotes.SelectedIDs, 0) & ") AND ReportTemplateDetail.PLNotesId IN (" & IIf(rbtProfitandLoss.Checked = True, lstNotes.SelectedIDs, 0) & ") AND ReportTemplateDetail.ReportTemplateId = " & frmBSandPLReports.TemplateId & "AND ReportTemplateDetail.AccountLevel = 'Sub Sub'"
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdSubSubAccount.DataSource = dt
                Me.grdSubSubAccount.RetrieveStructure()
                If Me.grdSubSubAccount.RootTable.Columns.Contains("Remove") = False Then
                    Me.grdSubSubAccount.RootTable.Columns.Add("Remove")
                    Me.grdSubSubAccount.RootTable.Columns("Remove").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdSubSubAccount.RootTable.Columns("Remove").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdSubSubAccount.RootTable.Columns("Remove").ButtonText = "Remove"
                    Me.grdSubSubAccount.RootTable.Columns("Remove").Key = "Remove"
                    Me.grdSubSubAccount.RootTable.Columns("Remove").Caption = "Action"
                End If
                Me.grdSubSubAccount.RootTable.Columns("AccountId").Visible = False
                Me.grdSubSubAccount.RootTable.Columns("PLNotesId").Visible = False
                Me.grdSubSubAccount.RootTable.Columns("BSNotesId").Visible = False
                Me.grdSubSubAccount.RootTable.Columns("ReportTemplateDetailId").Visible = False
                Me.grdSubSubAccount.RootTable.Columns("CategoryId").Visible = False
            ElseIf Condition = "DetailAccount" Then
                Dim str As String = "SELECT DISTINCT(ReportTemplateDetail.AccountId), ReportTemplateDetail.BSNotesId, ReportTemplateDetail.PLNotesId, ReportTemplateDetail.ReportTemplateDetailId, vwCOADetail.detail_title as DetailTitle,ISNULL(ReportTemplateDetail.CategoryId,0) AS CategoryId, ISNULL(ReportTemplateNotesCategory.CategoryTitle,'') AS CategoryTitle FROM ReportTemplateDetail LEFT OUTER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId LEFT OUTER JOIN vwCOADetail ON ReportTemplateDetail.AccountId = vwCOADetail.coa_detail_id where ReportTemplateDetail.BSNotesId IN (" & IIf(rbtBalanceSheet.Checked = True, lstNotes.SelectedIDs, 0) & ") AND ReportTemplateDetail.PLNotesId IN (" & IIf(rbtProfitandLoss.Checked = True, lstNotes.SelectedIDs, 0) & ") AND ReportTemplateDetail.ReportTemplateId = " & frmBSandPLReports.TemplateId & "AND ReportTemplateDetail.AccountLevel = 'Detail'"
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdDetailAccount.DataSource = dt
                Me.grdDetailAccount.RetrieveStructure()
                If Me.grdDetailAccount.RootTable.Columns.Contains("Remove") = False Then
                    Me.grdDetailAccount.RootTable.Columns.Add("Remove")
                    Me.grdDetailAccount.RootTable.Columns("Remove").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdDetailAccount.RootTable.Columns("Remove").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdDetailAccount.RootTable.Columns("Remove").ButtonText = "Remove"
                    Me.grdDetailAccount.RootTable.Columns("Remove").Key = "Remove"
                    Me.grdDetailAccount.RootTable.Columns("Remove").Caption = "Action"
                End If
                Me.grdDetailAccount.RootTable.Columns("AccountId").Visible = False
                Me.grdDetailAccount.RootTable.Columns("PLNotesId").Visible = False
                Me.grdDetailAccount.RootTable.Columns("BSNotesId").Visible = False
                Me.grdDetailAccount.RootTable.Columns("ReportTemplateDetailId").Visible = False
                Me.grdDetailAccount.RootTable.Columns("CategoryId").Visible = False
            ElseIf Condition = "Category" Then
                Dim str As String = "SELECT * FROM ReportTemplateNotesCategory WHERE BSNotesId IN (" & IIf(rbtBalanceSheet.Checked = True, lstNotes.SelectedIDs, 0) & ") AND PLNotesId IN (" & IIf(rbtProfitandLoss.Checked = True, lstNotes.SelectedIDs, 0) & ")"
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdCategory.DataSource = dt
                Me.grdCategory.RetrieveStructure()
                If Me.grdCategory.RootTable.Columns.Contains("Remove") = False Then
                    Me.grdCategory.RootTable.Columns.Add("Remove")
                    Me.grdCategory.RootTable.Columns("Remove").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdCategory.RootTable.Columns("Remove").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdCategory.RootTable.Columns("Remove").ButtonText = "Remove"
                    Me.grdCategory.RootTable.Columns("Remove").Key = "Remove"
                    Me.grdCategory.RootTable.Columns("Remove").Caption = "Action"
                End If
                Me.grdCategory.RootTable.Columns("CategoryId").Visible = False
                Me.grdCategory.RootTable.Columns("PLNotesId").Visible = False
                Me.grdCategory.RootTable.Columns("BSNotesId").Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Condition = "Category" Then
                If Me.cmbCategory.Text = "" Or Me.cmbCategory.Text = ".... Select Any Value ...." Then
                    ShowErrorMessage("Please Enter Valid Category Title")
                    Return False
                End If
            Else
                If cmbSubSubAccount.ActiveRow.Cells(0).Value <= 0 Then
                    ShowErrorMessage("Please Select an Account")
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
        Try
            GetAllRecords("SubSubAccount")
            GetAllRecords("DetailAccount")
            GetAllRecords("Category")
            FillCombos("Category")
            Me.cmbCategory.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If Condition = "SubSub" Then
                If New BSandPLTemplateMappingDAL().SaveSubSUb(Template) = True Then
                    Return True
                Else
                    Return False
                End If
                'SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtTitle.Text, True)
            ElseIf Condition = "Detail" Then
                If New BSandPLTemplateMappingDAL().SaveDetail(Template) = True Then
                    Return True
                Else
                    Return False
                End If
                'SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtTitle.Text, True)
            ElseIf Condition = "Category" Then
                If New BSandPLTemplateMappingDAL().SaveCategory(Template) = True Then
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

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If Condition = "SubSub" Then
                If New BSandPLTemplateMappingDAL().SaveSubSub(Template) = True Then
                    Return True
                Else
                    Return False
                End If
            ElseIf Condition = "Detail" Then
                If New BSandPLTemplateMappingDAL().SaveDetail(Template) = True Then
                    Return True
                Else
                    Return False
                End If
            ElseIf Condition = "Category" Then
                If New BSandPLTemplateMappingDAL().SaveCategory(Template) = True Then
                    Return True
                Else
                    Return False
                End If
            End If
            
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSubSubAdd_Click(sender As Object, e As EventArgs) Handles btnSubSubAdd.Click
        Try
            If IsValidate() = True Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdSubSubAccount.GetDataRows
                    If Val(row.Cells("AccountId").Value) = Me.cmbSubSubAccount.Value Then
                        msg_Error("Selected Account Exists Already")
                        Exit Sub
                    End If
                Next
                If Save("SubSub") = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDetailAdd_Click(sender As Object, e As EventArgs) Handles btnDetailAdd.Click
        Try
            If IsValidate() = True Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdDetailAccount.GetDataRows
                    If Val(row.Cells("AccountId").Value) = Me.cmbDetailAccount.Value Then
                        msg_Error("Selected Account Exists Already")
                        Exit Sub
                    End If
                Next
                If Save("Detail") = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailAccount_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetailAccount.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Remove" Then
                objDAL = New BSandPLTemplateMappingDAL
                objDAL.DeleteDetail(Val(Me.grdDetailAccount.GetRow.Cells("ReportTemplateDetailId").Value.ToString))
                Me.grdDetailAccount.GetRow.Delete()
                grdDetailAccount.UpdateData()
            End If
            GetAllRecords("DetailAccount")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSubSubAccount_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSubSubAccount.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Remove" Then
                objDAL = New BSandPLTemplateMappingDAL
                objDAL.DeleteDetail(Val(Me.grdSubSubAccount.GetRow.Cells("ReportTemplateDetailId").Value.ToString))
                'Me.grdDetailAccount.GetRow.Delete()
                grdDetailAccount.UpdateData()
            End If
            GetAllRecords("SubSubAccount")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtProfitandLoss_CheckedChanged(sender As Object, e As EventArgs) Handles rbtProfitandLoss.CheckedChanged
        Try
            If rbtBalanceSheet.Checked = True Then
                FillCombos("BalanceSheet")
            ElseIf rbtProfitandLoss.Checked = True Then
                FillCombos("ProfitLoss")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstNotes_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstNotes.SelectedIndexChaned
        Try
            GetAllRecords("SubSubAccount")
            GetAllRecords("DetailAccount")
            GetAllRecords("Category")
            FillCombos("Category")
            FillCombos("DetailCategory")
            FillCombos("SubSubCategory")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddCategory_Click(sender As Object, e As EventArgs) Handles btnAddCategory.Click
        Try
            If IsValidate(, "Category") = True Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdCategory.GetDataRows
                    If row.Cells("CategoryTitle").Value.ToString = Me.cmbCategory.Text Then
                        msg_Error("Category Title '" & Me.cmbCategory.Text & "' Exists Already")
                        Exit Sub
                    End If
                Next
                If Save("Category") = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdCategory_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCategory.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Dim str As String
            Dim dt As DataTable
            str = "select * from ReportTemplateDetail where CategoryId = " & Val(Me.grdCategory.GetRow.Cells("CategoryId").Value.ToString) & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                ShowErrorMessage(str_ErrorDependentRecordFound)
                Exit Sub
            End If
            If e.Column.Key = "Remove" Then
                objDAL = New BSandPLTemplateMappingDAL
                objDAL.DeleteCategory(Val(Me.grdCategory.GetRow.Cells("CategoryId").Value.ToString))
                grdCategory.UpdateData()
            End If
            GetAllRecords("Category")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            FillCombos("SubSubCategory")
            FillCombos("DetailCategory")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged
        Try
            If isFormLoaded = False Then Exit Sub
            'Dim Str As String = "SELECT OP 100 PERCENT coa_detail_id,detail_code ,detail_title , account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE (coa_detail_id > 0) order by detail_title"
            'FillUltraDropDown(cmbAccount, Str)
            Me.cmbSubSubAccount.DisplayMember = Me.cmbSubSubAccount.Rows(0).Cells(1).Column.Key.ToString
            Me.cmbSubSubAccount.Rows(0).Activate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        Try
            If isFormLoaded = False Then Exit Sub
            'Dim Str As String = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE (coa_detail_id > 0) order by detail_title"
            'FillUltraDropDown(cmbAccount, Str)
            Me.cmbSubSubAccount.DisplayMember = Me.cmbSubSubAccount.Rows(0).Cells(2).Column.Key.ToString
            Me.cmbSubSubAccount.Rows(0).Activate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rboCode1_CheckedChanged(sender As Object, e As EventArgs) Handles rboCode1.CheckedChanged
        Try
            If isFormLoaded = False Then Exit Sub
            'Dim Str As String = "SELECT OP 100 PERCENT coa_detail_id,detail_code ,detail_title , account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE (coa_detail_id > 0) order by detail_title"
            'FillUltraDropDown(cmbAccount, Str)
            Me.cmbDetailAccount.DisplayMember = Me.cmbDetailAccount.Rows(0).Cells(1).Column.Key.ToString
            Me.cmbDetailAccount.Rows(0).Activate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rboName1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rboName1.CheckedChanged
        Try
            If isFormLoaded = False Then Exit Sub
            'Dim Str As String = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE (coa_detail_id > 0) order by detail_title"
            'FillUltraDropDown(cmbAccount, Str)
            Me.cmbDetailAccount.DisplayMember = Me.cmbDetailAccount.Rows(0).Cells(2).Column.Key.ToString
            Me.cmbDetailAccount.Rows(0).Activate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class