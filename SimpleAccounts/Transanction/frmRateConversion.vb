Public Class frmRateConversion
    Dim ConversionTitle As String
    Dim Conversionfactor As Double
    Public Shared formname As String


    Private Sub frmRateConversion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRateConversion_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Conversionfactor = Val(getConfigValueByType("ConversionFactor").ToString)
            ConversionTitle = getConfigValueByType("ConversionTitle").ToString
            lblConvertedRate.Text = ""
            txtRate.Focus()
            If formname = "frmPurchaseNew" Then
                If frmPurchaseNew.txtCurrentRate.Text > "0" Then
                    txtRate.Text = frmPurchaseNew.txtCurrentRate.Text
                    If frmPurchaseNew.Rate > 0 Then
                        txtRate.Text = frmPurchaseNew.Rate
                    End If
                End If
            ElseIf formname = "frmPurchaseOrderNew" Then
                If frmPurchaseOrderNew.txtCurrentRate.Text > "0" Then
                    txtRate.Text = frmPurchaseOrderNew.txtCurrentRate.Text
                    If frmPurchaseOrderNew.Rate > 0 Then
                        txtRate.Text = frmPurchaseOrderNew.Rate
                    End If
                End If
            End If
            lblConversionTitle.Text = ConversionTitle
            lblConvertedRate.Text = Val(txtRate.Text) * Conversionfactor
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        Try
            lblConversionTitle.Text = ConversionTitle
            lblConvertedRate.Text = Val(txtRate.Text) * Conversionfactor
            'If formname = "frmPurchaseNew" Then
            '    frmPurchaseNew.txtCurrentRate.Text = lblConvertedRate.Text
            '    frmPurchaseNew.grd.CurrentRow.Cells("CurrentPrice").Value = lblConvertedRate.Text
            'Elseif formname = "frmPurchaseOrderNew" Then
            '    frmPurchaseOrderNew.txtCurrentRate.Text = lblConvertedRate.Text
            '    frmPurchaseOrderNew.grd.CurrentRow.Cells("CurrentPrice").Value = lblConvertedRate.Text
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRate.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If formname = "frmPurchaseNew" Then
                    frmPurchaseNew.txtCurrentRate.Text = lblConvertedRate.Text
                    If frmPurchaseNew.grd.RowCount > 0 Then
                        frmPurchaseNew.grd.CurrentRow.Cells("CurrentPrice").Value = lblConvertedRate.Text
                    End If

                ElseIf formname = "frmPurchaseOrderNew" Then
                    frmPurchaseOrderNew.txtCurrentRate.Text = lblConvertedRate.Text
                    If frmPurchaseOrderNew.grd.RowCount > 0 Then
                        frmPurchaseOrderNew.grd.CurrentRow.Cells("CurrentPrice").Value = lblConvertedRate.Text
                    End If
                End If
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class