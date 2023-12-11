<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAB
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.lblAgent = New System.Windows.Forms.Label()
        Me.txtAgent = New System.Windows.Forms.TextBox()
        Me.btnAgent = New System.Windows.Forms.Button()
        Me.btnBranch = New System.Windows.Forms.Button()
        Me.txtBranch = New System.Windows.Forms.TextBox()
        Me.lblBranch = New System.Windows.Forms.Label()
        Me.btnDealer = New System.Windows.Forms.Button()
        Me.txtDealer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEstate = New System.Windows.Forms.TextBox()
        Me.btnEstate = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(583, 25)
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
        'lblAgent
        '
        Me.lblAgent.AutoSize = True
        Me.lblAgent.Location = New System.Drawing.Point(16, 51)
        Me.lblAgent.Name = "lblAgent"
        Me.lblAgent.Size = New System.Drawing.Size(35, 13)
        Me.lblAgent.TabIndex = 1
        Me.lblAgent.Text = "Agent"
        '
        'txtAgent
        '
        Me.txtAgent.Location = New System.Drawing.Point(57, 48)
        Me.txtAgent.Name = "txtAgent"
        Me.txtAgent.Size = New System.Drawing.Size(248, 20)
        Me.txtAgent.TabIndex = 2
        '
        'btnAgent
        '
        Me.btnAgent.Location = New System.Drawing.Point(311, 46)
        Me.btnAgent.Name = "btnAgent"
        Me.btnAgent.Size = New System.Drawing.Size(91, 23)
        Me.btnAgent.TabIndex = 3
        Me.btnAgent.Text = "Show Agent"
        Me.btnAgent.UseVisualStyleBackColor = True
        '
        'btnBranch
        '
        Me.btnBranch.Location = New System.Drawing.Point(311, 72)
        Me.btnBranch.Name = "btnBranch"
        Me.btnBranch.Size = New System.Drawing.Size(91, 23)
        Me.btnBranch.TabIndex = 6
        Me.btnBranch.Text = "Show Branch"
        Me.btnBranch.UseVisualStyleBackColor = True
        '
        'txtBranch
        '
        Me.txtBranch.Location = New System.Drawing.Point(57, 74)
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.Size = New System.Drawing.Size(248, 20)
        Me.txtBranch.TabIndex = 5
        '
        'lblBranch
        '
        Me.lblBranch.AutoSize = True
        Me.lblBranch.Location = New System.Drawing.Point(12, 77)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(41, 13)
        Me.lblBranch.TabIndex = 4
        Me.lblBranch.Text = "Branch"
        '
        'btnDealer
        '
        Me.btnDealer.Location = New System.Drawing.Point(311, 99)
        Me.btnDealer.Name = "btnDealer"
        Me.btnDealer.Size = New System.Drawing.Size(91, 23)
        Me.btnDealer.TabIndex = 9
        Me.btnDealer.Text = "Show Dealer"
        Me.btnDealer.UseVisualStyleBackColor = True
        '
        'txtDealer
        '
        Me.txtDealer.Location = New System.Drawing.Point(57, 102)
        Me.txtDealer.Name = "txtDealer"
        Me.txtDealer.Size = New System.Drawing.Size(248, 20)
        Me.txtDealer.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Dealer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Estate"
        '
        'txtEstate
        '
        Me.txtEstate.Location = New System.Drawing.Point(57, 130)
        Me.txtEstate.Name = "txtEstate"
        Me.txtEstate.Size = New System.Drawing.Size(248, 20)
        Me.txtEstate.TabIndex = 11
        '
        'btnEstate
        '
        Me.btnEstate.Location = New System.Drawing.Point(311, 128)
        Me.btnEstate.Name = "btnEstate"
        Me.btnEstate.Size = New System.Drawing.Size(91, 23)
        Me.btnEstate.TabIndex = 12
        Me.btnEstate.Text = "Show Estate"
        Me.btnEstate.UseVisualStyleBackColor = True
        '
        'frmAB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 439)
        Me.Controls.Add(Me.btnEstate)
        Me.Controls.Add(Me.txtEstate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnDealer)
        Me.Controls.Add(Me.txtDealer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBranch)
        Me.Controls.Add(Me.txtBranch)
        Me.Controls.Add(Me.lblBranch)
        Me.Controls.Add(Me.btnAgent)
        Me.Controls.Add(Me.txtAgent)
        Me.Controls.Add(Me.lblAgent)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmAB"
        Me.Text = "AB"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblAgent As System.Windows.Forms.Label
    Friend WithEvents txtAgent As System.Windows.Forms.TextBox
    Friend WithEvents btnAgent As System.Windows.Forms.Button
    Friend WithEvents btnBranch As System.Windows.Forms.Button
    Friend WithEvents txtBranch As System.Windows.Forms.TextBox
    Friend WithEvents lblBranch As System.Windows.Forms.Label
    Friend WithEvents btnDealer As System.Windows.Forms.Button
    Friend WithEvents txtDealer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEstate As System.Windows.Forms.TextBox
    Friend WithEvents btnEstate As System.Windows.Forms.Button
End Class
