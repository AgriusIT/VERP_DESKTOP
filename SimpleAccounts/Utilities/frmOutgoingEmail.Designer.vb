<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOutgoingEmail
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnSend = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.sendProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblProgress = New System.Windows.Forms.ToolStripLabel()
        Me.btnAdd = New System.Windows.Forms.ToolStripSplitButton()
        Me.btnCCAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnBCCAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddEmailAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmbFrom = New System.Windows.Forms.ToolStripComboBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.lblSubject = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtAttachFile = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lblAttachFile = New System.Windows.Forms.Label()
        Me.txtDataFile = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBrowse1 = New System.Windows.Forms.Button()
        Me.btnRemove1 = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.txtBody = New System.Windows.Forms.RichTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.txtTo = New System.Windows.Forms.TextBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.lblErrorMessage = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSend, Me.ToolStripSeparator1, Me.sendProgress, Me.lblProgress, Me.btnAdd, Me.cmbFrom})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(559, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSend
        '
        Me.btnSend.Image = Global.SimpleAccounts.My.Resources.Resources.Email_Envelope
        Me.btnSend.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(53, 22)
        Me.btnSend.Text = "Send"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'sendProgress
        '
        Me.sendProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.sendProgress.Name = "sendProgress"
        Me.sendProgress.Size = New System.Drawing.Size(150, 22)
        '
        'lblProgress
        '
        Me.lblProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(52, 22)
        Me.lblProgress.Text = "Progress"
        '
        'btnAdd
        '
        Me.btnAdd.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCCAdd, Me.btnBCCAdd, Me.AddEmailAccountToolStripMenuItem})
        Me.btnAdd.Image = Global.SimpleAccounts.My.Resources.Resources.Attendanceadd
        Me.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(61, 22)
        Me.btnAdd.Text = "Add"
        '
        'btnCCAdd
        '
        Me.btnCCAdd.Name = "btnCCAdd"
        Me.btnCCAdd.Size = New System.Drawing.Size(152, 22)
        Me.btnCCAdd.Text = "Cc"
        '
        'btnBCCAdd
        '
        Me.btnBCCAdd.Name = "btnBCCAdd"
        Me.btnBCCAdd.Size = New System.Drawing.Size(152, 22)
        Me.btnBCCAdd.Text = "Bcc"
        '
        'AddEmailAccountToolStripMenuItem
        '
        Me.AddEmailAccountToolStripMenuItem.Name = "AddEmailAccountToolStripMenuItem"
        Me.AddEmailAccountToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AddEmailAccountToolStripMenuItem.Text = "Email Account"
        '
        'cmbFrom
        '
        Me.cmbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFrom.Name = "cmbFrom"
        Me.cmbFrom.Size = New System.Drawing.Size(150, 25)
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(22, 54)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblFrom.TabIndex = 1
        Me.lblFrom.Text = "Title:"
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(95, 100)
        Me.txtSubject.Multiline = True
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(452, 25)
        Me.txtSubject.TabIndex = 10
        '
        'lblSubject
        '
        Me.lblSubject.AutoSize = True
        Me.lblSubject.Location = New System.Drawing.Point(22, 106)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(46, 13)
        Me.lblSubject.TabIndex = 9
        Me.lblSubject.Text = "Subject:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(95, 48)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(452, 25)
        Me.TextBox1.TabIndex = 2
        '
        'txtAttachFile
        '
        Me.txtAttachFile.Location = New System.Drawing.Point(95, 413)
        Me.txtAttachFile.Name = "txtAttachFile"
        Me.txtAttachFile.ReadOnly = True
        Me.txtAttachFile.Size = New System.Drawing.Size(322, 20)
        Me.txtAttachFile.TabIndex = 18
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(423, 411)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(59, 23)
        Me.btnBrowse.TabIndex = 19
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblAttachFile
        '
        Me.lblAttachFile.AutoSize = True
        Me.lblAttachFile.Location = New System.Drawing.Point(12, 416)
        Me.lblAttachFile.Name = "lblAttachFile"
        Me.lblAttachFile.Size = New System.Drawing.Size(66, 13)
        Me.lblAttachFile.TabIndex = 17
        Me.lblAttachFile.Text = "Attach File 2"
        '
        'txtDataFile
        '
        Me.txtDataFile.Location = New System.Drawing.Point(95, 390)
        Me.txtDataFile.Name = "txtDataFile"
        Me.txtDataFile.ReadOnly = True
        Me.txtDataFile.Size = New System.Drawing.Size(322, 20)
        Me.txtDataFile.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 393)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Attach File 1"
        '
        'btnBrowse1
        '
        Me.btnBrowse1.Location = New System.Drawing.Point(423, 388)
        Me.btnBrowse1.Name = "btnBrowse1"
        Me.btnBrowse1.Size = New System.Drawing.Size(59, 23)
        Me.btnBrowse1.TabIndex = 15
        Me.btnBrowse1.Text = "Browse..."
        Me.btnBrowse1.UseVisualStyleBackColor = True
        '
        'btnRemove1
        '
        Me.btnRemove1.Location = New System.Drawing.Point(484, 388)
        Me.btnRemove1.Name = "btnRemove1"
        Me.btnRemove1.Size = New System.Drawing.Size(63, 23)
        Me.btnRemove1.TabIndex = 16
        Me.btnRemove1.Text = "Remove"
        Me.btnRemove1.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(484, 411)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(63, 23)
        Me.btnRemove.TabIndex = 20
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'txtBody
        '
        Me.txtBody.Location = New System.Drawing.Point(95, 131)
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(452, 251)
        Me.txtBody.TabIndex = 12
        Me.txtBody.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Message:"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(22, 80)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(23, 13)
        Me.lblTo.TabIndex = 3
        Me.lblTo.Text = "To:"
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(95, 74)
        Me.txtTo.Multiline = True
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(452, 25)
        Me.txtTo.TabIndex = 4
        '
        'lblErrorMessage
        '
        Me.lblErrorMessage.Location = New System.Drawing.Point(92, 32)
        Me.lblErrorMessage.Name = "lblErrorMessage"
        Me.lblErrorMessage.Size = New System.Drawing.Size(455, 13)
        Me.lblErrorMessage.TabIndex = 21
        '
        'frmOutgoingEmail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(559, 447)
        Me.Controls.Add(Me.lblErrorMessage)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBody)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.lblSubject)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.txtSubject)
        Me.Controls.Add(Me.btnRemove1)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.btnBrowse1)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.txtDataFile)
        Me.Controls.Add(Me.lblAttachFile)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtAttachFile)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOutgoingEmail"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Email"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents lblSubject As System.Windows.Forms.Label
    Friend WithEvents btnSend As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents sendProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtAttachFile As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lblAttachFile As System.Windows.Forms.Label
    Friend WithEvents txtDataFile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse1 As System.Windows.Forms.Button
    Friend WithEvents btnRemove1 As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents btnCCAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnBCCAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddEmailAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbFrom As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents txtBody As System.Windows.Forms.RichTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblErrorMessage As System.Windows.Forms.Label
End Class
