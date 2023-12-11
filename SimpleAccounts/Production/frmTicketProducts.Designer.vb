<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTicketProducts
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
        Dim grdItems_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTicketProducts))
        Me.grdItems = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnHeader = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbStage = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbTicketNo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbPlanNo = New System.Windows.Forms.ComboBox()
        Me.lblSalesOrder = New System.Windows.Forms.Label()
        Me.cmbSalesOrder = New System.Windows.Forms.ComboBox()
        Me.BtnLoad = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdItems
        '
        Me.grdItems.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdItems_DesignTimeLayout.LayoutString = resources.GetString("grdItems_DesignTimeLayout.LayoutString")
        Me.grdItems.DesignTimeLayout = grdItems_DesignTimeLayout
        Me.grdItems.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdItems.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdItems.GroupByBoxVisible = False
        Me.grdItems.Location = New System.Drawing.Point(0, 108)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndMoveToFirstCellInNewRow
        Me.grdItems.RecordNavigator = True
        Me.grdItems.Size = New System.Drawing.Size(902, 320)
        Me.grdItems.TabIndex = 6
        Me.grdItems.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnSearch)
        Me.pnlHeader.Controls.Add(Me.btnHeader)
        Me.pnlHeader.Controls.Add(Me.Button8)
        Me.pnlHeader.Controls.Add(Me.Button6)
        Me.pnlHeader.Controls.Add(Me.Button1)
        Me.pnlHeader.Controls.Add(Me.Button4)
        Me.pnlHeader.Controls.Add(Me.Button7)
        Me.pnlHeader.Controls.Add(Me.Button3)
        Me.pnlHeader.Controls.Add(Me.Button2)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(902, 42)
        Me.pnlHeader.TabIndex = 18
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(500, 7)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(72, 25)
        Me.btnSearch.TabIndex = 23
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnSearch.Visible = False
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
        Me.Button8.Location = New System.Drawing.Point(749, 9)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(25, 23)
        Me.Button8.TabIndex = 3
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
        Me.Button6.Location = New System.Drawing.Point(859, 6)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(27, 30)
        Me.Button6.TabIndex = 8
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(835, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 26)
        Me.Button1.TabIndex = 7
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(813, 9)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(28, 24)
        Me.Button4.TabIndex = 6
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"), System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.ForeColor = System.Drawing.SystemColors.Control
        Me.Button7.Location = New System.Drawing.Point(728, 10)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(20, 22)
        Me.Button7.TabIndex = 2
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
        Me.Button3.Location = New System.Drawing.Point(792, 9)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(25, 23)
        Me.Button3.TabIndex = 5
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
        Me.Button2.Location = New System.Drawing.Point(772, 9)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(29, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(174, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Product Search"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(629, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 15)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Stage"
        '
        'cmbStage
        '
        Me.cmbStage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStage.FormattingEnabled = True
        Me.cmbStage.Location = New System.Drawing.Point(632, 71)
        Me.cmbStage.Name = "cmbStage"
        Me.cmbStage.Size = New System.Drawing.Size(163, 23)
        Me.cmbStage.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.cmbStage, "Stage")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(421, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 15)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Ticket No"
        '
        'cmbTicketNo
        '
        Me.cmbTicketNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTicketNo.FormattingEnabled = True
        Me.cmbTicketNo.Location = New System.Drawing.Point(424, 71)
        Me.cmbTicketNo.Name = "cmbTicketNo"
        Me.cmbTicketNo.Size = New System.Drawing.Size(163, 23)
        Me.cmbTicketNo.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.cmbTicketNo, "Ticket No")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(210, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 15)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Plan No"
        '
        'cmbPlanNo
        '
        Me.cmbPlanNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPlanNo.FormattingEnabled = True
        Me.cmbPlanNo.Location = New System.Drawing.Point(213, 71)
        Me.cmbPlanNo.Name = "cmbPlanNo"
        Me.cmbPlanNo.Size = New System.Drawing.Size(163, 23)
        Me.cmbPlanNo.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.cmbPlanNo, "Plan No")
        '
        'lblSalesOrder
        '
        Me.lblSalesOrder.AutoSize = True
        Me.lblSalesOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSalesOrder.Location = New System.Drawing.Point(5, 53)
        Me.lblSalesOrder.Name = "lblSalesOrder"
        Me.lblSalesOrder.Size = New System.Drawing.Size(72, 15)
        Me.lblSalesOrder.TabIndex = 24
        Me.lblSalesOrder.Text = "Sales Order"
        '
        'cmbSalesOrder
        '
        Me.cmbSalesOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesOrder.FormattingEnabled = True
        Me.cmbSalesOrder.Location = New System.Drawing.Point(8, 71)
        Me.cmbSalesOrder.Name = "cmbSalesOrder"
        Me.cmbSalesOrder.Size = New System.Drawing.Size(163, 23)
        Me.cmbSalesOrder.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.cmbSalesOrder, "Sales Order")
        '
        'BtnLoad
        '
        Me.BtnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoad.Location = New System.Drawing.Point(826, 68)
        Me.BtnLoad.Name = "BtnLoad"
        Me.BtnLoad.Size = New System.Drawing.Size(72, 29)
        Me.BtnLoad.TabIndex = 28
        Me.BtnLoad.Text = "Load"
        Me.ToolTip1.SetToolTip(Me.BtnLoad, "Load")
        Me.BtnLoad.UseVisualStyleBackColor = True
        '
        'frmTicketProducts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(902, 429)
        Me.Controls.Add(Me.BtnLoad)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbStage)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbTicketNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbPlanNo)
        Me.Controls.Add(Me.lblSalesOrder)
        Me.Controls.Add(Me.cmbSalesOrder)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grdItems)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTicketProducts"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Product Search"
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdItems As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnHeader As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbStage As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTicketNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbPlanNo As System.Windows.Forms.ComboBox
    Friend WithEvents lblSalesOrder As System.Windows.Forms.Label
    Friend WithEvents cmbSalesOrder As System.Windows.Forms.ComboBox
    Friend WithEvents BtnLoad As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
