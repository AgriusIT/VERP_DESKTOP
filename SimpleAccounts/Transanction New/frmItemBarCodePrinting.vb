Imports Neodynamic.SDK.Barcode

Public Class frmItemBarCodePrinting
    Dim IsOpenForm As Boolean = False
    Private Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Dim strSQL As String = String.Empty


            If Condition = "item" Then
                Dim Str As String = "SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Location.Ranks as Rake, IsNull(ArticleDefView.PackQty,0) as PackQty  FROM ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId where Active=1"
                Me.cmbItem.DataSource = Nothing
                FillUltraDropDown(Me.cmbItem, Str)
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("PackQty").Hidden = True
                    If Me.rdoCode.Checked = True Then
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

    Private Sub frmItemBarCodePrinting_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillCombos("item")
            IsOpenForm = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub PrintBarCodes(ByVal articleId As Integer, Optional ByVal rows As Integer = 0)
        Try
            Dim strQuery As String = String.Empty
            strQuery = "Select ArticleId, ArticleCode, ArticleDescription, Convert(image, null) as BarCode From ArticleDefTable Where ArticleDescription <> '' And ArticleId = '" & articleId & "'"
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            dt = GetDataTable(strQuery)
            dt.AcceptChanges()
            dt1.Columns.Add("ArticleId", GetType(Int32))
            dt1.Columns.Add("ArticleCode", GetType(String))
            dt1.Columns.Add("ArticleDescription", GetType(String))
            dt1.Columns.Add("BarCode", GetType(Byte()))

            For Each DR As DataRow In dt.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                ' bcp.Symbology = Symbology.Code39
                'bcp.Symbology = Symbology.Code93

                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                'bcp.BarWidth = 0.1F
                'bcp.Width = CSng(Me.ndBarWidth.Value)
                'bcp.Height = CSng(Me.ndBarHeight.Value)
                bcp.BarWidth = CSng(Me.ndBarWidth.Value)
                bcp.BarHeight = CSng(Me.ndBarHeight.Value)
                bcp.Font = New Font("Verdana", 8.0F, FontStyle.Regular, GraphicsUnit.Point)
                bcp.Code = "?" & DR.Item("ArticleCode").ToString

                'bcp.Code = DR.Item("Employee_Code").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                DR.EndEdit()
                If rows = 0 Then
                    rows = 1
                End If
                For index As Integer = 1 To rows
                    Dim dt1Rows As DataRow = dt1.NewRow
                    dt1Rows(0) = DR(0)
                    dt1Rows(1) = DR(1)
                    dt1Rows(2) = DR(2)
                    dt1Rows(3) = DR(3)
                    dt1.Rows.Add(dt1Rows)
                    dt1.AcceptChanges()
                Next
            Next
            ShowReport("rptArticleBarCode", , , , , , , dt1)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)

        End Try
        'Me.Close()

    End Sub
    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.rdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbPrint_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click
        PrintBarCodes(Me.cmbItem.Value, Val(Me.ndRows.Value))

    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click
        Try
            Me.cmbItem.Value = Nothing
            Me.ndRows.Value = 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmItemBarCodePrinting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ndBarHeight.DecimalPlaces = 2
        Me.ndBarHeight.Increment = 0.25D
        Me.ndBarWidth.DecimalPlaces = 3
        Me.ndBarWidth.Increment = 0.001D
        Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
        Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            tsbPrint_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

