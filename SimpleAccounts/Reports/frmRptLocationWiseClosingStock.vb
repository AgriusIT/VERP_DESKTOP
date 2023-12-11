Public Class frmRptLocationWiseClosingStock


    Dim ShowZeroStock As Boolean = False
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Location" Then
                FillListBox(Me.lstLocation.ListItem, "Select Location_Id, Location_Name From tblDefLocation")
            ElseIf Condition = "Item" Then
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as Item, ArticleCode as Code, ArticleColorName as Color, ArticleSizeName as Size From ArticleDefView WHERE ArticleDescription <> '' Order By ArticleDescription ASC")
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmRptLocationWiseClosingStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FillCombo("Location")
            FillCombo("Item")
            chkShowZeroStock.Checked = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            GetCrystalReportRights()

            Dim strFilter As String = String.Empty
            strFilter = " {SP_LocationWiseClosingStock;1.ArticleDescription} <> ''"
            If Me.lstLocation.SelectedIDs.Trim.Length > 0 Then
                strFilter += " AND {SP_LocationWiseClosingStock;1.LocationId} IN [" & Me.lstLocation.SelectedIDs & "]"
            End If
            If Me.cmbItem.IsItemInList = True AndAlso Me.cmbItem.ActiveRow.Cells(0).Value > 0 Then
                strFilter += " AND {SP_LocationWiseClosingStock;1.ArticleId} =" & Me.cmbItem.Value & ""
            End If
            If Me.chkShowZeroStock.Checked = True Then
                ShowZeroStock = True
            Else
                ShowZeroStock = False
            End If
            AddRptParam("@ShowZeroStock", IIf(ShowZeroStock = True, 1, 0))
            ShowReport("rptLocationWiseClosingStock", strFilter.ToString)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class