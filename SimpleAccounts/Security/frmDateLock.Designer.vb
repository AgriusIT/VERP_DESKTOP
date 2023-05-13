<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDateLock
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDateLock))
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.gboxDateLock = New System.Windows.Forms.GroupBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.dtpDateLock = New System.Windows.Forms.DateTimePicker()
        Me.txtNUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblNoOfDays = New System.Windows.Forms.Label()
        Me.btnLock = New System.Windows.Forms.Button()
        Me.rbtnRelevant = New System.Windows.Forms.RadioButton()
        Me.btnDateUnLock = New System.Windows.Forms.Button()
        Me.rbtnFixed = New System.Windows.Forms.RadioButton()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdHistory = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.bgwDateLock = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.gboxDateLock.SuspendLayout()
        CType(Me.txtNUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.gboxDateLock)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(431, 253)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(431, 43)
        Me.pnlHeader.TabIndex = 14
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(11, 11)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(155, 36)
        Me.lblHeader.TabIndex = 12
        Me.lblHeader.Text = "Date Lock"
        '
        'gboxDateLock
        '
        Me.gboxDateLock.BackColor = System.Drawing.Color.Transparent
        Me.gboxDateLock.Controls.Add(Me.lblType)
        Me.gboxDateLock.Controls.Add(Me.dtpDateLock)
        Me.gboxDateLock.Controls.Add(Me.txtNUpDown)
        Me.gboxDateLock.Controls.Add(Me.Label2)
        Me.gboxDateLock.Controls.Add(Me.lblNoOfDays)
        Me.gboxDateLock.Controls.Add(Me.btnLock)
        Me.gboxDateLock.Controls.Add(Me.rbtnRelevant)
        Me.gboxDateLock.Controls.Add(Me.btnDateUnLock)
        Me.gboxDateLock.Controls.Add(Me.rbtnFixed)
        Me.gboxDateLock.Location = New System.Drawing.Point(11, 47)
        Me.gboxDateLock.Name = "gboxDateLock"
        Me.gboxDateLock.Size = New System.Drawing.Size(332, 145)
        Me.gboxDateLock.TabIndex = 13
        Me.gboxDateLock.TabStop = False
        Me.gboxDateLock.Text = "Date Lock"
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(16, 30)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(43, 20)
        Me.lblType.TabIndex = 9
        Me.lblType.Text = "Type"
        '
        'dtpDateLock
        '
        Me.dtpDateLock.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateLock.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateLock.Location = New System.Drawing.Point(89, 51)
        Me.dtpDateLock.Name = "dtpDateLock"
        Me.dtpDateLock.Size = New System.Drawing.Size(156, 26)
        Me.dtpDateLock.TabIndex = 4
        '
        'txtNUpDown
        '
        Me.txtNUpDown.Enabled = False
        Me.txtNUpDown.Location = New System.Drawing.Point(89, 78)
        Me.txtNUpDown.Name = "txtNUpDown"
        Me.txtNUpDown.Size = New System.Drawing.Size(75, 26)
        Me.txtNUpDown.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Date"
        '
        'lblNoOfDays
        '
        Me.lblNoOfDays.AutoSize = True
        Me.lblNoOfDays.Location = New System.Drawing.Point(16, 80)
        Me.lblNoOfDays.Name = "lblNoOfDays"
        Me.lblNoOfDays.Size = New System.Drawing.Size(90, 20)
        Me.lblNoOfDays.TabIndex = 5
        Me.lblNoOfDays.Text = "No Of Days"
        '
        'btnLock
        '
        Me.btnLock.Location = New System.Drawing.Point(89, 104)
        Me.btnLock.Name = "btnLock"
        Me.btnLock.Size = New System.Drawing.Size(75, 23)
        Me.btnLock.TabIndex = 7
        Me.btnLock.Text = "Lock"
        Me.btnLock.UseVisualStyleBackColor = True
        '
        'rbtnRelevant
        '
        Me.rbtnRelevant.AutoSize = True
        Me.rbtnRelevant.Location = New System.Drawing.Point(145, 28)
        Me.rbtnRelevant.Name = "rbtnRelevant"
        Me.rbtnRelevant.Size = New System.Drawing.Size(97, 24)
        Me.rbtnRelevant.TabIndex = 2
        Me.rbtnRelevant.TabStop = True
        Me.rbtnRelevant.Text = "Relevant"
        Me.rbtnRelevant.UseVisualStyleBackColor = True
        '
        'btnDateUnLock
        '
        Me.btnDateUnLock.Location = New System.Drawing.Point(170, 104)
        Me.btnDateUnLock.Name = "btnDateUnLock"
        Me.btnDateUnLock.Size = New System.Drawing.Size(75, 23)
        Me.btnDateUnLock.TabIndex = 8
        Me.btnDateUnLock.Text = "Un Lock"
        Me.btnDateUnLock.UseVisualStyleBackColor = True
        '
        'rbtnFixed
        '
        Me.rbtnFixed.AutoSize = True
        Me.rbtnFixed.Checked = True
        Me.rbtnFixed.Location = New System.Drawing.Point(89, 28)
        Me.rbtnFixed.Name = "rbtnFixed"
        Me.rbtnFixed.Size = New System.Drawing.Size(72, 24)
        Me.rbtnFixed.TabIndex = 1
        Me.rbtnFixed.TabStop = True
        Me.rbtnFixed.Text = "Fixed"
        Me.rbtnFixed.UseVisualStyleBackColor = True
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdHistory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(431, 253)
        '
        'grdHistory
        '
        Me.grdHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdHistory.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdHistory.GroupByBoxVisible = False
        Me.grdHistory.Location = New System.Drawing.Point(0, 0)
        Me.grdHistory.Name = "grdHistory"
        Me.grdHistory.RecordNavigator = True
        Me.grdHistory.Size = New System.Drawing.Size(431, 253)
        Me.grdHistory.TabIndex = 2
        Me.grdHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(433, 280)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Date Lock"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(431, 253)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(433, 32)
        Me.ToolStrip1.TabIndex = 14
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "New"
        '
        'frmDateLock
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(433, 312)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDateLock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Date Lock "
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.gboxDateLock.ResumeLayout(False)
        Me.gboxDateLock.PerformLayout()
        CType(Me.txtNUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents gboxDateLock As System.Windows.Forms.GroupBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents dtpDateLock As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblNoOfDays As System.Windows.Forms.Label
    Friend WithEvents btnLock As System.Windows.Forms.Button
    Friend WithEvents rbtnRelevant As System.Windows.Forms.RadioButton
    Friend WithEvents btnDateUnLock As System.Windows.Forms.Button
    Friend WithEvents rbtnFixed As System.Windows.Forms.RadioButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Private WithEvents bgwDateLock As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents grdHistory As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
