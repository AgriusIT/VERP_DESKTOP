<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProSalarySheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProSalarySheet))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Ttip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.dtpTransactionDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpMonth = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(261, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 15)
        Me.Label2.TabIndex = 102
        Me.Label2.Text = "Voucher Number"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(774, 46)
        Me.pnlHeader.TabIndex = 95
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(256, 1)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 3
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(121, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Salary Sheet"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(9, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 15)
        Me.Label3.TabIndex = 97
        Me.Label3.Text = "Transaction Date"
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
        Me.btnCancel.Location = New System.Drawing.Point(635, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 62)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 250)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(774, 68)
        Me.Panel2.TabIndex = 96
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
        Me.btnSave.Location = New System.Drawing.Point(708, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(54, 62)
        Me.btnSave.TabIndex = 0
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(387, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 15)
        Me.Label8.TabIndex = 100
        Me.Label8.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtRemarks.Location = New System.Drawing.Point(390, 79)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(372, 23)
        Me.txtRemarks.TabIndex = 99
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtVoucherNo.Location = New System.Drawing.Point(264, 79)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.ReadOnly = True
        Me.txtVoucherNo.Size = New System.Drawing.Size(120, 23)
        Me.txtVoucherNo.TabIndex = 98
        '
        'dtpTransactionDate
        '
        Me.dtpTransactionDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTransactionDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTransactionDate.Location = New System.Drawing.Point(12, 79)
        Me.dtpTransactionDate.Name = "dtpTransactionDate"
        Me.dtpTransactionDate.Size = New System.Drawing.Size(120, 23)
        Me.dtpTransactionDate.TabIndex = 104
        '
        'dtpMonth
        '
        Me.dtpMonth.CustomFormat = "MMM/yyyy"
        Me.dtpMonth.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.Location = New System.Drawing.Point(138, 79)
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.Size = New System.Drawing.Size(120, 23)
        Me.dtpMonth.TabIndex = 106
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(135, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 15)
        Me.Label4.TabIndex = 105
        Me.Label4.Text = "Month"
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnAutoResize = True
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(12, 108)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(750, 139)
        Me.grd.TabIndex = 107
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmProSalarySheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 318)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.dtpMonth)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpTransactionDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.txtVoucherNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmProSalarySheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Salary Sheet"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Ttip As System.Windows.Forms.ToolTip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpTransactionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpMonth As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblProgress As System.Windows.Forms.Label
End Class
