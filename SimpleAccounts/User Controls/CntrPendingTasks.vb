''15-12-2015 TASKTFS151 Muhammad Ameen: Pending Today's Tasks Show By User on Home Screen
Public Class CntrPendingTasks
    Private _TodayDate As DateTime

    Public Property TodayDate() As DateTime
        Get
            Return _TodayDate
        End Get
        Set(ByVal value As DateTime)
            _TodayDate = value
        End Set
    End Property
    Private _FutureDate As DateTime
    Public Property FutureDate() As DateTime
        Get
            Return _FutureDate
        End Get
        Set(ByVal value As DateTime)
            _FutureDate = value
        End Set
    End Property
    Public Function TodayTasks(ByVal userID As Integer) As Integer
        Dim strQuery As String = String.Empty
        Dim dt As New DataTable
        Try

            ServerDate()
            strQuery = "Select Count(IsNull(AssignedTo, 0)) As AssignedCount From dbo.TblDefTasks Where AssignedTo ='" & LoginUserId & "' And Convert(varchar, ClosingDate, 102) Is Null And Convert(varchar, TaskDate, 102) = Convert(datetime, '" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102) "
            dt = GetDataTable(strQuery)
            If Not dt.Rows.Count = 0 Then
                Return dt.Rows(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetTasks()
        Try
            'Label1.Text = LoginUserName
            Label2.Text = TodayTasks(LoginUserId).ToString()
            Label3.Text = PreviousTasks(LoginUserId).ToString()
            Label4.Text = FutureTasks(LoginUserId).ToString()
            lblTotalTasks.Text = Val(Val(Label2.Text) + Val(Label3.Text) + Val(Label4.Text)).ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function PreviousTasks(ByVal userID As Integer) As Integer
        Dim strQuery As String = String.Empty
        Dim dt As New DataTable
        Try

            strQuery = "Select  Count(IsNull(AssignedTo, 0)) As AssignedCount From dbo.TblDefTasks Where AssignedTo ='" & LoginUserId & "' And Convert(varchar, ClosingDate, 102) Is Null And Convert(varchar, TaskDate, 102) < Convert(datetime, '" & Date.Today & "',102) "

            dt = GetDataTable(strQuery)
            If Not dt.Rows.Count = 0 Then
                Return dt.Rows(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FutureTasks(ByVal userId As Integer) As Integer
        Dim strQuery As String = String.Empty
        Dim dt As New DataTable
        Try


            strQuery = "Select  Count(IsNull(AssignedTo, 0)) As AssignedCount From dbo.TblDefTasks Where AssignedTo ='" & LoginUserId & "' And Convert(varchar, ClosingDate, 102) Is Null And Convert(varchar, TaskDate, 102) > Convert(datetime, '" & Date.Today & "',102) "

            dt = GetDataTable(strQuery)
            If Not dt.Rows.Count = 0 Then
                Return dt.Rows(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Dim strQuery As String = String.Empty
    'Dim dt As New DataTable
    '        strQuery = "Select TaskDate, Count(TaskId) As NewTask, 0 As ClosedTask FROM  dbo.TblDefTasks " _
    '            & " WHERE (Convert(varchar, TaskDate ,102)) >= (Convert(datetime, '" & Me.ccFrom.Value & "' ,102)) And (Convert(varchar, TaskDate ,102)) <= (Convert(datetime, '" & Me.ccTo.Value & "', 102))" _
    '            & " Group By TaskDate " _
    '            & " Union" _
    '            & " Select  ClosingDate, 0, Count(TaskId) As ClosedTask1 FROM  dbo.TblDefTasks" _
    '            & " WHERE (Convert(varchar, ClosingDate ,102)) >= (Convert(datetime,'" & Me.ccFrom.Value & "',102)) And (Convert(varchar, ClosingDate ,102)) <= (Convert(datetime, '" & Me.ccTo.Value & "', 102))" _
    '            & " Group By ClosingDate "


    Private Sub llblTodayTasks_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llblTodayTasks.LinkClicked
        Try
            frmMain.LoadControl("Tasks")
            frmTasks.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
            'Dim dt As New DataTable
            'Dim dv As New DataView
            'dt = frmTasks.GrdTask.DataSource
            'dv = dt.DefaultView
            'dv.RowFilter = "TaskDate ='" & Date.Today.ToString("yyyy-MM-dd hh:mm:ss tt") & "'"
            'dt = dv.ToTable()
            'dt.AcceptChanges()
            'frmTasks.GrdTask.DataSource = dt



        Catch ex As Exception
            ShowErrorMessage(ex.Message) 'Throw ex
        End Try
    End Sub
    Private Sub lblNewTask_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNewTask.LinkClicked
        Try
            frmMain.LoadControl("Tasks")
            frmTasks.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(0).TabPage.Tab
            'frmTasks.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message) 'Throw ex
        End Try
    End Sub
End Class
