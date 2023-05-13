<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotificationUtility
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
        Me.components = New System.ComponentModel.Container()
        Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsSMSNotification = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.cmsSMSNotification.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tray
        '
        Me.Tray.Text = "NotifyIcon1"
        Me.Tray.Visible = True
        '
        'cmsSMSNotification
        '
        Me.cmsSMSNotification.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.cmsSMSNotification.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmExit})
        Me.cmsSMSNotification.Name = "cmsSMSNotification"
        Me.cmsSMSNotification.Size = New System.Drawing.Size(112, 34)
        '
        'tsmExit
        '
        Me.tsmExit.Name = "tsmExit"
        Me.tsmExit.Size = New System.Drawing.Size(111, 30)
        Me.tsmExit.Text = "Exit"
        '
        'Timer1
        '
        '
        'BackgroundWorker1
        '
        '
        'frmNotificationUtility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(490, 542)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmNotificationUtility"
        Me.Opacity = 0.0R
        Me.ShowInTaskbar = False
        Me.Text = "Notification Utility"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.cmsSMSNotification.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tray As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmsSMSNotification As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
