Public Class frmNotification
    Dim NewNotificationCount As Integer = 0
    Dim dtNotificationList As DataTable
    Private Sub lblCloseReport_Click(sender As Object, e As EventArgs) Handles lblCloseReport.Click
        'frmMain.pnlNotification.Visible = False
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwGetPendingNotification.DoWork

        Try

            NewNotificationCount = New SBDal.NotificationDAL().GetPendingNotificationsCount(LoginUserId)


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Timer1.Enabled = False
            Timer1.Interval = 60000

            bgwGetPendingNotification.RunWorkerAsync()
            Do While bgwGetPendingNotification.IsBusy
                Application.DoEvents()
            Loop
            If NewNotificationCount > 0 Then
                'frmMain.btnNotification.Text = NewNotificationCount
                'frmMain.btnNotification.BackColor = Color.Red
                'frmMain.btnNotification.Font = New Font(frmMain.btnNotification.Font.FontFamily.Name, frmMain.btnNotification.Font.Size, FontStyle.Bold)

                Me.AgriusNotificationCenter.ShowBalloonTip(5000, "Agrius ERP", "You have " & NewNotificationCount & " new notifications", ToolTipIcon.Info)
                AgriusNotificationCenter.Text = "You have " & NewNotificationCount & " new notifications"

            Else
                'frmMain.btnNotification.Text = String.Empty
                'frmMain.btnNotification.BackColor = Color.Transparent
                'frmMain.btnNotification.Font = New Font(frmMain.btnNotification.Font.FontFamily.Name, frmMain.btnNotification.Font.Size, FontStyle.Regular)

            End If

            bgwGetNotifications.RunWorkerAsync()

            Do While bgwGetNotifications.IsBusy
                Application.DoEvents()
            Loop

            DisplayNotifications()

        Catch ex As Exception
        Finally
            Timer1.Enabled = True
        End Try
    End Sub

    Private Sub bgwGetNotifications_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwGetNotifications.DoWork
        Try
            dtNotificationList = New SBDal.NotificationDAL().GetNotificationsList(LoginUserId)

        Catch ex As Exception
            msg_Error(ex.Message)

        End Try
    End Sub

    Sub DisplayNotifications()
        Try
            If dtNotificationList.Rows.Count > 0 Then
                For Each row As DataRow In dtNotificationList.Rows
                    If Not ListView1.Items.ContainsKey(row.Item("NotificationId").ToString) Then

                        Dim Item As New ListViewItem
                        Item.Name = row.Item("NotificationId").ToString
                        'Item.Text = row.Item("NotificationTitle").ToString
                        Item.Text = row.Item("NotificationDescription").ToString
                        If CType(row.Item("NotificationDate"), DateTime).ToString("dd-MMM-yyyy") = Date.Now.ToString("dd-MMM-yyyy") Then
                            Item.Group = ListView1.Groups("Current")
                        ElseIf CType(row.Item("NotificationDate"), DateTime).ToString("dd-MMM-yyyy") = Date.Now.AddDays(-1).ToString("dd-MMM-yyyy") Then
                            Item.Group = ListView1.Groups("Yesterday")
                        Else
                            Item.Group = ListView1.Groups("Old")

                        End If
                        '  Item.ImageIndex = 0
                        ListView1.Items.Insert(0, Item)

                    End If


                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Me.AgriusNotificationCenter.Visible = False
        End
    End Sub

    Private Sub frmNotification_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.AgriusNotificationCenter.Visible = False
    End Sub

    Private Sub AgriusNotificationCenter_BalloonTipClicked(sender As Object, e As EventArgs) Handles AgriusNotificationCenter.BalloonTipClicked
        Try

            'frmMain.pnlNotification.Visible = True
            'frmMain.pnlNotification.BringToFront()
            'frmMain.WindowState = FormWindowState.Maximized
            'frmMain.Activate()

            'frmMain.btnNotification.BackColor = Color.Transparent
            'frmMain.btnNotification.Font = New Font(frmMain.btnNotification.Font.FontFamily.Name, frmMain.btnNotification.Font.Size, FontStyle.Regular)
            'frmMain.btnNotification.Text = String.Empty

            Dim a = New SBDal.NotificationDAL().MarkNotificationRead(LoginUserId)

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub AgriusNotificationCenter_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AgriusNotificationCenter.MouseDoubleClick
        Try

            'frmMain.pnlNotification.Visible = True
            'frmMain.pnlNotification.BringToFront()
            'frmMain.WindowState = FormWindowState.Maximized
            'frmMain.Activate()

            'frmMain.btnNotification.BackColor = Color.Transparent
            'frmMain.btnNotification.Font = New Font(frmMain.btnNotification.Font.FontFamily.Name, frmMain.btnNotification.Font.Size, FontStyle.Regular)
            'frmMain.btnNotification.Text = String.Empty

            Dim a = New SBDal.NotificationDAL().MarkNotificationRead(LoginUserId)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub frmNotification_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class