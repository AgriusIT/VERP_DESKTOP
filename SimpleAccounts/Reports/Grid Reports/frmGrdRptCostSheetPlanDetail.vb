Imports SBModel
Public Class frmGrdRptCostSheetPlanDetail
    Dim intSRNo As Integer = 0I
    Public Sub FillCombo()
        Try
            FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], MasterId From ArticleDefView")
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
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
                ElseIf RightsDt.FormControlName = "Print" Then
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

    Private Sub frmGrdRptCostSheetPlanDetail_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            GetSecurityRights()
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            If Me.cmbItem.ActiveRow Is Nothing Then
                Me.cmbItem.Rows(0).Activate()
            End If
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombo()
            Me.cmbItem.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillGrid()

        '// Preparing connection
        Dim objCon As OleDb.OleDbConnection = Con
        '//Opening connection
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim cmd As New OleDb.OleDbCommand

        cmd.Connection = objCon
        cmd.CommandText = ""

        '// Clearing previous data
        cmd.CommandText = "Truncate Table tmpCostSheetPlan"
        cmd.ExecuteNonQuery()
        objCon.Close()

        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction

        Try

            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 300

            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO tmpCostSheetPlan([Main_Item_Id],[Parent_Item_Id],[Child_Item_Id],[ArticleDefId],[Qty],[CostPrice])  VALUES(" & Me.cmbItem.ActiveRow.Cells("MasterID").Value.ToString & ", 0," & Me.cmbItem.ActiveRow.Cells("MasterID").Value.ToString & "," & Me.cmbItem.Value & ",0,0) "
            cmd.ExecuteNonQuery()


            SaveAnotherCostSheet(0, trans)

            trans.Commit()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function SaveAnotherCostSheet(intCont As Integer, trans As OleDb.OleDbTransaction) As Integer

        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans

        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select *  from tmpCostSheetPlan Order By 1 ASC", trans)
            dt.AcceptChanges()

            If dt.Rows.Count > intCont Then

                For i As Integer = intCont To dt.Rows.Count - 1

                    Dim dt4 As New DataTable
                    dt4 = GetDataTable("Select tblCostSheet.ArticleId, tblCostSheet.MasterArticleId, Qty, ArticleDefTable.MasterID from tblCostSheet INNER JOIN ArticleDefTable on ArticleDefTable.ArticleId = tblCostSheet.ArticleId WHERE tblCostSheet.MasterArticleId = " & Val(dt.Rows(i).Item("Child_Item_Id").ToString) & "", trans)
                    dt4.AcceptChanges()

                    For Each dr As DataRow In dt4.Rows

                        cmd.CommandText = ""
                        cmd.CommandText = "INSERT INTO tmpCostSheetPlan([Main_Item_Id],[Parent_Item_Id],[Child_Item_Id],[ArticleDefId],[Qty],[CostPrice]) VALUES( " _
                            & " " & Val(Me.cmbItem.ActiveRow.Cells("MasterID").Value.ToString) & ", " & Val(dt.Rows(i).Item("Child_Item_Id").ToString) & "," & Val(dr.Item("MasterID").ToString) & "," & Val(dr.Item("ArticleId").ToString) & ", " & Val(dr.Item("Qty").ToString) & ",0) "
                        cmd.ExecuteNonQuery()

                    Next
                    intCont += 1
                Next

                SaveAnotherCostSheet(intCont, trans)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            FillGrid()
            FillGrid1()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillGrid1()
        Try

            Dim dt As New DataTable
            dt = GetDataTable("SELECT  [ID],[Main_Item_Id],[Parent_Item_Id],[Child_Item_Id],ArticleDefView.ArticleCode,ArticleDefView.ArticleDescription, 0 as [Esst Qty],[Qty],0 as [Actual Qty], [CostPrice],ArticleDefView.ArticlePicture " _
             & "  FROM [QamarTea_DB].[dbo].[tmpCostSheetPlan]  INNER JOIN ArticleDefView on ArticleDefView.ArticleId = tmpCostSheetPlan.ArticleDefId ")

            Me.grd.DataSource = dt
            Me.grd.ExpandRecords()

            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub grd_SelectionChanged(sender As Object, e As EventArgs) Handles grd.SelectionChanged
        Try


            If Me.grd.RowCount = 0 Then Exit Sub
            Me.lblItemCode.Text = Me.grd.GetRow.Cells("ArticleCode").Value.ToString
            Me.lblItemName.Text = Me.grd.GetRow.Cells("ArticleDescription").Value.ToString
            Me.lblEstimatedQty.Text = Me.grd.GetRow.Cells("Qty").Value.ToString
            Me.lblActualQty.Text = Me.grd.GetRow.Cells("Actual Qty").Value.ToString
            If IO.File.Exists(Me.grd.GetRow.Cells("ArticlePicture").Value.ToString) Then
                Me.PictureBox1.Image = Image.FromFile(Me.grd.GetRow.Cells("ArticlePicture").Value.ToString)
            Else
                Me.PictureBox1.Image = My.Resources.image_not_available
            End If



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cost Sheet Plan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class