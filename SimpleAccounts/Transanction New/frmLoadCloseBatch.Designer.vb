<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadCloseBatch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadCloseBatch))
        Dim grdItems_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnHeader = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbPlanNo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbTicketNo = New System.Windows.Forms.ComboBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdItems = New Janus.Windows.GridEX.GridEX()
        Me.cmbBatchNo = New System.Windows.Forms.ComboBox()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnHeader)
        Me.pnlHeader.Controls.Add(Me.Button8)
        Me.pnlHeader.Controls.Add(Me.Button6)
        Me.pnlHeader.Controls.Add(Me.BtnRefresh)
        Me.pnlHeader.Controls.Add(Me.Button4)
        Me.pnlHeader.Controls.Add(Me.Button7)
        Me.pnlHeader.Controls.Add(Me.Button3)
        Me.pnlHeader.Controls.Add(Me.Button2)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(802, 42)
        Me.pnlHeader.TabIndex = 19
        '
        'btnHeader
        '
        Me.btnHeader.BackgroundImage = CType(resources.GetObject("btnHeader.BackgroundImage"), System.Drawing.Image)
        Me.btnHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnHeader.FlatAppearance.BorderSize = 0
        Me.btnHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHeader.Location = New System.Drawing.Point(3, 10)
        Me.btnHeader.Name = "btnHeader"
        Me.btnHeader.Size = New System.Drawing.Size(21, 23)
        Me.btnHeader.TabIndex = 0
        Me.btnHeader.UseVisualStyleBackColor = True
        Me.btnHeader.Visible = False
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackgroundImage = CType(resources.GetObject("Button8.BackgroundImage"), System.Drawing.Image)
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button8.FlatAppearance.BorderSize = 0
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Location = New System.Drawing.Point(649, 9)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(25, 23)
        Me.Button8.TabIndex = 3
        Me.Button8.UseVisualStyleBackColor = True
        Me.Button8.Visible = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), System.Drawing.Image)
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Location = New System.Drawing.Point(759, 6)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(27, 30)
        Me.Button6.TabIndex = 8
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRefresh.BackgroundImage = CType(resources.GetObject("BtnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.BtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnRefresh.FlatAppearance.BorderSize = 0
        Me.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRefresh.Location = New System.Drawing.Point(734, 8)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(30, 26)
        Me.BtnRefresh.TabIndex = 7
        Me.BtnRefresh.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(713, 9)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(28, 24)
        Me.Button4.TabIndex = 6
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"), System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.ForeColor = System.Drawing.SystemColors.Control
        Me.Button7.Location = New System.Drawing.Point(628, 10)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(20, 22)
        Me.Button7.TabIndex = 2
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(692, 9)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(25, 23)
        Me.Button3.TabIndex = 5
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(672, 9)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(29, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(194, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Load Close Batch"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(0, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Plan No"
        '
        'cmbPlanNo
        '
        Me.cmbPlanNo.FormattingEnabled = True
        Me.cmbPlanNo.Location = New System.Drawing.Point(50, 48)
        Me.cmbPlanNo.Name = "cmbPlanNo"
        Me.cmbPlanNo.Size = New System.Drawing.Size(163, 21)
        Me.cmbPlanNo.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(217, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Ticket No"
        '
        'cmbTicketNo
        '
        Me.cmbTicketNo.FormattingEnabled = True
        Me.cmbTicketNo.Location = New System.Drawing.Point(277, 48)
        Me.cmbTicketNo.Name = "cmbTicketNo"
        Me.cmbTicketNo.Size = New System.Drawing.Size(163, 21)
        Me.cmbTicketNo.TabIndex = 28
        '
        'btnLoad
        '
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.Location = New System.Drawing.Point(681, 46)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(72, 25)
        Me.btnLoad.TabIndex = 30
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(448, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Batch No"
        '
        'grdItems
        '
        Me.grdItems.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdItems_DesignTimeLayout.LayoutString = resources.GetString("grdItems_DesignTimeLayout.LayoutString")
        Me.grdItems.DesignTimeLayout = grdItems_DesignTimeLayout
        Me.grdItems.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdItems.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdItems.GroupByBoxVisible = False
        Me.grdItems.Location = New System.Drawing.Point(1, 77)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndMoveToFirstCellInNewRow
        Me.grdItems.RecordNavigator = True
        Me.grdItems.Size = New System.Drawing.Size(801, 300)
        Me.grdItems.TabIndex = 33
        Me.grdItems.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmbBatchNo
        '
        Me.cmbBatchNo.FormattingEnabled = True
        Me.cmbBatchNo.Location = New System.Drawing.Point(506, 48)
        Me.cmbBatchNo.Name = "cmbBatchNo"
        Me.cmbBatchNo.Size = New System.Drawing.Size(163, 21)
        Me.cmbBatchNo.TabIndex = 34
        '
        'frmLoadCloseBatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 377)
        Me.Controls.Add(Me.cmbBatchNo)
        Me.Controls.Add(Me.grdItems)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbTicketNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbPlanNo)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "frmLoadCloseBatch"
        Me.Text = "Load Close Batch"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnHeader As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbPlanNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTicketNo As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdItems As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbBatchNo As System.Windows.Forms.ComboBox
End Class
