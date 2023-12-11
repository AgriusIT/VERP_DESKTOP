<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRevenueDataImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRevenueDataImport))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnImportdata = New System.Windows.Forms.Button()
        Me.lblFilesSelected = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblNoOfFiles = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(630, 44)
        Me.pnlHeader.TabIndex = 14
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(580, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(38, 30)
        Me.btnClose.TabIndex = 1
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(24, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(243, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Revenue Data Import"
        '
        'btnImportdata
        '
        Me.btnImportdata.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportdata.Location = New System.Drawing.Point(134, 59)
        Me.btnImportdata.Name = "btnImportdata"
        Me.btnImportdata.Size = New System.Drawing.Size(182, 26)
        Me.btnImportdata.TabIndex = 15
        Me.btnImportdata.Text = "Select files to Import Data From:"
        Me.btnImportdata.UseVisualStyleBackColor = True
        '
        'lblFilesSelected
        '
        Me.lblFilesSelected.AutoSize = True
        Me.lblFilesSelected.Location = New System.Drawing.Point(354, 66)
        Me.lblFilesSelected.Name = "lblFilesSelected"
        Me.lblFilesSelected.Size = New System.Drawing.Size(73, 13)
        Me.lblFilesSelected.TabIndex = 16
        Me.lblFilesSelected.Text = "Files Selected"
        '
        'grd
        '
        Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.BoundMode = Janus.Windows.GridEX.BoundMode.Unbound
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(12, 94)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(606, 375)
        Me.grd.TabIndex = 17
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnNext
        '
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Location = New System.Drawing.Point(382, 475)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(61, 27)
        Me.btnNext.TabIndex = 15
        Me.btnNext.Text = "Next >>"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'lblNoOfFiles
        '
        Me.lblNoOfFiles.AutoSize = True
        Me.lblNoOfFiles.Location = New System.Drawing.Point(343, 66)
        Me.lblNoOfFiles.Name = "lblNoOfFiles"
        Me.lblNoOfFiles.Size = New System.Drawing.Size(13, 13)
        Me.lblNoOfFiles.TabIndex = 16
        Me.lblNoOfFiles.Text = "0"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmRevenueDataImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(630, 510)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.lblNoOfFiles)
        Me.Controls.Add(Me.lblFilesSelected)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnImportdata)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "frmRevenueDataImport"
        Me.Text = "frmRevenueDataImport"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnImportdata As System.Windows.Forms.Button
    Friend WithEvents lblFilesSelected As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblNoOfFiles As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
