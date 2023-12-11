<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWalkInCustomerHistory
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
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWalkInCustomerHistory))
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.fromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.toDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.loadBtn = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.summaryRdo = New System.Windows.Forms.RadioButton()
        Me.detailRdo = New System.Windows.Forms.RadioButton()
        Me.CtrlGrdBarWalkInHistory = New SimpleAccounts.CtrlGrdBar()
        Me.txtNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.Location = New System.Drawing.Point(-1, 77)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(677, 355)
        Me.grd.TabIndex = 12
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnDelete)
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(675, 44)
        Me.pnlHeader.TabIndex = 13
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackgroundImage = CType(resources.GetObject("btnDelete.BackgroundImage"), System.Drawing.Image)
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(588, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(31, 36)
        Me.btnDelete.TabIndex = 11
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(625, 7)
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
        Me.lblHeader.Size = New System.Drawing.Size(291, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Walk-In Customer History"
        '
        'fromDate
        '
        Me.fromDate.Checked = False
        Me.fromDate.CustomFormat = "dd/MMM/yyyy"
        Me.fromDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 50)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.ShowCheckBox = True
        Me.fromDate.Size = New System.Drawing.Size(114, 23)
        Me.fromDate.TabIndex = 15
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(3, 54)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(35, 15)
        Me.lblDate.TabIndex = 14
        Me.lblDate.Text = "From"
        '
        'toDate
        '
        Me.toDate.Checked = False
        Me.toDate.CustomFormat = "dd/MMM/yyyy"
        Me.toDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.toDate.Location = New System.Drawing.Point(197, 50)
        Me.toDate.Name = "toDate"
        Me.toDate.ShowCheckBox = True
        Me.toDate.Size = New System.Drawing.Size(126, 23)
        Me.toDate.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(171, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "To"
        '
        'loadBtn
        '
        Me.loadBtn.Location = New System.Drawing.Point(559, 50)
        Me.loadBtn.Name = "loadBtn"
        Me.loadBtn.Size = New System.Drawing.Size(75, 23)
        Me.loadBtn.TabIndex = 18
        Me.loadBtn.Text = "Load"
        Me.loadBtn.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.summaryRdo)
        Me.Panel1.Controls.Add(Me.detailRdo)
        Me.Panel1.Location = New System.Drawing.Point(339, 50)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(131, 23)
        Me.Panel1.TabIndex = 19
        '
        'summaryRdo
        '
        Me.summaryRdo.AutoSize = True
        Me.summaryRdo.Location = New System.Drawing.Point(61, 3)
        Me.summaryRdo.Name = "summaryRdo"
        Me.summaryRdo.Size = New System.Drawing.Size(68, 17)
        Me.summaryRdo.TabIndex = 1
        Me.summaryRdo.Text = "Summary"
        Me.summaryRdo.UseVisualStyleBackColor = True
        '
        'detailRdo
        '
        Me.detailRdo.AutoSize = True
        Me.detailRdo.Checked = True
        Me.detailRdo.Location = New System.Drawing.Point(3, 4)
        Me.detailRdo.Name = "detailRdo"
        Me.detailRdo.Size = New System.Drawing.Size(52, 17)
        Me.detailRdo.TabIndex = 0
        Me.detailRdo.TabStop = True
        Me.detailRdo.Text = "Detail"
        Me.detailRdo.UseVisualStyleBackColor = True
        '
        'CtrlGrdBarWalkInHistory
        '
        Me.CtrlGrdBarWalkInHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarWalkInHistory.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarWalkInHistory.Email = Nothing
        Me.CtrlGrdBarWalkInHistory.FormName = Me
        Me.CtrlGrdBarWalkInHistory.Location = New System.Drawing.Point(640, 48)
        Me.CtrlGrdBarWalkInHistory.MyGrid = Me.grd
        Me.CtrlGrdBarWalkInHistory.Name = "CtrlGrdBarWalkInHistory"
        Me.CtrlGrdBarWalkInHistory.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBarWalkInHistory.TabIndex = 28
        '
        'txtNo
        '
        Me.txtNo.Location = New System.Drawing.Point(503, 52)
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(50, 20)
        Me.txtNo.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(474, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 15)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "No."
        '
        'frmWalkInCustomerHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(675, 432)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNo)
        Me.Controls.Add(Me.CtrlGrdBarWalkInHistory)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.loadBtn)
        Me.Controls.Add(Me.toDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.fromDate)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmWalkInCustomerHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmWalkInCustomerHistory"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents fromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents toDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents loadBtn As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents summaryRdo As System.Windows.Forms.RadioButton
    Friend WithEvents detailRdo As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlGrdBarWalkInHistory As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
End Class
