<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchWalkInCustomer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchWalkInCustomer))
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNo = New System.Windows.Forms.TextBox()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(11, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(239, 24)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Search WalkIn Customer"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(90, 15)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(415, 20)
        Me.txtSearch.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Type a word to be searched")
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Location = New System.Drawing.Point(1, 107)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(600, 370)
        Me.grd.TabIndex = 7
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(11, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Search Here :"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(551, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(38, 30)
        Me.btnClose.TabIndex = 119
        Me.btnClose.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(1, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(601, 53)
        Me.pnlHeader.TabIndex = 8
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtNo)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Location = New System.Drawing.Point(1, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(600, 55)
        Me.Panel2.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(510, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 15)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "No."
        '
        'txtNo
        '
        Me.txtNo.Location = New System.Drawing.Point(539, 15)
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(50, 20)
        Me.txtNo.TabIndex = 31
        '
        'frmSearchWalkInCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 479)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmSearchWalkInCustomer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Customers And Vendors"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
End Class
