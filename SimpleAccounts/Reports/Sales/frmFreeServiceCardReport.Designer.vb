<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFreeServiceCardReport
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFreeServiceCardReport))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnReport = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dtpInquiryToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpInquiryFromDate = New System.Windows.Forms.DateTimePicker()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnReport})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(591, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnReport
        '
        Me.btnReport.Image = Global.SimpleAccounts.My.Resources.Resources._1495105162_Document
        Me.btnReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(52, 22)
        Me.btnReport.Text = "&Print"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Controls.Add(Me.lblToDate)
        Me.Panel1.Controls.Add(Me.lblFromDate)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.dtpInquiryToDate)
        Me.Panel1.Controls.Add(Me.dtpInquiryFromDate)
        Me.Panel1.Location = New System.Drawing.Point(2, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(585, 68)
        Me.Panel1.TabIndex = 2
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(454, 30)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(38, 23)
        Me.btnPrint.TabIndex = 21
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblToDate.Location = New System.Drawing.Point(208, 34)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 13)
        Me.lblToDate.TabIndex = 20
        Me.lblToDate.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblFromDate.Location = New System.Drawing.Point(9, 34)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(56, 13)
        Me.lblFromDate.TabIndex = 19
        Me.lblFromDate.Text = "From Date"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(396, 30)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(55, 23)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dtpInquiryToDate
        '
        Me.dtpInquiryToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dtpInquiryToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpInquiryToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInquiryToDate.Location = New System.Drawing.Point(260, 32)
        Me.dtpInquiryToDate.Name = "dtpInquiryToDate"
        Me.dtpInquiryToDate.ShowCheckBox = True
        Me.dtpInquiryToDate.Size = New System.Drawing.Size(131, 20)
        Me.dtpInquiryToDate.TabIndex = 15
        '
        'dtpInquiryFromDate
        '
        Me.dtpInquiryFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dtpInquiryFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpInquiryFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInquiryFromDate.Location = New System.Drawing.Point(71, 32)
        Me.dtpInquiryFromDate.Name = "dtpInquiryFromDate"
        Me.dtpInquiryFromDate.ShowCheckBox = True
        Me.dtpInquiryFromDate.Size = New System.Drawing.Size(131, 20)
        Me.dtpInquiryFromDate.TabIndex = 15
        '
        'GridEX1
        '
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(0, 154)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndMoveToFirstCellInNewRow
        Me.GridEX1.Size = New System.Drawing.Size(587, 261)
        Me.GridEX1.TabIndex = 4
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(591, 42)
        Me.pnlHeader.TabIndex = 12
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(3, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(199, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Free Service Card"
        '
        'frmFreeServiceCardReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 418)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmFreeServiceCardReport"
        Me.Text = "Free Service Card Report"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dtpInquiryToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpInquiryFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class
