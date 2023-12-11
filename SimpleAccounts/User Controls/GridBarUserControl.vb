Imports Janus.Windows.GridEX
Public Class GridBarUserControl

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

    Private _FormName As Form
    Public Property FormName() As Form
        Get
            Return Me._FormName
        End Get
        Set(ByVal value As Form)
            Me._FormName = value
        End Set
    End Property


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

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Me.PageSetupDialog1.PageSettings.Landscape = True
            Me.PageSetupDialog1.ShowDialog()

            'Me.GridEXPrintDocument1.PageHeaderCenter = Me.txtGridTitle.Text

            'Me._MyGrid.RootTable.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
            'Me._MyGrid.RootTable.Caption = Me.txtGridTitle.Text
            'Me._MyGrid.RootTable.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'Me._MyGrid.Refresh()
            'Me._MyGrid.RootTable.TableHeader = Janus.Windows.GridEX.InheritableBoolean.True
            If Not IsNothing(Me.PrintPreviewDialog1.Document) Then
                Me.PrintPreviewDialog1.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnFieldChooser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFieldChooser.Click
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

    Private Sub btnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click
        Try
            If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
            End If

            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.FormName.Name & "_" & Me.MyGrid.Name, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)

            Me.MyGrid.SaveLayoutFile(fs)

            fs.Flush()
            fs.Close()
            fs.Dispose()

            ShowInformationMessage("Settings saved successfully.")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridBarUserControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
