''12-01-2015 TASKTFS81 Muhammad Ameen: Build security log on print outs and print name of the user on every generated report
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
'Imports CrystalDecisions.Shared.SharedUtils
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
'Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.ReportAppServer
Imports SBDal
Imports Microsoft.Office.Interop

Public Class rptReportViewerNew
    Dim str_RptParam As String
    ' Dim SBg As New SIRIUSUtility.Globel
    'Dim crpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    Dim crpt As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
    Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo
    Dim Param As CrystalDecisions.Shared.ParameterFields
    Dim CompanyNamed As CrystalDecisions.Shared.ParameterField
    Dim crSection As CrystalDecisions.CrystalReports.Engine.Section ''TFS3764
    Dim crObject As CrystalDecisions.CrystalReports.Engine.ReportObject  ''TFS3764
    Dim ObjFeild As CrystalDecisions.CrystalReports.Engine.FieldObject

    Public reportName As String = String.Empty
    Dim ServerName As String = ConServerName
    Dim UserId As String = ConUserId
    Dim Pass As String = ConPassword
    Dim DataBase As String = Con.Database
    Public UserName As Boolean = False
    Public ShowHeader As Boolean = False
    Public CostCenterId As String = String.Empty
    Public CostCenter As String = String.Empty
    Public ShowAddress As Boolean = False
    Public Company_Name As String = String.Empty
    Public User_Name As String = String.Empty
    Public Company_Address As String = String.Empty
    Public ParamFrom As String = String.Empty
    Public ParamTo As String = String.Empty
    Public ParamOpeningBalance As String = String.Empty
    Public SendToPrinter As Boolean
    Public Datatable As DataTable
    Public Voucher_No As String = String.Empty
    Public btnEmail As ToolStripButton
    Public btnCusttomized As ToolStripButton
    Public btnPost As ToolStripButton
    Public prgBarReport As New ToolStripProgressBar
    Dim IsOpenForm As Boolean = False
    Public str_Path As String = String.Empty
    Dim flgCompanyRights As Boolean = False
    Public PrintCountReport As String
    Public _ToEmailId As String = String.Empty
    Public ReportFormName As String = String.Empty
    Public ShowReportTitle As String = String.Empty
    Public CustomizedReportFolderName As String = String.Empty
    ''Start TFS3764
    Public PrintCopies As Integer = 1
    Public PrinterName As String = String.Empty
    Public PrintFont As String = "Verdana"
    Public PrintFontSize As Integer = 8
    ''End TFS3764
    Enum enmReport
        SalesmanVoucher
    End Enum
    Private Sub rptReportViewerNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If crpt IsNot Nothing Then
                crpt.Dispose()
                crpt.Close()
            End If

            crpt = New CrystalDecisions.CrystalReports.Engine.ReportDocument

            Me.Text = Me.Text & ":" & ShowReportTitle

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
                    btnCusttomized.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    CType(Ctrl, ToolStrip).Items.Add(btnCusttomized)
                    AddHandler btnCusttomized.Click, AddressOf CustomizeReport_Click

                    btnPost = New ToolStripButton
                    btnPost.Text = "Post"
                    btnPost.Name = "btnPost"
                    btnPost.Image = My.Resources.addIcon
                    btnPost.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
                    CType(Ctrl, ToolStrip).Items.Add(btnPost)
                    AddHandler btnPost.Click, AddressOf PostVoucher


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
            IsOpenForm = True
            If IsCrystalReportPrint = True Then
                CrystalReportViewer1.ShowPrintButton = True
            Else
                CrystalReportViewer1.ShowPrintButton = False
            End If
            If IsCrystalReportExport = True Then
                CrystalReportViewer1.ShowExportButton = True
            Else
                CrystalReportViewer1.ShowExportButton = False
            End If
            ''Start TFS3764
            CrystalReportViewer1.Font = New Font(PrintFont, PrintFontSize)
            ''End TFS3764
            IsCrystalReportPrint = True
            IsCrystalReportExport = True
            GetVersionInfo()
            If Val(SoftwareVersion) <= 2 Then
                btnEmail.Enabled = False
            End If



            Me.prgBarReport.Visible = True
            prgBarRpt = CType(prgBarReport.ProgressBar, ProgressBar)

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            Me.str_RptParam = str_ReportParam
            str_ReportParam = ""

            funShowReport(str_RptParam)

            If Me.SendToPrinter = True Then
                Dim newinvoice As Boolean = False
                Dim FastPrinting As Boolean = False
                Try
                    newinvoice = GetConfigValue("NewInvoice")
                Catch ex As Exception
                    newinvoice = False
                End Try
                ''Start TFS3764
                Dim PrintLayout As New CrystalDecisions.Shared.PrintLayoutSettings()
                Dim pPrinterSettings As New System.Drawing.Printing.PrinterSettings()

                pPrinterSettings.PrinterName = PrinterName
                pPrinterSettings.Copies = PrintCopies
                Dim pPageSettings As New System.Drawing.Printing.PageSettings(pPrinterSettings)
                PrintLayout.Scaling = PrintLayoutSettings.PrintScaling.DoNotScale

                crpt.PrintOptions.PrinterDuplex = PrinterDuplex.Simplex
                crpt.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True

                'If pPrinterSettings.IsValid Then
                '    If Me.reportName.Contains("rptArticleBarCode") Then crpt.PrintToPrinter(pPrinterSettings, pPageSettings, False, PrintLayout) : Me.Close() : Exit Sub
                'Else
                '    MessageBox.Show("Printer is invalid.")
                'End If
                'End TFS3764
                If newinvoice = False Then
                    If Me.reportName.Contains("SalesInvoice") Or Me.reportName.Contains("GatePass") Then crpt.PrintToPrinter(1, False, 0, 0) : Me.Close() : Exit Sub
                End If
                If Me.reportName.Contains("rptVoucher") Then crpt.PrintToPrinter(1, False, 0, 0) : Me.Close() : Exit Sub
                crpt.PrintToPrinter(1, False, 0, 0)
                Me.Close()
            Else
                'CrystalReportViewer1.RefreshReport() ': crpt.PrintToPrinter(1, False, 0, 0) : Me.Close()
                Me.Visible = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub funShowReport(ByRef rptPath As String, Optional ByVal strRecordSelection As String = "")
        Try


            Dim strReportPath As String = str_RptParam

            Dim crDatabase As Database
            Dim crTables As Tables
            Dim crTable As Table
            Dim crTableLogOnInfo As TableLogOnInfo
            Dim crConnectionInfo As ConnectionInfo
            'Create an instance of the strongly-typed report object

            'Setup the connection information structure to be used
            'to log onto the datasource for the report.
            crConnectionInfo = New ConnectionInfo

            'Sub report object of crystal report.

            'Dim mySubReportObject As CrystalDecisions.CrystalReports.Engine.SubreportObject

            'Sub report document of crystal report.
            Try


                Dim mySubRepDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                'Me.crpt.Load(str_ApplicationStartUpPath & "\Reports\" & Me.reportName & ".rpt", Me.ServerName)
                'Task No 2569 Checking The Report File In Customized Folder FOR Customized  Reports
                If File.Exists(ReportPath & "\CutomizedReports\" & CustomizedReportFolderName & "\" & Me.reportName & ".rpt") Then
                    Dim Dun As String = ReportPath & "\CutomizedReports\" & CustomizedReportFolderName & "\" & Me.reportName & ".rpt"
                    crpt.Load(ReportPath & "\CutomizedReports\" & CustomizedReportFolderName & "\" & Me.reportName & ".rpt")
                ElseIf File.Exists(ReportPath & "\CutomizedReports\" & Me.reportName & ".rpt") Then
                    Dim Dun As String = ReportPath & "\CutomizedReports\" & Me.reportName & ".rpt"
                    crpt.Load(ReportPath & "\CutomizedReports\" & Me.reportName & ".rpt")

                ElseIf File.Exists(ReportPath & "\" & Me.reportName & ".rdlc") Then
                    crpt.Load(ReportPath & "\" & Me.reportName & ".rdlc", Me.ServerName)
                Else
                    If File.Exists(ReportPath & "\" & Me.reportName & ".rpt") Then
                        crpt.Load(ReportPath & "\" & Me.reportName & ".rpt") 'Task:
                    Else
                        msg_Error("Can't find report, please make sure file [" & ReportPath & "\" & Me.reportName & ".rpt] exist.")
                        Exit Sub
                    End If
                End If
                'End Task 

            Catch ex As Exception
                Throw ex
            End Try

            If Me.UserId <> "" Then
                Me.crpt.DataSourceConnections.Item(0).SetConnection(Me.ServerName, Me.DataBase, Me.UserId, Me.Pass)
            Else
                Me.crpt.DataSourceConnections.Item(0).SetConnection(Me.ServerName, Me.DataBase, True)
            End If

            If Me.UserId <> "" Then
                Me.crpt.DataSourceConnections.Item(0).SetLogon(Me.UserId, Me.Pass)
            End If

            With crConnectionInfo
                .ServerName = ServerName
                .DatabaseName = DataBase
                If UserId = "" Then
                    .IntegratedSecurity = True
                Else
                    .UserID = UserId
                    .Password = Pass
                    .IntegratedSecurity = False
                End If
            End With
            'Get the table information from the report
            crDatabase = Me.crpt.Database
            crTables = crDatabase.Tables
            'Loop through all tables in the report and apply the connection
            'information for each table.
            For Each crTable In crTables
                crTableLogOnInfo = crTable.LogOnInfo
                crTableLogOnInfo.ConnectionInfo = crConnectionInfo
                crTable.ApplyLogOnInfo(crTableLogOnInfo)
            Next



            If Not Me.Datatable Is Nothing Then
                crpt.SetDataSource(Me.Datatable)
            End If


            Try

                'If reportName.Contains("SalesInvoiceNew") Then
                '    Dim objText As TextObject
                '    If TypeOf _
                '       (crpt.ReportDefinition.ReportObjects. _
                '       Item("txtSoftbeats")) Is TextObject Then
                '        objText = _
                '           crpt.ReportDefinition.ReportObjects. _
                '           Item("txtSoftbeats")
                '        objText.Text = "Software Developed By SIRIUS (Pvt) Ltd. (+92 (42) 3742 8374,3742 8416, 3742 8431)"
                '        objText.ObjectFormat.EnableSuppress = False
                '    End If
                'End If
                ''Start TFS3764
                If Me.reportName.Contains("rptArticleBarCode") Then
                    Dim objText As TextObject
                    For Each crSection In crpt.ReportDefinition.Sections
                        For Each crObject In crSection.ReportObjects
                            If (crObject.Kind = ReportObjectKind.TextObject) Then
                                objText = crObject
                                If objText IsNot Nothing Then
                                    objText.ApplyFont(New Font(PrintFont, PrintFontSize))
                                End If
                            ElseIf (crObject.Kind = ReportObjectKind.FieldObject) Then
                                ObjFeild = crObject
                                If ObjFeild IsNot Nothing Then
                                    ObjFeild.ApplyFont(New Font(PrintFont, PrintFontSize))
                                End If
                            End If
                        Next
                    Next

                End If
                ''End TFS3764
            Catch ex As Exception
                Throw New Exception("Some parameter is not supplied.")
            End Try
            'Try

            '    Dim obj = crpt.ReportClientDocument
            '    Dim blobField As PictureObject
            '    If TypeOf _
            '       (crpt.ReportDefinition.ReportObjects. _
            '       Item("CompanyLogo")) Is PictureObject Then
            '        blobField = _
            '           crpt.ReportDefinition.ReportObjects. _
            '           Item("CompanyLogo")
            '        Dim boSection As CrystalDecisions.ReportAppServer.ReportDefModel.Section
            '        boSection = crpt.ReportClientDocument.ReportDefController.ReportDefinition.ReportHeaderArea
            '        blobField = crpt.ReportClientDocument.ReportDefController.ReportObjectController.ImportPicture("", boSection, 300, 300)
            '    End If
            'Catch ex As Exception
            'End Try

            CrystalReportViewer1.ReportSource = crpt
            'Dim objParamterContainer As ParameterFieldDefinitions
            'This object variable is used to hold the name of the parameter
            'Dim objParmName As ParameterFieldDefinition
            'Hold all values of the paramete
            Dim objParmValues As New ParameterValues
            'hold current value of the parametere
            Dim objParmCurrentValue As New ParameterDiscreteValue

            If str_RptParam.Length > 1 Then
                Dim str() As String = str_RptParam.Split("&")
                For Each str1 As String In str

                    Dim strParam() As String = str1.Split("|")

                    For Each parm As ParameterField In crpt.ParameterFields
                        If parm.Name = strParam(0) Then
                            crpt.SetParameterValue(strParam(0), IIf(strParam(1) = Nothing, DBNull.Value, strParam(1)))
                            Exit For
                        End If
                    Next

                Next
            End If
            Try
                If flgCompanyRights = True Then
                    If MyCompanyRightsList IsNot Nothing Then
                        If MyCompanyRightsList.Count > 0 Then
                            Dim MyComp As New SBModel.CompanyInfo
                            MyComp = CompanyList.Find(AddressOf GetCompanyName)
                            Company_Name = MyComp.CompanyName
                            Company_Address = IIf(MyComp.Address = "", "--", MyComp.Address)
                            CostCenterId = MyComp.CostCenterId
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
            Try
                ''TASKTFS81
                For Each parm As ParameterField In crpt.ParameterFields
                    If parm.Name = "@UserName" Then
                        If User_Name.Length > 0 Then crpt.SetParameterValue("@UserName", User_Name) Else crpt.SetParameterValue("@UserName", String.Empty)
                    ElseIf parm.Name = "@CompanyName" Then
                        If Company_Name.Length > 0 Then crpt.SetParameterValue("@CompanyName", Company_Name) Else crpt.SetParameterValue("@CompanyName", String.Empty)
                    ElseIf parm.Name = "@CompanyAddress" Then
                        If Company_Address.Length > 0 Then crpt.SetParameterValue("@CompanyAddress", Company_Address) Else crpt.SetParameterValue("@CompanyAddress", String.Empty)
                    ElseIf parm.Name = "@ShowHeader" Then
                        crpt.SetParameterValue("@ShowHeader", ShowHeader)
                    End If
                Next

                If ParamFrom.Length > 1 Then crpt.SetParameterValue("@FromDate", ParamFrom)
                If ParamTo.Length > 1 Then crpt.SetParameterValue("@ToDate", ParamTo)

                If ParamOpeningBalance.Length > 1 Then
                    crpt.SetParameterValue("OpeningBalance", ParamOpeningBalance)
                End If



                ''TASKTFS81
                'SaveActivityLog("Report", ReportFormName, EnumActions.Print, LoginUserId, "Report Print", ShowReportTitle, False)
                SaveActivityLog("Report", ReportFormName, EnumActions.Print, LoginUserId, EnumRecordType.Report, ShowReportTitle, False)
            Catch ex As Exception
                'if not exist @CompanyName Parameter in Report Formated
                ShowHeader = False
                UserName = False
                Company_Name = String.Empty
                Company_Address = String.Empty
            End Try

            If ParamFrom.Length > 1 Then crpt.SetParameterValue("@FromDate", ParamFrom)
            If ParamTo.Length > 1 Then crpt.SetParameterValue("@ToDate", ParamTo)

            If ParamOpeningBalance.Length > 1 Then
                crpt.SetParameterValue("OpeningBalance", ParamOpeningBalance)
            End If
            If IsQuotationReportExportToPDF Then
                ReportToPDF(Nothing, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ' For Each strParams In objHashTableParamter.Keys

    '    ''Export and print rights also set in hashtable so we check it in if condition.
    '    If (strParams <> "ReportPath") And (strParams <> "ExportRights") And (strParams <> "PrintRights") And (strParams <> "DataTable") Then
    '        ''CR # 1173 When parameter name "blnEnablePoint in value" parameter is missing from reports. Unhandled exception appear on generating reports
    '        Try
    '            objParmCurrentValue.Value = objHashTableParamter.Item(strParams)
    '            ''Getting the reference of the parameter added on the reports
    '            objParamterContainer = objCr.DataDefinition.ParameterFields
    '            'Setting the index of the paramter
    '            objParmName = objParamterContainer.Item(strParams)
    '            'Adding the value to the parameter
    '            objParmValues.Add(objParmCurrentValue)
    '            objParmName.ApplyCurrentValues(objParmValues)
    '        Catch ex As Exception
    '            Continue For
    '        End Try
    '    End If
    'Next

    ''-----------
    'For index As Integer = 0 To Me.crpt.ReportDefinition.Sections.Count - 1
    '    For intCounter As Integer = 0 To Me.crpt.ReportDefinition.Sections(index).ReportObjects.Count - 1
    '        With Me.crpt.ReportDefinition.Sections(index)
    '            If .ReportObjects(intCounter).Kind = CrystalDecisions.Shared.ReportObjectKind.SubreportObject Then
    '                mySubReportObject = CType(.ReportObjects(intCounter), CrystalDecisions.CrystalReports.Engine.SubreportObject)
    '                mySubRepDoc = mySubReportObject.OpenSubreport(mySubReportObject.SubreportName)
    '                For intCounter1 As Integer = 0 To mySubRepDoc.Database.Tables.Count - 1
    '                    mySubRepDoc.Database.Tables(intCounter1).ApplyLogOnInfo(crTableLogOnInfo)
    '                    mySubRepDoc.Database.Tables(intCounter1).ApplyLogOnInfo(crTableLogOnInfo)
    '                Next
    '            End If
    '        End With
    '    Next
    'Next
    ''-----------


    'End Sub

    'Private Sub funShowReport()

    '    crpt = New CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    crpt.FileName = str_ApplicationStartUpPath & "\Reports\" & Me.reportName & ".rpt"
    '    Try
    '       If crpt.Database.Tables.Count > 0 Then
    '            For Each myTable In crpt.Database.Tables
    '                myLogin = myTable.LogOnInfo
    '                myLogin.ConnectionInfo.DatabaseName = Me.DataBase
    '                myLogin.ConnectionInfo.Password = Me.Pass
    '                myLogin.ConnectionInfo.UserID = Me.UserId
    '                myLogin.ConnectionInfo.ServerName = Me.ServerName

    '                myTable.ApplyLogOnInfo(myLogin)

    '            Next
    '        End If


    '        CrystalReportViewer1.ReportSource = crpt

    '        If str_RptParam.Length > 1 Then
    '            Dim str() As String = str_RptParam.Split("&")
    '            For Each str1 As String In str
    '                Dim strParam() As String = str1.Split("|")
    '                crpt.SetParameterValue(strParam(0), Val(strParam(1)))

    '            Next
    '        End If


    '    Catch ex As Exception
    '        If ex.Message = "The operation completed successfully" Then Me.Close() : Exit Sub
    '        MsgBox(ex.Message)
    '        Me.Close()

    '    End Try

    'End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub CustomizeReport_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FileName1 As String = Me.reportName & ".rpt"
        Dim ModifiedFileName As String = ""
        Dim onlyReportName1 As String = String.Empty
        Dim attributes As IO.FileAttributes
        Try
            If (Not System.IO.Directory.Exists(Application.StartupPath & "\Reports\CutomizedReports\")) Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\Reports\CutomizedReports\")
            End If
            Dim LengthFn As Integer = FileName1.Length
            Dim NewFolderTobeCreated As String = FileName1.Substring(0, LengthFn - 4)
            Dim SubReportPath As String = Application.StartupPath & "\Reports\CutomizedReports\" & NewFolderTobeCreated
            If AlreadyNestedCustomizedFolder.Length > 0 Then
                SaveFileDialog1.InitialDirectory = AlreadyNestedCustomizedFolder
            Else
                If Not System.IO.Directory.Exists(SubReportPath) Then
                    System.IO.Directory.CreateDirectory(SubReportPath)
                End If
                SaveFileDialog1.InitialDirectory = SubReportPath
            End If
            SaveFileDialog1.FileName = FileName1
            SaveFileDialog1.Filter = "Crystal Report|*.rpt"
            SaveFileDialog1.DefaultExt = ".rpt"
            SaveFileDialog1.Title = "Save a Crystal Report"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim onlyReportName As String = SaveFileDialog1.FileName
                Dim lastIndex As Integer = onlyReportName.LastIndexOf("\")
                onlyReportName1 = onlyReportName.Substring(lastIndex + 1)
                If AlreadyNestedCustomizedFolder.Length > 0 Then
                    If Not IO.File.Exists(AlreadyNestedCustomizedFolder & "\" & onlyReportName1) = True Then
                        If IO.File.Exists(AlreadyNestedCustomizedFolder & "\" & FileName1) Then
                            IO.File.Copy(AlreadyNestedCustomizedFolder & "\" & FileName1, AlreadyNestedCustomizedFolder & "\" & onlyReportName1, True)
                        End If
                    End If
                    attributes = IO.File.GetAttributes(AlreadyNestedCustomizedFolder & "\" & onlyReportName1)
                    If (attributes And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then
                        attributes = RemoveAttribute(attributes, IO.FileAttributes.ReadOnly)
                        IO.File.SetAttributes(AlreadyNestedCustomizedFolder & "\" & onlyReportName1, attributes)
                    End If
                    Process.Start(AlreadyNestedCustomizedFolder & "\" & onlyReportName1)
                Else
                    If Not IO.File.Exists(Application.StartupPath & "\Reports\CutomizedReports\" & NewFolderTobeCreated & "\" & onlyReportName1) = True Then
                        If IO.File.Exists(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1) Then
                            IO.File.Copy(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1, Application.StartupPath & "\Reports\CutomizedReports\" & NewFolderTobeCreated & "\" & onlyReportName1, True)
                        Else
                            IO.File.Copy(Application.StartupPath & "\Reports\" & FileName1, Application.StartupPath & "\Reports\CutomizedReports\" & NewFolderTobeCreated & "\" & onlyReportName1)
                        End If
                    End If
                    attributes = IO.File.GetAttributes(Application.StartupPath & "\Reports\CutomizedReports\" & NewFolderTobeCreated & "\" & onlyReportName1)
                    If (attributes And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then
                        attributes = RemoveAttribute(attributes, IO.FileAttributes.ReadOnly)
                        IO.File.SetAttributes(Application.StartupPath & "\Reports\CutomizedReports\" & NewFolderTobeCreated & "\" & onlyReportName1, attributes)
                    End If
                    Process.Start(Application.StartupPath & "\Reports\CutomizedReports\" & NewFolderTobeCreated & "\" & onlyReportName1)
                End If
            Else
                Dim var() As String = System.IO.Directory.GetFiles(AlreadyNestedCustomizedFolder)
                If System.IO.Directory.Exists(AlreadyNestedCustomizedFolder) Then
                    If var.Length = 0 Then
                        System.IO.Directory.Delete(AlreadyNestedCustomizedFolder)
                    End If
                End If
            End If
            Me.Close()
            AlreadyNestedCustomizedFolder = ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


        '''' Prior Code
        'Dim FileName1 As String = Me.reportName & ".rpt"
        'Try

        '    If (Not System.IO.Directory.Exists(Application.StartupPath & "\Reports\CutomizedReports\")) Then
        '        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Reports\CutomizedReports\")
        '    End If

        '    If Not IO.File.Exists(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1) = True Then

        '        IO.File.Copy(Application.StartupPath & "\Reports\" & FileName1, Application.StartupPath & "\Reports\CutomizedReports\" & FileName1)
        '    End If

        '    Dim attributes As IO.FileAttributes
        '    attributes = IO.File.GetAttributes(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1)

        '    If (attributes And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then
        '        attributes = RemoveAttribute(attributes, IO.FileAttributes.ReadOnly)
        '        IO.File.SetAttributes(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1, attributes)
        '    End If

        '    Process.Start(Application.StartupPath & "\Reports\CutomizedReports\" & FileName1)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Public Function RemoveAttribute(ByVal attributes As IO.FileAttributes, ByVal attributesToRemove As IO.FileAttributes) As IO.FileAttributes
        Return attributes And (Not attributesToRemove)
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
            If Not Directory.Exists(_FileExportPath) Then
                Directory.CreateDirectory(_FileExportPath)
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

    Public Function GetCompanyName(ByVal Company As SBModel.CompanyInfo) As Boolean
        Try
            If Company.CompanyID = MyCompanyId Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
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
    Sub PostVoucher()
        Try
            Dim VD As New VouchersDAL
            VD.PostVoucher(VoucherIDForPost)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rptReportViewerNew_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'If IO.Directory.Exists(Application.StartupPath & "\Reports\CutomizedReports\" & CustomizedReportFolderName) Then

            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ReportToPDF(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            'Try
            '    IO.File.Delete(_FileExportPath)
            'Catch ex As Exception

            'End Try
            Dim CRExportOpts As ExportOptions
            Dim CRDiskFileOpts As New DiskFileDestinationOptions
            Dim CRFormatTypeOpts As New PdfRtfWordFormatOptions
            If Not Directory.Exists(_FileExportPath) Then
                Directory.CreateDirectory(_FileExportPath)
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
            PDFPath = str_Path
            'Dim frm As New frmOutgoingEmail
            'frm.txtDataFile.Text = str_Path
            'frm.txtTo.Text = _ToEmailId
            'frm.StartPosition = FormStartPosition.CenterScreen
            'frm.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class