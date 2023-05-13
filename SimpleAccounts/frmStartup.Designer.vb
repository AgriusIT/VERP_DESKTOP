<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStartup
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
        Me.components = New System.ComponentModel.Container
        Dim PaintElement1 As Infragistics.UltraChart.Resources.Appearance.PaintElement = New Infragistics.UltraChart.Resources.Appearance.PaintElement
        Dim GradientEffect1 As Infragistics.UltraChart.Resources.Appearance.GradientEffect = New Infragistics.UltraChart.Resources.Appearance.GradientEffect
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Account Name")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Dr")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Cr")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Balance")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStartup))
        Me.UltraChart1 = New Infragistics.Win.UltraWinChart.UltraChart
        Me.ChartTAbleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.UltraDataSource1 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.ChartTAbleBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ChartTAbleBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
        CType(Me.UltraChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartTAbleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartTAbleBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ChartTAbleBindingNavigator.SuspendLayout()
        Me.SuspendLayout()
        '
        '			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
        '			'ChartType' must be persisted ahead of any Axes change made in design time.
        '		
        Me.UltraChart1.ChartType = Infragistics.UltraChart.[Shared].Styles.ChartType.PieChart
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
        Me.UltraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.FormatString = ""
        Me.UltraChart1.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
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
        Me.UltraChart1.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.X2.Labels.ItemFormatString = ""
        Me.UltraChart1.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.X2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.FormatString = ""
        Me.UltraChart1.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
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
        Me.UltraChart1.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.UltraChart1.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.FormatString = ""
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
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
        Me.UltraChart1.Axis.Y.TickmarkInterval = 50
        Me.UltraChart1.Axis.Y.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.UltraChart1.Axis.Y.Visible = True
        Me.UltraChart1.Axis.Y2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Y2.Labels.ItemFormatString = ""
        Me.UltraChart1.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.FormatString = ""
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.UltraChart1.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
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
        Me.UltraChart1.Axis.Y2.TickmarkInterval = 50
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
        Me.UltraChart1.Axis.Z2.Labels.ItemFormatString = ""
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
        Me.UltraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.[Shared].Styles.ColorModels.CustomLinear
        Me.UltraChart1.DataSource = Me.ChartTAbleBindingSource
        Me.UltraChart1.Effects.Effects.Add(GradientEffect1)
        Me.UltraChart1.Location = New System.Drawing.Point(430, 28)
        Me.UltraChart1.Name = "UltraChart1"
        Me.UltraChart1.Size = New System.Drawing.Size(313, 274)
        Me.UltraChart1.TabIndex = 0
        Me.UltraChart1.TitleTop.Text = "Favourite Accounts"
        Me.UltraChart1.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray
        Me.UltraChart1.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray
        '
        'ChartTAbleBindingSource
        '
        Me.ChartTAbleBindingSource.DataMember = "ChartTAble"
        '
        'UltraGrid1
        '
        Me.UltraGrid1.DataSource = Me.ChartTAbleBindingSource
        Appearance1.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance1
        Me.UltraGrid1.DisplayLayout.InterBandSpacing = 10
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Appearance3.BackColor = System.Drawing.SystemColors.Control
        Appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance3
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Appearance4.BackColor = System.Drawing.SystemColors.Control
        Appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.UltraGrid1.DisplayLayout.Override.RowSelectorAppearance = Appearance5
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption
        Appearance6.BackColor2 = System.Drawing.SystemColors.ActiveCaption
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Me.UltraGrid1.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.UltraGrid1.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark
        Me.UltraGrid1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(12, 28)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(404, 274)
        Me.UltraGrid1.TabIndex = 2
        '
        'UltraDataSource1
        '
        UltraDataColumn2.DataType = GetType(Integer)
        UltraDataColumn2.DefaultValue = 0
        UltraDataColumn3.DataType = GetType(Integer)
        UltraDataColumn3.DefaultValue = 0
        UltraDataColumn4.DataType = GetType(Integer)
        UltraDataColumn4.DefaultValue = 0
        Me.UltraDataSource1.Band.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4})
        '
        'ChartTAbleBindingNavigator
        '
        Me.ChartTAbleBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.ChartTAbleBindingNavigator.BindingSource = Me.ChartTAbleBindingSource
        Me.ChartTAbleBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.ChartTAbleBindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.ChartTAbleBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.ChartTAbleBindingNavigatorSaveItem})
        Me.ChartTAbleBindingNavigator.Location = New System.Drawing.Point(0, 0)
        Me.ChartTAbleBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.ChartTAbleBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.ChartTAbleBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.ChartTAbleBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.ChartTAbleBindingNavigator.Name = "ChartTAbleBindingNavigator"
        Me.ChartTAbleBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.ChartTAbleBindingNavigator.Size = New System.Drawing.Size(755, 25)
        Me.ChartTAbleBindingNavigator.TabIndex = 3
        Me.ChartTAbleBindingNavigator.Text = "BindingNavigator1"
        Me.ChartTAbleBindingNavigator.Visible = False
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 21)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ChartTAbleBindingNavigatorSaveItem
        '
        Me.ChartTAbleBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ChartTAbleBindingNavigatorSaveItem.Image = CType(resources.GetObject("ChartTAbleBindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.ChartTAbleBindingNavigatorSaveItem.Name = "ChartTAbleBindingNavigatorSaveItem"
        Me.ChartTAbleBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.ChartTAbleBindingNavigatorSaveItem.Text = "Save Data"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebBrowser1.Location = New System.Drawing.Point(12, 308)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(731, 255)
        Me.WebBrowser1.TabIndex = 4
        Me.WebBrowser1.Url = New System.Uri("http://www.blogs.SIRIUS.net", System.UriKind.Absolute)
        '
        'frmStartup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(755, 575)
        Me.Controls.Add(Me.ChartTAbleBindingNavigator)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.UltraChart1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmStartup"
        Me.Text = "frmStartup"
        CType(Me.UltraChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartTAbleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartTAbleBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ChartTAbleBindingNavigator.ResumeLayout(False)
        Me.ChartTAbleBindingNavigator.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents UltraChart1 As Infragistics.Win.UltraWinChart.UltraChart
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents ChartTAbleBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ChartTAbleBindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChartTAbleBindingNavigatorSaveItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
End Class
