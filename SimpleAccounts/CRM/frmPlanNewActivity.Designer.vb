<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlanNewActivity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPlanNewActivity))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.cmbContactPerson = New System.Windows.Forms.ComboBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Ttip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmbLeadTitle = New System.Windows.Forms.ComboBox()
        Me.txtAddressLine1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbActivityType = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpTime = New System.Windows.Forms.DateTimePicker()
        Me.cbConfirmed = New System.Windows.Forms.CheckBox()
        Me.cmbResponsiblePerson = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbInsideSales = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbManager = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtObjectives = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbOffice = New System.Windows.Forms.ComboBox()
        Me.lblOffice = New System.Windows.Forms.Label()
        Me.cmbProject = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(464, 75)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(143, 28)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Contact Person"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(13, 18)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(260, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Plan New Activity"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Location = New System.Drawing.Point(948, 5)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(81, 95)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 80)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 28)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Company Name"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(1058, 5)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(81, 95)
        Me.btnSave.TabIndex = 0
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'cmbContactPerson
        '
        Me.cmbContactPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbContactPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbContactPerson.BackColor = System.Drawing.Color.White
        Me.cmbContactPerson.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbContactPerson.FormattingEnabled = True
        Me.cmbContactPerson.Location = New System.Drawing.Point(468, 111)
        Me.cmbContactPerson.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbContactPerson.Name = "cmbContactPerson"
        Me.cmbContactPerson.Size = New System.Drawing.Size(334, 36)
        Me.cmbContactPerson.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1152, 71)
        Me.pnlHeader.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 718)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1152, 105)
        Me.Panel2.TabIndex = 173
        '
        'cmbLeadTitle
        '
        Me.cmbLeadTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLeadTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLeadTitle.BackColor = System.Drawing.Color.White
        Me.cmbLeadTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLeadTitle.FormattingEnabled = True
        Me.cmbLeadTitle.Location = New System.Drawing.Point(6, 111)
        Me.cmbLeadTitle.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbLeadTitle.Name = "cmbLeadTitle"
        Me.cmbLeadTitle.Size = New System.Drawing.Size(442, 36)
        Me.cmbLeadTitle.TabIndex = 0
        '
        'txtAddressLine1
        '
        Me.txtAddressLine1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddressLine1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddressLine1.Location = New System.Drawing.Point(6, 198)
        Me.txtAddressLine1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAddressLine1.Multiline = True
        Me.txtAddressLine1.Name = "txtAddressLine1"
        Me.txtAddressLine1.Size = New System.Drawing.Size(1136, 119)
        Me.txtAddressLine1.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(2, 168)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 28)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Address"
        '
        'cmbActivityType
        '
        Me.cmbActivityType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbActivityType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbActivityType.BackColor = System.Drawing.Color.White
        Me.cmbActivityType.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbActivityType.FormattingEnabled = True
        Me.cmbActivityType.Location = New System.Drawing.Point(6, 360)
        Me.cmbActivityType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbActivityType.Name = "cmbActivityType"
        Me.cmbActivityType.Size = New System.Drawing.Size(217, 36)
        Me.cmbActivityType.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(2, 329)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(124, 28)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Activity Type"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(234, 360)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(214, 33)
        Me.dtpDate.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(230, 329)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 28)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(464, 329)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 28)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Time"
        '
        'dtpTime
        '
        Me.dtpTime.Checked = False
        Me.dtpTime.CustomFormat = ""
        Me.dtpTime.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTime.Location = New System.Drawing.Point(468, 360)
        Me.dtpTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTime.Name = "dtpTime"
        Me.dtpTime.Size = New System.Drawing.Size(214, 33)
        Me.dtpTime.TabIndex = 6
        Me.dtpTime.Value = New Date(2018, 3, 1, 14, 53, 0, 0)
        '
        'cbConfirmed
        '
        Me.cbConfirmed.AutoSize = True
        Me.cbConfirmed.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbConfirmed.Location = New System.Drawing.Point(710, 363)
        Me.cbConfirmed.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbConfirmed.Name = "cbConfirmed"
        Me.cbConfirmed.Size = New System.Drawing.Size(130, 32)
        Me.cbConfirmed.TabIndex = 7
        Me.cbConfirmed.Text = "Confirmed"
        Me.cbConfirmed.UseVisualStyleBackColor = True
        Me.cbConfirmed.Visible = False
        '
        'cmbResponsiblePerson
        '
        Me.cmbResponsiblePerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbResponsiblePerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbResponsiblePerson.BackColor = System.Drawing.Color.White
        Me.cmbResponsiblePerson.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbResponsiblePerson.FormattingEnabled = True
        Me.cmbResponsiblePerson.Location = New System.Drawing.Point(6, 451)
        Me.cmbResponsiblePerson.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbResponsiblePerson.Name = "cmbResponsiblePerson"
        Me.cmbResponsiblePerson.Size = New System.Drawing.Size(217, 36)
        Me.cmbResponsiblePerson.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(2, 420)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(179, 28)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Responsible Person"
        '
        'cmbInsideSales
        '
        Me.cmbInsideSales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbInsideSales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbInsideSales.BackColor = System.Drawing.Color.White
        Me.cmbInsideSales.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbInsideSales.FormattingEnabled = True
        Me.cmbInsideSales.Items.AddRange(New Object() {"P1", "P2", "P3", "P4", "P5"})
        Me.cmbInsideSales.Location = New System.Drawing.Point(234, 451)
        Me.cmbInsideSales.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbInsideSales.Name = "cmbInsideSales"
        Me.cmbInsideSales.Size = New System.Drawing.Size(217, 36)
        Me.cmbInsideSales.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(230, 420)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 28)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Priorities"
        '
        'cmbManager
        '
        Me.cmbManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbManager.BackColor = System.Drawing.Color.White
        Me.cmbManager.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbManager.FormattingEnabled = True
        Me.cmbManager.Location = New System.Drawing.Point(468, 451)
        Me.cmbManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbManager.Name = "cmbManager"
        Me.cmbManager.Size = New System.Drawing.Size(217, 36)
        Me.cmbManager.TabIndex = 10
        Me.cmbManager.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(464, 420)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 28)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Manager"
        Me.Label11.Visible = False
        '
        'txtObjectives
        '
        Me.txtObjectives.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObjectives.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObjectives.Location = New System.Drawing.Point(6, 537)
        Me.txtObjectives.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtObjectives.Multiline = True
        Me.txtObjectives.Name = "txtObjectives"
        Me.txtObjectives.Size = New System.Drawing.Size(1136, 159)
        Me.txtObjectives.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(4, 506)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 28)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Objective"
        '
        'cmbOffice
        '
        Me.cmbOffice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbOffice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbOffice.BackColor = System.Drawing.Color.White
        Me.cmbOffice.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOffice.FormattingEnabled = True
        Me.cmbOffice.Location = New System.Drawing.Point(813, 111)
        Me.cmbOffice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbOffice.Name = "cmbOffice"
        Me.cmbOffice.Size = New System.Drawing.Size(328, 36)
        Me.cmbOffice.TabIndex = 2
        '
        'lblOffice
        '
        Me.lblOffice.AutoSize = True
        Me.lblOffice.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOffice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOffice.Location = New System.Drawing.Point(808, 75)
        Me.lblOffice.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOffice.Name = "lblOffice"
        Me.lblOffice.Size = New System.Drawing.Size(63, 28)
        Me.lblOffice.TabIndex = 5
        Me.lblOffice.Text = "Office"
        '
        'cmbProject
        '
        Me.cmbProject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProject.BackColor = System.Drawing.Color.White
        Me.cmbProject.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProject.FormattingEnabled = True
        Me.cmbProject.Location = New System.Drawing.Point(710, 451)
        Me.cmbProject.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbProject.Name = "cmbProject"
        Me.cmbProject.Size = New System.Drawing.Size(217, 36)
        Me.cmbProject.TabIndex = 174
        Me.cmbProject.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(705, 420)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 28)
        Me.Label4.TabIndex = 175
        Me.Label4.Text = "Project"
        Me.Label4.Visible = False
        '
        'frmPlanNewActivity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1152, 823)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbProject)
        Me.Controls.Add(Me.txtObjectives)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cmbManager)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmbInsideSales)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmbResponsiblePerson)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cbConfirmed)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpTime)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.cmbActivityType)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbLeadTitle)
        Me.Controls.Add(Me.txtAddressLine1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblOffice)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbOffice)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbContactPerson)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmPlanNewActivity"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Plan New Activity"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cmbContactPerson As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Ttip As System.Windows.Forms.ToolTip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbLeadTitle As System.Windows.Forms.ComboBox
    Friend WithEvents txtAddressLine1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbActivityType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbConfirmed As System.Windows.Forms.CheckBox
    Friend WithEvents cmbResponsiblePerson As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbInsideSales As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbManager As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtObjectives As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbOffice As System.Windows.Forms.ComboBox
    Friend WithEvents lblOffice As System.Windows.Forms.Label
    Friend WithEvents cmbProject As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
