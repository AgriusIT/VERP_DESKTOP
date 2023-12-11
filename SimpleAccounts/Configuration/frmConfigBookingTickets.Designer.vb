<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigBookingTickets
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
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkTravelingAccountFrontEndNot = New System.Windows.Forms.RadioButton()
        Me.chkTravelingAccountFrontEnd = New System.Windows.Forms.RadioButton()
        Me.cmbTravelingAccount = New System.Windows.Forms.ComboBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.linkDB = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel8
        '
        Me.Panel8.AutoScroll = True
        Me.Panel8.Controls.Add(Me.Panel1)
        Me.Panel8.Controls.Add(Me.cmbTravelingAccount)
        Me.Panel8.Controls.Add(Me.Panel10)
        Me.Panel8.Controls.Add(Me.Panel11)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Controls.Add(Me.Label16)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 61)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(793, 668)
        Me.Panel8.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkTravelingAccountFrontEndNot)
        Me.Panel1.Controls.Add(Me.chkTravelingAccountFrontEnd)
        Me.Panel1.Location = New System.Drawing.Point(256, 107)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(405, 34)
        Me.Panel1.TabIndex = 5
        '
        'chkTravelingAccountFrontEndNot
        '
        Me.chkTravelingAccountFrontEndNot.AutoSize = True
        Me.chkTravelingAccountFrontEndNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTravelingAccountFrontEndNot.Location = New System.Drawing.Point(92, 5)
        Me.chkTravelingAccountFrontEndNot.Name = "chkTravelingAccountFrontEndNot"
        Me.chkTravelingAccountFrontEndNot.Size = New System.Drawing.Size(47, 24)
        Me.chkTravelingAccountFrontEndNot.TabIndex = 1
        Me.chkTravelingAccountFrontEndNot.TabStop = True
        Me.chkTravelingAccountFrontEndNot.Text = "No"
        Me.chkTravelingAccountFrontEndNot.UseVisualStyleBackColor = True
        '
        'chkTravelingAccountFrontEnd
        '
        Me.chkTravelingAccountFrontEnd.AutoSize = True
        Me.chkTravelingAccountFrontEnd.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTravelingAccountFrontEnd.Location = New System.Drawing.Point(5, 5)
        Me.chkTravelingAccountFrontEnd.Name = "chkTravelingAccountFrontEnd"
        Me.chkTravelingAccountFrontEnd.Size = New System.Drawing.Size(48, 24)
        Me.chkTravelingAccountFrontEnd.TabIndex = 0
        Me.chkTravelingAccountFrontEnd.TabStop = True
        Me.chkTravelingAccountFrontEnd.Tag = "TravelingAccountFrontEndBookingTickets"
        Me.chkTravelingAccountFrontEnd.Text = "Yes"
        Me.chkTravelingAccountFrontEnd.UseVisualStyleBackColor = True
        '
        'cmbTravelingAccount
        '
        Me.cmbTravelingAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbTravelingAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTravelingAccount.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTravelingAccount.FormattingEnabled = True
        Me.cmbTravelingAccount.Location = New System.Drawing.Point(256, 14)
        Me.cmbTravelingAccount.Name = "cmbTravelingAccount"
        Me.cmbTravelingAccount.Size = New System.Drawing.Size(274, 28)
        Me.cmbTravelingAccount.TabIndex = 2
        Me.cmbTravelingAccount.TabStop = False
        Me.cmbTravelingAccount.Tag = "TravelingAccountBookingTickets"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel10.Controls.Add(Me.Label9)
        Me.Panel10.Location = New System.Drawing.Point(256, 149)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(405, 51)
        Me.Panel10.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(3, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(341, 34)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "use to allow mapping of traveling expense account from " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "front end on booking tic" & _
    "kets"
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel11.Controls.Add(Me.Label10)
        Me.Panel11.Location = New System.Drawing.Point(256, 50)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(405, 51)
        Me.Panel11.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(3, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(314, 17)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "use to configure traveling account of booking tickets"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label11.Location = New System.Drawing.Point(56, 113)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(183, 42)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Enable traveling account " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "mapping at front end"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label16.Location = New System.Drawing.Point(56, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(161, 42)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "Traveling Account for " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Booking Tickets"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.linkDB)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(793, 61)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(215, 668)
        Me.Panel3.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(20, 315)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 21)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Contact Us"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(20, 269)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(146, 21)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "Have a Question(s)"
        '
        'linkDB
        '
        Me.linkDB.AutoSize = True
        Me.linkDB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkDB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkDB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkDB.Location = New System.Drawing.Point(20, 59)
        Me.linkDB.Name = "linkDB"
        Me.linkDB.Size = New System.Drawing.Size(128, 21)
        Me.linkDB.TabIndex = 2
        Me.linkDB.Text = "Database Backup"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(20, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(130, 21)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Related Settings"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1008, 61)
        Me.Panel2.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(373, 37)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Booking Tickets Configuration"
        '
        'frmConfigBookingTickets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmConfigBookingTickets"
        Me.Text = "frmConfigBookingTickets"
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkTravelingAccountFrontEndNot As System.Windows.Forms.RadioButton
    Friend WithEvents chkTravelingAccountFrontEnd As System.Windows.Forms.RadioButton
    Friend WithEvents cmbTravelingAccount As System.Windows.Forms.ComboBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents linkDB As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
