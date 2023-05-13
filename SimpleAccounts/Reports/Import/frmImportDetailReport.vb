Imports SBModel

''TASK: TFS1275 Import Detail Report


Public Class frmImportDetailReport

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim dt As DataTable
        Try

            'Dim animal As Animal = GetAnimal()
            dt = GetDataTable("Exec sp_ImportDetail '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "'")
            dt.AcceptChanges()
            Me.grdImportDetail.DataSource = dt
            Me.grdImportDetail.RetrieveStructure()
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub ApplyGridSettings()
        Try
            ''Set visible false
            Me.grdImportDetail.RootTable.Columns("LCdoc_Id").Visible = False
            Me.grdImportDetail.RootTable.Columns("Reference_No").Visible = False
            Me.grdImportDetail.RootTable.Columns("Performa_No").Visible = False
            'Me.grdImportDetail.RootTable.Columns("LCdoc_Date").Visible = False
            'Me.grdImportDetail.RootTable.Columns("LCdoc_No").Visible = False
            Me.grdImportDetail.RootTable.Columns("Advising_Bank").Visible = False
            'Me.grdImportDetail.RootTable.Columns("LCOrigin").Visible = False
            Me.grdImportDetail.RootTable.Columns("BDR_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("BL_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("BL_No").Visible = False
            Me.grdImportDetail.RootTable.Columns("Cheque_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("Cheque_No").Visible = False
            Me.grdImportDetail.RootTable.Columns("Clearing_Agent").Visible = False
            Me.grdImportDetail.RootTable.Columns("DD_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("DTB_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("ETA_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("ETD_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("Expiry_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("Freight").Visible = False
            Me.grdImportDetail.RootTable.Columns("InsurranceValue").Visible = False
            Me.grdImportDetail.RootTable.Columns("LCAmount").Visible = False
            Me.grdImportDetail.RootTable.Columns("LCDescription").Visible = False
            Me.grdImportDetail.RootTable.Columns("LCdoc_Type").Visible = False
            Me.grdImportDetail.RootTable.Columns("NN_Date").Visible = False
            Me.grdImportDetail.RootTable.Columns("OpenedBy").Visible = False
            Me.grdImportDetail.RootTable.Columns("PortOfDischarge").Visible = False
            Me.grdImportDetail.RootTable.Columns("PortOfLoading").Visible = False
            Me.grdImportDetail.RootTable.Columns("LCRemarks").Visible = False
            Me.grdImportDetail.RootTable.Columns("LCSpecialInstructions").Visible = False
            Me.grdImportDetail.RootTable.Columns("Vessel").Visible = False
            ''Set caption
            Me.grdImportDetail.RootTable.Columns("LCdoc_Date").Caption = "Date"
            Me.grdImportDetail.RootTable.Columns("LCdoc_No").Caption = "L.C No"
            Me.grdImportDetail.RootTable.Columns("LCOrigin").Caption = "Origin"
            Me.grdImportDetail.RootTable.Columns("LCQty").Caption = "L/C Qty"
            Me.grdImportDetail.RootTable.Columns("PendingQty").Caption = "Pending Qty"
            Me.grdImportDetail.RootTable.Columns("ShippedQty").Caption = "Shipped Qty"
            Me.grdImportDetail.RootTable.Columns("ActualRate").Caption = "Actual Rate"
            Me.grdImportDetail.RootTable.Columns("LCRate").Caption = "LC Rate"
            Me.grdImportDetail.RootTable.Columns("Vendor").Caption = "Exporter"
            ''Set format
            Me.grdImportDetail.RootTable.Columns("LCdoc_Date").FormatString = str_DisplayDateFormat


            Me.grdImportDetail.RootTable.Columns("LCQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdImportDetail.RootTable.Columns("PendingQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdImportDetail.RootTable.Columns("ShippedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdImportDetail.RootTable.Columns("LCQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("PendingQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("ShippedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("LCQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("PendingQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("ShippedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdImportDetail.RootTable.Columns("ActualRate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdImportDetail.RootTable.Columns("LCRate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdImportDetail.RootTable.Columns("LCAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdImportDetail.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdImportDetail.RootTable.Columns("ActualRate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("LCRate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("LCAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("ActualRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("LCRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("LCAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdImportDetail.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            'Me.grdImportDetail.RootTable.Columns("ActualRate").Caption = "Actual Rate"
            'Me.grdImportDetail.RootTable.Columns("LCRate").Caption = "LC Rate"
            'Me.grdImportDetail.RootTable.Columns("LCRate").FormatString = 
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.dtpFrom.Value = Now
            Me.dtpTo.Value = Now
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

    Private Sub frmImportDetailReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ' ''' <summary>
    ' ''' This method is created for test purposes
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ''Private Function GetAnimal() As Animal
    ''    Dim animal As New Animal
    ''    Try
    ''        animal.Id = 1
    ''        animal.Name = "Goat"
    ''        animal.Type = "Halal"
    ''        animal.Color = "Red"
    ''        animal.Age = "2 Years"
    ''        Return animal
    ''    Catch ex As Exception
    ''        Throw ex
    ''    End Try
    'End Function

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdImportDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdImportDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdImportDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Import Detail " & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
