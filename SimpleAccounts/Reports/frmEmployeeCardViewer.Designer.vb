<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeCardViewer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        'Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        'Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeCardViewer))
        'Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.SuspendLayout()
        ''
        ''ReportViewer1
        ''
        'Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        'ReportDataSource1.Name = "dsEmployeeInformation_dtEmployeeInformation"
        'ReportDataSource1.Value = Nothing
        'Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        'Me.ReportViewer1.LocalReport.EnableExternalImages = True
        'Me.ReportViewer1.LocalReport.EnableHyperlinks = True
        'Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "SimpleAccounts.rptEmployeeCard.rdlc"
        'Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        'Me.ReportViewer1.Name = "ReportViewer1"
        'Me.ReportViewer1.Size = New System.Drawing.Size(548, 486)
        'Me.ReportViewer1.TabIndex = 0
        '
        'frmEmployeeCardViewer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(548, 486)
        'Me.Controls.Add(Me.ReportViewer1)
        'Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEmployeeCardViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Card View"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
