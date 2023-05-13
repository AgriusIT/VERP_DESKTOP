<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMissedVisitGraph
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
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series8 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series10 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series11 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series12 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.chrtMissedVisitGraph = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.chkVisitedMissedGraph = New System.Windows.Forms.CheckBox()
        Me.chrtApprovedMissedVisit = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbManager = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbInside = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbResponsible = New System.Windows.Forms.ComboBox()
        Me.rdoBarGraph = New System.Windows.Forms.RadioButton()
        Me.rdoPieGraph = New System.Windows.Forms.RadioButton()
        Me.chrtMissedVisitPieGraph = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.chrtApprovedMissedVisitPie = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.chrtInsideBar = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.chrtInsidePie = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtPie = New System.Windows.Forms.RadioButton()
        Me.rbtBar = New System.Windows.Forms.RadioButton()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbReport = New System.Windows.Forms.ComboBox()
        Me.pnlHeader.SuspendLayout()
        CType(Me.chrtMissedVisitGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chrtApprovedMissedVisit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chrtMissedVisitPieGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chrtApprovedMissedVisitPie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chrtInsideBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chrtInsidePie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.Teal
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(924, 38)
        Me.pnlHeader.TabIndex = 15
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(12, 4)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(246, 30)
        Me.lblHeader.TabIndex = 11
        Me.lblHeader.Text = "Visited vs Missed Graph"
        '
        'chrtMissedVisitGraph
        '
        ChartArea1.Name = "ChartArea1"
        Me.chrtMissedVisitGraph.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.chrtMissedVisitGraph.Legends.Add(Legend1)
        Me.chrtMissedVisitGraph.Location = New System.Drawing.Point(12, 513)
        Me.chrtMissedVisitGraph.Name = "chrtMissedVisitGraph"
        Series1.ChartArea = "ChartArea1"
        Series1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!)
        Series1.Legend = "Legend1"
        Series1.Name = "Missed"
        Series2.ChartArea = "ChartArea1"
        Series2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!)
        Series2.Legend = "Legend1"
        Series2.Name = "Visited"
        Me.chrtMissedVisitGraph.Series.Add(Series1)
        Me.chrtMissedVisitGraph.Series.Add(Series2)
        Me.chrtMissedVisitGraph.Size = New System.Drawing.Size(973, 10)
        Me.chrtMissedVisitGraph.TabIndex = 16
        Me.chrtMissedVisitGraph.Text = "Chart1"
        Title1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!)
        Title1.Name = "Missed VS Visited "
        Me.chrtMissedVisitGraph.Titles.Add(Title1)
        Me.chrtMissedVisitGraph.Visible = False
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(630, 118)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(75, 23)
        Me.btnLoad.TabIndex = 17
        Me.btnLoad.Text = "Load Graph"
        Me.btnLoad.UseVisualStyleBackColor = True
        Me.btnLoad.Visible = False
        '
        'chkVisitedMissedGraph
        '
        Me.chkVisitedMissedGraph.AutoSize = True
        Me.chkVisitedMissedGraph.Location = New System.Drawing.Point(461, 73)
        Me.chkVisitedMissedGraph.Name = "chkVisitedMissedGraph"
        Me.chkVisitedMissedGraph.Size = New System.Drawing.Size(193, 17)
        Me.chkVisitedMissedGraph.TabIndex = 18
        Me.chkVisitedMissedGraph.Text = "Show Approved Missed and Visited"
        Me.chkVisitedMissedGraph.UseVisualStyleBackColor = True
        Me.chkVisitedMissedGraph.Visible = False
        '
        'chrtApprovedMissedVisit
        '
        ChartArea2.Name = "ChartArea1"
        Me.chrtApprovedMissedVisit.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.chrtApprovedMissedVisit.Legends.Add(Legend2)
        Me.chrtApprovedMissedVisit.Location = New System.Drawing.Point(12, 488)
        Me.chrtApprovedMissedVisit.Name = "chrtApprovedMissedVisit"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Approved Missed"
        Series4.ChartArea = "ChartArea1"
        Series4.Legend = "Legend1"
        Series4.Name = "Approved Visited"
        Series5.ChartArea = "ChartArea1"
        Series5.Legend = "Legend1"
        Series5.Name = "Missed Rejected"
        Series6.ChartArea = "ChartArea1"
        Series6.Legend = "Legend1"
        Series6.Name = "Visit Rejected"
        Me.chrtApprovedMissedVisit.Series.Add(Series3)
        Me.chrtApprovedMissedVisit.Series.Add(Series4)
        Me.chrtApprovedMissedVisit.Series.Add(Series5)
        Me.chrtApprovedMissedVisit.Series.Add(Series6)
        Me.chrtApprovedMissedVisit.Size = New System.Drawing.Size(973, 93)
        Me.chrtApprovedMissedVisit.TabIndex = 19
        Me.chrtApprovedMissedVisit.Text = "Chart1"
        Me.chrtApprovedMissedVisit.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(308, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 17)
        Me.Label1.TabIndex = 221
        Me.Label1.Text = "Activity To"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(311, 73)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(144, 25)
        Me.dtpToDate.TabIndex = 219
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(157, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 17)
        Me.Label6.TabIndex = 222
        Me.Label6.Text = "Activity From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(160, 73)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(144, 25)
        Me.dtpFromDate.TabIndex = 218
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 17)
        Me.Label3.TabIndex = 224
        Me.Label3.Text = "Period"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(308, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 17)
        Me.Label5.TabIndex = 230
        Me.Label5.Text = "Manager"
        '
        'cmbManager
        '
        Me.cmbManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbManager.BackColor = System.Drawing.Color.White
        Me.cmbManager.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbManager.FormattingEnabled = True
        Me.cmbManager.Location = New System.Drawing.Point(311, 121)
        Me.cmbManager.Name = "cmbManager"
        Me.cmbManager.Size = New System.Drawing.Size(144, 25)
        Me.cmbManager.TabIndex = 229
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(157, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 17)
        Me.Label2.TabIndex = 228
        Me.Label2.Text = "Inside Person"
        '
        'cmbInside
        '
        Me.cmbInside.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbInside.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbInside.BackColor = System.Drawing.Color.White
        Me.cmbInside.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbInside.FormattingEnabled = True
        Me.cmbInside.Location = New System.Drawing.Point(160, 121)
        Me.cmbInside.Name = "cmbInside"
        Me.cmbInside.Size = New System.Drawing.Size(144, 25)
        Me.cmbInside.TabIndex = 227
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(6, 101)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(123, 17)
        Me.Label7.TabIndex = 226
        Me.Label7.Text = "Responsible Person"
        '
        'cmbResponsible
        '
        Me.cmbResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbResponsible.BackColor = System.Drawing.Color.White
        Me.cmbResponsible.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbResponsible.FormattingEnabled = True
        Me.cmbResponsible.Location = New System.Drawing.Point(9, 121)
        Me.cmbResponsible.Name = "cmbResponsible"
        Me.cmbResponsible.Size = New System.Drawing.Size(144, 25)
        Me.cmbResponsible.TabIndex = 225
        '
        'rdoBarGraph
        '
        Me.rdoBarGraph.AutoSize = True
        Me.rdoBarGraph.Location = New System.Drawing.Point(462, 121)
        Me.rdoBarGraph.Name = "rdoBarGraph"
        Me.rdoBarGraph.Size = New System.Drawing.Size(73, 17)
        Me.rdoBarGraph.TabIndex = 231
        Me.rdoBarGraph.TabStop = True
        Me.rdoBarGraph.Text = "Bar Graph"
        Me.rdoBarGraph.UseVisualStyleBackColor = True
        Me.rdoBarGraph.Visible = False
        '
        'rdoPieGraph
        '
        Me.rdoPieGraph.AutoSize = True
        Me.rdoPieGraph.Location = New System.Drawing.Point(552, 121)
        Me.rdoPieGraph.Name = "rdoPieGraph"
        Me.rdoPieGraph.Size = New System.Drawing.Size(72, 17)
        Me.rdoPieGraph.TabIndex = 232
        Me.rdoPieGraph.TabStop = True
        Me.rdoPieGraph.Text = "Pie Graph"
        Me.rdoPieGraph.UseVisualStyleBackColor = True
        Me.rdoPieGraph.Visible = False
        '
        'chrtMissedVisitPieGraph
        '
        ChartArea3.Name = "ChartArea1"
        Me.chrtMissedVisitPieGraph.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.chrtMissedVisitPieGraph.Legends.Add(Legend3)
        Me.chrtMissedVisitPieGraph.Location = New System.Drawing.Point(9, 488)
        Me.chrtMissedVisitPieGraph.Name = "chrtMissedVisitPieGraph"
        Series7.ChartArea = "ChartArea1"
        Series7.Legend = "Legend1"
        Series7.Name = "Series2"
        Series8.ChartArea = "ChartArea1"
        Series8.Legend = "Legend1"
        Series8.Name = "Series3"
        Me.chrtMissedVisitPieGraph.Series.Add(Series7)
        Me.chrtMissedVisitPieGraph.Series.Add(Series8)
        Me.chrtMissedVisitPieGraph.Size = New System.Drawing.Size(976, 97)
        Me.chrtMissedVisitPieGraph.TabIndex = 233
        Me.chrtMissedVisitPieGraph.Text = "Chart1"
        Me.chrtMissedVisitPieGraph.Visible = False
        '
        'chrtApprovedMissedVisitPie
        '
        ChartArea4.Name = "ChartArea1"
        Me.chrtApprovedMissedVisitPie.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Me.chrtApprovedMissedVisitPie.Legends.Add(Legend4)
        Me.chrtApprovedMissedVisitPie.Location = New System.Drawing.Point(9, 457)
        Me.chrtApprovedMissedVisitPie.Name = "chrtApprovedMissedVisitPie"
        Series9.ChartArea = "ChartArea1"
        Series9.Legend = "Legend1"
        Series9.Name = "Series2"
        Me.chrtApprovedMissedVisitPie.Series.Add(Series9)
        Me.chrtApprovedMissedVisitPie.Size = New System.Drawing.Size(976, 124)
        Me.chrtApprovedMissedVisitPie.TabIndex = 234
        Me.chrtApprovedMissedVisitPie.Text = "Chart1"
        Me.chrtApprovedMissedVisitPie.Visible = False
        '
        'chrtInsideBar
        '
        ChartArea5.Name = "ChartArea1"
        Me.chrtInsideBar.ChartAreas.Add(ChartArea5)
        Legend5.Name = "Legend1"
        Me.chrtInsideBar.Legends.Add(Legend5)
        Me.chrtInsideBar.Location = New System.Drawing.Point(9, 367)
        Me.chrtInsideBar.Name = "chrtInsideBar"
        Series10.ChartArea = "ChartArea1"
        Series10.Legend = "Legend1"
        Series10.Name = "Confirmed"
        Series11.ChartArea = "ChartArea1"
        Series11.Legend = "Legend1"
        Series11.Name = "Not Confirmed"
        Me.chrtInsideBar.Series.Add(Series10)
        Me.chrtInsideBar.Series.Add(Series11)
        Me.chrtInsideBar.Size = New System.Drawing.Size(976, 214)
        Me.chrtInsideBar.TabIndex = 235
        Me.chrtInsideBar.Text = "Chart1"
        Me.chrtInsideBar.Visible = False
        '
        'chrtInsidePie
        '
        ChartArea6.Name = "ChartArea1"
        Me.chrtInsidePie.ChartAreas.Add(ChartArea6)
        Legend6.Name = "Legend1"
        Me.chrtInsidePie.Legends.Add(Legend6)
        Me.chrtInsidePie.Location = New System.Drawing.Point(9, 428)
        Me.chrtInsidePie.Name = "chrtInsidePie"
        Series12.ChartArea = "ChartArea1"
        Series12.Legend = "Legend1"
        Series12.Name = "Series6"
        Me.chrtInsidePie.Series.Add(Series12)
        Me.chrtInsidePie.Size = New System.Drawing.Size(976, 153)
        Me.chrtInsidePie.TabIndex = 236
        Me.chrtInsidePie.Text = "Chart1"
        Me.chrtInsidePie.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(764, 93)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 237
        Me.Button1.Text = "Load Graph"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(212, 200)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(115, 90)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 239
        Me.PictureBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtPie)
        Me.GroupBox2.Controls.Add(Me.rbtBar)
        Me.GroupBox2.Controls.Add(Me.btnShow)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 200)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(197, 74)
        Me.GroupBox2.TabIndex = 238
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Chart Type"
        '
        'rbtPie
        '
        Me.rbtPie.AutoSize = True
        Me.rbtPie.Location = New System.Drawing.Point(7, 43)
        Me.rbtPie.Name = "rbtPie"
        Me.rbtPie.Size = New System.Drawing.Size(40, 17)
        Me.rbtPie.TabIndex = 10
        Me.rbtPie.Text = "Pie"
        Me.rbtPie.UseVisualStyleBackColor = True
        '
        'rbtBar
        '
        Me.rbtBar.AutoSize = True
        Me.rbtBar.Checked = True
        Me.rbtBar.Location = New System.Drawing.Point(7, 20)
        Me.rbtBar.Name = "rbtBar"
        Me.rbtBar.Size = New System.Drawing.Size(41, 17)
        Me.rbtBar.TabIndex = 8
        Me.rbtBar.TabStop = True
        Me.rbtBar.Text = "Bar"
        Me.rbtBar.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(116, 43)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 7
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'cmbPeriod
        '
        Me.cmbPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPeriod.BackColor = System.Drawing.Color.White
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(9, 73)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(144, 25)
        Me.cmbPeriod.TabIndex = 240
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 149)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 17)
        Me.Label8.TabIndex = 242
        Me.Label8.Text = "Reports"
        '
        'cmbReport
        '
        Me.cmbReport.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbReport.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbReport.BackColor = System.Drawing.Color.White
        Me.cmbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbReport.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbReport.FormattingEnabled = True
        Me.cmbReport.Items.AddRange(New Object() {"Visited and Missed Graph Report", "Approved and Rejected Feedback Graph Report", "Confirmed and Not Confirmed Activity Graph Report"})
        Me.cmbReport.Location = New System.Drawing.Point(9, 169)
        Me.cmbReport.Name = "cmbReport"
        Me.cmbReport.Size = New System.Drawing.Size(446, 25)
        Me.cmbReport.TabIndex = 241
        '
        'frmMissedVisitGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 512)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbReport)
        Me.Controls.Add(Me.cmbPeriod)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chrtInsidePie)
        Me.Controls.Add(Me.chrtInsideBar)
        Me.Controls.Add(Me.chrtApprovedMissedVisitPie)
        Me.Controls.Add(Me.chrtMissedVisitPieGraph)
        Me.Controls.Add(Me.rdoPieGraph)
        Me.Controls.Add(Me.rdoBarGraph)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbManager)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbInside)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbResponsible)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.chrtApprovedMissedVisit)
        Me.Controls.Add(Me.chkVisitedMissedGraph)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.chrtMissedVisitGraph)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmMissedVisitGraph"
        Me.Text = "Missed Visit Graph"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.chrtMissedVisitGraph, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chrtApprovedMissedVisit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chrtMissedVisitPieGraph, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chrtApprovedMissedVisitPie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chrtInsideBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chrtInsidePie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents chrtMissedVisitGraph As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents chkVisitedMissedGraph As System.Windows.Forms.CheckBox
    Friend WithEvents chrtApprovedMissedVisit As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbManager As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbInside As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents rdoBarGraph As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPieGraph As System.Windows.Forms.RadioButton
    Friend WithEvents chrtMissedVisitPieGraph As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents chrtApprovedMissedVisitPie As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents chrtInsideBar As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents chrtInsidePie As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtPie As System.Windows.Forms.RadioButton
    Friend WithEvents rbtBar As System.Windows.Forms.RadioButton
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbReport As System.Windows.Forms.ComboBox
End Class
