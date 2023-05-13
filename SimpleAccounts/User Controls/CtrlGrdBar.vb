Imports Janus.Windows.GridEX
Public Class CtrlGrdBar
    Public _AutoAdjust As Boolean = False
    Private AryColWidth() As Integer
    Private _MyGrid As Janus.Windows.GridEX.GridEX
    Private _ExpandCheckbox As Boolean = False
    Public str_Path As String = String.Empty
    Private _Email As SBModel.SendingEmail
    Private _FormName As Form
    Public Enum EnmAccountType
        Customers
        Vendors
        General
    End Enum
    Public Shared strForm_Name As String = String.Empty

    Public Property MyGrid() As Janus.Windows.GridEX.GridEX
        Get
            Return Me._MyGrid
        End Get
        Set(ByVal value As Janus.Windows.GridEX.GridEX)
            Me._MyGrid = value
            Me.GridEXPrintDocument1.GridEX = value
        End Set
    End Property
    Public Property Email() As SBModel.SendingEmail
        Get
            'Return CtrlGrdBar1.Email
            Return _Email
        End Get
        Set(ByVal value As SBModel.SendingEmail)
            _Email = value
        End Set
    End Property
    Public Property FormName() As Form
        Get
            Return Me._FormName
        End Get
        Set(ByVal value As Form)
            Me._FormName = value
        End Set
    End Property
    'Private Sub GridBarUserControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub mGridPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mGridPrint.Click
        Try
            Me.PageSetupDialog1.PageSettings.Landscape = True
            Me.GridEXPrintDocument1.PageHeaderCenter = Me.txtGridTitle.Text
            Me.PageSetupDialog1.ShowDialog()
            If Not IsNothing(Me.PrintPreviewDialog1.Document) Then
                : Me.PrintPreviewDialog1.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub mGridChooseFielder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mGridChooseFielder.Click
        Try
            Me.MyGrid.AllowRemoveColumns = InheritableBoolean.True
            If Me.MyGrid.IsFieldChooserVisible = False Then
                Me.MyGrid.ShowFieldChooser(Me.FormName)
            Else
                Me.MyGrid.HideFieldChooser()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub mGridSaveLayouts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mGridSaveLayouts.Click
        Try
            If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
            End If

            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.FormName.Name & "_" & Me.MyGrid.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)

            Me.MyGrid.SaveLayoutFile(fs)

            fs.Flush()
            fs.Close()
            fs.Dispose()

            msg_Information("Settings saved successfully.")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub mGridExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mGridExport.Click
        Try
            Dim myDailog As New System.Windows.Forms.SaveFileDialog()
            myDailog.AddExtension = True
            myDailog.DefaultExt = ".xls"
            myDailog.Filter = "Excel Files|*.xls"
            If (myDailog.ShowDialog = DialogResult.OK) Then
                Dim strFileName As String
                strFileName = myDailog.FileName
                If strFileName.Length > 0 Then
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
    Private Sub btnGridControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub GroupCollapsExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupCollaps.Click
        Try
            _ExpandCheckbox = False
            If Not Me._MyGrid Is Nothing AndAlso Not Me._MyGrid.RootTable Is Nothing Then
                'If Me._MyGrid.RootTable.Groups.Count > 0 Then
                If Not _ExpandCheckbox Then
                    Me._MyGrid.CollapseGroups()
                    Me._MyGrid.CollapseRecords()
                Else
                    Me._MyGrid.ExpandGroups()
                    Me._MyGrid.ExpandRecords()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridResizeColumnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridResizeColumnToolStripMenuItem.Click
        Try
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

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GroupExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupExpand.Click
        Try
            _ExpandCheckbox = True
            If Not Me._MyGrid Is Nothing AndAlso Not Me._MyGrid.RootTable Is Nothing Then
                'If Me._MyGrid.RootTable.Groups.Count > 0 Then
                If Not _ExpandCheckbox Then
                    Me._MyGrid.CollapseGroups()
                    Me._MyGrid.CollapseRecords()
                Else
                    Me._MyGrid.ExpandGroups()
                    Me._MyGrid.ExpandRecords()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SendEmailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendEmailToolStripMenuItem.Click
        Try
            If Email Is Nothing Then
                Me.SendEmailToolStripMenuItem.Enabled = False
                Exit Sub
            Else
                Me.SendEmailToolStripMenuItem.Enabled = True
            End If
            If Me.MyGrid.RowCount = 0 Then Exit Sub
            str_Path = _FileExportPath & "\" & Email.DocumentNo & "-" & Email.DocumentDate.Date.ToString("dd-MM-yyyy") & ".xls"
            Email.FileAttachment = str_Path
            If Not IO.File.Exists(str_Path) Then
                Dim fs As New IO.FileStream(str_Path, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.GridEXExporter1.GridEX = MyGrid
                Me.GridEXExporter1.Export(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
            End If
            Dim frmEmail As New frmOutgoingEmail
            If Email.ToEmail Is Nothing Then frmEmail.txtTo.Text = String.Empty Else frmEmail.txtTo.Text = Email.ToEmail
            If Email.CcEmail Is Nothing Then frmEmail.txtCc.Text = String.Empty Else frmEmail.txtCc.Text = Email.CcEmail
            If Email.BccEmail Is Nothing Then frmEmail.txtBcc.Text = String.Empty Else frmEmail.txtBcc.Text = Email.BccEmail
            If Email.Subject Is Nothing Then frmEmail.txtSubject.Text = String.Empty Else frmEmail.txtSubject.Text = Email.Subject
            If Email.Body Is Nothing Then frmEmail.txtBody.Text = String.Empty Else frmEmail.txtBody.Text = Email.Body
            If Email.FileAttachment Is Nothing Then frmEmail.txtDataFile.Text = String.Empty Else frmEmail.txtDataFile.Text = Email.FileAttachment
            frmEmail.Text = frmEmail.Text + " " + "[" & FormName.Text & "]"
            frmEmail.StartPosition = FormStartPosition.CenterScreen
            frmEmail.Show()
            frmEmail.BringToFront()
            frmEmail.TopMost = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

        End Try
    End Sub
    Private Sub EmailTemplateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            GetStrQuery()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Shared Function GetStrQuery() As Boolean
        Try
            GetStrQuery = Nothing
            Dim str As String = String.Empty
            frmEmailTemplate.btnInsertField.DropDownItems.Clear()
            Dim frm As New frmEmailTemplate
            If strForm_Name = EnmAccountType.Customers Then
                str = "Select * From tblCustomer"
            ElseIf strForm_Name = EnmAccountType.Vendors Then
                str = "Select * From tblVendor"
            ElseIf strForm_Name = EnmAccountType.General Then
                str = "Select * from vwCOADetail"
            End If
            If strForm_Name.Length > 0 Then
                Dim dt As DataTable = GetDataTable(str)
                For Each col As DataColumn In dt.Columns
                    frm.btnInsertField.DropDownItems.Add(col.ColumnName)
                Next
            Else
                Exit Function
            End If
            frm.ShowDialog()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnGridControl_ButtonDoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGridControl.ButtonDoubleClick
        Try
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnGridControl_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGridControl.ButtonClick

    End Sub

    Private Sub PrintPreviewDialog1_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog1.Load

    End Sub

    ''TASK TFS3586
    'Private Sub btnTreeCollapse_Click(sender As Object, e As EventArgs) Handles btnTreeCollapse.Click
    '    Try
    '        _ExpandCheckbox = False
    '        If Not Me._MyGrid Is Nothing AndAlso Not Me._MyGrid.RootTable Is Nothing Then
    '            'If Me._MyGrid.RootTable.Groups.Count > 0 Then
    '            If Not _ExpandCheckbox Then Me._MyGrid.CollapseRecords() Else Me._MyGrid.ExpandRecords()
    '        End If

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnTreeExpand_Click(sender As Object, e As EventArgs) Handles btnTreeExpand.Click
    '    Try
    '        _ExpandCheckbox = True
    '        If Not Me._MyGrid Is Nothing AndAlso Not Me._MyGrid.RootTable Is Nothing Then
    '            'If Me._MyGrid.RootTable.Groups.Count > 0 Then
    '            If Not _ExpandCheckbox Then Me._MyGrid.CollapseRecords() Else Me._MyGrid.ExpandRecords()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    ''END TASK TFS3586
End Class
