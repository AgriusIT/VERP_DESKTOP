<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefCustomerType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefCustomerType))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GrdCustomerTypes = New Janus.Windows.GridEX.GridEX()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblSubType = New System.Windows.Forms.Label()
        Me.cmbSubCustomerType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Sort = New System.Windows.Forms.Label()
        Me.TxtId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtSort = New System.Windows.Forms.TextBox()
        Me.UichkActive = New System.Windows.Forms.CheckBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnHeader = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.btnCustomerCategory = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.GrdCustomerTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.GrdCustomerTypes)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel3)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(2, 2)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(669, 432)
        '
        'GrdCustomerTypes
        '
        Me.GrdCustomerTypes.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GrdCustomerTypes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrdCustomerTypes.ColumnAutoResize = True
        Me.GrdCustomerTypes.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GrdCustomerTypes.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GrdCustomerTypes.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GrdCustomerTypes.GroupByBoxVisible = False
        Me.GrdCustomerTypes.Location = New System.Drawing.Point(-1, 226)
        Me.GrdCustomerTypes.Name = "GrdCustomerTypes"
        Me.GrdCustomerTypes.RecordNavigator = True
        Me.GrdCustomerTypes.Size = New System.Drawing.Size(670, 190)
        Me.GrdCustomerTypes.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.GrdCustomerTypes, "Define Customer Types")
        Me.GrdCustomerTypes.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.TxtRemarks)
        Me.Panel3.Location = New System.Drawing.Point(337, 83)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(322, 137)
        Me.Panel3.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(13, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtRemarks.Location = New System.Drawing.Point(16, 31)
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(292, 94)
        Me.TxtRemarks.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TxtRemarks, "Comments")
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblSubType)
        Me.Panel2.Controls.Add(Me.cmbSubCustomerType)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Sort)
        Me.Panel2.Controls.Add(Me.TxtId)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.TxtName)
        Me.Panel2.Controls.Add(Me.TxtSort)
        Me.Panel2.Controls.Add(Me.UichkActive)
        Me.Panel2.Location = New System.Drawing.Point(15, 83)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(316, 137)
        Me.Panel2.TabIndex = 13
        '
        'lblSubType
        '
        Me.lblSubType.AutoSize = True
        Me.lblSubType.ForeColor = System.Drawing.Color.Black
        Me.lblSubType.Location = New System.Drawing.Point(6, 41)
        Me.lblSubType.Name = "lblSubType"
        Me.lblSubType.Size = New System.Drawing.Size(49, 13)
        Me.lblSubType.TabIndex = 13
        Me.lblSubType.Text = "Category"
        '
        'cmbSubCustomerType
        '
        Me.cmbSubCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSubCustomerType.FormattingEnabled = True
        Me.cmbSubCustomerType.Location = New System.Drawing.Point(67, 38)
        Me.cmbSubCustomerType.Name = "cmbSubCustomerType"
        Me.cmbSubCustomerType.Size = New System.Drawing.Size(237, 21)
        Me.cmbSubCustomerType.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbSubCustomerType, "Enter or select sub type of the customer.")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(18, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "ID"
        Me.Label6.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Type"
        '
        'Sort
        '
        Me.Sort.AutoSize = True
        Me.Sort.ForeColor = System.Drawing.Color.Black
        Me.Sort.Location = New System.Drawing.Point(64, 89)
        Me.Sort.Name = "Sort"
        Me.Sort.Size = New System.Drawing.Size(55, 13)
        Me.Sort.TabIndex = 6
        Me.Sort.Text = "Sort Order"
        '
        'TxtId
        '
        Me.TxtId.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtId.Location = New System.Drawing.Point(67, 10)
        Me.TxtId.Name = "TxtId"
        Me.TxtId.Size = New System.Drawing.Size(237, 20)
        Me.TxtId.TabIndex = 11
        Me.TxtId.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(245, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Status"
        '
        'TxtName
        '
        Me.TxtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtName.Location = New System.Drawing.Point(67, 65)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(237, 20)
        Me.TxtName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.TxtName, "Customer Type")
        '
        'TxtSort
        '
        Me.TxtSort.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSort.Location = New System.Drawing.Point(67, 105)
        Me.TxtSort.Name = "TxtSort"
        Me.TxtSort.Size = New System.Drawing.Size(56, 20)
        Me.TxtSort.TabIndex = 7
        Me.TxtSort.Text = "1"
        Me.ToolTip1.SetToolTip(Me.TxtSort, "Sort Order")
        '
        'UichkActive
        '
        Me.UichkActive.AutoSize = True
        Me.UichkActive.Checked = True
        Me.UichkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.UichkActive.ForeColor = System.Drawing.Color.Black
        Me.UichkActive.Location = New System.Drawing.Point(248, 108)
        Me.UichkActive.Name = "UichkActive"
        Me.UichkActive.Size = New System.Drawing.Size(56, 17)
        Me.UichkActive.TabIndex = 9
        Me.UichkActive.Text = "Active" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ToolTip1.SetToolTip(Me.UichkActive, "Customer Type Status Active Or Inactive")
        Me.UichkActive.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnHeader)
        Me.pnlHeader.Controls.Add(Me.Button8)
        Me.pnlHeader.Controls.Add(Me.Button6)
        Me.pnlHeader.Controls.Add(Me.btnRefresh)
        Me.pnlHeader.Controls.Add(Me.Button4)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.Button7)
        Me.pnlHeader.Controls.Add(Me.Button3)
        Me.pnlHeader.Controls.Add(Me.Button2)
        Me.pnlHeader.Controls.Add(Me.Button1)
        Me.pnlHeader.Controls.Add(Me.btnReport)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 31)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(669, 42)
        Me.pnlHeader.TabIndex = 12
        '
        'btnHeader
        '
        Me.btnHeader.BackgroundImage = CType(resources.GetObject("btnHeader.BackgroundImage"), System.Drawing.Image)
        Me.btnHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnHeader.FlatAppearance.BorderSize = 0
        Me.btnHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHeader.Location = New System.Drawing.Point(3, 10)
        Me.btnHeader.Name = "btnHeader"
        Me.btnHeader.Size = New System.Drawing.Size(21, 23)
        Me.btnHeader.TabIndex = 0
        Me.btnHeader.UseVisualStyleBackColor = True
        Me.btnHeader.Visible = False
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackgroundImage = CType(resources.GetObject("Button8.BackgroundImage"), System.Drawing.Image)
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button8.FlatAppearance.BorderSize = 0
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Location = New System.Drawing.Point(502, 9)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(25, 23)
        Me.Button8.TabIndex = 0
        Me.Button8.UseVisualStyleBackColor = True
        Me.Button8.Visible = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), System.Drawing.Image)
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Location = New System.Drawing.Point(612, 6)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(27, 30)
        Me.Button6.TabIndex = 0
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(588, 8)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(30, 26)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.UseVisualStyleBackColor = True
        Me.btnRefresh.Visible = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(566, 9)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(28, 24)
        Me.Button4.TabIndex = 0
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(22, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(139, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Customer Type"
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"), System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Location = New System.Drawing.Point(481, 10)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(22, 22)
        Me.Button7.TabIndex = 0
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(545, 9)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(25, 23)
        Me.Button3.TabIndex = 0
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(525, 9)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 23)
        Me.Button2.TabIndex = 0
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(460, 9)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnReport
        '
        Me.btnReport.BackgroundImage = CType(resources.GetObject("btnReport.BackgroundImage"), System.Drawing.Image)
        Me.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnReport.FlatAppearance.BorderSize = 0
        Me.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReport.Location = New System.Drawing.Point(433, 8)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(29, 27)
        Me.btnReport.TabIndex = 0
        Me.btnReport.UseVisualStyleBackColor = True
        Me.btnReport.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton, Me.btnCustomerCategory})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(669, 31)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(59, 28)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(55, 28)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(59, 28)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(68, 28)
        Me.BtnDelete.Text = "&Delete"
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(60, 28)
        Me.BtnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'btnCustomerCategory
        '
        Me.btnCustomerCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnCustomerCategory.Image = CType(resources.GetObject("btnCustomerCategory.Image"), System.Drawing.Image)
        Me.btnCustomerCategory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCustomerCategory.Name = "btnCustomerCategory"
        Me.btnCustomerCategory.Size = New System.Drawing.Size(84, 28)
        Me.btnCustomerCategory.Text = "Add Category"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-6667, -6500)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(669, 432)
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(673, 455)
        Me.UltraTabControl1.TabIndex = 11
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        Appearance1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        UltraTab1.Appearance = Appearance1
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Type's"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History "
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(669, 432)
        '
        'frmDefCustomerType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(673, 455)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDefCustomerType"
        Me.Text = "Customer Type"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.GrdCustomerTypes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents TxtId As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UichkActive As System.Windows.Forms.CheckBox
    Friend WithEvents TxtSort As System.Windows.Forms.TextBox
    Friend WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Sort As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnHeader As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents GrdCustomerTypes As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbSubCustomerType As System.Windows.Forms.ComboBox
    Friend WithEvents lblSubType As System.Windows.Forms.Label
    Friend WithEvents btnCustomerCategory As System.Windows.Forms.ToolStripButton
End Class
