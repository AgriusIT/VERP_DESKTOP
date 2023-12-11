''03-Feb-2014    TASK:2413      Imran ali Production Level Against Plan No
'15-May-2014 Task: 2628 Junaid In production level report search bar added
Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Security
Public Class frmProductionLevel
    Implements IGeneral
    Dim PLevelID As Integer = 0I 'Declar Variable of PLevelId
    Dim ProductionLevel As ProdcutionLevelMasterBE 'Declar Variable of object
    Dim IsOpenForm As Boolean = False
    Dim StoreIssuanceDependonProductionPlan As Boolean = False
    Enum enmPlan
        ArticleId
        ArticleCode
        ArticleDescription
        Color
        Size
        LocationId
        ArticleSize
        PackQty
        PlanQty
        RecvQty
        Qty
    End Enum

    Private Sub frmProductionLevel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnPrint_Click(Nothing, Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmProductionLevel_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillCombos("Project")
            FillCombos("Plan")
            FillCombos("Steps")
            FillCombos("Location")

            'If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
            '    StoreIssuanceDependonProductionPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
            '    Me.cmbProductionPlan.Enabled = False
            'Else
            Me.cmbProductionPlan.Enabled = True
            'End If
            IsOpenForm = True
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmProductionInspection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Master" Then
                grdSaved.RootTable.Columns("PLevelId").Visible = False
                grdSaved.RootTable.Columns("PlanId").Visible = False
                Me.grdSaved.RootTable.Columns("ProjectId").Visible = False
                Me.grdSaved.RootTable.Columns("PStepsId").Visible = False
                Me.grdSaved.RootTable.Columns("Total_Qty").Visible = False
                Me.grdSaved.RootTable.Columns("Total_Amount").Visible = False
                'grdSaved.RootTable.Columns("UserName").Visible = False
                grdSaved.RootTable.Columns("Doc_Date").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                Me.grd.RootTable.Columns("ArticleId").Visible = False
                Me.grd.RootTable.Columns(enmPlan.PackQty).Visible = False
                For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                    If c.Index <> enmPlan.RecvQty AndAlso c.Index <> enmPlan.Qty AndAlso c.Index <> enmPlan.PackQty AndAlso c.Index <> enmPlan.ArticleSize Then
                        c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
                Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
                Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grd.RootTable.Columns("RecvQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("RecvQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("RecvQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


                Me.grd.RootTable.Columns("PlanQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("PlanQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("PlanQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grd.RootTable.Columns("ArticleSize").Caption = "Unit"
                Me.grd.AutoSizeColumns()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            ProductionLevel = New ProdcutionLevelMasterBE
            ProductionLevel.PLevelId = Me.grdSaved.GetRow.Cells("PLevelId").Value
            If New ProducionLevelDAL().Delete(ProductionLevel) = True Then
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
            Dim strSQL As String = String.Empty
            If Condition = "Project" Then
                FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1")
            ElseIf Condition = "Plan" Then
                FillDropDown(Me.cmbProductionPlan, "Select PlanId, PlanNo From PlanMasterTable ORDER BY PlanNo DESC")
            ElseIf Condition = "Steps" Then
                FillDropDown(Me.cmbProductionSteps, "Select ProdStep_Id, prod_step From tblproSteps")
            ElseIf Condition = "Location" Then
                strSQL = String.Empty
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strSQL = "Select Location_Id, Location_Code From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
                'Else
                '    strSQL = "Select Location_Id, Location_Code From tblDefLocation"
                'End If
                'FillDropDown(Me.cmbLocation, strSQL, False)

                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                         & " Else " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"


                FillDropDown(Me.cmbLocation, strSQL, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            ProductionLevel = New ProdcutionLevelMasterBE 'Create Object
            ProductionLevel.PLevelId = PLevelID           ' Set Plan Id Value on Property  
            ProductionLevel.Doc_No = Me.txtDocumentNo.Text
            ProductionLevel.Doc_Date = Me.dtpDocumentDate.Value
            ProductionLevel.ProjectId = Me.cmbProject.SelectedValue
            ProductionLevel.PlanId = Me.cmbProductionPlan.SelectedValue
            ProductionLevel.StepsId = Me.cmbProductionSteps.SelectedValue
            ProductionLevel.Remarks = Me.txtRemarks.Text.Replace("'", "''")
            ProductionLevel.TotalQty = Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)
            ProductionLevel.TotalAmount = 0
            ProductionLevel.ProductionLevelDetail = New List(Of ProductionLevelDetailBE)
            Dim ProductionLevelDetail As ProductionLevelDetailBE 'Create Object
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows ' Loop on Grd Detail
                ProductionLevelDetail = New ProductionLevelDetailBE 'Create Object
                ProductionLevelDetail.PLevelId = PLevelID ' Set Plan Id Value on Property 
                ProductionLevelDetail.LocationId = r.Cells("LocationId").Value
                ProductionLevelDetail.ArticleDefId = r.Cells("ArticleId").Value
                ProductionLevelDetail.Articlesize = r.Cells("Articlesize").Value.ToString
                ProductionLevelDetail.Price = 0
                ProductionLevelDetail.Sz1 = Val(r.Cells("RecvQty").Value.ToString)
                ProductionLevelDetail.Sz2 = Val(r.Cells("PackQty").Value.ToString)
                ProductionLevelDetail.Qty = r.Cells("Qty").Value
                ProductionLevel.ProductionLevelDetail.Add(ProductionLevelDetail)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then
                Dim dtMaster As New DataTable
                dtMaster = New ProducionLevelDAL().GetAllRecords
                Me.grdSaved.DataSource = dtMaster
                grdSaved.RetrieveStructure()
                ApplyGridSettings("Master")
            ElseIf Condition = "Detail" Then
                Dim dtDetail As New DataTable
                dtDetail = New ProducionLevelDAL().GetDetailRecord(PLevelID)
                Me.grd.DataSource = dtDetail
                grd.RetrieveStructure()
                ' Task: 2628 Search Filter in Grid is applied here
                'Me.grd.RootTable.Columns("ArticleId").EditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns("ArticleCode").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns("ArticleDescription").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns("color").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                Me.grd.RootTable.Columns("size").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns("PlanQty").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns("RecvQty").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns("Qty").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
                'End task: 2628

                Dim dt As New DataTable


                Dim strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                    & " Else " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()

                Me.grd.RootTable.Columns("LocationId").Caption = "Location"
                Me.grd.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grd.RootTable.Columns("LocationId").HasValueList = True
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")


                Dim str() As String = {"Loose", "Pack"}
                Me.grd.RootTable.Columns("Articlesize").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grd.RootTable.Columns("ArticleSize").HasValueList = True
                Me.grd.RootTable.Columns("ArticleSize").ValueList.PopulateValueList(str, "", "")

                ApplyGridSettings("Detail")
                If Me.btnSave.Text = "&Save" Then Me.cmbLocation_SelectedIndexChanged(Nothing, Nothing)
                CtrlGrdBar2_Load(Nothing, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            'Comment against task:2413
            'If StoreIssuanceDependonProductionPlan = True Then
            '    If Me.cmbProductionPlan.SelectedIndex = 0 Then
            '        ShowErrorMessage("Please select plan no")
            '        Me.cmbProductionPlan.Focus()
            '        Return False
            '    End If
            'End If
            'End Task:2413

            If Me.cmbProductionSteps.SelectedIndex = 0 Then
                ShowErrorMessage("Please select step")
                Me.cmbProductionSteps.Focus()
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
            PLevelID = 0
            Me.btnSave.Text = "&Save"
            Me.txtDocumentNo.Text = GetDocumentNo()
            Me.dtpDocumentDate.Value = Now
            ''If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
            ''If Not Me.cmbProductionPlan.SelectedIndex = -1 Then Me.cmbProductionPlan.SelectedIndex = 0
            If Not Me.cmbProductionSteps.SelectedIndex = -1 Then Me.cmbProductionSteps.SelectedIndex = 0
            Me.txtRemarks.Text = String.Empty
            If Not Me.cmbLocation.SelectedIndex = -1 Then Me.cmbLocation.SelectedIndex = 0
            Me.dtpDocumentDate.Enabled = True
            'Comment against task:2413
            'If StoreIssuanceDependonProductionPlan = True Then
            '    Me.cmbProductionPlan.Enabled = True
            'Else
            '    Me.cmbProductionPlan.Enabled = False
            'End If
            'End Task:2413
            Me.GetAllRecords("Master")
            Me.GetAllRecords("Detail")
            Me.cmbUnit.SelectedIndex = 0
            Me.dtpDocumentDate.Focus()
            ApplySecurity(EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If New ProducionLevelDAL().Save(ProductionLevel) = True Then
                Return True
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

            If New ProducionLevelDAL().Update(ProductionLevel) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub cmbLocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        Try
            If Me.cmbLocation.SelectedIndex = -1 Then Exit Sub
            If Me.grd.RowCount = 0 Then Exit Sub
            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                r.BeginEdit()
                r.Cells("LocationId").Value = Me.cmbLocation.SelectedValue
                r.EndEdit()
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()


                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()


                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informUpdate)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.grdSaved.CurrentRow.Delete()
            'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            Me.PLevelID = Me.grdSaved.GetRow.Cells("PLevelId").Value
            Me.txtDocumentNo.Text = Me.grdSaved.GetRow.Cells("Doc_No").Value
            Me.dtpDocumentDate.Value = Me.grdSaved.GetRow.Cells("Doc_Date").Value
            Me.cmbProject.SelectedValue = Me.grdSaved.GetRow.Cells("ProjectId").Value
            Me.cmbProductionPlan.SelectedValue = Me.grdSaved.GetRow.Cells("PlanId").Value
            Me.cmbProductionSteps.SelectedValue = Me.grdSaved.GetRow.Cells("PStepsId").Value
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value
            Me.GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

            'Comment against task:2413
            'If StoreIssuanceDependonProductionPlan = False Then
            '    Me.dtpDocumentDate_LostFocus(Nothing, Nothing)
            '    Me.dtpDocumentDate.Enabled = False
            'Else
            Me.dtpDocumentDate.Enabled = True
            'End If
            'End Task:2413

            'Comment against task:2413
            'If StoreIssuanceDependonProductionPlan = True Then
            '    Me.cmbProductionPlan.Enabled = True
            'Else
            'End Task:2413
            Me.cmbProductionPlan.Enabled = False
            'End If

            ApplySecurity(EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdSaved_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                ' Return GetSerialNo(IIf(CompanyBasePrefix = False & IIf(Me.GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length = 0, "SI" & Me.cmbCompany.SelectedValue & "-" & Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year & "-", "SalesMasterTable", "SalesNo")
                Return GetSerialNo("PL-" & Microsoft.VisualBasic.Right(Me.dtpDocumentDate.Value.Year, 2) & "-", "ProductionLevelMasterTable", "Doc_No")
            Else
                Return GetNextDocNo("PL", 6, "ProductionLevelMasterTable", "Doc_No")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnProductionSteps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductionSteps.Click
        Try

            Dim frm As New frmproductionSteps
            ApplyStyleSheet(frm)
            If frm.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Dim id As Integer = 0
                id = Me.cmbProductionSteps.SelectedIndex
                FillCombos("Steps")
                Me.cmbProductionSteps.SelectedIndex = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                r.BeginEdit()
                r.Cells("ArticleSize").Value = Me.cmbUnit.Text
                r.EndEdit()
            Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProductionPlan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProductionPlan.SelectedIndexChanged
        Try
            If Me.cmbProductionPlan.SelectedIndex = -1 Then Exit Sub
            'If Not StoreIssuanceDependonProductionPlan = True Then Exit Sub
            Dim dt As New DataTable
            dt = GetDataTable("Select ArticleDefId, SUM(IsNull(Qty,0)) as Qty From PlanDetailTable WHERE PlanId=" & Me.cmbProductionPlan.SelectedValue & " Group By ArticleDefId ")
            Dim dr() As DataRow
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                        dr = dt.Select("ArticleDefId=" & r.Cells("ArticleId").Value & "")
                        For Each drfound As DataRow In dr
                            r.BeginEdit()
                            r.Cells("PlanQty").Value = drfound(1)
                            r.EndEdit()
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub dtpDocumentDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDocumentDate.LostFocus
        Try
            'If StoreIssuanceDependonProductionPlan = False Then
            If Me.cmbProductionPlan.SelectedIndex = -1 Then Exit Sub
            Dim dt As New DataTable
            dt = GetDataTable("Select ArticleDefId, SUM(IsNull(Qty,0)) as Qty From SalesOrderDetailTable WHERE SalesOrderId IN(Select SalesOrderId From SalesOrderMasterTable WHERE(Convert(varchar, SalesOrderDate,102) = Convert(DateTime, '" & Me.dtpDocumentDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))) " & IIf(Me.cmbProject.SelectedIndex > 0, " AND SalesOrderId IN(Select SalesOrderId From SalesOrderMasterTable WHERE LocationId=" & Me.cmbProject.SelectedValue & ")", "") & "  Group By ArticleDefId ")
            Dim dr() As DataRow
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                        dr = dt.Select("ArticleDefId=" & r.Cells("ArticleId").Value & "")
                        For Each drfound As DataRow In dr
                            r.BeginEdit()
                            r.Cells("PlanQty").Value = drfound(1)
                            r.EndEdit()
                        Next
                    Next
                End If
            End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmProductionLevel"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Production Level (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Inventory
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class