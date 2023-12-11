Imports SBDal
Imports SBModel
Public Class frmProItem
    Implements IGeneral
    Dim ObjModel As PropertyItemBE
    Dim objDAL As PropertyItemDAL
    Public Shared ProItemId As Integer = 0I
    Dim blnEditMode As Boolean = False

 
    Private Sub frmProItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtTitle.Select()
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
        'ToolTip
        'Ttip.SetToolTip(Me.txtTitle, "Enter title")
        'Ttip.SetToolTip(Me.dtpEntryDate, "Select entry date")
        'Ttip.SetToolTip(Me.txtPlotNo, "Enter plot number")
        'Ttip.SetToolTip(Me.txtSector, "Enter sector")
        'Ttip.SetToolTip(Me.txtBlock, "Enter block")
        'Ttip.SetToolTip(Me.cmbPropertyType, "Enter property type")
        'Ttip.SetToolTip(Me.txtPlotSize, "Enter plot size")
        Ttip.SetToolTip(Me.cmbTerritory, "Enter Territory")
        Ttip.SetToolTip(Me.cmbFeature, "Enter Features")
        Ttip.SetToolTip(Me.txtSource, "Enter ownership")
        Ttip.SetToolTip(Me.txtDemandAmount, "Enter amount demand")
        Ttip.SetToolTip(Me.txtRemarks, "Enter remarks")
        Ttip.SetToolTip(Me.cbActive, "Check active status")
        Ttip.SetToolTip(Me.btnCancel, "Click to close the window")
        Ttip.SetToolTip(Me.btnSave, "Click to save the data")
        ReSetControls()
        If ProItemId > 0 Then
            GetAllRecords()
        End If
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "PropertyType" Then
                FillDropDown(Me.cmbPropertyType, "SELECT PropertyTypeId, PropertyType FROM PropertyType WHERE Active = 1 ORDER BY PropertyTypeId ASC, SortOrder ASC", True)
            ElseIf Condition = "Territory" Then
                FillDropDown(Me.cmbTerritory, "SELECT TerritoryId, TerritoryName FROM tblListTerritory WHERE Active = 1 ORDER BY TerritoryId ASC, SortOrder ASC", True)
            ElseIf Condition = "Features" Then
                FillDropDown(Me.cmbFeature, "select distinct Features, Features from PropertyItem where Features <> ''")
                ''Start TFS2892
            ElseIf Condition = "Title" Then
                FillDropDown(Me.cmbTitle, "select distinct Title, Title from PropertyItem where Title <> ''")
            ElseIf Condition = "Sector" Then
                FillDropDown(Me.cmbSector, "select distinct Sector, Sector from PropertyItem where Sector <> ''")
            ElseIf Condition = "PlotNo" Then
                FillDropDown(Me.cmbPlotNo, "select distinct PlotNo, PlotNo from PropertyItem where PlotNo <> ''")
            ElseIf Condition = "Block" Then
                FillDropDown(Me.cmbBlock, "select distinct Block, Block from PropertyItem where Block <> ''")
            ElseIf Condition = "PlotSize" Then
                FillDropDown(Me.cmbPlotSize, "select distinct PlotSize, PlotSize from PropertyItem where PlotSize <> ''")

                ''End TFS2892
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            ObjModel = New PropertyItemBE
            If blnEditMode = False Then
                ObjModel.PropertyItemId = 0
            Else
                ObjModel.PropertyItemId = ProItemId
            End If
            ObjModel.Title = Me.cmbTitle.Text
            ObjModel.EntryDate = Me.dtpEntryDate.Value
            ObjModel.PlotNo = Me.cmbPlotNo.Text
            ObjModel.Sector = Me.cmbSector.Text
            ObjModel.Block = Me.cmbBlock.Text
            ObjModel.PropertyTypeId = Me.cmbPropertyType.SelectedValue
            ObjModel.PlotSize = Me.cmbPlotSize.Text
            ObjModel.TerritoryId = Me.cmbTerritory.SelectedValue
            ObjModel.Ownership = Me.txtSource.Text
            ObjModel.DemandAmount = Me.txtDemandAmount.Text
            ObjModel.Remarks = Me.txtRemarks.Text
            ObjModel.Active = Me.cbActive.CheckState
            ObjModel.Features = Me.cmbFeature.Text
            ObjModel.SourceMobileNo = Me.txtSourceMobileNo.Text
            ObjModel.ActivityLog = New ActivityLog
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New PropertyItemDAL
            Dim dt As DataTable = objDAL.GetById(ProItemId)
            If dt.Rows.Count > 0 Then
                ProItemId = dt.Rows(0).Item("PropertyItemId").ToString
                Me.cmbTitle.Text = dt.Rows(0).Item("Title").ToString ''TFS2892
                Me.dtpEntryDate.Value = dt.Rows(0).Item("EntryDate").ToString
                Me.cmbPlotNo.Text = dt.Rows(0).Item("PlotNo").ToString ''TFS2892
                Me.cmbSector.Text = dt.Rows(0).Item("Sector").ToString ''TFS2892
                Me.cmbBlock.Text = dt.Rows(0).Item("Block").ToString ''TFS2892
                Me.cmbPropertyType.SelectedValue = dt.Rows(0).Item("PropertyTypeId").ToString
                Me.cmbPlotSize.Text = dt.Rows(0).Item("PlotSize").ToString ''TFS2892
                Me.cmbTerritory.SelectedValue = dt.Rows(0).Item("TerritoryId").ToString
                Me.cmbFeature.Text = dt.Rows(0).Item("Features").ToString
                Me.txtSource.Text = dt.Rows(0).Item("Ownership").ToString
                Me.txtSourceMobileNo.Text = dt.Rows(0).Item("SourceMobileNo").ToString
                Me.txtDemandAmount.Text = dt.Rows(0).Item("DemandAmount").ToString
                Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                Me.cbActive.Checked = IIf(dt.Rows(0).Item("Active").ToString = True, True, False)
                blnEditMode = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbTitle.Text = "" Then
                msg_Error("Please enter the valid Item title")
                Me.cmbTitle.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtTitle.Focus()
            FillCombos("PropertyType")
            FillCombos("Territory")
            FillCombos("Features")
            FillCombos("Title")
            FillCombos("PlotNo")
            FillCombos("Block")
            FillCombos("Sector")
            FillCombos("PlotSize")
            'Me.txtTitle.Text = ""
            Me.dtpEntryDate.Value = Date.Now
            'Me.txtPlotNo.Text = ""
            'Me.txtSector.Text = ""
            'Me.txtBlock.Text = ""
            Me.cmbPropertyType.SelectedValue = 0
            Me.txtPlotSize.Text = ""
            Me.cmbTerritory.SelectedValue = 0
            'If Not Me.cmbTitle.SelectedIndex = -1 Then Me.cmbTitle.SelectedIndex = 1
            'If Not Me.cmbBlock.SelectedIndex = -1 Then Me.cmbBlock.SelectedIndex = 1
            'If Not Me.cmbPlotNo.SelectedIndex = -1 Then Me.cmbPlotNo.SelectedIndex = 1
            'If Not Me.cmbSector.SelectedIndex = -1 Then Me.cmbSector.SelectedIndex = 1
            'Me.cmbFeature.Text = 0
            Me.txtSource.Text = ""
            Me.txtSourceMobileNo.Text = ""
            Me.txtDemandAmount.Text = 0
            Me.txtRemarks.Text = ""
            Me.cbActive.Checked = True
            Me.blnEditMode = False
            Me.cmbTitle.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New PropertyItemDAL
            If IsValidate() = True Then
                objDAL.Add(ObjModel)
                Return True
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
            objDAL = New PropertyItemDAL
            If IsValidate() = True Then
                objDAL.Update(ObjModel)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If blnEditMode = False Then
                If frmProItemList.SaveRight = True Then
                    If Save() = True Then
                        ReSetControls()
                        GetAllRecords()
                        msg_Information(str_informSave)
                        Me.Close()
                    End If
                Else
                    msg_Error("Sorry! You don't have access rights")
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If frmProItemList.UpdateRight = True Then
                    Update1()
                    ReSetControls()
                    ProItemId = 0
                    GetAllRecords()
                    msg_Information(str_informUpdate)
                    Me.Close()
                Else
                    msg_Error("Sorry! You don't have access rights")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmPropertySearch.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDemandAmount_TextChanged(sender As Object, e As EventArgs) Handles txtDemandAmount.TextChanged
        Try
            If txtDemandAmount.Text = "" Then
                lblNumberConvert.Text = ""
            Else
                lblNumberConvert.Text = ModGlobel.NumberToWords(Me.txtDemandAmount.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class