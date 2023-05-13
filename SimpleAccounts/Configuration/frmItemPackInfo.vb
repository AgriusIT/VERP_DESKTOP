Public Class frmItemPackInfo
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(ByVal MasterArticleId As Integer)


        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        GetPackData(MasterArticleId)
    End Sub
    Public Sub GetPackData(ByVal ArticleId As Integer)
        Try

            Dim strSQL As String = "Select ArticlePackId, PackName, Isnull(PackQty,1) as PackQty , Isnull(PackRate,1) as PackRate From ArticleDefPackTable WHERE ArticleMasterId = " & ArticleId & " ORDER BY PackName ASC "

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("ArticlePackId").Visible = False
            Me.grd.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("PackQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("PackRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackRate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackRate").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PackRate").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class