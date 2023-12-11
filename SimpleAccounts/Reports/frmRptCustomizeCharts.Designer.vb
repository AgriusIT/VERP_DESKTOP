<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptCustomizeCharts
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
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.nudTopRecord = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbValue2 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbValue1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbChartType = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudTopRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(900, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Chart1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 107)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(876, 461)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Chart Report"
        '
        'Chart1
        '
        Me.Chart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Chart1.BackColor = System.Drawing.Color.Khaki
        ChartArea4.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea4)
        Legend4.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column
        Legend4.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend4)
        Me.Chart1.Location = New System.Drawing.Point(6, 19)
        Me.Chart1.Name = "Chart1"
        Series4.ChartArea = "ChartArea1"
        Series4.IsValueShownAsLabel = True
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        Me.Chart1.Series.Add(Series4)
        Me.Chart1.Size = New System.Drawing.Size(864, 436)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'nudTopRecord
        '
        Me.nudTopRecord.Location = New System.Drawing.Point(288, 34)
        Me.nudTopRecord.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nudTopRecord.Name = "nudTopRecord"
        Me.nudTopRecord.Size = New System.Drawing.Size(85, 20)
        Me.nudTopRecord.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(285, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Record Count"
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(661, 31)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(61, 23)
        Me.btnApply.TabIndex = 10
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(517, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Value 2"
        '
        'cmbValue2
        '
        Me.cmbValue2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbValue2.FormattingEnabled = True
        Me.cmbValue2.Items.AddRange(New Object() {"Point", "FastPont", "Bubble", "Line", "Spline", "Stepline", "FastLine", "Bar", "StackedBar", "StackedBar100", "Column", "StackedColumn", "StackedColumn100", "Area", "SplineArea", "StackedArea", "StackedArea100", "Pie", "Doughnut", "Stock", "CandleStick", "Range", "SplineRange", "RangeBar", "RangeColumn", "Radar", "Polar", "ErrorBar", "Boxplot", "Renko", "ThreelineBreak", "Kagi", "PointAndFigure", "Funnel", "Pyramid"})
        Me.cmbValue2.Location = New System.Drawing.Point(520, 33)
        Me.cmbValue2.Name = "cmbValue2"
        Me.cmbValue2.Size = New System.Drawing.Size(135, 21)
        Me.cmbValue2.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(376, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Value 1"
        '
        'cmbValue1
        '
        Me.cmbValue1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbValue1.FormattingEnabled = True
        Me.cmbValue1.Items.AddRange(New Object() {"Point", "FastPont", "Bubble", "Line", "Spline", "Stepline", "FastLine", "Bar", "StackedBar", "StackedBar100", "Column", "StackedColumn", "StackedColumn100", "Area", "SplineArea", "StackedArea", "StackedArea100", "Pie", "Doughnut", "Stock", "CandleStick", "Range", "SplineRange", "RangeBar", "RangeColumn", "Radar", "Polar", "ErrorBar", "Boxplot", "Renko", "ThreelineBreak", "Kagi", "PointAndFigure", "Funnel", "Pyramid"})
        Me.cmbValue1.Location = New System.Drawing.Point(379, 33)
        Me.cmbValue1.Name = "cmbValue1"
        Me.cmbValue1.Size = New System.Drawing.Size(135, 21)
        Me.cmbValue1.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(144, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Category"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Items.AddRange(New Object() {"Point", "FastPont", "Bubble", "Line", "Spline", "Stepline", "FastLine", "Bar", "StackedBar", "StackedBar100", "Column", "StackedColumn", "StackedColumn100", "Area", "SplineArea", "StackedArea", "StackedArea100", "Pie", "Doughnut", "Stock", "CandleStick", "Range", "SplineRange", "RangeBar", "RangeColumn", "Radar", "Polar", "ErrorBar", "Boxplot", "Renko", "ThreelineBreak", "Kagi", "PointAndFigure", "Funnel", "Pyramid"})
        Me.cmbCategory.Location = New System.Drawing.Point(147, 33)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(135, 21)
        Me.cmbCategory.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Chart Type"
        '
        'cmbChartType
        '
        Me.cmbChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChartType.FormattingEnabled = True
        Me.cmbChartType.Items.AddRange(New Object() {"Point", "FastPont", "Bubble", "Line", "Spline", "Stepline", "FastLine", "Bar", "StackedBar", "StackedBar100", "Column", "StackedColumn", "StackedColumn100", "Area", "SplineArea", "StackedArea", "StackedArea100", "Pie", "Doughnut", "Stock", "CandleStick", "Range", "SplineRange", "RangeBar", "RangeColumn", "Radar", "Polar", "ErrorBar", "Boxplot", "Renko", "ThreelineBreak", "Kagi", "PointAndFigure", "Funnel", "Pyramid"})
        Me.cmbChartType.Location = New System.Drawing.Point(6, 33)
        Me.cmbChartType.Name = "cmbChartType"
        Me.cmbChartType.Size = New System.Drawing.Size(135, 21)
        Me.cmbChartType.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.btnApply)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.nudTopRecord)
        Me.GroupBox3.Controls.Add(Me.cmbValue2)
        Me.GroupBox3.Controls.Add(Me.cmbChartType)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.cmbValue1)
        Me.GroupBox3.Controls.Add(Me.cmbCategory)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 41)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(876, 60)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Criteria"
        '
        'frmRptCustomizeCharts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 580)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmRptCustomizeCharts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Customized Chart Report"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudTopRecord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbChartType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbValue1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbValue2 As System.Windows.Forms.ComboBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nudTopRecord As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
End Class
