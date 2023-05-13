<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFinishSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFinishSetup))
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker3 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker4 = New System.ComponentModel.BackgroundWorker()
        Me.lblConnecting = New System.Windows.Forms.Label()
        Me.lblCreateDb = New System.Windows.Forms.Label()
        Me.lblRestoreDb = New System.Windows.Forms.Label()
        Me.lblFanalizing = New System.Windows.Forms.Label()
        Me.lnkConnecting = New System.Windows.Forms.LinkLabel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lnkCreateDb = New System.Windows.Forms.LinkLabel()
        Me.lnkRestoreDb = New System.Windows.Forms.LinkLabel()
        Me.lnkFanalizing = New System.Windows.Forms.LinkLabel()
        Me.pbxFinalazingSetup = New System.Windows.Forms.PictureBox()
        Me.pbxRestoreBlankDB = New System.Windows.Forms.PictureBox()
        Me.pbxCreateDatabase = New System.Windows.Forms.PictureBox()
        Me.pbxConnectionServer = New System.Windows.Forms.PictureBox()
        CType(Me.pbxFinalazingSetup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxRestoreBlankDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxCreateDatabase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxConnectionServer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BackgroundWorker1
        '
        '
        'BackgroundWorker2
        '
        '
        'BackgroundWorker3
        '
        '
        'lblConnecting
        '
        Me.lblConnecting.AutoSize = True
        Me.lblConnecting.Location = New System.Drawing.Point(18, 37)
        Me.lblConnecting.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConnecting.Name = "lblConnecting"
        Me.lblConnecting.Size = New System.Drawing.Size(155, 20)
        Me.lblConnecting.TabIndex = 0
        Me.lblConnecting.Text = "Connecting to server"
        '
        'lblCreateDb
        '
        Me.lblCreateDb.AutoSize = True
        Me.lblCreateDb.Location = New System.Drawing.Point(18, 66)
        Me.lblCreateDb.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCreateDb.Name = "lblCreateDb"
        Me.lblCreateDb.Size = New System.Drawing.Size(131, 20)
        Me.lblCreateDb.TabIndex = 1
        Me.lblCreateDb.Text = "Create Database"
        '
        'lblRestoreDb
        '
        Me.lblRestoreDb.AutoSize = True
        Me.lblRestoreDb.Location = New System.Drawing.Point(18, 95)
        Me.lblRestoreDb.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRestoreDb.Name = "lblRestoreDb"
        Me.lblRestoreDb.Size = New System.Drawing.Size(184, 20)
        Me.lblRestoreDb.TabIndex = 2
        Me.lblRestoreDb.Text = "Restore Blank Database"
        '
        'lblFanalizing
        '
        Me.lblFanalizing.AutoSize = True
        Me.lblFanalizing.Location = New System.Drawing.Point(18, 125)
        Me.lblFanalizing.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFanalizing.Name = "lblFanalizing"
        Me.lblFanalizing.Size = New System.Drawing.Size(131, 20)
        Me.lblFanalizing.TabIndex = 3
        Me.lblFanalizing.Text = "Finalaizing Setup"
        '
        'lnkConnecting
        '
        Me.lnkConnecting.AutoSize = True
        Me.lnkConnecting.Location = New System.Drawing.Point(266, 37)
        Me.lnkConnecting.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkConnecting.Name = "lnkConnecting"
        Me.lnkConnecting.Size = New System.Drawing.Size(90, 20)
        Me.lnkConnecting.TabIndex = 5
        Me.lnkConnecting.TabStop = True
        Me.lnkConnecting.Text = "In Progress"
        Me.lnkConnecting.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(22, 228)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(368, 35)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(477, 228)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 35)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Finish"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(399, 228)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 35)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Back"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'lnkCreateDb
        '
        Me.lnkCreateDb.AutoSize = True
        Me.lnkCreateDb.Location = New System.Drawing.Point(266, 66)
        Me.lnkCreateDb.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkCreateDb.Name = "lnkCreateDb"
        Me.lnkCreateDb.Size = New System.Drawing.Size(90, 20)
        Me.lnkCreateDb.TabIndex = 9
        Me.lnkCreateDb.TabStop = True
        Me.lnkCreateDb.Text = "In Progress"
        Me.lnkCreateDb.Visible = False
        '
        'lnkRestoreDb
        '
        Me.lnkRestoreDb.AutoSize = True
        Me.lnkRestoreDb.Location = New System.Drawing.Point(266, 95)
        Me.lnkRestoreDb.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkRestoreDb.Name = "lnkRestoreDb"
        Me.lnkRestoreDb.Size = New System.Drawing.Size(90, 20)
        Me.lnkRestoreDb.TabIndex = 10
        Me.lnkRestoreDb.TabStop = True
        Me.lnkRestoreDb.Text = "In Progress"
        Me.lnkRestoreDb.Visible = False
        '
        'lnkFanalizing
        '
        Me.lnkFanalizing.AutoSize = True
        Me.lnkFanalizing.Location = New System.Drawing.Point(266, 125)
        Me.lnkFanalizing.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkFanalizing.Name = "lnkFanalizing"
        Me.lnkFanalizing.Size = New System.Drawing.Size(90, 20)
        Me.lnkFanalizing.TabIndex = 11
        Me.lnkFanalizing.TabStop = True
        Me.lnkFanalizing.Text = "In Progress"
        Me.lnkFanalizing.Visible = False
        '
        'pbxFinalazingSetup
        '
        Me.pbxFinalazingSetup.Location = New System.Drawing.Point(231, 125)
        Me.pbxFinalazingSetup.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pbxFinalazingSetup.Name = "pbxFinalazingSetup"
        Me.pbxFinalazingSetup.Size = New System.Drawing.Size(32, 20)
        Me.pbxFinalazingSetup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbxFinalazingSetup.TabIndex = 15
        Me.pbxFinalazingSetup.TabStop = False
        '
        'pbxRestoreBlankDB
        '
        Me.pbxRestoreBlankDB.Location = New System.Drawing.Point(231, 95)
        Me.pbxRestoreBlankDB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pbxRestoreBlankDB.Name = "pbxRestoreBlankDB"
        Me.pbxRestoreBlankDB.Size = New System.Drawing.Size(32, 20)
        Me.pbxRestoreBlankDB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbxRestoreBlankDB.TabIndex = 14
        Me.pbxRestoreBlankDB.TabStop = False
        '
        'pbxCreateDatabase
        '
        Me.pbxCreateDatabase.Location = New System.Drawing.Point(231, 66)
        Me.pbxCreateDatabase.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pbxCreateDatabase.Name = "pbxCreateDatabase"
        Me.pbxCreateDatabase.Size = New System.Drawing.Size(32, 20)
        Me.pbxCreateDatabase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbxCreateDatabase.TabIndex = 13
        Me.pbxCreateDatabase.TabStop = False
        '
        'pbxConnectionServer
        '
        Me.pbxConnectionServer.Location = New System.Drawing.Point(231, 37)
        Me.pbxConnectionServer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pbxConnectionServer.Name = "pbxConnectionServer"
        Me.pbxConnectionServer.Size = New System.Drawing.Size(32, 20)
        Me.pbxConnectionServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbxConnectionServer.TabIndex = 12
        Me.pbxConnectionServer.TabStop = False
        '
        'frmFinishSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(576, 291)
        Me.Controls.Add(Me.pbxFinalazingSetup)
        Me.Controls.Add(Me.pbxRestoreBlankDB)
        Me.Controls.Add(Me.pbxCreateDatabase)
        Me.Controls.Add(Me.pbxConnectionServer)
        Me.Controls.Add(Me.lnkFanalizing)
        Me.Controls.Add(Me.lnkRestoreDb)
        Me.Controls.Add(Me.lnkCreateDb)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lnkConnecting)
        Me.Controls.Add(Me.lblFanalizing)
        Me.Controls.Add(Me.lblRestoreDb)
        Me.Controls.Add(Me.lblCreateDb)
        Me.Controls.Add(Me.lblConnecting)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFinishSetup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setup"
        CType(Me.pbxFinalazingSetup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxRestoreBlankDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxCreateDatabase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxConnectionServer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker4 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblConnecting As System.Windows.Forms.Label
    Friend WithEvents lblCreateDb As System.Windows.Forms.Label
    Friend WithEvents lblRestoreDb As System.Windows.Forms.Label
    Friend WithEvents lblFanalizing As System.Windows.Forms.Label
    Friend WithEvents lnkConnecting As System.Windows.Forms.LinkLabel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lnkCreateDb As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkRestoreDb As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkFanalizing As System.Windows.Forms.LinkLabel
    Friend WithEvents pbxConnectionServer As System.Windows.Forms.PictureBox
    Friend WithEvents pbxCreateDatabase As System.Windows.Forms.PictureBox
    Friend WithEvents pbxRestoreBlankDB As System.Windows.Forms.PictureBox
    Friend WithEvents pbxFinalazingSetup As System.Windows.Forms.PictureBox
End Class
