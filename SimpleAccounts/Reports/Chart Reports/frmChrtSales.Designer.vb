<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChrtSales
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
        Dim PaintElement1 As Infragistics.UltraChart.Resources.Appearance.PaintElement = New Infragistics.UltraChart.Resources.Appearance.PaintElement()
        Dim GradientEffect1 As Infragistics.UltraChart.Resources.Appearance.GradientEffect = New Infragistics.UltraChart.Resources.Appearance.GradientEffect()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChrtSales))
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraChart1 = New Infragistics.Win.UltraWinChart.UltraChart()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1232, 797)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraChart1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1232, 797)
        '
        'UltraChart1
        '
        Me.UltraChart1.Axis.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        PaintElement1.ElementType = Infragistics.UltraChart.[Shared].Styles.PaintElementType.None
        PaintElement1.Fill = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.UltraChart1.Axis.PE = PaintElement1
        Me.UltraChart1.Axis.X.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.UltraChart1.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.X.LineThickness = 1
        Me.UltraChart1.Axis.X.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.UltraChart1.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.X.MajorGridLines.Visible = True
        Me.UltraChart1.Axis.X.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.UltraChart1.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.X.MinorGridLines.Visible = False
        Me.UltraChart1.Axis.X.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.UltraChart1.Axis.X.Visible = True
        Me.UltraChart1.Axis.X2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.UltraChart1.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.UltraChart1.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.X2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.X2.Labels.Visible = False
        Me.UltraChart1.Axis.X2.LineThickness = 1
        Me.UltraChart1.Axis.X2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.UltraChart1.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.X2.MajorGridLines.Visible = True
        Me.UltraChart1.Axis.X2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.UltraChart1.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.X2.MinorGridLines.Visible = False
        Me.UltraChart1.Axis.X2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.UltraChart1.Axis.X2.Visible = False
        Me.UltraChart1.Axis.Y.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.UltraChart1.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.UltraChart1.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.FormatString = ""
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Y.LineThickness = 1
        Me.UltraChart1.Axis.Y.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.UltraChart1.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Y.MajorGridLines.Visible = True
        Me.UltraChart1.Axis.Y.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.UltraChart1.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Y.MinorGridLines.Visible = False
        Me.UltraChart1.Axis.Y.TickmarkInterval = 50.0R
        Me.UltraChart1.Axis.Y.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.UltraChart1.Axis.Y.Visible = True
        Me.UltraChart1.Axis.Y2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.UltraChart1.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.FormatString = ""
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Y2.Labels.Visible = False
        Me.UltraChart1.Axis.Y2.LineThickness = 1
        Me.UltraChart1.Axis.Y2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.UltraChart1.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Y2.MajorGridLines.Visible = True
        Me.UltraChart1.Axis.Y2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.UltraChart1.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Y2.MinorGridLines.Visible = False
        Me.UltraChart1.Axis.Y2.TickmarkInterval = 50.0R
        Me.UltraChart1.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.UltraChart1.Axis.Y2.Visible = False
        Me.UltraChart1.Axis.Z.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Z.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.UltraChart1.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Z.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Z.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Z.Labels.Visible = False
        Me.UltraChart1.Axis.Z.LineThickness = 1
        Me.UltraChart1.Axis.Z.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.UltraChart1.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Z.MajorGridLines.Visible = True
        Me.UltraChart1.Axis.Z.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.UltraChart1.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Z.MinorGridLines.Visible = False
        Me.UltraChart1.Axis.Z.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.UltraChart1.Axis.Z.Visible = False
        Me.UltraChart1.Axis.Z2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Z2.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.UltraChart1.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Z2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.UltraChart1.Axis.Z2.Labels.Visible = False
        Me.UltraChart1.Axis.Z2.LineThickness = 1
        Me.UltraChart1.Axis.Z2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.UltraChart1.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Z2.MajorGridLines.Visible = True
        Me.UltraChart1.Axis.Z2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.UltraChart1.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.UltraChart1.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.UltraChart1.Axis.Z2.MinorGridLines.Visible = False
        Me.UltraChart1.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.UltraChart1.Axis.Z2.Visible = False
        Me.UltraChart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.UltraChart1.ColorModel.AlphaLevel = CType(150, Byte)
        Me.UltraChart1.ColorModel.ColorBegin = System.Drawing.Color.Pink
        Me.UltraChart1.ColorModel.ColorEnd = System.Drawing.Color.DarkRed
        Me.UltraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.[Shared].Styles.ColorModels.CustomLinear
        Me.UltraChart1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraChart1.Effects.Effects.Add(GradientEffect1)
        Me.UltraChart1.Location = New System.Drawing.Point(0, 0)
        Me.UltraChart1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraChart1.Name = "UltraChart1"
        Me.UltraChart1.Size = New System.Drawing.Size(1232, 797)
        Me.UltraChart1.TabIndex = 0
        Me.UltraChart1.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1234, 824)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Show"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1232, 797)
        '
        'frmChrtSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 849)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmChrtSales"
        Me.Text = "Sales chart"
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.UltraChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Private WithEvents UltraChart1 As Infragistics.Win.UltraWinChart.UltraChart
End Class
