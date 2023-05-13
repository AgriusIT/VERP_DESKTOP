<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHome
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.flowPanelHome = New System.Windows.Forms.FlowLayoutPanel()
        Me.frmVoucher = New Janus.Windows.EditControls.UIButton()
        Me.frmSales = New Janus.Windows.EditControls.UIButton()
        Me.frmSalesReturn = New Janus.Windows.EditControls.UIButton()
        Me.frmPurchase = New Janus.Windows.EditControls.UIButton()
        Me.frmPurchaseReturn = New Janus.Windows.EditControls.UIButton()
        Me.frmStoreIssuence = New Janus.Windows.EditControls.UIButton()
        Me.frmProductionStore = New Janus.Windows.EditControls.UIButton()
        Me.frmVendorPayment = New Janus.Windows.EditControls.UIButton()
        Me.frmCustomerCollection = New Janus.Windows.EditControls.UIButton()
        Me.frmExpense = New Janus.Windows.EditControls.UIButton()
        Me.frmDetailAccount = New Janus.Windows.EditControls.UIButton()
        Me.frmSubSubAccount = New Janus.Windows.EditControls.UIButton()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.chkIncludeUnPost = New System.Windows.Forms.CheckBox()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker3 = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.bwValidateSystem = New System.ComponentModel.BackgroundWorker()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.flowPanelHome.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.flowPanelHome)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1026, 624)
        '
        'flowPanelHome
        '
        Me.flowPanelHome.AutoSize = True
        Me.flowPanelHome.BackColor = System.Drawing.Color.Transparent
        Me.flowPanelHome.Controls.Add(Me.frmVoucher)
        Me.flowPanelHome.Controls.Add(Me.frmSales)
        Me.flowPanelHome.Controls.Add(Me.frmSalesReturn)
        Me.flowPanelHome.Controls.Add(Me.frmPurchase)
        Me.flowPanelHome.Controls.Add(Me.frmPurchaseReturn)
        Me.flowPanelHome.Controls.Add(Me.frmStoreIssuence)
        Me.flowPanelHome.Controls.Add(Me.frmProductionStore)
        Me.flowPanelHome.Controls.Add(Me.frmVendorPayment)
        Me.flowPanelHome.Controls.Add(Me.frmCustomerCollection)
        Me.flowPanelHome.Controls.Add(Me.frmExpense)
        Me.flowPanelHome.Controls.Add(Me.frmDetailAccount)
        Me.flowPanelHome.Controls.Add(Me.frmSubSubAccount)
        Me.flowPanelHome.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowPanelHome.Location = New System.Drawing.Point(0, 0)
        Me.flowPanelHome.Name = "flowPanelHome"
        Me.flowPanelHome.Padding = New System.Windows.Forms.Padding(20)
        Me.flowPanelHome.Size = New System.Drawing.Size(1026, 624)
        Me.flowPanelHome.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.flowPanelHome, "Frequently Used Item")
        '
        'frmVoucher
        '
        Me.frmVoucher.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmVoucher.Location = New System.Drawing.Point(23, 23)
        Me.frmVoucher.Name = "frmVoucher"
        Me.frmVoucher.Size = New System.Drawing.Size(160, 160)
        Me.frmVoucher.TabIndex = 0
        Me.frmVoucher.Text = "Voucher"
        Me.frmVoucher.Visible = False
        Me.frmVoucher.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmSales
        '
        Me.frmSales.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmSales.Location = New System.Drawing.Point(189, 23)
        Me.frmSales.Name = "frmSales"
        Me.frmSales.Size = New System.Drawing.Size(160, 160)
        Me.frmSales.TabIndex = 1
        Me.frmSales.Text = "Sales"
        Me.frmSales.Visible = False
        Me.frmSales.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmSalesReturn
        '
        Me.frmSalesReturn.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmSalesReturn.Location = New System.Drawing.Point(355, 23)
        Me.frmSalesReturn.Name = "frmSalesReturn"
        Me.frmSalesReturn.Size = New System.Drawing.Size(160, 160)
        Me.frmSalesReturn.TabIndex = 2
        Me.frmSalesReturn.Text = "Sales Return"
        Me.frmSalesReturn.Visible = False
        Me.frmSalesReturn.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmPurchase
        '
        Me.frmPurchase.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmPurchase.Location = New System.Drawing.Point(521, 23)
        Me.frmPurchase.Name = "frmPurchase"
        Me.frmPurchase.Size = New System.Drawing.Size(160, 160)
        Me.frmPurchase.TabIndex = 3
        Me.frmPurchase.Text = "Purchase"
        Me.frmPurchase.Visible = False
        Me.frmPurchase.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmPurchaseReturn
        '
        Me.frmPurchaseReturn.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmPurchaseReturn.Location = New System.Drawing.Point(687, 23)
        Me.frmPurchaseReturn.Name = "frmPurchaseReturn"
        Me.frmPurchaseReturn.Size = New System.Drawing.Size(160, 160)
        Me.frmPurchaseReturn.TabIndex = 6
        Me.frmPurchaseReturn.Text = "Purchase Return"
        Me.frmPurchaseReturn.Visible = False
        Me.frmPurchaseReturn.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmStoreIssuence
        '
        Me.frmStoreIssuence.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmStoreIssuence.Location = New System.Drawing.Point(23, 189)
        Me.frmStoreIssuence.Name = "frmStoreIssuence"
        Me.frmStoreIssuence.Size = New System.Drawing.Size(160, 160)
        Me.frmStoreIssuence.TabIndex = 7
        Me.frmStoreIssuence.Text = "Store Issuance"
        Me.frmStoreIssuence.Visible = False
        Me.frmStoreIssuence.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmProductionStore
        '
        Me.frmProductionStore.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmProductionStore.Location = New System.Drawing.Point(189, 189)
        Me.frmProductionStore.Name = "frmProductionStore"
        Me.frmProductionStore.Size = New System.Drawing.Size(160, 160)
        Me.frmProductionStore.TabIndex = 12
        Me.frmProductionStore.Text = "Production"
        Me.frmProductionStore.Visible = False
        Me.frmProductionStore.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmVendorPayment
        '
        Me.frmVendorPayment.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmVendorPayment.Location = New System.Drawing.Point(355, 189)
        Me.frmVendorPayment.Name = "frmVendorPayment"
        Me.frmVendorPayment.Size = New System.Drawing.Size(160, 160)
        Me.frmVendorPayment.TabIndex = 13
        Me.frmVendorPayment.Text = "Payment"
        Me.frmVendorPayment.Visible = False
        Me.frmVendorPayment.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmCustomerCollection
        '
        Me.frmCustomerCollection.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmCustomerCollection.Location = New System.Drawing.Point(521, 189)
        Me.frmCustomerCollection.Name = "frmCustomerCollection"
        Me.frmCustomerCollection.Size = New System.Drawing.Size(160, 160)
        Me.frmCustomerCollection.TabIndex = 14
        Me.frmCustomerCollection.Text = "Receipt"
        Me.frmCustomerCollection.Visible = False
        Me.frmCustomerCollection.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmExpense
        '
        Me.frmExpense.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmExpense.Location = New System.Drawing.Point(687, 189)
        Me.frmExpense.Name = "frmExpense"
        Me.frmExpense.Size = New System.Drawing.Size(160, 160)
        Me.frmExpense.TabIndex = 15
        Me.frmExpense.Text = "Expense"
        Me.frmExpense.Visible = False
        Me.frmExpense.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmDetailAccount
        '
        Me.frmDetailAccount.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmDetailAccount.Location = New System.Drawing.Point(23, 355)
        Me.frmDetailAccount.Name = "frmDetailAccount"
        Me.frmDetailAccount.Size = New System.Drawing.Size(160, 160)
        Me.frmDetailAccount.TabIndex = 16
        Me.frmDetailAccount.Text = "Account Detail"
        Me.frmDetailAccount.Visible = False
        Me.frmDetailAccount.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmSubSubAccount
        '
        Me.frmSubSubAccount.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmSubSubAccount.Location = New System.Drawing.Point(189, 355)
        Me.frmSubSubAccount.Name = "frmSubSubAccount"
        Me.frmSubSubAccount.Size = New System.Drawing.Size(160, 160)
        Me.frmSubSubAccount.TabIndex = 17
        Me.frmSubSubAccount.Text = "Sub Sub Account"
        Me.frmSubSubAccount.Visible = False
        Me.frmSubSubAccount.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1026, 624)
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1026, 624)
        Me.Panel1.TabIndex = 0
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.AccessibleName = "DateTimePicker1"
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(248, 1)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(120, 26)
        Me.DateTimePicker1.TabIndex = 0
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.AccessibleName = "DateTimePicker2"
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(392, 1)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(120, 26)
        Me.DateTimePicker2.TabIndex = 1
        '
        'chkIncludeUnPost
        '
        Me.chkIncludeUnPost.BackColor = System.Drawing.Color.Transparent
        Me.chkIncludeUnPost.Location = New System.Drawing.Point(518, 1)
        Me.chkIncludeUnPost.Name = "chkIncludeUnPost"
        Me.chkIncludeUnPost.Size = New System.Drawing.Size(169, 23)
        Me.chkIncludeUnPost.TabIndex = 2
        Me.chkIncludeUnPost.Text = "Include Un Posted Voucher"
        Me.chkIncludeUnPost.UseVisualStyleBackColor = False
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(1028, 651)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio
        Me.UltraTabControl1.TabIndex = 3
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Home"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Dash Board"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1026, 624)
        '
        'BackgroundWorker2
        '
        '
        'BackgroundWorker3
        '
        '
        'Timer1
        '
        '
        'bwValidateSystem
        '
        '
        'frmHome
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1028, 651)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Name = "frmHome"
        Me.Text = "Home"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.flowPanelHome.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents flowPanelHome As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents frmVoucher As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmSales As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmSalesReturn As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmPurchase As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmPurchaseReturn As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmStoreIssuence As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmProductionStore As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmVendorPayment As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmCustomerCollection As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmExpense As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmDetailAccount As Janus.Windows.EditControls.UIButton
    Friend WithEvents frmSubSubAccount As Janus.Windows.EditControls.UIButton
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents chkIncludeUnPost As CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents bwValidateSystem As System.ComponentModel.BackgroundWorker
    'Friend WithEvents CtrlStockLevel1 As SimpleAccounts.CtrlStockLevel


End Class
