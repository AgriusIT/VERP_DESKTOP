'' Task 20150204-01 Credit And Cash Sales Display, Imran Ali
Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class ucEmpAttendance

    Dim DateFrom As DateTime = Now  'ToDo Filter Query Date Range
    Dim DateTo As DateTime = Now  'ToDo Filter Query Date Range
    Dim flgCompanyRights As Boolean = False
    Dim _dtData As DataTable
    Private _dtpDateFrom As DateTimePicker
    Public Property dtpDateFrom() As DateTimePicker
        Get
            Return _dtpDateFrom
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateFrom = value

        End Set
    End Property

    Private _dtpDateTo As DateTimePicker
    Public Property dtpDateTo() As DateTimePicker
        Get
            Return _dtpDateTo
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateTo = value
        End Set
    End Property
    Public Function GetEmpAttendance(Optional ByVal Condition As String = "") As DataTable
        If Con.State = ConnectionState.Closed Then Con.Open() 'Open Database Connection
        Try
            'Set SQL Select Statment in strSQL
            Dim strSQL As String = String.Empty
            If Condition = "Absent" Then
                strSQL = "SELECT 'Absent' AS AttendanceStatus, COUNT(0) AS AttendanceCount FROM    dbo.tblDefEmployee AS Emp WHERE Employee_Id Not IN (SELECT EmpId  FROM dbo.tblAttendanceDetail WHERE (Convert(varchar,AttendanceDate,102) = Convert(DateTime,'" & dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102))) AND IsNull(Emp.Active,0)=1 "
            Else
                strSQL = "SELECT   AttendanceStatus, COUNT(AttendanceTime) AS AttendanceCount FROM dbo.tblAttendanceDetail AS Att WHERE (AttendanceStatus = 'Present') AND (CONVERT(varchar, AttendanceDate, 102) = CONVERT(DateTime, '" & dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND Att.EmpID Not In(Select Employee_Id From tblDefEmployee WHERE IsNull(Active,0)=0) GROUP BY AttendanceStatus"
            End If
            Dim dt As New DataTable 'Create object of datatable
            dt = GetDataTable(strSQL) 'Using datatable function (GetDataTable()) to assign dt object.
            dt.AcceptChanges() 'Update Record In DataTable

            Return dt 'Return object

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub urSalesType_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If dtpDateFrom Is Nothing Then
                dtpDateFrom = New DateTimePicker
                dtpDateFrom.Value = Now
            End If
            If dtpDateTo Is Nothing Then
                dtpDateTo = New DateTimePicker
                dtpDateTo.Value = Now
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            _dtData = GetEmpAttendance("Present")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            _dtData = GetEmpAttendance("Absent")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Dim lbl As New Label
        Me.Controls.Add(lbl)
        lbl.BackColor = Color.White
        lbl.AutoSize = False
        lbl.Dock = DockStyle.Fill
        lbl.TextAlign = ContentAlignment.MiddleCenter
        lbl.BringToFront()
        Application.DoEvents()
        lbl.Text = "Loading..."

        Try

            If _dtData.Rows.Count > 0 Then
                Me.lblPresent.Text = Val(_dtData.Rows(0).Item(1).ToString)
            Else
                Me.lblPresent.Text = 0
            End If
            Me.lblTotal.Text = Val(Me.lblPresent.Text) + Val(Me.lblLeaves.Text)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        Dim lbl As New Label
        Me.Controls.Add(lbl)
        lbl.BackColor = Color.White
        lbl.AutoSize = False
        lbl.Dock = DockStyle.Fill
        lbl.TextAlign = ContentAlignment.MiddleCenter
        lbl.BringToFront()
        Application.DoEvents()
        lbl.Text = "Loading..."
        Try
            If _dtData.Rows.Count > 0 Then
                Me.lblLeaves.Text = Val(_dtData.Rows(0).Item(1).ToString)
            Else
                Me.lblLeaves.Text = 0
            End If
            Me.lblTotal.Text = Val(Me.lblPresent.Text) + Val(Me.lblLeaves.Text)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
