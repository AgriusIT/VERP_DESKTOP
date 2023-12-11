<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptPostDatedCheques
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptPostDatedCheques))
        Me.BtnGenerate = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbTransaction = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnGenerate
        '
        Me.BtnGenerate.Location = New System.Drawing.Point(375, 152)
        Me.BtnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnGenerate.Name = "BtnGenerate"
        Me.BtnGenerate.Size = New System.Drawing.Size(112, 35)
        Me.BtnGenerate.TabIndex = 0
        Me.BtnGenerate.Text = "Generate"
        Me.ToolTip1.SetToolTip(Me.BtnGenerate, "Generate Report")
        Me.BtnGenerate.UseVisualStyleBackColor = True
        '
        'cmbTransaction
        '
        Me.cmbTransaction.FormattingEnabled = True
        Me.cmbTransaction.Items.AddRange(New Object() {"Received Cheques", "Issued Cheques", "Both"})
        Me.cmbTransaction.Location = New System.Drawing.Point(142, 97)
        Me.cmbTransaction.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTransaction.Name = "cmbTransaction"
        Me.cmbTransaction.Size = New System.Drawing.Size(343, 28)
        Me.cmbTransaction.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbTransaction, "Select Any Account")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 102)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Transaction"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(550, 77)
        Me.pnlHeader.TabIndex = 66
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(16, 22)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(344, 45)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Post Dated Cheque"
        '
        'frmRptPostDatedCheques
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(550, 203)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbTransaction)
        Me.Controls.Add(Me.BtnGenerate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptPostDatedCheques"
        Me.Text = "Post Dated Cheque"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnGenerate As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbTransaction As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class
