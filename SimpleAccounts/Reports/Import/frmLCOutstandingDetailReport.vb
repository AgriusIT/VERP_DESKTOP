Imports SBModel

''TASK: TFS1275 LC Outstanding Detail Report
Public Class frmLCOutstandingDetailReport

    Private Sub FillCombo()
        Dim strSQL As String = ""
        Try
            strSQL = "Select Distinct Bank, Bank From tblLetterOfCredit Where Bank <> '' "
            FillDropDown(Me.cmbBank, strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmLCOutstandingDetailReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombo()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim strSQL As String = ""
        Try
            strSQL = Me.cmbBank.Text
            FillCombo()
            Me.cmbBank.Text = strSQL
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetReport()
        Dim strSQL As String = ""
        Dim dtOutstanding As DataTable
        Try
            Dim Id As Integer = Me.cmbBank.SelectedIndex
            If Id > 0 Then
                strSQL = "Exec sp_LCOutstandingDetail '" & Me.cmbBank.Text & "'"
            Else
                strSQL = "Exec sp_LCOutstandingDetail '" & "" & "'"
            End If
            dtOutstanding = GetDataTable(strSQL)
            dtOutstanding.AcceptChanges()
            GridEX1.DataSource = dtOutstanding
            GridEX1.RootTable.Columns("LCDate").FormatString = str_DisplayDateFormat
            GridEX1.RootTable.Columns("ExpiryDate").FormatString = str_DisplayDateFormat


            GridEX1.RootTable.Columns("TotalAmount").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("TotalAmount").TotalFormatString = "N" & DecimalPointInQty

            GridEX1.RootTable.Columns("Shipped").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Shipped").TotalFormatString = "N" & DecimalPointInQty

            GridEX1.RootTable.Columns("OutStandingBalance").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("OutStandingBalance").TotalFormatString = "N" & DecimalPointInQty

            GridEX1.RootTable.Columns("CreditLimit").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("CreditLimit").TotalFormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If Not Me.cmbBank.SelectedValue Is Nothing AndAlso Me.cmbBank.Text.Length > 0 Then
                GetReport()
                CtrlGrdBar1_Load(Nothing, Nothing)
            Else
                msg_Error("Bank is required.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        ''TASK TFS1384 replaced Report Print and Report Export with Report Print and Report Export on 07-09-2017
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " LC Outstanding"

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblLCOutstandingDetail_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub
End Class