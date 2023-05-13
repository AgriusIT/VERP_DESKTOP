Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Public Class frmGrdCustomerBasedTarget
    Private _Year As Integer = 0
    Dim Dt As New DataTable
    Dim CustomerTargetDt As List(Of CustomerBasedTarget)
    Dim CustomerTarget As CustomerBasedTarget
    Enum enmGridTarget
        Id
        CustomerId
        Customer
        CustomerCode
        Type
        Year
        January
        February
        March
        April
        May
        June
        July
        August
        September
        October
        November
        December
        Total
    End Enum
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub frmGrdCustomerBasedTarget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmGrdCustomerBasedTarget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim dtYear As DataTable = GetDataTable("Select DISTINCT ISNULL(Target_Year,0) as Target_Year From tblDefCustomerTarget WHERE TargetDetailId IN(Select Max(TargetDetailId) From tblDefCustomerTarget)")
            If dtYear.Rows.Count > 0 Then
                _Year = dtYear.Rows(0).Item(0)
            Else
                _Year = "2012"
            End If

            Me.TextBox1.Text = _Year


        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading form: " & ex.Message)
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try

            _Year = Me.TextBox1.Text
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub GetData()
        Try
            Dt = GetDataTable("SP_CustomerBasedTarget " & _Year & "")
            Dt.Columns.Add(enmGridTarget.Total.ToString, GetType(System.Double))
            Dt.Columns(enmGridTarget.Total).Expression = "January+February+March+April+May+June+July+August+September+October+November+December"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            GetData()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub FillGrid()
        Dim lbl As New Label
        Try

            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            If Dt IsNot Nothing Then
                Dt.AcceptChanges()
                Me.GridEX1.DataSource = Dt
                Me.GridEX1.AutoSizeColumns()
                ApplyGridSettings()
                CtrlGrdBar1_Load(Nothing, Nothing)

            End If
        Catch ex As Exception
            Throw ex
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub frmGrdCustomerBasedTarget_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If col.Index = enmGridTarget.Id Or col.Index = enmGridTarget.CustomerId Or col.Index = enmGridTarget.Customer Or col.Index = enmGridTarget.CustomerCode Or col.Index = enmGridTarget.Type Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FillModel()
        Try
            Dim dtGrid As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            CustomerTargetDt = New List(Of CustomerBasedTarget)

            For Each row As DataRow In dtGrid.GetChanges.Rows
                CustomerTarget = New CustomerBasedTarget
                CustomerTarget.TargetDetailId = row.Item(enmGridTarget.Id)
                CustomerTarget.CustomerId = row.Item(enmGridTarget.CustomerId)
                CustomerTarget.Target_Year = Val(Me.TextBox1.Text)
                CustomerTarget.January = row(enmGridTarget.January)
                CustomerTarget.February = row(enmGridTarget.February)
                CustomerTarget.March = row(enmGridTarget.March)
                CustomerTarget.April = row(enmGridTarget.April)
                CustomerTarget.May = row(enmGridTarget.May)
                CustomerTarget.June = row(enmGridTarget.June)
                CustomerTarget.July = row(enmGridTarget.July)
                CustomerTarget.August = row(enmGridTarget.August)
                CustomerTarget.September = row(enmGridTarget.September)
                CustomerTarget.October = row(enmGridTarget.October)
                CustomerTarget.November = row(enmGridTarget.November)
                CustomerTarget.December = row(enmGridTarget.December)
                CustomerTargetDt.Add(CustomerTarget)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.TextBox1.Text = String.Empty Then
                ShowErrorMessage("Please enter year")
                Me.TextBox1.Focus()
                Exit Sub
            End If
            'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            FillModel()
            If New CustomerBasedTargetDAL().Add(CustomerTargetDt) = True Then
                'msg_Information(str_informSave)
                If BackgroundWorker1.IsBusy Then Exit Sub
                BackgroundWorker1.RunWorkerAsync()
                Do While BackgroundWorker1.IsBusy
                    Application.DoEvents()
                Loop
                FillGrid()
            End If
        Catch ex As Exception
            ShowErrorMessage("Error occurred while save record: " & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub


    Private Sub GridEX1_LoadingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEX1.LoadingRow
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells(enmGridTarget.Total).Value = 0 Then
                rowStyle.BackColor = Color.LightYellow
                e.Row.RowStyle = rowStyle
            Else
                rowStyle.BackColor = Color.White
                e.Row.RowStyle = rowStyle
            End If
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading rows: " & ex.Message)
        End Try
    End Sub
End Class