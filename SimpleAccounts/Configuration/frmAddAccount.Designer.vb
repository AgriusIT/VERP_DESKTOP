<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddAccount
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
        Dim grdAddAccount_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddAccount))
        Me.grdAddAccount = New Janus.Windows.GridEX.GridEX()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.grdAddAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdAddAccount
        '
        Me.grdAddAccount.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAddAccount.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdAddAccount_DesignTimeLayout.LayoutString = resources.GetString("grdAddAccount_DesignTimeLayout.LayoutString")
        Me.grdAddAccount.DesignTimeLayout = grdAddAccount_DesignTimeLayout
        Me.grdAddAccount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAddAccount.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAddAccount.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAddAccount.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdAddAccount.GroupByBoxVisible = False
        Me.grdAddAccount.Location = New System.Drawing.Point(0, 0)
        Me.grdAddAccount.Name = "grdAddAccount"
        Me.grdAddAccount.RecordNavigator = True
        Me.grdAddAccount.Size = New System.Drawing.Size(370, 447)
        Me.grdAddAccount.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdAddAccount, "Select Employee Payable Account")
        Me.grdAddAccount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmAddAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 447)
        Me.Controls.Add(Me.grdAddAccount)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Account"
        CType(Me.grdAddAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdAddAccount As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
