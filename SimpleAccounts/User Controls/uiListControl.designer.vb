<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uiListControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.ListItem = New System.Windows.Forms.ListBox
        Me.lblHeading = New System.Windows.Forms.Label
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.lblCount = New System.Windows.Forms.Label
        Me.uichkNot = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.btnSelectAll = New System.Windows.Forms.Button
        Me.uibtnAddNew = New System.Windows.Forms.Button
        Me.btnInverse = New System.Windows.Forms.Button
        Me.uibtnResetAll = New System.Windows.Forms.Button
        Me.uibtnMagnifier = New System.Windows.Forms.Button
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.Controls.Add(Me.ListItem, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblHeading, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(117, 269)
        Me.TableLayoutPanel1.TabIndex = 68
        '
        'ListItem
        '
        Me.ListItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListItem.FormattingEnabled = True
        Me.ListItem.Location = New System.Drawing.Point(0, 14)
        Me.ListItem.Margin = New System.Windows.Forms.Padding(0)
        Me.ListItem.Name = "ListItem"
        Me.ListItem.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListItem.Size = New System.Drawing.Size(90, 238)
        Me.ListItem.TabIndex = 68
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHeading.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.Location = New System.Drawing.Point(3, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(84, 14)
        Me.lblHeading.TabIndex = 70
        Me.lblHeading.Text = "Lable Text"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.Controls.Add(Me.lblCount, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.uichkNot, 1, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 252)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(90, 17)
        Me.TableLayoutPanel3.TabIndex = 72
        '
        'lblCount
        '
        Me.lblCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(3, -3)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(41, 20)
        Me.lblCount.TabIndex = 72
        Me.lblCount.Text = "Lable Text"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'uichkNot
        '
        Me.uichkNot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uichkNot.Location = New System.Drawing.Point(47, 0)
        Me.uichkNot.Margin = New System.Windows.Forms.Padding(0)
        Me.uichkNot.Name = "uichkNot"
        Me.uichkNot.Size = New System.Drawing.Size(43, 17)
        Me.uichkNot.TabIndex = 71
        Me.uichkNot.Text = "Not"
        Me.uichkNot.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnSelectAll, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.uibtnAddNew, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.btnInverse, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.uibtnResetAll, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.uibtnMagnifier, 0, 4)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(93, 17)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 7
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(21, 111)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(0, 0)
        Me.btnSelectAll.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(20, 22)
        Me.btnSelectAll.TabIndex = 70
        Me.btnSelectAll.Tag = "HideText"
        Me.btnSelectAll.Text = "A"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        Me.btnSelectAll.Visible = False
        '
        'uibtnAddNew
        '
        Me.uibtnAddNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uibtnAddNew.Location = New System.Drawing.Point(0, 44)
        Me.uibtnAddNew.Margin = New System.Windows.Forms.Padding(0)
        Me.uibtnAddNew.Name = "uibtnAddNew"
        Me.uibtnAddNew.Size = New System.Drawing.Size(20, 22)
        Me.uibtnAddNew.TabIndex = 71
        Me.uibtnAddNew.Tag = "HideText"
        Me.uibtnAddNew.Text = "+"
        Me.uibtnAddNew.UseVisualStyleBackColor = True
        '
        'btnInverse
        '
        Me.btnInverse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInverse.Location = New System.Drawing.Point(0, 22)
        Me.btnInverse.Margin = New System.Windows.Forms.Padding(0)
        Me.btnInverse.Name = "btnInverse"
        Me.btnInverse.Size = New System.Drawing.Size(20, 22)
        Me.btnInverse.TabIndex = 72
        Me.btnInverse.Tag = "HideText"
        Me.btnInverse.Text = "I"
        Me.btnInverse.UseVisualStyleBackColor = True
        Me.btnInverse.Visible = False
        '
        'uibtnResetAll
        '
        Me.uibtnResetAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uibtnResetAll.Location = New System.Drawing.Point(0, 88)
        Me.uibtnResetAll.Margin = New System.Windows.Forms.Padding(0)
        Me.uibtnResetAll.Name = "uibtnResetAll"
        Me.uibtnResetAll.Size = New System.Drawing.Size(20, 22)
        Me.uibtnResetAll.TabIndex = 74
        Me.uibtnResetAll.Tag = "HideText"
        Me.uibtnResetAll.Text = "R"
        Me.uibtnResetAll.UseVisualStyleBackColor = True
        '
        'uibtnMagnifier
        '
        Me.uibtnMagnifier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uibtnMagnifier.Location = New System.Drawing.Point(0, 66)
        Me.uibtnMagnifier.Margin = New System.Windows.Forms.Padding(0)
        Me.uibtnMagnifier.Name = "uibtnMagnifier"
        Me.uibtnMagnifier.Size = New System.Drawing.Size(20, 22)
        Me.uibtnMagnifier.TabIndex = 75
        Me.uibtnMagnifier.Tag = "HideText"
        Me.uibtnMagnifier.Text = "M"
        Me.uibtnMagnifier.UseVisualStyleBackColor = True
        '
        'uiListControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "uiListControl"
        Me.Size = New System.Drawing.Size(117, 269)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ListItem As System.Windows.Forms.ListBox
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents uichkNot As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents uibtnAddNew As System.Windows.Forms.Button
    Friend WithEvents btnInverse As System.Windows.Forms.Button
    Friend WithEvents uibtnResetAll As System.Windows.Forms.Button
    Friend WithEvents uibtnMagnifier As System.Windows.Forms.Button
    Friend WithEvents lblCount As System.Windows.Forms.Label

End Class
