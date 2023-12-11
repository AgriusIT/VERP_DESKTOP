Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmAddItem
    Implements IGeneral
    Enum EnumArticalLocation
        LocationID
        Location
        Rank
        Delete
        ArticleId
    End Enum
    Dim ArticleMaster As Article
    Dim ArticleDetail As ArticleDetail
    Dim ArticleId As Integer = 0I
    Dim COADetailId As Integer = 0I
    Private _str_Path As String = String.Empty
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        If Condition = "ArticalLocation" Then
            With Me.grdItemLocation.RootTable
                .Columns(EnumArticalLocation.LocationID).Visible = False
                .Columns(EnumArticalLocation.Location).Width = 200
                .Columns(EnumArticalLocation.Rank).Width = 400
                .Columns(EnumArticalLocation.Rank).MaxLength = 50
                .Columns(EnumArticalLocation.Rank).Caption = "Tag"
                .Columns(EnumArticalLocation.Rank).Selectable = True
                For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grdItemLocation.RootTable.Columns
                    If c.Index <> EnumArticalLocation.Rank Then
                        c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
                '.Columns(EnumArticalLocation.Delete).ColumnType = Janus.Windows.GridEX.ColumnType.Link
            End With
        End If
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Department" Then
                Me.cmbDepartment.DisplayMember = "ArticleGroupName"
                Me.cmbDepartment.ValueMember = "ArticleGroupId"
                Me.cmbDepartment.DataSource = New AddNewItemDAL().Department
            ElseIf Condition = "Type" Then
                Me.cmbtype.DisplayMember = "ArticleTypeName"
                Me.cmbtype.ValueMember = "ArticleTypeId"
                Me.cmbtype.DataSource = New AddNewItemDAL().type
            ElseIf Condition = "Unit" Then
                Me.cmbUnit.DisplayMember = "ArticleUnitName"
                Me.cmbUnit.ValueMember = "ArticleUnitId"
                Me.cmbUnit.DataSource = New AddNewItemDAL().unit
            ElseIf Condition = "Size" Then
                Me.lstSize.ListItem.DisplayMember = "ArticleSizeName"
                Me.lstSize.ListItem.ValueMember = "ArticleSizeId"
                Me.lstSize.ListItem.DataSource = New AddNewItemDAL().size
            ElseIf Condition = "Color" Then
                Me.lstCombination.ListItem.DisplayMember = "ArticleColorName"
                Me.lstCombination.ListItem.ValueMember = "ArticleColorId"
                Me.lstCombination.ListItem.DataSource = New AddNewItemDAL().Combination
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            Dim strArticleSizeCode As String = String.Empty
            Dim strArticleColorCode As String = String.Empty
            Dim str As String = String.Empty

            ArticleMaster = New Article
            ArticleMaster.ArticleCode = Me.txtItemCode.Text.Replace("'", "''")
            ArticleMaster.ArticleDescription = Me.txtItem.Text.Replace("'", "''")
            ArticleMaster.ArticleGenderID = 1
            ArticleMaster.Active = True
            ArticleMaster.ArticleGroupID = Me.cmbDepartment.SelectedValue
            ArticleMaster.ArticleLPOID = 1
            ArticleMaster.ArticleRemarks = String.Empty
            ArticleMaster.ArticleTypeID = Me.cmbtype.SelectedValue
            ArticleMaster.ArticleUnitID = Me.cmbUnit.SelectedValue
            ArticleMaster.PackQty = Val(Me.txtPackQty.Text)
            ArticleMaster.PurchasePrice = Val(Me.txtPuchasePrice.Text)
            ArticleMaster.SalePrice = Val(Me.txtSaleprice.Text)
            ArticleMaster.SortOrder = 1
            ArticleMaster.StockLevel = 0
            ArticleMaster.StockLevelMax = 0
            ArticleMaster.StockLevelOpt = 0
            ArticleMaster.AccountID = Val(CType(Me.cmbDepartment.SelectedItem, DataRowView).Row.Item("SubSubId").ToString)
            ArticleMaster.IsDate = Date.Today
            ArticleMaster.ArticlePicture = _str_Path
            ArticleMaster.LargestPackQty = Val(Me.txtLargestPackQty.Text)
            ArticleMaster.COADetail = New COADeail
            ArticleMaster.COADetail.COADetailID = COADetailId
            ArticleMaster.COADetail.DetailTitle = Me.txtItem.Text

            ArticleMaster.ArticleDetails = New List(Of ArticleDetail)
            Dim sSizeIds As String() = Me.lstSize.SelectedIDs.Split(",")
            Dim sColorIds As String() = Me.lstCombination.SelectedIDs.Split(",")
            For Each SizeId As String In sSizeIds
                'ArticleSizeCode Here...
                Str = "Select SizeCode From ArticleSizeDefTable WHERE ArticleSizeId =" & SizeId
                Dim dtArticleSizeCode As DataTable = GetDataTable(Str)
                If dtArticleSizeCode.Rows.Count > 0 Then
                    strArticleSizeCode = dtArticleSizeCode.Rows(0).Item(0).ToString
                Else
                    strArticleSizeCode = SizeId
                End If
                For Each ColorID As String In sColorIds
                    str = "Select ColorCode From ArticleColorDefTable WHERE ArticleColorId=" & ColorID
                    Dim dtArticleColorCode As DataTable = GetDataTable(str)
                    If dtArticleColorCode.Rows.Count > 0 Then
                        strArticleColorCode = dtArticleColorCode.Rows(0).Item(0).ToString
                    Else
                        strArticleColorCode = ColorID
                    End If
                    ArticleDetail = New ArticleDetail
                    ArticleDetail.ArticleCode = Me.txtItemCode.Text.Replace("'", "''") + "-" + strArticleSizeCode + "-" + strArticleColorCode
                    ArticleDetail.ArticleDescription = Me.txtItem.Text.Replace("'", "''")
                    ArticleDetail.ArticleGroupID = Me.cmbDepartment.SelectedValue
                    ArticleDetail.ArticleTypeID = Me.cmbtype.SelectedValue
                    ArticleDetail.ArticleGenderID = 0
                    ArticleDetail.ArticleUnitID = Me.cmbUnit.SelectedValue
                    ArticleDetail.ArticleLPOID = 0
                    ArticleDetail.PurchasePrice = Val(Me.txtPuchasePrice.Text)
                    ArticleDetail.SalePrice = Val(Me.txtSaleprice.Text)
                    ArticleDetail.PackQty = Val(Me.txtPackQty.Text)
                    ArticleDetail.StockLevel = 0
                    ArticleDetail.StockLevelMax = 0
                    ArticleDetail.Active = 0
                    ArticleDetail.SortOrder = 1
                    ArticleDetail.IsDate = Date.Today
                    ArticleDetail.SizeRangeID = SizeId
                    ArticleDetail.ArticleColorID = ColorID
                    ArticleDetail.Active = True
                    ArticleDetail.LargestPackQty = Val(Me.txtLargestPackQty.Text)
                    ArticleMaster.ArticleDetails.Add(ArticleDetail)
                Next
            Next

            ArticleMaster.ArticalLocationRank = New List(Of ArticalLocationRank)
            Dim objALR As ArticalLocationRank
            Dim dtGrid As DataTable = CType(Me.grdItemLocation.DataSource, DataTable)
            For Each dr As DataRow In dtGrid.Rows
                objALR = New ArticalLocationRank
                With objALR
                    .LocationID = Val(dr("Location ID").ToString)
                    .Rank = dr("Ranks").ToString
                End With
                ArticleMaster.ArticalLocationRank.Add(objALR)
            Next

            ArticleMaster.ActivityLog = New ActivityLog
            ArticleMaster.ActivityLog.UserID = LoginUserId
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.txtItemCode.Text = String.Empty Then
                ShowErrorMessage("Please enter valid article code")
                Me.txtItemCode.Focus()
                Return False
            End If
            If Me.txtItem.Text = String.Empty Then
                ShowErrorMessage("Please enter article description")
                Me.txtItem.Focus()
                Return False
            End If
            If Val(Me.txtPackQty.Text) = 0 Then
                ShowErrorMessage("Please enter minimum pack quantity of 1")
                Me.txtPackQty.Focus()
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
            If Not Me.cmbDepartment.SelectedIndex = -1 Then Me.cmbDepartment.SelectedIndex = 0
            If Not Me.cmbtype.SelectedIndex = -1 Then Me.cmbtype.SelectedIndex = 0
            Me.txtItemCode.Text = String.Empty
            Me.txtItem.Text = String.Empty
            If Not Me.cmbUnit.SelectedIndex = -1 Then Me.cmbUnit.SelectedIndex = 0
            Me.txtPuchasePrice.Text = 0
            Me.txtSaleprice.Text = 0
            Me.txtPackQty.Text = 1
            Dim strSQL As String = String.Empty
            strSQL = "SELECT location_id [Location ID],location_name [Location],'' [Ranks] FROM tblDefLocation"
            Me.grdItemLocation.DataSource = Nothing
            Me.grdItemLocation.DataSource = UtilityDAL.GetDataTable(strSQL)
            Me.grdItemLocation.RetrieveStructure()
            Me.ApplyGridSettings("ArticalLocation")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                Call New AddArticleDAL().Add(ArticleMaster)
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

    End Function

    Private Sub frmAddItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos("Department")
            FillCombos("Type")
            FillCombos("Unit")
            FillCombos("Size")
            FillCombos("Color")
            ApplyStyleSheet(Me)
            ReSetControls()
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmAddItem_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Try
        '    If e.Cancel = False Then
        '        Me.Close()
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            ' If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            If Save() = True Then
                If _str_Path <> String.Empty Then
                    If PictureBox1.Image IsNot Nothing Then
                        Try
                            PictureBox1.Image.Save(_str_Path, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Catch ex As Exception

                        End Try
                    End If
                End If
                'msg_Information(str_informSave)
                ReSetControls()
            End If
           
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub frmAddItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 16 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddDept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDept.Click
        Try
            If AddInventoryDept.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                Dim id As Integer = 0
                id = Me.cmbDepartment.SelectedValue
                FillCombos("Department")
                Me.cmbDepartment.SelectedValue = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAddType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddType.Click
        Try

            Add.Combo = Add.WhichCombo.Type
            If Add.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Dim id As Integer = 0I
                id = Me.cmbtype.SelectedValue
                FillCombos("Type")
                Me.cmbtype.SelectedValue = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnit.Click
        Try
            Add.Combo = Add.WhichCombo.Unit
            If Add.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Dim id As Integer = 0I
                id = Me.cmbUnit.SelectedValue
                FillCombos("Unit")
                Me.cmbUnit.SelectedValue = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            FillCombos("Size")
            FillCombos("Color")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ArticleId = 0
            COADetailId = 0
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lnkUploadPic_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkUploadPic.LinkClicked
        Try
            If Me.txtItemCode.Text = String.Empty Then
                ShowErrorMessage("Please enter Article code")
                Me.txtItemCode.Focus()
                Exit Sub
            End If
            OpenFileDialog1.Filter = "Image File |*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.PictureBox1.ImageLocation = OpenFileDialog1.FileName ' Image.FromStream(fs, False, False)
                PictureBox1.Update()
                _str_Path = _ArticlePicPath & "\" & OpenFileDialog1.FileName.Replace(OpenFileDialog1.FileName, Me.txtItemCode.Text.Replace("'", "''") & ".jpg")
                OpenFileDialog1.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class