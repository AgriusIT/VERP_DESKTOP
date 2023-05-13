Public Class uiCtrlGridBar

#Region "Properties"
    Public _AutoAdjust As Boolean = False
    Private AryColWidth() As Integer

    'Private _GridExPrintDocument As Janus.Windows.GridEX.GridEXPrintDocument

    'Public Property GridExPrintDocument() As Janus.Windows.GridEX.GridEXPrintDocument
    '    Get
    '        Return _GridExPrintDocument
    '    End Get
    '    Set(ByVal value As Janus.Windows.GridEX.GridEXPrintDocument)
    '        _GridExPrintDocument = value
    '        PrintPreviewDialog1.Document = value
    '    End Set
    'End Property

    Private _MyGrid As Janus.Windows.GridEX.GridEX
    Public Property MyGrid() As Janus.Windows.GridEX.GridEX
        Get
            Return Me._MyGrid
        End Get
        Set(ByVal value As Janus.Windows.GridEX.GridEX)
            Me._MyGrid = value
            Me.GridEXPrintDocument1.GridEX = value
        End Set
    End Property

#End Region




    ''========================================================

#Region "Local Functions"
    Private Function ExcelExport(ByVal MyDataGrid As Janus.Windows.GridEX.GridEX) As Object

        If MyDataGrid.RootTable Is Nothing Then
            Return Nothing
            Exit Function
        End If

        If MyDataGrid.RootTable.Columns.Count = 0 Then
            Return Nothing
            Exit Function
        End If


        Dim Excel As Object = CreateObject("Excel.Application")


        If Excel Is Nothing Then
            ShowErrorMessage("It appears that Excel is not installed on this machine. This operation requires MS Excel to be installed on this machine.")
            Return Nothing
        End If
        'Initialize Excel Sheet
        With Excel

            .Workbooks.Add()
            .SheetsInNewWorkbook = 1
            .Worksheets(1).Select()


            

            'Add header row to Excel Sheet by copying column headers from the Datagrid 

            Dim Col As Janus.Windows.GridEX.GridEXColumn
            Dim i As Integer = 1
            For Each Col In MyDataGrid.RootTable.Columns
                If Col.Visible = True Then
                    .Cells(1, i).Value = Col.Caption
                    .Cells(1, i).Font.bold = True
                    i += 1
                End If
            Next

            'Add data to excel sheet by looping through the rows
            'in the datagrid
            i = 2
            Dim RowItem As Janus.Windows.GridEX.GridEXRow
            Dim Cell As Janus.Windows.GridEX.GridEXCell
            For Each RowItem In MyDataGrid.GetRows
                Dim j As Integer = 1
                For Each Cell In RowItem.Cells
                    If Cell.Column.Visible = True Then
                        .Cells(i, j).Value = Cell.Value
                        j += 1
                    End If
                Next
                i += 1
            Next



            
        End With

        Return Excel


    End Function
    Private Function ExportToExcel(ByVal dt As DataTable, ByVal FileName As String) As Boolean
        'Try

        '    Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        '    Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        '    Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet


        '    wBook = excel.Workbooks.Add()
        '    wSheet = wBook.ActiveSheet()

        '    Dim dc As System.Data.DataColumn
        '    Dim dr As System.Data.DataRow
        '    Dim colIndex As Integer = 0
        '    Dim rowIndex As Integer = 0

        '    For Each dc In dt.Columns
        '        colIndex = colIndex + 1
        '        excel.Cells(1, colIndex) = dc.ColumnName
        '    Next

        '    For Each dr In dt.Rows
        '        rowIndex = rowIndex + 1
        '        colIndex = 0
        '        For Each dc In dt.Columns
        '            colIndex = colIndex + 1
        '            excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)

        '        Next
        '    Next
        '    wSheet.Range("A1", "IV1").Font.Bold = True
        '    wSheet.Columns.AutoFit()
        '    Dim strFileName As String = FileName ' "D:\ss.xls"
        '    Dim blnFileOpen As Boolean = False

        '    Try
        '        Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
        '        fileTemp.Close()
        '    Catch ex As Exception
        '        blnFileOpen = False
        '    End Try

        '    ' = New Font("Arial", 10, FontStyle.Bold)

        '    If System.IO.File.Exists(strFileName) Then
        '        System.IO.File.Delete(strFileName)
        '    End If

        '    wBook.SaveAs(strFileName)
        '    wBook.Close()
        '    excel.Quit()

        '    System.GC.Collect()

        '    excel = Nothing
        '    wBook = Nothing
        '    wSheet = Nothing
        '    Return True

        'Catch ex As Exception

        '    Return False
        'End Try


    End Function
#End Region

    ''========================================================

#Region "Events"
    Private Sub chkCollapsExpand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCollapsExpand.CheckedChanged
        If Not Me._MyGrid Is Nothing AndAlso Not Me._MyGrid.RootTable Is Nothing Then
            'If Me._MyGrid.RootTable.Groups.Count > 0 Then
            If Not Me.chkCollapsExpand.Checked Then Me._MyGrid.CollapseGroups() Else Me._MyGrid.ExpandGroups()
        End If

    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try

            Dim myDailog As New System.Windows.Forms.SaveFileDialog()
            myDailog.AddExtension = True
            myDailog.DefaultExt = ".xls"
            myDailog.Filter = "Excel Files|*.xls"

            If (myDailog.ShowDialog = DialogResult.OK) Then
                Dim strFileName As String
                strFileName = myDailog.FileName
                If strFileName.Length > 0 Then
                    ''Dim dt As DataTable = CType(_MyGrid.DataSource, DataTable)
                    ''If ExportToExcel(dt, strFileName) Then

                    'Dim Excel As Object = ExcelExport(_MyGrid)
                    'If Not IsNothing(Excel) Then





                    '        Try
                    '            Call Excel.Workbooks.Item(1).Close(True, myDailog.FileName.ToString)
                    '            Excel.Quit()
                    '            '                        ShowInformationMessage(gstrMsgAfterExport)
                    '            MsgBox("Exported successfully", MsgBoxStyle.Information)
                    '        Catch ex As Exception
                    '            ex = Nothing
                    '        End Try



                    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(Excel)
                    '        Excel = Nothing
                    '    End If
                    '    ' Excel = Nothing

                    'End If

                    Dim fs As New IO.FileStream(strFileName, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.GridEXExporter1.GridEX = MyGrid
                    Me.GridEXExporter1.Export(fs)

                    fs.Flush()
                    fs.Close()
                    fs.Dispose()

                    msg_Information("Exported successfully")

                End If
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Me.PageSetupDialog1.PageSettings.Landscape = True
        'Me.PageSetupDialog1.ShowDialog()

        Me.GridEXPrintDocument1.PageHeaderCenter = Me.txtGridTitle.Text

        'Me._MyGrid.RootTable.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
        'Me._MyGrid.RootTable.Caption = Me.txtGridTitle.Text
        'Me._MyGrid.RootTable.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        'Me._MyGrid.Refresh()
        'Me._MyGrid.RootTable.TableHeader = Janus.Windows.GridEX.InheritableBoolean.True
        If Not IsNothing(Me.PrintPreviewDialog1.Document) Then
            Me.PrintPreviewDialog1.ShowDialog()
        End If
        'Me._MyGrid.RootTable.TableHeader = Janus.Windows.GridEX.InheritableBoolean.False

    End Sub

    Private Sub btnDecreaseFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecreaseFont.Click
        If _MyGrid.Font.Size < 9 Then Exit Sub
        Dim fnt As New System.Drawing.Font(_MyGrid.Font.FontFamily, _MyGrid.Font.Size - 1, _MyGrid.Font.Style)
        _MyGrid.Font = fnt
    End Sub

    Private Sub btnIncreaseFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncreaseFont.Click
        If _MyGrid.Font.Size > 12 Then Exit Sub
        Dim fnt As New System.Drawing.Font(_MyGrid.Font.FontFamily, _MyGrid.Font.Size + 1, _MyGrid.Font.Style)
        _MyGrid.Font = fnt
    End Sub

    Private Sub btnBold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBold.Click
        Dim fnt As System.Drawing.Font
        If Me._MyGrid.Font.Bold = True Then
            fnt = New System.Drawing.Font(_MyGrid.Font.FontFamily, _MyGrid.Font.Size, FontStyle.Regular)
        Else
            fnt = New System.Drawing.Font(_MyGrid.Font.FontFamily, _MyGrid.Font.Size, FontStyle.Bold)
        End If

        _MyGrid.Font = fnt
        fnt = Nothing
    End Sub

    Public Sub btnAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjust.Click

        If Me._MyGrid.RootTable Is Nothing Then
            Exit Sub
        End If

        If Me._MyGrid.RootTable.Columns.Count = 0 Then
            Exit Sub
        End If

        If _AutoAdjust Then

            For ColInd As Integer = 0 To Me._MyGrid.RootTable.Columns.Count - 1
                Me._MyGrid.RootTable.Columns(ColInd).Width = AryColWidth(ColInd)
            Next

            _AutoAdjust = False
        Else

            ''Getting Default Colum Width of columns 
            ReDim AryColWidth(Me._MyGrid.RootTable.Columns.Count - 1)
            For ColInd As Integer = 0 To Me._MyGrid.RootTable.Columns.Count - 1
                AryColWidth(ColInd) = Me._MyGrid.RootTable.Columns(ColInd).Width
            Next

            For ColInd As Integer = 0 To Me._MyGrid.RootTable.Columns.Count - 1
                Me._MyGrid.RootTable.Columns(ColInd).AutoSize()
            Next

            _AutoAdjust = True

        End If

    End Sub




#End Region
 
    Private Sub txtGridTitle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGridTitle.TextChanged

    End Sub
End Class
