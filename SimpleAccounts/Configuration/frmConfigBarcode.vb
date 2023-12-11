Imports System.Drawing.Printing
Imports SBModel
Imports SBDal
Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Drawing.Text
Imports System.Drawing
Imports System.IO
Public Class frmConfigBarcode
    Public Shared InstalledPrinters As PrinterSettings.StringCollection
    ' Create a obejct of InstalledFontCollection
    Dim InstalledFonts As New InstalledFontCollection
    ' Gets the array of FontFamily objects associated with this FontCollection.
    Dim fontfamilies() As FontFamily = InstalledFonts.Families()
    Public isFormOpen As Boolean = False
    Dim _SearchBCode As New DataTable
    Dim _SearchDBCode As New DataTable
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try

            Dim cbValues As String = String.Empty

            cbValues = getConfigValueByType("BARCodeDisplayInformation").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 4 Then
                    'PN&False|PP&True|PC&True|VC&True|PQ&False
                    cbProname.Checked = Convert.ToBoolean(arday(0).Trim.Substring(3))
                    cbProPrice.Checked = Convert.ToBoolean(arday(1).Trim.Substring(3))
                    cbProductCode.Checked = Convert.ToBoolean(arday(2).Trim.Substring(3))
                    cbVendorCode.Checked = Convert.ToBoolean(arday(3).Trim.Substring(3))
                    cbPackQuantity.Checked = Convert.ToBoolean(arday(4).Trim.Substring(3))
                    cbPrintDate.Checked = Convert.ToBoolean(arday(5).Trim.Substring(3))
                    cbCompanyName.Checked = Convert.ToBoolean(arday(6).Trim.Substring(3))
                    cbProSize.Checked = Convert.ToBoolean(arday(7).Trim.Substring(3))
                End If
            End If
            Dim PrinterName As String = String.Empty
            PrinterName = getConfigValueByType("PrinterNameForBarCode").ToString()
            If PrinterName.Length > 0 Then
                For x As Integer = 0 To lbPrinterList.Items.Count - 1
                    If lbPrinterList.Items(x).ToString = PrinterName Then
                        lbPrinterList.SelectedItem = lbPrinterList.Items(x)
                        Exit For
                    End If
                Next
            End If
            Dim Font As String = String.Empty
            Font = getConfigValueByType("BarCodeFont").ToString()
            If Font.Length > 0 Then
                For x As Integer = 0 To cmbFont.Items.Count - 1
                    If cmbFont.Items(x).ToString = Font Then
                        cmbFont.SelectedItem = cmbFont.Items(x)
                        Exit For
                    End If
                Next
            Else
                cmbFont.SelectedIndex = 0
            End If
            cmbFontSize.Text = Val(getConfigValueByType("BarCodeFontSize").ToString())
            txtPrintCount.Text = Val(getConfigValueByType("PrintCountForBarCode").ToString())
            txtBarCodeLeftGap.Text = Val(getConfigValueByType("PrintBarcodeTopGap").ToString())
            txtBarCodeTopGap.Text = Val(getConfigValueByType("PrintBarcodeLeftGap").ToString())
            cmbDefaultBarCodeSource.SelectedIndex = 0
            If Not getConfigValueByType("DefaultBarCodeSource").ToString = "Error" Then
                Me.cmbDefaultBarCodeSource.Text = getConfigValueByType("DefaultBarCodeSource").ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional Condition As String = "")
        Try

            FillListBox(Me.lbUiBarcodeItems.ListItem, "Select ArticleId , ArticleCode + ' ~ ' + ArticleDescription  Item_Name from ArticleDefView where IsNull(ArticleBARCodeDisable, 0 ) = 0 order by ArticleId desc ")
            _SearchBCode = CType(Me.lbUiBarcodeItems.ListItem.DataSource, DataTable)
            _SearchBCode.AcceptChanges()
            lbUiBarcodeItems.DeSelect()
            FillListBox(Me.lbUiDiabledBarcode.ListItem, "Select ArticleId , ArticleCode + ' ~ ' + ArticleDescription  Item_Name from ArticleDefView where IsNull(ArticleBARCodeDisable, 0 ) = 1 order by ArticleId desc")
            _SearchDBCode = CType(Me.lbUiDiabledBarcode.ListItem.DataSource, DataTable)
            _SearchDBCode.AcceptChanges()
            lbUiDiabledBarcode.DeSelect()
            GetPrinters()
            GetFonts()
            GetFontSize()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cbProname_CheckedChanged(sender As Object, e As EventArgs) Handles cbProname.CheckedChanged, cbProPrice.CheckedChanged, cbPackQuantity.CheckedChanged, cbProductCode.CheckedChanged, cbVendorCode.CheckedChanged, cbCompanyName.CheckedChanged, cbPrintDate.CheckedChanged, cbProSize.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty

            strValues += "PN^" & cbProname.Checked & "|"
            strValues += "PP^" & cbProPrice.Checked & "|"
            strValues += "PC^" & cbProductCode.Checked & "|"
            strValues += "VC^" & cbVendorCode.Checked & "|"
            strValues += "PQ^" & cbPackQuantity.Checked & "|"
            strValues += "PD^" & cbPrintDate.Checked & "|"
            strValues += "CN^" & cbCompanyName.Checked & "|"
            strValues += "PS^" & cbProSize.Checked
            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, strValues)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetPrinters()
        Try
            lbPrinterList.Items.Clear()
            Dim i As Integer
            Dim pkInstalledPrinters As String

            For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
                pkInstalledPrinters = PrinterSettings.InstalledPrinters.Item(i)
                lbPrinterList.Items.Add(pkInstalledPrinters)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetFonts()
        Try
            For Each fontFamily As FontFamily In fontfamilies
                cmbFont.Items.Add(fontFamily.Name)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetFontSize()
        Try
            For i As Integer = 8 To 80 Step 2
                cmbFontSize.Items.Add(i)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmConfigBarcode_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.isFormOpen = True
            FillCombos()
            getConfigValueList()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBarCodeItems_KeyUp(sender As Object, e As KeyEventArgs) Handles txtBarCodeItems.KeyUp
        Try
            Dim dv As New DataView
            _SearchBCode.TableName = "Default"
            _SearchBCode.CaseSensitive = False
            dv.Table = _SearchBCode
            dv.RowFilter = "Item_Name Like '%" & Me.txtBarCodeItems.Text & "%'"
            Me.lbUiBarcodeItems.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDisabledBarCodeItems_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDisabledBarCodeItems.KeyUp
        Try
            Dim dv As New DataView
            _SearchDBCode.TableName = "Default"
            _SearchDBCode.CaseSensitive = False
            dv.Table = _SearchDBCode
            dv.RowFilter = "Item_Name Like '%" & Me.txtDisabledBarCodeItems.Text & "%'"
            Me.lbUiDiabledBarcode.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        Try
            If Me.lbUiBarcodeItems.SelectedItems.Length <= 0 And Me.lbUiDiabledBarcode.SelectedItems.Length <= 0 Then
                ShowErrorMessage("Please Select an Item")
            ElseIf Me.lbUiBarcodeItems.SelectedItems.Length > 0 Then
                UpdateBarCodeFlag(lbUiBarcodeItems.SelectedIDs, 1)
                FillCombos()
            ElseIf Me.lbUiDiabledBarcode.SelectedItems.Length > 0 Then
                UpdateBarCodeFlag(lbUiDiabledBarcode.SelectedIDs, 0)
                FillCombos()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UpdateBarCodeFlag(ByVal ArticleIds As String, ByVal flgDisabled As Boolean)

        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            Dim ArticleId As String() = ArticleIds.Split(New Char() {","c})
            Dim i As Integer = 0
            'Update Bar Code Flag in Article Defination
            For i = 0 To ArticleId.Length - 1
                str = "Update ArticleDeftable set ArticleBARCodeDisable = " & IIf(flgDisabled = True, 1, 0) & " where ArticleId = " & Val(ArticleId(i)) & " "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = "Select MasterID from ArticleDefView  where ArticleId = " & Val(ArticleId(i)) & " "
                Dim MasterId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                str = "Update ArticleDeftableMaster set ArticleBARCodeDisable = " & IIf(flgDisabled = True, 1, 0) & " where ArticleId = " & MasterId & " "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub lbBarcodeItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbUiBarcodeItems.SelectedIndexChaned
        Try
            If lbUiBarcodeItems.SelectedItems.Length > 0 Then
                lbUiDiabledBarcode.DeSelect()
            ElseIf lbUiDiabledBarcode.SelectedItems.Length > 0 Then
                lbUiDiabledBarcode.DeSelect()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lbDiabledBarcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbUiDiabledBarcode.SelectedIndexChaned
        Try
            If lbUiBarcodeItems.SelectedItems.Length > 0 Then
                lbUiBarcodeItems.DeSelect()
            ElseIf lbUiDiabledBarcode.SelectedItems.Length > 0 Then
                lbUiBarcodeItems.DeSelect()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lbPrinterList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbPrinterList.SelectedIndexChanged
        Try
            Dim strValues As String = String.Empty

            strValues = Me.lbPrinterList.SelectedItem.ToString
            If lbPrinterList.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(lbPrinterList.Tag, strValues)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbFont_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFont.SelectedIndexChanged
        Try
            Dim strValues As String = String.Empty

            strValues = Me.cmbFont.Text.ToString
            If cmbFont.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(cmbFont.Tag, strValues)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbFontSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFontSize.KeyPress
        Try
            cmbNumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbFontSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFontSize.SelectedIndexChanged
        Try
            Dim FontSize As Integer = 12
            If Val(Me.cmbFontSize.Text.ToString) > 8 Then
                FontSize = Val(Me.cmbFontSize.Text.ToString)
            End If
            If cmbFontSize.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(cmbFontSize.Tag, FontSize)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrintCount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrintCount.KeyPress, txtBarCodeLeftGap.KeyPress, txtBarCodeTopGap.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrintCount_TextChanged(sender As Object, e As EventArgs) Handles txtPrintCount.TextChanged
        Try
            Dim PrintCount As Integer = 1
            If Val(txtPrintCount.Text.ToString) <> 0 Then
                PrintCount = Val(txtPrintCount.Text.ToString)
            End If
            If txtPrintCount.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(txtPrintCount.Tag, PrintCount)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBarCodeLeftGap_TextChanged(sender As Object, e As EventArgs) Handles txtBarCodeLeftGap.TextChanged
        Try
            Dim LeftGap As Integer = 0
            If Val(txtBarCodeLeftGap.Text.ToString) <> 0 Then
                LeftGap = Val(txtBarCodeLeftGap.Text.ToString)
            End If
            If txtBarCodeLeftGap.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(txtBarCodeLeftGap.Tag, LeftGap)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBarCodeTopGap_TextChanged(sender As Object, e As EventArgs) Handles txtBarCodeTopGap.TextChanged
        Try
            Dim TopGap As Integer = 0
            If Val(txtBarCodeTopGap.Text.ToString) <> 0 Then
                TopGap = Val(txtBarCodeTopGap.Text.ToString)
            End If
            If txtBarCodeTopGap.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(txtBarCodeTopGap.Tag, TopGap)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub ComboBox1_Leave(sender As Object, e As EventArgs) Handles cmbDefaultBarCodeSource.Leave
        Try
            KeyType = "DefaultBarCodeSource"
            KeyValue = Me.cmbDefaultBarCodeSource.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class