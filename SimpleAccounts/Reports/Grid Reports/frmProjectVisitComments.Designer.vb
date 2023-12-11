<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectVisitComments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProjectVisitComments))
        Me.gboxDirector = New System.Windows.Forms.GroupBox
        Me.txtDirector = New System.Windows.Forms.TextBox
        Me.gboxGM = New System.Windows.Forms.GroupBox
        Me.txtGM = New System.Windows.Forms.TextBox
        Me.gboxASM = New System.Windows.Forms.GroupBox
        Me.txtASM = New System.Windows.Forms.TextBox
        Me.gboxManager = New System.Windows.Forms.GroupBox
        Me.txtManager = New System.Windows.Forms.TextBox
        Me.gboxRP = New System.Windows.Forms.GroupBox
        Me.txtRP = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtOthers = New System.Windows.Forms.TextBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.gboxDirector.SuspendLayout()
        Me.gboxGM.SuspendLayout()
        Me.gboxASM.SuspendLayout()
        Me.gboxManager.SuspendLayout()
        Me.gboxRP.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gboxDirector
        '
        Me.gboxDirector.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxDirector.Controls.Add(Me.txtDirector)
        Me.gboxDirector.Location = New System.Drawing.Point(12, 26)
        Me.gboxDirector.Name = "gboxDirector"
        Me.gboxDirector.Size = New System.Drawing.Size(628, 95)
        Me.gboxDirector.TabIndex = 1
        Me.gboxDirector.TabStop = False
        Me.gboxDirector.Text = "Director"
        '
        'txtDirector
        '
        Me.txtDirector.BackColor = System.Drawing.SystemColors.Window
        Me.txtDirector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDirector.Location = New System.Drawing.Point(3, 16)
        Me.txtDirector.Multiline = True
        Me.txtDirector.Name = "txtDirector"
        Me.txtDirector.ReadOnly = True
        Me.txtDirector.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDirector.Size = New System.Drawing.Size(622, 76)
        Me.txtDirector.TabIndex = 0
        '
        'gboxGM
        '
        Me.gboxGM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxGM.Controls.Add(Me.txtGM)
        Me.gboxGM.Location = New System.Drawing.Point(12, 113)
        Me.gboxGM.Name = "gboxGM"
        Me.gboxGM.Size = New System.Drawing.Size(628, 95)
        Me.gboxGM.TabIndex = 2
        Me.gboxGM.TabStop = False
        Me.gboxGM.Text = "GM"
        '
        'txtGM
        '
        Me.txtGM.BackColor = System.Drawing.SystemColors.Window
        Me.txtGM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGM.Location = New System.Drawing.Point(3, 16)
        Me.txtGM.Multiline = True
        Me.txtGM.Name = "txtGM"
        Me.txtGM.ReadOnly = True
        Me.txtGM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGM.Size = New System.Drawing.Size(622, 76)
        Me.txtGM.TabIndex = 1
        '
        'gboxASM
        '
        Me.gboxASM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxASM.Controls.Add(Me.txtASM)
        Me.gboxASM.Location = New System.Drawing.Point(12, 182)
        Me.gboxASM.Name = "gboxASM"
        Me.gboxASM.Size = New System.Drawing.Size(628, 95)
        Me.gboxASM.TabIndex = 3
        Me.gboxASM.TabStop = False
        Me.gboxASM.Text = "ASM"
        '
        'txtASM
        '
        Me.txtASM.BackColor = System.Drawing.SystemColors.Window
        Me.txtASM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtASM.Location = New System.Drawing.Point(3, 16)
        Me.txtASM.Multiline = True
        Me.txtASM.Name = "txtASM"
        Me.txtASM.ReadOnly = True
        Me.txtASM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtASM.Size = New System.Drawing.Size(622, 76)
        Me.txtASM.TabIndex = 2
        '
        'gboxManager
        '
        Me.gboxManager.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxManager.Controls.Add(Me.txtManager)
        Me.gboxManager.Location = New System.Drawing.Point(12, 267)
        Me.gboxManager.Name = "gboxManager"
        Me.gboxManager.Size = New System.Drawing.Size(628, 95)
        Me.gboxManager.TabIndex = 2
        Me.gboxManager.TabStop = False
        Me.gboxManager.Text = "Manager"
        '
        'txtManager
        '
        Me.txtManager.BackColor = System.Drawing.SystemColors.Window
        Me.txtManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtManager.Location = New System.Drawing.Point(3, 16)
        Me.txtManager.Multiline = True
        Me.txtManager.Name = "txtManager"
        Me.txtManager.ReadOnly = True
        Me.txtManager.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtManager.Size = New System.Drawing.Size(622, 76)
        Me.txtManager.TabIndex = 1
        '
        'gboxRP
        '
        Me.gboxRP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxRP.Controls.Add(Me.txtRP)
        Me.gboxRP.Location = New System.Drawing.Point(12, 352)
        Me.gboxRP.Name = "gboxRP"
        Me.gboxRP.Size = New System.Drawing.Size(628, 95)
        Me.gboxRP.TabIndex = 2
        Me.gboxRP.TabStop = False
        Me.gboxRP.Text = "RP"
        '
        'txtRP
        '
        Me.txtRP.BackColor = System.Drawing.SystemColors.Window
        Me.txtRP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRP.Location = New System.Drawing.Point(3, 16)
        Me.txtRP.Multiline = True
        Me.txtRP.Name = "txtRP"
        Me.txtRP.ReadOnly = True
        Me.txtRP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRP.Size = New System.Drawing.Size(622, 76)
        Me.txtRP.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtOthers)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 444)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(628, 95)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Others"
        '
        'txtOthers
        '
        Me.txtOthers.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOthers.Location = New System.Drawing.Point(3, 16)
        Me.txtOthers.Multiline = True
        Me.txtOthers.Name = "txtOthers"
        Me.txtOthers.ReadOnly = True
        Me.txtOthers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOthers.Size = New System.Drawing.Size(622, 76)
        Me.txtOthers.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPrint, Me.btnCancel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(652, 25)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 22)
        Me.btnCancel.Text = "&Cancel"
        '
        'frmProjectVisitComments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 550)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gboxRP)
        Me.Controls.Add(Me.gboxManager)
        Me.Controls.Add(Me.gboxASM)
        Me.Controls.Add(Me.gboxGM)
        Me.Controls.Add(Me.gboxDirector)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmProjectVisitComments"
        Me.Text = "Project Visit Comments"
        Me.gboxDirector.ResumeLayout(False)
        Me.gboxDirector.PerformLayout()
        Me.gboxGM.ResumeLayout(False)
        Me.gboxGM.PerformLayout()
        Me.gboxASM.ResumeLayout(False)
        Me.gboxASM.PerformLayout()
        Me.gboxManager.ResumeLayout(False)
        Me.gboxManager.PerformLayout()
        Me.gboxRP.ResumeLayout(False)
        Me.gboxRP.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gboxDirector As System.Windows.Forms.GroupBox
    Friend WithEvents gboxGM As System.Windows.Forms.GroupBox
    Friend WithEvents gboxASM As System.Windows.Forms.GroupBox
    Friend WithEvents gboxManager As System.Windows.Forms.GroupBox
    Friend WithEvents gboxRP As System.Windows.Forms.GroupBox
    Friend WithEvents txtDirector As System.Windows.Forms.TextBox
    Friend WithEvents txtGM As System.Windows.Forms.TextBox
    Friend WithEvents txtASM As System.Windows.Forms.TextBox
    Friend WithEvents txtManager As System.Windows.Forms.TextBox
    Friend WithEvents txtRP As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOthers As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
End Class
