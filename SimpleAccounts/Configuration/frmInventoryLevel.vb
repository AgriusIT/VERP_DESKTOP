Imports System.Data.OleDb
Public Class frmInventoryLevel
    Enum GridCol
        Article_ID
        Code
        Description
        Group_Name
        Color
        Size
        Type
        Batch_No
        BatchID
        Min
        OPT
        Max
    End Enum

    Private Sub frmInventoryLevel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnClose_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmInventoryLevel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillGrid()

        If Me.grd.Rows.Count > 0 Then
            Me.grd.Rows.ExpandAll(True)
            Me.grd.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.BasedOnDataType
        End If
    End Sub
    Private Sub FillGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        Dim strSql As String

        strSql = "SELECT     dbo.tblStockStandard.Article_ID as ArticleID, dbo.ArticleDefTable.ArticleCode AS Code, dbo.ArticleDefTable.ArticleDescription AS [Article Description], " _
                    & " dbo.ArticleGroupDefTable.ArticleGroupName AS [Group Name], dbo.ArticleColorDefTable.ArticleColorName AS Color,  " _
                    & " dbo.ArticleSizeDefTable.ArticleSizeName AS [Size], dbo.ArticleTypeDefTable.ArticleTypeName AS [Article Type],  " _
                    & " dbo.PurchaseBatchTable.BatchNo AS [Batch No], dbo.tblStockStandard.Batch_ID as BatchID, dbo.tblStockStandard.Minimum AS [Min],  " _
                    & " dbo.tblStockStandard.Optimal AS Opt, dbo.tblStockStandard.Maximum AS [Max] " _
                    & " FROM         dbo.PurchaseBatchTable INNER JOIN dbo.tblStockStandard INNER JOIN  " _
                    & " dbo.ArticleDefTable ON dbo.tblStockStandard.Article_ID = dbo.ArticleDefTable.ArticleId INNER JOIN " _
                    & " dbo.ArticleGroupDefTable ON dbo.ArticleDefTable.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId INNER JOIN " _
                    & " dbo.ArticleColorDefTable ON dbo.ArticleDefTable.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId INNER JOIN " _
                    & " dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId INNER JOIN " _
                    & " dbo.ArticleTypeDefTable ON dbo.ArticleDefTable.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId ON  " _
                    & " dbo.PurchaseBatchTable.BatchID = dbo.tblStockStandard.Batch_ID " _
                    & " union " _
                    & " SELECT     dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleCode Code,  " _
                    & " dbo.ArticleDefTable.ArticleDescription [Article Description], dbo.ArticleGroupDefTable.ArticleGroupName [Group Name],  " _
                    & " dbo.ArticleColorDefTable.ArticleColorName [Color], dbo.ArticleSizeDefTable.ArticleSizeName [Size], dbo.ArticleTypeDefTable.ArticleTypeName [Article Type], dbo.PurchaseBatchTable.BatchNo [Batch No], PurchaseBatchTable.BatchID,  null as [Min], null as [Opt], null as [Max] FROM         dbo.ArticleDefTable  " _
                    & " INNER JOIN dbo.ArticleGroupDefTable ON dbo.ArticleDefTable.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId INNER JOIN  " _
                    & " dbo.ArticleTypeDefTable ON dbo.ArticleDefTable.ArticleTypeId = dbo.ArticleTypeDefTable.ArticleTypeId INNER JOIN  " _
                    & " dbo.ArticleSizeDefTable ON dbo.ArticleDefTable.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId INNER JOIN  " _
                    & " dbo.ArticleColorDefTable ON dbo.ArticleDefTable.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId  " _
                    & " INNER JOIN   dbo.SizeRangeTable ON dbo.ArticleSizeDefTable.ArticleSizeId = dbo.SizeRangeTable.SizeID  " _
                    & " INNER JOIN   dbo.PurchaseBatchTable ON dbo.SizeRangeTable.BatchID = dbo.PurchaseBatchTable.BatchID " _
                    & " Where  convert(varchar,dbo.ArticleDefTable.ArticleId)+'-'+ convert(varchar,PurchaseBatchTable.BatchID)+'-'+  " _
                    & " convert(varchar, articledeftable.sizerangeid) not in  " _
                    & " (SELECT     CONVERT(varchar, dbo.tblStockStandard.Article_ID) + '-' + CONVERT(varchar, dbo.tblStockStandard.Batch_ID) + '-' + CONVERT(varchar,  " _
                    & " dbo.ArticleSizeDefTable.ArticleSizeId) AS PK FROM         dbo.ArticleSizeDefTable INNER JOIN " _
                    & " dbo.ArticleDefTable ON dbo.ArticleSizeDefTable.ArticleSizeId = dbo.ArticleDefTable.SizeRangeId INNER JOIN " _
                    & " dbo.SizeRangeTable ON dbo.ArticleSizeDefTable.ArticleSizeId = dbo.SizeRangeTable.SizeID INNER JOIN " _
                    & " dbo.tblStockStandard ON dbo.ArticleDefTable.ArticleId = dbo.tblStockStandard.Article_ID AND  " _
                    & " dbo.SizeRangeTable.BatchID = dbo.tblStockStandard.Batch_ID)"

        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        grd.DataSource = dt
        GridFormating()


    End Sub

    Private Sub GridFormating()
        grd.DisplayLayout.Bands(0).Columns(0).Hidden = True
        grd.DisplayLayout.Bands(0).Columns("BatchID").Hidden = True
        'grd.DisplayLayout.Bands(0).Groups(0).

        'Dim band As UltraGridBand = Me.ultraGrid1.DisplayLayout.Bands(0)

        'band.SortedColumns.Add("col", True, True)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validate_Record() Then
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Save() Then
                'msg_Information("Record save successfully")
            End If
            Me.lblprogress.visible = False
        End If
    End Sub
    Private Function Validate_Record() As Boolean
        If grd.Rows.Count <= 0 Then
            ShowErrorMessage("There is no record in grid")
            Return False
        End If
        Dim intRecordCount As Integer = 0
        'Dim row As Infragistics.Win.UltraWinGrid.UltraGridRow

        Dim dt As DataTable = CType(grd.DataSource, DataTable).GetChanges(DataRowState.Modified)

        If dt Is Nothing Then
            ShowErrorMessage("You must enter values in grid")
            Return False
        End If

        'For Each row In grd.Rows
        '    If Not (TypeOf row Is Infragistics.Win.UltraWinGrid.UltraGridGroupByRow) Then
        '        MessageBox.Show(row.Cells(0).ToString)
        '    End If
        '    If Not Nothing Is row.ChildBands Then
        '        Dim childBand As Infragistics.Win.UltraWinGrid.UltraGridChildBand = Nothing
        '        For Each childBand In row.ChildBands
        '            '    MessageBox.Show(childBand.Rows(0).Cells(1).Value.ToString)
        '            MessageBox.Show(row.Cells(1).Value.ToString)

        '        Next

        '    End If

        'Next
        ''Dim row As Infragistics.Win.ultra
        'For i As Integer = 0 To grd.Rows.Count - 1
        '    'If grd.DisplayLayout.Bands(0).
        '    If Not (grd.Rows(i).Cells("Min").Value = 0 And grd.Rows(i).Cells("Opt").Value = 0 And grd.Rows(i).Cells("Max").Value = 0) Then
        '        intRecordCount = 1
        '        Exit For
        '    End If
        'Next

        'If intRecordCount = 0 Then
        '    MessageBox.Show("You must enter values in grid")
        '    Return False

        'End If
        Return True

    End Function

    Private Function Save() As Boolean
        Dim strSql As String
        Dim objCommand As New OleDb.OleDbCommand
        Dim objCon As OleDb.OleDbConnection
        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans

            ' ''First delete the existing record
            'strSql = "Delete from tblStockStandard"
            'objCommand.CommandText = strSql
            'objCommand.ExecuteNonQuery()

            Dim dt As DataTable = CType(grd.DataSource, DataTable).GetChanges(DataRowState.Modified)

            For i As Integer = 0 To dt.Rows.Count - 1
                strSql = "Insert into tblStockStandard (Article_ID, Batch_ID, Minimum, Optimal, Maximum) values( " _
                & " " & dt.Rows(i)("ArticleID") & ", " & dt.Rows(i)("BatchID") & ", " _
                & " " & Val(dt.Rows(i)("Min") & "") & "," & Val(dt.Rows(i)("Opt") & "") & ", " _
                & " " & Val(dt.Rows(i)("Max") & "") & ")"
                objCommand.CommandText = strSql
                objCommand.ExecuteNonQuery()
            Next

            trans.Commit()
            Save = True

        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

    End Function

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked = True Then
            Me.grd.Rows.CollapseAll(Me.CheckBox1.Checked)
        Else
            Me.grd.Rows.ExpandAll(True)
        End If
    End Sub
End Class