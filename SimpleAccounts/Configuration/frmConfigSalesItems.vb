Public Class frmConfigSalesItems

    Public isFormOpen As Boolean = False

    Private Sub frmConfigSalesItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        GetAllRecords()
        getConfigValueList()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            If Not getConfigValueByType("ServiceItem").ToString = "Error" Then
                Me.chkServiceItem.Checked = Convert.ToBoolean(getConfigValueByType("ServiceItem").ToString)
            End If
            If chkServiceItem.Checked = False Then
                Me.chkServiceItemNot.Checked = True
            End If

            If Not getConfigValueByType("LoadAllItemPackQty").ToString = "Error" Then
                Me.chkLoadAllItemPackQty.Checked = Convert.ToBoolean(getConfigValueByType("LoadAllItemPackQty").ToString)
            End If
            If chkLoadAllItemPackQty.Checked = False Then
                Me.chkLoadAllItemPackQtyNot.Checked = True
            End If

            If Not getConfigValueByType("ItemFilterByName").ToString = "Error" Then
                Me.chkItemFilterByName.Checked = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            If chkItemFilterByName.Checked = False Then
                Me.chkItemFilterByNameNot.Checked = True
            End If

            If Not getConfigValueByType("RackonSalesItemLoad").ToString = "Error" Then
                Me.chkRackonSalesItemLoad.Checked = Convert.ToBoolean(getConfigValueByType("RackonSalesItemLoad").ToString)
            End If
            If chkRackonSalesItemLoad.Checked = False Then
                Me.chkRackonSalesItemLoadNot.Checked = True
            End If

            If Not getConfigValueByType("SerialNoIncludingcharacter").ToString = "Error" Then
                Me.chkSerialNumeric.Checked = Convert.ToBoolean(getConfigValueByType("SerialNoIncludingcharacter").ToString)
            End If
            If chkSerialNumeric.Checked = False Then
                Me.chkSerialNumericNo.Checked = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub lblAccounts_Click(sender As Object, e As EventArgs) Handles lblAccounts.Click
        Try
            If frmConfigSalesAccount.isFormOpen = True Then
                frmConfigSalesAccount.Dispose()
            End If

            frmConfigSalesAccount.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblSecurity_Click(sender As Object, e As EventArgs) Handles lblSecurity.Click
        Try
            If frmconfigSalesSecurity.isFormOpen = True Then
                frmconfigSalesSecurity.Dispose()
            End If

            frmconfigSalesSecurity.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblGeneral_Click(sender As Object, e As EventArgs) Handles lblGeneral.Click
        Try
            If frmConfigSales.isFormOpen = True Then
                frmConfigSales.Dispose()
            End If

            frmConfigSales.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkServiceItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkServiceItem.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkLoadAllItemPackQty_CheckedChanged(sender As Object, e As EventArgs) Handles chkLoadAllItemPackQty.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkItemFilterByName_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemFilterByName.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkRackonSalesItemLoad_CheckedChanged(sender As Object, e As EventArgs) Handles chkRackonSalesItemLoad.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkSerialNumeric_CheckedChanged(sender As Object, e As EventArgs) Handles chkSerialNumeric.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

End Class