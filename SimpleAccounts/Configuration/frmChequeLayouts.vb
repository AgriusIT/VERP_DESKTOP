Public Class frmChequeLayouts

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            Me.OpenFileDialog1.InitialDirectory = Application.StartupPath & "\Reports"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If IO.File.Exists(Me.OpenFileDialog1.FileName) Then
                    Me.txtLayoutPath.Text = Me.OpenFileDialog1.FileName
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Ehtisham ul haq

    ' Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Try
    '       If UltraTabControl1.SelectedTab.Index = 0 Then
    '          If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
    '             Me.Cursor = Cursors.WaitCursor
    '            Me.lblProgress.Text = "Processing Please Wait ..."
    '           Me.BackColor = Color.LightYellow
    '          Me.lblProgress.Visible = True
    '         Application.DoEvents()
    '        If Save() = True Then
    '           ReSetControls()
    '      End If
    '
    '             Else
    '                   If Update() = True Then
    '                      ReSetControls()
    '
    '                   End If
    '              End If
    '         End If
    '    Catch ex As Exception
    '   Finally
    '      Me.lblProgress.Visible = False
    '     Me.Cursor = Cursors.Default
    '
    '       End Try
    '  End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

    End Sub
End Class