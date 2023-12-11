Public Class frmLoadJobCards

    'Public JobCardCustomer As String = ""
    'Public JobCardNo As String = ""
    'Public JobCardDate As DateTime
    'Public Vehicle As String = ""
    'Public Lift As String = ""
    'Public JobCardID As Integer = 0

    Public IsSales As Boolean = False
    Public IsDeliveryChalan As Boolean = False

    Private Sub FillCombo()
        '29-Jun-2017: Task # 975: Waqar Raza: To load all jobcard which payment status is 0. 
        Try
            'Ali Faisal : TFS1606 : Apply condition of CompanyId from sales screen
            Dim strJobCard As String = " SELECT Card.JobCardID, Card.JobCardNo, Card.JobCardDate, tblVehicleInfo.RegistrationNo, Card.LiftID, tblDefCostCenter.Name AS LiftName, tblVehicleInfo.ModelID, tblDefModelList.Name As Model, ArticleColorDefTable.ArticleColorName As Color, tblVehicleInfo.EngineNo, tblVehicleInfo.ChessisNo, tblVehicleInfo.DOP, " _
           & " tblCompanyContacts.ContactName, TblCompanyContacts.Mobile, Card.Remarks, TblCompanyContacts.Address, Card.PaymentStatus " _
           & " FROM tblJobCard AS Card INNER JOIN " _
           & " tblVehicleInfo ON Card.VehicleID = tblVehicleInfo.VahicleID INNER JOIN " _
           & " tblCompanyContacts ON tblVehicleInfo.CompanyContactID = TblCompanyContacts.PK_Id LEFT OUTER JOIN " _
           & " tblDefModelList ON tblVehicleInfo.ModelID = tblDefModelList.ModelId LEFT OUTER JOIN " _
           & " ArticleColorDefTable ON tblVehicleInfo.ColorID = ArticleColorDefTable.ArticleColorId LEFT OUTER JOIN " _
           & " tblDefCostCenter ON Card.LiftID = tblDefCostCenter.CostCenterID Where JobCardDate between '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' And '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' And (ISNULL(Card.PaymentStatus, 0) = 0) AND Card.CompanyId = " & frmSales.SalesCompanyId & ""
            'Ali Faisal : TFS1606 : End
            FillUltraDropDown(Me.cmbJobCard, strJobCard)
            Me.cmbJobCard.DisplayLayout.Bands(0).Columns("JobCardID").Hidden = True
            Me.cmbJobCard.DisplayLayout.Bands(0).Columns("LiftID").Hidden = True
            Me.cmbJobCard.DisplayLayout.Bands(0).Columns("ModelID").Hidden = True
            '29-Jun-2017: Task # 975: Waqar Raza: To hide these columns from dropdown list.
            Me.cmbJobCard.DisplayLayout.Bands(0).Columns("Remarks").Hidden = True
            Me.cmbJobCard.DisplayLayout.Bands(0).Columns("Address").Hidden = True
            Me.cmbJobCard.DisplayLayout.Bands(0).Columns("EngineNo").Hidden = True
            Me.cmbJobCard.DisplayLayout.Bands(0).Columns("ChessisNo").Hidden = True
            '29-Jun-2017: Task # TFS1051: Waqar Raza: Added to check which radio button is enable and on which system will apply filter to search.
            'Start TFS1051:
            rboRegNo_CheckedChanged(Nothing, Nothing)
            'End TFS1051:
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmLoadJobCards_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCombo()

    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            If IsDeliveryChalan = True Then
                LoadData()
            ElseIf IsSales = True Then
                LoadDataForSales()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub LoadData()
        Try
            If Me.cmbJobCard.Value > 0 Then
                frmDeliveryChalan.txtJobCardCustomer.Text = Me.cmbJobCard.ActiveRow.Cells("ContactName").Value.ToString
                frmDeliveryChalan.txtJobCardNo.Text = Me.cmbJobCard.ActiveRow.Cells("JobCardNo").Value.ToString
                frmDeliveryChalan.txtRegistrationNo.Text = Me.cmbJobCard.ActiveRow.Cells("RegistrationNo").Value.ToString
                frmDeliveryChalan.dtpJobCardDate.Value = Me.cmbJobCard.ActiveRow.Cells("JobCardDate").Value
                frmDeliveryChalan.txtVehicle.Text = Me.cmbJobCard.ActiveRow.Cells("Model").Value.ToString
                frmDeliveryChalan.txtLeft.Text = Me.cmbJobCard.ActiveRow.Cells("LiftName").Value.ToString
                frmDeliveryChalan.txtJobCardId.Text = Val(Me.cmbJobCard.ActiveRow.Cells("JobCardID").Value.ToString)
                frmDeliveryChalan.ModelId = Val(Me.cmbJobCard.ActiveRow.Cells("ModelID").Value.ToString)
                frmDeliveryChalan.FillCombo("Item")
                frmDeliveryChalan.gbJobCard.Visible = True
                DialogResult = Windows.Forms.DialogResult.Yes
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub LoadDataForSales()
        Try
            If Me.cmbJobCard.Value > 0 Then
                frmSales.txtJobCardCustomer.Text = Me.cmbJobCard.ActiveRow.Cells("ContactName").Value.ToString
                frmSales.txtJobCardNo.Text = Me.cmbJobCard.ActiveRow.Cells("JobCardNo").Value.ToString
                frmSales.txtRegistrationNo.Text = Me.cmbJobCard.ActiveRow.Cells("RegistrationNo").Value.ToString
                frmSales.dtpJobCardDate.Value = Me.cmbJobCard.ActiveRow.Cells("JobCardDate").Value
                frmSales.txtVehicle.Text = Me.cmbJobCard.ActiveRow.Cells("Model").Value.ToString
                frmSales.cmbLift.Value = Val(Me.cmbJobCard.ActiveRow.Cells("LiftID").Value.ToString)
                frmSales.txtJobCardId.Text = Val(Me.cmbJobCard.ActiveRow.Cells("JobCardID").Value.ToString)
                frmSales.ModelId = Val(Me.cmbJobCard.ActiveRow.Cells("ModelID").Value.ToString)
                frmSales.FillCombo("Item")
                '12-July-2017: Task # TFS1042: Call this Funtion from sales screen to pass jobcardid to parameter.
                'Start TFS1042:
                If rdoPreInvoice.Checked = True Then
                    frmSales.jobcarddetail(Val(Me.cmbJobCard.ActiveRow.Cells("JobCardID").Value.ToString))
                End If

                'End TFS1042:
                frmSales.gbJobCard.Visible = True
                DialogResult = Windows.Forms.DialogResult.Yes
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim Id As Integer = 0
            Id = Me.cmbJobCard.Value
            FillCombo()
            Me.cmbJobCard.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkRefresh_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRefresh.LinkClicked
        Try
            Dim Id As Integer = 0
            Id = Me.cmbJobCard.Value
            FillCombo()
            Me.cmbJobCard.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '12-July-2017: Task # TFS1042: Added to load jobcard on pressing Enter key.
    'Start TFS1042:
    Private Sub cmbJobCard_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbJobCard.KeyDown
        Try
            If e.KeyCode = Keys.Enter AndAlso cmbJobCard.Value > 0 Then
                btnLoad_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End TFS1042:
    '12-July-2017: Task # TFS1042: Added to make display member on the basis of radio button checked.
    'Start TFS1042:
   Private Sub rboRegNo_CheckedChanged(sender As Object, e As EventArgs) Handles rboRegNo.CheckedChanged, rboCustomer.CheckedChanged
        Try
            If rboRegNo.Checked = True Then
                Me.cmbJobCard.DisplayMember = "RegistrationNo"
            Else
                Me.cmbJobCard.DisplayMember = "ContactName"
            End If
            Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End TFS1042:
End Class