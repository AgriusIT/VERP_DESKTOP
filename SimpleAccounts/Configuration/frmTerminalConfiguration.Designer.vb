<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTerminalConfiguration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTerminalConfiguration))
        Dim grdComputers_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdUsers_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.btnAddSingleApplication = New System.Windows.Forms.ToolStripButton()
        Me.lblGroupName = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lstgroup = New System.Windows.Forms.ListBox()
        Me.PnlUsers = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdComputers = New Janus.Windows.GridEX.GridEX()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grdUsers = New Janus.Windows.GridEX.GridEX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMenu = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtMainManu = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnAddManu = New System.Windows.Forms.Button()
        Me.lstManus = New System.Windows.Forms.ListBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbApplicationsMultiple = New System.Windows.Forms.ComboBox()
        Me.lstApplications = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnAddApplication = New System.Windows.Forms.Button()
        Me.txtSubTitle = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbApplicationSingle = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbLayout = New System.Windows.Forms.ComboBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnhelp = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.PnlUsers.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grdComputers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMenu.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGroupName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstgroup)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PnlUsers)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlMenu)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(918, 663)
        Me.SplitContainer1.SplitterDistance = 176
        Me.SplitContainer1.TabIndex = 1
        '
        'ToolStrip3
        '
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAddSingleApplication})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(176, 25)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'btnAddSingleApplication
        '
        Me.btnAddSingleApplication.Image = CType(resources.GetObject("btnAddSingleApplication.Image"), System.Drawing.Image)
        Me.btnAddSingleApplication.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddSingleApplication.Name = "btnAddSingleApplication"
        Me.btnAddSingleApplication.Size = New System.Drawing.Size(127, 29)
        Me.btnAddSingleApplication.Text = "Add Single"
        Me.btnAddSingleApplication.Visible = False
        '
        'lblGroupName
        '
        Me.lblGroupName.AutoSize = True
        Me.lblGroupName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroupName.ForeColor = System.Drawing.Color.Black
        Me.lblGroupName.Location = New System.Drawing.Point(3, 37)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Size = New System.Drawing.Size(62, 20)
        Me.lblGroupName.TabIndex = 1
        Me.lblGroupName.Text = "Search"
        Me.lblGroupName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(4, 53)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(169, 26)
        Me.txtSearch.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Select Any Group")
        '
        'lstgroup
        '
        Me.lstgroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstgroup.FormattingEnabled = True
        Me.lstgroup.ItemHeight = 20
        Me.lstgroup.Location = New System.Drawing.Point(4, 79)
        Me.lstgroup.Name = "lstgroup"
        Me.lstgroup.Size = New System.Drawing.Size(169, 564)
        Me.lstgroup.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.lstgroup, "User Groups")
        '
        'PnlUsers
        '
        Me.PnlUsers.Controls.Add(Me.TableLayoutPanel1)
        Me.PnlUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlUsers.Location = New System.Drawing.Point(0, 341)
        Me.PnlUsers.Name = "PnlUsers"
        Me.PnlUsers.Size = New System.Drawing.Size(738, 322)
        Me.PnlUsers.TabIndex = 16
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grdComputers, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grdUsers, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 305.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(738, 322)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'grdComputers
        '
        Me.grdComputers.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdComputers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdComputers.ColumnAutoResize = True
        Me.grdComputers.ColumnAutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCells
        grdComputers_DesignTimeLayout.LayoutString = resources.GetString("grdComputers_DesignTimeLayout.LayoutString")
        Me.grdComputers.DesignTimeLayout = grdComputers_DesignTimeLayout
        Me.grdComputers.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdComputers.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdComputers.GroupByBoxVisible = False
        Me.grdComputers.Location = New System.Drawing.Point(3, 23)
        Me.grdComputers.Name = "grdComputers"
        Me.grdComputers.RecordNavigator = True
        Me.grdComputers.Size = New System.Drawing.Size(363, 299)
        Me.grdComputers.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.grdComputers, "Select Employee Payable Account")
        Me.grdComputers.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(372, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(245, 20)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Select computers to attach with"
        '
        'grdUsers
        '
        Me.grdUsers.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdUsers.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdUsers.ColumnAutoResize = True
        Me.grdUsers.ColumnAutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCells
        grdUsers_DesignTimeLayout.LayoutString = resources.GetString("grdUsers_DesignTimeLayout.LayoutString")
        Me.grdUsers.DesignTimeLayout = grdUsers_DesignTimeLayout
        Me.grdUsers.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdUsers.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdUsers.GroupByBoxVisible = False
        Me.grdUsers.Location = New System.Drawing.Point(372, 23)
        Me.grdUsers.Name = "grdUsers"
        Me.grdUsers.RecordNavigator = True
        Me.grdUsers.Size = New System.Drawing.Size(363, 299)
        Me.grdUsers.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.grdUsers, "Select Employee Payable Account")
        Me.grdUsers.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(199, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select user to attach with"
        '
        'pnlMenu
        '
        Me.pnlMenu.Controls.Add(Me.TableLayoutPanel2)
        Me.pnlMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMenu.Location = New System.Drawing.Point(0, 84)
        Me.pnlMenu.Name = "pnlMenu"
        Me.pnlMenu.Size = New System.Drawing.Size(738, 257)
        Me.pnlMenu.TabIndex = 14
        Me.pnlMenu.Visible = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel3, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(738, 257)
        Me.TableLayoutPanel2.TabIndex = 12
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtMainManu)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.btnAddManu)
        Me.Panel2.Controls.Add(Me.lstManus)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(363, 251)
        Me.Panel2.TabIndex = 10
        '
        'txtMainManu
        '
        Me.txtMainManu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMainManu.Location = New System.Drawing.Point(6, 19)
        Me.txtMainManu.Name = "txtMainManu"
        Me.txtMainManu.Size = New System.Drawing.Size(303, 26)
        Me.txtMainManu.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtMainManu, "Select Any Group")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Main menu title"
        '
        'btnAddManu
        '
        Me.btnAddManu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddManu.Location = New System.Drawing.Point(315, 17)
        Me.btnAddManu.Name = "btnAddManu"
        Me.btnAddManu.Size = New System.Drawing.Size(45, 23)
        Me.btnAddManu.TabIndex = 8
        Me.btnAddManu.Text = "Add"
        Me.btnAddManu.UseVisualStyleBackColor = True
        '
        'lstManus
        '
        Me.lstManus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstManus.FormattingEnabled = True
        Me.lstManus.ItemHeight = 20
        Me.lstManus.Location = New System.Drawing.Point(6, 45)
        Me.lstManus.Name = "lstManus"
        Me.lstManus.Size = New System.Drawing.Size(354, 184)
        Me.lstManus.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.cmbApplicationsMultiple)
        Me.Panel3.Controls.Add(Me.lstApplications)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.btnAddApplication)
        Me.Panel3.Controls.Add(Me.txtSubTitle)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(372, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(363, 251)
        Me.Panel3.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(4, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(141, 20)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Select application"
        '
        'cmbApplicationsMultiple
        '
        Me.cmbApplicationsMultiple.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbApplicationsMultiple.FormattingEnabled = True
        Me.cmbApplicationsMultiple.Location = New System.Drawing.Point(7, 19)
        Me.cmbApplicationsMultiple.Name = "cmbApplicationsMultiple"
        Me.cmbApplicationsMultiple.Size = New System.Drawing.Size(351, 28)
        Me.cmbApplicationsMultiple.TabIndex = 12
        '
        'lstApplications
        '
        Me.lstApplications.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstApplications.FormattingEnabled = True
        Me.lstApplications.ItemHeight = 20
        Me.lstApplications.Location = New System.Drawing.Point(9, 84)
        Me.lstApplications.MultiColumn = True
        Me.lstApplications.Name = "lstApplications"
        Me.lstApplications.Size = New System.Drawing.Size(351, 144)
        Me.lstApplications.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(6, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 20)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Sub menu title"
        '
        'btnAddApplication
        '
        Me.btnAddApplication.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddApplication.Location = New System.Drawing.Point(315, 56)
        Me.btnAddApplication.Name = "btnAddApplication"
        Me.btnAddApplication.Size = New System.Drawing.Size(45, 23)
        Me.btnAddApplication.TabIndex = 9
        Me.btnAddApplication.Text = "Add"
        Me.btnAddApplication.UseVisualStyleBackColor = True
        '
        'txtSubTitle
        '
        Me.txtSubTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSubTitle.Location = New System.Drawing.Point(9, 58)
        Me.txtSubTitle.Name = "txtSubTitle"
        Me.txtSubTitle.Size = New System.Drawing.Size(300, 26)
        Me.txtSubTitle.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtSubTitle, "Select Any Group")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cmbApplicationSingle)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbLayout)
        Me.Panel1.Controls.Add(Me.txtTitle)
        Me.Panel1.Controls.Add(Me.CtrlGrdBar1)
        Me.Panel1.Controls.Add(Me.ToolStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(738, 84)
        Me.Panel1.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(3, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 20)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Title"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(401, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Select application"
        '
        'cmbApplicationSingle
        '
        Me.cmbApplicationSingle.FormattingEnabled = True
        Me.cmbApplicationSingle.Location = New System.Drawing.Point(404, 52)
        Me.cmbApplicationSingle.Name = "cmbApplicationSingle"
        Me.cmbApplicationSingle.Size = New System.Drawing.Size(321, 28)
        Me.cmbApplicationSingle.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(274, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Layout"
        '
        'cmbLayout
        '
        Me.cmbLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLayout.FormattingEnabled = True
        Me.cmbLayout.Items.AddRange(New Object() {"Single Application", "Multi Application"})
        Me.cmbLayout.Location = New System.Drawing.Point(277, 52)
        Me.cmbLayout.Name = "cmbLayout"
        Me.cmbLayout.Size = New System.Drawing.Size(121, 28)
        Me.cmbLayout.TabIndex = 3
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(6, 52)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(265, 26)
        Me.txtTitle.TabIndex = 0
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(697, 0)
        Me.CtrlGrdBar1.MyGrid = Nothing
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(39, 28)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CtrlGrdBar1, "Settings")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.toolStripSeparator, Me.btnPrint, Me.btnhelp, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(699, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 22)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 22)
        Me.btnSave.Text = "&Save"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 22)
        Me.btnPrint.Text = "&Print"
        '
        'btnhelp
        '
        Me.btnhelp.Image = CType(resources.GetObject("btnhelp.Image"), System.Drawing.Image)
        Me.btnhelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnhelp.Name = "btnhelp"
        Me.btnhelp.Size = New System.Drawing.Size(77, 22)
        Me.btnhelp.Text = "He&lp"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.miniToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(736, 25)
        Me.miniToolStrip.TabIndex = 1
        '
        'frmTerminalConfiguration
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(918, 663)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTerminalConfiguration"
        Me.Text = "Terminal Configuration"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.PnlUsers.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.grdComputers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMenu.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblGroupName As System.Windows.Forms.Label
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAddSingleApplication As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lstgroup As System.Windows.Forms.ListBox
    Friend WithEvents miniToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents PnlUsers As System.Windows.Forms.Panel
    Friend WithEvents pnlMenu As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSubTitle As System.Windows.Forms.TextBox
    Friend WithEvents lstApplications As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMainManu As System.Windows.Forms.TextBox
    Friend WithEvents lstManus As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbApplicationSingle As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbLayout As System.Windows.Forms.ComboBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnhelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdComputers As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grdUsers As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnAddManu As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbApplicationsMultiple As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddApplication As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
