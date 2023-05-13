''Task# I08062015: Ahmad Sharif , 08-June-2015 


Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows
Imports CrystalDecisions.Windows.Forms
Public Class frmCrReportViewer
    Public objDataSet As New DataSet
    Public crpt As New ReportDocument
    Public ReportName As String = String.Empty
    Public crParameter As String = String.Empty
    Public crParameterValue As String = String.Empty
    Public ReportPath As String = String.Empty

    '''Task# I08062015:
    Public btnEmail As ToolStripButton
    Public btnCusttomized As ToolStripButton
    Public prgBarReport As New ToolStripProgressBar
    Public _ToEmailId As String = String.Empty
    Public str_Path As String = String.Empty
    Public PrintCountReport As String
    Enum enmReport
        SalesmanVoucher
    End Enum

    Private Sub frmCrReportViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = Cursors.WaitCursor

        Try

            For Each Ctrl As Control In CrystalReportViewer1.Controls
                If TypeOf Ctrl Is ToolStrip Then
                    btnEmail = New ToolStripButton
                    btnEmail.Text = "Send Email"
                    btnEmail.Name = "btnEmail"
                    btnEmail.Image = My.Resources.Resources.Email_Envelope
                    btnEmail.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    CType(Ctrl, ToolStrip).Items.Add(btnEmail)
                    AddHandler btnEmail.Click, AddressOf OpenEmailForm

                    btnCusttomized = New ToolStripButton
                    btnCusttomized.Text = "Customize Report"
                    btnCusttomized.Name = "btnCustomizeReport"
                    btnCusttomized.Image = My.Resources.custom_reports
                    btnEmail.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    CType(Ctrl, ToolStrip).Items.Add(btnCusttomized)
                    AddHandler btnCusttomized.Click, AddressOf CustomizeReport_Click

                    prgBarReport.Name = "prgBarReport"
                    prgBarReport.Size = New System.Drawing.Size(150, 23)
                    prgBarReport.Alignment = ToolStripItemAlignment.Right
                    prgBarReport.Style = ProgressBarStyle.Marquee
                    CType(Ctrl, ToolStrip).Items.Add(prgBarReport)

                    Try
                        For Each obj As Object In CType(Ctrl, ToolStrip).Items
                            If TypeOf obj Is ToolStripDropDownButton Then
                            ElseIf TypeOf obj Is ToolStripButton Then
                                Dim PrintButton As ToolStripButton = obj
                                If PrintButton.AccessibleName = "Print Report" Then
                                    AddHandler PrintButton.Click, AddressOf PrintCount_Click
                                    Exit For
                                End If
                            End If
                        Next
                    Catch ex As Exception

                    End Try
                End If
            Next

            Dim strReportPath As String = String.Empty
            strReportPath = ReportPath
            If strReportPath.Length <= 0 Then
                crpt.Load(Application.StartupPath & "\Reports\" & ReportName & ".rpt")
            Else
                crpt.Load(Application.StartupPath & ReportPath & ReportName & ".rpt")
            End If
            ReportPath = String.Empty
            Dim MyComp As New SBModel.CompanyInfo
            Dim flgCompanyRights As Boolean = False
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            crpt.SetDataSource(objDataSet)
            Me.CrystalReportViewer1.ReportSource = crpt
            ShowHeaderCompany()
            If flgCompanyRights = True Then
                If MyCompanyRightsList IsNot Nothing Then
                    If MyCompanyRightsList.Count > 0 Then
                        MyComp = CompanyList.Find(AddressOf GetCompanyName)
                        CompanyNameHeader = MyComp.CompanyName
                        CompanyAddressHeader = MyComp.Address
                    End If
                End If
            Else
                CompanyNameHeader = CompanyTitle
                CompanyAddressHeader = CompanyAddHeader
            End If
            Try

                If CompanyNameHeader.Length > 1 Then crpt.SetParameterValue("@CompanyName", CompanyNameHeader) Else crpt.SetParameterValue("@CompanyName", " ")
                If CompanyAddressHeader.Length > 1 Then crpt.SetParameterValue("@CompanyAddress", CompanyAddressHeader) Else crpt.SetParameterValue("@CompanyAddress", " ")
                crpt.SetParameterValue("@ShowHeader", False)
            Catch ex As Exception

            End Try
            If str_ReportParam.Length > 1 Then
                Dim str() As String = str_ReportParam.Split("&")
                For Each str1 As String In str
                    Dim strParam() As String = str1.Split("|")
                    crpt.SetParameterValue(strParam(0), strParam(1))
                Next
            End If

            Me.Visible = True
            Me.BringToFront()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            str_ReportParam = String.Empty
            prgBarReport.Visible = False       ''Task# I08062015: When report load progress set to visible false
        End Try
    End Sub

    Public Function GetCompanyName(ByVal Company As SBModel.CompanyInfo) As Boolean
        Try
            If Company.CompanyID = MyCompanyId Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Private Sub OpenEmailForm(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Try
                IO.File.Delete(_FileExportPath)
            Catch ex As Exception

            End Try
            Dim CRExportOpts As ExportOptions
            Dim CRDiskFileOpts As New DiskFileDestinationOptions
            Dim CRFormatTypeOpts As New PdfRtfWordFormatOptions
            If Not IO.Directory.Exists(_FileExportPath) Then
                IO.Directory.CreateDirectory(_FileExportPath)
            End If
            str_Path = _FileExportPath & "\" & reportName & "" & Date.Today.ToString("yyyyMMdd") & "" & ".Pdf"
            CRDiskFileOpts.DiskFileName = str_Path
            CRExportOpts = crpt.ExportOptions
            With CRExportOpts
                .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                .ExportDestinationOptions = CRDiskFileOpts
                .ExportFormatOptions = CRFormatTypeOpts
            End With
            Try
                crpt.Export()


            Catch ex As Exception

            End Try
            Dim frm As New frmOutgoingEmail
            frm.txtDataFile.Text = str_Path
            frm.txtTo.Text = _ToEmailId
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CustomizeReport_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FileName1 As String = Me.reportName & ".rpt"
        Try
            If (Not System.IO.Directory.Exists(Application.StartupPath & "\Reports\CutomizedReports\")) Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\Reports\CutomizedReports\")
            End If

            If Not IO.File.Exists(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1) = True Then
                IO.File.Copy(Application.StartupPath & "\Reports\" & FileName1, Application.StartupPath & "\Reports\CutomizedReports\" & FileName1)
            End If

            Dim attributes As IO.FileAttributes
            attributes = IO.File.GetAttributes(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1)

            If (attributes And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then
                attributes = RemoveAttribute(attributes, IO.FileAttributes.ReadOnly)
                IO.File.SetAttributes(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1, attributes)
            End If

            Process.Start(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Sub PrintCount_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If PrintCountReport Is Nothing Then Exit Sub
            If PrintCountReport = enmReport.SalesmanVoucher Then
                rptVoucher.PrintCount()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Function RemoveAttribute(ByVal attributes As IO.FileAttributes, ByVal attributesToRemove As IO.FileAttributes) As IO.FileAttributes
        Return attributes And (Not attributesToRemove)
    End Function

    ''End Task# I08062015:
End Class