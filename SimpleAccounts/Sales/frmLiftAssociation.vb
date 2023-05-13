Imports SBDal

Public Class frmLiftAssociation
    Dim ModelListDAL As New ModelListDAL
    Private Sub frmLiftAssociation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Lift Association"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdRcords.LoadLayoutFile(fs1)
                fs1.Dispose()
                fs1.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & Chr(10) & "Lift Association" & Chr(10) & "Date From: " & Me.dtpFrom.Value.ToString("dd-MM-yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd-Mm-yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAllRecords()
        Try
            Dim str As String = String.Empty
            'str = "SELECT JobCard_ID, JobCard_No, JobCard_Date, Customer_Name, Registration_No, LiftId, Phone_No, Phone_No2, DOB, CNIC, Model, Color, Chessis_No, Engine_No, DOP, Remarks, Address FROM tblJobCard Where JobCard_Date between " & Me.dtpFrom.Value.ToString("yyyy-MM-dd") & " And " & Me.dtpTo.Value.ToString("yyyy-MM-dd") & ""
            str = " SELECT Card.JobCardID, Card.JobCardNo, Card.JobCardDate, tblVehicleInfo.RegistrationNo, IsNull(Card.LiftID, 0) As LiftID, tblDefCostCenter.Name AS LiftName, tblDefModelList.Name As Model, ArticleColorDefTable.ArticleColorName As Color, tblVehicleInfo.EngineNo, tblVehicleInfo.ChessisNo, tblVehicleInfo.DOP, " _
            & " tblCompanyContacts.ContactName, TblCompanyContacts.Mobile, Card.Remarks, TblCompanyContacts.Address " _
            & " FROM tblJobCard AS Card INNER JOIN " _
            & " tblVehicleInfo ON Card.VehicleID = tblVehicleInfo.VahicleID INNER JOIN " _
            & " tblCompanyContacts ON tblVehicleInfo.CompanyContactID = TblCompanyContacts.PK_Id LEFT JOIN " _
            & " tblDefModelList ON tblVehicleInfo.ModelID = tblDefModelList.ModelId LEFT JOIN " _
            & " ArticleColorDefTable ON tblVehicleInfo.ColorID = ArticleColorDefTable.ArticleColorId LEFT OUTER JOIN " _
            & " tblDefCostCenter ON Card.LiftID = tblDefCostCenter.CostCenterID Where Convert(Varchar, JobCardDate, 102) between Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) And Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-MM-dd") & "', 102) "
            Me.grdRcords.DataSource = GetDataTable(str)
            Me.grdRcords.RetrieveStructure()
            ApplyGridSettings()
            FillCombo()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            Dim str As String = ""
            str = "Select CostCenterID, Name from tblDefCostCenter Where Active = 1 Order by SortOrder ASC"
            Dim dt As DataTable = GetDataTable(str)
            Me.grdRcords.RootTable.Columns("LiftID").ValueList.PopulateValueList(dt.DefaultView, "CostCenterID", "Name")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            GetAllRecords()
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            Me.grdRcords.RootTable.Columns("JobCardID").Visible = False
            Me.grdRcords.RootTable.Columns("LiftName").Visible = False
            Me.grdRcords.RootTable.Columns("LiftID").Caption = "Lift"
            Me.grdRcords.RootTable.Columns("LiftID").HasValueList = True
            'Me.grdRcords.RootTable.Columns("LiftID").LimitToList = True
            Me.grdRcords.RootTable.Columns("LiftID").EditType = Janus.Windows.GridEX.EditType.Combo
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdRcords_KeyDown(sender As Object, e As KeyEventArgs) Handles grdRcords.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.grdRcords.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    ModelListDAL.SetCostCentreOnListAssociation(Val(Me.grdRcords.GetRow.Cells("LiftID").Value.ToString), Val(Me.grdRcords.GetRow.Cells("JobCardID").Value.ToString))
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class