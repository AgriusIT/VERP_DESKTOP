<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTicketBookingList
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
        Dim grdTicketBooking_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTicketBookingList))
        Me.grdTicketBooking = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.btnAddDock = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        CType(Me.grdTicketBooking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdTicketBooking
        '
        Me.grdTicketBooking.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdTicketBooking_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdTicketBooking.DesignTimeLayout = grdTicketBooking_DesignTimeLayout
        Me.grdTicketBooking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTicketBooking.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdTicketBooking.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdTicketBooking.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdTicketBooking.GroupByBoxVisible = False
        Me.grdTicketBooking.Location = New System.Drawing.Point(0, 46)
        Me.grdTicketBooking.Name = "grdTicketBooking"
        Me.grdTicketBooking.RecordNavigator = True
        Me.grdTicketBooking.Size = New System.Drawing.Size(1008, 683)
        Me.grdTicketBooking.TabIndex = 40
        Me.grdTicketBooking.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTicketBooking.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdTicketBooking.TreeLineColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.grdTicketBooking.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnRefresh)
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Controls.Add(Me.btnAddDock)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1008, 46)
        Me.pnlHeader.TabIndex = 39
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(967, 10)
        Me.CtrlGrdBar1.MyGrid = Me.grdTicketBooking
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 18
        Me.CtrlGrdBar1.TabStop = False
        '
        'btnAddDock
        '
        Me.btnAddDock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddDock.BackColor = System.Drawing.Color.Transparent
        Me.btnAddDock.BackgroundImage = CType(resources.GetObject("btnAddDock.BackgroundImage"), System.Drawing.Image)
        Me.btnAddDock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddDock.FlatAppearance.BorderSize = 0
        Me.btnAddDock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddDock.Location = New System.Drawing.Point(905, 15)
        Me.btnAddDock.Name = "btnAddDock"
        Me.btnAddDock.Size = New System.Drawing.Size(20, 20)
        Me.btnAddDock.TabIndex = 17
        Me.btnAddDock.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(161, 30)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Ticket Booking"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(931, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(30, 26)
        Me.btnRefresh.TabIndex = 19
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'frmTicketBookingList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.grdTicketBooking)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "frmTicketBookingList"
        Me.Text = "Ticket Booking"
        CType(Me.grdTicketBooking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdTicketBooking As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnAddDock As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
End Class
