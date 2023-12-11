<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttachmentViewForApproval
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
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdSaved_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column3.Image")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttachmentViewForApproval))
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdSaved_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdSaved_DesignTimeLayout_Reference_0.Instance"), Object)
        grdSaved_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdSaved_DesignTimeLayout_Reference_0})
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(613, 360)
        Me.grdSaved.TabIndex = 2
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmAttachmentViewForApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(613, 360)
        Me.Controls.Add(Me.grdSaved)
        Me.Name = "frmAttachmentViewForApproval"
        Me.Text = "Attachment View For Approval"
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
End Class
