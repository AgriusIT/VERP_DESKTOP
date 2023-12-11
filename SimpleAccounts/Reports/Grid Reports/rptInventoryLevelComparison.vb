Imports System.Data.OleDb
Imports SBModel
Public Class rptInventoryLevelComparison

    Private Sub rptInventoryLevelComparison_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub rptAccountBalances_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.FillAllCombos()
            Me.RefreshControls()
            Me.FillGrid()
            Me.GetSecurityRights()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Sub FillGrid()
        Try
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strLevel As String = String.Empty

            Dim strSql As String = "SELECT     dbo.ArticleDefTable.ArticleId , dbo.ArticleDefTable.ArticleCode [Article Code], dbo.ArticleDefTable.ArticleDescription as [Description] , dbo.ArticleGroupDefTable.ArticleGroupName as [Group], " _
                                 & " dbo.ArticleTypeDefTable.ArticleTypeName [Type], dbo.ArticleColorDefTable.ArticleColorName [Combinition], dbo.ArticleSizeDefTable.ArticleSizeName [Size], " _
                                 & " dbo.ArticleGenderDefTable.ArticleGenderName [Gender], dbo.ArticleLpoDefTable.ArticleLpoName [Distributer/LPO],  " _
                                 & " dbo.vw_ArticleStock.Stock    "

            If Me.cmbInventoryLevel.Text = "Min" Then
                strSql = strSql & " , dbo.ArticleDefTable.StockLevel as [Min Level]  , dbo.vw_ArticleStock.Stock - dbo.ArticleDefTable.StockLevel as Difference"
                strLevel = "  dbo.ArticleDefTable.StockLevel "
            ElseIf Me.cmbInventoryLevel.Text = "Opt" Then
                strSql = strSql & " , dbo.ArticleDefTable.StockLevelOpt as [Opt Level] , dbo.vw_ArticleStock.Stock - dbo.ArticleDefTable.StockLevelOpt as Difference "
                strLevel = "  dbo.ArticleDefTable.StockLevelOpt "
            ElseIf Me.cmbInventoryLevel.Text = "Max" Then
                strSql = strSql & " , dbo.ArticleDefTable.StockLevelMax as [Max Level], dbo.vw_ArticleStock.Stock - dbo.ArticleDefTable.StockLevelMax as Difference "
                strLevel = "  dbo.ArticleDefTable.StockLevelMax "
            End If

            strSql = strSql & "  FROM      dbo.ArticleDefTable INNER JOIN" _
                                 & " dbo.vw_ArticleStock ON dbo.ArticleDefTable.ArticleId = dbo.vw_ArticleStock.ArticleId LEFT OUTER JOIN  " _
                                 & "       dbo.ArticleLpoDefTable ON dbo.ArticleDefTable.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN  " _
                                 & "   dbo.ArticleGenderDefTable ON dbo.ArticleDefTable.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId LEFT OUTER JOIN  " _
                                 & " dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN  " _
                                 & " dbo.ArticleColorDefTable ON dbo.ArticleDefTable.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId LEFT OUTER JOIN  " _
                                 & " dbo.ArticleTypeDefTable ON dbo.ArticleDefTable.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId LEFT OUTER JOIN  " _
                                 & " dbo.ArticleGroupDefTable ON dbo.ArticleDefTable.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId  " _
                                 & " Where 1 = 1"

            If Me.CheckBox1.Checked = False Then

                If Me.cmbInventoryLevel.Text = "Min" Then
                    strSql = strSql & " and dbo.ArticleDefTable.StockLevel > 0  "
                    strLevel = "  dbo.ArticleDefTable.StockLevel "
                ElseIf Me.cmbInventoryLevel.Text = "Opt" Then
                    strSql = strSql & " and dbo.ArticleDefTable.StockLevelOpt > 0 "
                    strLevel = "  dbo.ArticleDefTable.StockLevelOpt "
                ElseIf Me.cmbInventoryLevel.Text = "Max" Then
                    strSql = strSql & " and dbo.ArticleDefTable.StockLevelMax > 0 "
                    strLevel = "  dbo.ArticleDefTable.StockLevelMax "
                End If
            End If

            If Me.uicmbCategory.Value > 0 Then
                strSql = strSql & " AND (dbo.ArticleDefTable.ArticleGroupId = " & Me.uicmbCategory.ActiveRow.Cells(0).Value & ")"
            End If

            If Me.uicmbType.Value > 0 Then
                strSql = strSql & " AND (dbo.ArticleDefTable.ArticleTypeId = " & Me.uicmbType.ActiveRow.Cells(0).Value & ")"
            End If

            If Me.uiCmbGender.Value > 0 Then
                strSql = strSql & " AND (dbo.ArticleDefTable.ArticleGenderId =  " & Me.uiCmbGender.ActiveRow.Cells(0).Value & ")"
            End If

            If Me.uicmbColor.Value > 0 Then
                strSql = strSql & " AND (dbo.ArticleDefTable.ArticleColorId =  " & Me.uicmbColor.ActiveRow.Cells(0).Value & ")"
            End If

            If Me.uicmbSize.Value > 0 Then
                strSql = strSql & " AND (dbo.ArticleDefTable.SizeRangeId  =  " & Me.uicmbSize.ActiveRow.Cells(0).Value & ")"
            End If

            If Me.uicmbDistributor.Value > 0 Then
                strSql = strSql & " AND (dbo.ArticleDefTable.ArticleLPOId  =  " & Me.uicmbDistributor.ActiveRow.Cells(0).Value & ")"
            End If

            If Me.cmbCondition.SelectedIndex <> -1 AndAlso Me.cmbCondition.Text.Length > 0 Then
                strSql = strSql & " AND dbo.vw_ArticleStock.Stock " & Me.cmbCondition.Text & strLevel
            End If

            strSql = strSql & " AND ArticleDefTable.Active = 1 "
            strSql = strSql & " Order by ArticleDefTable.sortorder"

            adp = New OleDbDataAdapter(strSql, Con)

            adp.Fill(dt)
            Me.grdStock.DataSource = dt
            Me.grdStock.RetrieveStructure()
            Me.grdStock.RootTable.Columns(0).Visible = False
            'Me.grdStock.DisplayLayout.Bands(0).Columns(0).Hidden = True

            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.FillGrid()
        Me.GetSecurityRights()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Me.SaveFileDialog1.Filter = "Excel Files|.xls"
        'Me.SaveFileDialog1.DefaultExt = ".xls"
        Me.SaveFileDialog1.FileName = "Inventory Levels"
        Me.SaveFileDialog1.InitialDirectory = "C:\"
        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If Me.SaveFileDialog1.FileName = "" Then Exit Sub

        'Me.UltraGridExcelExporter1.Export(Me.grdStock, Me.SaveFileDialog1.FileName) '& ".xls") '"c:\temp.xls")
        System.Diagnostics.Process.Start(Me.SaveFileDialog1.FileName) '& ".xls")

    End Sub

    Private Sub RefreshDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Not Me.grdStock.Rows.Count > 0 Then
        '    msg_Error(str_ErrorNoRecordFound) : Exit Sub
        'End If
        Me.BackgroundWorker1.RunWorkerAsync()

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Grid Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Sub FillAllCombos()
        Try

            FillUltraDropDown(Me.uicmbColor, "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder")
            FillUltraDropDown(Me.uicmbSize, "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder")
            FillUltraDropDown(Me.uicmbCategory, "select ArticleGroupId as Id, ArticleGroupName as Name from ArticleGroupDefTable where active=1 order by sortOrder")
            FillUltraDropDown(Me.uicmbType, "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder")
            FillUltraDropDown(Me.uiCmbGender, "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder")
            FillUltraDropDown(Me.uicmbDistributor, "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID,                       dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId")

            If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
            If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
            If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()
            If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()
            If Me.uiCmbGender.Rows.Count > 0 Then Me.uiCmbGender.Rows(0).Activate()
            If Me.uicmbDistributor.Rows.Count > 0 Then Me.uicmbDistributor.Rows(0).Activate()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.FillGrid()
    End Sub

    Private Sub RefreshControls()
        Try
            Me.cmbInventoryLevel.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0
            id = Me.uicmbColor.Value
            FillUltraDropDown(Me.uicmbColor, "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder")
            Me.uicmbColor.Value = id
            id = Me.uicmbSize.Value
            FillUltraDropDown(Me.uicmbSize, "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder")
            Me.uicmbSize.Value = id
            id = Me.uicmbCategory.Value
            FillUltraDropDown(Me.uicmbCategory, "select ArticleGroupId as Id, ArticleGroupName as Name from ArticleGroupDefTable where active=1 order by sortOrder")
            Me.uicmbCategory.Value = id
            id = Me.uicmbType.Value
            FillUltraDropDown(Me.uicmbType, "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder")
            Me.uicmbType.Value = id
            id = Me.uiCmbGender.Value
            FillUltraDropDown(Me.uiCmbGender, "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder")
            Me.uiCmbGender.Value = id
            id = Me.uicmbDistributor.Value
            FillUltraDropDown(Me.uicmbDistributor, "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID,                       dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId")
            If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
            If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
            If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()
            If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()
            If Me.uiCmbGender.Rows.Count > 0 Then Me.uiCmbGender.Rows(0).Activate()
            If Me.uicmbDistributor.Rows.Count > 0 Then Me.uicmbDistributor.Rows(0).Activate()
            Me.uicmbDistributor.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdStock.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Inventory Level Comparison" & Chr(10) & " Level: " & Me.cmbInventoryLevel.Text & "" & Chr(10) & "Print Date: " & Date.Now & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class