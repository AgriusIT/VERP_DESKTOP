'22-June-2017 TFS# 981 : Ali Faisal : Add new form to save update and delete records through this form.
'12-March-2018 TFS#2708 : Ayesha Rehman :Service Item Tasks screen there should only shown services items
Imports SBDal
Imports SBModel
Public Class frmServiceItemTask
    Implements IGeneral
    Dim objModel As ServiceItemTaskBE
    Dim objDal As New ServiceItemTaskDAL
    Dim TaskId As Integer = 0
    ''' <summary>
    ''' Ali Faisal : Apply grid seeting to hide columns containing Id's and also to add a new column of Action to remove the selected row.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grd.RootTable.Columns("Id").Visible = False
            Me.grd.RootTable.Columns("ItemId").Visible = False
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grd.RootTable.Columns("TaskRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TaskRate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TaskRate").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("SortOrder").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("SortOrder").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TaskRate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("TaskRate").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End Apply grid settings

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    ''' <summary>
    ''' Ali Faisal : Apply Security for Standard User to show some specific buttons that he/she have rights to use at this form.
    ''' </summary>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
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
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End Apply Security
    ''' <summary>
    ''' 'Insert Activity Log by Ali Faisal on 29-June-2017
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDal.Delete(objModel.ItemId)
            SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.cmbItem.Value, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : FillCombos of items here.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Item" Then
                ''Ayesha Rehman : TFS2708 : only shown services items
                str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,PackQty as PackQty, ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], IsNull(TradePrice,0) as [Trade Price] ,Isnull(ServiceItem,0) As ServiceItem FROM ArticleDefView"
                str += " where Active=1 And Isnull(ServiceItem,0) = 1"
                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    ''22-June-2017 TFS# 981 : Ali Faisal : To change the dispaly member when check is changed
                    If Me.rbtnByCode.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End FillCombos
    ''' <summary>
    ''' Ali Faisal : FillModel to fill the values of Master and Detail records for further use
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New ServiceItemTaskBE
            objModel.ItemId = Me.cmbItem.Value
            objModel.Detail = New List(Of ServiceItemDetailTaskBE)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New ServiceItemDetailTaskBE
                Detail.TaskId = Val(Row.Cells("Id").Value.ToString)
                Detail.TaskTitle = Row.Cells("TaskTitle").Value.ToString
                Detail.TaskDetail = Row.Cells("TaskDetail").Value.ToString
                Detail.TaskUnit = Row.Cells("TaskUnit").Value.ToString
                Detail.TaskRate = Row.Cells("TaskRate").Value.ToString
                Detail.SortOrder = Row.Cells("SortOrder").Value.ToString
                Detail.Active = Row.Cells("Active").Value.ToString
                objModel.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End FillModel 

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : DisplayRecords to get the data from ArticleDefTaskDetails for selected Item in drop down
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Sub DisplayRecords(ByVal ItemId As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Id, ItemId, TaskTitle, TaskDetail, TaskUnit, CONVERT(decimal(18," & DecimalPointInValue & "),TaskRate) TaskRate, SortOrder, Active FROM ArticleDefTaskDetails Where ItemId = " & ItemId & ""
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End DisplayRecords
    ''' <summary>
    ''' Ali Faisal : Validate that Item is selected in the drop down
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Not Me.cmbItem.Value > 0 Then
                msg_Error("Please select any service Item")
                Me.cmbItem.Focus()
                Return False
            End If
            If Me.grd.RowCount = 0 Then
                msg_Error("No record found in grid")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '22-June-2017 TFS# 981 : Ali Faisal : End Validation
    ''' <summary>
    ''' Ali Faisal : ResetControls to initial stage from where start
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            TaskId = 0
            Me.txtTitle.Text = ""
            Me.txtDetail.Text = ""
            Me.txtUnit.Text = ""
            Me.txtRate.Text = 0
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End ResetControls
    ''' <summary>
    ''' Ali Faisal : Calls the save from DAL and also checked the validity before save.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                FillModel()
                objDal.Save(objModel)
                'Insert Activity Log by Ali Faisal on 29-June-2017
                SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.cmbItem.Value, True)
                Return True
            End If
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    '22-June-2017 TFS# 981 : Ali Faisal : End Save
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    ''' <summary>
    ''' Ali Faisal : Calls the Update from DAL and also checked the validity before update.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If IsValidate() = True Then
                objDal.Update(objModel)
                'Insert Activity Log by Ali Faisal on 29-June-2017
                SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.cmbItem.Value, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '22-June-2017 TFS# 981 : Ali Faisal : End Update
    ''' <summary>
    ''' Ali Faisal : Add data of all text boxes and checks to grid
    ''' </summary>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Sub AddToGrid()
        Try
            Dim dt As DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("Id") = 0
            dr("ItemId") = Me.cmbItem.Value
            dr("TaskTitle") = Me.txtTitle.Text
            dr("TaskDetail") = Me.txtDetail.Text
            dr("TaskUnit") = Me.txtUnit.Text
            dr("TaskRate") = Me.txtRate.Text
            dr("SortOrder") = Me.txtSortOrder.Text
            dr("Active") = Me.chkActive.Checked
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End Add to Grid
    ''' <summary>
    ''' Ali Faisal : Call all methods to reset controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            FillCombos("Item")
            DisplayRecords(-1)
            Me.cmbItem.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End New
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Save/Update records with Save button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Text = "&Save" Then
                If Save() = True Then
                    ReSetControls()
                    GetAllRecords()
                    FillCombos("Item")
                    DisplayRecords(-1)
                    msg_Information(str_informSave)
                    Me.cmbItem.Focus()

                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Me.grd.UpdateData()
                Update1()
                ReSetControls()
                GetAllRecords()
                FillCombos("Item")
                DisplayRecords(-1)
                msg_Information(str_informUpdate)
                Me.cmbItem.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End Save/Update
    ''' <summary>
    ''' Ali Faisal : Delete the records from Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Not Me.grd.RowCount = 0 Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Delete()
                msg_Information(str_informDelete)
                FillCombos("Item")
                DisplayRecords(-1)
                Me.cmbItem.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End Delete
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmServiceItemTask_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmServiceItemTask_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("Item")
            ReSetControls()
            ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Validate if Title/Detail is empty then do nothing other wise add data in Grid and After that focus on Item drop down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Not Me.txtTitle.Text = "" AndAlso Not Me.txtDetail.Text = "" Then
                AddToGrid()
                ReSetControls()
                Me.txtTitle.Focus()
            Else
                msg_Error("Please enter title or detail")
                Me.txtTitle.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '22-June-2017 TFS# 981 : Ali Faisal : End

    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.ValueChanged
        Try
            DisplayRecords(Me.cmbItem.Value)
            If Me.cmbItem.Value = 0 Or Me.grd.RowCount = 0 Then
                Me.btnSave.Text = "&Save"
            Else
                Me.btnSave.Text = "&Update"
            End If
            FillModel()
            ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDal.DeleteDetail(Val(Me.grd.GetRow.Cells("Id").Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Service Item Tasks"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtnByCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnByCode.CheckedChanged
        Try
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                ''22-June-2017 TFS# 981 : Ali Faisal : To change the dispaly member when check is changed
                If Me.rbtnByCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class