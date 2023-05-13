<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefEmployeeOld
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefEmployeeOld))
        Me.txtNTN = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.chkActive = New System.Windows.Forms.CheckBox
        Me.txtNIC = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFather = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtReligion = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.dtpDOB = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtpJoining = New System.Windows.Forms.DateTimePicker
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtSalary = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.ddlMaritalStatus = New System.Windows.Forms.ComboBox
        Me.ddlGender = New System.Windows.Forms.ComboBox
        Me.ddlCity = New System.Windows.Forms.ComboBox
        Me.ddlDept = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.ddlDesignation = New System.Windows.Forms.ComboBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.dtpLeaving = New System.Windows.Forms.DateTimePicker
        Me.Label20 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.grdSaved = New System.Windows.Forms.DataGridView
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtEmpID = New System.Windows.Forms.TextBox
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtNTN
        '
        Me.txtNTN.Location = New System.Drawing.Point(553, 54)
        Me.txtNTN.Name = "txtNTN"
        Me.txtNTN.Size = New System.Drawing.Size(137, 20)
        Me.txtNTN.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(455, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "NTN Number"
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(127, 289)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(56, 17)
        Me.chkActive.TabIndex = 39
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'txtNIC
        '
        Me.txtNIC.Location = New System.Drawing.Point(127, 55)
        Me.txtNIC.Name = "txtNIC"
        Me.txtNIC.Size = New System.Drawing.Size(137, 20)
        Me.txtNIC.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "CNIC"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(553, 6)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(137, 20)
        Me.txtCode.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(455, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Employee Code"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(127, 6)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(220, 20)
        Me.txtName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Employee Name"
        '
        'txtFather
        '
        Me.txtFather.Location = New System.Drawing.Point(127, 29)
        Me.txtFather.Name = "txtFather"
        Me.txtFather.Size = New System.Drawing.Size(220, 20)
        Me.txtFather.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Father Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(455, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Gender"
        '
        'txtReligion
        '
        Me.txtReligion.Location = New System.Drawing.Point(127, 81)
        Me.txtReligion.Name = "txtReligion"
        Me.txtReligion.Size = New System.Drawing.Size(137, 20)
        Me.txtReligion.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(30, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Religion"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(455, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Marital Status"
        '
        'dtpDOB
        '
        Me.dtpDOB.CustomFormat = "dd/MM/yyyy"
        Me.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOB.Location = New System.Drawing.Point(127, 107)
        Me.dtpDOB.Name = "dtpDOB"
        Me.dtpDOB.Size = New System.Drawing.Size(137, 20)
        Me.dtpDOB.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(30, 111)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Date of Birth"
        '
        'dtpJoining
        '
        Me.dtpJoining.CustomFormat = "dd/MM/yyyy"
        Me.dtpJoining.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpJoining.Location = New System.Drawing.Point(553, 107)
        Me.dtpJoining.Name = "dtpJoining"
        Me.dtpJoining.Size = New System.Drawing.Size(137, 20)
        Me.dtpJoining.TabIndex = 20
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(455, 111)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Joining Date"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(31, 169)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "City Name"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(127, 133)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(563, 29)
        Me.txtAddress.TabIndex = 22
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(30, 139)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Address"
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(554, 169)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(137, 20)
        Me.txtPhone.TabIndex = 26
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(456, 176)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(78, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Phone Number"
        '
        'txtMobile
        '
        Me.txtMobile.Location = New System.Drawing.Point(554, 190)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(137, 20)
        Me.txtMobile.TabIndex = 30
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(456, 197)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(78, 13)
        Me.Label14.TabIndex = 29
        Me.Label14.Text = "Mobile Number"
        '
        'txtSalary
        '
        Me.txtSalary.Location = New System.Drawing.Point(554, 212)
        Me.txtSalary.Name = "txtSalary"
        Me.txtSalary.Size = New System.Drawing.Size(137, 20)
        Me.txtSalary.TabIndex = 34
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(456, 219)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(36, 13)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Salary"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(128, 194)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(137, 20)
        Me.txtEmail.TabIndex = 28
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(31, 197)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(32, 13)
        Me.Label17.TabIndex = 27
        Me.Label17.Text = "Email"
        '
        'ddlMaritalStatus
        '
        Me.ddlMaritalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlMaritalStatus.FormattingEnabled = True
        Me.ddlMaritalStatus.Items.AddRange(New Object() {"Married", "UnMarried"})
        Me.ddlMaritalStatus.Location = New System.Drawing.Point(553, 80)
        Me.ddlMaritalStatus.Name = "ddlMaritalStatus"
        Me.ddlMaritalStatus.Size = New System.Drawing.Size(137, 21)
        Me.ddlMaritalStatus.TabIndex = 16
        '
        'ddlGender
        '
        Me.ddlGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlGender.FormattingEnabled = True
        Me.ddlGender.Items.AddRange(New Object() {"Male", "Female"})
        Me.ddlGender.Location = New System.Drawing.Point(553, 28)
        Me.ddlGender.Name = "ddlGender"
        Me.ddlGender.Size = New System.Drawing.Size(137, 21)
        Me.ddlGender.TabIndex = 7
        '
        'ddlCity
        '
        Me.ddlCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlCity.FormattingEnabled = True
        Me.ddlCity.Location = New System.Drawing.Point(128, 166)
        Me.ddlCity.Name = "ddlCity"
        Me.ddlCity.Size = New System.Drawing.Size(137, 21)
        Me.ddlCity.TabIndex = 24
        '
        'ddlDept
        '
        Me.ddlDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlDept.FormattingEnabled = True
        Me.ddlDept.Location = New System.Drawing.Point(128, 219)
        Me.ddlDept.Name = "ddlDept"
        Me.ddlDept.Size = New System.Drawing.Size(137, 21)
        Me.ddlDept.TabIndex = 32
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(31, 222)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(62, 13)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Department"
        '
        'ddlDesignation
        '
        Me.ddlDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlDesignation.FormattingEnabled = True
        Me.ddlDesignation.Location = New System.Drawing.Point(554, 235)
        Me.ddlDesignation.Name = "ddlDesignation"
        Me.ddlDesignation.Size = New System.Drawing.Size(137, 21)
        Me.ddlDesignation.TabIndex = 36
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(456, 239)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(63, 13)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Designation"
        '
        'dtpLeaving
        '
        Me.dtpLeaving.CustomFormat = "dd/MM/yyyy"
        Me.dtpLeaving.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLeaving.Location = New System.Drawing.Point(553, 289)
        Me.dtpLeaving.Name = "dtpLeaving"
        Me.dtpLeaving.Size = New System.Drawing.Size(137, 20)
        Me.dtpLeaving.TabIndex = 41
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(455, 290)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(71, 13)
        Me.Label20.TabIndex = 40
        Me.Label20.Text = "Leaving Date"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(355, 315)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(58, 23)
        Me.btnCancel.TabIndex = 43
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(291, 315)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(58, 23)
        Me.btnSave.TabIndex = 42
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grdSaved
        '
        Me.grdSaved.AllowUserToAddRows = False
        Me.grdSaved.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSaved.Location = New System.Drawing.Point(2, 344)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(770, 236)
        Me.grdSaved.TabIndex = 44
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(128, 258)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(563, 29)
        Me.txtComments.TabIndex = 38
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(31, 264)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(56, 13)
        Me.Label21.TabIndex = 37
        Me.Label21.Text = "Comments"
        '
        'txtEmpID
        '
        Me.txtEmpID.Location = New System.Drawing.Point(301, 84)
        Me.txtEmpID.Name = "txtEmpID"
        Me.txtEmpID.Size = New System.Drawing.Size(137, 20)
        Me.txtEmpID.TabIndex = 14
        '
        'frmDefEmployeeOld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 592)
        Me.Controls.Add(Me.txtEmpID)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.grdSaved)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.dtpLeaving)
        Me.Controls.Add(Me.ddlDesignation)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.ddlDept)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.ddlCity)
        Me.Controls.Add(Me.ddlGender)
        Me.Controls.Add(Me.ddlMaritalStatus)
        Me.Controls.Add(Me.txtSalary)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.dtpJoining)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.dtpDOB)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtReligion)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtFather)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtNTN)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.txtNIC)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDefEmployeeOld"
        Me.Text = "Employee"
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNTN As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtNIC As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFather As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtReligion As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpDOB As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpJoining As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSalary As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents ddlMaritalStatus As System.Windows.Forms.ComboBox
    Friend WithEvents ddlGender As System.Windows.Forms.ComboBox
    Friend WithEvents ddlCity As System.Windows.Forms.ComboBox
    Friend WithEvents ddlDept As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ddlDesignation As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents dtpLeaving As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdSaved As System.Windows.Forms.DataGridView
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtEmpID As System.Windows.Forms.TextBox
End Class
