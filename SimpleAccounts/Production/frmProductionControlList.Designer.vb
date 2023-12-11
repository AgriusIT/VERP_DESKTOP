<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionControlList
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
        Dim grdProductionProgress_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductionControlList))
        Dim grdMaterial_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdExpense_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdWarrentyClaim_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdProductionProgress = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdMaterial = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdExpense = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdWarrentyClaim = New Janus.Windows.GridEX.GridEX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CtrlGrdBarWarrentyClaim = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBarExpense = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBarMaterial = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBarProductionProgress = New SimpleAccounts.CtrlGrdBar()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnAddDock = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbSalesOrder = New System.Windows.Forms.ComboBox()
        Me.cmbPlanNo = New System.Windows.Forms.ComboBox()
        Me.cmbTicketNo = New System.Windows.Forms.ComboBox()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grdProductionProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.grdExpense, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        CType(Me.grdWarrentyClaim, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.grdProductionProgress)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 27)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1604, 704)
        '
        'grdProductionProgress
        '
        Me.grdProductionProgress.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdProductionProgress_DesignTimeLayout.LayoutString = resources.GetString("grdProductionProgress_DesignTimeLayout.LayoutString")
        Me.grdProductionProgress.DesignTimeLayout = grdProductionProgress_DesignTimeLayout
        Me.grdProductionProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProductionProgress.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdProductionProgress.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdProductionProgress.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdProductionProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdProductionProgress.GroupByBoxVisible = False
        Me.grdProductionProgress.Location = New System.Drawing.Point(0, 0)
        Me.grdProductionProgress.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdProductionProgress.Name = "grdProductionProgress"
        Me.grdProductionProgress.Size = New System.Drawing.Size(1604, 704)
        Me.grdProductionProgress.TabIndex = 0
        Me.grdProductionProgress.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdMaterial)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-15000, -15385)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1604, 704)
        '
        'grdMaterial
        '
        Me.grdMaterial.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdMaterial_DesignTimeLayout.LayoutString = resources.GetString("grdMaterial_DesignTimeLayout.LayoutString")
        Me.grdMaterial.DesignTimeLayout = grdMaterial_DesignTimeLayout
        Me.grdMaterial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMaterial.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdMaterial.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdMaterial.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdMaterial.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMaterial.GroupByBoxVisible = False
        Me.grdMaterial.Location = New System.Drawing.Point(0, 0)
        Me.grdMaterial.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdMaterial.Name = "grdMaterial"
        Me.grdMaterial.Size = New System.Drawing.Size(1604, 704)
        Me.grdMaterial.TabIndex = 1
        Me.grdMaterial.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.grdExpense)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-15000, -15385)
        Me.UltraTabPageControl3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(1604, 704)
        '
        'grdExpense
        '
        Me.grdExpense.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdExpense_DesignTimeLayout.LayoutString = resources.GetString("grdExpense_DesignTimeLayout.LayoutString")
        Me.grdExpense.DesignTimeLayout = grdExpense_DesignTimeLayout
        Me.grdExpense.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdExpense.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdExpense.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdExpense.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdExpense.GroupByBoxVisible = False
        Me.grdExpense.Location = New System.Drawing.Point(0, 0)
        Me.grdExpense.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdExpense.Name = "grdExpense"
        Me.grdExpense.Size = New System.Drawing.Size(1604, 704)
        Me.grdExpense.TabIndex = 1
        Me.grdExpense.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.grdWarrentyClaim)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-15000, -15385)
        Me.UltraTabPageControl4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(1604, 704)
        '
        'grdWarrentyClaim
        '
        Me.grdWarrentyClaim.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdWarrentyClaim_DesignTimeLayout.LayoutString = resources.GetString("grdWarrentyClaim_DesignTimeLayout.LayoutString")
        Me.grdWarrentyClaim.DesignTimeLayout = grdWarrentyClaim_DesignTimeLayout
        Me.grdWarrentyClaim.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdWarrentyClaim.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdWarrentyClaim.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdWarrentyClaim.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdWarrentyClaim.GroupByBoxVisible = False
        Me.grdWarrentyClaim.Location = New System.Drawing.Point(0, 0)
        Me.grdWarrentyClaim.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdWarrentyClaim.Name = "grdWarrentyClaim"
        Me.grdWarrentyClaim.Size = New System.Drawing.Size(1604, 704)
        Me.grdWarrentyClaim.TabIndex = 1
        Me.grdWarrentyClaim.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(18, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(309, 45)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Production Control"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel3.Controls.Add(Me.CtrlGrdBarWarrentyClaim)
        Me.Panel3.Controls.Add(Me.CtrlGrdBarExpense)
        Me.Panel3.Controls.Add(Me.CtrlGrdBarMaterial)
        Me.Panel3.Controls.Add(Me.CtrlGrdBarProductionProgress)
        Me.Panel3.Controls.Add(Me.btnRefresh)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.btnAddDock)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1650, 58)
        Me.Panel3.TabIndex = 16
        '
        'CtrlGrdBarWarrentyClaim
        '
        Me.CtrlGrdBarWarrentyClaim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarWarrentyClaim.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarWarrentyClaim.Email = Nothing
        Me.CtrlGrdBarWarrentyClaim.FormName = Me
        Me.CtrlGrdBarWarrentyClaim.Location = New System.Drawing.Point(1581, 9)
        Me.CtrlGrdBarWarrentyClaim.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBarWarrentyClaim.MyGrid = Me.grdWarrentyClaim
        Me.CtrlGrdBarWarrentyClaim.Name = "CtrlGrdBarWarrentyClaim"
        Me.CtrlGrdBarWarrentyClaim.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBarWarrentyClaim.TabIndex = 27
        '
        'CtrlGrdBarExpense
        '
        Me.CtrlGrdBarExpense.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarExpense.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarExpense.Email = Nothing
        Me.CtrlGrdBarExpense.FormName = Me
        Me.CtrlGrdBarExpense.Location = New System.Drawing.Point(1581, 9)
        Me.CtrlGrdBarExpense.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBarExpense.MyGrid = Me.grdExpense
        Me.CtrlGrdBarExpense.Name = "CtrlGrdBarExpense"
        Me.CtrlGrdBarExpense.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBarExpense.TabIndex = 27
        '
        'CtrlGrdBarMaterial
        '
        Me.CtrlGrdBarMaterial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarMaterial.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarMaterial.Email = Nothing
        Me.CtrlGrdBarMaterial.FormName = Me
        Me.CtrlGrdBarMaterial.Location = New System.Drawing.Point(1581, 9)
        Me.CtrlGrdBarMaterial.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBarMaterial.MyGrid = Me.grdMaterial
        Me.CtrlGrdBarMaterial.Name = "CtrlGrdBarMaterial"
        Me.CtrlGrdBarMaterial.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBarMaterial.TabIndex = 27
        '
        'CtrlGrdBarProductionProgress
        '
        Me.CtrlGrdBarProductionProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarProductionProgress.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarProductionProgress.Email = Nothing
        Me.CtrlGrdBarProductionProgress.FormName = Me
        Me.CtrlGrdBarProductionProgress.Location = New System.Drawing.Point(1581, 9)
        Me.CtrlGrdBarProductionProgress.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBarProductionProgress.MyGrid = Me.grdProductionProgress
        Me.CtrlGrdBarProductionProgress.Name = "CtrlGrdBarProductionProgress"
        Me.CtrlGrdBarProductionProgress.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBarProductionProgress.TabIndex = 26
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(1527, 9)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(45, 40)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnAddDock
        '
        Me.btnAddDock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddDock.BackColor = System.Drawing.Color.Transparent
        Me.btnAddDock.BackgroundImage = CType(resources.GetObject("btnAddDock.BackgroundImage"), System.Drawing.Image)
        Me.btnAddDock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddDock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddDock.Location = New System.Drawing.Point(1599, 12)
        Me.btnAddDock.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddDock.Name = "btnAddDock"
        Me.btnAddDock.Size = New System.Drawing.Size(30, 31)
        Me.btnAddDock.TabIndex = 6
        Me.btnAddDock.UseVisualStyleBackColor = False
        Me.btnAddDock.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 80)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(140, 32)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Sales Order"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(298, 80)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 32)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Plan No"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(576, 80)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 32)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Ticket No"
        '
        'cmbSalesOrder
        '
        Me.cmbSalesOrder.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesOrder.FormattingEnabled = True
        Me.cmbSalesOrder.Location = New System.Drawing.Point(27, 117)
        Me.cmbSalesOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSalesOrder.Name = "cmbSalesOrder"
        Me.cmbSalesOrder.Size = New System.Drawing.Size(266, 36)
        Me.cmbSalesOrder.TabIndex = 20
        '
        'cmbPlanNo
        '
        Me.cmbPlanNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPlanNo.FormattingEnabled = True
        Me.cmbPlanNo.Location = New System.Drawing.Point(304, 117)
        Me.cmbPlanNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPlanNo.Name = "cmbPlanNo"
        Me.cmbPlanNo.Size = New System.Drawing.Size(266, 36)
        Me.cmbPlanNo.TabIndex = 21
        '
        'cmbTicketNo
        '
        Me.cmbTicketNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTicketNo.FormattingEnabled = True
        Me.cmbTicketNo.Location = New System.Drawing.Point(582, 117)
        Me.cmbTicketNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTicketNo.Name = "cmbTicketNo"
        Me.cmbTicketNo.Size = New System.Drawing.Size(266, 36)
        Me.cmbTicketNo.TabIndex = 22
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl1.Location = New System.Drawing.Point(26, 188)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1606, 732)
        Me.UltraTabControl1.TabIndex = 24
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Production Progress"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Material"
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "Expenses"
        UltraTab4.TabPage = Me.UltraTabPageControl4
        UltraTab4.Text = "Warrenty Claim"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3, UltraTab4})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1604, 704)
        '
        'btnLoad
        '
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.Location = New System.Drawing.Point(861, 117)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(112, 42)
        Me.btnLoad.TabIndex = 25
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'frmProductionControlList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1650, 938)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.cmbTicketNo)
        Me.Controls.Add(Me.cmbPlanNo)
        Me.Controls.Add(Me.cmbSalesOrder)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmProductionControlList"
        Me.Text = "Production Control"
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.grdProductionProgress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.grdExpense, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        CType(Me.grdWarrentyClaim, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnAddDock As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbSalesOrder As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPlanNo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTicketNo As System.Windows.Forms.ComboBox
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdProductionProgress As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdMaterial As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdExpense As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdWarrentyClaim As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents CtrlGrdBarProductionProgress As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBarWarrentyClaim As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBarExpense As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBarMaterial As SimpleAccounts.CtrlGrdBar
End Class
