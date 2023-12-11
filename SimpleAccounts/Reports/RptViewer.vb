
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
'Imports SIRIUSUtility.Globel
Public Class RptViewer

    'Public Class RptViewer
    'Dim crpt As CrystalReport1
    Dim str_RptParam As String
    ' Dim SBg As New SIRIUSUtility.Globel
    Dim crpt As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
    Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo
    Dim Param As CrystalDecisions.Shared.ParameterFields


    Public reportName As String
    Dim ServerName As String = ConServerName
    Dim UserId As String = ConUserId
    Dim Pass As String = ConPassword
    Dim DataBase As String = Con.Database
    Public ShowHeader As Boolean = False
    Public ShowAddress As Boolean = False
    Public Company_Name As String = String.Empty
    Public Company_Address As String = String.Empty
    Public ParamFrom As String = String.Empty
    Public ParamTo As String = String.Empty
    Public ParamOpeningBalance As String = String.Empty
    Public SendToPrinter As Boolean
    Public prgBarReportOld As New ToolStripProgressBar
    Private Sub RptViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each Ctrl As Control In CrystalReportViewer1.Controls
            If TypeOf Ctrl Is ToolStrip Then
                prgBarReportOld.Name = "prgBarReportOld"
                prgBarReportOld.Alignment = ToolStripItemAlignment.Right
                prgBarReportOld.Style = ProgressBarStyle.Marquee
                CType(Ctrl, ToolStrip).Items.Add(prgBarReportOld)
            End If
        Next
        Me.prgBarReportOld.Visible = True
        prgBarRpt = CType(prgBarReportOld.ProgressBar, ProgressBar)

        Me.str_RptParam = str_ReportParam
        str_ReportParam = ""

        Me.funShowReport()

        If Me.SendToPrinter = True Then
            If Me.reportName.Contains("SalesInvoice") Then CrystalReportViewer1.RefreshReport() : crpt.PrintToPrinter(1, False, 0, 0) : Me.Close() : Exit Sub
            'crpt.PrintToPrinter(1, False, 0, 0) : Me.Close()
            Me.CrystalReportViewer1.PrintReport()
            Me.Close()
        Else
            'CrystalReportViewer1.RefreshReport() : crpt.PrintToPrinter(1, False, 0, 0) : Me.Close()
            Me.Visible = True
        End If

    End Sub
    Private Sub funShowReport()
        crpt = New CrystalDecisions.CrystalReports.Engine.ReportDocument
        ' Me.LoadSettingsFrom(My.Application.Info.DirectoryPath + "\DefaultValues.txt")
      
        crpt.FileName = str_ApplicationStartUpPath & "\Reports\" & Me.reportName & ".rpt"

         'crpt.SetParameterValue("@ToDate", "2008.12.30")
        Try
            'If Me.ShowAddress = False Then
            '    crpt.Subreports("ReportHeader.rpt").ReportDefinition.ReportObjects.Item(1).ObjectFormat.EnableSuppress = True

            'End If

            'If Me.ShowHeader = False Then
            '    crpt.ReportDefinition.Areas(0).AreaFormat.EnableSuppress = True
            'End If
            If crpt.Database.Tables.Count > 0 Then
                For Each myTable In crpt.Database.Tables
                    myLogin = myTable.LogOnInfo
                    myLogin.ConnectionInfo.DatabaseName = Me.DataBase
                    myLogin.ConnectionInfo.Password = Me.Pass
                    myLogin.ConnectionInfo.UserID = Me.UserId
                    myLogin.ConnectionInfo.ServerName = Me.ServerName

                    myTable.ApplyLogOnInfo(myLogin)

                Next
            End If
            

            CrystalReportViewer1.ReportSource = crpt

            If str_RptParam.Length > 1 Then
                Dim str() As String = str_RptParam.Split("&")
                For Each str1 As String In str
                    Dim strParam() As String = str1.Split("|")
                    crpt.SetParameterValue(strParam(0), strParam(1))

                Next
            End If


            Try
                If Company_Name.Length > 1 Then crpt.SetParameterValue("@CompanyName", Company_Name)
                If Company_Address.Length > 1 Then crpt.SetParameterValue("@CompanyAddress", Company_Address)
                crpt.SetParameterValue("@ShowHeader", ShowHeader)
            Catch ex As Exception
                'if not exist @CompanyName Parameter in Report Formated
                ShowHeader = False
                Company_Name = String.Empty
                Company_Address = String.Empty
            End Try

            If ParamFrom.Length > 1 Then crpt.SetParameterValue("@FromDate", ParamFrom)
            If ParamTo.Length > 1 Then crpt.SetParameterValue("@ToDate", ParamTo)
            If ParamOpeningBalance.Length > 1 Then
                crpt.SetParameterValue("OpeningBalance", ParamOpeningBalance)
            End If

        Catch ex As Exception
            If ex.Message = "The operation completed successfully" Then Me.Close() : Exit Sub
            ShowErrorMessage(ex.Message)
            Me.Close()
            'If MsgBox(ex.Message & ": Do you want to change report settings...?", MsgBoxStyle.YesNo, "Lumensoft Technologies") = MsgBoxResult.Yes Then
            '    System.Diagnostics.Process.Start(Application.StartupPath & "\DefaultValues.txt")
            '    MsgBox("Please Re Load report after changing settings", MsgBoxStyle.Information, "Lumensoft Technologies")
            'End If
        End Try

     

    End Sub

    'Private Sub ReLoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReLoadToolStripMenuItem.Click
    '    Me.funShowReport()
    'End Sub

    Sub LoadSettingsFrom(ByVal sFileName As String)
        Dim DS As New DataSet
        If Not My.Computer.FileSystem.FileExists(sFileName) Then
            'MessageBox.Show("Settings file '" + sFileName + "' does not exists.", "File not exists", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If (My.Computer.FileSystem.FileExists(sFileName)) Then
            DS.ReadXml(sFileName)

            Dim DT As DataTable
            'Load Preset combo box
            DT = DS.Tables("Preset")
            'If (Not IsNothing(DT)) Then
            '    txtPreset.DisplayMember = "Server"
            '    txtPreset.ValueMember = "Server"
            '    txtPreset.DataSource = DT
            'End If


            'Fill default values for server, database etc.
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                Me.reportName = DT.Rows(0)("Report")
                Me.ServerName = DT.Rows(0)("Server")
                Me.UserId = DT.Rows(0)("UserName")
                Me.Pass = DT.Rows(0)("Password")
                Me.DataBase = DT.Rows(0)("DBName")
                Me.ShowHeader = DT.Rows(0)("ShowHeader")
                Me.ShowAddress = DT.Rows(0)("ShowAddress")
            Else
                Me.reportName = "c:\inetpub\wwwroot\LLMS\Reports\rptColoRequest.rpt"
                Me.ServerName = "Rizwan"
                Me.UserId = "Rai"
                Me.Pass = "Rai"
            End If

            'If Not IsNothing(DS) Then
            '    ReadTablesFromSettingsDataset(DS, dgTables, "table")
            '    ReadTablesFromSettingsDataset(DS, dgViews, "view")
            '    ReadTablesFromSettingsDataset(DS, dgSps, "sp")
            '    ReadTablesFromSettingsDataset(DS, dgUDFs, "udf")
            '    ReadTablesFromSettingsDataset(DS, dgUDDs, "udd")
            '    ReadTablesFromSettingsDataset(DS, dgUsers, "user")
            'End If

        End If
    End Sub

    'Private Sub ChangeSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeSettingsToolStripMenuItem.Click
    '    System.Diagnostics.Process.Start(Application.StartupPath & "\DefaultValues.txt")
    'End Sub

End Class