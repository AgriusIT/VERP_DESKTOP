''TASK TFS1777 done by Ameen on 29-12-2017. Load related items configured on inventory item definition.
Public Class frmRelatedItems
    Public articleid As Integer
    Public formname As String
    Sub New(ByRef fname As String)

        ' This call is required by the designer.
        InitializeComponent()
        formname = fname
        ' Add any initialization after the InitializeComponent() call.
        GetAll()
    End Sub
    Sub New(ByRef fname As String, ByVal MasterArticleId As Integer)


        ' This call is required by the designer.
        InitializeComponent()
        formname = fname
        ' Add any initialization after the InitializeComponent() call.
        GetAll(MasterArticleId)
        'GetAll()
    End Sub

    Private Sub GetAll(ByVal MasterArticleId As Integer)
        Try
            Dim dtChildItems As New DataTable
            Dim Strng As String = "Select RelatedArticleId FROM tblRelatedItem WHERE ArticleId = " & MasterArticleId & ""
            Dim dtRelatedItems As DataTable = GetDataTable(Strng)
            For Each Row As DataRow In dtRelatedItems.Rows
                Strng = String.Empty
                Strng = "SELECT ArticleId as Id, ArticleCode AS Code, ArticleDescription AS Description, ArticleTypeName as Type , ArticleCompanyName + ' > ' + ArticleLpoName AS [Category > Sub Category] , SalePrice , ArticleSizeName AS Size, ArticleUnitName AS Unit, ArticleColorName AS Color FROM ArticleDefView WHERE ArticleId = " & Row.Item("RelatedArticleId") & ""
                Dim dt As DataTable = GetDataTable(Strng)
                If dtChildItems.Rows.Count < 1 Then
                    dtChildItems = dt.Clone()
                End If
                If dt.Rows.Count > 0 Then
                    dtChildItems.Merge(dt)
                End If
            Next
            If dtChildItems.Rows.Count > 0 Then
                Me.GridEX1.DataSource = dtChildItems
                Me.GridEX1.RetrieveStructure()
                Me.GridEX1.RootTable.Columns("Id").Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAll()
        Try
            Dim dtChildItems As New DataTable
            Dim Strng As String = "SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination, ArticleModel.Name As Model, ArticleUnitName as Unit, ISNULL(PurchasePrice,0) as Price, Isnull(StockDetail.Stock,0) as Stock, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model ,ArticleDefView.ArticleBrandName As Grade, ArticleDefView.SizeRangeID as [Size ID], Location.Ranks as Rake, Isnull(ArticleDefView.SubSubId,0) as AccountId, ArticleDefView.SortOrder, ArticleDefView.MasterId  FROM ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT JOIN (Select ArticleDefId, Sum(IsNull(InQty, 0)-IsNull(OutQty, 0)) As Stock From StockDetailTable  WHERE LocationId=7 Group By ArticleDefId) As StockDetail ON ArticleDefView.ArticleId = StockDetail.ArticleDefId Left Outer Join (Select ArticleId, ArticleModelList.ModelId, tblDefModelList.Name From ArticleModelList Inner Join tblDefModelList ON  ArticleModelList.ModelId = tblDefModelList.ModelId) As ArticleModel On  ArticleDefView.ArticleId=ArticleModel.ArticleId where ArticleDefView.Active=1 ORDER BY ArticleDefView.SortOrder Asc"
            Dim dtRelatedItems As DataTable = GetDataTable(Strng)
            If dtRelatedItems.Rows.Count > 0 Then
                Me.GridEX1.DataSource = dtRelatedItems
                Me.GridEX1.RetrieveStructure()
                Me.GridEX1.RootTable.Columns("Id").Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' TASK TFS1777
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridEX1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEX1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.GridEX1.GetRow.Cells("Id").Value > 0 Then
                    If formname = "frmReceivingNote" Then
                        frmReceivingNote.grd.CurrentRow.Cells("AlternativeItemId").Value = Me.GridEX1.GetRow.Cells("Id").Value
                        frmReceivingNote.grd.CurrentRow.Cells("AlternativeItem").Value = Me.GridEX1.GetRow.Cells("Code").Value.ToString
                    ElseIf formname = "frmDeliveryChalan" Then
                        frmDeliveryChalan.grd.CurrentRow.Cells("AlternativeItemId").Value = Me.GridEX1.GetRow.Cells("Id").Value
                        frmDeliveryChalan.grd.CurrentRow.Cells("AlternativeItem").Value = Me.GridEX1.GetRow.Cells("Code").Value.ToString
                    Else
                        frmSales.cmbItem.Value = Me.GridEX1.GetRow.Cells("ArticleId").Value
                    End If

                    Me.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1777
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridEX1_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles GridEX1.RowDoubleClick
        Try
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.GridEX1.GetRow.Cells("Id").Value > 0 Then
                    If formname = "frmReceivingNote" Then
                        frmReceivingNote.grd.CurrentRow.Cells("AlternativeItemId").Value = Me.GridEX1.GetRow.Cells("Id").Value
                        frmReceivingNote.grd.CurrentRow.Cells("AlternativeItem").Value = Me.GridEX1.GetRow.Cells("Code").Value.ToString
                    ElseIf formname = "frmDeliveryChalan" Then
                        frmDeliveryChalan.grd.CurrentRow.Cells("AlternativeItemId").Value = Me.GridEX1.GetRow.Cells("Id").Value
                        frmDeliveryChalan.grd.CurrentRow.Cells("AlternativeItem").Value = Me.GridEX1.GetRow.Cells("Code").Value.ToString
                    Else
                        frmSales.cmbItem.Value = Me.GridEX1.GetRow.Cells("ArticleId").Value
                    End If

                    Me.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_Click(sender As Object, e As EventArgs) Handles GridEX1.Click
        Try
            If GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.GridEX1.GetRow.Cells("Id").Value > 0 Then
                    If formname = "frmReceivingNote" Then
                        frmReceivingNote.grd.CurrentRow.Cells("AlternativeItemId").Value = Me.GridEX1.GetRow.Cells("Id").Value
                        frmReceivingNote.grd.CurrentRow.Cells("AlternativeItem").Value = Me.GridEX1.GetRow.Cells("Code").Value.ToString
                    ElseIf formname = "frmDeliveryChalan" Then
                        frmDeliveryChalan.grd.CurrentRow.Cells("AlternativeItemId").Value = Me.GridEX1.GetRow.Cells("Id").Value
                        frmDeliveryChalan.grd.CurrentRow.Cells("AlternativeItem").Value = Me.GridEX1.GetRow.Cells("Code").Value.ToString
                    Else
                        frmSales.cmbItem.Value = Me.GridEX1.GetRow.Cells("ArticleId").Value
                    End If

                    Me.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class