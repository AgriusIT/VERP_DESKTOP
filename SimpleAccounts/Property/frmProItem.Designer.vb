<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProItem))
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbTerritory = New System.Windows.Forms.ComboBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbPropertyType = New System.Windows.Forms.ComboBox()
        Me.txtSector = New System.Windows.Forms.TextBox()
        Me.txtPlotNo = New System.Windows.Forms.TextBox()
        Me.Ttip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbTitle = New System.Windows.Forms.ComboBox()
        Me.cmbPlotNo = New System.Windows.Forms.ComboBox()
        Me.cmbSector = New System.Windows.Forms.ComboBox()
        Me.cmbBlock = New System.Windows.Forms.ComboBox()
        Me.cmbPlotSize = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpEntryDate = New System.Windows.Forms.DateTimePicker()
        Me.txtBlock = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPlotSize = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtDemandAmount = New System.Windows.Forms.TextBox()
        Me.cbActive = New System.Windows.Forms.CheckBox()
        Me.lblNumberConvert = New System.Windows.Forms.Label()
        Me.Feature = New System.Windows.Forms.Label()
        Me.cmbFeature = New System.Windows.Forms.ComboBox()
        Me.lblSourceMobile = New System.Windows.Forms.Label()
        Me.txtSourceMobileNo = New System.Windows.Forms.TextBox()
        Me.Panel2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(308, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 15)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Territory"
        '
        'cmbTerritory
        '
        Me.cmbTerritory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTerritory.FormattingEnabled = True
        Me.cmbTerritory.Location = New System.Drawing.Point(308, 122)
        Me.cmbTerritory.Name = "cmbTerritory"
        Me.cmbTerritory.Size = New System.Drawing.Size(140, 21)
        Me.cmbTerritory.TabIndex = 18
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
        Me.btnCancel.Location = New System.Drawing.Point(788, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 62)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(304, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Plot Number *"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(157, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Entry Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(10, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Title"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 282)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(917, 68)
        Me.Panel2.TabIndex = 28
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
        Me.btnSave.Location = New System.Drawing.Point(861, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(54, 62)
        Me.btnSave.TabIndex = 0
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtTitle.Location = New System.Drawing.Point(737, 238)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(141, 23)
        Me.txtTitle.TabIndex = 1
        Me.txtTitle.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(917, 46)
        Me.pnlHeader.TabIndex = 0
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(251, 1)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(185, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Property Inventory"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(9, 154)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 15)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtRemarks.Location = New System.Drawing.Point(12, 172)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(584, 89)
        Me.txtRemarks.TabIndex = 26
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(13, 106)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 15)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Property Type"
        '
        'cmbPropertyType
        '
        Me.cmbPropertyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPropertyType.FormattingEnabled = True
        Me.cmbPropertyType.Location = New System.Drawing.Point(13, 124)
        Me.cmbPropertyType.Name = "cmbPropertyType"
        Me.cmbPropertyType.Size = New System.Drawing.Size(142, 21)
        Me.cmbPropertyType.TabIndex = 14
        '
        'txtSector
        '
        Me.txtSector.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSector.Location = New System.Drawing.Point(727, 238)
        Me.txtSector.Name = "txtSector"
        Me.txtSector.Size = New System.Drawing.Size(142, 23)
        Me.txtSector.TabIndex = 7
        Me.txtSector.Visible = False
        '
        'txtPlotNo
        '
        Me.txtPlotNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPlotNo.Location = New System.Drawing.Point(728, 267)
        Me.txtPlotNo.Name = "txtPlotNo"
        Me.txtPlotNo.Size = New System.Drawing.Size(141, 23)
        Me.txtPlotNo.TabIndex = 5
        Me.txtPlotNo.Visible = False
        '
        'cmbTitle
        '
        Me.cmbTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTitle.FormattingEnabled = True
        Me.cmbTitle.Location = New System.Drawing.Point(13, 74)
        Me.cmbTitle.Name = "cmbTitle"
        Me.cmbTitle.Size = New System.Drawing.Size(140, 21)
        Me.cmbTitle.TabIndex = 2
        Me.Ttip.SetToolTip(Me.cmbTitle, "Select Title :")
        '
        'cmbPlotNo
        '
        Me.cmbPlotNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPlotNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPlotNo.FormattingEnabled = True
        Me.cmbPlotNo.Location = New System.Drawing.Point(307, 74)
        Me.cmbPlotNo.Name = "cmbPlotNo"
        Me.cmbPlotNo.Size = New System.Drawing.Size(140, 21)
        Me.cmbPlotNo.TabIndex = 6
        Me.Ttip.SetToolTip(Me.cmbPlotNo, "Select PlotNo :")
        '
        'cmbSector
        '
        Me.cmbSector.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSector.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSector.FormattingEnabled = True
        Me.cmbSector.Location = New System.Drawing.Point(453, 75)
        Me.cmbSector.Name = "cmbSector"
        Me.cmbSector.Size = New System.Drawing.Size(140, 21)
        Me.cmbSector.TabIndex = 8
        Me.Ttip.SetToolTip(Me.cmbSector, "Select Sector :")
        '
        'cmbBlock
        '
        Me.cmbBlock.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbBlock.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBlock.FormattingEnabled = True
        Me.cmbBlock.Location = New System.Drawing.Point(599, 77)
        Me.cmbBlock.Name = "cmbBlock"
        Me.cmbBlock.Size = New System.Drawing.Size(140, 21)
        Me.cmbBlock.TabIndex = 10
        Me.Ttip.SetToolTip(Me.cmbBlock, "Select Block :")
        '
        'cmbPlotSize
        '
        Me.cmbPlotSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPlotSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPlotSize.FormattingEnabled = True
        Me.cmbPlotSize.Location = New System.Drawing.Point(160, 123)
        Me.cmbPlotSize.Name = "cmbPlotSize"
        Me.cmbPlotSize.Size = New System.Drawing.Size(140, 21)
        Me.cmbPlotSize.TabIndex = 16
        Me.Ttip.SetToolTip(Me.cmbPlotSize, "Select PlotSize :")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(451, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Sector *"
        '
        'dtpEntryDate
        '
        Me.dtpEntryDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpEntryDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEntryDate.Location = New System.Drawing.Point(160, 75)
        Me.dtpEntryDate.Name = "dtpEntryDate"
        Me.dtpEntryDate.Size = New System.Drawing.Size(141, 23)
        Me.dtpEntryDate.TabIndex = 4
        '
        'txtBlock
        '
        Me.txtBlock.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtBlock.Location = New System.Drawing.Point(716, 238)
        Me.txtBlock.Name = "txtBlock"
        Me.txtBlock.Size = New System.Drawing.Size(142, 23)
        Me.txtBlock.TabIndex = 9
        Me.txtBlock.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(599, 55)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 15)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Block *"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(158, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Plot Size *"
        '
        'txtPlotSize
        '
        Me.txtPlotSize.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPlotSize.Location = New System.Drawing.Point(717, 256)
        Me.txtPlotSize.Name = "txtPlotSize"
        Me.txtPlotSize.Size = New System.Drawing.Size(141, 23)
        Me.txtPlotSize.TabIndex = 15
        Me.txtPlotSize.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(452, 102)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 15)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Source"
        '
        'txtSource
        '
        Me.txtSource.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSource.Location = New System.Drawing.Point(455, 120)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(141, 23)
        Me.txtSource.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(747, 102)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(99, 15)
        Me.Label13.TabIndex = 23
        Me.Label13.Text = "Demand Amount"
        '
        'txtDemandAmount
        '
        Me.txtDemandAmount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtDemandAmount.Location = New System.Drawing.Point(750, 120)
        Me.txtDemandAmount.Name = "txtDemandAmount"
        Me.txtDemandAmount.Size = New System.Drawing.Size(141, 23)
        Me.txtDemandAmount.TabIndex = 24
        '
        'cbActive
        '
        Me.cbActive.AutoSize = True
        Me.cbActive.Checked = True
        Me.cbActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbActive.Location = New System.Drawing.Point(603, 208)
        Me.cbActive.Name = "cbActive"
        Me.cbActive.Size = New System.Drawing.Size(56, 17)
        Me.cbActive.TabIndex = 27
        Me.cbActive.Text = "Active"
        Me.cbActive.UseVisualStyleBackColor = True
        '
        'lblNumberConvert
        '
        Me.lblNumberConvert.AutoSize = True
        Me.lblNumberConvert.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblNumberConvert.Location = New System.Drawing.Point(747, 154)
        Me.lblNumberConvert.Name = "lblNumberConvert"
        Me.lblNumberConvert.Size = New System.Drawing.Size(10, 13)
        Me.lblNumberConvert.TabIndex = 25
        Me.lblNumberConvert.Text = "-"
        '
        'Feature
        '
        Me.Feature.AutoSize = True
        Me.Feature.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Feature.Location = New System.Drawing.Point(747, 59)
        Me.Feature.Name = "Feature"
        Me.Feature.Size = New System.Drawing.Size(54, 15)
        Me.Feature.TabIndex = 11
        Me.Feature.Text = "Feature *"
        '
        'cmbFeature
        '
        Me.cmbFeature.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFeature.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFeature.FormattingEnabled = True
        Me.cmbFeature.Location = New System.Drawing.Point(750, 77)
        Me.cmbFeature.Name = "cmbFeature"
        Me.cmbFeature.Size = New System.Drawing.Size(140, 21)
        Me.cmbFeature.TabIndex = 12
        '
        'lblSourceMobile
        '
        Me.lblSourceMobile.AutoSize = True
        Me.lblSourceMobile.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSourceMobile.Location = New System.Drawing.Point(596, 102)
        Me.lblSourceMobile.Name = "lblSourceMobile"
        Me.lblSourceMobile.Size = New System.Drawing.Size(102, 15)
        Me.lblSourceMobile.TabIndex = 21
        Me.lblSourceMobile.Text = "Source Mobile No"
        '
        'txtSourceMobileNo
        '
        Me.txtSourceMobileNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSourceMobileNo.Location = New System.Drawing.Point(602, 120)
        Me.txtSourceMobileNo.Name = "txtSourceMobileNo"
        Me.txtSourceMobileNo.Size = New System.Drawing.Size(141, 23)
        Me.txtSourceMobileNo.TabIndex = 22
        '
        'frmProItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 350)
        Me.Controls.Add(Me.cmbPlotSize)
        Me.Controls.Add(Me.lblSourceMobile)
        Me.Controls.Add(Me.txtSourceMobileNo)
        Me.Controls.Add(Me.cmbBlock)
        Me.Controls.Add(Me.cmbSector)
        Me.Controls.Add(Me.cmbPlotNo)
        Me.Controls.Add(Me.cmbTitle)
        Me.Controls.Add(Me.cmbFeature)
        Me.Controls.Add(Me.Feature)
        Me.Controls.Add(Me.lblNumberConvert)
        Me.Controls.Add(Me.cbActive)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtDemandAmount)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPlotSize)
        Me.Controls.Add(Me.txtBlock)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.dtpEntryDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmbTerritory)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbPropertyType)
        Me.Controls.Add(Me.txtSector)
        Me.Controls.Add(Me.txtPlotNo)
        Me.Controls.Add(Me.Label4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item"
        Me.Panel2.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbTerritory As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbPropertyType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSector As System.Windows.Forms.TextBox
    Friend WithEvents txtPlotNo As System.Windows.Forms.TextBox
    Friend WithEvents Ttip As System.Windows.Forms.ToolTip
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpEntryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtBlock As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPlotSize As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDemandAmount As System.Windows.Forms.TextBox
    Friend WithEvents cbActive As System.Windows.Forms.CheckBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblNumberConvert As System.Windows.Forms.Label
    Friend WithEvents Feature As System.Windows.Forms.Label
    Friend WithEvents cmbFeature As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTitle As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPlotNo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSector As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBlock As System.Windows.Forms.ComboBox
    Friend WithEvents lblSourceMobile As System.Windows.Forms.Label
    Friend WithEvents txtSourceMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbPlotSize As System.Windows.Forms.ComboBox
End Class
