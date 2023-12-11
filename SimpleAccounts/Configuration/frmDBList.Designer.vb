<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDBList
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
        Dim grdDB_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDBList))
        Me.grdDB = New Janus.Windows.GridEX.GridEX()
        CType(Me.grdDB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdDB
        '
        Me.grdDB.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        resources.ApplyResources(grdDB_DesignTimeLayout, "grdDB_DesignTimeLayout")
        Me.grdDB.DesignTimeLayout = grdDB_DesignTimeLayout
        resources.ApplyResources(Me.grdDB, "grdDB")
        Me.grdDB.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdDB.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdDB.GroupByBoxVisible = False
        Me.grdDB.Name = "grdDB"
        Me.grdDB.RecordNavigator = True
        Me.grdDB.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'frmDBList
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grdDB)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDBList"
        CType(Me.grdDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdDB As Janus.Windows.GridEX.GridEX
End Class
