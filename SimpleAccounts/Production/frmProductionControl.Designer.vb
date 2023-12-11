<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductionControl))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbTicketNo = New System.Windows.Forms.ComboBox()
        Me.cmbPlanNo = New System.Windows.Forms.ComboBox()
        Me.cmbSalesOrder = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdMaterial = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GridEX3 = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GridEX4 = New Janus.Windows.GridEX.GridEX()
        Me.Panel2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.GridEX3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        CType(Me.GridEX4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 459)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(574, 68)
        Me.Panel2.TabIndex = 24
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(435, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 62)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(508, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(54, 62)
        Me.btnSave.TabIndex = 1
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(574, 39)
        Me.pnlHeader.TabIndex = 23
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(186, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Production Control"
        '
        'cmbTicketNo
        '
        Me.cmbTicketNo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTicketNo.FormattingEnabled = True
        Me.cmbTicketNo.Location = New System.Drawing.Point(383, 78)
        Me.cmbTicketNo.Name = "cmbTicketNo"
        Me.cmbTicketNo.Size = New System.Drawing.Size(179, 23)
        Me.cmbTicketNo.TabIndex = 30
        '
        'cmbPlanNo
        '
        Me.cmbPlanNo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPlanNo.FormattingEnabled = True
        Me.cmbPlanNo.Location = New System.Drawing.Point(198, 78)
        Me.cmbPlanNo.Name = "cmbPlanNo"
        Me.cmbPlanNo.Size = New System.Drawing.Size(179, 23)
        Me.cmbPlanNo.TabIndex = 29
        '
        'cmbSalesOrder
        '
        Me.cmbSalesOrder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesOrder.FormattingEnabled = True
        Me.cmbSalesOrder.Location = New System.Drawing.Point(13, 78)
        Me.cmbSalesOrder.Name = "cmbSalesOrder"
        Me.cmbSalesOrder.Size = New System.Drawing.Size(179, 23)
        Me.cmbSalesOrder.TabIndex = 28
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(379, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 15)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Ticket No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(194, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 15)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Plan No"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 15)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Sales Order"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl1.Location = New System.Drawing.Point(13, 107)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(549, 340)
        Me.UltraTabControl1.TabIndex = 31
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Production Progress"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Material"
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "Expenses"
        UltraTab4.TabPage = Me.UltraTabPageControl4
        UltraTab4.Text = "Warrenty Claim"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3, UltraTab4})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(547, 317)
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.GridEX1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 22)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(547, 317)
        '
        'GridEX1
        '
        Me.GridEX1.Location = New System.Drawing.Point(-1, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(545, 314)
        Me.GridEX1.TabIndex = 0
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdMaterial)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1074, 453)
        '
        'grdMaterial
        '
        Me.grdMaterial.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMaterial.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdMaterial.Location = New System.Drawing.Point(-1, -1)
        Me.grdMaterial.Name = "grdMaterial"
        Me.grdMaterial.Size = New System.Drawing.Size(1076, 454)
        Me.grdMaterial.TabIndex = 1
        Me.grdMaterial.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.GridEX3)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(1074, 453)
        '
        'GridEX3
        '
        Me.GridEX3.Location = New System.Drawing.Point(-1, -1)
        Me.GridEX3.Name = "GridEX3"
        Me.GridEX3.Size = New System.Drawing.Size(1076, 454)
        Me.GridEX3.TabIndex = 1
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.GridEX4)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(1074, 453)
        '
        'GridEX4
        '
        Me.GridEX4.Location = New System.Drawing.Point(-1, -1)
        Me.GridEX4.Name = "GridEX4"
        Me.GridEX4.Size = New System.Drawing.Size(1076, 454)
        Me.GridEX4.TabIndex = 1
        '
        'frmProductionControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 527)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.cmbTicketNo)
        Me.Controls.Add(Me.cmbPlanNo)
        Me.Controls.Add(Me.cmbSalesOrder)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProductionControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Production Control"
        Me.Panel2.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.GridEX3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        CType(Me.GridEX4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents cmbTicketNo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPlanNo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSalesOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdMaterial As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GridEX3 As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GridEX4 As Janus.Windows.GridEX.GridEX
End Class
