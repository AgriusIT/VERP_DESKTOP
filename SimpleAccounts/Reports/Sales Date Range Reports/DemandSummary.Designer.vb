<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DemandSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DemandSummary))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.pnlInvType = New System.Windows.Forms.Panel()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
        Me.pnlInvType.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 15)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(122, 11)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(403, 28)
        Me.cmbPeriod.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(304, 62)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(8, 62)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(46, 20)
        Me.lblFrom.TabIndex = 20
        Me.lblFrom.Text = "From"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(351, 52)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(174, 26)
        Me.DateTimePicker2.TabIndex = 23
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(122, 52)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(172, 26)
        Me.DateTimePicker1.TabIndex = 21
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.btnPrint, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(314, 258)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(244, 45)
        Me.TableLayoutPanel1.TabIndex = 27
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPrint.Location = New System.Drawing.Point(120, 5)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(118, 35)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "Print"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 5)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(106, 35)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Show"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(576, 65)
        Me.pnlHeader.TabIndex = 112
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(282, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Demand Summary "
        '
        'pnlPeriod
        '
        Me.pnlPeriod.BackColor = System.Drawing.Color.Transparent
        Me.pnlPeriod.Controls.Add(Me.cmbPeriod)
        Me.pnlPeriod.Controls.Add(Me.Label3)
        Me.pnlPeriod.Controls.Add(Me.lblFrom)
        Me.pnlPeriod.Controls.Add(Me.DateTimePicker1)
        Me.pnlPeriod.Controls.Add(Me.Label2)
        Me.pnlPeriod.Controls.Add(Me.DateTimePicker2)
        Me.pnlPeriod.Location = New System.Drawing.Point(18, 143)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(540, 106)
        Me.pnlPeriod.TabIndex = 31
        '
        'pnlInvType
        '
        Me.pnlInvType.BackColor = System.Drawing.Color.Transparent
        Me.pnlInvType.Controls.Add(Me.lblCompany)
        Me.pnlInvType.Controls.Add(Me.cmbCompany)
        Me.pnlInvType.Location = New System.Drawing.Point(18, 74)
        Me.pnlInvType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlInvType.Name = "pnlInvType"
        Me.pnlInvType.Size = New System.Drawing.Size(540, 60)
        Me.pnlInvType.TabIndex = 113
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Location = New System.Drawing.Point(4, 18)
        Me.lblCompany.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(68, 20)
        Me.lblCompany.TabIndex = 0
        Me.lblCompany.Text = "Inv Type"
        '
        'cmbCompany
        '
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(122, 12)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(403, 28)
        Me.cmbCompany.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(504, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 116
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'DemandSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(576, 328)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.pnlInvType)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.pnlPeriod)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "DemandSummary"
        Me.Text = "Demand Summary"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.pnlInvType.ResumeLayout(False)
        Me.pnlInvType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents pnlInvType As System.Windows.Forms.Panel
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
