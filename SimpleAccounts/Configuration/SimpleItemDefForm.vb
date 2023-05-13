Imports System.Data.OleDb
Public Class SimpleItemDefForm
    Dim CurrentId As Integer

    Private Sub SimpleItemDefForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub DefMainAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Me.FillAllCombos()
            Me.RefreshForm() 'FillComboBox()
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        'If Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then Me.uitxtItemCode.Text = GetNextDocNo("", 6, "ArticleDefTable", "ArticleCode") Else Me.uitxtItemCode.Text = ""
        Me.uitxtItemCode.Text = ""
        Me.uitxtItemCode.Focus()
        Me.uitxtItemName.Text = ""
        Me.uitxtPackQty.Text = 1
        Me.uitxtStockLevel.Text = 0
        Me.uitxtStockLevelMaximum.Text = 0
        Me.uitxtStockLevelOptimal.Text = 0
        Me.uitxtPrice.Text = 0
        Me.uitxtSalePrice.Text = 0
        Me.uitxtSortOrder.Text = 0
        Me.txtAccountID.Text = 0
        Me.uichkActive.Checked = True
        ' Me.uicmbCategory.SelectedIndex = 0
        If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
        If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
        If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()
        If Me.uiCmbGender.Rows.Count > 0 Then Me.uiCmbGender.Rows(0).Activate()
        If Me.uicmbDistributor.Rows.Count > 0 Then Me.uicmbDistributor.Rows(0).Activate()
        ' If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()
        Me.BindGrid()

        Me.GetSecurityRights()

    End Sub
    Sub BindGrid()
        If Me.uicmbCategory.Rows.Count > 0 Then
            If Not Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then
                If Me.DataGridView1.RowCount > 0 Then DataGridView1.DataSource.clear()
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "SELECT     dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, dbo.ArticleGroupDefTable.ArticleGroupName, " & _
                 "dbo.ArticleGroupDefTable.ArticleGroupId,dbo.ArticleDefTable.PurchasePrice, dbo.ArticleDefTable.SalePrice, dbo.ArticleColorDefTable.ArticleColorName, dbo.ArticleSizeDefTable.ArticleSizeName, dbo.ArticleDefTable.PackQty,dbo.ArticleDefTable.StockLevel,dbo.ArticleDefTable.StockLevelopt,dbo.ArticleDefTable.StockLevelmax, " & _
                 "dbo.ArticleDefTable.Active, dbo.ArticleDefTable.SortOrder,dbo.ArticleDefTable.ArticleTypeId, dbo.ArticleDefTable.ArticleColorId,  dbo.ArticleDefTable.SizeRangeId, " & _
                 "  dbo.ArticleTypeDefTable.ArticleTypeName,   dbo.ArticleGenderDefTable.ArticleGenderName, dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS LPO, dbo.ArticleDefTable.AccountID  FROM         dbo.ArticleDefTable " & _
                 "INNER JOIN " & _
                     " dbo.ArticleGroupDefTable ON dbo.ArticleDefTable.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId INNER JOIN " & _
                     " dbo.ArticleTypeDefTable ON dbo.ArticleDefTable.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId INNER JOIN " & _
                      "dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId INNER JOIN " & _
                      "dbo.ArticleColorDefTable ON dbo.ArticleDefTable.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " & _
                      "dbo.ArticleGenderDefTable ON dbo.ArticleDefTable.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId LEFT OUTER JOIN " & _
                      "dbo.ArticleLpoDefTable ON dbo.ArticleDefTable.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN " & _
                      "dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId " & _
                " where ArticleDefTable.ArticleGroupId =" & Me.uicmbCategory.ActiveRow.Cells(0).Value & " order by ArticleDefTable.ArticleId desc"
        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        Me.DataGridView1.DataSource = dt
    End Sub
    Sub EditRecord()
        Me.uitxtItemCode.Text = DataGridView1.CurrentRow.Cells("Articlecode").Value.ToString
        Me.uitxtItemName.Text = DataGridView1.CurrentRow.Cells("ArticleDescription").Value.ToString
        ' Me.uicmbCategory.Text = DataGridView1.CurrentRow.Cells("ArticleGroupId").Value
        Me.uicmbSize.Text = DataGridView1.CurrentRow.Cells("ArticleSizeName").Value.ToString
        Me.uicmbColor.Text = DataGridView1.CurrentRow.Cells("ArticleColorName").Value.ToString
        Me.uicmbType.Text = DataGridView1.CurrentRow.Cells("ArticleTypeName").Value.ToString
        If Not Me.uiCmbGender.IsItemInList(DataGridView1.CurrentRow.Cells("Gender").Value.ToString) Then
            Me.uiCmbGender.Rows(0).Activate()
        Else
            Me.uiCmbGender.Text = DataGridView1.CurrentRow.Cells("Gender").Value.ToString

        End If
        If Not Me.uicmbDistributor.IsItemInList(DataGridView1.CurrentRow.Cells("LPO").Value.ToString) Then
            Me.uicmbDistributor.Rows(0).Activate()
        Else
            Me.uicmbDistributor.Text = DataGridView1.CurrentRow.Cells("LPO").Value.ToString
        End If
        Me.uitxtPackQty.Text = DataGridView1.CurrentRow.Cells("PackQty").Value.ToString
        Me.uitxtStockLevelOptimal.Text = DataGridView1.CurrentRow.Cells("StockLevelOpt").Value.ToString
        Me.uitxtStockLevelMaximum.Text = DataGridView1.CurrentRow.Cells("StockLevelMaximum").Value.ToString
        Me.uitxtStockLevel.Text = DataGridView1.CurrentRow.Cells("StockLevel").Value.ToString
        Me.uitxtPrice.Text = DataGridView1.CurrentRow.Cells("PurchasePrice").Value.ToString
        Me.txtOldPurchasePrice.Text = DataGridView1.CurrentRow.Cells("PurchasePrice").Value.ToString
        Me.uitxtSalePrice.Text = DataGridView1.CurrentRow.Cells("SalePrice").Value.ToString
        Me.txtOldSalePrice.Text = DataGridView1.CurrentRow.Cells("SalePrice").Value.ToString
        Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value.ToString
        Me.uichkActive.Checked = DataGridView1.CurrentRow.Cells("Active").Value.ToString
        Me.txtAccountID.Text = DataGridView1.CurrentRow.Cells("AccountID").Value.ToString

        Me.CurrentId = Me.DataGridView1.CurrentRow.Cells(0).Value.ToString
        Me.BtnSave.Text = "&Update"
        Me.GetSecurityRights()
    End Sub
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Me.EditRecord()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Not Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then
            msg_Error("Please select category")
            Me.uicmbCategory.Focus()
            Exit Sub
        End If

        If Not Me.uicmbType.ActiveRow.Cells(0).Value > 0 Then
            msg_Error("Please select type")
            Me.uicmbType.Focus()
            Exit Sub
        End If

        If Me.uitxtItemCode.Text = "" Then
            msg_Error("Please enter valid item code")
            Me.uitxtItemCode.Focus()
            Exit Sub

        End If

        If Me.uitxtItemName.Text = "" Then
            msg_Error("Please enter valid item name")
            Me.uitxtItemName.Focus()
            Exit Sub
        End If

        If Not Me.uicmbColor.ActiveRow.Cells(0).Value > 0 Then
            msg_Error("Please select color")
            Me.uicmbColor.Focus()
            Exit Sub
        End If

        If Not Me.uicmbSize.ActiveRow.Cells(0).Value > 0 Then
            msg_Error("Please select size")
            Me.uicmbSize.Focus()
            Exit Sub
        End If


        'If Not Me.uitxtPrice.Text > 0 Then
        '    msg_Error("Please enter valid purchase price")
        '    Me.uitxtPrice.Focus()
        '    Exit Sub
        'End If

        'If Not Me.uitxtSalePrice.Text > 0 Then
        '    msg_Error("Please enter valid sale price")
        '    Me.uitxtSalePrice.Focus()
        '    Exit Sub
        'End If

        If Not Me.uitxtPackQty.Text > 0 Then
            msg_Error("Please enter minimum pack quantity of 1")
            Me.uitxtPackQty.Focus()
            Exit Sub
        End If

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Dim trans As OleDbTransaction = Con.BeginTransaction

        ''get Sub Sub Code
        Dim SubSubCode As String = GetDataTable("SELECT sub_sub_code    FROM tblCOAMainSubSub   WHERE(main_sub_sub_id = " & Me.uicmbCategory.ActiveRow.Cells("SubSubID").Value & ")").Rows(0)(0).ToString()

        ' '' Check Acoount already exist
        'If Not Me.SaveToolStripButton.Text = "Save" Or Not Me.SaveToolStripButton.Text = "&Save" Then
        '    GetDataTable("")
        'End If

        ''get next Doc No
        Dim DocNo As String = Microsoft.VisualBasic.Right(GetNextDocNo(SubSubCode, 11, "tblCOAMainSubSubDetail", "detail_code"), 5)

        cm.Transaction = trans

        Dim identity As Integer = 0I

        Try
            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then

                cm.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title]) " & _
                                    "VALUES( " & Me.uicmbCategory.ActiveRow.Cells("SubSubID").Value & ", '" & SubSubCode & "-" & DocNo & "', '" & Me.uitxtItemName.Text.Trim.Replace("'", "''") & "') Select @@Identity"
                Dim AccountID As Integer = cm.ExecuteScalar()

                cm.CommandText = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId,SizeRangeId,ArticleColorId,ArticleTypeId,ArticleGenderId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel,StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID) values('" & Me.uitxtItemCode.Text & "','" & Me.uitxtItemName.Text & "'," & Me.uicmbCategory.ActiveRow.Cells(0).Value & "," & Me.uicmbSize.ActiveRow.Cells(0).Value & "," & Me.uicmbColor.ActiveRow.Cells(0).Value & "," & Me.uicmbType.ActiveRow.Cells(0).Value & "," & Me.uiCmbGender.ActiveRow.Cells(0).Value & "," & Me.uicmbDistributor.ActiveRow.Cells(0).Value & "," & Me.uitxtPrice.Text & "," & Me.uitxtSalePrice.Text & "," & Me.uitxtPackQty.Text & "," & Me.uitxtStockLevel.Text & "," & Me.uitxtStockLevelOptimal.Text & "," & Me.uitxtStockLevelMaximum.Text & "," & IIf(Me.uichkActive.Checked = True, 1, 0) & "," & Me.uitxtSortOrder.Text & ",'" & Date.Today & "', " & AccountID & ") Select @@Identity"
                identity = Convert.ToInt32(cm.ExecuteScalar())

                Me.CurrentId = identity
            Else
                If Val(Me.txtAccountID.Text) > 0 Then
                    cm.CommandText = "update ArticleDefTable set ArticleCode='" & Me.uitxtItemCode.Text & "',ArticleDescription='" & Me.uitxtItemName.Text & "', ArticleGroupId='" & Me.uicmbCategory.ActiveRow.Cells(0).Value & "',SizeRangeId=" & Me.uicmbSize.ActiveRow.Cells(0).Value & ",articleColorId=" & Me.uicmbColor.ActiveRow.Cells(0).Value & ",ArticleTypeId=" & Me.uicmbType.ActiveRow.Cells(0).Value & ",ArticleGenderId=" & Val(Me.uiCmbGender.ActiveRow.Cells(0).Value) & ",ArticlelpoId=" & Me.uicmbDistributor.ActiveRow.Cells(0).Value & ",PurchasePrice=" & Me.uitxtPrice.Text & ",SalePrice=" & Me.uitxtSalePrice.Text & ",PackQty=" & Me.uitxtPackQty.Text & ",StockLevel=" & Me.uitxtStockLevel.Text & ",StockLevelopt=" & Me.uitxtStockLevelOptimal.Text & ",StockLevelMax=" & Me.uitxtStockLevelMaximum.Text & ",Active=" & IIf(Me.uichkActive.Checked = True, 1, 0) & ",SortOrder=" & Me.uitxtSortOrder.Text & " where ArticleId=" & Me.CurrentId
                    cm.ExecuteNonQuery()

                    cm.CommandText = "update tblCOAMainSubsubDetail set Detail_title = '" & Me.uitxtItemName.Text.Trim.Replace("'", "''") & "' where coa_detail_id = " & Me.txtAccountID.Text
                    cm.ExecuteNonQuery()
                Else

                    cm.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title]) " & _
                                                     "VALUES( " & Me.uicmbCategory.ActiveRow.Cells("SubSubID").Value & ", '" & SubSubCode & "-" & DocNo & "', '" & Me.uitxtItemName.Text.Trim.Replace("'", "''") & "') Select @@Identity"

                    Dim AccountID As Integer = cm.ExecuteScalar()


                    cm.CommandText = "update ArticleDefTable set AccountId=" & AccountID & ", ArticleCode='" & Me.uitxtItemCode.Text & "',ArticleDescription='" & Me.uitxtItemName.Text & "', ArticleGroupId='" & Me.uicmbCategory.ActiveRow.Cells(0).Value & "',SizeRangeId=" & Me.uicmbSize.ActiveRow.Cells(0).Value & ",articleColorId=" & Me.uicmbColor.ActiveRow.Cells(0).Value & ",ArticleTypeId=" & Me.uicmbType.ActiveRow.Cells(0).Value & ",ArticleGenderId=" & Val(Me.uiCmbGender.ActiveRow.Cells(0).Value) & ",ArticlelpoId=" & Me.uicmbDistributor.ActiveRow.Cells(0).Value & ",PurchasePrice=" & Me.uitxtPrice.Text & ",SalePrice=" & Me.uitxtSalePrice.Text & ",PackQty=" & Me.uitxtPackQty.Text & ",StockLevel=" & Me.uitxtStockLevel.Text & ",StockLevelopt=" & Me.uitxtStockLevelOptimal.Text & ",StockLevelMax=" & Me.uitxtStockLevelMaximum.Text & ",Active=" & IIf(Me.uichkActive.Checked = True, 1, 0) & ",SortOrder=" & Me.uitxtSortOrder.Text & " where ArticleId=" & Me.CurrentId
                    cm.ExecuteNonQuery()

                    'cm.CommandText = "update tblCOAMainSubsubDetail set Detail_title = '" & Me.uitxtItemName.Text.Trim.Replace("'", "''") & "' where coa_detail_id = " & Me.txtAccountID.Text
                    'cm.ExecuteNonQuery()

                End If
            End If


            'update IncrementReductionTable when prices will be change

            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then

                cm.CommandText = "INSERT INTO IncrementReductionTable" _
                                              & " (IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice, SaleNewPrice) " _
                                              & " VALUES( getdate()," & Me.CurrentId & "," & 0 & " ," & 0 & " ," & Me.uitxtPrice.Text.Trim & " ," & 0 & " ," & Me.uitxtSalePrice.Text.Trim & " )"
                cm.ExecuteNonQuery()

            Else
                If Me.uitxtPrice.Text.Trim <> Me.txtOldPurchasePrice.Text.Trim Or Me.uitxtSalePrice.Text.Trim <> Me.txtOldSalePrice.Text.Trim Then

                    If Not msg_Confirm("Do you want to update the changes?") = True Then
                        trans.Rollback()
                        Exit Sub
                    End If

                    cm.CommandText = "SELECT     Stock  FROM vw_ArticleStock  WHERE(ArticleId = " & Me.CurrentId & ")"
                    Dim dt As DataTable = GetDataTable(cm.CommandText, trans)

                    Dim dblStock As Double = 0D
                    If dt.Rows.Count > 0 Then
                        dblStock = dt.Rows(0).Item(0)
                        cm.CommandText = "INSERT INTO IncrementReductionTable" _
                                   & " (IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice, SaleNewPrice) " _
                                   & " VALUES( getdate()," & Me.CurrentId & "," & dblStock & " ," & Me.txtOldPurchasePrice.Text.Trim & " ," & Me.uitxtPrice.Text.Trim & " ," & Me.txtOldSalePrice.Text.Trim & " ," & Me.uitxtSalePrice.Text.Trim & " )"
                        cm.ExecuteNonQuery()
                    End If

                End If

            End If

            'msg_Information(str_informSave)
            trans.Commit()

            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, Me.uitxtItemCode.Text.Trim, True)
            Catch ex As Exception
                trans.Rollback()
            End Try

            Me.CurrentId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshForm()
    End Sub
    Sub FillAllCombos()

        FillUltraDropDown(Me.uicmbColor, "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder")
        FillUltraDropDown(Me.uicmbSize, "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder")
        FillUltraDropDown(Me.uicmbCategory, "select ArticleGroupId as Id, ArticleGroupName as Name, SubSubID from ArticleGroupDefTable where active=1 order by sortOrder")
        FillUltraDropDown(Me.uicmbType, "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder")
        FillUltraDropDown(Me.uiCmbGender, "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder")
        FillUltraDropDown(Me.uicmbDistributor, "SELECT     dbo.ArticleLpoDefTable.ArticleLpoId AS ID,                       dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId")

        If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
        If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
        If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()
        If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()

        Me.uicmbCategory.DisplayLayout.Bands(0).Columns("SubSubID").Hidden = True

    End Sub
    Sub FillComboBox()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        Try
            'strSql = " SELECT coa_main_id,main_sub_id, main_title + '  " - "  ' + main_code  AS Title, tblCoaMainSub.sub_code AS sub_code, sub_title" & _
            '     " From tblCoaMain INNER JOIN" & _
            '     " tblCoaMainSub ON tblCoaMain.coa_main_id = tblCoaMainSub.coa_main_id" & _
            '     "  where tblCoaMain.coa_main_id = " & cboAccMaincode.ItemData(cboAccMaincode.ListIndex) & _
            '     " ORDER BY sub_code "
            adp = New OleDbDataAdapter("select * from ArticleGroupDefTable where active=1 order by sortOrder", Con)
            adp.Fill(dt)

            Me.uicmbCategory.ValueMember = "ARTICLEGROUPID"
            Me.uicmbCategory.DisplayMember = "ARTICLEGROUPNAME"
            Me.uicmbCategory.DataSource = dt

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.RefreshForm()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.EditRecord()
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        FillUltraDropDown(Me.uicmbCategory, "select ArticleGroupId as Id, ArticleGroupName as Name from ArticleGroupDefTable where active=1 order by sortOrder")
        If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()

    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label11.Click
        FillUltraDropDown(Me.uicmbColor, "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder")

        If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click
        FillUltraDropDown(Me.uicmbSize, "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder")

        If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click

        FillUltraDropDown(Me.uicmbType, "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder")

        If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()

    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("PurchaseOrderDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True And IsValidToDelete("ReceivingDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True And IsValidToDelete("SalesDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True And IsValidToDelete("SalesOrderDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Try
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from ArticleDefTable where ArticleId=" & Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, DataGridView1.CurrentRow.Cells("Articlecode").Value.ToString, True)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                    Me.lblProgress.Visible = False
                End Try
                Me.RefreshForm()


            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
    End Sub

    Private Sub uicmbCategory_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles uicmbCategory.RowSelected
        If Not Me.uicmbCategory.ActiveRow Is Nothing Then
            Me.BindGrid()
            ' If Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then Me.uitxtItemCode.Text = GetNextDocNo("", 6, "ArticleDefTable", "ArticleCode") Else Me.uitxtItemCode.Text = ""
            Me.uitxtItemCode.Text = ""
        End If
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                'Dim dt As DataTable = GetFormRights(EnumForms.SimpleItemDefForm)
                'If Not dt Is Nothing Then
                '    If Not dt.Rows.Count = 0 Then
                '        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                '            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                '        Else
                '            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                '        End If
                '        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                '        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                '        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                '    End If
                'End If
            Else
                'Me.Visible = False
                Me.BtnNew.Enabled = False
                Me.BtnEdit.Enabled = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "New" Then
                        Me.BtnNew.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Edit" Then
                        Me.BtnEdit.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
End Class