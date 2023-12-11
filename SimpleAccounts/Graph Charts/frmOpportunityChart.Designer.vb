<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpportunityChart
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.DockedToChartArea = "ChartArea1"
        Legend1.Name = "Legend1"
        Legend1.Title = "Stages"
        Legend1.TitleFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(3, 47)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series1.CustomProperties = "PieLineColor=Black, LabelsHorizontalLineSize=0.5, PieLabelStyle=Outside, LabelsRa" & _
    "dialLineSize=0.5"
        Series1.EmptyPointStyle.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center
        Series1.IsValueShownAsLabel = True
        Series1.IsXValueIndexed = True
        Series1.Legend = "Legend1"
        Series1.Name = "Stages"
        Series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes
        Series1.SmartLabelStyle.CalloutLineColor = System.Drawing.Color.White
        Series1.SmartLabelStyle.CalloutLineWidth = 10
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(728, 240)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 31)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "OPPORTUNITY STATUS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.Panel1)
        Me.pnlHeader.Controls.Add(Me.Label1)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(727, 46)
        Me.pnlHeader.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(0, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(731, 244)
        Me.Panel1.TabIndex = 2
        '
        'frmOpportunityChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(727, 287)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Chart1)
        Me.Name = "frmOpportunityChart"
        Me.Text = "frmOpportunityChart"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
