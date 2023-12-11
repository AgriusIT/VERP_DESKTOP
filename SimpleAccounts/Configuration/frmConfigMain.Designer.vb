<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfigMain))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnBarCode = New System.Windows.Forms.Button()
        Me.btnApproval = New System.Windows.Forms.Button()
        Me.btnProperty = New System.Windows.Forms.Button()
        Me.btnDB = New System.Windows.Forms.Button()
        Me.btnInventory = New System.Windows.Forms.Button()
        Me.btnPurchase = New System.Windows.Forms.Button()
        Me.btnSales = New System.Windows.Forms.Button()
        Me.btnEmail = New System.Windows.Forms.Button()
        Me.btnCompany = New System.Windows.Forms.Button()
        Me.btnHR = New System.Windows.Forms.Button()
        Me.btnImports = New System.Windows.Forms.Button()
        Me.btnAccounts = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlSubNav = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlLoadForm = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.pnlSubNav.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnBarCode)
        Me.Panel1.Controls.Add(Me.btnApproval)
        Me.Panel1.Controls.Add(Me.btnProperty)
        Me.Panel1.Controls.Add(Me.btnDB)
        Me.Panel1.Controls.Add(Me.btnInventory)
        Me.Panel1.Controls.Add(Me.btnPurchase)
        Me.Panel1.Controls.Add(Me.btnSales)
        Me.Panel1.Controls.Add(Me.btnEmail)
        Me.Panel1.Controls.Add(Me.btnCompany)
        Me.Panel1.Controls.Add(Me.btnHR)
        Me.Panel1.Controls.Add(Me.btnImports)
        Me.Panel1.Controls.Add(Me.btnAccounts)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(57, 729)
        Me.Panel1.TabIndex = 0
        '
        'btnBarCode
        '
        Me.btnBarCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBarCode.Image = CType(resources.GetObject("btnBarCode.Image"), System.Drawing.Image)
        Me.btnBarCode.Location = New System.Drawing.Point(0, 639)
        Me.btnBarCode.Name = "btnBarCode"
        Me.btnBarCode.Size = New System.Drawing.Size(54, 49)
        Me.btnBarCode.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.btnBarCode, "Approval Configuration")
        Me.btnBarCode.UseVisualStyleBackColor = True
        '
        'btnApproval
        '
        Me.btnApproval.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApproval.Image = CType(resources.GetObject("btnApproval.Image"), System.Drawing.Image)
        Me.btnApproval.Location = New System.Drawing.Point(0, 584)
        Me.btnApproval.Name = "btnApproval"
        Me.btnApproval.Size = New System.Drawing.Size(54, 49)
        Me.btnApproval.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.btnApproval, "Approval Configuration")
        Me.btnApproval.UseVisualStyleBackColor = True
        '
        'btnProperty
        '
        Me.btnProperty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProperty.Image = CType(resources.GetObject("btnProperty.Image"), System.Drawing.Image)
        Me.btnProperty.Location = New System.Drawing.Point(0, 526)
        Me.btnProperty.Name = "btnProperty"
        Me.btnProperty.Size = New System.Drawing.Size(54, 49)
        Me.btnProperty.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.btnProperty, "Other Configuration")
        Me.btnProperty.UseVisualStyleBackColor = True
        '
        'btnDB
        '
        Me.btnDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDB.Image = CType(resources.GetObject("btnDB.Image"), System.Drawing.Image)
        Me.btnDB.Location = New System.Drawing.Point(0, 468)
        Me.btnDB.Name = "btnDB"
        Me.btnDB.Size = New System.Drawing.Size(54, 49)
        Me.btnDB.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.btnDB, "Database Configuration")
        Me.btnDB.UseVisualStyleBackColor = True
        '
        'btnInventory
        '
        Me.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventory.Image = CType(resources.GetObject("btnInventory.Image"), System.Drawing.Image)
        Me.btnInventory.Location = New System.Drawing.Point(1, 178)
        Me.btnInventory.Name = "btnInventory"
        Me.btnInventory.Size = New System.Drawing.Size(54, 49)
        Me.btnInventory.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnInventory, "Inventory Configuration")
        Me.btnInventory.UseVisualStyleBackColor = True
        '
        'btnPurchase
        '
        Me.btnPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPurchase.Image = CType(resources.GetObject("btnPurchase.Image"), System.Drawing.Image)
        Me.btnPurchase.Location = New System.Drawing.Point(1, 120)
        Me.btnPurchase.Name = "btnPurchase"
        Me.btnPurchase.Size = New System.Drawing.Size(54, 49)
        Me.btnPurchase.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnPurchase, "Purchase Configuration")
        Me.btnPurchase.UseVisualStyleBackColor = True
        '
        'btnSales
        '
        Me.btnSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSales.Image = CType(resources.GetObject("btnSales.Image"), System.Drawing.Image)
        Me.btnSales.Location = New System.Drawing.Point(2, 63)
        Me.btnSales.Name = "btnSales"
        Me.btnSales.Size = New System.Drawing.Size(54, 49)
        Me.btnSales.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btnSales, "Sales Configuration")
        Me.btnSales.UseVisualStyleBackColor = True
        '
        'btnEmail
        '
        Me.btnEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEmail.Image = CType(resources.GetObject("btnEmail.Image"), System.Drawing.Image)
        Me.btnEmail.Location = New System.Drawing.Point(0, 410)
        Me.btnEmail.Name = "btnEmail"
        Me.btnEmail.Size = New System.Drawing.Size(54, 49)
        Me.btnEmail.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnEmail, "Email Configuration")
        Me.btnEmail.UseVisualStyleBackColor = True
        '
        'btnCompany
        '
        Me.btnCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCompany.Image = CType(resources.GetObject("btnCompany.Image"), System.Drawing.Image)
        Me.btnCompany.Location = New System.Drawing.Point(0, 352)
        Me.btnCompany.Name = "btnCompany"
        Me.btnCompany.Size = New System.Drawing.Size(54, 49)
        Me.btnCompany.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnCompany, "Company Configuration")
        Me.btnCompany.UseVisualStyleBackColor = True
        '
        'btnHR
        '
        Me.btnHR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHR.Image = CType(resources.GetObject("btnHR.Image"), System.Drawing.Image)
        Me.btnHR.Location = New System.Drawing.Point(0, 294)
        Me.btnHR.Name = "btnHR"
        Me.btnHR.Size = New System.Drawing.Size(54, 49)
        Me.btnHR.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btnHR, "HR Configuration")
        Me.btnHR.UseVisualStyleBackColor = True
        '
        'btnImports
        '
        Me.btnImports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImports.Image = CType(resources.GetObject("btnImports.Image"), System.Drawing.Image)
        Me.btnImports.Location = New System.Drawing.Point(1, 237)
        Me.btnImports.Name = "btnImports"
        Me.btnImports.Size = New System.Drawing.Size(54, 49)
        Me.btnImports.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnImports, "Imports Configuration")
        Me.btnImports.UseVisualStyleBackColor = True
        '
        'btnAccounts
        '
        Me.btnAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAccounts.Image = CType(resources.GetObject("btnAccounts.Image"), System.Drawing.Image)
        Me.btnAccounts.Location = New System.Drawing.Point(2, 5)
        Me.btnAccounts.Name = "btnAccounts"
        Me.btnAccounts.Size = New System.Drawing.Size(54, 49)
        Me.btnAccounts.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnAccounts, "Accounts Configuration")
        Me.btnAccounts.UseVisualStyleBackColor = True
        '
        'pnlSubNav
        '
        Me.pnlSubNav.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSubNav.Controls.Add(Me.Label4)
        Me.pnlSubNav.Controls.Add(Me.Label3)
        Me.pnlSubNav.Controls.Add(Me.Label2)
        Me.pnlSubNav.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSubNav.Location = New System.Drawing.Point(57, 0)
        Me.pnlSubNav.Name = "pnlSubNav"
        Me.pnlSubNav.Size = New System.Drawing.Size(181, 729)
        Me.pnlSubNav.TabIndex = 1
        Me.pnlSubNav.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Gray
        Me.Label4.Location = New System.Drawing.Point(23, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 21)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "SMS"
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Gray
        Me.Label3.Location = New System.Drawing.Point(23, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 21)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Security Rights"
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(23, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 21)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Company"
        Me.Label2.Visible = False
        '
        'pnlLoadForm
        '
        Me.pnlLoadForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLoadForm.Location = New System.Drawing.Point(238, 0)
        Me.pnlLoadForm.Name = "pnlLoadForm"
        Me.pnlLoadForm.Size = New System.Drawing.Size(770, 729)
        Me.pnlLoadForm.TabIndex = 2
        '
        'frmConfigMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.pnlLoadForm)
        Me.Controls.Add(Me.pnlSubNav)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmConfigMain"
        Me.Text = "Main Configuration"
        Me.Panel1.ResumeLayout(False)
        Me.pnlSubNav.ResumeLayout(False)
        Me.pnlSubNav.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnAccounts As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnImports As System.Windows.Forms.Button
    Friend WithEvents btnSales As System.Windows.Forms.Button
    Friend WithEvents btnPurchase As System.Windows.Forms.Button
    Friend WithEvents btnInventory As System.Windows.Forms.Button
    Friend WithEvents btnHR As System.Windows.Forms.Button
    Friend WithEvents btnCompany As System.Windows.Forms.Button
    Friend WithEvents btnEmail As System.Windows.Forms.Button
    Friend WithEvents btnApproval As System.Windows.Forms.Button
    Friend WithEvents btnProperty As System.Windows.Forms.Button
    Friend WithEvents btnDB As System.Windows.Forms.Button
    Friend WithEvents pnlSubNav As System.Windows.Forms.Panel
    Friend WithEvents pnlLoadForm As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnBarCode As System.Windows.Forms.Button
End Class
