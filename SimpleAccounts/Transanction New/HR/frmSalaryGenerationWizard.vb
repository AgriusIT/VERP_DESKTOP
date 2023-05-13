''TASK-484 Muhammad Ameen on 18-07-2016. Add feature of Salary Generation Wizard
Imports System.Windows.Media

Public Class frmSalaryGenerationWizard
    '    Public IsActiveAttendanceSetup As Boolean = False
    '    Public IsActiveLoadDeduction As Boolean = False
    '    Public IsActiveOverTime As Boolean = False
    '    Public IsActiveSalaryGeneration As Boolean = False
    '    Public IsActivePanelAS As Boolean = False
    '    Public IsActivePanelDA As Boolean = False
    '    Public IsActivePanelAR As Boolean = False
    '    Public IsActivePanelLD As Boolean = False
    '    Public IsActivePanelAO As Boolean = False
    '    Public IsActivePanelEO As Boolean = False
    '    Public IsActivePanelASG As Boolean = False
    '    Private NextAS As Boolean = False
    '    Private NextLD As Boolean = False
    '    Private NextOT As Boolean = False
    '    Private NextSG As Boolean = False
    '    Private Back As Boolean = False
    '    Private Forward As Boolean = False



    '    Private Sub frmSalaryGenerationWizard_Load(sender As Object, e As EventArgs) Handles Me.Load
    '        Try
    '            SetLoadDefault()
    '            Me.BackColor = System.Drawing.Color.White
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub



    '    Private Sub btnAttendanceSetup_MouseEnter(sender As Object, e As EventArgs) Handles btnAttendanceSetup.MouseEnter
    '        Try

    '            'Me.btnAttendanceSetup.Image = ((System.Drawing.Image)(Properties.Resources.img2))
    '            ''this.button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.img2));
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnAttendanceSetup_MouseHover(sender As Object, e As EventArgs) Handles btnAttendanceSetup.MouseHover
    '        Try
    '            With btnAttendanceSetup
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnAttendanceSetup_MouseLeave(sender As Object, e As EventArgs) Handles btnAttendanceSetup.MouseLeave
    '        Try
    '            If IsActiveAttendanceSetup = False Then
    '                With btnAttendanceSetup
    '                    .Image = My.Resources._100_reso_button_gray
    '                End With
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub


    '    Private Sub btnOverTime_MouseHover(sender As Object, e As EventArgs) Handles btnOverTime.MouseHover

    '        Try
    '            With btnOverTime
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnOverTime_MouseLeave(sender As Object, e As EventArgs) Handles btnOverTime.MouseLeave
    '        Try
    '            If IsActiveOverTime = False Then
    '                With btnOverTime
    '                    .Image = My.Resources._100_reso_button_gray
    '                End With
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnSalaryGeneration_MouseHover(sender As Object, e As EventArgs) Handles btnSalaryGeneration.MouseHover
    '        Try
    '            With btnSalaryGeneration
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnSalaryGeneration_MouseLeave(sender As Object, e As EventArgs) Handles btnSalaryGeneration.MouseLeave
    '        Try
    '            If IsActiveSalaryGeneration = False Then
    '                With btnSalaryGeneration()
    '                    .Image = My.Resources._100_reso_button_gray
    '                End With
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnLoanDeduction_MouseHover_1(sender As Object, e As EventArgs) Handles btnLoanDeduction.MouseHover
    '        Try
    '            With btnLoanDeduction
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '        Catch ex As Exception
    '            Show(ErrorMessage(ex.Message))
    '        End Try
    '    End Sub

    '    Private Sub btnLoanDeduction_MouseLeave_1(sender As Object, e As EventArgs) Handles btnLoanDeduction.MouseLeave
    '        Try
    '            If IsActiveLoadDeduction = False Then
    '                With btnLoanDeduction()
    '                    .Image = My.Resources._100_reso_button_gray
    '                End With
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub


    '    Private Sub btnAttendanceSetup_Click(sender As Object, e As EventArgs) Handles btnAttendanceSetup.Click
    '        Try
    '            IsActiveAttendanceSetup = True
    '            IsActiveLoadDeduction = False
    '            IsActiveOverTime = False
    '            IsActiveSalaryGeneration = False
    '            IsActivePanelAS = True
    '            IsActivePanelDA = False
    '            IsActivePanelAR = False
    '            Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '            Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '            Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '            With btnAttendanceSetup
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '            Me.lblAttendanceSetup.Visible = True
    '            Me.lblDailyAttendance.Visible = True
    '            Me.lblAttendanceRegister.Visible = True
    '            Me.lblLoanDeduction.Visible = False
    '            Me.lblEmployeeOverTime.Visible = False
    '            Me.lblAutoOverTime.Visible = False
    '            Me.lblAutoSalaryGenerate.Visible = False
    '            'Me.lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            'lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            'lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            Me.lblAttendanceSetup_Click(Nothing, Nothing)
    '            'CallForm(frmHolidySetup)
    '            NextAS = True
    '            NextLD = False
    '            NextOT = False
    '            NextSG = False

    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub pnlPresent_MouseHover(sender As Object, e As EventArgs)
    '        Try
    '            'pnlAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub






    '    Private Sub pnlOffDays_MouseHover(sender As Object, e As EventArgs)
    '        Try
    '            lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub


    '    Private Sub lblPresent_MouseHover(sender As Object, e As EventArgs) Handles lblAttendanceSetup.MouseHover
    '        Try
    '            lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblPresent_MouseLeave(sender As Object, e As EventArgs) Handles lblAttendanceSetup.MouseLeave
    '        Try
    '            If IsActivePanelAS = False Then
    '                lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblAbsent_MouseHover(sender As Object, e As EventArgs) Handles lblDailyAttendance.MouseHover
    '        Try
    '            lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblAbsent_MouseLeave(sender As Object, e As EventArgs) Handles lblDailyAttendance.MouseLeave
    '        Try
    '            If IsActivePanelDA = False Then
    '                lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            End If
    'Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblOffDays_MouseHover(sender As Object, e As EventArgs) Handles lblAttendanceRegister.MouseHover
    '        Try
    '            lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblOffDays_MouseLeave(sender As Object, e As EventArgs) Handles lblAttendanceRegister.MouseLeave
    '        Try
    '            If IsActivePanelAR = False Then
    '                lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub SetLoadDefault()
    '        Try
    '            Me.btnAttendanceSetup_Click(Nothing, Nothing)
    '            NextAS = True
    '            Me.lblLoanDeduction.Visible = False
    '            Me.lblAutoOverTime.Visible = False
    '            Me.lblEmployeeOverTime.Visible = False
    '            Me.lblAutoSalaryGenerate.Visible = False

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub

    '    Private Sub btnLoanDeduction_Click(sender As Object, e As EventArgs) Handles btnLoanDeduction.Click
    '        Try
    '            IsActiveLoadDeduction = True
    '            IsActiveSalaryGeneration = False
    '            IsActiveOverTime = False
    '            IsActiveAttendanceSetup = False
    '            IsActivePanelLD = True
    '            Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '            Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '            Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '            With btnLoanDeduction
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '            Me.lblLoanDeduction.Visible = True
    '            Me.lblAttendanceSetup.Visible = False
    '            Me.lblDailyAttendance.Visible = False
    '            Me.lblAttendanceRegister.Visible = False
    '            Me.lblEmployeeOverTime.Visible = False
    '            Me.lblAutoOverTime.Visible = False
    '            Me.lblAutoSalaryGenerate.Visible = False
    '            Me.lblLoanDeduction.Location = New System.Drawing.Point(10, 27)
    '            'Me.pnlLoanDeduction.Size = New System.Drawing.Size(158, 34)
    '            Me.lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            Me.lblLoanDeduction_Click(Nothing, Nothing)
    '            'CallForm(frmEmployeeDeductions)
    '            NextLD = True
    '            NextAS = False
    '            NextOT = False
    '            NextSG = False
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnOverTime_Click(sender As Object, e As EventArgs) Handles btnOverTime.Click
    '        Try
    '            IsActiveOverTime = True
    '            IsActiveSalaryGeneration = False
    '            IsActiveLoadDeduction = False
    '            IsActiveAttendanceSetup = False
    '            IsActivePanelAO = True
    '            IsActivePanelEO = False
    '            Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '            Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '            Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '            With btnOverTime
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '            Me.lblLoanDeduction.Visible = False
    '            Me.lblAttendanceSetup.Visible = False
    '            Me.lblDailyAttendance.Visible = False
    '            Me.lblAttendanceRegister.Visible = False
    '            Me.lblEmployeeOverTime.Visible = True
    '            Me.lblAutoOverTime.Visible = True
    '            Me.lblAutoSalaryGenerate.Visible = False
    '            'Me.lblEmployeeOverTime.Location = New System.Drawing.Point(10, 27)
    '            'Me.lblAutoOverTime.Location = New System.Drawing.Point(10, 71)
    '            Me.lblEmployeeOverTime.Location = New System.Drawing.Point(10, 71)
    '            Me.lblAutoOverTime.Location = New System.Drawing.Point(10, 27)
    '            'Me.lblEmployeeOverTime_Click(Nothing, Nothing)
    '            Me.lblAutoOverTime_Click(Nothing, Nothing)
    '            NextOT = True
    '            NextLD = False
    '            NextAS = False
    '            NextSG = False
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnSalaryGeneration_Click(sender As Object, e As EventArgs) Handles btnSalaryGeneration.Click
    '        Try
    '            IsActiveSalaryGeneration = True
    '            IsActiveOverTime = False
    '            IsActiveLoadDeduction = False
    '            IsActiveAttendanceSetup = False
    '            IsActivePanelASG = True
    '            Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '            Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '            Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '            With btnSalaryGeneration
    '                .Image = My.Resources._100_reso_blue_button_new
    '            End With
    '            Me.lblLoanDeduction.Visible = False
    '            Me.lblAttendanceSetup.Visible = False
    '            Me.lblDailyAttendance.Visible = False
    '            Me.lblAttendanceRegister.Visible = False
    '            Me.lblEmployeeOverTime.Visible = False
    '            Me.lblAutoOverTime.Visible = False
    '            Me.lblAutoSalaryGenerate.Visible = True
    '            Me.lblAutoSalaryGenerate.Location = New System.Drawing.Point(10, 27)
    '            'Me.pnlAutoSalaryGenerate.Size = New System.Drawing.Size(153, 34)
    '            Me.lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            Me.lblAutoSalaryGenerate_Click(Nothing, Nothing)
    '            'CallForm(frmAutoSalaryGenerate)
    '            NextSG = True
    '            NextOT = False
    '            NextLD = False
    '            NextAS = False
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblLoanDeduction_MouseHover(sender As Object, e As EventArgs) Handles lblLoanDeduction.MouseHover
    '        Try
    '            Me.lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblLoanDeduction_MouseLeave(sender As Object, e As EventArgs) Handles lblLoanDeduction.MouseLeave
    '        Try
    '            If IsActivePanelLD = False Then
    '                lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub





    '    Private Sub lblAutoOverTime_MouseHover(sender As Object, e As EventArgs) Handles lblAutoOverTime.MouseHover
    '        Try
    '            lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblAutoOverTime_MouseLeave(sender As Object, e As EventArgs) Handles lblAutoOverTime.MouseLeave
    '        Try
    '            If IsActivePanelAO = False Then
    '                lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub



    '    Private Sub lblEmployeeOverTime_MouseHover(sender As Object, e As EventArgs) Handles lblEmployeeOverTime.MouseHover
    '        Try
    '            lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblEmployeeOverTime_MouseLeave(sender As Object, e As EventArgs) Handles lblEmployeeOverTime.MouseLeave
    '        Try
    '            If IsActivePanelEO = False Then
    '                lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub



    '    Private Sub lblAutoSalaryGenerate_MouseHover(sender As Object, e As EventArgs) Handles lblAutoSalaryGenerate.MouseHover
    '        Try
    '            lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblAutoSalaryGenerate_MouseLeave(sender As Object, e As EventArgs) Handles lblAutoSalaryGenerate.MouseLeave
    '        Try
    '            If IsActivePanelASG = False Then
    '                lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub








    '    Private Sub pnlAutoSalaryGenerate_Click(sender As Object, e As EventArgs)
    '        Try
    '            IsActivePanelASG = True
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub


    '    Private Sub lblAttendanceSetup_Click(sender As Object, e As EventArgs) Handles lblAttendanceSetup.Click
    '       Try
    '            IsActivePanelAS = True
    '            IsActivePanelDA = False
    '            IsActivePanelAR = False
    '            lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            CallForm(frmHolidySetup)

    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblDailyAttendance_Click(sender As Object, e As EventArgs) Handles lblDailyAttendance.Click
    '        Try
    '            IsActivePanelDA = True
    '            IsActivePanelAR = False
    '            IsActivePanelAS = False
    '            lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            CallForm(frmAttendanceEmployees)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblAttendanceRegister_Click(sender As Object, e As EventArgs) Handles lblAttendanceRegister.Click
    '        Try
    '            IsActivePanelAR = True
    '            IsActivePanelDA = False
    '            IsActivePanelAS = False
    '            lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            CallForm(frmGrdRptAttendanceRegister)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblLoanDeduction_Click(sender As Object, e As EventArgs) Handles lblLoanDeduction.Click
    '        Try
    '            IsActivePanelLD = True
    '            CallForm(frmEmployeeDeductions)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblAutoOverTime_Click(sender As Object, e As EventArgs) Handles lblAutoOverTime.Click
    '        Try
    '            IsActivePanelAO = True
    '            IsActivePanelEO = False
    '            Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            CallForm(frmEmployeeAutoOverTime)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblEmployeeOverTime_Click(sender As Object, e As EventArgs) Handles lblEmployeeOverTime.Click
    '        Try
    '            IsActivePanelEO = True
    '            IsActivePanelAO = False
    '            Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '            Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '            CallForm(frmEmpOverTimeSchedule)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub lblAutoSalaryGenerate_Click(sender As Object, e As EventArgs) Handles lblAutoSalaryGenerate.Click
    '        Try
    '            IsActivePanelASG = True
    '            CallForm(frmAutoSalaryGenerate)
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '        Try

    '            NextStep()

    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub
    '    'Private Sub NextScreens()
    '    '    Try

    '    '        'Public IsActiveAttendanceSetup As Boolean = False
    '    '        'Public IsActiveLoadDeduction As Boolean = False
    '    '        'Public IsActiveOverTime As Boolean = False
    '    '        'Public IsActiveSalaryGeneration As Boolean = False
    '    '        'Public IsActivePanelAS As Boolean = False
    '    '        'Public IsActivePanelDA As Boolean = False
    '    '        'Public IsActivePanelAR As Boolean = False
    '    '        'Public IsActivePanelLD As Boolean = False
    '    '        'Public IsActivePanelAO As Boolean = False
    '    '        'Public IsActivePanelEO As Boolean = False
    '    '        'Public IsActivePanelASG As Boolean = False
    '    '        If IsActiveAttendanceSetup = True Then
    '    '            If IsActivePanelAS = True Then
    '    '                IsActivePanelAS = False
    '    '                IsActivePanelDA = True
    '    '                IsActivePanelAR = False
    '    '                lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(181, 235, 249) ''System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmAttendanceEmployees)
    '    '            ElseIf IsActivePanelDA = True Then
    '    '                IsActivePanelAS = False
    '    '                IsActivePanelDA = False
    '    '                IsActivePanelAR = True
    '    '                lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(90, 211, 242) ''System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(181, 235, 249) '' System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmGrdRptAttendanceRegister)
    '    '            ElseIf IsActivePanelAR = True Then

    '    '                IsActivePanelLD = True
    '    '                IsActivePanelAS = False
    '    '                IsActivePanelDA = False
    '    '                IsActivePanelAR = False

    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '    '                With btnLoanDeduction
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With
    '    '                Me.lblLoanDeduction.Visible = True
    '    '                Me.lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmEmployeeDeductions)
    '    '            ElseIf IsActivePanelLD = True Then
    '    '                'IsActivePanelAO = False
    '    '                IsActivePanelEO = True
    '    '                IsActivePanelLD = False
    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '    '                With btnOverTime
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With



    '    '                Me.lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblEmployeeOverTime.Visible = True
    '    '                CallForm(frmEmpOverTimeSchedule)
    '    '            ElseIf IsActivePanelEO = True Then
    '    '                IsActivePanelEO = False
    '    '                IsActivePanelAO = True
    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '    '                With btnOverTime
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With
    '    '                Me.lblAutoOverTime.Visible = True
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmEmployeeAutoOverTime)
    '    '            ElseIf IsActivePanelAO = True Then
    '    '                IsActivePanelASG = True
    '    '                IsActivePanelAO = False
    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '    '                With btnSalaryGeneration
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With

    '    '                Me.lblAutoSalaryGenerate.Visible = True
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                Me.lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmAutoSalaryGenerate)
    '    '            End If
    '    '        ElseIf IsActiveLoadDeduction Then
    '    '            If IsActivePanelLD = True Then
    '    '                IsActivePanelLD = True
    '    '                lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmEmployeeDeductions)
    '    '            End If
    '    '        ElseIf IsActiveOverTime Then
    '    '            If IsActivePanelEO = True Then
    '    '                IsActivePanelEO = False
    '    '                IsActivePanelAO = True
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242) ''System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmEmployeeAutoOverTime)
    '    '            ElseIf IsActivePanelAO = True Then
    '    '                IsActivePanelEO = True
    '    '                IsActivePanelAO = False
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmEmpOverTimeSchedule)
    '    '            End If
    '    '        ElseIf IsActiveSalaryGeneration Then
    '    '            If IsActivePanelASG = True Then
    '    '                IsActivePanelASG = True
    '    '                Me.lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmAutoSalaryGenerate)
    '    '            End If
    '    '        End If
    '    '    Catch ex As Exception
    '    '        Throw ex
    '    '    End Try
    '    'End Sub
    '    'Private Sub BackScreens()
    '    '    Try

    '    '        If IsActiveAttendanceSetup = True Then
    '    '            If IsActivePanelDA = True Then
    '    '                IsActivePanelAS = True
    '    '                IsActivePanelDA = False
    '    '                IsActivePanelAR = False
    '    '                lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(90, 211, 242) ''System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                'lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242) '' System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                lblAttendanceSetup.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmHolidySetup)
    '    '            ElseIf IsActivePanelAR = True Then
    '    '                IsActivePanelAS = False
    '    '                IsActivePanelDA = True
    '    '                IsActivePanelAR = False
    '    '                IsActivePanelAR = False
    '    '                lblDailyAttendance.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmAttendanceEmployees)
    '    '            ElseIf IsActivePanelLD = True Then
    '    '                IsActivePanelAS = False
    '    '                IsActivePanelDA = False
    '    '                IsActivePanelAR = True
    '    '                IsActivePanelLD = False
    '    '                Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '    '                With btnAttendanceSetup
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With
    '    '                lblAttendanceRegister.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmGrdRptAttendanceRegister)
    '    '            ElseIf IsActivePanelEO = True Then
    '    '                IsActivePanelLD = True
    '    '                IsActivePanelAS = False
    '    '                IsActivePanelDA = False
    '    '                IsActivePanelAR = False
    '    '                IsActivePanelEO = False
    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '    '                With btnLoanDeduction
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With
    '    '                Me.lblLoanDeduction.Visible = True
    '    '                Me.lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmEmployeeDeductions)
    '    '            ElseIf IsActivePanelAO = True Then
    '    '                IsActivePanelEO = True
    '    '                IsActivePanelAO = False
    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '    '                With btnOverTime
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblEmployeeOverTime.Visible = True
    '    '                CallForm(frmEmpOverTimeSchedule)
    '    '            ElseIf IsActivePanelASG = True Then
    '    '                IsActivePanelASG = False
    '    '                IsActivePanelAO = True
    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnSalaryGeneration.Image = My.Resources._100_reso_button_gray
    '    '                With btnOverTime
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With
    '    '                Me.lblAutoOverTime.Visible = True
    '    '                Me.lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmEmployeeAutoOverTime)
    '    '            ElseIf IsActivePanelAO = True Then
    '    '                IsActivePanelASG = True
    '    '                IsActivePanelAO = False
    '    '                Me.btnAttendanceSetup.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnLoanDeduction.Image = My.Resources._100_reso_button_gray
    '    '                Me.btnOverTime.Image = My.Resources._100_reso_button_gray
    '    '                With btnSalaryGeneration
    '    '                    .Image = My.Resources._100_reso_blue_button_new
    '    '                End With

    '    '                Me.lblAutoSalaryGenerate.Visible = True
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                Me.lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmAutoSalaryGenerate)
    '    '            End If
    '    '        ElseIf IsActiveLoadDeduction Then
    '    '            If IsActivePanelLD = True Then
    '    '                IsActivePanelLD = True
    '    '                lblLoanDeduction.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmEmployeeDeductions)
    '    '            End If
    '    '        ElseIf IsActiveOverTime Then
    '    '            If IsActivePanelEO = True Then
    '    '                IsActivePanelEO = False
    '    '                IsActivePanelAO = True
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242) ''System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmEmployeeAutoOverTime)
    '    '            ElseIf IsActivePanelAO = True Then
    '    '                IsActivePanelEO = True
    '    '                IsActivePanelAO = False
    '    '                Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                Me.lblAutoOverTime.BackColor = System.Drawing.Color.FromArgb(90, 211, 242)
    '    '                CallForm(frmEmpOverTimeSchedule)
    '    '            End If
    '    '        ElseIf IsActiveSalaryGeneration Then
    '    '            If IsActivePanelASG = True Then
    '    '                IsActivePanelASG = True
    '    '                Me.lblAutoSalaryGenerate.BackColor = System.Drawing.Color.FromArgb(181, 235, 249)
    '    '                CallForm(frmAutoSalaryGenerate)
    '    '            End If
    '    '        End If
    '    '    Catch ex As Exception
    '    '        Throw ex
    '    '    End Try
    '    'End Sub

    '    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
    '        Try
    '            BackStep()
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub
    '    Private Sub CallForm(ByVal frm As Form)
    '        Try
    '            frm.TopLevel = False
    '            frm.AutoSize = False
    '            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    '            frm.Dock = DockStyle.Fill
    '            Panel6.Controls.Add(frm)
    '            frm.Show()
    '            frm.BringToFront()
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub
    '    Private Sub NextStep()
    '        Try
    '            If NextAS = True Then
    '                While IsActivePanelAR = False
    '                    If IsActivePanelAS = True Then
    '                        Me.lblDailyAttendance_Click(Nothing, Nothing)
    '                        Exit Sub
    '                    ElseIf IsActivePanelDA = True Then
    '                        Me.lblAttendanceRegister_Click(Nothing, Nothing)
    '                        Exit Sub
    '                    End If
    '                End While
    '                btnLoanDeduction_Click(Nothing, Nothing)
    '                NextAS = False
    '            ElseIf NextLD = True Then
    '                If IsActivePanelLD = True Then
    '                    btnOverTime_Click(Nothing, Nothing)
    '                    IsActivePanelLD = False
    '                    Exit Sub
    '                End If
    '                If IsActivePanelAO = True Then
    '                    Me.lblEmployeeOverTime_Click(Nothing, Nothing)
    '                    NextLD = False
    '                End If
    '            ElseIf NextOT = True Then
    '                If IsActivePanelAO = True Then
    '                    Me.lblEmployeeOverTime_Click(Nothing, Nothing)
    '                    IsActivePanelAO = False
    '                Else
    '                    btnSalaryGeneration_Click(Nothing, Nothing)
    '                    NextOT = False
    '                End If
    '            End If
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub
    '    Private Sub BackStep()
    '        Try
    '            If NextSG = True Then
    '                If IsActivePanelASG = True Then
    '                    btnOverTime_Click(Nothing, Nothing)
    '                    lblEmployeeOverTime_Click(Nothing, Nothing)
    '                    IsActivePanelASG = False
    '                    Exit Sub
    '                Else
    '                    Me.lblAutoOverTime_Click(Nothing, Nothing)
    '                    NextSG = False
    '                End If
    '            ElseIf NextOT = True Then
    '                If IsActivePanelEO = True Then
    '                    Me.lblAutoOverTime_Click(Nothing, Nothing)
    '                    Exit Sub
    '                End If
    '                If IsActivePanelAO = True Then
    '                    btnLoanDeduction_Click(Nothing, Nothing)
    '                    IsActivePanelAO = False
    '                    NextOT = False
    '                End If
    '                ElseIf NextLD = True Then
    '                If IsActivePanelLD = True Then
    '                    btnAttendanceSetup_Click(Nothing, Nothing)
    '                    Me.lblAttendanceRegister_Click(Nothing, Nothing)
    '                    IsActivePanelLD = False
    '                ElseIf IsActivePanelAR = True Then
    '                    Me.lblDailyAttendance_Click(Nothing, Nothing)
    '                ElseIf IsActivePanelDA = True Then
    '                    Me.lblAttendanceSetup_Click(Nothing, Nothing)
    '                    NextLD = False
    '                End If
    '            ElseIf NextAS = True Then
    '                If IsActivePanelAR = True Then
    '                    Me.lblDailyAttendance_Click(Nothing, Nothing)
    '                ElseIf IsActivePanelDA = True Then
    '                    Me.lblAttendanceSetup_Click(Nothing, Nothing)
    '                End If
    '            End If
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub

    '    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
    '        Try
    '            Me.Close()
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub btnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click
    '        Try
    '            Me.Close()
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    End Sub
End Class