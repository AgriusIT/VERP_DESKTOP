<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerCategory
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
        Dim DataGridView1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerCategory))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCategoryName = New System.Windows.Forms.TextBox()
        Me.richTxtRemarks = New System.Windows.Forms.RichTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.uichkActive = New System.Windows.Forms.CheckBox()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New Janus.Windows.GridEX.GridEX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(29, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Category Name"
        '
        'txtCategoryName
        '
        Me.txtCategoryName.Location = New System.Drawing.Point(32, 90)
        Me.txtCategoryName.Name = "txtCategoryName"
        Me.txtCategoryName.Size = New System.Drawing.Size(290, 20)
        Me.txtCategoryName.TabIndex = 0
        '
        'richTxtRemarks
        '
        Me.richTxtRemarks.Location = New System.Drawing.Point(32, 131)
        Me.richTxtRemarks.Name = "richTxtRemarks"
        Me.richTxtRemarks.Size = New System.Drawing.Size(290, 73)
        Me.richTxtRemarks.TabIndex = 1
        Me.richTxtRemarks.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(29, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Remarks"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(89, 214)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Sort Order"
        '
        'uichkActive
        '
        Me.uichkActive.AutoSize = True
        Me.uichkActive.Checked = True
        Me.uichkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.uichkActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.uichkActive.Location = New System.Drawing.Point(266, 213)
        Me.uichkActive.Name = "uichkActive"
        Me.uichkActive.Size = New System.Drawing.Size(56, 17)
        Me.uichkActive.TabIndex = 3
        Me.uichkActive.Text = "Active"
        Me.uichkActive.UseVisualStyleBackColor = True
        '
        'txtSortOrder
        '
        Me.txtSortOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSortOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.txtSortOrder.Location = New System.Drawing.Point(150, 211)
        Me.txtSortOrder.MaxLength = 50
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(110, 20)
        Me.txtSortOrder.TabIndex = 2
        Me.txtSortOrder.Text = "1"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnAutoResize = True
        DataGridView1_DesignTimeLayout.LayoutString = resources.GetString("DataGridView1_DesignTimeLayout.LayoutString")
        Me.DataGridView1.DesignTimeLayout = DataGridView1_DesignTimeLayout
        Me.DataGridView1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.DataGridView1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.DataGridView1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.DataGridView1.GroupByBoxVisible = False
        Me.DataGridView1.Location = New System.Drawing.Point(13, 236)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RecordNavigator = True
        Me.DataGridView1.Size = New System.Drawing.Size(701, 244)
        Me.DataGridView1.TabIndex = 4
        Me.DataGridView1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 495)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(726, 68)
        Me.Panel1.TabIndex = 5
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
        Me.btnCancel.Location = New System.Drawing.Point(587, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 62)
        Me.btnCancel.TabIndex = 1
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
        Me.btnSave.Location = New System.Drawing.Point(660, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(54, 62)
        Me.btnSave.TabIndex = 0
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(726, 46)
        Me.pnlHeader.TabIndex = 118
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(185, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Customer Category"
        '
        'frmCustomerCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 563)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.richTxtRemarks)
        Me.Controls.Add(Me.txtSortOrder)
        Me.Controls.Add(Me.uichkActive)
        Me.Controls.Add(Me.txtCategoryName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmCustomerCategory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Category"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCategoryName As System.Windows.Forms.TextBox
    Friend WithEvents richTxtRemarks As System.Windows.Forms.RichTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents uichkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class
