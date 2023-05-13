Public Class frmRptReturnStoreIssuence
    Implements IGeneral

    Dim _SearchDt As New DataTable

    Private Sub frmRptReturnStoreIssuence_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        FillCombos("Location")
        Me.lstLocation.DeSelect()
        FillCombos("Department")
        Me.lstInventoryDepartment.DeSelect()
        FillCombos("Type")
        Me.lstInventoryType.DeSelect()
        FillCombos("Item")
        _SearchDt = CType(Me.lstItems.ListItem.DataSource, DataTable)
        _SearchDt.AcceptChanges()
        Me.lstItems.DeSelect()
        FillCombos("Category")
        Me.lstInventoryCategory.DeSelect()
        FillCombos("HeadCostCenter")
        Me.lstHeadCostCenter.DeSelect()
        FillCombos("CostCenter")
        Me.lstCostCenter.DeSelect()
        CtrlGrdBar1_Load(Nothing, Nothing)
        ApplySecurityRights()
    End Sub

    Private Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnShow.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False

            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            Me.btnShow.Enabled = False
            Me.btnPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
                GetCrystalReportRights()
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String
        If Condition = "Location" Then

            Str = "Select location_id, location_name from tbldeflocation"

            FillListBox(Me.lstLocation.ListItem, Str)


        End If

        If Condition = "CostCenter" Then

            Str = "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 Order By Name"

            FillListBox(Me.lstCostCenter.ListItem, Str)


        End If

        If Condition = "HeadCostCenter" Then
            Str = "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1"
            FillListBox(Me.lstHeadCostCenter.ListItem, Str)

        End If

        If Condition = "Category" Then

            Str = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode" & _
                    " FROM ArticleCompanyDefTable" & _
                    " WHERE Active = 1"

            FillListBox(Me.lstInventoryCategory.ListItem, Str)

        End If

        If Condition = "Department" Then
            Str = "SELECT  ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID, ArticleGroupDefTable.GroupCode " & _
                       " FROM ArticleGroupDefTable LEFT OUTER JOIN " & _
                         "    tblCOAMainSubSubDetail ON ArticleGroupDefTable.SubSubID = tblCOAMainSubSubDetail.coa_detail_id" & _
                       " WHERE     (ArticleGroupDefTable.Active = 1) AND (tblCOAMainSubSubDetail.main_sub_sub_id In(Select main_sub_sub_id from tblCOAMainSubSub WHERE Account_Type='Inventory'))" & _
                       " ORDER BY ArticleGroupDefTable.SortOrder"

            FillListBox(Me.lstInventoryDepartment.ListItem, Str)

        End If

        If Condition = "Type" Then

            Str = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
            FillListBox(Me.lstInventoryType.ListItem, Str)

        End If

        If Condition = "Item" Then

            Str = "SELECT ArticleDefView.ArticleId ,ArticleDefView.ArticleCode+ ' ~ ' + ArticleDefView.ArticleDescription+ ' ~ ' +  ArticleDefView.ArticleColorName+ ' ~ ' + ArticleDefView.ArticleSizeName AS ArticleDescription FROM ArticleDefView"

            FillListBox(Me.lstItems.ListItem, Str)

        End If

    End Sub
    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "ArticleDescription Like '%" & Me.txtSearch.Text & "%'"
            Me.lstItems.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

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


    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "') Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "') ")
            Else

                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 ")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@LocationIds", Me.lstLocation.SelectedIDs)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            AddRptParam("@CategoryIds", Me.lstInventoryCategory.SelectedIDs)
            AddRptParam("@DepartmentIds", Me.lstInventoryDepartment.SelectedIDs)
            AddRptParam("@TypeIds", Me.lstInventoryType.SelectedIDs)
            AddRptParam("@ItemIds", Me.lstItems.SelectedIDs)
            ShowReport("rptReturnStoreIssuenceReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim strData As String = ""
        Dim dtData As New DataTable
        strData = "EXEC dbo.SP_ReturnStoreIssuenceReport '" & Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00") & "','" & Me.dtpTo.Value.ToString("yyyy-MM-dd 00:00:00") & "', '" & Me.lstLocation.SelectedIDs.ToString & "','" & Me.lstCostCenter.SelectedIDs & "','" & lstInventoryCategory.SelectedIDs & "','" & lstInventoryDepartment.SelectedIDs & "','" & lstInventoryType.SelectedIDs & "','" & lstItems.SelectedIDs & "' "
        dtData = GetDataTable(strData)
        dtData.AcceptChanges()
        Me.grdSaved.DataSource = dtData
        Me.grdSaved.RetrieveStructure()
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Return Store Issuence Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.lstLocation.DeSelect()
        Me.lstInventoryDepartment.DeSelect()
        Me.lstInventoryType.DeSelect()
        Me.lstItems.DeSelect()
        Me.lstInventoryCategory.DeSelect()
        Me.lstHeadCostCenter.DeSelect()
        Me.lstCostCenter.DeSelect()
    End Sub
End Class