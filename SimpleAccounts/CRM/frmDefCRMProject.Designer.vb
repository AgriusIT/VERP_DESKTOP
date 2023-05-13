<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefCRMProject
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
        Dim grdActivities_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdQuotations_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdSalesOrder_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefCRMProject))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tbMain = New System.Windows.Forms.TabPage()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmbManager = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbInsideSales = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbResponsible = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbContractAward = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbEngineeringConsultant = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbMainContactPerson = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbLead = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbProjectCategory = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbProjectStatus = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbCity = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbRegion = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtScope = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPlant = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProject = New System.Windows.Forms.TextBox()
        Me.tbDetails = New System.Windows.Forms.TabPage()
        Me.richProjectDetail = New System.Windows.Forms.RichTextBox()
        Me.tbActivities = New System.Windows.Forms.TabPage()
        Me.grdActivities = New Janus.Windows.GridEX.GridEX()
        Me.tbQuotation = New System.Windows.Forms.TabPage()
        Me.grdQuotations = New Janus.Windows.GridEX.GridEX()
        Me.btnAddQuotation = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbQuotation = New System.Windows.Forms.ComboBox()
        Me.tbSalesOrder = New System.Windows.Forms.TabPage()
        Me.grdSalesOrder = New Janus.Windows.GridEX.GridEX()
        Me.btnAddSalesOrder = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbSalesOrder = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tbMain.SuspendLayout()
        Me.tbDetails.SuspendLayout()
        Me.tbActivities.SuspendLayout()
        CType(Me.grdActivities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbQuotation.SuspendLayout()
        CType(Me.grdQuotations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbSalesOrder.SuspendLayout()
        CType(Me.grdSalesOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1305, 115)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(18, 28)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(288, 41)
        Me.lblHeader.TabIndex = 11
        Me.lblHeader.Text = "Project Definition"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tbMain)
        Me.TabControl1.Controls.Add(Me.tbDetails)
        Me.TabControl1.Controls.Add(Me.tbActivities)
        Me.TabControl1.Controls.Add(Me.tbQuotation)
        Me.TabControl1.Controls.Add(Me.tbSalesOrder)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 115)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1305, 917)
        Me.TabControl1.TabIndex = 0
        '
        'tbMain
        '
        Me.tbMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbMain.Controls.Add(Me.Label18)
        Me.tbMain.Controls.Add(Me.cmbManager)
        Me.tbMain.Controls.Add(Me.Label17)
        Me.tbMain.Controls.Add(Me.cmbInsideSales)
        Me.tbMain.Controls.Add(Me.Label16)
        Me.tbMain.Controls.Add(Me.cmbResponsible)
        Me.tbMain.Controls.Add(Me.Label15)
        Me.tbMain.Controls.Add(Me.cmbContractAward)
        Me.tbMain.Controls.Add(Me.Label14)
        Me.tbMain.Controls.Add(Me.cmbEngineeringConsultant)
        Me.tbMain.Controls.Add(Me.Label13)
        Me.tbMain.Controls.Add(Me.cmbMainContactPerson)
        Me.tbMain.Controls.Add(Me.Label12)
        Me.tbMain.Controls.Add(Me.cmbLead)
        Me.tbMain.Controls.Add(Me.Label11)
        Me.tbMain.Controls.Add(Me.cmbProjectCategory)
        Me.tbMain.Controls.Add(Me.Label10)
        Me.tbMain.Controls.Add(Me.cmbProjectStatus)
        Me.tbMain.Controls.Add(Me.Label9)
        Me.tbMain.Controls.Add(Me.txtProduct)
        Me.tbMain.Controls.Add(Me.Label8)
        Me.tbMain.Controls.Add(Me.cmbCity)
        Me.tbMain.Controls.Add(Me.Label7)
        Me.tbMain.Controls.Add(Me.cmbRegion)
        Me.tbMain.Controls.Add(Me.Label6)
        Me.tbMain.Controls.Add(Me.txtAddress)
        Me.tbMain.Controls.Add(Me.Label5)
        Me.tbMain.Controls.Add(Me.txtScope)
        Me.tbMain.Controls.Add(Me.Label4)
        Me.tbMain.Controls.Add(Me.txtPlant)
        Me.tbMain.Controls.Add(Me.Label3)
        Me.tbMain.Controls.Add(Me.txtCode)
        Me.tbMain.Controls.Add(Me.Label2)
        Me.tbMain.Controls.Add(Me.txtProject)
        Me.tbMain.Location = New System.Drawing.Point(4, 29)
        Me.tbMain.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbMain.Name = "tbMain"
        Me.tbMain.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbMain.Size = New System.Drawing.Size(1297, 884)
        Me.tbMain.TabIndex = 0
        Me.tbMain.Text = "Main"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(834, 665)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(90, 28)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "Manager"
        '
        'cmbManager
        '
        Me.cmbManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbManager.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbManager.FormattingEnabled = True
        Me.cmbManager.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbManager.Location = New System.Drawing.Point(838, 695)
        Me.cmbManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbManager.Name = "cmbManager"
        Me.cmbManager.Size = New System.Drawing.Size(412, 36)
        Me.cmbManager.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.cmbManager, "Manager")
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(424, 665)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(112, 28)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Inside Sales"
        '
        'cmbInsideSales
        '
        Me.cmbInsideSales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbInsideSales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbInsideSales.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbInsideSales.FormattingEnabled = True
        Me.cmbInsideSales.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbInsideSales.Location = New System.Drawing.Point(429, 695)
        Me.cmbInsideSales.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbInsideSales.Name = "cmbInsideSales"
        Me.cmbInsideSales.Size = New System.Drawing.Size(398, 36)
        Me.cmbInsideSales.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.cmbInsideSales, "Inside Sales")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(15, 665)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(116, 28)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Responsible"
        '
        'cmbResponsible
        '
        Me.cmbResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbResponsible.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbResponsible.FormattingEnabled = True
        Me.cmbResponsible.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbResponsible.Location = New System.Drawing.Point(20, 695)
        Me.cmbResponsible.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbResponsible.Name = "cmbResponsible"
        Me.cmbResponsible.Size = New System.Drawing.Size(398, 36)
        Me.cmbResponsible.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmbResponsible, "Responsible")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(630, 592)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(195, 28)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Contract Awarded To"
        '
        'cmbContractAward
        '
        Me.cmbContractAward.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbContractAward.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbContractAward.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbContractAward.FormattingEnabled = True
        Me.cmbContractAward.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbContractAward.Location = New System.Drawing.Point(634, 623)
        Me.cmbContractAward.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbContractAward.Name = "cmbContractAward"
        Me.cmbContractAward.Size = New System.Drawing.Size(616, 36)
        Me.cmbContractAward.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmbContractAward, "Contract Awarded")
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(15, 592)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(215, 28)
        Me.Label14.TabIndex = 29
        Me.Label14.Text = "Engineering Consultant"
        '
        'cmbEngineeringConsultant
        '
        Me.cmbEngineeringConsultant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbEngineeringConsultant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEngineeringConsultant.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEngineeringConsultant.FormattingEnabled = True
        Me.cmbEngineeringConsultant.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbEngineeringConsultant.Location = New System.Drawing.Point(20, 623)
        Me.cmbEngineeringConsultant.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbEngineeringConsultant.Name = "cmbEngineeringConsultant"
        Me.cmbEngineeringConsultant.Size = New System.Drawing.Size(487, 36)
        Me.cmbEngineeringConsultant.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbEngineeringConsultant, "Engineering Consultant")
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(630, 517)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(192, 28)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "Main Contact Person"
        '
        'cmbMainContactPerson
        '
        Me.cmbMainContactPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbMainContactPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMainContactPerson.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMainContactPerson.FormattingEnabled = True
        Me.cmbMainContactPerson.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbMainContactPerson.Location = New System.Drawing.Point(634, 548)
        Me.cmbMainContactPerson.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbMainContactPerson.Name = "cmbMainContactPerson"
        Me.cmbMainContactPerson.Size = New System.Drawing.Size(616, 36)
        Me.cmbMainContactPerson.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbMainContactPerson, "Contact Person")
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(15, 517)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 28)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Lead"
        '
        'cmbLead
        '
        Me.cmbLead.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbLead.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLead.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLead.FormattingEnabled = True
        Me.cmbLead.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbLead.Location = New System.Drawing.Point(20, 548)
        Me.cmbLead.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbLead.Name = "cmbLead"
        Me.cmbLead.Size = New System.Drawing.Size(487, 36)
        Me.cmbLead.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cmbLead, "Lead")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(630, 442)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(158, 28)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Project Category"
        '
        'cmbProjectCategory
        '
        Me.cmbProjectCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbProjectCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProjectCategory.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProjectCategory.FormattingEnabled = True
        Me.cmbProjectCategory.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbProjectCategory.Location = New System.Drawing.Point(634, 472)
        Me.cmbProjectCategory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbProjectCategory.Name = "cmbProjectCategory"
        Me.cmbProjectCategory.Size = New System.Drawing.Size(616, 36)
        Me.cmbProjectCategory.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbProjectCategory, "Project Category")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(15, 442)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(131, 28)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Project Status"
        '
        'cmbProjectStatus
        '
        Me.cmbProjectStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbProjectStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProjectStatus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProjectStatus.FormattingEnabled = True
        Me.cmbProjectStatus.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbProjectStatus.Location = New System.Drawing.Point(20, 472)
        Me.cmbProjectStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbProjectStatus.Name = "cmbProjectStatus"
        Me.cmbProjectStatus.Size = New System.Drawing.Size(487, 36)
        Me.cmbProjectStatus.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cmbProjectStatus, "Project Status")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 368)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 28)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "Products"
        '
        'txtProduct
        '
        Me.txtProduct.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProduct.Location = New System.Drawing.Point(20, 398)
        Me.txtProduct.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.Size = New System.Drawing.Size(1231, 33)
        Me.txtProduct.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtProduct, "Products")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(630, 292)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 28)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "City"
        '
        'cmbCity
        '
        Me.cmbCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCity.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCity.FormattingEnabled = True
        Me.cmbCity.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbCity.Location = New System.Drawing.Point(634, 323)
        Me.cmbCity.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCity.Name = "cmbCity"
        Me.cmbCity.Size = New System.Drawing.Size(616, 36)
        Me.cmbCity.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbCity, "City")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 292)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 28)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Region"
        '
        'cmbRegion
        '
        Me.cmbRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRegion.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRegion.FormattingEnabled = True
        Me.cmbRegion.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbRegion.Location = New System.Drawing.Point(20, 323)
        Me.cmbRegion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbRegion.Name = "cmbRegion"
        Me.cmbRegion.Size = New System.Drawing.Size(487, 36)
        Me.cmbRegion.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbRegion, "Region")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 218)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 28)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Address"
        '
        'txtAddress
        '
        Me.txtAddress.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(20, 249)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(1231, 33)
        Me.txtAddress.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtAddress, "Address")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 92)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 28)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Scope"
        '
        'txtScope
        '
        Me.txtScope.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScope.Location = New System.Drawing.Point(20, 123)
        Me.txtScope.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtScope.Multiline = True
        Me.txtScope.Name = "txtScope"
        Me.txtScope.Size = New System.Drawing.Size(1231, 89)
        Me.txtScope.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtScope, "Scope")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(630, 17)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 28)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Plant"
        '
        'txtPlant
        '
        Me.txtPlant.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlant.Location = New System.Drawing.Point(634, 48)
        Me.txtPlant.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.Size = New System.Drawing.Size(619, 33)
        Me.txtPlant.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtPlant, "Plant")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(513, 17)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 28)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(518, 48)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(106, 33)
        Me.txtCode.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtCode, "Code")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 17)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 28)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Project"
        '
        'txtProject
        '
        Me.txtProject.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProject.Location = New System.Drawing.Point(20, 48)
        Me.txtProject.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProject.Name = "txtProject"
        Me.txtProject.Size = New System.Drawing.Size(487, 33)
        Me.txtProject.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtProject, "Project Title")
        '
        'tbDetails
        '
        Me.tbDetails.Controls.Add(Me.richProjectDetail)
        Me.tbDetails.Location = New System.Drawing.Point(4, 29)
        Me.tbDetails.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbDetails.Name = "tbDetails"
        Me.tbDetails.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbDetails.Size = New System.Drawing.Size(1297, 884)
        Me.tbDetails.TabIndex = 1
        Me.tbDetails.Text = "Details"
        Me.tbDetails.UseVisualStyleBackColor = True
        '
        'richProjectDetail
        '
        Me.richProjectDetail.Location = New System.Drawing.Point(20, 29)
        Me.richProjectDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.richProjectDetail.Name = "richProjectDetail"
        Me.richProjectDetail.Size = New System.Drawing.Size(672, 221)
        Me.richProjectDetail.TabIndex = 0
        Me.richProjectDetail.Text = ""
        '
        'tbActivities
        '
        Me.tbActivities.Controls.Add(Me.grdActivities)
        Me.tbActivities.Location = New System.Drawing.Point(4, 29)
        Me.tbActivities.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbActivities.Name = "tbActivities"
        Me.tbActivities.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbActivities.Size = New System.Drawing.Size(1297, 884)
        Me.tbActivities.TabIndex = 2
        Me.tbActivities.Text = "Activities"
        Me.tbActivities.UseVisualStyleBackColor = True
        '
        'grdActivities
        '
        Me.grdActivities.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdActivities.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdActivities_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdActivities.DesignTimeLayout = grdActivities_DesignTimeLayout
        Me.grdActivities.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdActivities.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdActivities.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdActivities.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdActivities.GroupByBoxVisible = False
        Me.grdActivities.Location = New System.Drawing.Point(4, 5)
        Me.grdActivities.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdActivities.Name = "grdActivities"
        Me.grdActivities.RecordNavigator = True
        Me.grdActivities.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdActivities.ScrollBarWidth = 20
        Me.grdActivities.Size = New System.Drawing.Size(1276, 754)
        Me.grdActivities.TabIndex = 223
        Me.grdActivities.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'tbQuotation
        '
        Me.tbQuotation.Controls.Add(Me.grdQuotations)
        Me.tbQuotation.Controls.Add(Me.btnAddQuotation)
        Me.tbQuotation.Controls.Add(Me.Label19)
        Me.tbQuotation.Controls.Add(Me.cmbQuotation)
        Me.tbQuotation.Location = New System.Drawing.Point(4, 29)
        Me.tbQuotation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbQuotation.Name = "tbQuotation"
        Me.tbQuotation.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbQuotation.Size = New System.Drawing.Size(1297, 884)
        Me.tbQuotation.TabIndex = 3
        Me.tbQuotation.Text = "Quotation"
        Me.tbQuotation.UseVisualStyleBackColor = True
        '
        'grdQuotations
        '
        Me.grdQuotations.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdQuotations.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdQuotations_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdQuotations.DesignTimeLayout = grdQuotations_DesignTimeLayout
        Me.grdQuotations.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdQuotations.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdQuotations.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdQuotations.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdQuotations.GroupByBoxVisible = False
        Me.grdQuotations.Location = New System.Drawing.Point(12, 95)
        Me.grdQuotations.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdQuotations.Name = "grdQuotations"
        Me.grdQuotations.RecordNavigator = True
        Me.grdQuotations.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdQuotations.ScrollBarWidth = 20
        Me.grdQuotations.Size = New System.Drawing.Size(1269, 663)
        Me.grdQuotations.TabIndex = 3
        Me.grdQuotations.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnAddQuotation
        '
        Me.btnAddQuotation.Location = New System.Drawing.Point(280, 48)
        Me.btnAddQuotation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddQuotation.Name = "btnAddQuotation"
        Me.btnAddQuotation.Size = New System.Drawing.Size(112, 38)
        Me.btnAddQuotation.TabIndex = 2
        Me.btnAddQuotation.Text = "Add"
        Me.btnAddQuotation.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(4, 23)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(79, 20)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Quotation"
        '
        'cmbQuotation
        '
        Me.cmbQuotation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbQuotation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbQuotation.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQuotation.FormattingEnabled = True
        Me.cmbQuotation.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbQuotation.Location = New System.Drawing.Point(9, 48)
        Me.cmbQuotation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbQuotation.Name = "cmbQuotation"
        Me.cmbQuotation.Size = New System.Drawing.Size(260, 36)
        Me.cmbQuotation.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbQuotation, "Region")
        '
        'tbSalesOrder
        '
        Me.tbSalesOrder.Controls.Add(Me.grdSalesOrder)
        Me.tbSalesOrder.Controls.Add(Me.btnAddSalesOrder)
        Me.tbSalesOrder.Controls.Add(Me.Label20)
        Me.tbSalesOrder.Controls.Add(Me.cmbSalesOrder)
        Me.tbSalesOrder.Location = New System.Drawing.Point(4, 29)
        Me.tbSalesOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbSalesOrder.Name = "tbSalesOrder"
        Me.tbSalesOrder.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tbSalesOrder.Size = New System.Drawing.Size(1297, 884)
        Me.tbSalesOrder.TabIndex = 4
        Me.tbSalesOrder.Text = "Sales Order"
        Me.tbSalesOrder.UseVisualStyleBackColor = True
        '
        'grdSalesOrder
        '
        Me.grdSalesOrder.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSalesOrder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdSalesOrder_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdSalesOrder.DesignTimeLayout = grdSalesOrder_DesignTimeLayout
        Me.grdSalesOrder.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSalesOrder.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSalesOrder.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSalesOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrder.GroupByBoxVisible = False
        Me.grdSalesOrder.Location = New System.Drawing.Point(16, 88)
        Me.grdSalesOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSalesOrder.Name = "grdSalesOrder"
        Me.grdSalesOrder.RecordNavigator = True
        Me.grdSalesOrder.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdSalesOrder.ScrollBarWidth = 20
        Me.grdSalesOrder.Size = New System.Drawing.Size(1269, 663)
        Me.grdSalesOrder.TabIndex = 2
        Me.grdSalesOrder.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnAddSalesOrder
        '
        Me.btnAddSalesOrder.Location = New System.Drawing.Point(285, 40)
        Me.btnAddSalesOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddSalesOrder.Name = "btnAddSalesOrder"
        Me.btnAddSalesOrder.Size = New System.Drawing.Size(112, 38)
        Me.btnAddSalesOrder.TabIndex = 1
        Me.btnAddSalesOrder.Text = "Add"
        Me.btnAddSalesOrder.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 15)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(90, 20)
        Me.Label20.TabIndex = 3
        Me.Label20.Text = "Sales order"
        '
        'cmbSalesOrder
        '
        Me.cmbSalesOrder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSalesOrder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSalesOrder.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesOrder.FormattingEnabled = True
        Me.cmbSalesOrder.Items.AddRange(New Object() {"Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbSalesOrder.Location = New System.Drawing.Point(14, 40)
        Me.cmbSalesOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSalesOrder.Name = "cmbSalesOrder"
        Me.cmbSalesOrder.Size = New System.Drawing.Size(260, 36)
        Me.cmbSalesOrder.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmbSalesOrder, "Region")
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 917)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1305, 115)
        Me.Panel2.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(1096, 15)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(81, 95)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(1206, 15)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(81, 95)
        Me.btnSave.TabIndex = 1
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frmDefCRMProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1305, 1032)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDefCRMProject"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Project Management"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tbMain.ResumeLayout(False)
        Me.tbMain.PerformLayout()
        Me.tbDetails.ResumeLayout(False)
        Me.tbActivities.ResumeLayout(False)
        CType(Me.grdActivities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbQuotation.ResumeLayout(False)
        Me.tbQuotation.PerformLayout()
        CType(Me.grdQuotations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbSalesOrder.ResumeLayout(False)
        Me.tbSalesOrder.PerformLayout()
        CType(Me.grdSalesOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tbMain As System.Windows.Forms.TabPage
    Friend WithEvents tbDetails As System.Windows.Forms.TabPage
    Friend WithEvents tbActivities As System.Windows.Forms.TabPage
    Friend WithEvents tbQuotation As System.Windows.Forms.TabPage
    Friend WithEvents tbSalesOrder As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtProject As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPlant As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtScope As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbRegion As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbCity As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbProjectStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbProjectCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbLead As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbMainContactPerson As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbEngineeringConsultant As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbContractAward As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbManager As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbInsideSales As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents richProjectDetail As System.Windows.Forms.RichTextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grdActivities As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAddQuotation As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbQuotation As System.Windows.Forms.ComboBox
    Friend WithEvents grdQuotations As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdSalesOrder As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAddSalesOrder As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbSalesOrder As System.Windows.Forms.ComboBox
End Class
