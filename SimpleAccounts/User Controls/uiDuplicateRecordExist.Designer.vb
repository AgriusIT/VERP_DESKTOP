<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uiDuplicateRecordExist
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.lnkExpectedDuplicateRecords = New System.Windows.Forms.LinkLabel()
        Me.lblExpectedRecords = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'lblAccount
        '
        Me.lblAccount.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAccount.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAccount.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.ForeColor = System.Drawing.SystemColors.Window
        Me.lblAccount.Location = New System.Drawing.Point(0, 0)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(275, 27)
        Me.lblAccount.TabIndex = 13
        Me.lblAccount.Text = "Expected Duplicate Record"
        Me.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkExpectedDuplicateRecords
        '
        Me.lnkExpectedDuplicateRecords.AutoSize = True
        Me.lnkExpectedDuplicateRecords.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkExpectedDuplicateRecords.Location = New System.Drawing.Point(8, 41)
        Me.lnkExpectedDuplicateRecords.Name = "lnkExpectedDuplicateRecords"
        Me.lnkExpectedDuplicateRecords.Size = New System.Drawing.Size(218, 14)
        Me.lnkExpectedDuplicateRecords.TabIndex = 14
        Me.lnkExpectedDuplicateRecords.TabStop = True
        Me.lnkExpectedDuplicateRecords.Text = "No of Expected Duplicate Records"
        '
        'lblExpectedRecords
        '
        Me.lblExpectedRecords.AutoSize = True
        Me.lblExpectedRecords.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpectedRecords.Location = New System.Drawing.Point(232, 41)
        Me.lblExpectedRecords.Name = "lblExpectedRecords"
        Me.lblExpectedRecords.Size = New System.Drawing.Size(15, 14)
        Me.lblExpectedRecords.TabIndex = 15
        Me.lblExpectedRecords.Text = "0"
        '
        'BackgroundWorker1
        '
        '
        'uiDuplicateRecordExist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.Controls.Add(Me.lblExpectedRecords)
        Me.Controls.Add(Me.lnkExpectedDuplicateRecords)
        Me.Controls.Add(Me.lblAccount)
        Me.Name = "uiDuplicateRecordExist"
        Me.Size = New System.Drawing.Size(275, 71)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblAccount As System.Windows.Forms.Label
    Friend WithEvents lnkExpectedDuplicateRecords As System.Windows.Forms.LinkLabel
    Friend WithEvents lblExpectedRecords As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker

End Class
