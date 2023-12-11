<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmlatetimeSlot
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmlatetimeSlot))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSlotStart = New System.Windows.Forms.TextBox()
        Me.txtSlotEnd = New System.Windows.Forms.TextBox()
        Me.ChkActive = New System.Windows.Forms.CheckBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.grdLateTimeSlot = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdLateTimeSlot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 115)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = " Start Time (Mins)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 154)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "End Time (Mins)"
        '
        'txtSlotStart
        '
        Me.txtSlotStart.Location = New System.Drawing.Point(159, 109)
        Me.txtSlotStart.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSlotStart.Name = "txtSlotStart"
        Me.txtSlotStart.Size = New System.Drawing.Size(170, 26)
        Me.txtSlotStart.TabIndex = 3
        '
        'txtSlotEnd
        '
        Me.txtSlotEnd.Location = New System.Drawing.Point(159, 149)
        Me.txtSlotEnd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSlotEnd.Name = "txtSlotEnd"
        Me.txtSlotEnd.Size = New System.Drawing.Size(170, 26)
        Me.txtSlotEnd.TabIndex = 5
        '
        'ChkActive
        '
        Me.ChkActive.AutoSize = True
        Me.ChkActive.Location = New System.Drawing.Point(162, 189)
        Me.ChkActive.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ChkActive.Name = "ChkActive"
        Me.ChkActive.Size = New System.Drawing.Size(78, 24)
        Me.ChkActive.TabIndex = 6
        Me.ChkActive.Text = "Active"
        Me.ChkActive.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(525, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 29)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(77, 29)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'grdLateTimeSlot
        '
        Me.grdLateTimeSlot.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdLateTimeSlot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLateTimeSlot.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLateTimeSlot.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLateTimeSlot.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLateTimeSlot.Location = New System.Drawing.Point(0, 223)
        Me.grdLateTimeSlot.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdLateTimeSlot.Name = "grdLateTimeSlot"
        Me.grdLateTimeSlot.RecordNavigator = True
        Me.grdLateTimeSlot.Size = New System.Drawing.Size(525, 285)
        Me.grdLateTimeSlot.TabIndex = 7
        Me.grdLateTimeSlot.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(241, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Late Time Slot"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(120, 258)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(288, 75)
        Me.lblProgress.TabIndex = 8
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 43)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(525, 57)
        Me.pnlHeader.TabIndex = 9
        '
        'frmlatetimeSlot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 508)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.grdLateTimeSlot)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ChkActive)
        Me.Controls.Add(Me.txtSlotEnd)
        Me.Controls.Add(Me.txtSlotStart)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmlatetimeSlot"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Late Time Slot"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdLateTimeSlot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSlotStart As System.Windows.Forms.TextBox
    Friend WithEvents txtSlotEnd As System.Windows.Forms.TextBox
    Friend WithEvents ChkActive As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdLateTimeSlot As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
