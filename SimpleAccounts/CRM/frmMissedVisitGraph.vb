Imports SBDal
Imports SBModel
Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmMissedVisitGraph
    Implements IGeneral
    Dim Assets As AssetBE
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub BindChart()
        Try
            Dim dt As DataTable
            Dim str As String

            chrtApprovedMissedVisit.Hide()
            chrtMissedVisitGraph.Hide()
            chrtApprovedMissedVisitPie.Hide()
            chrtMissedVisitPieGraph.Hide()
            chrtInsideBar.Hide()
            chrtInsidePie.Hide()

            If cmbInside.SelectedValue > 0 Then
                If rdoBarGraph.Checked = True Then

                    chrtInsideBar.Show()
                    str = " SELECT         tblDefEmployee_1.Employee_Name As EmployeeName,COUNT(CASE WHEN LeadActivity.IsConfirmed = '0' THEN LeadActivity.IsConfirmed END) AS NotConfirmed,COUNT(CASE WHEN LeadActivity.IsConfirmed = '1' THEN LeadActivity.IsConfirmed END) AS Confirmed " & _
                                " FROM            tblDefEmployee RIGHT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 RIGHT OUTER JOIN LeadActivity ON tblDefEmployee_2.Employee_ID = LeadActivity.Manager_Employee_Id LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee_1.Employee_ID ON tblDefEmployee.Employee_ID = LeadActivity.ResponsiblePerson_Employee_Id " & _
                                " WHERE " & IIf(cmbResponsible.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id  =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id  =  " & cmbInside.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id  =  " & cmbManager.SelectedValue & "AND", "") & " LeadActivity.ActivityDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND LeadActivity.ActivityDate   <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)  " & _
                                " GROUP BY tblDefEmployee_1.Employee_Name   "

                    dt = GetDataTable(str)
                    chrtInsideBar.DataSource = dt

                    chrtInsideBar.Series("Not Confirmed").XValueMember = "EmployeeName"
                    chrtInsideBar.Series("Not Confirmed").YValueMembers = "NotConfirmed"

                    'chrtMissedVisitGraph.Series("Missed").YValueMembers = "Missed"
                    chrtInsideBar.Series("Confirmed").XValueMember = "EmployeeName"
                    chrtInsideBar.Series("Confirmed").YValueMembers = "Confirmed"



                    ' Set chart type like Bar chart, Pie chart 
                    chrtInsideBar.Series("Confirmed").ChartType = SeriesChartType.StackedColumn
                    chrtInsideBar.Series("Not Confirmed").ChartType = SeriesChartType.StackedColumn
                    ' To show chart value           
                    chrtInsideBar.Series("Not Confirmed").IsValueShownAsLabel = True
                    chrtInsideBar.Series("Confirmed").IsValueShownAsLabel = True
                Else
                    chrtInsidePie.Show()
                    str = " SELECT         tblDefEmployee_1.Employee_Name As EmployeeName,COUNT(CASE WHEN LeadActivity.IsConfirmed = '0' THEN LeadActivity.IsConfirmed END) AS NotConfirmed,COUNT(CASE WHEN LeadActivity.IsConfirmed = '1' THEN LeadActivity.IsConfirmed END) AS Confirmed " & _
                                " FROM            tblDefEmployee RIGHT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 RIGHT OUTER JOIN LeadActivity ON tblDefEmployee_2.Employee_ID = LeadActivity.Manager_Employee_Id LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee_1.Employee_ID ON tblDefEmployee.Employee_ID = LeadActivity.ResponsiblePerson_Employee_Id " & _
                                " WHERE " & IIf(cmbResponsible.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id  =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id  =  " & cmbInside.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id  =  " & cmbManager.SelectedValue & "AND", "") & " LeadActivity.ActivityDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND LeadActivity.ActivityDate   <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)  " & _
                                " GROUP BY tblDefEmployee_1.Employee_Name   "

                    dt = GetDataTable(str)
                    chrtInsidePie.DataSource = dt

                    chrtInsidePie.Series("Series2").XValueMember = "EmployeeName"
                    chrtInsidePie.Series("Series2").YValueMembers = "Confirmed"
                    'chrtInsidePie.Series("Series3").XValueMember = "EmployeeName"
                    chrtInsidePie.Series("Series3").YValueMembers = "NotConfirmed"



                    ' Set chart type like Bar chart, Pie chart 
                    chrtInsidePie.Series("Series2").ChartType = SeriesChartType.Pie
                    chrtInsidePie.Series("Series3").ChartType = SeriesChartType.Pie

                    ' To show chart value           
                    chrtInsidePie.Series("Series2").IsValueShownAsLabel = True
                    chrtInsidePie.Series("Series3").IsValueShownAsLabel = True
                End If

            Else




                If chkVisitedMissedGraph.Checked = False Then


                    chrtMissedVisitGraph.Show()

                    If Me.rdoBarGraph.Checked = True Then
                        chrtMissedVisitGraph.Show()

                        'str = "select tblDefEmployee.Employee_Name AS Employee, " & _
                        '" Count(Case WHEN ActivityFeedbackStatus.Status = 'Missed' Then ActivityFeedbackStatus.Status End) AS Visited " & _
                        '"  , Count(Case WHEN ActivityFeedbackStatus.Status = 'Complete' Then ActivityFeedbackStatus.Status End) AS Missed  from ActivityFeedback " & _
                        '" left outer join LeadActivity ON ActivityFeedback.ActivityId = LeadActivity.ActivityId " & _
                        '" left outer join ActivityFeedbackStatus on ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId  " & _
                        '" LEFT OUTER JOIN tblDefEmployee ON  LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID " & _
                        '" where " & IIf(cmbResponsible.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id  =  " & cmbResponsible.SelectedValue & "AND", "") & " ActivityFeedback.FeedbackDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityFeedback.FeedbackDate  <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102) " & _
                        '"  Group by tblDefEmployee.Employee_Name"


                        str = " SELECT        tblDefEmployee.Employee_Name AS Employee, COUNT(CASE WHEN ActivityFeedbackStatus.Status = 'Missed' THEN ActivityFeedbackStatus.Status END) AS Visited,  COUNT(CASE WHEN ActivityFeedbackStatus.Status = 'Complete' THEN ActivityFeedbackStatus.Status END) AS Missed " & _
                                " FROM tblDefEmployee AS tblDefEmployee_2 RIGHT OUTER JOIN LeadActivity ON tblDefEmployee_2.Employee_ID = LeadActivity.Manager_Employee_Id LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee_1.Employee_ID RIGHT OUTER JOIN ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId LEFT OUTER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId LEFT OUTER JOIN tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID " & _
                                " WHERE " & IIf(cmbResponsible.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id  =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id  =  " & cmbManager.SelectedValue & "AND", "") & " ActivityFeedback.FeedbackDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityFeedback.FeedbackDate  <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)  " & _
                                " GROUP BY tblDefEmployee.Employee_Name  "

                        dt = GetDataTable(str)
                        chrtMissedVisitGraph.DataSource = dt

                        chrtMissedVisitGraph.Series("Missed").XValueMember = "Employee"
                        chrtMissedVisitGraph.Series("Missed").YValueMembers = "Visited"

                        'chrtMissedVisitGraph.Series("Missed").YValueMembers = "Missed"

                        chrtMissedVisitGraph.Series("Visited").XValueMember = "Employee"
                        chrtMissedVisitGraph.Series("Visited").YValueMembers = "Missed"


                        ' Set chart type like Bar chart, Pie chart 
                        chrtMissedVisitGraph.Series("Visited").ChartType = SeriesChartType.StackedColumn
                        chrtMissedVisitGraph.Series("Missed").ChartType = SeriesChartType.StackedColumn
                        ' To show chart value           
                        chrtMissedVisitGraph.Series("Visited").IsValueShownAsLabel = True
                        chrtMissedVisitGraph.Series("Missed").IsValueShownAsLabel = True
                    Else


                        chrtMissedVisitPieGraph.Show()

                        str = " SELECT     tblDefEmployee.Employee_Name AS Employee,  COUNT(CASE WHEN ActivityFeedbackStatus.Status = 'Missed' THEN ActivityFeedbackStatus.Status END) AS Visited,  COUNT(CASE WHEN ActivityFeedbackStatus.Status = 'Complete' THEN ActivityFeedbackStatus.Status END) AS Missed " & _
                                " FROM tblDefEmployee AS tblDefEmployee_2 RIGHT OUTER JOIN LeadActivity ON tblDefEmployee_2.Employee_ID = LeadActivity.Manager_Employee_Id LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee_1.Employee_ID RIGHT OUTER JOIN ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId LEFT OUTER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId LEFT OUTER JOIN tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID " & _
                                " WHERE " & IIf(cmbResponsible.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id  =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id  =  " & cmbInside.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id  =  " & cmbManager.SelectedValue & "AND", "") & " ActivityFeedback.FeedbackDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityFeedback.FeedbackDate  <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)  " & _
                         " GROUP BY tblDefEmployee.Employee_Name  "

                        dt = GetDataTable(str)
                        chrtMissedVisitPieGraph.DataSource = dt

                        chrtMissedVisitPieGraph.Series("Series2").YValueMembers = "Employee"
                        chrtMissedVisitPieGraph.Series("Series2").XValueMember = "Visited"

                        chrtMissedVisitPieGraph.Series("Series3").YValueMembers = "Employee"
                        chrtMissedVisitPieGraph.Series("Series3").XValueMember = "Missed"

                        'chrtMissedVisitGraph.Series("Visited").XValueMember = "Employee"
                        'chrtMissedVisitGraph.Series("Visited").YValueMembers = "Missed"


                        ' Set chart type like Bar chart, Pie chart 
                        chrtMissedVisitPieGraph.Series("Series2").ChartType = SeriesChartType.Pie
                        chrtMissedVisitPieGraph.Series("Series3").ChartType = SeriesChartType.Pie
                        ' To show chart value           
                        chrtMissedVisitPieGraph.Series("Series2").IsValueShownAsLabel = True
                        chrtMissedVisitPieGraph.Series("Series3").IsValueShownAsLabel = True

                    End If
                Else

                    chrtApprovedMissedVisit.Show()


                    If rdoBarGraph.Checked = True Then


                        chrtApprovedMissedVisit.Show()

                        'str = "SELECT tblDefEmployee.Employee_Name As Employee,  Count( case When ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedVisits , " & _
                        '    " Count (case When ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedMissed, " & _
                        '    " Count (case when ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As VisitRejected, " & _
                        '    " Count (case when ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As MissedRejected " & _
                        '    " FROM            ActivityFeedback INNER JOIN " & _
                        '    " LeadActivity ON ActivityFeedback.ActivityId = LeadActivity.ActivityId RIGHT OUTER JOIN " & _
                        '    " ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId LEFT OUTER JOIN " & _
                        '    " tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID " & _
                        '     " where ActivityFeedback.FeedbackDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityFeedback.FeedbackDate  <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102) " & _
                        '    "  Group By tblDefEmployee.Employee_Name  "

                        str = " SELECT tblDefEmployee.Employee_Name As Employee,  Count( case When ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedVisits ,  Count (case When ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedMissed,  Count (case when ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As VisitRejected,  Count (case when ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As MissedRejected   " & _
                            " FROM tblDefEmployee AS tblDefEmployee_2 RIGHT OUTER JOIN LeadActivity ON tblDefEmployee_2.Employee_ID = LeadActivity.Manager_Employee_Id LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee_1.Employee_ID RIGHT OUTER JOIN ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId LEFT OUTER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId LEFT OUTER JOIN tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID " & _
                            " WHERE " & IIf(cmbResponsible.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id  =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id  =  " & cmbInside.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id  =  " & cmbManager.SelectedValue & "AND", "") & " ActivityFeedback.FeedbackDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityFeedback.FeedbackDate  <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)  " & _
                            " Group By tblDefEmployee.Employee_Name   "


                        dt = GetDataTable(str)
                        chrtApprovedMissedVisit.DataSource = dt

                        chrtApprovedMissedVisit.Series("Approved Missed").XValueMember = "Employee"
                        chrtApprovedMissedVisit.Series("Approved Missed").YValueMembers = "ApprovedMissed"

                        'chrtMissedVisitGraph.Series("Missed").YValueMembers = "Missed"

                        chrtApprovedMissedVisit.Series("Approved Visited").XValueMember = "Employee"
                        chrtApprovedMissedVisit.Series("Approved Visited").YValueMembers = "ApprovedVisits"

                        chrtApprovedMissedVisit.Series("Missed Rejected").XValueMember = "Employee"
                        chrtApprovedMissedVisit.Series("Missed Rejected").YValueMembers = "MissedRejected"

                        chrtApprovedMissedVisit.Series("Visit Rejected").XValueMember = "Employee"
                        chrtApprovedMissedVisit.Series("Visit Rejected").YValueMembers = "VisitRejected"

                        ' Set chart type like Bar chart, Pie chart 
                        chrtApprovedMissedVisit.Series("Approved Visited").ChartType = SeriesChartType.StackedColumn
                        chrtApprovedMissedVisit.Series("Approved Missed").ChartType = SeriesChartType.StackedColumn
                        chrtApprovedMissedVisit.Series("Missed Rejected").ChartType = SeriesChartType.StackedColumn
                        chrtApprovedMissedVisit.Series("Visit Rejected").ChartType = SeriesChartType.StackedColumn
                        ' To show chart value           
                        chrtApprovedMissedVisit.Series("Approved Visited").IsValueShownAsLabel = True
                        chrtApprovedMissedVisit.Series("Approved Missed").IsValueShownAsLabel = True
                        chrtApprovedMissedVisit.Series("Missed Rejected").IsValueShownAsLabel = True
                        chrtApprovedMissedVisit.Series("Visit Rejected").IsValueShownAsLabel = True
                    Else


                        chrtApprovedMissedVisitPie.Show()

                        'str = "SELECT tblDefEmployee.Employee_Name As Employee,  Count( case When ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedVisits , " & _
                        '    " Count (case When ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedMissed, " & _
                        '    " Count (case when ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As VisitRejected, " & _
                        '    " Count (case when ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As MissedRejected " & _
                        '    " FROM            ActivityFeedback INNER JOIN " & _
                        '    " LeadActivity ON ActivityFeedback.ActivityId = LeadActivity.ActivityId RIGHT OUTER JOIN " & _
                        '    " ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId LEFT OUTER JOIN " & _
                        '    " tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID " & _
                        '     " where ActivityFeedback.FeedbackDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityFeedback.FeedbackDate  <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102) " & _
                        '    "  Group By tblDefEmployee.Employee_Name  "

                        str = " SELECT tblDefEmployee.Employee_Name As Employee,  Count( case When ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedVisits ,  Count (case When ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 1 Then ActivityFeedbackStatus.Status End ) As ApprovedMissed,  Count (case when ActivityFeedbackStatus.Status = 'Complete' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As VisitRejected,  Count (case when ActivityFeedbackStatus.Status = 'Missed' And ActivityFeedback.Approval = 0 Then ActivityFeedbackStatus.Status End ) As MissedRejected   " & _
                            " FROM tblDefEmployee AS tblDefEmployee_2 RIGHT OUTER JOIN LeadActivity ON tblDefEmployee_2.Employee_ID = LeadActivity.Manager_Employee_Id LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee_1.Employee_ID RIGHT OUTER JOIN ActivityFeedback ON LeadActivity.ActivityId = ActivityFeedback.ActivityId LEFT OUTER JOIN ActivityFeedbackStatus ON ActivityFeedback.ActivityFeedbackStatusId = ActivityFeedbackStatus.ActivityFeedbackStatusId LEFT OUTER JOIN tblDefEmployee ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee.Employee_ID " & _
                            " WHERE " & IIf(cmbResponsible.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id  =  " & cmbResponsible.SelectedValue & "AND", "") & " " & IIf(cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id  =  " & cmbInside.SelectedValue & "AND", "") & " " & IIf(cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id  =  " & cmbManager.SelectedValue & "AND", "") & " ActivityFeedback.FeedbackDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityFeedback.FeedbackDate  <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)  " & _
                            " Group By tblDefEmployee.Employee_Name   "


                        dt = GetDataTable(str)
                        chrtApprovedMissedVisitPie.DataSource = dt
                        chrtApprovedMissedVisitPie.Series("Series2").XValueMember = "Employee"
                        chrtApprovedMissedVisitPie.Series("Series2").YValueMembers = "ApprovedMissed"
                        chrtApprovedMissedVisitPie.Series("Series2").YValueMembers = "ApprovedVisits"
                        'chrtApprovedMissedVisitPie.Series("Series1").YValueMembers = "MissedRejected"
                        'chrtApprovedMissedVisitPie.Series("Series1").YValueMembers = "VisitRejected"

                        chrtApprovedMissedVisitPie.Series("Series2").ChartType = SeriesChartType.Pie
                        chrtApprovedMissedVisitPie.Series("Series2").IsValueShownAsLabel = True


                    End If




                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmMissedVisitGraph_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            chrtMissedVisitGraph.Titles.Add("Visited vs Missed")
            FillDropDown(Me.cmbResponsible, "Select Employee_ID, Employee_Name From tblDefEmployee", True)
            FillDropDown(Me.cmbInside, "Select Employee_ID, Employee_Name From tblDefEmployee", True)
            FillDropDown(Me.cmbManager, "Select Employee_ID, Employee_Name  From tblDefEmployee", True)
            Me.cmbPeriod.Text = "Current Month"
            Me.cmbReport.SelectedIndex = 0
            Me.chkVisitedMissedGraph.Checked = False
            Me.rdoBarGraph.Checked = True
            BindChart()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub frmMissedVisitGraph_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            BindChart()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-1)
            Me.dtpToDate.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpToDate.Value = Date.Today
        End If
    End Sub
    Private Sub rbtBar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtBar.CheckedChanged
        Try
            If rbtBar.Checked = True Then
                Me.PictureBox1.Image = My.Resources.bar_graph
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtPie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtPie.CheckedChanged
        Try
            If Me.rbtPie.Checked = True Then
                Me.PictureBox1.Image = My.Resources.pie_chart_icon
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged

    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If rbtBar.Checked = True Then
                If Me.cmbReport.SelectedIndex = 0 Then
                    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                    AddRptParam("@ResponsiblePersonId", Me.cmbResponsible.SelectedValue)
                    AddRptParam("@InsidePersonId", Me.cmbInside.SelectedValue)
                    AddRptParam("@ManagerId", Me.cmbManager.SelectedValue)
                    ShowReport("rptGraphVisitedMisedBar")
                ElseIf Me.cmbReport.SelectedIndex = 1 Then
                    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                    AddRptParam("@ResponsiblePersonId", Me.cmbResponsible.SelectedValue)
                    AddRptParam("@InsidePersonId", Me.cmbInside.SelectedValue)
                    AddRptParam("@ManagerId", Me.cmbManager.SelectedValue)
                    ShowReport("rptGraphApprovedNotApprovedBar")
                ElseIf Me.cmbReport.SelectedIndex = 2 Then
                    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                    AddRptParam("@InsidePersonId", Me.cmbInside.SelectedValue)
                    ShowReport("rptGraphConfirmedNotConfirmedBar")
                End If
            Else
                If Me.cmbReport.SelectedIndex = 0 Then
                    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                    AddRptParam("@ResponsiblePersonId", Me.cmbResponsible.SelectedValue)
                    AddRptParam("@InsidePersonId", Me.cmbInside.SelectedValue)
                    AddRptParam("@ManagerId", Me.cmbManager.SelectedValue)
                    ShowReport("rptGraphVisitedMisedPie")
                ElseIf Me.cmbReport.SelectedIndex = 1 Then
                    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                    AddRptParam("@ResponsiblePersonId", Me.cmbResponsible.SelectedValue)
                    AddRptParam("@InsidePersonId", Me.cmbInside.SelectedValue)
                    AddRptParam("@ManagerId", Me.cmbManager.SelectedValue)
                    ShowReport("rptGraphApprovedNotApprovedPie")
                ElseIf Me.cmbReport.SelectedIndex = 2 Then
                    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
                    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
                    AddRptParam("@InsidePersonId", Me.cmbInside.SelectedValue)
                    ShowReport("rptGraphConfirmedNotConfirmedPie")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class