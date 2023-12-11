<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockTransferItemsList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockTransferItemsList))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
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
        Me.pnlHeader.Size = New System.Drawing.Size(849, 44)
        Me.pnlHeader.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(803, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(38, 30)
        Me.btnClose.TabIndex = 1
        Me.btnClose.TabStop = False
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(11, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(283, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Stock Transfer Items List"
        '
        'grd
        '
        Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 76)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(849, 392)
        Me.grd.TabIndex = 11
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(755, 477)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(82, 33)
        Me.btnLoad.TabIndex = 12
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.ForeColor = System.Drawing.Color.White
        Me.lblItem.Location = New System.Drawing.Point(31, 53)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(31, 15)
        Me.lblItem.TabIndex = 13
        Me.lblItem.Text = "Item"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(77, 50)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(278, 20)
        Me.txtSearch.TabIndex = 14
        '
        'frmStockTransferItemsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(849, 519)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.lblItem)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.pnlHeader)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmStockTransferItemsList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock Transfer Items List"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
End Class
