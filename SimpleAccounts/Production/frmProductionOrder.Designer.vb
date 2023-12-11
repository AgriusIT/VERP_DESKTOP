<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductionOrder))
        Dim grdInput_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdOverHeads_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleId")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleCode")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdLabourType_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand3 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdOutput_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand4 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn15 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn16 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand5 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn17 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn18 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn19 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn20 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand6 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn21 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn22 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn49 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn50 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn51 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn52 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.txtProductionOrderNo = New System.Windows.Forms.TextBox()
        Me.txtTicketNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpProductionDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtBatchSize = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbSection = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.grdInput = New Janus.Windows.GridEX.GridEX()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.txtTotalQty = New System.Windows.Forms.TextBox()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.lblTotalQty = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.txtStock = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.rdoCode = New System.Windows.Forms.RadioButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnAddProductionOverHeads = New System.Windows.Forms.Button()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.cmbAccount = New System.Windows.Forms.ComboBox()
        Me.grdOverHeads = New Janus.Windows.GridEX.GridEX()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btnAddLabourAllocation = New System.Windows.Forms.Button()
        Me.cmbLabourType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtAmount1 = New System.Windows.Forms.TextBox()
        Me.grdLabourType = New Janus.Windows.GridEX.GridEX()
        Me.lblPerUnitRate = New System.Windows.Forms.Label()
        Me.lblByProductLabourType = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbName1 = New System.Windows.Forms.RadioButton()
        Me.rbCode1 = New System.Windows.Forms.RadioButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbByProduct = New System.Windows.Forms.RadioButton()
        Me.rbFinish = New System.Windows.Forms.RadioButton()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtTotalQty1 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtStock1 = New System.Windows.Forms.TextBox()
        Me.cmbCategory1 = New System.Windows.Forms.ComboBox()
        Me.cmbUnit1 = New System.Windows.Forms.ComboBox()
        Me.txtPackQty1 = New System.Windows.Forms.TextBox()
        Me.txtQty1 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtRate1 = New System.Windows.Forms.TextBox()
        Me.cmbItem1 = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtTotal1 = New System.Windows.Forms.TextBox()
        Me.btnAdd1 = New System.Windows.Forms.Button()
        Me.grdOutput = New Janus.Windows.GridEX.GridEX()
        Me.cbApproved = New System.Windows.Forms.CheckBox()
        Me.cmbProductProduced = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbBOM = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbCGAccount = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblCostOfGoodsAccount = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtTotalQuantity = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rbName2 = New System.Windows.Forms.RadioButton()
        Me.rbCode2 = New System.Windows.Forms.RadioButton()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rbName3 = New System.Windows.Forms.RadioButton()
        Me.rbCode3 = New System.Windows.Forms.RadioButton()
        Me.lblTotalQuantity = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdOverHeads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.cmbLabourType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLabourType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.cmbItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProductProduced, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCGAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Controls.Add(Me.BtnPrint)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 828)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1234, 112)
        Me.Panel2.TabIndex = 23
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnPrint.FlatAppearance.BorderSize = 0
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.ForeColor = System.Drawing.Color.White
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(1036, 12)
        Me.BtnPrint.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(81, 95)
        Me.BtnPrint.TabIndex = 2
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(936, 12)
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
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(1132, 12)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(81, 95)
        Me.btnSave.TabIndex = 0
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(394, 220)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 2
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccount.Location = New System.Drawing.Point(10, 9)
        Me.lblAccount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(81, 28)
        Me.lblAccount.TabIndex = 0
        Me.lblAccount.Text = "Plan No"
        '
        'txtProductionOrderNo
        '
        Me.txtProductionOrderNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductionOrderNo.Location = New System.Drawing.Point(14, 40)
        Me.txtProductionOrderNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProductionOrderNo.Name = "txtProductionOrderNo"
        Me.txtProductionOrderNo.ReadOnly = True
        Me.txtProductionOrderNo.Size = New System.Drawing.Size(262, 33)
        Me.txtProductionOrderNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtProductionOrderNo, "Select plan no")
        '
        'txtTicketNo
        '
        Me.txtTicketNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTicketNo.Location = New System.Drawing.Point(296, 40)
        Me.txtTicketNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTicketNo.Name = "txtTicketNo"
        Me.txtTicketNo.ReadOnly = True
        Me.txtTicketNo.Size = New System.Drawing.Size(262, 33)
        Me.txtTicketNo.TabIndex = 3
        Me.txtTicketNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTicketNo, "Select ticket no")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(296, 9)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 28)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Ticket No"
        '
        'dtpProductionDate
        '
        Me.dtpProductionDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpProductionDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpProductionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpProductionDate.Location = New System.Drawing.Point(578, 40)
        Me.dtpProductionDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpProductionDate.Name = "dtpProductionDate"
        Me.dtpProductionDate.Size = New System.Drawing.Size(170, 33)
        Me.dtpProductionDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpProductionDate, "Select production date")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(573, 9)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 28)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Production Date"
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.Location = New System.Drawing.Point(768, 40)
        Me.txtBatchNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.Size = New System.Drawing.Size(232, 33)
        Me.txtBatchNo.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtBatchNo, "Type batch no")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(764, 9)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 28)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Batch No"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(1017, 9)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 28)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Expiry Date"
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpExpiryDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(1022, 40)
        Me.dtpExpiryDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(170, 33)
        Me.dtpExpiryDate.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.dtpExpiryDate, "Select expirty date")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(9, 91)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 28)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Product"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(291, 91)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 28)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "BOM"
        '
        'txtBatchSize
        '
        Me.txtBatchSize.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchSize.Location = New System.Drawing.Point(578, 126)
        Me.txtBatchSize.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBatchSize.Name = "txtBatchSize"
        Me.txtBatchSize.ReadOnly = True
        Me.txtBatchSize.Size = New System.Drawing.Size(170, 33)
        Me.txtBatchSize.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtBatchSize, "Type batch size")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(576, 94)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 28)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Batch Size"
        '
        'cmbSection
        '
        Me.cmbSection.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSection.FormattingEnabled = True
        Me.cmbSection.Location = New System.Drawing.Point(768, 126)
        Me.cmbSection.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSection.Name = "cmbSection"
        Me.cmbSection.Size = New System.Drawing.Size(232, 36)
        Me.cmbSection.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.cmbSection, "Select section no")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(764, 91)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 28)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Section"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(14, 211)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(540, 35)
        Me.txtRemarks.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Type remarks here")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(10, 172)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 28)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Remarks"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(0, 335)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1234, 465)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblProgress)
        Me.TabPage1.Controls.Add(Me.grdInput)
        Me.TabPage1.Controls.Add(Me.Panel7)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage1.Size = New System.Drawing.Size(1226, 432)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Input Material"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grdInput
        '
        Me.grdInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdInput_DesignTimeLayout.LayoutString = resources.GetString("grdInput_DesignTimeLayout.LayoutString")
        Me.grdInput.DesignTimeLayout = grdInput_DesignTimeLayout
        Me.grdInput.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdInput.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdInput.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdInput.GroupByBoxVisible = False
        Me.grdInput.Location = New System.Drawing.Point(4, 198)
        Me.grdInput.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdInput.Name = "grdInput"
        Me.grdInput.RecordNavigator = True
        Me.grdInput.Size = New System.Drawing.Size(1214, 218)
        Me.grdInput.TabIndex = 1
        Me.grdInput.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Panel7
        '
        Me.Panel7.AutoScroll = True
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel7.Controls.Add(Me.txtTotalQty)
        Me.Panel7.Controls.Add(Me.cmbCategory)
        Me.Panel7.Controls.Add(Me.lblTotalQty)
        Me.Panel7.Controls.Add(Me.Label16)
        Me.Panel7.Controls.Add(Me.Label61)
        Me.Panel7.Controls.Add(Me.txtTotal)
        Me.Panel7.Controls.Add(Me.Label15)
        Me.Panel7.Controls.Add(Me.txtRate)
        Me.Panel7.Controls.Add(Me.txtStock)
        Me.Panel7.Controls.Add(Me.Label12)
        Me.Panel7.Controls.Add(Me.Label14)
        Me.Panel7.Controls.Add(Me.Label13)
        Me.Panel7.Controls.Add(Me.cmbUnit)
        Me.Panel7.Controls.Add(Me.txtQty)
        Me.Panel7.Controls.Add(Me.txtPackQty)
        Me.Panel7.Controls.Add(Me.Label18)
        Me.Panel7.Controls.Add(Me.btnAdd)
        Me.Panel7.Controls.Add(Me.lblStock)
        Me.Panel7.Controls.Add(Me.rdoName)
        Me.Panel7.Controls.Add(Me.cmbItem)
        Me.Panel7.Controls.Add(Me.rdoCode)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(4, 5)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1218, 183)
        Me.Panel7.TabIndex = 0
        '
        'txtTotalQty
        '
        Me.txtTotalQty.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalQty.Location = New System.Drawing.Point(272, 128)
        Me.txtTotalQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.Size = New System.Drawing.Size(148, 33)
        Me.txtTotalQty.TabIndex = 15
        Me.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(14, 42)
        Me.cmbCategory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(406, 36)
        Me.cmbCategory.TabIndex = 1
        '
        'lblTotalQty
        '
        Me.lblTotalQty.AutoSize = True
        Me.lblTotalQty.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalQty.Location = New System.Drawing.Point(267, 100)
        Me.lblTotalQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(91, 28)
        Me.lblTotalQty.TabIndex = 14
        Me.lblTotalQty.Text = "Total Qty"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(438, 9)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(51, 28)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Item"
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(9, 94)
        Me.Label61.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(78, 28)
        Me.Label61.TabIndex = 10
        Me.Label61.Text = "Pck Qty"
        '
        'txtTotal
        '
        Me.txtTotal.Enabled = False
        Me.txtTotal.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(540, 128)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(139, 33)
        Me.txtTotal.TabIndex = 19
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1022, 6)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 28)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "Unit"
        '
        'txtRate
        '
        Me.txtRate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(442, 128)
        Me.txtRate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(74, 33)
        Me.txtRate.TabIndex = 17
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStock
        '
        Me.txtStock.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStock.Location = New System.Drawing.Point(872, 42)
        Me.txtStock.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtStock.Name = "txtStock"
        Me.txtStock.ReadOnly = True
        Me.txtStock.Size = New System.Drawing.Size(132, 33)
        Me.txtStock.TabIndex = 7
        Me.txtStock.TabStop = False
        Me.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(537, 94)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(54, 28)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "Total"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(138, 97)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 28)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Qty"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(440, 95)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 28)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Rate"
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbUnit.Location = New System.Drawing.Point(1026, 42)
        Me.cmbUnit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(132, 36)
        Me.cmbUnit.TabIndex = 9
        '
        'txtQty
        '
        Me.txtQty.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.Location = New System.Drawing.Point(142, 128)
        Me.txtQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(106, 33)
        Me.txtQty.TabIndex = 13
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPackQty
        '
        Me.txtPackQty.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackQty.Location = New System.Drawing.Point(14, 128)
        Me.txtPackQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(104, 33)
        Me.txtPackQty.TabIndex = 11
        Me.txtPackQty.TabStop = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(9, 9)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(87, 28)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Location"
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.White
        Me.btnAdd.Location = New System.Drawing.Point(700, 128)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 40)
        Me.btnAdd.TabIndex = 20
        Me.btnAdd.Text = "Add"
        Me.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'lblStock
        '
        Me.lblStock.AutoSize = True
        Me.lblStock.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStock.Location = New System.Drawing.Point(867, 9)
        Me.lblStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(60, 28)
        Me.lblStock.TabIndex = 6
        Me.lblStock.Text = "Stock"
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(573, 9)
        Me.rdoName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(76, 24)
        Me.rdoName.TabIndex = 5
        Me.rdoName.Text = "Name"
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'cmbItem
        '
        Me.cmbItem.AlwaysInEditMode = True
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.TextHAlignAsString = "Left"
        Appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance5
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance6.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbItem.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbItem.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbItem.EditAreaDisplayStyle = Infragistics.Win.UltraWinGrid.EditAreaDisplayStyle.DisplayText
        Me.cmbItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItem.LimitToList = True
        Me.cmbItem.Location = New System.Drawing.Point(442, 42)
        Me.cmbItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(408, 37)
        Me.cmbItem.TabIndex = 3
        '
        'rdoCode
        '
        Me.rdoCode.AutoSize = True
        Me.rdoCode.Checked = True
        Me.rdoCode.Location = New System.Drawing.Point(495, 9)
        Me.rdoCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoCode.Name = "rdoCode"
        Me.rdoCode.Size = New System.Drawing.Size(72, 24)
        Me.rdoCode.TabIndex = 4
        Me.rdoCode.TabStop = True
        Me.rdoCode.Text = "Code"
        Me.rdoCode.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnAddProductionOverHeads)
        Me.TabPage2.Controls.Add(Me.txtAmount)
        Me.TabPage2.Controls.Add(Me.cmbAccount)
        Me.TabPage2.Controls.Add(Me.grdOverHeads)
        Me.TabPage2.Controls.Add(Me.lblAmount)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Size = New System.Drawing.Size(1226, 450)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Over Head"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnAddProductionOverHeads
        '
        Me.btnAddProductionOverHeads.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnAddProductionOverHeads.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddProductionOverHeads.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddProductionOverHeads.ForeColor = System.Drawing.Color.White
        Me.btnAddProductionOverHeads.Location = New System.Drawing.Point(408, 51)
        Me.btnAddProductionOverHeads.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddProductionOverHeads.Name = "btnAddProductionOverHeads"
        Me.btnAddProductionOverHeads.Size = New System.Drawing.Size(92, 38)
        Me.btnAddProductionOverHeads.TabIndex = 4
        Me.btnAddProductionOverHeads.Text = "Add"
        Me.btnAddProductionOverHeads.UseVisualStyleBackColor = False
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(268, 51)
        Me.txtAmount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(114, 33)
        Me.txtAmount.TabIndex = 3
        '
        'cmbAccount
        '
        Me.cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAccount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAccount.FormattingEnabled = True
        Me.cmbAccount.Location = New System.Drawing.Point(15, 51)
        Me.cmbAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccount.Name = "cmbAccount"
        Me.cmbAccount.Size = New System.Drawing.Size(228, 36)
        Me.cmbAccount.TabIndex = 1
        '
        'grdOverHeads
        '
        Me.grdOverHeads.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdOverHeads_DesignTimeLayout.LayoutString = resources.GetString("grdOverHeads_DesignTimeLayout.LayoutString")
        Me.grdOverHeads.DesignTimeLayout = grdOverHeads_DesignTimeLayout
        Me.grdOverHeads.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdOverHeads.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdOverHeads.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdOverHeads.GroupByBoxVisible = False
        Me.grdOverHeads.Location = New System.Drawing.Point(0, 98)
        Me.grdOverHeads.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdOverHeads.Name = "grdOverHeads"
        Me.grdOverHeads.RecordNavigator = True
        Me.grdOverHeads.Size = New System.Drawing.Size(1208, 336)
        Me.grdOverHeads.TabIndex = 5
        Me.grdOverHeads.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(262, 20)
        Me.lblAmount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(83, 28)
        Me.lblAmount.TabIndex = 2
        Me.lblAmount.Text = "Amount"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(10, 20)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 28)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Account"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnAddLabourAllocation)
        Me.TabPage3.Controls.Add(Me.cmbLabourType)
        Me.TabPage3.Controls.Add(Me.txtAmount1)
        Me.TabPage3.Controls.Add(Me.grdLabourType)
        Me.TabPage3.Controls.Add(Me.lblPerUnitRate)
        Me.TabPage3.Controls.Add(Me.lblByProductLabourType)
        Me.TabPage3.Location = New System.Drawing.Point(4, 29)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage3.Size = New System.Drawing.Size(1226, 450)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Labour"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnAddLabourAllocation
        '
        Me.btnAddLabourAllocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnAddLabourAllocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddLabourAllocation.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddLabourAllocation.ForeColor = System.Drawing.Color.White
        Me.btnAddLabourAllocation.Location = New System.Drawing.Point(466, 52)
        Me.btnAddLabourAllocation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddLabourAllocation.Name = "btnAddLabourAllocation"
        Me.btnAddLabourAllocation.Size = New System.Drawing.Size(87, 40)
        Me.btnAddLabourAllocation.TabIndex = 4
        Me.btnAddLabourAllocation.Text = "Add"
        Me.btnAddLabourAllocation.UseVisualStyleBackColor = False
        '
        'cmbLabourType
        '
        Me.cmbLabourType.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbLabourType.CheckedListSettings.CheckStateMember = ""
        UltraGridColumn5.Header.VisiblePosition = 0
        UltraGridColumn5.Hidden = True
        UltraGridColumn6.Header.VisiblePosition = 1
        UltraGridColumn7.Header.VisiblePosition = 2
        UltraGridColumn7.Width = 70
        UltraGridColumn8.Header.VisiblePosition = 3
        UltraGridColumn8.Width = 80
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbLabourType.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbLabourType.DisplayLayout.InterBandSpacing = 10
        Me.cmbLabourType.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbLabourType.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbLabourType.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance7.BackColor = System.Drawing.Color.Transparent
        Me.cmbLabourType.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Me.cmbLabourType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbLabourType.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance8.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance8.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance8.ForeColor = System.Drawing.Color.White
        Appearance8.TextHAlignAsString = "Left"
        Appearance8.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbLabourType.DisplayLayout.Override.HeaderAppearance = Appearance8
        Me.cmbLabourType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance9.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbLabourType.DisplayLayout.Override.RowAppearance = Appearance9
        Appearance10.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance10.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbLabourType.DisplayLayout.Override.RowSelectorAppearance = Appearance10
        Me.cmbLabourType.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbLabourType.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance11.ForeColor = System.Drawing.Color.Black
        Me.cmbLabourType.DisplayLayout.Override.SelectedRowAppearance = Appearance11
        Me.cmbLabourType.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbLabourType.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbLabourType.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbLabourType.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbLabourType.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbLabourType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbLabourType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbLabourType.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbLabourType.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbLabourType.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLabourType.LimitToList = True
        Me.cmbLabourType.Location = New System.Drawing.Point(9, 52)
        Me.cmbLabourType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbLabourType.MaxDropDownItems = 20
        Me.cmbLabourType.Name = "cmbLabourType"
        Me.cmbLabourType.Size = New System.Drawing.Size(262, 37)
        Me.cmbLabourType.TabIndex = 1
        '
        'txtAmount1
        '
        Me.txtAmount1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount1.Location = New System.Drawing.Point(290, 52)
        Me.txtAmount1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAmount1.Name = "txtAmount1"
        Me.txtAmount1.Size = New System.Drawing.Size(157, 33)
        Me.txtAmount1.TabIndex = 3
        '
        'grdLabourType
        '
        Me.grdLabourType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdLabourType_DesignTimeLayout.LayoutString = resources.GetString("grdLabourType_DesignTimeLayout.LayoutString")
        Me.grdLabourType.DesignTimeLayout = grdLabourType_DesignTimeLayout
        Me.grdLabourType.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLabourType.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLabourType.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLabourType.GroupByBoxVisible = False
        Me.grdLabourType.Location = New System.Drawing.Point(9, 103)
        Me.grdLabourType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdLabourType.Name = "grdLabourType"
        Me.grdLabourType.RecordNavigator = True
        Me.grdLabourType.Size = New System.Drawing.Size(1204, 331)
        Me.grdLabourType.TabIndex = 5
        Me.grdLabourType.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblPerUnitRate
        '
        Me.lblPerUnitRate.AutoSize = True
        Me.lblPerUnitRate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerUnitRate.Location = New System.Drawing.Point(290, 25)
        Me.lblPerUnitRate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPerUnitRate.Name = "lblPerUnitRate"
        Me.lblPerUnitRate.Size = New System.Drawing.Size(83, 28)
        Me.lblPerUnitRate.TabIndex = 2
        Me.lblPerUnitRate.Text = "Amount"
        '
        'lblByProductLabourType
        '
        Me.lblByProductLabourType.AutoSize = True
        Me.lblByProductLabourType.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblByProductLabourType.Location = New System.Drawing.Point(4, 18)
        Me.lblByProductLabourType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblByProductLabourType.Name = "lblByProductLabourType"
        Me.lblByProductLabourType.Size = New System.Drawing.Size(119, 28)
        Me.lblByProductLabourType.TabIndex = 0
        Me.lblByProductLabourType.Text = "Labour Type"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Panel8)
        Me.TabPage4.Controls.Add(Me.grdOutput)
        Me.TabPage4.Location = New System.Drawing.Point(4, 29)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage4.Size = New System.Drawing.Size(1226, 450)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Output"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Panel8
        '
        Me.Panel8.AutoScroll = True
        Me.Panel8.Controls.Add(Me.Panel4)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.Panel3)
        Me.Panel8.Controls.Add(Me.Label26)
        Me.Panel8.Controls.Add(Me.Label25)
        Me.Panel8.Controls.Add(Me.txtTotalQty1)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.Label17)
        Me.Panel8.Controls.Add(Me.Label23)
        Me.Panel8.Controls.Add(Me.Label19)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.txtStock1)
        Me.Panel8.Controls.Add(Me.cmbCategory1)
        Me.Panel8.Controls.Add(Me.cmbUnit1)
        Me.Panel8.Controls.Add(Me.txtPackQty1)
        Me.Panel8.Controls.Add(Me.txtQty1)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.txtRate1)
        Me.Panel8.Controls.Add(Me.cmbItem1)
        Me.Panel8.Controls.Add(Me.txtTotal1)
        Me.Panel8.Controls.Add(Me.btnAdd1)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(4, 5)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1218, 163)
        Me.Panel8.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbName1)
        Me.Panel4.Controls.Add(Me.rbCode1)
        Me.Panel4.Location = New System.Drawing.Point(266, 6)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(166, 28)
        Me.Panel4.TabIndex = 4
        '
        'rbName1
        '
        Me.rbName1.AutoSize = True
        Me.rbName1.Location = New System.Drawing.Point(90, 0)
        Me.rbName1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbName1.Name = "rbName1"
        Me.rbName1.Size = New System.Drawing.Size(76, 24)
        Me.rbName1.TabIndex = 1
        Me.rbName1.Text = "Name"
        Me.rbName1.UseVisualStyleBackColor = True
        '
        'rbCode1
        '
        Me.rbCode1.AutoSize = True
        Me.rbCode1.Checked = True
        Me.rbCode1.Location = New System.Drawing.Point(6, 0)
        Me.rbCode1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbCode1.Name = "rbCode1"
        Me.rbCode1.Size = New System.Drawing.Size(72, 24)
        Me.rbCode1.TabIndex = 0
        Me.rbCode1.TabStop = True
        Me.rbCode1.Text = "Code"
        Me.rbCode1.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(9, 8)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(87, 28)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Location"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbByProduct)
        Me.Panel3.Controls.Add(Me.rbFinish)
        Me.Panel3.Location = New System.Drawing.Point(459, 40)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(222, 35)
        Me.Panel3.TabIndex = 5
        '
        'rbByProduct
        '
        Me.rbByProduct.AutoSize = True
        Me.rbByProduct.Location = New System.Drawing.Point(102, 5)
        Me.rbByProduct.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbByProduct.Name = "rbByProduct"
        Me.rbByProduct.Size = New System.Drawing.Size(111, 24)
        Me.rbByProduct.TabIndex = 1
        Me.rbByProduct.Text = "By Product"
        Me.rbByProduct.UseVisualStyleBackColor = True
        '
        'rbFinish
        '
        Me.rbFinish.AutoSize = True
        Me.rbFinish.Checked = True
        Me.rbFinish.Location = New System.Drawing.Point(3, 5)
        Me.rbFinish.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbFinish.Name = "rbFinish"
        Me.rbFinish.Size = New System.Drawing.Size(76, 24)
        Me.rbFinish.TabIndex = 0
        Me.rbFinish.TabStop = True
        Me.rbFinish.Text = "Finish"
        Me.rbFinish.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(178, 9)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(51, 28)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "Item"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(812, 6)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(49, 28)
        Me.Label25.TabIndex = 8
        Me.Label25.Text = "Unit"
        '
        'txtTotalQty1
        '
        Me.txtTotalQty1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalQty1.Location = New System.Drawing.Point(4, 117)
        Me.txtTotalQty1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotalQty1.Name = "txtTotalQty1"
        Me.txtTotalQty1.Size = New System.Drawing.Size(151, 33)
        Me.txtTotalQty1.TabIndex = 15
        Me.txtTotalQty1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(1060, 3)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(44, 28)
        Me.Label24.TabIndex = 12
        Me.Label24.Text = "Qty"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(9, 86)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(91, 28)
        Me.Label17.TabIndex = 14
        Me.Label17.Text = "Total Qty"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(176, 86)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(51, 28)
        Me.Label23.TabIndex = 16
        Me.Label23.Text = "Rate"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(946, 5)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(78, 28)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "Pck Qty"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(278, 86)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(54, 28)
        Me.Label22.TabIndex = 18
        Me.Label22.Text = "Total"
        '
        'txtStock1
        '
        Me.txtStock1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStock1.Location = New System.Drawing.Point(705, 38)
        Me.txtStock1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtStock1.Name = "txtStock1"
        Me.txtStock1.ReadOnly = True
        Me.txtStock1.Size = New System.Drawing.Size(88, 33)
        Me.txtStock1.TabIndex = 7
        Me.txtStock1.TabStop = False
        Me.txtStock1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbCategory1
        '
        Me.cmbCategory1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory1.FormattingEnabled = True
        Me.cmbCategory1.Location = New System.Drawing.Point(8, 38)
        Me.cmbCategory1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCategory1.Name = "cmbCategory1"
        Me.cmbCategory1.Size = New System.Drawing.Size(148, 36)
        Me.cmbCategory1.TabIndex = 1
        '
        'cmbUnit1
        '
        Me.cmbUnit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUnit1.FormattingEnabled = True
        Me.cmbUnit1.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbUnit1.Location = New System.Drawing.Point(819, 38)
        Me.cmbUnit1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbUnit1.Name = "cmbUnit1"
        Me.cmbUnit1.Size = New System.Drawing.Size(110, 36)
        Me.cmbUnit1.TabIndex = 9
        '
        'txtPackQty1
        '
        Me.txtPackQty1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackQty1.Location = New System.Drawing.Point(956, 38)
        Me.txtPackQty1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPackQty1.Name = "txtPackQty1"
        Me.txtPackQty1.Size = New System.Drawing.Size(79, 33)
        Me.txtPackQty1.TabIndex = 11
        Me.txtPackQty1.TabStop = False
        '
        'txtQty1
        '
        Me.txtQty1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty1.Location = New System.Drawing.Point(1060, 38)
        Me.txtQty1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQty1.Name = "txtQty1"
        Me.txtQty1.Size = New System.Drawing.Size(80, 33)
        Me.txtQty1.TabIndex = 13
        Me.txtQty1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(699, 6)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(60, 28)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "Stock"
        '
        'txtRate1
        '
        Me.txtRate1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate1.Location = New System.Drawing.Point(180, 117)
        Me.txtRate1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRate1.Name = "txtRate1"
        Me.txtRate1.Size = New System.Drawing.Size(74, 33)
        Me.txtRate1.TabIndex = 17
        Me.txtRate1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbItem1
        '
        Me.cmbItem1.AlwaysInEditMode = True
        Me.cmbItem1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem1.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbItem1.CheckedListSettings.CheckStateMember = ""
        Appearance12.BackColor = System.Drawing.Color.White
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem1.DisplayLayout.Appearance = Appearance12
        UltraGridColumn9.Header.VisiblePosition = 0
        UltraGridColumn9.Hidden = True
        UltraGridColumn10.Header.VisiblePosition = 1
        UltraGridColumn11.Header.VisiblePosition = 2
        UltraGridColumn12.Header.VisiblePosition = 3
        UltraGridBand3.Columns.AddRange(New Object() {UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12})
        Me.cmbItem1.DisplayLayout.BandsSerializer.Add(UltraGridBand3)
        Me.cmbItem1.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance13.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem1.DisplayLayout.Override.CardAreaAppearance = Appearance13
        Me.cmbItem1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem1.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance14.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance14.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.ForeColor = System.Drawing.Color.White
        Appearance14.TextHAlignAsString = "Left"
        Appearance14.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem1.DisplayLayout.Override.HeaderAppearance = Appearance14
        Me.cmbItem1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance15.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem1.DisplayLayout.Override.RowAppearance = Appearance15
        Appearance16.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance16.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem1.DisplayLayout.Override.RowSelectorAppearance = Appearance16
        Me.cmbItem1.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem1.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance17.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance17.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance17.ForeColor = System.Drawing.Color.Black
        Me.cmbItem1.DisplayLayout.Override.SelectedRowAppearance = Appearance17
        Me.cmbItem1.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem1.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem1.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbItem1.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbItem1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem1.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbItem1.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbItem1.EditAreaDisplayStyle = Infragistics.Win.UltraWinGrid.EditAreaDisplayStyle.DisplayText
        Me.cmbItem1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItem1.LimitToList = True
        Me.cmbItem1.Location = New System.Drawing.Point(182, 37)
        Me.cmbItem1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbItem1.MaxDropDownItems = 20
        Me.cmbItem1.Name = "cmbItem1"
        Me.cmbItem1.Size = New System.Drawing.Size(254, 37)
        Me.cmbItem1.TabIndex = 3
        '
        'txtTotal1
        '
        Me.txtTotal1.Enabled = False
        Me.txtTotal1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal1.Location = New System.Drawing.Point(282, 117)
        Me.txtTotal1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotal1.Name = "txtTotal1"
        Me.txtTotal1.ReadOnly = True
        Me.txtTotal1.Size = New System.Drawing.Size(145, 33)
        Me.txtTotal1.TabIndex = 19
        Me.txtTotal1.TabStop = False
        Me.txtTotal1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnAdd1
        '
        Me.btnAdd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnAdd1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd1.ForeColor = System.Drawing.Color.White
        Me.btnAdd1.Location = New System.Drawing.Point(452, 117)
        Me.btnAdd1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd1.Name = "btnAdd1"
        Me.btnAdd1.Size = New System.Drawing.Size(87, 40)
        Me.btnAdd1.TabIndex = 20
        Me.btnAdd1.Text = "Add"
        Me.btnAdd1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAdd1.UseVisualStyleBackColor = False
        '
        'grdOutput
        '
        Me.grdOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdOutput_DesignTimeLayout.LayoutString = resources.GetString("grdOutput_DesignTimeLayout.LayoutString")
        Me.grdOutput.DesignTimeLayout = grdOutput_DesignTimeLayout
        Me.grdOutput.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdOutput.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdOutput.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdOutput.GroupByBoxVisible = False
        Me.grdOutput.Location = New System.Drawing.Point(0, 177)
        Me.grdOutput.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdOutput.Name = "grdOutput"
        Me.grdOutput.RecordNavigator = True
        Me.grdOutput.Size = New System.Drawing.Size(1208, 257)
        Me.grdOutput.TabIndex = 1
        Me.grdOutput.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'cbApproved
        '
        Me.cbApproved.AutoSize = True
        Me.cbApproved.Checked = True
        Me.cbApproved.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbApproved.Location = New System.Drawing.Point(1022, 218)
        Me.cbApproved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbApproved.Name = "cbApproved"
        Me.cbApproved.Size = New System.Drawing.Size(103, 24)
        Me.cbApproved.TabIndex = 26
        Me.cbApproved.Text = "Approved"
        Me.ToolTip1.SetToolTip(Me.cbApproved, "Approved")
        Me.cbApproved.UseVisualStyleBackColor = True
        '
        'cmbProductProduced
        '
        Me.cmbProductProduced.AlwaysInEditMode = True
        Me.cmbProductProduced.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbProductProduced.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbProductProduced.CheckedListSettings.CheckStateMember = ""
        Appearance18.BackColor = System.Drawing.Color.White
        Appearance18.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbProductProduced.DisplayLayout.Appearance = Appearance18
        UltraGridColumn13.Header.VisiblePosition = 0
        UltraGridColumn13.Hidden = True
        UltraGridColumn14.Header.VisiblePosition = 1
        UltraGridColumn15.Header.VisiblePosition = 2
        UltraGridColumn16.Header.VisiblePosition = 3
        UltraGridBand4.Columns.AddRange(New Object() {UltraGridColumn13, UltraGridColumn14, UltraGridColumn15, UltraGridColumn16})
        Me.cmbProductProduced.DisplayLayout.BandsSerializer.Add(UltraGridBand4)
        Me.cmbProductProduced.DisplayLayout.InterBandSpacing = 10
        Me.cmbProductProduced.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbProductProduced.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbProductProduced.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance19.BackColor = System.Drawing.Color.Transparent
        Me.cmbProductProduced.DisplayLayout.Override.CardAreaAppearance = Appearance19
        Me.cmbProductProduced.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbProductProduced.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance20.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance20.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance20.ForeColor = System.Drawing.Color.White
        Appearance20.TextHAlignAsString = "Left"
        Appearance20.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbProductProduced.DisplayLayout.Override.HeaderAppearance = Appearance20
        Me.cmbProductProduced.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance21.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbProductProduced.DisplayLayout.Override.RowAppearance = Appearance21
        Appearance22.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance22.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbProductProduced.DisplayLayout.Override.RowSelectorAppearance = Appearance22
        Me.cmbProductProduced.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbProductProduced.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance23.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance23.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance23.ForeColor = System.Drawing.Color.Black
        Me.cmbProductProduced.DisplayLayout.Override.SelectedRowAppearance = Appearance23
        Me.cmbProductProduced.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbProductProduced.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbProductProduced.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbProductProduced.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbProductProduced.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbProductProduced.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbProductProduced.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbProductProduced.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbProductProduced.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbProductProduced.EditAreaDisplayStyle = Infragistics.Win.UltraWinGrid.EditAreaDisplayStyle.DisplayText
        Me.cmbProductProduced.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProductProduced.LimitToList = True
        Me.cmbProductProduced.Location = New System.Drawing.Point(14, 126)
        Me.cmbProductProduced.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbProductProduced.MaxDropDownItems = 20
        Me.cmbProductProduced.Name = "cmbProductProduced"
        Me.cmbProductProduced.Size = New System.Drawing.Size(264, 37)
        Me.cmbProductProduced.TabIndex = 11
        '
        'cmbBOM
        '
        Me.cmbBOM.AlwaysInEditMode = True
        Me.cmbBOM.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbBOM.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbBOM.CheckedListSettings.CheckStateMember = ""
        Appearance24.BackColor = System.Drawing.Color.White
        Appearance24.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbBOM.DisplayLayout.Appearance = Appearance24
        UltraGridColumn17.Header.VisiblePosition = 0
        UltraGridColumn17.Hidden = True
        UltraGridColumn18.Header.VisiblePosition = 1
        UltraGridColumn19.Header.VisiblePosition = 2
        UltraGridColumn20.Header.VisiblePosition = 3
        UltraGridBand5.Columns.AddRange(New Object() {UltraGridColumn17, UltraGridColumn18, UltraGridColumn19, UltraGridColumn20})
        Me.cmbBOM.DisplayLayout.BandsSerializer.Add(UltraGridBand5)
        Me.cmbBOM.DisplayLayout.InterBandSpacing = 10
        Me.cmbBOM.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbBOM.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbBOM.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance25.BackColor = System.Drawing.Color.Transparent
        Me.cmbBOM.DisplayLayout.Override.CardAreaAppearance = Appearance25
        Me.cmbBOM.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbBOM.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance26.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance26.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance26.ForeColor = System.Drawing.Color.White
        Appearance26.TextHAlignAsString = "Left"
        Appearance26.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbBOM.DisplayLayout.Override.HeaderAppearance = Appearance26
        Me.cmbBOM.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance27.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbBOM.DisplayLayout.Override.RowAppearance = Appearance27
        Appearance28.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance28.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbBOM.DisplayLayout.Override.RowSelectorAppearance = Appearance28
        Me.cmbBOM.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbBOM.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance29.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance29.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance29.ForeColor = System.Drawing.Color.Black
        Me.cmbBOM.DisplayLayout.Override.SelectedRowAppearance = Appearance29
        Me.cmbBOM.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBOM.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBOM.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbBOM.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbBOM.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbBOM.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbBOM.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbBOM.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbBOM.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbBOM.EditAreaDisplayStyle = Infragistics.Win.UltraWinGrid.EditAreaDisplayStyle.DisplayText
        Me.cmbBOM.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBOM.LimitToList = True
        Me.cmbBOM.Location = New System.Drawing.Point(296, 126)
        Me.cmbBOM.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbBOM.MaxDropDownItems = 20
        Me.cmbBOM.Name = "cmbBOM"
        Me.cmbBOM.Size = New System.Drawing.Size(262, 37)
        Me.cmbBOM.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmbBOM, "Select finish good")
        '
        'cmbCGAccount
        '
        Me.cmbCGAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbCGAccount.CheckedListSettings.CheckStateMember = ""
        Appearance30.BackColor = System.Drawing.Color.White
        Appearance30.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbCGAccount.DisplayLayout.Appearance = Appearance30
        UltraGridColumn21.Header.VisiblePosition = 0
        UltraGridColumn21.Hidden = True
        UltraGridColumn22.Header.VisiblePosition = 1
        UltraGridColumn22.Width = 141
        UltraGridColumn49.Header.VisiblePosition = 2
        UltraGridColumn50.Header.VisiblePosition = 3
        UltraGridColumn51.Header.VisiblePosition = 4
        UltraGridColumn52.Header.VisiblePosition = 5
        UltraGridColumn52.Hidden = True
        UltraGridBand6.Columns.AddRange(New Object() {UltraGridColumn21, UltraGridColumn22, UltraGridColumn49, UltraGridColumn50, UltraGridColumn51, UltraGridColumn52})
        Appearance31.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        UltraGridBand6.Override.HeaderAppearance = Appearance31
        Appearance32.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance32.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        UltraGridBand6.Override.SelectedRowAppearance = Appearance32
        Me.cmbCGAccount.DisplayLayout.BandsSerializer.Add(UltraGridBand6)
        Me.cmbCGAccount.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCGAccount.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCGAccount.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCGAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance33.BackColor = System.Drawing.Color.Transparent
        Me.cmbCGAccount.DisplayLayout.Override.CardAreaAppearance = Appearance33
        Me.cmbCGAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCGAccount.DisplayLayout.Override.CellPadding = 3
        Me.cmbCGAccount.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance34.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance34.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance34.TextHAlignAsString = "Left"
        Me.cmbCGAccount.DisplayLayout.Override.HeaderAppearance = Appearance34
        Me.cmbCGAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance35.BorderColor = System.Drawing.Color.LightGray
        Appearance35.TextVAlignAsString = "Middle"
        Me.cmbCGAccount.DisplayLayout.Override.RowAppearance = Appearance35
        Appearance36.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance36.BackColor2 = System.Drawing.Color.White
        Appearance36.BorderColor = System.Drawing.Color.Black
        Appearance36.ForeColor = System.Drawing.Color.Black
        Me.cmbCGAccount.DisplayLayout.Override.SelectedRowAppearance = Appearance36
        Me.cmbCGAccount.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCGAccount.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCGAccount.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCGAccount.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCGAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCGAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCGAccount.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCGAccount.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCGAccount.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCGAccount.LimitToList = True
        Me.cmbCGAccount.Location = New System.Drawing.Point(768, 211)
        Me.cmbCGAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCGAccount.Name = "cmbCGAccount"
        Me.cmbCGAccount.Size = New System.Drawing.Size(234, 33)
        Me.cmbCGAccount.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.cmbCGAccount, "Select cgs account")
        '
        'lblCostOfGoodsAccount
        '
        Me.lblCostOfGoodsAccount.AutoSize = True
        Me.lblCostOfGoodsAccount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostOfGoodsAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCostOfGoodsAccount.Location = New System.Drawing.Point(765, 177)
        Me.lblCostOfGoodsAccount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostOfGoodsAccount.Name = "lblCostOfGoodsAccount"
        Me.lblCostOfGoodsAccount.Size = New System.Drawing.Size(209, 28)
        Me.lblCostOfGoodsAccount.TabIndex = 24
        Me.lblCostOfGoodsAccount.Text = "Cost Of Good Account"
        '
        'txtTotalQuantity
        '
        Me.txtTotalQuantity.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalQuantity.Location = New System.Drawing.Point(578, 211)
        Me.txtTotalQuantity.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotalQuantity.Multiline = True
        Me.txtTotalQuantity.Name = "txtTotalQuantity"
        Me.txtTotalQuantity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalQuantity.Size = New System.Drawing.Size(170, 35)
        Me.txtTotalQuantity.TabIndex = 23
        Me.txtTotalQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTotalQuantity, "Type remarks here")
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.rbName2)
        Me.Panel5.Controls.Add(Me.rbCode2)
        Me.Panel5.Location = New System.Drawing.Point(92, 91)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(186, 29)
        Me.Panel5.TabIndex = 12
        '
        'rbName2
        '
        Me.rbName2.AutoSize = True
        Me.rbName2.Location = New System.Drawing.Point(104, 0)
        Me.rbName2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbName2.Name = "rbName2"
        Me.rbName2.Size = New System.Drawing.Size(76, 24)
        Me.rbName2.TabIndex = 1
        Me.rbName2.Text = "Name"
        Me.rbName2.UseVisualStyleBackColor = True
        '
        'rbCode2
        '
        Me.rbCode2.AutoSize = True
        Me.rbCode2.Checked = True
        Me.rbCode2.Location = New System.Drawing.Point(8, 0)
        Me.rbCode2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbCode2.Name = "rbCode2"
        Me.rbCode2.Size = New System.Drawing.Size(72, 24)
        Me.rbCode2.TabIndex = 0
        Me.rbCode2.TabStop = True
        Me.rbCode2.Text = "Code"
        Me.rbCode2.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rbName3)
        Me.Panel6.Controls.Add(Me.rbCode3)
        Me.Panel6.Location = New System.Drawing.Point(372, 89)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(186, 28)
        Me.Panel6.TabIndex = 15
        '
        'rbName3
        '
        Me.rbName3.AutoSize = True
        Me.rbName3.Location = New System.Drawing.Point(104, 0)
        Me.rbName3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbName3.Name = "rbName3"
        Me.rbName3.Size = New System.Drawing.Size(76, 24)
        Me.rbName3.TabIndex = 1
        Me.rbName3.Text = "Name"
        Me.rbName3.UseVisualStyleBackColor = True
        '
        'rbCode3
        '
        Me.rbCode3.AutoSize = True
        Me.rbCode3.Checked = True
        Me.rbCode3.Location = New System.Drawing.Point(8, 0)
        Me.rbCode3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbCode3.Name = "rbCode3"
        Me.rbCode3.Size = New System.Drawing.Size(72, 24)
        Me.rbCode3.TabIndex = 0
        Me.rbCode3.TabStop = True
        Me.rbCode3.Text = "Code"
        Me.rbCode3.UseVisualStyleBackColor = True
        '
        'lblTotalQuantity
        '
        Me.lblTotalQuantity.AutoSize = True
        Me.lblTotalQuantity.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalQuantity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotalQuantity.Location = New System.Drawing.Point(578, 180)
        Me.lblTotalQuantity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotalQuantity.Name = "lblTotalQuantity"
        Me.lblTotalQuantity.Size = New System.Drawing.Size(135, 28)
        Me.lblTotalQuantity.TabIndex = 22
        Me.lblTotalQuantity.Text = "Total Quantity"
        '
        'Panel9
        '
        Me.Panel9.AutoScroll = True
        Me.Panel9.Controls.Add(Me.lblAccount)
        Me.Panel9.Controls.Add(Me.lblTotalQuantity)
        Me.Panel9.Controls.Add(Me.txtProductionOrderNo)
        Me.Panel9.Controls.Add(Me.txtTotalQuantity)
        Me.Panel9.Controls.Add(Me.Panel6)
        Me.Panel9.Controls.Add(Me.lblCostOfGoodsAccount)
        Me.Panel9.Controls.Add(Me.Label2)
        Me.Panel9.Controls.Add(Me.cmbCGAccount)
        Me.Panel9.Controls.Add(Me.Panel5)
        Me.Panel9.Controls.Add(Me.cbApproved)
        Me.Panel9.Controls.Add(Me.txtTicketNo)
        Me.Panel9.Controls.Add(Me.cmbBOM)
        Me.Panel9.Controls.Add(Me.dtpProductionDate)
        Me.Panel9.Controls.Add(Me.Label3)
        Me.Panel9.Controls.Add(Me.Label4)
        Me.Panel9.Controls.Add(Me.cmbProductProduced)
        Me.Panel9.Controls.Add(Me.txtBatchNo)
        Me.Panel9.Controls.Add(Me.dtpExpiryDate)
        Me.Panel9.Controls.Add(Me.txtRemarks)
        Me.Panel9.Controls.Add(Me.Label5)
        Me.Panel9.Controls.Add(Me.Label10)
        Me.Panel9.Controls.Add(Me.Label6)
        Me.Panel9.Controls.Add(Me.cmbSection)
        Me.Panel9.Controls.Add(Me.Label7)
        Me.Panel9.Controls.Add(Me.Label9)
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Controls.Add(Me.txtBatchSize)
        Me.Panel9.Location = New System.Drawing.Point(0, 74)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1234, 260)
        Me.Panel9.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(292, 41)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Production Order"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel1.Controls.Add(Me.CtrlGrdBar3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1234, 65)
        Me.Panel1.TabIndex = 0
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Me
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(1174, 12)
        Me.CtrlGrdBar3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar3.MyGrid = Me.grdInput
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar3.TabIndex = 1
        '
        'frmProductionOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1234, 940)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProductionOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdOverHeads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.cmbLabourType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLabourType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.cmbItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOutput, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProductProduced, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCGAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblAccount As System.Windows.Forms.Label
    Friend WithEvents txtProductionOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents txtTicketNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpProductionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBatchSize As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbSection As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents btnAddProductionOverHeads As System.Windows.Forms.Button
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents cmbAccount As System.Windows.Forms.ComboBox
    Friend WithEvents grdOverHeads As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnAddLabourAllocation As System.Windows.Forms.Button
    Friend WithEvents cmbLabourType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txtAmount1 As System.Windows.Forms.TextBox
    Friend WithEvents grdLabourType As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblPerUnitRate As System.Windows.Forms.Label
    Friend WithEvents lblByProductLabourType As System.Windows.Forms.Label
    Friend WithEvents grdInput As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtTotalQty As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalQty As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtStock As System.Windows.Forms.TextBox
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtTotalQty1 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtStock1 As System.Windows.Forms.TextBox
    Friend WithEvents cmbUnit1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtQty1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRate1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal1 As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd1 As System.Windows.Forms.Button
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbCode1 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbItem1 As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtPackQty1 As System.Windows.Forms.TextBox
    Friend WithEvents cmbCategory1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cbApproved As System.Windows.Forms.CheckBox
    Friend WithEvents grdOutput As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbProductProduced As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbBOM As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbByProduct As System.Windows.Forms.RadioButton
    Friend WithEvents rbFinish As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCGAccount As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblCostOfGoodsAccount As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbName1 As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rbName2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbCode2 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rbName3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbCode3 As System.Windows.Forms.RadioButton
    Friend WithEvents txtTotalQuantity As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalQuantity As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
